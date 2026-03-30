#!/bin/bash
# Test: a11y zoom live capture — real-time WriteableBitmap rendering for occluded windows
# Verifies InputZoomOverlay has fast raw-pixel path (no codec encode/decode)
# Implements suggest #8: 돋보기 실시간 뷰 (2026-03-30T12:37)
#
# Live test: open any window behind another → a11y type → zoom shows real-time view
#   even when target is fully occluded (PrintWindow ignores Z-order)

PASS=0; FAIL=0
OVERLAY="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/InputZoomOverlay.cs"
ZOOM="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/ClickZoomHelper.cs"
INPUT="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/InputCommand.cs"

echo "=== Test: a11y zoom live capture (WriteableBitmap fast path) ==="

# 1. WriteableBitmap field present
if grep -q "WriteableBitmap" "$OVERLAY"; then
  echo "PASS: WriteableBitmap fast-path field present"
  PASS=$((PASS+1))
else
  echo "FAIL: WriteableBitmap not found in InputZoomOverlay"
  FAIL=$((FAIL+1))
fi

# 2. UpdateBitmapRaw (no codec) method present
if grep -q "UpdateBitmapRaw" "$OVERLAY"; then
  echo "PASS: UpdateBitmapRaw (codec-free pixel copy) method present"
  PASS=$((PASS+1))
else
  echo "FAIL: UpdateBitmapRaw missing"
  FAIL=$((FAIL+1))
fi

# 3. WritePixels direct copy (no PNG/BMP encode)
if grep -q "WritePixels" "$OVERLAY"; then
  echo "PASS: WritePixels direct pixel copy (no encode round-trip)"
  PASS=$((PASS+1))
else
  echo "FAIL: WritePixels missing"
  FAIL=$((FAIL+1))
fi

# 4. StartLiveCaptureFast with 150ms polling
if grep -q "StartLiveCaptureFast" "$OVERLAY"; then
  echo "PASS: StartLiveCaptureFast polling loop present"
  PASS=$((PASS+1))
else
  echo "FAIL: StartLiveCaptureFast missing"
  FAIL=$((FAIL+1))
fi

# 5. intervalMs = 150 default (real-time feel)
if grep -q "intervalMs = 150" "$OVERLAY"; then
  echo "PASS: default interval 150ms (real-time)"
  PASS=$((PASS+1))
else
  echo "FAIL: 150ms default interval missing"
  FAIL=$((FAIL+1))
fi

# 6. CaptureControlRawFast in InputCommand (PrintWindow for occluded + CopyFromScreen for fg)
if grep -q "CaptureControlRawFast" "$INPUT"; then
  echo "PASS: CaptureControlRawFast in InputCommand (PrintWindow + CopyFromScreen)"
  PASS=$((PASS+1))
else
  echo "FAIL: CaptureControlRawFast missing from InputCommand"
  FAIL=$((FAIL+1))
fi

# 7. LockBits / Marshal.Copy raw BGRA (no intermediate codec)
if grep -q "LockBits" "$INPUT" || grep -q "Marshal.Copy" "$INPUT"; then
  echo "PASS: raw BGRA pixel copy via LockBits/Marshal.Copy"
  PASS=$((PASS+1))
else
  echo "FAIL: raw BGRA copy not found"
  FAIL=$((FAIL+1))
fi

# 8. PrintWindow fallback for background (occluded) windows
if grep -q "PrintWindow" "$ZOOM" || grep -q "PrintWindow" "$INPUT"; then
  echo "PASS: PrintWindow used for background/occluded window capture"
  PASS=$((PASS+1))
else
  echo "FAIL: PrintWindow fallback missing"
  FAIL=$((FAIL+1))
fi

echo ""
echo "--- Live test hint (occluded window) ---"
echo "  1) Open Notepad, then cover it with another window"
echo "  2) wkappbot a11y type '*메모장*#*편집*' 'live test'"
echo "  3) Zoom overlay should show REAL-TIME content of Notepad"
echo "     even though it's behind another window (PrintWindow ignores Z-order)"
echo "  4) wkappbot a11y windows | grep -i 'InputZoom'"
echo "     → confirms overlay window is live"

echo ""
echo "Results: $PASS PASS, $FAIL FAIL"
[ $FAIL -eq 0 ] && echo "ALL TESTS PASSED" && exit 0
exit 1
