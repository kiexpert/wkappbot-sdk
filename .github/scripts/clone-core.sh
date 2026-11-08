#!/usr/bin/env bash
# Clone the private core repo at CORE_REF into ./core with full history AND all blobs, so the
# core's version rule (patch = commit count since the version bump) gives the same 7.12.N on
# every runner. A blob:none clone printed 7.12.0 on Windows (lazy blob fetch fails inside the
# msbuild git call) while Linux/macOS printed 7.12.25. Never prints the token.
# Env: GH_TOKEN (read access), CORE_REPO (owner/name), CORE_REF (branch, tag or sha; default main).
set -euo pipefail
: "${GH_TOKEN:?WKAPPBOT_CORE_PAT secret is not set}"
: "${CORE_REPO:?CORE_REPO not set}"
ref="${CORE_REF:-main}"
git clone --quiet --no-checkout \
  "https://x-access-token:${GH_TOKEN}@github.com/${CORE_REPO}.git" core
git -C core -c advice.detachedHead=false checkout --quiet "$ref"
sha=$(git -C core rev-parse HEAD)
echo "core: ${CORE_REPO}@${ref} -> ${sha} (commits: $(git -C core rev-list --count HEAD), shallow: $(git -C core rev-parse --is-shallow-repository))"
[ -n "${GITHUB_OUTPUT:-}" ] && echo "sha=${sha}" >> "$GITHUB_OUTPUT"
exit 0
