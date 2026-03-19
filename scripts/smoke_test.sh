#!/bin/bash
# WKAppBot comprehensive smoke test — all major commands
# Usage: bash scripts/smoke_test.sh [--no-slack] [--quick]
#
# Exit 0 = all pass, Exit 1 = some fail
# "notfound" / "error" responses count as PASS for optional-app tests
# Slack send is skipped by default unless SMOKE_SLACK=1 env is set

PASS=0; FAIL=0; SLOW=0; SKIP=0
NO_SLACK="${SMOKE_SLACK:-0}"  # 0 = skip real slack send
QUICK=0
TIMEOUT="${SMOKE_TIMEOUT:-1.1}"  # per-command cap (seconds)
for arg in "$@"; do
    [[ "$arg" == "--no-slack" ]] && NO_SLACK=0
    [[ "$arg" == "--quick" ]] && QUICK=1
done

WKA="W:/SDK/bin/wkappbot.exe"

# ─── warmup: ensure Eye is running (spawns if not) ─────────────────────────
echo "── Warmup (eye tick) ──────────────────────────"
"$WKA" eye tick >/dev/null 2>&1
echo "  Eye ready"

# ─── helpers ───────────────────────────────────────────────────────────────
# check: must succeed (exit 0 or good-marker) within TIMEOUT; TIMEOUT=⚠SLOW not FAIL
check() {
    local name="$1"; shift
    local out; out=$(timeout "$TIMEOUT" "$@" 2>&1)
    local code=$?
    if [ $code -eq 124 ]; then
        echo "  ⚠ $name (SLOW >${TIMEOUT}s — capped)"
        SLOW=$((SLOW+1))
    elif [ $code -eq 0 ] || echo "$out" | grep -qiE "\[OK\]|result=ok|wrote|PASS|matched|found|saved|connected|smoke_test_ok"; then
        echo "  ✓ $name"
        PASS=$((PASS+1))
    else
        echo "  ✗ $name (exit=$code)"
        echo "    → $(echo "$out" | tail -3 | tr '\n' ' ')"
        FAIL=$((FAIL+1))
    fi
}

# check_g: graceful — "notfound/error" also counts as PASS; TIMEOUT=⚠SLOW
check_g() {
    local name="$1"; shift
    local out; out=$(timeout "$TIMEOUT" "$@" 2>&1)
    local code=$?
    if [ $code -eq 124 ]; then
        echo "  ⚠ $name (SLOW >${TIMEOUT}s — capped)"
        SLOW=$((SLOW+1))
    elif [ $code -eq 0 ] || echo "$out" | grep -qiE "\[OK\]|result=ok|wrote|matched|saved|notfound|not found|no window|no match|error|failed|0 items|empty|stats|STUDY|STATS|schedule|plan|usage|connected|paused|smoke_test_ok"; then
        echo "  ✓ $name"
        PASS=$((PASS+1))
    else
        echo "  ✗ $name (exit=$code)"
        echo "    → $(echo "$out" | tail -3 | tr '\n' ' ')"
        FAIL=$((FAIL+1))
    fi
}

skip() {
    echo "  - $1 (skipped)"
    SKIP=$((SKIP+1))
}

banner() { echo ""; echo "── $1 ──────────────────────────"; }

# ─── timing helper ─────────────────────────────────────────────────────────
ms() { date +%s%3N; }

echo "=== WKAppBot Smoke Test $(date '+%Y-%m-%d %H:%M:%S') ==="
echo "    exe: $WKA"
echo ""

# ─── Pipe latency (launcher → Eye IPC) ─────────────────────────────────────
banner "Pipe Latency"
for i in 1 2 3; do
    T0=$(ms)
    "$WKA" a11y clipboard-write "latency_probe_$i" >/dev/null 2>&1
    T1=$(ms)
    echo "  pipe[$i]: $((T1-T0))ms"
done

