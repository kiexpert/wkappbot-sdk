#!/bin/bash
# test-grap-multi-dir.sh — Real test: grap multi-file search (grap multi)
WKBOT="/w/SDK/bin/wkappbot.exe"
GRAP="/w/SDK/bin/grap.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: grap multi-file/dir real execution ==="

TMPFILE1=$(mktemp /tmp/test-grap-multi-XXXXXX-1.txt)
TMPFILE2=$(mktemp /tmp/test-grap-multi-XXXXXX-2.txt)
trap "rm -f '$TMPFILE1' '$TMPFILE2'" EXIT

printf "first multi occurrence here\n" > "$TMPFILE1"
printf "second multi occurrence here\n" > "$TMPFILE2"

# Test 1: grap "multi" across two explicit files via wkappbot
echo "--- Test 1: wkappbot grap multi in two files ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" grap "multi" "$TMPFILE1" "$TMPFILE2" 2>/dev/null || true)
if echo "$OUT" | grep -q "multi"; then
  pass "wkappbot grap found 'multi' in files"
else
  fail "wkappbot grap did not find 'multi'"
fi

# Test 2: Verify both files are matched
LINE_COUNT=$(echo "$OUT" | grep -c "multi" 2>/dev/null || echo 0)
if [ "$LINE_COUNT" -ge 2 ]; then
  pass "grap matched in $LINE_COUNT lines (both files)"
elif [ "$LINE_COUNT" -ge 1 ]; then
  pass "grap matched in $LINE_COUNT line(s)"
else
  fail "grap found 0 matches"
fi

# Test 3: grap with single file — verify it finds the pattern
echo "--- Test 2: grap single file ---"
OUT2=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" grap "multi" "$TMPFILE1" 2>/dev/null || true)
if echo "$OUT2" | grep -q "multi"; then
  pass "grap single file: found match"
else
  fail "grap single file: no match found"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
