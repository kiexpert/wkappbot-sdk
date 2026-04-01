#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$ROOT"
case "$ROOT" in
  /mnt/*)
    DRIVE_LETTER="$(printf '%s' "$ROOT" | cut -d/ -f3 | tr '[:lower:]' '[:upper:]')"
    REST="$(printf '%s' "$ROOT" | cut -d/ -f4- | sed 's#/#\\\\#g')"
    ROOT_WIN="${DRIVE_LETTER}:\\${REST}"
    ;;
  *)
    ROOT_WIN="$ROOT"
    ;;
esac

PASS=0
FAIL=0
PS="powershell.exe -NoProfile -Command"

echo "=== test-ask-question-state-lifecycle Real Execution Test ==="

echo -n "Test 1: AskSession exposes question lifecycle helpers... "
if $PS "Set-Location '$ROOT_WIN'; if (Select-String -Path 'csharp/src/WKAppBot.CLI/AskSession.cs' -Pattern 'MarkQueued|MarkRunning|MarkDone|MarkTimedOut|MarkRetrying|MarkCancelled|MarkFailed|CancelQuestionAsync' -Quiet) { exit 0 } else { exit 1 }"; then
  echo "PASS"
  PASS=$((PASS+1))
else
  echo "FAIL"
  FAIL=$((FAIL+1))
fi

echo -n "Test 2: provider send-wait paths mark lifecycle transitions... "
if $PS "Set-Location '$ROOT_WIN'; if (Select-String -Path 'csharp/src/WKAppBot.CLI/Commands/AskCommands.ChatGpt.SendWait.cs','csharp/src/WKAppBot.CLI/Commands/AskCommands.Claude.cs','csharp/src/WKAppBot.CLI/Commands/AskCommands.Gemini.cs' -Pattern 'MarkRunning|MarkDone|MarkTimedOut' -Quiet) { exit 0 } else { exit 1 }"; then
  echo "PASS"
  PASS=$((PASS+1))
else
  echo "FAIL"
  FAIL=$((FAIL+1))
fi

echo -n "Test 3: build CLI after question lifecycle wiring... "
if $PS "Set-Location '$ROOT_WIN'; dotnet build csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj -nologo | Out-File -FilePath '.wkappbot/test-ask-question-state-lifecycle.build.log' -Encoding utf8; exit \$LASTEXITCODE"; then
  echo "PASS"
  PASS=$((PASS+1))
else
  echo "FAIL"
  $PS "Set-Location '$ROOT_WIN'; Get-Content '.wkappbot/test-ask-question-state-lifecycle.build.log' -Tail 80"
  FAIL=$((FAIL+1))
fi

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
