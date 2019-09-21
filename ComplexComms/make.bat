@echo off
echo Make complex uart
echo.

@del uart.nex


@sjasmplus.exe --zxnext=cspect --sym=uart.sym complexuart.asm --msg=war --fullpath --nologo
echo.




