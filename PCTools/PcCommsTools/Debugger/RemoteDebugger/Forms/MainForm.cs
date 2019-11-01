
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using RemoteDebugger;
using RemoteDebugger.Dialogs;
using RemoteDebugger.Docks;
using RemoteDebugger.Main;
using SendNex;
using WeifenLuo.WinFormsUI.Docking;

namespace RemoteDebugger
{
    public partial class MainForm : Form
    {
	    public static int[] banks = new int[8];

	    public static SourceCodeView sourceCodeView;

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
        public static Breakpoint myBreakpoints;
        //public static List<BaseDock> myDocks;


	    public static string TraceDataPath = "";

        // -------------------------------------------------------------------------------------------------
        // Default constructor
        // -------------------------------------------------------------------------------------------------
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


            UpdateStatus();



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

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by Form1 for load events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by Form1 for form closing events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Form closing event information.
        // -------------------------------------------------------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dockPanel.SaveAsXml("layout.xml");
            //Program.telnetConnection.CloseConnection();
        }

        // -------------------------------------------------------------------------------------------------
        // Updates the status
        // -------------------------------------------------------------------------------------------------
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
            Program.serialport.GetRegisters(
                delegate(byte[] response, int tag)
                { 
                    Invoke((MethodInvoker)delegate { RegisterUpdate(response,tag); });
                }
                , 0);

        }



        // -------------------------------------------------------------------------------------------------
        // Registers the update
        //
        // \param   response
        // The response.
        // \param   tag
        // The tag.
        // -------------------------------------------------------------------------------------------------
        private void RegisterUpdate(byte[] response, int tag)
        {
            myNewRegisters.regcallback(response,tag);


            if (Program.InStepMode) FocusPC();

            MainForm.myBreakpoints.RequestUpdate();

            MainForm.myMemoryWatch.UpdateMemory();
            MainForm.myMemWatch.UpdateMemory();
            MainForm.myWatches.UpdateWatches();
            MainForm.mycallstack.UpdateCallStack();

            int pc = myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);
            myDisassembly.RequestUpdate(pc);

            Program.StepBusy = false;



        }

        public void FocusPC()
        {
            if (myNewRegisters == null) return;

            int pc = myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);
            int bank = MainForm.banks[ TraceFile.GetBankIndex(pc) ]; 
            TraceFile.SetPC(pc,bank,true);
  
        }

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by aboutToolStripMenuItem for click events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (AboutBox box = new AboutBox())
            //{
            //    box.ShowDialog(this);
            //}
        }




        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by parseTraceDataToolStripMenuItem for click events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
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



		}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by disasmHelpToolStripMenuItem for click events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
        private void disasmHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (Disassembly dis = new Disassembly("dis","dis2"))
            {
                dis.ShowDialog(this);
            }

        }

        private void transmitNexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Nex Files|*.nex";  
            openFileDialog1.Title = "Select a Nex File";  
            openFileDialog1.CheckFileExists = true;
            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)  
            {  
                TraceDataPath = openFileDialog1.FileName;
                TraceFile.ParseTraceData(openFileDialog1.FileName);
                if (mySourceWindow!=null)
                {
                    Program.serialport.Pause(true);

                    while (Program.serialport.busy())
                    {
                    }

                    NexReader.SendNext(Program.serialport.GetSerialPort(), openFileDialog1.FileName);
                    Program.serialport.Pause(false);

                    //mySourceWindow.InitSourceWindow(Path.GetDirectoryName(openFileDialog1.FileName));
                }

            } 

        }
    }
}
