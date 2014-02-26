echo off
del *.ipch /s
del *.sdf /s
del *.ncb /s
del *.pdb /s
del *.ilk /s
del *.exe* /s
del *.exe* /s /a h
del *.user /s /a h
del *.suo /s /a h
del *.tlog /s
del *.log /s
del *.manifest* /s
del *_manifest* /s
del *.idb /s
del *.cache /s
del *.lastbuildstate /s
del *.cache /s
del BuildLog.htm /s
del *.obj /s
del *.filters /s /a h
del *.pch /s

echo Xoa thu muc rong
for /f "delims=" %%d in ('dir /s /b /ad ^| sort /r') do rd "%%d"
pause