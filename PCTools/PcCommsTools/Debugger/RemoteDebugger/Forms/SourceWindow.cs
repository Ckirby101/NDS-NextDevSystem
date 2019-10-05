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


            cf.codewindow.Lines[line].EnsureVisible();
            var start = cf.codewindow.Lines[line - (linesOnScreen / 3)].Position;
            var end = cf.codewindow.Lines[line + (linesOnScreen / 3)].Position;

            cf.codewindow.ScrollRange(start, end);


            cf.codewindow.Lines[line + (linesOnScreen / 3)].EnsureVisible();
            cf.codewindow.Lines[line - (linesOnScreen / 3)].EnsureVisible();

            cf.codewindow.Lines[line].EnsureVisible();
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

			foreach (Labels.Label l in Labels.labels )
            {
                if (!l.nextAddress.isFixedValue())
                {
                    FunctionComboBox.Items.Add(l);
                }

			}

		}



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
                Program.serialport.PauseExecution(
                    delegate(byte[] response, int tag)
                    { 
                        Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(true); });
                    }
                    ,false);
            }
			if (!Program.InStepMode && pause)
            {
                Program.serialport.PauseExecution(
                    delegate(byte[] response, int tag)
                    { 
                        Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(true); });
                    }
                    ,true);
            }


			UpdateSourceButtons();

		}


        // -------------------------------------------------------------------------------------------------
        // Updates the pause status described by mode
        //
        // \param   mode    The mode.
        // -------------------------------------------------------------------------------------------------
        public void UpdatePauseStatus(int mode)
        {
            if (mode == 1 && !Program.InStepMode)
            {
                Program.InStepMode = true;
                UpdateSourceButtons();

            }

            if (mode == 0 && Program.InStepMode)
            {
                Program.InStepMode = false;
                UpdateSourceButtons();

            }

        }

        // -------------------------------------------------------------------------------------------------
        // Continue execution
        // -------------------------------------------------------------------------------------------------
        private void ContinueExecution()
        {
            Program.serialport.PauseExecution(null,false);
            Program.myMainForm.UpdateAllWindows(true);
        }


        // -------------------------------------------------------------------------------------------------
        // Updates the source buttons
        // -------------------------------------------------------------------------------------------------
		public void UpdateSourceButtons()
		{

            if (!Program.InStepMode)
            {
                //not in step mode
                breakbutton.Visible = true;
                continuebutton.Visible = false;

            }
            else
            {
                //are in step mode
                breakbutton.Visible = false;
                continuebutton.Visible = true;

                stepbutton.Visible = true;
                stepoverbutton.Visible = true;
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

            //get the address of where to run to
            int breakpointAddress = MainForm.myDisassembly.GetStepAddress();
            int bank = NextAddress.GetBankFromAddress(ref MainForm.banks, breakpointAddress);

            Program.serialport.SetBreakpoint(
                delegate(byte[] response, int tag)
                { 
                    Invoke((MethodInvoker)delegate { ContinueExecution(); });
                }
                ,breakpointAddress,bank);


        }

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by stepoverbutton for click events
        //
        // \param   sender  Source of the event.
        // \param   e       Event information.
        // -------------------------------------------------------------------------------------------------
		private void stepoverbutton_Click(object sender, EventArgs e)
		{

            int breakpointAddress = MainForm.myDisassembly.GetStepOverAddress();
            int bank = NextAddress.GetBankFromAddress(ref MainForm.banks, breakpointAddress);


            Program.serialport.SetBreakpoint(
                delegate(byte[] response, int tag)
                { 
                    Invoke((MethodInvoker)delegate { ContinueExecution(); });
                }
                ,breakpointAddress,bank);
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

			TraceFile.GotoLabel( (Labels.Label)FunctionComboBox.Items[ FunctionComboBox.SelectedIndex ]);
		}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by SourceTab for tab index changed events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
        private void SourceTab_TabIndexChanged(object sender, EventArgs e)
        {
        }

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by SourceTab for selected index changed events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
        private void SourceTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;

            TraceFile tf = TraceFile.GetByTabPage(current);
            if (tf == null) return;

            MainForm.sourceCodeView.UpdateMarginAddress(tf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.myMainForm.FocusPC();
        }
    }
}
