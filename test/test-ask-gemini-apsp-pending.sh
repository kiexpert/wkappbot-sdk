#!/bin/bash
# test-ask-gemini-apsp-pending.sh — verify non-loop ask gemini not blocked by stale APSP tool calls
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
checkgrep() { if grep -q "$1" "$2" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL: pattern '$1' not found in $2"; FAIL=$((FAIL+1)); fi; }
checknotgrep() { if grep -q "$1" "$2" >/dev/null 2>&1; then echo "FAIL: bad pattern '$1' still present in $2"; FAIL=$((FAIL+1)); else echo "PASS"; PASS=$((PASS+1)); fi; }

echo "=== ask gemini APSP Pending Stuck Fix Test ==="
GEMINI="$ROOT/csharp/src/WKAppBot.CLI/Commands/AskCommands.Gemini.cs"

echo -n "Test 1: AskGemini function exists... "
checkgrep "AskGemini\|static.*Gemini" "$GEMINI"

echo -n "Test 2: loopMode guard on post-persona tool call skip... "
checkgrep "loopMode && latePersonaResp.Contains" "$GEMINI"

echo -n "Test 3: Non-loop informational log present... "
checkgrep "Non-loop ask.*ignoring stale APSP\|non-loop.*stale APSP" "$GEMINI"

echo -n "Test 4: loopMode guard on persona early tool call... "
checkgrep "loopMode && effectiveLoopPersona && stablePersonaResp.Contains" "$GEMINI"

echo -n "Test 5: All 3 APSP skip paths are guarded by loopMode... "
# Verify each "skipping question send" is preceded by a loopMode guard in its if-condition
SKIP_COUNT=$(grep -c "skipping question send\|Late persona tool call\|Persona continuation has tool call\|Post-persona tool call" "$GEMINI")
LOOP_GUARD_COUNT=$(grep -c "loopMode && effectiveLoopPersona\|loopMode && latePersonaResp" "$GEMINI")
if [ "$LOOP_GUARD_COUNT" -ge 3 ]; then
    echo "PASS ($LOOP_GUARD_COUNT loopMode guards found)"
    PASS=$((PASS+1))
else
    echo "FAIL: only $LOOP_GUARD_COUNT loopMode guards, expected 3+"
    FAIL=$((FAIL+1))
fi

echo -n "Test 6: hasLoopPersonaState variable used... "
checkgrep "hasLoopPersonaState" "$GEMINI"

echo -n "Test 7: effectiveLoopPersona still used for loop-mode paths... "
checkgrep "effectiveLoopPersona" "$GEMINI"

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
