@echo off
REM wk-gg-main.cmd: SDK Product Manager Main Duties Automation
REM Run from anywhere: wk-gg-main.cmd (assumes repo root or PATH)
REM Purpose: Proactive health check, anomaly detection, escalation

setlocal enabledelayedexpansion

if exist "scripts\gg-main.sh" (
  echo [SDK gg-main] Running health check...
  bash scripts\gg-main.sh %*
) else if exist "%~dp0scripts\gg-main.sh" (
  echo [SDK gg-main] Running health check from %~dp0
  bash "%~dp0scripts\gg-main.sh" %*
) else (
  echo [ERROR] gg-main.sh not found in scripts/ directory
  exit /b 1
)

endlocal
