#!/bin/bash
# test-web-tab-dedup-real.sh — verify web open uses sandboxed tab path (not legacy about:blank)
# Command under test: wkappbot web open <url>
# Evidence for: BUG FIX web open/navigate tab dedup (ts=2026-03-30T16:14:09)
#
# What this verifies:
#   - ConnectCdp is called WITH navigateUrl → sandboxed tab reuse path is active
#   - Legacy "Switched to WebBot tab" (about:blank ConnectCdp) path is NOT used
#   - [SANDBOX] marker appears in stdout (sandbox path, not legacy path)
PASS=0; FAIL=0
TEST_URL="https://google.com"

echo "=== Web Tab Dedup Sandbox Path Test ==="

# Run wkappbot web open, capture stdout (timeout=30s to avoid hang)
# Command may fail with "WebBot bar not found" if Chrome isn't WebBot-launched — that's OK.
# We only care whether the SANDBOX path was taken (not the legacy about:blank path).
echo "Running: wkappbot web open $TEST_URL ..."
OUT=$(timeout 30 wkappbot web open "$TEST_URL" 2>/dev/null)
echo "$OUT"
echo ""

# Test 1: [SANDBOX] appears → sandboxed tab path was used (not legacy)
echo -n "Test 1: [SANDBOX] path used (not legacy ConnectCdp)... "
if echo "$OUT" | grep -q "\[SANDBOX\]"; then
    echo "PASS ([SANDBOX] confirmed — ConnectCdp called with navigateUrl)"
    PASS=$((PASS+1))
else
    echo "FAIL ([SANDBOX] not found — may be using legacy about:blank path)"
    FAIL=$((FAIL+1))
fi

# Test 2: Legacy path NOT used
echo -n "Test 2: No legacy 'Switched to WebBot tab' output... "
if echo "$OUT" | grep -q "Switched to WebBot tab"; then
    echo "FAIL (legacy about:blank path still active)"
    FAIL=$((FAIL+1))
else
    echo "PASS"
    PASS=$((PASS+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
