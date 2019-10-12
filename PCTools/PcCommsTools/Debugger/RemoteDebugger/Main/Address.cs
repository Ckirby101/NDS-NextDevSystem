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

        private bool FixedValue = false;            //if true then its a vlaue and dont do anything odd with it!


        // -------------------------------------------------------------------------------------------------
        // Constructor
        //
        // \param   _addr
        // The address.
        // \param   _bank
        // The bank.
        // -------------------------------------------------------------------------------------------------
        public NextAddress(int _addr, int _bank)
        {
            SetAddress(_addr, _bank);
        }

        public NextAddress(int _longaddr)
        {
            int _addr = _longaddr & 0x1fff;
            int _bank = (_longaddr&0x7fffe000) / 8192;


            SetAddress(_addr, _bank);
        }


        // -------------------------------------------------------------------------------------------------
        // Query if this object is fixed value
        //
        // \return  True if fixed value, false if not.
        // -------------------------------------------------------------------------------------------------
        public bool isFixedValue()
        {
            return FixedValue == true;
        }

        // -------------------------------------------------------------------------------------------------
        // Sets the address
        //
        // \exception   Exception
        // Thrown when an exception error condition occurs.
        //
        // \param   _addr
        // The address.
        // \param   _bank
        // The bank.
        // -------------------------------------------------------------------------------------------------
        public void SetAddress(int _addr, int _bank)
        {
            if (_bank < 0)
            {
                FixedValue = true;
                addr = _addr;
                bank = -1;
                longAddress = addr;
                return;
            }

            if (_bank > 223 && _bank !=255)
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

        // -------------------------------------------------------------------------------------------------
        // Sets address long
        //
        // \param   longaddr
        // The longaddr.
        // -------------------------------------------------------------------------------------------------
        public void SetAddressLong(int longaddr)
        {
            if (FixedValue)
            {
                addr = longaddr;
                bank = -1;
                longAddress = addr;
                return;
            }

            addr = longaddr & 0x1fff;
            bank = (longaddr&0x7fffe000) / 8192;

            longAddress = MakeLongAddress(bank, addr);
        }


        // -------------------------------------------------------------------------------------------------
        // Gets the bank
        //
        // \return  The bank.
        // -------------------------------------------------------------------------------------------------
        public int GetBank()
        {
            return bank;
        }

        // -------------------------------------------------------------------------------------------------
        // Gets the address
        //
        // \return  The address.
        // -------------------------------------------------------------------------------------------------
        public int GetAddr()
        {
            return addr;
        }


        // -------------------------------------------------------------------------------------------------
        // Gets local z coordinate 80 address
        //
        // \param [in,out]  banks
        // The banks.
        //
        // \return  The local z coordinate 80 address.
        // -------------------------------------------------------------------------------------------------
        public int GetLocalZ80Address(ref int[] banks)
        {
            if (FixedValue)
                return addr;


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

        // -------------------------------------------------------------------------------------------------
        // Tests if two int objects are considered equal
        //
        // \param   _addr
        // Int to be compared.
        // \param   _bank
        // Int to be compared.
        //
        // \return  True if the objects are considered equal, false if they are not.
        // -------------------------------------------------------------------------------------------------
        public bool Equals(int _addr, int _bank)
        {
            return (addr == _addr && bank == _bank);
        }

        // -------------------------------------------------------------------------------------------------
        // Convert this object into a string representation
        //
        // \param   f
        // (Optional) The format string.
        //
        // \return  A string that represents this object.
        // -------------------------------------------------------------------------------------------------
        public string ToString(string f="")
        {
            if (FixedValue) return addr.ToString("X4");

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




        // -------------------------------------------------------------------------------------------------
        // Gets long address
        //
        // \return  The long address.
        // -------------------------------------------------------------------------------------------------
        public int GetLongAddress()
        {
            return longAddress;
        }

        // -------------------------------------------------------------------------------------------------
        // Makes long address
        //
        // \param   bank
        // The bank.
        // \param   addr
        // The address.
        //
        // \return  An int.
        // -------------------------------------------------------------------------------------------------
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
