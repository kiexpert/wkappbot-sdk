@echo off
C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0wkdoctor.ps1" %*
exit /b %ERRORLEVEL%
