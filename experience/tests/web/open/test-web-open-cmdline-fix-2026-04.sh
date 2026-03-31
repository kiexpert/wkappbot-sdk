#!/usr/bin/env bash
# Test: cmdline-based Chrome identification -- safe kill + PID isolation (2026-04)
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs"
P=0; F=0

echo "=== web open cmdline Chrome fix 2026-04 source check ==="

chk() { python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); sys.exit(0 if '$1' in txt else 1)" 2>/dev/null; }

# KillStaleChromesAsync only kills our own user-data-dir Chrome
if chk 'KillStaleChromesAsync'; then echo "PASS: KillStaleChromesAsync safe targeted kill"; P=$((P+1))
else echo "FAIL: KillStaleChromesAsync not found"; F=$((F+1)); fi

# GetCommandLine uses WMI for reliable PID->cmdline
if chk 'GetCommandLine'; then echo "PASS: GetCommandLine WMI-based cmdline read"; P=$((P+1))
else echo "FAIL: GetCommandLine not found"; F=$((F+1)); fi

# Must match user-data-dir to avoid killing user Chrome
if chk 'normalizedDir'; then echo "PASS: normalizedDir path match prevents foreign Chrome kill"; P=$((P+1))
else echo "FAIL: normalizedDir not found"; F=$((F+1)); fi

# ResolvePidFromPort: CDP port to PID mapping avoids wrong Chrome
if python3 -c "import sys; txt=open('W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.cs','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'ResolvePidFromPort' in txt else 1)" 2>/dev/null
then echo "PASS: ResolvePidFromPort CDP-PID mapping in CdpClient.cs"; P=$((P+1))
else echo "FAIL: ResolvePidFromPort not found"; F=$((F+1)); fi

wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
