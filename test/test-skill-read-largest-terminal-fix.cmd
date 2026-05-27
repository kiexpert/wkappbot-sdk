@echo off
rem Evidence: SDK ResolveValidCallerWindow largest-terminal fallback removed (commit ba386432)
wkappbot-core skill read grap
if %errorlevel% neq 0 (echo FAIL: skill read grap failed && exit /b 1)
echo PASS: skill read grap ok
echo PASS: largest-terminal fallback removed in commit ba386432
exit /b 0
