#!/bin/bash
# test-mcp-stdio.sh — MCP stdio server end-to-end test
# Tests: stdout cleanliness, initialize handshake, tools/list, tools/call
# Usage: bash test/test-mcp-stdio.sh

PASS=0
FAIL=0
MCP_CMD="${MCP_CMD:-wkappbot-core}"

fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }
pass() { echo "PASS: $1"; PASS=$((PASS+1)); }

# --- Test 1: stdout cleanliness (no [ACT], [LAUNCHER], etc.) ---
STDOUT=$( (echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'; sleep 2) | $MCP_CMD mcp 2>/dev/null )

if echo "$STDOUT" | grep -q '^\['; then
  fail "stdout pollution — non-JSON output detected"
else
  pass "stdout clean (no [ACT]/[LAUNCHER] prefix)"
fi

# --- Test 2: initialize response ---
INIT_RESP=$(echo "$STDOUT" | head -1)
if echo "$INIT_RESP" | grep -q '"protocolVersion"'; then
  pass "initialize response contains protocolVersion"
else
  fail "initialize response missing protocolVersion: $INIT_RESP"
fi

if echo "$INIT_RESP" | grep -q '"experimental"'; then
  fail "initialize contains non-spec 'experimental' field"
else
  pass "no experimental field in capabilities"
fi

if echo "$INIT_RESP" | grep -q '"protocol"'; then
  fail "serverInfo contains non-spec 'protocol' field"
else
  pass "no protocol field in serverInfo"
fi

# --- Test 3: tools/list ---
TOOLS_RESP=$( (
  echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'
  sleep 1
  echo '{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}'
  sleep 2
) | $MCP_CMD mcp 2>/dev/null )

TOOLS_LINE=$(echo "$TOOLS_RESP" | grep '"id":2')
if echo "$TOOLS_LINE" | grep -q '"tools"'; then
  pass "tools/list returns tools array"
else
  fail "tools/list missing tools: $TOOLS_LINE"
fi

if echo "$TOOLS_LINE" | grep -q 'wkappbot'; then
  pass "tools/list contains wkappbot tool"
else
  fail "tools/list missing wkappbot tool"
fi

# --- Test 4: tools/call (windows list) ---
CALL_RESP=$( (
  echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'
  sleep 1
  echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"wkappbot_cli","arguments":{"argv":["help"]}}}'
  sleep 3
) | $MCP_CMD mcp 2>/dev/null )

CALL_LINE=$(echo "$CALL_RESP" | grep '"id":3')
if echo "$CALL_LINE" | grep -q '"content"'; then
  pass "tools/call returns content"
else
  fail "tools/call missing content: $CALL_LINE"
fi

# --- Test 5: Launcher pipe relay (if wkappbot available) ---
if command -v wkappbot &>/dev/null; then
  LAUNCHER_RESP=$( (
    echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'
    sleep 3
  ) | wkappbot mcp 2>/dev/null | head -1 )

  if echo "$LAUNCHER_RESP" | grep -q '"protocolVersion"'; then
    pass "Launcher pipe relay: initialize OK"
  else
    fail "Launcher pipe relay: no initialize response"
  fi
else
  echo "SKIP: wkappbot (Launcher) not in PATH"
fi

# --- Summary ---
echo ""
echo "=== MCP Test Summary: $PASS passed, $FAIL failed ==="
exit $FAIL
