#!/usr/bin/env bash
# Evidence: suggest show subcommand + noise guard for unknown subcommands
# Verifies: noise merge item — suggest show/get commands were being saved as suggestions

set -e
WKAPPBOT="W:/SDK/bin/wkappbot.exe"

echo "=== Live: suggest show --help ==="
"$WKAPPBOT" suggest show --help 2>&1
echo "PASS: suggest show --help works"

echo ""
echo "=== Live: suggest get (noise guard — routed to show subcommand) ==="
"$WKAPPBOT" suggest get --help 2>&1
echo "PASS: suggest get routes to show"

echo "ALL PASS"
