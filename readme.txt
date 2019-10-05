NDS - Next Development System


Gettings started:

Open SimpleComms folder and edit Nex.asm, set the required baud rate at the top of the file. Assemble using make.bat
copy NDS.nex to spectrum next and execute, you shoudl see a scrolling TBBlue screen. NDS is now waiting for commands.

You can use SendNex command line tool to transmit a Nex file to the Target and execute it.
You can use SendMemeory command line too to put data into Nex memeory as required.

RemoteDebugger allows you to debug the target hardware real time.  If you compile with my Special version of SJASMPLUS or my version of PASMO you can even source level debug.

RemoteDebugger supports:

Disassembly
reg view and edit
Next Registers view
Breakpoints
Watch windows
memory View
Future PC (using built in z80 emulator)
callstack
Single stepping
Transmitting of nex file


System has been tested at 2mpbs and is stable.

Right now i need to overlay my NDS mmu bank over rom, but this will be removed later as i use the DivMMC automapping feature.
