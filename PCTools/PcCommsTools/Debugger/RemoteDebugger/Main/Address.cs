using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAccessException = System.FieldAccessException;

namespace RemoteDebugger.Main
{
    public class NextAddress
    {
        private int addr;       //0-8191
        private int bank;       //0-222
        private int longAddress;


        public NextAddress(int _addr, int _bank)
        {
            SetAddress(_addr, _bank);
        }


        public void SetAddress(int _addr, int _bank)
        {
            if (_bank < 0 || _bank > 223)
            {
                throw new Exception("SetAddress Bank out of Range");
            }

            if (_addr < 0 || _addr > 65536)
            {
                throw new Exception("SetAddress Address out of Range");
            }

            addr = _addr & 0x1fff;
            bank = _bank;


            longAddress = MakeLongAddress(bank, addr);
        }

        public void SetAddressLong(int longaddr)
        {
            addr = longaddr & 0x1fff;
            bank = (longaddr&0x7fffe000) / 8192;

            longAddress = MakeLongAddress(bank, addr);
        }


        public int GetBank()
        {
            return bank;
        }

        public int GetAddr()
        {
            return addr;
        }


        public int GetLocalZ80Address(ref int[] banks)
        {
            int offset = 0;
            foreach (int b in banks)
            {
                if (b == bank)
                {
                    //found it
                    return addr + offset;
                }

                offset += 8192;
            }

            //bank is not in memory
            return -1;
        }

        public bool Equals(int _addr, int _bank)
        {
            return (addr == _addr && bank == _bank);
        }

        public string ToString(string f="")
        {
            if (f == "b")
            {
                int v = GetLocalZ80Address(ref MainForm.banks);
                if (v >= 0)
                {
                    return "<"+bank.ToString("X2") + ":" + v.ToString("X4")+">";
                }


            }

            
            return bank.ToString("X2") + ":" + addr.ToString("X4");
        }




        public int GetLongAddress()
        {
            return longAddress;
        }

        public static int MakeLongAddress(int bank,int addr)
        {
            return (bank * 8192) + (addr&0x1fff);
        }


        // -------------------------------------------------------------------------------------------------
        // Gets bank from address
        // Given a address 0-64k returns the paged in bank number
        //
        // \param [in,out]  banks   The banks.
        // \param           addr    The address.
        //
        // \return  The bank from address.
        // -------------------------------------------------------------------------------------------------
        public static int GetBankFromAddress(ref int[] banks,int addr)
        {
            int b = (addr & 0xe000) >> 13;

            Debug.Assert(b>=0 & b<=7);

            return (banks[b]);
        }





    }
}
