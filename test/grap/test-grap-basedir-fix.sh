#!/bin/bash
# Evidence: grap basedir arg parsing + format sync (was suggest #58, #61)
# Fixed: GrapArgsToLogcat return order + path stripping when --basedir set
GRAP=${GRAP:-/w/SDK/bin/grap.exe}
TMPDIR=$(mktemp -d)

cat > "$TMPDIR/test.log" << 'EOF'
line one
MATCH_TARGET here
line three
another MATCH_TARGET here
line five
EOF

# Test 1: absolute path + basedir = correct pattern filtering
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --basedir "$TMPDIR" 2>/dev/null)
count=$(echo "$output" | grep -c "MATCH_TARGET")
if [ "$count" -eq 2 ]; then
  echo "PASS: basedir + abs path: 2 matches"
else
  echo "FAIL: expected 2 matches, got $count"
  rm -rf "$TMPDIR"; exit 1
fi

# Test 2: --past mode also works
output=$("$GRAP" "MATCH_TARGET" "$TMPDIR/test.log" --past 1h --basedir "$TMPDIR" 2>/dev/null)
count=$(echo "$output" | grep -c "MATCH_TARGET")
if [ "$count" -eq 2 ]; then
  echo "PASS: --past + basedir: 2 matches"
else
  echo "FAIL: --past expected 2 matches, got $count"
  rm -rf "$TMPDIR"; exit 1
fi

# Test 3: non-matching lines excluded
if echo "$output" | grep -q "line one"; then
  echo "FAIL: non-match leaked through"
  rm -rf "$TMPDIR"; exit 1
else
  echo "PASS: non-match excluded"
fi

rm -rf "$TMPDIR"
echo "ALL PASS"
exit 0
