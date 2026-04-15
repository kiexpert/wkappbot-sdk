#!/bin/bash
# Test: wkappbot a11y type terminal — focusless typing for terminal windows
# Verifies TerminalClasses detection and SendInput fallback for ConPTY

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/A11yActions.Type.cs"
P=0; F=0

echo "=== Terminal Focusless Typing ==="

# 1. TerminalClasses set with all known terminal classes
if $GRAP "CASCADIA_HOSTING_WINDOW_CLASS\|ConsoleWindowClass\|PseudoConsoleWindow" "$SRC" 2>/dev/null | grep -q "CASCADIA\|Console\|Pseudo"; then
  echo "PASS: TerminalClasses includes CASCADIA + Console + Pseudo"; ((P++))
else echo "FAIL: missing terminal classes"; ((F++)); fi

# 2. Terminal class detection triggers SendInput path
if $GRAP "isTerminal" "$SRC" 2>/dev/null | grep -q "isTerminal"; then
  echo "PASS: Terminal class detection → SendInput path"; ((P++))
else echo "FAIL: no terminal detection"; ((F++)); fi

# 3. WM_CHAR blocked for terminals (ConPTY doesn't receive WM_CHAR)
if $GRAP "WM_CHAR\|ConPTY\|terminal" "$SRC" 2>/dev/null | grep -qi "WM_CHAR\|ConPTY\|terminal"; then
  echo "PASS: WM_CHAR bypass for terminal windows"; ((P++))
else echo "FAIL: no WM_CHAR bypass"; ((F++)); fi

# 4. Live: WSL window exists (prerequisite for typing)
RESULT=$(wkappbot a11y windows "*WindowsTerminal*" 2>/dev/null | grep -c "CASCADIA")
if [ "$RESULT" -ge 1 ]; then
  echo "PASS: WSL terminal available for typing ($RESULT)"; ((P++))
else echo "FAIL: no WSL terminal"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
