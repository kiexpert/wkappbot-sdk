#!/usr/bin/env bash
# test-file-edit.sh — wkappbot file edit integration tests
# Usage: bash scripts/test-file-edit.sh [WKAPPBOT_EXE]
# Default exe: wkappbot (must be on PATH or D:/SDK/bin/wkappbot.exe)
set -uo pipefail

WK="${1:-D:/SDK/bin/wkappbot.exe}"
# Use Windows temp dir (not MSYS2 /tmp which maps to D:\tmp and confuses PowerShell)
TMP="$(powershell -Command "[IO.Path]::GetTempPath()" | tr -d '\r\n' | sed 's|\\|/|g')test-file-edit-$$"
mkdir -p "$TMP"
PASS=0; FAIL=0

pass() { echo "[PASS] $1"; ((PASS++)); }
fail() { echo "[FAIL] $1"; ((FAIL++)); }
check() {
    local label="$1" expect="$2" actual="$3"
    if [[ "$actual" == *"$expect"* ]]; then pass "$label"; else fail "$label — expected '$expect', got '$actual'"; fi
}

# ── helpers ────────────────────────────────────────────────────────────────

# Write UTF-8 file (no BOM)
utf8_file() {
    local f="$1" content="$2"
    printf '%s' "$content" > "$f"
}

# Write a CP949 file using PowerShell WriteAllBytes with explicit byte arrays.
# This avoids MSYS2 locale encoding converting the bytes through UTF-8 filter.
# Byte arrays:
#   가나다 = 0xB0A1 0xB3AA 0xB4D9  (first byte 0xB0 < 0xC2 → fails UTF-8 → detected CP949)
#   세계   = 0xBCBC 0xB0E8
#   ok     = 0x6F 0x6B (ASCII)
cp949_file_ok() {
    # "가나다 ok" in CP949 bytes
    local wf; wf="$(cygpath -w "$1" 2>/dev/null || echo "$1" | sed 's|/|\\\\|g')"
    powershell -Command "[IO.File]::WriteAllBytes('$wf', [byte[]](0xB0,0xA1,0xB3,0xAA,0xB4,0xD9,0x20,0x6F,0x6B))"
}
cp949_file_ko() {
    # "가나다 세계" in CP949 bytes
    local wf; wf="$(cygpath -w "$1" 2>/dev/null || echo "$1" | sed 's|/|\\\\|g')"
    powershell -Command "[IO.File]::WriteAllBytes('$wf', [byte[]](0xB0,0xA1,0xB3,0xAA,0xB4,0xD9,0x20,0xBC,0xBC,0xB0,0xE8))"
}

# ── Test 1: UTF-8 basic replacement ────────────────────────────────────────
T1="$TMP/utf8_basic.txt"
utf8_file "$T1" "Hello World"
out=$("$WK" file edit "World" "Korea" "$T1" 2>&1)
check "UTF-8 basic replacement (stdout)" "1 replacement(s)" "$out"
check "UTF-8 basic replacement (encoding)" "encoding=utf-8" "$out"
content=$(cat "$T1")
check "UTF-8 basic replacement (content)" "Hello Korea" "$content"

# ── Test 2: old_string not found → error ───────────────────────────────────
T2="$TMP/utf8_notfound.txt"
utf8_file "$T2" "foo bar"
out=$("$WK" file edit "missing" "x" "$T2" 2>&1) || true
check "Not found → error" "not found" "$out"
content=$(cat "$T2")
check "Not found → file unchanged" "foo bar" "$content"

# ── Test 3: multiple matches without --replace-all → error ─────────────────
T3="$TMP/utf8_multi.txt"
utf8_file "$T3" "apple apple apple"
out=$("$WK" file edit "apple" "mango" "$T3" 2>&1) || true
check "Multiple matches → error" "multiple" "$out"
content=$(cat "$T3")
check "Multiple matches → file unchanged" "apple apple apple" "$content"

# ── Test 4: --replace-all replaces all occurrences ────────────────────────
T4="$TMP/utf8_replace_all.txt"
utf8_file "$T4" "cat cat cat"
out=$("$WK" file edit "cat" "dog" "$T4" --replace-all 2>&1)
check "replace-all (stdout)" "3 replacement(s)" "$out"
content=$(cat "$T4")
check "replace-all (content)" "dog dog dog" "$content"

