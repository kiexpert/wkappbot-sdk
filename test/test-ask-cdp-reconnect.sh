#!/bin/bash
# Test: wkappbot ask cdp Runtime.enable reconnect resilience
# Verifies CDP has reconnect logic for transient connection failures

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot"
P=0; F=0

echo "=== CDP Reconnect Resilience ==="

# 1. ConnectAsync exists for reconnection
if $GRAP "ConnectAsync\|_ws" "$SRC/CdpClient.cs" 2>/dev/null | grep -q "Connect"; then
  echo "PASS: ConnectAsync for WebSocket connection"; ((P++))
else echo "FAIL: no ConnectAsync"; ((F++)); fi

# 2. Timeout handling in SendCommandAsync
if $GRAP "SendCommandAsync\|CancellationToken\|timeout" "$SRC/CdpClient.cs" 2>/dev/null | grep -q "timeout\|Cancel"; then
  echo "PASS: Timeout handling in SendCommandAsync"; ((P++))
else echo "FAIL: no timeout handling"; ((F++)); fi

# 3. EvalAsync retry via EvalAsyncCore
if $GRAP "EvalAsyncCore\|retrying once" "$SRC/CdpClient.Actions.cs" 2>/dev/null | grep -q "EvalAsyncCore\|retrying"; then
  echo "PASS: EvalAsync retry-once mechanism"; ((P++))
else echo "FAIL: no retry"; ((F++)); fi

# 4. Error context dump for debugging
if $GRAP "wsUrl\|chromeHwnd\|Connected" "$SRC/CdpClient.Actions.cs" 2>/dev/null | grep -q "wsUrl\|Connected"; then
  echo "PASS: Error context dump (wsUrl/Connected)"; ((P++))
else echo "FAIL: no error context"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
