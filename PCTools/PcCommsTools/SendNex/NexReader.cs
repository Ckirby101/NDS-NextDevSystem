using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendNex
{
    class NexReader
    {
        private BinaryReader Reader;

        public class Header
        {
            public string Next; // = new byte[4];			//"Next"
            public string VersionNumber; // = new byte[4];	//"V1.1" = Gold distro. V1.2 allows entering with PC in a 16K bank >= 8.
            public byte RAM_Required;		//0=768K, 1=1792K
            public byte NumBanksToLoad;	//0-112 x 16K banks
            public byte LoadingScreen;	//1 = layer2 at 16K page 9, 2=ULA loading, 4=LORES, 8=HiRes, 16=HIColour, +128 = don't load palette.
            public byte BorderColour;		//0-7 ld a,BorderColour:out(254),a

            public UInt16 SP; // = new byte[2];				//Stack Pointer
            public UInt16 PC; // = new byte[2];;				//Code Entry Point : $0000 = Don't run just load.
            public UInt16 NumExtraFiles; // = new byte[2];;	//NumExtraFiles

            public byte[] Banks; // = new byte[64 + 48];		//Which 16K Banks load.	: Bank 5 = $0000-$3fff, Bank 2 = $4000-$7fff, Bank 0 = $c000-$ffff
            public byte loadingBar;		//Loading bar off=0/on=1
            public byte loadingColour;	//Loading bar Layer2 index colour
            public byte loadingBankDelay;	//Delay after each bank
            public byte loadedDelay;		//Delay (frames) after loading before running
            public byte dontResetRegs;	//Don't reset the registers

            public byte[] CoreRequired; // = new byte[3];	//CoreRequired byte per value, decimal, not string. ordering... Major, Minor, Subminor
            public byte HiResColours;		//to be anded with three, and shifted left three times, and add the mode number for hires and out (255),a
            public byte EntryBank;		//V1.2: 0-112, this 16K bank will be paged in at $C000 before jumping to PC. The default is 0, which is the default upper 16K bank anyway.
            public byte[] RestOf512Bytes; // = new byte[512 - (4 + 4 + 1 + 1 + 1 + 1 + 2 + 2 + 2 + 64 + 48 + 1 + 1 + 1 + 1 + 1 + 3 + 1 + 1)];
        }


        public NexReader(BinaryReader reader)
        {
            Reader=reader;

            Console.WriteLine("Data length "+Reader.BaseStream.Length);

        }


        public Header ReadHeader()
        {


            Header header=new Header();
            header.Next=System.Text.Encoding.UTF8.GetString(Reader.ReadBytes(4));
            header.VersionNumber=System.Text.Encoding.UTF8.GetString(Reader.ReadBytes(4));

            header.RAM_Required = Reader.ReadByte();
            header.NumBanksToLoad = Reader.ReadByte();
            header.LoadingScreen = Reader.ReadByte();
            header.BorderColour = Reader.ReadByte();

            header.SP = ReadUInt16BE(Reader);
            header.PC = ReadUInt16BE(Reader);
            header.NumExtraFiles = Reader.ReadUInt16();


            header.Banks = Reader.ReadBytes(64 + 48);
            header.loadingBar = Reader.ReadByte();
            header.loadingColour = Reader.ReadByte();
            header.loadingBankDelay = Reader.ReadByte();
            header.loadedDelay = Reader.ReadByte();
            header.dontResetRegs = Reader.ReadByte();

            header.CoreRequired = Reader.ReadBytes(3);
            header.HiResColours = Reader.ReadByte();
            header.EntryBank = Reader.ReadByte();
            header.RestOf512Bytes = Reader.ReadBytes(370+2);


            if (header.LoadingScreen != 0)
            {
                long bytesleft = Reader.BaseStream.Length - Reader.BaseStream.Position;

                long need = header.NumBanksToLoad * 16384;

                Console.WriteLine("Skipping: "+(bytesleft-need));
                
                Reader.BaseStream.Position += (bytesleft-need);



                //skip optional palette (for Layer2 or LoRes screen)
                //Reader.BaseStream.Position += 512;
                //skip Layer2 loading screen
                //Reader.BaseStream.Position += 49152;
            }


            /*
            header.field1=Reader.ReadInt32();
            header.field2=Reader.ReadInt32();
            header.records=new Record[15];
            for(int i=0;i<block.records.Length;i++)
                block.records[i]=ReadRecord();
            block.filler1=Reader.ReadChars(24);

    */
            return header;
        }



        public static UInt16 ReadUInt16BE( BinaryReader binRdr)
        {
            UInt16 v = binRdr.ReadUInt16();

            //UInt16 o = (UInt16) ((((int)v & 0xff) << 8) | (((int)v & 0xff00) >> 8));

            return v;
        }


        public void AddData(ref List<byte> b,int length)
        {

            long bytesleft = Reader.BaseStream.Length - Reader.BaseStream.Position;

            Console.WriteLine("Add Data , bytes left "+bytesleft);

            // Reader.BaseStream.Length

            for (int i = 0; i < length; i++)
            {
                if (Reader.BaseStream.Position<Reader.BaseStream.Length)
                    b.Add( Reader.ReadByte());
            }

        }

    }
}
