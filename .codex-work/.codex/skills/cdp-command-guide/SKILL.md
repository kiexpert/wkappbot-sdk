---
id: cdp-command-guide
app: wkappbot-workflow
description: "CDP quick-ref: port isolation rule, command table, typical flow, error codes. For step-by-step: cdp-command-guide-howto. For full reference: cdp-command-guide-ref."
tags: [cdp, chrome, guide, cheatsheet, port, grap, beginner]
---

> **Refresh**: `wkappbot skill read cdp-command-guide --if-newer` — v1.42 (2026-05-08)

# CDP Command Guide

## Steps

1. WHAT IS CDP + PORT ISOLATION: wkappbot cdp commands control Chrome via CDP. Each project gets SHA256-derived port (9300-9995). NEVER hardcode 9222. cdp open is the ONLY command that auto-detects/launches Chrome.
2. QUICK COMMANDS: cdp open <url> (launch/reuse Chrome, returns grap) -> navigate GRAP URL -> html GRAP (page source) -> capture GRAP (PNG) -> url/title GRAP -> a11y read GRAP --eval-js '...' -> tabs (list) -> close GRAP.
3. GRAP ALWAYS REQUIRED: all commands except cdp open need {proc:'chrome',cdp:PORT} grap. Get port from cdp open output. Missing grap = error. Bare URL as first arg = error.
4. TYPICAL FLOW: cdp open <url> -> save grap {proc:'chrome',cdp:PORT} -> cdp html GRAP -> cdp navigate GRAP <url2> -> a11y read GRAP --eval-js '...' -> cdp capture GRAP.
5. QUICK ERRORS: grap required = passed URL not grap. not in project CDP block = wrong port. No node with given id = DOM changed, re-read html. CDP:DIALOG:BLOCKED = JS alert auto-dismissed, command retried.
6. next: step-by-step howto (open/navigate/eval/tab-recovery/error-handling): wkappbot skill read cdp-command-guide-howto
7. next: full reference (cdp-mon anomaly codes, RIQUA, PS5.1 gotchas, bug history): wkappbot skill read cdp-command-guide-ref
