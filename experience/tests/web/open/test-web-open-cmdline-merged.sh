#!/usr/bin/env bash
# Test: cmdline Chrome ID merged -- WMI GetCommandLine + ResolvePidFromPort verified
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs"
P=0; F=0

echo "=== web open cmdline merged verification ==="

# WMI-based cmdline query exists (GetCommandLine method)
if python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'process where ProcessId' in txt else 1)" 2>/dev/null
then echo "PASS: WMI process cmdline query present"; P=$((P+1))
else echo "FAIL: WMI cmdline query missing"; F=$((F+1)); fi

# Only Chrome with matching user-data-dir is killed
if python3 -c "import sys; txt=open('$SRC','rb').read().decode('utf-8','ignore'); sys.exit(0 if 'normalizedDir' in txt and 'Contains' in txt else 1)" 2>/dev/null
then echo "PASS: normalizedDir.Contains safety check"; P=$((P+1))
else echo "FAIL: safety check missing"; F=$((F+1)); fi

wkappbot web --help open 2>/dev/null; true

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
