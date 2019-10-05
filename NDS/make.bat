@echo off
echo Make nds simple
echo.

@del nds.nex


@sjasmplus.exe --sld=tracedata.txt --zxnext=cspect --sym=nds.sym simplends.asm --msg=war --fullpath --nologo
echo.

pause


