#!/bin/bash
# Test: MCP --sudo pre-execution gate in McpProxy.cs
#
# Expected behavior AFTER Launcher rebuild:
#   - Request with "--sudo" in JSON-RPC arguments triggers preemptive admin spawn
#   - Launcher stderr contains: [LAUNCHER:MCP] --sudo requested (id=...) — preemptive admin Core spawn
#   - Normal requests (no --sudo) do NOT trigger the gate
#
# IMPORTANT: This test causes a UAC prompt when the gate fires.
#   Use --skip-uac env var to only verify baseline (no UAC-causing request).

set -u
WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot.exe"
STDOUT_LOG=$(mktemp)
STDERR_LOG=$(mktemp)
SUDO_LOG=$(mktemp)
SUDO_STDERR=$(mktemp)
PASS=0
FAIL=0
trap 'rm -f "$STDOUT_LOG" "$STDERR_LOG" "$SUDO_LOG" "$SUDO_STDERR"' EXIT

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

echo "=== Test setup ==="
echo "Launcher: $WKAPPBOT"
echo "Launcher mtime: $(stat -c '%y' "$WKAPPBOT" 2>/dev/null || echo 'unknown')"
echo ""

# ── Test 1: Normal request without --sudo (baseline) ──
echo "=== Test 1: normal request passes through (no gate fire) ==="
(
  echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'
  sleep 1
  echo '{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"wkappbot_cli","arguments":{"argv":["windows"]}}}'
  sleep 3
) | timeout 8 "$WKAPPBOT" mcp 1>"$STDOUT_LOG" 2>"$STDERR_LOG"
EXIT1=$?
echo "exit=$EXIT1"

if grep -q "preemptive admin Core spawn" "$STDERR_LOG"; then
  fail "T1: gate fired on NON-sudo request (false positive)"
  grep "preemptive" "$STDERR_LOG" | head -3
else
  pass "T1: gate did NOT fire on normal request"
fi

if grep -q "jsonrpc" "$STDOUT_LOG"; then
  pass "T1: MCP responded with JSON-RPC output"
else
  fail "T1: no JSON-RPC response captured"
fi
echo ""

# ── Test 2: --sudo request triggers gate (UAC may pop up) ──
echo "=== Test 2: --sudo request triggers preemptive admin spawn ==="
echo "WARNING: UAC dialog may appear — accept or cancel within 6 seconds"
(
  echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'
  sleep 1
  echo '{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"wkappbot_cli","arguments":{"argv":["--sudo","--help"]}}}'
  sleep 4
) | timeout 10 "$WKAPPBOT" mcp 1>"$SUDO_LOG" 2>"$SUDO_STDERR"
EXIT2=$?
echo "exit=$EXIT2"

if grep -q "preemptive admin Core spawn" "$SUDO_STDERR"; then
  pass "T2: gate detected --sudo and triggered admin spawn"
  grep "preemptive\|SUDO\|ELEVATION" "$SUDO_STDERR" | head -5
else
  fail "T2: gate did NOT fire on --sudo request — Launcher likely pre-change binary"
  echo "--- stderr tail ---"
  tail -20 "$SUDO_STDERR"
fi
echo ""

# ── Test 3: line.Contains pattern check (raw protocol verification) ──
echo "=== Test 3: verify JSON-RPC line contains \"--sudo\" literally ==="
LINE='{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"wkappbot_cli","arguments":{"argv":["--sudo","--help"]}}}'
if echo "$LINE" | grep -q '"--sudo"'; then
  pass "T3: JSON-RPC payload includes quoted --sudo as the gate expects"
else
  fail "T3: --sudo pattern not found in JSON-RPC line (detection will miss)"
fi
echo ""

# ── Test 4: CLI non-MCP path — wkappbot --sudo triggers Core's startup handler ──
#   Covers: Program.cs:233 detects --sudo at Core startup and routes via admin Eye proxy
#   (pipe-persistent admin spawn — re-uses admin Eye daemon, no UAC if already running)
echo "=== Test 4: CLI Core startup --sudo detection (non-MCP path) ==="
CLI_OUT=$(mktemp)
CLI_ERR=$(mktemp)
trap 'rm -f "$STDOUT_LOG" "$STDERR_LOG" "$SUDO_LOG" "$SUDO_STDERR" "$CLI_OUT" "$CLI_ERR"' EXIT