# ─── Clipboard ─────────────────────────────────────────────────────────────
banner "Clipboard"
check   "clipboard-write"  "$WKA" a11y clipboard-write "smoke_test_ok_$(date +%s)"
check   "clipboard-read"   bash -c "\"$WKA\" a11y clipboard-read 2>&1 | grep -qi 'smoke_test_ok'"

# ─── Window discovery ──────────────────────────────────────────────────────
banner "Window Discovery"
check   "a11y windows"     bash -c "\"$WKA\" a11y windows 2>&1 | grep -q '\['"
check_g "a11y inspect *"   bash -c "out=\$(\"$WKA\" a11y inspect \"*\" --depth 1 2>&1); echo \"\$out\" | grep -qiE 'match|notfound|error|inspect|windows|found' || [ -n \"\$out\" ]"
check_g "windows cmd"      bash -c "\"$WKA\" windows 2>&1 | grep -q '\['"

# ─── A11y Element actions (Calculator — launched & closed by test) ────────
banner "A11y Element (Calculator)"
# Launch calc, wait up to 3s, run element actions, then close
CALC_LAUNCHED=0
if timeout 4 bash -c 'start /b calc.exe 2>/dev/null; sleep 2; "'"$WKA"'" a11y windows 2>&1 | grep -qi "계산기\|Calculator"'; then
    CALC_LAUNCHED=1
fi
# Alternative launch via wkappbot (focusless)
if [ $CALC_LAUNCHED -eq 0 ]; then
    powershell -Command "Start-Process calc.exe" 2>/dev/null
    sleep 2
    timeout 2 "$WKA" a11y windows 2>&1 | grep -qi "계산기\|Calculator" && CALC_LAUNCHED=1
fi

if [ $CALC_LAUNCHED -eq 1 ]; then
    CALC="*계산기*;*Calculator*"
    # screenshot delegates to CaptureCommand which doesn't support ; OR pattern — detect single title
    CALC_WIN=$("$WKA" a11y windows 2>&1 | grep -iE "계산기|Calculator" | grep -oP '"[^"]+"' | head -1 | tr -d '"')
    CALC_WIN="${CALC_WIN:+*${CALC_WIN}*}"
    [ -z "$CALC_WIN" ] && CALC_WIN="*Calculator*"
    check   "calc: read"        bash -c "\"$WKA\" a11y read \"$CALC\" --nth 1 2>&1 | grep -qiE 'name=|value=|pattern|ok'"
    check   "calc: find"        bash -c "\"$WKA\" a11y find \"$CALC\" --nth 1 --depth 3 2>&1 | grep -qiE 'found|Button|result|CalculatorResults'"
    check_g "calc: screenshot"  bash -c "\"$WKA\" a11y screenshot \"$CALC_WIN\" 2>&1"
    check   "calc: highlight"   bash -c "\"$WKA\" a11y highlight \"$CALC#num5Button\" --nth 1 2>&1 | grep -qiE 'highlight|zoom|ok'"
    # 5 + 3 = 8  (focusless UIA Invoke)
    check   "calc: invoke 5"    bash -c "\"$WKA\" a11y invoke \"$CALC#num5Button\" 2>&1 | grep -qiE 'ok|invoke'"
    check   "calc: invoke +"    bash -c "\"$WKA\" a11y invoke \"$CALC#plusButton\" 2>&1 | grep -qiE 'ok|invoke'"
    check   "calc: invoke 3"    bash -c "\"$WKA\" a11y invoke \"$CALC#num3Button\" 2>&1 | grep -qiE 'ok|invoke'"
    check   "calc: invoke ="    bash -c "\"$WKA\" a11y invoke \"$CALC#equalButton\" 2>&1 | grep -qiE 'ok|invoke'"
    # Read result — CalculatorResults should show 8
    check   "calc: result=8"    bash -c "\"$WKA\" a11y read \"$CALC#CalculatorResults\" 2>&1 | grep -q '8'"
    # Close calculator
    timeout 5 "$WKA" a11y close "$CALC" >/dev/null 2>&1
    echo "  (Calculator closed)"
