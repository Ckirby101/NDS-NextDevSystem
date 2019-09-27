
namespace Z80EmuLib
{
	partial class Z80Emu
	{
		OperationDelegate[] m_opcodes_dd;

		void InitializeOpcodesDD()
		{
			m_opcodes_dd = new OperationDelegate[] {
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_DD_0x09, null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_DD_0x19, null      , null      ,
								null      , null      , null      , null      ,
								null      , op_DD_0x21, op_DD_0x22, op_DD_0x23,
								op_DD_0x24, op_DD_0x25, op_DD_0x26, null      ,
								null      , op_DD_0x29, op_DD_0x2a, op_DD_0x2b,
								op_DD_0x2c, op_DD_0x2d, op_DD_0x2e, null      ,
								null      , null      , null      , null      ,
								op_DD_0x34, op_DD_0x35, op_DD_0x36, null      ,
								null      , op_DD_0x39, null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								op_DD_0x44, op_DD_0x45, op_DD_0x46, null      ,
								null      , null      , null      , null      ,
								op_DD_0x4c, op_DD_0x4d, op_DD_0x4e, null      ,
								null      , null      , null      , null      ,
								op_DD_0x54, op_DD_0x55, op_DD_0x56, null      ,
								null      , null      , null      , null      ,
								op_DD_0x5c, op_DD_0x5d, op_DD_0x5e, null      ,
								op_DD_0x60, op_DD_0x61, op_DD_0x62, op_DD_0x63,
								op_DD_0x64, op_DD_0x65, op_DD_0x66, op_DD_0x67,
								op_DD_0x68, op_DD_0x69, op_DD_0x6a, op_DD_0x6b,
								op_DD_0x6c, op_DD_0x6d, op_DD_0x6e, op_DD_0x6f,
								op_DD_0x70, op_DD_0x71, op_DD_0x72, op_DD_0x73,
								op_DD_0x74, op_DD_0x75, null      , op_DD_0x77,
								null      , null      , null      , null      ,
								op_DD_0x7c, op_DD_0x7d, op_DD_0x7e, null      ,
								null      , null      , null      , null      ,
								op_DD_0x84, op_DD_0x85, op_DD_0x86, null      ,
								null      , null      , null      , null      ,
								op_DD_0x8c, op_DD_0x8d, op_DD_0x8e, null      ,
								null      , null      , null      , null      ,
								op_DD_0x94, op_DD_0x95, op_DD_0x96, null      ,
								null      , null      , null      , null      ,
								op_DD_0x9c, op_DD_0x9d, op_DD_0x9e, null      ,
								null      , null      , null      , null      ,
								op_DD_0xa4, op_DD_0xa5, op_DD_0xa6, null      ,
								null      , null      , null      , null      ,
								op_DD_0xac, op_DD_0xad, op_DD_0xae, null      ,
								null      , null      , null      , null      ,
								op_DD_0xb4, op_DD_0xb5, op_DD_0xb6, null      ,
								null      , null      , null      , null      ,
								op_DD_0xbc, op_DD_0xbd, op_DD_0xbe, null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_DD_0xe1, null      , op_DD_0xe3,
								null      , op_DD_0xe5, null      , null      ,
								null      , op_DD_0xe9, null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_DD_0xf9, null      , null      ,
								null      , null      , null      , null
						};
		}

		/*ADD Registers.IX,Registers.BC*/
		void op_DD_0x09()
		{
			ADD16(ref Registers.IX, Registers.BC);

			CPUSetTState(11);
		}

		/*ADD Registers.IX,Registers.DE*/
		void op_DD_0x19()
		{
			ADD16(ref Registers.IX, Registers.DE);

			CPUSetTState(11);
		}

		/*LD Registers.IX,@*/
		void op_DD_0x21()
		{
			Registers.TDl = READ_OP();
			Registers.TDh = READ_OP();
			LD16(ref Registers.IX, Registers.TD);

			CPUSetTState(10);
		}

