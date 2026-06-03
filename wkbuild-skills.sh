#!/usr/bin/env bash
# wkbuild-skills: rebuild docs/skills/ -- wk-prefix = pace-exempt
set -euo pipefail
REPO="$(cd "$(dirname "$0")" && pwd)"
python "$REPO/scripts/build-skill-page.py"
if [[ "${1:-}" == "--serve" ]]; then
  python -m http.server "${2:-8888}" --directory "$REPO/docs"
fi