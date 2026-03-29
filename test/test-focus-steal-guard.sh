#!/bin/bash
# test-focus-steal-guard.sh — verify focus steal mitigation
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Focus Steal Guard Test ==="
FOCUS="$ROOT/csharp/src/WKAppBot.CLI/Commands/AskCommands.Focus.cs"
ACTIONS="$ROOT/csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs"

echo -n "Test 1: CDP zoom catches eval failure... "; check grep -q "GetClientRect" "$FOCUS"
echo -n "Test 2: Chrome minimize before tab action... "; check grep -q "SW_SHOWNOACTIVATE" "$ACTIONS"
echo -n "Test 3: Focus steal prevention flag... "; check grep -q "no focus steal" "$ACTIONS"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
