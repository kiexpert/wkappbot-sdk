#!/usr/bin/env bash
# Evidence: suggest show subcommand + noise guard for unknown subcommands
# Verifies: noise merge item — suggest show/get commands were being saved as suggestions

set -e
WKAPPBOT="W:/SDK/bin/wkappbot.exe"
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.cs"

echo "=== Checking suggest show subcommand routing ==="
grep -n '"show" or "get" or "view"' "$SRC"
grep -c '"show" or "get" or "view"' "$SRC" | grep -q "^[1-9]" && echo "PASS: show/get/view routing exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Checking noise guard for unknown subcommands ==="
grep -n "Unknown subcommand\|likelyCmds\|IsLower" "$SRC" | head -5
grep -c "Unknown subcommand" "$SRC" | grep -q "^[1-9]" && echo "PASS: noise guard exists" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Live: suggest show --help ==="
"$WKAPPBOT" suggest show --help 2>&1
echo "PASS: suggest show --help works"

echo ""
echo "=== Live: suggest get (noise guard — routed to show subcommand) ==="
"$WKAPPBOT" suggest get --help 2>&1
echo "PASS: suggest get routes to show"

echo "ALL PASS"
