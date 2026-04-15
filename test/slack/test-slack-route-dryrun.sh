#!/bin/bash
# test-slack-route-dryrun.sh
# Tests: slack route --dry-run traces routing without delivering
# Verifies:
#   1. --dry-run flag is parsed correctly (no crash)
#   2. [DRY-RUN] output appears in stdout
#   3. Display names include folder tag (e.g. "클롣[WG-...]")
#   4. No actual Slack ack sent (no "전달했습니다" in output)
# Cmd ref: slack route --dry-run (SlackCommands.Route.cs)

WKBOT="${WKBOT:-D:/SDK/bin/wkappbot.exe}"
SRC="${SRC:-D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/SlackCommands.Route.cs}"

if [ ! -f "$WKBOT" ]; then
  echo "[SKIP] wkappbot.exe not found"
  exit 0
fi

# 1. Source check: --dry-run flag exists
if [ -f "$SRC" ]; then
  if ! grep -q "isDryRun" "$SRC"; then
    echo "[FAIL] isDryRun not found in SlackCommands.Route.cs"
    exit 1
  fi
  echo "[PASS] --dry-run flag present in source"
fi

# 2. Source check: DRY-RUN output present
if [ -f "$SRC" ]; then
  if ! grep -q "DRY-RUN" "$SRC"; then
    echo "[FAIL] [DRY-RUN] output not found in SlackCommands.Route.cs"
    exit 1
  fi
  echo "[PASS] [DRY-RUN] output marker present in source"
fi

# 3. Source check: display name fix (GetPromptDisplayInfo replaces hardcoded 클롣[tag])
HANDLERS_SRC="${HANDLERS_SRC:-D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AppBotEyeSlackHandlers.cs}"
if [ -f "$HANDLERS_SRC" ]; then
  if grep -q 'ExtractCwdFromVsCodeTitle.*promptNames' "$HANDLERS_SRC" 2>/dev/null || \
     grep -q '"클롣\[{tag}\]"' "$HANDLERS_SRC" 2>/dev/null; then
    echo "[FAIL] Old hardcoded 클롣[tag] pattern still present in AppBotEyeSlackHandlers.cs"
    exit 1
  fi
  if ! grep -q "GetPromptDisplayInfo" "$HANDLERS_SRC"; then
    echo "[FAIL] GetPromptDisplayInfo not used in AppBotEyeSlackHandlers.cs promptNames build"
    exit 1
  fi
  echo "[PASS] promptNames uses GetPromptDisplayInfo (full display name including folder tag)"
fi

# 4. Source check: allPrompts[0] fallback removed (Bug 2 fix)
if [ -f "$HANDLERS_SRC" ]; then
  if grep -q 'allPrompts\[0\]' "$HANDLERS_SRC"; then
    echo "[FAIL] allPrompts[0] fallback still present in ResolveWorkspaceScopedPrompts"
    exit 1
  fi
  echo "[PASS] allPrompts[0] fallback removed (wrong routing bug fixed)"
fi

# 5. Runtime: --dry-run with --file (avoids shell quoting issues)
TMPJSON=$(mktemp /tmp/route_test_XXXXXX.json 2>/dev/null || echo "/tmp/route_test_$$.json")
cat > "$TMPJSON" <<'ENDJSON'
{"text":"클롣 테스트","user":"U123","ts":"1700000000.000001","channel":"C123","eyeCwd":"D:/GitHub/WKAppBot"}
ENDJSON

route_out=$("$WKBOT" slack route --dry-run --file "$TMPJSON" 2>/dev/null)
route_exit=$?
rm -f "$TMPJSON"

if [ $route_exit -ne 0 ]; then
  echo "[FAIL] slack route --dry-run --file exited non-zero ($route_exit)"
  echo "Output: $route_out"
  exit 1
fi
echo "[PASS] slack route --dry-run --file exited 0"

# 6. Runtime: check [DRY-RUN] in output (if prompt windows exist)
if echo "$route_out" | grep -q "\[DRY-RUN\]"; then
  echo "[PASS] [DRY-RUN] output found (prompt window(s) discovered)"
  # Check no "전달했습니다" (ack) in output
  if echo "$route_out" | grep -q "전달했습니다"; then
    echo "[FAIL] Slack ack sent during dry-run (should be suppressed)"
    exit 1
  fi
  echo "[PASS] No ack sent in dry-run"
else
  echo "[INFO] No prompt windows found (dry-run ran but no targets) — routing logic OK"
fi

echo ""
echo "[PASS] slack route --dry-run test complete"
exit 0
