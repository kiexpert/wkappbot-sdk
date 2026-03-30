#!/bin/bash
# test-launcher-exit-delay.sh — verify Launcher exits quickly (no 30s DETACHED_PROCESS delay)
# Real execution: wkappbot launcher exit (unknown cmd — tests timing, not routing)
# CMD guard: dbgCmd=launcher, dbgSub=exit → -launcher-exit>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Launcher Exit Delay Real Execution Test ==="

# Test 1: wkappbot exits in < 10s (old bug was 30s due to inherited pipe handles)
echo -n "Test 1: wkappbot exits in < 10s (no 30s delay)... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" launcher exit >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (timeout 12s — still hanging!)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 10000 ]; then
    echo "PASS (${ELAPSED}ms — well within 10s)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms — too slow, fix may be missing)"
    FAIL=$((FAIL+1))
fi

# Test 2: wkappbot help also exits quickly
echo -n "Test 2: wkappbot help exits < 10s... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" help >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (timeout)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 10000 ]; then
    echo "PASS (${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 3: wkappbot launcher exit exits without hang
echo -n "Test 3: launcher subcommand routing exits cleanly... "
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" launcher exit >/dev/null 2>&1; CODE=$?
if [ "$CODE" -ne 124 ]; then
    echo "PASS (exited without hang, code=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (timed out)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
