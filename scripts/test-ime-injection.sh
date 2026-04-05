#!/bin/bash
# IME injection POC verification
# Tests: TypeViaIme method exists, P/Invoke declarations, integration points

WKAPPBOT="wkappbot"
PASS=0; FAIL=0

check() {
  if [ $1 -eq 0 ]; then echo "  PASS: $2"; ((PASS++)); else echo "  FAIL: $2"; ((FAIL++)); fi
}

echo "=== IME Injection POC Verification ==="

# P/Invoke declarations
echo "[TEST] IMM32 P/Invoke"
grep -q "ImmNotifyIME" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Native/NativeMethods.cs
check $? "ImmNotifyIME declared"
grep -q "ImmSetCompositionStringW" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Native/NativeMethods.cs
check $? "ImmSetCompositionStringW declared"
grep -q "ImmGetContext" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Native/NativeMethods.cs
check $? "ImmGetContext declared"

# TypeViaIme method
echo "[TEST] TypeViaIme POC"
grep -q "public static bool TypeViaIme" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Input/KeyboardInput.cs
check $? "TypeViaIme method exists"
grep -q "AttachThreadInput" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Input/KeyboardInput.cs
check $? "AttachThreadInput for focusless access"
grep -q "CPS_COMPLETE" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Input/KeyboardInput.cs
check $? "CPS_COMPLETE composition finalize"
grep -q "NI_COMPOSITIONSTR" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Input/KeyboardInput.cs
check $? "NI_COMPOSITIONSTR constant"

# Build verification
echo "[TEST] Build"
test -f W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/bin/Release/net8.0-windows10.0.22621.0/win-x64/wkappbot-core.dll
check $? "wkappbot-core.dll built"

# Existing IME infrastructure
echo "[TEST] Existing IME infra"
grep -q "ImeComposing" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Input/KeyboardInput.cs
check $? "FocusSnapshot captures IME composing state"
grep -q "ImmSetConversionStatus" W:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Input/KeyboardInput.cs
check $? "IME conversion status restore"

# Command verification
echo "[CMD] wkappbot a11y type --help"
timeout 10 $WKAPPBOT a11y type --help 2>&1 | head -3

echo ""
echo "=== Result: $PASS passed, $FAIL failed ==="
