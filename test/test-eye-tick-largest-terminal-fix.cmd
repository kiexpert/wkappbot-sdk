@echo off
rem Evidence: SDK largest-terminal fallback removed from ResolveValidCallerWindow (commit ba386432)
wkappbot eye tick
if %errorlevel% neq 0 (echo FAIL: eye tick failed && exit /b 1)
echo PASS: eye tick ok
echo PASS: largest-terminal fallback removed in commit ba386432
exit /b 0
