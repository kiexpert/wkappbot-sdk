#!/usr/bin/env bash
# Clone the private kih harness repo into ./kih (shallow; depth grows on push retry).
# Env: GH_TOKEN (KIH_PUSH_PAT: contents read/write on kiexpert/wkappbot-kih). Never prints the token.
set -euo pipefail
: "${GH_TOKEN:?KIH_PUSH_PAT secret is not set}"
git clone --quiet --depth 50 "https://x-access-token:${GH_TOKEN}@github.com/kiexpert/wkappbot-kih.git" kih
echo "kih: $(git -C kih rev-parse --short HEAD) $(git -C kih log -1 --format=%s)"
