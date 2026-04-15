#!/usr/bin/env bash
set -euo pipefail

ROOT="/w/GitHub/WKAppBot"
EXE="D:\\SDK\\bin\\wkappbot-core.exe"

cd "$ROOT"

run() {
  powershell.exe -NoProfile -Command "& '$EXE' slack send $1 --dry-run | Out-String -Width 4096"
}

out1="$(run '"first line" "second line"')"
echo "$out1"
[[ "$out1" == *'firstLine=first line'* ]]
[[ "$out1" == *'DRY-RUN preview: first line\nsecond line'* ]]

out2="$(run 'notice "first item" "second item"')"
echo "$out2"
[[ "$out2" == *'firstLine=notice'* ]]
[[ "$out2" == *'DRY-RUN preview: notice\nfirst item\nsecond item'* ]]

out3="$(run 'dry run should not send')"
echo "$out3"
[[ "$out3" == *'firstLine=dry run should not send'* ]]
[[ "$out3" == *'DRY-RUN preview: dry run should not send'* ]]
