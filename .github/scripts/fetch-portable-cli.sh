#!/usr/bin/env bash
# Collect the portable wkappbot CLI binaries + SHA256SUMS into OUT_DIR.
#
#   1. Download them from the newest core release that carries all of them
#      (attached by the core repo's "Portable CLI Build" workflow on v* tags).
#   2. Otherwise build them from a core checkout (WKAppBot.CLI.Linux, single-file,
#      self-contained; osx targets cross-publish from Linux).
#
# Env: GH_TOKEN (read access to CORE_REPO), CORE_REPO (owner/name), OUT_DIR (default: portable).
# Prints "source=release:<tag>" or "source=build:<sha>" to $GITHUB_OUTPUT when set.
set -euo pipefail

ASSETS=(wkappbot-linux-x64 wkappbot-osx-arm64 wkappbot-osx-x64)
RIDS=(linux-x64 osx-arm64 osx-x64)
OUT_DIR="${OUT_DIR:-portable}"
: "${CORE_REPO:?CORE_REPO not set}"
: "${GH_TOKEN:?GH_TOKEN not set}"
mkdir -p "$OUT_DIR"

emit() { echo "$1"; [ -n "${GITHUB_OUTPUT:-}" ] && echo "$1" >> "$GITHUB_OUTPUT"; return 0; }

try_release() {
  local tag
  # Newest release whose asset list holds every portable binary and SHA256SUMS.
  tag=$(gh release list --repo "$CORE_REPO" --limit 30 --json tagName --jq '.[].tagName' | while read -r t; do
    names=$(gh release view "$t" --repo "$CORE_REPO" --json assets --jq '.assets[].name' 2>/dev/null || true)
    ok=1
    for a in "${ASSETS[@]}" SHA256SUMS; do grep -qx "$a" <<<"$names" || { ok=0; break; }; done
    if [ "$ok" = 1 ]; then echo "$t"; break; fi
  done)
  [ -n "$tag" ] || return 1
  echo "Fetching portable CLI from $CORE_REPO release $tag"
  local args=()
  for a in "${ASSETS[@]}" SHA256SUMS; do args+=(--pattern "$a"); done
  gh release download "$tag" --repo "$CORE_REPO" --dir "$OUT_DIR" --clobber "${args[@]}"
  emit "source=release:$tag"
}

build_from_source() {
  echo "No core release carries the portable CLI -- building from $CORE_REPO source"
  local src
  src=$(mktemp -d)
  git clone --depth 1 "https://x-access-token:${GH_TOKEN}@github.com/${CORE_REPO}.git" "$src/core"
  local proj="$src/core/csharp/src/WKAppBot.CLI.Linux"
  [ -f "$proj/WKAppBot.CLI.Linux.csproj" ] || { echo "::error::$proj/WKAppBot.CLI.Linux.csproj missing"; return 1; }
  for i in "${!RIDS[@]}"; do
    (cd "$proj" && dotnet publish WKAppBot.CLI.Linux.csproj -c Release -r "${RIDS[$i]}" --self-contained true \
        -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o "publish-${RIDS[$i]}")
    cp "$proj/publish-${RIDS[$i]}/wkappbot" "$OUT_DIR/${ASSETS[$i]}"
  done
  (cd "$OUT_DIR" && sha256sum "${ASSETS[@]}" > SHA256SUMS)
  emit "source=build:$(git -C "$src/core" rev-parse --short HEAD)"
}

try_release || build_from_source

(cd "$OUT_DIR" && sha256sum -c SHA256SUMS)
ls -la "$OUT_DIR"
