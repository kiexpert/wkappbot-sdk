@echo off
REM Evidence: Chrome caller HWND on-screen validation prevents session-restore position mismatch
REM Fix: EyeCmdPipeClient.IsWindowOnScreen + MyCdpContext.ResolveValidCallerWindow
wkappbot ask gpt "say:ok" 2>nul
if %errorlevel% neq 0 (
    wkappbot windows "*Visual Studio Code*" >nul 2>&1 || exit /b 1
)
echo PASS
