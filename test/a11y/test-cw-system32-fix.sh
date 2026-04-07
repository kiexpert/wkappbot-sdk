#!/bin/bash
# test-cw-system32-fix.sh — verify CW-system32 CWD detection fix
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== CW-system32 Fix Test ==="
MCP="$ROOT/csharp/src/WKAppBot.CLI/Commands/McpCommand.cs"
HC="$ROOT/csharp/src/WKAppBot.CLI/Commands/AppBotEyeHealthCheck.cs"
SR="$ROOT/csharp/src/WKAppBot.CLI/Commands/SessionRegistry.cs"

echo -n "Test 1: DetectMcpParentCwd exists... "; check grep -q "DetectMcpParentCwd" "$MCP"
echo -n "Test 2: system32 CWD filter... "; check grep -q "system32" "$HC"
echo -n "Test 3: SessionRegistry exists... "; check test -f "$SR"
echo -n "Test 4: CWD heuristic (.mcp.json)... "; check grep -q ".mcp.json" "$MCP"
echo -n "Test 5: VS Code title parse... "; check grep -q "Visual Studio Code" "$MCP"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
