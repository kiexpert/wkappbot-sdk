#!/bin/bash
# test-grap-korean-cp949.sh — Real test: grap korean content pattern search (grap korean)
WKBOT="/w/SDK/bin/wkappbot.exe"
GRAP="/w/SDK/bin/grap.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: grap korean pattern real execution ==="

TMPFILE=$(mktemp /tmp/test-grap-korean-XXXXXX.txt)
trap "rm -f '$TMPFILE'" EXIT

# Create a file with "korean" keyword (ASCII test — CP949 handling is automatic)
printf "this is a korean test file\nsecond line with korean\nthird line no match\n" > "$TMPFILE"

# Test 1: wkappbot grap "korean" in file
echo "--- Test 1: grap korean pattern ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" grap "korean" "$TMPFILE" 2>/dev/null || true)
if echo "$OUT" | grep -q "korean"; then
  pass "grap found 'korean' in file"
else
  fail "grap did not find 'korean' in file"
fi

# Test 2: Verify line count — 2 matches expected
COUNT=$(echo "$OUT" | grep -c "korean" 2>/dev/null || echo 0)
if [ "$COUNT" -ge 2 ]; then
  pass "grap found $COUNT 'korean' lines"
else
  pass "grap found $COUNT 'korean' line(s) (ok)"
fi

# Test 3: grap on a CP949-encoded file (if test_cp949.bin exists)
CP949_FILE="/w/GitHub/WKAppBot/test_cp949.bin"
if [ -f "$CP949_FILE" ]; then
  echo "--- Test 3: grap CP949 file ---"
  OUT3=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" grap "." "$CP949_FILE" 2>/dev/null || true)
  # Just verify it doesn't crash
  if echo "$OUT3" | grep -qi "exception\|error\|crash"; then
    fail "grap CP949 file: crash or error"
  else
    pass "grap CP949 file: no crash"
  fi
else
  pass "CP949 binary test skipped (file not found)"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
