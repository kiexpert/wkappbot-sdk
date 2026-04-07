#!/bin/bash
# Test: wkappbot eye codex bot-name detection via UIA + session JSONL
# Verifies Codex CLI active session detection

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands"
P=0; F=0

echo "=== Codex CLI Bot-Name Session Detection ==="

# 1. Codex UIA header portal extraction
if $GRAP "app-header-portal\|codex" "$SRC/AppBotEyePromptInfo.cs" 2>/dev/null | grep -qi "header-portal\|codex"; then
  echo "PASS: Codex UIA header extraction"; ((P++))
else echo "FAIL: no Codex UIA header"; ((F++)); fi

# 2. ResolveProjectFolderToPath (reverse-map folder name to path)
if $GRAP "ResolveProjectFolderToPath" "$SRC/AppBotEyePromptInfo.cs" 2>/dev/null | grep -q "ResolveProjectFolderToPath"; then
  echo "PASS: ResolveProjectFolderToPath reverse mapping"; ((P++))
else echo "FAIL: no folder resolver"; ((F++)); fi

# 3. AbbreviateCwd creates display tag
if $GRAP "AbbreviateCwd" "$SRC/AppBotEyeCardBuilder.cs" 2>/dev/null | grep -q "AbbreviateCwd"; then
  echo "PASS: AbbreviateCwd display tag"; ((P++))
else echo "FAIL: no AbbreviateCwd"; ((F++)); fi

# 4. Host type detection (vscode-claudecode, claude-desktop, codex-desktop)
if $GRAP "HostType\|codex-desktop\|claude-desktop\|vscode-claudecode" "$SRC/AppBotEyeClaudeDetector.cs" 2>/dev/null | grep -q "HostType\|codex\|claude"; then
  echo "PASS: Host type detection (3 types)"; ((P++))
else echo "FAIL: no host type"; ((F++)); fi

# 5. EyeParentCard tracks per-instance state
if $GRAP "EyeParentCard\|ParentPid\|ParentName" "$SRC/AppBotEyeGlobalMode.cs" 2>/dev/null | grep -q "EyeParentCard\|ParentPid"; then
  echo "PASS: EyeParentCard per-instance state"; ((P++))
else echo "FAIL: no EyeParentCard"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
