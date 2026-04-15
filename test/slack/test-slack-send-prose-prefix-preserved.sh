#!/usr/bin/env bash
set -euo pipefail

ROOT="/w/GitHub/WKAppBot"
EXE="D:\\SDK\\bin\\wkappbot-core.exe"

cd "$ROOT"

plain="$(powershell.exe -NoProfile -Command "& '$EXE' slack send hello world test --dry-run | Out-String -Width 4096")"
echo "$plain"
[[ "$plain" == *'firstLine=hello world test'* ]]
[[ "$plain" == *'DRY-RUN preview: hello world test'* ]]
[[ "$plain" == *'message-lines=1'* ]]

mixed="$(powershell.exe -NoProfile -Command "& '$EXE' slack send notice \"first item\" \"second item\" --dry-run | Out-String -Width 4096")"
echo "$mixed"
[[ "$mixed" == *'firstLine=notice'* ]]
[[ "$mixed" == *'DRY-RUN preview: notice\nfirst item\nsecond item'* ]]
[[ "$mixed" != *'DRY-RUN preview: notice first item second item'* ]]
