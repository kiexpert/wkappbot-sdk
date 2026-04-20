#!/usr/bin/env bash
# test-sudo.sh -- verify --sudo / admin Eye lifecycle
# Usage: bash scripts/test-sudo.sh
# Requires: UAC prompt will appear once

set -euo pipefail
PASS=0; FAIL=0
ok()   { echo "  [PASS] $*"; ((PASS++)); }
fail() { echo "  [FAIL] $*"; ((FAIL++)); }
sep()  { echo "--------------------------------------------"; }

sep
echo "=== --sudo admin Eye lifecycle test ==="
sep

# 1. Kill any stale admin Eye first
echo "[1] Kill stale admin Eye (if any)"
ADMIN_PIDS=$(wmic process where "CommandLine like '%eye --elevated%'" get ProcessId 2>/dev/null | grep -E '^[0-9]+' || true)
if [[ -n "$ADMIN_PIDS" ]]; then
  echo "    stale admin Eye pids: $ADMIN_PIDS"
  for PID in $ADMIN_PIDS; do taskkill //PID $PID //F 2>/dev/null || true; done
  sleep 1
  ok "Stale admin Eye cleared"
else
  ok "No stale admin Eye"
fi

# 2. Verify user Eye is alive
echo "[2] Check user Eye"
if wkappbot eye tick 2>&1 | grep -q "Eye\|MCP\|Slack\|cwd"; then
  ok "User Eye alive"
else
  fail "User Eye not responding -- run 'wkappbot eye' first"
fi

# 3. Invoke --sudo (UAC will appear)
echo "[3] Invoke wkappbot --sudo eye tick  (UAC prompt expected)"
SUDO_OUT=$(wkappbot --sudo eye tick 2>&1 || true)
echo "$SUDO_OUT" | head -10

if echo "$SUDO_OUT" | grep -q "admin Eye pipe up\|handshake OK"; then
  ok "Admin Eye spawned and pipe handshake OK"
else
  fail "Admin Eye did not come up -- check logs"
fi

if echo "$SUDO_OUT" | grep -q "High.*admin\|integrity.*High"; then
  ok "Admin Eye running at High integrity"
else
  fail "Integrity level not confirmed"
fi

if echo "$SUDO_OUT" | grep -q "UAC.*approved\|approved=YES"; then
  ok "UAC approved"
else
  fail "UAC not confirmed approved"
fi

# 4. Check admin Eye is still alive after user Eye's evict loop runs
echo "[4] Check admin Eye survives evict loop (wait 5s)"
sleep 5
ADMIN_AFTER=$(wmic process where "CommandLine like '%eye --elevated%'" get ProcessId 2>/dev/null | grep -E '^[0-9]+' || true)
if [[ -n "$ADMIN_AFTER" ]]; then
  ok "Admin Eye still alive after evict cycle (pids: $ADMIN_AFTER)"
else
  fail "Admin Eye died after evict loop -- check [EYE:EVICT] logs"
fi

# 5. Verify proxy command (known to timeout currently -- track as known issue)
echo "[5] Proxy command: wkappbot --sudo windows"
PROXY_OUT=$(timeout 40 wkappbot --sudo windows 2>&1 || true)
if echo "$PROXY_OUT" | grep -q "Subprocess timeout\|proxy failed"; then
  echo "    [KNOWN BUG] Proxy subprocess timeout -- see suggest list"
  ((FAIL++))
elif echo "$PROXY_OUT" | grep -q "windows\|hwnd"; then
  ok "Proxy windows command returned output"
else
  fail "Proxy command unexpected result: $(echo "$PROXY_OUT" | tail -3)"
fi

sep
echo "Result: PASS=$PASS  FAIL=$FAIL"
[[ $FAIL -eq 0 ]] && echo "ALL PASS" && exit 0 || echo "SOME FAILURES" && exit 1
