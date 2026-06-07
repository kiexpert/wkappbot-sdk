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
