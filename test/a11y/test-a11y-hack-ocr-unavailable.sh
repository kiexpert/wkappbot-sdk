#!/usr/bin/env bash
# test-a11y-hack-ocr-unavailable.sh — verify SimpleOcrAnalyzer ctor exception is caught gracefully
# Fix: RunOcrWorker now wraps new SimpleOcrAnalyzer() in try-catch, returns early on failure
PASS=0; FAIL=0

check() {
    local label="$1"; local cmd="$2"
    if eval "$cmd" > /dev/null 2>&1; then
        echo "[PASS] $label"; PASS=$((PASS+1))
    else
        echo "[FAIL] $label"; FAIL=$((FAIL+1))
    fi
}

SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/A11yHackCommand.OcrWorker.cs"

# Fix: constructor is wrapped in try-catch
check "SimpleOcrAnalyzer ctor in try-catch" \
    "grep -q 'try.*{.*ocr = new SimpleOcrAnalyzer\|try { ocr = new SimpleOcrAnalyzer' '$SRC'"

# Fix: catch with return (graceful skip)
check "catch block returns early (no crash)" \
    "grep -q 'OCR.*unavailable.*skipping' '$SRC'"

# Fix: using var _ = ocr (disposal preserved)
check "ocr disposed via using" \
    "grep -q 'using var _ = ocr' '$SRC'"

# No unhandled exception: wkappbot a11y windows runs (smoke test)
check "wkappbot a11y windows smoke test" \
    "! \"D:/SDK/bin/wkappbot.exe\" a11y windows 2>&1 | grep -qa 'FileNotFoundException\|Unhandled exception'"

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ]
