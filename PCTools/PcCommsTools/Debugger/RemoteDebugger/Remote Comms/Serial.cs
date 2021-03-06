﻿// -------------------------------------------------------------------------------------------------
// \file    Remote\Serial.cs.
//
// Implements the serial class
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDebugger.Remote
{
    // A serial.
    class Serial
    {

        public enum UARTCommand
        {
            Cmd_SetBank	= 180,
            Cmd_PutData	= 181,
            Cmd_Execute	= 182,

            Cmd_SendRegs	= 183,		//send registers to pc
            Cmd_SetRegs	= 184,		//set the registers
            Cmd_SendMem	= 185,		//send memory to pc
            Cmd_Pause	= 186,
            Cmd_Continue = 187,
            Cmd_SetBreakpoint = 188,
            Cmd_SendWatch = 189,
            Cmd_GetBreakpoint = 190,
            Cmd_RemoveBreakpoint = 191,
            
        }


        public delegate void SerialCallback(byte[] response,int tag);
        public class SerialCommand
        {
            public SerialCommand(UARTCommand uart,byte[] c, SerialCallback cb,int wantBytes, int _tag=0,int _a0=0,int _a1=0)
            {
                sendcommand = c;
                callback = cb;
                tag = _tag;
                uartCommand = uart;
                returnbytes = wantBytes;
                a0 = _a0;
                a1 = _a1;
            }
            //public ConcurrentQueue<string> callback;
            public byte[] sendcommand;
            public SerialCallback callback;
            public int tag=0;
            public UARTCommand uartCommand;
            public int returnbytes = 0;

            public int a0 = 0;
            public int a1 = 0;
        }





        ConcurrentQueue<SerialCommand> commands;

        // The sendbuffer
        private List<byte> sendbuffer = new List<byte>();

        private SerialPort mySerialPort;

        private Thread serialThread;

        private bool pause = false;


        public SerialPort GetSerialPort()
        {
            return mySerialPort;
        }
        // -------------------------------------------------------------------------------------------------
        // Constructor
        //
        // \param   baudrate    (Optional) The baudrate.
        // \param   comport     (Optional) The comport.
        // -------------------------------------------------------------------------------------------------
        public Serial(int baudrate = 921600, string comport= "COM4")
        {
            mySerialPort = new SerialPort(comport, baudrate, Parity.None, 8, StopBits.One);
            mySerialPort.Open();


            //remoteIsPaused = false;
            //port = 0;
            //host = "";
            //connected = false;
            //JustConnected = false;
            //messages = new ConcurrentQueue<string>();
            commands = new ConcurrentQueue<SerialCommand>();

            serialThread =  new Thread(ReadConsumer);
            serialThread.IsBackground = true;
            serialThread.Start();

//            new Thread(() =>
 //           {
 //               Thread.CurrentThread.IsBackground = true;
 //               ReadConsumer();
 //           }).Start();



        }




        // Finaliser
        ~Serial()
        {
            serialThread.Abort();

            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();

            }

        }


        public void Pause(bool _pause)
        {
            pause = _pause;
        }


        public string GetStatus()
        {
            string v = "";
            if (mySerialPort.IsOpen) v = v + "Connected @" + mySerialPort.BaudRate+" | Commands in Que:"+commands.Count;

            return v;

        }

        public bool busy()
        {
            if (!mySerialPort.IsOpen) return true;
            if (commands.Count > 0) return true;
            return false;
        }

        // -------------------------------------------------------------------------------------------------
        // Ready byte
        //
        // \return  An int.
        // -------------------------------------------------------------------------------------------------
        private int ReadyByte()
        {
            return mySerialPort.BytesToRead;
        }




        // -------------------------------------------------------------------------------------------------
        // Gets the byte
        //
        // \return  The byte.
        // -------------------------------------------------------------------------------------------------
        private byte GetByte()
        {
            if (mySerialPort.BytesToRead > 0)
            {
                return (byte) mySerialPort.ReadByte();
            }

            return 0;
        }

        // -------------------------------------------------------------------------------------------------
        // Sends the bytes
        //
        // \param   data    The data.
        // -------------------------------------------------------------------------------------------------
        private void SendBytes(byte[] data)
        {
            mySerialPort.Write(data, 0, data.Length);
        }


        // -------------------------------------------------------------------------------------------------
        // Adds a command to 'num'
        //
        // \param [in,out]  b   A List&lt;byte&gt; to process.
        // \param           num Number of.
        // -------------------------------------------------------------------------------------------------
        private  void AddCommand(ref List<byte> b, int num)
        {
            b.Add((byte)'C');
            b.Add((byte)'M');
            b.Add((byte)'D');
            b.Add((byte)num);

        }

        // -------------------------------------------------------------------------------------------------
        // Adds a 16 value to 'value'
        //
        // \param [in,out]  b       A List&lt;byte&gt; to process.
        // \param           value   The value.
        // -------------------------------------------------------------------------------------------------
        private  void Add16Value(ref List<byte> b, int value)
        {
            b.Add((byte)(value&0xff));          //lo
            b.Add((byte)((value&0xff00)>>8) );     //hi
        }
        // -------------------------------------------------------------------------------------------------
        // Adds a 8 value to 'value'
        //
        // \param [in,out]  b       A List&lt;byte&gt; to process.
        // \param           value   The value.
        // -------------------------------------------------------------------------------------------------
        private  void Add8Value(ref List<byte> b, int value)
        {
            b.Add((byte)(value&0xff));          //lo
        }

        public static int Get16Bit(ref byte[] b, ref int index)
        {
            int value = b[index] | (b[index + 1] << 8);
            index += 2;

            return value;
        }

        public static  int Get8Bit(ref byte[] b, ref int index)
        {
            int value = b[index];
            index ++;

            return value;
        }
        // -------------------------------------------------------------------------------------------------
        // Gets the registers
        //
        // \param   cb  The cb.
        // \param   tag (Optional) The tag.
        // -------------------------------------------------------------------------------------------------
        public void GetRegisters(SerialCallback cb,int tag = 0)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_SendRegs);

            SendCommand(UARTCommand.Cmd_SendRegs, sendbuffer.ToArray(), cb,93, tag,0,0);

        }


        // -------------------------------------------------------------------------------------------------
        // Gets a memory
        //
        // \param   cb      The cb.
        // \param   addr    The address.
        // \param   bytes   The bytes.
        // \param   tag     (Optional) The tag.
        // -------------------------------------------------------------------------------------------------
        public void GetMemory(SerialCallback cb,int addr,int bytes,int bank,int tag = 0)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_SendMem);
            Add8Value(ref sendbuffer,bank);
            Add16Value(ref sendbuffer,addr);
            Add16Value(ref sendbuffer,bytes);

            SendCommand(UARTCommand.Cmd_SendMem, sendbuffer.ToArray(), cb,bytes+5, tag,addr,bytes);

        }

        // -------------------------------------------------------------------------------------------------
        // Sends a watch
        //
        // \param   cb      The cb.
        // \param   addr    The address.
        // \param   bank    The bank.
        // \param   tag     (Optional) The tag.
        // -------------------------------------------------------------------------------------------------
        public void SendWatch(SerialCallback cb,int addr,int bank,int tag = 0)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_SendWatch);
            Add8Value(ref sendbuffer,bank);
            Add16Value(ref sendbuffer,addr);

            SendCommand(UARTCommand.Cmd_SendWatch, sendbuffer.ToArray(), cb,5, tag,addr,0);

        }


        // -------------------------------------------------------------------------------------------------
        // Gets the breakpoints
        //
        // \param   cb  The cb.
        // \param   tag (Optional) The tag.
        // -------------------------------------------------------------------------------------------------
        public void GetBreakpoints(SerialCallback cb,int tag = 0)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_GetBreakpoint);

            SendCommand(UARTCommand.Cmd_GetBreakpoint, sendbuffer.ToArray(), cb,53, tag,0,0);

        }


        // -------------------------------------------------------------------------------------------------
        // Pause execution
        //
        // \param   cb      The cb.
        // \param   pause   True to pause.
        // -------------------------------------------------------------------------------------------------
        public void PauseExecution(SerialCallback cb,bool pause)
        {

            sendbuffer.Clear();
            if (pause)
            {
                Program.StepBusy = true;
                AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_Pause);
                SendCommand(UARTCommand.Cmd_Pause, sendbuffer.ToArray(), cb,0, 0,0,0);
            }
            else
            {
                AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_Continue);
                SendCommand(UARTCommand.Cmd_Continue, sendbuffer.ToArray(), cb,0, 0,0,0);
            }

        }


        // -------------------------------------------------------------------------------------------------
        // Steps
        //
        // \param   cb      The cb.
        // \param   addr    The address.
        // -------------------------------------------------------------------------------------------------
        public void SetBreakpoint(SerialCallback cb, int addr,int bank)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_SetBreakpoint);
            Add8Value(ref sendbuffer,bank);
            Add16Value(ref sendbuffer,addr);

            SendCommand(UARTCommand.Cmd_SetBreakpoint, sendbuffer.ToArray(), cb,0, 0,0,0);
        }

        public void RemoveBreakpoint(SerialCallback cb, int addr,int bank)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_RemoveBreakpoint);
            Add8Value(ref sendbuffer,bank);
            Add16Value(ref sendbuffer,addr);

            SendCommand(UARTCommand.Cmd_RemoveBreakpoint, sendbuffer.ToArray(), cb,0, 0,0,0);
        }


        public void SendRegisters(SerialCallback cb)
        {

            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_SetRegs);
            Add8Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.f_e) );
            Add8Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.a_e) );
            Add8Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.r) );
            Add8Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.i) );
            Add8Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.f) );
            Add8Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.a) );

            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.iy) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.ix) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.bc_e) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.de_e) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.hl_e) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.bc) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.de) );
            Add16Value(ref sendbuffer,MainForm.myNewRegisters.GetRegisterValueint( Registers.Z80Register.hl) );

            SendCommand(UARTCommand.Cmd_SetRegs, sendbuffer.ToArray(), cb,0, 0,0,0);

        }


        // -------------------------------------------------------------------------------------------------
        // Sends a command
        //
        // \param   uart        The uart.
        // \param   sendBytes   The send in bytes.
        // \param   cb          The cb.
        // \param   wantbytes   The wantbytes.
        // \param   tag         (Optional) The tag.
        // -------------------------------------------------------------------------------------------------
        public void SendCommand(UARTCommand uart,byte[] sendBytes, SerialCallback cb, int wantbytes ,int tag ,int a0,int a1)
        {
            if (pause) return;

            if (commands.Count > 50) return;
            if (!mySerialPort.IsOpen) return;

            SerialCommand t = new SerialCommand(uart,sendBytes, cb, wantbytes,tag,a0,a1);
            commands.Enqueue(t);
        }


        // -------------------------------------------------------------------------------------------------
        // Reads the consumer
        // -------------------------------------------------------------------------------------------------
        void ReadConsumer()
        {

            while (true)
            {
                SerialCommand sc;

                Thread.Sleep(10);

                //is port open
                if (mySerialPort.IsOpen)
                {
                    if (commands.TryDequeue(out sc))
                    {
                        Console.WriteLine("Deque "+sc.uartCommand.ToString());
                        //got a command
                        SendBytes(sc.sendcommand);

                        //wait until all bytes sent
                        while (mySerialPort.BytesToWrite >0)
                        {
                            Thread.Sleep(5);
                        }

                        while (mySerialPort.BytesToRead <sc.returnbytes)
                        {
                            Thread.Sleep(5);
                        }

                        //read all the bytes in receive buffer
                        byte[] returnbytes = new byte[sc.returnbytes];

                        mySerialPort.Read(returnbytes, 0, sc.returnbytes);



                        //sc.callback.Invoke(returnbytes, sc.tag);
                        if (sc.callback!=null)
                            sc.callback(returnbytes, sc.tag);
                    }
                    else
                    {
                    }

                }
            }
        }



    }
}
