---
id: cdp-command-guide
app: wkappbot-workflow
description: "Beginner-friendly guide to all wkappbot cdp commands: open, navigate, html, capture, eval, tabs. Explains port isolation, grap format, typical session flow, and common errors."
tags: [cdp, chrome, guide, cheatsheet, navigate, html, capture, port, grap, beginner]
---

> **Refresh**: `wkappbot skill read cdp-command-guide --if-newer` — v1.34 (2026-05-08)

# CDP Command Complete Guide

## Steps

1. WHAT IS CDP HERE: wkappbot cdp commands control Chrome via Chrome DevTools Protocol. Chrome is launched per-project with an isolated SHA256-derived port (9300-9995). Every cdp command talks to YOUR project's Chrome only.
2. FIRST STEP ALWAYS -- cdp open: wkappbot cdp open <url> returns OK {proc:'chrome',cdp:9300,hwnd:0x...,domain:'...'}. ONLY command that auto-detects/launches Chrome. SAVE the cdp:{port} from output.
3. cdp:{port} IN GRAP IS MANDATORY for all other commands: {proc:'chrome',cdp:9300}. Omitting it = error. Bare URL as first arg = error. Copy port from cdp open output.
4. NAVIGATE: wkappbot cdp navigate '{proc:"chrome",cdp:9300}' 'https://example.com' -- waits for load. Reuses existing tab or opens new one if host changed.
5. READ HTML: wkappbot cdp html '{proc:"chrome",cdp:9300}' -- returns full outerHTML for scraping/inspection.
6. CAPTURE: wkappbot cdp capture '{proc:"chrome",cdp:9300}' -- saves PNG screenshot, prints path.
7. URL+TITLE: wkappbot cdp url / cdp title with grap -- current page URL or document title.
8. EVAL JS: wkappbot a11y read '{proc:"chrome",cdp:9300}' --eval-js 'document.title' -- a11y eval is removed, always use a11y read --eval-js.
9. TABS: wkappbot cdp tabs -- list all tabs. wkappbot cdp close <grap> -- close current tab.
10. PORT FORMULA: SHA256(project-git-root)[0:4] % 174 * 4 + 9300, aligned to 0xFFFC. Same project = same port always. Wrong port = 'not in project CDP block' error.
11. TYPICAL FLOW: cdp open <url> -> save grap -> cdp html <grap> -> cdp navigate <grap> <url2> -> a11y read <grap> --eval-js '...' -> cdp capture <grap>.
12. ERRORS: 'grap required' = passed URL not grap. 'not in project CDP block' = wrong port. 'No node with given id' = DOM changed, re-read html. '[CDP:DIALOG:BLOCKED]' = JS alert auto-dismissed, command retried.
13. LOCATION: D:\GitHub\WKAppBot\bin\cdp-mon.ps1 (also scripts/monitor-cdp.ps1 in wkappbot-sdk repo).
USAGE: cdp-mon.ps1 [-f] [-i N] [-tabs] [-nofix]
  -f: follow/refresh mode  -tabs: per-tab detail + idle + alert  -nofix: skip IME auto-kill
