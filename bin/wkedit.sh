#!/usr/bin/env bash
# wkedit.sh -- encoding-safe file editor (harness-managed)
# AUTO-FIX: --old-file/--new-file normalized (BOM strip + line-ending match).
# ROUTING:
#   wkedit.sh file1.ps1 file2.md            -> wkedit.exe (multi-file open)
#   wkedit.sh "old" "new" file.ps1          -> wkedit.exe OLD NEW FILE (string replace)
#   wkedit.sh edit --old-file O --new-file N FILE  -> wkedit.exe (strips 'edit' subcmd)
#   wkedit.sh write FILE --text "content"   -> wkappbot file write (different subcmd)
#   wkedit.sh read FILE                     -> wkappbot file read

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
      _wk_harness_block "wkedit" "$pattern"
      exit 1
    fi
  done < <(awk '/^##[[:space:]]+harness:block[[:space:]]+/ { sub(/^##[[:space:]]+harness:block[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)

  while IFS= read -r pattern; do
    [[ -z "$pattern" ]] && continue
    if printf '%s' "$tool_input" | grep -Pq -- "$pattern" 2>/dev/null; then
      _wk_harness_warn "wkedit" "$pattern"
    fi
  done < <(awk '/^##[[:space:]]+harness:warn[[:space:]]+/ { sub(/^##[[:space:]]+harness:warn[[:space:]]+/, ""); print }' "$claude_md" 2>/dev/null)
}
check_harness "$@"

# ? ?  BOM strip + line-ending normalize for a pattern file ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 
_norm_pattern() {
  local src="$1" target="$2"
  local tmp; tmp=$(mktemp)
  local bom; bom=$(head -c3 "$src" | od -An -tx1 | tr -d ' \n')
  if [[ "$bom" == "efbbbf"* ]]; then
    tail -c +4 "$src" > "$tmp"
    echo "[wkedit.sh] BOM stripped from pattern" >&2
  else
    cp "$src" "$tmp"
  fi
  if [[ -f "$target" ]] && grep -qP '\r$' "$target" 2>/dev/null; then
    sed -i 's/\r$//' "$tmp" && sed -i 's/$/\r/' "$tmp"
    echo "[wkedit.sh] pattern CRLF-normalized to match target" >&2
  else
    sed -i 's/\r$//' "$tmp"
  fi
  echo "$tmp"
}

