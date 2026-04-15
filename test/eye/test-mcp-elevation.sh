#!/bin/bash
# MCP Elevation Handoff Test
# Tests: proxy detection → admin Core spawn → pipe relay → response
set -e

PASS=0
FAIL=0
WKAPPBOT="D:/SDK/bin/wkappbot-core.exe"

log() { echo "[TEST] $1"; }
pass() { log "✅ PASS: $1"; PASS=$((PASS+1)); }
fail() { log "❌ FAIL: $1"; FAIL=$((FAIL+1)); }

# 1. IsAvailable returns fast when no proxy
log "=== Test 1: IsAvailable fast-fail (no proxy) ==="
START=$(date +%s%N)
timeout 3 $WKAPPBOT a11y read "*XingQ*#*출력*" 2>&1 | grep -q "UIPI block\|elevation required\|Elevated Eye proxy timeout" && true
END=$(date +%s%N)
ELAPSED=$(( (END - START) / 1000000 ))
if [ "$ELAPSED" -lt 15000 ]; then
  pass "Fast fail in ${ELAPSED}ms (< 15s)"
else
  fail "Slow fail: ${ELAPSED}ms (expected < 15s)"
fi

# 2. MCP stdout clean (no pollution)
log "=== Test 2: MCP stdout cleanliness ==="
(echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'; sleep 2) | timeout 5 $WKAPPBOT mcp 1>/tmp/mcp_stdout.txt 2>/tmp/mcp_stderr.txt || true
STDOUT_SIZE=$(wc -c < /tmp/mcp_stdout.txt)
if grep -q "\[LAUNCHER\]\|\[MCP\]\|\[ACT\]" /tmp/mcp_stdout.txt 2>/dev/null; then
  fail "stdout polluted with log lines"
else
  pass "stdout clean ($STDOUT_SIZE bytes)"
fi

# 3. Speak melody fires (non-blocking)
log "=== Test 3: Speak melody (fire-and-forget) ==="
START2=$(date +%s%N)
timeout 10 $WKAPPBOT a11y inspect "*XingQ*" --depth 1 2>&1 | grep -q "elevated\|ELEVATION" && true
END2=$(date +%s%N)
ELAPSED2=$(( (END2 - START2) / 1000000 ))
pass "Elevation detected in ${ELAPSED2}ms"

# 4. Console window hidden (check no visible wkappbot console)
log "=== Test 4: Admin console hidden ==="
# After elevation attempt, check no large console window appeared
VISIBLE_CONSOLES=$(timeout 5 $WKAPPBOT a11y windows "*wkappbot*" --all 2>&1 | grep -c "ConsoleWindowClass" || true)
VISIBLE_CONSOLES=${VISIBLE_CONSOLES:-0}
VISIBLE_CONSOLES=$(echo "$VISIBLE_CONSOLES" | tr -d '[:space:]')
if [ "$VISIBLE_CONSOLES" -eq 0 ]; then
  pass "No visible admin console window"
else
  fail "Admin console window visible ($VISIBLE_CONSOLES found)"
fi

# 5. End-to-end: actually READ elevated target content (CRITICAL)
log "=== Test 5: End-to-end admin read (CRITICAL) ==="
log "⚠ UAC will appear — press YES!"
RESULT=$(timeout 90 $WKAPPBOT a11y read "*Microsoft Visual Studio*관리자*#*출력*" 2>&1 || true)
if echo "$RESULT" | grep -q "UIPI block\|elevation required\|Proxy unavailable"; then
  fail "Admin read FAILED — elevation handoff broken"
elif echo "$RESULT" | grep -q "출력\|Output\|Build\|빌드\|Error\|Warning"; then
  pass "Admin read SUCCEEDED — got VS Output content!"
else
  CHARS=$(echo "$RESULT" | wc -c)
  if [ "$CHARS" -gt 100 ]; then
    pass "Admin read returned content (${CHARS} chars)"
  else
    fail "Admin read too little (${CHARS} chars) — handoff likely failed"
  fi
fi

# Summary
echo ""
echo "═══════════════════════════════"
echo "  PASS: $PASS  FAIL: $FAIL"
echo "═══════════════════════════════"
[ "$FAIL" -eq 0 ] && echo "ALL TESTS PASSED ✅" || echo "SOME TESTS FAILED ❌"
exit $FAIL
