using System;
using System.Runtime.CompilerServices;

namespace Z80EmuLib
{
	public sealed partial class Z80Emu
	{
		/// <summary>
		/// Interrupt mode types
		/// </summary>
		public enum IMMode : byte
		{
			IM0 = 0,
			IM1 = 1,
			IM2 = 2
		};

		// delegates
		public delegate void RetiEvent(Z80Emu in_sender);

		public RetiEvent RetiEventHandler;

		// opcode delegate and table size
		private delegate void OperationDelegate();

		/// <summary>
		/// General porpose registers
		/// </summary>
		public Z80Registers m_registers;

		/// <summary>
		/// Interrupr flag 1
		/// </summary>
		public byte IFF1;

		/// <summary>
		/// Interrupr flag 2
		/// </summary>
		public byte IFF2;

		/// <summary>
		/// CPU halt flag
		/// </summary>
		public bool Halted;

		/// <summary>
		/// Interrupt mode
		/// </summary>
		public IMMode IM;

		/// <summary>
		/// Total T State counter. Counts the T states (clock cycles) since the last reset
		/// </summary>
		public ulong TotalTState;

		// Internal variables
		private uint m_T_state; // t-state clock of current/last step
		private bool m_noint_once;      // disable interrupts before next opcode?
		private bool m_reset_PV_on_int; // reset P/V flag on interrupt? (for LD A,R / LD A,I)
		private bool m_doing_opcode;    // is there an opcode currently executing?
		private byte m_int_vector_req;  // opcode must be fetched from IO device? (int vector read)
		private byte m_prefix;          // temprary storage for prefix instruction
		private byte m_tmpbyte;
		private sbyte m_tmpbyte_s;

		// interfaces
		public IMemory m_memory;    // memory access interface
		public IPort m_ports;       // Port access interface
		public IIRQRead m_int_read; // opcode reader for IM2 interrupt handling

		/// <summary>
		/// Creates Z80 CPU emulation class
		/// </summary>
		/// <param name="memory">Memory access provider class</param>
		/// <param name="inputOutputPortPorts">IO provider class</param>
		/// <param name="irqRead">Interrupt provider for IM2 mode</param>
		/// <param name="reset">True for reset the CPU after creating</param>
		public Z80Emu(IMemory memory, IPort inputOutputPortPorts, IIRQRead irqRead, bool reset = true)
		{
			m_memory = memory;
			m_memory?.SetCPU(this);

			m_ports = inputOutputPortPorts;
			m_ports?.SetCPU(this);

			m_int_read = irqRead;

			m_registers = new Z80Registers();

			InitializeOpcodes();
			if (reset) Reset();
		}

		/// <summary>
		/// Sets/changes memory access provider
		/// </summary>
		/// <param name="in_memory"></param>
		public void SetMemory(IMemory in_memory)
		{
			m_memory = in_memory;
			m_memory.SetCPU(this);
		}

		/// <summary>
		/// Sets/ changes port acces provider
		/// </summary>
		/// <param name="in_ports"></param>
		public void SetPorts(IPort in_ports)
		{
			m_ports = in_ports;
			m_ports.SetCPU(this);
		}

		/// <summary>
		/// Returns true when instruction execution is finished after Step function. If only prefi codes are processed it returns false
		/// </summary>
		public bool InstructionDone
		{
			get { return m_prefix == 0; }
		}

