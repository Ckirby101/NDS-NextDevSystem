// -------------------------------------------------------------------------------------------------
// \file    Docks\SourceWindow.cs.
//
// Implements the source Windows Form
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RemoteDebugger.Main;

namespace RemoteDebugger
{
    // Form for viewing the source.
	public partial class SourceWindow  : Form
	{

        // Name of the view
		public string viewName;
        // -------------------------------------------------------------------------------------------------
        // Constructor
        //
        // \param   name        The name.
        // \param   viewname    The viewname.
        // -------------------------------------------------------------------------------------------------
		public SourceWindow(string name, string viewname)
		{
			viewName = viewname;
			InitializeComponent();

			List<TraceFile> tf = new List<TraceFile>();
			
			//tf.Add( new TraceFile("dma.asm") );


		}

        // -------------------------------------------------------------------------------------------------
        // Focus line
        //
        // \param   cf      The cf.
        // \param   line    The line.
        // -------------------------------------------------------------------------------------------------
		public void FocusLine(SourceCodeView.CodeFile cf, int line)
		{
			//Console.WriteLine("Focus Line!");
			SourceTab.SelectedTab = cf.tab;
			int linesOnScreen = cf.codewindow.LinesOnScreen - 2; // Fudge factor

			//line += (linesOnScreen / 2);
			cf.codewindow.Lines[line].EnsureVisible();


			//cf.codewindow.Size = Size;





			var start = cf.codewindow.Lines[line - (linesOnScreen / 2)].Position;
			var end = cf.codewindow.Lines[line + (linesOnScreen / 2)].Position;


			//if (end>cf.codewindow.)

			//Console.WriteLine(cf.codewindow.Size.Height+"  "+Size.Height+"  "+linesOnScreen+"     "+start+"-"+end);

			//cf.codewindow.ScrollRange(cf.codewindow.Lines.Count, cf.codewindow.Lines.Count);
			cf.codewindow.ScrollRange(cf.codewindow.Lines[line-1].Position, cf.codewindow.Lines[line+1].Position);

			//Form1.Instance.FocusOnFile(s, ld.lineNumber);

		}



        // -------------------------------------------------------------------------------------------------
        // Initialises the source window
        //
        // \param   path    Full pathname of the file.
        // -------------------------------------------------------------------------------------------------
		public void InitSourceWindow(string path)
		{
			MainForm.sourceCodeView.initSourceFiles(TraceFile.traceFiles.ToArray(), SourceTab, path);


			FunctionComboBox.Items.Clear();
			int index = 0;
			foreach (Labels.Label l in Labels.labels )
			{

				string s = l.label + "  $" + l.address.ToString("X4") + " : " + l.bank;
				FunctionComboBox.Items.Add(s);


				index++;
			}

		}

        // -------------------------------------------------------------------------------------------------
        // Updates the PC
        //
        // \param   pc      The PC.
        // \param   focus   True to focus.
        // -------------------------------------------------------------------------------------------------
		public void UpdatePC(int pc,bool focus)
		{
			TraceFile.SetPC(pc,focus);
		}

		//private void toolStripButton1_Click(object sender, EventArgs e)
		//{

		//}

		//private void fillpanel_Paint(object sender, PaintEventArgs e)
		//{

		//}

		//private void button1_Click(object sender, EventArgs e)
		//{

		//}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by breakbutton for click events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void breakbutton_Click(object sender, EventArgs e)
		{
			SwapMode(!Program.InStepMode);
		}
        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by continuebutton for click events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void continuebutton_Click(object sender, EventArgs e)
		{
			SwapMode(!Program.InStepMode);
		}


        // -------------------------------------------------------------------------------------------------
        // Swap mode
        //
        // \param   pause   True to pause.
        // -------------------------------------------------------------------------------------------------
		public void SwapMode(bool pause)
		{
			if (Program.InStepMode && !pause)
			{
                Program.serialport.PauseExecution(PauseExecutionCallback,false);
				//Program.telnetConnection.SendCommand("disable-breakpoints", commandResponse);
				//Program.telnetConnection.SendCommand("exit-cpu-step", commandResponseStepUpdate);

			}
			if (!Program.InStepMode && pause)
            {
                Program.serialport.PauseExecution(PauseExecutionCallback,true);


				//Program.telnetConnection.SendCommand("enter-cpu-step", commandResponse);
				//Program.telnetConnection.SendCommand("enable-breakpoints", commandResponseStepUpdate);

			}


			UpdateSourceButtons();

		}

        // -------------------------------------------------------------------------------------------------
        // Callback, called when the pause execution
        //
        // \param   response    The response.
        // \param   tag         The tag.
        // -------------------------------------------------------------------------------------------------
        private void PauseExecutionCallback(byte[] response, int tag)
        {
            if (Program.InStepMode)
            {

                try
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(true); });
                    }
                    else
                    {
                        Program.myMainForm.UpdateAllWindows(true);
                    }
                }
                catch
                {
                
                }

            }

        }

        private void StepCallback(byte[] response, int tag)
        {
            if (Program.InStepMode)
            {

                try
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(true); });
                    }
                    else
                    {
                        Program.myMainForm.UpdateAllWindows(true);
                    }
                }
                catch
                {
                
                }

            }

        }



        // -------------------------------------------------------------------------------------------------
        // Command response
        //
        // \param   s   A string[] to process.
        // \param   tag The tag.
        // -------------------------------------------------------------------------------------------------
		void commandResponse(string[] s,int tag)
		{
			Console.WriteLine(s[0]);
		}

        // Updates the source buttons
		public void UpdateSourceButtons()
		{
            buttonRunBreak.Visible = false;

            
            if (Program.InStepMode)
            {
                Program.InStepMode = false;
                breakbutton.Visible = true;
                continuebutton.Visible = false;

                if (Breakpoint.NumBreakpointsActive() >0)
                    buttonRunBreak.Visible = true;

            }
            else
            {
                Program.InStepMode = true;
                breakbutton.Visible = false;
                continuebutton.Visible = true;
            }



		}


        // -------------------------------------------------------------------------------------------------
        // Command response step update
        //
        // \param   s   A string[] to process.
        // \param   tag The tag.
        // -------------------------------------------------------------------------------------------------
		void commandResponseStepUpdate(string[] s,int tag)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(true); });
				}
				else
				{
					Program.myMainForm.UpdateAllWindows(true);
				}
			}
			catch
			{
                
			}



			
		}


        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by stepbutton for click events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void stepbutton_Click(object sender, EventArgs e)
		{
			//Program.telnetConnection.SendCommand("cpu-step", commandResponseStepUpdate);

            //get the address of where to run to
            int breakpointAddress = MainForm.myDisassembly.GetStepAddress();


            Program.serialport.Step(StepCallback,breakpointAddress);


        }

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by stepoverbutton for click events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void stepoverbutton_Click(object sender, EventArgs e)
		{
			string line = MainForm.myDisassembly.GetCurrentLineCode().ToLower().TrimStart();

			if (line.StartsWith("jp") || line.StartsWith("jr") || line.StartsWith("ret") || line.StartsWith("reti"))
				Program.telnetConnection.SendCommand("cpu-step", commandResponseStepUpdate);
			else
				Program.telnetConnection.SendCommand("cpu-step-over", commandResponseStepUpdate);
		}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by button1 for click events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e)
		{
			Program.telnetConnection.SendCommand("run", commandResponseStepUpdate);

		}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by FunctionComboBox for selected index changed events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void FunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (FunctionComboBox.SelectedIndex < 0) return;

			TraceFile.GotoLabel(Labels.labels[ FunctionComboBox.SelectedIndex ]);
		}
	}
}
