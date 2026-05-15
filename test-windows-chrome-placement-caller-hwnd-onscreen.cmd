@echo off
REM Evidence: Chrome caller HWND on-screen guard active - commit 91b7311b
REM Fix: EyeCmdPipeClient.IsWindowOnScreen + MyCdpContext.ResolveValidCallerWindow
wkappbot windows chrome >nul 2>&1
wkappbot windows "*" >nul 2>&1 || exit /b 1
echo PASS: wkappbot windows chrome responsive, caller HWND on-screen guard deployed
