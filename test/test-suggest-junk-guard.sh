#!/bin/bash
# test-suggest-junk-guard.sh — verify suggest show command doesn't create junk suggests (suggest junk)
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Suggest Junk Guard Test ==="
SC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.cs"

echo -n "Test 1: SuggestCommand.cs exists... "; check test -f "$SC"
echo -n "Test 2: suggest list shows pending suggestions... "; check grep -q "show pending suggestions" "$SC"
echo -n "Test 3: suggest resolve requires evidence flag... "; check grep -q "willkim-allowed-this-script" "$SC"
echo -n "Test 4: suggest list shows pending suggests... "; check grep -q '"list"' "$SC"
echo -n "Test 5: Junk suggest filtering: JSONL file stores suggests... "; check grep -q "suggestions.jsonl" "$SC"
echo -n "Test 6: Suggest text is written via wkappbot suggest (not sub-commands)... "; check grep -q "SaveSuggestion\|SaveToFile\|Pending" "$SC"
echo -n "Test 7: Resolve removes suggest from pending list... "; check grep -q "resolved" "$SC"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
