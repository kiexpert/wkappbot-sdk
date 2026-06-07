#!/usr/bin/env bash
# wkcheck.sh -- common pre-delegation guards, sourced by wkcodex.sh and wkclaude.sh
# Usage: . "$(dirname "${BASH_SOURCE[0]}")/wkcheck.sh" [wkcodex|wkclaude]
_WK_CALLER="${1:-wk}"

. "$(dirname "${BASH_SOURCE[0]}")/wkharness-hints.sh"

# Guard 1: skill update pending (per-repo path; fallback to WK_SKILL_LOCK env var)
_wk_gr=$(git rev-parse --show-toplevel 2>/dev/null)
_wk_sl="${_wk_gr:+$_wk_gr/.wkappbot/harness/code_changed_skill_not_updated_next_agent_starts_blind.lock}"
[[ -z "$_wk_sl" ]] && _wk_sl="${WK_SKILL_LOCK:-}"
if [[ -n "$_wk_sl" && -f "$_wk_sl" ]]; then
  echo "[$_WK_CALLER] ERROR: prior work not recorded -- next agent starts blind. record BOTH: (1) wkappbot skill edit <id> --add-step \"what was done/learned\" and (2) CLAUDE.md Pending/todo section" >&2
  echo "[$_WK_CALLER] hint: after the note is restored, retry the delegation with 3 skill refs and a concrete task." >&2
  exit 1
fi

# Guard 2: uncommitted harness files -- resolve dir from wkcodex.ps1 symlink target
_WK_SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[1]}")" && pwd)"
_WK_HARNESS_DIR=$(powershell -NoProfile -NonInteractive -Command "
  \$link = '$(cygpath -w "$_WK_SCRIPT_DIR/../../wkcodex.ps1" 2>/dev/null || echo "D:/GitHub/wkcodex.ps1")'
  if (Test-Path \$link) { Split-Path (Split-Path ((Get-Item \$link).ResolvedTarget)) }
" 2>/dev/null | tr -d '\r\n')
[[ -z "$_WK_HARNESS_DIR" ]] && _WK_HARNESS_DIR="D:/GitHub/personal-docs"
if git -C "$_WK_HARNESS_DIR" status --porcelain -- tools/ 2>/dev/null | grep -q .; then
  echo "[$_WK_CALLER] ERROR: uncommitted harness changes in tools/ -- commit first" >&2
  echo "[$_WK_CALLER] hint: commit the harness edits first, then retry the delegation path." >&2
  exit 1
fi