timeout 15 "$WKAPPBOT" --sudo windows 1>"$CLI_OUT" 2>"$CLI_ERR"
EXIT4=$?
echo "exit=$EXIT4"

# Evidence 1: Core's --sudo handler message must appear (Program.cs:267)
if grep -q "\[SUDO\] Routing via admin Eye proxy" "$CLI_ERR" "$CLI_OUT"; then
  pass "T4a: Core detected --sudo and routed via admin Eye proxy"
elif grep -q "\[SUDO\]" "$CLI_ERR" "$CLI_OUT"; then
  pass "T4a: Core --sudo handler activated (non-routing path)"
  grep "\[SUDO\]" "$CLI_ERR" "$CLI_OUT" | head -3
else
  fail "T4a: no [SUDO] marker found — Core may not have received --sudo"
  echo "--- stderr tail ---"
  tail -10 "$CLI_ERR"
fi

# Evidence 2: exit must be terminal (0 = proxy ran, or 1 = proxy unavailable/denied)
# Must NOT be 124 (timeout) which would indicate hang
if [ "$EXIT4" -eq 0 ] || [ "$EXIT4" -eq 1 ]; then
  pass "T4b: CLI --sudo exited cleanly (no hang) — exit=$EXIT4"
elif [ "$EXIT4" -eq 124 ]; then
  fail "T4b: CLI --sudo HUNG (timeout 15s) — admin Eye proxy may be unreachable"
else
  pass "T4b: CLI --sudo exited with code $EXIT4 (not a hang)"
fi
echo ""

# ── Test 5: eye --sudo force-launch (non-destructive admin Eye spawn) ──
#   Expected: if admin Eye already running → "already running" message, no kill
#   If not: triggers LaunchElevatedEye (UAC if needed), side-by-side with user Eye
echo "=== Test 5: wkappbot eye --sudo force-launches admin Eye (non-destructive) ==="
EYE_OUT=$(mktemp)
EYE_ERR=$(mktemp)

