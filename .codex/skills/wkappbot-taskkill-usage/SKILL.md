---
id: wkappbot-taskkill-usage
app: wkappbot
description: "taskkill CLASSIFY: shows full cmdline per process (2 lines). Zombie Eye cleanup after long uptime."
---

> **Refresh**: `wkappbot skill read wkappbot-taskkill-usage --if-newer` — v1.8 (2026-05-09)

# wkappbot taskkill: classify and kill wkappbot processes

## Steps

1. CLASSIFY (callout popup, no actual kill): wkappbot taskkill /IM wkappbot-core.exe
Each match = 2 lines:
  Line1: PID {id} {image} [{ctime}] reason PROTECTED/ZOMBIE/UNKNOWN
  Line2:       > full command line (not truncated)
Force-kill: wkappbot taskkill --force 1234,5678
2. Zombie Eye cleanup (100+ procs after 24h):
High CPU = old Eye daemon. Kill all but newest:
  Get-Process wkappbot-core 
3. --force PID: CDP port guard (--remote-debugging-port) is now bypassed when --force + explicit PID list are both provided (Core fix 9e62e69e7). Chrome CDP sessions are killable via --force without --arg workaround.
4. PENDING behavior improvement (suggest filed 2026-05-26): /IM should auto-kill non-protected (ZOMBIE/UNKNOWN) processes immediately like Windows taskkill; PROTECTED shown separately with '--force <pids>' hint. Per-process info should include: PID, caller window, CPU%, memory, handle count, thread count (in addition to existing cmdline). Currently /IM = classify-only, blank > for zombie cmdlines.
5. TEST A verified 2026-05-26: wkappbot taskkill --force 17076 killed live CDP Chrome (--remote-debugging-port=9982) via WM_CLOSE gracefully after Core fix 9e62e69e7. Build clean+publish succeeded on retry (CS0009 transient artifact fixed by dotnet clean first).
