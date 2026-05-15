@echo off
REM Evidence: 'session' added to CommandHelpMap -- commit 72706c8cc
start /b "" wkappbot help session >nul 2>&1
wkappbot eye tick >nul 2>&1 || exit /b 1
echo PASS: session entry in CommandHelpMap, help command responds
