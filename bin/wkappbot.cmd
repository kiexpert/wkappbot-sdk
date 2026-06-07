@echo off
setlocal

rem wkappbot.cmd -- bootstrap fallback: auto-builds wkappbot.exe when missing.
rem Lives in wkappbot-sdk/bin/ (committed). Windows prefers .exe over .cmd,
rem so this only fires when wkappbot.exe is absent (first clone / clean env).

set THIS_DIR=%~dp0
if "%THIS_DIR:~-1%"=="\" set THIS_DIR=%THIS_DIR:~0,-1%

set WKEXE=%THIS_DIR%\wkappbot.exe
set WKCORE=%THIS_DIR%\wkappbot-core.exe

if not exist "%WKEXE%" goto :rebuild
if not exist "%WKCORE%" goto :warn_core
goto :run

:rebuild
rem Delegate the USER-SIDE bootstrap build to wkdoctor -Build. wkdoctor owns the
rem build AND the lock-FREE single-flight guard (process-list election), so many
rem concurrent wkappbot.cmd fallbacks (PATH resolves this .cmd only while wkappbot.exe
rem is absent) collapse to exactly ONE launcher build instead of a build storm.
echo [wkappbot] wkappbot.exe missing -- delegating build to wkdoctor -Build...
call "%THIS_DIR%\wkdoctor.cmd" -Build
set WKBUILD_RC=%ERRORLEVEL%
if not exist "%WKEXE%" (
    if "%WKBUILD_RC%"=="2" (
        echo [wkappbot] launcher build already in progress -- retry shortly.
    ) else (
        echo [wkappbot] launcher build did not produce wkappbot.exe ^(rc=%WKBUILD_RC%^)
    )
    exit /b 1
)
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
