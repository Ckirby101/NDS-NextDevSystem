/*
 
The MIT License (MIT) 
 
Copyright (c) 2017 Savoury SnaX 
 
Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions: 
 
The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software. 
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
SOFTWARE. 
"C:\Users\ckirb\Documents\Spectrum Next\Beast\tracedata.txt"
*/
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NDesk.Options;
using RemoteDebugger.Remote;

//-trace="C:\Users\ckirb\Documents\Spectrum Next\tetris\tracedata.txt"
// -trace="C:\Users\ckirb\Documents\Spectrum Next\UART\UARTCOMMS\NDS\tracedata.txt"

namespace RemoteDebugger
{


    static class Program
    {
        public static bool show_help = false;
        public static string tracefile = "";
        public static int SerialSpeed = 1958400;
        public static string SerialPort = "COM4";

        //public static TelNetSpec telnetConnection=new TelNetSpec();
        public static bool InStepMode = false;
        public static bool StepBusy = false;
	    public static MainForm myMainForm;
        public static Serial serialport;
  

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Console.WriteLine("SendMemory by C.Kirby 0.1");


            

            OptionSet p = new OptionSet
            {

                {
                    "t|trace=", "trace file from assembler",
                    v => tracefile = v
                },
                {
                    "s|speed=", "Serial Speed e.g 115200,1958400 etc Defaults to 1958400",
                    v => SerialSpeed = int.Parse(v)
                },
                {
                    "p|port=", "Com port e.g COM1 COM2 etc Defaults to COM4",
                    v => SerialPort = v
                },
                {
                    "h|help", "show this message and exit",
                    v => show_help = v != null
                }
            };


            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `NDS (Next Development System) Remote Debugger --help' for more information.");
                return;
            }

            if (show_help)
            {
                ShowHelp(p);
                return;
            }


	        if (!string.IsNullOrEmpty(tracefile))
	        {
		        MainForm.TraceDataPath = tracefile;
	        }

            
            serialport = new Serial(SerialSpeed,SerialPort);


			Breakpoint.InitBreakpointData();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

	        myMainForm = new MainForm();



            Application.Run( myMainForm );



        }
        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: RemoteDebugger [OPTIONS]+");

            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }



}
