#!/bin/bash
# test-eye-message-routing.sh — verify eye message subcommand and eye tick routing
# Real execution: wkappbot eye message (unknown subcmd — tests routing + quick exit)
# CMD guard: dbgCmd=eye, dbgSub=message → -eye-message>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Eye Message Routing Real Execution Test ==="

# Test 1: eye message exits without hang (unknown subcmd handled gracefully)
echo -n "Test 1: eye message exits in < 8s... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" eye message >/dev/null 2>&1; CODE=$?
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

# Test 2: eye tick output does NOT go to stdout when run via pipe (no Claude injection)
echo -n "Test 2: eye tick stdout is clean (no raw slack msg leaking)... "
OUT=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" eye tick 2>/dev/null)
CODE=$?
# Eye tick output should be diagnostic/structured, NOT raw Slack JSON
if echo "$OUT" | grep -qP '"type":"message"|"channel":"C[A-Z0-9]+"'; then
    echo "FAIL (raw Slack JSON in stdout — message routing broken!)"
    FAIL=$((FAIL+1))
else
    echo "PASS (no raw Slack JSON in stdout)"
    PASS=$((PASS+1))
fi

# Test 3: eye tick exits 0
echo -n "Test 3: eye tick exits 0... "
WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" eye tick >/dev/null 2>&1; CODE=$?
if [ "$CODE" -eq 0 ]; then
    echo "PASS"
    PASS=$((PASS+1))
elif [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
else
    echo "FAIL (exit=$CODE)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
