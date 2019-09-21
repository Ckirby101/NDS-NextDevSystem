﻿/*
 
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

*/
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RemoteDebugger.Remote;

namespace RemoteDebugger
{


    static class Program
    {


        public static TelNetSpec telnetConnection=new TelNetSpec();
        public static bool InStepMode = false;
	    public static MainForm myMainForm;
        public static Serial serialport;
  

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

	        if (args.Length == 1)
	        {
		        MainForm.TraceDataPath = args[0];
	        }

            serialport = new Serial(921600,"COM3");

			Breakpoint.InitBreakpointData();
            telnetConnection.UpdateSettings(Properties.Settings.Default.remoteAddress, Properties.Settings.Default.remotePort);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

	        myMainForm = new MainForm();



            Application.Run( myMainForm );



        }
    }
}
