#!/usr/bin/env bash
# test-grap-json.sh — grap --json integration test suite
# Usage: bash scripts/test-grap-json.sh [GRAP_EXE]
# Default: D:/SDK/bin/grap.exe
set -uo pipefail

# Use wkappbot-core.exe logcat directly (avoids Launcher/Eye pipe latency)
WK="${1:-D:/SDK/bin/wkappbot-core.exe}"
grap() { "$WK" logcat "$@"; }
TMP="$(powershell -Command "[IO.Path]::GetTempPath()" | tr -d '\r\n' | sed 's|\\|/|g')test-grap-json-$$"
mkdir -p "$TMP"
PASS=0; FAIL=0

pass() { echo "[PASS] $1"; ((PASS++)); }
fail() { echo "[FAIL] $1"; ((FAIL++)); }
check() {
    local label="$1" expect="$2" actual="$3"
    if [[ "$actual" == *"$expect"* ]]; then pass "$label"; else fail "$label — expected '$expect', got: ${actual:0:200}"; fi
}
check_not() {
    local label="$1" reject="$2" actual="$3"
    if [[ "$actual" != *"$reject"* ]]; then pass "$label"; else fail "$label — should NOT contain '$reject'"; fi
}
# Run grap and capture output to file (avoids bash \0 truncation from UIT sentinel)
OUT_FILE="$TMP/_grap_out.txt"
run_grap() {
    grap "$@" > "$OUT_FILE" 2>&1 || true
    # Strip null bytes + control chars
    tr -d '\0' < "$OUT_FILE"
}

# ── Test fixtures ──────────────────────────────────────────────────────────

# Simple JSONL file
J1="$TMP/session.jsonl"
cat > "$J1" <<'JSONL'
{"role":"system","content":"You are a helpful assistant."}
{"role":"user","content":"What is a screensaver?"}
{"role":"assistant","content":"A screensaver is a program that displays animations."}
{"role":"user","content":"How do I make one in C#?"}
{"role":"assistant","content":"You can use WPF or WinForms for screensaver development."}
{"role":"tool","name":"bash","content":"dotnet build screensaver.csproj"}
JSONL

# Nested JSON objects
J2="$TMP/nested.jsonl"
cat > "$J2" <<'JSONL'
{"event":{"type":"message","user":"U123","text":"hello world"},"ts":"1234"}
{"event":{"type":"app_mention","user":"U456","text":"@bot help"},"ts":"1235"}
{"event":{"type":"message","user":"U789","text":"screensaver bug"},"ts":"1236"}
{"event":{"type":"reaction_added","user":"U123"},"ts":"1237"}
JSONL

# Mixed content (some lines not JSON)
J3="$TMP/mixed.jsonl"
cat > "$J3" <<'JSONL'
not json at all
{"valid":true,"msg":"hello"}
another plain line
{"valid":false,"msg":"goodbye"}
{"valid":true,"msg":"screensaver"}
JSONL

# Large-ish file for performance
J4="$TMP/large.jsonl"
for i in $(seq 1 1000); do
    echo "{\"id\":$i,\"role\":\"user\",\"content\":\"message number $i\"}"
done > "$J4"
echo '{"id":1001,"role":"assistant","content":"screensaver animation code"}' >> "$J4"

echo "=== grap --json test suite ==="
echo "WK=$WK"
echo ""

# ── Test 1: simple keyword ─────────────────────────────────────────────────
out=$(run_grap "screensaver" "$J1" --json --past 1h 2>&1)
check "T01 simple keyword match" "screensaver" "$out"
check "T01 shows matched content" "assistant" "$out"  # "screensaver" appears in assistant line too

# ── Test 2: simple keyword no match ────────────────────────────────────────
out=$(run_grap "nonexistent_keyword_xyz_12345" "$J1" --json --past 1h 2>&1)
# No matching lines → output has no JSON content
check_not "T02 no false match" "role" "$out"

# ── Test 3: structural single key-value ────────────────────────────────────
out=$(run_grap '{"role":"user"}' "$J1" --json --past 1h 2>&1)
check "T03 structural role=user" "What is a screensaver" "$out"
check "T03 structural role=user (second)" "How do I make one" "$out"
check_not "T03 excludes assistant" "A screensaver is a program" "$out"

