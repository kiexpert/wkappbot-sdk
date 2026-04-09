#!/usr/bin/env bash
# Live regression: a11y FOCUS/TARGET grap output format + exit codes
# Launches Calculator, tests key actions, verifies stdout patterns + exit codes.
# Usage: bash test-a11y-output-live.sh [--no-launch]
#
# Exit-code contract tested:
#   match found  → exit 0,  stdout has ## FOCUS + # TARGET (non-find) or ## heading (find)
#   no match     → exit 1,  stdout has no # TARGET / no ## heading

WKA="W:/SDK/bin/wkappbot.exe"
P=0; F=0; S=0  # passed, failed, skipped

# ── helpers ──────────────────────────────────────────────────────────────────

pass() { echo "PASS  $1"; ((P++)); }
fail() { echo "FAIL  $1"; ((F++)); }
skip() { echo "SKIP  $1  ($2)"; ((S++)); }

# Run one command, capture stdout into OUT and exit code into RC
run() {
    local action="$1"; shift
    OUT=$("$WKA" a11y "$action" "$@" 2>/dev/null)
    RC=$?
}

# Assert exit code
expect_exit() {
    local name="$1" want="$2"
    if [[ $RC -eq $want ]]; then pass "$name  [exit=$RC]"
    else fail "$name  [exit=$RC expected=$want]"
    fi
}

# Assert stdout contains pattern
expect_out() {
    local name="$1" pattern="$2"
    if echo "$OUT" | grep -q "$pattern"; then pass "$name"
    else
        fail "$name  [missing: $pattern]"
        echo "       stdout: $(echo "$OUT" | head -5 | tr '\n' '|')"
    fi
}

# Assert stdout does NOT contain pattern
expect_no_out() {
    local name="$1" pattern="$2"
    if ! echo "$OUT" | grep -q "$pattern"; then pass "$name"
    else fail "$name  [unexpected: $pattern]"
    fi
}

# Run + assert exit 0 + check all patterns in one shot
check() {
    local name="$1" action="$2" grap="$3"; shift 3
    run "$action" "$grap" "$@"
    if [[ $RC -ne 0 ]]; then fail "$name  [exit=$RC expected=0]"; return 1; fi
    pass "$name  [exit=0]"
    return 0
}

# ── setup: launch Calculator ──────────────────────────────────────────────────

echo "=== a11y output format: live tests ==="
MISS="*__no_such_window_xyzzy_99__*"
CALC_GRAP="*계산기*;*Calculator*"

CALC_FOUND=0
# Use class-based grap — locale-independent, avoids Korean title encoding issues
CALC_CLS_GRAP="{cls:'ApplicationFrameWindow',proc:'ApplicationFrameHost'}"

if [[ "$1" != "--no-launch" ]]; then
    taskkill //F //IM CalculatorApp.exe 2>/dev/null || true
    sleep 0.3
    start "" calc.exe
    for i in $(seq 1 10); do
        sleep 0.5
        WIN_EXIT=$("$WKA" a11y windows "$CALC_CLS_GRAP" 2>/dev/null; echo $?)
        WIN_EXIT="${WIN_EXIT##*$'\n'}"
        if "$WKA" a11y windows "$CALC_CLS_GRAP" 2>/dev/null | grep -q 'ApplicationFrameWindow\|CalculatorApp'; then
            echo "INFO  Calculator ready (after ${i}x0.5s)"
            CALC_FOUND=1; break
        fi
        [[ $i -eq 10 ]] && echo "WARN  Calculator not found after 5s"
    done
fi

if [[ "$1" == "--no-launch" ]]; then
    "$WKA" a11y windows "$CALC_CLS_GRAP" 2>/dev/null | grep -q 'ApplicationFrameWindow' && CALC_FOUND=1
fi

if [[ $CALC_FOUND -eq 0 ]]; then
    echo "WARN  Calculator not found — live tests skipped"
    CALC_GRAP=""
else
    # Extract hwnd from TARGET block (not FOCUS block) to get Calculator's precise hwnd
    FIND_OUT=$("$WKA" a11y find "$CALC_CLS_GRAP" 2>/dev/null)
    # Skip ## FOCUS block, take first hwnd from a TARGET ## heading block
    CALC_HWND=$(echo "$FIND_OUT" | awk '/^## FOCUS/{skip=1} /^```$/{if(skip)skip=0} skip{next} {print}' \
                | grep -oE 'hwnd:0x[0-9A-Fa-f]+' | head -1)
    if [[ -n "$CALC_HWND" ]]; then
        CALC_GRAP="$CALC_HWND"
        echo "INFO  Using hwnd grap: $CALC_GRAP"
    else
        CALC_GRAP="$CALC_CLS_GRAP"
        echo "INFO  Using class grap (hwnd extract failed)"
    fi
fi

# ── GROUP 1: find — code-fence block ──────────────────────────────────────────
echo ""
echo "── find action ──"

