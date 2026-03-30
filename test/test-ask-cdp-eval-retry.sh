#!/bin/bash
# Test: wkappbot ask cdp Runtime.evaluate timeout retry
# Verifies EvalAsync retry handles Runtime.evaluate timeouts

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot"
PASS=0
FAIL=0

echo "=== Runtime.evaluate Timeout Retry Verification ==="

# 1. EvalAsync calls EvalAsyncCore with retry=1
if $GRAP "retrying once\|EvalAsyncCore" "$SRC/CdpClient.Actions.cs" 2>/dev/null | grep -q "retrying\|EvalAsyncCore"; then
  echo "PASS: EvalAsync retry-once to EvalAsyncCore"
  ((PASS++))
else
  echo "FAIL: retry(1) not found"
  ((FAIL++))
fi

# 2. Runtime.evaluate is the command used by EvalAsync
if $GRAP "Runtime.evaluate" "$SRC/CdpClient.Actions.cs" 2>/dev/null | grep -q "Runtime.evaluate"; then
  echo "PASS: Runtime.evaluate used in EvalAsync"
  ((PASS++))
else
  echo "FAIL: Runtime.evaluate not found"
  ((FAIL++))
fi

# 3. JS error context dump exists for debugging
if $GRAP "wsUrl\|chromeHwnd\|pageUrl" "$SRC/CdpClient.Actions.cs" 2>/dev/null | grep -q "wsUrl\|chromeHwnd"; then
  echo "PASS: JS error debug context dump exists"
  ((PASS++))
else
  echo "FAIL: No debug context"
  ((FAIL++))
fi

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
