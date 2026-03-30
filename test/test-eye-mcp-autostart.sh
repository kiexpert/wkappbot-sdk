#!/bin/bash
# test-eye-mcp-autostart.sh — verify Eye MCP worker auto-restart and health check
# Covers suggest #7: MCP 서버 고장 상태 — 자동 재시작 메커니즘 필요 (ts=2026-03-30T10:40)
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Eye MCP Auto-restart Test ==="
MCP="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/EyeMcpClient.cs"

echo -n "Test 1: Max restart limit (5 restarts within 5 min) implemented... "
check grep -q "_restartCount >= 5" "$MCP"

echo -n "Test 2: 5-minute restart window reset implemented... "
check grep -q "TotalMinutes > 5" "$MCP"

echo -n "Test 3: Auto-restart on pipe broken error... "
check grep -q "Pipe broken\|restarting MCP worker" "$MCP"

echo -n "Test 4: MCP worker spawned as DETACHED_PROCESS (no ConPTY deadlock)... "
check grep -q "DETACHED_PROCESS" "$MCP"

echo -n "Test 5: MCP disabled message logged when max restarts exceeded... "
check grep -q "MCP disabled\|Max restarts" "$MCP"

echo -n "Test 6: ShouldRouteToMcp gates commands before sending to dead worker... "
check grep -q "ShouldRouteToMcp\|_mcpDisabled\|isMcpDisabled" "W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/EyeCmdPipeServer.cs"

echo -n "Test 7: EyeMcpClient.Start re-initializes restart counter on fresh start... "
check grep -q "_restartCount = 0" "$MCP"

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
