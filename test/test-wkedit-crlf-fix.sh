#!/bin/bash
# Evidence: wkedit crlf — --old-file CRLF/LF normalization (was suggest #54, #55, #56)
# Fixed in commit f04eb08: auto line-ending normalization
WKEDIT=${WKEDIT:-/w/SDK/bin/wkedit.exe}
TMPDIR=$(mktemp -d)

# Create a file with CRLF line endings
printf "line one\r\nline two\r\nline three\r\n" > "$TMPDIR/target.txt"

# Create old-file with LF endings (simulates bash-generated temp file)
printf "line two" > "$TMPDIR/old.txt"
printf "REPLACED" > "$TMPDIR/new.txt"

# wkedit should handle CRLF/LF mismatch via --old-file
"$WKEDIT" --old-file "$TMPDIR/old.txt" --new-file "$TMPDIR/new.txt" "$TMPDIR/target.txt" 2>/dev/null
rc=$?

if [ $rc -eq 0 ]; then
  if grep -q "REPLACED" "$TMPDIR/target.txt"; then
    echo "PASS: wkedit CRLF/LF normalization works"
  else
    echo "FAIL: replacement not applied"
    exit 1
  fi
else
  echo "FAIL: wkedit exit code $rc"
  exit 1
fi

rm -rf "$TMPDIR"
echo "ALL PASS"
exit 0
