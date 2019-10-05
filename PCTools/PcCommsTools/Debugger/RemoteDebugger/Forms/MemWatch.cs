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
	public partial class MemWatch : Form
	{
		private ByteProvider byteProvider;
		private int memaddress = 0;

        private int bankNum = -1;
        // -------------------------------------------------------------------------------------------------
        // Default constructor
        // -------------------------------------------------------------------------------------------------
		public MemWatch()
		{
			InitializeComponent();



			byteProvider = new ByteProvider();
			byteProvider.init(512,0);
			MEMPTRHexControl.Model.ByteProvider = byteProvider;
			MEMPTRHexControl.UpdateView();
		}



		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates the memory. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public void UpdateMemory()
		{
            if (!this.Visible) return;

			int v = memaddress;
			ByteProvider bp = byteProvider;
			bp.offset = v;

            int bank = bankNum;
            if (bankNum < 0)
            {
                bank = NextAddress.GetBankFromAddress(ref MainForm.banks, v);
            }



            Program.serialport.GetMemory(
                delegate(byte[] response, int tag)
                { 
                    Invoke((MethodInvoker)delegate { UIUpdate(response,tag); });
                }
                , v,512,bank,0);
		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates this object. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		private void UIUpdate(byte[] response,int tag)
        {

            byte[] arraycopy = new byte[response.Length-5];
            Array.Copy(response, 5, arraycopy, 0,arraycopy.Length);


            byteProvider.bytes = arraycopy;// parseData(response[0]);
			MEMPTRHexControl.UpdateView();
		}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by AddrtextBox for text changed events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Event information.
        // -------------------------------------------------------------------------------------------------
		private void AddrtextBox_TextChanged(object sender, EventArgs e)
		{
            NumberStyles style = NumberStyles.HexNumber;
			string s = AddrtextBox.Text;

            Regex addrRegex = new Regex(@"(?:0x|\$|#)([0-9A-Fa-f]+)(?:[:]?)");

            MatchCollection  m = addrRegex.Matches(s);//.Match(s);
            if (m.Count == 2)
            {
                int.TryParse(m[0].Groups[1].Value, style, null, out bankNum);
                int.TryParse(m[1].Groups[1].Value, style, null, out memaddress);
            }
            else
            if (m.Count == 2)
            {
                int.TryParse(m[0].Groups[1].Value, style, null, out memaddress);
                bankNum = -1;
            }

			//MainForm.ParseExpression(s,ref memaddress);
			UpdateMemory();
		}

        // -------------------------------------------------------------------------------------------------
        // Event handler. Called by AddrtextBox for key press events
        //
        // \param   sender
        // Source of the event.
        // \param   e
        // Key press event information.
        // -------------------------------------------------------------------------------------------------
        private void AddrtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char) 13)
            //{
            //    UpdateMemory();
            //}
        }
    }
}
