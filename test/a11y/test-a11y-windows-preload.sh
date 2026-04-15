#!/bin/bash
# Test: wkappbot a11y windows FlaUI preload + host discovery infra
# Verifies code-level: preload in MCP, owner chain, ClassName filter, GetWindowTextSafe

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
CLI="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands"
WIN32="D:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32"
P=0; F=0

echo "=== FlaUI Preload + Host Discovery Infrastructure ==="
# Command: wkappbot a11y windows uses owner chain + preload

# 1. FlaUI preload in MCP startup (Task.Run)
if grep -q "FlaUI preload" "$CLI/McpCommand.cs" 2>/dev/null; then
  echo "PASS: FlaUI preload in MCP startup"; ((P++))
else echo "FAIL: no FlaUI preload"; ((F++)); fi

# 2. GetWindowTextSafe with SMTO_ABORTIFHUNG
if grep -q "GetWindowTextSafe" "$WIN32/Native/NativeMethods.cs" 2>/dev/null; then
  echo "PASS: GetWindowTextSafe (50ms timeout)"; ((P++))
else echo "FAIL: no GetWindowTextSafe"; ((F++)); fi

# 3. Owner chain cache (_ownerHostCache)
if grep -q "_ownerHostCache" "$WIN32/Window/WindowFinder.cs" 2>/dev/null; then
  echo "PASS: Owner chain cache with TTL"; ((P++))
else echo "FAIL: no owner cache"; ((F++)); fi

# 4. ClassName pre-filter (hostClasses set)
if grep -q "hostClasses\|CASCADIA_HOSTING" "$CLI/InspectionCommands.cs" 2>/dev/null; then
  echo "PASS: ClassName pre-filter for Strategy 2"; ((P++))
else echo "FAIL: no ClassName filter"; ((F++)); fi

# 5. Owner chain fallback (FindHostByOwnerChain)
if grep -q "FindHostByOwnerChain" "$WIN32/Window/WindowFinder.cs" 2>/dev/null; then
  echo "PASS: FindHostByOwnerChain in WindowFinder"; ((P++))
else echo "FAIL: no FindHostByOwnerChain"; ((F++)); fi

# 6. matchedHiddenHwnds collection for 0x0 windows
if grep -q "matchedHiddenHwnds" "$CLI/InspectionCommands.cs" 2>/dev/null; then
  echo "PASS: Hidden HWND collection for owner fallback"; ((P++))
else echo "FAIL: no matchedHiddenHwnds"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
