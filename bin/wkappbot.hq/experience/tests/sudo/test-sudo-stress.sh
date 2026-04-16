#!/bin/bash
# Stress tests for --sudo path targeting the "worrying scenarios" flagged in
# v5.14.7 post-mortem:
#   S1: broadcast-close kills in-flight request (repeat --sudo in quick succession)
#   S2: Layer 8 post-UAC handshake reuses pre-booting admin Eye (no duplicate spawn)
#   S3: admin Eye .new.exe auto-drain loop (promote once, no infinite successor cycle)
#   S4: user Eye pipe FALLBACK integrity (args with --sudo → exit 125 without dispatch)
#
# Run this AFTER an admin Eye is already up. If admin Eye is missing the script
# will trigger UAC on S1 — abort with Ctrl+C and launch admin Eye first
# (`wkappbot eye --sudo`), then re-run.

set -u
WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot.exe"
BIN_DIR="D:/GitHub/WKAppBot/bin"
ADMIN_PIPE='\\.\pipe\wkappbot_elevated'
USER_PIPE='\\.\pipe\WKAppBotCmdPipe'
PASS=0
FAIL=0

pass() { echo "PASS: $1"; PASS=$((PASS+1)); }
fail() { echo "FAIL: $1"; FAIL=$((FAIL+1)); }

admin_pipe_alive() {
  powershell -Command "
    \$p = New-Object System.IO.Pipes.NamedPipeClientStream('.', 'wkappbot_elevated', [System.IO.Pipes.PipeDirection]::InOut)
    try { \$p.Connect(100); \$p.Close(); Write-Output 'YES' } catch { Write-Output 'NO' }
  " 2>/dev/null | tr -d '[:space:]'
}

user_pipe_alive() {
  powershell -Command "
    \$p = New-Object System.IO.Pipes.NamedPipeClientStream('.', 'WKAppBotCmdPipe', [System.IO.Pipes.PipeDirection]::InOut)
    try { \$p.Connect(100); \$p.Close(); Write-Output 'YES' } catch { Write-Output 'NO' }
  " 2>/dev/null | tr -d '[:space:]'
}

echo "=== Stress test setup ==="
echo "admin pipe: $(admin_pipe_alive)"
echo "user pipe:  $(user_pipe_alive)"
echo ""

# ── S1: broadcast-close does not kill in-flight during 5 rapid --sudo calls ──
# NOTE: first 1-2 iterations are allowed to fail (admin Eye cold-start + UAC wait).
# After admin Eye is warm, subsequent iterations must succeed with windows output.
echo "=== S1: 5 rapid --sudo windows — steady-state survives (cold-start tolerated) ==="
S1_OUT=$(mktemp)
S1_WARM_FAIL=0
S1_COLD_FAIL=0
for i in 1 2 3 4 5; do
  start=$(date +%s%N)
  timeout 12 "$WKAPPBOT" --sudo windows 1>"$S1_OUT" 2>/dev/null
  code=$?
  end=$(date +%s%N)
  ms=$(( (end - start) / 1000000 ))
  # windows output uses [XXXXXXXX] hwnd brackets, not "hwnd:0x"
  hwnd=$(grep -cE '^\s*\[[0-9A-Fa-f]{8}\]' "$S1_OUT")
  echo "  iter $i: exit=$code rtt=${ms}ms hwnd_lines=$hwnd"
  if [ "$code" -ne 0 ] || [ "$hwnd" -eq 0 ]; then
    if [ "$i" -le 2 ]; then
      S1_COLD_FAIL=$((S1_COLD_FAIL+1))
    else
      S1_WARM_FAIL=$((S1_WARM_FAIL+1))
    fi
  fi
done
rm -f "$S1_OUT"
if [ "$S1_WARM_FAIL" -eq 0 ]; then
  pass "S1: warm iterations (3-5) all succeeded — cold-start failures: $S1_COLD_FAIL (tolerated)"
else
  fail "S1: $S1_WARM_FAIL of 3 warm iterations failed — admin Eye unstable once warm"
fi
echo ""

