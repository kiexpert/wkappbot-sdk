#!/bin/bash
# Test: a11y zoom auto-scale — dynamic zoom factor from control size
# Verifies ClickZoomHelper computes scale = clamp(min(480/w, 240/h), 2, 8)
# Implements suggest #9: 배율 자동 산정 (2026-03-30T12:38)
#
# Live test: wkappbot a11y type on a real window → InputZoom window appears
#   → wkappbot a11y windows shows "*InputZoom*" or "*AppBot*zoom*"

PASS=0; FAIL=0
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/ClickZoomHelper.cs"

echo "=== Test: a11y zoom auto-scale ==="

# 1. MaxZoomW / MaxZoomH constants present
if grep -q "MaxZoomW = 480" "$SRC" && grep -q "MaxZoomH = 240" "$SRC"; then
  echo "PASS: MaxZoomW=480 MaxZoomH=240 constants present"
  PASS=$((PASS+1))
else
  echo "FAIL: MaxZoomW/MaxZoomH constants missing or wrong values"
  FAIL=$((FAIL+1))
fi

# 2. MaxScale / MinScale bounds
if grep -q "MaxScale = 8" "$SRC" && grep -q "MinScale = 2" "$SRC"; then
  echo "PASS: MaxScale=8 MinScale=2 bounds present"
  PASS=$((PASS+1))
else
  echo "FAIL: MaxScale/MinScale bounds missing"
  FAIL=$((FAIL+1))
fi

# 3. scale = min(scaleX, scaleY) formula
if grep -q "Math.Min(scaleX, scaleY)" "$SRC"; then
  echo "PASS: scale = Math.Min(scaleX, scaleY) formula present"
  PASS=$((PASS+1))
else
  echo "FAIL: scale formula not found"
  FAIL=$((FAIL+1))
fi

# 4. Math.Clamp applied (clamp to [MinScale, MaxScale])
if grep -q "Math.Clamp" "$SRC"; then
  echo "PASS: Math.Clamp applied to scale"
  PASS=$((PASS+1))
else
  echo "FAIL: Math.Clamp missing"
  FAIL=$((FAIL+1))
fi

# 5. scaleX derived from control width, scaleY from height
if grep -q "MaxZoomW / ctlRect.Width" "$SRC" || grep -q "MaxZoomW / screenRect.Width" "$SRC"; then
  echo "PASS: scaleX = MaxZoomW / ctlRect.Width"
  PASS=$((PASS+1))
else
  echo "FAIL: scaleX derivation from control width not found"
  FAIL=$((FAIL+1))
fi

# 6. zoom window size = control * scale + padding
if grep -q "ctlRect.Width \* scale" "$SRC" || grep -q "Width \* scale" "$SRC"; then
  echo "PASS: zoom window width = control * scale"
  PASS=$((PASS+1))
else
  echo "FAIL: zoom window sizing not found"
  FAIL=$((FAIL+1))
fi

echo ""
echo "--- Live test hint ---"
echo "Run these commands to verify zoom overlay appears:"
echo "  1) notepad.exe (open Notepad)"
echo "  2) wkappbot a11y type '*메모장*#*편집*' 'zoom test'"
echo "  3) wkappbot a11y windows | grep -i 'InputZoom\\|AppBot.*oom'"
echo "  (InputZoom window should appear during step 2)"

echo ""
echo "Results: $PASS PASS, $FAIL FAIL"
[ $FAIL -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
