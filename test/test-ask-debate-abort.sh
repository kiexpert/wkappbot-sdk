#!/bin/bash
# Test: wkappbot ask debate abort — Ctrl+C graceful shutdown
# Verifies CancellationTokenSource + CancelKeyPress + loop guards + Slack broadcast

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.DebateRunner.cs"
PASS=0
FAIL=0

echo "=== Debate Abort (Ctrl+C Graceful Shutdown) ==="

# 1. CancellationTokenSource field exists
if $GRAP "_debateCts" "$SRC" 2>/dev/null | grep -q "_debateCts"; then
  echo "PASS: _debateCts CancellationTokenSource field"
  ((PASS++))
else
  echo "FAIL: _debateCts not found"
  ((FAIL++))
fi

# 2. Console.CancelKeyPress handler
if $GRAP "CancelKeyPress" "$SRC" 2>/dev/null | grep -q "CancelKeyPress"; then
  echo "PASS: Console.CancelKeyPress handler registered"
  ((PASS++))
else
  echo "FAIL: CancelKeyPress not found"
  ((FAIL++))
fi

# 3. Slack abort notification
if $GRAP "Ctrl.C" "$SRC" 2>/dev/null | grep -q "Ctrl"; then
  echo "PASS: Slack abort notification (Ctrl+C)"
  ((PASS++))
else
  echo "FAIL: Slack abort message not found"
  ((FAIL++))
fi

# 4. IsCancellationRequested checks in loops
COUNT=$($GRAP "IsCancellationRequested" "$SRC" 2>/dev/null | grep -c "IsCancellationRequested")
if [ "$COUNT" -ge 3 ]; then
  echo "PASS: $COUNT cancellation checks in debate loops"
  ((PASS++))
else
  echo "FAIL: Only $COUNT cancellation checks (need >=3)"
  ((FAIL++))
fi

# 5. Handler cleanup (CancelKeyPress -= )
if $GRAP "CancelKeyPress -=" "$SRC" 2>/dev/null | grep -q "CancelKeyPress"; then
  echo "PASS: Handler cleanup (CancelKeyPress -=)"
  ((PASS++))
else
  echo "FAIL: No handler cleanup"
  ((FAIL++))
fi

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
