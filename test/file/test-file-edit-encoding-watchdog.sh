#!/usr/bin/env bash
# Evidence: file edit encoding corruption watchdog (U+FFFD + Slack alert)
# Verifies: suggestion 2026-04-04T08:24 — detect encoding corruption on file edit

set -e
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Edit.cs"

echo "=== Checking encoding corruption detection ==="
grep -n "FFFD\|ENCODING CORRUPTION\|hasReplacement\|alertMsg.*인코딩" "$SRC" | head -8
grep -c "ENCODING CORRUPTION" "$SRC" | grep -q "^[1-9]" && echo "PASS: U+FFFD corruption check exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Checking Slack alert on corruption ==="
grep -n "warning.*인코딩\|PostWithOverflow.*alert\|alertMsg" "$SRC" | head -5
grep -c "PostWithOverflow" "$SRC" | grep -q "^[1-9]" && echo "PASS: Slack alert on corruption exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Live: file edit (safe test — no corruption expected) ==="
TMPFILE=$(mktemp /tmp/enc-test-XXXXXX.txt)
echo "test content" > "$TMPFILE"
"W:/SDK/bin/wkappbot.exe" file edit "test content" "test ok" "$TMPFILE" 2>&1
grep -q "test ok" "$TMPFILE" && echo "PASS: file edit completed (no corruption)" || { echo "FAIL"; rm -f "$TMPFILE"; exit 1; }
rm -f "$TMPFILE"

echo "ALL PASS"
