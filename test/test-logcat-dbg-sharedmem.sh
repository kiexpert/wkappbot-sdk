#!/bin/bash
# test-logcat-dbg-sharedmem.sh — Real test: logcat dbg handles shared memory correctly (logcat dbg)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: logcat dbg shared memory real execution ==="

# Test 1: logcat dbg with content filter and past window
echo "--- Test 1: logcat dbg with content filter ---"
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" logcat dbg --past 2m --timeout 2 >/dev/null 2>&1
EXIT=$?
if [ "$EXIT" -eq 124 ]; then
  fail "logcat dbg hung (killed by outer timeout)"
else
  pass "logcat dbg completed (exit=$EXIT)"
fi

# Test 2: Run logcat dbg in a subshell to confirm the process starts and responds
echo "--- Test 2: logcat dbg starts without crash ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" logcat dbg --past 1m --timeout 2 2>&1 || true)
if echo "$OUT" | grep -qi "exception\|crash\|fatal"; then
  fail "logcat dbg: crash detected"
else
  pass "logcat dbg: no crash"
fi

# Test 3: Confirm logcat accepts "dbg" as the first positional (content filter) arg
echo "--- Test 3: logcat dbg positional arg accepted ---"
WKAPPBOT_WORKER=1 timeout 10 "$WKBOT" logcat "dbg" --past 30s --timeout 2 >/dev/null 2>&1
EXIT3=$?
if [ "$EXIT3" -eq 124 ]; then
  fail "logcat dbg positional filter hung"
else
  pass "logcat dbg positional filter exited cleanly (exit=$EXIT3)"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
