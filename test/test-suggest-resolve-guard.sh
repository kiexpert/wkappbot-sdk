#!/bin/bash
# test-suggest-resolve-guard.sh — verify resolve guard actually blocks invalid inputs
# Command under test: wkappbot suggest resolve
#
# Previous version was grep-only (checked SuggestCommand.cs for strings).
# This version actually RUNS suggest resolve and verifies guard behavior.
PASS=0; FAIL=0
FLAG="--i-completed-the-code-and-built-successfully-and-deployed-and-tested-with-real-scenarios-and-confirmed-meaningful-results-and-have-evidence-and-willkim-allowed-this-script"

echo "=== Suggest Resolve Guard — Real Execution Test ==="

# Test 1: No confirm flag → guard must block
echo -n "Test 1: No flag → must exit non-zero with guard message... "
OUT=$(WKAPPBOT_WORKER=1 wkappbot suggest resolve "1970-01-01T00:00:00" "test" 2>/dev/null)
CODE=$?
if [ $CODE -ne 0 ] && echo "$OUT" | grep -qi "RESOLVE GUARD\|willkim\|evidence"; then
    echo "PASS (exit=$CODE, guard message shown)"
    PASS=$((PASS+1))
else
    echo "FAIL (expected non-zero + guard msg, got exit=$CODE)"
    FAIL=$((FAIL+1))
fi

# Test 2: Evidence file not found → must block
echo -n "Test 2: Missing evidence file → must exit non-zero... "
OUT=$(WKAPPBOT_WORKER=1 wkappbot suggest resolve "1970-01-01T00:00:00" "test" $FLAG /tmp/nonexistent-xyz-abc.sh 2>/dev/null)
CODE=$?
if [ $CODE -ne 0 ]; then
    echo "PASS (exit=$CODE, missing file blocked)"
    PASS=$((PASS+1))
else
    echo "FAIL (should have blocked missing file, got exit=0)"
    FAIL=$((FAIL+1))
fi

# Test 3: Bad filename convention (not test-cmd-subcmd-*.sh) → must block
TMP_BAD=$(mktemp /tmp/badname-XXXX.sh)
echo '#!/bin/bash; echo ok' > "$TMP_BAD"
chmod +x "$TMP_BAD"
echo -n "Test 3: Bad filename convention → must exit non-zero... "
OUT=$(WKAPPBOT_WORKER=1 wkappbot suggest resolve "1970-01-01T00:00:00" "test" $FLAG "$TMP_BAD" 2>/dev/null)
CODE=$?
rm -f "$TMP_BAD"
if [ $CODE -ne 0 ]; then
    echo "PASS (exit=$CODE, bad filename blocked)"
    PASS=$((PASS+1))
else
    echo "FAIL (should have blocked bad filename, got exit=0)"
    FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
