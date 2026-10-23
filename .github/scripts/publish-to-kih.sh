#!/usr/bin/env bash
# Publish the verified platform binaries into kih bin/ with kih's own tools/wk-bin-acquire.sh,
# then push all resulting commits once. Skips every binary whose sha256 already matches kih
# (MANIFEST row; for the Windows core, the decompressed bin/win-x64/wkappbot-core.exe.xz), so an
# unchanged build makes no commit.
# Usage: publish-to-kih.sh DIST   (DIST holds the raw wkappbot-<rid> files, wkappbot.exe, wkappbot-core.exe)
set -euo pipefail
dist="$(cd "${1:?usage: publish-to-kih.sh DIST}" && pwd)"
cd kih
acq=tools/wk-bin-acquire.sh
[ -f "$acq" ] || { echo "::error::kih $acq missing"; exit 1; }
git config user.name  "wkappbot-sdk platform-release"
git config user.email "41898282+github-actions[bot]@users.noreply.github.com"

sha() { sha256sum "$1" | cut -d' ' -f1; }
manifest_sha() {  # manifest_sha REL -> sha256 column of the bin/MANIFEST.md row for REL
  awk -F'|' -v p="$1" '/^\|/ { x=$2; gsub(/^[ \t]+|[ \t]+$/, "", x); if (x == p) { y=$6; gsub(/^[ \t]+|[ \t]+$/, "", y); print y; exit } }' bin/MANIFEST.md
}

published=0
pub() {  # pub SRC PLATFORM REL
  local src="$1" plat="$2" rel="$3" have want
  have="$(sha "$src")"
  if [ "$plat" = win-x64 ] && [ "$rel" = win-x64/wkappbot-core.exe.xz ]; then
    want=""
    [ -f "bin/$rel" ] && want="$(xz -dc "bin/$rel" | sha256sum | cut -d' ' -f1)"
  else
    want="$(manifest_sha "$rel" || true)"
  fi
  if [ "$have" = "$want" ]; then
    echo "up to date: bin/$rel ($have)"
    return 0
  fi
  echo "publish: $src -> bin/$rel (kih ${want:-none} -> $have)"
  WK_BIN_NO_PUSH=1 bash "$acq" publish "$src" --platform "$plat"
  published=$((published + 1))
}

pub "$dist/wkappbot-linux-x64" linux-x64 linux-x64/wkappbot
pub "$dist/wkappbot-osx-arm64" osx-arm64 osx-arm64/wkappbot
pub "$dist/wkappbot-osx-x64"   osx-x64   osx-x64/wkappbot
pub "$dist/wkappbot.exe"       win-x64   win-x64/wkappbot.exe
pub "$dist/wkappbot-core.exe"  win-x64   win-x64/wkappbot-core.exe.xz

if [ "$published" = 0 ]; then
  echo "kih bin/ already matches this build -- nothing to push"
  exit 0
fi
git log --oneline -n "$published"
for i in 1 2 3 4 5; do
  if git push origin HEAD:main; then
    echo "pushed $published commit(s) to kih main: $(git rev-parse --short HEAD)"
    exit 0
  fi
  echo "push rejected (attempt $i) -- rebasing on kih main"
  git fetch --quiet origin main
  git rebase origin/main || { git rebase --abort; echo "::error::rebase conflict on kih main"; exit 1; }
  sleep $((i * 3))
done
echo "::error::kih push failed after 5 attempts"
exit 1
