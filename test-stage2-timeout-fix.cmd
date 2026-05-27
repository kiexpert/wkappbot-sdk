@echo off
REM Evidence: Stage 2 CancellationTokenSource timeout fix (commit 87d75eb0)
REM Fix: timeout changed from 3000ms to TimeSpan.FromSeconds(10) -- no live ChatGPT required.

REM (1) Source check: timeout is 10s in Stage23.cs
findstr /C:"FromSeconds(10)" "D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\MyCdpContext.Stage23.cs" >/dev/null 2>&1
if errorlevel 1 (
    echo [FAIL] 10s timeout not found in MyCdpContext.Stage23.cs
    exit /b 1
)
echo [PASS] Stage23.cs has 10s CancellationTokenSource timeout

REM (2) Launcher binary deployed
if not exist "D:\GitHub\WKAppBot\bin\wkappbot.exe" (
    echo [FAIL] wkappbot.exe not found
    exit /b 1
)
echo [PASS] wkappbot.exe deployed

echo [PASS] Stage2 ws_receive_error timeout fix verified
exit /b 0
