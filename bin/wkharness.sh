#!/usr/bin/env bash
# wkharness.sh -- bash wrapper for wkharness.ps1 (harness-managed)
# Usage: wkharness.sh [-Status] [-Test] [-Task "..."] [-C dir]
exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "D:/GitHub/personal-docs/tools/wkharness.ps1" "$@"