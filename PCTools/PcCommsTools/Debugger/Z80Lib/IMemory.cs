namespace Z80EmuLib
{
	/// <summary>
	/// Interface for memory access of the Z80 CPU
	/// </summary>
	public interface IMemory
	{
		void SetCPU(Z80Emu in_cpu);

		byte Read(ushort in_address, bool in_m1_state = false);
		void Write(ushort in_address, byte in_value);
	}
}
