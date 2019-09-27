namespace Z80EmuLib
{
	partial class Z80Emu
	{
		OperationDelegate[] m_opcodes_cb;

		void InitializeOpcodesCB()
		{
			m_opcodes_cb = new OperationDelegate[] {
								op_CB_0x00, op_CB_0x01, op_CB_0x02, op_CB_0x03,
								op_CB_0x04, op_CB_0x05, op_CB_0x06, op_CB_0x07,
								op_CB_0x08, op_CB_0x09, op_CB_0x0a, op_CB_0x0b,
								op_CB_0x0c, op_CB_0x0d, op_CB_0x0e, op_CB_0x0f,
								op_CB_0x10, op_CB_0x11, op_CB_0x12, op_CB_0x13,
								op_CB_0x14, op_CB_0x15, op_CB_0x16, op_CB_0x17,
								op_CB_0x18, op_CB_0x19, op_CB_0x1a, op_CB_0x1b,
								op_CB_0x1c, op_CB_0x1d, op_CB_0x1e, op_CB_0x1f,
								op_CB_0x20, op_CB_0x21, op_CB_0x22, op_CB_0x23,
								op_CB_0x24, op_CB_0x25, op_CB_0x26, op_CB_0x27,
								op_CB_0x28, op_CB_0x29, op_CB_0x2a, op_CB_0x2b,
								op_CB_0x2c, op_CB_0x2d, op_CB_0x2e, op_CB_0x2f,
								op_CB_0x30, op_CB_0x31, op_CB_0x32, op_CB_0x33,
								op_CB_0x34, op_CB_0x35, op_CB_0x36, op_CB_0x37,
								op_CB_0x38, op_CB_0x39, op_CB_0x3a, op_CB_0x3b,
								op_CB_0x3c, op_CB_0x3d, op_CB_0x3e, op_CB_0x3f,
								op_CB_0x40, op_CB_0x41, op_CB_0x42, op_CB_0x43,
								op_CB_0x44, op_CB_0x45, op_CB_0x46, op_CB_0x47,
								op_CB_0x48, op_CB_0x49, op_CB_0x4a, op_CB_0x4b,
								op_CB_0x4c, op_CB_0x4d, op_CB_0x4e, op_CB_0x4f,
								op_CB_0x50, op_CB_0x51, op_CB_0x52, op_CB_0x53,
								op_CB_0x54, op_CB_0x55, op_CB_0x56, op_CB_0x57,
								op_CB_0x58, op_CB_0x59, op_CB_0x5a, op_CB_0x5b,
								op_CB_0x5c, op_CB_0x5d, op_CB_0x5e, op_CB_0x5f,
								op_CB_0x60, op_CB_0x61, op_CB_0x62, op_CB_0x63,
								op_CB_0x64, op_CB_0x65, op_CB_0x66, op_CB_0x67,
								op_CB_0x68, op_CB_0x69, op_CB_0x6a, op_CB_0x6b,
								op_CB_0x6c, op_CB_0x6d, op_CB_0x6e, op_CB_0x6f,
								op_CB_0x70, op_CB_0x71, op_CB_0x72, op_CB_0x73,
								op_CB_0x74, op_CB_0x75, op_CB_0x76, op_CB_0x77,
								op_CB_0x78, op_CB_0x79, op_CB_0x7a, op_CB_0x7b,
								op_CB_0x7c, op_CB_0x7d, op_CB_0x7e, op_CB_0x7f,
								op_CB_0x80, op_CB_0x81, op_CB_0x82, op_CB_0x83,
								op_CB_0x84, op_CB_0x85, op_CB_0x86, op_CB_0x87,
								op_CB_0x88, op_CB_0x89, op_CB_0x8a, op_CB_0x8b,
								op_CB_0x8c, op_CB_0x8d, op_CB_0x8e, op_CB_0x8f,
								op_CB_0x90, op_CB_0x91, op_CB_0x92, op_CB_0x93,
								op_CB_0x94, op_CB_0x95, op_CB_0x96, op_CB_0x97,
								op_CB_0x98, op_CB_0x99, op_CB_0x9a, op_CB_0x9b,
								op_CB_0x9c, op_CB_0x9d, op_CB_0x9e, op_CB_0x9f,
								op_CB_0xa0, op_CB_0xa1, op_CB_0xa2, op_CB_0xa3,
								op_CB_0xa4, op_CB_0xa5, op_CB_0xa6, op_CB_0xa7,
								op_CB_0xa8, op_CB_0xa9, op_CB_0xaa, op_CB_0xab,
								op_CB_0xac, op_CB_0xad, op_CB_0xae, op_CB_0xaf,
								op_CB_0xb0, op_CB_0xb1, op_CB_0xb2, op_CB_0xb3,
								op_CB_0xb4, op_CB_0xb5, op_CB_0xb6, op_CB_0xb7,
								op_CB_0xb8, op_CB_0xb9, op_CB_0xba, op_CB_0xbb,
								op_CB_0xbc, op_CB_0xbd, op_CB_0xbe, op_CB_0xbf,
								op_CB_0xc0, op_CB_0xc1, op_CB_0xc2, op_CB_0xc3,
								op_CB_0xc4, op_CB_0xc5, op_CB_0xc6, op_CB_0xc7,
								op_CB_0xc8, op_CB_0xc9, op_CB_0xca, op_CB_0xcb,
								op_CB_0xcc, op_CB_0xcd, op_CB_0xce, op_CB_0xcf,
								op_CB_0xd0, op_CB_0xd1, op_CB_0xd2, op_CB_0xd3,
								op_CB_0xd4, op_CB_0xd5, op_CB_0xd6, op_CB_0xd7,
								op_CB_0xd8, op_CB_0xd9, op_CB_0xda, op_CB_0xdb,
								op_CB_0xdc, op_CB_0xdd, op_CB_0xde, op_CB_0xdf,
								op_CB_0xe0, op_CB_0xe1, op_CB_0xe2, op_CB_0xe3,
								op_CB_0xe4, op_CB_0xe5, op_CB_0xe6, op_CB_0xe7,
								op_CB_0xe8, op_CB_0xe9, op_CB_0xea, op_CB_0xeb,
								op_CB_0xec, op_CB_0xed, op_CB_0xee, op_CB_0xef,
								op_CB_0xf0, op_CB_0xf1, op_CB_0xf2, op_CB_0xf3,
								op_CB_0xf4, op_CB_0xf5, op_CB_0xf6, op_CB_0xf7,
								op_CB_0xf8, op_CB_0xf9, op_CB_0xfa, op_CB_0xfb,
								op_CB_0xfc, op_CB_0xfd, op_CB_0xfe, op_CB_0xff
						};
		}

		/*RLC Registers.B*/
		void op_CB_0x00()
		{
			RLC(ref Registers.B);

			CPUSetTState(4);
		}

		/*RLC Registers.C*/
		void op_CB_0x01()
		{
			RLC(ref Registers.C);

			CPUSetTState(4);
		}
		
		/*RLC Registers.D*/
		void op_CB_0x02()
		{
			RLC(ref Registers.D);

			CPUSetTState(4);
		}

		/*RLC Registers.E*/
		void op_CB_0x03()
		{
			RLC(ref Registers.E);
			CPUSetTState(4);
		}

		/*RLC Registers.H*/
		void op_CB_0x04()
		{
			RLC(ref Registers.H);

			CPUSetTState(4);
		}

		/*RLC Registers.L*/
		void op_CB_0x05()
		{
			RLC(ref Registers.L);

			CPUSetTState(4);
		}

		/*RLC (Registers.HL)*/
		void op_CB_0x06()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RLC(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}
		
		/*RLC Registers.A*/
		void op_CB_0x07()
		{
			RLC(ref Registers.A);

			CPUSetTState(4);
		}

		/*RRC Registers.B*/
		void op_CB_0x08()
		{
			RRC(ref Registers.B);

			CPUSetTState(4);
		}

		/*RRC Registers.C*/
		void op_CB_0x09()
		{
			RRC(ref Registers.C);

			CPUSetTState(4);
		}

		/*RRC Registers.D*/
		void op_CB_0x0a()
		{
			RRC(ref Registers.D);

			CPUSetTState(4);
		}

		/*RRC Registers.E*/
		void op_CB_0x0b()
		{
			RRC(ref Registers.E);

			CPUSetTState(4);
		}

		/*RRC Registers.H*/
		void op_CB_0x0c()
		{
			RRC(ref Registers.H);

			CPUSetTState(4);
		}

		/*RRC Registers.L*/
		void op_CB_0x0d()
		{
			RRC(ref Registers.L);
			CPUSetTState(4);
		}

		/*RRC (Registers.HL)*/
		void op_CB_0x0e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RRC(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RRC Registers.A*/
		void op_CB_0x0f()
		{
			RRC(ref Registers.A);

			CPUSetTState(4);
		}

		/*RL Registers.B*/
		void op_CB_0x10()
		{
			RL(ref Registers.B);

			CPUSetTState(4);
		}

		/*RL Registers.C*/
		void op_CB_0x11()
		{
			RL(ref Registers.C);

			CPUSetTState(4);
		}

		/*RL Registers.D*/
		void op_CB_0x12()
		{
			RL(ref Registers.D);

			CPUSetTState(4);
		}

		/*RL Registers.E*/
		void op_CB_0x13()
		{
			RL(ref Registers.E);

			CPUSetTState(4);
		}

		/*RL Registers.H*/
		void op_CB_0x14()
		{
			RL(ref Registers.H);

			CPUSetTState(4);
		}

		/*RL Registers.L*/
		void op_CB_0x15()
		{
			RL(ref Registers.L);

			CPUSetTState(4);
		}

		/*RL (Registers.HL)*/
		void op_CB_0x16()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RL(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RL Registers.A*/
		void op_CB_0x17()
		{
			RL(ref Registers.A);

			CPUSetTState(4);
		}

		/*RR Registers.B*/
		void op_CB_0x18()
		{
			RR(ref Registers.B);

			CPUSetTState(4);
		}

		/*RR Registers.C*/
		void op_CB_0x19()
		{
			RR(ref Registers.C);

			CPUSetTState(4);
		}

		/*RR Registers.D*/
		void op_CB_0x1a()
		{
			RR(ref Registers.D);

			CPUSetTState(4);
		}

		/*RR Registers.E*/
		void op_CB_0x1b()
		{
			RR(ref Registers.E);

			CPUSetTState(4);
		}

		/*RR Registers.H*/
		void op_CB_0x1c()
		{
			RR(ref Registers.H);

			CPUSetTState(4);
		}

		/*RR Registers.L*/
		void op_CB_0x1d()
		{
			RR(ref Registers.L);

			CPUSetTState(4);
		}

		/*RR (Registers.HL)*/
		void op_CB_0x1e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RR(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RR Registers.A*/
		void op_CB_0x1f()
		{
			RR(ref Registers.A);

			CPUSetTState(4);
		}

		/*SLA Registers.B*/
		void op_CB_0x20()
		{
			SLA(ref Registers.B);

			CPUSetTState(4);
		}

		/*SLA Registers.C*/
		void op_CB_0x21()
		{
			SLA(ref Registers.C);

			CPUSetTState(4);
		}

		/*SLA Registers.D*/
		void op_CB_0x22()
		{
			SLA(ref Registers.D);

			CPUSetTState(4);
		}

		/*SLA Registers.E*/
		void op_CB_0x23()
		{
			SLA(ref Registers.E);

			CPUSetTState(4);
		}

		/*SLA Registers.H*/
		void op_CB_0x24()
		{
			SLA(ref Registers.H);

			CPUSetTState(4);
		}

		/*SLA Registers.L*/
		void op_CB_0x25()
		{
			SLA(ref Registers.L);

			CPUSetTState(4);
		}

		/*SLA (Registers.HL)*/
		void op_CB_0x26()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SLA(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*SLA Registers.A*/
		void op_CB_0x27()
		{
			SLA(ref Registers.A);

			CPUSetTState(4);
		}

		/*SRA Registers.B*/
		void op_CB_0x28()
		{
			SRA(ref Registers.B);

			CPUSetTState(4);
		}

		/*SRA Registers.C*/
		void op_CB_0x29()
		{
			SRA(ref Registers.C);

			CPUSetTState(4);
		}

		/*SRA Registers.D*/
		void op_CB_0x2a()
		{
			SRA(ref Registers.D);

			CPUSetTState(4);
		}

		/*SRA Registers.E*/
		void op_CB_0x2b()
		{
			SRA(ref Registers.E);

			CPUSetTState(4);
		}

		/*SRA Registers.H*/
		void op_CB_0x2c()
		{
			SRA(ref Registers.H);

			CPUSetTState(4);
		}

		/*SRA Registers.L*/
		void op_CB_0x2d()
		{
			SRA(ref Registers.L);

			CPUSetTState(4);
		}

		/*SRA (Registers.HL)*/
		void op_CB_0x2e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SRA(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*SRA Registers.A*/
		void op_CB_0x2f()
		{
			SRA(ref Registers.A);

			CPUSetTState(4);
		}

		/*SLL Registers.B*/
		void op_CB_0x30()
		{
			SLL(ref Registers.B);

			CPUSetTState(4);
		}

		/*SLL Registers.C*/
		void op_CB_0x31()
		{
			SLL(ref Registers.C);

			CPUSetTState(4);
		}

		/*SLL Registers.D*/
		void op_CB_0x32()
		{
			SLL(ref Registers.D);

			CPUSetTState(4);
		}

		/*SLL Registers.E*/
		void op_CB_0x33()
		{
			SLL(ref Registers.E);

			CPUSetTState(4);
		}

		/*SLL Registers.H*/
		void op_CB_0x34()
		{
			SLL(ref Registers.H);

			CPUSetTState(4);
		}

		/*SLL Registers.L*/
		void op_CB_0x35()
		{
			SLL(ref Registers.L);

			CPUSetTState(4);
		}

		/*SLL (Registers.HL)*/
		void op_CB_0x36()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SLL(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}
		
		/*SLL Registers.A*/
		void op_CB_0x37()
		{
			SLL(ref Registers.A);

			CPUSetTState(4);
		}
		
		/*SRL Registers.B*/
		void op_CB_0x38()
		{
			SRL(ref Registers.B);

			CPUSetTState(4);
		}
		
		/*SRL Registers.C*/
		void op_CB_0x39()
		{
			SRL(ref Registers.C);

			CPUSetTState(4);
		}
		
		/*SRL Registers.D*/
		void op_CB_0x3a()
		{
			SRL(ref Registers.D);

			CPUSetTState(4);
		}
		
		/*SRL Registers.E*/
		void op_CB_0x3b()
		{
			SRL(ref Registers.E);

			CPUSetTState(4);
		}
		
		/*SRL Registers.H*/
		void op_CB_0x3c()
		{
			SRL(ref Registers.H);

			CPUSetTState(4);
		}
		
		/*SRL Registers.L*/
		void op_CB_0x3d()
		{
			SRL(ref Registers.L);

			CPUSetTState(4);
		}

		/*SRL (Registers.HL)*/
		void op_CB_0x3e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SRL(ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*SRL Registers.A*/
		void op_CB_0x3f()
		{
			SRL(ref Registers.A);

			CPUSetTState(4);
		}

		/*BIT 0,Registers.B*/
		void op_CB_0x40()
		{
			BIT(0, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 0,Registers.C*/
		void op_CB_0x41()
		{
			BIT(0, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 0,Registers.D*/
		void op_CB_0x42()
		{
			BIT(0, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 0,Registers.E*/
		void op_CB_0x43()
		{
			BIT(0, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 0,Registers.H*/
		void op_CB_0x44()
		{
			BIT(0, Registers.H);

			CPUSetTState(4);
		}

		/*BIT 0,Registers.L*/
		void op_CB_0x45()
		{
			BIT(0, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 0,(Registers.HL)*/
		void op_CB_0x46()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(0, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 0,Registers.A*/
		void op_CB_0x47()
		{
			BIT(0, Registers.A);

			CPUSetTState(4);
		}

		/*BIT 1,Registers.B*/
		void op_CB_0x48()
		{
			BIT(1, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 1,Registers.C*/
		void op_CB_0x49()
		{
			BIT(1, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 1,Registers.D*/
		void op_CB_0x4a()
		{
			BIT(1, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 1,Registers.E*/
		void op_CB_0x4b()
		{
			BIT(1, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 1,Registers.H*/
		void op_CB_0x4c()
		{
			BIT(1, Registers.H);

			CPUSetTState(4);
		}

		/*BIT 1,Registers.L*/
		void op_CB_0x4d()
		{
			BIT(1, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 1,(Registers.HL)*/
		void op_CB_0x4e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(1, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 1,Registers.A*/
		void op_CB_0x4f()
		{
			BIT(1, Registers.A);

			CPUSetTState(4);
		}

		/*BIT 2,Registers.B*/
		void op_CB_0x50()
		{
			BIT(2, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 2,Registers.C*/
		void op_CB_0x51()
		{
			BIT(2, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 2,Registers.D*/
		void op_CB_0x52()
		{
			BIT(2, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 2,Registers.E*/
		void op_CB_0x53()
		{
			BIT(2, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 2,Registers.H*/
		void op_CB_0x54()
		{
			BIT(2, Registers.H);

			CPUSetTState(4);
		}

		/*BIT 2,Registers.L*/
		void op_CB_0x55()
		{
			BIT(2, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 2,(Registers.HL)*/
		void op_CB_0x56()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(2, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 2,Registers.A*/
		void op_CB_0x57()
		{
			BIT(2, Registers.A);

			CPUSetTState(4);
		}

		/*BIT 3,Registers.B*/
		void op_CB_0x58()
		{
			BIT(3, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 3,Registers.C*/
		void op_CB_0x59()
		{
			BIT(3, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 3,Registers.D*/
		void op_CB_0x5a()
		{
			BIT(3, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 3,Registers.E*/
		void op_CB_0x5b()
		{
			BIT(3, Registers.E);

			CPUSetTState(4);
		}
		
		/*BIT 3,Registers.H*/
		void op_CB_0x5c()
		{
			BIT(3, Registers.H);

			CPUSetTState(4);
		}
		
		/*BIT 3,Registers.L*/
		void op_CB_0x5d()
		{
			BIT(3, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 3,(Registers.HL)*/
		void op_CB_0x5e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(3, m_tmpbyte);

			CPUSetTState(8);
		}
		
		/*BIT 3,Registers.A*/
		void op_CB_0x5f()
		{
			BIT(3, Registers.A);

			CPUSetTState(4);
		}
		
		/*BIT 4,Registers.B*/
		void op_CB_0x60()
		{
			BIT(4, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 4,Registers.C*/
		void op_CB_0x61()
		{
			BIT(4, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 4,Registers.D*/
		void op_CB_0x62()
		{
			BIT(4, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 4,Registers.E*/
		void op_CB_0x63()
		{
			BIT(4, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 4,Registers.H*/
		void op_CB_0x64()
		{
			BIT(4, Registers.H);

			CPUSetTState(4);
		}

		/*BIT 4,Registers.L*/
		void op_CB_0x65()
		{
			BIT(4, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 4,(Registers.HL)*/
		void op_CB_0x66()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(4, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 4,Registers.A*/
		void op_CB_0x67()
		{
			BIT(4, Registers.A);

			CPUSetTState(4);
		}

		/*BIT 5,Registers.B*/
		void op_CB_0x68()
		{
			BIT(5, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 5,Registers.C*/
		void op_CB_0x69()
		{
			BIT(5, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 5,Registers.D*/
		void op_CB_0x6a()
		{
			BIT(5, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 5,Registers.E*/
		void op_CB_0x6b()
		{
			BIT(5, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 5,Registers.H*/
		void op_CB_0x6c()
		{
			BIT(5, Registers.H);

			CPUSetTState(4);
		}

		/*BIT 5,Registers.L*/
		void op_CB_0x6d()
		{
			BIT(5, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 5,(Registers.HL)*/
		void op_CB_0x6e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(5, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 5,Registers.A*/
		void op_CB_0x6f()
		{
			BIT(5, Registers.A);

			CPUSetTState(4);
		}

		/*BIT 6,Registers.B*/
		void op_CB_0x70()
		{
			BIT(6, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 6,Registers.C*/
		void op_CB_0x71()
		{
			BIT(6, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 6,Registers.D*/
		void op_CB_0x72()
		{
			BIT(6, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 6,Registers.E*/
		void op_CB_0x73()
		{
			BIT(6, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 6,Registers.H*/
		void op_CB_0x74()
		{
			BIT(6, Registers.H);

			CPUSetTState(4);
		}
		
		/*BIT 6,Registers.L*/
		void op_CB_0x75()
		{
			BIT(6, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 6,(Registers.HL)*/
		void op_CB_0x76()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(6, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 6,Registers.A*/
		void op_CB_0x77()
		{
			BIT(6, Registers.A);

			CPUSetTState(4);
		}

		/*BIT 7,Registers.B*/
		void op_CB_0x78()
		{
			BIT(7, Registers.B);

			CPUSetTState(4);
		}

		/*BIT 7,Registers.C*/
		void op_CB_0x79()
		{
			BIT(7, Registers.C);

			CPUSetTState(4);
		}

		/*BIT 7,Registers.D*/
		void op_CB_0x7a()
		{
			BIT(7, Registers.D);

			CPUSetTState(4);
		}

		/*BIT 7,Registers.E*/
		void op_CB_0x7b()
		{
			BIT(7, Registers.E);

			CPUSetTState(4);
		}

		/*BIT 7,Registers.H*/
		void op_CB_0x7c()
		{
			BIT(7, Registers.H);

			CPUSetTState(4);
		}

		/*BIT 7,Registers.L*/
		void op_CB_0x7d()
		{
			BIT(7, Registers.L);

			CPUSetTState(4);
		}

		/*BIT 7,(Registers.HL)*/
		void op_CB_0x7e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			//TODO:check Registers.WZ = Registers.HL;
			BIT_MPTR(7, m_tmpbyte);

			CPUSetTState(8);
		}

		/*BIT 7,Registers.A*/
		void op_CB_0x7f()
		{
			BIT(7, Registers.A);

			CPUSetTState(4);
		}

		/*RES 0,Registers.B*/
		void op_CB_0x80()
		{
			RES(0, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 0,Registers.C*/
		void op_CB_0x81()
		{
			RES(0, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 0,Registers.D*/
		void op_CB_0x82()
		{
			RES(0, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 0,Registers.E*/
		void op_CB_0x83()
		{
			RES(0, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 0,Registers.H*/
		void op_CB_0x84()
		{
			RES(0, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 0,Registers.L*/
		void op_CB_0x85()
		{
			RES(0, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 0,(Registers.HL)*/
		void op_CB_0x86()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(0, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RES 0,Registers.A*/
		void op_CB_0x87()
		{
			RES(0, ref Registers.A);

			CPUSetTState(4);
		}

		/*RES 1,Registers.B*/
		void op_CB_0x88()
		{
			RES(1, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 1,Registers.C*/
		void op_CB_0x89()
		{
			RES(1, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 1,Registers.D*/
		void op_CB_0x8a()
		{
			RES(1, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 1,Registers.E*/
		void op_CB_0x8b()
		{
			RES(1, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 1,Registers.H*/
		void op_CB_0x8c()
		{
			RES(1, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 1,Registers.L*/
		void op_CB_0x8d()
		{
			RES(1, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 1,(Registers.HL)*/
		void op_CB_0x8e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(1, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RES 1,Registers.A*/
		void op_CB_0x8f()
		{
			RES(1, ref Registers.A);
			CPUSetTState(4);
		}
		
		/*RES 2,Registers.B*/
		void op_CB_0x90()
		{
			RES(2, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 2,Registers.C*/
		void op_CB_0x91()
		{
			RES(2, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 2,Registers.D*/
		void op_CB_0x92()
		{
			RES(2, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 2,Registers.E*/
		void op_CB_0x93()
		{
			RES(2, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 2,Registers.H*/
		void op_CB_0x94()
		{
			RES(2, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 2,Registers.L*/
		void op_CB_0x95()
		{
			RES(2, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 2,(Registers.HL)*/
		void op_CB_0x96()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(2, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RES 2,Registers.A*/
		void op_CB_0x97()
		{
			RES(2, ref Registers.A);

			CPUSetTState(4);
		}

		/*RES 3,Registers.B*/
		void op_CB_0x98()
		{
			RES(3, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 3,Registers.C*/
		void op_CB_0x99()
		{
			RES(3, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 3,Registers.D*/
		void op_CB_0x9a()
		{
			RES(3, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 3,Registers.E*/
		void op_CB_0x9b()
		{
			RES(3, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 3,Registers.H*/
		void op_CB_0x9c()
		{
			RES(3, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 3,Registers.L*/
		void op_CB_0x9d()
		{
			RES(3, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 3,(Registers.HL)*/
		void op_CB_0x9e()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(3, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RES 3,Registers.A*/
		void op_CB_0x9f()
		{
			RES(3, ref Registers.A);

			CPUSetTState(4);
		}

		/*RES 4,Registers.B*/
		void op_CB_0xa0()
		{
			RES(4, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 4,Registers.C*/
		void op_CB_0xa1()
		{
			RES(4, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 4,Registers.D*/
		void op_CB_0xa2()
		{
			RES(4, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 4,Registers.E*/
		void op_CB_0xa3()
		{
			RES(4, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 4,Registers.H*/
		void op_CB_0xa4()
		{
			RES(4, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 4,Registers.L*/
		void op_CB_0xa5()
		{
			RES(4, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 4,(Registers.HL)*/
		void op_CB_0xa6()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(4, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RES 4,Registers.A*/
		void op_CB_0xa7()
		{
			RES(4, ref Registers.A);

			CPUSetTState(4);
		}

		/*RES 5,Registers.B*/
		void op_CB_0xa8()
		{
			RES(5, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 5,Registers.C*/
		void op_CB_0xa9()
		{
			RES(5, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 5,Registers.D*/
		void op_CB_0xaa()
		{
			RES(5, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 5,Registers.E*/
		void op_CB_0xab()
		{
			RES(5, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 5,Registers.H*/
		void op_CB_0xac()
		{
			RES(5, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 5,Registers.L*/
		void op_CB_0xad()
		{
			RES(5, ref Registers.L);
			CPUSetTState(4);
		}
		
		/*RES 5,(Registers.HL)*/
		void op_CB_0xae()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(5, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*RES 5,Registers.A*/
		void op_CB_0xaf()
		{
			RES(5, ref Registers.A);

			CPUSetTState(4);
		}

		/*RES 6,Registers.B*/
		void op_CB_0xb0()
		{
			RES(6, ref Registers.B);

			CPUSetTState(4);
		}

		/*RES 6,Registers.C*/
		void op_CB_0xb1()
		{
			RES(6, ref Registers.C);

			CPUSetTState(4);
		}

		/*RES 6,Registers.D*/
		void op_CB_0xb2()
		{
			RES(6, ref Registers.D);

			CPUSetTState(4);
		}

		/*RES 6,Registers.E*/
		void op_CB_0xb3()
		{
			RES(6, ref Registers.E);

			CPUSetTState(4);
		}

		/*RES 6,Registers.H*/
		void op_CB_0xb4()
		{
			RES(6, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 6,Registers.L*/
		void op_CB_0xb5()
		{
			RES(6, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 6,(Registers.HL)*/
		void op_CB_0xb6()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(6, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}
		/*RES 6,Registers.A*/
		void op_CB_0xb7()
		{
			RES(6, ref Registers.A);
			CPUSetTState(4);
		}
		/*RES 7,Registers.B*/
		void op_CB_0xb8()
		{
			RES(7, ref Registers.B);
			CPUSetTState(4);
		}
		/*RES 7,Registers.C*/
		void op_CB_0xb9()
		{
			RES(7, ref Registers.C);
			CPUSetTState(4);
		}
		/*RES 7,Registers.D*/
		void op_CB_0xba()
		{
			RES(7, ref Registers.D);
			CPUSetTState(4);
		}
		/*RES 7,Registers.E*/
		void op_CB_0xbb()
		{
			RES(7, ref Registers.E);
			CPUSetTState(4);
		}
		/*RES 7,Registers.H*/
		void op_CB_0xbc()
		{
			RES(7, ref Registers.H);

			CPUSetTState(4);
		}

		/*RES 7,Registers.L*/
		void op_CB_0xbd()
		{
			RES(7, ref Registers.L);

			CPUSetTState(4);
		}

		/*RES 7,(Registers.HL)*/
		void op_CB_0xbe()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			RES(7, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}
		/*RES 7,Registers.A*/
		void op_CB_0xbf()
		{
			RES(7, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 0,Registers.B*/
		void op_CB_0xc0()
		{
			SET(0, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 0,Registers.C*/
		void op_CB_0xc1()
		{
			SET(0, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 0,Registers.D*/
		void op_CB_0xc2()
		{
			SET(0, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 0,Registers.E*/
		void op_CB_0xc3()
		{
			SET(0, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 0,Registers.H*/
		void op_CB_0xc4()
		{
			SET(0, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 0,Registers.L*/
		void op_CB_0xc5()
		{
			SET(0, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 0,(Registers.HL)*/
		void op_CB_0xc6()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(0, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 0,Registers.A*/
		void op_CB_0xc7()
		{
			SET(0, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 1,Registers.B*/
		void op_CB_0xc8()
		{
			SET(1, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 1,Registers.C*/
		void op_CB_0xc9()
		{
			SET(1, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 1,Registers.D*/
		void op_CB_0xca()
		{
			SET(1, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 1,Registers.E*/
		void op_CB_0xcb()
		{
			SET(1, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 1,Registers.H*/
		void op_CB_0xcc()
		{
			SET(1, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 1,Registers.L*/
		void op_CB_0xcd()
		{
			SET(1, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 1,(Registers.HL)*/
		void op_CB_0xce()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(1, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 1,Registers.A*/
		void op_CB_0xcf()
		{
			SET(1, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 2,Registers.B*/
		void op_CB_0xd0()
		{
			SET(2, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 2,Registers.C*/
		void op_CB_0xd1()
		{
			SET(2, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 2,Registers.D*/
		void op_CB_0xd2()
		{
			SET(2, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 2,Registers.E*/
		void op_CB_0xd3()
		{
			SET(2, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 2,Registers.H*/
		void op_CB_0xd4()
		{
			SET(2, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 2,Registers.L*/
		void op_CB_0xd5()
		{
			SET(2, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 2,(Registers.HL)*/
		void op_CB_0xd6()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(2, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 2,Registers.A*/
		void op_CB_0xd7()
		{
			SET(2, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 3,Registers.B*/
		void op_CB_0xd8()
		{
			SET(3, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 3,Registers.C*/
		void op_CB_0xd9()
		{
			SET(3, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 3,Registers.D*/
		void op_CB_0xda()
		{
			SET(3, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 3,Registers.E*/
		void op_CB_0xdb()
		{
			SET(3, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 3,Registers.H*/
		void op_CB_0xdc()
		{
			SET(3, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 3,Registers.L*/
		void op_CB_0xdd()
		{
			SET(3, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 3,(Registers.HL)*/
		void op_CB_0xde()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(3, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 3,Registers.A*/
		void op_CB_0xdf()
		{
			SET(3, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 4,Registers.B*/
		void op_CB_0xe0()
		{
			SET(4, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 4,Registers.C*/
		void op_CB_0xe1()
		{
			SET(4, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 4,Registers.D*/
		void op_CB_0xe2()
		{
			SET(4, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 4,Registers.E*/
		void op_CB_0xe3()
		{
			SET(4, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 4,Registers.H*/
		void op_CB_0xe4()
		{
			SET(4, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 4,Registers.L*/
		void op_CB_0xe5()
		{
			SET(4, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 4,(Registers.HL)*/
		void op_CB_0xe6()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(4, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 4,Registers.A*/
		void op_CB_0xe7()
		{
			SET(4, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 5,Registers.B*/
		void op_CB_0xe8()
		{
			SET(5, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 5,Registers.C*/
		void op_CB_0xe9()
		{
			SET(5, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 5,Registers.D*/
		void op_CB_0xea()
		{
			SET(5, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 5,Registers.E*/
		void op_CB_0xeb()
		{
			SET(5, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 5,Registers.H*/
		void op_CB_0xec()
		{
			SET(5, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 5,Registers.L*/
		void op_CB_0xed()
		{
			SET(5, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 5,(Registers.HL)*/
		void op_CB_0xee()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(5, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 5,Registers.A*/
		void op_CB_0xef()
		{
			SET(5, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 6,Registers.B*/
		void op_CB_0xf0()
		{
			SET(6, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 6,Registers.C*/
		void op_CB_0xf1()
		{
			SET(6, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 6,Registers.D*/
		void op_CB_0xf2()
		{
			SET(6, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 6,Registers.E*/
		void op_CB_0xf3()
		{
			SET(6, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 6,Registers.H*/
		void op_CB_0xf4()
		{
			SET(6, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 6,Registers.L*/
		void op_CB_0xf5()
		{
			SET(6, ref Registers.L);
			CPUSetTState(4);
		}
		/*SET 6,(Registers.HL)*/
		void op_CB_0xf6()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(6, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);
			CPUSetTState(11);
		}
		/*SET 6,Registers.A*/
		void op_CB_0xf7()
		{
			SET(6, ref Registers.A);
			CPUSetTState(4);
		}
		/*SET 7,Registers.B*/
		void op_CB_0xf8()
		{
			SET(7, ref Registers.B);
			CPUSetTState(4);
		}
		/*SET 7,Registers.C*/
		void op_CB_0xf9()
		{
			SET(7, ref Registers.C);
			CPUSetTState(4);
		}
		/*SET 7,Registers.D*/
		void op_CB_0xfa()
		{
			SET(7, ref Registers.D);
			CPUSetTState(4);
		}
		/*SET 7,Registers.E*/
		void op_CB_0xfb()
		{
			SET(7, ref Registers.E);
			CPUSetTState(4);
		}
		/*SET 7,Registers.H*/
		void op_CB_0xfc()
		{
			SET(7, ref Registers.H);
			CPUSetTState(4);
		}
		/*SET 7,Registers.L*/
		void op_CB_0xfd()
		{
			SET(7, ref Registers.L);
			CPUSetTState(4);
		}

		/*SET 7,(Registers.HL)*/
		void op_CB_0xfe()
		{
			m_tmpbyte = CPUReadMemory((Registers.HL));
			SET(7, ref m_tmpbyte);
			CPUWriteMemory((Registers.HL), m_tmpbyte);

			CPUSetTState(11);
		}

		/*SET 7,Registers.A*/
		void op_CB_0xff()
		{
			SET(7, ref Registers.A);

			CPUSetTState(4);
		}

	}
}
