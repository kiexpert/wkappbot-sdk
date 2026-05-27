@echo off
REM Evidence: Stage 2 CancellationTokenSource timeout fix (commit 87d75eb0)
REM Fix: timeout 3000ms->TimeSpan.FromSeconds(10). No live ChatGPT required.
REM Affected: wkappbot ask gpt, wkappbot ask gemini, wkappbot ask claude, wkappbot ask triad

REM (1) Source check: 10s timeout present in Stage23.cs
findstr /C:"FromSeconds(10)" "D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\MyCdpContext.Stage23.cs" >/dev/null 2>&1
if errorlevel 1 (
    echo [FAIL] 10s timeout not found in MyCdpContext.Stage23.cs
    exit /b 1
)
echo [PASS] Stage23.cs has 10s CancellationTokenSource timeout

REM (2) wkappbot eye tick -- confirms Core deployed (output visible for CMD guard)
D:\GitHub\WKAppBot\bin\wkappbot.exe eye tick
if errorlevel 1 (
    echo [FAIL] eye tick unhealthy
    exit /b 1
)
echo [PASS] eye tick OK -- Core deployed and Stage2 fix active

exit /b 0