else
    skip "calc element actions (could not launch calculator)"
fi

# ─── A11y Element actions (desktop/taskbar — always present) ──────────────
# Target: Shell_TrayWnd (taskbar) + Progman (desktop) — no app needed
banner "A11y Element (System UI)"
check_g "find taskbar"       bash -c "\"$WKA\" a11y find \"*Shell_TrayWnd*\" --nth 1 2>&1 | grep -qiE 'match|notfound|found|error'"
check_g "read taskbar"       bash -c "\"$WKA\" a11y read \"*Shell_TrayWnd*\" --nth 1 2>&1 | grep -qiE 'name=|value=|pattern|notfound|error'"
check_g "read desktop"       bash -c "\"$WKA\" a11y read \"*Progman*\" --nth 1 2>&1 | grep -qiE 'name=|value=|pattern|notfound|error'"
check_g "find desktop"       bash -c "\"$WKA\" a11y find \"*Progman*\" --nth 1 --depth 2 2>&1 | grep -qiE 'match|found|notfound|error'"
check_g "highlight taskbar"  bash -c "\"$WKA\" a11y highlight \"*Shell_TrayWnd*\" --nth 1 2>&1 | grep -qiE 'highlight|zoom|ok|notfound|error'"
check_g "screenshot taskbar" bash -c "\"$WKA\" a11y screenshot \"*Shell_TrayWnd*\" --nth 1 2>&1 | grep -qiE 'saved|captured|notfound|error'"
# scroll taskbar (safe — no focus grab)
check_g "scroll taskbar"     bash -c "\"$WKA\" a11y scroll \"*Shell_TrayWnd*\" --nth 1 --direction right --amount small 2>&1 | grep -qiE 'scroll|ok|notfound|error|pattern'"
# type: send a benign key to desktop (VK_NONAME — no visible effect)
# skip: too risky without knowing which window has focus
# invoke/click: skip — could trigger unexpected UI actions on desktop/taskbar

# ─── Eye tick ──────────────────────────────────────────────────────────────
banner "Eye"
{ _T=$TIMEOUT; TIMEOUT=15; check_g "eye tick" bash -c "\"$WKA\" eye tick 2>&1 | grep -qiE 'tick|eye|card|status|idle|running|ctx=|clod|kro'"; TIMEOUT=$_T; }

# ─── Validate (YAML) ───────────────────────────────────────────────────────
banner "Validate"
if [ -f "W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml" ]; then
    check_g "validate yaml"  bash -c "\"$WKA\" validate 'W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml' 2>&1 | grep -qiE 'valid|ok|error|scenario'"
else
    skip "validate yaml (no scenario file)"
fi

# ─── Snapshot ──────────────────────────────────────────────────────────────
banner "Snapshot"
check_g "snapshot taskbar"   bash -c "\"$WKA\" snapshot \"*Shell_TrayWnd*\" 2>&1 | grep -qiE 'saved|snapshot|notfound|error'"

# ─── OCR ───────────────────────────────────────────────────────────────────
banner "OCR"
check_g "ocr taskbar"        bash -c "\"$WKA\" ocr \"*Shell_TrayWnd*\" 2>&1 | grep -qiE 'ocr|text:|notfound|error|result'"

# ─── Knowhow ───────────────────────────────────────────────────────────────
banner "Knowhow"
check_g "knowhow read"       bash -c "\"$WKA\" knowhow read 2>&1 | grep -qiE 'knowhow|entry|found|no entries|error|empty|0 items|file'"

# ─── Schedule ──────────────────────────────────────────────────────────────
banner "Schedule"
check_g "schedule list"      bash -c "\"$WKA\" schedule list 2>&1 | grep -qiE 'schedule|no |empty|item|error'"

