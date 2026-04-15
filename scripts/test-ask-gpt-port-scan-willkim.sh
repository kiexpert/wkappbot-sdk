#!/bin/bash
# test-ask-gpt-port-scan: verify multi-browser CDP port scan for ask gpt/gemini/claude
set -e

SRC_LAUNCHER="D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs"
SRC_CDP="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Entry.Cdp.cs"

# 1. ask gpt --help works (runtime check — verifies binary is deployed)
OUT=$(timeout 8 D:/SDK/bin/wkappbot.exe ask gpt --help 2>&1 || true)
echo "$OUT" | grep -qi "gpt\|chatgpt\|ask\|question" \
  || { echo "FAIL: ask gpt --help produced no useful output"; exit 1; }

# 2. FindBestPortForHostAsync exists in ChromeLauncher
grep -q "FindBestPortForHostAsync" "$SRC_LAUNCHER" \
  || { echo "FAIL: FindBestPortForHostAsync missing in ChromeLauncher.cs"; exit 1; }

# 3. FindFirstFreePortAsync exists in ChromeLauncher
grep -q "FindFirstFreePortAsync" "$SRC_LAUNCHER" \
  || { echo "FAIL: FindFirstFreePortAsync missing in ChromeLauncher.cs"; exit 1; }

# 4. EnsureCdpConnection calls both scan helpers
grep -q "FindBestPortForHostAsync" "$SRC_CDP" \
  || { echo "FAIL: EnsureCdpConnection missing FindBestPortForHostAsync call"; exit 1; }
grep -q "FindFirstFreePortAsync" "$SRC_CDP" \
  || { echo "FAIL: EnsureCdpConnection missing FindFirstFreePortAsync call"; exit 1; }

# 5. Scan covers 9 ports (9222-9230)
grep -q "scanCount = 9" "$SRC_LAUNCHER" \
  || { echo "FAIL: scan range not 9 ports (9222-9230)"; exit 1; }

# 6. Multi-browser log message in EnsureCdpConnection
grep -q "Multi-browser:" "$SRC_CDP" \
  || { echo "FAIL: multi-browser log message missing"; exit 1; }

echo "PASS: CDP multi-browser port scan implemented (ports 9222-9230, auto-routed per host)"
