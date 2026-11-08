#!/usr/bin/env bash
# Compute the core patch number ONCE with the core's own rule (_WKAutoVersion in
# csharp/Directory.Build.props, confirmed by ROOT W / WKAppBot PR #191):
#   bump  = newest commit whose diff changes the literal <WKAppBotBaseVersion>X.Y</WKAppBotBaseVersion>
#           line in csharp/Directory.Build.props (git log -1 -S pickaxe; no tags)
#   patch = git rev-list --count bump..HEAD -- '*.cs' '*.csproj' '*.props' '*.yaml'
# Every build job gets it as env WKAPPBOT_PATCH and -p:WKAppBotPatch (the override from WKAppBot
# PR #185), so all platforms print the same X.Y.N.
# Needs ./core cloned by clone-core.sh (full history). Writes patch=<N> and bump=<sha>.
set -euo pipefail
out() { echo "$1"; [ -n "${GITHUB_OUTPUT:-}" ] && echo "$1" >> "$GITHUB_OUTPUT"; return 0; }
cd core
head="$(git rev-parse HEAD)"
props=csharp/Directory.Build.props
base="$(sed -n 's:.*<WKAppBotBaseVersion>\([^<]*\)</WKAppBotBaseVersion>.*:\1:p' "$props" | head -1)"
[ -n "$base" ] || { echo "::error::no <WKAppBotBaseVersion> in $props"; exit 1; }
bump="$(git log -1 --format=%H -S "<WKAppBotBaseVersion>${base}</WKAppBotBaseVersion>" "$head" -- "$props")"
[ -n "$bump" ] || { echo "::error::no commit introduced <WKAppBotBaseVersion>${base}</WKAppBotBaseVersion>"; exit 1; }
n="$(git rev-list --count "$bump..$head" -- '*.cs' '*.csproj' '*.props' '*.yaml')"
echo "core base version: $base -> $base.$n"
echo "core version bump commit: $(git log -1 --format='%h %cI %s' "$bump" | cut -c1-120)"
out "bump=$bump"
out "patch=$n"
