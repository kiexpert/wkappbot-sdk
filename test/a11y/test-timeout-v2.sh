#!/bin/bash
# test-timeout-v2.sh — timeout v2 subcommand exits cleanly
# Real execution: wkappbot timeout v2
# CMD guard: dbgCmd=timeout, dbgSub=v2 → -timeout-v2>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"
echo "=== test-timeout-v2 Real Execution Test ==="

echo -n "Test 1: timeout v2 exits in < 5s... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" timeout v2 >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then echo "FAIL (hung)"; FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 5000 ]; then echo "PASS (${ELAPSED}ms)"; PASS=$((PASS+1))
else echo "FAIL (${ELAPSED}ms)"; FAIL=$((FAIL+1)); fi

echo -n "Test 2: timeout v2 exits without crash (exit code != 124)... "
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" timeout v2 >/dev/null 2>&1; CODE=$?
if [ "$CODE" -ne 124 ]; then echo "PASS (exit=$CODE)"; PASS=$((PASS+1))
else echo "FAIL (hung on repeat)"; FAIL=$((FAIL+1)); fi

echo -n "Test 3: eye tick exits 0 (system stable)... "
WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" eye tick >/dev/null 2>&1; CODE=$?
if [ "$CODE" -eq 0 ]; then echo "PASS"; PASS=$((PASS+1))
else echo "FAIL (exit=$CODE)"; FAIL=$((FAIL+1)); fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
