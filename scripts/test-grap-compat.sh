#!/usr/bin/env bash
# test-grap-compat.sh — grap vs grep compatibility test suite
# Usage: ./test-grap-compat.sh [--grap PATH] [--grep PATH]
# Uses $GRAP (default: grap) and $GREP (default: grep) env vars
#   GRAP=/path/to/grap.exe ./test-grap-compat.sh
#   Or override via CLI:  ./test-grap-compat.sh --grap /w/SDK/bin/grap.exe

GRAP="${GRAP:-grap}"
GREP="${GREP:-grep}"

for arg in "$@"; do
  case "$arg" in
    --grap) shift; GRAP="$1"; shift ;;
    --grep) shift; GREP="$1"; shift ;;
  esac
done

# ── Test fixtures ────────────────────────────────────────────────────────────
TMPDIR_TEST=$(mktemp -d)
trap 'rm -rf "$TMPDIR_TEST"' EXIT

# Single file
F1="$TMPDIR_TEST/fruits.txt"
cat > "$F1" <<'EOF'
apple
banana
cherry
apricot
APPLE
EOF

# Second file for multi-file tests
F2="$TMPDIR_TEST/veggies.txt"
cat > "$F2" <<'EOF'
carrot
cabbage
apple
tomato
EOF

# Subdirectory for -r tests
mkdir -p "$TMPDIR_TEST/sub"
cat > "$TMPDIR_TEST/sub/more.txt" <<'EOF'
pineapple
strawberry
apple pie
EOF

PASS=0; FAIL=0

# ── Helpers ─────────────────────────────────────────────────────────────────
run_test() {
  local desc="$1"; local sorted=0
  [[ "$1" == "--sorted" ]] && { sorted=1; shift; desc="$1"; }
  shift
  local grap_args=("$@")

  # Run grep (reference)
  local grep_out grep_exit
  grep_out=$("$GREP" "${grap_args[@]}" 2>/dev/null); grep_exit=$?

  # Run grap (under test)
  local grap_out grap_exit
  grap_out=$("$GRAP" "${grap_args[@]}" 2>/dev/null); grap_exit=$?

  # Normalize paths: grap outputs Windows absolute paths, grep outputs relative/short
  # For comparison: strip path prefix, keep only "filename:content" or just content
  local grep_norm grap_norm
  grep_norm=$(echo "$grep_out" | sed 's|.*/||')   # keep filename:content
  grap_norm=$(echo "$grap_out" | sed 's|.*[/\\]||') # strip Windows path prefix
  # --sorted: sort both sides before comparison (for -r where order may differ)
  if [[ $sorted -eq 1 ]]; then
    grep_norm=$(echo "$grep_norm" | sort)
    grap_norm=$(echo "$grap_norm" | sort)
  fi

  local ok=1
  local issues=""

  # Check exit codes match
  if [[ $grap_exit -ne $grep_exit ]]; then
    issues+=" exit(grap=$grap_exit,grep=$grep_exit)"
    ok=0
  fi

  # Check output content matches (after path normalization)
  if [[ "$grap_norm" != "$grep_norm" ]]; then
    issues+=" output-diff"
    ok=0
  fi

  if [[ $ok -eq 1 ]]; then
    echo "  PASS  $desc"
    ((PASS++))
  else
    echo "  FAIL  $desc [$issues]"
    if [[ "$grap_norm" != "$grep_norm" ]]; then
      diff <(echo "$grep_norm") <(echo "$grap_norm") | head -10 | sed 's/^/        /'
    fi
    ((FAIL++))
  fi
}

run_exit_test() {
  local desc="$1"; shift
  local expected_exit="$1"; shift
  local grap_args=("$@")

  local grap_out grap_exit
  grap_out=$("$GRAP" "${grap_args[@]}" 2>/dev/null); grap_exit=$?

  if [[ $grap_exit -eq $expected_exit ]]; then
    echo "  PASS  $desc (exit=$grap_exit)"
    ((PASS++))
  else
    echo "  FAIL  $desc (expected=$expected_exit, got=$grap_exit)"
    ((FAIL++))
  fi
}

echo "============================================"
echo "  grap compatibility test"
echo "  GRAP: $GRAP"
echo "  GREP: $GREP"
echo "============================================"

# ── Section 1: Basic pattern matching ───────────────────────────────────────
echo ""
echo "[1] Basic matching"
run_test "match word"             "apple"        "$F1"
run_test "match partial"         "app"           "$F1"
run_test "no match"              "mango"         "$F1"
run_test "regex dot"             "a.ple"         "$F1"
run_test "regex anchor"          "^apple$"       "$F1"
run_test "regex or"              "apple\|cherry" "$F1"

# ── Section 2: Exit codes ────────────────────────────────────────────────────
echo ""
echo "[2] Exit codes"
run_exit_test "match → exit 0"   0 "apple"  "$F1"
run_exit_test "no match → exit 1" 1 "mango" "$F1"

# ── Section 3: Flags ─────────────────────────────────────────────────────────
echo ""
echo "[3] Flags"
run_test "-i case insensitive"   -i "apple" "$F1"
run_test "-v invert"             -v "apple" "$F1"
run_test "-l files only"         -l "apple" "$F1" "$F2"
run_test "-c count"              -c "apple" "$F1"
run_test "-n line numbers"       -n "apple" "$F1"

# ── Section 4: Context lines ─────────────────────────────────────────────────
echo ""
echo "[4] Context"
run_test "-A after context"      -A 1 "cherry" "$F1"
run_test "-B before context"     -B 1 "cherry" "$F1"
run_test "-C context"            -C 1 "cherry" "$F1"

# ── Section 5: Multiple files ─────────────────────────────────────────────────
echo ""
echo "[5] Multiple files"
run_test "two files"             "apple" "$F1" "$F2"
run_test "wildcard glob"         "apple" "$TMPDIR_TEST"/*.txt

# ── Section 6: Recursive ─────────────────────────────────────────────────────
echo ""
echo "[6] Recursive"
run_test --sorted "-r recursive" -r "apple" "$TMPDIR_TEST"
run_test "-r with pattern"       -r "pineapple" "$TMPDIR_TEST"

# ── Section 7: Stdout cleanliness ────────────────────────────────────────────
echo ""
echo "[7] Stdout cleanliness (no diagnostic noise in stdout)"
diag_in_stdout=$(grap "apple" "$F1" 2>/dev/null | grep -c "^\[" || true)
if [[ "$diag_in_stdout" -eq 0 ]]; then
  echo "  PASS  no [LOGCAT]/[ACT] lines in stdout"
  ((PASS++))
else
  echo "  FAIL  diagnostic lines leaked to stdout ($diag_in_stdout lines)"
  ((FAIL++))
fi

# ── Section 8: Pipe capture ───────────────────────────────────────────────────
echo ""
echo "[8] Pipe/capture"
captured=$("$GRAP" "apple" "$F1" 2>/dev/null | wc -l | tr -d ' ')
expected=$("$GREP" "apple" "$F1" 2>/dev/null | wc -l | tr -d ' ')
if [[ "$captured" -eq "$expected" ]]; then
  echo "  PASS  piped line count matches grep ($captured lines)"
  ((PASS++))
else
  echo "  FAIL  piped line count: grap=$captured grep=$expected"
  ((FAIL++))
fi

# ── Summary ──────────────────────────────────────────────────────────────────
echo ""
echo "============================================"
echo "  TOTAL: $((PASS+FAIL))  PASS: $PASS  FAIL: $FAIL"
echo "============================================"
[[ $FAIL -eq 0 ]]
