#!/bin/bash
# Evidence: file open routes an HTS project source file to the responsible company VS Code workspace window.

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

TARGET="D:\\HTS_Project\\Source\\RunControls\\ScGridCheCtl\\ScCtlGridChe.cpp:5352"
EXPECTED_CWD="D:\\HTS_Project\\Source\\RunControls\\ScGridCheCtl"

OUT=$("$WKAPPBOT" file open "$TARGET" 2>&1)
STATUS=$?

if [ $STATUS -ne 0 ]; then
  echo "FAIL: file open exited with $STATUS"
  echo "$OUT"
  exit 1
fi

case "$OUT" in
  *"[FILE] open target -> hwnd="*"cwd=$EXPECTED_CWD"*) ;;
  *)
    echo "FAIL: missing responsible HTS workspace target"
    echo "$OUT"
    exit 1
    ;;
esac

case "$OUT" in
  *"[FILE] open OK ->"*"ScCtlGridChe.cpp:5352"*) ;;
  *)
    echo "FAIL: missing goto success for HTS file"
    echo "$OUT"
    exit 1
    ;;
esac

case "$OUT" in
  *"falling back to code.exe"*)
    echo "FAIL: command fell back instead of using the matched HTS workspace window"
    echo "$OUT"
    exit 1
    ;;
esac

echo "PASS: file open matched the responsible HTS workspace window"
echo "PASS: file open launched goto for the requested HTS source line"
echo "ALL PASS"
exit 0