		/*LD (@),Registers.IX*/
		void op_DD_0x22()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.IX, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}
		/*INC Registers.IX*/
		void op_DD_0x23()
		{
			INC16(ref Registers.IX);

			CPUSetTState(6);
		}
		/*INC Registers.XH*/
		void op_DD_0x24()
		{
			INC(ref Registers.XH);

			CPUSetTState(4);
		}
		/*DEC Registers.XH*/
		void op_DD_0x25()
		{
			DEC(ref Registers.XH);

			CPUSetTState(4);
		}
		/*LD Registers.XH,#*/
		void op_DD_0x26()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.XH, m_tmpbyte);

			CPUSetTState(7);
		}
		/*ADD Registers.IX,Registers.IX*/
		void op_DD_0x29()
		{
			ADD16(ref Registers.IX, Registers.IX);

			CPUSetTState(11);
		}
		/*LD Registers.IX,(@)*/
		void op_DD_0x2a()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.IX, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}
		/*DEC Registers.IX*/
		void op_DD_0x2b()
		{
			DEC16(ref Registers.IX);

			CPUSetTState(6);
		}

		/*INC Registers.XL*/
		void op_DD_0x2c()
		{
			INC(ref Registers.XL);

			CPUSetTState(4);
		}

		/*DEC Registers.XL*/
		void op_DD_0x2d()
		{
			DEC(ref Registers.XL);

			CPUSetTState(4);
		}

		/*LD Registers.XL,#*/
		void op_DD_0x2e()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.XL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*INC (Registers.IX+$)*/
		void op_DD_0x34()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			INC(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*DEC (Registers.IX+$)*/
		void op_DD_0x35()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			DEC(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*LD (Registers.IX+$),#*/
		void op_DD_0x36()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = READ_OP();
			LD(ref m_tmpbyte, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}
		/*ADD Registers.IX,Registers.SP*/
		void op_DD_0x39()
		{
			ADD16(ref Registers.IX, Registers.SP);

			CPUSetTState(11);
		}

		/*LD Registers.B,Registers.XH*/
		void op_DD_0x44()
		{
			LD(ref Registers.B, Registers.XH);

			CPUSetTState(4);
		}

		/*LD Registers.B,Registers.XL*/
		void op_DD_0x45()
		{
			LD(ref Registers.B, Registers.XL);

			CPUSetTState(4);
		}

		/*LD Registers.B,(Registers.IX+$)*/
		void op_DD_0x46()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.B, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.C,Registers.XH*/
		void op_DD_0x4c()
		{
			LD(ref Registers.C, Registers.XH);

			CPUSetTState(4);
		}
		/*LD Registers.C,Registers.XL*/
		void op_DD_0x4d()
		{
			LD(ref Registers.C, Registers.XL);

			CPUSetTState(4);
		}
		
		/*LD Registers.C,(Registers.IX+$)*/
		void op_DD_0x4e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.C, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.D,Registers.XH*/
		void op_DD_0x54()
		{
			LD(ref Registers.D, Registers.XH);

			CPUSetTState(4);
		}

		/*LD Registers.D,Registers.XL*/
		void op_DD_0x55()
		{
			LD(ref Registers.D, Registers.XL);

			CPUSetTState(4);
		}

		/*LD Registers.D,(Registers.IX+$)*/
		void op_DD_0x56()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.D, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.E,Registers.XH*/
		void op_DD_0x5c()
		{
			LD(ref Registers.E, Registers.XH);

			CPUSetTState(4);
		}

		/*LD Registers.E,Registers.XL*/
		void op_DD_0x5d()
		{
			LD(ref Registers.E, Registers.XL);

			CPUSetTState(4);
		}

		/*LD Registers.E,(Registers.IX+$)*/
		void op_DD_0x5e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.E, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.XH,Registers.B*/
		void op_DD_0x60()
		{
			LD(ref Registers.XH, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.XH,Registers.C*/
		void op_DD_0x61()
		{
			LD(ref Registers.XH, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.XH,Registers.D*/
		void op_DD_0x62()
		{
			LD(ref Registers.XH, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.XH,Registers.E*/
		void op_DD_0x63()
		{
			LD(ref Registers.XH, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.XH,Registers.XH*/
		void op_DD_0x64()
		{
			LD(ref Registers.XH, Registers.XH);

			CPUSetTState(4);
		}

		/*LD Registers.XH,Registers.XL*/
		void op_DD_0x65()
		{
			LD(ref Registers.XH, Registers.XL);

			CPUSetTState(4);
		}

		/*LD H,(Registers.IX+$)*/
		void op_DD_0x66()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.H, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.XH,Registers.A*/
		void op_DD_0x67()
		{
			LD(ref Registers.XH, Registers.A);

			CPUSetTState(4);
		}

		/*LD Registers.XL,Registers.B*/
		void op_DD_0x68()
		{
			LD(ref Registers.XL, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.XL,Registers.C*/
		void op_DD_0x69()
		{
			LD(ref Registers.XL, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.XL,Registers.D*/
		void op_DD_0x6a()
		{
			LD(ref Registers.XL, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.XL,Registers.E*/
		void op_DD_0x6b()
		{
			LD(ref Registers.XL, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.XL,Registers.XH*/
		void op_DD_0x6c()
		{
			LD(ref Registers.XL, Registers.XH);

			CPUSetTState(4);
		}

		/*LD Registers.XL,Registers.XL*/
		void op_DD_0x6d()
		{
			LD(ref Registers.XL, Registers.XL);

			CPUSetTState(4);
		}

		/*LD L,(Registers.IX+$)*/
		void op_DD_0x6e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.L, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.XL,Registers.A*/
		void op_DD_0x6f()
		{
			LD(ref Registers.XL, Registers.A);

			CPUSetTState(4);
		}

		/*LD (Registers.IX+$),Registers.B*/
		void op_DD_0x70()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.B);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IX+$),Registers.C*/
		void op_DD_0x71()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.C);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IX+$),Registers.D*/
		void op_DD_0x72()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.D);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IX+$),Registers.E*/
		void op_DD_0x73()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.E);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IX+$),H*/
		void op_DD_0x74()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.H);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IX+$),L*/
		void op_DD_0x75()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.L);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IX+$),Registers.A*/
		void op_DD_0x77()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.A);
			CPUWriteMemory((ushort)(Registers.IX + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.A,Registers.XH*/
		void op_DD_0x7c()
		{
			LD(ref Registers.A, Registers.XH);

			CPUSetTState(4);
		}

		/*LD Registers.A,Registers.XL*/
		void op_DD_0x7d()
		{
			LD(ref Registers.A, Registers.XL);

			CPUSetTState(4);
		}

		/*LD Registers.A,(Registers.IX+$)*/
		void op_DD_0x7e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			LD(ref Registers.A, m_tmpbyte);

			CPUSetTState(15);
		}

		/*ADD Registers.A,Registers.XH*/
		void op_DD_0x84()
		{
			ADD(Registers.XH);

			CPUSetTState(4);
		}

		/*ADD Registers.A,Registers.XL*/
		void op_DD_0x85()
		{
			ADD(Registers.XL);

			CPUSetTState(4);
		}

		/*ADD Registers.A,(Registers.IX+$)*/
		void op_DD_0x86()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			ADD(m_tmpbyte);

			CPUSetTState(15);
		}

		/*ADC Registers.A,Registers.XH*/
		void op_DD_0x8c()
		{
			ADC(Registers.XH);

			CPUSetTState(4);
		}

		/*ADC Registers.A,Registers.XL*/
		void op_DD_0x8d()
		{
			ADC(Registers.XL);

			CPUSetTState(4);
		}

		/*ADC Registers.A,(Registers.IX+$)*/
		void op_DD_0x8e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			ADC(m_tmpbyte);

			CPUSetTState(15);
		}

		/*SUB Registers.XH*/
		void op_DD_0x94()
		{
			SUB(Registers.XH);

			CPUSetTState(4);
		}
		/*SUB Registers.XL*/
		void op_DD_0x95()
		{
			SUB(Registers.XL);

			CPUSetTState(4);
		}

		/*SUB (Registers.IX+$)*/
		void op_DD_0x96()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SUB(m_tmpbyte);

			CPUSetTState(15);
		}

		/*SBC Registers.A,Registers.XH*/
		void op_DD_0x9c()
		{
			SBC(Registers.XH);

			CPUSetTState(4);
		}

		/*SBC Registers.A,Registers.XL*/
		void op_DD_0x9d()
		{
			SBC(Registers.XL);

			CPUSetTState(4);
		}

		/*SBC Registers.A,(Registers.IX+$)*/
		void op_DD_0x9e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			SBC(m_tmpbyte);

			CPUSetTState(15);
		}

		/*AND Registers.XH*/
		void op_DD_0xa4()
		{
			AND(Registers.XH);

			CPUSetTState(4);
		}

		/*AND Registers.XL*/
		void op_DD_0xa5()
		{
			AND(Registers.XL);

			CPUSetTState(4);
		}

		/*AND (Registers.IX+$)*/
		void op_DD_0xa6()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			AND(m_tmpbyte);

			CPUSetTState(15);
		}

		/*XOR Registers.XH*/
		void op_DD_0xac()
		{
			XOR(Registers.XH);

			CPUSetTState(4);
		}

		/*XOR Registers.XL*/
		void op_DD_0xad()
		{
			XOR(Registers.XL);

			CPUSetTState(4);
		}

		/*XOR (Registers.IX+$)*/
		void op_DD_0xae()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			XOR(m_tmpbyte);

			CPUSetTState(15);
		}

		/*OR Registers.XH*/
		void op_DD_0xb4()
		{
			OR(Registers.XH);

			CPUSetTState(4);
		}
		/*OR Registers.XL*/
		void op_DD_0xb5()
		{
			OR(Registers.XL);

			CPUSetTState(4);
		}

		/*OR (Registers.IX+$)*/
		void op_DD_0xb6()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			OR(m_tmpbyte);

			CPUSetTState(15);
		}

		/*CP Registers.XH*/
		void op_DD_0xbc()
		{
			CP(Registers.XH);

			CPUSetTState(4);
		}

		/*CP Registers.XL*/
		void op_DD_0xbd()
		{
			CP(Registers.XL);

			CPUSetTState(4);
		}

		/*CP (Registers.IX+$)*/
		void op_DD_0xbe()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IX + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IX + m_tmpbyte_s));
			CP(m_tmpbyte);

			CPUSetTState(15);
		}

		/*POP Registers.IX*/
		void op_DD_0xe1()
		{
			POP(ref Registers.IX);

			CPUSetTState(10);
		}

		/*EX (Registers.SP),Registers.IX*/
		void op_DD_0xe3()
		{
			Registers.TDl = CPUReadMemory(Registers.SP);
			Registers.TDh = CPUReadMemory((ushort)(Registers.SP + 1));
			EX_MPTR(ref Registers.TD, ref Registers.IX);
			CPUWriteMemory(Registers.SP, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.SP + 1), Registers.TDh);

			CPUSetTState(19);
		}

		/*PUSH Registers.IX*/
		void op_DD_0xe5()
		{
			PUSH(Registers.IX);

			CPUSetTState(11);
		}
		
		/*JP Registers.IX*/
		void op_DD_0xe9()
		{
			JP_NO_MPTR(Registers.IX);

			CPUSetTState(4);
		}

		/*LD Registers.SP,Registers.IX*/
		void op_DD_0xf9()
		{
			LD16(ref Registers.SP, Registers.IX);

			CPUSetTState(6);
		}

	}
}
