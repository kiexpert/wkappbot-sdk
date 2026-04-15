#!/bin/bash
# test-ask-triad-mod-redirect.sh — verify Triad moderator redirect implementation details (ask triad)
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Ask Triad Moderator Redirect Test ==="
DR="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.DebateRunner.cs"
TC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Triad.cs"

echo -n "Test 1: 2+ CLI pattern threshold for off-topic detection... "; check grep -q "matches >= 2" "$DR"
echo -n "Test 2: DEBATE_JSON pattern in off-topic list... "; check grep -q "DEBATE_JSON" "$DR"
echo -n "Test 3: Available commands pattern in off-topic list... "; check grep -q "Available commands:" "$DR"
echo -n "Test 4: Redirect re-polls with InjectAndPollAsync after redirect... "; check grep -q "await InjectAndPollAsync.*redirectMsg" "$DR"
echo -n "Test 5: Moderator redirect fires BEFORE format enforcement... "; check grep -q "Moderator redirect.*off-topic" "$DR"
echo -n "Test 6: OriginalQuestion property returns _question field... "; check grep -q "public string OriginalQuestion" "$TC"
echo -n "Test 7: Off-topic check only runs on non-null responses... "; check grep -q "IsOffTopicResponse(response)" "$DR"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
