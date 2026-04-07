#!/bin/bash
# Evidence: file open chooses the responsible VS Code workspace window when one is already open.

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

OUT=$("$WKAPPBOT" file open "$TARGET" 2>&1)
STATUS=$?

if [ $STATUS -ne 0 ]; then
  echo "FAIL: file open exited with $STATUS"
  echo "$OUT"
  exit 1
fi

case "$OUT" in
  *"[FILE] open target -> hwnd="*"cwd=W:\\GitHub\\WKAppBot"*) ;;
  *)
    echo "FAIL: missing workspace-matched VS Code target"
    echo "$OUT"
    exit 1
    ;;
esac

case "$OUT" in
  *"[FILE] open OK ->"*"FileToolCommands.Open.cs:1"*) ;;
  *)
    echo "FAIL: missing goto success output"
    echo "$OUT"
    exit 1
    ;;
esac

echo "PASS: file open matched responsible VS Code workspace window"
echo "PASS: file open launched goto target"
echo "ALL PASS"
exit 0
