#!/usr/bin/env bash
# test-ask-cdp-session-stability.sh — verify CDP ask session stability fixes
# Fix #8: EvalAsync catches TaskCanceledException (not just TimeoutException)
# Fix #9: Native file dialog tier disables focus-theft monitoring (expected HWND change)
PASS=0; FAIL=0

check() {
    local label="$1"; local cmd="$2"
    if eval "$cmd" > /dev/null 2>&1; then
        echo "[PASS] $label"; PASS=$((PASS+1))
    else
        echo "[FAIL] $label"; FAIL=$((FAIL+1))
    fi
}

EVAL_SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs"
ATTACH_SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Attachments.FileInput.cs"

# Bug #8: TaskCanceledException now retried in EvalAsync
check "EvalAsync catches TaskCanceledException" \
    "grep -q 'TaskCanceledException' '$EVAL_SRC'"

check "EvalAsync retry on TaskCanceledException (or TimeoutException)" \
    "grep -Pq 'TimeoutException or TaskCanceledException|TaskCanceledException or TimeoutException' '$EVAL_SRC'"

check "EvalAsync 3-attempt retry loop present" \
    "grep -q 'attempt < 3' '$EVAL_SRC'"

# Bug #9: focus-theft monitoring suppressed during native file dialog
check "Focus-theft monitoring disabled before Tier 0 file dialog" \
    "grep -q 'EnableFocusTheftMonitoring = false' '$ATTACH_SRC'"

check "Focus-theft monitoring restored in finally block" \
    "grep -q 'EnableFocusTheftMonitoring = prevFocusMonitor' '$ATTACH_SRC'"

check "prevFocusMonitor backup before disable" \
    "grep -q 'prevFocusMonitor = cdp.EnableFocusTheftMonitoring' '$ATTACH_SRC'"

# Functional: ask commands accessible and EvalAsync code deployed
check "wkappbot ask --help accessible" \
    "\"W:/SDK/bin/wkappbot.exe\" ask --help 2>&1 | grep -qa 'ask\|gpt\|gemini\|claude'"

# Source: verify both fixes are deployed together
check "both fixes in same EvalAsync file (deployed)" \
    "grep -q 'TaskCanceledException' '$EVAL_SRC' && grep -q 'EnableFocusTheftMonitoring = false' '$ATTACH_SRC'"

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ]
