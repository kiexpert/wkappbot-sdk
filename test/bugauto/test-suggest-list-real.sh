#!/usr/bin/env bash
set -euo pipefail

ROOT="/w/GitHub/WKAppBot"
EXE="W:\\SDK\\bin\\wkappbot-core.exe"
PASS=0
FAIL=0

cd "$ROOT"

echo "=== Suggest Show/List Real Execution Test ==="

OUT="$(powershell.exe -NoProfile -Command "& '$EXE' suggest list | Out-String -Width 4096")"
CODE=$?

echo "Exit code: $CODE"
echo "$OUT"

echo -n "Test 1: suggest list exits 0... "
if [ "$CODE" -eq 0 ]; then
    echo "PASS"
    PASS=$((PASS+1))
else
    echo "FAIL (exit=$CODE)"
    FAIL=$((FAIL+1))
fi

echo -n "Test 2: shows pending count... "
if [[ "$OUT" == *"Pending:"* ]]; then
    echo "PASS"
    PASS=$((PASS+1))
else
    echo "FAIL (no pending count in output)"
    FAIL=$((FAIL+1))
fi

echo -n "Test 3: shows timestamped entries... "
if [[ "$OUT" =~ ts=20[0-9]{2}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2} ]]; then
    echo "PASS (timestamp entries found)"
    PASS=$((PASS+1))
else
    echo "FAIL (no timestamp entries)"
    FAIL=$((FAIL+1))
fi

echo -n "Test 4: resolve hint shown... "
if [[ "$OUT" == *'resolve: wkappbot suggest resolve <ts> "note"'* ]]; then
    echo "PASS (resolve hint present)"
    PASS=$((PASS+1))
else
    echo "FAIL (no resolve hint)"
    FAIL=$((FAIL+1))
fi

echo
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
