#!/usr/bin/env bash
# Basic smoke for an installed portable wkappbot CLI (Linux / macOS).
# Usage: portable-smoke.sh <path-to-wkappbot>   (run from the wkappbot-sdk checkout root)
set -euo pipefail
bin="${1:?usage: portable-smoke.sh <wkappbot>}"
work="${RUNNER_TEMP:-$(mktemp -d)}/wkappbot-smoke"
rm -rf "$work"; mkdir -p "$work"

export WKAPPBOT_HQ="$work/hq"
mkdir -p "$WKAPPBOT_HQ/skills/wkappbot-workflow"
cp skills/wkappbot-workflow/on-load*.skill.json "$WKAPPBOT_HQ/skills/wkappbot-workflow/"

fail=0
check() {  # check <name> <cmd...>: run and require rc=0; stdout kept in $work/<name>.out
  local name="$1"; shift
  local out="$work/$name.out" rc=0
  "$@" >"$out" 2>"$work/$name.err" || rc=$?
  if [ "$rc" = 0 ]; then
    echo "PASS $name: $(head -1 "$out")"
  else
    echo "::error::FAIL $name (rc=$rc): $(cat "$work/$name.err" "$out" | head -3 | tr '\n' ' ')"
    fail=1
  fi
}
nonempty() { [ -s "$work/$1.out" ] || { echo "::error::FAIL $1 printed nothing"; fail=1; }; }

check version    "$bin" --version
check help       "$bin" help
check skill-read "$bin" skill read on-load
check skill-list "$bin" skill list
for n in version help skill-read; do nonempty "$n"; done

# Hangul round-trip through file write / file read (byte-exact).
printf '한글 라운드트립 ✓\n' > "$work/src.txt"
check file-write "$bin" file write "$work/out.txt" --source-file "$work/src.txt"
if cmp -s "$work/src.txt" "$work/out.txt"; then echo "PASS file-write bytes"; else echo "::error::FAIL file-write bytes differ"; fail=1; fi
check file-read  "$bin" file read "$work/out.txt"
grep -q '한글' "$work/file-read.out" || { echo "::error::FAIL file-read lost Hangul"; fail=1; }

exit $fail
