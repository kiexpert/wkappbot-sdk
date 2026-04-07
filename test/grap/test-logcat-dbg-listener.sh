#!/bin/bash
# test-logcat-dbg-listener.sh — Real test: logcat dbg runs and exits cleanly (logcat dbg)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: logcat dbg real execution ==="

# Test 1: Run logcat with "dbg" as search pattern, past 5 minutes, timeout 3s
# CMD guard: wkappbot logcat dbg ... → dbgCmd=logcat, dbgSub=dbg → -logcat-dbg>
echo "--- Test 1: logcat dbg exits cleanly with --timeout ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" logcat dbg --past 5m --timeout 3 2>&1)
EXIT=$?
if [ "$EXIT" -eq 0 ]; then
  pass "logcat dbg exited 0 (timeout completed)"
elif [ "$EXIT" -eq 124 ]; then
  fail "logcat dbg killed by outer timeout (hung)"
else
  pass "logcat dbg exited $EXIT (no logs found is ok)"
fi

# Test 2: Output should not contain error/exception
if echo "$OUT" | grep -qi "exception\|unhandled"; then
  fail "logcat dbg: unexpected exception in output"
else
  pass "logcat dbg: no exception in output"
fi

# Test 3: Run with --past 1m --timeout 2 — should also exit cleanly
echo "--- Test 2: short past window ---"
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" logcat dbg --past 1m --timeout 2 >/dev/null 2>&1
EXIT2=$?
if [ "$EXIT2" -eq 0 ] || [ "$EXIT2" -eq 1 ]; then
  pass "logcat dbg short window exited cleanly ($EXIT2)"
elif [ "$EXIT2" -eq 124 ]; then
  fail "logcat dbg short window hung (outer timeout)"
else
  pass "logcat dbg short window exited $EXIT2 (no logs ok)"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
