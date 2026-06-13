#!/usr/bin/env bash
# wkcodex.sh -- bash wrapper for wkcodex.ps1/wkharness (harness-managed)
# Usage: wkcodex.sh task words without quotes [-C dir] [--model m]
# Non-option args joined into single -Task string -- no quoting needed.

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
      echo "[harness:block] $pattern" >&2
      exit 1
    fi
  done < <(awk '/^##[[:space:]]+harness:block[[:space:]]+/ { sub(/^##[[:space:]]+harness:block[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)

  while IFS= read -r pattern; do
    [[ -z "$pattern" ]] && continue
    if printf '%s' "$tool_input" | grep -Pq -- "$pattern" 2>/dev/null; then
      echo "[harness:warn] $pattern" >&2
    fi
  done < <(awk '/^##[[:space:]]+harness:warn[[:space:]]+/ { sub(/^##[[:space:]]+harness:warn[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)
}
check_harness "$@"

# Guard: uncommitted harness files
_SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
_HARNESS_DIR=$(git -C "$_SCRIPT_DIR/../.." rev-parse --show-toplevel 2>/dev/null || (cd "$_SCRIPT_DIR/../.." && pwd))
_CURRENT_REPO=$(git rev-parse --show-toplevel 2>/dev/null || echo "")
if [[ "$_CURRENT_REPO" == "$_HARNESS_DIR" ]] && git -C "$_HARNESS_DIR" status --porcelain -- tools/ 2>/dev/null | grep -q .; then
  echo "[wkcodex] ERROR: uncommitted harness changes in tools/ -- commit first" >&2
  exit 1
fi
# Arg routing: ONLY -C/--model (with values) are wrapper options; tokens after a
# literal -- are codex extra-args. Everything else -- including dash-prefixed task
# words like --limit 50 -- stays in the task. A previous catch-all (-* -> opts)
# leaked task dash-tokens into wkharness.ps1 param binding (prefix-matched to
# -MonthlyLimitM), crashing the whole codex path whenever a task contained a --flag.
opts=(); task_parts=(); skip_next=false; dashdash=false
for a in "$@"; do
  if $dashdash; then opts+=("$a")
  elif $skip_next; then opts+=("$a"); skip_next=false
  elif [[ "$a" == "--" ]]; then dashdash=true
  elif [[ "$a" == "-C" || "$a" == "--model" ]]; then opts+=("$a"); skip_next=true
  else task_parts+=("$a")
  fi
done
exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass \
  -File "D:/GitHub/wkcodex.ps1" -Task "${task_parts[*]}" "${opts[@]}"
