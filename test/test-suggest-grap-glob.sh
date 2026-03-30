#!/bin/bash
# Test: grap file glob patterns should not be parsed as regex
# Bug: "grap *.cpp" → "Quantifier * following nothing" (regex parse error)
# Fix: LooksLikeFileGlob() detects *.ext patterns → routes to glob matching

GRAP="${GRAP:-/w/SDK/bin/grap.exe}"
PASS=0; FAIL=0

run() {
  local desc="$1"; shift
  local output
  output=$("$@" 2>&1)
  local rc=$?
  if echo "$output" | grep -qi "Quantifier\|RegexParseException\|Invalid pattern"; then
    echo "FAIL: $desc → regex error in output"
    FAIL=$((FAIL+1))
  else
    echo "PASS: $desc (rc=$rc)"
    PASS=$((PASS+1))
  fi
}

# Test cases: file glob patterns that previously caused regex errors
run "*.cpp as first arg"      "$GRAP" "*.cpp" --past 1s
run "*.cs as first arg"       "$GRAP" "*.cs" --past 1s
run "*.log as first arg"      "$GRAP" "*.log" --past 1s
run "*.txt as first arg"      "$GRAP" "*.txt" --past 1s
run "FEATURE *.cpp two args"  "$GRAP" FEATURE "*.cpp" --past 1s
run "test *.cs two args"      "$GRAP" test "*.cs" --past 1s

# Negative: actual regex should still work
output=$("$GRAP" "regex:.*test" --past 1s 2>&1)
if [ $? -le 1 ]; then
  echo "PASS: regex pattern still works"
  PASS=$((PASS+1))
else
  echo "FAIL: regex pattern broken"
  FAIL=$((FAIL+1))
fi

echo "---"
echo "Results: $PASS PASS, $FAIL FAIL (total $((PASS+FAIL)))"
[ $FAIL -eq 0 ] && echo "ALL PASS" && exit 0 || exit 1
