using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

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


        // -------------------------------------------------------------------------------------------------
        // Constructor
        //
        // \param   reader
        // The reader.
        // -------------------------------------------------------------------------------------------------
        public NexReader(BinaryReader reader)
        {
            Reader=reader;

            Console.WriteLine("Data length "+Reader.BaseStream.Length);

        }


        // -------------------------------------------------------------------------------------------------
        // Reads the header
        //
        // \return  The header.
        // -------------------------------------------------------------------------------------------------
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

            }

            return header;
        }



        // -------------------------------------------------------------------------------------------------
        // Reads u int 16 be
        //
        // \param   binRdr
        // The bin reader.
        //
        // \return  The u int 16 be.
        // -------------------------------------------------------------------------------------------------
        public static UInt16 ReadUInt16BE( BinaryReader binRdr)
        {
            UInt16 v = binRdr.ReadUInt16();


            return v;
        }


        // -------------------------------------------------------------------------------------------------
        // Adds a data to 'length'
        //
        // \param [in,out]  b
        // A List&lt;byte&gt; to process.
        // \param           length
        // The length.
        //
        // \return  A byte.
        // -------------------------------------------------------------------------------------------------
        public byte AddData(ref List<byte> b,int length)
        {
            byte checksum = 0;

            long bytesleft = Reader.BaseStream.Length - Reader.BaseStream.Position;


            for (int i = 0; i < length; i++)
            {
                if (Reader.BaseStream.Position < Reader.BaseStream.Length)
                {
                    byte data = Reader.ReadByte();
                    b.Add( data );
                    checksum ^= data;//(byte)(checksum ^ data);
                }
            }

            Console.WriteLine("Add Data , bytes left "+bytesleft + " checksum "+checksum.ToString("X2"));

            return checksum;
        }


        public static bool SendNext(SerialPort mySerialPort, string file)
        {
            using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open)))
            {

                List<byte> b = new List<byte>();

                NexReader nex= new NexReader(br);
                NexReader.Header header = nex.ReadHeader();

                Console.WriteLine("Opening nex file = "+header.Next+" "+header.VersionNumber);


                Console.WriteLine("PC Address : 0x"+header.PC.ToString("X"));
                Console.WriteLine("SP Address : 0x"+header.SP.ToString("X"));





                AddCommand(ref b,186);
                SendData(mySerialPort,ref b);

                Thread.Sleep(50);

                for (int i = 0; i < header.Banks.Length; i++)
                {
                    int bank = GetRealBank(i);


                    
                    if (header.Banks[bank] != 0)
                    {
                        bank *= 2;

                        for (int j = 0; j < 2; j++)
                        {
                            Console.WriteLine("Sending 8k mmu bank : "+bank);

                            //AddCommand(ref b,180);
                            //Add8Value(ref b,7);
                            //Add8Value(ref b,bank);
                            //SendData(ref b);

                            AddCommand(ref b,181);
                            Add8Value(ref b,bank);

                            Add16Value(ref b,0xE000);
                            Add16Value(ref b,8192);

                            byte checksum = nex.AddData(ref b, 8192);

                            SendData(mySerialPort,ref b);
                            //int cs = ReadByteData();
                            int ok = ReadByteData(mySerialPort);

                            int clo = ReadByteData(mySerialPort);
                            int chi = ReadByteData(mySerialPort);

                            //if (cs != checksum)
                            //{
                            //    Console.WriteLine("Checksum Error "+checksum+" != "+cs);
                            //    throw new Exception();
                            //}

                            if (clo != 0 || chi != 0)
                            {
                                Console.WriteLine("Send Error ");
                                throw new Exception();
                            }
                            if (ok != 0)
                            {
                                Console.WriteLine("Timeout Error!");
                                throw new Exception();
                            }





                            bank++;
                        }


                    }

                }


                Console.WriteLine("Setting entry bank to "+header.EntryBank);

                AddCommand(ref b,180);
                Add8Value(ref b,6);
                Add8Value(ref b,header.EntryBank);
                //SendData(ref b);
                AddCommand(ref b,180);
                Add8Value(ref b,7);
                Add8Value(ref b,header.EntryBank+1);
                SendData(mySerialPort,ref b);



                AddCommand(ref b, 182); //execute!
                Add16Value(ref b,header.SP);
                Add16Value(ref b,header.PC);
                SendData(mySerialPort,ref b);

                Console.WriteLine("Finished!");
            }

            return true;
        }

         // -------------------------------------------------------------------------------------------------
        // Gets real bank
        //
        // \param   index   Zero-based index of the.
        //
        // \return  The real bank.
        // -------------------------------------------------------------------------------------------------
        private static int GetRealBank(int index)
        {
            int[] banks = {5, 2, 0, 1, 3, 4, 6, 7};
            if (index > 7) return index;
            return (banks[index]);

        }


        // -------------------------------------------------------------------------------------------------
        // Adds a command to 'num'
        //
        // \param [in,out]  b   A List&lt;byte&gt; to process.
        // \param           num Number of.
        // -------------------------------------------------------------------------------------------------
        private static void AddCommand(ref List<byte> b, int num)
        {
            b.Add((byte)'C');
            b.Add((byte)'M');
            b.Add((byte)'D');
            b.Add((byte)num);

        }

        // -------------------------------------------------------------------------------------------------
        // Adds a 16 value to 'value'
        //
        // \param [in,out]  b       A List&lt;byte&gt; to process.
        // \param           value   The value.
        // -------------------------------------------------------------------------------------------------
        private static void Add16Value(ref List<byte> b, int value)
        {
            b.Add((byte)(value&0xff));          //lo
            b.Add((byte)((value&0xff00)>>8) );     //hi
        }
        // -------------------------------------------------------------------------------------------------
        // Adds a 8 value to 'value'
        //
        // \param [in,out]  b       A List&lt;byte&gt; to process.
        // \param           value   The value.
        // -------------------------------------------------------------------------------------------------
        private static void Add8Value(ref List<byte> b, int value)
        {
            b.Add((byte)value);          //lo
        }

        // -------------------------------------------------------------------------------------------------
        // Sends a data
        //
        // \param [in,out]  b   A List&lt;byte&gt; to process.
        // -------------------------------------------------------------------------------------------------
        private static void SendData(SerialPort port,ref List<byte> b)
        {
            int buffersize = 256;

            //make sure buffer is empty
            while (port.BytesToWrite>0)
                Thread.Sleep(10);

            byte[] data = b.ToArray();

            int length = data.Length;

            int index = 0;
            while (length > 0)
            {
                int writelength = length;
                if (writelength > buffersize) writelength = buffersize;

                port.Write(data, index, writelength);
                index += writelength;
                length -= writelength;

                do
                {
                    Thread.Sleep(3);
                    
                } while (port.BytesToWrite>0);



            }

            b.Clear();
        }


        private static byte ReadByteData(SerialPort port)
        {
            while (port.BytesToRead <=0)
                Thread.Sleep(10);

            return (byte)port.ReadByte();
        }


    }
}
