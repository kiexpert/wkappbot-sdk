@echo off
setlocal enabledelayedexpansion
powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass ^
    -File "%~dp0wkprompt-caller.ps1" %*
exit /b !errorlevel!
