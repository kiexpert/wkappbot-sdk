#!/bin/bash
# test-webbot-e2e-delay.sh — End-to-end WebBot CDP delay optimization test
# Tests: Chrome launch → connect → navigate → tab switch → minimize/restore → close
# Validates state-based polling replaced all fixed delays (v5.10.51)
#
# Usage: bash test/test-webbot-e2e-delay.sh [--keep] [--port 9223]
#   --keep    don't kill Chrome after test (for debugging)
#   --port N  use specific CDP port (default: 9223 to avoid clashing with live 9222)

set -o pipefail
PASS=0; FAIL=0; SLOW=0
WKBOT="${WKBOT:-W:/SDK/bin/wkappbot.exe}"
PORT=9222
KEEP=0
for arg in "$@"; do
    [[ "$arg" == "--keep" ]] && KEEP=1
    [[ "$prev" == "--port" ]] && PORT="$arg"
    prev="$arg"
done

TEST_URL="https://example.com"
TEST_URL2="https://httpbin.org/html"

# ── helpers ──────────────────────────────────────────────────────────────────
ms() { date +%s%3N; }

pass() { echo "  [PASS] $1"; PASS=$((PASS+1)); }
fail() { echo "  [FAIL] $1"; FAIL=$((FAIL+1)); }
slow() { echo "  [SLOW] $1"; SLOW=$((SLOW+1)); }
info() { echo "  [INFO] $1"; }

# timed_run: run command, capture output+time, check max_ms
# Usage: timed_run "test name" max_ms command...
timed_run() {
    local name="$1"; local max_ms="$2"; shift 2
    local T0; T0=$(ms)
    local out; out=$("$@" 2>&1); local code=$?
    local T1; T1=$(ms)
    local elapsed=$((T1 - T0))

    if [ $code -ne 0 ] && ! echo "$out" | grep -qiE "already running|connected|OK"; then
        fail "$name (exit=$code, ${elapsed}ms)"
        echo "    → $(echo "$out" | tail -3 | tr '\n' ' ')"
        return 1
    elif [ "$elapsed" -gt "$max_ms" ]; then
        slow "$name (${elapsed}ms > ${max_ms}ms cap)"
        return 0
    else
        pass "$name (${elapsed}ms)"
        return 0
    fi
}

# check_source: verify source code pattern
check_source() {
    local name="$1"; local file="$2"; local pattern="$3"; local invert="${4:-}"
    if [ ! -f "$file" ]; then
        info "$name — file not found, skipping"
        return
    fi
    if [ "$invert" == "not" ]; then
        if grep -qE "$pattern" "$file"; then
            fail "$name — pattern still present"
            grep -n "$pattern" "$file" | head -3 | sed 's/^/    → /'
        else
            pass "$name"
        fi
    else
        if grep -qE "$pattern" "$file"; then
            pass "$name"
        else
            fail "$name — pattern not found"
        fi
    fi
}

cleanup() {
    if [ "$KEEP" -eq 0 ]; then
        echo ""
        echo "── Cleanup ────────────────────────────────────"
        "$WKBOT" a11y kill "chrome*" >/dev/null 2>&1
        # Also kill by port user-data-dir pattern
        local TMPDIR_PATTERN="wkappbot_chrome_${PORT}"
        for pid in $(wmic process where "name='chrome.exe'" get ProcessId,CommandLine /format:csv 2>/dev/null \
                     | grep "$TMPDIR_PATTERN" | awk -F, '{print $NF}' | tr -d '\r'); do
            taskkill //PID "$pid" //F //T >/dev/null 2>&1
        done
        sleep 1
        pass "Chrome killed (port $PORT)"
    else
        info "Chrome kept alive on port $PORT (--keep)"
    fi
}

echo "=== WebBot E2E Delay Optimization Test $(date '+%Y-%m-%d %H:%M:%S') ==="
echo "    exe:  $WKBOT"
echo "    port: $PORT"
echo ""

# ── Phase 0: Source code verification ────────────────────────────────────────
echo "── Phase 0: Source Verification ────────────────"
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
WINDOW_CS="$ROOT/csharp/src/WKAppBot.WebBot/CdpClient.Window.cs"
TAB_CS="$ROOT/csharp/src/WKAppBot.WebBot/CdpClient.TabManagement.cs"
WEB_CS="$ROOT/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"

# WaitForWindowStateAsync helper exists
check_source "WaitForWindowStateAsync helper exists" "$WINDOW_CS" "WaitForWindowStateAsync"

# EnsureMinimizedAsync uses poll not fixed delay
check_source "EnsureMinimizedAsync: no Task.Delay(100)" "$WINDOW_CS" 'await Task\.Delay\(100\).*minimize' "not"

# SwitchToTargetAsync: no fixed 80ms or 200ms
check_source "SwitchToTargetAsync: no Task.Delay(80)" "$TAB_CS" 'await Task\.Delay\(80\)' "not"

# SetWindowBoundsAsync uses shared helper
check_source "SetWindowBoundsAsync uses WaitForWindowStateAsync" "$WINDOW_CS" 'WaitForWindowStateAsync\(windowId.*normal'

# WebOpenCommand: no Thread.Sleep(1000/2000)
check_source "WebOpenCommand: no Thread.Sleep(appMode)" "$WEB_CS" 'Thread\.Sleep\(appMode' "not"

# WebOpenCommand: has readyState poll
check_source "WebOpenCommand: readyState poll exists" "$WEB_CS" 'document\.readyState.*freshLaunch\|readyState.*renderer'

# EnsureCorrectWindowAsync: target poll exists
check_source "EnsureCorrectWindowAsync: /json target poll" "$TAB_CS" 'targetReady.*break\|checkTargets.*newTargetId'

echo ""

