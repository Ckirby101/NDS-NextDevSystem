echo complex uart run

@TSKILL CSPect 2> nul
@"C:\Users\ckirb\Documents\Spectrum Next\CSPECT\CSpect.exe" -tv -60 -vsync -16bit -joy -s14 -map=uart.map -w4 -brk -zxnext -mmc=.\ uart.nex
