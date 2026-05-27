@echo off
reg delete HKCU\Environment /v WKAPPBOT_NO_SCREENSAVER /f
echo DONE
reg query HKCU\Environment /v WKAPPBOT_NO_SCREENSAVER 2>&1