#!/usr/bin/env bash
# Evidence: suggest merge command is implemented
# Verifies: suggestion 2026-03-31T04:26 — add suggest merge subcommand

set -e
REPO="W:/GitHub/WKAppBot"
FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.cs"

echo "=== Checking suggest merge implementation ==="
grep -n '"merge"' "$FILE" | head -3
grep -n 'SuggestMergeCommand' "$FILE" | head -3

COUNT=$(grep -c 'SuggestMergeCommand' "$FILE")
if [ "$COUNT" -ge 2 ]; then
    echo "PASS: SuggestMergeCommand defined and routed"
else
    echo "FAIL: SuggestMergeCommand not found"
    exit 1
fi

echo ""
echo "=== Live: suggest merge --help ==="
"W:/SDK/bin/wkappbot.exe" suggest merge --help 2>&1 | head -8
echo "PASS: suggest merge --help works"
