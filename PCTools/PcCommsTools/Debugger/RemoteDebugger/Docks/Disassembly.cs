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
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using eZDisasm;
using RemoteDebugger.Main;

namespace RemoteDebugger
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary> A disassembly. </summary>
    ///
    /// <remarks> 12/09/2018. </remarks>
    /// -------------------------------------------------------------------------------------------------
    public partial class Disassembly : Form
    {
        /// <summary> Information describing the disassembly. </summary>
        BindingList<DisassemblyData> disassemblyData;
        /// <summary> The dis RegEx. </summary>
        Regex disRegex;
	    /// <summary> The address RegEx. </summary>
	    Regex addrRegex;
        /// <summary> Name of the view. </summary>
        string viewName;

        private byte[] data;


        /// -------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        ///
        /// <remarks> 12/09/2018. </remarks>
        ///
        /// <param name="name">	    The name. </param>
        /// <param name="viewname"> The viewname. </param>
        /// -------------------------------------------------------------------------------------------------
        public Disassembly(string name, string viewname)
        {

            data = File.ReadAllBytes("out.bin");

            viewName = viewname;
            InitializeComponent();
            disassemblyData = new BindingList<DisassemblyData>();
            disRegex = new Regex(@"([0-9a-fA-F]{4})\s([0-9a-fA-F]*)\s*(.*)");   //gets the address and opcode hex
	        addrRegex = new Regex(@"([0-9a-fA-F]{4})");
            for (int a=0;a<30;a++)
            {
                disassemblyData.Add(new DisassemblyData() { Address = "0000", Value = "" });
            }

            DissasemblyDataGrid.DataSource = disassemblyData;

            DissasemblyDataGrid.Columns[0].ReadOnly = true;
            DissasemblyDataGrid.Columns[1].ReadOnly = true;
            DissasemblyDataGrid.AllowUserToAddRows = false;
            DissasemblyDataGrid.RowHeadersVisible = false;


            UIUpdate(null,0);
        }

        ~Disassembly()
        {
            // Your code
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Request update. </summary>
        ///
        /// <remarks> 12/09/2018. </remarks>
        ///
        /// <param name="address"> The address. </param>
        /// -------------------------------------------------------------------------------------------------
        public void RequestUpdate(int pc)
        {

	        int addr = TraceFile.GetCloestValidCodeAddress(pc - 10);

			if (addr<0)
	        {
		        Program.telnetConnection.SendCommand("d "+pc.ToString()+" 30", Callback,pc);
	        }
	        else
	        {
		        
		        Program.telnetConnection.SendCommand("d "+addr.ToString()+" 30", Callback,pc);
	        }


        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Updates the given items. </summary>
        ///
        /// <remarks> 12/09/2018. </remarks>
        ///
        /// <param name="items"> The items. </param>
        /// -------------------------------------------------------------------------------------------------
        void UIUpdate(string[] items,int pc)
        {
            int start = 0;
            int end = 100;
            int baseAddress = 0x4000;
            bool hasBaseAddress = true;
            bool adlMode = false;
            bool z80ClassicMode = false;
            bool addLabels = false;

            eZ80Disassembler.DisassembledInstruction[] instrs =
                eZ80Disassembler.Disassemble(data, start, end, baseAddress, hasBaseAddress, adlMode, z80ClassicMode, addLabels ? "label_" : "", addLabels ? "loc_" : "");

            int index = 0;
            foreach (eZ80Disassembler.DisassembledInstruction di in instrs)
            {
                if (index >= 30) break;

                int addr = (baseAddress + di.StartPosition);
                string addrstr = addr.ToString("X4")+"  ";

                Labels.Label l = Labels.GetLabel(addr);
                for (int i = 0; i < di.Length; i++)
                {
                    addrstr = addrstr + data[di.StartPosition + i].ToString("X2")+" ";
                }

                //add address label
                if (l != null)
                    addrstr = addrstr + l.label;
                disassemblyData[index].Address = addrstr ;


                // do value

                string dis = di.ToString();
                Match  m = addrRegex.Match(dis);

                if (m.Success)
                {
                    if (int.TryParse(m.Value, NumberStyles.AllowHexSpecifier, null, out addr))
                    {
                        l = null;
                        int offset;
                        if (Labels.GetLabelWithOffset(addr,out l, out offset))
                        {
                            if (Program.InStepMode)
                                MainForm.myWatches.AddLocalWatch(l);


                            if (offset == 0)
                            {
                                dis = dis.Replace(m.Value, l.label) + "  ;"+m.Value;
                            }
                            else
                            {
                                //dis = dis.Replace(m.Value, l.label+"+"+offset)+ "  ;"+m.Value;
                            }
                        }

                    }


                }
                disassemblyData[index].Value = dis;



                index++;
            }



/*
            //bool updated = false;
            for (int a=0;a<items.Count() && a<disassemblyData.Count();a++)
            {
                Match m = disRegex.Match(items[a]);
                if (m.Success)
                {
	                DissasemblyDataGrid.Rows[a].DefaultCellStyle.BackColor = System.Drawing.Color.White;

	                //group 3 is disassebly
	                int addr = 0;
	                Labels.Label l = null;
	                if (int.TryParse(m.Groups[1].Value, NumberStyles.AllowHexSpecifier, null, out addr))
	                {
		                l = Labels.GetLabel(addr);
	                }

					if (addr == pc)
						DissasemblyDataGrid.Rows[a].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;



	                if (l != null)
	                {
		                disassemblyData[a].Address = m.Groups[1].Value + " "+m.Groups[2].Value+" "+l.label+":";
	                }
	                else
	                {
		                disassemblyData[a].Address = m.Groups[1].Value + " "+m.Groups[2].Value;
	                }



	                string dis = m.Groups[3].Value;
	                m = addrRegex.Match(dis);

	                if (m.Success)
	                {
		                if (int.TryParse(m.Value, NumberStyles.AllowHexSpecifier, null, out addr))
		                {
			                l = null;
			                int offset;
			                if (Labels.GetLabelWithOffset(addr,out l, out offset))
			                {
								if (Program.InStepMode)
					                MainForm.myWatches.AddLocalWatch(l);


				                if (offset == 0)
				                {
					                dis = dis.Replace(m.Value, l.label) + "  ;"+m.Value;
				                }
								else
								{
									dis = dis.Replace(m.Value, l.label+"+"+offset)+ "  ;"+m.Value;
								}
			                }

		                }


	                }
	                disassemblyData[a].Value = dis;

                    //updated = true;
                }
            }



			//after disasm then update watch
	        if (Program.InStepMode)
	        {
		        if (MainForm.myMemoryWatch != null)
		        {
			        MainForm.myMemoryWatch.UpdateMemory();
		        }

	        }

*/
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Callbacks. </summary>
        ///
        /// <remarks> 12/09/2018. </remarks>
        ///
        /// <param name="response"> The response. </param>
        /// <param name="tag">	    The tag. </param>
        /// -------------------------------------------------------------------------------------------------
        void Callback(string[] response,int pc)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { UIUpdate(response,pc); });
                }
                else
                {
                    UIUpdate(response,pc);
                }
            }
            catch
            {
                
            }
        }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Gets current line code. </summary>
	    ///
	    /// <remarks> 12/09/2018. </remarks>
	    ///
	    /// <returns> The current line code. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public string GetCurrentLineCode()
	    {
		    return disassemblyData[0].Value;

	    }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Event handler. Called by dataGridView1 for cell double click events. </summary>
        ///
        /// <remarks> 12/09/2018. </remarks>
        ///
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">	  Data grid view cell event information. </param>
        /// -------------------------------------------------------------------------------------------------
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
/*            int address = Convert.ToInt32(disassemblyData[e.RowIndex].Address, 16);
            if (Program.IsBreakpoint(address))
            {
                Program.RemoveBreakpoint(address);
            }
            else
            {
                Program.AddBreakpoint(address);
            }*/
        }

        private void DissasemblyDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    /// -------------------------------------------------------------------------------------------------
    /// <summary> A disassembly data. </summary>
    ///
    /// <remarks> 12/09/2018. </remarks>
    /// -------------------------------------------------------------------------------------------------
    class DisassemblyData
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the address. </summary>
        ///
        /// <value> The address. </value>
        /// -------------------------------------------------------------------------------------------------
        public string Address { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the value. </summary>
        ///
        /// <value> The value. </value>
        /// -------------------------------------------------------------------------------------------------
        public string Value { get; set; }
    }
}
