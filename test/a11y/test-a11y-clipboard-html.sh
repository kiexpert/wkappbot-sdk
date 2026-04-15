#!/bin/bash
# Test: wkappbot a11y clipboard-write --html CF_HTML support
# Verifies CF_HTML clipboard format for rich paste in Gmail/Outlook

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
CLI="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands"
P=0; F=0

echo "=== Clipboard CF_HTML Support ==="

# 1. RegisterClipboardFormatW for "HTML Format"
if grep -q "RegisterClipboardFormatW" "$CLI/ClipboardCommand.cs" 2>/dev/null; then
  echo "PASS: RegisterClipboardFormatW declared"; ((P++))
else echo "FAIL: no RegisterClipboardFormatW"; ((F++)); fi

# 2. CF_HTML header format (Version:0.9 + offsets)
if grep -q "Version:0.9" "$CLI/ClipboardCommand.cs" 2>/dev/null; then
  echo "PASS: CF_HTML header format (Version:0.9)"; ((P++))
else echo "FAIL: no CF_HTML header"; ((F++)); fi

# 3. StartFragment/EndFragment markers
if grep -q "StartFragment" "$CLI/ClipboardCommand.cs" 2>/dev/null; then
  echo "PASS: Fragment markers (Start/EndFragment)"; ((P++))
else echo "FAIL: no fragment markers"; ((F++)); fi

# 4. UTF-8 encoding for CF_HTML payload
if grep -q "UTF8.GetBytes" "$CLI/ClipboardCommand.cs" 2>/dev/null; then
  echo "PASS: UTF-8 encoding for CF_HTML"; ((P++))
else echo "FAIL: no UTF-8 encoding"; ((F++)); fi

# 5. --html flag in A11yCommand
if grep -q "\-\-html" "$CLI/A11yCommand.cs" 2>/dev/null; then
  echo "PASS: --html flag in a11y clipboard-write"; ((P++))
else echo "FAIL: no --html flag"; ((F++)); fi

# 6. Dual format: CF_UNICODETEXT + CF_HTML
if grep -q "SetClipboardUnicode" "$CLI/ClipboardCommand.cs" 2>/dev/null; then
  echo "PASS: Dual format (Unicode + HTML)"; ((P++))
else echo "FAIL: no dual format"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
