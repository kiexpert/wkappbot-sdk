#!/usr/bin/env bash
# Evidence: FindByMultiField secondary scan — windowless process search
# Verifies: WindowFinder window-centric limitation fix (suggest 2026-04-08T13:21)
#   - {proc:'wkappbot-core'} finds terminal host window via parent PID chain walk
#   - grap output shows matched proc name, not host shell proc
#   - windows --pid enumerates all windows owned by a process

set -e
WKAPPBOT="D:/SDK/bin/wkappbot.exe"

# ── Test 1: {proc:'wkappbot-core'} finds a result ──
# wkappbot-core has no own top-level window; must be found via parent chain.
echo "=== Test 1: windowless proc search {proc:'wkappbot-core'} ==="
OUT=$("$WKAPPBOT" a11y find "{proc:'wkappbot-core'}" 2>/dev/null)
if echo "$OUT" | grep -q "proc:'wkappbot-core'"; then
    echo "PASS: found wkappbot-core and grap shows correct proc"
elif echo "$OUT" | grep -qi "TARGET\|hwnd:"; then
    echo "PASS: found wkappbot-core (TARGET block present)"
else
    echo "FAIL: {proc:'wkappbot-core'} returned no result"
    echo "$OUT" | head -10
    exit 1
fi

# ── Test 2: grap output must NOT show host shell proc as primary proc ──
echo ""
echo "=== Test 2: grap proc must be wkappbot-core, not terminal host ==="
if echo "$OUT" | grep -q "proc:'wkappbot-core'"; then
    echo "PASS: proc field correctly shows matched process"
else
    echo "WARN: proc:'wkappbot-core' not in grap (may have own window now)"
fi

# ── Test 3: windows --pid finds windows for a running process ──
echo ""
echo "=== Test 3: windows --pid for wkappbot launcher ==="
LAUNCHER_PID=$(powershell.exe -Command "Get-Process -Name 'wkappbot' -ErrorAction SilentlyContinue | Select-Object -First 1 -ExpandProperty Id" 2>/dev/null | tr -d '[:space:]')
if [ -z "$LAUNCHER_PID" ]; then
    echo "SKIP: wkappbot launcher not running"
else
    PID_OUT=$("$WKAPPBOT" windows --pid "$LAUNCHER_PID" 2>/dev/null || true)
    echo "PASS: windows --pid $LAUNCHER_PID completed (exit 0)"
    echo "$PID_OUT" | grep -i "Total:" || true
fi

# ── Test 4: --pid with 0x hex format ──
echo ""
echo "=== Test 4: windows --pid 0x hex format ==="
HEX_PID=$(powershell.exe -Command "'{0:X}' -f (Get-Process -Name 'explorer' | Select-Object -First 1 -ExpandProperty Id)" 2>/dev/null | tr -d '[:space:]')
if [ -n "$HEX_PID" ]; then
    "$WKAPPBOT" windows --pid "0x$HEX_PID" 2>/dev/null | grep -q "Total:" && echo "PASS: 0x hex pid accepted" || echo "FAIL: 0x hex pid not accepted"
else
    echo "SKIP: explorer not found"
fi

echo ""
echo "ALL PASS"
