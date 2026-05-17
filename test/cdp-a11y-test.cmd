@echo off
setlocal enabledelayedexpansion
REM WKAppBot CDP/A11y Integration Test v2
REM Uses: cdp open, cdp html, a11y inspect, a11y invoke+restore, a11y type, a11y read
REM Requires: wkappbot in PATH

set WK=wkappbot
set PAGE=file:///D:/GitHub/wkappbot-sdk/docs/test/index.html
set PASS=0
set FAIL=0
set TOTAL=6
set CHROME_HW=

echo =============================================
echo  WKAppBot CDP/A11y Integration Test v2
echo =============================================

REM -- T1: Open page -------------------------
echo.
echo [T1] cdp open test page (local)...
for /f "tokens=*" %%L in ('%WK% cdp open "%PAGE%" 2^>^&1') do (
    echo %%L | findstr /C:"hwnd:" >/dev/null && set LINE=%%L
)
if "!LINE!"=="" ( echo FAIL: cdp open & set /a FAIL+=1 & goto :done )
echo PASS: !LINE:~0,80!
set /a PASS+=1

REM Extract hwnd from OK line
for /f "tokens=2 delims=:" %%H in ("!LINE!") do (
    for /f "tokens=1 delims=," %%W in ("%%H") do set CHROME_HW=%%W
)
echo Detected hwnd: !CHROME_HW!

REM -- T2: cdp html - page loaded ------------
echo.
echo [T2] cdp html - verify test page loaded...
for /f "tokens=*" %%L in ('%WK% cdp html "!CHROME_HW!" 2^>^&1') do (
    echo %%L | findstr /C:"WKAppBot CDP" >/dev/null && goto :t2pass
)
echo FAIL: page title not found
set /a FAIL+=1
goto :t3
:t2pass
echo PASS: WKAppBot CDP/A11y Test Page loaded
set /a PASS+=1

REM -- T3: a11y inspect - find button --------
:t3
echo.
echo [T3] a11y inspect - find btn-primary...
for /f "tokens=*" %%L in ('%WK% a11y inspect "!CHROME_HW!" 2^>^&1') do (
    echo %%L | findstr /C:"btn-primary" >/dev/null && goto :t3pass
)
echo FAIL: btn-primary not found in a11y tree
set /a FAIL+=1
goto :t4
:t3pass
echo PASS: btn-primary found with Invoke pattern
set /a PASS+=1

REM -- T4: a11y restore + invoke -------------
:t4
echo.
echo [T4] a11y restore + invoke btn-primary...
%WK% a11y restore "!CHROME_HW!" >/dev/null 2>&1
timeout /t 2 /nobreak >/dev/null
%WK% a11y invoke "btn-primary#!CHROME_HW!" >/dev/null 2>&1
timeout /t 2 /nobreak >/dev/null
REM Verify via cdp html
for /f "tokens=*" %%L in ('%WK% cdp html "!CHROME_HW!" 2^>^&1') do (
    echo %%L | findstr /C:"clicked:" >/dev/null && goto :t4pass
    echo %%L | findstr /C:"count=1" >/dev/null && goto :t4pass
)
echo WARN: invoke fired but DOM not updated (JS onclick may need focus)
set /a FAIL+=1
goto :t5
:t4pass
echo PASS: button click confirmed in DOM
set /a PASS+=1

REM -- T5: a11y type -------------------------
:t5
echo.
echo [T5] a11y type into input-text...
%WK% a11y type "input-text#!CHROME_HW!" "hello wkappbot" >/dev/null 2>&1
timeout /t 1 /nobreak >/dev/null
for /f "tokens=*" %%L in ('%WK% cdp html "!CHROME_HW!" 2^>^&1') do (
    echo %%L | findstr /C:"hello wkappbot" >/dev/null && goto :t5pass
)
echo FAIL: typed text not found in DOM
set /a FAIL+=1
goto :t6
:t5pass
echo PASS: typed text confirmed in DOM
set /a PASS+=1

REM -- T6: a11y read -------------------------
:t6
echo.
echo [T6] a11y read #read-target...
for /f "tokens=*" %%L in ('%WK% a11y read "read-target#!CHROME_HW!" 2^>^&1') do (
    echo %%L | findstr /C:"fox" >/dev/null && goto :t6pass
)
echo FAIL: read-target text not found
set /a FAIL+=1
goto :done
:t6pass
echo PASS: read-target contains expected text
set /a PASS+=1

:done
echo.
echo =============================================
echo  Results: !PASS! PASS  /  !FAIL! FAIL
echo =============================================
REM -- Auto-suggest on failure -------------------
if !FAIL! gtr 0 (
    echo.
    echo [AUTO] Submitting bug suggest for !FAIL! failed test(s)...
    pushd D:\GitHub\WKAppBot
    wkappbot-core.exe suggest "a11y/cdp integration test failure: !FAIL! of !TOTAL! tests failed. Run: test\cdp-a11y-test.cmd to reproduce." --requirement "test\cdp-a11y-test.cmd => 0 FAIL" --requirement "wkappbot cdp open file:///D:/GitHub/wkappbot-sdk/docs/test/index.html => OK" --requirement "wkappbot a11y inspect {cdp:9741} => btn-primary" 2>nul
    popd
    if not errorlevel 1 ( echo [AUTO] Suggest filed successfully. ) else ( echo [AUTO] Suggest filing failed -- check wkappbot. )
)
if !FAIL! gtr 0 ( exit /b 1 ) else ( exit /b 0 )
