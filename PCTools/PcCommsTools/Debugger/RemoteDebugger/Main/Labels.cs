using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteDebugger.Main;

namespace RemoteDebugger
{
	public class Labels
	{

		public class Label
		{
			public Label(string l,int a,int b,bool f)
			{
				address = a;
				bank = b;
				label = l;
				function = f;
			}


			public int address;	//16bit address of data
			public int bank;		//16bit bank number
			public string label;
			public bool function;
		}













		public static List<Label> labels = new List<Label>();


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Adds a label. </summary>
		///
		/// <remarks> 07/09/2018. </remarks>
		///
		/// <param name="label"> The label. </param>
		/// <param name="addr">  The address. </param>
		/// <param name="bank">  The bank. </param>
		/// -------------------------------------------------------------------------------------------------
		public static void AddLabel(string label, int addr, int bank,bool isfunction)
		{

			labels.Add( new Label(label,addr,bank,isfunction) );



		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets a label. </summary>
		///
		/// <remarks> 07/09/2018. </remarks>
		///
		/// <param name="addr"> The address. </param>
		///
		/// <returns> The label. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static Label GetLabel(int addr)
		{

			foreach (Label l in labels)
			{
				if (addr == l.address)
				{
					return l;
				}
			}

			return null;
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets label close. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="addr"> The address. </param>
		///
		/// <returns> The label close. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static Label GetLabelClose(int addr)
		{
			Label best = null;
			int bestdist = Int32.MaxValue;

			foreach (Label l in labels)
			{
				if (addr == l.address)
				{
					//cannot get any closer
					return l;
				}

				int diff = Math.Abs(addr - l.address);
				if (bestdist > diff)
				{
					bestdist = diff;
					best = l;
				}


			}

			return best;
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets label with offset. </summary>
		///
		/// <remarks> 08/09/2018. </remarks>
		///
		/// <param name="addr">   The address. </param>
		/// <param name="lab">    [out] The lab. </param>
		/// <param name="offset"> [out] The offset. </param>
		///
		/// <returns> True if it succeeds, false if it fails. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static bool GetLabelWithOffset(int addr,out Label lab,out int offset)
		{
			lab = null;
			offset = 0;

			int best = int.MaxValue;
			int bestindex = -1;
			int index = 0;
			foreach (Label l in labels)
			{
				if (addr >= l.address)
				{
					int off = addr - l.address;
					if (off < best)
					{
						best = off;
						bestindex = index;
					}

				}

				index++;
			}


			if (bestindex < 0) return false;

			lab = labels[bestindex];
			offset = best;

			return true;
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets function with offset. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="addr">   The address. </param>
		/// <param name="lab">    [out] The lab. </param>
		/// <param name="offset"> [out] The offset. </param>
		///
		/// <returns> True if it succeeds, false if it fails. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static bool GetFunctionWithOffset(int addr,out Label lab,out int offset)
		{
			lab = null;
			offset = 0;

			int best = int.MaxValue;
			int bestindex = -1;
			int index = 0;
			foreach (Label l in labels)
			{
				if (addr >= l.address && l.function)
				{
					int off = addr - l.address;
					if (off < best)
					{
						best = off;
						bestindex = index;
					}

				}

				index++;
			}


			if (bestindex < 0) return false;

			lab = labels[bestindex];
			offset = best;

			return true;
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets function with offset banked. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="addr">   The address. </param>
		/// <param name="lab">    [out] The lab. </param>
		/// <param name="offset"> [out] The offset. </param>
		///
		/// <returns> True if it succeeds, false if it fails. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static bool GetFunctionWithOffsetBanked(int addr,out Label lab,out int offset)
		{
			lab = null;
			offset = 0;

			int best = int.MaxValue;
			int bestindex = -1;
			int index = 0;
			foreach (Label l in labels)
			{
				if (l.bank == MainForm.banks[TraceFile.GetBankIndex(l.address)])
				{
					if (addr >= l.address && l.function)
					{
						int off = addr - l.address;
						if (off < best)
						{
							best = off;
							bestindex = index;
						}

					}
				}


				index++;
			}


			if (bestindex < 0) return false;

			lab = labels[bestindex];
			offset = best;

			return true;
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Searches for the first label. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="lab"> [out] The lab. </param>
		///
		/// <returns> The found label. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static Label FindLabel(string lab)
		{
			lab = lab.ToLower();

			foreach (Label l in labels)
			{
				if (l.label.ToLower() == lab)
				{
					return l;
				}
			}

			return null;
		}


	}
}