# ? ?  Normalize --old-file/--new-file in an arg array ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 
_do_edit() {
  # args: all args after subcommand stripping (if any)
  # find target file (last non-option non-tempfile arg)
  local args=("$@") target="" old_file="" new_file="" old_idx=-1 new_idx=-1
  local has_replace_all=0 has_old_file=0
  for arg in "${args[@]}"; do
    case "$arg" in --replace-all) has_replace_all=1 ;; --old-file|--old-string) has_old_file=1 ;; esac
  done

  run_once() {
    local _args=("$@") _target="" _old_file="" _new_file="" _old_idx=-1 _new_idx=-1
    local _tmps=()

    for ((i=0; i<${#_args[@]}; i++)); do
      case "${_args[$i]}" in
        --old-file|--old-string) _old_idx=$i; ((i++)); _old_file="${_args[$i]}" ;;
        --new-file|--new-string) _new_idx=$i; ((i++)); _new_file="${_args[$i]}" ;;
      esac
    done
    for ((i=${#_args[@]}-1; i>=0; i--)); do
      local _a="${_args[$i]}"
      [[ -f "$_a" && "$_a" != /tmp/* && "$_a" != /var/* ]] && { _target="$_a"; break; }
    done

    if [[ -n "$_old_file" && -f "$_old_file" ]]; then
      local _norm; _norm=$(_norm_pattern "$_old_file" "$_target")
      _args[$((_old_idx+1))]="$_norm"; _tmps+=("$_norm")
    fi
    if [[ -n "$_new_file" && -f "$_new_file" ]]; then
      local _norm; _norm=$(_norm_pattern "$_new_file" "$_target")
      _args[$((_new_idx+1))]="$_norm"; _tmps+=("$_norm")
    fi

    wkedit.exe "${_args[@]}"
    local _rc=$?
    rm -f "${_tmps[@]}"
    return $_rc
  }

  local stderr_tmp retry_stderr_tmp old_arg_tmp="" rc retry_rc
  stderr_tmp=$(mktemp)
  retry_stderr_tmp=$(mktemp)

  run_once "${args[@]}" 2>"$stderr_tmp"
  rc=$?
  if (( rc == 0 )); then
    rm -f "$stderr_tmp" "$retry_stderr_tmp"
    return 0
  fi

  if [[ -z "${WKEDIT_NO_AUTO:-}" ]]; then
    if grep -qi 'matches multiple' "$stderr_tmp" && (( has_replace_all == 0 )); then
      echo "[wkedit.sh] auto-retry: matches multiple; retrying with --replace-all" >&2
      run_once "${args[@]}" --replace-all 2>"$retry_stderr_tmp"
      retry_rc=$?
      if (( retry_rc == 0 )); then
        rm -f "$stderr_tmp" "$retry_stderr_tmp"
        return 0
      fi
      cat "$stderr_tmp" >&2
      cat "$retry_stderr_tmp" >&2
      rm -f "$stderr_tmp" "$retry_stderr_tmp"
      return $retry_rc
    fi

    if grep -qi 'old_string not found' "$stderr_tmp" && (( has_old_file == 0 )) && (( ${#args[@]} >= 3 )) && [[ "${args[0]}" != --* ]]; then
      old_arg_tmp=$(mktemp)
      printf '%s' "${args[0]}" > "$old_arg_tmp"
      echo "[wkedit.sh] auto-retry: old_string not found; retrying with --old-file tempfile" >&2
      run_once --old-file "$old_arg_tmp" "${args[1]}" "${args[@]:2}" 2>"$retry_stderr_tmp"
      retry_rc=$?
      rm -f "$old_arg_tmp"
      if (( retry_rc == 0 )); then
        rm -f "$stderr_tmp" "$retry_stderr_tmp"
        return 0
      fi
      cat "$stderr_tmp" >&2
      cat "$retry_stderr_tmp" >&2
      rm -f "$stderr_tmp" "$retry_stderr_tmp"
      return $retry_rc
    fi
  fi

  cat "$stderr_tmp" >&2
  rm -f "$stderr_tmp" "$retry_stderr_tmp"
  return $rc
}

# ? ?  BOM strip from script output files ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 
_strip_bom_outputs() {
  for arg in "$@"; do
    [[ -f "$arg" ]] || continue
    case "$arg" in *.sh|*.py|*.rb|*.js|*.ts|*.bash) ;; *) continue ;; esac
    local bom; bom=$(head -c3 "$arg" | od -An -tx1 | tr -d ' \n')
    if [[ "$bom" == "efbbbf"* ]]; then
      local tmp; tmp=$(mktemp)
      tail -c +4 "$arg" > "$tmp" && mv "$tmp" "$arg"
      chmod +x "$arg" 2>/dev/null || true
      echo "[wkedit.sh] stripped BOM from output: $arg" >&2
    fi
  done
}

# _do_patch: stdin heredoc with OLD<NL>===SPLIT===<NL>NEW -> wkedit.exe --old-file/--new-file TARGET
_do_patch() {
  local target="$1"
  if [[ -z "$target" ]]; then
    echo "[wkedit.sh] --patch requires a target file path" >&2
    return 2
  fi
  local stdin_tmp old_tmp new_tmp
  stdin_tmp=$(mktemp); old_tmp=$(mktemp); new_tmp=$(mktemp)
  cat > "$stdin_tmp"
  if ! grep -q '^===SPLIT===$' "$stdin_tmp"; then
    echo "[wkedit.sh] --patch stdin missing ===SPLIT=== marker line" >&2
    rm -f "$stdin_tmp" "$old_tmp" "$new_tmp"
    return 2
  fi
  local split_line; split_line=$(grep -n '^===SPLIT===$' "$stdin_tmp" | head -n1 | cut -d: -f1)
  sed -n "1,$((split_line-1))p" "$stdin_tmp" > "$old_tmp"
  tail -n "+$((split_line+1))" "$stdin_tmp" > "$new_tmp"
  _do_edit --old-file "$old_tmp" --new-file "$new_tmp" "$target"
  local rc=$?
  rm -f "$stdin_tmp" "$old_tmp" "$new_tmp"
  return $rc
}

# ? ?  Main routing ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 
case "$1" in
  write|read|grep|glob)
    # These are wkappbot file subcommands, not wkedit.exe
    wkappbot file "$@"
    rc=$?
    [[ "$1" == "write" ]] && _strip_bom_outputs "$2"
    exit $rc
    ;;
  edit)
    # Strip 'edit' subcommand (wkedit.exe IS file edit -- don't double-apply)
    shift
    _do_edit "$@"
    rc=$?
    _strip_bom_outputs "$@"
    exit $rc
    ;;
  --patch)
    # Heredoc patch: cat OLDNEW.txt with marker line | wkedit.sh --patch TARGET
    shift
    _do_patch "$@"
    exit $?
    ;;
  --test)
    _p=0; _f=0
    _t() {
      local l="$1" wr="$2" ws="$3"; shift 3
      eval "$@" >/tmp/wket.txt 2>&1; local r=$?
      if [[ $r -eq $wr ]] && { [[ -z "$ws" ]] || grep -q "$ws" /tmp/wket.txt; }; then
        echo "  PASS  $l"; ((_p++))
      else echo "  FAIL  $l  (rc=$r want=$wr)"; head -1 /tmp/wket.txt | sed 's/^/        /'; ((_f++)); fi
    }
    echo ""; echo "=== wkedit.sh self-test ==="
    tf=$(mktemp)
    printf 'foo=1\nfoo=2\nbar=3\n' > "$tf"
    _t "T1 basic replace"           0 ""           'wkedit.sh '"'"'foo=1'"'"' '"'"'baz=1'"'"' "$tf"'
    printf 'foo=1\nfoo=2\nbar=3\n' > "$tf"
    _t "T2 auto-retry multi"        0 "auto-retry" 'wkedit.sh '"'"'foo'"'"' '"'"'baz'"'"' "$tf" 2>&1'
    printf 'foo=1\nfoo=2\nbar=3\n' > "$tf"
    _t "T3 WKEDIT_NO_AUTO opt-out"  1 ""           'WKEDIT_NO_AUTO=1 wkedit.sh '"'"'foo'"'"' '"'"'baz'"'"' "$tf" 2>/dev/null'
    printf 'alpha\nbeta\ngamma\n'   > "$tf"
    _t "T4 patch mode"              0 ""           'printf '"'"'beta\n===SPLIT===\nDELTA\n'"'"' | wkedit.sh --patch "$tf"'
    printf 'version=1.2.3\n'        > "$tf"
    _t "T5 regex capture group"     0 ""           'wkedit.sh '"'"'version=([0-9.]+)'"'"' '"'"'ver=$1'"'"' "$tf" --regex'
    printf 'foo=1\n'                > "$tf"
    _t "T6 genuine no-match fails"  1 ""           'wkedit.sh '"'"'NOMATCH_XYZ'"'"' '"'"'new'"'"' "$tf" 2>/dev/null'
    rm -f "$tf" /tmp/wket.txt
    echo ""; echo "  Result: $_p/$((_p+_f)) passed"
    [[ $_f -eq 0 ]] && exit 0 || exit 1
    ;;
  *)
    # Direct: file1 file2, "old" "new" file, or --old-file/--new-file ...
    _do_edit "$@"
    rc=$?
    _strip_bom_outputs "$@"
    exit $rc
    ;;
esac
