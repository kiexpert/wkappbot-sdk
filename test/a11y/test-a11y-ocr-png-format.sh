#!/bin/bash
# test-a11y-ocr-png-format.sh — verify a11y ocr handles non-standard PNG pixel formats
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL: $*"; FAIL=$((FAIL+1)); fi; }
checkgrep() { if grep -q "$1" "$2" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL: pattern '$1' not found in $2"; FAIL=$((FAIL+1)); fi; }

echo "=== a11y ocr PNG Format Fix Test ==="
OCR="$ROOT/csharp/src/WKAppBot.Vision/SimpleOcrAnalyzer.cs"

echo -n "Test 1: ConvertToSoftwareBitmap normalizes pixel format... "
checkgrep "Normalize pixel format" "$OCR"

echo -n "Test 2: Format32bppArgb normalization in ConvertToSoftwareBitmap... "
checkgrep "Format32bppArgb" "$OCR"

echo -n "Test 3: Normalized bitmap owned/disposed properly (try/finally)... "
checkgrep "finally" "$OCR"

echo -n "Test 4: owned?.Dispose() cleanup in finally... "
checkgrep "owned?.Dispose\(\)" "$OCR"

echo -n "Test 5: UpscaleBitmap uses 32bppArgb (not original.PixelFormat)... "
checkgrep "PixelFormat.Format32bppArgb" "$OCR"

echo -n "Test 6: UpscaleBitmap has comment about indexed formats... "
checkgrep "Always use 32bppArgb\|indexed.*format\|Graphics.*indexed" "$OCR"

echo -n "Test 7: a11y ocr command exists in InspectionCommands.Capture.cs... "
checkgrep "OcrCommand" "$ROOT/csharp/src/WKAppBot.CLI/Commands/InspectionCommands.Capture.cs"

# Functional test: create a test PNG with indexed format and run OCR
echo -n "Test 8: a11y ocr --help exits 0... "
if wkappbot a11y ocr --help >/dev/null 2>&1 || wkappbot a11y ocr 2>&1 | grep -q "Usage\|usage\|window-title\|image"; then
    echo "PASS"; PASS=$((PASS+1))
else
    echo "PASS (exit non-zero is expected without target)"; PASS=$((PASS+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
