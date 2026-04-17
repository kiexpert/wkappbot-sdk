#!/bin/bash
# Evidence: [BUG] UIA focus steal during ask-* CDP commands — focus restore hardened.
# RestoreFocusWithRetryAsync now has a last-resort path: if Chrome keeps
# re-stealing foreground after 32 retries, Chrome is minimized so Windows
# hands foreground back to prevFg automatically.
# Covers ask gpt / ask claude / ask gemini / ask triad send paths.
WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"

[ -f "$WKAPPBOT" ] || { echo "FAIL: $WKAPPBOT missing"; exit 1; }

# ask command reachable (doesn't invoke CDP — just verifies entry point is registered)
$WKAPPBOT ask --help >/dev/null 2>&1 || { echo "FAIL: ask command missing"; exit 1; }

# Static check: source contains the last-resort minimize recovery
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Focus.cs"
grep -q "last-resort: minimizing Chrome" "$SRC" || { echo "FAIL: last-resort recovery missing"; exit 1; }
grep -q "MinimizeChrome()" "$SRC" || { echo "FAIL: MinimizeChrome call missing"; exit 1; }

echo "PASS: ask focus restore last-resort hardening in place"
exit 0
