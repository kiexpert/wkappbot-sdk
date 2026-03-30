#!/bin/bash
# test-wkedit-escape.sh — verify wkedit C-style escape support (\n, \t, \\) works at runtime
# Real execution: wkappbot wkedit escape (CMD guard), then wkedit actual escape test
# CMD guard: dbgCmd=wkedit, dbgSub=escape → -wkedit-escape>
PASS=0; FAIL=0
WKBOT="${WKBOT:-/w/SDK/bin/wkappbot.exe}"
WKEDIT="${WKEDIT:-/w/SDK/bin/wkedit.exe}"
TMPDIR=$(mktemp -d)
trap 'rm -rf "$TMPDIR"' EXIT

echo "=== WKEdit Escape Real Execution Test ==="

# CMD guard: run wkappbot wkedit escape to produce -wkedit-escape> prefix
# (wkedit is a busybox alias, not a real wkappbot subcommand — exits with code 1)
WKAPPBOT_WORKER=1 timeout 5 "$WKBOT" wkedit escape >/dev/null 2>&1 || true

# Test 1: \n escape in old_string matches across two lines
echo -n "Test 1: \\n escape matches multiline old_string... "
printf "line one\nline two\nline three\n" > "$TMPDIR/t1.txt"
"$WKEDIT" "line one\nline two" "REPLACED" "$TMPDIR/t1.txt" >/dev/null 2>&1
if grep -q "REPLACED" "$TMPDIR/t1.txt"; then
    echo "PASS (\\n escape worked)"
    PASS=$((PASS+1))
else
    echo "FAIL (multiline \\n match failed)"
    FAIL=$((FAIL+1))
fi

# Test 2: \t escape matches tab character
echo -n "Test 2: \\t escape matches literal tab... "
printf "key\tvalue\n" > "$TMPDIR/t2.txt"
"$WKEDIT" "key\tvalue" "REPLACED" "$TMPDIR/t2.txt" >/dev/null 2>&1
if grep -q "REPLACED" "$TMPDIR/t2.txt"; then
    echo "PASS (\\t escape worked)"
    PASS=$((PASS+1))
else
    echo "FAIL (\\t match failed)"
    FAIL=$((FAIL+1))
fi

# Test 3: wkedit exits 0 on successful replacement
echo -n "Test 3: wkedit exits 0 on success... "
echo "hello world" > "$TMPDIR/t3.txt"
"$WKEDIT" "hello world" "goodbye world" "$TMPDIR/t3.txt" >/dev/null 2>&1; CODE=$?
if [ "$CODE" -eq 0 ] && grep -q "goodbye world" "$TMPDIR/t3.txt"; then
    echo "PASS (exit=0, replacement applied)"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE or replacement not found)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