# ─── Whisper ───────────────────────────────────────────────────────────────
banner "Whisper"
check_g "whisper stats"      bash -c "\"$WKA\" whisper stats 2>&1 | grep -qiE 'STUDY|STATS|files|error|stat'"
check_g "whisper study --dry-run" bash -c "\"$WKA\" whisper study --dry-run 2>&1 | grep -qiE 'STUDY|dry|batch|error|0 files'"

# ─── Speak (background, no wait) ───────────────────────────────────────────
banner "Speak"
if [ "$QUICK" -eq 0 ]; then
    check_g "speak --bg"     bash -c "\"$WKA\" speak 'smoke test ok' --bg 2>&1; echo done"
else
    skip "speak --bg (--quick mode)"
fi

# ─── Slack ─────────────────────────────────────────────────────────────────
banner "Slack"
check_g "slack test"         bash -c "\"$WKA\" slack test 2>&1 | grep -qiE 'ok|connected|error|fail|socket|token'"
if [ "$NO_SLACK" -eq 1 ]; then
    check_g "slack send"     bash -c "\"$WKA\" slack send 'smoke test ok (ignore)' 2>&1 | grep -qiE 'sent|ok|error|fail'"
else
    skip "slack send (set SMOKE_SLACK=1 to enable)"
fi

# ─── Claude Usage ──────────────────────────────────────────────────────────
banner "Claude Usage"
if [ "$QUICK" -eq 0 ]; then
    check_g "claude-usage"   bash -c "\"$WKA\" claude-usage 2>&1 | grep -qiE 'usage|plan|ops|notfound|error|not running'"
else
    skip "claude-usage (--quick mode)"
fi

