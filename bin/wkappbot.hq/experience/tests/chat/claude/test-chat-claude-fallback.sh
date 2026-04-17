#!/bin/bash
# Evidence: [FEATURE] wkappbot chat — Claude CLI passthrough + rate-limit fallback.
# Tests:
#   1. `wkappbot chat --help` shows help (command registered)
#   2. `wkappbot chat --no-fallback "q"` returns error 127 when claude absent
#   3. Source contains rate-limit markers and AskTriadFallback path
WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"

[ -f "$WKAPPBOT" ] || { echo "FAIL: $WKAPPBOT missing"; exit 1; }

# Test 1: chat --help works
help_out=$($WKAPPBOT chat --help 2>&1)
echo "$help_out" | grep -q "Claude Code CLI passthrough" || { echo "FAIL: chat help missing"; exit 1; }
echo "OK: chat --help works"

# Test 2: --no-fallback + claude absent returns 127
$WKAPPBOT chat --no-fallback "dummy" >/dev/null 2>&1
rc=$?
[ "$rc" = "127" ] && echo "OK: --no-fallback + claude-absent returns 127" \
  || { echo "FAIL: expected 127, got $rc"; exit 1; }

# Test 3: source has the fallback infrastructure
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/ChatCommand.cs"
grep -q "usage limit" "$SRC" || { echo "FAIL: rate-limit markers missing"; exit 1; }
grep -q "AskTriadFallback" "$SRC" || { echo "FAIL: AskTriadFallback missing"; exit 1; }
grep -q "ResolveClaudeExe" "$SRC" || { echo "FAIL: ResolveClaudeExe missing"; exit 1; }
echo "OK: source contains fallback infrastructure"

echo "PASS: chat command with claude-fallback in place"
exit 0
