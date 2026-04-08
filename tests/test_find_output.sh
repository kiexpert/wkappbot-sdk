#!/usr/bin/env bash
# test_find_output.sh — verify a11y find output format + ambiguity guard find-redirections
#
# Tests:
#   1. find stdout format: ## FOCUS, ## TARGET heading, paste grap, command line, [OK] Nms
#   2. No [[double-bracket]] verify marks
#   3. No [Gemini]/[ChatGPT] stdout noise
#   4. Layer 1 (multi-window) → ## TARGETS N matches header
#   5. Layer 5 (no #scope on click) → redirects to find, tip has BuildFindGrap-style grap
#   6. Layer 6 (scope miss on invoke) → lists elements, tip has BuildFindGrap-style grap
#
# Requires: VS Code open (proc=Code), wkappbot in PATH or W:/SDK/bin/

# Note: run after hot-swap completes (a few seconds after publish).
# If you get "wkappbot-core.exe not found", wait 3s and retry.
WK="${WKAPPBOT:-W:/SDK/bin/wkappbot.exe} a11y"
PASS=0
FAIL=0

# ── helpers ────────────────────────────────────────────────────────────────────

check() {
    local desc="$1" result="$2" pattern="$3"
    if echo "$result" | grep -qP "$pattern"; then
        printf "  \e[92m[PASS]\e[0m %s\n" "$desc"
        ((PASS++))
    else
        printf "  \e[91m[FAIL]\e[0m %s\n" "$desc"
        printf "         expected pattern: %s\n" "$pattern"
        printf "         got (first 5 lines):\n"
        echo "$result" | head -5 | sed 's/^/           /'
        ((FAIL++))
    fi
}

check_not() {
    local desc="$1" result="$2" pattern="$3"
    if echo "$result" | grep -qP "$pattern"; then
        printf "  \e[91m[FAIL]\e[0m %s  (should NOT contain)\n" "$desc"
        echo "$result" | grep -P "$pattern" | head -3 | sed 's/^/         /'
        ((FAIL++))
    else
        printf "  \e[92m[PASS]\e[0m %s\n" "$desc"
        ((PASS++))
    fi
}

sep() { printf "\n\e[2m--- %s ---\e[0m\n" "$1"; }

# ── Test 1: basic find output format (VS Code, always open) ────────────────────
sep "Test 1: a11y find {proc:'Code'} — stdout format"
STDOUT=$(${WK} find "{proc:'Code'}" 2>/dev/null)

check "## FOCUS heading present"          "$STDOUT" '^## FOCUS$'
check "FOCUS has quoted grap"             "$STDOUT" '^"\{hwnd:0x[0-9A-Fa-f]+'
check "## TARGET heading present"         "$STDOUT" '^## .+$'
check "TARGET has quoted grap with hwnd"  "$STDOUT" '^\"\{hwnd:0x[0-9A-Fa-f]+,pid:[0-9]+'
check "TARGET has wkappbot command line"  "$STDOUT" '^wkappbot a11y find "'
check "TARGET has [OK] or [MISS] mark"    "$STDOUT" '\[OK\]|\[MISS\]'

check_not "no [[double-bracket]] marks"   "$STDOUT" '\[\[OK\]\]|\[\[MISS\]\]'
check_not "no [Gemini] in stdout"         "$STDOUT" '^\[Gemini\]'
check_not "no [ChatGPT] in stdout"        "$STDOUT" '^\[ChatGPT\]'
check_not "no [Gemini] streaming noise"   "$STDOUT" 'streaming\.\.\.'

# ── Test 2: mandatory grap fields (hwnd first, pid second, proc) ──────────────
sep "Test 2: mandatory fields in grap (hwnd, pid, proc)"
GRAP_LINE=$(echo "$STDOUT" | grep -P '^\"\{hwnd:' | head -1)

check "hwnd is first field"   "$GRAP_LINE" '"\{hwnd:0x[0-9A-Fa-f]+'
check "pid is second field"   "$GRAP_LINE" 'hwnd:[^,]+,pid:[0-9]+'
check "proc field present"    "$GRAP_LINE" "proc:'[^']+'"

# ── Test 3: multiple-target header ─────────────────────────────────────────────
sep "Test 3: multiple matches → ## TARGETS N matches"
# Use a broad pattern likely to match multiple windows (chrome;Code;explorer etc.)
MULTI=$(${WK} find "{proc:'chrome'};{proc:'Code'}" 2>/dev/null || true)
if echo "$MULTI" | grep -qP '^## TARGETS\s+[0-9]+ matches$'; then
    printf "  \e[92m[PASS]\e[0m ## TARGETS N matches header present\n"; ((PASS++))
elif echo "$MULTI" | grep -qP '^## TARGET$'; then
    # Only one matched — still valid output, just no multi-header needed
    printf "  \e[93m[SKIP]\e[0m only 1 match found, ## TARGETS header not needed\n"
else
    printf "  \e[91m[FAIL]\e[0m expected ## TARGET or ## TARGETS header\n"; ((FAIL++))
fi

# ── Test 4: Layer 5 — no #scope on interactive command ────────────────────────
sep "Test 4: Layer 5 — click without #scope → find tip with BuildFindGrap grap"
# Capture stderr (guard output goes to stderr)
L5=$(${WK} click "{proc:'Code'}" 2>&1 >/dev/null)

check "L5 tip has a11y find command"       "$L5" 'a11y find '
check "L5 tip grap has hwnd field"         "$L5" 'hwnd:0x[0-9A-Fa-f]+'
check "L5 tip grap has pid field"          "$L5" 'pid:[0-9]+'
check "L5 tip grap has proc field"         "$L5" "proc:'[^']+'"
check_not "L5 tip has no old BuildTargetGrap format (hwnd last)" "$L5" "proc:'[^']+',hwnd:"

# ── Test 5: Layer 6 — scope miss on invoke ────────────────────────────────────
sep "Test 5: Layer 6 — scope miss → element list + find tip"
SCOPE_MISS="_xNONEXISTENTx_"
L6=$(${WK} invoke "{proc:'Code'}#${SCOPE_MISS}" 2>&1 >/dev/null)

check "L6 reports scope not found"   "$L6" '"?'$SCOPE_MISS'"? not found|not found'
check "L6 tip has a11y find command" "$L6" 'a11y find '
check "L6 tip grap has hwnd field"   "$L6" 'hwnd:0x[0-9A-Fa-f]+'
check "L6 tip grap has pid field"    "$L6" 'pid:[0-9]+'

# ── Test 6: Chrome find (domain field) ────────────────────────────────────────
sep "Test 6: chrome window find — domain field in grap"
CHROME=$(${WK} find "{proc:'chrome'}" 2>/dev/null | head -20)
if [ -n "$CHROME" ]; then
    check "Chrome grap has domain or proc field" "$CHROME" "domain:'[^']+'|proc:'chrome'"
    check_not "Chrome grap has no title field"   "$CHROME" "title:'[^']+'"
else
    printf "  \e[93m[SKIP]\e[0m chrome not running\n"
fi

# ── Summary ────────────────────────────────────────────────────────────────────
printf "\n\e[1m=== Results: "
if [ $FAIL -eq 0 ]; then
    printf "\e[92m%d passed\e[0m, 0 failed ===\e[0m\n" "$PASS"
else
    printf "%d passed, \e[91m%d failed\e[0m ===\e[0m\n" "$PASS" "$FAIL"
fi

exit $FAIL
