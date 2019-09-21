using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HexControlLibrary;
using RemoteDebugger.Dialogs;

namespace RemoteDebugger.Docks
{
	public partial class Watch : Form
	{
		public Watch()
		{
			InitializeComponent();

			//breakpointwindow
			Program.myMainForm.myBreakpoints = new Breakpoint();
			Program.myMainForm.myBreakpoints.TopLevel = false;
			tabBreakpoints.Controls.Add(Program.myMainForm.myBreakpoints);
			Program.myMainForm.myBreakpoints.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			Program.myMainForm.myBreakpoints.Dock = DockStyle.Fill;
			Program.myMainForm.myBreakpoints.Show();

			MainForm.myMemoryWatch = new RegMemWatch();
			MainForm.myMemoryWatch.TopLevel = false;
			tabMemory.Controls.Add(MainForm.myMemoryWatch);
			MainForm.myMemoryWatch.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MainForm.myMemoryWatch.Dock = DockStyle.Fill;
			MainForm.myMemoryWatch.Show();

			MainForm.myMemWatch = new MemWatch();
			MainForm.myMemWatch.TopLevel = false;
			tabMem.Controls.Add(MainForm.myMemWatch);
			MainForm.myMemWatch.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MainForm.myMemWatch.Dock = DockStyle.Fill;
			MainForm.myMemWatch.Show();


			MainForm.mycallstack = new callstack();
			MainForm.mycallstack.TopLevel = false;
			tabCallStack.Controls.Add(MainForm.mycallstack);
			MainForm.mycallstack.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MainForm.mycallstack.Dock = DockStyle.Fill;
			MainForm.mycallstack.Show();

		

			MainForm.myWatches = new Watches();
			MainForm.myWatches.TopLevel = false;
			tabWatch.Controls.Add(MainForm.myWatches);
			MainForm.myWatches.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MainForm.myWatches.Dock = DockStyle.Fill;
			MainForm.myWatches.Show();

			
			

			//MemoryHexControl1.Model.ByteProvider = new ByteProvider();
			//MemoryHexControl1.UpdateView();




		}

	}
}
