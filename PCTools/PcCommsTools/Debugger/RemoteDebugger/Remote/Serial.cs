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
            Cmd_GetMem	= 185,		//send memeory to pc
            
        }


        public delegate void SerialCallback(byte[] response,int tag);
        public class SerialCommand
        {
            public SerialCommand(UARTCommand uart,byte[] c, SerialCallback cb,int wantBytes, int _tag=0)
            {
                sendcommand = c;
                callback = cb;
                tag = _tag;
                uartCommand = uart;
                returnbytes = wantBytes;
            }
            //public ConcurrentQueue<string> callback;
            public byte[] sendcommand;
            public SerialCallback callback;
            public int tag=0;
            public UARTCommand uartCommand;
            public int returnbytes = 0;
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
        public Serial(int baudrate = 921600, string comport= "COM3")
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

            SendCommand(UARTCommand.Cmd_SendRegs, sendbuffer.ToArray(), cb,84, tag);

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
        public void SendCommand(UARTCommand uart,byte[] sendBytes, SerialCallback cb, int wantbytes ,int tag = 0)
        {
            SerialCommand t = new SerialCommand(uart,sendBytes, cb, wantbytes,tag);
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

                Thread.Sleep(100);

                //is port open
                if (mySerialPort.IsOpen)
                {
                    if (commands.TryDequeue(out sc))
                    {
                        //got a command
                        SendBytes(sc.sendcommand);

                        while (mySerialPort.BytesToRead <sc.returnbytes)
                        {
                            Thread.Sleep(10);
                        }

                        //read all the bytes in receive buffer
                        byte[] returnbytes = new byte[sc.returnbytes];

                        mySerialPort.Read(returnbytes, 0, sc.returnbytes);

                        sc.callback(returnbytes, sc.tag);
                    }

                }
            }
        }


    }
}
