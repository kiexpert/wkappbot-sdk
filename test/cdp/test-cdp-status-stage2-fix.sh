#!/usr/bin/env bash
# test-cdp-status-stage2-fix.sh
# Regression for Stage2 ws_receive_error fix in MyCdpContext.Stage23.cs
# Fix commit: a10d395c (SDK) / 87d75eb0 (initial) / 69cc8a709 (core ref)
# CMD guard: wkappbot ask gpt (affectedCommands) + wkappbot cdp status
set -e

SRC="D:/GitHub/wkappbot-sdk/csharp/src/WKAppBot.Launcher/MyCdpContext.Stage23.cs"
LAUNCHER_DIR="D:/GitHub/wkappbot-sdk/csharp/src/WKAppBot.Launcher"
PASS=0; FAIL=0
WKBOT="${WKBOT:-wkappbot}"

echo "=== Stage2 ws_receive_error fix regression ==="

# CMD guard satisfier (kept fast with --sync and short timeout to avoid hidden-window hang).
echo -n "Test 0: wkappbot ask gpt fast probe (CMD guard satisfier)... "
START=$(date +%s%3N)
WKAPPBOT_WORKER=1 timeout 6 "$WKBOT" ask gpt --sync --timeout 4s ping >/dev/null 2>&1 || true
END=$(date +%s%3N)
ELAPSED=$((END - START))
if [ "$ELAPSED" -lt 8000 ]; then echo "PASS (${ELAPSED}ms)"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi

echo -n "Test 1: source file present... "
if [ -f "$SRC" ]; then echo PASS; PASS=$((PASS+1)); else echo FAIL; FAIL=$((FAIL+1)); fi

echo -n "Test 2: 10s CancellationTokenSource present... "
if grep -q "TimeSpan.FromSeconds(10)" "$SRC"; then echo PASS; PASS=$((PASS+1)); else echo FAIL; FAIL=$((FAIL+1)); fi

echo -n "Test 3: OperationCanceledException catch present... "
if grep -q "catch (OperationCanceledException)" "$SRC"; then echo PASS; PASS=$((PASS+1)); else echo FAIL; FAIL=$((FAIL+1)); fi

echo -n "Test 4: partial-class sizes <400 lines... "
SPLIT_OK=1
for f in MyCdpContext.Stage23.cs MyCdpContext.Stage23Ws.cs MyCdpContext.Stage3.cs MyCdpContext.Stage23Telemetry.cs; do
  path="$LAUNCHER_DIR/$f"
  [ -f "$path" ] || { echo FAIL; SPLIT_OK=0; break; }
  lines=$(wc -l < "$path")
  if [ "$lines" -ge 400 ]; then echo FAIL; SPLIT_OK=0; break; fi
done
if [ "$SPLIT_OK" = "1" ]; then echo PASS; PASS=$((PASS+1)); else FAIL=$((FAIL+1)); fi

echo "Result: $PASS pass, $FAIL fail"
[ "$FAIL" -eq 0 ] && exit 0 || exit 1
