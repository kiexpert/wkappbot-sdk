#!/usr/bin/env bash
set -euo pipefail

ROOT="/w/GitHub/WKAppBot"
EXE="W:\\SDK\\bin\\wkappbot-core.exe"

cd "$ROOT"

out="$(powershell.exe -NoProfile -Command "& '$EXE' suggest list | Out-String -Width 4096")"

echo "$out"

[[ "$out" == *"Pending:"* ]]
[[ "$out" == *"No new matches found in Slack."* ]]
[[ "$out" == *'resolve: wkappbot suggest resolve <ts> "note"'* ]]
[[ "$out" =~ ts=20[0-9]{2}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2} ]]
