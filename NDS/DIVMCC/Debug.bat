SET pvar=%cd%
ECHO %pvar% 

"..\PCTools\REmoteDebugger.exe" -trace="%pvar%\tracedata.txt"


pause