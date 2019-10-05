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
        public static int speed = 1958400;
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

            Console.WriteLine("NDS SendNex by C.Kirby 0.1");


            

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


            mySerialPort = new SerialPort(com, speed, Parity.None, 8, StopBits.One);
            mySerialPort.Open();
            Console.WriteLine("Write Buffer="+mySerialPort.WriteBufferSize + " | Read Buffer="+mySerialPort.ReadBufferSize);

            if (!mySerialPort.IsOpen)
            {
                Console.WriteLine("Failed to open port.");
                return;
            }


            Stopwatch sw = new Stopwatch();
            sw.Restart();          //So you dont have to call sw.Reset()

            NexReader.SendNext(mySerialPort, file);

            sw.Stop();
            Thread.Sleep(10);
            mySerialPort.Close();

            Console.WriteLine("Sent "+(bytesent/1024)+"k in "+(sw.ElapsedMilliseconds / 1000.0f) +" seconds");



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
