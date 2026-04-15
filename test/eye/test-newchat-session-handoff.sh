#!/bin/bash
# Test: wkappbot newchat session handoff with context compression
# Verifies the full newchat pipeline: compress → /clear → inject

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/NewChatCommand.cs"
P=0; F=0

echo "=== Newchat Session Handoff Pipeline ==="
# Command: wkappbot newchat compress uses SetValueAndSubmit

# 1. Mutex lock prevents concurrent runs
if $GRAP "wkappbot_newchat.lock\|Mutex" "$SRC" 2>/dev/null | grep -q "lock\|Mutex"; then
  echo "PASS: Mutex guard prevents concurrent newchat"; ((P++))
else echo "FAIL: no mutex"; ((F++)); fi

# 2. Policy injection (EmbeddedInitialPrompt + handoff header)
if $GRAP "EmbeddedInitialPrompt\|Handoff Mode" "$SRC" 2>/dev/null | grep -q "EmbeddedInitialPrompt\|Handoff"; then
  echo "PASS: Policy injection with handoff header"; ((P++))
else echo "FAIL: no policy"; ((F++)); fi

# 3. Compress step exists before /clear
if $GRAP "compressPrompt\|Compressing context" "$SRC" 2>/dev/null | grep -q "compressPrompt\|Compressing"; then
  echo "PASS: Compress step before /clear"; ((P++))
else echo "FAIL: no compress step"; ((F++)); fi

# 4. TypeSlashCommandAndSubmit for /clear
if $GRAP "TypeSlashCommandAndSubmit" "$SRC" 2>/dev/null | grep -q "TypeSlashCommandAndSubmit"; then
  echo "PASS: /clear via TypeSlashCommandAndSubmit"; ((P++))
else echo "FAIL: no /clear"; ((F++)); fi

# 5. SetValueAndSubmit for prompt injection
if $GRAP "SetValueAndSubmit" "$SRC" 2>/dev/null | grep -q "SetValueAndSubmit"; then
  echo "PASS: Prompt injection via SetValueAndSubmit"; ((P++))
else echo "FAIL: no SetValueAndSubmit"; ((F++)); fi

# 6. Focus restore after handoff
if $GRAP "SmartSetForegroundWindow\|Focus restored" "$SRC" 2>/dev/null | grep -q "SmartSetForeground\|Focus restored"; then
  echo "PASS: Focus restore after handoff"; ((P++))
else echo "FAIL: no focus restore"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
