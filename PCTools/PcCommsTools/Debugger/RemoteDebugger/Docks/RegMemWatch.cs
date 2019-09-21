using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HexControlLibrary;

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

			ByteProviders[5] = new ByteProvider();
			ByteProviders[5].init(32,1000);
			MEMHexControl.Model.ByteProvider = ByteProviders[5];
			MEMHexControl.UpdateView();
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates the memory. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public void UpdateMemory()
		{
			int v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.hl);
			ByteProvider bp = ByteProviders[0];
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 32", Callback,0);
			HLlabel.Text = "( HL ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.hl);

			
			v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.de);
			bp = ByteProviders[1];
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 32", Callback,1);
			DElabel.Text = "( DE ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.de);

			v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.bc);
			bp = ByteProviders[2];
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 32", Callback,2);
			BClabel.Text = "( BC ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.bc);

			v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.ix);
			bp = ByteProviders[3];
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 32", Callback,3);
			IXlabel.Text = "( IX ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.ix);

			v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.iy);
			bp = ByteProviders[4];
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 32", Callback,4);
			IYlabel.Text = "( IY ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.iy);

			v = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.memptr);
			bp = ByteProviders[5];
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 32", Callback,5);
			MEMlabel.Text = "( MEMPTR ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.memptr);

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
			ByteProviders[tag].parseData(response[0]);

			switch (tag)
			{
				case 0:
					HLHexControl.UpdateView();
					break;
				case 1:
					DEHexControl.UpdateView();
					break;
				case 2:
					BCHexControl.UpdateView();
					break;
				case 3:
					IXHexControl.UpdateView();
					break;
				case 4:
					IYHexControl.UpdateView();
					break;
				case 5:
					MEMHexControl.UpdateView();
					break;
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
