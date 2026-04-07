#!/usr/bin/env bash
set -euo pipefail

# cursor containment evidence wrapper
"/mnt/c/Windows/System32/WindowsPowerShell/v1.0/powershell.exe" -ExecutionPolicy Bypass -File test/test-codex-prompt-cursor-containment.ps1 2>/dev/null ||
"/c/Windows/System32/WindowsPowerShell/v1.0/powershell.exe" -ExecutionPolicy Bypass -File test/test-codex-prompt-cursor-containment.ps1
