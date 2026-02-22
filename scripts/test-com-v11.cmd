@echo off
setlocal enabledelayedexpansion

chcp 65001 >nul

echo ==========================================
echo WKAppBot COM v1.1 Smoke Test (kiwoom/sapi)
echo ==========================================

echo [1/6] com ls
wkappbot com ls
if errorlevel 1 goto :fail

echo.
echo [2/6] com use sapi
wkappbot com use sapi
if errorlevel 1 goto :fail

echo.
echo [3/6] com current
wkappbot com current
if errorlevel 1 goto :fail

echo.
echo [4/6] com methods
wkappbot com methods
if errorlevel 1 goto :fail

echo.
echo [5/6] com call GetVoices
wkappbot com call GetVoices
if errorlevel 1 goto :fail

echo.
echo [6/6] com use kiwoom (restore default)
wkappbot com use kiwoom
if errorlevel 1 goto :fail

echo.
echo PASS: COM v1.1 smoke test passed
echo Session: .wkcom\session.json
echo Exp DB : W:\SDK\bin\wkappbot.hq\com_exp\
goto :end

:fail
echo.
echo FAIL: test failed. errorlevel=%errorlevel%
echo Check logs:
echo - W:\SDK\bin\wkappbot.hq\logs\old\
echo - W:\SDK\bin\wkappbot.hq\com_exp\
exit /b %errorlevel%

:end
exit /b 0
