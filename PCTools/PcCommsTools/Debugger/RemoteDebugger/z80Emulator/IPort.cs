namespace Z80EmuLib
{
	public interface IPort
	{
		void SetCPU(Z80Emu in_cpu);

		byte Read(ushort addr);
		void Write(ushort addr, byte value);
	}
}
