#!/usr/bin/env bash
# Smoke for an installed portable wkappbot CLI (Linux / macOS), in two tiers.
# Usage: portable-smoke.sh <path-to-wkappbot> [expected-rid]   (run from the wkappbot-sdk checkout root)
#   SMOKE_TIER=required (default): --version (+ runtime label) and skill read on-load; any FAIL exits 1.
#   SMOKE_TIER=advisory: help, skill list/search, suggest list, file write/read; failures are
#     ::warning:: annotations and the script exits 0 (they never block publish or release).
set -euo pipefail
bin="${1:?usage: portable-smoke.sh <wkappbot> [expected-rid]}"
want_rid="${2:-}"
tier="${SMOKE_TIER:-required}"
work="${RUNNER_TEMP:-$(mktemp -d)}/wkappbot-smoke-$tier"
rm -rf "$work"; mkdir -p "$work"

export WKAPPBOT_HQ="$work/hq"
mkdir -p "$WKAPPBOT_HQ/skills/wkappbot-workflow"
cp skills/wkappbot-workflow/on-load*.skill.json "$WKAPPBOT_HQ/skills/wkappbot-workflow/"

fail=0
level=error; [ "$tier" = advisory ] && level=warning
bad() { echo "::$level::FAIL $*"; fail=1; }
check() {  # check <name> <cmd...>: run and require rc=0; stdout kept in $work/<name>.out
  local name="$1"; shift
  local out="$work/$name.out" rc=0
  "$@" >"$out" 2>"$work/$name.err" || rc=$?
  if [ "$rc" = 0 ]; then
    echo "PASS $name: $(head -1 "$out")"
  else
    bad "$name (rc=$rc): $(cat "$work/$name.err" "$out" | head -3 | tr '\n' ' ')"
  fi
}
nonempty() { [ -s "$work/$1.out" ] || bad "$1 printed nothing"; }

if [ "$tier" = required ]; then
  check version    "$bin" --version
  check skill-read "$bin" skill read on-load
  for n in version skill-read; do nonempty "$n"; done
  # The version label names the binary's own runtime (an osx-x64 build under Rosetta must say osx-x64).
  if [ -n "$want_rid" ]; then
    if grep -q "($want_rid)" "$work/version.out"; then echo "PASS version label: $want_rid"
    else bad "version label: want ($want_rid), got: $(head -1 "$work/version.out")"; fi
  fi
  exit $fail
fi

check help         "$bin" help
check skill-list   "$bin" skill list
check skill-search "$bin" skill search on-load
check suggest-list "$bin" suggest list
for n in help skill-search; do nonempty "$n"; done

# Hangul round-trip through file write / file read (byte-exact).
printf '한글 라운드트립 ✓\n' > "$work/src.txt"
check file-write "$bin" file write "$work/out.txt" --source-file "$work/src.txt"
if cmp -s "$work/src.txt" "$work/out.txt"; then echo "PASS file-write bytes"; else bad "file-write bytes differ"; fi
check file-read  "$bin" file read "$work/out.txt"
grep -q '한글' "$work/file-read.out" || bad "file-read lost Hangul"

# file write creates missing parent folders.
check file-write-newdir "$bin" file write "$work/new/deeper/out.txt" --source-file "$work/src.txt"
cmp -s "$work/src.txt" "$work/new/deeper/out.txt" || bad "file-write-newdir bytes"

echo "advisory smoke: $([ $fail = 0 ] && echo clean || echo 'warnings above (non-blocking)')"
exit 0
