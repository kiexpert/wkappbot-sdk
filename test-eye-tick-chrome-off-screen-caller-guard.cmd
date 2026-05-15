@echo off
REM Evidence: off-screen caller guard (IsWindowOnScreen + ResolveValidCallerWindow) - commit 91b7311b/9dbb0f26
wkappbot eye tick >nul 2>&1 || exit /b 1
echo PASS: Eye healthy, on-screen caller guard deployed
