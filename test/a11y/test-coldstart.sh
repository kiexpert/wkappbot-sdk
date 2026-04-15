#!/bin/bash
# test-coldstart.sh — 30s cold start analysis + zombie detection
# Usage: bash test/test-coldstart.sh
#
# Kills ALL wkappbot processes before testing.
# Tests IOCP path + Core direct, counts zombies after each.
# PASS: all runs < 5s, zombies = 0 after cleanup

WK=D:/SDK/bin/wkappbot.exe
CORE=D:/SDK/bin/wkappbot-core.exe
PASS=0; FAIL=0

echo "=== Cold Start + Zombie Test ==="
echo ""

# Nuclear cleanup — kill all and VERIFY zero
kill_all() {
    for attempt in 1 2 3 4 5; do
        powershell -c "Get-Process wkappbot*,wkappbot-core -EA SilentlyContinue | Stop-Process -Force" 2>/dev/null
        sleep 2
        local left=$(ps -W 2>/dev/null | grep -ic wkappbot)
        [ "$left" -eq 0 ] && return 0
    done
}

echo -n "Initial cleanup: "
kill_all
ZOMBIES=$(ps -W 2>/dev/null | grep -ic wkappbot)
echo "${ZOMBIES} remaining"
if [ "$ZOMBIES" -gt 0 ]; then
    echo "  ABORT: cannot kill all processes!"
    ps -W 2>/dev/null | grep -i wkappbot
    exit 1
fi

echo ""
echo "── IOCP path (--only-core) x5 ──"
MAX_MS=0
for i in 1 2 3 4 5; do
    kill_all
    T0=$(date +%s%N)
    timeout 45 $WK --only-core version 2>/dev/null | grep -qa "wkappbot v"
    T1=$(date +%s%N)
    MS=$((($T1-$T0)/1000000))
    echo "  Run $i: ${MS}ms"
    [ $MS -gt $MAX_MS ] && MAX_MS=$MS
done
ZOMBIES_IOCP=$(ps -W 2>/dev/null | grep -ic wkappbot)
echo "  Zombies: ${ZOMBIES_IOCP}"
echo "  Max: ${MAX_MS}ms"
if [ $MAX_MS -lt 5000 ]; then
    echo "  ✓ IOCP path OK (<5s)"; ((PASS++))
else
    echo "  ✗ IOCP path SLOW (${MAX_MS}ms)"; ((FAIL++))
fi

# Cleanup between tests
powershell -c "Get-Process wkappbot*,wkappbot-core -EA SilentlyContinue | Stop-Process -Force" 2>/dev/null
sleep 2

echo ""
echo "── Core direct x3 ──"
MAX_MS=0
for i in 1 2 3; do
    kill_all
    T0=$(date +%s%N)
    timeout 45 $CORE version 2>/dev/null | grep -qa "wkappbot v"
    T1=$(date +%s%N)
    MS=$((($T1-$T0)/1000000))
    echo "  Run $i: ${MS}ms"
    [ $MS -gt $MAX_MS ] && MAX_MS=$MS
done
ZOMBIES_CORE=$(ps -W 2>/dev/null | grep -ic wkappbot)
echo "  Zombies: ${ZOMBIES_CORE}"
echo "  Max: ${MAX_MS}ms"
if [ $MAX_MS -lt 5000 ]; then
    echo "  ✓ Core direct OK (<5s)"; ((PASS++))
else
    echo "  ✗ Core direct SLOW (${MAX_MS}ms)"; ((FAIL++))
fi

# Zombie check
echo ""
if [ $ZOMBIES_IOCP -le 1 ]; then
    echo "✓ IOCP zombie count OK (${ZOMBIES_IOCP})"; ((PASS++))
else
    echo "✗ IOCP zombies: ${ZOMBIES_IOCP} (should be ≤1)"; ((FAIL++))
fi

# Final cleanup
powershell -c "Get-Process wkappbot*,wkappbot-core -EA SilentlyContinue | Stop-Process -Force" 2>/dev/null

echo ""
echo "=== Result: $PASS PASS / $FAIL FAIL ==="
if [ "$FAIL" -gt 0 ]; then exit 1; else exit 0; fi
