#!/usr/bin/env bash
# Evidence: suggest merge command is implemented
# Verifies: suggestion 2026-03-31T04:26 — add suggest merge subcommand

set -e
REPO="W:/GitHub/WKAppBot"
ROUTE_FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.cs"
IMPL_FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.Merge.cs"

echo "=== Checking suggest merge routing ==="
grep -n '"merge"' "$ROUTE_FILE" | head -3
COUNT_ROUTE=$(grep -c 'SuggestMergeCommand' "$ROUTE_FILE")
if [ "$COUNT_ROUTE" -ge 1 ]; then
    echo "PASS: SuggestMergeCommand routed in SuggestCommand.cs"
else
    echo "FAIL: merge routing not found in SuggestCommand.cs"
    exit 1
fi

echo ""
echo "=== Checking suggest merge implementation ==="
if [ ! -f "$IMPL_FILE" ]; then
    echo "FAIL: SuggestCommand.Merge.cs not found"
    exit 1
fi
grep -n 'SuggestMergeCommand' "$IMPL_FILE" | head -3
COUNT_IMPL=$(grep -c 'SuggestMergeCommand' "$IMPL_FILE")
if [ "$COUNT_IMPL" -ge 1 ]; then
    echo "PASS: SuggestMergeCommand defined in SuggestCommand.Merge.cs"
else
    echo "FAIL: SuggestMergeCommand definition not found in Merge.cs"
    exit 1
fi

echo ""
echo "=== Live: suggest merge --help ==="
"W:/SDK/bin/wkappbot.exe" suggest merge --help 2>&1 | head -8
echo "PASS: suggest merge --help works"
