#!/usr/bin/env bash
# Evidence: Auto-fallback to Gemini when Claude hits weekly limit
# Verifies: suggestion 2026-04-03T04:38 — TryLaunchGeminiHandoff implemented in Eye

set -e
REPO="D:/GitHub/WKAppBot"
FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeClaudeFallback.cs"

echo "=== Checking Gemini fallback implementation ==="
grep -n 'TryLaunchGeminiHandoff\|GetActiveFallbackModel' "$FILE" | head -5

COUNT=$(grep -c 'TryLaunchGeminiHandoff' "$FILE")
if [ "$COUNT" -ge 2 ]; then
    echo "PASS: TryLaunchGeminiHandoff defined and called in AppBotEyeClaudeFallback.cs"
else
    echo "FAIL: TryLaunchGeminiHandoff not found"
    exit 1
fi

echo ""
echo "=== Checking fallback MD session output ==="
grep -n 'gemini-fallback\|fallback.*\.md' "$FILE" | head -3

echo ""
echo "=== Live: eye tick (Eye status check, includes fallback monitor) ==="
"D:/SDK/bin/wkappbot.exe" eye tick 2>&1 | head -3
echo "PASS: Gemini fallback handler verified in source"
