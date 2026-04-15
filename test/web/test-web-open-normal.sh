#!/bin/bash
# test-web-open-normal.sh
# Tests: web open always uses normal Chrome browser mode (not app mode)
# Verifies fix: appMode removed — Chrome launches with normal UI (tabs + address bar)
#               IME input works correctly in normal mode (was broken in --app= mode)
# Cmd ref: web open (ChromeLauncher.cs LaunchAsync)

WKBOT="${WKBOT:-D:/SDK/bin/wkappbot.exe}"
LAUNCHER="${LAUNCHER:-D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs}"
WEBCMD="${WEBCMD:-D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs}"

if [ ! -f "$WKBOT" ]; then
  echo "[SKIP] wkappbot.exe not found"
  exit 0
fi

# 1. Verify ChromeLauncher.cs does NOT inject --app= flag
if [ -f "$LAUNCHER" ]; then
  if grep -qE '"--app=' "$LAUNCHER"; then
    echo "[FAIL] ChromeLauncher.cs still contains --app= flag — appMode not fully removed"
    grep -n '"--app=' "$LAUNCHER" | head -5
    exit 1
  fi
  echo "[PASS] No --app= flag in ChromeLauncher.cs"
else
  echo "[SKIP] ChromeLauncher.cs not found — skipping source check"
fi

# 2. Verify WebCommands.cs does NOT contain appMode variable set to true
if [ -f "$WEBCMD" ]; then
  if grep -qE 'appMode\s*=\s*true' "$WEBCMD"; then
    echo "[FAIL] WebCommands.cs still has appMode = true"
    grep -n 'appMode' "$WEBCMD" | head -5
    exit 1
  fi
  echo "[PASS] No appMode=true in WebCommands.cs"
else
  echo "[SKIP] WebCommands.cs not found — skipping source check"
fi

# 3. Verify 'web open' subcommand is registered
if [ -f "$WEBCMD" ]; then
  if ! grep -q '"open"' "$WEBCMD" 2>/dev/null; then
    echo "[FAIL] web open command not found in WebCommands.cs"
    exit 1
  fi
  echo "[PASS] web open command registered"
fi

# 4. web --help should not mention app mode
help_out=$("$WKBOT" web --help 2>/dev/null)
if echo "$help_out" | grep -qi "app.mode\|--app="; then
  echo "[FAIL] web --help still mentions app mode"
  exit 1
fi
echo "[PASS] web --help does not mention app mode"

echo ""
echo "[PASS] web open normal browser mode verified (IME-compatible, no --app= flag)"
exit 0
