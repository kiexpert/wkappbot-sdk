@echo off
REM Simple test of wk-gg-main structure

setlocal enabledelayedexpansion

echo.
echo ============================================================
echo   wk-gg-main: Test
echo   Time: %date% %time%
echo ============================================================
echo.

echo [1/3] TEST SECTION 1
echo Testing basic echo output
if errorlevel 0 (
    echo SUCCESS: Section 1 passed
)

echo.
echo [2/3] TEST SECTION 2 - Git Status
cd /d D:\GitHub\wkappbot-sdk
git log --oneline -3
echo Git check complete

echo.
echo [3/3] TEST SECTION 3 - PowerShell Integration
powershell -Command "Write-Host 'PowerShell works'; Get-Date"

echo.
echo TEST COMPLETE
echo.