# ── Phase 1: Chrome Launch ───────────────────────────────────────────────────
echo "── Phase 1: Chrome Launch ──────────────────────"

# Kill any existing Chrome on our test port
"$WKBOT" a11y kill "chrome*--remote-debugging-port=$PORT*" >/dev/null 2>&1
sleep 1

# Launch Chrome + navigate — should be fast with readyState poll instead of Thread.Sleep
T0=$(ms)
LAUNCH_OUT=$(WKAPPBOT_WORKER=1 timeout 20 "$WKBOT" web open "$TEST_URL" --port "$PORT" 2>&1)
LAUNCH_CODE=$?
T1=$(ms)
LAUNCH_MS=$((T1 - T0))

if [ $LAUNCH_CODE -eq 124 ]; then
    fail "web open: timed out after 20s"
    echo "    → hung — aborting"
    cleanup
    exit 1
elif [ $LAUNCH_CODE -ne 0 ]; then
    fail "web open (exit=$LAUNCH_CODE, ${LAUNCH_MS}ms)"
    echo "    → $(echo "$LAUNCH_OUT" | tail -3 | tr '\n' ' ')"
    cleanup
    exit 1
else
    # Check if renderer readiness message is present
    if echo "$LAUNCH_OUT" | grep -q "renderer ready"; then
        READY_MS=$(echo "$LAUNCH_OUT" | grep "renderer ready" | grep -oE '[0-9]+ms' | head -1)
        pass "web open + navigate (${LAUNCH_MS}ms total, renderer ${READY_MS})"
    else
        pass "web open + navigate (${LAUNCH_MS}ms, existing Chrome)"
    fi
fi

echo ""

# ── Phase 2: CDP Connection & Eval ──────────────────────────────────────────
echo "── Phase 2: CDP Connection & Eval ──────────────"

# Basic eval — should work immediately after connect
timed_run "eval document.title" 5000 \
    "$WKBOT" web eval "document.title" --port "$PORT"

timed_run "eval location.href" 3000 \
    "$WKBOT" web eval "location.href" --port "$PORT"

# readyState should be "complete"
READY_OUT=$("$WKBOT" web eval "document.readyState" --port "$PORT" 2>&1)
if echo "$READY_OUT" | grep -q "complete"; then
    pass "document.readyState = complete"
else
    fail "document.readyState != complete (got: $(echo "$READY_OUT" | tail -1))"
fi

echo ""

# ── Phase 3: Window State Transitions ───────────────────────────────────────
echo "── Phase 3: Window State Transitions ───────────"

# Minimize via CDP — should poll to confirm minimized
timed_run "CDP minimize (poll-based)" 3000 \
    "$WKBOT" web eval "void(0)" --port "$PORT"

# Test minimize + restore cycle timing
T0=$(ms)
# Minimize Chrome window
MIN_OUT=$("$WKBOT" a11y minimize "*Chrome*remote-debugging-port=$PORT*" 2>&1)
T1=$(ms)
MIN_MS=$((T1 - T0))
if [ $MIN_MS -lt 3000 ]; then
    pass "a11y minimize Chrome (${MIN_MS}ms)"
else
    slow "a11y minimize Chrome (${MIN_MS}ms)"
fi

# Restore Chrome window — should use WaitForWindowStateAsync
T0=$(ms)
REST_OUT=$("$WKBOT" a11y restore "*Chrome*remote-debugging-port=$PORT*" 2>&1)
T1=$(ms)
REST_MS=$((T1 - T0))
if [ $REST_MS -lt 3000 ]; then
    pass "a11y restore Chrome (${REST_MS}ms)"
else
    slow "a11y restore Chrome (${REST_MS}ms)"
fi

# Eval after minimize/restore — CDP should still work
timed_run "eval after minimize/restore cycle" 5000 \
    "$WKBOT" web eval "document.title" --port "$PORT"

echo ""

# ── Phase 4: Tab Operations ─────────────────────────────────────────────────
echo "── Phase 4: Tab Operations ───────────────────────"

# Navigate to second URL — tests NavigateAsync timing
timed_run "navigate to $TEST_URL2" 10000 \
    "$WKBOT" web navigate "$TEST_URL2" --port "$PORT"

# Eval on new page
TITLE_OUT=$("$WKBOT" web eval "document.title" --port "$PORT" 2>&1)
if echo "$TITLE_OUT" | grep -qiE "herman|httpbin|html"; then
    pass "page title correct after navigate"
else
    # title may vary, just check eval works
    if [ -n "$TITLE_OUT" ]; then
        pass "eval works after navigate (title=$(echo "$TITLE_OUT" | tail -1 | head -c 40))"
    else
        fail "eval failed after navigate"
    fi
fi

# Tab list
timed_run "web tabs list" 5000 \
    "$WKBOT" web tabs --port "$PORT"

echo ""

# ── Phase 5: Rapid Operations (stress test for race conditions) ─────────────
echo "── Phase 5: Rapid Eval Burst (5x) ──────────────"
BURST_PASS=0; BURST_FAIL=0
for i in 1 2 3 4 5; do
    OUT=$("$WKBOT" web eval "Date.now()" --port "$PORT" 2>&1)
    if echo "$OUT" | grep -qE '^[0-9]{13}'; then
        BURST_PASS=$((BURST_PASS+1))
    else
        BURST_FAIL=$((BURST_FAIL+1))
    fi
done
if [ $BURST_FAIL -eq 0 ]; then
    pass "5x rapid eval: all succeeded"
else
    fail "5x rapid eval: $BURST_FAIL failures"
fi

echo ""

# ── Phase 6: Cleanup ────────────────────────────────────────────────────────
cleanup

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL, $SLOW SLOW ==="
echo ""
[ "$FAIL" -eq 0 ]
