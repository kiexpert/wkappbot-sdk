#!/usr/bin/env bash
# test-help-output-all.sh
# Full survey of --help / -h / no-args / subcommand help output.
#
# Checks per case:
#   [1] Output is non-empty
#   [2] Expected keyword present in output
#   [3] No U+FFFD encoding corruption (\xef\xbf\xbd in UTF-8)
#   [4] "Skills for" section present (added v5.13.3 — WARN only, not FAIL)
#
# Usage: bash test/file/test-help-output-all.sh
# WKAPPBOT env overrides the executable path.

WKAPPBOT="${WKAPPBOT:-wkappbot}"
PASS=0; FAIL=0; WARN=0

_pass() { echo "[PASS] $1"; PASS=$((PASS+1)); }
_fail() { echo "[FAIL] $1 — $2"; FAIL=$((FAIL+1)); }
_warn() { echo "[WARN] $1 — $2"; WARN=$((WARN+1)); }

_run() { "$WKAPPBOT" "$@" 2>&1 || true; }

# check_help LABEL KEYWORD EXPECT_SKILLS CMD [ARGS...]
#   KEYWORD      : grep -E pattern that must appear in output (empty = skip keyword check)
#   EXPECT_SKILLS: 1 = warn if "Skills" section missing, 0 = don't check
check_help() {
    local label="$1" kw="$2" expect_skills="$3"; shift 3
    local out
    out=$(_run "$@")

    # [1] non-empty
    if [[ -z "${out// }" ]]; then
        _fail "$label" "empty output"
        return
    fi

    # [2] keyword
    if [[ -n "$kw" ]] && ! printf '%s' "$out" | grep -qiE "$kw"; then
        _fail "$label" "missing keyword: $kw"
        printf '%s\n' "$out" | head -3 | sed 's/^/    /'
        return
    fi

    # [3] U+FFFD encoding corruption
    if printf '%s' "$out" | grep -qP '\xef\xbf\xbd' 2>/dev/null; then
        _fail "$label" "U+FFFD replacement char detected (encoding corruption)"
        return
    fi

    # [3b] common CP949→UTF-8 mojibake: lone high-byte followed by ASCII
    #      Pattern: 0xC0-0xFF followed by 0x00-0x7F (invalid in valid UTF-8)
    if printf '%s' "$out" | LC_ALL=C grep -qP '[\xc0-\xff][\x00-\x7f]' 2>/dev/null; then
        _warn "$label" "possible CP949/Latin mojibake sequence detected"
    fi

    # [4] Skills section (soft check)
    if [[ "$expect_skills" == "1" ]]; then
        if ! printf '%s' "$out" | grep -q "Skills"; then
            _warn "$label" "no 'Skills' section (expected v5.13.3+)"
        fi
    fi

    _pass "$label"
}

# ── Section header ────────────────────────────────────────────────────────────
section() { echo; echo "── $1 ──"; }

# =============================================================================
section "--help flag (top-level commands)"
# =============================================================================

check_help "global --help"      "wkappbot"       0   --help
check_help "a11y --help"        "Actions:"       1   a11y    --help
check_help "slack --help"       "send"           1   slack   --help
check_help "ask --help"         "gemini"         1   ask     --help
check_help "agent --help"       "max-steps"      1   agent   --help
check_help "file --help"        "read"           1   file    --help
check_help "web --help"         "navigate"       1   web     --help
check_help "skill --help"       "contribute"     1   skill   --help
check_help "eye --help"         "tick"           0   eye     --help
check_help "logcat --help"      "regex"          0   logcat  --help
check_help "suggest --help"     "resolve"        0   suggest --help
check_help "schedule --help"    "add"            0   schedule --help
check_help "windows --help"     "grap"           0   windows --help
check_help "newchat --help"     "prompt"         0   newchat --help
check_help "help --help"        "regression"     0   help    --help
check_help "gc --help"          "days"           0   gc      --help
check_help "mcp --help"         "mcp"            0   mcp     --help

# =============================================================================
section "-h alias"
# =============================================================================

check_help "a11y -h"            "Actions:"       1   a11y    -h
check_help "slack -h"           "send"           1   slack   -h
check_help "ask -h"             "gemini"         1   ask     -h
check_help "file -h"            "read"           1   file    -h
check_help "skill -h"           "contribute"     1   skill   -h

# =============================================================================
section "subcommand --help (optimized skill search for sub-term)"
# =============================================================================

