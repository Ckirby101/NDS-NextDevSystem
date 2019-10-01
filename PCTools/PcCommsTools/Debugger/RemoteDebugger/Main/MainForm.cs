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

*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using RemoteDebugger;
using RemoteDebugger.Dialogs;
using RemoteDebugger.Docks;
using RemoteDebugger.Main;
using WeifenLuo.WinFormsUI.Docking;

namespace RemoteDebugger
{
    public partial class MainForm : Form
    {
	    public static int[] banks = new int[8];



	    public static SourceCodeView sourceCodeView;


        DockPanel dockPanel;
        //public static ButtonBar myButtonBar;
        //public static LogView myLog;
	    public static Registers myNewRegisters;
	    public static Watch myWatchWindow;
	    public static Disassembly myDisassembly;
	    public static SourceWindow mySourceWindow;
	    public static RegMemWatch myMemoryWatch;
	    public static MemWatch myMemWatch;
	    public static callstack mycallstack;
	    public static Watches myWatches;
        SpectrumScreen myScreen;
        public static Breakpoint myBreakpoints;
        //public static List<BaseDock> myDocks;

        bool refreshScreen;

	    public static string TraceDataPath = "";

        public MainForm()
        {
	        Program.myMainForm = this;
            //myDocks = new List<BaseDock>();
            InitializeComponent();

	        sourceCodeView = new SourceCodeView();



			//spawn source window and dock it
	        mySourceWindow = new SourceWindow("","");
	        mySourceWindow.TopLevel = false;
			LeftsplitContainer.Panel1.Controls.Add(mySourceWindow);
	        mySourceWindow.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	        mySourceWindow.Dock = DockStyle.Fill;
	        mySourceWindow.Show();

	        //spawn log
	        //myLog = new LogView("","");
	        //myLog.TopLevel = false;
	        //LeftsplitContainer.Panel2.Controls.Add(myLog);
	        //myLog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	        //myLog.Dock = DockStyle.Fill;
	        //myLog.Show();



			//spawn register window
	        myNewRegisters = new Registers("","");
	        myNewRegisters.TopLevel = false;
	        RightFlow.Controls.Add(myNewRegisters);
	        myNewRegisters.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	        //myNewRegisters.Dock = DockStyle.Fill;
	        myNewRegisters.Show();

			//add dissasembly window
	        myDisassembly = new Disassembly("","");
	        myDisassembly.TopLevel = false;
	        RightFlow.Controls.Add(myDisassembly);
	        myDisassembly.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	        myDisassembly.Dock = DockStyle.Fill;
	        myDisassembly.Show();


	        //add watch window
	        myWatchWindow = new Watch();
	        myWatchWindow.TopLevel = false;
	        RightFlow.Controls.Add(myWatchWindow);
	        myWatchWindow.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	        myWatchWindow.Dock = DockStyle.Fill;
	        myWatchWindow.Show();




            //this.IsMdiContainer = true;
            //this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            //this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.Controls.Add(this.dockPanel);

            UpdateStatus();

			/*
            if (File.Exists("layout.xml"))
            {
                dockPanel.LoadFromXml("layout.xml",DelegateHandler);
            }
            else
            {
                myButtonBar = new ButtonBar("Control");
                myLogs.Add(new LogView("Log", "Log"));
                myNewRegisters = new Registers("Registers", "Registers");
                myDisassembly = new Disassembly("Disassembly", "Disassembly");
                myScreen = new SpectrumScreen("Screen", "Screen");
                myBreakpoints = new Breakpoint("Breakpoints", "Breakpoints");
                myDocks.Add(new SpriteView("Sprite Patterns", "SpritePatterns"));

                myButtonBar.Show(this.dockPanel, DockState.DockTop);
                myNewRegisters.Show(this.dockPanel, DockState.DockLeft);
                myDisassembly.Show(this.dockPanel, DockState.DockRight);
                myLogs[0].Show(this.dockPanel, DockState.DockBottom);
                myBreakpoints.Show(this.dockPanel, DockState.DockLeft);
                myScreen.Show(this.dockPanel, DockState.DockRight);
                myDocks[0].Show(this.dockPanel, DockState.Float);
            }
			*/
            //Program.telnetConnection.SendCommand("help", null);
            refreshScreen = false;


	        if (!string.IsNullOrEmpty(TraceDataPath))
	        {
		        if (System.IO.File.Exists(TraceDataPath))
		        {

			        TraceFile.ParseTraceData(TraceDataPath);
			        if (mySourceWindow!=null)
			        {
				        mySourceWindow.InitSourceWindow(Path.GetDirectoryName(TraceDataPath));
			        }



		        }

	        }

            Invalidate();
        }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Endians. </summary>
	    ///
	    /// <remarks> 12/09/2018. </remarks>
	    ///
	    /// <param name="v"> An int to process. </param>
	    ///
	    /// <returns> An int. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static int Endian(int v)
	    {
		    return ((v & 0xff) << 8) | ((v & 0xff00) >> 8);
	    }


	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Parse expression. </summary>
	    ///
	    /// <remarks> 13/09/2018. </remarks>
	    ///
	    /// <param name="s"> The string. </param>
	    ///
	    /// <returns> An int. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static bool ParseExpression(string s,ref int  v)
	    {
		    int value = 0;

		    NumberStyles style = NumberStyles.Any;
		    if (s.Contains("0x") || s.Contains("#") || s.Contains("$"))
		    {
			    s = s.Replace("#", "").Replace("$", "").Replace("0x","");

			    //hex
			    style = NumberStyles.HexNumber;
		    }

		    if (!int.TryParse(s, style, null, out value))
		    {
			    return false;
		    }


		    v = value;

		    return true;

	    }



/*        public IDockContent DelegateHandler(string name)
        {
            string[] split = name.Split(':');
            switch (split[0])
            {
                case "Control":
                    if (myButtonBar==null)
                    {
                        myButtonBar = new ButtonBar("Control");
                    }
                    return myButtonBar;
                case "Log":
                    {
                        foreach (LogView lv in myLogs)
                        {
                            if (lv.viewName == name)
                                return lv;
                        }
                        LogView t = new LogView("Log", "Log");
                        myLogs.Add(t);
                        return t;
                    }
                //case "Registers":
                //    if (myNewRegisters==null)
                //    {
                //        myNewRegisters = new Registers("Registers", "Registers");
                //    }
                //    return myNewRegisters;
                case "Disassembly":
                    if (myDisassembly == null)
                    {
                        myDisassembly = new Disassembly("Disassembly", "Disassembly");
                    }
                    return myDisassembly;
	            case "SourceWindow":
		            if (mySourceWindow == null)
		            {
			            mySourceWindow = new SourceWindow("Source Window", "SourceWindow");
		            }
		            return myDisassembly;
                case "Screen":
                    if (myScreen == null)
                    {
                        myScreen = new SpectrumScreen("Screen", "Screen");
                    }
                    return myScreen;
                case "Breakpoints":
                    if (myBreakpoints == null)
                    {
                        myBreakpoints = new Breakpoint("Breakpoints", "Breakpoints");
                    }
                    return myBreakpoints;
                case "SpritePatterns":
                    {
                        foreach (BaseDock sv in myDocks)
                        {
                            if (sv.viewName == name)
                                return sv;
                        }
                        SpriteView t = new SpriteView("Sprite Patterns", "SpritePatterns");
                        myDocks.Add(t);
                        return t;
                    }
                default:
                    break;
            }
            return null;
        }
*/
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dockPanel.SaveAsXml("layout.xml");
            //Program.telnetConnection.CloseConnection();
        }

