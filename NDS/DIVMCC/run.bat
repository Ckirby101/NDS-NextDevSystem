echo NDS test

@TSKILL CSPect 2> nul
@"C:\Users\ckirb\Documents\Spectrum Next\CSPECT\CSpect.exe" -tv -60 -vsync -16bit -joy -s14 -map=nds.map -w4 -brk -zxnext -mmc=.\ NDS.nex 2> nul
