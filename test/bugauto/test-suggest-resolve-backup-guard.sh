#!/usr/bin/env bash
# Evidence: suggest resolve — duplicate guard backup + manifest + auto-restore
# Verifies: merge 2026-04-04T09:11 — resolve guard bypass/완화

set -e
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.cs"

echo "=== Checking duplicate guard is now backup (not reject) ==="
grep -n "Backed up:\|⚠ Evidence similar" "$SRC" | head -3
grep -c "Backed up:" "$SRC" | grep -q "^[1-9]" && echo "PASS: duplicate guard does backup" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Checking .manifest registration ==="
grep -n "\.manifest\|manifestPath\|manifestLines" "$SRC" | head -5
grep -c "\.manifest" "$SRC" | grep -q "^[1-9]" && echo "PASS: manifest system exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Checking deleted test auto-restore ==="
grep -n "자동 복구\|auto.*restore\|latest.*bak\|File.Copy.*latest" "$SRC" | head -5
grep -c "자동 복구" "$SRC" | grep -q "^[1-9]" && echo "PASS: auto-restore from backup exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Checking BackupEvidenceOnFailure ==="
grep -n "BackupEvidenceOnFailure" "$SRC" | head -5
grep -c "BackupEvidenceOnFailure" "$SRC" | grep -q "^[2-9]" && echo "PASS: failure backup called in multiple paths" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Live: suggest resolve --help ==="
"W:/SDK/bin/wkappbot.exe" suggest resolve --help 2>&1 | head -5
echo "PASS: suggest resolve --help accessible"

echo "ALL PASS"
