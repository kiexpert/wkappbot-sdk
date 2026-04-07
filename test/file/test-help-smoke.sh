#!/usr/bin/env bash
# test-help-smoke.sh — Fast smoke test for help output correctness (5 key cases).
# Subset of test-help-output-all.sh, designed for suggest resolve evidence.
# Runs in ~15s. For full coverage use test-help-output-all.sh.

WKAPPBOT="${WKAPPBOT:-wkappbot}"
PASS=0; FAIL=0

_pass() { echo "[PASS] $1"; PASS=$((PASS+1)); }
_fail() { echo "[FAIL] $1 — $2"; FAIL=$((FAIL+1)); }

_run() { "$WKAPPBOT" "$@" 2>&1 || true; }

chk() {
    local label="$1" kw="$2"; shift 2
    local out; out=$(_run "$@")
    [[ -z "${out// }" ]] && { _fail "$label" "empty output"; return; }
    [[ -n "$kw" ]] && ! printf '%s' "$out" | grep -qiE "$kw" && { _fail "$label" "missing: $kw"; return; }
    printf '%s' "$out" | grep -qP '\xef\xbf\xbd' 2>/dev/null && { _fail "$label" "U+FFFD corruption"; return; }
    _pass "$label"
}

# Global entry points (no args, --help, -h)
chk "global (no args)" "wkappbot"  # zero args
chk "global --help"    "wkappbot"  --help
chk "global -h"        "wkappbot"  -h

# Core coverage: commands that had real bugs
chk "a11y --help"      "Actions"   a11y    --help
chk "ask --help"       "gemini"    ask     --help   # was: background-dispatched
chk "logcat --help"    "regex"     logcat  --help   # was: GrepMode swallowed to stderr
chk "ask (no-args)"    "gemini"    ask              # was: background-dispatched
chk "skill contribute" "title"     skill   contribute --help

echo; echo "PASS=$PASS  FAIL=$FAIL"
[[ $FAIL -eq 0 ]] && echo "ALL PASS" || echo "FAILURES DETECTED"
[[ $FAIL -eq 0 ]]
