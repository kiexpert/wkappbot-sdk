#!/bin/bash
# Test: grap file glob patterns should not be parsed as regex
# Bug: "grap *.cpp" → "Quantifier * following nothing" (regex parse error)
# Fix: LooksLikeFileGlob() detects *.ext patterns → routes to glob matching
# Requires: grap command available in PATH

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

# File glob as first arg (was: regex parse error)
run "grap *.cpp"              grap "*.cpp" --past 1s
run "grap *.cs"               grap "*.cs" --past 1s
run "grap *.log"              grap "*.log" --past 1s
run "grap *.txt"              grap "*.txt" --past 1s
run "grap *.json"             grap "*.json" --past 1s
run "grap *.yaml"             grap "*.yaml" --past 1s
run "grap *.xml"              grap "*.xml" --past 1s
run "grap ?.log"              grap "?.log" --past 1s

# File glob as second arg
run "grap FEATURE *.cpp"      grap FEATURE "*.cpp" --past 1s
run "grap test *.cs"          grap test "*.cs" --past 1s
run "grap error *.log"        grap error "*.log" --past 1s

# Regex still works
run "grap regex:.*test"       grap "regex:.*test" --past 1s

# Normal content pattern (no glob)
run "grap keyword"            grap "keyword" --past 1s
run "grap FEATURE"            grap "FEATURE" --past 1s

echo "---"
echo "Results: $PASS PASS, $FAIL FAIL (total $((PASS+FAIL)))"
[ $FAIL -eq 0 ] && echo "ALL PASS" && exit 0 || exit 1
