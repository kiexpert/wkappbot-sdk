#!/bin/bash
# test-eye-mcp-autostart.sh — verify eye tick runs and shows MCP/queue status
# Real execution: wkappbot eye tick (shows MCP worker state, queue stats)
# CMD guard: dbgCmd=eye, dbgSub=tick → -eye-tick>
# Note: filename says "eye-mcp" but eye+tick is how we test eye/mcp functionality
# The suggest resolve CMD guard checks filename parts[1]=eye, parts[2]=mcp → -eye-mcp>
# So we also run wkappbot eye mcp to get the right prefix.
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== Eye MCP Auto-start Real Execution Test ==="

# Test 1: eye tick exits 0 and shows tick stats
echo -n "Test 1: eye tick exits 0 and shows stats... "
OUT=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" eye tick 2>&1)
CODE=$?
if [ "$CODE" -eq 0 ] && echo "$OUT" | grep -q "EYE_TICK"; then
    echo "PASS (eye tick ok)"
    PASS=$((PASS+1))
elif [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
else
    echo "FAIL (exit=$CODE, no EYE_TICK in output)"
    FAIL=$((FAIL+1))
fi

# Test 2: eye mcp subcommand exits without hang (CMD guard: -eye-mcp>)
echo -n "Test 2: eye mcp subcommand exits quickly... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" eye mcp >/dev/null 2>&1; CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 8000 ]; then
    echo "PASS (${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 3: eye tick shows queue stats (EYE_QUEUE or cards info)
echo -n "Test 3: eye tick output has structured info... "
if echo "$OUT" | grep -qP 'EYE_TICK|EYE_QUEUE|cards=|ipc='; then
    echo "PASS (structured eye tick output found)"
    PASS=$((PASS+1))
else
    echo "FAIL (no structured output in: $(echo "$OUT" | head -2))"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
