#!/bin/bash
# Test: MCP server via Launcher (wkappbot.exe mcp) — full launcher relay
# Verifies: Launcher spawns Core, stdin→pipe→Core→pipe→stdout relay works

WKAPPBOT="${WKAPPBOT:-W:/SDK/bin/wkappbot.exe}"
PASS=0; FAIL=0
OUTFILE=/tmp/mcp_launcher_stdout.txt
ERRFILE=/tmp/mcp_launcher_stderr.txt

echo "=== MCP Launcher Relay Test ==="

# Send JSON-RPC requests via pipe to Launcher
echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test-launcher","version":"1.0"}}}
{"jsonrpc":"2.0","method":"notifications/initialized"}
{"jsonrpc":"2.0","id":2,"method":"ping"}' | timeout 30 "$WKAPPBOT" mcp --no-wt > "$OUTFILE" 2> "$ERRFILE"
MCP_EXIT=$?

echo "[TEST] exit=$MCP_EXIT, stdout=$(wc -l < "$OUTFILE") lines"

# Test 1: Launcher spawned Core
if grep -q "core started" "$ERRFILE" 2>/dev/null; then
  echo "PASS: Launcher spawned Core process"
  PASS=$((PASS+1))
else
  echo "FAIL: Core not spawned"
  FAIL=$((FAIL+1))
fi

# Test 2: stdin relay delivered data
if grep -qa "stdin relay started\|stdin.*core" "$ERRFILE" 2>/dev/null; then
  echo "PASS: stdin relay delivered data to Core"
  PASS=$((PASS+1))
else
  echo "FAIL: stdin relay didn't deliver data"
  FAIL=$((FAIL+1))
fi

# Test 3: stdout has JSON responses (initialize + ping = 2)
STDOUT_LINES=$(wc -l < "$OUTFILE")
if [ "$STDOUT_LINES" -ge 2 ]; then
  echo "PASS: stdout has $STDOUT_LINES response lines"
  PASS=$((PASS+1))
else
  echo "FAIL: expected >=2 stdout lines, got $STDOUT_LINES"
  FAIL=$((FAIL+1))
fi

# Test 4: No stdout pollution (all lines are JSON)
NON_JSON=$(grep -aPv '^{' "$OUTFILE" | grep -aPv '^$')
if [ -z "$NON_JSON" ]; then
  echo "PASS: stdout is pure JSON (no pollution)"
  PASS=$((PASS+1))
else
  echo "FAIL: non-JSON in stdout: $NON_JSON"
  FAIL=$((FAIL+1))
fi

# Test 5: initialize response
if grep -qa "serverInfo" "$OUTFILE" 2>/dev/null; then
  echo "PASS: initialize response has serverInfo"
  PASS=$((PASS+1))
else
  echo "FAIL: serverInfo missing"
  FAIL=$((FAIL+1))
fi

# Test 6: Launcher relayed stdin to Core (check stderr log)
if grep -qa "stdin relay started" "$ERRFILE" 2>/dev/null; then
  echo "PASS: Launcher stdin→pipe→Core relay confirmed"
  PASS=$((PASS+1))
else
  echo "FAIL: stdin relay not confirmed in logs"
  FAIL=$((FAIL+1))
fi

# Test 7: ping pong
if grep -qa '"pong"' "$OUTFILE" 2>/dev/null; then
  echo "PASS: ping returned pong"
  PASS=$((PASS+1))
else
  echo "FAIL: pong missing"
  FAIL=$((FAIL+1))
fi

echo ""
echo "Results: $PASS PASS, $FAIL FAIL"
[ $FAIL -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
echo "SOME TESTS FAILED" && exit 1
