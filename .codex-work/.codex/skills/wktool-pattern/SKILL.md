---
id: wktool-pattern
app: wkappbot-workflow
description: "wk-tool scripts are QA-first tools: they detect bugs, validate state, and report anomalies cheaply so Claude never wastes tokens polling logs. MANDATORY before any ask/CDP work. Covers: wkask/wkcdp usage, QA script authoring (state-check→eval-js→auto-suggest), health check sequence, cookie/off-screen detection."
tags: [wkask, wkcdp, token, efficiency, monitor, cdp, ask, health, pattern, script, bug, evidence]
---

> **Refresh**: `wkappbot skill read wktool-pattern --if-newer` — v1.12 (2026-05-10)

# wktool pattern: token-efficient monitoring

## Steps

1. HAIKU QUICK START (exact commands, copy-paste ready):
MEMORY CHECK: Get-Process chrome,wkappbot-core | Measure-Object WorkingSet64 -Sum | Select Sum
SESSION STATUS: powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1
SESSION DETAIL: powershell -File D:/GitHub/WKAppBot/bin/cdp-mon.ps1
CLEANUP STALE: powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1 clean
ASK HEALTH GPT: powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 gpt 'say:ok' -Timeout 60
ASK HEALTH GEMINI: powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 gemini 'say:ok' -Timeout 90
ASK FAST (2s): wkappbot ask groq 'say:ok'
NEVER poll log files in loops -- use these scripts which stream internally.
2. STANDARD HEALTH CHECK SEQUENCE (run in order):
1. powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1 clean
2. wkappbot ask groq 'say:ok' (fast 2-3s REST, no Chrome needed)
3. powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 gpt 'say:ok' -Timeout 60 (Chrome CDP)
4. powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 gemini 'say:ok' -Timeout 90
All pass = healthy. If gpt/gemini slow (>30s): run wkcdp clean + retry. groq always fast (2s) = good baseline.
3. WKASK SIGNALS: [WAIT]=editor-wait [SERVER]=turn-wait [CHUNK]=streaming [REPLY]=done [WARN]=timeout/stuck [COOKIE]=banner detected. At 9s no activity: warns off-screen Chrome. Post-ask: checks consent cookies.
4. WKCDP COMMANDS (exact paths):
STATUS: powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1
CLEAN STALE (kills idle sessions >2h): powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1 clean
KILL PORT: powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1 kill 9740
COOKIES CHECK: powershell -File D:/GitHub/WKAppBot/bin/wkcdp.ps1 cookies
DETAIL VIEW: powershell -File D:/GitHub/WKAppBot/bin/cdp-mon.ps1
wkcdp output: N session(s) WARN/CRIT flags. Run clean first, then re-check. CRIT=off-screen(false positive possible on left monitor), WARN=drift/age/dup-tabs.
5. QA SCRIPT PATTERN -- state-check first (cheapest): check TGT-POS from cdp-mon. Off-screen (x<-100) = geometry bug = skip eval, auto-file suggest immediately. No Chrome invocation needed.
6. QA SCRIPT PATTERN -- minimized recovery: before eval-js, check if Chrome minimized. If yes: wkappbot a11y restore GRAP, wait 2s, then eval. Minimized Chrome does not render -- DOM checks return false negatives.
7. QA SCRIPT PATTERN -- eval-js (medium cost): wkappbot a11y eval-js 'JS' --grap {proc:chrome,cdp:PORT}. Keep JS under 200 chars. Return null for not-found. Cross-validate with fast heuristic (file size, log keyword). Mismatch = update threshold.
8. QA SCRIPT PATTERN -- auto-suggest on confirmed bugs: wkappbot-core.exe suggest 'BUG: description' with exact port, observed vs expected, repro command. 3 requirements using wkappbot commands only.
9. BUG EVIDENCE WORKFLOW: when filing suggest about ask/CDP failure, run wkask --raw to capture full log. Include [WARN]/[ERROR] lines in suggest text. wkcdp output is ready-made evidence for session anomalies.
10. EXTEND THE PATTERN: any recurring monitor task = new wk-tool script in D:/GitHub/WKAppBot/bin/. Name: wk<domain>.ps1. Add test subcommand that validates heuristic vs eval-js agreement. Claude orchestrates, scripts execute.
11. CDP-MON USAGE: powershell -File D:/GitHub/WKAppBot/bin/cdp-mon.ps1 for detailed per-port view (PID, MB, age, TGT-POS, ACT-POS, drift, tabs, latency). Use when wkcdp output is not detailed enough. Key columns: TGT-POS (where Chrome should be), ACT-POS (where it actually is), DRIFT (delta). CRIT=off-screen, WARN=drift/age/dup-tabs. Note: off-screen detection uses MonitorFromPoint API (fixed 2026-05-14) -- negative coords are valid on left monitors.
