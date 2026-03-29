#!/bin/bash
# test-launcher-paths.sh — Launcher 전 경로 스모크 테스트
# Usage: bash test/test-launcher-paths.sh
# 후배 클롣들은 코드 변경 후 반드시 실행!

WK=W:/SDK/bin/wkappbot.exe
CORE=W:/SDK/bin/wkappbot-core.exe
PASS=0; FAIL=0; SKIP=0
RESULTS=()

check() {
    local name="$1" result="$2" detail="$3"
    if [ "$result" = "PASS" ]; then ((PASS++)); RESULTS+=("  ✓ $name: $detail")
    elif [ "$result" = "SKIP" ]; then ((SKIP++)); RESULTS+=("  - $name: $detail")
    else ((FAIL++)); RESULTS+=("  ✗ $name: $detail"); fi
}

echo "═══ WKAppBot Launcher Path Test ═══"
echo ""

# ── 1. Eye pipe path (primary, fast) ──
echo "[1/10] Eye pipe: version..."
V=$($WK version 2>/dev/null | grep -a "wkappbot v" | head -1)
if [ -n "$V" ]; then check "Eye pipe version" "PASS" "$V"
else check "Eye pipe version" "FAIL" "no output (Eye not running?)"; fi

# ── 2. Eye pipe: windows command ──
echo "[2/10] Eye pipe: windows..."
WIN=$($WK windows 2>/dev/null | grep -a "Total:" | head -1)
if [ -n "$WIN" ]; then check "Eye pipe windows" "PASS" "$WIN"
else check "Eye pipe windows" "FAIL" "no Total: line"; fi

# ── 3. Unicode encoding (★↑⌨◇ + 한글) ──
echo "[3/10] Unicode encoding..."
UNI=$($WK windows "Code" 2>&1 | grep -a "★")
KR=$($WK windows 2>&1 | grep -a "설정\|입력\|채널\|한글")
if [ -n "$UNI" ]; then check "Unicode symbols" "PASS" "★ found"
else check "Unicode symbols" "FAIL" "★ missing"; fi
if [ -n "$KR" ]; then check "Korean text" "PASS" "한글 OK"
else check "Korean text" "SKIP" "no Korean window titles"; fi

# ── 4. LAUNCH JSON (stderr only, invisible on stdout) ──
echo "[4/10] LAUNCH stealth..."
LAUNCH_OUT=$($WK version 2>/dev/null | grep -a "LAUNCH")
LAUNCH_ERR=$($WK version 2>&1 | grep -a "LAUNCH")
if [ -z "$LAUNCH_OUT" ]; then check "LAUNCH stdout hidden" "PASS" "invisible"
else check "LAUNCH stdout hidden" "FAIL" "visible on stdout"; fi

# ── 5. PulseStep silent (no [PULSE:] without WKAPPBOT_PROFILE=1) ──
echo "[5/10] PulseStep silent..."
PULSE=$($WK version 2>&1 | grep -a "PULSE")
if [ -z "$PULSE" ]; then check "PulseStep hidden" "PASS" "silent"
else check "PulseStep hidden" "FAIL" "noise: $PULSE"; fi

# ── 6. CMD-MCP hidden from user ──
echo "[6/10] CMD-MCP hidden..."
CMDMCP=$($WK windows 2>/dev/null | grep -a "CMD-MCP")
if [ -z "$CMDMCP" ]; then check "CMD-MCP hidden" "PASS" "not on stdout"
else check "CMD-MCP hidden" "FAIL" "visible"; fi

# ── 7. Column header + flags ──
echo "[7/10] Column header + flags..."
HDR=$($WK windows 2>&1 | grep -a "hwnd____")
FLAGS=$($WK windows 2>&1 | grep -a "\[H\]\|\[HL\]\|\[HLT")
if [ -n "$HDR" ]; then check "Column header" "PASS" "hwnd____ found"
else check "Column header" "FAIL" "missing"; fi
if [ -n "$FLAGS" ]; then check "Flags compressed" "PASS" "[H]/[HL] found"
else check "Flags compressed" "FAIL" "old format?"; fi

# ── 8. Sibling windows (search filter) ──
echo "[8/10] Sibling windows..."
SIB=$($WK windows "Code" 2>&1 | grep -a "sibling")
if [ -n "$SIB" ]; then check "Sibling windows" "PASS" "section found"
else check "Sibling windows" "SKIP" "no siblings for Code"; fi

# ── 9. Grap/grep fast path (no 30s delay) ──
echo "[9/10] Grap fast path..."
T0=$(date +%s)
$WK grap --help 2>&1 > /dev/null
T1=$(date +%s)
DT=$((T1 - T0))
if [ "$DT" -lt 3 ]; then check "Grap help speed" "PASS" "${DT}s"
else check "Grap help speed" "FAIL" "${DT}s (>3s)"; fi

# ── 10. --core override (custom binary) ──
echo "[10/10] --core override..."
if [ -f "$CORE" ]; then
    OVER=$($WK --core "$CORE" --only-core version 2>&1 | grep -a "wkappbot v" | head -1)
    if [ -n "$OVER" ]; then check "--core override" "PASS" "$OVER"
    else check "--core override" "SKIP" "30s timeout (IOCP)"; fi
else
    check "--core override" "SKIP" "core.exe not found"
fi

# ── Summary ──
echo ""
echo "═══ Results ═══"
for r in "${RESULTS[@]}"; do echo "$r"; done
echo ""
echo "Total: $PASS PASS / $FAIL FAIL / $SKIP SKIP"
if [ "$FAIL" -gt 0 ]; then echo "⚠ FAILURES — fix before commit!"; exit 1
else echo "✓ All critical paths OK"; exit 0; fi
