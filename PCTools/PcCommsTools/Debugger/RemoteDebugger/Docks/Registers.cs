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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RemoteDebugger.Main;

namespace RemoteDebugger
{
    public partial class Registers : Form
    {
	    public enum Z80Register
	    {
		    a = 0,
		    hl,
		    bc,
		    de,

		    a_e,
		    hl_e,
			bc_e,
			de_e,

		    ix,
		    iy,

		    sp,
		    pc,

		    i,
		    r,

		    f,
		    f_e,

		    memptr,

			numRgisters

	    }

        // -------------------------------------------------------------------------------------------------
        // Displays a function callback described by ri
        //
        // \param [in,out]  ri  The ri.
        //
        // \return  A string.
        // -------------------------------------------------------------------------------------------------
        delegate string DisplayFunctionCallback (ref RegisterItem ri);

        // A register item.
	    class RegisterItem
	    {
		    public Z80Register reg;

            public DisplayFunctionCallback GetString;


		    public string DisplayString;
		    public string RegisterName;
		    public int Value;

		    public string labelstring;		//if the register points to a memeory location we put the label here
		    public TextBox uiTextBox;

	    }



        

        private string DisplaySimpleRegister(ref RegisterItem ri)
        {
            return ri.Value.ToString(ri.DisplayString);
        }

        // -------------------------------------------------------------------------------------------------
        // Registers the validate
        //
        // \param       toValidate  to validate.
        // \param [out] error       The error.
        //
        // \return  A bool.
        // -------------------------------------------------------------------------------------------------
        delegate bool RegisterValidate(string toValidate,out string error);

	    private Regex BankReg;
        RegisterItem[] registerData;
        Dictionary<string, int> nameMap;

        string viewName;

        void InitialiseRegister(string name,string display,Z80Register index,TextBox UiTextBox,DisplayFunctionCallback DF)
        {
	        RegisterItem reg = new RegisterItem()
		        {RegisterName = name, Value = 0, DisplayString = display,uiTextBox = UiTextBox,reg = index, GetString = DF};

	        registerData[(int) index] = reg;

            //registerData.Add(new RegisterItem() { RegisterName = name, Value = 0 ,regex = new Regex(regex), DisplayString = display });
            //nameMap.Add(name, registerData.Count - 1);
        }

        // -------------------------------------------------------------------------------------------------
        // One byte
        //
        // \param       input   The input.
        // \param [out] error   The error.
        //
        // \return  True if it succeeds, false if it fails.
        // -------------------------------------------------------------------------------------------------
        bool VOneByte(string input, out string error)
        {
            error = "";
            int num = Convert.ToInt32(input, 16);
            if (num<0 || num>255)
            {
                error = "Hexadecimal input out of range (00-FF)";
                return false;
            }
            return true;
        }

