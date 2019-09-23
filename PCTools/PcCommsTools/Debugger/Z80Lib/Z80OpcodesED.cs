namespace Z80EmuLib
{
	partial class Z80Emu
	{
		OperationDelegate[] m_opcodes_ed;

		void InitializeOpcodesED()
		{
			m_opcodes_ed = new OperationDelegate[] {
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								op_ED_0x40, op_ED_0x41, op_ED_0x42, op_ED_0x43,
								op_ED_0x44, op_ED_0x45, op_ED_0x46, op_ED_0x47,
								op_ED_0x48, op_ED_0x49, op_ED_0x4a, op_ED_0x4b,
								op_ED_0x4c, op_ED_0x4d, op_ED_0x4e, op_ED_0x4f,
								op_ED_0x50, op_ED_0x51, op_ED_0x52, op_ED_0x53,
								op_ED_0x54, op_ED_0x55, op_ED_0x56, op_ED_0x57,
								op_ED_0x58, op_ED_0x59, op_ED_0x5a, op_ED_0x5b,
								op_ED_0x5c, op_ED_0x5d, op_ED_0x5e, op_ED_0x5f,
								op_ED_0x60, op_ED_0x61, op_ED_0x62, op_ED_0x63,
								op_ED_0x64, op_ED_0x65, op_ED_0x66, op_ED_0x67,
								op_ED_0x68, op_ED_0x69, op_ED_0x6a, op_ED_0x6b,
								op_ED_0x6c, op_ED_0x6d, op_ED_0x6e, op_ED_0x6f,
								op_ED_0x70, op_ED_0x71, op_ED_0x72, op_ED_0x73,
								op_ED_0x74, op_ED_0x75, op_ED_0x76, null      ,
								op_ED_0x78, op_ED_0x79, op_ED_0x7a, op_ED_0x7b,
								op_ED_0x7c, op_ED_0x7d, op_ED_0x7e, null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								op_ED_0xa0, op_ED_0xa1, op_ED_0xa2, op_ED_0xa3,
								null      , null      , null      , null      ,
								op_ED_0xa8, op_ED_0xa9, op_ED_0xaa, op_ED_0xab,
								null      , null      , null      , null      ,
								op_ED_0xb0, op_ED_0xb1, op_ED_0xb2, op_ED_0xb3,
								null      , null      , null      , null      ,
								op_ED_0xb8, op_ED_0xb9, op_ED_0xba, op_ED_0xbb,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null      ,
								null      , null      , null      , null
						};
		}

		/*IN Registers.B,(Registers.C)*/
		void op_ED_0x40()
		{
			IN(ref Registers.B, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.B*/
		void op_ED_0x41()
		{
			OUT(Registers.BC, Registers.B);

			CPUSetTState(8);
		}

		/*SBC Registers.HL,Registers.BC*/
		void op_ED_0x42()
		{
			SBC16(Registers.HL, Registers.BC);

			CPUSetTState(11);
		}

		/*LD (@),Registers.BC*/
		void op_ED_0x43()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.BC, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x44()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETN*/
		void op_ED_0x45()
		{
			RETN();

			CPUSetTState(10);
		}

		/*IM 0*/
		void op_ED_0x46()
		{
			IMx(IMMode.IM0);

			CPUSetTState(4);
		}

		/*LD cpu.i,Registers.A*/
		void op_ED_0x47()
		{
			LD(ref Registers.I, Registers.A);

			CPUSetTState(5);
		}

		/*IN Registers.C,(Registers.C)*/
		void op_ED_0x48()
		{
			IN(ref Registers.C, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.C*/
		void op_ED_0x49()
		{
			OUT(Registers.BC, Registers.C);

			CPUSetTState(8);
		}

		/*ADC Registers.HL,Registers.BC*/
		void op_ED_0x4a()
		{
			ADC16(Registers.HL, Registers.BC);

			CPUSetTState(11);
		}

		/*LD Registers.BC,(@)*/
		void op_ED_0x4b()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.BC, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x4c()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETI*/
		void op_ED_0x4d()
		{
			RETI();

			CPUSetTState(10);
		}

		/*IM 0*/
		void op_ED_0x4e()
		{
			IMx(IMMode.IM0);

			CPUSetTState(4);
		}

		/*LD R,Registers.A*/
		void op_ED_0x4f()
		{
			LD_R_A();

			CPUSetTState(5);
		}

		/*IN Registers.D,(Registers.C)*/
		void op_ED_0x50()
		{
			IN(ref Registers.D, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.D*/
		void op_ED_0x51()
		{
			OUT(Registers.BC, Registers.D);

			CPUSetTState(8);
		}

		/*SBC Registers.HL,Registers.DE*/
		void op_ED_0x52()
		{
			SBC16(Registers.HL, Registers.DE);

			CPUSetTState(11);
		}

		/*LD (@),Registers.DE*/
		void op_ED_0x53()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.DE, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x54()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETN*/
		void op_ED_0x55()
		{
			RETN();

			CPUSetTState(10);
		}

		/*IM 1*/
		void op_ED_0x56()
		{
			IMx(IMMode.IM1);

			CPUSetTState(4);
		}

		/*LD Registers.A,cpu.i*/
		void op_ED_0x57()
		{
			LD_A_I();

			CPUSetTState(5);
		}

		/*IN Registers.E,(Registers.C)*/
		void op_ED_0x58()
		{
			IN(ref Registers.E, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.E*/
		void op_ED_0x59()
		{
			OUT(Registers.BC, Registers.E);

			CPUSetTState(8);
		}

		/*ADC Registers.HL,Registers.DE*/
		void op_ED_0x5a()
		{
			ADC16(Registers.HL, Registers.DE);

			CPUSetTState(11);
		}

		/*LD Registers.DE,(@)*/
		void op_ED_0x5b()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.DE, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x5c()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETI*/
		void op_ED_0x5d()
		{
			RETI();

			CPUSetTState(10);
		}

		/*IM 2*/
		void op_ED_0x5e()
		{
			IMx(IMMode.IM2);

			CPUSetTState(4);
		}

		/*LD Registers.A,R*/
		void op_ED_0x5f()
		{
			LD_A_R();

			CPUSetTState(5);
		}

		/*IN Registers.H,(Registers.C)*/
		void op_ED_0x60()
		{
			IN(ref Registers.H, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.H*/
		void op_ED_0x61()
		{
			OUT(Registers.BC, Registers.H);

			CPUSetTState(8);
		}

		/*SBC Registers.HL,Registers.HL*/
		void op_ED_0x62()
		{
			SBC16(Registers.HL, Registers.HL);

			CPUSetTState(11);
		}

		/*LD (@),Registers.HL*/
		void op_ED_0x63()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.HL, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x64()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETN*/
		void op_ED_0x65()
		{
			RETN();

			CPUSetTState(10);
		}

		/*IM 0*/
		void op_ED_0x66()
		{
			IMx(IMMode.IM0);

			CPUSetTState(4);
		}

		/*RRD*/
		void op_ED_0x67()
		{
			RRD();

			CPUSetTState(14);
		}

		/*IN Registers.L,(Registers.C)*/
		void op_ED_0x68()
		{
			IN(ref Registers.L, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.L*/
		void op_ED_0x69()
		{
			OUT(Registers.BC, Registers.L);

			CPUSetTState(8);
		}

		/*ADC Registers.HL,Registers.HL*/
		void op_ED_0x6a()
		{
			ADC16(Registers.HL, Registers.HL);

			CPUSetTState(11);
		}

		/*LD Registers.HL,(@)*/
		void op_ED_0x6b()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.HL, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x6c()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETI*/
		void op_ED_0x6d()
		{
			RETI();

			CPUSetTState(10);
		}

		/*IM 0*/
		void op_ED_0x6e()
		{
			IMx(IMMode.IM0);

			CPUSetTState(4);
		}

		/*RLD*/
		void op_ED_0x6f()
		{
			RLD();

			CPUSetTState(14);
		}

		/*IN_F (Registers.C)*/
		void op_ED_0x70()
		{
			IN_F(Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),0*/
		void op_ED_0x71()
		{
			OUT(Registers.BC, 0);

			CPUSetTState(8);
		}

		/*SBC Registers.HL,Registers.SP*/
		void op_ED_0x72()
		{
			SBC16(Registers.HL, Registers.SP);

			CPUSetTState(11);
		}

		/*LD (@),Registers.SP*/
		void op_ED_0x73()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			LD_RP_TO_ADDR_MPTR_16(out Registers.TD, Registers.SP, Registers.TA);
			CPUWriteMemory(Registers.TA, Registers.TDl);
			CPUWriteMemory((ushort)(Registers.TA + 1), Registers.TDh);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x74()
		{
			NEG();

			CPUSetTState(4);
		}

		/*RETN*/
		void op_ED_0x75()
		{
			RETN();

			CPUSetTState(10);
		}

		/*IM 1*/
		void op_ED_0x76()
		{
			IMx(IMMode.IM1);

			CPUSetTState(4);
		}

		/*IN Registers.A,(Registers.C)*/
		void op_ED_0x78()
		{
			IN(ref Registers.A, Registers.BC);

			CPUSetTState(8);
		}

		/*OUT (Registers.C),Registers.A*/
		void op_ED_0x79()
		{
			OUT(Registers.BC, Registers.A);
			CPUSetTState(8);
		}

		/*ADC Registers.HL,Registers.SP*/
		void op_ED_0x7a()
		{
			ADC16(Registers.HL, Registers.SP);
			CPUSetTState(11);
		}

		/*LD Registers.SP,(@)*/
		void op_ED_0x7b()
		{
			Registers.TAl = READ_OP();
			Registers.TAh = READ_OP();
			Registers.TDl = CPUReadMemory(Registers.TA);
			Registers.TDh = CPUReadMemory((ushort)(Registers.TA + 1));
			LD_RP_FROM_ADDR_MPTR_16(ref Registers.SP, Registers.TD, Registers.TA);

			CPUSetTState(16);
		}

		/*NEG*/
		void op_ED_0x7c()
		{
			NEG();
			CPUSetTState(4);
		}

		/*RETI*/
		void op_ED_0x7d()
		{
			RETI();

			CPUSetTState(10);
		}

		/*IM 2*/
		void op_ED_0x7e()
		{
			IMx(IMMode.IM2);

			CPUSetTState(4);
		}

		/*LDI*/
		void op_ED_0xa0()
		{
			LDI();

			CPUSetTState(12);
		}

		/*CPI*/
		void op_ED_0xa1()
		{
			CPI();

			CPUSetTState(12);
		}

		/*INI*/
		void op_ED_0xa2()
		{
			INI();

			CPUSetTState(12);
		}

		/*OUTI*/
		void op_ED_0xa3()
		{
			OUTI();

			CPUSetTState(12);
		}

		/*LDD*/
		void op_ED_0xa8()
		{
			LDD();

			CPUSetTState(12);
		}

		/*CPD*/
		void op_ED_0xa9()
		{
			CPD();

			CPUSetTState(12);
		}

		/*IND*/
		void op_ED_0xaa()
		{
			IND();

			CPUSetTState(12);
		}

		/*OUTD*/
		void op_ED_0xab()
		{
			OUTD();

			CPUSetTState(12);
		}

		/*LDIR*/
		void op_ED_0xb0()
		{
			LDIR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*CPIR*/
		void op_ED_0xb1()
		{
			CPIR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*INIR*/
		void op_ED_0xb2()
		{
			INIR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*OTIR*/
		void op_ED_0xb3()
		{
			OTIR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*LDDR*/
		void op_ED_0xb8()
		{
			LDDR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*CPDR*/
		void op_ED_0xb9()
		{
			CPDR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*INDR*/
		void op_ED_0xba()
		{
			INDR(/*t:*/ /*t1*/12,/*t2*/17);
		}

		/*OTDR*/
		void op_ED_0xbb()
		{
			OTDR(/*t:*/ /*t1*/12,/*t2*/17);
		}

	}
}
