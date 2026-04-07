#!/usr/bin/env bash
set -euo pipefail

ROOT="/w/GitHub/WKAppBot"
EXE="W:\\SDK\\bin\\wkappbot-core.exe"

cd "$ROOT"

run() {
  powershell.exe -NoProfile -Command "& '$EXE' slack send $1 --dry-run | Out-String -Width 4096"
}

out1="$(run 'hello world test')"
echo "$out1"
[[ "$out1" == *'DRY-RUN preview: hello world test'* ]]
[[ "$out1" == *'message-lines=1'* ]]

out2="$(run '"first line" "second line"')"
echo "$out2"
[[ "$out2" == *'DRY-RUN preview: first line\nsecond line'* ]]
[[ "$out2" == *'message-lines=2'* ]]

out3="$(run 'notice "first item" "second item"')"
echo "$out3"
[[ "$out3" == *'DRY-RUN preview: notice\nfirst item\nsecond item'* ]]
[[ "$out3" == *'message-lines=3'* ]]