# ── S2: post-UAC handshake reuses pre-existing admin Eye, no duplicate spawn ──
echo "=== S2: admin Eye count stable across 3 --sudo calls (Layer 8 reuse) ==="
count_cores() {
  powershell -Command "(Get-Process wkappbot-core -ErrorAction SilentlyContinue | Where-Object { \$_.Path -eq \$null -or \$_.Path -eq '' }).Count" 2>/dev/null | tr -d '[:space:]'
}
BEFORE=$(count_cores)
echo "  admin-candidate cores before: $BEFORE"
for i in 1 2 3; do
  timeout 10 "$WKAPPBOT" --sudo windows 1>/dev/null 2>/dev/null
done
AFTER=$(count_cores)
echo "  admin-candidate cores after:  $AFTER"
# Accept growth of 0 or 1 (if none existed, first call spawned one). Growth >1 means duplicates.
DELTA=$((AFTER - BEFORE))
if [ "$DELTA" -le 1 ]; then
  pass "S2: admin Eye reused (delta=$DELTA, no duplicate spawn)"
else
  fail "S2: $DELTA duplicate admin Eye processes spawned — Layer 8 handshake not kicking in"
fi
echo ""

# ── S3: .new.exe stability — seeded stale .new.exe should NOT trigger loop ──
echo "=== S3: admin Eye does not infinite-loop on stale .new.exe ==="
S3_BEFORE_CORES=$(count_cores)
# Auto-cleanup pre-existing .new.exe from prior aborted runs before testing
if [ -f "$BIN_DIR/wkappbot-core.new.exe" ]; then
  rm -f "$BIN_DIR/wkappbot-core.new.exe"
  echo "  cleared pre-existing .new.exe from prior run"
fi
if true; then
  cp "$BIN_DIR/wkappbot-core.exe" "$BIN_DIR/wkappbot-core.new.exe" 2>/dev/null
  if [ $? -ne 0 ]; then
    fail "S3: could not stage .new.exe (bin locked?)"
  else
    echo "  staged .new.exe — waiting 15s for admin Eye response"
    sleep 15
    S3_AFTER_CORES=$(count_cores)
    DELTA_S3=$((S3_AFTER_CORES - S3_BEFORE_CORES))
    # .new.exe should be consumed (renamed) OR left alone
    # Admin Eye count should grow by at most 1 (single successor)
    REMAINING_NEW=$([ -f "$BIN_DIR/wkappbot-core.new.exe" ] && echo "yes" || echo "no")
    echo "  .new.exe remaining: $REMAINING_NEW, admin-cores delta=$DELTA_S3"
    if [ "$DELTA_S3" -le 1 ]; then
      pass "S3: no admin Eye cascade (delta=$DELTA_S3 ≤ 1)"
    else
      fail "S3: $DELTA_S3 admin Eye processes appeared — infinite successor loop suspected"
    fi
    # Cleanup: remove .new.exe if still there
    rm -f "$BIN_DIR/wkappbot-core.new.exe"
  fi
fi
echo ""

# ── S4: user Eye pipe FALLBACK integrity ──
echo "=== S4: user Eye pipe refuses --sudo with FALLBACK marker (exit 125) ==="
if [ "$(user_pipe_alive)" = "NO" ]; then
  fail "S4: user Eye pipe not alive — skip (start Eye first)"
