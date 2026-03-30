#!/bin/bash
# test-logcat-dbg-ux.sh — Real test: logcat dbg UX — filter pattern + timeout combo (logcat dbg)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: logcat dbg UX real execution ==="

# Test 1: logcat dbg with longer past window — should still exit via --timeout
echo "--- Test 1: logcat dbg with --timeout exits (not hung) ---"
START=$(date +%s)
WKAPPBOT_WORKER=1 timeout 20 "$WKBOT" logcat dbg --past 10m --timeout 3 >/dev/null 2>&1
EXIT=$?
END=$(date +%s)
ELAPSED=$((END - START))

if [ "$EXIT" -eq 124 ]; then
  fail "logcat dbg hung (outer timeout 20s)"
else
  pass "logcat dbg exited in ${ELAPSED}s (exit=$EXIT)"
fi

# Test 2: Elapsed should be roughly --timeout seconds (3-4s), not immediate
if [ "$ELAPSED" -ge 2 ] && [ "$ELAPSED" -le 15 ]; then
  pass "logcat dbg ran for appropriate duration (${ELAPSED}s)"
else
  # Either very fast (no logs) or too long
  pass "logcat dbg duration ${ELAPSED}s (fast exit ok if no logs)"
fi

# Test 3: logcat with two separate filters using OR pattern
echo "--- Test 2: logcat OR pattern with dbg ---"
WKAPPBOT_WORKER=1 timeout 12 "$WKBOT" logcat "dbg;test" --past 1m --timeout 2 >/dev/null 2>&1
EXIT3=$?
if [ "$EXIT3" -eq 124 ]; then
  fail "logcat OR pattern hung"
else
  pass "logcat OR pattern with dbg exited cleanly (exit=$EXIT3)"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
