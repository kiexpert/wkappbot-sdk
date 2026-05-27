@echo off
setlocal enabledelayedexpansion
:: wkwrap.cmd -- universal CMD->PS1 relay + auto-install (harness-managed)
set "SELF=%~n0"& set "DIR=%~dp0"
if /i not "%SELF%"=="wkwrap" goto :relay
set "ARG=%~1"
if "%ARG%"=="--install" goto :install
if "%ARG%"=="--status"  goto :status
echo wkwrap.cmd --install ^| --status ^| (called as wkXXX.cmd -^> relay to wkXXX.ps1)
echo bash/Claude: wkXXX.sh  ^|  CMD/user: wkXXX.cmd  ^|  PS direct: wkXXX.ps1
goto :eof
:relay
powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "%DIR%%SELF%.ps1" %*
exit /b %ERRORLEVEL%
:install
set /a n=0
for %%f in ("%DIR%wk*.ps1") do if /i not "%%~nf"=="wkwrap" (
    if not exist "%DIR%%%~nf.sh"  ( copy "%DIR%wkwrap.sh"  "%DIR%%%~nf.sh"  >/dev/null & echo   [sh]  %%~nf.sh  & set /a n+=1 )
    if not exist "%DIR%%%~nf.cmd" ( copy "%DIR%wkwrap.cmd" "%DIR%%%~nf.cmd" >/dev/null & echo   [cmd] %%~nf.cmd & set /a n+=1 )
)
echo Done. !n! wrappers created.& goto :eof
:status
for %%f in ("%DIR%wk*.ps1") do if /i not "%%~nf"=="wkwrap" (
    set s=--& set c=--
    if exist "%DIR%%%~nf.sh"  set s=OK
    if exist "%DIR%%%~nf.cmd" set c=OK
    echo   %%~nf  .sh:!s!  .cmd:!c!
)