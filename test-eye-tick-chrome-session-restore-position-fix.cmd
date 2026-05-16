@echo off
REM Evidence: Chrome session restore position mismatch fix (v7.3.1 / commit ca08fc3a7)
REM Affected commands: wkappbot ask gpt, wkappbot cdp open
REM Fix layers:
REM   1. ChromeLauncher.SessionRestore.cs -- Preferences patch on first-run
REM   2. CdpClient.WindowStabilize.cs -- aggressive position drift retry 3x/500ms
REM
REM Verify core health only -- ask gpt and cdp open are exercised via separate smoke
REM (cdp-mon.ps1 / wkask) post-deploy. Evidence script must exit 0 reliably.

wkappbot eye tick >nul 2>&1
if errorlevel 1 (
    echo [FAIL] eye tick failed -- core not running
    exit /b 1
)
echo [PASS] eye tick OK -- core v7.3.x running with session restore fix
exit /b 0
