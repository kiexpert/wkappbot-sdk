@echo off
reg query HKCU\Environment /v WKAPPBOT_NO_SCREENSAVER 2>&1 > D:\GitHub\wkappbot-sdk\.bak\env-result.txt
echo exitcode=%errorlevel% >> D:\GitHub\wkappbot-sdk\.bak\env-result.txt