else
  S4_OUT=$(mktemp)
  powershell -Command "
    \$pipe = New-Object System.IO.Pipes.NamedPipeClientStream('.', 'WKAppBotCmdPipe', [System.IO.Pipes.PipeDirection]::InOut)
    \$pipe.Connect(2000)
    \$w = New-Object System.IO.StreamWriter(\$pipe); \$w.AutoFlush = \$true
    \$r = New-Object System.IO.StreamReader(\$pipe)
    \$payload = ConvertTo-Json @('--sudo','windows') -Compress
    \$w.WriteLine(\$payload)
    \$deadline = (Get-Date).AddSeconds(3)
    while ((Get-Date) -lt \$deadline) {
      \$line = \$r.ReadLine()
      if (\$null -eq \$line) { break }
      Write-Host \$line
      if (\$line.StartsWith([char]0 + 'END')) { break }
    }
    \$pipe.Close()
  " 2>&1 | tr -d '\r' > "$S4_OUT"

  got_fallback=$(grep -c "EYE:FALLBACK" "$S4_OUT")
  got_125=$(grep -c "END 125" "$S4_OUT")
  if [ "$got_fallback" -ge 1 ] && [ "$got_125" -ge 1 ]; then
    pass "S4: user Eye returned [EYE:FALLBACK] + exit 125 as expected"
  else
    fail "S4: expected FALLBACK marker + exit 125 — got fallback=$got_fallback end125=$got_125"
    echo "  --- raw response ---"
    head -10 "$S4_OUT"
  fi
  rm -f "$S4_OUT"
fi
echo ""

# ── S5: admin Eye process actually has High integrity (proves UAC-granted elevation) ──
echo "=== S5: admin Eye highest integrity level is High (admin) ==="
if [ "$(admin_pipe_alive)" = "NO" ]; then
  fail "S5: admin pipe not alive — skip (cannot verify integrity without admin Eye)"
else
  IL_OUT=$(powershell -Command "
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public class IL {
  [DllImport(\"kernel32.dll\", SetLastError=true)]
  public static extern IntPtr OpenProcess(int access, bool inherit, int pid);
  [DllImport(\"kernel32.dll\")] public static extern bool CloseHandle(IntPtr h);
  [DllImport(\"advapi32.dll\", SetLastError=true)]
  public static extern bool OpenProcessToken(IntPtr p, int access, out IntPtr tok);
  [DllImport(\"advapi32.dll\", SetLastError=true)]
  public static extern bool GetTokenInformation(IntPtr tok, int cls, IntPtr info, int len, out int ret);
  [DllImport(\"advapi32.dll\", SetLastError=true)]
  public static extern IntPtr GetSidSubAuthority(IntPtr sid, int idx);
  public const int TokenIntegrityLevel=25;
  public const int TOKEN_QUERY=8;
  public const int PROCESS_QUERY_LIMITED_INFORMATION=0x1000;
  public static int Level(int pid) {
    var h = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, pid);
    if (h == IntPtr.Zero) return 0;
    try {
      IntPtr tok;
      if (!OpenProcessToken(h, TOKEN_QUERY, out tok)) return 0;
      try {
        int sz; GetTokenInformation(tok, TokenIntegrityLevel, IntPtr.Zero, 0, out sz);
        var buf = Marshal.AllocHGlobal(sz);
        try {
          if (!GetTokenInformation(tok, TokenIntegrityLevel, buf, sz, out sz)) return 0;
          var sid = Marshal.ReadIntPtr(buf);
          var last = GetSidSubAuthority(sid, Marshal.ReadByte(sid,1)-1);
          return Marshal.ReadInt32(last);
        } finally { Marshal.FreeHGlobal(buf); }
      } finally { CloseHandle(tok); }
    } finally { CloseHandle(h); }
  }
}
'@ -PassThru | Out-Null
Get-Process wkappbot-core -ErrorAction SilentlyContinue | ForEach-Object {
  \$il = [IL]::Level(\$_.Id)
  Write-Host ('pid={0} il=0x{1:X}' -f \$_.Id, \$il)
}
  " 2>/dev/null)
  echo "$IL_OUT" | head -12
  if echo "$IL_OUT" | grep -qE "il=0x[34][0-9A-Fa-f]{3}"; then
    pass "S5: at least one wkappbot-core runs with High (0x3000+) integrity — UAC elevation confirmed"
  else
    fail "S5: NO wkappbot-core has High integrity — admin Eye not actually elevated"
  fi
fi
echo ""

# ── Summary ──
echo "=== Summary ==="
echo "PASS: $PASS  FAIL: $FAIL"
[ "$FAIL" -eq 0 ] && echo "ALL STRESS TESTS PASSED" || echo "STRESS TESTS FAILED"
exit $FAIL
