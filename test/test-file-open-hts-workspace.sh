#!/bin/bash
# Evidence: file open sends a real HTS source file to the matched HTS VS Code workspace window.

set -euo pipefail

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

TARGET='W:\HTS_Project\Source\RunControls\ScGridCheCtl\ScCtlGridChe.cpp:5352'
TMP="${TMPDIR:-/tmp}/wkappbot-file-open-hts-$$.log"
: >"$TMP"

# Let InputReadiness age out recent user input so this hidden test can focus safely.
powershell.exe -Command "Start-Sleep -Seconds 31" >/dev/null 2>&1

"$WKAPPBOT" file open "$TARGET" >"$TMP" 2>&1

grep -F 'cwd=W:\HTS_Project\Source\RunControls\ScGridCheCtl' "$TMP" >/dev/null || {
  echo "FAIL: HTS workspace cwd was not selected"
  cat "$TMP"
  exit 1
}

grep -F 'ScCtlGridChe.cpp:5352' "$TMP" >/dev/null || {
  echo "FAIL: requested HTS goto target missing"
  cat "$TMP"
  exit 1
}

if grep -F 'falling back to code.exe' "$TMP" >/dev/null; then
  echo "FAIL: command fell back instead of using the HTS workspace window"
  cat "$TMP"
  exit 1
fi

echo "PASS: HTS workspace window selected"
echo "PASS: requested HTS file/line routed through file open"
echo "ALL PASS"
