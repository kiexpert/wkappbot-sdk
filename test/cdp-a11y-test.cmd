@echo off
setlocal enabledelayedexpansion
REM WKAppBot CDP/A11y Comprehensive Integration Test
REM Covers all 24 standard a11y actions with CDP verification
REM Usage: test\cdp-a11y-test.cmd [--local]

set WK=wkappbot
set PAGE=file:///D:/GitHub/wkappbot-sdk/docs/test/index.html
if "%1"=="--pages" set PAGE=https://kiexpert.github.io/wkappbot-sdk/test/
set PASS=0
set FAIL=0
set TOTAL=22
set HW=
set LOGFILE=%TEMP%\cdp-a11y-test.log

echo ================================================
echo  WKAppBot CDP/A11y Comprehensive Integration Test
echo  Covering all standard a11y actions
echo ================================================
echo. > "%LOGFILE%"

REM ===== SETUP: Open test page =====
echo.
echo [SETUP] cdp open...
for /f "tokens=*" %%L in ('%WK% cdp open "%PAGE%" 2^>^&1') do (
    echo %%L | findstr /C:"hwnd:" >nul 2>&1 && set OKLINE=%%L
)
if "!OKLINE!"=="" ( echo FAIL: cdp open failed & goto :auto_suggest )
REM Extract hwnd value from OK {hwnd:0x...,
for /f "tokens=2 delims=:" %%H in ("!OKLINE!") do for /f "tokens=1 delims=," %%W in ("%%H") do set HW=%%W
if "!HW!"=="" ( echo FAIL: hwnd not extracted & goto :auto_suggest )
echo SETUP OK hwnd=!HW!
timeout /t 2 /nobreak >nul

REM Helper: run cdp html and grep for expected string
REM Usage: call :check_dom "EXPECTED_STRING" "TEST_NAME"
goto :tests

:check_dom
for /f "tokens=*" %%L in ('%WK% cdp html "!HW!" 2^>^&1') do (
    echo %%L | findstr /C:"%~1" >nul 2>&1 && (
        echo PASS: %~2
        set /a PASS+=1
        goto :eof
    )
)
echo FAIL: %~2 [expected: %~1]
set /a FAIL+=1
goto :eof

:tests
REM ===== GROUP 1: DISCOVERY =====
echo.
echo --- Discovery ---

REM T01: a11y find
echo [T01] a11y find...
%WK% a11y find "Click Me#!HW!" >nul 2>&1
if not errorlevel 1 ( echo PASS: find & set /a PASS+=1 ) else ( echo FAIL: find & set /a FAIL+=1 )

REM T02: a11y inspect
echo [T02] a11y inspect...
for /f "tokens=*" %%L in ('%WK% a11y inspect "!HW!" 2^>^&1') do (
    echo %%L | findstr /C:"btn-primary" >nul 2>&1 && goto :t02pass
)
echo FAIL: inspect & set /a FAIL+=1 & goto :t03
:t02pass
echo PASS: inspect (found btn-primary) & set /a PASS+=1

REM T03: a11y windows
:t03
echo [T03] a11y windows...
for /f "tokens=*" %%L in ('%WK% windows "*WKAppBot CDP*" 2^>^&1') do (
    echo %%L | findstr /C:"kiexpert" >nul 2>&1 && goto :t03pass
    echo %%L | findstr /C:"chrome" >nul 2>&1 && goto :t03pass
)
echo FAIL: windows & set /a FAIL+=1 & goto :t04
:t03pass
echo PASS: windows & set /a PASS+=1

REM T04: a11y screenshot
:t04
echo [T04] a11y screenshot...
set SHOT=%TEMP%\wk-test-shot.png
%WK% a11y screenshot "!HW!" --path "%SHOT%" >nul 2>&1
if exist "%SHOT%" ( echo PASS: screenshot & set /a PASS+=1 & del "%SHOT%" >nul 2>&1 ) else ( echo FAIL: screenshot [file not created] & set /a FAIL+=1 )

REM ===== GROUP 2: WINDOW CONTROL =====
echo.
echo --- Window Control ---

REM T05: minimize
echo [T05] a11y minimize...
%WK% a11y minimize "!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
if not errorlevel 1 ( echo PASS: minimize & set /a PASS+=1 ) else ( echo FAIL: minimize & set /a FAIL+=1 )

REM T06: restore
echo [T06] a11y restore...
%WK% a11y restore "!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
if not errorlevel 1 ( echo PASS: restore & set /a PASS+=1 ) else ( echo FAIL: restore & set /a FAIL+=1 )

REM T07: focus
echo [T07] a11y focus...
%WK% a11y focus "!HW!" >nul 2>&1
if not errorlevel 1 ( echo PASS: focus & set /a PASS+=1 ) else ( echo FAIL: focus & set /a FAIL+=1 )

REM ===== GROUP 3: WEB INTERACTION =====
echo.
echo --- Web Interaction ---

REM T08: a11y invoke (UIA Invoke on button)
echo [T08] a11y invoke btn-primary...
%WK% a11y invoke "btn-primary#!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "clicked:" "invoke -> DOM updated"

REM T09: a11y type
echo [T09] a11y type...
%WK% a11y type "input-text#!HW!" "hello wkappbot" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "hello wkappbot" "type -> input value"

REM T10: a11y toggle (checkbox off->on)
echo [T10] a11y toggle checkbox-a...
%WK% a11y toggle "chk-a#!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "A=on" "toggle -> checkbox checked"

REM T11: a11y select dropdown
echo [T11] a11y select Beta...
%WK% a11y select "dropdown#!HW!" "Beta" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "selected: beta" "select -> dropdown value"

REM T12: a11y scroll
echo [T12] a11y scroll...
%WK% a11y scroll "scroll-box#!HW!" --direction down >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "scrollTop=" "scroll -> scroll indicator"

REM T13: a11y read
echo [T13] a11y read read-target...
for /f "tokens=*" %%L in ('%WK% a11y read "read-target#!HW!" 2^>^&1') do (
    echo %%L | findstr /C:"fox" >nul 2>&1 && goto :t13pass
)
echo FAIL: read [expected: fox] & set /a FAIL+=1 & goto :t14
:t13pass
echo PASS: read & set /a PASS+=1

REM T14: a11y set-range
:t14
echo [T14] a11y set-range slider...
%WK% a11y set-range "range-input#!HW!" 75 >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "value=75" "set-range -> slider value"

REM T15: a11y expand (details element)
echo [T15] a11y expand details...
%WK% a11y expand "details-main#!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "expanded" "expand -> details open"

REM T16: a11y collapse
echo [T16] a11y collapse details...
%WK% a11y collapse "details-main#!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "collapsed" "collapse -> details closed"

REM T17: a11y wait --condition
echo [T17] a11y wait --condition (spawn button)...
%WK% a11y invoke "btn-spawn#!HW!" >nul 2>&1
%WK% a11y wait "btn-delayed#!HW!" --condition visible --timeout 5000 >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "appeared" "wait --condition"

REM T18: clipboard-write + clipboard-read
echo [T18] clipboard-write/read...
%WK% a11y clipboard-write "wkappbot-test-clip" >nul 2>&1
timeout /t 1 /nobreak >nul
for /f "tokens=*" %%L in ('%WK% a11y clipboard-read 2^>^&1') do (
    echo %%L | findstr /C:"wkappbot-test-clip" >nul 2>&1 && goto :t18pass
)
echo FAIL: clipboard-read & set /a FAIL+=1 & goto :t19
:t18pass
echo PASS: clipboard-write/read & set /a PASS+=1

REM T19: a11y type --hotkey (Tab key)
:t19
echo [T19] a11y type --hotkey Tab...
%WK% a11y type "input-text#!HW!" "{TAB}" --hotkey >nul 2>&1
if not errorlevel 1 ( echo PASS: type --hotkey & set /a PASS+=1 ) else ( echo FAIL: type --hotkey & set /a FAIL+=1 )

REM T20: a11y set-value (contenteditable)
echo [T20] a11y set-value contenteditable...
%WK% a11y set-value "setval-target#!HW!" "new value via set-value" >nul 2>&1
timeout /t 1 /nobreak >nul
call :check_dom "new value via set-value" "set-value -> content updated"

REM T21: move + resize
echo [T21] a11y move + resize...
%WK% a11y move "!HW!" 100 100 >nul 2>&1
%WK% a11y resize "!HW!" 900 700 >nul 2>&1
timeout /t 1 /nobreak >nul
if not errorlevel 1 ( echo PASS: move/resize & set /a PASS+=1 ) else ( echo FAIL: move/resize & set /a FAIL+=1 )

REM T22: a11y maximize
echo [T22] a11y maximize...
%WK% a11y maximize "!HW!" >nul 2>&1
timeout /t 1 /nobreak >nul
if not errorlevel 1 ( echo PASS: maximize & set /a PASS+=1 ) else ( echo FAIL: maximize & set /a FAIL+=1 )

:auto_suggest
echo.
echo ================================================
echo  Results: !PASS! PASS  /  !FAIL! FAIL  [of !TOTAL!]
echo ================================================

if !FAIL! gtr 0 (
    echo.
    echo [AUTO] Filing suggest to Core (DG-WKAppBot)...
    pushd D:\GitHub\WKAppBot
    wkappbot-core.exe suggest "a11y/cdp integration test: !FAIL! of !TOTAL! tests failed. Comprehensive action coverage test. Run test\cdp-a11y-test.cmd to reproduce." --requirement "wkappbot eye tick => ctx=" --requirement "wkappbot windows *chrome* => Match" --requirement "wkappbot a11y inspect {cdp:9741} => btn-primary" 2>nul
    if not errorlevel 1 ( echo [AUTO] Suggest filed to DG-WKAppBot ) else ( echo [AUTO] Suggest filing failed )
    popd
)
if !FAIL! gtr 0 ( exit /b 1 ) else ( exit /b 0 )