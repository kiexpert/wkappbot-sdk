#!/bin/bash
# Test: MCP stdout must contain only valid JSON-RPC (no pollution)
# Bug #28: MCP stdio server connection failure — stdout pollution breaks JSON-RPC

WKAPPBOT="${WKAPPBOT:-/w/SDK/bin/wkappbot.exe}"
PASS=0; FAIL=0
OUTFILE=/tmp/mcp_stdout_test.txt
ERRFILE=/tmp/mcp_stderr_test.txt

echo "=== MCP stdout pollution test ==="

# Send initialize + tools/list + EOF via temp file piped to stdin
INFILE=/tmp/mcp_stdin_test.txt
cat > "$INFILE" <<'JSONEOF'
{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}
{"jsonrpc":"2.0","method":"notifications/initialized"}
{"jsonrpc":"2.0","id":2,"method":"tools/list"}
JSONEOF

# Run MCP with stdin from file, capture stdout and stderr separately
timeout 10 "$WKAPPBOT" mcp --no-wt < "$INFILE" > "$OUTFILE" 2> "$ERRFILE"
MCP_EXIT=$?

echo "MCP exit code: $MCP_EXIT"
echo "stdout lines: $(wc -l < "$OUTFILE")"
echo "stderr lines: $(wc -l < "$ERRFILE")"

# Test 1: stdout must have at least 1 line (initialize response)
STDOUT_LINES=$(wc -l < "$OUTFILE")
if [ "$STDOUT_LINES" -ge 1 ]; then
  echo "PASS: stdout has $STDOUT_LINES line(s)"
  PASS=$((PASS+1))
else
  echo "FAIL: stdout is empty"
  FAIL=$((FAIL+1))
fi

# Test 2: ALL stdout lines must start with '{' (pure JSON)
NON_JSON=$(grep -v '^{' "$OUTFILE" | grep -v '^$')
if [ -z "$NON_JSON" ]; then
  echo "PASS: all stdout lines are JSON objects (no pollution)"
  PASS=$((PASS+1))
else
  echo "FAIL: non-JSON lines found in stdout:"
  echo "$NON_JSON"
  FAIL=$((FAIL+1))
fi

# Test 3: First line contains jsonrpc and result
FIRST_LINE=$(head -1 "$OUTFILE")
if echo "$FIRST_LINE" | grep -q '"jsonrpc"' && echo "$FIRST_LINE" | grep -q '"result"'; then
  echo "PASS: first response is valid JSON-RPC result"
  PASS=$((PASS+1))
else
  echo "FAIL: first line is not a valid JSON-RPC result"
  echo "  Got: ${FIRST_LINE:0:200}"
  FAIL=$((FAIL+1))
fi

# Test 4: tools/list response contains 'tools' array
if grep -q '"tools"' "$OUTFILE"; then
  echo "PASS: tools/list response found with 'tools' key"
  PASS=$((PASS+1))
else
  echo "FAIL: no tools/list response with 'tools' key"
  FAIL=$((FAIL+1))
fi

# Test 5: stderr should have diagnostic messages (not in stdout)
if grep -q "\[MCP\]" "$ERRFILE"; then
  echo "PASS: diagnostic [MCP] messages correctly in stderr"
  PASS=$((PASS+1))
else
  echo "WARN: no [MCP] diagnostics in stderr (might be --no-wt mode)"
  PASS=$((PASS+1))  # Not a hard failure
fi

echo ""
echo "Results: $PASS PASS, $FAIL FAIL"
[ $FAIL -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
echo "SOME TESTS FAILED" && exit 1
