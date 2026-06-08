#!/usr/bin/env bash
# Shared block/warn guidance for harness-managed wrappers.

_wk_harness_hint_for_pattern() {
  local caller="${1:-wk}"
  local pattern="${2:-}"

  case "$pattern" in
    *"write_file("*|*"apply_patch("*|*"read_file("*)
      echo "[$caller] hint: use wkappbot file read/write/edit or route the work through Agent.cmd with 3 wkappbot skill read refs."
      echo "[$caller] hint: relevant skills: wkappbot skill read wkharness-guards, wkappbot skill read codex-tool-wrappers, wkappbot skill read agent-cmd"
      ;;
    *"bash.*powershell"*|*"bash.*pwsh"*)
      echo "[$caller] hint: run PowerShell directly instead of wrapping it inside bash."
      echo "[$caller] hint: relevant skills: wkappbot skill read wkharness-guards, wkappbot skill read codex-tool-wrappers"
      ;;
    *"core.hooksPath"*)
      echo "[$caller] hint: do not bypass git hooks. Use the normal git path so the harness can inspect the commit."
      echo "[$caller] hint: if this is a delegation task, refresh CLAUDE.md Pending first and then retry."
      ;;
  esac
}

_wk_harness_block() {
  local caller="${1:-wk}"
  local pattern="${2:-}"
  echo "[$caller] [harness:block] $pattern" >&2
  _wk_harness_hint_for_pattern "$caller" "$pattern" >&2
}

_wk_harness_warn() {
  local caller="${1:-wk}"
  local pattern="${2:-}"
  echo "[$caller] [harness:warn] $pattern" >&2
}

_wk_harness_prompt_is_delegation() {
  local prompt="${1:-}"
  printf '%s' "$prompt" | grep -Eiq -- '(^|[^[:alnum:]_])(Agent\.cmd|delegate|delegation|subagent|wkappbot[[:space:]]+skill[[:space:]]+read|SPEC)([^[:alnum:]_]|$)' 2>/dev/null
}

_wk_harness_section_present() {
  local prompt="${1:-}"
  local pattern="${2:-}"
  printf '%s' "$prompt" | grep -Eiq -- "$pattern" 2>/dev/null
}

_wk_harness_validate_delegation_brief() {
  local caller="${1:-wk}"
  local raw_input="${2:-}"
  local prompt_text="${3:-}"
  local model_hint="${4:-}"
  local model="${model_hint:-}"
  local skill_reads missing
  local -a missing_sections=()

  [[ -z "$prompt_text" ]] && return 0
  if ! _wk_harness_prompt_is_delegation "$prompt_text"; then
    return 0
  fi

  if printf '%s' "$raw_input" | grep -Eiq -- '(^|[[:space:]])(--model|-m)[[:space:]]+opus([^[:alnum:]_]|$)|claude-opus' 2>/dev/null; then
    model="opus"
  fi

  skill_reads=$(printf '%s' "$prompt_text" | grep -Eic -- 'wkappbot[[:space:]]+skill[[:space:]]+read[[:space:]]+' 2>/dev/null || true)
  if (( skill_reads < 3 )); then
    echo "[$caller] [harness:block] delegation brief too thin: need 3 wkappbot skill read refs" >&2
    echo "[$caller] hint: start with 3 lines like 'wkappbot skill read on-load', 'wkappbot skill read wkharness-guards', and one task-specific skill" >&2
    echo "[$caller] hint: see wkappbot skill read agent-dispatch-preflight" >&2
    return 1
  fi

  if ! _wk_harness_section_present "$prompt_text" '(^|[[:space:]])(1\.Goal|Goal:|GOAL:)([[:space:]]|$)'; then missing_sections+=("Goal"); fi
  if ! _wk_harness_section_present "$prompt_text" '(^|[[:space:]])(2\.Files|Files:|FILES:)([[:space:]]|$)'; then missing_sections+=("Files"); fi
  if ! _wk_harness_section_present "$prompt_text" '(^|[[:space:]])(3\.Approach|Approach:|APPROACH:)([[:space:]]|$)'; then missing_sections+=("Approach"); fi
  if ! _wk_harness_section_present "$prompt_text" '(^|[[:space:]])(4\.Constraints|Constraints:|CONSTRAINTS:)([[:space:]]|$)'; then missing_sections+=("Constraints"); fi
  if ! _wk_harness_section_present "$prompt_text" '(^|[[:space:]])(5\.Exit|Exit:|EXIT:)([[:space:]]|$)'; then missing_sections+=("Exit"); fi

  if ((${#missing_sections[@]} > 0)); then
    missing="${missing_sections[*]}"
    echo "[$caller] [harness:block] delegation brief missing: $missing" >&2
    echo "[$caller] hint: add a 5-part brief: Goal / Files / Approach / Constraints / Exit" >&2
    if [[ "$model" == *opus* ]]; then
      echo "[$caller] hint: for Opus, add SPEC 1.Goal 2.Files 3.Approach 4.Constraints 5.Exit at the top" >&2
    fi
    echo "[$caller] hint: see wkappbot skill read agent-dispatch-preflight and wkappbot skill read codex-opus-agent-delegation" >&2
    return 1
  fi

  if [[ "$model" == *opus* ]] && ! _wk_harness_section_present "$prompt_text" '(^|[[:space:]])SPEC([[:space:]]|$|:)'; then
    echo "[$caller] [harness:block] Opus brief missing SPEC block" >&2
    echo "[$caller] hint: add SPEC 1.Goal 2.Files 3.Approach 4.Constraints 5.Exit" >&2
    echo "[$caller] hint: see wkappbot skill read agent-dispatch-preflight" >&2
    return 1
  fi
}
