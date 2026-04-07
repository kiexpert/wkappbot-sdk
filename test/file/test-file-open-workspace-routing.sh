#!/bin/bash
# Evidence: file open routes to the best matching VS Code workspace window.

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
if [ -f /w/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs ]; then
  SRC="/w/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs"
elif [ -f /mnt/w/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs ]; then
  SRC="/mnt/w/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs"
else
  echo "FAIL: could not locate FileToolCommands.Open.cs in /w or /mnt/w"
  exit 1
fi

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
    echo "FAIL: missing workspace-routed VS Code target"
    echo "$OUT"
    exit 1
    ;;
esac

case "$OUT" in
  *"falling back to code.exe"*)
    echo "FAIL: command fell back instead of using workspace window"
    echo "$OUT"
    exit 1
    ;;
esac

/usr/bin/grep -F "OrderByDescending(c => c.WorkspaceCwd!.Length)" "$SRC" >/dev/null || {
  echo "FAIL: longest workspace prefix selection missing from source"
  exit 1
}

/usr/bin/grep -F "NativeMethods.SmartSetForegroundWindow(target.Hwnd)" "$SRC" >/dev/null || {
  echo "FAIL: focus handoff to matched VS Code window missing from source"
  exit 1
}

echo "PASS: file open routed to responsible VS Code workspace window"
echo "PASS: source keeps longest-prefix workspace selection and focus handoff"
echo "ALL PASS"
exit 0
