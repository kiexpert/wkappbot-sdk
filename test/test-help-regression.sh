#!/bin/bash
# test-help-regression.sh
# Self-test for wkappbot --help and --regression global interceptors.
# Run: wkappbot help --regression

PASS=0; FAIL=0
ok()   { echo "[PASS] $1"; PASS=$((PASS+1)); }
fail() { echo "[FAIL] $1: $2"; FAIL=$((FAIL+1)); }

# ── --help interceptor ────────────────────────────────────────────────────────

out=$(wkappbot suggest --help 2>/dev/null)
echo "$out" | grep -q "suggest" && ok "--help suggest" || fail "--help suggest" "no 'suggest' in output"

out=$(wkappbot slack --help 2>/dev/null)
echo "$out" | grep -q "slack" && ok "--help slack" || fail "--help slack" "no 'slack' in output"

out=$(wkappbot a11y --help 2>/dev/null)
echo "$out" | grep -q "a11y" && ok "--help a11y" || fail "--help a11y" "no 'a11y' in output"

# --help on unknown command should still exit 0 (prints fallback)
wkappbot help --help >/dev/null 2>&1 && ok "--help help exits 0" || fail "--help help" "non-zero exit"

# ── --regression interceptor ──────────────────────────────────────────────────

# suggest resolve has stored scripts → should show [REGRESSION]
out=$(wkappbot suggest resolve --regression 2>/dev/null)
echo "$out" | grep -q "REGRESSION" && ok "--regression interceptor" || fail "--regression interceptor" "no [REGRESSION] in output"

# No scripts for a non-existent cmd → should show "No test scripts found"
out=$(wkappbot _nosuchwkacmd_ --regression 2>/dev/null)
echo "$out" | grep -qi "no.*test scripts\|unknown command\|REGRESSION" \
  && ok "--regression no-scripts graceful" \
  || fail "--regression no-scripts" "unexpected output: $out"

# ── summary ───────────────────────────────────────────────────────────────────
echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ $FAIL -eq 0 ] && echo "ALL PASS" && exit 0 || exit 1
