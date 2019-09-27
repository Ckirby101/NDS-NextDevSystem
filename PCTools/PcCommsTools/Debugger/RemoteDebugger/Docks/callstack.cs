using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteDebugger.Main;

namespace RemoteDebugger.Docks
{
	public partial class callstack : Form
	{
		private Regex regex;
		private Regex findhexregex;

		public callstack()
		{
			regex = new Regex(@"([0-9A-F]+)H\s*");

			findhexregex = new Regex(@".*\$([0-9A-Fa-f]{4})");


			InitializeComponent();
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates the call stack. </summary>
		///
		/// <remarks> 11/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public void UpdateCallStack()
		{
			//Program.telnetConnection.SendCommand("get-stack-backtrace ", Callback,0);
		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Callbacks. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		void Callback(string[] response,int tag)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke((MethodInvoker)delegate { UIUpdate(response,tag); });
				}
				else
				{
					UIUpdate(response,tag);
				}
			}
			catch
			{
                
			}
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates this object. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		private void UIUpdate(string[] response,int tag)
		{
			if (response.Length != 1) return;


			MatchCollection  m = regex.Matches(response[0]);
			callstacklistbox.Items.Clear();

			int offset;
			Labels.Label l;
			string indent = "";
			for (int i = (m.Count-1); i >= 0; i--)
			{
				int addr;

				if (!string.IsNullOrEmpty(m[i].Value))
				{
					int.TryParse(m[i].Value.Replace("H",""), NumberStyles.HexNumber, null, out addr);


					string s = "";
					if (Labels.GetFunctionWithOffsetBanked(addr,out l,out offset))
					{
						if (offset!=0)
							s = indent+"┕ $"+addr.ToString("X4")+"   "+l.label+"+"+offset;
						else
							s = indent+"┕ $"+addr.ToString("X4")+"   "+l.label;



					}
					else
					{
						s = indent+"┕ $"+addr.ToString("X4");
						
					}

					callstacklistbox.Items.Add(s);
					indent = indent + "  ";

				}




			}

			//now do current location

			int pc = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);
			string pcs = "";
			if (Labels.GetFunctionWithOffset(pc,out l,out offset))
			{
				if (offset!=0)
					pcs = indent+"┕ PC = $"+pc.ToString("X4")+"   "+l.label+"+"+offset;
				else
					pcs = indent+"┕ PC =$"+pc.ToString("X4")+"   "+l.label;



			}
			else
			{
				//s = indent+"> $"+addr.ToString("X4");
						
			}

			callstacklistbox.Items.Add(pcs);




			//byteProvider.parseData(response[0]);
			//MEMPTRHexControl.UpdateView();
		}


		private void callstacklistbox_DoubleClick(object sender, EventArgs e)
		{

			MouseEventArgs args = (MouseEventArgs) e;

			int index = this.callstacklistbox.IndexFromPoint(args.Location);
			if (index != System.Windows.Forms.ListBox.NoMatches)
			{

				Match m = findhexregex.Match(callstacklistbox.SelectedItem.ToString());

				if (m.Groups.Count >= 2)
				{
					int addr;
					if (int.TryParse(m.Groups[1].Value, NumberStyles.HexNumber, null, out addr))
					{


						TraceFile.GotoLine(addr);


					}

					//MessageBox.Show(callstacklistbox.SelectedItem.ToString());
				}
			}

		}
	}
}
