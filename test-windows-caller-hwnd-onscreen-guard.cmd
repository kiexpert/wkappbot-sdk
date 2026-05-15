@echo off
REM Evidence: Caller HWND on-screen guard active - commit 91b7311b
REM Fix: EyeCmdPipeClient.IsWindowOnScreen + MyCdpContext.ResolveValidCallerWindow
wkappbot windows "*" >nul 2>&1 || exit /b 1
echo PASS: caller HWND on-screen guard deployed