14. cdp-mon ANOMALY REPORT: [CRIT] offscreen TGT-POS(x<-100) / LAT=DEAD / JS-alert / MEM>2GB. [WARN] sessions>5 / tabs>6 / drift>200px / lat>80ms / age>10h / MEM>1GB / dups>2. [INFO] CWD unknown.
15. cdp-mon LAUNCHED-BY (3-tier chain): (1) alive parent PID -- walk up process chain until wkappbot-core.exe found, parse cmdline for subcmd (cdp open/navigate/eval etc). (2) dead parent -- glob HQ/logs/wkappbot-core.exe.out-*.CMD.pid=PID.log; filename encodes command so no log read needed. (3) Chrome URL fallback -- extract hostname from active tab URL (gemini.google.com -> ask gemini, chatgpt.com -> ask gpt). PS5.1 gotcha: switch -Wildcard incompatible with scriptblock cases -- use if/elseif chain instead.
16. cdp-mon JS ALERT detection: Get-TabIdle connects WebSocket (200ms timeout); if connect OK but Runtime.evaluate times out (300ms) -> Alert=true. JS alerts block all CDP eval -- tab shows LAT=ALERT in report. Post-command alerts (fired after wkappbot exits) are NOT detected by cdp-mon; only stale alerts blocking at poll time are caught. Fix: wkappbot cdp eval '{cdp:PORT}' 'window.onbeforeunload=null' to clear blocking handlers.
17. cdp-mon IME DAEMON: Watch-ImeDaemon counts *ime-relay-daemon* processes; if 2+ found, auto-kills oldest by StartTime (unless -nofix). Root cause of duplicates: HasRealImeContext(WT hwnd) was returning true on CASCADIA_HOSTING_WINDOW_CLASS (ConPTY) because ImmGetContext returns non-null even though IME is non-functional. Fix (commit 139cf6fb): check UnicodeWindowClasses list BEFORE ImmGetContext -- if class matches WT/Chrome/WinRT, return false immediately.
18. cdp-mon PS5.1 GOTCHAS: (1) localhost resolves to IPv6 on Win11 but Chrome CDP binds 127.0.0.1 only -- always use http://127.0.0.1:PORT/json with 'Host: localhost' header. (2) switch -Wildcard incompatible with scriptblock cases in PS5.1 -- use if/elseif chain. (3) ?? null-coalescing not available in PS5.1 -- use if/else. (4) Nullable struct: .Value.L fails -- PS unwraps nullable automatically, use .L.
19. cdp-mon TAB ACCUMULATION BUG (gemini-whisper, fixed fe65cbf0): WhisperStudy was passing --target-tag {engine}-whisper to EnsureCdpConnection, causing sandbox-miss on wrong Chrome instance (HTS port 9712 instead of project port). Fix: removed --target-tag arg entirely -- EnsureCdpConnection URL-matches existing Gemini tab by host, no new tab creation. Symptom: cdp-tab-growth.jsonl shows repeated gemini-whisper sandbox-miss-create entries on unexpected port.
20. DIAGNOSTIC TOOL: when CDP anomaly suspected (wrong position, white screen, tab accumulation, LAT=DEAD), first run: powershell -File D:/GitHub/WKAppBot/bin/wkcdp-mon.ps1 -- shows all sessions with TGT-POS vs ACT-POS, DRIFT, tab count, latency. CRIT flags = immediate action. No CRIT = check drift value and tab DUPs.
21. Verified 2026-05-18: test-cdp-tabs-translate-feature-flag.sh (resolve ts=2026-05-17T20:23:43.7809669Z)
22. POST-REBOOT RECOVERY (2026-05-18): After hard reboot, cdp open may hang/fail due to (1) zombie Chrome holding project port from prior session, (2) SQLite *.lock / *-journal / LOCK / LOG.old files in chrome-profiles/cdp=PORT/ from unclean shutdown. Fix in cdp open: before launch, enumerate chrome procs, kill ONLY PIDs whose cmdline contains --remote-debugging-port=<our-port> (NOT all chrome), then delete *.lock *-journal LOCK LOG.old from profile dir (PRESERVE Cookies / Login Data / Local State -- destroying these wipes login session). Suggests fixed: 2026-05-18T03:31:29 + 2026-05-14T04:09:20 + 2026-05-12T15:16:53.
23. Verified 2026-05-18: test-suggest-2026-05-18T09-17-39-real-site-cdp-a11y.sh (resolve ts=2026-05-18T09:17:39)
24. Verified 2026-05-18: test-cdp-open-zombie-port-cleanup.sh (resolve ts=2026-05-18T03:31:29)
25. TAB TARGETING: after cdp open, use 'wkappbot cdp tabs --port N' to list all open tabs with their cid (short hex ID). To target a specific tab use grap {proc:'chrome',cdp:PORT,cid:HEXID} -- NOT the hwnd from cdp open (which may point to wrong tab after navigation). Pattern: (1) cdp open URL -> gets PORT. (2) cdp tabs --port PORT -> find target tab cid. (3) use {proc:'chrome',cdp:PORT,cid:HEXID} for all subsequent commands on that tab.
26. EVAL-JS VIA A11Y (2026-05-20): cdp commands (navigate/html/capture) work even when Chrome is minimized. BUT wkappbot a11y read --eval-js requires Chrome ON-SCREEN. Warning signal: '[CALLER:RESOLVE] preferred off-screen/invalid' = eval-js will silently fail (returns a11y tree but not JS result). Fix: (1) wkappbot a11y focus {hwnd:MAIN_HWND,proc:chrome} to bring Chrome to foreground. (2) Use Chrome_WidgetWin_1 hwnd (from 'wkappbot a11y list {proc:chrome}'), NOT Chrome_RenderWidgetHostHWND from cdp open. (3) Include #Doc_RootWebArea in grap. (4) --eval-js returns nothing to stdout -- execution only. For reading values: wkappbot cdp html {grap} -o out.html then grep.
27. CHROME OFF-SCREEN AUDIO: wkappbot CDP Chrome opens at -1758,-1010 (off-screen) by default. YouTube will play audio even when window is invisible. To kill hidden Chrome: wkappbot a11y kill '{proc:chrome,cdp:PORT}' -- kills the browser process and all children. cdp html returns empty body for raw XML/JSON responses (Chrome native viewer). eval-js cannot inject+read in same call -- use DOM injection then cdp html separately.
28. yt-dlp for YouTube transcript: codex exec --model codex-mini approach. yt-dlp requires non-wk path so goes through codex. skill-pending lock must be cleared separately before each codex exec -- chaining wkappbot skill edit AND codex exec in one bash call still triggers the block (guard evaluates both patterns on full cmd string). Two separate Bash calls required.
