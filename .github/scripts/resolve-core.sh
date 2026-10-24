#!/usr/bin/env bash
# Resolve the core ref to ONE sha for every build job, and on the hourly cron decide whether to build.
# Cron builds only when WKAppBot main moved past the core sha kih bin/ was last published from
# (recorded as "WKAppBot@<sha>" in the kih bin commit message) AND csharp/ changed between them.
# Env: GH_TOKEN (core read), KIH_TOKEN (kih read), CORE_REPO, CORE_REF, EVENT (github.event_name).
# Writes sha=<40 hex> and build=true|false to $GITHUB_OUTPUT.
set -euo pipefail
: "${GH_TOKEN:?WKAPPBOT_CORE_PAT secret is not set}"
: "${CORE_REPO:?CORE_REPO not set}"
ref="${CORE_REF:-main}"
out() { echo "$1"; [ -n "${GITHUB_OUTPUT:-}" ] && echo "$1" >> "$GITHUB_OUTPUT"; return 0; }
core_url="https://x-access-token:${GH_TOKEN}@github.com/${CORE_REPO}.git"

if [[ "$ref" =~ ^[0-9a-f]{40}$ ]]; then
  sha="$ref"
else
  sha="$(git ls-remote "$core_url" "refs/heads/$ref" "refs/tags/$ref" | head -1 | cut -f1)"
  [ -n "$sha" ] || { echo "::error::core ref '$ref' not found in $CORE_REPO"; exit 1; }
fi
out "sha=$sha"

if [ "${EVENT:-}" != schedule ]; then
  out "build=true"
  exit 0
fi

# Cron fallback: compare with the core sha kih bin/ was last published from.
last=""
if [ -n "${KIH_TOKEN:-}" ]; then
  git clone --quiet --depth 200 --filter=blob:none --no-checkout \
    "https://x-access-token:${KIH_TOKEN}@github.com/kiexpert/wkappbot-kih.git" kih-log
  last="$(git -C kih-log log -1 --format=%B --grep='WKAppBot@' -- bin/MANIFEST.md \
          | grep -oE 'WKAppBot@[0-9a-f]{40}' | head -1 | cut -d@ -f2 || true)"
fi
echo "core main: $sha; kih bin/ last published from: ${last:-unknown}"
if [ "$last" = "$sha" ]; then
  echo "kih bin/ is current -- nothing to build"
  out "build=false"
  exit 0
fi
if [ -n "$last" ]; then
  git clone --quiet --filter=blob:none --no-checkout "$core_url" core-log
  if git -C core-log cat-file -e "$last^{commit}" 2>/dev/null && git -C core-log diff --quiet --no-renames "$last" "$sha" -- csharp/; then
    echo "core moved but csharp/ is unchanged since $last -- nothing to build"
    out "build=false"
    exit 0
  fi
fi
out "build=true"
