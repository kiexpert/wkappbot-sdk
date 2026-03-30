#!/bin/bash
# test-ask-gemini-cdp-js-error.sh — verify GetElementRectAsync JS syntax fix (double-brace bug)
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
checkgrep() { if grep -q "$1" "$2" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL: pattern '$1' not found in $2"; FAIL=$((FAIL+1)); fi; }
checknotgrep() { if grep -q "$1" "$2" >/dev/null 2>&1; then echo "FAIL: bad pattern '$1' found in $2"; FAIL=$((FAIL+1)); else echo "PASS"; PASS=$((PASS+1)); fi; }

echo "=== ask gemini CDP JS SyntaxError Fix Test ==="
ACTIONS="$ROOT/csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs"

echo -n "Test 1: GetElementRectAsync exists... "
checkgrep "GetElementRectAsync" "$ACTIONS"

echo -n "Test 2: Fixed — single closing brace before )() in GetElementRectAsync... "
checkgrep "Math.round(r.height)})()" "$ACTIONS"

echo -n "Test 3: No double-brace bug (}}) before )() in GetElementRectAsync... "
# The buggy pattern was: height}})()" — check it's gone
if grep -A3 "GetElementRectAsync" "$ACTIONS" | grep -q "height}})()"; then
    echo "FAIL: double-brace bug still present"
    FAIL=$((FAIL+1))
else
    echo "PASS"
    PASS=$((PASS+1))
fi

echo -n "Test 4: EvalAsync JS error logging still present (CDP:JS-ERR)... "
checkgrep "CDP:JS-ERR" "$ACTIONS"

echo -n "Test 5: ask gemini command exists... "
checkgrep "AskGemini\|ask.*gemini\|gemini" "$ROOT/csharp/src/WKAppBot.CLI/Commands/AskCommands.Gemini.cs"

echo -n "Test 6: QueryExistsAsync used in WaitForEditorA11y... "
checkgrep "QueryExistsAsync" "$ROOT/csharp/src/WKAppBot.CLI/Commands/AskCommands.ChatGpt.cs"

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
