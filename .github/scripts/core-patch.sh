#!/usr/bin/env bash
# Compute the core patch number ONCE, with the core's own rule: the commit count from the last
# commit that changed the <Version>/<VersionPrefix> line in the core csproj/props files up to the
# core sha. Every build job gets it as env WKAPPBOT_PATCH and -p:WKAppBotPatch (the override the
# core added in WKAppBot PR #185), so all platforms print the same 7.12.N.
# Needs ./core cloned by clone-core.sh (full history). Writes patch=<N> and bump=<sha>.
set -euo pipefail
out() { echo "$1"; [ -n "${GITHUB_OUTPUT:-}" ] && echo "$1" >> "$GITHUB_OUTPUT"; return 0; }
cd core
head="$(git rev-parse HEAD)"
bump="$(git log -1 --format=%H -G '<Version(Prefix)?>[[:space:]]*[0-9]+\.[0-9]+' "$head" -- \
        ':(glob)**/*.csproj' ':(glob)**/*.props')"
[ -n "$bump" ] || { echo "::error::no commit changed a <Version>/<VersionPrefix> line in the core csproj/props"; exit 1; }
n="$(git rev-list --count "$bump..$head")"
echo "core version bump commit: $(git log -1 --format='%h %cI %s' "$bump" | cut -c1-120)"
echo "files: $(git show --name-only --format= "$bump" | grep -E '\.(csproj|props)$' | tr '\n' ' ')"
out "bump=$bump"
out "patch=$n"
