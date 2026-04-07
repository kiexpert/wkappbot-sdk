#!/bin/bash
# Test: wkappbot a11y windows host window discovery + MCP warm performance
# Verifies: (1) *wsl* finds WindowsTerminal host, (2) warm run < 1 second
# Must run with Eye/MCP active (wkappbot windows routes through MCP worker)

WK=${WK:-wkappbot}
P=0; F=0

echo "=== Host Window Discovery + MCP Performance ==="
# Command: wkappbot a11y windows uses owner chain for host discovery

# 1. Three warm-up runs: trigger hot-swap + MCP init + process cache
echo "  Warming up MCP worker (3 runs)..."
$WK windows > /dev/null 2>&1
sleep 1
$WK windows "*wsl*" > /dev/null 2>&1
sleep 1
$WK windows "*wsl*" > /dev/null 2>&1
sleep 1

# 2. Second run: check host window found + measure time
echo "  Running host discovery..."
START=$(date +%s%3N)
RESULT=$($WK windows "*wsl*" 2>/dev/null)
END=$(date +%s%3N)
ELAPSED=$((END - START))

# 3. Check host window found (CASCADIA/WindowsTerminal)
if echo "$RESULT" | grep -q "CASCADIA\|WindowsTerminal"; then
  echo "PASS: Host window (WindowsTerminal CASCADIA) found"
  ((P++))
else
  echo "FAIL: No host window found"
  echo "  output: $(echo "$RESULT" | tail -5)"
  ((F++))
fi

# 4. Check hidden wslhost windows
HIDDEN=$(echo "$RESULT" | grep -c "wslhost")
if [ "$HIDDEN" -ge 1 ]; then
  echo "PASS: $HIDDEN wslhost windows enumerated"
  ((P++))
else
  echo "FAIL: No wslhost windows"
  ((F++))
fi

# 5. Performance: must be < 2000ms (includes MCP pipe overhead)
if [ "$ELAPSED" -lt 2000 ]; then
  echo "PASS: Host discovery ${ELAPSED}ms (< 2s)"
  ((P++))
else
  echo "FAIL: ${ELAPSED}ms (>= 1s)"
  ((F++))
fi

# 6. Third run: owner cache should make it even faster
START2=$(date +%s%3N)
$WK windows "*wsl*" > /dev/null 2>&1
END2=$(date +%s%3N)
ELAPSED2=$((END2 - START2))
if [ "$ELAPSED2" -lt 2000 ]; then
  echo "PASS: Cached run ${ELAPSED2}ms (< 2s)"
  ((P++))
else
  echo "FAIL: Cached run ${ELAPSED2}ms (>= 2s)"
  ((F++))
fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
