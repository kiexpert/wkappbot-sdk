@echo off
:: wkhippo.cmd -- standalone CMD relay that invokes the SDK-local wkhippo.ps1 brain.
:: Vendored into wkappbot-sdk/bin: routes directly to the sibling wkhippo.ps1
:: (no wkwrap resolver needed -- this is a standalone copy, mirrors wkhippo.sh).
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0wkhippo.ps1" %*
exit /b %ERRORLEVEL%