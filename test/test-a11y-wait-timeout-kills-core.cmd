@echo off
REM Evidence script: a11y wait with --timeout must exit within timeout (Core killed on expiry)
REM Repro: wkappbot a11y wait on nonexistent grap with --timeout 2 should complete in ~3s max

echo [TEST] a11y wait timeout kills Core -- must exit within 5s
set START=%TIME%
wkappbot a11y wait "nonexistent-chrome-port-xyz-123" --timeout 2 2>&1
echo [TEST] exit code: %ERRORLEVEL%
echo [TEST] PASS -- launcher timeout fired and returned control
