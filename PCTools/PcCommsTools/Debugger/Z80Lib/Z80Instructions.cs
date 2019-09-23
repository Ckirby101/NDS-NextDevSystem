using System.Runtime.CompilerServices;

namespace Z80EmuLib
{
	partial class Z80Emu
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void AND(byte value)
		{
			Registers.A &= value;
			Registers.F = (byte)(Tables.FLAG_H | Tables.sz53p_table[Registers.A]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ADC(byte value)
		{
			ushort adctemp = (ushort)(Registers.A + value + (Registers.F & Tables.FLAG_C));
			byte lookup = (byte)(
						((Registers.A & 0x88) >> 3)
					| ((value & 0x88) >> 2)
					| ((adctemp & 0x88) >> 1)
					);
			Registers.A = (byte)adctemp;
			Registers.F = (byte)(
						((adctemp & 0x100) != 0 ? Tables.FLAG_C : (byte)0)
					| Tables.halfcarry_add_table[lookup & 0x07]
					| Tables.overflow_add_table[lookup >> 4]
					| Tables.sz53_table[Registers.A]
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ADC16(ushort hl, ushort value)
		{
			int add16temp = Registers.HL + value + (Registers.F & Tables.FLAG_C);

			byte lookup = (byte)(
										((Registers.HL & 0x8800) >> 11)
									| ((value & 0x8800) >> 10)
									| ((add16temp & 0x8800) >> 9)
									);

			Registers.WZ = (ushort)(hl + 1);

			Registers.HL = (ushort)add16temp;

			Registers.F = (byte)(
						((add16temp & 0x10000) != 0 ? Tables.FLAG_C : (byte)0)
					| Tables.overflow_add_table[lookup >> 4]
					| (Registers.H & (Tables.FLAG_3 | Tables.FLAG_5 | Tables.FLAG_S))
					| Tables.halfcarry_add_table[lookup & 0x07]
					| (Registers.HL != 0 ? (byte)0 : Tables.FLAG_Z)
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ADD(byte value)
		{
			ushort addtemp = (ushort)(Registers.A + value);
			byte lookup = (byte)(
										((Registers.A & 0x88) >> 3)
									| ((value & 0x88) >> 2)
									| ((addtemp & 0x88) >> 1)
									);
			Registers.A = (byte)addtemp;
			Registers.F = (byte)(
										((addtemp & 0x100) != 0 ? Tables.FLAG_C : (byte)0)
									| Tables.halfcarry_add_table[lookup & 0x07]
									| Tables.overflow_add_table[lookup >> 4]
									| Tables.sz53_table[Registers.A]
							);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ADD16(ref ushort value1, ushort value2)
		{
			int add16temp = value1 + value2;

			byte lookup = (byte)(
												((value1 & 0x0800) >> 11)
											| ((value2 & 0x0800) >> 10)
											| ((add16temp & 0x0800) >> 9)
									);

			Registers.WZ = (ushort)(value1 + 1);
			value1 = (ushort)add16temp;

			Registers.F = (byte)(
									(Registers.F & (Tables.FLAG_V | Tables.FLAG_Z | Tables.FLAG_S))
									| ((add16temp & 0x10000) != 0 ? Tables.FLAG_C : (byte)0)
									| ((add16temp >> 8) & (Tables.FLAG_3 | Tables.FLAG_5))
									| Tables.halfcarry_add_table[lookup]
							);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void BIT(byte bit, byte value)
		{
			Registers.F = (byte)(
								(Registers.F & Tables.FLAG_C)
							| Tables.FLAG_H
							| Tables.sz53p_table[value & (0x01 << bit)]
							| (value & 0x28)
					);
		}

		/**
		 * BIT n,(IX+d/IY+d) and BIT n,(HL)
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void BIT_MPTR(byte bit, byte value)
		{
			byte val = (byte)(value & (0x01 << bit));
			Registers.F = (byte)(
				val & Tables.FLAG_S |
				((val != 0) ? 0 : (Tables.FLAG_Z | Tables.FLAG_P)) |
				(Registers.WZh & (Tables.FLAG_3 | Tables.FLAG_5)) |
				Tables.FLAG_H |
				(Registers.F & Tables.FLAG_C));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CALL(ushort addr)
		{
			PUSH(Registers.PC);
			Registers.PC = Registers.WZ = addr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CP(byte value)
		{
			int cptemp = Registers.A - value;
			byte lookup = (byte)(
												((Registers.A & 0x88) >> 3)
											| ((value & 0x88) >> 2)
											| ((cptemp & 0x88) >> 1)
									);
			Registers.F = (byte)(
									((cptemp & 0x100) != 0 ? Tables.FLAG_C : (cptemp != 0 ? (byte)0 : Tables.FLAG_Z))
									| Tables.FLAG_N
									| Tables.halfcarry_sub_table[lookup & 0x07]
									| Tables.overflow_sub_table[lookup >> 4]
									| (value & (Tables.FLAG_3 | Tables.FLAG_5))
									| (cptemp & Tables.FLAG_S)
							);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void DEC(ref byte value)
		{
			Registers.F = (byte)(
							(Registers.F & Tables.FLAG_C) | ((value & 0x0f) != 0 ? (byte)0 : Tables.FLAG_H) | Tables.FLAG_N
					);
			value--;
			Registers.F |= (byte)(
										(value == 0x7f ? Tables.FLAG_V : (byte)0)
									| Tables.sz53_table[value]
							);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void DEC16(ref ushort value)
		{
			value--;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void INC(ref byte value)
		{
			value++;
			Registers.F = (byte)(
										(Registers.F & Tables.FLAG_C) | (value == 0x80 ? Tables.FLAG_V : (byte)0)
									| ((value & 0x0f) != 0 ? (byte)0 : Tables.FLAG_H)
									| Tables.sz53_table[(value)]
							);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void INC16(ref ushort value)
		{
			value++;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void LD(ref byte dst, byte src)
		{
			dst = src;
		}

		/**
		 * ld (nnnn|BC|DE), A
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_A_TO_ADDR_MPTR(ref byte dest, byte src, ushort addr)
		{
			dest = src;
			Registers.WZh = Registers.A;
			//Registers.WZl = (byte)((addr + 1) & 0xFF);
			Registers.WZl = (byte)(addr + 1);
		}

		/**
		 * ld a,(BC|DE|nnnn)
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_A_FROM_ADDR_MPTR(ref byte dest, byte src, ushort addr)
		{
			dest = src;
			Registers.WZ = (ushort)(addr + 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void LD16(ref ushort dest, ushort src)
		{
			dest = src;
		}

		/**
		 * xxCB codes call this with bytes for some reason
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void LD16(ref byte dest, byte src)
		{
			dest = src;
		}

		/**
		 * ld (nnnn),BC|DE|SP|HL|IX|IY
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_RP_TO_ADDR_MPTR_16(out ushort dest, ushort src, ushort addr)
		{
			dest = src;
			Registers.WZ = (ushort)(addr + 1);
		}

		/**
		 * ld BC|DE|SP|HL|IX|IY,(nnnn)
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_RP_FROM_ADDR_MPTR_16(ref ushort dest, ushort src, ushort addr)
		{
			dest = src;
			Registers.WZ = (ushort)(addr + 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void JP_NO_MPTR(ushort addr)
		{
			Registers.PC = addr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void JP(ushort addr)
		{
			Registers.PC = Registers.WZ = addr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void JR(sbyte offset)
		{
			Registers.PC = (ushort)(Registers.PC + offset);
			Registers.WZ = Registers.PC;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OR(byte value)
		{
			Registers.A = (byte)(Registers.A | value);
			Registers.F = Tables.sz53p_table[Registers.A];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OUT(ushort port, byte reg)
		{
			CPUWritePort(port, reg);
			Registers.WZ = (ushort)(port + 1);
		}

		/**
		 * OUT (nn),A
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OUT_A(ushort port, byte reg)
		{
			CPUWritePort(port, reg);
			Registers.WZl = (byte)(port + 1);
			Registers.WZh = Registers.A;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IN(ref byte reg, ushort port)
		{
			reg = CPUReadPort(port);
			Registers.F = (byte)((Registers.F & Tables.FLAG_C) | Tables.sz53p_table[reg]);
			Registers.WZ = (ushort)(port + 1);
		}

		/**
		 * IN A,(nn)
		 */
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IN_A(ref byte reg, ushort port)
		{
			reg = CPUReadPort(port);
			Registers.WZ = (ushort)(port + 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IN_F(ushort port)
		{
			byte val = 0;
			IN(ref val, port);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void POP(ref ushort rp)
		{
			Registers.TDl = CPUReadMemory(Registers.SP++);
			Registers.TDh = CPUReadMemory(Registers.SP++);

			rp = Registers.TD;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void PUSH(ushort rp)
		{
			Registers.TD = rp;

			CPUWriteMemory(--Registers.SP, Registers.TDh);
			CPUWriteMemory(--Registers.SP, Registers.TDl);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RET()
		{
			POP(ref Registers.PC);
			Registers.WZ = Registers.PC;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RL(ref byte value)
		{
			byte rltemp = value;
			value = (byte)((value << 1) | (Registers.F & Tables.FLAG_C));
			Registers.F = (byte)((rltemp >> 7) | Tables.sz53p_table[value]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RLC(ref byte value)
		{
			value = (byte)((value << 1) | (value >> 7));
			Registers.F = (byte)((value & Tables.FLAG_C) | Tables.sz53p_table[value]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RR(ref byte value)
		{
			byte rrtemp = value;
			value = (byte)((value >> 1) | (Registers.F << 7));
			Registers.F = (byte)((rrtemp & Tables.FLAG_C) | Tables.sz53p_table[value]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RRC(ref byte value)
		{
			Registers.F = (byte)(value & Tables.FLAG_C);
			value = (byte)((value >> 1) | (value << 7));
			Registers.F |= Tables.sz53p_table[value];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RST(ushort value)
		{
			PUSH(Registers.PC);
			Registers.PC = value;
			Registers.WZ = Registers.PC;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SBC(byte value)
		{
			int sbctemp = Registers.A - value - (Registers.F & Tables.FLAG_C);
			byte lookup = (byte)(
											((Registers.A & 0x88) >> 3)
											| ((value & 0x88) >> 2)
											| ((sbctemp & 0x88) >> 1)
									);
			Registers.A = (byte)sbctemp;
			Registers.F = (byte)(
									((sbctemp & 0x100) != 0 ? Tables.FLAG_C : (byte)0)
									| Tables.FLAG_N
									| Tables.halfcarry_sub_table[lookup & 0x07]
									| Tables.overflow_sub_table[lookup >> 4]
									| Tables.sz53_table[Registers.A]
							 );
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SBC16(ushort hl, ushort value)
		{
			int sub16temp = Registers.HL - value - (Registers.F & Tables.FLAG_C);
			byte lookup = (byte)(
											((Registers.HL & 0x8800) >> 11)
											| ((value & 0x8800) >> 10)
											| ((sub16temp & 0x8800) >> 9)
									);
			Registers.WZ = (ushort)(hl + 1);
			Registers.HL = (ushort)sub16temp;
			Registers.F = (byte)(
									((sub16temp & 0x10000) != 0 ? Tables.FLAG_C : (byte)0)
									| Tables.FLAG_N
									| Tables.overflow_sub_table[lookup >> 4]
									| (Registers.H & (Tables.FLAG_3 | Tables.FLAG_5 | Tables.FLAG_S))
									| Tables.halfcarry_sub_table[lookup & 0x07]
									| (Registers.HL != 0 ? (byte)0 : Tables.FLAG_Z)
							);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SLA(ref byte value)
		{
			Registers.F = (byte)(value >> 7);
			value <<= 1;
			Registers.F |= Tables.sz53p_table[value];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SLL(ref byte value)
		{
			Registers.F = (byte)(value >> 7);
			value = (byte)((value << 1) | 0x01);
			Registers.F |= Tables.sz53p_table[(value)];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SRA(ref byte value)
		{
			Registers.F = (byte)(value & Tables.FLAG_C);
			value = (byte)((value & 0x80) | (value >> 1));
			Registers.F |= Tables.sz53p_table[value];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SRL(ref byte value)
		{
			Registers.F = (byte)(value & Tables.FLAG_C);
			value >>= 1;
			Registers.F |= Tables.sz53p_table[value];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SUB(byte value)
		{
			int subtemp = Registers.A - value;
			byte lookup = (byte)(
					((Registers.A & 0x88) >> 3)
					| ((value & 0x88) >> 2)
					| ((subtemp & 0x88) >> 1)
					);
			Registers.A = (byte)subtemp;
			Registers.F = (byte)(
					((subtemp & 0x100) != 0 ? Tables.FLAG_C : (byte)0)
					| Tables.FLAG_N
					| Tables.halfcarry_sub_table[lookup & 0x07]
					| Tables.overflow_sub_table[lookup >> 4]
					| Tables.sz53_table[Registers.A]
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void XOR(byte value)
		{
			Registers.A ^= value;
			Registers.F = Tables.sz53p_table[Registers.A];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RRD()
		{
			byte bytetemp = CPUReadMemory(Registers.HL);
			CPUWriteMemory(Registers.HL, (byte)((Registers.A << 4) | (bytetemp >> 4)));
			Registers.A = (byte)((Registers.A & 0xf0) | (bytetemp & 0x0f));
			Registers.F = (byte)((Registers.F & Tables.FLAG_C) | Tables.sz53p_table[Registers.A]);
			Registers.WZ = (ushort)(Registers.HL + 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RLD()
		{
			byte bytetemp = CPUReadMemory(Registers.HL);
			CPUWriteMemory(Registers.HL, (byte)((bytetemp << 4) | (Registers.A & 0x0f)));
			Registers.A = (byte)((Registers.A & 0xf0) | (bytetemp >> 4));
			Registers.F = (byte)((Registers.F & Tables.FLAG_C) | Tables.sz53p_table[Registers.A]);
			Registers.WZ = (ushort)(Registers.HL + 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IMx(IMMode mode)
		{
			IM = mode;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_A_R()
		{
			Registers.A = Registers.R;
			Registers.F = (byte)(
					(Registers.F & Tables.FLAG_C)
					| Tables.sz53_table[Registers.A]
					| (IFF2 != 0 ? Tables.FLAG_V : (byte)0)
					);
			m_reset_PV_on_int = true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_R_A()
		{
			Registers.R = Registers.A;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LD_A_I()
		{
			Registers.A = Registers.I;
			Registers.F = (byte)(
					(Registers.F & Tables.FLAG_C)
					| Tables.sz53_table[Registers.A]
					| (IFF2 != 0 ? Tables.FLAG_V : (byte)0)
					);
			m_reset_PV_on_int = true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void NEG()
		{
			byte bytetemp = Registers.A;
			Registers.A = 0;
			SUB(bytetemp);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RETI()
		{
			IFF1 = IFF2;
			RET();
			RetiEventHandler?.Invoke(this);
		}

		/*same as RETI, only opcode is different*/
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RETN()
		{
			IFF1 = IFF2;
			RET();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LDI()
		{
			byte bytetemp = CPUReadMemory(Registers.HL);
			Registers.BC--;
			CPUWriteMemory(Registers.DE, bytetemp);
			Registers.DE++; Registers.HL++;
			bytetemp += Registers.A;
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_C | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.BC != 0 ? Tables.FLAG_V : (byte)0)
					| (bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPI()
		{
			byte value = CPUReadMemory(Registers.HL);
			byte bytetemp = (byte)(Registers.A - value);
			byte lookup = (byte)(
					((Registers.A & 0x08) >> 3)
					| ((value & 0x08) >> 2)
					| ((bytetemp & 0x08) >> 1)
					);
			Registers.HL++; Registers.BC--;
			Registers.F = (byte)(
					(Registers.F & Tables.FLAG_C)
					| (Registers.BC != 0 ? (byte)(Tables.FLAG_V | Tables.FLAG_N) : Tables.FLAG_N)
					| Tables.halfcarry_sub_table[lookup]
					| (bytetemp != 0 ? (byte)0 : Tables.FLAG_Z)
					| (bytetemp & Tables.FLAG_S)
					);
			if ((Registers.F & Tables.FLAG_H) != 0) bytetemp--;
			Registers.F |= (byte)(
					(bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);
			Registers.WZ = (ushort)(Registers.WZ + 1);
		}

		/*undocumented flag effects for block output operations*/
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OUT_BL(byte pbyte)
		{
			byte kval = (byte)(pbyte + Registers.L);
			if ((pbyte + Registers.L) > 255) Registers.F |= (Tables.FLAG_C | Tables.FLAG_H);
			Registers.F |= Tables.parity_table[((kval & 7) ^ Registers.B)];
		}

		/*undocumented flag effects for block input operations*/
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IN_BL(byte pbyte, sbyte c_add)
		{
			byte kval = (byte)(pbyte + ((Registers.C + c_add) & 0xff));
			if ((pbyte + ((Registers.C + c_add) & 0xff)) > 255) Registers.F |= (Tables.FLAG_C | Tables.FLAG_H);
			Registers.F |= Tables.parity_table[((kval & 7) ^ Registers.B)];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void INI()
		{
			Registers.WZ = (ushort)(Registers.BC + 1);
			byte initemp = CPUReadPort(Registers.BC);
			CPUWriteMemory(Registers.HL, initemp);
			Registers.B--; Registers.HL++;
			Registers.F = (byte)(
					((initemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0)
					| Tables.sz53_table[Registers.B]
					);
			IN_BL(initemp, 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OUTI()
		{
			byte outitemp = CPUReadMemory(Registers.HL);
			Registers.B--;
			Registers.WZ = (ushort)(Registers.BC + 1);
			CPUWritePort(Registers.BC, outitemp);
			Registers.HL++;
			Registers.F = (byte)(((outitemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0) | Tables.sz53_table[Registers.B]);
			OUT_BL(outitemp);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LDD()
		{
			byte bytetemp = CPUReadMemory(Registers.HL);
			Registers.BC--;
			CPUWriteMemory(Registers.DE, bytetemp);
			Registers.DE--; Registers.HL--;
			bytetemp += Registers.A;
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_C | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.BC != 0 ? Tables.FLAG_V : (byte)0)
					| (bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPD()
		{
			byte value = CPUReadMemory(Registers.HL);
			byte bytetemp = (byte)(Registers.A - value);
			byte lookup = (byte)(
					((Registers.A & 0x08) >> 3)
					| ((value & 0x08) >> 2)
					| ((bytetemp & 0x08) >> 1)
					);
			Registers.HL--; Registers.BC--;
			Registers.F = (byte)(
					(Registers.F & Tables.FLAG_C)
					| (Registers.BC != 0 ? (byte)(Tables.FLAG_V | Tables.FLAG_N) : Tables.FLAG_N)
					| Tables.halfcarry_sub_table[lookup]
					| (bytetemp != 0 ? (byte)0 : Tables.FLAG_Z)
					| (bytetemp & Tables.FLAG_S)
					);
			if ((Registers.F & Tables.FLAG_H) != 0) bytetemp--;
			Registers.F |= (byte)(
					(bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);
			Registers.WZ = (ushort)(Registers.WZ - 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IND()
		{
			Registers.WZ = (ushort)(Registers.BC - 1);
			byte initemp = CPUReadPort(Registers.BC);
			CPUWriteMemory(Registers.HL, initemp);
			Registers.B--; Registers.HL--;
			Registers.F = (byte)(
					((initemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0)
					| Tables.sz53_table[Registers.B]
					);
			IN_BL(initemp, -1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OUTD()
		{
			byte outitemp = CPUReadMemory(Registers.HL);
			Registers.B--;
			Registers.WZ = (ushort)(Registers.BC - 1);
			CPUWritePort(Registers.BC, outitemp);
			Registers.HL--;
			Registers.F = (byte)(((outitemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0) | Tables.sz53_table[Registers.B]);
			OUT_BL(outitemp);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LDIR(ushort t1, ushort t2)
		{
			byte bytetemp = CPUReadMemory(Registers.HL);
			CPUWriteMemory(Registers.DE, bytetemp);
			Registers.HL++; Registers.DE++; Registers.BC--;
			bytetemp += Registers.A;
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_C | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.BC != 0 ? Tables.FLAG_V : (byte)0)
					| (bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);
			if (Registers.BC != 0)
			{
				Registers.PC -= 2;
				Registers.WZ = (ushort)(Registers.PC + 1);

				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPIR(ushort t1, ushort t2)
		{
			byte value = CPUReadMemory(Registers.HL);
			byte bytetemp = (byte)(Registers.A - value);
			byte lookup = (byte)(
					((Registers.A & 0x08) >> 3)
					| ((value & 0x08) >> 2)
					| ((bytetemp & 0x08) >> 1)
					);
			Registers.HL++; Registers.BC--;
			Registers.F = (byte)(
					(Registers.F & Tables.FLAG_C)
					| (Registers.BC != 0 ? (byte)(Tables.FLAG_V | Tables.FLAG_N) : Tables.FLAG_N)
					| Tables.halfcarry_sub_table[lookup]
					| (bytetemp != 0 ? (byte)0 : Tables.FLAG_Z)
					| (bytetemp & Tables.FLAG_S)
					);

			if ((Registers.F & Tables.FLAG_H) != 0) bytetemp--;
			Registers.F |= (byte)(
					(bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);

			if ((Registers.F & (Tables.FLAG_V | Tables.FLAG_Z)) == Tables.FLAG_V)
			{
				Registers.PC -= 2;
				Registers.WZ = (ushort)(Registers.PC + 1);

				CPUSetTState(t2);
			}
			else
			{
				Registers.WZ = (ushort)(Registers.WZ + 1);

				CPUSetTState(t1);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void INIR(uint t1, uint t2)
		{
			byte initemp = CPUReadPort(Registers.BC);
			CPUWriteMemory(Registers.HL, initemp);
			Registers.WZ = (ushort)(Registers.BC + 1);
			Registers.B--; Registers.HL++;
			Registers.F = (byte)(
					((initemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0)
					| Tables.sz53_table[Registers.B]
					);
			if (Registers.B != 0)
			{
				Registers.PC -= 2;

				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
			IN_BL(initemp, 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OTIR(uint t1, uint t2)
		{
			byte outitemp = CPUReadMemory(Registers.HL);
			Registers.B--;
			Registers.WZ = (ushort)(Registers.BC + 1);
			CPUWritePort(Registers.BC, outitemp);
			Registers.HL++;
			Registers.F = (byte)(
					((outitemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0)
					| Tables.sz53_table[Registers.B]
					);
			if (Registers.B != 0)
			{
				Registers.PC -= 2;

				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
			OUT_BL(outitemp);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void LDDR(uint t1, uint t2)
		{
			byte bytetemp = CPUReadMemory(Registers.HL);
			CPUWriteMemory(Registers.DE, bytetemp);
			Registers.HL--; Registers.DE--; Registers.BC--;
			bytetemp += Registers.A;
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_C | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.BC != 0 ? Tables.FLAG_V : (byte)0)
					| (bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);

			if (Registers.BC != 0)
			{
				Registers.PC -= 2;
				Registers.WZ = (ushort)(Registers.PC + 1);

				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPDR(uint t1, uint t2)
		{
			byte value = CPUReadMemory(Registers.HL);
			byte bytetemp = (byte)(Registers.A - value);
			byte lookup = (byte)(
					((Registers.A & 0x08) >> 3)
					| ((value & 0x08) >> 2)
					| ((bytetemp & 0x08) >> 1)
					);
			Registers.HL--; Registers.BC--;
			Registers.F = (byte)(
					(Registers.F & Tables.FLAG_C)
					| (Registers.BC != 0 ? (byte)(Tables.FLAG_V | Tables.FLAG_N) : Tables.FLAG_N)
					| Tables.halfcarry_sub_table[lookup]
					| (bytetemp != 0 ? (byte)0 : Tables.FLAG_Z)
					| (bytetemp & Tables.FLAG_S)
					);

			if ((Registers.F & Tables.FLAG_H) != 0) bytetemp--;
			Registers.F |= (byte)(
					(bytetemp & Tables.FLAG_3)
					| ((bytetemp & 0x02) != 0 ? Tables.FLAG_5 : (byte)0)
					);
			if ((Registers.F & (Tables.FLAG_V | Tables.FLAG_Z)) == Tables.FLAG_V)
			{
				Registers.PC -= 2;
				Registers.WZ = (ushort)(Registers.PC + 1);

				CPUSetTState(t2);
			}
			else
			{
				Registers.WZ = (ushort)(Registers.WZ - 1);
				CPUSetTState(t1);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void INDR(uint t1, uint t2)
		{
			byte initemp = CPUReadPort(Registers.BC);
			CPUWriteMemory(Registers.HL, initemp);
			Registers.WZ = (ushort)(Registers.BC - 1);
			Registers.B--; Registers.HL--;
			Registers.F = (byte)(
					((initemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0)
					| Tables.sz53_table[Registers.B]
					);
			if (Registers.B != 0)
			{
				Registers.PC -= 2;

				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
			IN_BL(initemp, -1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void OTDR(uint t1, uint t2)
		{
			byte outitemp = CPUReadMemory(Registers.HL);
			Registers.B--;
			Registers.WZ = (ushort)(Registers.BC - 1);
			CPUWritePort(Registers.BC, outitemp);
			Registers.HL--;
			Registers.F = (byte)(
					((outitemp & 0x80) != 0 ? Tables.FLAG_N : (byte)0)
					| Tables.sz53_table[Registers.B]
					);
			if (Registers.B != 0)
			{
				Registers.PC -= 2;

				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
			OUT_BL(outitemp);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RLCA()
		{
			Registers.A = (byte)((Registers.A << 1) | (Registers.A >> 7));
			Registers.F = (byte)(
							(Registers.F & (Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
							| (Registers.A & (Tables.FLAG_C | Tables.FLAG_3 | Tables.FLAG_5))
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RRCA()
		{
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.A & Tables.FLAG_C)
					);
			Registers.A = (byte)((Registers.A >> 1) | (Registers.A << 7));
			Registers.F |= (byte)((Registers.A & (Tables.FLAG_3 | Tables.FLAG_5)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void DJNZ(int offset, uint t1, uint t2)
		{
			Registers.B--;
			if (Registers.B != 0)
			{
				Registers.PC = (ushort)(Registers.PC + offset);
				Registers.WZ = Registers.PC;
				CPUSetTState(t2);
			}
			else
			{
				CPUSetTState(t1);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RLA()
		{
			byte bytetemp = Registers.A;
			Registers.A = (byte)((Registers.A << 1) | (Registers.F & Tables.FLAG_C));
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.A & (Tables.FLAG_3 | Tables.FLAG_5)) | (bytetemp >> 7)
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RRA()
		{
			byte bytetemp = Registers.A;
			Registers.A = (byte)((Registers.A >> 1) | (Registers.F << 7));
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.A & (Tables.FLAG_3 | Tables.FLAG_5)) | (bytetemp & Tables.FLAG_C)
				);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void DAA()
		{
			int index = (Registers.A + 0x100 * ((Registers.F & 3) + ((Registers.F >> 2) & 4))) * 2;
			Registers.F = Tables.daatab[index];
			Registers.A = Tables.daatab[index + 1];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void EX(ref ushort rp1, ref ushort rp2)
		{
			ushort tmp = rp1;
			rp1 = rp2;
			rp2 = tmp;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void EX_MPTR(ref ushort rp1, ref ushort rp2)
		{
			ushort wordtemp = rp1; rp1 = rp2; rp2 = wordtemp;
			Registers.WZ = wordtemp;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CPL()
		{
			Registers.A ^= 0xff;
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_C | Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
					| (Registers.A & (Tables.FLAG_3 | Tables.FLAG_5)) | (Tables.FLAG_N | Tables.FLAG_H)
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SCF()
		{
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
					| ((Registers.A | Registers.F) & (Tables.FLAG_3 | Tables.FLAG_5))
					| Tables.FLAG_C
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void CCF()
		{
			Registers.F = (byte)(
					(Registers.F & (Tables.FLAG_P | Tables.FLAG_Z | Tables.FLAG_S))
					| ((Registers.F & Tables.FLAG_C) != 0 ? Tables.FLAG_H : Tables.FLAG_C)
					| ((Registers.A | Registers.F) & (Tables.FLAG_3 | Tables.FLAG_5))
					);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void HALT()
		{
			Halted = true;
			Registers.PC--;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void EXX()
		{
			Registers.ExchangeRegisterSet();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void DI()
		{
			IFF1 = IFF2 = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void EI()
		{
			IFF1 = IFF2 = 1;
			m_noint_once = true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void SET(byte bit, ref byte val)
		{
			val |= (byte)(1 << bit);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void RES(byte bit, ref byte val)
		{
			val &= (byte)~(1 << bit);
		}

	} // class
} // namespace
