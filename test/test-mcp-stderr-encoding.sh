#!/bin/bash
# test-mcp-stderr-encoding.sh — verify MCP stderr does not bleed into stdout (encoding/relay fix)
# Real execution: wkappbot mcp stderr (EOF on stdin → checks stdout vs stderr separation)
# CMD guard: dbgCmd=mcp, dbgSub=stderr → -mcp-stderr>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"

echo "=== MCP Stderr Encoding Real Execution Test ==="

# Test 1: mcp stdout contains no ANSI escape codes (stderr stripped from stdout)
echo -n "Test 1: mcp stdout has no ANSI escape codes... "
INIT='{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{}}}'
STDOUT=$(printf '%s\n' "$INIT" "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp stderr 2>/dev/null)
CODE=$?
if [ "$CODE" -eq 124 ]; then
    echo "FAIL (hung)"
    FAIL=$((FAIL+1))
elif echo "$STDOUT" | grep -qP '\x1b\['; then
    echo "FAIL (ANSI codes in stdout: $(echo "$STDOUT" | grep -oP '\x1b\[.*?m' | head -3))"
    FAIL=$((FAIL+1))
else
    echo "PASS (no ANSI in stdout)"
    PASS=$((PASS+1))
fi

# Test 2: mcp stdout contains no [LAUNCHER]/[SUGGEST] diagnostic lines
echo -n "Test 2: mcp stdout has no diagnostic lines bleeding in... "
INIT='{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{}}}'
STDOUT=$(printf '%s\n' "$INIT" "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp stderr 2>/dev/null)
if echo "$STDOUT" | grep -qP '^\[LAUNCHER\]|^\[SUGGEST\]|^\[EYE\]|^\[SLACK\]'; then
    echo "FAIL (diagnostic lines in stdout)"
    FAIL=$((FAIL+1))
else
    echo "PASS (no diagnostic lines in stdout)"
    PASS=$((PASS+1))
fi

# Test 3: mcp exits on EOF (not hanging)
echo -n "Test 3: mcp exits on stdin EOF in < 8s... "
START=$(date +%s%3N)
echo "" | WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" mcp stderr >/dev/null 2>&1
CODE=$?
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

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
