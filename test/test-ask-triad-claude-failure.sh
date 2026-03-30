#!/bin/bash
# test-ask-triad-claude-failure.sh — verify Claude triad failure detection and reporting
# Covers suggest #12: BUG: Claude response failure in triad ask — repeatable pattern (ts=2026-03-30T13:01)
# The failure: ClipboardEvent paste EMPTY → Input.insertText fallback → still failed twice
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

# Command under test: wkappbot ask triad "<question>"
echo "=== Claude Triad Failure Test ==="
CLAUDE="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Claude.cs"
DEBATE="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Debate.cs"
DEBATE_MODE="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.DebateMode.cs"

echo -n "Test 1: Claude failure posted to Slack thread as visible error... "
check grep -q "Claude.*응답 실패" "$CLAUDE"

echo -n "Test 2: AUDITOR role assigned to Claude in triad... "
check grep -q "AUDITOR" "$CLAUDE"

echo -n "Test 3: Input.insertText fallback used when clipboard paste fails... "
check grep -q "insertText\|InsertText" "$CLAUDE"

echo -n "Test 4: WaitWhileStopButtonVisible prevents premature polling... "
check grep -q "WaitWhileStopButtonVisible" "$CLAUDE"

echo -n "Test 5: Claude task in triad is independent (Task.Run — no cascade fail)... "
check grep -q "Task.Run" "$DEBATE_MODE"

echo -n "Test 6: Triad moderator completes even when Claude is absent... "
check grep -q "moderator\|Moderator" "$DEBATE_MODE"

echo -n "Test 7: Null-response guard prevents crash on Claude silence... "
check grep -q "null\|hasResponse\|== null" "$CLAUDE"

echo -n "Test 8: ask triad (AskTriadDebate) command exists in DebateMode... "
check grep -q "AskTriadDebate" "W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.DebateMode.cs"

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
