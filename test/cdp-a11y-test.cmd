@echo off
echo [DEPRECATED] cdp-a11y-test.cmd is deprecated. Forwarding to cdp-a11y-test.ps1...
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0cdp-a11y-test.ps1" %*
exit /b %ERRORLEVEL%
