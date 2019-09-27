// -------------------------------------------------------------------------------------------------
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
            Cmd_SendMem	= 185,		//send memeory to pc
            Cmd_Pause	= 186,		//send memeory to pc
            Cmd_Continue = 187,
            Cmd_Step = 188,
            
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
        public ConcurrentQueue<string> messages;
        //public bool connected;
        //public bool JustConnected;
        //public bool remoteIsPaused;
        //string host;
        //int port;


        
        // The sendbuffer
        private List<byte> sendbuffer = new List<byte>();
//        private List<byte> readbuffer = new List<byte>();
        // my serial port
        private SerialPort mySerialPort;

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

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                ReadConsumer();
            }).Start();



        }




        // Finaliser
        ~Serial()
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();

            }

        }


        public string GetStatus()
        {
            string v = "";
            if (mySerialPort.IsOpen) v = v + "Connected @" + mySerialPort.BaudRate+" | Commands in Que:"+commands.Count;

            return v;

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

            SendCommand(UARTCommand.Cmd_SendRegs, sendbuffer.ToArray(), cb,92, tag,0,0);

        }


        public void GetMemory(SerialCallback cb,int addr,int bytes,int tag = 0)
        {
            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_SendMem);
            Add16Value(ref sendbuffer,addr);
            Add16Value(ref sendbuffer,bytes);

            SendCommand(UARTCommand.Cmd_SendMem, sendbuffer.ToArray(), cb,bytes+5, tag,addr,bytes);

        }


        public void PauseExecution(SerialCallback cb,bool pause)
        {
            sendbuffer.Clear();
            if (pause)
            {
                AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_Pause);
                SendCommand(UARTCommand.Cmd_Pause, sendbuffer.ToArray(), cb,0, 0,0,0);
            }
            else
            {
                AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_Continue);
                SendCommand(UARTCommand.Cmd_Continue, sendbuffer.ToArray(), cb,0, 0,0,0);
            }

        }


        public void Step(SerialCallback cb, int addr)
        {
            sendbuffer.Clear();
            AddCommand(ref sendbuffer, (int) UARTCommand.Cmd_Step);
            Add16Value(ref sendbuffer,addr);

            SendCommand(UARTCommand.Cmd_Step, sendbuffer.ToArray(), cb,0, 0,0,0);
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

                        sc.callback(returnbytes, sc.tag);
                    }

                }
            }
        }



    }
}