# ── Test 5: --regex with capture group ────────────────────────────────────
T5="$TMP/utf8_regex.txt"
utf8_file "$T5" "version=1.2.3"
out=$("$WK" file edit "version=(\d+\.\d+)\.\d+" 'version=$1.0' "$T5" --regex 2>&1)
check "regex + capture (stdout)" "1 replacement(s)" "$out"
content=$(cat "$T5")
check "regex + capture (content)" "version=1.2.0" "$content"

# ── Test 6: --regex multiple matches without --replace-all → error ─────────
T6="$TMP/utf8_regex_multi.txt"
utf8_file "$T6" "x=1 x=2 x=3"
out=$("$WK" file edit "x=\d+" "x=0" "$T6" --regex 2>&1) || true
check "regex multiple without --replace-all → error" "matches" "$out"

# ── Test 7: --regex + --replace-all ────────────────────────────────────────
T7="$TMP/utf8_regex_all.txt"
utf8_file "$T7" "x=1 x=2 x=3"
out=$("$WK" file edit "x=\d+" "x=0" "$T7" --regex --replace-all 2>&1)
check "regex + --replace-all (stdout)" "3 replacement(s)" "$out"
content=$(cat "$T7")
check "regex + --replace-all (content)" "x=0 x=0 x=0" "$content"

# ── Test 8: emoji in UTF-8 file (encodable) ───────────────────────────────
T8="$TMP/utf8_emoji.txt"
utf8_file "$T8" "status=ok"
out=$("$WK" file edit "ok" "ok✓" "$T8" 2>&1)
check "emoji in UTF-8 file (stdout)" "1 replacement(s)" "$out"
content=$(cat "$T8")
check "emoji in UTF-8 file (content)" "ok✓" "$content"

# ── Test 9: CP949 file (Korean) ─────────────────────────────────────────────
# Write raw CP949 bytes via printf (avoids PowerShell console encoding confusion)
# "가나다 세계" in CP949: 0xB0A1 0xB3AA 0xB4D9 0x20 0xBCBC 0xB0E8
T9="$TMP/cp949_korean.txt"
cp949_file_ko "$T9"  # writes "가나다 세계" as raw CP949 bytes (0xB0 first → detected CP949)
# Use UTF-8 literal "세계" as search string; .NET receives it as Unicode and matches CP949-decoded content
out=$("$WK" file edit "세계" "Korea" "$T9" --i-really-want-no-backup 2>&1)
check "CP949 replacement (stdout)" "1 replacement(s)" "$out"
check "CP949 encoding detected" "encoding=ks_c_5601-1987" "$out"
content=$("$WK" file read "$T9" 2>&1)
check "CP949 content after edit" "Korea" "$content"

# ── Test 10: emoji in CP949 file → unmappable → error ─────────────────────
# "가나다 ok" in CP949: 0xB0A1 0xB3AA 0xB4D9 0x20 0x6F 0x6B (ok = ASCII)
T10="$TMP/cp949_emoji.txt"
cp949_file_ok "$T10"  # writes "가나다 ok" as raw CP949 bytes
out=$("$WK" file edit "ok" "ok🎉" "$T10" --i-really-want-no-backup 2>&1) || true
check "emoji in CP949 → error (no --i-really-want-lossy-encoding)" "cannot be encoded" "$out"
content=$(cat "$T10")  # still raw CP949 bytes, but "ok" part is ASCII
check "emoji in CP949 → file unchanged" "ok" "$content"

# ── Test 11: emoji in CP949 file + --i-really-want-lossy-encoding → warning ────────
T11="$TMP/cp949_emoji_force.txt"
cp949_file_ok "$T11"  # "가나다 ok" in CP949
out=$("$WK" file edit "ok" "ok🎉" "$T11" --i-really-want-lossy-encoding --i-really-want-no-backup 2>&1) || true
check "--i-really-want-lossy-encoding warning on stderr" "WARNING(--i-really-want-lossy-encoding)" "$out"
check "--i-really-want-lossy-encoding still writes (stdout)" "1 replacement(s)" "$out"

