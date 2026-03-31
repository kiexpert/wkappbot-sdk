#!/usr/bin/env bash
# Test: web open and navigate both output TabID -- CDP-first Chrome validation (no Aborting)
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"
P=0; F=0

echo "=== web open target output source check ==="

chk() { python3 -c "import sys; txt=open('$1','rb').read().decode('utf-8','ignore'); sys.exit(0 if '$2' in txt else 1)" 2>/dev/null; }
nochk() { python3 -c "import sys; txt=open('$1','rb').read().decode('utf-8','ignore'); sys.exit(1 if '$2' in txt else 0)" 2>/dev/null; }

COUNT=$(python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); print(txt.count('[WEB] TabID:'))" 2>/dev/null)
if [ "$COUNT" -ge 2 ] 2>/dev/null; then echo "PASS: [WEB] TabID: in both open and navigate ($COUNT occurrences)"; P=$((P+1))
else echo "FAIL: [WEB] TabID: missing (found: $COUNT)"; F=$((F+1)); fi

if chk "$SRC" 'TargetId ?? ""'; then echo "PASS: cdp.TargetId used as TabID source"; P=$((P+1))
else echo "FAIL: cdp.TargetId not used"; F=$((F+1)); fi

if chk "$SRC" 'ConnectCdp(port,'; then echo "PASS: ConnectCdp(port) called"; P=$((P+1))
else echo "FAIL: ConnectCdp(port) not found"; F=$((F+1)); fi

if nochk "$SRC" 'Aborting'; then echo "PASS: no Aborting on non-WebBot Chrome"; P=$((P+1))
else echo "FAIL: Aborting still in code"; F=$((F+1)); fi

wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
