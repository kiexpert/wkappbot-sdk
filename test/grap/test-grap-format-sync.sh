#!/bin/bash
# Test: grap pattern matching, grep compatibility, and arg parsing
# Note: $() capture sets IsOutputRedirected=true → pipe/grep-compatible format (no arrows)
# Arrow/line-number/│ formatting is terminal-only and cannot be tested via shell capture.

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
PASS=0; FAIL=0

check() {
  local desc="$1" pattern="$2" output="$3"
  if echo "$output" | grep -qE "$pattern"; then
    echo "PASS: $desc"
    PASS=$((PASS+1))
  else
    echo "FAIL: $desc — expected pattern: $pattern"
    echo "  output: $(echo "$output" | head -5)"
    FAIL=$((FAIL+1))
  fi
}

check_not() {
  local desc="$1" pattern="$2" output="$3"
  if echo "$output" | grep -qE "$pattern"; then
    echo "FAIL: $desc — unexpected pattern found: $pattern"
    echo "  output: $(echo "$output" | head -5)"
    FAIL=$((FAIL+1))
  else
    echo "PASS: $desc"
    PASS=$((PASS+1))
  fi
}

# Create temp test files
TMPDIR=$(mktemp -d)
cat > "$TMPDIR/test.log" << 'EOF'
line one
line two
MATCH_TARGET here
line four
line five
another MATCH_TARGET here
line seven
EOF

cat > "$TMPDIR/other.log" << 'EOF'
nothing here
also nothing
EOF

# === Pattern filtering tests ===

# Test 1: only matching lines returned (no --past, autoOneShot)
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --basedir "$TMPDIR" 2>/dev/null)
lines=$(echo "$output" | wc -l)
check "pattern filter: only 2 matches" "^2$" "$lines"

# Test 2: matching content present
check "match content present" "MATCH_TARGET here" "$output"

# Test 3: non-matching lines excluded
check_not "non-match excluded" "^line one$" "$output"

# === --past mode tests ===

# Test 4: --past mode also filters correctly
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --past 1h --basedir "$TMPDIR" 2>/dev/null)
lines=$(echo "$output" | grep -c "MATCH_TARGET")
check "--past: 2 matches" "^2$" "$lines"

# Test 5: --past non-match excluded
check_not "--past: non-match excluded" "^line four$" "$output"

# === basedir + absolute path arg parsing ===

# Test 6: absolute path file arg works (no explicit --basedir)
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" 2>/dev/null)
check "absolute path arg: matches found" "MATCH_TARGET" "$output"

# Test 7: absolute path + explicit --basedir
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --basedir "$TMPDIR" 2>/dev/null)
check "abs path + basedir: matches found" "MATCH_TARGET" "$output"

# === Grep compatibility (pipe mode) ===

# Test 8: pipe mode grep-compatible (no arrows, no │)
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --basedir "$TMPDIR" 2>/dev/null | cat)
check "pipe: content present" "MATCH_TARGET" "$output"
check_not "pipe: no arrow marker" "^→" "$output"

# Test 9: context lines with separator
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --basedir "$TMPDIR" -C 1 2>/dev/null)
check "context: includes surrounding lines" "line two" "$output"

# Test 10: single file = no filename prefix
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --basedir "$TMPDIR" 2>/dev/null)
check_not "single file: no filename prefix" "test\.log:" "$output"

# Test 11: empty result = exit code 1
"$GRAP" "NONEXISTENT_PATTERN" "$TMPDIR/test.log" --basedir "$TMPDIR" 2>/dev/null
rc=$?
if [ $rc -eq 1 ]; then
  echo "PASS: no match → exit code 1"
  PASS=$((PASS+1))
else
  echo "FAIL: no match → expected exit 1, got $rc"
  FAIL=$((FAIL+1))
fi

# Test 12: file grep same filtering
output=$(wkappbot file grep "MATCH_TARGET" --path "$TMPDIR" 2>/dev/null)
check "file grep: matches found" "MATCH_TARGET" "$output"

rm -rf "$TMPDIR"

echo "---"
echo "Results: $PASS PASS, $FAIL FAIL (total $((PASS+FAIL)))"
[ $FAIL -eq 0 ] && echo "ALL PASS" && exit 0 || exit 1
