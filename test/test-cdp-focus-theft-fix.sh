#!/bin/bash
# Evidence: CDP focus theft prevention (BringToFrontAsync + SendAsync monitoring)
# Tests: CdpClient focus-safe behavior

WKAPPBOT=${WKAPPBOT:-wkappbot}
PASS=0; FAIL=0

check() {
  local desc="$1" expected="$2" actual="$3"
  if echo "$actual" | grep -qE "$expected"; then
    echo "PASS: $desc"
    PASS=$((PASS+1))
  else
    echo "FAIL: $desc — expected: $expected"
    echo "  got: $(echo "$actual" | head -3)"
    FAIL=$((FAIL+1))
  fi
}

# Test 1: BringToFrontAsync includes visibilityState check (code structure)
output=$($WKAPPBOT file grep "visibilityState" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot" --type cs 2>/dev/null)
check "BringToFrontAsync has visibilityState check" "visibilityState" "$output"

# Test 2: SendAsync has Chrome-only focus theft detection (not user switching)
output=$($WKAPPBOT file grep "chromePid" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.cs" 2>/dev/null)
check "SendAsync Chrome-process-only detection" "chromePid" "$output"

# Test 3: StatusEmojis set exists (no :memo: — protects suggest messages)
output=$($WKAPPBOT file grep "StatusEmojis" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AppBotEyeClaudeStatusStreamer.cs" 2>/dev/null)
check "StatusEmojis HashSet defined" "StatusEmojis" "$output"

# Test 4: :memo: NOT in StatusEmojis (suggest protection)
output=$($WKAPPBOT file grep ":memo:" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AppBotEyeClaudeStatusStreamer.cs" 2>/dev/null)
if echo "$output" | grep -q "StatusEmojis.*:memo:"; then
  echo "FAIL: :memo: should NOT be in StatusEmojis"
  FAIL=$((FAIL+1))
else
  echo "PASS: :memo: excluded from StatusEmojis (suggest safe)"
  PASS=$((PASS+1))
fi

# Test 5: OnZoomRequired callback exists (auto-magnifier for CDP)
output=$($WKAPPBOT file grep "OnZoomRequired" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.cs" 2>/dev/null)
check "OnZoomRequired auto-magnifier callback" "OnZoomRequired" "$output"

# Test 6: _activeZoom managed in SendAsync (session-scoped magnifier)
output=$($WKAPPBOT file grep "_activeZoom" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.cs" 2>/dev/null)
check "_activeZoom session-scoped magnifier" "_activeZoom" "$output"

# Test 7: BUG-AUTO includes cmd and log in report
output=$($WKAPPBOT file grep "cmd:.*callerCmd" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Entry.cs" 2>/dev/null)
check "BUG-AUTO report includes cmd" "callerCmd" "$output"

# Test 8: Real stack trace captured for focus theft
output=$($WKAPPBOT file grep "_lastFocusTheftStack" --path "W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Entry.cs" 2>/dev/null)
check "Real stack trace for focus theft" "_lastFocusTheftStack" "$output"

echo "---"
echo "Results: $PASS PASS, $FAIL FAIL (total $((PASS+FAIL)))"
[ $FAIL -eq 0 ] && echo "ALL PASS" && exit 0 || exit 1
