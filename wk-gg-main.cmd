@echo off
REM wk-gg-main.cmd: SDK Product Manager Main Duties Automation
REM Run from anywhere: wk-gg-main.cmd (assumes repo root or PATH)
REM Purpose: Proactive health check, anomaly detection, escalation

setlocal enabledelayedexpansion

if exist "scripts\gg-main.ps1" (
  echo [SDK gg-main] Running health check...
  powershell -NoProfile -NonInteractive -File "scripts\gg-main.ps1"
) else if exist "%~dp0scripts\gg-main.ps1" (
  echo [SDK gg-main] Running health check from %~dp0
  powershell -NoProfile -NonInteractive -File "%~dp0scripts\gg-main.ps1"
) else (
  echo [ERROR] gg-main.ps1 not found in scripts/ directory
  exit /b 1
)

endlocal
