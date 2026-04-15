#!/bin/bash
# Evidence: file open, code open, and vscode open share the same workspace-aware goto behavior.

if [ -z "${WKAPPBOT:-}" ]; then
  if [ -x /w/SDK/bin/wkappbot-core.exe ]; then
    WKAPPBOT=/w/SDK/bin/wkappbot-core.exe
  elif [ -x /mnt/w/SDK/bin/wkappbot-core.exe ]; then
    WKAPPBOT=/mnt/w/SDK/bin/wkappbot-core.exe
  else
    echo "FAIL: could not locate wkappbot-core.exe in /w or /mnt/w"
    exit 1
  fi
fi
TARGET="csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs:1"

run_and_check() {
  local label="$1"
  shift

  local out
  out=$("$WKAPPBOT" "$@" 2>&1)
  local status=$?

  if [ $status -ne 0 ]; then
    echo "FAIL: $label exited with $status"
    echo "$out"
    exit 1
  fi

  case "$out" in
    *"Unknown command"*)
      echo "FAIL: $label reported unknown command"
      echo "$out"
      exit 1
      ;;
  esac

  case "$out" in
    *"[FILE] open target -> hwnd="*"cwd=D:\\GitHub\\WKAppBot"*) ;;
    *)
      echo "FAIL: $label did not bind to the responsible WKAppBot workspace window"
      echo "$out"
      exit 1
      ;;
  esac

  case "$out" in
    *"[FILE] open OK ->"*"FileToolCommands.Open.cs:1"*) ;;
    *)
      echo "FAIL: $label did not report goto success"
      echo "$out"
      exit 1
      ;;
  esac

  case "$out" in
    *"falling back to code.exe"*)
      echo "FAIL: $label fell back instead of using the matched workspace window"
      echo "$out"
      exit 1
      ;;
  esac
}

run_and_check "file open" file open "$TARGET"
run_and_check "code open" code open "$TARGET"
run_and_check "vscode open" vscode open "$TARGET"

echo "PASS: file open uses workspace-aware goto"
echo "PASS: code open alias keeps the same routing"
echo "PASS: vscode open alias keeps the same routing"
echo "ALL PASS"
exit 0
