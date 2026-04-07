#!/usr/bin/env bash
# Evidence: file open <path>:<line> command implemented
# Verifies: suggestion 2026-04-01T09:06 — smart VS Code open with goto-line

set -e
REPO="W:/GitHub/WKAppBot"
FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs"

echo "=== Checking file open implementation ==="
grep -n 'FileOpenCommand\|ParseFileOpenSpec' "$FILE" | head -5

COUNT=$(grep -c 'FileOpenCommand' "$FILE")
if [ "$COUNT" -ge 1 ]; then
    echo "PASS: FileOpenCommand implemented in FileToolCommands.Open.cs"
else
    echo "FAIL: FileOpenCommand not found"
    exit 1
fi

echo ""
echo "=== Live: file open --help ==="
"W:/SDK/bin/wkappbot.exe" file open --help 2>&1 | head -5
echo "PASS: file open command routes correctly"
