namespace Z80EmuLib
{
	partial class Z80Emu
	{
		OperationDelegate[] m_opcodes_ddcb;

		void InitializeOpcodesDDCB()
		{
			m_opcodes_ddcb = new OperationDelegate[] {
								op_DDCB_0x00, op_DDCB_0x01, op_DDCB_0x02, op_DDCB_0x03,
								op_DDCB_0x04, op_DDCB_0x05, op_DDCB_0x06, op_DDCB_0x07,
								op_DDCB_0x08, op_DDCB_0x09, op_DDCB_0x0a, op_DDCB_0x0b,
								op_DDCB_0x0c, op_DDCB_0x0d, op_DDCB_0x0e, op_DDCB_0x0f,
								op_DDCB_0x10, op_DDCB_0x11, op_DDCB_0x12, op_DDCB_0x13,
								op_DDCB_0x14, op_DDCB_0x15, op_DDCB_0x16, op_DDCB_0x17,
								op_DDCB_0x18, op_DDCB_0x19, op_DDCB_0x1a, op_DDCB_0x1b,
								op_DDCB_0x1c, op_DDCB_0x1d, op_DDCB_0x1e, op_DDCB_0x1f,
								op_DDCB_0x20, op_DDCB_0x21, op_DDCB_0x22, op_DDCB_0x23,
								op_DDCB_0x24, op_DDCB_0x25, op_DDCB_0x26, op_DDCB_0x27,
								op_DDCB_0x28, op_DDCB_0x29, op_DDCB_0x2a, op_DDCB_0x2b,
								op_DDCB_0x2c, op_DDCB_0x2d, op_DDCB_0x2e, op_DDCB_0x2f,
								op_DDCB_0x30, op_DDCB_0x31, op_DDCB_0x32, op_DDCB_0x33,
								op_DDCB_0x34, op_DDCB_0x35, op_DDCB_0x36, op_DDCB_0x37,
								op_DDCB_0x38, op_DDCB_0x39, op_DDCB_0x3a, op_DDCB_0x3b,
								op_DDCB_0x3c, op_DDCB_0x3d, op_DDCB_0x3e, op_DDCB_0x3f,
								op_DDCB_0x47, op_DDCB_0x47, op_DDCB_0x47, op_DDCB_0x47,
								op_DDCB_0x47, op_DDCB_0x47, op_DDCB_0x47, op_DDCB_0x47,
								op_DDCB_0x4f, op_DDCB_0x4f, op_DDCB_0x4f, op_DDCB_0x4f,
								op_DDCB_0x4f, op_DDCB_0x4f, op_DDCB_0x4f, op_DDCB_0x4f,
								op_DDCB_0x57, op_DDCB_0x57, op_DDCB_0x57, op_DDCB_0x57,
								op_DDCB_0x57, op_DDCB_0x57, op_DDCB_0x57, op_DDCB_0x57,
								op_DDCB_0x5f, op_DDCB_0x5f, op_DDCB_0x5f, op_DDCB_0x5f,
								op_DDCB_0x5f, op_DDCB_0x5f, op_DDCB_0x5f, op_DDCB_0x5f,
								op_DDCB_0x67, op_DDCB_0x67, op_DDCB_0x67, op_DDCB_0x67,
								op_DDCB_0x67, op_DDCB_0x67, op_DDCB_0x67, op_DDCB_0x67,
								op_DDCB_0x6f, op_DDCB_0x6f, op_DDCB_0x6f, op_DDCB_0x6f,
								op_DDCB_0x6f, op_DDCB_0x6f, op_DDCB_0x6f, op_DDCB_0x6f,
								op_DDCB_0x77, op_DDCB_0x77, op_DDCB_0x77, op_DDCB_0x77,
								op_DDCB_0x77, op_DDCB_0x77, op_DDCB_0x77, op_DDCB_0x77,
								op_DDCB_0x7f, op_DDCB_0x7f, op_DDCB_0x7f, op_DDCB_0x7f,
								op_DDCB_0x7f, op_DDCB_0x7f, op_DDCB_0x7f, op_DDCB_0x7f,
								op_DDCB_0x80, op_DDCB_0x81, op_DDCB_0x82, op_DDCB_0x83,
								op_DDCB_0x84, op_DDCB_0x85, op_DDCB_0x86, op_DDCB_0x87,
								op_DDCB_0x88, op_DDCB_0x89, op_DDCB_0x8a, op_DDCB_0x8b,
								op_DDCB_0x8c, op_DDCB_0x8d, op_DDCB_0x8e, op_DDCB_0x8f,
								op_DDCB_0x90, op_DDCB_0x91, op_DDCB_0x92, op_DDCB_0x93,
								op_DDCB_0x94, op_DDCB_0x95, op_DDCB_0x96, op_DDCB_0x97,
								op_DDCB_0x98, op_DDCB_0x99, op_DDCB_0x9a, op_DDCB_0x9b,
								op_DDCB_0x9c, op_DDCB_0x9d, op_DDCB_0x9e, op_DDCB_0x9f,
								op_DDCB_0xa0, op_DDCB_0xa1, op_DDCB_0xa2, op_DDCB_0xa3,
								op_DDCB_0xa4, op_DDCB_0xa5, op_DDCB_0xa6, op_DDCB_0xa7,
								op_DDCB_0xa8, op_DDCB_0xa9, op_DDCB_0xaa, op_DDCB_0xab,
								op_DDCB_0xac, op_DDCB_0xad, op_DDCB_0xae, op_DDCB_0xaf,
								op_DDCB_0xb0, op_DDCB_0xb1, op_DDCB_0xb2, op_DDCB_0xb3,
								op_DDCB_0xb4, op_DDCB_0xb5, op_DDCB_0xb6, op_DDCB_0xb7,
								op_DDCB_0xb8, op_DDCB_0xb9, op_DDCB_0xba, op_DDCB_0xbb,
								op_DDCB_0xbc, op_DDCB_0xbd, op_DDCB_0xbe, op_DDCB_0xbf,
								op_DDCB_0xc0, op_DDCB_0xc1, op_DDCB_0xc2, op_DDCB_0xc3,
								op_DDCB_0xc4, op_DDCB_0xc5, op_DDCB_0xc6, op_DDCB_0xc7,
								op_DDCB_0xc8, op_DDCB_0xc9, op_DDCB_0xca, op_DDCB_0xcb,
								op_DDCB_0xcc, op_DDCB_0xcd, op_DDCB_0xce, op_DDCB_0xcf,
								op_DDCB_0xd0, op_DDCB_0xd1, op_DDCB_0xd2, op_DDCB_0xd3,
								op_DDCB_0xd4, op_DDCB_0xd5, op_DDCB_0xd6, op_DDCB_0xd7,
								op_DDCB_0xd8, op_DDCB_0xd9, op_DDCB_0xda, op_DDCB_0xdb,
								op_DDCB_0xdc, op_DDCB_0xdd, op_DDCB_0xde, op_DDCB_0xdf,
								op_DDCB_0xe0, op_DDCB_0xe1, op_DDCB_0xe2, op_DDCB_0xe3,
								op_DDCB_0xe4, op_DDCB_0xe5, op_DDCB_0xe6, op_DDCB_0xe7,
								op_DDCB_0xe8, op_DDCB_0xe9, op_DDCB_0xea, op_DDCB_0xeb,
								op_DDCB_0xec, op_DDCB_0xed, op_DDCB_0xee, op_DDCB_0xef,
								op_DDCB_0xf0, op_DDCB_0xf1, op_DDCB_0xf2, op_DDCB_0xf3,
								op_DDCB_0xf4, op_DDCB_0xf5, op_DDCB_0xf6, op_DDCB_0xf7,
								op_DDCB_0xf8, op_DDCB_0xf9, op_DDCB_0xfa, op_DDCB_0xfb,
								op_DDCB_0xfc, op_DDCB_0xfd, op_DDCB_0xfe, op_DDCB_0xff
						};
		}

