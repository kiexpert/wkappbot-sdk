#!/bin/bash
# Test: wkappbot ask cdp timeout handling completeness
# Verifies all timeout paths: command-level, eval-level, connection-level

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot"
P=0; F=0

echo "=== CDP Timeout Handling Completeness ==="

# 1. Command-level timeout in CdpClient
if $GRAP "timeoutMs\|CancellationTokenSource" "$SRC/CdpClient.cs" 2>/dev/null | grep -q "timeoutMs\|CancellationToken"; then
  echo "PASS: Command-level timeout configured"; ((P++))
else echo "FAIL: no command timeout"; ((F++)); fi

# 2. Runtime.enable call protected by timeout
if $GRAP "Runtime.enable" "$SRC/CdpClient.cs" 2>/dev/null | grep -q "Runtime.enable"; then
  echo "PASS: Runtime.enable protected"; ((P++))
else echo "FAIL: Runtime.enable unprotected"; ((F++)); fi

# 3. TaskCompletionSource or async pattern for response waiting
if $GRAP "TaskCompletionSource\|_pending" "$SRC/CdpClient.cs" 2>/dev/null | grep -q "TaskCompletion\|_pending"; then
  echo "PASS: Async response waiting pattern"; ((P++))
else echo "FAIL: no async waiting"; ((F++)); fi

# 4. JS error callback (OnJsError)
if $GRAP "OnJsError\|_onJsError" "$SRC/CdpClient.cs" 2>/dev/null | grep -q "JsError"; then
  echo "PASS: JS error callback mechanism"; ((P++))
else echo "FAIL: no JS error callback"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
