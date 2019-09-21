using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RemoteDebugger.Main;

namespace RemoteDebugger
{
	public partial class SourceWindow  : Form
	{

		public string viewName;
		public SourceWindow(string name, string viewname)
		{
			viewName = viewname;
			InitializeComponent();

			List<TraceFile> tf = new List<TraceFile>();
			
			//tf.Add( new TraceFile("dma.asm") );


		}

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

		private void breakbutton_Click(object sender, EventArgs e)
		{
			SwapMode(!Program.InStepMode);
		}
		private void continuebutton_Click(object sender, EventArgs e)
		{
			SwapMode(!Program.InStepMode);
		}


		public void SwapMode(bool pause)
		{
			if (Program.InStepMode && !pause)
			{
				Program.telnetConnection.SendCommand("disable-breakpoints", commandResponse);
				Program.telnetConnection.SendCommand("exit-cpu-step", commandResponseStepUpdate);
				Program.InStepMode = false;
				breakbutton.Visible = true;
				continuebutton.Visible = false;
			}
			if (!Program.InStepMode && pause)
			{
				Program.telnetConnection.SendCommand("enter-cpu-step", commandResponse);
				Program.telnetConnection.SendCommand("enable-breakpoints", commandResponseStepUpdate);
				Program.InStepMode = true;
				breakbutton.Visible = false;
				continuebutton.Visible = true;
			}

			UpdateSourceButtons();

		}
		void commandResponse(string[] s,int tag)
		{
			Console.WriteLine(s[0]);
		}

		public void UpdateSourceButtons()
		{
			buttonRunBreak.Visible = false;
			if (Program.InStepMode)
			{
				//only enable if there is breakpoints.. otherwise emualtor goes a bit mad
				if (Breakpoint.NumBreakpointsActive() >0)
					buttonRunBreak.Visible = true;

			}

		}


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


		private void stepbutton_Click(object sender, EventArgs e)
		{
			Program.telnetConnection.SendCommand("cpu-step", commandResponseStepUpdate);

		}

		private void stepoverbutton_Click(object sender, EventArgs e)
		{
			string line = MainForm.myDisassembly.GetCurrentLineCode().ToLower().TrimStart();

			if (line.StartsWith("jp") || line.StartsWith("jr") || line.StartsWith("ret") || line.StartsWith("reti"))
				Program.telnetConnection.SendCommand("cpu-step", commandResponseStepUpdate);
			else
				Program.telnetConnection.SendCommand("cpu-step-over", commandResponseStepUpdate);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Program.telnetConnection.SendCommand("run", commandResponseStepUpdate);

		}

		private void FunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (FunctionComboBox.SelectedIndex < 0) return;

			TraceFile.GotoLabel(Labels.labels[ FunctionComboBox.SelectedIndex ]);
		}
	}
}