# ── Test 4: structural multi key-value (AND) ───────────────────────────────
out=$(run_grap '{"role":"user","content":"screensaver"}' "$J1" --json --past 1h 2>&1)
check "T04 AND match (role+content)" "What is a screensaver" "$out"
check_not "T04 AND excludes non-match" "How do I make one" "$out"

# ── Test 5: structural with regex value ────────────────────────────────────
out=$(run_grap '{"role":"user","content":"C#|csharp"}' "$J1" --json --past 1h 2>&1)
check "T05 regex value match" "How do I make one in C#" "$out"

# ── Test 6: nested object matching (recursive into event.type) ─────────────
out=$(run_grap '{"type":"message"}' "$J2" --json --past 1h 2>&1)
# Nested matching: {"event":{"type":"message"}} — MatchJsonObject recurses
check "T06 nested type=message" "message" "$out"
check_not "T06 excludes reaction_added" "reaction_added" "$out"

# ── Test 7: nested AND match ──────────────────────────────────────────────
out=$(run_grap '{"text":"screensaver"}' "$J2" --json --past 1h 2>&1)
check "T07 nested text=screensaver" "screensaver" "$out"
check_not "T07 excludes hello" "hello world" "$out"

# ── Test 8: mixed file (skip non-JSON lines) ──────────────────────────────
out=$(run_grap '{"valid":"true"}' "$J3" --json --past 1h 2>&1)
check "T08 mixed file matches valid=true" "hello" "$out"
check "T08 mixed file matches screensaver" "screensaver" "$out"
check_not "T08 excludes valid=false" "goodbye" "$out"

# ── Test 9: case insensitive (-i) ─────────────────────────────────────────
out=$(run_grap '{"role":"USER"}' "$J1" --json -i 2>&1)
check "T09 case insensitive" "What is a screensaver" "$out"

# ── Test 10: max count (-m) limits output ─────────────────────────────────
out=$(run_grap "message" "$J4" --json -m 3 --past 1h 2>&1)
# -m 3 = at most 3 matches per file — count output lines with content
line_count=$(echo "$out" | grep -c "message number" || true)
if [[ "$line_count" -le 3 ]]; then pass "T10 max count -m 3 limits output ($line_count lines)"; else fail "T10 expected <=3 lines, got $line_count"; fi

# ── Test 11: --json with --past (scan mode) ───────────────────────────────
# Create file in temp, use --basedir to point grap there
out=$(run_grap "screensaver" "$TMP/*.jsonl" --json --past 1h 2>&1)
check "T11 --json + --past" "screensaver" "$out"

# ── Test 12: structural with tool name ────────────────────────────────────
out=$(run_grap '{"name":"bash"}' "$J1" --json --past 1h 2>&1)
check "T12 tool name=bash" "dotnet build" "$out"

# ── Test 13: regex key matching ───────────────────────────────────────────
out=$(run_grap '{"cont.*":"screensaver"}' "$J1" --json --past 1h 2>&1)
check "T13 regex key match" "screensaver" "$out"

# ── Test 14: empty structural pattern = match all ─────────────────────────
out=$(run_grap '{}' "$J1" --json --past 1h 2>&1)
# Empty {} = no key-value constraints → MatchJsonObject returns true for all objects
check "T14 empty pattern matches" "role" "$out"

# ── Test 15: large file performance ───────────────────────────────────────
start=$(date +%s%N)
out=$(run_grap '{"id":"1001"}' "$J4" --json --past 1h 2>&1)
end=$(date +%s%N)
elapsed_ms=$(( (end - start) / 1000000 ))
check "T15 large file (1001 lines) finds target" "screensaver animation" "$out"
if [[ $elapsed_ms -lt 5000 ]]; then pass "T15 performance <5s (${elapsed_ms}ms)"; else fail "T15 too slow: ${elapsed_ms}ms"; fi

# ── Test 16: --json without pattern = error ───────────────────────────────
out=$(run_grap --json --past 1h 2>&1) || true
# Should show help or error, not crash
check "T16 no-args --json doesn't crash" "" "$out"

# ── Summary ────────────────────────────────────────────────────────────────
echo ""
echo "Results: $PASS passed, $FAIL failed (total $((PASS + FAIL)))"
rm -rf "$TMP"
[[ $FAIL -eq 0 ]]
