#!/bin/bash
# Evidence: file open <path>:<line> command exists and launches VS Code goto flow.

WKAPPBOT=${WKAPPBOT:-/w/SDK/bin/wkappbot-core.exe}
TARGET="csharp/src/WKAppBot.CLI/Commands/FileToolCommands.Open.cs:1"

OUT=$("$WKAPPBOT" file open "$TARGET" 2>&1)
STATUS=$?

if [ $STATUS -ne 0 ]; then
  echo "FAIL: file open exited with $STATUS"
  echo "$OUT"
  exit 1
fi

case "$OUT" in
  *"[FILE] open OK ->"*) ;;
  *)
    echo "FAIL: missing open success marker"
    echo "$OUT"
    exit 1
    ;;
esac

case "$OUT" in
  *"FileToolCommands.Open.cs:1"*) ;;
  *)
    echo "FAIL: missing goto target in output"
    echo "$OUT"
    exit 1
    ;;
esac

HELP_OUT=$("$WKAPPBOT" file --help 2>&1)
case "$HELP_OUT" in
  *"open <path>[:line[:col]]"*) ;;
  *)
    echo "FAIL: file help missing open subcommand"
    echo "$HELP_OUT"
    exit 1
    ;;
esac

echo "PASS: file open command launches goto flow"
echo "PASS: file help exposes open subcommand"
echo "ALL PASS"
exit 0
