#!/bin/bash
# test-file-edit-bak-subfolder.sh — Real test: file edit creates .bak/ subfolder backup (file edit)
WKBOT="/w/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test: file edit .bak/ subfolder backup ==="

TMPDIR=$(mktemp -d /tmp/test-bak-subfolder-XXXXXX)
TMPFILE="$TMPDIR/sample.txt"
BAKDIR="$TMPDIR/.bak"
trap "rm -rf '$TMPDIR'" EXIT

printf "hello world\nsecond line\nthird line\n" > "$TMPFILE"

# Run file edit — should create a backup in .bak/ subfolder
if WKAPPBOT_WORKER=1 "$WKBOT" file edit "second line" "CHANGED" "$TMPFILE" 2>/dev/null; then
  pass "file edit command succeeded"
else
  fail "file edit command failed"
fi

# Verify replacement happened
if grep -q "CHANGED" "$TMPFILE"; then
  pass "replacement applied correctly"
else
  fail "replacement not found in file"
fi

# Verify .bak/ subfolder was created
if [ -d "$BAKDIR" ]; then
  pass ".bak/ subfolder created"
else
  fail ".bak/ subfolder not found at $BAKDIR"
fi

# Verify backup file exists in .bak/
BAK_COUNT=$(ls "$BAKDIR" 2>/dev/null | wc -l)
if [ "$BAK_COUNT" -gt 0 ]; then
  pass ".bak/ contains $BAK_COUNT backup file(s)"
else
  fail ".bak/ is empty - no backup created"
fi

# Verify backup has original content
if ls "$BAKDIR"/*.txt 2>/dev/null | head -1 | xargs grep -q "second line" 2>/dev/null; then
  pass "backup file contains original content"
else
  fail "backup file missing original content"
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