if [[ -z "$CALC_GRAP" ]]; then skip "find group" "Calculator not found"
else
    run find "$CALC_GRAP"
    expect_exit "find match: exit 0" 0
    expect_out  "find match: ## FOCUS in stdout"     "## FOCUS"
    expect_out  "find match: code fence in stdout"   '```'
    expect_out  "find match: hwnd: in FOCUS grap"    'hwnd:0x'
    expect_out  "find match: [OK] verify tag"        '\[OK\]'
    # ## WindowTitle block (the actual TARGET block heading)
    TITLE_LINE=$(echo "$OUT" | grep -E '^## ' | grep -v "^## FOCUS" | head -1)
    if [[ -n "$TITLE_LINE" ]]; then pass "find match: ## WindowTitle heading ($TITLE_LINE)"
    else fail "find match: no ## WindowTitle heading in stdout"
    fi
fi

run find "$MISS"
expect_exit   "find miss: exit non-zero" 1
expect_no_out "find miss: no # TARGET"  "# TARGET"
expect_no_out "find miss: no ## heading" "^## "

# ── GROUP 2: minimize / restore / maximize / focus ────────────────────────────
echo ""
echo "── non-find actions ──"

if [[ -z "$CALC_GRAP" ]]; then skip "non-find group" "Calculator not found"
else
    # minimize
    run minimize "$CALC_GRAP"
    expect_exit "minimize: exit 0" 0
    expect_out  "minimize: ## FOCUS block" "## FOCUS"
    expect_out  "minimize: # TARGET line"  "# TARGET"
    expect_out  "minimize: hwnd in TARGET" 'hwnd:0x'
    sleep 0.5

    # restore
    run restore "$CALC_GRAP"
    expect_exit "restore: exit 0" 0
    expect_out  "restore: ## FOCUS block"  "## FOCUS"
    expect_out  "restore: # TARGET line"   "# TARGET"
    sleep 0.5

    # maximize
    run maximize "$CALC_GRAP"
    expect_exit "maximize: exit 0" 0
    expect_out  "maximize: # TARGET line"  "# TARGET"
    sleep 0.5
    "$WKA" a11y restore "$CALC_GRAP" 2>/dev/null; sleep 0.3

    # focus
    run focus "$CALC_GRAP"
    expect_exit "focus: exit 0" 0
    expect_out  "focus: ## FOCUS block"    "## FOCUS"
    expect_out  "focus: # TARGET line"     "# TARGET"
fi

# ── GROUP 3: no-match → non-zero exit, no TARGET ──────────────────────────────
echo ""
echo "── no-match exit codes ──"

for action in minimize maximize restore focus click read invoke; do
    run "$action" "$MISS"
    expect_exit   "$action miss: exit non-zero" 1
    expect_no_out "$action miss: no # TARGET"   "# TARGET"
done

# ── GROUP 4: grap format validity ─────────────────────────────────────────────
echo ""
echo "── grap format validity ──"

# Re-resolve hwnd via class grap — UWP windows can change hwnd after maximize/restore
FRESH_FIND=$("$WKA" a11y find "$CALC_CLS_GRAP" 2>/dev/null)
FRESH_HWND=$(echo "$FRESH_FIND" | awk '/^## FOCUS/{skip=1} /^```$/{if(skip)skip=0} skip{next} {print}' \
             | grep -oE 'hwnd:0x[0-9A-Fa-f]+' | head -1)
[[ -n "$FRESH_HWND" ]] && GRAP4="$FRESH_HWND" || GRAP4="$CALC_GRAP"

if [[ -n "$GRAP4" ]]; then
    run find "$GRAP4"
    if [[ $RC -ne 0 ]]; then
        skip "grap format checks" "find returned exit $RC (Calculator may have closed)"
    else
        # FOCUS block grap must contain hwnd:0x (ANSI stripped by pipe, ^" anchoring unreliable)
        if echo "$OUT" | grep -A4 "## FOCUS" | grep -qE 'hwnd:0x[0-9A-Fa-f]+'; then
            pass "find: FOCUS block contains hwnd:0x..."
        else
            fail "find: FOCUS block missing hwnd:0x..."
            echo "       $(echo "$OUT" | grep -A4 '## FOCUS' | head -5)"
        fi

        # FOCUS block grap must be in a code fence line containing '{'
        if echo "$OUT" | grep -A4 "## FOCUS" | grep -qE '^\"\{|^\{'; then
            pass "find: FOCUS grap is JSON5 format"
        else
            fail "find: FOCUS grap is not JSON5 format"
        fi

        run minimize "$GRAP4"
        # # TARGET line must contain hwnd:0x (no ^anchor, ANSI may prefix)
        if echo "$OUT" | grep "TARGET" | grep -qE 'hwnd:0x[0-9A-Fa-f]+'; then
            pass "non-find: # TARGET grap has hwnd:0x..."
        else
            fail "non-find: # TARGET grap missing hwnd:0x..."
            echo "       $(echo "$OUT" | grep 'TARGET')"
        fi
        "$WKA" a11y restore "$GRAP4" 2>/dev/null; sleep 0.3
    fi
fi

# ── teardown ─────────────────────────────────────────────────────────────────
echo ""
if [[ "$1" != "--no-launch" ]]; then
    taskkill //F //IM CalculatorApp.exe 2>/dev/null || true
    echo "INFO  Calculator closed"
fi

echo "════════════════════════════════"
echo "Result: $P passed, $F failed, $S skipped"
[[ $F -eq 0 ]]
