#!/bin/bash
# test-findprompts-mcp.sh — verify find-prompts routes through MCP (not separate spawn)
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== find-prompts MCP Routing Test ==="
SRC="$ROOT/csharp/src/WKAppBot.CLI/Commands/AppBotEyeClaudeDetector.cs"

echo -n "Test 1: Uses EyeMcpClient.CallAsync... "; check grep -q "EyeMcpClient.CallAsync" "$SRC"
echo -n "Test 2: No AppBotPipe.Spawn for find-prompts... "; ! grep -q 'AppBotPipe.Spawn.*find-prompts' "$SRC" && { echo "PASS"; PASS=$((PASS+1)); } || { echo "FAIL"; FAIL=$((FAIL+1)); }
echo -n "Test 3: 30s MCP timeout... "; check grep -q "30_000" "$SRC"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
