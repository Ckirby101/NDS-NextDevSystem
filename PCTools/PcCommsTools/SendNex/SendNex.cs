// -------------------------------------------------------------------------------------------------
// \file    SendNex.cs.
//
// Implements the send nex class
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NDesk.Options;

namespace SendNex
{
    // A send nex.
    class SendNex
    {



       // True to show, false to hide the help
       public static bool show_help = false;
        // The file
        public static string file = "";
        // The com
        public static string com = "COM3";
        // my serial port
        public static SerialPort mySerialPort;
        // The bytesent
        public static int bytesent;


        // -------------------------------------------------------------------------------------------------
        // Main entry-point for this application
        //
        // \param   args    An array of command-line argument strings.
        // -------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {

            Console.WriteLine("SendNex by C.Kirby 0.1");


            

            OptionSet p = new OptionSet
            {
                {
                    "c|com=", "com port",
                    v => com = v
                },
                {
                    "f|file=", "nex filename",
                    v => file = v
                },
                {
                    "h|help", "show this message and exit",
                    v => show_help = v != null
                }
            };


            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `SendNex --help' for more information.");
                return;
            }


            if (show_help)
            {
                ShowHelp(p);
                return;
            }


            mySerialPort = new SerialPort(com, 921600, Parity.None, 8, StopBits.One);


            mySerialPort.Open();

            if (!mySerialPort.IsOpen)
            {
                Console.WriteLine("Failed to open port.");
                return;
            }


            Stopwatch sw = new Stopwatch();
            sw.Restart();          //So you dont have to call sw.Reset()

            using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open)))
            {

                List<byte> b = new List<byte>();

                NexReader nex= new NexReader(br);
                NexReader.Header header = nex.ReadHeader();

                Console.WriteLine("Opening nex file = "+header.Next+" "+header.VersionNumber);


                Console.WriteLine("PC Address : 0x"+header.PC.ToString("X"));
                Console.WriteLine("SP Address : 0x"+header.SP.ToString("X"));


                for (int i = 0; i < header.Banks.Length; i++)
                {
                    int bank = GetRealBank(i);


                    
                    if (header.Banks[bank] != 0)
                    {
                        bank *= 2;

                        for (int j = 0; j < 2; j++)
                        {
                            Console.WriteLine("Sending 8k mmu bank : "+bank);

                            AddCommand(ref b,180);
                            Add8Value(ref b,7);
                            Add8Value(ref b,bank);
                            //SendData(ref b);

                            AddCommand(ref b,181);
                            Add16Value(ref b,0xE000);
                            Add16Value(ref b,8192);

                            nex.AddData(ref b, 8192);

                            bank++;
                        }

                        SendData(ref b);

                    }

                }


                Console.WriteLine("Setting entry bank to "+header.EntryBank);

                AddCommand(ref b,180);
                Add8Value(ref b,6);
                Add8Value(ref b,header.EntryBank);
                //SendData(ref b);
                AddCommand(ref b,180);
                Add8Value(ref b,7);
                Add8Value(ref b,header.EntryBank+1);
                //SendData(ref b);



                AddCommand(ref b, 182); //execute!
                Add16Value(ref b,header.SP);
                Add16Value(ref b,header.PC);
                SendData(ref b);

                Console.WriteLine("Finished!");
            }


            sw.Stop();

            Thread.Sleep(100);

            mySerialPort.Close();

            Console.WriteLine("Sent "+(bytesent/1024)+"k in "+(sw.ElapsedMilliseconds / 1000.0f) +" seconds");



        }

        // -------------------------------------------------------------------------------------------------
        // Gets real bank
        //
        // \param   index   Zero-based index of the.
        //
        // \return  The real bank.
        // -------------------------------------------------------------------------------------------------
        private static int GetRealBank(int index)
        {
            int[] banks = {5, 2, 0, 1, 3, 4, 6, 7};
            if (index > 7) return index;
            return (banks[index]);

        }


        // -------------------------------------------------------------------------------------------------
        // Adds a command to 'num'
        //
        // \param [in,out]  b   A List&lt;byte&gt; to process.
        // \param           num Number of.
        // -------------------------------------------------------------------------------------------------
        private static void AddCommand(ref List<byte> b, int num)
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
        private static void Add16Value(ref List<byte> b, int value)
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
        private static void Add8Value(ref List<byte> b, int value)
        {
            b.Add((byte)(value&0xff));          //lo
        }

        // -------------------------------------------------------------------------------------------------
        // Sends a data
        //
        // \param [in,out]  b   A List&lt;byte&gt; to process.
        // -------------------------------------------------------------------------------------------------
        private static void SendData(ref List<byte> b)
        {


            //Console.WriteLine("SendData "+b.Count);
            byte[] data = b.ToArray();
            mySerialPort.Write(data, 0, data.Length);
            bytesent += data.Length;
            while (mySerialPort.BytesToWrite>512)
                Thread.Sleep(2);
            b.Clear();
        }




        // -------------------------------------------------------------------------------------------------
        // Shows the help.
        //
        // \param   p   An OptionSet to process.
        //
        // ### remarks  8/28/2019.
        // -------------------------------------------------------------------------------------------------
        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: SendNex [OPTIONS]+");

            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