        // -------------------------------------------------------------------------------------------------
        // Constructor
        //
        // \param   name        The name.
        // \param   viewname    The viewname.
        // -------------------------------------------------------------------------------------------------
        public Registers(string name, string viewname)
        {
            viewName = viewname;
            InitializeComponent();
            //dataGridView1.CausesValidation = false;
            registerData = new RegisterItem[ (int)Z80Register.numRgisters ];
            //regexList = new List<Regex>();
            nameMap = new Dictionary<string, int>();
            //validateList = new List<RegisterValidate>();


	        BankReg = new Regex(@"([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s");
            InitialiseRegister("A", "X2",Z80Register.a,RegA,DisplaySimpleRegister);
	        InitialiseRegister("HL", "X4",Z80Register.hl,RegHL,DisplaySimpleRegister);
	        InitialiseRegister("BC", "X4",Z80Register.bc,RegBC,DisplaySimpleRegister);
	        InitialiseRegister("DE", "X4",Z80Register.de,RegDE,DisplaySimpleRegister);


	        InitialiseRegister("A'", "X2",Z80Register.a_e,RegExA,DisplaySimpleRegister);
	        InitialiseRegister("HL'", "X4",Z80Register.hl_e,RegExHL,DisplaySimpleRegister);
	        InitialiseRegister("BC'", "X4",Z80Register.bc_e,RegExBC,DisplaySimpleRegister);
	        InitialiseRegister("DE'", "X4",Z80Register.de_e,RegExDE,DisplaySimpleRegister);

	        InitialiseRegister("IX", "X4",Z80Register.ix,RegIX,DisplaySimpleRegister);
	        InitialiseRegister("IY", "X4",Z80Register.iy,RegIY,DisplaySimpleRegister);

	        InitialiseRegister("SP", "X4",Z80Register.sp,RegSP,DisplaySimpleRegister);
	        InitialiseRegister("PC", "X4",Z80Register.pc,RegPC,DisplaySimpleRegister);

	        InitialiseRegister("I", "X2",Z80Register.i,RegI,DisplaySimpleRegister);
	        InitialiseRegister("R", "X2",Z80Register.r,RegR,DisplaySimpleRegister);

	        InitialiseRegister("F", "X2",Z80Register.f,RegF,DisplaySimpleRegister);
	        InitialiseRegister("F'", "X2",Z80Register.f_e,RegExF,DisplaySimpleRegister);
	        InitialiseRegister("MEMPTR", "X2",Z80Register.memptr,null,DisplaySimpleRegister);


            

        }


        // Request update
        public void RequestUpdate()
        {

            Program.serialport.GetRegisters(regcallback, 0);
	        //Program.telnetConnection.SendCommand("get-memory-pages", BankCallback);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 81", BankCallback,81);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 82", BankCallback,82);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 83", BankCallback,83);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 84", BankCallback,84);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 85", BankCallback,85);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 86", BankCallback,86);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 87", BankCallback,87);
	        //Program.telnetConnection.SendCommand("get-registers", cb);

        }



        // -------------------------------------------------------------------------------------------------
        // Regcallback, called when the register
        //
        // \param   response    The response.
        // \param   tag         The tag.
        // -------------------------------------------------------------------------------------------------
        void regcallback(byte[] response, int tag)
        {
            int index = 3;

            registerData[(int) Z80Register.f_e].Value = Get8Bit(ref response, ref index);
            registerData[(int) Z80Register.a_e].Value = Get8Bit(ref response, ref index);
            registerData[(int) Z80Register.r].Value = Get8Bit(ref response, ref index);
            registerData[(int) Z80Register.i].Value = Get8Bit(ref response, ref index);
            registerData[(int) Z80Register.f].Value = Get8Bit(ref response, ref index);
            registerData[(int) Z80Register.a].Value = Get8Bit(ref response, ref index);

            registerData[(int) Z80Register.iy].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.ix].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.bc_e].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.de_e].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.hl_e].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.bc].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.de].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.hl].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.pc].Value = Get16Bit(ref response, ref index);
            registerData[(int) Z80Register.sp].Value = Get16Bit(ref response, ref index);

            try
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { UIUpdate(); });
                }
                else
                {
                    UIUpdate();
                }
            }
            catch
            {

            }


            

        }


        public int Get16Bit(ref byte[] b, ref int index)
        {
            int value = b[index] | (b[index + 1] << 8);
            index += 2;

            return value;
        }

        public int Get8Bit(ref byte[] b, ref int index)
        {
            int value = b[index];
            index ++;

            return value;
        }


        public string GetRegisterValue(string reg)
        {
            //return registerData[nameMap[reg]].Value;
	        return "";
        }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Gets register valueint. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    ///
	    /// <param name="reg"> The register. </param>
	    ///
	    /// <returns> The register valueint. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public int GetRegisterValueint(Z80Register reg)
	    {
		    return registerData[(int)reg].Value;
	    }

	    public string GetRegisterLabelString(Z80Register reg)
	    {
		    return registerData[(int)reg].labelstring;
	    }
	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Updates the registers described by items. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    ///
	    /// <param name="items"> The items. </param>
	    /// -------------------------------------------------------------------------------------------------
