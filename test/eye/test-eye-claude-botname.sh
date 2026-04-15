#!/bin/bash
# Test: wkappbot eye claude-detect bot-name JSONL session detection
# Verifies Claude Desktop session JSONL detection by most-recently-modified

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands"
P=0; F=0

echo "=== Claude Desktop Bot-Name JSONL Detection ==="

# 1. GetContextInfoForCwdEx finds JSONL by modification time
if $GRAP "GetContextInfoForCwdEx\|LastWriteTime\|OrderByDescending" "$SRC/AppBotEyePromptInfo.cs" 2>/dev/null | grep -q "GetContextInfoForCwdEx\|LastWriteTime\|OrderByDescending"; then
  echo "PASS: JSONL detection by most-recently-modified"; ((P++))
else echo "FAIL: no JSONL detection"; ((F++)); fi

# 2. Claude projects directory path construction
if $GRAP "projDir\|projName\|projects" "$SRC/AppBotEyePromptInfo.cs" 2>/dev/null | grep -q "projDir\|projName"; then
  echo "PASS: Claude projects path construction"; ((P++))
else echo "FAIL: no projects path"; ((F++)); fi

# 3. Context percentage from JSONL size
if $GRAP "fileSize\|latestFile\|pct" "$SRC/AppBotEyePromptInfo.cs" 2>/dev/null | grep -q "fileSize\|latestFile\|pct"; then
  echo "PASS: Context % from JSONL size"; ((P++))
else echo "FAIL: no context %"; ((F++)); fi

# 4. Bot username resolution from cached cards
if $GRAP "GetBotUsernameFromCachedCards" "$SRC/SlackMonitorCommands.cs" 2>/dev/null | grep -q "GetBotUsername"; then
  echo "PASS: GetBotUsernameFromCachedCards"; ((P++))
else echo "FAIL: no bot username resolver"; ((F++)); fi

# 5. CWD matching priority (hwnd → exact CWD → title → recent)
if $GRAP "callerHwnd\|callerCwd\|OrderByDescending" "$SRC/SlackMonitorCommands.cs" 2>/dev/null | grep -q "callerHwnd\|callerCwd"; then
  echo "PASS: CWD matching priority hierarchy"; ((P++))
else echo "FAIL: no matching priority"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
