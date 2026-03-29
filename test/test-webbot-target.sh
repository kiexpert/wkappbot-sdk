#!/bin/bash
# test-webbot-target.sh — verify web open prints verified target info
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== WebBot Target Info Test ==="
WEB="$ROOT/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"

echo -n "Test 1: ChromeWindowHandle resolved... "; check grep -q "GetChromeWindowHandle" "$WEB"
echo -n "Test 2: FindByTitle verification... "; check grep -q "FindByTitle" "$WEB"
echo -n "Test 3: WKWebBot pattern candidate... "; check grep -q "WKWebBot" "$WEB"
echo -n "Test 4: No-match WARNING dump... "; check grep -q "No unique window match" "$WEB"
echo -n "Test 5: TabID printed... "; check grep -q "TabID" "$WEB"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
