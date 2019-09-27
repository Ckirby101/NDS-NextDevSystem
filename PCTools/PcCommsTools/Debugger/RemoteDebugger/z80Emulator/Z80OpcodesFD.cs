namespace Z80EmuLib
{
	partial class Z80Emu
	{
		OperationDelegate[] m_opcodes_fd;

		void InitializeOpcodesFD()
		{
			m_opcodes_fd = new OperationDelegate[] {
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_FD_0x09, null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_FD_0x19, null      , null      ,
								null      , null      , null      , null      ,
								null      , op_FD_0x21, op_FD_0x22, op_FD_0x23,
								op_FD_0x24, op_FD_0x25, op_FD_0x26, null      ,
								null      , op_FD_0x29, op_FD_0x2a, op_FD_0x2b,
								op_FD_0x2c, op_FD_0x2d, op_FD_0x2e, null      ,
								null      , null      , null      , null      ,
								op_FD_0x34, op_FD_0x35, op_FD_0x36, null      ,
								null      , op_FD_0x39, null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								op_FD_0x44, op_FD_0x45, op_FD_0x46, null      ,
								null      , null      , null      , null      ,
								op_FD_0x4c, op_FD_0x4d, op_FD_0x4e, null      ,
								null      , null      , null      , null      ,
								op_FD_0x54, op_FD_0x55, op_FD_0x56, null      ,
								null      , null      , null      , null      ,
								op_FD_0x5c, op_FD_0x5d, op_FD_0x5e, null      ,
								op_FD_0x60, op_FD_0x61, op_FD_0x62, op_FD_0x63,
								op_FD_0x64, op_FD_0x65, op_FD_0x66, op_FD_0x67,
								op_FD_0x68, op_FD_0x69, op_FD_0x6a, op_FD_0x6b,
								op_FD_0x6c, op_FD_0x6d, op_FD_0x6e, op_FD_0x6f,
								op_FD_0x70, op_FD_0x71, op_FD_0x72, op_FD_0x73,
								op_FD_0x74, op_FD_0x75, null      , op_FD_0x77,
								null      , null      , null      , null      ,
								op_FD_0x7c, op_FD_0x7d, op_FD_0x7e, null      ,
								null      , null      , null      , null      ,
								op_FD_0x84, op_FD_0x85, op_FD_0x86, null      ,
								null      , null      , null      , null      ,
								op_FD_0x8c, op_FD_0x8d, op_FD_0x8e, null      ,
								null      , null      , null      , null      ,
								op_FD_0x94, op_FD_0x95, op_FD_0x96, null      ,
								null      , null      , null      , null      ,
								op_FD_0x9c, op_FD_0x9d, op_FD_0x9e, null      ,
								null      , null      , null      , null      ,
								op_FD_0xa4, op_FD_0xa5, op_FD_0xa6, null      ,
								null      , null      , null      , null      ,
								op_FD_0xac, op_FD_0xad, op_FD_0xae, null      ,
								null      , null      , null      , null      ,
								op_FD_0xb4, op_FD_0xb5, op_FD_0xb6, null      ,
								null      , null      , null      , null      ,
								op_FD_0xbc, op_FD_0xbd, op_FD_0xbe, null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_FD_0xe1, null      , op_FD_0xe3,
								null      , op_FD_0xe5, null      , null      ,
								null      , op_FD_0xe9, null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , op_FD_0xf9, null      , null      ,
								null      , null      , null      , null
						};
		}

		/*ADD Registers.IY,Registers.BC*/
		void op_FD_0x09()
		{
			ADD16(ref  Registers.IY, Registers.BC);

			CPUSetTState(11);
		}

		/*ADD Registers.IY,Registers.DE*/
		void op_FD_0x19()
		{
			ADD16(ref Registers.IY, Registers.DE);

			CPUSetTState(11);
		}

		/*LD Registers.IY,@*/
		void op_FD_0x21()
		{
			ushort tmp  = READ_OP();
			tmp += (ushort)(READ_OP() << 8);
			LD16(ref Registers.IY, tmp);

			CPUSetTState(10);
		}

		/*LD (@),Registers.IY*/
		void op_FD_0x22()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.IY, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}

		/*INC Registers.IY*/
		void op_FD_0x23()
		{
			INC16(ref Registers.IY);

			CPUSetTState(6);
		}

		/*INC Registers.YH*/
		void op_FD_0x24()
		{
			INC(ref Registers.YH);

			CPUSetTState(4);
		}

		/*DEC Registers.YH*/
		void op_FD_0x25()
		{
			DEC(ref Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.YH,#*/
		void op_FD_0x26()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.YH, m_tmpbyte);

			CPUSetTState(7);
		}

		/*ADD Registers.IY,Registers.IY*/
		void op_FD_0x29()
		{
			ADD16(ref Registers.IY, Registers.IY);

			CPUSetTState(11);
		}

		/*LD Registers.IY,(@)*/
		void op_FD_0x2a()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.IY, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}

		/*DEC Registers.IY*/
		void op_FD_0x2b()
		{
			DEC16(ref Registers.IY);

			CPUSetTState(6);
		}

		/*INC Registers.YL*/
		void op_FD_0x2c()
		{
			INC(ref Registers.YL);

			CPUSetTState(4);
		}

		/*DEC Registers.YL*/
		void op_FD_0x2d()
		{
			DEC(ref Registers.YL);

			CPUSetTState(4);
		}

		/*LD Registers.YL,#*/
		void op_FD_0x2e()
		{
			m_tmpbyte = READ_OP();
			LD(ref Registers.YL, m_tmpbyte);

			CPUSetTState(7);
		}

		/*INC (Registers.IY+$)*/
		void op_FD_0x34()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			INC(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*DEC (Registers.IY+$)*/
		void op_FD_0x35()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			DEC(ref m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(19);
		}

		/*LD (Registers.IY+$),#*/
		void op_FD_0x36()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = READ_OP();
			LD(ref m_tmpbyte, m_tmpbyte);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*ADD Registers.IY,Registers.SP*/
		void op_FD_0x39()
		{
			ADD16(ref Registers.IY, Registers.SP);

			CPUSetTState(11);
		}

		/*LD Registers.B,Registers.YH*/
		void op_FD_0x44()
		{
			LD(ref Registers.B, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.B,Registers.YL*/
		void op_FD_0x45()
		{
			LD(ref Registers.B, Registers.YL);

			CPUSetTState(4);
		}

		/*LD Registers.B,(Registers.IY+$)*/
		void op_FD_0x46()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			LD(ref Registers.B, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.C,Registers.YH*/
		void op_FD_0x4c()
		{
			LD(ref Registers.C, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.C,Registers.YL*/
		void op_FD_0x4d()
		{
			LD(ref Registers.C, Registers.YL);

			CPUSetTState(4);
		}

		/*LD Registers.C,(Registers.IY+$)*/
		void op_FD_0x4e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			LD(ref Registers.C, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.D,Registers.YH*/
		void op_FD_0x54()
		{
			LD(ref Registers.D, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.D,Registers.YL*/
		void op_FD_0x55()
		{
			LD(ref Registers.D, Registers.YL);

			CPUSetTState(4);
		}

		/*LD Registers.D,(Registers.IY+$)*/
		void op_FD_0x56()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			LD(ref Registers.D, m_tmpbyte);
			CPUSetTState(15);
		}

		/*LD Registers.E,Registers.YH*/
		void op_FD_0x5c()
		{
			LD(ref Registers.E, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.E,Registers.YL*/
		void op_FD_0x5d()
		{
			LD(ref Registers.E, Registers.YL);

			CPUSetTState(4);
		}

		/*LD Registers.E,(Registers.IY+$)*/
		void op_FD_0x5e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			LD(ref Registers.E, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.YH,Registers.B*/
		void op_FD_0x60()
		{
			LD(ref Registers.YH, Registers.B);

			CPUSetTState(4);
		}

		/*LD Registers.YH,Registers.C*/
		void op_FD_0x61()
		{
			LD(ref Registers.YH, Registers.C);

			CPUSetTState(4);
		}

		/*LD Registers.YH,Registers.D*/
		void op_FD_0x62()
		{
			LD(ref Registers.YH, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.YH,Registers.E*/
		void op_FD_0x63()
		{
			LD(ref Registers.YH, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.YH,Registers.YH*/
		void op_FD_0x64()
		{
			LD(ref Registers.YH, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.YH,Registers.YL*/
		void op_FD_0x65()
		{
			LD(ref Registers.YH, Registers.YL);

			CPUSetTState(4);
		}

		/*LD H,(Registers.IY+$)*/
		void op_FD_0x66()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));

			LD(ref Registers.H, m_tmpbyte);
			CPUSetTState(15);
		}

		/*LD Registers.YH,Registers.A*/
		void op_FD_0x67()
		{
			LD(ref Registers.YH, Registers.A);
			CPUSetTState(4);
		}

		/*LD Registers.YL,Registers.B*/
		void op_FD_0x68()
		{
			LD(ref Registers.YL, Registers.B);
			CPUSetTState(4);
		}

		/*LD Registers.YL,Registers.C*/
		void op_FD_0x69()
		{
			LD(ref Registers.YL, Registers.C);
			CPUSetTState(4);
		}

		/*LD Registers.YL,Registers.D*/
		void op_FD_0x6a()
		{
			LD(ref Registers.YL, Registers.D);

			CPUSetTState(4);
		}

		/*LD Registers.YL,Registers.E*/
		void op_FD_0x6b()
		{
			LD(ref Registers.YL, Registers.E);

			CPUSetTState(4);
		}

		/*LD Registers.YL,Registers.YH*/
		void op_FD_0x6c()
		{
			LD(ref Registers.YL, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.YL,Registers.YL*/
		void op_FD_0x6d()
		{
			LD(ref Registers.YL, Registers.YL);

			CPUSetTState(4);
		}

		/*LD L,(Registers.IY+$)*/
		void op_FD_0x6e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			LD(ref Registers.L, m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.YL,Registers.A*/
		void op_FD_0x6f()
		{
			LD(ref Registers.YL, Registers.A);

			CPUSetTState(4);
		}

		/*LD (Registers.IY+$),Registers.B*/
		void op_FD_0x70()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.B);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IY+$),Registers.C*/
		void op_FD_0x71()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.C);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IY+$),Registers.D*/
		void op_FD_0x72()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.D);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IY+$),Registers.E*/
		void op_FD_0x73()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.E);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IY+$),H*/
		void op_FD_0x74()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.H);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IY+$),L*/
		void op_FD_0x75()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.L);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD (Registers.IY+$),Registers.A*/
		void op_FD_0x77()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			LD(ref m_tmpbyte, Registers.A);
			CPUWriteMemory((ushort)(Registers.IY + m_tmpbyte_s), m_tmpbyte);

			CPUSetTState(15);
		}

		/*LD Registers.A,Registers.YH*/
		void op_FD_0x7c()
		{
			LD(ref Registers.A, Registers.YH);

			CPUSetTState(4);
		}

		/*LD Registers.A,Registers.YL*/
		void op_FD_0x7d()
		{
			LD(ref Registers.A, Registers.YL);

			CPUSetTState(4);
		}

		/*LD Registers.A,(Registers.IY+$)*/
		void op_FD_0x7e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			LD(ref Registers.A, m_tmpbyte);

			CPUSetTState(15);
		}

		/*ADD Registers.A,Registers.YH*/
		void op_FD_0x84()
		{
			ADD(Registers.YH);

			CPUSetTState(4);
		}

		/*ADD Registers.A,Registers.YL*/
		void op_FD_0x85()
		{
			ADD(Registers.YL);

			CPUSetTState(4);
		}

		/*ADD Registers.A,(Registers.IY+$)*/
		void op_FD_0x86()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			ADD(m_tmpbyte);

			CPUSetTState(15);
		}

		/*ADC Registers.A,Registers.YH*/
		void op_FD_0x8c()
		{
			ADC(Registers.YH);

			CPUSetTState(4);
		}

		/*ADC Registers.A,Registers.YL*/
		void op_FD_0x8d()
		{
			ADC(Registers.YL);

			CPUSetTState(4);
		}

		/*ADC Registers.A,(Registers.IY+$)*/
		void op_FD_0x8e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			ADC(m_tmpbyte);

			CPUSetTState(15);
		}

		/*SUB Registers.YH*/
		void op_FD_0x94()
		{
			SUB(Registers.YH);

			CPUSetTState(4);
		}

		/*SUB Registers.YL*/
		void op_FD_0x95()
		{
			SUB(Registers.YL);

			CPUSetTState(4);
		}

		/*SUB (Registers.IY+$)*/
		void op_FD_0x96()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			SUB(m_tmpbyte);

			CPUSetTState(15);
		}

		/*SBC Registers.A,Registers.YH*/
		void op_FD_0x9c()
		{
			SBC(Registers.YH);

			CPUSetTState(4);
		}

		/*SBC Registers.A,Registers.YL*/
		void op_FD_0x9d()
		{
			SBC(Registers.YL);

			CPUSetTState(4);
		}

		/*SBC Registers.A,(Registers.IY+$)*/
		void op_FD_0x9e()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			SBC(m_tmpbyte);

			CPUSetTState(15);
		}

		/*AND Registers.YH*/
		void op_FD_0xa4()
		{
			AND(Registers.YH);

			CPUSetTState(4);
		}

		/*AND Registers.YL*/
		void op_FD_0xa5()
		{
			AND(Registers.YL);

			CPUSetTState(4);
		}

		/*AND (Registers.IY+$)*/
		void op_FD_0xa6()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			AND(m_tmpbyte);

			CPUSetTState(15);
		}

		/*XOR Registers.YH*/
		void op_FD_0xac()
		{
			XOR(Registers.YH);

			CPUSetTState(4);
		}

		/*XOR Registers.YL*/
		void op_FD_0xad()
		{
			XOR(Registers.YL);

			CPUSetTState(4);
		}

		/*XOR (Registers.IY+$)*/
		void op_FD_0xae()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			XOR(m_tmpbyte);

			CPUSetTState(15);
		}

		/*OR Registers.YH*/
		void op_FD_0xb4()
		{
			OR(Registers.YH);

			CPUSetTState(4);
		}

		/*OR Registers.YL*/
		void op_FD_0xb5()
		{
			OR(Registers.YL);

			CPUSetTState(4);
		}

		/*OR (Registers.IY+$)*/
		void op_FD_0xb6()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			OR(m_tmpbyte);

			CPUSetTState(15);
		}

		/*CP Registers.YH*/
		void op_FD_0xbc()
		{
			CP(Registers.YH);

			CPUSetTState(4);
		}

		/*CP Registers.YL*/
		void op_FD_0xbd()
		{
			CP(Registers.YL);

			CPUSetTState(4);
		}

		/*CP (Registers.IY+$)*/
		void op_FD_0xbe()
		{
			m_tmpbyte = READ_OP();
			m_tmpbyte_s = (sbyte)((m_tmpbyte & 0x80) != 0 ? -(((~m_tmpbyte) & 0x7f) + 1) : m_tmpbyte);
			Registers.WZ = (ushort)(Registers.IY + m_tmpbyte_s);
			m_tmpbyte = CPUReadMemory((ushort)(Registers.IY + m_tmpbyte_s));
			CP(m_tmpbyte);

			CPUSetTState(15);
		}


		/*POP Registers.IY*/
		void op_FD_0xe1()
		{
			POP(ref Registers.IY);

			CPUSetTState(10);
		}

		/*EX (Registers.SP),Registers.IY*/
		void op_FD_0xe3()
		{
			Registers.TDl = CPUReadMemory((Registers.SP));
			Registers.TDh = CPUReadMemory((ushort)(Registers.SP + 1));
			EX_MPTR(ref Registers.TD, ref Registers.IY);
			CPUWriteMemory((Registers.SP), Registers.TDl);
			CPUWriteMemory((ushort)(Registers.SP + 1), Registers.TDh);

			CPUSetTState(19);
		}

		/*PUSH Registers.IY*/
		void op_FD_0xe5()
		{
			PUSH(Registers.IY);

			CPUSetTState(11);
		}

		/*JP Registers.IY*/
		void op_FD_0xe9()
		{
			JP_NO_MPTR(Registers.IY);

			CPUSetTState(4);
		}

		/*LD Registers.SP,Registers.IY*/
		void op_FD_0xf9()
		{
			LD16(ref Registers.SP, Registers.IY);

			CPUSetTState(6);
		}
	}
}
