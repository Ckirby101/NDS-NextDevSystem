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
        // The bank
        public static int bank = -1;
        // The mmu
        public static int mmu = -1;

        // The address
        public static int addr = -1;

        // -------------------------------------------------------------------------------------------------
        // Main entry-point for this application
        //
        // \param   args    An array of command-line argument strings.
        // -------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {

            Console.WriteLine("SendMemory by C.Kirby 0.1");


            

            OptionSet p = new OptionSet
            {
                {
                    "c|com=", "com port",
                    v => com = v
                },
                {
                    "f|file=", "bin filename",
                    v => file = v
                },
                {
                    "a|addr=", "hex memory address to write data to",
                    v => addr = int.Parse(v)
                },
                {
                    "b|bank=", "bank number",
                    v => bank = int.Parse(v)
                },
                {
                    "m|mmu=", "mmu slot 0-7",
                    v => mmu = int.Parse(v)
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



            SerialPort mySerialPort = new SerialPort(com, 921600, Parity.None, 8, StopBits.One);

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


            List<byte> b = new List<byte>();
            byte[] data;

            if (bank != -1 && mmu != -1)
            {
                AddCommand(ref b,180);
                Add8Value(ref b,mmu&7);
                Add8Value(ref b,bank);

                Console.WriteLine("Sending mmu:"+mmu+" & bank:"+bank);
                data = b.ToArray();
                mySerialPort.Write(data, 0, data.Length);
                Thread.Sleep(50);
            }





            AddCommand(ref b,181);
            Add16Value(ref b,addr);
            Add16Value(ref b,filebytes.Length);



            Console.WriteLine("Sending "+filebytes.Length+"bytes to 0x"+addr.ToString("X"));

            for (int i = 0; i < filebytes.Length; i++)
            {
                b.Add((byte)filebytes[i]);

            }

            data = b.ToArray();
            mySerialPort.Write(data, 0, data.Length);
            Thread.Sleep(350);


            
            mySerialPort.Close();
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
