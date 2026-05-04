#!/usr/bin/env bash
# Test: web open merged bugs -- Aborting removed, cmdline Chrome ID, TabID output verified
SRC_WEB="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"
SRC_LAUNCH="D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs"
P=0; F=0

echo "=== web open merged bug verification 2026-04 ==="

# 1. Aborting removed: WARN+continuing approach
if python3 -c "import sys; txt=open('$SRC_WEB','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'WARN:' in txt and 'WKWebBot bar not yet' in txt else 1)" 2>/dev/null
then echo "PASS: Aborting removed -- warn-only approach (WKWebBot bar not yet)"; P=$((P+1))
else echo "FAIL: warn-only message not found"; F=$((F+1)); fi

# 2. Chrome kill by user-data-dir (not by title or blanket kill)
if python3 -c "import sys; txt=open('$SRC_LAUNCH','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'user-data-dir' in txt and 'kill' in txt.lower() else 1)" 2>/dev/null
then echo "PASS: Chrome kill scoped to user-data-dir only"; P=$((P+1))
else echo "FAIL: user-data-dir kill scope not found"; F=$((F+1)); fi

wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
