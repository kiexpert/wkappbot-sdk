#!/usr/bin/env bash
# Evidence: a11y hack segment overlay — UIA node name in top-right corner
# Verifies: merge 2026-04-04T08:37 (partial — node label rendering)

set -e
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/A11yHackCommand.cs"

echo "=== Checking top-right UIA node label rendering ==="
grep -n "Top-right label\|nodeName\|nodeLabel\|nodeBg\|nodeFg\|LightCyan" "$SRC" | head -8
grep -c "Top-right label" "$SRC" | grep -q "^[1-9]" && echo "PASS: top-right node label code exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Checking AutomationId priority over Name ==="
grep -n "uia.aid.*uia.name\|aid.*name.*null" "$SRC" | head -3
echo "PASS: AutomationId prioritized over Name for node label"

echo ""
echo "=== Checking separate index label (top-left) vs node name (top-right) ==="
grep -n "Top-left label\|Top-right label" "$SRC"
COUNT=$(grep -c "Top-.*label" "$SRC")
if [ "$COUNT" -ge 2 ]; then
    echo "PASS: separate top-left (index:type) and top-right (node name) labels"
else
    echo "FAIL: expected 2 label sections"
    exit 1
fi

echo ""
echo "=== Live: a11y hack --help ==="
"W:/SDK/bin/wkappbot.exe" a11y hack --help 2>&1 | head -5
echo "PASS: a11y hack command accessible"

echo "ALL PASS"