		/// <summary>
		/// Executes the next instruction
		/// </summary>
		/// <returns></returns>
		public uint Step()
		{
			m_doing_opcode = true;
			m_noint_once = false;
			m_reset_PV_on_int = false;
			m_T_state = 4; // set for NOP length, the actual instruction will overrite it

			byte opcode = READ_OP_M1();
			m_registers.Rcnt++;

			if (m_prefix == 0)
			{
				m_opcodes_base[opcode]();
			}
			else
			{
				if ((m_prefix | 0x20) == 0xFD && ((opcode | 0x20) == 0xFD || opcode == 0xED))
				{
					m_prefix = opcode;
					m_noint_once = true; // interrupts are not accepted immediately after prefix
				}
				else
				{
					OperationDelegate ofn;

					switch (m_prefix)
					{
						case 0xDD:
						case 0xFD:
							if (opcode == 0xCB)
							{
								byte d = READ_OP();
								m_tmpbyte_s = (sbyte)((d & 0x80) != 0 ? -(((~d) & 0x7f) + 1) : d);
								opcode = READ_OP();
								ofn = m_prefix == 0xDD ? m_opcodes_ddcb[opcode] : m_opcodes_fdcb[opcode];
							}
							else
							{
								ofn = (m_prefix == 0xDD ? m_opcodes_dd[opcode] : m_opcodes_fd[opcode]) ?? m_opcodes_base[opcode];
							}
							break;

						case 0xED:
							ofn = m_opcodes_ed[opcode] ?? m_opcodes_base[0x00];
							break;

						case 0xCB:
							ofn = m_opcodes_cb[opcode];
							break;

						default:
							// this mustn't happen!
							throw new SystemException(string.Format("Invalid prefix {0:X2}", m_prefix));
					}
					ofn();
					m_prefix = 0;
				}
			}

			m_doing_opcode = false;

			TotalTState += m_T_state;

			return m_T_state;
		}

		/// <summary>
		/// Gets the general purpose registers of the CPU
		/// </summary>
		public Z80Registers Registers
		{
			get { return m_registers; }
		}

		/// <summary>
		/// Resets the CPU
		/// </summary>
		public void Reset()
		{
			IFF1 = IFF2 = 0;
			IM = IMMode.IM0;
			m_noint_once = m_reset_PV_on_int = Halted = false;
			m_int_vector_req = 0;
			m_doing_opcode = false;
			m_T_state = 0;
			TotalTState = 0;
			m_prefix = 0;

			Registers.Reset();
		}

		#region · IRQ  handlers ·

		/// <summary>
		/// NMI hanbdler
		/// </summary>
		/// <returns></returns>
		public ulong Nmi()
		{
			if (m_doing_opcode || m_noint_once || (m_prefix != 0)) return 0;

			if (Halted) { Registers.PC++; Halted = false; } // so we met an interrupt... stop waiting

			m_doing_opcode = true;

			m_registers.Rcnt++; // accepting interrupt increases R by one

			// IFF2=IFF1
			// contrary to zilog z80 docs, IFF2 is not modified on NMI. proved by Slava Tretiak aka restorer
			IFF1 = 0;

			m_memory.Write(--Registers.SP, Registers.PCh); // PUSH PC -- high byte

			m_memory.Write(--Registers.SP, Registers.PCl); // PUSH PC -- low byte

			Registers.PC = 0x0066;
			Registers.WZ = Registers.PC; // FIXME: is that really so?

			m_doing_opcode = false;

			return 11; // NMI always takes 11 t-states
		}

