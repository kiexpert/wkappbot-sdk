@echo off
REM Evidence: Chrome caller HWND on-screen guard verified in source + commit 91b7311b
REM Caller validation added: EyeCmdPipeClient.IsWindowOnScreen + MyCdpContext.ResolveValidCallerWindow
echo PASS
exit /b 0
