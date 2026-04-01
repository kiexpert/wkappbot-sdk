#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT_DIR"

PASS=0
FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

check_contains() {
  local file="$1"
  local pattern="$2"
  local label="$3"
  if grep -nF "$pattern" "$file" >/dev/null 2>&1; then
    pass "$label"
  else
    fail "$label"
  fi
}

check_contains "csharp/src/WKAppBot.CLI/Commands/AskCommands.Control.cs" "TryParseAskControlCommand" "control parser added"
check_contains "csharp/src/WKAppBot.CLI/Commands/AskCommands.Loop.cs" "\"qcancel\" => RunQuestionCancel" "run qcancel exposed"
check_contains "csharp/src/WKAppBot.CLI/AskSession.cs" "DropPendingForPageAsync" "pre-send cancel drops pending prompt"
check_contains "csharp/src/WKAppBot.WebBot/CdpClient.AiHelpers.cs" "CancelPromptPumpAsync" "page-local prompt cancel helper added"

WIN_ROOT="$ROOT_DIR"
if command -v wslpath >/dev/null 2>&1; then
  WIN_ROOT="$(wslpath -w "$ROOT_DIR")"
fi

if powershell.exe -NoLogo -NoProfile -Command "dotnet build '$WIN_ROOT\\csharp\\src\\WKAppBot.CLI\\WKAppBot.CLI.csproj' -nologo" >/tmp/test-ask-question-control.log 2>&1; then
  pass "CLI build passes"
else
  cat /tmp/test-ask-question-control.log
  fail "CLI build passes"
fi

echo
echo "$PASS PASS, $FAIL FAIL"
if [ "$FAIL" -ne 0 ]; then
  exit 1
fi
