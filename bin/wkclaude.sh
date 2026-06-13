#!/usr/bin/env bash
# wkclaude.sh -- non-interactive claude wrapper (harness-managed)
# Usage: wkclaude.sh prompt words without quotes [--model opus] [--output-format json]
# All non-option args are joined as the prompt; options are passed through.

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

. "$(dirname "${BASH_SOURCE[0]}")/wkcheck.sh" wkclaude
MODEL="${WKCLAUDE_MODEL:-haiku}"
opts=("--print" "--dangerously-skip-permissions" "--model" "$MODEL")
prompt_parts=()
skip_next=false
for a in "$@"; do
  if $skip_next; then
    opts+=("$a"); skip_next=false
  elif [[ "$a" == --model || "$a" == -m || "$a" == --output-format ]]; then
    opts+=("$a"); skip_next=true
  elif [[ "$a" == -* ]]; then
    opts+=("$a")
  else
    prompt_parts+=("$a")
  fi
done
exec claude "${opts[@]}" "${prompt_parts[*]}"