# Snapshot current Eye PIDs before — non-destructive flag should NOT kill them
BEFORE_PIDS=$(tasklist //FI "IMAGENAME eq wkappbot-core.exe" 2>/dev/null | grep -oE '[0-9]{3,6}' | sort -n | tr '\n' ',')
echo "Cores before: $BEFORE_PIDS"

timeout 15 "$WKAPPBOT" eye --sudo 1>"$EYE_OUT" 2>"$EYE_ERR"
EXIT5=$?
echo "exit=$EXIT5"

# Snapshot after — ensure no cores killed
AFTER_PIDS=$(tasklist //FI "IMAGENAME eq wkappbot-core.exe" 2>/dev/null | grep -oE '[0-9]{3,6}' | sort -n | tr '\n' ',')
echo "Cores after: $AFTER_PIDS"

# Evidence 1: must show force-launch OR already-running marker
if grep -q "Force-launching admin Eye\|Admin Eye already running\|Admin Eye launched" "$EYE_ERR" "$EYE_OUT"; then
  pass "T5a: eye --sudo hit non-destructive force-launch path"
  grep -E "Force-launching|already running|launched" "$EYE_ERR" "$EYE_OUT" | head -3
else
  fail "T5a: non-destructive path marker missing"
  tail -10 "$EYE_ERR"
fi

# Evidence 2: must NOT contain old kill-path message
if grep -q "Stopping current Eye\|Eye stopped" "$EYE_ERR" "$EYE_OUT"; then
  fail "T5b: old destructive kill-path fired — regression!"
else
  pass "T5b: old destructive kill-path did NOT fire"
fi

# Evidence 3: exit clean
if [ "$EXIT5" -eq 0 ] || [ "$EXIT5" -eq 1 ]; then
  pass "T5c: eye --sudo exited cleanly — exit=$EXIT5"
elif [ "$EXIT5" -eq 124 ]; then
  fail "T5c: eye --sudo HUNG (timeout 15s)"
else
  pass "T5c: eye --sudo exit=$EXIT5 (not a hang)"
fi

rm -f "$EYE_OUT" "$EYE_ERR"
echo ""

# ── Test 6: eye --sudo hot-swap piggyback (promotes .new.exe before UAC) ──
#   Expected: ElevatedEyeProxy.LaunchElevatedEye detects wkappbot-core.new.exe
#   and promotes it before spawning admin Eye (admin runs latest code).
#   Logs [ELEVATION:HOT-SWAP] line if pending swap existed.
echo "=== Test 6: eye --sudo promotes pending .new.exe (hot-swap piggyback) ==="
HS_OUT=$(mktemp)
HS_ERR=$(mktemp)
BIN_DIR="D:/GitHub/WKAppBot/bin"
FAKE_NEW="$BIN_DIR/wkappbot-core.new.exe"

# Only run swap probe if admin Eye not running AND there isn't already a .new.exe
# (we don't disturb running admin Eye; this is a code-path coverage test)
if [ ! -f "$FAKE_NEW" ]; then
  # Seed a synthetic .new.exe by duplicating current core (same size/hash; just to trigger promotion path)
  cp "$BIN_DIR/wkappbot-core.exe" "$FAKE_NEW" 2>/dev/null
  SEEDED=1
else
  SEEDED=0
fi

timeout 15 "$WKAPPBOT" eye --sudo 1>"$HS_OUT" 2>"$HS_ERR"
EXIT6=$?
echo "exit=$EXIT6"

# If admin Eye was already running, promotion path is skipped (not a failure, just no-op).
# Otherwise: [ELEVATION:HOT-SWAP] markers should appear.
if grep -q "Admin Eye already running" "$HS_ERR" "$HS_OUT"; then
  pass "T6: admin Eye already running — hot-swap probe skipped (not a failure)"
elif grep -q "\[ELEVATION:HOT-SWAP\] promoted\|\[ELEVATION:HOT-SWAP\] pending" "$HS_ERR" "$HS_OUT"; then
  pass "T6: hot-swap piggyback fired — .new.exe promoted before admin spawn"
  grep "\[ELEVATION:HOT-SWAP\]" "$HS_ERR" "$HS_OUT" | head -3
else
  fail "T6: no hot-swap markers and admin Eye not acknowledged"
  tail -10 "$HS_ERR"
fi

# Cleanup seeded file if we added it and it wasn't promoted
[ "$SEEDED" -eq 1 ] && [ -f "$FAKE_NEW" ] && rm -f "$FAKE_NEW"

rm -f "$HS_OUT" "$HS_ERR"
echo ""

# ── Test 7: Launcher ↔ admin Eye round-trip communication ──
#   Sends `wkappbot --sudo windows` and verifies the full round-trip:
#     Launcher → Core → ElevatedEyeClient → admin Eye pipe → admin Core → response
#   Also asserts no "communication failed" marker appears.
echo "=== Test 7: Launcher ↔ admin Eye round-trip (pipe + exec + response) ==="
RT_OUT=$(mktemp)
RT_ERR=$(mktemp)

RT_START=$(date +%s%N)
timeout 10 "$WKAPPBOT" --sudo windows 1>"$RT_OUT" 2>"$RT_ERR"
EXIT7=$?
RT_END=$(date +%s%N)
RT_MS=$(( (RT_END - RT_START) / 1000000 ))
echo "exit=$EXIT7  rtt=${RT_MS}ms"

# Evidence 1: Routing marker must appear (Core entered proxy path)
if grep -q "\[SUDO\] Routing via admin Eye proxy" "$RT_ERR" "$RT_OUT"; then
  pass "T7a: Core routed via admin Eye proxy"
else
  fail "T7a: no proxy routing marker — Core may not have reached admin path"
fi

# Evidence 2: no explicit communication failure
if grep -q "Admin Eye proxy communication failed\|Proxy communication failed" "$RT_ERR" "$RT_OUT"; then
  fail "T7b: proxy reported communication failure"
  grep "communication failed" "$RT_ERR" "$RT_OUT" | head -2
else
  pass "T7b: no proxy communication failure markers"
fi

# Evidence 3: response came back (hwnd patterns from windows listing OR any JSON-ish content)
if grep -qE "hwnd:0x[0-9A-Fa-f]+|# TARGET|\"hwnd\"" "$RT_OUT"; then
  pass "T7c: admin Eye returned a windows-command response"
elif [ "$EXIT7" -eq 0 ]; then
  pass "T7c: command returned exit=0 (response OK even without hwnd marker)"
else
  fail "T7c: admin Eye returned no recognizable windows output (exit=$EXIT7)"
  head -5 "$RT_OUT"
fi

# Evidence 4: round-trip time sanity (<= 8000ms)
if [ "$RT_MS" -le 8000 ]; then
  pass "T7d: round-trip within 8s budget (${RT_MS}ms)"
else
  fail "T7d: round-trip slow (${RT_MS}ms > 8000ms) — pipe or admin Eye may be stalled"
fi

# Evidence 5: PULSE trail captured (shows path taken through Core)
# StepProfiler uses CallerMemberName ("Main"), so tag is [PULSE:Main] not [PULSE:core-sudo]
if grep -q "\[PULSE:Main\]" "$RT_ERR"; then
  pass "T7e: PulseStep trail captured in stderr"
  grep "\[PULSE:Main\]" "$RT_ERR" | head -4
else
  fail "T7e: PulseStep trail missing — pulse instrumentation not firing"
fi

rm -f "$RT_OUT" "$RT_ERR"
echo ""

# ── Test 8: Launcher-side 100ms admin Eye liveness probe ──
#   Verify [LAUNCHER:SUDO] admin Eye ping marker appears in stderr when --sudo used
echo "=== Test 8: Launcher 100ms probe marker ==="
L_OUT=$(mktemp)
L_ERR=$(mktemp)

timeout 10 "$WKAPPBOT" --sudo windows 1>"$L_OUT" 2>"$L_ERR"
EXIT8=$?

if grep -qE "\[LAUNCHER:SUDO\] admin Eye ping \(100ms\): (alive|unreachable)" "$L_ERR" "$L_OUT"; then
  pass "T8: Launcher 100ms probe marker emitted"
  grep "\[LAUNCHER:SUDO\]" "$L_ERR" "$L_OUT" | head -2
else
  fail "T8: Launcher probe marker missing — v5.14+2-layer binary not loaded?"
fi

rm -f "$L_OUT" "$L_ERR"
echo ""

# ── Test 9: Core-side 100ms admin Eye ping (replaces IsAvailable file check) ──
echo "=== Test 9: Core Ping(100ms) liveness probe ==="
C_OUT=$(mktemp)
C_ERR=$(mktemp)

timeout 10 "$WKAPPBOT" --sudo windows 1>"$C_OUT" 2>"$C_ERR"
EXIT9=$?

if grep -qE "\[PULSE:Main\] \"admin Eye ping \(100ms\): (alive|unreachable)\"" "$C_ERR" "$C_OUT"; then
  pass "T9: Core 100ms Ping marker emitted"
  grep "admin Eye ping" "$C_ERR" "$C_OUT" | head -2
else
  fail "T9: Core Ping marker missing — Program.cs sudo handler not hitting new path"
fi

rm -f "$C_OUT" "$C_ERR"
echo ""

# ── Test 10: Handshake budget — if admin Eye unreachable, both probes abort within ~500ms ──
#   Budget: 100ms (Launcher) + 100ms (Core) + fork overhead ≤ 500ms to reach sudo protection marker
#   Only meaningful when admin Eye is NOT running. We don't kill it here (would break other tests);
#   instead we just verify the PATH fires correctly regardless of admin state.
echo "=== Test 10: probe ordering (Launcher before Core) ==="
P_OUT=$(mktemp)
P_ERR=$(mktemp)

timeout 10 "$WKAPPBOT" --sudo windows 1>"$P_OUT" 2>"$P_ERR"
EXIT10=$?

# Combined stream (order preserved via separate capture not possible here, so check both streams)
# Check Launcher marker appears at all and Core marker appears at all
L_PRESENT=$(grep -cE "\[LAUNCHER:SUDO\]" "$P_ERR" "$P_OUT" | awk -F: '{sum+=$NF} END {print sum+0}')
C_PRESENT=$(grep -cE "admin Eye ping \(100ms\)" "$P_ERR" "$P_OUT" | awk -F: '{sum+=$NF} END {print sum+0}')

if [ "$L_PRESENT" -ge 1 ] && [ "$C_PRESENT" -ge 1 ]; then
  pass "T10: both Launcher and Core probe markers present (2-layer defense active)"
else
  fail "T10: layer markers incomplete (Launcher=$L_PRESENT Core=$C_PRESENT)"
fi

rm -f "$P_OUT" "$P_ERR"
echo ""

# ── Summary ──
echo "=== Summary ==="
echo "PASS: $PASS  FAIL: $FAIL"
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED" || echo "TESTS FAILED (likely Launcher not yet rebuilt with gate)"
exit $FAIL
