#!/bin/bash
# Evidence: file edit with slash in old_string works (was suggest #7-#10)
# Tests: wkedit "a/b" → no IOException, replacement works correctly
WKEDIT=${WKEDIT:-/w/SDK/bin/wkedit.exe}
TMPDIR=$(mktemp -d)

# Test 1: slash in old_string
echo "test a/b content" > "$TMPDIR/t1.txt"
"$WKEDIT" "a/b" "c" "$TMPDIR/t1.txt" 2>/dev/null
if grep -q "test c content" "$TMPDIR/t1.txt"; then
  echo "PASS: file edit slash in old_string"
else
  echo "FAIL: slash replacement failed"; rm -rf "$TMPDIR"; exit 1
fi

# Test 2: backslash in old_string (wkedit uses C-style escapes: \\ = literal backslash)
echo "path\\to\\file here" > "$TMPDIR/t2.txt"
"$WKEDIT" 'path\\to\\file' "replaced" "$TMPDIR/t2.txt" 2>/dev/null
if grep -q "replaced here" "$TMPDIR/t2.txt"; then
  echo "PASS: file edit backslash in old_string"
else
  echo "FAIL: backslash replacement failed"; rm -rf "$TMPDIR"; exit 1
fi

# Test 3: Windows-style path in old_string
echo "open C:/Users/test/file.txt done" > "$TMPDIR/t3.txt"
"$WKEDIT" "C:/Users/test/file.txt" "D:/other.txt" "$TMPDIR/t3.txt" 2>/dev/null
if grep -q "open D:/other.txt done" "$TMPDIR/t3.txt"; then
  echo "PASS: file edit Windows path in old_string"
else
  echo "FAIL: Windows path replacement failed"; rm -rf "$TMPDIR"; exit 1
fi

rm -rf "$TMPDIR"
echo "ALL PASS"
exit 0
