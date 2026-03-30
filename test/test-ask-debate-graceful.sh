#!/bin/bash
# Test: wkappbot ask debate graceful shutdown architecture
# Verifies the wrapper/core split pattern and resource cleanup

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.DebateRunner.cs"
PASS=0
FAIL=0

echo "=== Debate Graceful Shutdown Architecture ==="

# 1. RunDebateLoop wraps RunDebateLoopCore (separation of concerns)
if $GRAP "RunDebateLoopCore" "$SRC" 2>/dev/null | grep -q "RunDebateLoopCore"; then
  echo "PASS: RunDebateLoop → RunDebateLoopCore wrapper pattern"
  ((PASS++))
else
  echo "FAIL: RunDebateLoopCore not found"
  ((FAIL++))
fi

# 2. try/finally pattern for cleanup
if $GRAP "finally" "$SRC" 2>/dev/null | grep -q "finally"; then
  echo "PASS: try/finally cleanup pattern"
  ((PASS++))
else
  echo "FAIL: no finally block for cleanup"
  ((FAIL++))
fi

# 3. CTS Dispose in finally
if $GRAP "Dispose" "$SRC" 2>/dev/null | grep -q "Dispose"; then
  echo "PASS: CTS.Dispose() in finally"
  ((PASS++))
else
  echo "FAIL: no Dispose"
  ((FAIL++))
fi

# 4. R3 loop has cancellation guard
R3_CHECKS=$($GRAP "IsCancellationRequested.*true.*break\|IsCancellationRequested.*true.*return" "$SRC" 2>/dev/null | wc -l)
if [ "$R3_CHECKS" -ge 2 ]; then
  echo "PASS: R3 loop + cross-prompt have cancel guards ($R3_CHECKS)"
  ((PASS++))
else
  echo "FAIL: insufficient R3/cross guards ($R3_CHECKS)"
  ((FAIL++))
fi

# 5. SlackPostToThread abort message exists
if $GRAP "SlackPostToThread.*Ctrl" "$SRC" 2>/dev/null | grep -q "Slack"; then
  echo "PASS: SlackPostToThread abort broadcast"
  ((PASS++))
else
  echo "FAIL: no Slack abort broadcast"
  ((FAIL++))
fi

# 6. Handler unsubscribe (prevents leak across multiple debate runs)
UNSUB=$($GRAP "CancelKeyPress -=" "$SRC" 2>/dev/null | wc -l)
if [ "$UNSUB" -ge 1 ]; then
  echo "PASS: CancelKeyPress unsubscribe ($UNSUB)"
  ((PASS++))
else
  echo "FAIL: handler leak — no unsubscribe"
  ((FAIL++))
fi

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