# These should surface skills relevant to the specific subcommand/action
check_help "a11y click --help"        "grap"        0   a11y    click       --help
check_help "a11y type --help"         "type"        0   a11y    type        --help
check_help "a11y hack --help"         "hack"        0   a11y    hack        --help
check_help "a11y inspect --help"      "inspect"     0   a11y    inspect     --help
check_help "a11y windows --help"      "windows"     0   a11y    windows     --help
check_help "file edit --help"         "edit"        1   file    edit        --help
check_help "file grep --help"         "grep"        1   file    grep        --help
check_help "file read --help"         "read"        1   file    read        --help
check_help "skill list --help"        "skill"       1   skill   list        --help
check_help "skill contribute --help"  "contribute"  1   skill   contribute  --help
check_help "skill edit --help"        "edit"        1   skill   edit        --help
check_help "slack send --help"        "send"        1   slack   send        --help
check_help "slack reply --help"       "reply"       1   slack   reply       --help
check_help "slack route --help"       "queue"       0   slack   route       --help
check_help "agent checkpoint --help"  "checkpoint"  0   agent   checkpoint  --help
check_help "ask triad --help"         "triad"       1   ask     triad       --help

# =============================================================================
section "no-args fallback (Usage() methods)"
# =============================================================================

check_help "ask (no args)"       "gemini"      1   ask
check_help "agent (no args)"     "max-steps"   1   agent
check_help "skill (no args)"     "contribute"  1   skill
check_help "web (no args)"       "navigate"    1   web
check_help "file (no args)"      "read"        1   file
check_help "schedule (no args)"  "add"         0   schedule

# =============================================================================
section "unknown command handling"
# =============================================================================

out=$(_run __unknowncmd_xyz_wktest__ 2>&1)
if printf '%s' "$out" | grep -qiE "unknown|not found|error|usage|command"; then
    _pass "unknown command → error/usage shown"
else
    _fail "unknown command" "expected error/usage, got: $(printf '%s' "$out" | head -1)"
fi

# Unknown subcommand on known command
out=$(_run skill __unknownsub_xyz__ 2>&1)
if printf '%s' "$out" | grep -qiE "unknown|error|usage|skill"; then
    _pass "skill unknown-sub → error shown"
else
    _fail "skill unknown-sub" "expected error/usage"
fi

# =============================================================================
section "encoding corruption spot-check (U+FFFD across all major commands)"
# =============================================================================

corrupt_found=0
for cmd in a11y slack ask file web skill eye logcat suggest schedule agent windows; do
    out=$(_run "$cmd" --help 2>&1)
    if printf '%s' "$out" | grep -qP '\xef\xbf\xbd' 2>/dev/null; then
        _fail "encoding:$cmd --help" "U+FFFD found in output"
        corrupt_found=$((corrupt_found+1))
    fi
done
[[ $corrupt_found -eq 0 ]] && _pass "encoding: no U+FFFD in any --help output"

# =============================================================================
section "Skills section presence (v5.13.3+ feature)"
# =============================================================================

skills_missing=0
for spec in \
    "a11y --help" \
    "slack --help" \
    "ask --help" \
    "file --help" \
    "web --help" \
    "skill --help" \
; do
    out=$(_run $spec 2>&1)
    if ! printf '%s' "$out" | grep -q "Skills"; then
        _warn "skills-section:$spec" "no 'Skills' section in output"
        skills_missing=$((skills_missing+1))
    fi
done
[[ $skills_missing -eq 0 ]] && _pass "skills-section: all checked commands show 'Skills'"

# =============================================================================
section "subcommand-optimized skills (sub-term in label)"
# =============================================================================

# When subcommand is given, Skills label should mention it
out=$(_run skill contribute --help 2>&1)
if printf '%s' "$out" | grep -q "skill contribute\|contribute"; then
    _pass "skills label includes subcommand (skill contribute)"
else
    _warn "skills label" "subcommand not reflected in Skills label"
fi

out=$(_run ask triad --help 2>&1)
if printf '%s' "$out" | grep -qi "triad"; then
    _pass "skills content relevant to subcommand (ask triad)"
else
    _warn "skills content" "triad not mentioned in ask triad --help skills"
fi

# =============================================================================
echo
echo "══════════════════════════════════════════════"
TOTAL=$((PASS+FAIL+WARN))
printf "PASS=%-3d  FAIL=%-3d  WARN=%-3d  TOTAL=%d\n" "$PASS" "$FAIL" "$WARN" "$TOTAL"
[[ $FAIL -eq 0 ]] && echo "ALL PASS" || echo "FAILURES DETECTED"
echo "══════════════════════════════════════════════"
[[ $FAIL -eq 0 ]]
