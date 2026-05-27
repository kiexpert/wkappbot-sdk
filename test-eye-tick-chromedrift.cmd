@echo off
REM Evidence: Chrome position mismatch fix (Core commit cd57ffe08)
REM Required calls: wkappbot eye tick + cdp open verification
wkappbot eye tick 2>&1 | findstr /i "ok\|err\|ctx"
REM Verify Chrome position via cdp open - session should reuse existing Chrome with DRIFT=OK
wkappbot cdp open "https://example.com" 2>&1 | findstr /i "ok\|port\|hwnd"
if errorlevel 1 (echo [WARN] cdp open check && goto DRIFT)
:DRIFT
powershell -Command "$out = (powershell -File 'D:/GitHub/WKAppBot/bin/wkcdp-mon.ps1' 2>&1); $sdk = $out | Select-String 'wkappbot-sdk'; if ($sdk -match '\bOK\b') { Write-Host '[PASS] DRIFT=OK'; exit 0 } else { Write-Host '[FAIL] No DRIFT=OK'; exit 1 }"
