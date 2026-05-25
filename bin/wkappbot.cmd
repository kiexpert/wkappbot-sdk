@echo off
setlocal

rem wkappbot.cmd -- bootstrap fallback: auto-builds wkappbot.exe when missing.
rem Lives in wkappbot-sdk/bin/ (committed). Windows prefers .exe over .cmd,
rem so this only fires when wkappbot.exe is absent (first clone / clean env).

set THIS_DIR=%~dp0
if "%THIS_DIR:~-1%"=="\" set THIS_DIR=%THIS_DIR:~0,-1%

set WKEXE=%THIS_DIR%\wkappbot.exe
set WKCORE=%THIS_DIR%\wkappbot-core.exe
set LAUNCHER_PROJ=%THIS_DIR%\..\csharp\src\WKAppBot.Launcher\WKAppBot.Launcher.csproj

if not exist "%WKEXE%" goto :rebuild
if not exist "%WKCORE%" goto :warn_core
goto :run

:rebuild
echo [wkappbot] wkappbot.exe missing -- building launcher...
if not exist "%LAUNCHER_PROJ%" (
    echo [wkappbot] ERROR: Launcher project not found.
    echo             Expected: %LAUNCHER_PROJ%
    exit /b 1
)
"C:\Program Files\dotnet\dotnet.exe" publish "%LAUNCHER_PROJ%" -c Release --verbosity minimal
if errorlevel 1 (
    echo [wkappbot] Build FAILED
    exit /b 1
)
echo [wkappbot] Build OK -- wkappbot.exe deployed
if not exist "%WKCORE%" goto :warn_core
goto :run

:warn_core
echo [wkappbot] WARNING: wkappbot-core.exe missing.
echo             Build or copy core from the WKAppBot core repo.
echo             Launcher-only commands work, core-dependent commands will fail.

:run
if not exist "%WKEXE%" (
    echo [wkappbot] ERROR: wkappbot.exe still missing after build attempt
    exit /b 1
)
"%WKEXE%" %*
exit /b %ERRORLEVEL%
