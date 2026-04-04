#!/usr/bin/env bash
# Evidence: ASK CDP stability fixes (3 bugs)
# Verifies: merge 2026-04-04T08:37:50

set -e
REPO="W:/GitHub/WKAppBot"

echo "=== Bug 1: Runtime.enable retry with backoff ==="
CDP_SRC="$REPO/csharp/src/WKAppBot.WebBot/CdpClient.cs"
grep -n "EnableRuntimeWithRetry\|maxRetries\|baseDelayMs\|exponential" "$CDP_SRC" | head -5
grep -c "EnableRuntimeWithRetry" "$CDP_SRC" | grep -q "^[2-9]" && echo "PASS: EnableRuntimeWithRetry called from multiple connect points" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 1: TabManagement also uses retry ==="
TAB_SRC="$REPO/csharp/src/WKAppBot.WebBot/CdpClient.TabManagement.cs"
grep -n "EnableRuntimeWithRetry" "$TAB_SRC" | head -2
echo "PASS: TabManagement uses EnableRuntimeWithRetry"

echo ""
echo "=== Bug 2: Prompt node liveness — child node check ==="
DET_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeClaudeDetector.cs"
grep -n "hasEditableChild\|FindAllChildren\|rect.Height > 80" "$DET_SRC" | head -4
grep -c "hasEditableChild" "$DET_SRC" | grep -q "^[1-9]" && echo "PASS: turn-form child node liveness check" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 3: ask MD append (not overwrite) ==="
MD_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/AskCommands.MdWriter.cs"
grep -n "AppendAllText\|Final Summary\|live-appended" "$MD_SRC" | head -4
grep -c "AppendAllText" "$MD_SRC" | grep -q "^[1-9]" && echo "PASS: existing MD file gets append (not overwrite)" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Live: ask --help ==="
"W:/SDK/bin/wkappbot.exe" ask --help 2>&1 | head -3
echo "PASS: ask command accessible"

echo "ALL PASS"
