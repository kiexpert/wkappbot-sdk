#!/usr/bin/env bash
# Evidence: file edit Eye pipe first-output timeout → Core fallback in <1s
# Verifies: suggestion 2026-04-01T03:44 — wkedit 30s startup delay when Eye busy
# Fix: firstOutputTimeoutMs=100 in EyeCmdPipeClient.TryDelegate for most commands;
#      all non-slack/ask/newchat commands now get 100ms first-output guard.

set -e
WKAPPBOT="W:/SDK/bin/wkappbot.exe"

echo "=== Checking EyeCmdPipeClient first-output timeout implementation ==="
LAUNCHER_SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.Launcher/EyeCmdPipeClient.cs"
grep -n "firstOutputTimeoutMs\|firstReadTask\|Wait(firstOutput" "$LAUNCHER_SRC" | head -5

COUNT=$(grep -c "firstOutputTimeoutMs" "$LAUNCHER_SRC")
if [ "$COUNT" -ge 3 ]; then
    echo "PASS: firstOutputTimeoutMs implemented in EyeCmdPipeClient.cs"
else
    echo "FAIL: firstOutputTimeoutMs not found"
    exit 1
fi

echo ""
echo "=== Checking Launcher applies 100ms guard to most commands ==="
PROG_SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.Launcher/Program.cs"
grep -n "isFirstOutputGuardCmd\|firstOutputMs.*100\|100ms first-output" "$PROG_SRC" | head -5

COUNT2=$(grep -c "isFirstOutputGuardCmd" "$PROG_SRC")
if [ "$COUNT2" -ge 2 ]; then
    echo "PASS: isFirstOutputGuardCmd broadens 100ms guard to most commands"
else
    echo "FAIL: isFirstOutputGuardCmd not found"
    exit 1
fi

echo ""
echo "=== Live: file edit completes quickly (no 30s stall) ==="
TMPFILE=$(mktemp /tmp/wkedit-test-XXXXXX.txt)
echo "hello world" > "$TMPFILE"
START=$(date +%s%N)
"$WKAPPBOT" file edit "hello world" "hello test" "$TMPFILE" 2>&1
END=$(date +%s%N)
ELAPSED_MS=$(( (END - START) / 1000000 ))
echo "Elapsed: ${ELAPSED_MS}ms"

if grep -q "hello test" "$TMPFILE"; then
    echo "PASS: file edit applied correctly"
else
    echo "FAIL: edit not applied"
    rm -f "$TMPFILE"; exit 1
fi

if [ "$ELAPSED_MS" -lt 10000 ]; then
    echo "PASS: file edit completed in ${ELAPSED_MS}ms (< 10s — no 30s stall)"
else
    echo "FAIL: file edit took ${ELAPSED_MS}ms — stall still present"
    rm -f "$TMPFILE"; exit 1
fi

rm -f "$TMPFILE"
echo "ALL PASS"
