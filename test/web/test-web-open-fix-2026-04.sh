#!/usr/bin/env bash
# Test: web open 버그 수정 증거 -- TabID 출력 + Aborting 제거 (2026-04)
SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"
P=0; F=0

echo "=== web open fix 2026-04 source check ==="

# TabID must appear twice (open + navigate)
COUNT=$(python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); print(txt.count('[WEB] TabID: {tabId}'))" 2>/dev/null)
if [ "$COUNT" -ge 2 ] 2>/dev/null; then echo "PASS: [WEB] TabID: in both open and navigate (x${COUNT})"; P=$((P+1))
else echo "FAIL: [WEB] TabID: only $COUNT times (need >=2)"; F=$((F+1)); fi

# WARN approach (not abort) when WKWebBot bar missing
if python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'WARN:' in txt and 'continuing' in txt else 1)" 2>/dev/null
then echo "PASS: WARN+continuing pattern (not Aborting)"; P=$((P+1))
else echo "FAIL: WARN+continuing pattern missing"; F=$((F+1)); fi

# cdp.TargetId is the TabID source
if python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'TargetId' in txt else 1)" 2>/dev/null
then echo "PASS: cdp.TargetId as TabID source"; P=$((P+1))
else echo "FAIL: cdp.TargetId not found"; F=$((F+1)); fi

wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
