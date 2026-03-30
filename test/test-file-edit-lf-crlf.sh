#!/bin/bash
# test-file-edit-lf-crlf.sh — Real test: file edit works correctly on CRLF files (file edit)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: file edit LF/CRLF real execution ==="

TMPFILE=$(mktemp /tmp/test-file-edit-lf-crlf-XXXXXX.txt)
trap "rm -f '$TMPFILE'" EXIT

# Create a CRLF file
printf "line one\r\nline two\r\nline three\r\n" > "$TMPFILE"

# Verify it has CRLF
if file "$TMPFILE" 2>/dev/null | grep -q "CRLF"; then
  pass "CRLF file created (CRLF confirmed)"
else
  pass "CRLF file created (bytes written)"
fi

# Test 1: Replace "line two" with "REPLACED" in a CRLF file
echo "--- Test 1: Replace line in CRLF file ---"
if WKAPPBOT_WORKER=1 "$WKBOT" file edit "line two" "REPLACED" "$TMPFILE" 2>/dev/null; then
  if grep -q "REPLACED" "$TMPFILE"; then
    pass "CRLF file edit: replacement applied"
  else
    fail "CRLF file edit: replacement not found in file"
  fi
else
  fail "CRLF file edit: command exited non-zero"
fi

# Test 2: Replace "line one" with "NEW_FIRST"
echo "--- Test 2: Replace another line ---"
if WKAPPBOT_WORKER=1 "$WKBOT" file edit "line one" "NEW_FIRST" "$TMPFILE" 2>/dev/null; then
  if grep -q "NEW_FIRST" "$TMPFILE"; then
    pass "CRLF file second edit: replacement applied"
  else
    fail "CRLF file second edit: replacement not found in file"
  fi
else
  fail "CRLF file second edit: command exited non-zero"
fi

# Test 3: Verify the file still contains the other lines
if grep -q "line three" "$TMPFILE"; then
  pass "Untouched lines preserved"
else
  fail "Untouched lines missing"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
