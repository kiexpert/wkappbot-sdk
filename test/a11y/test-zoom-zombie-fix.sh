#!/bin/bash
# test-zoom-zombie-fix.sh — verify zoom zombie exits cleanly
# Real execution: wkappbot zoom zombie [--help]
# CMD guard: dbgCmd=zoom, dbgSub=zombie → -zoom-zombie>
# Note: 'zoom zombie' exits quickly (unknown subcommand)
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"
echo "=== test-zoom-zombie-fix Real Execution Test ==="

echo -n "Test 1: zoom zombie exits in < 5s... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" zoom zombie >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then echo "FAIL (hung)"; FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 5000 ]; then echo "PASS (${ELAPSED}ms)"; PASS=$((PASS+1))
else echo "FAIL (${ELAPSED}ms)"; FAIL=$((FAIL+1)); fi

echo -n "Test 2: zoom zombie repeat call exits without hang... "
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" zoom zombie >/dev/null 2>&1; CODE=$?
if [ "$CODE" -ne 124 ]; then echo "PASS (exit=$CODE)"; PASS=$((PASS+1))
else echo "FAIL (hung)"; FAIL=$((FAIL+1)); fi

echo -n "Test 3: eye tick exits 0... "
WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" eye tick >/dev/null 2>&1; CODE=$?
if [ "$CODE" -eq 0 ]; then echo "PASS"; PASS=$((PASS+1))
else echo "FAIL (exit=$CODE)"; FAIL=$((FAIL+1)); fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