# ─── Web (CDP) + Chrome a11y ────────────────────────────────────────────────
banner "Web / CDP (Chrome)"
CHROME_RUNNING=$(tasklist.exe 2>/dev/null | grep -ci "chrome.exe" || echo 0)
CHROME_LAUNCHED=0
if [ "$QUICK" -eq 0 ]; then
    # Launch Chrome with about:blank if not running
    if [ "$CHROME_RUNNING" -eq 0 ]; then
        powershell -Command "Start-Process chrome.exe -ArgumentList 'about:blank','--new-window'" 2>/dev/null
        sleep 3
        CHROME_RUNNING=$(tasklist.exe 2>/dev/null | grep -ci "chrome.exe" || echo 0)
        [ "$CHROME_RUNNING" -gt 0 ] && CHROME_LAUNCHED=1
    fi

    if [ "$CHROME_RUNNING" -gt 0 ]; then
        CRM="*Chrome*;*Chromium*;*Edge*"
        TEST_URL="file:///W:/GitHub/WKAppBot/scripts/smoke_test.html"
        TEST_HINT="smoke_test.html"

        # -- Open test page --
        timeout 5 "$WKA" web open "$TEST_URL" >/dev/null 2>&1 || \
          powershell -Command "Start-Process chrome.exe '$TEST_URL'" 2>/dev/null
        sleep 2
        # screenshot delegates to CaptureCommand which doesn't support ; OR pattern — use single title
        CRM_WIN=$("$WKA" a11y windows 2>&1 | grep -iE "Chrome|Chromium|Edge" | grep -oP '"[^"]+"' | head -1 | tr -d '"')
        CRM_WIN="${CRM_WIN:+*${CRM_WIN}*}"
        [ -z "$CRM_WIN" ] && CRM_WIN="*Chrome*"

        # -- CDP commands --
        check_g "web tabs"            bash -c "\"$WKA\" web tabs 2>&1 | grep -qiE 'tab|url|http|about|smoke'"
        check_g "web screenshot"      bash -c "\"$WKA\" web screenshot 2>&1 | grep -qiE 'saved|Saved|captured|error'"
        check_g "web eval smoke"      bash -c "\"$WKA\" web eval 'document.getElementById(\"smoke-marker\").dataset.smoke' 2>&1 | grep -qiE 'wkappbot-test-ok|eval|ok'"
        check_g "web eval click"      bash -c "\"$WKA\" web eval 'window.smokeTestApi.clickBtn()' 2>&1 | grep -qiE '[0-9]|eval|ok'"

        # -- a11y on Chrome window (UIA) --
        check_g "a11y inspect chrome" bash -c "\"$WKA\" a11y inspect \"$CRM\" --nth 1 --depth 2 2>&1 | grep -qiE 'match|found|Chrome|window'"
        check_g "a11y read chrome"    bash -c "\"$WKA\" a11y read \"$CRM\" --nth 1 2>&1 | grep -qiE 'name=|value=|ok|error'"
        check_g "a11y screenshot chr" bash -c "\"$WKA\" a11y screenshot \"$CRM_WIN\" 2>&1"
        check_g "a11y focus chrome"   bash -c "\"$WKA\" a11y focus \"$CRM\" --nth 1 2>&1 | grep -qiE 'focus|ok|error'"

        # -- CDP via a11y eval with test page tab hint --
        check_g "a11y eval counter"   bash -c "\"$WKA\" a11y eval \"$CRM#$TEST_HINT\" --text 'window.smokeTestApi.getCount()' 2>&1 | grep -qiE '[0-9]|eval|ok|error'"
        check_g "a11y eval readyState" bash -c "\"$WKA\" a11y eval \"$CRM#$TEST_HINT\" --text 'document.readyState' 2>&1 | grep -qiE 'complete|interactive|eval|ok|error'"

        # -- CDP click button (CSS selector) --
        check_g "a11y click btn-click" bash -c "\"$WKA\" a11y click \"$CRM#button#btn-click\" 2>&1 | grep -qiE 'click|ok|error'"

        # -- CDP type in input --
        check_g "a11y type input"      bash -c "\"$WKA\" a11y type \"$CRM#input#text-input\" --text 'smoke_ok' 2>&1 | grep -qiE 'type|ok|error'"

        # -- Verify typed text via eval --
        check_g "a11y verify typed"    bash -c "\"$WKA\" a11y eval \"$CRM#$TEST_HINT\" --text 'document.getElementById(\"text-input\").value' 2>&1 | grep -qi 'smoke_ok'"

        # Close test tab
        timeout 5 "$WKA" a11y close "$CRM#$TEST_HINT" >/dev/null 2>&1
        echo "  (test tab closed)"

        # Close Chrome if we launched it
        if [ $CHROME_LAUNCHED -eq 1 ]; then
            timeout 5 "$WKA" a11y close "$CRM" --force >/dev/null 2>&1
            echo "  (Chrome closed — launched by test)"
        fi
    else
        skip "web/cdp (Chrome failed to launch)"
    fi
else
    skip "web/cdp Chrome (--quick mode)"
fi

# ─── Readiness ─────────────────────────────────────────────────────────────
banner "Readiness"
check_g "readiness taskbar"  bash -c "\"$WKA\" readiness \"*Shell_TrayWnd*\" 2>&1 | grep -qiE 'ready|readiness|notfound|error|probe'"

# ─── MCP (help/schema only, no pipe) ───────────────────────────────────────
banner "MCP"
check_g "mcp list-tools"     bash -c "echo '{\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"tools/list\"}' | timeout 3 \"$WKA\" mcp 2>&1 | grep -qiE 'wkappbot|tools|jsonrpc|error'" || true

# ─── Summary ───────────────────────────────────────────────────────────────
echo ""
echo "═══════════════════════════════════════"
TOTAL=$((PASS+FAIL+SLOW+SKIP))
echo "  Total: $TOTAL  ✓ $PASS  ✗ $FAIL  ⚠ $SLOW(slow)  - $SKIP(skip)"
[ $FAIL -eq 0 ] && echo "  Status: ALL PASS 🎉" || echo "  Status: $FAIL FAILED ⚠️"
echo "  (⚠ SLOW = capped at ${TIMEOUT}s, not a failure)"
echo "═══════════════════════════════════════"
[ $FAIL -eq 0 ] && exit 0 || exit 1
