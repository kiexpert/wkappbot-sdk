@echo off
REM Evidence: 'session' added to CommandHelpMap -- commit 72706c8cc
wkappbot session list --claude >nul 2>&1 || exit /b 1
echo PASS: session command registered in CommandHelpMap
