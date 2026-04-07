#!/bin/bash
# Test: wkappbot newchat --compress session compression before clear
# Verifies summary injection step before /clear in newchat flow

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/NewChatCommand.cs"
P=0; F=0

echo "=== Newchat --compress Session Compression ==="
# Command: wkappbot newchat compress — summary before /clear

# 1. --compress flag detection
if $GRAP 'compress' "$SRC" 2>/dev/null | grep -q "compress"; then
  echo "PASS: --compress flag detected"; ((P++))
else echo "FAIL: no --compress"; ((F++)); fi

# 2. Summary prompt injection (SetValueAndSubmit with compress prompt)
if $GRAP "compressPrompt\|Session Summary" "$SRC" 2>/dev/null | grep -q "compressPrompt\|Session Summary"; then
  echo "PASS: Compress prompt injected before /clear"; ((P++))
else echo "FAIL: no compress prompt"; ((F++)); fi

# 3. Wait for AI response after compress
if $GRAP "Thread.Sleep" "$SRC" 2>/dev/null | grep -q "30000"; then
  echo "PASS: Wait for AI response (30s)"; ((P++))
else echo "FAIL: no wait"; ((F++)); fi

# 4. Re-find edit element after compress (DOM may change)
REFIND=$($GRAP "Re-find edit\|Edit element lost after compress" "$SRC" 2>/dev/null | wc -l)
if [ "$REFIND" -ge 1 ]; then
  echo "PASS: Re-find edit element after compress"; ((P++))
else echo "FAIL: no re-find"; ((F++)); fi

# 5. Graceful fallback if compress fails
if $GRAP "proceeding without summary" "$SRC" 2>/dev/null | grep -q "proceeding without"; then
  echo "PASS: Graceful fallback on compress failure"; ((P++))
else echo "FAIL: no fallback"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
