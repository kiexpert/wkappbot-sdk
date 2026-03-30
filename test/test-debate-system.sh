#!/bin/bash
# Evidence: debate system implementation (uses wkappbot file grep)
PASS=0; FAIL=0
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands"
check() {
  result=$(wkappbot file grep "$2" --path "$SRC" --max 1 2>/dev/null)
  if echo "$result" | /w/SDK/bin/grap.exe "$2" 2>/dev/null | head -1 | grep -q .; then
    echo "PASS: $1"; PASS=$((PASS+1))
  else echo "FAIL: $1"; FAIL=$((FAIL+1)); fi
}
check "debate loop" "미합의"
check "debate STANCE" "STANCE"
check "debate JSON" "DEBATE_JSON"
check "moderator" "ModeratorEnabled"
check "DM inject" "InjectToSingle"
check "cross-prompt" "UpdateChunk"
echo "---"
echo "Results: $PASS PASS, $FAIL FAIL"
[ $FAIL -eq 0 ] && exit 0 || exit 1
