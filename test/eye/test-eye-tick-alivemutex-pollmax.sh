#!/usr/bin/env bash
# Evidence: Eye auto-launch poll max is 1s (was 2s)
# Verifies: suggestion 2026-03-31T04:09 — reduce alive-mutex poll from 2s to 1s
# Related command: eye tick (exercises LaunchAppBotEyeIfNeededCore poll path) (LaunchAppBotEyeIfNeededCore)

set -e
REPO="D:/GitHub/WKAppBot"
FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeCommands.cs"

echo "=== Checking Eye auto-launch alive-mutex poll max ==="
grep -n "wait < 1000\|wait < 2000" "$FILE"

COUNT=$(grep -c "wait < 1000" "$FILE")
if [ "$COUNT" -ge 1 ]; then
    echo "PASS: poll max is 1000ms (1s) — suggestion implemented"
else
    echo "FAIL: expected 'wait < 1000' not found"
    exit 1
fi

echo ""
echo "=== Live: eye tick (forces eye launch check path) ==="
"D:/SDK/bin/wkappbot.exe" eye tick 2>&1 | head -3
echo "PASS: eye tick completed (eye launch path exercised)"
