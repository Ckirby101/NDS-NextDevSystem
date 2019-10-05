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
                nextAddress = new NextAddress(a,b);

				label = l;
				function = f;
			}

			public string label;
			public bool function;
            public NextAddress nextAddress;

            public override string ToString()
            {
                return label + "  $" + nextAddress.ToString();
            }

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
		public static Label GetLabel(ref int[] bankmap,int addr)
        {
            int bank = NextAddress.GetBankFromAddress(ref bankmap,addr);
            int longaddr = NextAddress.MakeLongAddress(bank, addr);


			foreach (Label l in labels)
			{

				if (longaddr == l.nextAddress.GetLongAddress())
				{
					return l;
				}
			}

			return null;
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
		public static bool GetLabelWithOffset(ref int[] bankmap,int addr,out Label lab,out int offset)
		{

            int bank = NextAddress.GetBankFromAddress(ref bankmap,addr);
            int longaddr = NextAddress.MakeLongAddress(bank, addr);

			lab = null;
			offset = 0;

			int best = int.MaxValue;
			int bestindex = -1;
			int index = 0;
			foreach (Label l in labels)
            {



                int laddr = l.nextAddress.GetLongAddress();
				if (longaddr >= laddr)
				{
					int off = addr - laddr;
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
		public static bool GetFunctionWithOffset(ref int[] bankmap,int addr,out Label lab,out int offset)
		{
            int bank = NextAddress.GetBankFromAddress(ref bankmap,addr);
            int longaddr = NextAddress.MakeLongAddress(bank, addr);



			lab = null;
			offset = 0;

			int best = int.MaxValue;
			int bestindex = -1;
			int index = 0;
			foreach (Label l in labels)
			{
				if (longaddr >= l.nextAddress.GetLongAddress() && l.function)
				{
					int off = longaddr - l.nextAddress.GetLongAddress();
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
