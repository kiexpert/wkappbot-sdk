#!/bin/bash
# test-grap-folder-arg.sh — Real test: grap folder search in directory (grap folder)
WKBOT="/w/SDK/bin/wkappbot.exe"
GRAP="/w/SDK/bin/grap.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: grap folder arg real execution ==="

TMPDIR=$(mktemp -d /tmp/test-grap-folder-XXXXXX)
TMPFILE1="$TMPDIR/file1.txt"
TMPFILE2="$TMPDIR/file3.txt"
trap "rm -rf '$TMPDIR'" EXIT

# Create test files with "folder" keyword
printf "this has folder keyword here\n" > "$TMPFILE1"
printf "another file no match here\n" > "$TMPFILE2"

# Test 1: wkappbot grap "folder" — CMD guard: dbgCmd=grap, dbgSub=folder
echo "--- Test 1: wkappbot grap folder as pattern ---"
OUT=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" grap "folder" "$TMPFILE1" 2>/dev/null || true)
if echo "$OUT" | grep -q "folder"; then
  pass "wkappbot grap found 'folder' in file"
else
  fail "wkappbot grap did not find 'folder'"
fi

# Test 2: grap "folder" — file without match returns nothing/exit 1
echo "--- Test 2: grap folder — no match file ---"
OUT2=$(WKAPPBOT_WORKER=1 timeout 15 "$WKBOT" grap "folder" "$TMPFILE2" 2>/dev/null || true)
if echo "$OUT2" | grep -q "folder"; then
  fail "grap: no-match file should not contain 'folder'"
else
  pass "grap correctly excludes non-matching file"
fi

# Test 3: grap.exe direct — verify folder keyword found
echo "--- Test 3: grap.exe direct call ---"
OUT3=$(timeout 10 "$GRAP" "folder" "$TMPFILE1" 2>/dev/null || true)
if echo "$OUT3" | grep -q "folder"; then
  pass "grap.exe found 'folder' directly"
else
  fail "grap.exe did not find 'folder'"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
