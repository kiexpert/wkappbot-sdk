#!/bin/bash
# test-launcher-handle-noninherit.sh — verify Launcher exits quickly without hanging on inherited handles
# Real execution: wkappbot launcher handle (unknown cmd — tests exit behavior)
# CMD guard: dbgCmd=launcher, dbgSub=handle → -launcher-handle>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Launcher Handle Non-Inherit Real Execution Test ==="

# Test 1: wkappbot exits in < 10s — verifies DETACHED_PROCESS + non-inherit fix works
echo -n "Test 1: exits in < 10s (no bash pipe handle leak)... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" launcher handle >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (timed out — inherited handle leak?)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 10000 ]; then
    echo "PASS (${ELAPSED}ms — no hang)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms — too slow)"
    FAIL=$((FAIL+1))
fi

# Test 2: piped invocation also exits quickly (most affected by handle inheritance)
echo -n "Test 2: piped invocation exits in < 10s... "
START=$(date +%s%3N)
(WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" launcher handle) 2>/dev/null; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (piped invocation hung)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 10000 ]; then
    echo "PASS (${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 3: Three consecutive runs all exit quickly (regression check)
echo -n "Test 3: three consecutive runs all exit quickly... "
ALL_OK=1
for i in 1 2 3; do
    START=$(date +%s%3N)
    WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" launcher handle >/dev/null 2>&1; CODE=$?
    END=$(date +%s%3N)
    ELAPSED=$((END - START))
    if [ "$CODE" -eq 124 ] || [ "$ELAPSED" -ge 10000 ]; then
        ALL_OK=0
        break
    fi
done
if [ "$ALL_OK" -eq 1 ]; then
    echo "PASS (all 3 runs exited quickly)"
    PASS=$((PASS+1))
else
    echo "FAIL (one of 3 runs was slow/hung)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
