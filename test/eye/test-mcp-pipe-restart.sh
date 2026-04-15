#!/bin/bash
# test-mcp-pipe-restart.sh — verify MCP auto-restart on pipe error
ROOT="${WKAPPBOT_ROOT:-D:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== MCP Pipe Auto-Restart Test ==="
MCP="$ROOT/csharp/src/WKAppBot.CLI/EyeMcpClient.cs"

echo -n "Test 1: IOException triggers restart... "; check grep -q "IOException" "$MCP"
echo -n "Test 2: ObjectDisposedException triggers restart... "; check grep -q "ObjectDisposedException" "$MCP"
echo -n "Test 3: Process.Kill on broken pipe... "; check grep -q "_process?.Kill" "$MCP"
echo -n "Test 4: EnsureStarted after kill... "; check grep -q "EnsureStarted" "$MCP"
echo -n "Test 5: Restarting MCP worker log... "; check grep -q "restarting MCP worker" "$MCP"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
