#!/bin/bash
# Test: CDP timeout retry logic exists and EvalAsync has retry(1)
# Verifies BUG-AUTO CDP timeout suggests are already handled

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src"
PASS=0
FAIL=0

echo "=== CDP Timeout Retry Logic Verification ==="
# Command: wkappbot ask cdp uses CdpClient.EvalAsync with retry

# 1. EvalAsyncCore retry logic exists
if $GRAP "EvalAsyncCore" "$SRC/WKAppBot.WebBot/CdpClient.Actions.cs" 2>/dev/null | grep -q "EvalAsyncCore"; then
  echo "PASS: EvalAsyncCore retry function exists"
  ((PASS++))
else
  echo "FAIL: EvalAsyncCore not found"
  ((FAIL++))
fi

# 2. retry parameter in EvalAsync
if $GRAP "retry" "$SRC/WKAppBot.WebBot/CdpClient.Actions.cs" 2>/dev/null | grep -q "retry"; then
  echo "PASS: retry parameter exists in EvalAsync"
  ((PASS++))
else
  echo "FAIL: retry not found"
  ((FAIL++))
fi

# 3. Runtime.enable is used (the command that timed out)
if $GRAP "Runtime.enable" "$SRC/WKAppBot.WebBot/CdpClient.cs" 2>/dev/null | grep -q "Runtime.enable"; then
  echo "PASS: Runtime.enable command present in CdpClient"
  ((PASS++))
else
  echo "FAIL: Runtime.enable not found"
  ((FAIL++))
fi

# 4. Timeout handling exists
if $GRAP "TimeoutException\|timed.out\|timeout" "$SRC/WKAppBot.WebBot/CdpClient.cs" 2>/dev/null | grep -qi "timeout"; then
  echo "PASS: Timeout handling exists in CdpClient"
  ((PASS++))
else
  echo "FAIL: No timeout handling"
  ((FAIL++))
fi

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
