// -------------------------------------------------------------------------------------------------
// \file    SendMemory.cs.
//
// Implements the send memory class
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NDesk.Options;
using SendNex;

namespace SendMemory
{
    // A send memory.
    class SendMemory
    {
        // True to show, false to hide the help
        public static bool show_help = false;
        // The file
        public static string file = "";
        // The com
        public static string com = "COM3";
        public static int speed = 1958400;        // The bank
        public static int bank = -1;
        // The mmu
        public static int mmu = -1;

        // The address
        public static int addr = 0;

        // -------------------------------------------------------------------------------------------------
        // Main entry-point for this application
        //
        // \param   args    An array of command-line argument strings.
        // -------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {

            Console.WriteLine("NDS SendMemory by C.Kirby 0.1");


            

            OptionSet p = new OptionSet
            {
                {
                    "c|com=", "com port",
                    v => com = v
                },
                {
                    "s|speed=", "Serial Speed e.g 115200,1958400 etc Defaults to 1958400",
                    v => speed = int.Parse(v)
                },
                {
                    "f|file=", "bin filename max 8192 bytes",
                    v => file = v
                },
                {
                    "a|addr=", "memory address offset in bank to write data to 0-8191",
                    v => addr = int.Parse(v)
                },
                {
                    "b|bank=", "bank number to send data",
                    v => bank = int.Parse(v)
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
                Console.WriteLine("Try `SendMemory --help' for more information.");
                return;
            }


            if (show_help)
            {
                ShowHelp(p);
                return;
            }



            SerialPort mySerialPort = new SerialPort(com, speed, Parity.None, 8, StopBits.One);

            mySerialPort.Open();

            if (!mySerialPort.IsOpen)
            {
                Console.WriteLine("Failed to open port.");
                return;
            }

            byte[] filebytes = File.ReadAllBytes(file);

            if (filebytes.Length <= 0)
            {
                Console.WriteLine("Failed to read data");
                return;
            }
            if ((filebytes.Length+addr) >= 8192)
            {
                Console.WriteLine("data file too big or addr offset incorrect");
                return;
            }
            if (addr < 0 || addr >=8192)
            {
                Console.WriteLine("addr offset incorrect 0-8191 only");
                return;
            }

            if (bank <0)
            {
                Console.WriteLine("Bank must be specified");
                return;
            }

            List<byte> b = new List<byte>();

            //break execution
            NexReader.AddCommand(ref b,186);
            NexReader.SendData(mySerialPort,ref b);
            Thread.Sleep(50);


            NexReader.AddCommand(ref b,181);
            NexReader.Add8Value(ref b,bank);

            NexReader.Add16Value(ref b,0xE000+addr);
            NexReader.Add16Value(ref b,filebytes.Length);




            Console.WriteLine("Sending "+filebytes.Length+" bytes to 0x"+addr.ToString("X4")+" in mmu bank "+bank);

            for (int i = 0; i < filebytes.Length; i++)
            {
                b.Add((byte)filebytes[i]);
            }


            NexReader.SendData(mySerialPort, ref b);

            int ok =  NexReader.ReadByteData(mySerialPort);
            int clo =  NexReader.ReadByteData(mySerialPort);
            int chi =  NexReader.ReadByteData(mySerialPort);


            if (clo != 0 || chi != 0)
            {
                Console.WriteLine("Send Error ");
                throw new Exception();
            }
            if (ok != 0)
            {
                Console.WriteLine("Timeout Error!");
                throw new Exception();
            }

            Console.WriteLine("ok");



            mySerialPort.Close();
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
            Console.WriteLine("Usage: SendMemory [OPTIONS]+");

            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

    }
}
