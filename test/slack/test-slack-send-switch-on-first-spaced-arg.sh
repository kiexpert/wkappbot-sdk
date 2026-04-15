#!/usr/bin/env bash
set -euo pipefail

ROOT="/w/GitHub/WKAppBot"
EXE="D:\\SDK\\bin\\wkappbot-core.exe"
cd "$ROOT"

out="$(powershell.exe -NoProfile -Command "& '$EXE' slack send notice \"first item\" \"second item\" --dry-run | Out-String -Width 4096")"
echo "$out"

[[ "$out" == *'firstLine=notice'* ]]
[[ "$out" == *'DRY-RUN preview: notice\nfirst item\nsecond item'* ]]
[[ "$out" == *'message-lines=3'* ]]

quoted="$(powershell.exe -NoProfile -Command "& '$EXE' slack send \"first line\" \"second line\" --dry-run | Out-String -Width 4096")"
echo "$quoted"

[[ "$quoted" == *'firstLine=first line'* ]]
[[ "$quoted" == *'DRY-RUN preview: first line\nsecond line'* ]]
[[ "$quoted" == *'message-lines=2'* ]]
