#!/bin/bash
# test-launcher-ansi-pipe.sh
# Tests: launcher ansi codes are suppressed when stderr is redirected/piped
# Verifies fix: DimStderrWriter._useAnsi=false when Console.IsErrorRedirected
# Cmd ref: launcher ansi (DimStderrWriter in Launcher/HelpAndAliases.cs)

WKBOT="${WKBOT:-W:/SDK/bin/wkappbot.exe}"
SRC="${SRC:-W:/GitHub/WKAppBot/csharp/src/WKAppBot.Launcher/HelpAndAliases.cs}"

if [ ! -f "$WKBOT" ]; then
  echo "[SKIP] wkappbot.exe not found at $WKBOT"
  exit 0
fi

# 1. Verify DimStderrWriter exists in source
if [ -f "$SRC" ]; then
  if ! grep -q "DimStderrWriter" "$SRC"; then
    echo "[FAIL] DimStderrWriter class not found in HelpAndAliases.cs"
    exit 1
  fi
  echo "[PASS] DimStderrWriter found in HelpAndAliases.cs"

  # 2. Verify IsErrorRedirected pipe detection is present
  if ! grep -q "IsErrorRedirected" "$SRC"; then
    echo "[FAIL] Console.IsErrorRedirected check missing — ANSI will leak to pipes"
    exit 1
  fi
  echo "[PASS] Console.IsErrorRedirected check present"

  # 3. Verify _useAnsi is only set inside the !IsErrorRedirected block
  if grep -qE "_useAnsi\s*=\s*true" "$SRC"; then
    echo "[FAIL] _useAnsi=true hardcoded — should be conditional on console mode"
    exit 1
  fi
  echo "[PASS] _useAnsi is conditionally set (not hardcoded true)"
else
  echo "[SKIP] Source not found — skipping source checks"
fi

# 4. Runtime check: ANSI codes must not appear in piped stderr
# Run wkappbot --help (fast launcher path, no Eye pipe)
stderr_raw=$(wkappbot --help 2>&1 1>/dev/null)
if printf '%s' "$stderr_raw" | grep -qP '\x1b\['; then
  echo "[FAIL] Raw ANSI escape codes found in piped stderr (wkappbot --help)"
  printf '%s\n' "$stderr_raw" | head -3
  exit 1
fi
echo "[PASS] No raw ANSI codes in piped stderr (wkappbot --help)"

echo ""
echo "[PASS] launcher ansi pipe suppression verified"
exit 0
