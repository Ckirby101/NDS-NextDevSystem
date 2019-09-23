using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HexControlLibrary;
using Microsoft.Win32;

namespace RemoteDebugger.Docks
{

    /// -------------------------------------------------------------------------------------------------
    /// <summary> A byte provider. </summary>
    ///
    /// <remarks> 09/09/2018. </remarks>
    /// -------------------------------------------------------------------------------------------------


    public partial class RegMemWatch : Form
    {
        private ByteProvider[] ByteProviders;

        public Registers.Z80Register[] regs = { Registers.Z80Register.hl, Registers.Z80Register.de, Registers.Z80Register.bc, Registers.Z80Register.ix, Registers.Z80Register.iy };
        private bool sending = false;


		public RegMemWatch()
		{
			InitializeComponent();


			ByteProviders = new ByteProvider[6];

			ByteProviders[0] = new ByteProvider();
			ByteProviders[0].init(32,0);
			HLHexControl.Model.ByteProvider = ByteProviders[0];
			HLHexControl.UpdateView();

			ByteProviders[1] = new ByteProvider();
			ByteProviders[1].init(32,1000);
			DEHexControl.Model.ByteProvider = ByteProviders[1];
			DEHexControl.UpdateView();

			ByteProviders[2] = new ByteProvider();
			ByteProviders[2].init(32,1000);
			BCHexControl.Model.ByteProvider = ByteProviders[2];
			BCHexControl.UpdateView();

			ByteProviders[3] = new ByteProvider();
			ByteProviders[3].init(32,1000);
			IXHexControl.Model.ByteProvider = ByteProviders[3];
			IXHexControl.UpdateView();

			ByteProviders[4] = new ByteProvider();
			ByteProviders[4].init(32,1000);
			IYHexControl.Model.ByteProvider = ByteProviders[4];
			IYHexControl.UpdateView();


		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates the memory. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public void UpdateMemory()
        {

            if (!this.Visible) return;
            if (sending) return;
            sending = true;

            
            int v;
            for (int i = 0; i < 5; i++)
            {
                v = MainForm.myNewRegisters.GetRegisterValueint(regs[i]);
                ByteProvider bp = ByteProviders[i]; 
                bp.offset = v; 
                Program.serialport.GetMemory(Callback, v,32,i);
            }


			

		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Callbacks. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		void Callback(byte[] response,int tag)
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
		private void UIUpdate(byte[] response,int tag)
        {

            int v = 0;


            byte[] arraycopy = new byte[response.Length-5];
            Array.Copy(response, 5, arraycopy, 0,arraycopy.Length);

            ByteProviders[tag].bytes = arraycopy;// parseData(response[0]);

			switch (tag)
			{
				case 0:
                    v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.hl);
                    HLlabel.Text = "( HL ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.hl);
					HLHexControl.UpdateView();
					break;
				case 1:
                    v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.de);
                    DElabel.Text = "( DE ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.de);
					DEHexControl.UpdateView();
					break;
				case 2:
                    v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.bc);
                    BClabel.Text = "( BC ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.bc);
					BCHexControl.UpdateView();
					break;
				case 3:
                    v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.ix);
                    IXlabel.Text = "( IX ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.hl);
					IXHexControl.UpdateView();
					break;
				case 4:
                    v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.iy);
                    IYlabel.Text = "( IY ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.hl);
					IYHexControl.UpdateView();
                    sending = false;
 
					break;
				//case 5:
				//	MEMHexControl.UpdateView();
				//	break;
			}
		}






		


	}


	class ByteProvider : IByteProvider
	{

		public byte[] bytes;
		public int offset;
		public void init(int size, int off)
		{
			bytes = new byte[size];
			offset = off;
		}



		public byte GetByte(int offset)
		{
			if (offset>=0 && offset < bytes.Length)
				return bytes[offset];
			else
				return 0;
		}

		public int Length
		{
			get { return bytes.Length; }
		}
		public int Offset
		{
			get { return offset; }
		}

		public void parseData(string data)
		{
			int len = data.Length;

			string hex = "";

			for(int i=0; i<len; i+=2)
			{
				byte b;
				if (byte.TryParse(data.Substring(i, 2), NumberStyles.HexNumber, null, out b))
				{
					bytes[i / 2] = b;
				}
			}

		}

	}

}
