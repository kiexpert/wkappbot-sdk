#!/usr/bin/env bash
# test-a11y-platforms.sh — cross-platform a11y access test suite
# Tests: Windows UIA, Chrome UIA, Chrome CDP (WebBot), Android ADB
# Usage: bash scripts/test-a11y-platforms.sh [WK_EXE]
set -uo pipefail

WK="${1:-D:/SDK/bin/wkappbot.exe}"
PASS=0; FAIL=0; SKIP=0

pass() { echo "[PASS] $1"; ((PASS++)); }
fail() { echo "[FAIL] $1"; ((FAIL++)); }
skip() { echo "[SKIP] $1"; ((SKIP++)); }
run() {
    local label="$1"; shift
    local out
    out=$("$WK" "$@" 2>&1 | tr -d '\0') || true
    echo "$out"
}

echo "=== WKAppBot Cross-Platform A11y Test ==="
echo "WK=$WK"
echo ""

# ═══ Windows UIA ═══════════════════════════════════════════════════════════
echo "── Windows UIA ──"

# T01: windows list (always available)
out=$(run "T01" a11y windows 2>&1 | tr -d '\0')
if echo "$out" | grep -qE '\[[0-9A-Fa-f]{6,8}\]'; then pass "T01 windows list"; else fail "T01 windows list — no hwnd found"; fi

# T02: inspect explorer/taskbar
out=$(run "T02" a11y inspect "*작업 표시줄*" --depth 2 2>&1 | tr -d '\0')
if [[ "$out" == *"[Button]"* ]] || [[ "$out" == *"[Pane]"* ]]; then pass "T02 inspect taskbar"; else skip "T02 inspect taskbar (not found)"; fi

# T03: inspect calculator (if running)
out=$(run "T03" a11y inspect "*계산기*" --depth 2 2>&1 | tr -d '\0')
if [[ "$out" == *"[Button]"* ]]; then pass "T03 inspect calculator"; else skip "T03 calculator not running"; fi

# ═══ Chrome UIA (native a11y) ═════════════════════════════════════════════
echo ""
echo "── Chrome UIA ──"

# T04: find Chrome windows
out=$(run "T04" a11y windows "*Chrome*" 2>&1 | tr -d '\0')
if [[ "$out" == *"Chrome"* ]]; then pass "T04 Chrome window found"; else skip "T04 Chrome not running"; fi

# T05: inspect Chrome UIA tree
out=$(run "T05" a11y inspect "*Chrome*" --depth 3 2>&1 | tr -d '\0')
if [[ "$out" == *"[Document]"* ]] || [[ "$out" == *"[Pane]"* ]]; then pass "T05 Chrome UIA inspect"; else skip "T05 Chrome UIA not accessible"; fi

# ═══ Chrome CDP (WebBot) ═════════════════════════════════════════════════
echo ""
echo "── Chrome CDP (WebBot) ──"

# T06: read with --eval-js via CDP (replaces deprecated a11y eval)
out=$(run "T06" a11y read "*Chrome*#body" --eval-js "document.title" --timeout 10 2>&1 | tr -d '\0')
if [[ "$out" == *"eval-js"* ]] || [[ "$out" == *"title"* ]] || [[ ${#out} -gt 50 ]]; then pass "T06 CDP --eval-js"; else skip "T06 CDP not available"; fi

# T07: CSS selector inspect (web a11y)
out=$(run "T07" a11y read "*Chrome*#body" --timeout 10 2>&1 | tr -d '\0')
if [[ "$out" == *"[READ]"* ]] || [[ ${#out} -gt 100 ]]; then pass "T07 CDP CSS read"; else skip "T07 CDP read not available"; fi

# ═══ Android ADB ═══════════════════════════════════════════════════════════
echo ""
echo "── Android ADB ──"

# T08: adb device connected?
adb_out=$(adb devices 2>&1)
if [[ "$adb_out" == *"device"* ]] && [[ "$adb_out" != *"List of devices attached"$'\n'$'\n'* ]]; then
    pass "T08 ADB device connected"

    # T09: adb windows list
    out=$(run "T09" a11y windows "adb:" --timeout 15 2>&1 | tr -d '\0')
    if [[ "$out" == *"adb"* ]] || [[ "$out" == *"package"* ]]; then pass "T09 ADB windows"; else fail "T09 ADB windows — no output"; fi

    # T10: adb inspect current app
    out=$(run "T10" a11y inspect "adb://" --depth 3 --timeout 15 2>&1 | tr -d '\0')
    if [[ "$out" == *"FrameLayout"* ]] || [[ "$out" == *"View"* ]] || [[ "$out" == *"Button"* ]] || [[ "$out" == *"android"* ]]; then
        pass "T10 ADB inspect"
    else
        fail "T10 ADB inspect — no android elements"
    fi

    # T11: adb inspect with grap filter
    pkg=$(adb shell "dumpsys activity activities | grep mResumedActivity" 2>&1 | grep -oP 'com\.\S+' | head -1 | tr -d '\r')
    if [[ -n "$pkg" ]]; then
        out=$(run "T11" a11y inspect "adb://*${pkg}*" --depth 2 --timeout 15 2>&1 | tr -d '\0')
        if [[ ${#out} -gt 50 ]]; then pass "T11 ADB inspect with package filter ($pkg)"; else fail "T11 ADB inspect filter — too short"; fi
    else
        skip "T11 no foreground package detected"
    fi
else
    skip "T08 no ADB device"
    skip "T09 no ADB device"
    skip "T10 no ADB device"
    skip "T11 no ADB device"
fi

# ═══ Summary ═══════════════════════════════════════════════════════════════
echo ""
echo "Results: $PASS passed, $FAIL failed, $SKIP skipped (total $((PASS + FAIL + SKIP)))"
[[ $FAIL -eq 0 ]]
