@echo off
setlocal enabledelayedexpansion
REM WKAppBot CDP/A11y Integration Test -- Phase 1 (simple core tests)
REM Usage: cdp-a11y-test.cmd
REM Requires: Chrome closed, wkappbot-sdk project active

set WK=wkappbot
set PAGE_URL=https://kiexpert.github.io/wkappbot-sdk/test/
set PASS=0
set FAIL=0

echo ========================================
echo  WKAppBot CDP/A11y Integration Test
echo  Phase 1: Core CDP + a11y basics
echo ========================================

REM ── 1. Open test page ────────────────────
echo.
echo [T1] cdp open test page...
%WK% cdp open "%PAGE_URL%"
if errorlevel 1 ( echo FAIL: cdp open & set /a FAIL+=1 & goto :done )
echo PASS: page opened
set /a PASS+=1

REM ── 2. CDP ping ──────────────────────────
echo.
echo [T2] cdp eval ping (JS API check)...
for /f "delims=" %%R in ('%WK% cdp eval "gemini.google.com" "window.__wkTest ? window.__wkTest.ping() : \"NO_API\"" 2^>nul') do set R=%%R
if "!R!"=="pong" (
    echo PASS: JS API ready
    set /a PASS+=1
) else (
    echo FAIL: expected pong got "!R!"
    set /a FAIL+=1
)

REM ── 3. a11y windows find ─────────────────
echo.
echo [T3] a11y windows -- find test page window...
%WK% windows "*WKAppBot CDP*" >/dev/null 2>&1
if errorlevel 1 ( echo FAIL: window not found & set /a FAIL+=1 ) else ( echo PASS: window found & set /a PASS+=1 )

REM ── 4. cdp eval click counter before ────
echo.
echo [T4] cdp eval: click count before = 0...
for /f "delims=" %%R in ('%WK% cdp eval "gemini.google.com" "window.__wkTest.getClickCount()" 2^>nul') do set R=%%R
if "!R!"=="0" ( echo PASS: initial count=0 & set /a PASS+=1 ) else ( echo FAIL: expected 0 got !R! & set /a FAIL+=1 )

REM ── 5. a11y click button ─────────────────
echo.
echo [T5] a11y click #btn-primary...
%WK% a11y click "Click Me#{domain:kiexpert.github.io}" >/dev/null 2>&1
timeout /t 1 /nobreak >/dev/null
if errorlevel 1 ( echo FAIL: click failed & set /a FAIL+=1 ) else ( echo PASS: click sent & set /a PASS+=1 )

REM ── 6. cdp eval click count after ───────
echo.
echo [T6] cdp eval: click count after = 1...
for /f "delims=" %%R in ('%WK% cdp eval "gemini.google.com" "window.__wkTest.getClickCount()" 2^>nul') do set R=%%R
if "!R!"=="1" ( echo PASS: count=1 confirmed & set /a PASS+=1 ) else ( echo FAIL: expected 1 got !R! & set /a FAIL+=1 )

REM ── 7. a11y type ─────────────────────────
echo.
echo [T7] a11y type "hello" into input...
%WK% a11y type "#input-text#{domain:kiexpert.github.io}" "hello" >/dev/null 2>&1
timeout /t 1 /nobreak >/dev/null
for /f "delims=" %%R in ('%WK% cdp eval "gemini.google.com" "window.__wkTest.getInputValue()" 2^>nul') do set R=%%R
if "!R!"=="hello" ( echo PASS: input="hello" & set /a PASS+=1 ) else ( echo FAIL: expected hello got "!R!" & set /a FAIL+=1 )

REM ── 8. a11y read ─────────────────────────
echo.
echo [T8] a11y read #read-target...
for /f "delims=" %%R in ('%WK% a11y read "#read-target#{domain:kiexpert.github.io}" 2^>nul') do set R=%%R
echo !R! | findstr /i "fox" >/dev/null
if errorlevel 1 ( echo FAIL: read missing "fox" & set /a FAIL+=1 ) else ( echo PASS: read OK & set /a PASS+=1 )

:done
echo.
echo ========================================
echo  Result: !PASS! PASS  !FAIL! FAIL
echo ========================================
if !FAIL! gtr 0 ( exit /b 1 ) else ( exit /b 0 )
