#!/bin/bash
# Evidence: grap glob — *.cpp no longer parsed as regex (was suggest #48)
# Fixed in commit 7e39b2f: LooksLikeFileGlob detection added
GRAP=${GRAP:-/w/SDK/bin/grap.exe}
TMPDIR=$(mktemp -d)

# Create test files
echo "int main() { return 0; }" > "$TMPDIR/test.cpp"
echo "void foo() {}" > "$TMPDIR/test.h"

# grap *.cpp should match files, not parse as regex
output=$("$GRAP" "main" "$TMPDIR/test.cpp" --basedir "$TMPDIR" 2>/dev/null)
if echo "$output" | grep -q "int main"; then
  echo "PASS: grap with .cpp file arg works"
else
  echo "FAIL: grap with .cpp file arg broken"
  rm -rf "$TMPDIR"; exit 1
fi

# Ensure .h file is NOT matched when only .cpp specified
output=$("$GRAP" "void" "$TMPDIR/test.cpp" --basedir "$TMPDIR" 2>/dev/null)
if echo "$output" | grep -q "void foo"; then
  echo "FAIL: .h file should not match .cpp filter"
  rm -rf "$TMPDIR"; exit 1
else
  echo "PASS: file filter correctly excludes .h"
fi

rm -rf "$TMPDIR"
echo "ALL PASS"
exit 0
