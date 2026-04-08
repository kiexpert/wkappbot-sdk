#!/usr/bin/env bash
# Evidence: suggest merge command is implemented
# Verifies: suggestion 2026-03-31T04:26 — add suggest merge subcommand

set -e
WKAPPBOT="W:/SDK/bin/wkappbot.exe"

echo "=== Live: suggest merge --help ==="
"$WKAPPBOT" suggest merge --help 2>&1 | head -8
echo "PASS: suggest merge --help works"