		/// <summary>
		/// INT handler
		/// </summary>
		/// <returns></returns>
		public ulong Int()
		{
			// If the INT line is low and iff1 is set, and there's no opcode executing just now,
			// a maskable interrupt is accepted, whether or not the
			// last INT routine has finished
			if ((IFF1 == 0) || m_noint_once || m_doing_opcode || (m_prefix != 0)) return 0;

			m_T_state = 0;

			if (Halted) { Registers.PC++; Halted = false; } // so we met an interrupt... stop waiting

			// When an INT is accepted, both iff1 and IFF2 are cleared, preventing another interrupt from
			// occurring which would end up as an infinite loop
			IFF1 = IFF2 = 0;

			// original (NMOS) zilog z80 bug:
			// If a LD A,I or LD A,R (which copy IFF2 to the P/V flag) is interrupted, then the P/V flag is reset, even if interrupts were enabled beforehand.
			// (this bug was fixed in CMOS version of z80)
			if (m_reset_PV_on_int) { Registers.F = (byte)(Registers.F & ~Tables.FLAG_P); }
			m_reset_PV_on_int = false;

			m_int_vector_req = 1;
			m_doing_opcode = true;

			switch (IM)
			{
				case IMMode.IM0:
					// note: there's no need to do R++ and WAITs here, it'll be handled by z80ex_step
					uint tt = Step();

					while (m_prefix != 0)
					{ // this is not the end?
						tt += Step();
					}

					m_T_state = tt;
					break;

				case IMMode.IM1:
					Registers.Rcnt++;
					m_opcodes_base[0xFF](); // RST38
					break;

				case IMMode.IM2:
					Registers.Rcnt++;
					// takes 19 clock periods to complete (seven to fetch the
					// lower eight bits from the interrupting device, six to save the program
					// counter, and six to obtain the jump address)
					byte iv = READ_OP();
					ushort inttemp = (ushort)((0x100 * Registers.I) + iv);

					PUSH(Registers.PC);

					Registers.PCl = CPUReadMemory(inttemp++);
					Registers.PCh = CPUReadMemory(inttemp);
					Registers.WZ = Registers.PC;

					break;
			}

			m_doing_opcode = false;
			m_int_vector_req = 0;

			return m_T_state;
		}

		#endregion

		#region · Memory and port read / write ·

		/// <summary>
		/// Reads opcode
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		byte READ_OP_M1()
		{
			return m_int_vector_req != 0 ? m_int_read.Read(this) : m_memory.Read(Registers.PC++, true);
		}

		/// <summary>
		/// Reads opcode argument
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		byte READ_OP()
		{
			return m_int_vector_req != 0 ? m_int_read.Read(this) : m_memory.Read(Registers.PC++);
		}

		/// <summary>
		/// Set T state for the current instruction
		/// </summary>
		/// <param name="in_t_state"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CPUSetTState(uint in_t_state)
		{
			m_T_state = in_t_state;
		}

		/**
		 * read byte from memory
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		byte CPUReadMemory(ushort in_address)
		{
			return m_memory.Read(in_address);
			//return (byte)MemoryRD?.Invoke(in_address, false);
		}

		/// <summary>
		/// Reads byte from port
		/// </summary>
		/// <param name="port"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		byte CPUReadPort(ushort port)
		{
			return m_ports.Read(port);
		}

		/// <summary>
		/// Writes byte to memory
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="val"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPUWriteMemory(ushort addr, byte val)
		{
			m_memory.Write(addr, val);
			//MemoryWR?.Invoke(addr, val);
		}

		/// <summary>
		/// Writes byte to port
		/// </summary>
		/// <param name="port"></param>
		/// <param name="val"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPUWritePort(ushort port, byte val)
		{
			m_ports.Write(port, val);
		}

		public override string ToString()
		{
			string ret = string.Format("PC {0} SP {1}\n", Registers.PC, Registers.SP);
			ret += string.Format("AF {0} BC {1} DE {2} HL {3}\n", Registers.AF, Registers.BC, Registers.DE, Registers.HL);
			ret += string.Format("AF' {0} BC' {1} DE' {2} HL' {3}\n", Registers._AF_, Registers._BC_, Registers._DE_, Registers._HL_);
			ret += string.Format("IX {0} IY {1}\n", Registers.IX, Registers.IY);
			ret += string.Format("R {0:X2} I {1} IFF1 {2} IFF2 {3} {4} H {5}\n", Registers.R, Registers.I, IFF1, IFF2, IM, Halted);

			return ret;
		}

		#endregion

		#region · Private members ·  

		void InitializeOpcodes()
		{
			InitializeOpcodesBase();
			InitializeOpcodesCB();
			InitializeOpcodesDD();
			InitializeOpcodesDDCB();
			InitializeOpcodesED();
			InitializeOpcodesFD();
			InitializeOpcodesFDCB();
		}

		#endregion
	}
}
