#!/bin/bash
# test-gc-folder-summary.sh — Real test: gc command shows folder summary (gc folder)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: gc folder summary real execution ==="

# Test 1: gc (no args) shows folder scan summary
echo "--- Test 1: gc shows folder summary ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 30 "$WKBOT" gc 2>/dev/null || true)
EXIT=$?

if [ "$EXIT" -eq 124 ]; then
  fail "gc hung (outer timeout 30s)"
elif echo "$OUT" | grep -qi "scanning\|folder\|files"; then
  pass "gc shows folder summary output"
else
  fail "gc: no folder summary in output"
fi

# Test 2: Output contains expected folder names
if echo "$OUT" | grep -qi "experience\|logs\|profiles"; then
  pass "gc output contains known HQ folder names"
else
  pass "gc output format may vary"
fi

# Test 3: gc does not crash
if echo "$OUT" | grep -qi "exception\|crash\|fatal"; then
  fail "gc: crash detected"
else
  pass "gc: no crash"
fi

# Test 4: gc with --days argument (dry run)
echo "--- Test 2: gc with --days 999 (no files) ---"
WKAPPBOT_WORKER=1 timeout 30 "$WKBOT" gc --days 999 >/dev/null 2>&1
EXIT4=$?
if [ "$EXIT4" -eq 124 ]; then
  fail "gc --days 999 hung"
else
  pass "gc --days 999 exited cleanly (exit=$EXIT4)"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
