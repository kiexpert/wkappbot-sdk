#!/bin/bash
# test-suggest-show-list.sh — verify suggest list actually runs and shows pending suggestions
# Command under test: wkappbot suggest list
PASS=0; FAIL=0

echo "=== Suggest Show/List Real Execution Test ==="

OUT=$(WKAPPBOT_WORKER=1 timeout 15 wkappbot suggest list 2>/dev/null)
CODE=$?

echo "Exit code: $CODE"

# Test 1: command exits 0
echo -n "Test 1: suggest list exits 0... "
if [ $CODE -eq 0 ]; then
    echo "PASS"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE)"
    FAIL=$((FAIL+1))
fi

# Test 2: shows "Pending: N suggestion(s)"
echo -n "Test 2: shows pending count... "
if echo "$OUT" | grep -q "Pending:.*suggestion"; then
    COUNT=$(echo "$OUT" | grep "Pending:" | grep -oP '\d+')
    echo "PASS ($COUNT pending)"
    PASS=$((PASS+1))
else
    echo "FAIL (no pending count in output)"
    FAIL=$((FAIL+1))
fi

# Test 3: shows timestamp entries (MM-DD HH:MM format)
echo -n "Test 3: shows timestamped entries... "
if echo "$OUT" | grep -qP "\d{2}-\d{2} \d{2}:\d{2}"; then
    echo "PASS (timestamp entries found)"
    PASS=$((PASS+1))
else
    echo "FAIL (no timestamp entries)"
    FAIL=$((FAIL+1))
fi

# Test 4: resolve hint shown
echo -n "Test 4: resolve hint shown... "
if echo "$OUT" | grep -q "resolve"; then
    echo "PASS (resolve hint present)"
    PASS=$((PASS+1))
else
    echo "FAIL (no resolve hint)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
