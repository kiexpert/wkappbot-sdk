#!/usr/bin/env bash
# Clone the private core repo at CORE_REF into ./core (full commit history, blobs on demand,
# so Directory.Build.props can count the patch number). Never prints the token; the token
# stays in the clone's remote URL because blobs are fetched lazily (ephemeral runner only).
# Env: GH_TOKEN (read access), CORE_REPO (owner/name), CORE_REF (branch, tag or sha; default main).
set -euo pipefail
: "${GH_TOKEN:?WKAPPBOT_CORE_PAT secret is not set}"
: "${CORE_REPO:?CORE_REPO not set}"
ref="${CORE_REF:-main}"
git clone --quiet --filter=blob:none --no-checkout \
  "https://x-access-token:${GH_TOKEN}@github.com/${CORE_REPO}.git" core
git -C core -c advice.detachedHead=false checkout --quiet "$ref"
sha=$(git -C core rev-parse HEAD)
echo "core: ${CORE_REPO}@${ref} -> ${sha}"
[ -n "${GITHUB_OUTPUT:-}" ] && echo "sha=${sha}" >> "$GITHUB_OUTPUT"
exit 0
