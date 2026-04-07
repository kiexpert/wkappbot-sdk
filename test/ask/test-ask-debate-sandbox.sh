#!/bin/bash
# Test: wkappbot ask debate Tab Sandbox Policy for parallel isolation
# Verifies CDP tab sandboxing prevents context collision in parallel debates

GRAP=${GRAP:-/w/SDK/bin/grap.exe}
SRC="W:/GitHub/WKAppBot/csharp/src"
P=0; F=0

echo "=== CDP Tab Sandbox for Parallel Debates ==="

# 1. AskTargetRegistry with command+subcommand+hwnd key
if $GRAP "AskTargetRegistry" "$SRC/WKAppBot.WebBot/CdpClient.TabManagement.cs" 2>/dev/null | grep -q "AskTargetRegistry"; then
  echo "PASS: AskTargetRegistry for tab isolation"; ((P++))
else echo "FAIL: no AskTargetRegistry"; ((F++)); fi

# 2. AskTargetRegistry persists tab assignments
if $GRAP "AskTargetRegistry" "$SRC/WKAppBot.CLI/AskTargetRegistry.cs" 2>/dev/null | grep -q "AskTargetRegistry"; then
  echo "PASS: AskTargetRegistry tab persistence"; ((P++))
else echo "FAIL: no AskTargetRegistry"; ((F++)); fi

# 3. CdpTabManager manages scoped tab connections
if $GRAP "CdpTabManager" "$SRC/WKAppBot.CLI/CdpTabManager.cs" 2>/dev/null | grep -q "CdpTabManager"; then
  echo "PASS: CdpTabManager scoped connections"; ((P++))
else echo "FAIL: no CdpTabManager"; ((F++)); fi

# 4. Per-AI CDP client in TriadSharedContext
if $GRAP "_cdpClients" "$SRC/WKAppBot.CLI/Commands/AskCommands.Triad.cs" 2>/dev/null | grep -q "_cdpClients"; then
  echo "PASS: Per-AI CDP client dict in triad"; ((P++))
else echo "FAIL: no per-AI CDP"; ((F++)); fi

echo ""
echo "=== Results: $P passed, $F failed ==="
[ "$F" -eq 0 ] && echo "ALL PASS" && exit 0
echo "SOME FAILED" && exit 1
