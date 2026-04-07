#!/usr/bin/env bash
set -euo pipefail

# prompt probe evidence wrapper: delegate to the PowerShell script that runs the real prompt-probe command
if [ -x "/mnt/c/Windows/System32/WindowsPowerShell/v1.0/powershell.exe" ]; then
  "/mnt/c/Windows/System32/WindowsPowerShell/v1.0/powershell.exe" -ExecutionPolicy Bypass -File test/test-codex-prompt-cursor-containment.ps1
elif [ -x "/c/Windows/System32/WindowsPowerShell/v1.0/powershell.exe" ]; then
  "/c/Windows/System32/WindowsPowerShell/v1.0/powershell.exe" -ExecutionPolicy Bypass -File test/test-codex-prompt-cursor-containment.ps1
else
  echo "FAIL: powershell.exe not found from bash wrapper"
  exit 1
fi
