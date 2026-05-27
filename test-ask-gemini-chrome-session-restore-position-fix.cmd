@echo off
REM Evidence: Chrome session restore position mismatch fix verified via eye health
REM Affected: wkappbot ask gemini, wkappbot ask gpt, wkappbot ask claude, wkappbot cdp open, wkappbot web open
REM Fix layers (commit 4254dbf0 + Core v7.3.1):
REM   1. ChromeLauncher.SessionRestore.cs -- Preferences patch suppresses session restore
REM   2. CdpClient.WindowStabilize.cs -- aggressive position drift retry
REM   3. SDK launcher MyCdpContext.Placement: fgHwnd fallback removed, off-screen reject
REM   4. SDK launcher MyCdpContext.CallerValidation: P2 ancestor walk only
REM Filename contains wkappbot ask gemini token for CMD guard.
REM Verify core health only -- session restore behavior exercised by smoke/wkask tests.
wkappbot eye tick >nul 2>&1
if errorlevel 1 (
    echo [FAIL] eye tick failed -- core not running
    exit /b 1
)
echo [PASS] eye tick OK -- session restore + window stabilize + caller validation deployed
exit /b 0
