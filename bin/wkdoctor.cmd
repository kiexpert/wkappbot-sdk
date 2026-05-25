@echo off
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0wkdoctor.ps1" %*
exit /b %ERRORLEVEL%
