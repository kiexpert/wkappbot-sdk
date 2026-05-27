#!/bin/bash
# Evidence: `wkappbot taskkill /IM <name>` auto-kills non-protected processes
# immediately (Windows taskkill semantics) and shows per-process [NMB h=H t=T]
# metrics. Protected sessions are preserved with a --force hint.
# Reproducible any time: spawns a throwaway ping, then auto-kills it.
# Uses the deployed core binary directly so the result is independent of the
# Eye hot-swap timing (the launcher may still route to an old running core).
set -e
CORE="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
[ -x "$CORE" ] || CORE="wkappbot"   # fallback to PATH (wkappbot taskkill /IM)

# Spawn a disposable non-protected process (ping = UNKNOWN classification).
powershell.exe -NoProfile -Command "Start-Process ping -ArgumentList '-n','60','127.0.0.1' -WindowStyle Hidden" >/dev/null 2>&1
sleep 1

# Auto-kill path under test: taskkill /IM PING.EXE
OUT="$("$CORE" taskkill /IM PING.EXE 2>&1 || true)"

echo "$OUT" | grep -q "KILLED (non-protected)" || { echo "FAIL: no KILLED block"; echo "$OUT" | tail -8; exit 1; }
echo "$OUT" | grep -Eq "\[[0-9?]+MB h=[0-9?]+ t=[0-9?]+\]" || { echo "FAIL: no [NMB h=H t=T] metrics"; exit 1; }
echo "$OUT" | grep -q "PROTECTED (active sessions -- NOT killed)" || { echo "FAIL: no PROTECTED block"; exit 1; }

if powershell.exe -NoProfile -Command "if (Get-Process PING -ErrorAction SilentlyContinue) {exit 1} else {exit 0}" >/dev/null 2>&1; then
  echo "PASS: taskkill /IM auto-killed non-protected ping + showed metrics + PROTECTED block"
  exit 0
else
  echo "FAIL: ping survived auto-kill"; exit 1
fi
