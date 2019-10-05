

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RemoteDebugger.Main;
using RemoteDebugger.Remote;

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

            MACHINE_ID_00,NEXT_VERSION_01,NEXT_RESET_02,MACHINE_TYPE_03,ROM_MAPPING_04,PERIPHERAL_1_05,PERIPHERAL_2_06,TURBO_CONTROL_07,PERIPHERAL_3_08,PERIPHERAL_4_09,NEXT_VERSION_MINOR_0E,ANTI_BRICK_10,VIDEO_TIMING_11,LAYER2_RAM_BANK_12,LAYER2_RAM_SHADOW_BANK_13,GLOBAL_TRANSPARENCY_14,SPRITE_CONTROL_15,LAYER2_XOFFSET_16,LAYER2_YOFFSET_17,CLIP_LAYER2_18,CLIP_SPRITE_19,CLIP_ULA_LORES_1A,CLIP_TILEMAP_1B,CLIP_WINDOW_CONTROL_1C,RASTER_LINE_MSB_1E,RASTER_LINE_LSB_1F,RASTER_INTERUPT_CONTROL_22,RASTER_INTERUPT_VALUE_23,TILEMAP_XOFFSET_MSB_2F,TILEMAP_XOFFSET_LSB_30,TILEMAP_YOFFSET_31,LORES_XOFFSET_32,LORES_YOFFSET_33,SPRITE_ATTR_SLOT_SEL_34,PALETTE_INDEX_40,PALETTE_VALUE_41,PALETTE_FORMAT_42,PALETTE_CONTROL_43,PALETTE_VALUE_9BIT_44,TRANSPARENCY_FALLBACK_COL_4A,SPRITE_TRANSPARENCY_I_4B,TILEMAP_TRANSPARENCY_I_4C,
            MMU0_0000_50,MMU1_2000_51,MMU2_4000_52,MMU3_6000_53,MMU4_8000_54,MMU5_A000_55,MMU6_C000_56,MMU7_E000_57,ULA_CONTROL_68,TILEMAP_CONTROL_6B,TILEMAP_DEFAULT_ATTR_6C,TILEMAP_BASE_ADR_6E,TILEMAP_GFX_ADR_6F,



		    //memptr,

			numRegisters,

            //numZ80Registers = hw0,
            numHW = TILEMAP_GFX_ADR_6F - MACHINE_ID_00


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
            public DisplayFunctionCallback GetString;

            public bool changed = false;

		    public string DisplayString;
            public int Value;

		    public string labelstring;		//if the register points to a memeory location we put the label here
		    public TextBox uiTextBox;

            public void SetValue(int value)
            {
                changed = Value != value;
                Value = value;
            }
        }


        delegate bool RegisterValidate(string toValidate,out string error);

        RegisterItem[] registerData;

        public int[] stackdata = new int[4];


        // -------------------------------------------------------------------------------------------------
        // Initialises the register
        //
        // \param   name
        // The name.
        // \param   display
        // The display.
        // \param   index
        // Zero-based index of the.
        // \param   UiTextBox
        // The text box control.
        // \param   DF
        // The df.
        // -------------------------------------------------------------------------------------------------
        void InitialiseRegister(string name,string display,Z80Register index,TextBox UiTextBox,DisplayFunctionCallback DF)
        {
	        RegisterItem reg = new RegisterItem()
		        {Value = 0, DisplayString = display,uiTextBox = UiTextBox, GetString = DF};

	        registerData[(int) index] = reg;

        }

        
        // -------------------------------------------------------------------------------------------------
        // Displays a simple register described by ri
        //
        // \param [in,out]  ri
        // The ri.
        //
        // \return  A string.
        // -------------------------------------------------------------------------------------------------
        private string DisplaySimpleRegister(ref RegisterItem ri)
        {
            return ri.Value.ToString(ri.DisplayString);
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

            InitializeComponent();
            registerData = new RegisterItem[ (int)Z80Register.numRegisters ];



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



            InitialiseRegister("F'", "X2",Z80Register.MACHINE_ID_00,RegExF,DisplaySimpleRegister);

            int y = 7;
            for (int i = (int) Z80Register.MACHINE_ID_00; i <= (int)Z80Register.TILEMAP_GFX_ADR_6F; i++)
            {
                Z80Register reg = (Z80Register) i;


                Label lb = new System.Windows.Forms.Label();

                lb.AutoSize = true;
                lb.Location = new System.Drawing.Point(6, y);
                lb.Name = "";
                lb.Size = new System.Drawing.Size(37, 13);
                lb.TabIndex = 67;
                lb.Text = reg.ToString();
                lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                lb.Font = new System.Drawing.Font("Consolas", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                TextBox HW = new System.Windows.Forms.TextBox();

                HW.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                HW.Location = new System.Drawing.Point(180, y);
                HW.Name = "RegA";
                HW.Size = new System.Drawing.Size(210, 23);
                HW.TabIndex = 32;
                HW.Text = "$00 / 256";
                HW.ReadOnly = false;
                HW.BorderStyle = BorderStyle.None;

                this.hwpanel.Controls.Add(lb);
                this.hwpanel.Controls.Add(HW);


                y += 25;


                InitialiseRegister(reg.ToString(), "X2",reg,HW,DisplaySimpleRegister);


            }

        }


        // -------------------------------------------------------------------------------------------------
        // Regcallback, called when the register
        //
        // \param   response    The response.
        // \param   tag         The tag.
        // -------------------------------------------------------------------------------------------------
        public void regcallback(byte[] response, int tag)
        {
            int index = 3;

            registerData[(int) Z80Register.f_e].SetValue(Serial.Get8Bit(ref response, ref index));
            registerData[(int) Z80Register.a_e].SetValue(Serial.Get8Bit(ref response, ref index));
            registerData[(int) Z80Register.r].SetValue(Serial.Get8Bit(ref response, ref index));
            registerData[(int) Z80Register.i].SetValue(Serial.Get8Bit(ref response, ref index));
            registerData[(int) Z80Register.f].SetValue(Serial.Get8Bit(ref response, ref index));
            registerData[(int) Z80Register.a].SetValue(Serial.Get8Bit(ref response, ref index));

            registerData[(int) Z80Register.iy].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.ix].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.bc_e].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.de_e].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.hl_e].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.bc].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.de].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.hl].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.pc].SetValue(Serial.Get16Bit(ref response, ref index));
            registerData[(int) Z80Register.sp].SetValue(Serial.Get16Bit(ref response, ref index) + 2);  //plus 2 because comms has return address on stack.

            for (int i = (int) Z80Register.MACHINE_ID_00; i <= (int) Z80Register.TILEMAP_GFX_ADR_6F; i++)
            {
                Z80Register reg = (Z80Register) i;
                registerData[(int) reg].SetValue(Serial.Get8Bit(ref response, ref index));
            }
            //grab the current debugging mode
            int mode = Serial.Get8Bit(ref response, ref index); 

            //grab thr stacl data
            stackdata[0] = Serial.Get16Bit(ref response, ref index);
            stackdata[1] = Serial.Get16Bit(ref response, ref index);
            stackdata[2] = Serial.Get16Bit(ref response, ref index);
            stackdata[3] = Serial.Get16Bit(ref response, ref index);


            //not in step mode be device is! then breakpoint has happened
            MainForm.mySourceWindow.UpdatePauseStatus(mode);



            //copy bank data from data
            for (int i = 0; i < MainForm.banks.Length; i++)
            {
                MainForm.banks[ i ] = registerData[(int)(Z80Register.MMU0_0000_50+i)].Value;
            }


            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { UIUpdate(); });
            else
                UIUpdate();
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

        // -------------------------------------------------------------------------------------------------
        // Gets register label string
        //
        // \param   reg
        // The register.
        //
        // \return  The register label string.
        // -------------------------------------------------------------------------------------------------
	    public string GetRegisterLabelString(Z80Register reg)
	    {
		    return registerData[(int)reg].labelstring;
	    }




        // -------------------------------------------------------------------------------------------------
        // Updates the emulator PC
        // -------------------------------------------------------------------------------------------------
        public void UpdateEmulatorPC()
        {
            if (MainForm.myDisassembly!=null)
                nextPC.Text = "$" + MainForm.myDisassembly.GetEmulatorPC().ToString("X4");

        }
        

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Updates this object. </summary>
        ///
        /// <remarks> 07/09/2018. </remarks>
        /// -------------------------------------------------------------------------------------------------
        void UIUpdate()
        {

	        for (int r = 0; r < (int)Z80Register.numRegisters; r++)
	        {
                if (registerData[r]==null) continue;

		        if (registerData[r].uiTextBox != null)
		        {
                    //registerData[r].uiTextBox.Text = "$"+registerData[r].Value.ToString(registerData[r].DisplayString) + " / " +
                    //                                 registerData[r].Value.ToString();
                    registerData[r].uiTextBox.Text = "$"+registerData[r].GetString( ref registerData[r] ) + " / " +
                                                     registerData[r].Value.ToString();



                    registerData[r].uiTextBox.ForeColor = registerData[r].changed ? Color.Red : Color.Black;

		        }

	        }


            UpdateEmulatorPC();

	        BankData.Text = String.Format("${0:X2}  ${1:X2}  ${2:X2}  ${3:X2}  ${4:X2}  ${5:X2}  ${6:X2}  ${7:X2}", MainForm.banks[0],
		        MainForm.banks[1], MainForm.banks[2], MainForm.banks[3], MainForm.banks[4], MainForm.banks[5],
		        MainForm.banks[6], MainForm.banks[7]);


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

            string stackstr = "";
            for (int i = stackdata.Length - 1; i >= 0; i--)
            {
                stackstr = stackstr + i.ToString()+" - "+stackdata[i].ToString("X4") + Environment.NewLine;
            }
            stack.Text = stackstr;
        }




        // -------------------------------------------------------------------------------------------------
        // Updates the register
        //
        // \param   e
        // Key event information.
        // \param   s
        // The string.
        // \param   reg
        // The register.
        // -------------------------------------------------------------------------------------------------
	    private void UpdateRegister(KeyEventArgs e, string s, Z80Register reg)
	    {
		    if (e.KeyCode == Keys.Enter)
		    {

			    int addr = 0;
			    if (MainForm.ParseExpression(s, ref addr))
			    {
				    registerData[(int) reg].Value = addr;

				    SetRegister(addr, reg);

                    Program.serialport.SendRegisters(
                        delegate(byte[] response, int tag)
                        { 
                            Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(true); });
                        }                        
                        );








				    //Program.telnetConnection.SendCommand("set-register "+assign+""+addr, RegChangeCallback);

				    UIUpdate();
			    }



		    }

	    }

        // -------------------------------------------------------------------------------------------------
        // Sets a register
        //
        // \param   value
        // The value.
        // \param   reg
        // The register.
        // -------------------------------------------------------------------------------------------------
	    public void SetRegister(int value, Z80Register reg)
	    {
		    registerData[(int) reg].Value = value;
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

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
