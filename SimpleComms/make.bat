@echo off
echo Make uart
echo.

@del uart.nex


@sjasmplus.exe --zxnext=cspect --sym=uart.sym uart.asm --msg=war --fullpath --nologo
echo.




