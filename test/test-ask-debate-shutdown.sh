#!/bin/bash
# Test: wkappbot ask debate shutdown — verify abort integration across files
# Checks that Entry.cs calls RunDebateLoop which now has abort support

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
CLI="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands"
P=0; F=0

echo "=== Debate Shutdown Integration ==="

# 1. Entry.cs calls RunDebateLoop (the wrapper that installs abort handler)
if $GRAP "RunDebateLoop" "$CLI/AskCommands.Entry.cs" 2>/dev/null | grep -q "RunDebateLoop"; then
  echo "PASS: Entry.cs invokes RunDebateLoop"; ((P++))
else echo "FAIL: Entry.cs missing RunDebateLoop call"; ((F++)); fi

# 2. DebateRunner has CancellationTokenSource for abort
if $GRAP "CancellationTokenSource" "$CLI/AskCommands.DebateRunner.cs" 2>/dev/null | grep -q "CancellationTokenSource"; then
  echo "PASS: DebateRunner uses CancellationTokenSource"; ((P++))
else echo "FAIL: no CancellationTokenSource"; ((F++)); fi

# 3. Messages.cs defines moderator messages (separate from abort)
if $GRAP "DebateMsg" "$CLI/AskCommands.DebateRunner.Messages.cs" 2>/dev/null | grep -q "DebateMsg"; then
  echo "PASS: DebateMsg class in Messages.cs"; ((P++))
else echo "FAIL: DebateMsg not found"; ((F++)); fi

# 4. Emoji.cs has emoji assignment (separate from abort)
if $GRAP "AssignEmoji\|SlackAiName" "$CLI/AskCommands.DebateRunner.Emoji.cs" 2>/dev/null | grep -q "Emoji\|SlackAiName"; then
  echo "PASS: Emoji assignment in Emoji.cs"; ((P++))
else echo "FAIL: no emoji logic"; ((F++)); fi

# 5. RunDebateLoopCore is the actual logic (separated from handler setup)
CORE=$($GRAP "static void RunDebateLoopCore" "$CLI/AskCommands.DebateRunner.cs" 2>/dev/null | wc -l)
if [ "$CORE" -ge 1 ]; then
  echo "PASS: RunDebateLoopCore method exists ($CORE)"; ((P++))
else echo "FAIL: RunDebateLoopCore missing"; ((F++)); fi

# 6. Cross-prompt loop also checks cancellation
if $GRAP "IsCancellationRequested" "$CLI/AskCommands.DebateRunner.cs" 2>/dev/null | grep -q "CrossPrompt\|cross\|sw.Elapsed"; then
  echo "PASS: CrossPromptLoop checks cancellation"; ((P++))
else echo "FAIL: CrossPromptLoop unchecked"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
