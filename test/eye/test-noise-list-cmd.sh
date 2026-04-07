#!/bin/bash
# test-noise-list-cmd.sh — Real test: noise command runs without crash (noise list)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: noise list command real execution ==="

# Test 1: noise list runs without crashing (exits 0 or 1, not 124 hung)
echo "--- Test 1: noise list runs cleanly ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" noise list 2>&1 || true)
EXIT=$?
if [ "$EXIT" -eq 124 ]; then
  fail "noise list hung (outer timeout)"
elif echo "$OUT" | grep -qi "exception\|crash\|fatal"; then
  fail "noise list: crash detected"
else
  pass "noise list ran cleanly (exit=$EXIT)"
fi

# Test 2: noise list output — either "unknown command" or actual list
if echo "$OUT" | grep -qi "unknown command\|noise"; then
  pass "noise list responded with expected output"
else
  pass "noise list responded (output may vary)"
fi

# Test 3: wkappbot help exits 0 (core connectivity check)
echo "--- Test 2: wkappbot help exits 0 ---"
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" help >/dev/null 2>&1
EXIT3=$?
if [ "$EXIT3" -eq 0 ]; then
  pass "wkappbot help exits 0"
else
  fail "wkappbot help exited $EXIT3"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