/*	    public void UpdateRegisters(string[] items)
	    {
            if (items.Count() != 1)
                return;

		    for (int r = 0; r < registerData.Length; r++)
		    {
			    Match m = registerData[r].regex.Match(items[0]);
			    if (m.Success)
			    {

				    if (!string.IsNullOrEmpty(m.Groups[1].Value))
					    int.TryParse(m.Groups[1].Value, NumberStyles.HexNumber, null, out registerData[r].Value);

				    
				    //memptr is off by one!!
				    if (registerData[r].reg == Z80Register.memptr)
				    {
					    registerData[r].Value--;

				    }


				    if (registerData[r].reg == Z80Register.hl ||
				        registerData[r].reg == Z80Register.de ||
				        registerData[r].reg == Z80Register.bc ||
				        registerData[r].reg == Z80Register.ix ||
				        registerData[r].reg == Z80Register.iy ||
				        registerData[r].reg == Z80Register.hl_e ||
				        registerData[r].reg == Z80Register.de_e ||
				        registerData[r].reg == Z80Register.bc_e ||
					    registerData[r].reg == Z80Register.memptr)
				    {
					    int offset;
					    Labels.Label l;
					    if (Labels.GetLabelWithOffset(registerData[r].Value,out l,out offset))
					    {
							if (offset!=0)
							    registerData[r].labelstring = l.label+"+"+offset;
							else
							    registerData[r].labelstring = l.label;
					    }
					    else
					    {
						    registerData[r].labelstring = "";
					    }

				    }


			    }
		    }



		    UIUpdate();

	    }*/


        

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Updates this object. </summary>
        ///
        /// <remarks> 07/09/2018. </remarks>
        /// -------------------------------------------------------------------------------------------------
        void UIUpdate()
        {

	        for (int r = 0; r < registerData.Length; r++)
	        {
		        if (registerData[r].uiTextBox != null)
		        {
                    //registerData[r].uiTextBox.Text = "$"+registerData[r].Value.ToString(registerData[r].DisplayString) + " / " +
                    //                                 registerData[r].Value.ToString();
                    registerData[r].uiTextBox.Text = "$"+registerData[r].GetString( ref registerData[r] ) + " / " +
                                                     registerData[r].Value.ToString();

		        }

	        }


//	        BankData.Text = String.Format("${0:X2}  ${1:X2}  ${2:X2}  ${3:X2}  ${4:X2}  ${5:X2}  ${6:X2}  ${7:X2}", MainForm.banks[0],
//		        MainForm.banks[1], MainForm.banks[2], MainForm.banks[3], MainForm.banks[4], MainForm.banks[5],
//		        MainForm.banks[6], MainForm.banks[7]);


	        int flg = registerData[(int) Z80Register.f].Value;
	        string flags = " ";


	        string[] flagsoff = { "- ","- ","- ","- ","- ","- ","- ","- " };
	        string[] flagson = { "C ","N ","P ","3 ","H ","5 ","Z ","S " };

	        for (int i = 7; i >= 0; i--)
	        {
		        if ( (flg & (1 << i)) !=0 )
		        {
			        flags = flags + flagson[i];
		        }
		        else
		        {
			        flags = flags + flagsoff[i];

		        }

	        }

	        RegFlags.Text = flags;





/*            if (items.Count() != 1)
                return;
            bool updated = false;
            dataGridView1.CausesValidation = false;
            for (int r = 0; r < regexList.Count; r++)
            {
                Match m = regexList[r].Match(items[0]);
                if (m.Success)
                {
                    string newValue = m.Groups[1].Value;
                    if (registerData[r].Value != newValue)
                    {
                        registerData[r].Value = newValue;
                        updated = true;
                    }
                }
            }
            if (updated)
            {
                dataGridView1.Invalidate(true);
            }
            dataGridView1.CausesValidation = true;*/
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Callbacks. </summary>
        ///
        /// <remarks> 07/09/2018. </remarks>
        ///
        /// <param name="response"> The response. </param>
        /// <param name="tag">	    The tag. </param>
        /// -------------------------------------------------------------------------------------------------
/*        void Callback(string[] response,int tag)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { UpdateRegisters(response); });
                }
                else
                {
	                UpdateRegisters(response);
                }
            }
            catch
            {

            }
        }*/



	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Callback, called when the bank. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    ///
	    /// <param name="response"> The response. </param>
	    /// <param name="tag">	    The tag. </param>
	    /// -------------------------------------------------------------------------------------------------
	    void BankCallback(string[] response,int tag)
	    {
		    if (response.Count() != 1) return;

		    Match m = BankReg.Match(response[0]);


		    for (int i = 0; i < (16/2); i++)
		    {
			    if (m.Groups[1 + (i*2)].ToString() == "O")
			    {
				    MainForm.banks[ i ] = 0xff;
			    }
			    else
			    {
				    int bank;
				    if (int.TryParse(m.Groups[2 + (i*2)].ToString(), NumberStyles.Integer, null, out bank))
					    MainForm.banks[ i ] = bank;
				    
			    }




		    }

	    }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
/*            if (e.ColumnIndex == 1)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                string regName = dataGridView1.Rows[e.RowIndex].Cells[0].Value as string;
                string cValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value as string;
                if (cValue != e.FormattedValue.ToString())
                {
                    string err;
                    if (validateList[e.RowIndex](e.FormattedValue.ToString(), out err))
                    {
                        Program.telnetConnection.SendCommand("set-register " + regName + "=" + e.FormattedValue.ToString()+"h", null);
                    }
                    dataGridView1.Rows[e.RowIndex].ErrorText = err;
                }
            }*/
        }


	    void RegChangeCallback(string[] response,int tag)
	    {
		    try
		    {
			    if (InvokeRequired)
			    {
				    Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(Program.InStepMode); });
			    }
			    else
			    {
				    Program.myMainForm.UpdateAllWindows(Program.InStepMode);
			    }
		    }
		    catch
		    {

		    }
	    }


	    private void UpdateRegister(KeyEventArgs e, string s, Z80Register reg)
	    {
		    if (e.KeyCode == Keys.Enter)
		    {

			    int addr = 0;
			    if (MainForm.ParseExpression(s, ref addr))
			    {
				    registerData[(int) reg].Value = addr;

				    SetRegister(addr, reg);

				    //Program.telnetConnection.SendCommand("set-register "+assign+""+addr, RegChangeCallback);

				    UIUpdate();
			    }



		    }

	    }

	    public void SetRegister(int value, Z80Register reg)
	    {
		    registerData[(int) reg].Value = value;
		    Program.telnetConnection.SendCommand("set-register "+registerData[(int) reg].RegisterName+"="+value, RegChangeCallback);

	    }



		private void RegA_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegA.Text, Z80Register.a);
		}


		private void RegExA_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExA.Text,Z80Register.a_e);

		}

		private void RegHL_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegHL.Text,Z80Register.hl);

		}

		private void RegDE_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegDE.Text,Z80Register.de);

		}

		private void RegBC_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegBC.Text,Z80Register.bc);

		}

		private void RegPC_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegPC.Text,Z80Register.pc);

		}

		private void RegExHL_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExHL.Text,Z80Register.hl_e);

		}

		private void RegExDE_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExDE.Text,Z80Register.de_e);

		}

		private void RegExBC_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExBC.Text,Z80Register.bc_e);

		}

		private void RegIX_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegIX.Text,Z80Register.ix);

		}

		private void RegIY_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegIY.Text,Z80Register.iy);

		}

		private void RegSP_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegSP.Text,Z80Register.sp);

		}
	}


}
