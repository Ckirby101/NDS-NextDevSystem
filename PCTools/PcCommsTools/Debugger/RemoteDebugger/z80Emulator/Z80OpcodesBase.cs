namespace Z80EmuLib
{
	partial class Z80Emu
	{
		OperationDelegate[] m_opcodes_base;

		void InitializeOpcodesBase()
		{
			m_opcodes_base = new OperationDelegate[] {
								 op_0x00, op_0x01, op_0x02, op_0x03,
								 op_0x04, op_0x05, op_0x06, op_0x07,
								 op_0x08, op_0x09, op_0x0a, op_0x0b,
								 op_0x0c, op_0x0d, op_0x0e, op_0x0f,
								 op_0x10, op_0x11, op_0x12, op_0x13,
								 op_0x14, op_0x15, op_0x16, op_0x17,
								 op_0x18, op_0x19, op_0x1a, op_0x1b,
								 op_0x1c, op_0x1d, op_0x1e, op_0x1f,
								 op_0x20, op_0x21, op_0x22, op_0x23,
								 op_0x24, op_0x25, op_0x26, op_0x27,
								 op_0x28, op_0x29, op_0x2a, op_0x2b,
								 op_0x2c, op_0x2d, op_0x2e, op_0x2f,
								 op_0x30, op_0x31, op_0x32, op_0x33,
								 op_0x34, op_0x35, op_0x36, op_0x37,
								 op_0x38, op_0x39, op_0x3a, op_0x3b,
								 op_0x3c, op_0x3d, op_0x3e, op_0x3f,
								 op_0x40, op_0x41, op_0x42, op_0x43,
								 op_0x44, op_0x45, op_0x46, op_0x47,
								 op_0x48, op_0x49, op_0x4a, op_0x4b,
								 op_0x4c, op_0x4d, op_0x4e, op_0x4f,
								 op_0x50, op_0x51, op_0x52, op_0x53,
								 op_0x54, op_0x55, op_0x56, op_0x57,
								 op_0x58, op_0x59, op_0x5a, op_0x5b,
								 op_0x5c, op_0x5d, op_0x5e, op_0x5f,
								 op_0x60, op_0x61, op_0x62, op_0x63,
								 op_0x64, op_0x65, op_0x66, op_0x67,
								 op_0x68, op_0x69, op_0x6a, op_0x6b,
								 op_0x6c, op_0x6d, op_0x6e, op_0x6f,
								 op_0x70, op_0x71, op_0x72, op_0x73,
								 op_0x74, op_0x75, op_0x76, op_0x77,
								 op_0x78, op_0x79, op_0x7a, op_0x7b,
								 op_0x7c, op_0x7d, op_0x7e, op_0x7f,
								 op_0x80, op_0x81, op_0x82, op_0x83,
								 op_0x84, op_0x85, op_0x86, op_0x87,
								 op_0x88, op_0x89, op_0x8a, op_0x8b,
								 op_0x8c, op_0x8d, op_0x8e, op_0x8f,
								 op_0x90, op_0x91, op_0x92, op_0x93,
								 op_0x94, op_0x95, op_0x96, op_0x97,
								 op_0x98, op_0x99, op_0x9a, op_0x9b,
								 op_0x9c, op_0x9d, op_0x9e, op_0x9f,
								 op_0xa0, op_0xa1, op_0xa2, op_0xa3,
								 op_0xa4, op_0xa5, op_0xa6, op_0xa7,
								 op_0xa8, op_0xa9, op_0xaa, op_0xab,
								 op_0xac, op_0xad, op_0xae, op_0xaf,
								 op_0xb0, op_0xb1, op_0xb2, op_0xb3,
								 op_0xb4, op_0xb5, op_0xb6, op_0xb7,
								 op_0xb8, op_0xb9, op_0xba, op_0xbb,
								 op_0xbc, op_0xbd, op_0xbe, op_0xbf,
								 op_0xc0, op_0xc1, op_0xc2, op_0xc3,
								 op_0xc4, op_0xc5, op_0xc6, op_0xc7,
								 op_0xc8, op_0xc9, op_0xca, op_p_CB,
								 op_0xcc, op_0xcd, op_0xce, op_0xcf,
								 op_0xd0, op_0xd1, op_0xd2, op_0xd3,
								 op_0xd4, op_0xd5, op_0xd6, op_0xd7,
								 op_0xd8, op_0xd9, op_0xda, op_0xdb,
								 op_0xdc, op_p_DD, op_0xde, op_0xdf,
								 op_0xe0, op_0xe1, op_0xe2, op_0xe3,
								 op_0xe4, op_0xe5, op_0xe6, op_0xe7,
								 op_0xe8, op_0xe9, op_0xea, op_0xeb,
								 op_0xec, op_p_ED, op_0xee, op_0xef,
								 op_0xf0, op_0xf1, op_0xf2, op_0xf3,
								 op_0xf4, op_0xf5, op_0xf6, op_0xf7,
								 op_0xf8, op_0xf9, op_0xfa, op_0xfb,
								 op_0xfc, op_p_FD, op_0xfe, op_0xff
						};
		}

		/*NOP*/
		void op_0x00()
		{
			CPUSetTState(4);
		}

		/*LD BC,@*/
		void op_0x01()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			LD16(ref Registers.BC, Registers.TD);

			CPUSetTState(10);
		}

		/*LD (BC),Registers.A*/
		void op_0x02()
		{
			LD_A_TO_ADDR_MPTR(ref m_tmpbyte, Registers.A, Registers.BC);
			CPUWriteMemory((Registers.BC), m_tmpbyte);

			CPUSetTState(7);
		}

		/*INC BC*/
		void op_0x03()
		{
			INC16(ref Registers.BC);

			CPUSetTState(6);
		}

		/*INC Registers.B*/
		void op_0x04()
		{
			INC(ref Registers.B);

			CPUSetTState(4);
		}

		/*DEC Registers.B*/
		void op_0x05()
		{
			DEC(ref Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.B,#*/
		void op_0x06()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.B, m_tmpbyte);

			CPUSetTState(7);
		}

		/*RLCA*/
		void op_0x07()
		{
			RLCA();

			CPUSetTState(4);
		}

		/*EX Registers.AF.w, Registers.AF.w'*/
		void op_0x08()
		{
			Registers.ExchangeAfSet();

			CPUSetTState(4);
		}

		/*ADD Registers.HL, BC*/
		void op_0x09()
		{
			ADD16(ref Registers.HL, Registers.BC);

			CPUSetTState(11);
		}

		/*LD Registers.A,(BC)*/
		void op_0x0a()
		{
			m_tmpbyte = CPUReadMemory(Registers.BC);
			LD_A_FROM_ADDR_MPTR(ref Registers.A, m_tmpbyte, Registers.BC);

			CPUSetTState(7);
		}

		/*DEC BC*/
		void op_0x0b()
		{
			DEC16(ref Registers.BC);

			CPUSetTState(6);
		}

		/*INC Registers.C*/
		void op_0x0c()
		{
			INC(ref Registers.C);

			CPUSetTState(4);
		}

		/*DEC Registers.C*/
		void op_0x0d()
		{
			DEC(ref Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.C,#*/
		void op_0x0e()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.C, m_tmpbyte);

			CPUSetTState(7);
		}

		/*RRCA*/
		void op_0x0f()
		{
			RRCA();

			CPUSetTState(4);
		}

		/*DJNZ %*/
		void op_0x10()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			DJNZ(m_tmpbyte_s, /*t:*/ /*t1*/8,/*t2*/13);
		}

		/*LD Registers.DE,@*/
		void op_0x11()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			LD16(ref Registers.DE, Registers.TD);

			CPUSetTState(10);
		}

		/*LD (Registers.DE),Registers.A*/
		void op_0x12()
		{
			LD_A_TO_ADDR_MPTR(ref m_tmpbyte, Registers.A, Registers.DE);
			CPUWriteMemory((Registers.DE), m_tmpbyte);

			CPUSetTState(7);
		}

		/*INC Registers.DE*/
		void op_0x13()
		{
			INC16(ref Registers.DE);

			CPUSetTState(6);
		}

		/*INC Registers.D*/
		void op_0x14()
		{
			INC(ref Registers.D);

			CPUSetTState(4);
		}

		/*DEC Registers.D*/
		void op_0x15()
		{
			DEC(ref Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.D,#*/
		void op_0x16()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.D, m_tmpbyte);

			CPUSetTState(7);
		}

		/*RLA*/
		void op_0x17()
		{
			RLA();

			CPUSetTState(4);
		}

		/*JR %*/
		void op_0x18()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			JR(m_tmpbyte_s);

			CPUSetTState(12);
		}

		/*ADD Registers.HL, Registers.DE*/
		void op_0x19()
		{
			ADD16(ref Registers.HL, Registers.DE);

			CPUSetTState(11);
		}

		/*LD Registers.A,(Registers.DE)*/
		void op_0x1a()
		{
			m_tmpbyte = CPUReadMemory(Registers.DE);
			LD_A_FROM_ADDR_MPTR(ref Registers.A, m_tmpbyte, Registers.DE);

			CPUSetTState(7);
		}

		/*DEC Registers.DE*/
		void op_0x1b()
		{
			DEC16(ref Registers.DE);

			CPUSetTState(6);
		}

		/*INC Registers.E*/
		void op_0x1c()
		{
			INC(ref Registers.E);

			CPUSetTState(4);
		}

		/*DEC Registers.E*/
		void op_0x1d()
		{
			DEC(ref Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.E,#*/
		void op_0x1e()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.E, m_tmpbyte);

			CPUSetTState(7);
		}

		/*RRA*/
		void op_0x1f()
		{
			RRA();

			CPUSetTState(4);
		}

		/*JR NZ,%*/
		void op_0x20()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			if ((Registers.F & Tables.FLAG_Z) == 0)
			{
				JR(m_tmpbyte_s);

				CPUSetTState(12);
			}
			else
			{
				CPUSetTState(7);
			}
		}

		/*LD Registers.HL,@*/
		void op_0x21()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			LD16(ref Registers.HL, Registers.TD);

			CPUSetTState(10);
		}

		/*LD (@),Registers.HL*/
		void op_0x22()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.HL, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}

		/*INC Registers.HL*/
		void op_0x23()
		{
			INC16(ref Registers.HL);

			CPUSetTState(6);
		}

		/*INC Registers.H*/
		void op_0x24()
		{
			INC(ref Registers.H);

			CPUSetTState(4);
		}

		/*DEC Registers.H*/
		void op_0x25()
		{
			DEC(ref Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.H,#*/
		void op_0x26()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.H, m_tmpbyte);

			CPUSetTState(7);
		}

		/*DAA*/
		void op_0x27()
		{
			DAA();

			CPUSetTState(4);
		}

		/*JR Z,%*/
		void op_0x28()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			if ((Registers.F & Tables.FLAG_Z) != 0)
			{
				JR(m_tmpbyte_s);

				CPUSetTState(12);
			}
			else
			{
				CPUSetTState(7);
			}
		}

		/*ADD Registers.HL, Registers.HL*/
		void op_0x29()
		{
			ADD16(ref Registers.HL, Registers.HL);

			CPUSetTState(11);
		}

		/*LD Registers.HL,(@)*/
		void op_0x2a()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.HL, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}

		/*DEC Registers.HL*/
		void op_0x2b()
		{
			DEC16(ref Registers.HL);

			CPUSetTState(6);
		}

		/*INC Registers.L*/
		void op_0x2c()
		{
			INC(ref Registers.L);

			CPUSetTState(4);
		}

		/*DEC Registers.L*/
		void op_0x2d()
		{
			DEC(ref Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.L,#*/
		void op_0x2e()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.L, m_tmpbyte);

			CPUSetTState(7);
		}

		/*CPL*/
		void op_0x2f()
		{
			CPL();

			CPUSetTState(4);
		}

		/*JR NC,%*/
		void op_0x30()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			if ((Registers.F & Tables.FLAG_C) == 0)
			{
				JR(m_tmpbyte_s);

				CPUSetTState(12);
			}
			else
			{
				CPUSetTState(7);
			}
		}

		/*LD Registers.SP,@*/
		void op_0x31()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			LD16(ref Registers.SP, Registers.TD);

			CPUSetTState(10);
		}

		/*LD (@),Registers.A*/
		void op_0x32()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_A_TO_ADDR_MPTR(ref m_tmpbyte, Registers.A, Registers.TA);
			CPUWriteMemory(Registers.TA, m_tmpbyte);

			CPUSetTState(13);
		}

		/*INC Registers.SP*/
		void op_0x33()
		{
			INC16(ref Registers.SP);

			CPUSetTState(6);
		}

		/*INC Registers.HL*/
		void op_0x34()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			INC(ref m_tmpbyte);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(11);
		}

		/*DEC Registers.HL*/
		void op_0x35()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			DEC(ref m_tmpbyte);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(11);
		}

		/*LD Registers.HL,#*/
		void op_0x36()
		{
			m_tmpbyte = READ_OP();
			LD(ref m_tmpbyte, m_tmpbyte);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(10);
		}

		/*SCF*/
		void op_0x37()
		{
			SCF();

			CPUSetTState(4);
		}

		/*JR Registers.C,%*/
		void op_0x38()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			if ((Registers.F & Tables.FLAG_C) != 0)
			{
				JR(m_tmpbyte_s);
				CPUSetTState(12);
			}
			else
			{
				CPUSetTState(7);
			}
		}

		/*ADD Registers.HL, Registers.SP*/
		void op_0x39()
		{
			ADD16(ref Registers.HL, Registers.SP);

			CPUSetTState(11);
		}

		/*LD Registers.A,(@)*/
		void op_0x3a()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			m_tmpbyte = CPUReadMemory(Registers.TA);
			LD_A_FROM_ADDR_MPTR(ref Registers.A, m_tmpbyte, Registers.TA);

			CPUSetTState(13);
		}

		/*DEC Registers.SP*/
		void op_0x3b()
		{
			DEC16(ref Registers.SP);

			CPUSetTState(6);
		}

		/*INC Registers.A*/
		void op_0x3c()
		{
			INC(ref Registers.A);

			CPUSetTState(4);
		}

		/*DEC Registers.A*/
		void op_0x3d()
		{
			DEC(ref Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.A,#*/
		void op_0x3e()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.A, m_tmpbyte);

			CPUSetTState(7);
		}

		/*CCF*/
		void op_0x3f()
		{
			CCF();

			CPUSetTState(4);
		}

		/*LD Registers.B, Registers.B*/
		void op_0x40()
		{
			LD(ref Registers.B, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.B, Registers.C*/
		void op_0x41()
		{
			LD(ref Registers.B, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.B, Registers.D*/
		void op_0x42()
		{
			LD(ref Registers.B, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.B, Registers.E*/
		void op_0x43()
		{
			LD(ref Registers.B, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.B, Registers.H*/
		void op_0x44()
		{
			LD(ref Registers.B, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.B, Registers.L*/
		void op_0x45()
		{
			LD(ref Registers.B, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.B,Registers.HL*/
		void op_0x46()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.B, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.B, Registers.A*/
		void op_0x47()
		{
			LD(ref Registers.B, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.C, Registers.B*/
		void op_0x48()
		{
			LD(ref Registers.C, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.C, Registers.C*/
		void op_0x49()
		{
			LD(ref Registers.C, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.C, Registers.D*/
		void op_0x4a()
		{
			LD(ref Registers.C, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.C, Registers.E*/
		void op_0x4b()
		{
			LD(ref Registers.C, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.C, Registers.H*/
		void op_0x4c()
		{
			LD(ref Registers.C, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.C, Registers.L*/
		void op_0x4d()
		{
			LD(ref Registers.C, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.C,Registers.HL*/
		void op_0x4e()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.C, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.C, Registers.A*/
		void op_0x4f()
		{
			LD(ref Registers.C, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.D, Registers.B*/
		void op_0x50()
		{
			LD(ref Registers.D, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.D, Registers.C*/
		void op_0x51()
		{
			LD(ref Registers.D, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.D, Registers.D*/
		void op_0x52()
		{
			LD(ref Registers.D, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.D, Registers.E*/
		void op_0x53()
		{
			LD(ref Registers.D, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.D, Registers.H*/
		void op_0x54()
		{
			LD(ref Registers.D, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.D, Registers.L*/
		void op_0x55()
		{
			LD(ref Registers.D, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.D,Registers.HL*/
		void op_0x56()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.D, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.D, Registers.A*/
		void op_0x57()
		{
			LD(ref Registers.D, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.E, Registers.B*/
		void op_0x58()
		{
			LD(ref Registers.E, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.E, Registers.C*/
		void op_0x59()
		{
			LD(ref Registers.E, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.E, Registers.D*/
		void op_0x5a()
		{
			LD(ref Registers.E, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.E, Registers.E*/
		void op_0x5b()
		{
			LD(ref Registers.E, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.E, Registers.H*/
		void op_0x5c()
		{
			LD(ref Registers.E, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.E, Registers.L*/
		void op_0x5d()
		{
			LD(ref Registers.E, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.E,Registers.HL*/
		void op_0x5e()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.E, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.E, Registers.A*/
		void op_0x5f()
		{
			LD(ref Registers.E, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.H, Registers.B*/
		void op_0x60()
		{
			LD(ref Registers.H, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.H, Registers.C*/
		void op_0x61()
		{
			LD(ref Registers.H, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.H, Registers.D*/
		void op_0x62()
		{
			LD(ref Registers.H, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.H, Registers.E*/
		void op_0x63()
		{
			LD(ref Registers.H, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.H, Registers.H*/
		void op_0x64()
		{
			LD(ref Registers.H, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.H, Registers.L*/
		void op_0x65()
		{
			LD(ref Registers.H, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.H,Registers.HL*/
		void op_0x66()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.H, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.H, Registers.A*/
		void op_0x67()
		{
			LD(ref Registers.H, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.L, Registers.B*/
		void op_0x68()
		{
			LD(ref Registers.L, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.L, Registers.C*/
		void op_0x69()
		{
			LD(ref Registers.L, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.L, Registers.D*/
		void op_0x6a()
		{
			LD(ref Registers.L, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.L, Registers.E*/
		void op_0x6b()
		{
			LD(ref Registers.L, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.L, Registers.H*/
		void op_0x6c()
		{
			LD(ref Registers.L, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.L, Registers.L*/
		void op_0x6d()
		{
			LD(ref Registers.L, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.L,Registers.HL*/
		void op_0x6e()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.L, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.L, Registers.A*/
		void op_0x6f()
		{
			LD(ref Registers.L, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.HL,Registers.B*/
		void op_0x70()
		{
			LD(ref m_tmpbyte, Registers.B);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.HL,Registers.C*/
		void op_0x71()
		{
			LD(ref m_tmpbyte, Registers.C);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.HL,Registers.D*/
		void op_0x72()
		{
			LD(ref m_tmpbyte, Registers.D);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.HL,Registers.E*/
		void op_0x73()
		{
			LD(ref m_tmpbyte, Registers.E);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.HL,Registers.H*/
		void op_0x74()
		{
			LD(ref m_tmpbyte, Registers.H);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.HL,Registers.L*/
		void op_0x75()
		{
			LD(ref m_tmpbyte, Registers.L);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*HALT*/
		void op_0x76()
		{
			HALT();

			CPUSetTState(4);
		}

		/*LD Registers.HL,Registers.A*/
		void op_0x77()
		{
			LD(ref m_tmpbyte, Registers.A);
			CPUWriteMemory(Registers.HL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.A, Registers.B*/
		void op_0x78()
		{
			LD(ref Registers.A, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.A, Registers.C*/
		void op_0x79()
		{
			LD(ref Registers.A, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.A, Registers.D*/
		void op_0x7a()
		{
			LD(ref Registers.A, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.A, Registers.E*/
		void op_0x7b()
		{
			LD(ref Registers.A, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.A, Registers.H*/
		void op_0x7c()
		{
			LD(ref Registers.A, Registers.H);

			CPUSetTState(4);
		}

		/*LD Registers.A, Registers.L*/
		void op_0x7d()
		{
			LD(ref Registers.A, Registers.L);

			CPUSetTState(4);
		}

		/*LD Registers.A,Registers.HL*/
		void op_0x7e()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			LD(ref Registers.A, m_tmpbyte);

			CPUSetTState(7);
		}

		/*LD Registers.A, Registers.A*/
		void op_0x7f()
		{
			LD(ref Registers.A, Registers.A);

			CPUSetTState(4);
		}

		/*ADD Registers.A, Registers.B*/
		void op_0x80()
		{
			ADD(Registers.B);

			CPUSetTState(4);
		}

		/*ADD Registers.A, Registers.C*/
		void op_0x81()
		{
			ADD(Registers.C);

			CPUSetTState(4);
		}

		/*ADD Registers.A, Registers.D*/
		void op_0x82()
		{
			ADD(Registers.D);

			CPUSetTState(4);
		}

		/*ADD Registers.A, Registers.E*/
		void op_0x83()
		{
			ADD(Registers.E);

			CPUSetTState(4);
		}

		/*ADD Registers.A, Registers.H*/
		void op_0x84()
		{
			ADD(Registers.H);

			CPUSetTState(4);
		}

		/*ADD Registers.A, Registers.L*/
		void op_0x85()
		{
			ADD(Registers.L);

			CPUSetTState(4);
		}

		/*ADD Registers.A,Registers.HL*/
		void op_0x86()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			ADD(m_tmpbyte);

			CPUSetTState(7);
		}

		/*ADD Registers.A, Registers.A*/
		void op_0x87()
		{
			ADD(Registers.A);

			CPUSetTState(4);
		}

		/*ADC Registers.A, Registers.B*/
		void op_0x88()
		{
			ADC(Registers.B);

			CPUSetTState(4);
		}

		/*ADC Registers.A, Registers.C*/
		void op_0x89()
		{
			ADC(Registers.C);

			CPUSetTState(4);
		}

		/*ADC Registers.A, Registers.D*/
		void op_0x8a()
		{
			ADC(Registers.D);

			CPUSetTState(4);
		}

		/*ADC Registers.A, Registers.E*/
		void op_0x8b()
		{
			ADC(Registers.E);

			CPUSetTState(4);
		}

		/*ADC Registers.A, Registers.H*/
		void op_0x8c()
		{
			ADC(Registers.H);

			CPUSetTState(4);
		}

		/*ADC Registers.A, Registers.L*/
		void op_0x8d()
		{
			ADC(Registers.L);

			CPUSetTState(4);
		}

		/*ADC Registers.A,Registers.HL*/
		void op_0x8e()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			ADC(m_tmpbyte);

			CPUSetTState(7);
		}

		/*ADC Registers.A, Registers.A*/
		void op_0x8f()
		{
			ADC(Registers.A);

			CPUSetTState(4);
		}

		/*SUB Registers.B*/
		void op_0x90()
		{
			SUB(Registers.B);

			CPUSetTState(4);
		}

		/*SUB Registers.C*/
		void op_0x91()
		{
			SUB(Registers.C);

			CPUSetTState(4);
		}

		/*SUB Registers.D*/
		void op_0x92()
		{
			SUB(Registers.D);

			CPUSetTState(4);
		}

		/*SUB Registers.E*/
		void op_0x93()
		{
			SUB(Registers.E);

			CPUSetTState(4);
		}

		/*SUB Registers.H*/
		void op_0x94()
		{
			SUB(Registers.H);

			CPUSetTState(4);
		}

		/*SUB Registers.L*/
		void op_0x95()
		{
			SUB(Registers.L);

			CPUSetTState(4);
		}

		/*SUB Registers.HL*/
		void op_0x96()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			SUB(m_tmpbyte);

			CPUSetTState(7);
		}

		/*SUB Registers.A*/
		void op_0x97()
		{
			SUB(Registers.A);

			CPUSetTState(4);
		}

		/*SBC Registers.A, Registers.B*/
		void op_0x98()
		{
			SBC(Registers.B);

			CPUSetTState(4);
		}

		/*SBC Registers.A, Registers.C*/
		void op_0x99()
		{
			SBC(Registers.C);

			CPUSetTState(4);
		}

		/*SBC Registers.A, Registers.D*/
		void op_0x9a()
		{
			SBC(Registers.D);

			CPUSetTState(4);
		}

		/*SBC Registers.A, Registers.E*/
		void op_0x9b()
		{
			SBC(Registers.E);

			CPUSetTState(4);
		}

		/*SBC Registers.A, Registers.H*/
		void op_0x9c()
		{
			SBC(Registers.H);

			CPUSetTState(4);
		}

		/*SBC Registers.A, Registers.L*/
		void op_0x9d()
		{
			SBC(Registers.L);

			CPUSetTState(4);
		}

		/*SBC Registers.A,Registers.HL*/
		void op_0x9e()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			SBC(m_tmpbyte);

			CPUSetTState(7);
		}

		/*SBC Registers.A, Registers.A*/
		void op_0x9f()
		{
			SBC(Registers.A);

			CPUSetTState(4);
		}

		/*AND Registers.B*/
		void op_0xa0()
		{
			AND(Registers.B);

			CPUSetTState(4);
		}

		/*AND Registers.C*/
		void op_0xa1()
		{
			AND(Registers.C);

			CPUSetTState(4);
		}

		/*AND Registers.D*/
		void op_0xa2()
		{
			AND(Registers.D);

			CPUSetTState(4);
		}

		/*AND Registers.E*/
		void op_0xa3()
		{
			AND(Registers.E);

			CPUSetTState(4);
		}

		/*AND Registers.H*/
		void op_0xa4()
		{
			AND(Registers.H);

			CPUSetTState(4);
		}

		/*AND Registers.L*/
		void op_0xa5()
		{
			AND(Registers.L);

			CPUSetTState(4);
		}

		/*AND Registers.HL*/
		void op_0xa6()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			AND(m_tmpbyte);

			CPUSetTState(7);
		}

		/*AND Registers.A*/
		void op_0xa7()
		{
			AND(Registers.A);

			CPUSetTState(4);
		}

		/*XOR Registers.B*/
		void op_0xa8()
		{
			XOR(Registers.B);

			CPUSetTState(4);
		}

		/*XOR Registers.C*/
		void op_0xa9()
		{
			XOR(Registers.C);

			CPUSetTState(4);
		}

		/*XOR Registers.D*/
		void op_0xaa()
		{
			XOR(Registers.D);

			CPUSetTState(4);
		}

		/*XOR Registers.E*/
		void op_0xab()
		{
			XOR(Registers.E);

			CPUSetTState(4);
		}

		/*XOR Registers.H*/
		void op_0xac()
		{
			XOR(Registers.H);

			CPUSetTState(4);
		}

		/*XOR Registers.L*/
		void op_0xad()
		{
			XOR(Registers.L);

			CPUSetTState(4);
		}

		/*XOR Registers.HL*/
		void op_0xae()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			XOR(m_tmpbyte);

			CPUSetTState(7);
		}

		/*XOR Registers.A*/
		void op_0xaf()
		{
			XOR(Registers.A);

			CPUSetTState(4);
		}

		/*OR Registers.B*/
		void op_0xb0()
		{
			OR(Registers.B);

			CPUSetTState(4);
		}

		/*OR Registers.C*/
		void op_0xb1()
		{
			OR(Registers.C);

			CPUSetTState(4);
		}

		/*OR Registers.D*/
		void op_0xb2()
		{
			OR(Registers.D);

			CPUSetTState(4);
		}

		/*OR Registers.E*/
		void op_0xb3()
		{
			OR(Registers.E);

			CPUSetTState(4);
		}

		/*OR Registers.H*/
		void op_0xb4()
		{
			OR(Registers.H);

			CPUSetTState(4);
		}

		/*OR Registers.L*/
		void op_0xb5()
		{
			OR(Registers.L);

			CPUSetTState(4);
		}

		/*OR Registers.HL*/
		void op_0xb6()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			OR(m_tmpbyte);

			CPUSetTState(7);
		}

		/*OR Registers.A*/
		void op_0xb7()
		{
			OR(Registers.A);

			CPUSetTState(4);
		}

		/*CP Registers.B*/
		void op_0xb8()
		{
			CP(Registers.B);

			CPUSetTState(4);
		}

		/*CP Registers.C*/
		void op_0xb9()
		{
			CP(Registers.C);

			CPUSetTState(4);
		}

		/*CP Registers.D*/
		void op_0xba()
		{
			CP(Registers.D);

			CPUSetTState(4);
		}

		/*CP Registers.E*/
		void op_0xbb()
		{
			CP(Registers.E);

			CPUSetTState(4);
		}

		/*CP Registers.H*/
		void op_0xbc()
		{
			CP(Registers.H);

			CPUSetTState(4);
		}

		/*CP Registers.L*/
		void op_0xbd()
		{
			CP(Registers.L);

			CPUSetTState(4);
		}

		/*CP Registers.HL*/
		void op_0xbe()
		{
			m_tmpbyte = CPUReadMemory(Registers.HL);
			CP(m_tmpbyte);

			CPUSetTState(7);
		}

		/*CP Registers.A*/
		void op_0xbf()
		{
			CP(Registers.A);

			CPUSetTState(4);
		}

		/*RET NZ*/
		void op_0xc0()
		{
			if ((Registers.F & Tables.FLAG_Z) == 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*POP BC*/
		void op_0xc1()
		{
			POP(ref Registers.BC);

			CPUSetTState(10);
		}

		/*JP NZ,@*/
		void op_0xc2()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_Z) == 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*JP @*/
		void op_0xc3()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			JP(Registers.TD);

			CPUSetTState(10);
		}

		/*CALL NZ,@*/
		void op_0xc4()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_Z) == 0)
			{
				CALL(Registers.TD);

				CPUSetTState(17);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*PUSH BC*/
		void op_0xc5()
		{
			PUSH(Registers.BC);

			CPUSetTState(11);
		}

		/*ADD Registers.A,#*/
		void op_0xc6()
		{
			m_tmpbyte = READ_OP();
			ADD(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x00*/
		void op_0xc7()
		{
			RST(0x00);

			CPUSetTState(11);
		}

		/*RET Z*/
		void op_0xc8()
		{
			if ((Registers.F & Tables.FLAG_Z) != 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*RET*/
		void op_0xc9()
		{
			RET();

			CPUSetTState(10);
		}

		/*JP Z,@*/
		void op_0xca()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_Z) != 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;
				CPUSetTState(10);
			}
		}

		void op_p_CB()
		{
			m_prefix = 0xCB;
			m_noint_once = true;
		}

		/*CALL Z,@*/
		void op_0xcc()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_Z) != 0)
			{
				CALL(Registers.TD);

				CPUSetTState(17);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*CALL @*/
		void op_0xcd()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			CALL(Registers.TD);

			CPUSetTState(17);
		}

		/*ADC Registers.A,#*/
		void op_0xce()
		{
			m_tmpbyte = READ_OP();
			ADC(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x08*/
		void op_0xcf()
		{
			RST(0x08);

			CPUSetTState(11);
		}

		/*RET NC*/
		void op_0xd0()
		{
			if ((Registers.F & Tables.FLAG_C) == 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*POP Registers.DE*/
		void op_0xd1()
		{
			POP(ref Registers.DE);

			CPUSetTState(10);
		}

		/*JP NC,@*/
		void op_0xd2()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_C) == 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*OUT (#),Registers.A*/
		void op_0xd3()
		{
			Registers.TD = (ushort)(READ_OP() + (Registers.A << 8));
			OUT_A(Registers.TD, Registers.A);

			CPUSetTState(11);
		}

		/*CALL NC,@*/
		void op_0xd4()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_C) == 0)
			{
				CALL(Registers.TD);

				CPUSetTState(17);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*PUSH Registers.DE*/
		void op_0xd5()
		{
			PUSH(Registers.DE);

			CPUSetTState(11);
		}

		/*SUB #*/
		void op_0xd6()
		{
			m_tmpbyte = READ_OP();
			SUB(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x10*/
		void op_0xd7()
		{
			RST(0x10);

			CPUSetTState(11);
		}

		/*RET Registers.C*/
		void op_0xd8()
		{
			if ((Registers.F & Tables.FLAG_C) != 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*EXX*/
		void op_0xd9()
		{
			EXX();

			CPUSetTState(4);
		}

		/*JP Registers.C,@*/
		void op_0xda()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_C) != 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*IN Registers.A,(#)*/
		void op_0xdb()
		{
			Registers.TD = (ushort)(READ_OP() + (Registers.A << 8));
			IN_A(ref Registers.A, Registers.TD);

			CPUSetTState(11);
		}

		/*CALL Registers.C,@*/
		void op_0xdc()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_C) != 0)
			{
				CALL(Registers.TD);

				CPUSetTState(17);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		void op_p_DD()
		{
			m_prefix = 0xDD;
			m_noint_once = true;
		}

		/*SBC Registers.A,#*/
		void op_0xde()
		{
			m_tmpbyte = READ_OP();
			SBC(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x18*/
		void op_0xdf()
		{
			RST(0x18);

			CPUSetTState(11);
		}

		/*RET PO*/
		void op_0xe0()
		{
			if ((Registers.F & Tables.FLAG_P) == 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*POP Registers.HL*/
		void op_0xe1()
		{
			POP(ref Registers.HL);

			CPUSetTState(10);
		}

		/*JP PO,@*/
		void op_0xe2()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_P) == 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*EX (Registers.SP),Registers.HL*/
		void op_0xe3()
		{
			Registers.TDl = CPUReadMemory(Registers.SP);
			Registers.TDh = CPUReadMemory((ushort)(Registers.SP + 1));
			EX_MPTR(ref Registers.TD, ref Registers.HL);
			CPUWriteMemory((Registers.SP), Registers.TDl);
			CPUWriteMemory((ushort)(Registers.SP + 1), Registers.TDh);

			CPUSetTState(19);
		}

		/*CALL PO,@*/
		void op_0xe4()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_P) == 0)
			{
				CALL(Registers.TD);

				CPUSetTState(17);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*PUSH Registers.HL*/
		void op_0xe5()
		{
			PUSH(Registers.HL);

			CPUSetTState(11);
		}

		/*AND #*/
		void op_0xe6()
		{
			m_tmpbyte = READ_OP();
			AND(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x20*/
		void op_0xe7()
		{
			RST(0x20);

			CPUSetTState(11);
		}

		/*RET PE*/
		void op_0xe8()
		{
			if ((Registers.F & Tables.FLAG_P) != 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*JP Registers.HL*/
		void op_0xe9()
		{
			JP_NO_MPTR(Registers.HL);

			CPUSetTState(4);
		}

		/*JP PE,@*/
		void op_0xea()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_P) != 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*EX Registers.DE, Registers.HL*/
		void op_0xeb()
		{
			EX(ref Registers.DE, ref Registers.HL);

			CPUSetTState(4);
		}

		/*CALL PE,@*/
		void op_0xec()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_P) != 0)
			{
				CALL(Registers.TD);
				CPUSetTState(17);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		void op_p_ED()
		{
			m_prefix = 0xED;
			m_noint_once = true;
		}

		/*XOR #*/
		void op_0xee()
		{
			m_tmpbyte = READ_OP();
			XOR(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x28*/
		void op_0xef()
		{
			RST(0x28);

			CPUSetTState(11);
		}

		/*RET P*/
		void op_0xf0()
		{
			if ((Registers.F & Tables.FLAG_S) == 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*POP Registers.AF.w*/
		void op_0xf1()
		{
			POP(ref Registers.AF);

			CPUSetTState(10);
		}

		/*JP P,@*/
		void op_0xf2()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_S) == 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				Registers.WZ = Registers.TD;

				CPUSetTState(10);
			}
		}

		/*DI*/
		void op_0xf3()
		{
			DI();

			CPUSetTState(4);
		}

		/*CALL P,@*/
		void op_0xf4()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_S) == 0)
			{
				CALL(Registers.TD);
				CPUSetTState(17);
			}
			else
			{
				CPUSetTState(10);
				Registers.WZ = Registers.TD;
			}
		}

		/*PUSH Registers.AF.w*/
		void op_0xf5()
		{
			PUSH(Registers.AF);

			CPUSetTState(11);
		}

		/*OR #*/
		void op_0xf6()
		{
			m_tmpbyte = READ_OP();
			OR(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x30*/
		void op_0xf7()
		{
			RST(0x30);

			CPUSetTState(11);
		}

		/*RET M*/
		void op_0xf8()
		{
			if ((Registers.F & Tables.FLAG_S) != 0)
			{
				RET();

				CPUSetTState(11);
			}
			else
			{
				CPUSetTState(5);
			}
		}

		/*LD Registers.SP, Registers.HL*/
		void op_0xf9()
		{
			LD16(ref Registers.SP, Registers.HL);

			CPUSetTState(6);
		}

		/*JP M,@*/
		void op_0xfa()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_S) != 0)
			{
				JP(Registers.TD);

				CPUSetTState(10);
			}
			else
			{
				CPUSetTState(10);
				Registers.WZ = Registers.TD;
			}
		}

		/*EI*/
		void op_0xfb()
		{
			EI();

			CPUSetTState(4);
		}

		/*CALL M,@*/
		void op_0xfc()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			if ((Registers.F & Tables.FLAG_S) != 0)
			{
				CALL(Registers.TD);
				CPUSetTState(17);
			}
			else
			{
				CPUSetTState(10);
				Registers.WZ = Registers.TD;
			}
		}

		void op_p_FD()
		{
			m_prefix = 0xFD;
			m_noint_once = true;
		}

		/*CP #*/
		void op_0xfe()
		{
			m_tmpbyte = READ_OP();
			CP(m_tmpbyte);

			CPUSetTState(7);
		}

		/*RST 0x38*/
		void op_0xff()
		{
			RST(0x38);

			CPUSetTState(11);
		}

	}
}
