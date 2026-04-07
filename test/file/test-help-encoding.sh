#!/usr/bin/env bash
# test-help-encoding.sh — runs test-help-encoding.ps1 via powershell
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
powershell -NoProfile -ExecutionPolicy Bypass -File "$SCRIPT_DIR/test-help-encoding.ps1"
exit $?
