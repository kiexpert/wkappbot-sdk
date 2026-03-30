#!/bin/bash
# Evidence: file edit --undo restore-then-edit in one shot
WKEDIT=${WKEDIT:-/w/SDK/bin/wkedit.exe}
TMPDIR=$(mktemp -d)

# Setup: create file and make an edit (generates backup)
echo "original content here" > "$TMPDIR/test.txt"
"$WKEDIT" "original" "modified" "$TMPDIR/test.txt" 2>/dev/null

# Find backup timestamp
BAK=$(ls "$TMPDIR"/test.txt.bak-*.txt 2>/dev/null | head -1)
TS=$(basename "$BAK" | sed 's/test\.txt\.bak-\([0-9-]*\)\..*/\1/')

if [ -z "$TS" ]; then
  echo "FAIL: no backup found after initial edit"; rm -rf "$TMPDIR"; exit 1
fi

# Test 1: current file should be modified
if grep -q "modified content here" "$TMPDIR/test.txt"; then
  echo "PASS: initial edit applied"
else
  echo "FAIL: initial edit not applied"; rm -rf "$TMPDIR"; exit 1
fi

# Test 2: --undo restores and applies new edit
"$WKEDIT" --undo "$TS" "original" "UNDO_EDITED" "$TMPDIR/test.txt" 2>/dev/null
if grep -q "UNDO_EDITED content here" "$TMPDIR/test.txt"; then
  echo "PASS: --undo restored backup and applied edit"
else
  echo "FAIL: --undo + edit result wrong: $(cat "$TMPDIR/test.txt")"
  rm -rf "$TMPDIR"; exit 1
fi

# Test 3: safety backup created (undo-of-undo)
UNDO_BAK=$(ls "$TMPDIR"/test.txt.undo-*.txt 2>/dev/null | head -1)
if [ -n "$UNDO_BAK" ]; then
  echo "PASS: safety backup created for undo-of-undo"
else
  echo "FAIL: no safety backup found"; rm -rf "$TMPDIR"; exit 1
fi

# Test 4: safety backup contains the modified version
if grep -q "modified content here" "$UNDO_BAK"; then
  echo "PASS: safety backup has pre-undo content"
else
  echo "FAIL: safety backup content wrong"; rm -rf "$TMPDIR"; exit 1
fi

# Test 5: --undo with non-existent timestamp fails
"$WKEDIT" --undo "99999999-999999" "x" "y" "$TMPDIR/test.txt" 2>/dev/null
rc=$?
if [ $rc -ne 0 ]; then
  echo "PASS: --undo with bad timestamp fails gracefully"
else
  echo "FAIL: --undo with bad timestamp should fail"; rm -rf "$TMPDIR"; exit 1
fi

rm -rf "$TMPDIR"
echo "ALL PASS"
exit 0
