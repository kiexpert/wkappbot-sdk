#!/usr/bin/env bash
# wk-gg-main.sh: SDK Product Manager Main Duties Automation Relay
# Calls bash scripts/gg-main.sh from repo root

SCRIPT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)
REPO_ROOT="${SCRIPT_DIR}"

if [ -f "${REPO_ROOT}/scripts/gg-main.sh" ]; then
  bash "${REPO_ROOT}/scripts/gg-main.sh" "$@"
else
  echo "[ERROR] gg-main.sh not found in scripts/ directory"
  exit 1
fi
