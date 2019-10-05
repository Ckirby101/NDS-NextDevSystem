
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RemoteDebugger.Main;
using RemoteDebugger.Remote;

namespace RemoteDebugger
{



    public partial class Breakpoint : Form
    {


        public class BreakpointData
        {
            // -------------------------------------------------------------------------------------------------
            // Default constructor
            // -------------------------------------------------------------------------------------------------
            public BreakpointData()
            {
                nextAddress = new NextAddress(0,0);
            }
            // -------------------------------------------------------------------------------------------------
            // Gets the no
            //
            // \return  The index of breakpoint.
            // -------------------------------------------------------------------------------------------------
            public string No
            {
                get { return (breakpointData.IndexOf(this)+1).ToString(); }
            }
            // -------------------------------------------------------------------------------------------------
            // Gets the address
            //
            // \return  The address.
            // -------------------------------------------------------------------------------------------------
            public string Address
            {
                get
                {
                    if (!used) return "";
                    return nextAddress.ToString("b");
                }
            }

            public bool used = false;
            public NextAddress nextAddress;

        }






        // -------------------------------------------------------------------------------------------------
        // Default constructor
        // -------------------------------------------------------------------------------------------------
        public Breakpoint()
        {
            InitializeComponent();

            dataGridView1.DataSource = breakpointData;
            dataGridView1.AutoGenerateColumns = false;
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellClick += dataGridViewSoftware_CellClick;
        }


        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by dataGridViewSoftware for cell click events
        //
        // \param   sender  Source of the event.
        // \param   e       Data grid view cell event information.
        // -------------------------------------------------------------------------------------------------
        private void dataGridViewSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!breakpointData[e.RowIndex].used) return;

            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                Program.serialport.RemoveBreakpoint(null,breakpointData[e.RowIndex].nextAddress.GetAddr(),breakpointData[e.RowIndex].nextAddress.GetBank());
                Update();

            }

            //goto address
            if (e.ColumnIndex <= dataGridView1.Columns["BPAddr"].Index)
            {
                TraceFile.FocusAddr(breakpointData[e.RowIndex].nextAddress.GetAddr(),breakpointData[e.RowIndex].nextAddress.GetBank());
                Update();
            }
        }

        // -------------------------------------------------------------------------------------------------
        // Request update
        // -------------------------------------------------------------------------------------------------
        public void RequestUpdate()
        {
            //Program.serialport.GetBreakpoints(GetBreakpointsCallback,0);
            Program.serialport.GetBreakpoints(
                delegate(byte[] response, int tag)
                { 
                    Invoke((MethodInvoker)delegate { UIUpdate(response); });
                }
                ,0);
        }
        // -------------------------------------------------------------------------------------------------
        // Callbacks
        //
        // \param   response
        // The response.
        // \param   tag
        // The tag.
        // -------------------------------------------------------------------------------------------------
//        private void GetBreakpointsCallback(byte[] response,int tag)
 //       {
 //           if (InvokeRequired)
 //               Invoke((MethodInvoker)delegate { UIUpdate(response); });
 //           else
 //               UIUpdate(response);
 //       }


        // -------------------------------------------------------------------------------------------------
        // Updates the given items
        //
        // \param   items   The items.
        // -------------------------------------------------------------------------------------------------
        void UIUpdate(byte[] items)
        {
            int index = 3;

            for (int i=0;i<10;i++)
            {
                breakpointData[i].used = Serial.Get8Bit(ref items, ref index) !=0;



                int addr = Serial.Get16Bit(ref items, ref index);
                int bank = Serial.Get8Bit(ref items, ref index);
                breakpointData[i].nextAddress.SetAddress(addr,bank); 
                
                index++;    //skip over opcode
            }

            MainForm.sourceCodeView.UpdateBreakpointView(ref breakpointData);

            dataGridView1.Invalidate(true);
        }























	    public static BindingList<BreakpointData> breakpointData;
	    static readonly int BreakpointCount=10;

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
		    //Program.telnetConnection.SendCommand("enable-breakpoints",null);

		    for (int a = 0; a < BreakpointCount; a++)
		    {
			    //breakpointData[a].IsEnabled = false;
			    //breakpointData[a].used = false;
			    //Program.telnetConnection.SendCommand("set-breakpoint " + (a + 1), null);
			    //Program.telnetConnection.SendCommand("disable-breakpoint " + (a + 1),null);

			    
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
			    //if (breakpointData[i].used && breakpointData[i].IsEnabled) c++;
		    }

		    return c;
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
/*	    public static bool SetBreakPoint(int addr, BreakpointType type, string Condition, string filename,int linenum)
	    {
		    int b = FindFreeBreakpoint();
		    if (b < 0) return false;


		    breakpointData[b].address = addr;
		    breakpointData[b].used = true;
		    //breakpointData[b].IsEnabled = true;
		    //breakpointData[b].ConditionString = Condition;
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
		    //Program.telnetConnection.SendCommand("sba " + (b + 1) + " break", null);
		    //Program.telnetConnection.SendCommand("sb " + (b + 1) + " PC=" + addr.ToString("X4")+"H", Program.myMainForm.myBreakpoints.UpdateCallback);

		    //Program.myMainForm.UpdateBreakpoints();


		    MainForm.mySourceWindow.UpdateSourceButtons();

		    return true;
	    }*/



    }

}
