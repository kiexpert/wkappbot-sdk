@echo off
REM Evidence: cdp open exit 255 LAUNCH JSON only PseudoConsoleWindow chain=[pid:8]
REM Fix: cdp open Eye timeout 9s->90s (Program.cs eyeTimeoutMs typo)
echo [TEST] cdp open must return OK with cdp: port
wkappbot eye tick
wkappbot cdp open https://example.com 2>&1 | findstr /C:"cdp:"
if %ERRORLEVEL%==0 (echo [PASS] cdp open returned cdp: port) else (echo [FAIL] no cdp: in output & exit 1)
wkappbot windows {proc:'chrome'}
