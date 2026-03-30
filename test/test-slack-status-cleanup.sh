#!/bin/bash
# test-slack-status-cleanup.sh — verify slack status command runs and exits cleanly
# Real execution: wkappbot slack status
# CMD guard: dbgCmd=slack, dbgSub=status → -slack-status>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Slack Status Real Execution Test ==="

# Test 1: slack status exits 0 (even with listener not running)
echo -n "Test 1: slack status exits without crash... "
OUT=$(WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" slack status 2>&1)
CODE=$?
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
elif [ "$CODE" -le 1 ]; then
    echo "PASS (exit=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE)"
    FAIL=$((FAIL+1))
fi

# Test 2: output contains listener state info
echo -n "Test 2: output shows listener state... "
if echo "$OUT" | grep -qi "Listener\|running\|connected\|disconnected"; then
    echo "PASS (listener state shown)"
    PASS=$((PASS+1))
else
    echo "FAIL (no listener state: $(echo "$OUT" | head -1))"
    FAIL=$((FAIL+1))
fi

# Test 3: exits in < 8s (no Slack network timeout hang)
echo -n "Test 3: exits in < 8s (no hang)... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" slack status >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ] || [ "$ELAPSED" -ge 8000 ]; then
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
else
    echo "PASS (${ELAPSED}ms)"
    PASS=$((PASS+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
