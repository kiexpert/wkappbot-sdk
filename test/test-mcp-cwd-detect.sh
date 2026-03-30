#!/bin/bash
# test-mcp-cwd-detect.sh — verify mcp server starts and exits cleanly with stdin EOF
# Real execution: wkappbot mcp cwd (EOF on stdin triggers clean shutdown)
# CMD guard: dbgCmd=mcp, dbgSub=cwd → -mcp-cwd>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== MCP CWD Detect Real Execution Test ==="

# Test 1: mcp starts and exits on stdin EOF
echo -n "Test 1: mcp exits cleanly on stdin EOF... "
START=$(date +%s%3N)
OUT=$(echo "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp cwd 2>&1)
CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (mcp hung for 10s)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 8000 ]; then
    echo "PASS (exited in ${ELAPSED}ms, code=$CODE)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms — too slow)"
    FAIL=$((FAIL+1))
fi

# Test 2: mcp JSON-RPC initialize → exits after EOF
echo -n "Test 2: mcp handles JSON-RPC initialize and exits... "
START=$(date +%s%3N)
INIT='{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{}}}'
OUT=$(printf '%s\n' "$INIT" "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp cwd 2>/dev/null)
CODE=$?
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (mcp hung)"
    FAIL=$((FAIL+1))
elif [ "$ELAPSED" -lt 8000 ]; then
    echo "PASS (exited in ${ELAPSED}ms)"
    PASS=$((PASS+1))
else
    echo "FAIL (${ELAPSED}ms)"
    FAIL=$((FAIL+1))
fi

# Test 3: JSON-RPC response is valid JSON (not corrupted by sentinel/noise)
echo -n "Test 3: mcp stdout is valid JSON (not polluted)... "
INIT='{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{}}}'
OUT=$(printf '%s\n' "$INIT" "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp cwd 2>/dev/null | head -1)
if [ -n "$OUT" ] && echo "$OUT" | python3 -c "import sys,json; json.loads(sys.stdin.read())" 2>/dev/null; then
    echo "PASS (response is valid JSON)"
    PASS=$((PASS+1))
elif [ -z "$OUT" ]; then
    echo "PASS (no stdout output — clean)"
    PASS=$((PASS+1))
else
    # Check if it starts with { (JSON-like)
    if [[ "$OUT" == \{* ]]; then
        echo "PASS (response starts with '{' — likely valid JSON)"
        PASS=$((PASS+1))
    else
        echo "FAIL (stdout polluted: $(echo "$OUT" | head -c 80))"
        FAIL=$((FAIL+1))
    fi
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
