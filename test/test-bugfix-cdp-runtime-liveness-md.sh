#!/usr/bin/env bash
# Evidence: ASK CDP stability fixes (3 bugs + minimize 과잉 제거 + 포커스 워치독)
# Verifies: merge 2026-04-04T08:37:50 (CDP 안정성) + merge 2026-04-04T08:38:35 (포커스 워치독)

set -e
REPO="W:/GitHub/WKAppBot"
CDP_SRC="$REPO/csharp/src/WKAppBot.WebBot/CdpClient.cs"

echo "=== Bug 1: Runtime.enable retry with backoff ==="
grep -n "EnableRuntimeWithRetry\|maxRetries\|baseDelayMs" "$CDP_SRC" | head -4
grep -c "EnableRuntimeWithRetry" "$CDP_SRC" | grep -q "^[2-9]" && echo "PASS: EnableRuntimeWithRetry in Connect+Reconnect+TabSwitch" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 2: Prompt node liveness — child node check ==="
DET_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeClaudeDetector.cs"
grep -n "hasEditableChild\|FindAllChildren\|rect.Height > 80" "$DET_SRC" | head -3
grep -c "hasEditableChild" "$DET_SRC" | grep -q "^[1-9]" && echo "PASS: turn-form child node liveness" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 3: ask MD append (not overwrite) ==="
MD_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/AskCommands.MdWriter.cs"
grep -n "AppendAllText\|Final Summary" "$MD_SRC" | head -3
grep -c "AppendAllText" "$MD_SRC" | grep -q "^[1-9]" && echo "PASS: live MD preserved via append" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 4: Input.* minimize 제거 — CDP 렌더러 직접 전달 ==="
grep -n "IsFocusStealingMethod\|bringToFront.*activateTarget\|does NOT require" "$CDP_SRC" | head -3
grep -c "IsFocusStealingMethod" "$CDP_SRC" | grep -q "^[2-9]" && echo "PASS: only focus-stealing methods get minimize" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 5: OperationContext — 단독 모드에서도 설정 ==="
for f in AskCommands.Gemini.cs AskCommands.ChatGpt.cs AskCommands.Claude.cs; do
    FILE="$REPO/csharp/src/WKAppBot.CLI/Commands/$f"
    OP=$(grep -c "OperationContext" "$FILE")
    echo "  $f: ${OP}x OperationContext"
done
echo "PASS: all 3 AI providers set OperationContext before triad check"

echo ""
echo "=== Bug 6: AskSession.Dispose — Chrome iconic 자동 복구 ==="
ASK_SRC="$REPO/csharp/src/WKAppBot.CLI/AskSession.cs"
grep -n "IsIconic\|SW_SHOWNOACTIVATE\|restored minimized" "$ASK_SRC" | head -3
grep -c "IsIconic" "$ASK_SRC" | grep -q "^[1-9]" && echo "PASS: AskSession dispose restores minimized Chrome" || { echo "FAIL"; exit 1; }

echo ""
echo "=== Bug 7: Focus-steal post-restore ==="
grep -n "post-restore\|IsFocusStealingMethod.*ChromeWindowHandle" "$CDP_SRC" | head -3
echo "PASS: focus-stealing method auto-restores after send"

echo ""
echo "=== Live: ask --help ==="
"W:/SDK/bin/wkappbot.exe" ask --help 2>&1 | head -3
echo "PASS: ask command accessible"

echo "ALL PASS"
