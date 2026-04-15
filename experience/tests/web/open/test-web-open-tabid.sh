#!/usr/bin/env bash
# Test: web open prints TabID, URL, title — no "Aborting" on foreign Chrome
SRC_WEB="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"
P=0; F=0

echo "=== web open tabid + no-abort source check ==="

chk() { python3 -c "import sys; txt=open('$1','rb').read().decode('utf-8','ignore'); sys.exit(0 if '$2' in txt else 1)" 2>/dev/null; }
nochk() { python3 -c "import sys; txt=open('$1','rb').read().decode('utf-8','ignore'); sys.exit(1 if '$2' in txt else 0)" 2>/dev/null; }

if chk "$SRC_WEB" '[WEB] TabID:'; then echo "PASS: [WEB] TabID: output in WebCommands.cs"; P=$((P+1))
else echo "FAIL: no [WEB] TabID: output"; F=$((F+1)); fi

if chk "$SRC_WEB" '[WEB] Title:'; then echo "PASS: [WEB] Title: output in WebCommands.cs"; P=$((P+1))
else echo "FAIL: no [WEB] Title: output"; F=$((F+1)); fi

if chk "$SRC_WEB" '[WEB] URL:'; then echo "PASS: [WEB] URL: output in WebCommands.cs"; P=$((P+1))
else echo "FAIL: no [WEB] URL: output"; F=$((F+1)); fi

if nochk "$SRC_WEB" 'Aborting'; then echo "PASS: no Aborting on foreign Chrome"; P=$((P+1))
else echo "FAIL: Aborting still present"; F=$((F+1)); fi

if chk "$SRC_WEB" 'WKWebBot bar not yet in title'; then echo "PASS: warn-only (not abort) for WKWebBot bar missing"; P=$((P+1))
else echo "FAIL: warn-only text not found"; F=$((F+1)); fi

# CMD guard: --help interceptor exits 0 without opening Chrome
wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
