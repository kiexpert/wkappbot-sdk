#!/bin/bash
# test-launcher-pipe-inherit.sh — verify Launcher stdout/stderr non-inherit fix prevents 30s hang
# Real execution: wkappbot launcher pipe (unknown cmd — tests exit timing in pipe context)
# CMD guard: dbgCmd=launcher, dbgSub=pipe → -launcher-pipe>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Launcher Pipe Inherit Fix Real Execution Test ==="

# Test 1: Run in bash $() subshell (most prone to pipe inheritance hang)
echo -n "Test 1: bash \$() subshell exits < 10s... "
START=$(date +%s%3N)
OUT=$(WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" launcher pipe 2>/dev/null)
CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (subshell timed out)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 10000 ]; then
    echo "PASS (${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 2: Pipe redirect (simulates bash pipe context)
echo -n "Test 2: with pipe redirect exits < 10s... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" launcher pipe 2>/dev/null | cat >/dev/null
CODE=${PIPESTATUS[0]}
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$ELAPSED" -lt 10000 ]; then
    echo "PASS (${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms — pipe handle not closed?)"
    FAIL=$((FAIL+1))
fi

# Test 3: No zombie wkappbot processes after exit
echo -n "Test 3: no zombie wkappbot processes after exit... "
BEFORE=$(powershell.exe -NoProfile -Command 'Get-Process wkappbot-core -ErrorAction SilentlyContinue | Measure-Object | Select-Object -ExpandProperty Count' 2>/dev/null | tr -d '\r')
WKAPPBOT_WORKER=1 timeout 8 "$WKBOT" launcher pipe >/dev/null 2>&1
sleep 1
AFTER=$(powershell.exe -NoProfile -Command 'Get-Process wkappbot-core -ErrorAction SilentlyContinue | Measure-Object | Select-Object -ExpandProperty Count' 2>/dev/null | tr -d '\r')
# Allow same or fewer (BEFORE could be 0, AFTER should be same or 0)
if [ "${AFTER:-0}" -le "${BEFORE:-0}" ] || [ "${AFTER:-0}" -eq 0 ]; then
    echo "PASS (no zombies: before=${BEFORE:-0} after=${AFTER:-0})"
    PASS=$((PASS+1))
else
    echo "FAIL (zombie processes: before=${BEFORE:-0} after=${AFTER:-0})"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
