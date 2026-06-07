#!/usr/bin/env bash
# wkcodex.sh -- bash wrapper for wkcodex.ps1/wkharness (harness-managed)
# Usage: wkcodex.sh task words without quotes [-C dir] [--model m]
# Non-option args joined into single -Task string -- no quoting needed.

. "$(dirname "${BASH_SOURCE[0]}")/wkharness-hints.sh"

check_harness() {
  local root claude_md tool_input pattern
  root=$(git rev-parse --show-toplevel 2>/dev/null) || return 0
  claude_md="$root/CLAUDE.md"
  [[ -f "$claude_md" ]] || return 0
  tool_input="$(basename "$0") $*"

  while IFS= read -r pattern; do
    [[ -z "$pattern" ]] && continue
    if printf '%s' "$tool_input" | grep -Pq -- "$pattern" 2>/dev/null; then
      return 0
    fi
  done < <(awk '/^##[[:space:]]+harness:safe[[:space:]]+/ { sub(/^##[[:space:]]+harness:safe[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)

  while IFS= read -r pattern; do
    [[ -z "$pattern" ]] && continue
    if printf '%s' "$tool_input" | grep -Pq -- "$pattern" 2>/dev/null; then
      _wk_harness_block "wkcodex" "$pattern"
      exit 1
    fi
  done < <(awk '/^##[[:space:]]+harness:block[[:space:]]+/ { sub(/^##[[:space:]]+harness:block[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)

  while IFS= read -r pattern; do
    [[ -z "$pattern" ]] && continue
    if printf '%s' "$tool_input" | grep -Pq -- "$pattern" 2>/dev/null; then
      _wk_harness_warn "wkcodex" "$pattern"
    fi
  done < <(awk '/^##[[:space:]]+harness:warn[[:space:]]+/ { sub(/^##[[:space:]]+harness:warn[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)
}
check_harness "$@"

# Guard: skill update pending (per-repo path; fallback to WK_SKILL_LOCK env var)
_wk_gr=$(git rev-parse --show-toplevel 2>/dev/null)
_wk_sl="${_wk_gr:+$_wk_gr/.wkappbot/harness/code_changed_skill_not_updated_next_agent_starts_blind.lock}"
[[ -z "$_wk_sl" ]] && _wk_sl="${WK_SKILL_LOCK:-}"
if [[ -n "$_wk_sl" && -f "$_wk_sl" ]]; then
  echo "[wkcodex] ERROR: prior work not recorded -- next agent starts blind. record BOTH: (1) wkappbot skill edit <id> --add-step \"what was done/learned\" and (2) CLAUDE.md Pending/todo section" >&2
  exit 1
fi
# Guard: uncommitted harness files
_HARNESS_DIR=$(git -C "D:/GitHub/personal-docs" rev-parse --show-toplevel 2>/dev/null || echo "D:/GitHub/personal-docs")
_CURRENT_REPO=$(git rev-parse --show-toplevel 2>/dev/null || echo "")
if [[ "$_CURRENT_REPO" == "$_HARNESS_DIR" ]] && git -C "$_HARNESS_DIR" status --porcelain -- tools/ 2>/dev/null | grep -q .; then
  echo "[wkcodex] ERROR: uncommitted harness changes in tools/ -- commit first" >&2
  exit 1
fi
opts=(); task_parts=(); skip_next=false; dashdash=false
for a in "$@"; do
  if $dashdash; then task_parts+=("$a")
  elif $skip_next; then opts+=("$a"); skip_next=false
  elif [[ "$a" == "--" ]]; then dashdash=true
  elif [[ "$a" == "-C" || "$a" == "--model" ]]; then opts+=("$a"); skip_next=true
  elif [[ "$a" == -* ]]; then opts+=("$a")
  else task_parts+=("$a")
  fi
done
exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass \
  -File "D:/GitHub/wkcodex.ps1" -Task "${task_parts[*]}" "${opts[@]}"
