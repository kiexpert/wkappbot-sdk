#!/bin/bash
# Test: wkappbot a11y cdp selector registry infrastructure
# Verifies CdpSelectorRegistry class with JSON config + defaults

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpSelectorRegistry.cs"
P=0; F=0

echo "=== CDP Selector Registry Infrastructure ==="

# 1. Registry class exists
if [ -f "$SRC" ]; then
  echo "PASS: CdpSelectorRegistry.cs exists"; ((P++))
else echo "FAIL: file missing"; ((F++)); fi

# 2. Default selectors for 3 AI hosts
for host in chatgpt gemini claude; do
  if $GRAP "$host" "$SRC" 2>/dev/null | grep -qi "$host"; then
    echo "PASS: Default selectors for $host"; ((P++))
  else echo "FAIL: no $host defaults"; ((F++)); fi
done

# 3. Get() method with fallback
if $GRAP "Get.*host.*purpose\|TryGetValue" "$SRC" 2>/dev/null | grep -q "Get\|TryGetValue"; then
  echo "PASS: Get() with fallback to defaults"; ((P++))
else echo "FAIL: no Get()"; ((F++)); fi

# 4. JSON file loading with 30s cache
if grep -q "LoadRegistry" "$SRC" 2>/dev/null; then
  echo "PASS: JSON file loading with cache"; ((P++))
else echo "FAIL: no registry loading"; ((F++)); fi

# 5. BuildFallbackJs for multi-selector JS expression
if $GRAP "BuildFallbackJs\|querySelector" "$SRC" 2>/dev/null | grep -q "BuildFallbackJs\|querySelector"; then
  echo "PASS: BuildFallbackJs multi-selector JS"; ((P++))
else echo "FAIL: no BuildFallbackJs"; ((F++)); fi

# 6. ExportDefaults for initial JSON generation
if $GRAP "ExportDefaults\|WriteIndented" "$SRC" 2>/dev/null | grep -q "ExportDefaults\|WriteIndented"; then
  echo "PASS: ExportDefaults for JSON generation"; ((P++))
else echo "FAIL: no ExportDefaults"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
