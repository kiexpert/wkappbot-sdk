#!/bin/bash
# Close all running Notepad instances gracefully.
# Uses a11y close with process=notepad scope — NOT title match (to avoid
# accidentally matching VSCode tabs or other apps showing "메모장" in title).
# If a "저장하시겠습니까?" dialog appears, auto-dismisses via "저장 안 함".

WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot.exe"
TIMESTAMP=$(date +"%H:%M:%S")

echo "[$TIMESTAMP] Closing all Notepad instances..."

# Find all notepad windows via process scope (NOT title — prevents false matches)
NOTEPAD_COUNT=$(tasklist //FI "IMAGENAME eq notepad.exe" 2>/dev/null | grep -c "notepad.exe")
echo "  Found $NOTEPAD_COUNT notepad.exe processes"

if [ "$NOTEPAD_COUNT" -eq 0 ]; then
  echo "  No notepad instances running"
  exit 0
fi

# Close via a11y close with process-scoped grap
# {proc:'notepad'} matches notepad.exe windows regardless of title
$WKAPPBOT a11y close "{proc:'notepad'}" --all 2>&1 | tail -5

sleep 1

# Dismiss any save prompts that appeared (Windows 11 Notepad shows ContentDialog)
# Try "저장 안 함" button first, fall back to Escape (same as Cancel)
PROMPT_COUNT=$(tasklist //FI "IMAGENAME eq notepad.exe" 2>/dev/null | grep -c "notepad.exe")
if [ "$PROMPT_COUNT" -gt 0 ]; then
  echo "  $PROMPT_COUNT notepad(s) still alive — dismissing save prompts..."
  for i in 1 2 3; do
    $WKAPPBOT a11y invoke "{proc:'notepad'}#*저장 안 함*" 2>&1 | tail -1
    sleep 0.5
    REMAIN=$(tasklist //FI "IMAGENAME eq notepad.exe" 2>/dev/null | grep -c "notepad.exe")
    [ "$REMAIN" -eq 0 ] && break
  done
fi

# Final status
FINAL=$(tasklist //FI "IMAGENAME eq notepad.exe" 2>/dev/null | grep -c "notepad.exe")
echo "[$TIMESTAMP] Done. Remaining notepad.exe: $FINAL"
