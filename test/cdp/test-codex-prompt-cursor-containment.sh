#!/usr/bin/env bash
set -euo pipefail

SRC="csharp/src/WKAppBot.Win32/Window/ClaudePromptHelper.HostFinders.cs"
CONTENT="$(<"$SRC")"
CORE="D:/SDK/bin/wkappbot-core.exe"
NEW_CORE="D:/SDK/bin/wkappbot-core.new.exe"

pass() { printf 'PASS: %s\n' "$1"; }
fail() { printf 'FAIL: %s\n' "$1"; exit 1; }

if [ -f "$NEW_CORE" ]; then
  CORE="$NEW_CORE"
  pass "using staged new core"
else
  pass "using installed core"
fi

case "$CONTENT" in
  *"Reject VS Code Codex Message input (cursor outside)"*)
    pass "codex message-input rejection log exists"
    ;;
  *)
    fail "missing codex message-input rejection log"
    ;;
esac

case "$CONTENT" in
  *"Reject VS Code Codex focus rect (cursor outside)"*)
    pass "codex focus-rect rejection log exists"
    ;;
  *)
    fail "missing codex focus-rect rejection log"
    ;;
esac

case "$CONTENT" in
  *"ContainsCurrentCursor(messageInputRect)"*"ContainsCurrentCursor(promptRect)"*)
    pass "codex prompt acceptance requires cursor containment"
    ;;
  *)
    fail "missing cursor containment gate for codex prompt"
    ;;
esac

PROBE="$("$CORE" prompt-probe --limit 2 --depth 1 2>&1 || true)"
printf '%s\n' "$PROBE"

case "$PROBE" in
  *"cursor="*|*"prompt.cursor"*)
    pass "prompt-probe emits cursor containment output"
    ;;
  *)
    fail "prompt-probe missing cursor containment output"
    ;;
esac
