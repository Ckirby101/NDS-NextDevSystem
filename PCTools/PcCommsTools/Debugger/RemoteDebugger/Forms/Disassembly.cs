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
using Z80EmuLib;

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

        private byte[] disassemblyMemory;
        private int dissassemblyBaseAddress;
        private int disasmline;

        private TestMemory EmulatorMemory;
        private TestPorts EmulatorPorts;
        private Z80Emu Emulatorcpu;



        /// <summary> The dis RegEx. </summary>
        //Regex disRegex;
        /// <summary> The address RegEx. </summary>
        Regex addrRegex;


        private eZ80Disassembler.DisassembledInstruction[] instrs;

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

            InitializeComponent();
            disassemblyData = new BindingList<DisassemblyData>();
            //disRegex = new Regex(@"([0-9a-fA-F]{4})\s([0-9a-fA-F]*)\s*(.*)");   //gets the address and opcode hex
            addrRegex = new Regex(@"([0-9a-fA-F]{4})");
            for (int a = 0; a < 30; a++)
            {
                disassemblyData.Add(new DisassemblyData() {Address = "0000", Value = ""});
            }

            DissasemblyDataGrid.DataSource = disassemblyData;

            DissasemblyDataGrid.Columns[0].ReadOnly = true;
            DissasemblyDataGrid.Columns[1].ReadOnly = true;
            DissasemblyDataGrid.AllowUserToAddRows = false;
            DissasemblyDataGrid.RowHeadersVisible = false;


            InitEmulator();

        }

        // -------------------------------------------------------------------------------------------------
        // Finaliser
        // -------------------------------------------------------------------------------------------------
        ~Disassembly()
        {

        }


        // -------------------------------------------------------------------------------------------------
        // Gets step address
        //
        // \return  The step address.
        // -------------------------------------------------------------------------------------------------
        public int GetStepAddress()
        {
            //just gets the pc upon next step!
            return GetEmulatorPC();
        }

        // -------------------------------------------------------------------------------------------------
        // Gets step over address
        //
        // \return  The step over address.
        // -------------------------------------------------------------------------------------------------
        public int GetStepOverAddress()
        {
            if (!instrs[0].IsCall)
                return GetStepAddress();

            //get address of next instruction after call
            return instrs[1].MemoryAddress;
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
            int addr = Math.Max(0,pc-64);

            int bank = NextAddress.GetBankFromAddress(ref MainForm.banks, addr);


            Program.serialport.GetMemory(
                delegate(byte[] response, int tag) { Invoke((MethodInvoker) delegate { UIUpdate(tag, response); }); }
                , addr, 128, bank, addr);
        }



        private int DisasmWithOffset(ref byte[] data, int disaddr,int pc, int offset)
        {
            dissassemblyBaseAddress = disaddr+offset;
            disassemblyMemory = new byte[(data.Length - 5)+offset];
            Array.Copy(data, 5+offset, disassemblyMemory, 0, (data.Length - 5)-offset);

            int start = 0;
            int end = disassemblyMemory.Length - 1;
            int baseAddress = dissassemblyBaseAddress;
            bool hasBaseAddress = true;
            bool adlMode = false;
            bool z80ClassicMode = true;
            bool addLabels = false;


            instrs = eZ80Disassembler.Disassemble(disassemblyMemory, start, end, baseAddress, hasBaseAddress, adlMode,
                z80ClassicMode, addLabels ? "label_" : "", addLabels ? "loc_" : "");

            int foundline = -1;
            int index=0;
            foreach (eZ80Disassembler.DisassembledInstruction di in instrs)
            {  
                int addr = (baseAddress + di.StartPosition);
                if (addr == pc)
                {
                    foundline = index;
                    break;
                }

                index++;
            }

            return foundline;
        }


        /// -------------------------------------------------------------------------------------------------
        /// <summary> Updates the given items. </summary>
        ///
        /// <remarks> 12/09/2018. </remarks>
        ///
        /// <param name="items"> The items. </param>
        /// -------------------------------------------------------------------------------------------------
        void UIUpdate(int disaddr, byte[] data)
        {
            int pc = MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);

            int offset=-1;
            int foundline = -1;
            do
            {
                offset++;
            } while ( (foundline = DisasmWithOffset(ref data, disaddr,pc, offset)) < 0 );
            disasmline = foundline;

            //goto next instruction
            SyncEmulator();
            StepEmulator();

            int instructionOffset = Math.Max(0,disasmline - 15);
            for (int index = 0; index < 30; index++)
            {
                eZ80Disassembler.DisassembledInstruction di = instrs[index + instructionOffset];
                
                int addr = (dissassemblyBaseAddress + di.StartPosition);
                string addrstr = addr.ToString("X4") + "  ";

                Labels.Label l = Labels.GetLabel(ref MainForm.banks, addr);
                for (int i = 0; i < di.Length; i++)
                {
                    addrstr = addrstr + data[di.StartPosition + i + 5].ToString("X2") + " ";
                }

                //add address label
                if (l != null)
                    addrstr = addrstr + l.label;
                disassemblyData[index].Address = addrstr;


                // do value

                string dis = di.ToString();
                Match m = addrRegex.Match(dis);

                if (m.Success)
                {
                    int jaddr;
                    if (int.TryParse(m.Value, NumberStyles.AllowHexSpecifier, null, out jaddr))
                    {
                        l = null;
                        int voffset;
                        if (Labels.GetLabelWithOffset(ref MainForm.banks, jaddr, out l, out voffset))
                        {
                            if (Program.InStepMode)
                                MainForm.myWatches.AddLocalWatch(l);


                            if (voffset == 0)
                            {
                                dis = dis.Replace(m.Value, l.label) + "  ;" + m.Value;
                            }
                            else
                            {
                                //dis = dis.Replace(m.Value, l.label+"+"+offset)+ "  ;"+m.Value;
                            }
                        }

                    }


                }

                disassemblyData[index].Value = dis;

                DissasemblyDataGrid.Rows[index].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

                // mark the next instruction to run
                if (addr == GetEmulatorPC())
                    DissasemblyDataGrid.Rows[index].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                else
                    DissasemblyDataGrid.Rows[index].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

                //mark the current z80 pc instruction
                if (addr == pc)
                    DissasemblyDataGrid.Rows[index].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                else
                    DissasemblyDataGrid.Rows[index].DefaultCellStyle.BackColor = System.Drawing.Color.White;


            }



            DissasemblyDataGrid.Invalidate();

            Invalidate();


            MainForm.sourceCodeView.UpdateDismWindow();

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




        // -------------------------------------------------------------------------------------------------
        // Initialises the emulator
        // -------------------------------------------------------------------------------------------------
        private void InitEmulator()
        {
            EmulatorMemory = new TestMemory();
            EmulatorPorts = new TestPorts();
            Emulatorcpu = new Z80Emu(EmulatorMemory, EmulatorPorts, null, true);

        }


        // -------------------------------------------------------------------------------------------------
        // Synchronises the emulator
        // -------------------------------------------------------------------------------------------------
        private void SyncEmulator()
        {
            //Emulatorcpu.Registers.A = Registers[ Z80Registers]

            //copy ram into z80 emulator
            for (int i = 0; i < disassemblyMemory.Length; i++)
            {
                EmulatorMemory[dissassemblyBaseAddress + i] = disassemblyMemory[i];
            }

            Emulatorcpu.Registers.A = (byte) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.a);
            Emulatorcpu.Registers.HL = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.hl);
            Emulatorcpu.Registers.BC = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.bc);
            Emulatorcpu.Registers.DE = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.de);

            Emulatorcpu.Registers.A_ = (byte) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.a_e);
            Emulatorcpu.Registers.HL = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.hl_e);
            Emulatorcpu.Registers.BC = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.bc_e);
            Emulatorcpu.Registers.DE = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.de_e);


            Emulatorcpu.Registers.IX = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.ix);
            Emulatorcpu.Registers.IY = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.iy);

            Emulatorcpu.Registers.SP = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.sp);
            Emulatorcpu.Registers.PC = (ushort) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.pc);

            Emulatorcpu.Registers.I = (byte) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.i);
            Emulatorcpu.Registers.R = (byte) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.r);

            Emulatorcpu.Registers.F = (byte) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.f);
            Emulatorcpu.Registers.F_ = (byte) MainForm.myNewRegisters.GetRegisterValueint(Registers.Z80Register.f_e);
        }


        // -------------------------------------------------------------------------------------------------
        // Step emulator
        // -------------------------------------------------------------------------------------------------
        private void StepEmulator()
        {

            do
            {
                Emulatorcpu.Step();

            } while (!Emulatorcpu.InstructionDone);

            MainForm.myNewRegisters.UpdateEmulatorPC();
        }


        // -------------------------------------------------------------------------------------------------
        // Gets emulator PC
        //
        // \return  The emulator PC.
        // -------------------------------------------------------------------------------------------------
        public int GetEmulatorPC()
        {
            return Emulatorcpu.Registers.PC;
        }



        // -------------------------------------------------------------------------------------------------
        // Gets dissasembly source
        //
        // \return  The dissasembly source.
        // -------------------------------------------------------------------------------------------------
        public string GetDissasemblySource(ref TraceFile tf,int maxlines)
        {
            string textfile = "";
            tf.lines.Clear();

            if (instrs == null) return "";



            int instructionOffset = 0;//Math.Max(0,disasmline - (maxlines/2) );
            int lineNumber = 0;
            for (int index = 0; index < instrs.Length; index++)
            {
                eZ80Disassembler.DisassembledInstruction di = instrs[index + instructionOffset];
                
                int addr = (dissassemblyBaseAddress + di.StartPosition);

                
                //if (lineNumber >= maxlines)
                //    break;
                
                //int addr = (dissassemblyBaseAddress + di.StartPosition);


                Labels.Label l = Labels.GetLabel(ref MainForm.banks, addr);

                if (l != null)
                {
                    if (!l.label.Contains("."))
                    {
                        textfile = textfile +"\n; **************************************************************************\n";
                        textfile = textfile + "; "+l.label + "\n";
                        textfile = textfile +"; **************************************************************************\n";
                        lineNumber += 4;

                    }
                    textfile = textfile + l.label + ":\n";
                    lineNumber++;
                }


                bool useComment = true;
                string Comment = "; ";
                for (int i = 0; i < di.Length; i++)
                {
                    Comment = Comment + disassemblyMemory[di.StartPosition +i].ToString("X2") + " ";
                }



                /*
                string addrstr = addr.ToString("X4") + "  ";
                for (int i = 0; i < di.Length; i++)
                {
                    addrstr = addrstr + disassemblyMemory[di.StartPosition + 5].ToString("X2") + " ";
                }
                */
                //add address label
                //if (l != null)
                //    addrstr = addrstr + l.label;
                //disassemblyData[index].Address = addrstr;


                // do value

                LineData ld = new LineData();
                //ld.address = addr;
                ld.lineNumber = lineNumber;
                ld.nextAddress = new NextAddress(addr,NextAddress.GetBankFromAddress(ref MainForm.banks,addr));
                ld.tf = tf;
                tf.lines.Add(ld);




                string dis = di.ToString();
                Match m = addrRegex.Match(dis);

                if (m.Success)
                {
                    int jaddr;
                    if (int.TryParse(m.Value, NumberStyles.AllowHexSpecifier, null, out jaddr))
                    {
                        l = null;
                        int offset;
                        if (Labels.GetLabelWithOffset(ref MainForm.banks, jaddr, out l, out offset))
                        {
                            if (Program.InStepMode)
                                MainForm.myWatches.AddLocalWatch(l);


                            if (offset == 0)
                            {
                                dis = dis.Replace(m.Value, l.label) + "  ;" + m.Value;
                            }
                            else
                            {
                                //dis = dis.Replace(m.Value, l.label+"+"+offset)+ "  ;"+m.Value;
                            }
                        }

                    }


                }

                string line = "          " + dis;
                if (useComment)
                {
                    while (line.Length < 40)
                    {
                        line = line + " ";
                    }

                    line = line + Comment;

                }
                textfile = textfile + line + "\n";
                lineNumber++;



            }


            return textfile;
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





    #region Emulation_data


    class TestMemory : IMemory
    {
        const int MemSize = 0x10000; // size of the memory in bytes

        private Z80Emu m_cpu;

        public byte this[int Address] { get => m_memory[Address]; set => m_memory[Address] = value; }

        public int Size => MemSize;

        private readonly byte[] m_memory = new byte[MemSize];

        public void SetCPU(Z80Emu in_cpu)
        {
            m_cpu = in_cpu;
        }

        public byte Read(ushort in_address, bool in_m1_state)
        {
            return m_memory[in_address];
        }

        public void Write(ushort in_address, byte in_value)
        {
            m_memory[in_address] = in_value;
        }

    }


    class TestPorts : IPort
    {
        private Z80Emu m_cpu;

        public void SetCPU(Z80Emu in_cpu)
        {
            m_cpu = in_cpu;
        }

        public byte Read(ushort in_address)
        {
            return (byte)(in_address >> 8);
        }

        public void Write(ushort in_address, byte in_value)
        {
        }
    }


    #endregion

}
