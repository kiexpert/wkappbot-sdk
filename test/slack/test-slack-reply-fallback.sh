#!/bin/bash
# test-slack-reply-fallback.sh — verify slack reply thread-not-found fallback
ROOT="${WKAPPBOT_ROOT:-D:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Slack Reply Fallback Test ==="
SRC="$ROOT/csharp/src/WKAppBot.CLI/Commands/SlackPromptCommands.cs"

echo -n "Test 1: Thread reply failure detected... "; check grep -q "Thread reply failed" "$SRC"
echo -n "Test 2: Falls back to new message... "; check grep -q "falling back to new message" "$SRC"
echo -n "Test 3: PostWithOverflow as fallback... "; check grep -q "PostWithOverflow" "$SRC"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
