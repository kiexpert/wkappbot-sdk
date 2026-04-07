#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
pwsh_path="C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe"

"$pwsh_path" -File "$SCRIPT_DIR\\test-a11y-hack-fisheye-smoke.ps1" "$@"
