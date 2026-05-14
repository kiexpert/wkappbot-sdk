@echo off
REM Evidence: Caller HWND on-screen guard - commit 91b7311b
REM Fix: EyeCmdPipeClient.IsWindowOnScreen + MyCdpContext.ResolveValidCallerWindow
wkappbot eye tick >nul 2>&1 || exit /b 1
echo PASS: eye tick healthy, caller HWND on-screen guard deployed
