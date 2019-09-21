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

using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RemoteDebugger.Main;

namespace RemoteDebugger
{



    public partial class Breakpoint : Form
    {





	    Regex breakRegex;

        public Breakpoint()
        {
            InitializeComponent();

            breakRegex = new Regex(@"(Enabled|Disabled)\s*[0-9]+:\s*(.*)");
            dataGridView1.DataSource = breakpointData;

			dataGridView1.Columns[0].ReadOnly = true;
			dataGridView1.Columns[1].ReadOnly = false;
	        dataGridView1.Columns[2].ReadOnly = false;
	        //dataGridView1.Columns[3].ReadOnly = false;
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.RowHeadersVisible = false;
	        //dataGridView1.AutoResizeColumns();
	        //dataGridView1.Columns[0].Width = 50;
        }


        public void RequestUpdate()
        {
            Program.telnetConnection.SendCommand("get-breakpoints", Callback);
        }

        void UIUpdate(string[] items)
        {
            bool updated = false;
            items = items.Skip(1).ToArray();    // Skip first line
            for (int a=0;a<items.Count() && a< breakpointData.Count();a++)
            {
                Match m = breakRegex.Match(items[a]);
                if (m.Success)
                {
                    breakpointData[a].IsEnabled = m.Groups[1].Value=="Enabled";
                    breakpointData[a].ConditionString = m.Groups[2].Value;
                    updated = true;
                }
            }
            if (updated)
            {
                dataGridView1.Invalidate(true);
            }
        }

