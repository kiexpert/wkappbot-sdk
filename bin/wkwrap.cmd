@echo off
C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0wkdoctor.ps1" %*
exit /b %ERRORLEVEL%
echo Done. !n! wrappers created.& goto :eof
:status
for %%f in ("%DIR%wk*.ps1") do if /i not "%%~nf"=="wkwrap" (
    set s=--& set c=--
    if exist "%DIR%%%~nf.sh"  set s=OK
    if exist "%DIR%%%~nf.cmd" set c=OK
    echo   %%~nf  .sh:!s!  .cmd:!c!
)