        private void newRegisterViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (myNewRegisters == null)
            //{
                //myNewRegisters = new Registers("Registers", "Registers");
                //myNewRegisters.Show(dockPanel, DockState.Float);
            //}
        }

        private void UpdateStatus()
        {
            toolStripStatusLabel1.Text = Program.serialport.GetStatus();
        }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Event handler. Called by timer1 for tick events. </summary>
	    ///
	    /// <remarks> 09/09/2018. </remarks>
	    ///
	    /// <param name="sender"> Source of the event. </param>
	    /// <param name="e">	  Event information. </param>
	    /// -------------------------------------------------------------------------------------------------
	    private void timer1_Tick(object sender, EventArgs e)
	    {
            UpdateStatus();

		    if (Program.InStepMode) return;


		    UpdateAllWindows(false);
	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Updates all windows described by fromstep. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <param name="fromstep"> True to fromstep. </param>
	    /// -------------------------------------------------------------------------------------------------
	    public void UpdateAllWindows(bool fromstep)
	    {

            if (fromstep)
                Program.StepBusy = true;


            //we yupdate registeres first because all other windows rely on registers
            Program.serialport.GetRegisters(RegisterUpdateCallback, 0);





        }

        // -------------------------------------------------------------------------------------------------
        // Registers the update callback
        //
        // \param   response    The response.
        // \param   tag         The tag.
        // -------------------------------------------------------------------------------------------------
        public void RegisterUpdateCallback(byte[] response, int tag)
        {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker) delegate { RegisterUpdate(response, tag); });
                }
                else
                {
                    RegisterUpdate(response, tag);
                }
        }

        private void RegisterUpdate(byte[] response, int tag)
        {
            myNewRegisters.regcallback(response,tag);

                //UpdateBreakpoints();

                if (myDisassembly != null)
                {
                    int pc = 0;
                    if (myNewRegisters != null)
                    {
                        pc = myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);
                        int bank = MainForm.banks[ TraceFile.GetBankIndex(pc) ];
                        if (Program.InStepMode)
                        {
                            TraceFile.SetPC(pc,bank,true);
                        }
                    }
                    myDisassembly.RequestUpdate(pc);
                }


                MainForm.myMemoryWatch.UpdateMemory();
                MainForm.myMemWatch.UpdateMemory();
                MainForm.myWatches.UpdateWatches();
                MainForm.myBreakpoints.RequestUpdate();

                Program.StepBusy = false;



        }





	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Updates the after registers described by response. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <param name="response"> The response. </param>
	    /// -------------------------------------------------------------------------------------------------
	    public void UpdateAfterRegisters()
	    {
/*
		    if (UpdatePcFocus)
		    {

			    if (myMemWatch != null)
			    {
				    myMemWatch.UpdateMemory();
			    }

			    if (mycallstack != null)
			    {
				    mycallstack.UpdateCallStack();
			    }

				if (myWatches!=null)
					myWatches.Update();

		    }


		    if (mySourceWindow!=null)
		    {
			    if (myNewRegisters != null)
			    {
				    int pc = myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);
				    mySourceWindow.UpdatePC(pc, UpdatePcFocus);//Program.InStepMode);
			    }

		    }
*/
	    }


	    public void UpdateBreakpoints()
	    {
		    if (myBreakpoints != null)
		    {
			    myBreakpoints.RequestUpdate();
		    }
	    }


        private void newLogViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (myLogs.Count==0)
            //{
            //    LogView t = new LogView("Log", "Log");
            //    myLogs.Add(t);
            //    t.Show(dockPanel, DockState.Float);
            //}
        }

        private void newDisassemblyViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (myDisassembly == null)
            //{
            //    myDisassembly = new Disassembly("Disassembly", "Disassembly");
            //    myDisassembly.Show(dockPanel, DockState.Float);
            //}

        }

        private void newScreenViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myScreen == null)
            {
                myScreen = new SpectrumScreen("Screen", "Screen");
                myScreen.Show(dockPanel, DockState.Float);
            }

        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (AboutBox box = new AboutBox())
            //{
            //    box.ShowDialog(this);
            //}
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Settings settings = new Settings())
            {
                settings.ShowDialog(this);
            }
        }

        private void newBreakpointsViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myBreakpoints == null)
            {
                myBreakpoints = new Breakpoint();
                //myBreakpoints.Show(dockPanel, DockState.Float);
            }
        }

        private void newSpriteViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SpriteView t = new SpriteView("Sprite Patterns", "SpritePatterns");
            //t.Show(dockPanel, DockState.Float);
            //myDocks.Add(t);
        }

		private void newSourceWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mySourceWindow == null)
			{
				mySourceWindow = new SourceWindow("Source Window", "SourceWindow");
			}

			//mySourceWindow.Show(dockPanel, DockState.Float);

		}

		private void parseTraceDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Trace Files|tracedata.txt";  
			openFileDialog1.Title = "Select a Trace File";  
			openFileDialog1.CheckFileExists = true;
			if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)  
			{  
				TraceDataPath = openFileDialog1.FileName;
				TraceFile.ParseTraceData(openFileDialog1.FileName);
				if (mySourceWindow!=null)
				{
					mySourceWindow.InitSourceWindow(Path.GetDirectoryName(openFileDialog1.FileName));
				}

			} 



			//mapFile = new MapFile("C:\\Users\\ckirb\\Documents\\Spectrum Next\\bombjack\\bombjack.map");

			//InitTabs();

		}

        private void disasmHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (Disassembly dis = new Disassembly("dis","dis2"))
            {
                dis.ShowDialog(this);
            }

        }
    }
}
