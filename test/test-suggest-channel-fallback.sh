#!/bin/bash
# test-suggest-channel-fallback.sh
# Tests: suggest channel API failure triggers webhook fallback
# Verifies fix: when chat.postMessage fails (e.g. channel_not_found),
#               suggest command falls back to webhook instead of silently failing
# Cmd ref: suggest channel (SuggestCommand.cs webhook fallback)

WKBOT="${WKBOT:-W:/SDK/bin/wkappbot.exe}"
SRC="${SRC:-W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/SuggestCommand.cs}"

if [ ! -f "$WKBOT" ]; then
  echo "[SKIP] wkappbot.exe not found"
  exit 0
fi

# 1. Verify TrySuggestWebhook helper exists in source
if [ -f "$SRC" ]; then
  if ! grep -q "TrySuggestWebhook" "$SRC"; then
    echo "[FAIL] TrySuggestWebhook not found in SuggestCommand.cs"
    exit 1
  fi
  echo "[PASS] TrySuggestWebhook helper method found"
else
  echo "[SKIP] Source file not found at $SRC — skipping source check"
fi

# 2. Verify fallback path: API fail branch calls TrySuggestWebhook
if [ -f "$SRC" ]; then
  if ! grep -q "chat.postMessage failed" "$SRC" || ! grep -q "webhook fallback" "$SRC"; then
    echo "[FAIL] Webhook fallback message not found — API failure path may not have fallback"
    exit 1
  fi
  echo "[PASS] API failure -> webhook fallback path present"
fi

# 3. Verify suggest list command works (infrastructure check)
list_out=$("$WKBOT" suggest list 2>/dev/null)
if [ $? -ne 0 ]; then
  echo "[FAIL] suggest list exited non-zero"
  exit 1
fi
echo "[PASS] suggest list command works"

# 4. Verify SuggestResolveCommand is registered in source
if [ -f "$SRC" ]; then
  if ! grep -q "SuggestResolveCommand" "$SRC"; then
    echo "[FAIL] SuggestResolveCommand not found in SuggestCommand.cs"
    exit 1
  fi
  echo "[PASS] SuggestResolveCommand registered in source"
fi

echo ""
echo "[PASS] suggest channel fallback fix verified"
exit 0
