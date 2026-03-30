#!/bin/bash
# test-msg-too-long-fix.sh — verify msg too subcommand exits cleanly
# Real execution: wkappbot msg too
# CMD guard: dbgCmd=msg, dbgSub=too → -msg-too>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"
echo "=== Msg Too Long Real Execution Test ==="

echo -n "Test 1: msg too exits in < 5s... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" msg too >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then echo "FAIL (hung)"; FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 5000 ]; then echo "PASS (${ELAPSED}ms)"; PASS=$((PASS+1))
else echo "FAIL (${ELAPSED}ms)"; FAIL=$((FAIL+1)); fi

echo -n "Test 2: slack send exits with usage (not crash)... "
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" slack send >/dev/null 2>&1; CODE=$?
if [ "$CODE" -le 1 ]; then echo "PASS (exit=$CODE)"; PASS=$((PASS+1))
else echo "FAIL (exit=$CODE)"; FAIL=$((FAIL+1)); fi

echo -n "Test 3: msg too second run also exits quickly... "
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" msg too >/dev/null 2>&1; CODE=$?
if [ "$CODE" -ne 124 ]; then echo "PASS (exit=$CODE)"; PASS=$((PASS+1))
else echo "FAIL (hung)"; FAIL=$((FAIL+1)); fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
