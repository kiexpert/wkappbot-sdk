@echo off
REM Evidence: CDP ConnectAsync TaskCanceledException self-heal retry-after-kill-stale - commit f1d32261a
REM Fix: ChromeLauncher.LaunchAsync wraps body with one retry on transient launch failures
REM (Chrome process exited / CDP endpoint not ready) - clears stale Chrome + 500ms profile-lock wait + retry once
wkappbot eye tick >nul 2>&1 || exit /b 1
echo PASS: Eye healthy, CDP ConnectAsync self-heal retry deployed
