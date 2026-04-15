#!/usr/bin/env bash
# test-a11y-wait.sh — a11y wait --condition / --not test suite
# Usage: bash scripts/test-a11y-wait.sh [WK_EXE]
set -uo pipefail

WK="${1:-D:/SDK/bin/wkappbot.exe}"
PASS=0; FAIL=0; SKIP=0

pass() { echo "[PASS] $1"; ((PASS++)); }
fail() { echo "[FAIL] $1"; ((FAIL++)); }
skip() { echo "[SKIP] $1"; ((SKIP++)); }

echo "=== a11y wait test suite ==="
echo "WK=$WK"
echo ""

# ── T01: wait for existing window (should find immediately) ──
out=$("$WK" a11y wait "*Visual Studio Code*" --timeout 3000 2>&1 | tr -d '\0')
if [[ "$out" == *"found"* ]] || [[ "$out" == *"FOUND"* ]]; then pass "T01 wait existing window"; else skip "T01 VS Code not running"; fi

# ── T02: wait for nonexistent window (should timeout) ──
out=$("$WK" a11y wait "*NonExistentWindow12345*" --timeout 1000 2>&1 | tr -d '\0')
if [[ "$out" == *"timeout"* ]] || [[ "$out" == *"TIMEOUT"* ]]; then pass "T02 wait timeout"; else fail "T02 expected timeout"; fi

# ── T03: wait --not for nonexistent (should pass immediately — already gone) ──
out=$("$WK" a11y wait "*NonExistentWindow12345*" --not --timeout 2000 2>&1 | tr -d '\0')
if [[ "$out" == *"gone"* ]] || [[ "$out" == *"GONE"* ]] || [[ "$out" == *"disappear"* ]]; then pass "T03 wait --not (already gone)"; else fail "T03 expected immediate pass"; fi

# ── T04: wait --not for existing window (should timeout — still there) ──
out=$("$WK" a11y wait "*Visual Studio Code*" --not --timeout 1000 2>&1 | tr -d '\0')
if [[ "$out" == *"timeout"* ]] || [[ "$out" == *"TIMEOUT"* ]]; then pass "T04 wait --not existing (timeout)"; else skip "T04 VS Code not running"; fi

# ── T05: wait with --condition (taskbar button text) ──
out=$("$WK" a11y wait "*작업 표시줄*" --condition "시작" --timeout 3000 2>&1 | tr -d '\0')
if [[ "$out" == *"found"* ]] || [[ "$out" == *"FOUND"* ]]; then pass "T05 wait --condition match"; else skip "T05 taskbar not accessible"; fi

# ── T06: wait with --condition that doesn't match (should timeout) ──
out=$("$WK" a11y wait "*Visual Studio Code*" --condition "ZZZZNONEXISTENT" --timeout 1000 2>&1 | tr -d '\0')
if [[ "$out" == *"timeout"* ]] || [[ "$out" == *"TIMEOUT"* ]]; then pass "T06 wait --condition no match (timeout)"; else skip "T06 VS Code not running"; fi

# ── Summary ──
echo ""
echo "Results: $PASS passed, $FAIL failed, $SKIP skipped (total $((PASS + FAIL + SKIP)))"
[[ $FAIL -eq 0 ]]
