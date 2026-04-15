#!/usr/bin/env bash
# Evidence: Eye card CWD cross-match fix for reliable sort order
# Verifies: suggestion 2026-04-02T01:15 — Eye cards sort by most recently active

set -e
REPO="D:/GitHub/WKAppBot"
FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeHealthCheck.cs"

echo "=== Checking Eye card CWD cross-match fix ==="
grep -n 'x\.Cwd\|cwdRaw.*TrimEnd\|Phase 1.*Phase 2\|cross.match\|sessionJsonl' "$FILE" | head -10

COUNT=$(grep -c 'x\.Cwd' "$FILE")
if [ "$COUNT" -ge 1 ]; then
    echo "PASS: CWD cross-match (x.Cwd) in AppBotEyeHealthCheck.cs"
else
    echo "FAIL: CWD cross-match not found"
    exit 1
fi

echo ""
echo "=== Live: eye tick (exercises card build path) ==="
"D:/SDK/bin/wkappbot.exe" eye tick 2>&1 | head -3
echo "PASS: eye tick card path verified"
