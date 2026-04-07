#!/bin/bash
# test-mcp-stdout-guard.sh — verify MCP stdout not polluted by launcher sentinel (\0UIT)
# Real execution: wkappbot mcp stdout (verify stdout cleanliness in MCP mode)
# CMD guard: dbgCmd=mcp, dbgSub=stdout → -mcp-stdout>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== MCP Stdout Guard Real Execution Test ==="

# Test 1: mcp stdout subcommand exits without hang
echo -n "Test 1: mcp stdout exits cleanly (no hang)... "
START=$(date +%s%3N)
STDOUT=$(echo "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp stdout 2>/dev/null)
CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 8000 ]; then
    echo "PASS (exited in ${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 2: mcp stdout only contains JSON lines (each line parseable as JSON or empty)
echo -n "Test 2: mcp stdout lines are JSON or empty... "
INIT='{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{}}}'
STDOUT=$(printf '%s\n' "$INIT" "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp stdout 2>/dev/null)
BAD_LINES=0
while IFS= read -r line; do
    [ -z "$line" ] && continue
    if ! echo "$line" | python3 -c "import sys,json; json.loads(sys.stdin.read())" 2>/dev/null; then
        BAD_LINES=$((BAD_LINES+1))
    fi
done <<< "$STDOUT"
if [ "$BAD_LINES" -eq 0 ]; then
    echo "PASS (all stdout lines are valid JSON)"
    PASS=$((PASS+1))
else
    echo "FAIL ($BAD_LINES non-JSON lines in stdout)"
    FAIL=$((FAIL+1))
fi

# Test 3: mcp exits on EOF without hang
echo -n "Test 3: mcp exits on stdin close... "
START=$(date +%s%3N)
echo "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp stdout >/dev/null 2>&1
CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
else
    echo "PASS (exited in ${ELAPSED}ms)"
    PASS=$((PASS+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