		/*LD Registers.B,RLC (Registers.IX+$)*/
		void op_DDCB_0x00()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*LD Registers.C,RLC (Registers.IX+$)*/
		void op_DDCB_0x01()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*LD Registers.D,RLC (Registers.IX+$)*/
		void op_DDCB_0x02()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*LD Registers.E,RLC (Registers.IX+$)*/
		void op_DDCB_0x03()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RLC (Registers.IX+$)*/
		void op_DDCB_0x04()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RLC (Registers.IX+$)*/
		void op_DDCB_0x05()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RLC (Registers.IX+$)*/
		void op_DDCB_0x06()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RLC (Registers.IX+$)*/
		void op_DDCB_0x07()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RLC(ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RRC (Registers.IX+$)*/
		void op_DDCB_0x08()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RRC (Registers.IX+$)*/
		void op_DDCB_0x09()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RRC (Registers.IX+$)*/
		void op_DDCB_0x0a()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RRC (Registers.IX+$)*/
		void op_DDCB_0x0b()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RRC (Registers.IX+$)*/
		void op_DDCB_0x0c()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RRC (Registers.IX+$)*/
		void op_DDCB_0x0d()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RRC (Registers.IX+$)*/
		void op_DDCB_0x0e()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RRC (Registers.IX+$)*/
		void op_DDCB_0x0f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RRC(ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RL (Registers.IX+$)*/
		void op_DDCB_0x10()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RL (Registers.IX+$)*/
		void op_DDCB_0x11()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RL (Registers.IX+$)*/
		void op_DDCB_0x12()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RL (Registers.IX+$)*/
		void op_DDCB_0x13()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RL (Registers.IX+$)*/
		void op_DDCB_0x14()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RL (Registers.IX+$)*/
		void op_DDCB_0x15()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RL (Registers.IX+$)*/
		void op_DDCB_0x16()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RL (Registers.IX+$)*/
		void op_DDCB_0x17()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RL(ref m_tmpbyte);
			LD16(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RR (Registers.IX+$)*/
		void op_DDCB_0x18()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RR (Registers.IX+$)*/
		void op_DDCB_0x19()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RR (Registers.IX+$)*/
		void op_DDCB_0x1a()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RR (Registers.IX+$)*/
		void op_DDCB_0x1b()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RR (Registers.IX+$)*/
		void op_DDCB_0x1c()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RR (Registers.IX+$)*/
		void op_DDCB_0x1d()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RR (Registers.IX+$)*/
		void op_DDCB_0x1e()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RR (Registers.IX+$)*/
		void op_DDCB_0x1f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RR(ref m_tmpbyte);
			LD16(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SLA (Registers.IX+$)*/
		void op_DDCB_0x20()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SLA (Registers.IX+$)*/
		void op_DDCB_0x21()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SLA (Registers.IX+$)*/
		void op_DDCB_0x22()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SLA (Registers.IX+$)*/
		void op_DDCB_0x23()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SLA (Registers.IX+$)*/
		void op_DDCB_0x24()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SLA (Registers.IX+$)*/
		void op_DDCB_0x25()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SLA (Registers.IX+$)*/
		void op_DDCB_0x26()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SLA (Registers.IX+$)*/
		void op_DDCB_0x27()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLA(ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SRA (Registers.IX+$)*/
		void op_DDCB_0x28()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SRA (Registers.IX+$)*/
		void op_DDCB_0x29()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SRA (Registers.IX+$)*/
		void op_DDCB_0x2a()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SRA (Registers.IX+$)*/
		void op_DDCB_0x2b()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SRA (Registers.IX+$)*/
		void op_DDCB_0x2c()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SRA (Registers.IX+$)*/
		void op_DDCB_0x2d()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SRA (Registers.IX+$)*/
		void op_DDCB_0x2e()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SRA (Registers.IX+$)*/
		void op_DDCB_0x2f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRA(ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SLL (Registers.IX+$)*/
		void op_DDCB_0x30()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SLL (Registers.IX+$)*/
		void op_DDCB_0x31()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SLL (Registers.IX+$)*/
		void op_DDCB_0x32()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SLL (Registers.IX+$)*/
		void op_DDCB_0x33()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SLL (Registers.IX+$)*/
		void op_DDCB_0x34()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SLL (Registers.IX+$)*/
		void op_DDCB_0x35()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SLL (Registers.IX+$)*/
		void op_DDCB_0x36()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SLL (Registers.IX+$)*/
		void op_DDCB_0x37()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SLL(ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SRL (Registers.IX+$)*/
		void op_DDCB_0x38()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SRL (Registers.IX+$)*/
		void op_DDCB_0x39()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SRL (Registers.IX+$)*/
		void op_DDCB_0x3a()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SRL (Registers.IX+$)*/
		void op_DDCB_0x3b()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SRL (Registers.IX+$)*/
		void op_DDCB_0x3c()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SRL (Registers.IX+$)*/
		void op_DDCB_0x3d()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SRL (Registers.IX+$)*/
		void op_DDCB_0x3e()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SRL (Registers.IX+$)*/
		void op_DDCB_0x3f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SRL(ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*BIT 0,(Registers.IX+$)*/
		void op_DDCB_0x47()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(0, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 1,(Registers.IX+$)*/
		void op_DDCB_0x4f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(1, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 2,(Registers.IX+$)*/
		void op_DDCB_0x57()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(2, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 3,(Registers.IX+$)*/
		void op_DDCB_0x5f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(3, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 4,(Registers.IX+$)*/
		void op_DDCB_0x67()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(4, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 5,(Registers.IX+$)*/
		void op_DDCB_0x6f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(5, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 6,(Registers.IX+$)*/
		void op_DDCB_0x77()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(6, m_tmpbyte);
			CPUSetTState(16);
		}

		/*BIT 7,(Registers.IX+$)*/
		void op_DDCB_0x7f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			BIT_MPTR(7, m_tmpbyte);
			CPUSetTState(16);
		}

		/*LD Registers.B,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x80()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x81()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x82()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x83()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x84()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x85()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 0,(Registers.IX+$)*/
		void op_DDCB_0x86()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 0,(Registers.IX+$)*/
		void op_DDCB_0x87()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(0, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x88()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x89()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x8a()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x8b()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x8c()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x8d()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 1,(Registers.IX+$)*/
		void op_DDCB_0x8e()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 1,(Registers.IX+$)*/
		void op_DDCB_0x8f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(1, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x90()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x91()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x92()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x93()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x94()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x95()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 2,(Registers.IX+$)*/
		void op_DDCB_0x96()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 2,(Registers.IX+$)*/
		void op_DDCB_0x97()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(2, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x98()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x99()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x9a()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x9b()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x9c()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x9d()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 3,(Registers.IX+$)*/
		void op_DDCB_0x9e()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 3,(Registers.IX+$)*/
		void op_DDCB_0x9f()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(3, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa0()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa1()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa2()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa3()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa4()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa5()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa6()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 4,(Registers.IX+$)*/
		void op_DDCB_0xa7()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(4, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xa8()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xa9()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xaa()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xab()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xac()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xad()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 5,(Registers.IX+$)*/
		void op_DDCB_0xae()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 5,(Registers.IX+$)*/
		void op_DDCB_0xaf()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(5, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb0()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb1()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb2()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb3()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb4()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb5()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb6()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 6,(Registers.IX+$)*/
		void op_DDCB_0xb7()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(6, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xb8()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xb9()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xba()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xbb()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xbc()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xbd()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*RES 7,(Registers.IX+$)*/
		void op_DDCB_0xbe()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,RES 7,(Registers.IX+$)*/
		void op_DDCB_0xbf()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			RES(7, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc0()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc1()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc2()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc3()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc4()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc5()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc6()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 0,(Registers.IX+$)*/
		void op_DDCB_0xc7()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(0, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xc8()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xc9()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xca()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xcb()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xcc()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xcd()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 1,(Registers.IX+$)*/
		void op_DDCB_0xce()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 1,(Registers.IX+$)*/
		void op_DDCB_0xcf()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(1, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd0()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd1()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd2()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd3()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd4()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd5()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd6()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 2,(Registers.IX+$)*/
		void op_DDCB_0xd7()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(2, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xd8()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xd9()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xda()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xdb()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xdc()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xdd()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 3,(Registers.IX+$)*/
		void op_DDCB_0xde()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 3,(Registers.IX+$)*/
		void op_DDCB_0xdf()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(3, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe0()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe1()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe2()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe3()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe4()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe5()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe6()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 4,(Registers.IX+$)*/
		void op_DDCB_0xe7()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(4, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xe8()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xe9()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xea()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xeb()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xec()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xed()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 5,(Registers.IX+$)*/
		void op_DDCB_0xee()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 5,(Registers.IX+$)*/
		void op_DDCB_0xef()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(5, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf0()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf1()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf2()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf3()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf4()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf5()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf6()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 6,(Registers.IX+$)*/
		void op_DDCB_0xf7()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(6, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.B,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xf8()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.B, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.C,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xf9()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.C, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.D,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xfa()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.D, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.E,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xfb()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.E, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.H,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xfc()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.H, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.L,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xfd()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.L, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*SET 7,(Registers.IX+$)*/
		void op_DDCB_0xfe()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}

		/*LD Registers.A,SET 7,(Registers.IX+$)*/
		void op_DDCB_0xff()
		{
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SET(7, ref m_tmpbyte);
			LD(ref Registers.A, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);
			CPUSetTState(19);
		}
	}
}
