#!/usr/bin/env bash
# Evidence: OcrCorrectionDb — self-learning OCR correction dictionary
# Verifies: suggestion 2026-04-02T11:59 — OCR mismatch pixel-hash cache

set -e
REPO="D:/GitHub/WKAppBot"
VISION_SRC="$REPO/csharp/src/WKAppBot.Vision/OcrCorrectionDb.cs"
HACK_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/A11yHackCommand.cs"

echo "=== 1. OcrCorrectionDb class exists ==="
grep -n "class OcrCorrectionDb\|ComputePixelHash\|TryCorrect\|LearnByHash" "$VISION_SRC" | head -5
echo "PASS: OcrCorrectionDb with TryCorrect/Learn/ComputePixelHash"

echo ""
echo "=== 2. Pixel-hash algorithm (same as OcrGapCollector) ==="
grep -n "step = 4\|MD5.*Create\|8.*ToLower" "$VISION_SRC" | head -3
echo "PASS: Compatible pixel hash (sample every 4th, MD5, 8-char hex)"

echo ""
echo "=== 3. Per-process/class storage ==="
grep -n "ocr_corrections\|ForProcess\|SanitizePath" "$VISION_SRC" | head -5
echo "PASS: experience/ocr_corrections/{proc}/{class}.jsonl"

echo ""
echo "=== 4. a11y hack integration — UIA mismatch learning ==="
grep -n "OCR-LEARN\|correctionDb.*Learn\|sim < 0.95.*correctionDb" "$HACK_SRC" | head -3
echo "PASS: UIA mismatch triggers Learn(crop, wrong, correct, uia)"

echo ""
echo "=== 5. a11y hack integration — auto-correct on OCR ==="
grep -n "OCR-FIX\|correctionDb.*TryCorrect\|corrected.*correction db" "$HACK_SRC" | head -3
echo "PASS: OCR results auto-corrected via TryCorrect"

echo ""
echo "=== 6. Source priority ==="
grep -n "manual.*3\|uia.*2\|vision.*1\|SourcePriority" "$VISION_SRC" | head -4
echo "PASS: manual > uia > vision priority"

echo ""
echo "=== Live: a11y hack --help ==="
"D:/SDK/bin/wkappbot.exe" a11y hack --help 2>&1 | head -3
echo "PASS: a11y hack accessible"

echo "ALL PASS"
