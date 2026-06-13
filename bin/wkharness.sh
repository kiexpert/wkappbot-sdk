#!/usr/bin/env bash
# wkharness.sh -- bash wrapper for wkharness.ps1 (harness-managed)
# Usage: wkharness.sh [-Status] [-Test] [-Task "..."] [-C dir]
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
TOOLS_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"
exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "$TOOLS_DIR/wkharness.ps1" "$@"
