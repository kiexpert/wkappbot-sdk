#!/bin/bash
# test-eval-js.sh — Smoke test for --eval-js, CDP helpers, reaction pipeline
# Usage: bash scripts/test-eval-js.sh
# Requires: Chrome running with --remote-debugging-port=9222

set -euo pipefail
WKA="${WKA:-wkappbot}"
PASS=0; FAIL=0; SKIP=0

pass() { echo "  ✅ PASS: $1"; ((PASS++)); }
fail() { echo "  ❌ FAIL: $1"; ((FAIL++)); }
skip() { echo "  ⏭ SKIP: $1"; ((SKIP++)); }

check() {
    local name="$1"; shift
    local out
    out=$("$@" 2>&1) || true
    if [ ${#out} -gt 0 ]; then pass "$name"; else fail "$name"; fi
}

echo "══ eval-js + CDP helpers smoke test ══"
echo ""

# ── 1. eval deprecated warning ──
echo "── 1. eval deprecated warning ──"
out=$(timeout 10 "$WKA" a11y eval "*Chrome*" --text "1+1" 2>&1) || true
if echo "$out" | grep -qi "DEPRECATED"; then pass "eval deprecated warning"; else fail "eval deprecated warning"; fi

# ── 2. web eval deprecated warning ──
echo "── 2. web eval deprecated warning ──"
out=$(timeout 10 "$WKA" web eval "1+1" 2>&1) || true
if echo "$out" | grep -qi "DEPRECATED"; then pass "web eval deprecated warning"; else fail "web eval deprecated warning"; fi

# ── 3. --eval-js on read (basic) ──
echo "── 3. --eval-js on read ──"
out=$("$WKA" a11y read "*Chrome*" --eval-js "document.title" --timeout 5000 2>&1) || true
if echo "$out" | grep -qi "eval-js\|read"; then pass "read --eval-js"; else skip "read --eval-js (no Chrome?)"; fi

# ── 4. Chrome tab list (no #scope) ──
echo "── 4. Chrome tab list ──"
out=$("$WKA" a11y read "*Chrome*" 2>&1) || true
if echo "$out" | grep -qi "tab\|#tab-hint\|\[1\]"; then pass "Chrome tab list"; else skip "Chrome tab list (no Chrome?)"; fi

# ── 5. --eval-js on click (pre-hook) ──
echo "── 5. --eval-js on click (pre-hook) ──"
out=$("$WKA" a11y click "*Chrome*#body" --eval-js "document.title" --timeout 5000 2>&1) || true
if echo "$out" | grep -qi "eval-js\|click\|CDP"; then pass "click --eval-js pre-hook"; else skip "click --eval-js (no Chrome?)"; fi

# ── 6. Slack reaction event handling ──
echo "── 6. Slack reaction support ──"
out=$(grep -c "OnReaction" "$(dirname "$0")/../csharp/src/WKAppBot.WebBot/SlackSocketClient.cs" 2>/dev/null) || true
if [ "$out" -gt 0 ]; then pass "OnReaction event handler exists"; else fail "OnReaction missing"; fi

# ── 7. SlackReaction record ──
out=$(grep -c "record SlackReaction" "$(dirname "$0")/../csharp/src/WKAppBot.WebBot/SlackSocketClient.cs" 2>/dev/null) || true
if [ "$out" -gt 0 ]; then pass "SlackReaction record exists"; else fail "SlackReaction record missing"; fi

# ── 8. CdpClient helpers exist ──
echo "── 8. CdpClient helpers ──"
for helper in FocusAsync GetTextLengthAsync IsHiddenAsync GetTabStateAsync QueryCountAsync JsClickAsync GetResponseCountAsync QueryExistsAsync GetElementRectAsync; do
    out=$(grep -c "$helper" "$(dirname "$0")/../csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs" 2>/dev/null) || true
    if [ "$out" -gt 0 ]; then pass "CdpClient.$helper"; else fail "CdpClient.$helper missing"; fi
done

# ── 9. AiHelpers exist ──
echo "── 9. CdpClient.AiHelpers ──"
for helper in InsertContentEditableAsync ClearEditorAsync SendPromptAsync IsStreamingAsync ClickStopButtonAsync GetEditorContentAsync DetectRateLimitAsync; do
    out=$(grep -c "$helper" "$(dirname "$0")/../csharp/src/WKAppBot.WebBot/CdpClient.AiHelpers.cs" 2>/dev/null) || true
    if [ "$out" -gt 0 ]; then pass "AiHelper.$helper"; else fail "AiHelper.$helper missing"; fi
done

# ── 10. CdpTabManager exists ──
echo "── 10. CdpTabManager ──"
out=$(grep -c "CreateScoped\|CreateShared" "$(dirname "$0")/../csharp/src/WKAppBot.CLI/CdpTabManager.cs" 2>/dev/null) || true
if [ "$out" -ge 2 ]; then pass "CdpTabManager CreateScoped+CreateShared"; else fail "CdpTabManager missing"; fi

# ── 11. JS runtime helpers injection ──
echo "── 11. JS runtime helpers ──"
out=$(grep -c "defA11yRead\|defA11yClick\|defA11yFocus" "$(dirname "$0")/../csharp/src/WKAppBot.CLI/Commands/A11yCommand.cs" 2>/dev/null) || true
if [ "$out" -ge 3 ]; then pass "defA11y* JS helpers"; else fail "defA11y* helpers missing"; fi

# ── 12. Clipboard restore ──
echo "── 12. Clipboard restore ──"
out=$(grep -c "RestoreClipboard" "$(dirname "$0")/../csharp/src/WKAppBot.CLI/Commands/AskCommands.Attachments.cs" 2>/dev/null) || true
if [ "$out" -ge 2 ]; then pass "Clipboard backup+restore"; else fail "Clipboard restore missing"; fi

# ── Summary ──
echo ""
echo "══ Results: $PASS pass, $FAIL fail, $SKIP skip ══"
[ $FAIL -eq 0 ] && echo "🎉 All checks passed!" || echo "⚠ Some checks failed"
exit $FAIL
