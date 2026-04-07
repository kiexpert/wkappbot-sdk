#!/bin/bash
# test-a11y-windows-wsl.sh — verify a11y windows owner-chain fallback for hidden process windows (WSL/wslhost fix)
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== a11y windows Owner-Chain Fallback Fix Test ==="
WIN="$ROOT/csharp/src/WKAppBot.CLI/Commands/InspectionCommands.Windows.cs"

echo -n "Test 1: matchedHiddenHwnds owner-chain candidate check exists... "; check grep -q "matchedHiddenHwnds\.Add" "$WIN"
echo -n "Test 2: Owner chain check no longer guarded by !hasFilter... "; check bash -c "! grep -q '!hasFilter && ownerCandidateMatcher' '$WIN'"
echo -n "Test 3: ownerCandidateMatcher != null && info == null condition... "; check grep -q "ownerCandidateMatcher != null && info == null" "$WIN"
echo -n "Test 4: Process name matched against ownerCandidateMatcher in check... "; check grep -q "ownerCandidateMatcher\.IsMatch.*fpn\|fpn.*IsMatch" "$WIN"
echo -n "Test 5: Owner chain fallback strategy 1 + strategy 2... "; check grep -q "Strategy 1\|Strategy 2" "$WIN"
echo -n "Test 6: CASCADIA_HOSTING_WINDOW_CLASS in host class whitelist... "; check grep -q "CASCADIA_HOSTING_WINDOW_CLASS" "$WIN"
echo -n "Test 7: filterTitle triggers matchedHiddenHwnds owner fallback... "; check grep -q "filterTitle != null.*visibleResultCount == 0.*matchedHiddenHwnds\|filterTitle != null.*matchedHiddenHwnds" "$WIN"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
