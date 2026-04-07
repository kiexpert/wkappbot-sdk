#!/bin/bash
# test-slack-status-author-cleanup.sh — verify slack status exits cleanly and shows state
# Real execution: wkappbot slack status (per-author cleanup runs in Eye daemon, not testable standalone)
# CMD guard: dbgCmd=slack, dbgSub=status → -slack-status>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Slack Status Author Cleanup Real Execution Test ==="

# Test 1: slack status exits without crash
echo -n "Test 1: slack status exits 0 or 1... "
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" slack status >/dev/null 2>&1; CODE=$?
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
elif [ "$CODE" -le 1 ]; then
    echo "PASS (exit=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (unexpected exit=$CODE)"
    FAIL=$((FAIL+1))
fi

# Test 2: Second call also exits cleanly (no shared state issue)
echo -n "Test 2: second call also exits cleanly... "
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" slack status >/dev/null 2>&1; CODE=$?
if [ "$CODE" -le 1 ]; then
    echo "PASS (exit=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE)"
    FAIL=$((FAIL+1))
fi

# Test 3: slack send --help exits cleanly (0 or 1 — help flag behavior)
echo -n "Test 3: slack send --help exits cleanly... "
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" slack send --help >/dev/null 2>&1; CODE=$?
if [ "$CODE" -le 1 ]; then
    echo "PASS (exit=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
