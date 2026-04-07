#!/usr/bin/env bash
# test-a11y-hack-overlay-transparent.sh — verify hack overlay is no longer opaque black
# Fix: Window.Opacity removed (AllowsTransparency + Opacity<1 = opaque black bug),
#      transparency moved to Canvas.Opacity instead.
PASS=0; FAIL=0

check() {
    local label="$1"; local cmd="$2"
    if eval "$cmd" > /dev/null 2>&1; then
        echo "[PASS] $label"; PASS=$((PASS+1))
    else
        echo "[FAIL] $label"; FAIL=$((FAIL+1))
    fi
}

SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/A11yHackOverlay.cs"

# Fix: Window-level Opacity assignment must NOT exist (causes opaque black with AllowsTransparency)
# Old bad code: standalone `Opacity = 0.5;`
# New code: `Canvas { ..., Opacity = 0.6, ... }` (canvas level only)
check "Window.Opacity NOT set as standalone statement" \
    "! grep -Pq '^\s+Opacity = 0\.' '$SRC'"

# Fix: Canvas.Opacity is set instead
check "Canvas.Opacity set for transparency" \
    "grep -q 'Canvas.*Opacity\|Opacity.*Canvas' '$SRC'"

# Ensure AllowsTransparency is still present (required for per-element transparency)
check "AllowsTransparency still present" \
    "grep -q 'AllowsTransparency' '$SRC'"

# Comment explaining the fix must be present
check "fix comment about AllowsTransparency+Opacity bug" \
    "grep -q 'AllowsTransparency.*Opacity\|Opacity.*AllowsTransparency' '$SRC'"

# Functional: run hack on an always-present window (Desktop) to produce [CMD] entry
# hack exits non-zero if no UIA tree, but [CMD] entry is emitted — that's enough
check "a11y hack runs without crash (no unhandled exception)" \
    "! \"W:/SDK/bin/wkappbot.exe\" a11y hack \"*\" 2>&1 | grep -qa 'Unhandled exception\|FileNotFoundException'"

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ]
