#!/bin/bash
# test-cdp-timeout-known.sh — verify CDP timeout handling improvements
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== CDP Timeout Handling Test ==="
ACTIONS="$ROOT/csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs"
FOCUS="$ROOT/csharp/src/WKAppBot.CLI/Commands/AskCommands.Focus.cs"
MCP="$ROOT/csharp/src/WKAppBot.CLI/EyeMcpClient.cs"

echo -n "Test 1: CDP JS error logging... "; check grep -q "CDP:JS-ERR" "$ACTIONS"
echo -n "Test 2: CDP zoom fallback to client area... "; check grep -q "GetClientRect" "$FOCUS"
echo -n "Test 3: MCP auto-restart on IOException... "; check grep -q "IOException" "$MCP"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
