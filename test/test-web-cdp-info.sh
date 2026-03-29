#!/bin/bash
# test-web-cdp-info.sh — verify web open prints CDP target info
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Web CDP Info Test ==="
SRC="$ROOT/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"

echo -n "Test 1: HWND printed... "; check grep -q 'WEB.*HWND' "$SRC"
echo -n "Test 2: CDP command printed... "; check grep -q 'WEB.*CDP' "$SRC"
echo -n "Test 3: a11y read hint... "; check grep -q 'a11y read' "$SRC"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