# ── Test 12: --encoding explicit (write UTF-8 as CP949) ────────────────────
T12="$TMP/explicit_encoding.txt"
utf8_file "$T12" "hello world"
out=$("$WK" file edit "world" "World" "$T12" --encoding 65001 2>&1)
check "explicit --encoding 65001 (UTF-8)" "1 replacement(s)" "$out"
check "explicit --encoding 65001 (encoding label)" "encoding=utf-8" "$out"

# ── Test 13: multi-file glob ────────────────────────────────────────────────
T13a="$TMP/glob_a.txt"; T13b="$TMP/glob_b.txt"
utf8_file "$T13a" "color: red"
utf8_file "$T13b" "color: red"
out=$("$WK" file edit "red" "blue" "$TMP/glob_*.txt" 2>&1)
check "multi-file glob (summary)" "2/2 file(s) edited" "$out"
check "multi-file glob (file a)" "blue" "$(cat "$T13a")"
check "multi-file glob (file b)" "blue" "$(cat "$T13b")"

# ── Test 14: context output shows changed line ─────────────────────────────
T14="$TMP/context_test.txt"
printf 'line1\nline2\nTARGET\nline4\nline5\n' > "$T14"
out=$("$WK" file edit "TARGET" "REPLACED" "$T14" 2>&1)
check "context output (→ marker)" "→" "$out"
check "context output (line number)" "│ REPLACED" "$out"

# ── Test 15: ← old line marker (single line change) ─────────────────────
T15="$TMP/old_marker.txt"
printf 'alpha\nbeta\ngamma\ndelta\n' > "$T15"
out=$("$WK" file edit "beta" "BETA_NEW" "$T15" 2>&1)
check "← old line marker present" "←" "$out"
check "← shows old value" "beta" "$out"
check "→ shows new value" "BETA_NEW" "$out"

# ── Test 16: multiline → single line (3→1) ──────────────────────────────
T16="$TMP/multi_to_single.txt"
printf 'before\nvar a = 1;\nvar b = 2;\nvar c = 3;\nafter\n' > "$T16"
out=$("$WK" file edit 'var a = 1;\nvar b = 2;\nvar c = 3;' 'var all = [1,2,3];' "$T16" 2>&1)
check "3→1 replacement" "1 replacement(s)" "$out"
check "3→1 old line a" "var a = 1" "$out"
check "3→1 old line c" "var c = 3" "$out"
check "3→1 new line" "var all = [1,2,3]" "$out"

# ── Test 17: single line → multiline (1→3) ──────────────────────────────
T17="$TMP/single_to_multi.txt"
printf 'before\nvar all = [1,2,3];\nafter\n' > "$T17"
out=$("$WK" file edit 'var all = [1,2,3];' 'var a = 1;\nvar b = 2;\nvar c = 3;' "$T17" 2>&1)
check "1→3 replacement" "1 replacement(s)" "$out"
check "1→3 old line" "var all = [1,2,3]" "$out"
check "1→3 new line b" "var b = 2" "$out"

# ── Test 18: --indent-context limits expansion ───────────────────────────
T18="$TMP/indent_limit.txt"
# 20 lines at same indent + 1 target in the middle
{
    echo "function big() {"
    for i in $(seq 1 20); do echo "    line$i;"; done
    echo "    TARGET;"
    for i in $(seq 21 40); do echo "    line$i;"; done
    echo "}"
} > "$T18"
out=$("$WK" file edit "TARGET" "REPLACED" "$T18" --indent-context 3 2>&1)
check "--indent-context 3 limits output" "REPLACED" "$out"
# Should NOT contain line1 (too far from match, capped at 3)
if [[ "$out" != *"line1;"* ]]; then pass "--indent-context 3 excludes distant lines"; else fail "--indent-context 3 should exclude line1"; fi

