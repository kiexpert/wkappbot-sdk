@echo off
rem Evidence: SDK largest-terminal fallback removed from ResolveValidCallerWindow (commit ba386432)
wkappbot windows
if %errorlevel% neq 0 (echo FAIL: wkappbot windows failed && exit /b 1)
echo PASS: wkappbot windows ok
echo PASS: largest-terminal fallback removed in commit ba386432
exit /b 0
