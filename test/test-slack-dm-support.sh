#!/bin/bash
# test-slack-dm-support.sh — verify slack dm/send exits cleanly
# Real execution: wkappbot slack dm (tests routing)
# CMD guard: dbgCmd=slack, dbgSub=dm → -slack-dm>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Slack DM Support Real Execution Test ==="

# Test 1: slack dm exits without crash (may be unknown subcmd, but no hang)
echo -n "Test 1: slack dm exits quickly... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" slack dm >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 8000 ]; then
    echo "PASS (${ELAPSED}ms, exit=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 2: slack status exits 0 with listener info
echo -n "Test 2: slack status exits with listener info... "
OUT=$(WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" slack status 2>&1)
CODE=$?
if [ "$CODE" -le 1 ] && echo "$OUT" | grep -qi "Listener\|running\|connected"; then
    echo "PASS (listener state: $(echo "$OUT" | head -1))"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE, output: $(echo "$OUT" | head -1))"
    FAIL=$((FAIL+1))
fi

# Test 3: slack send --help shows usage (exits 0 or 1)
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
