#!/usr/bin/env bash
# wkcheck.sh -- common pre-delegation guards, sourced by wkcodex.sh and wkclaude.sh
# Usage: . "$(dirname "${BASH_SOURCE[0]}")/wkcheck.sh" [wkcodex|wkclaude]
_WK_CALLER="${1:-wk}"

# Guard: uncommitted harness files -- resolve dir from wkcodex.ps1 symlink target
_WK_SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[1]}")" && pwd)"
_WK_HARNESS_DIR=$(powershell -NoProfile -NonInteractive -Command "
  \$link = '$(cygpath -w "$_WK_SCRIPT_DIR/../../wkcodex.ps1" 2>/dev/null || echo "D:/GitHub/wkcodex.ps1")'
  if (Test-Path \$link) { Split-Path (Split-Path ((Get-Item \$link).ResolvedTarget)) }
" 2>/dev/null | tr -d '\r\n')
[[ -z "$_WK_HARNESS_DIR" ]] && _WK_HARNESS_DIR="$(cd "$_WK_SCRIPT_DIR/../.." && pwd)"
if git -C "$_WK_HARNESS_DIR" status --porcelain -- tools/ 2>/dev/null | grep -q .; then
  echo "[$_WK_CALLER] ERROR: uncommitted harness changes in tools/ -- commit first" >&2
  exit 1
fi
