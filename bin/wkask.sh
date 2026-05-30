#!/usr/bin/env bash
# wkask.sh -- streaming relay for wkask.ps1 (long runtime, must stream)
# NOT a wkwrap relay -- uses exec without -NonInteractive for streaming.
exec powershell -NoProfile -ExecutionPolicy Bypass -File "$(dirname "$(realpath "$0")")/wkask.ps1" "$@"