        void Callback(string[] response,int tag)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { UIUpdate(response); });
                }
                else
                {
                    UIUpdate(response);
                }
            }
            catch
            {
                
            }
        }

















	    public enum BreakpointType
	    {
		    disabled,
		    PC
	    }

	    public class BreakpointData
	    {
		    public bool used { get; set; }

		    public bool IsEnabled { get; set; }
		    public string ConditionString { get ; set; }

		    public string addrString
		    {
			    get { return address.ToString("X4"); }
		    }


		    public int address;

		    public BreakpointType breakpointType = BreakpointType.disabled;

		    //for the markers in source code
		    public bool markerset = false;
		    public int markerSourceLine = 0;
		    public string markerSourcefilename = "";
	    }



	    public static BindingList<BreakpointData> breakpointData;
	    static readonly int BreakpointCount=30;

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Initializes the breakpoint data. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    /// -------------------------------------------------------------------------------------------------
	    public static void InitBreakpointData()
	    {
		    breakpointData = new BindingList<BreakpointData>();

		    for (int i = 0; i < BreakpointCount; i++)
		    {
			    breakpointData.Add(new BreakpointData());
		    }

	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Resets the breakpoints. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    /// -------------------------------------------------------------------------------------------------
	    public static void ResetBreakpoints()
	    {
		    Program.telnetConnection.SendCommand("enable-breakpoints",null);

		    for (int a = 0; a < BreakpointCount; a++)
		    {
			    breakpointData[a].IsEnabled = false;
			    breakpointData[a].used = false;
			    Program.telnetConnection.SendCommand("set-breakpoint " + (a + 1), null);
			    Program.telnetConnection.SendCommand("disable-breakpoint " + (a + 1),null);

			    
		    }


	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Searches for the first free breakpoint. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <returns> The found free breakpoint. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static int FindFreeBreakpoint()
	    {
		    for (int i = 0; i < breakpointData.Count; i++)
		    {
			    if (!breakpointData[i].used)
				    return i;
		    }

		    return -1;
	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Number breakpoints active. </summary>
	    ///
	    /// <remarks> 09/09/2018. </remarks>
	    ///
	    /// <returns> The total number of breakpoints active. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static int NumBreakpointsActive()
	    {
		    int c = 0;
		    for (int i = 0; i < breakpointData.Count; i++)
		    {
			    if (breakpointData[i].used && breakpointData[i].IsEnabled) c++;
		    }

		    return c;
	    }
	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Searches for the first free breakpoint at address. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <param name="addr"> The address. </param>
	    ///
	    /// <returns> The found free breakpoint at address. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static int FindFreeBreakpointAtAddress(int addr)
	    {
		    for (int i = 0; i < breakpointData.Count; i++)
		    {
			    if (breakpointData[i].used && addr == breakpointData[i].address)
				    return i;
		    }

		    return -1;
	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Sets break point. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <param name="addr">		 The address. </param>
	    /// <param name="type">		 The type. </param>
	    /// <param name="Condition"> The condition. </param>
	    /// <param name="filename">  Filename of the file. </param>
	    /// <param name="linenum">   The linenum. </param>
	    ///
	    /// <returns> True if it succeeds, false if it fails. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static bool SetBreakPoint(int addr, BreakpointType type, string Condition, string filename,int linenum)
	    {
		    int b = FindFreeBreakpoint();
		    if (b < 0) return false;


		    breakpointData[b].address = addr;
		    breakpointData[b].used = true;
		    breakpointData[b].IsEnabled = true;
		    breakpointData[b].ConditionString = Condition;
		    breakpointData[b].breakpointType = type;
		    breakpointData[b].markerset = false;





			//set a marker in the source code
		    if (!string.IsNullOrEmpty(filename))
		    {
				//no filename then we dont set a marker

			    if (SourceCodeView.SetBreakPointMarker(true, filename, linenum))
			    {
				    breakpointData[b].markerset = true;
				    breakpointData[b].markerSourceLine = linenum;
				    breakpointData[b].markerSourcefilename = filename;
			    }
		    }
		    Program.telnetConnection.SendCommand("sba " + (b + 1) + " break", null);
		    Program.telnetConnection.SendCommand("sb " + (b + 1) + " PC=" + addr.ToString("X4")+"H", Program.myMainForm.myBreakpoints.UpdateCallback);

		    //Program.myMainForm.UpdateBreakpoints();


		    MainForm.mySourceWindow.UpdateSourceButtons();

		    return true;
	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Removes the break point at address described by addr. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <param name="addr"> The address. </param>
	    ///
	    /// <returns> True if it succeeds, false if it fails. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public static bool RemoveBreakPointAtAddress(int addr)
	    {
		    int b = FindFreeBreakpointAtAddress(addr);
		    if (b < 0) return false;

		    if (breakpointData[b].markerset)
		    {
			    if (SourceCodeView.SetBreakPointMarker(false, breakpointData[b].markerSourcefilename, breakpointData[b].markerSourceLine))
			    {
			    }


		    }

		    
		    breakpointData[b].address = -1;
		    breakpointData[b].used = false;
		    breakpointData[b].IsEnabled = false;
		    breakpointData[b].ConditionString = "";
		    breakpointData[b].breakpointType = BreakpointType.disabled;
		    breakpointData[b].markerset = false;

		    Program.telnetConnection.SendCommand("sb " + (b + 1), null);
		    Program.telnetConnection.SendCommand("disable-breakpoint " + (b + 1),Program.myMainForm.myBreakpoints.UpdateCallback);


		    MainForm.mySourceWindow.UpdateSourceButtons();
		    return true;
	    }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Updates the callback. </summary>
	    ///
	    /// <remarks> 08/09/2018. </remarks>
	    ///
	    /// <param name="response"> The response. </param>
	    /// <param name="tag">	    The tag. </param>
	    /// -------------------------------------------------------------------------------------------------
	    public void UpdateCallback(string[] response,int tag)
	    {
		    try
		    {
			    if (InvokeRequired)
			    {
				    Invoke((MethodInvoker) delegate
				    {
					    Program.myMainForm.UpdateBreakpoints();
				    });
			    }
			    else
			    {
				    Program.myMainForm.UpdateBreakpoints();
			    }
		    }
		    catch
		    {

		    }
	    }

    }

}
