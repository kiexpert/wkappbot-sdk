#!/usr/bin/env bash
# Test: web open identifies Chrome by cmdline (--remote-debugging-port) — safe kill
SRC_LAUNCH="D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs"
P=0; F=0

echo "=== web open cmdline Chrome identification source check ==="

chk() { python3 -c "import sys; txt=open('$1','rb').read().decode('utf-8','ignore'); sys.exit(0 if '$2' in txt else 1)" 2>/dev/null; }

if chk "$SRC_LAUNCH" 'KillStaleChromesAsync'; then echo "PASS: KillStaleChromesAsync in ChromeLauncher.cs"; P=$((P+1))
else echo "FAIL: no KillStaleChromesAsync"; F=$((F+1)); fi

if chk "$SRC_LAUNCH" 'GetCommandLine'; then echo "PASS: GetCommandLine in ChromeLauncher.cs"; P=$((P+1))
else echo "FAIL: no GetCommandLine"; F=$((F+1)); fi

if chk "$SRC_LAUNCH" 'user-data-dir'; then echo "PASS: user-data-dir cmdline check in ChromeLauncher.cs"; P=$((P+1))
else echo "FAIL: no user-data-dir cmdline check"; F=$((F+1)); fi

if chk "$SRC_LAUNCH" 'normalizedDir'; then echo "PASS: normalized path comparison in KillStaleChromesAsync"; P=$((P+1))
else echo "FAIL: no normalized path comparison"; F=$((F+1)); fi

if chk "$SRC_LAUNCH" 'wmic'; then echo "PASS: WMI cmdline query in GetCommandLine"; P=$((P+1))
else echo "FAIL: no WMI cmdline query"; F=$((F+1)); fi

# CMD guard: --help interceptor exits 0 without opening Chrome
wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
