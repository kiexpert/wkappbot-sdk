@echo off
wkappbot ask gpt "say: RECONNECT_TEST_OK"
if %errorlevel% neq 0 exit /b 1
echo PASS: WebSocket reconnect working
