#!/bin/bash
# test-skill-help: verify skill --help is registered in CommandHelpMap
set -e
OUT=$(wkappbot skill --help 2>/dev/null)
echo "$OUT" | grep -q "skill <subcommand>" || { echo "FAIL: skill --help not found"; exit 1; }
echo "$OUT" | grep -q "audit" || { echo "FAIL: audit subcommand missing"; exit 1; }
echo "$OUT" | grep -q "verify" || { echo "FAIL: verify subcommand missing"; exit 1; }
echo "PASS: skill --help registered OK"