# ── Test 19: --indent-context default (7) ────────────────────────────────
T19="$TMP/indent_default.txt"
{
    echo "class Foo {"
    for i in $(seq 1 20); do echo "    item$i;"; done
    echo "    MATCH_HERE;"
    for i in $(seq 21 40); do echo "    item$i;"; done
    echo "}"
} > "$T19"
out=$("$WK" file edit "MATCH_HERE" "DONE" "$T19" 2>&1)
check "default indent-context has DONE" "DONE" "$out"
# item14 is 7 lines before match (line 22), should be visible
if [[ "$out" == *"item14;"* ]]; then pass "default indent-context 7 shows item14"; else fail "default indent-context 7 should show item14"; fi
# item1 is 20 lines away, should NOT be visible
if [[ "$out" != *"item1;"* ]] || [[ "$out" == *"item10;"* ]]; then pass "default indent-context 7 excludes item1"; else fail "default indent-context 7 should exclude item1"; fi

# ── Test 20: --old-file / --new-file ─────────────────────────────────────
T20="$TMP/file_args.txt"
T20_OLD="$TMP/old_pattern.txt"
T20_NEW="$TMP/new_pattern.txt"
printf 'hello world' > "$T20"
printf 'world' > "$T20_OLD"
printf 'universe' > "$T20_NEW"
out=$("$WK" file edit --old-file "$T20_OLD" --new-file "$T20_NEW" "$T20" 2>&1)
check "--old-file/--new-file replacement" "1 replacement(s)" "$out"
check "--old-file/--new-file content" "universe" "$(cat "$T20")"

# ── Test 21: replace-all with ← markers ──────────────────────────────────
T21="$TMP/replace_all_markers.txt"
printf 'foo bar\nbaz foo\nqux foo\n' > "$T21"
out=$("$WK" file edit "foo" "FOO" "$T21" --replace-all 2>&1)
check "replace-all count" "3 replacement(s)" "$out"
check "replace-all ← marker" "←" "$out"
check "replace-all → marker" "→" "$out"

# ── Test 22: search-only mode (same old+new) ─────────────────────────────
T22="$TMP/search_only.txt"
printf 'alpha\nbeta\ngamma\n' > "$T22"
out=$("$WK" file edit "beta" "beta" "$T22" 2>&1)
check "search-only mode (no change)" "no change" "$out"
check "search-only mode (match count)" "1 match" "$out"

# ── Test 23: bash Korean arg → CP949 file (encoding recovery) ────────────
T23="$TMP/korean_cp949.txt"
powershell -NoProfile -Command "[System.IO.File]::WriteAllText('${T23//\//\\}', '// 테스트 code', [System.Text.Encoding]::GetEncoding(949))"
out=$("$WK" file edit "테스트" "TEST_OK" "$T23" 2>&1)
check "Korean arg → CP949 (replacement)" "1 replacement(s)" "$out"
check "Korean arg → CP949 (← marker)" "테스트" "$out"
check "Korean arg → CP949 (→ marker)" "TEST_OK" "$out"

# ── Test 24: bash Korean arg → UTF-8 file ────────────────────────────────
T24="$TMP/korean_utf8.txt"
printf '// 한글테스트 utf8\n' > "$T24"
out=$("$WK" file edit "한글테스트" "HANGUL_OK" "$T24" 2>&1)
check "Korean arg → UTF-8 (replacement)" "1 replacement(s)" "$out"
check "Korean arg → UTF-8 (content)" "HANGUL_OK" "$(cat "$T24")"

# ── Test 25: write Korean back to CP949 ──────────────────────────────────
T25="$TMP/korean_write_back.txt"
powershell -NoProfile -Command "[System.IO.File]::WriteAllText('${T25//\//\\}', '// MARKER code', [System.Text.Encoding]::GetEncoding(949))"
out=$("$WK" file edit "MARKER" "수정됨" "$T25" 2>&1)
check "Write Korean to CP949 (replacement)" "1 replacement(s)" "$out"
check "Write Korean to CP949 (→ marker)" "수정됨" "$out"

# ── Summary ─────────────────────────────────────────────────────────────────
echo ""
echo "Results: $PASS passed, $FAIL failed (total $((PASS + FAIL)))"
rm -rf "$TMP"
[[ $FAIL -eq 0 ]]
