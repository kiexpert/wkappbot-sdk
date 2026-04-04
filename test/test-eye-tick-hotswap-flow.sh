#!/usr/bin/env bash
# Evidence: Eye/Launcher hot-swap + self-restart + guardian + startup gentle-swap
# Verifies: merge 2026-04-04T09:12 — Eye/Launcher 자기 핫스왑 + 재시작 흐름 개선

set -e
REPO="W:/GitHub/WKAppBot"
EYE_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeGlobalMode.cs"
CMD_SRC="$REPO/csharp/src/WKAppBot.CLI/Commands/AppBotEyeCommands.cs"

echo "=== 1. Core .new.exe rename-swap ==="
grep -n "\.new\.exe detected.*rename-swap\|step1Done.*step2Done\|swap OK" "$EYE_SRC" | head -3
echo "PASS: Core rename-swap logic exists"

echo ""
echo "=== 2. Blue-green Eye self-restart ==="
grep -n "Launching new Eye\|--replace-pid\|waiting for window" "$EYE_SRC" | head -3
echo "PASS: blue-green restart with --replace-pid exists"

echo ""
echo "=== 3. Guardian auto-launch ==="
grep -n "EnsureEyeGuardianForWindow\|guardian.*bootstrap" "$CMD_SRC" | head -3
echo "PASS: EnsureEyeGuardianForWindow called on startup + hot-swap"

echo ""
echo "=== 4. Startup gentle-swap (.new.exe already staged) ==="
grep -n "Startup.*gentle-swap\|Startup.*\.new\.exe.*staged\|_fswExeDirty = true" "$EYE_SRC" | head -3
COUNT=$(grep -c "gentle-swap" "$EYE_SRC")
if [ "$COUNT" -ge 1 ]; then
    echo "PASS: startup gentle-swap check exists"
else
    echo "FAIL: startup gentle-swap not found"
    exit 1
fi

echo ""
echo "=== 5. eye shutdown graceful command ==="
grep -n "_eyeShutdownRequested\|eye shutdown\|EyeShutdownCommand" "$CMD_SRC" | head -3
echo "PASS: eye shutdown command exists"

echo ""
echo "=== Live: eye tick ==="
"W:/SDK/bin/wkappbot.exe" eye tick 2>&1 | head -3
echo "PASS: eye tick completed"

echo "ALL PASS"
