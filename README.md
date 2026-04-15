# WKAppBot

> *Building the Eyes of Claude to realize autonomous secretarial ops.*  
> All AI agents welcome — Claude, GPT, Gemini, Copilot, and beyond.

**Windows & Android app automation framework — focusless, self-healing, AI-native.**

WKAppBot automates any desktop or mobile UI without requiring the target app to be in the foreground. It speaks UIA, Win32, and CDP natively, falls back gracefully across tiers, and exposes a single unified CLI that Claude, Codex, and human operators all speak the same way.

---

## Why WKAppBot?

Most automation tools steal focus, break on owner-drawn controls, and go silent when the DOM changes. WKAppBot is built around three hard-won principles:

**Focusless-first** — UIA Invoke/Value/Toggle/Select never steal focus. Win32 PostMessage handles legacy MFC. SendInput is the last resort, used only when nothing else works.

**Self-healing** — When UIA fails on owner-drawn controls (MFC/HTS), CCA segmentation + OCR triple cross-validation + Gemini Vision inference recover element identity automatically and cache results in an Experience DB for next time.

**AI-native** — The same binary that runs automation also delegates to GPT, Gemini, and Claude in parallel, streams prompts into live browser AI sessions via CDP, and manages its own context handoff when token budgets run low.

---

## What It Automates

| Target | Method |
|--------|--------|
| Modern Windows apps (WPF, UWP, Electron) | UIA Invoke / Value / Toggle / Select patterns |
| Legacy MFC / HTS trading terminals | Win32 PostMessage, WM_CHAR, CMaskEditEx path |
| Web apps (Chrome / Edge) | CDP — click, type, eval JS, read DOM text |
| Browser AI (Claude, GPT, Gemini) | CDP prompt pump, cross-prompt chunking, attachment lock |
| Android apps | ADB + Accessibility tree (`adb://device/...` grap) |
| Owner-drawn controls with no UIA | CCA segmentation → OCR → Vision API fallback chain |

---

## Core Features

### grap — Universal Element Address

> *Human sees windows; AI points with grap.*

Every UI element gets a single address that works across Win32, UIA, web, and Android:

```
{proc:'chrome', domain:'claude.ai'}#main textarea   # CSS inside a browser window
heroes#realtime-account                             # UIA scope inside a window grap
adb://Fold5/*heromts*#balance                       # Android element
hwnd:0x010B084A                                     # Direct Win32 handle
*notepad*;*calc*                                    # OR pattern
```

`a11y find <grap>` prints a verified `# TARGET "hwnd:0x..."` line — copy-paste ready for the next command.

### Auto-Pipeline on Every Action
Every `a11y` action runs a smart pre-flight before executing:

```
blocker dismiss → minimize restore → tab activate
  → zoom/magnifier → execute (3-tier) → result feedback → fade
```

No manual "wait for window" boilerplate. Blocking dialogs, minimized windows, and wrong-tab states are handled automatically.

### 5-Tier Element Search
UIA → Vision Cache → Simple OCR → Vision API (Claude) → Coordinate-based.  
Each tier auto-logs hits to an Experience DB; repeat runs skip expensive tiers.

### AppBot Eye — Always-On Daemon
A single background process combining:
- **Slack daemon** (Socket Mode) — live command delivery, thread-slot dashboard
- **MCP broker** — exposes all CLI commands as JSON-RPC tools for Claude/Codex
- **Hot-swap watchdog** — detects `wkappbot-core.new.exe`, drains in-flight requests, renames atomically. Zero downtime on `dotnet publish`.
- **Watchdog VBS** — if Eye itself dies for 2+ minutes, kills orphan cores and restarts.

### Multi-AI Delegation
```bash
wkappbot ask triad "is this approach correct?"      # GPT + Gemini + Claude in parallel
wkappbot ask claude "explain this chart" chart.png  # vision-capable single ask
```
Triad runs thesis-antithesis-synthesis debate. Useful for architecture decisions, bug root causes, and code review — anything where one model's blind spot is another's strength.

### Skill System
Accumulated operator knowledge lives in versioned skills, queryable at any time:
```bash
wkappbot skill list
wkappbot skill show focusless-first-principle
wkappbot skill show grap
```
Skills capture per-project knowhow — MFC quirks, CDP gotchas, MTS layout bugs — so every session starts informed instead of exploring from scratch.

### Suggest-Driven Backlog
```bash
wkappbot suggest "title: description"              # queue a bug or improvement mid-task
wkappbot suggest list                              # review the backlog
wkappbot suggest resolve <ts> "note" --i-completed-... evidence.sh
```
AI agents queue findings without interrupting the current task. Evidence scripts are required to close a suggest — no unverified resolves.

---

## CLI at a Glance

```bash
# Discovery
wkappbot a11y inspect <grap>           # dump UIA tree
wkappbot a11y find <grap>              # resolve + print # TARGET
wkappbot a11y highlight <grap>         # visualize with zoom overlay
wkappbot windows [grap]                # list matching top-level windows
wkappbot scan <window-title>           # scan app structure, build Experience DB

# Interaction (focusless where possible)
wkappbot a11y click  <grap>
wkappbot a11y type   <grap> "text"
wkappbot a11y read   <grap>            # CDP-first on browser windows, UIA fallback
wkappbot a11y scroll <grap> down 3
wkappbot dismiss "<window-title>"      # auto-dismiss popups (OCR importance check)

# Browser / CDP
wkappbot a11y read   "{proc:'chrome'}"                   # full page text via CDP
wkappbot a11y click  ".submit-btn"                       # CSS selector
wkappbot a11y read   "{proc:'chrome'}" --eval-js "document.title"

# Scenarios & high-level automation
wkappbot run scenario.yaml             # run a YAML test scenario
wkappbot do "<window>" <form> <button> # full combo-select + click + dialog flow

# AI delegation
wkappbot ask triad  "is this lock-free?"
wkappbot ask claude "explain this chart" chart.png

# Daemon & context
wkappbot eye tick                      # one-shot status + ctx%
wkappbot newchat "continue from: ..."  # handoff to fresh session
wkappbot claude-usage                  # JSONL size + context %
```

---

## YAML Scenarios

Describe multi-step test flows in plain YAML:

```yaml
scenario: { name: "Order placement" }
app: { launch: "trading.exe", wait_for_window: { title_contains: "HTS" } }
steps:
  - { name: "Enter stock code", target: { automation_id: "codeEdit" }, action: type_text, params: { text: "005930" } }
  - { name: "Click buy",        target: { name: "매수" },               action: click }
  - { name: "Verify",           target: { automation_id: "resultLabel" }, action: assert,
      params: { type: text_contains, expected: "주문완료" } }
```

---

## Architecture

```
WKAppBot.CLI        CLI entry, command routing, grap resolution
WKAppBot.Core       ScenarioRunner, ActionExecutor, AAR readiness
WKAppBot.Win32      NativeMethods, WindowFinder, UiaLocator, SendInput tiers
WKAppBot.Vision     ChartAnalyzer, SimpleOcrAnalyzer, VisionCache, CCA
WKAppBot.WebBot     CdpClient, ChromeLauncher, SlackSocketClient
WKAppBot.Android    AdbClient, AndroidA11yTree
WKAppBot.Launcher   Hot-swap staging, pipe relay, admin elevation
```

Eye (always-on) ↔ MCP worker (Core) over JSON-RPC named pipe.  
UIA isolated in a separate MCP worker process — prevents ConPTY LPC deadlock.

---

## Runtime

```
bin/wkappbot.exe          official launcher  (alias: a11y.exe)
bin/wkappbot-core.exe     core worker        (auto hot-swapped on publish)
bin/wkappbot.hq/          runtime HQ — experience DB, skills, sessions, logs
```

.NET 8.0 · `net8.0-windows10.0.22621.0` · Korean UI support

---

## References

- [CLAUDE.md](./CLAUDE.md) — detailed operational guidance for AI agents
- [AGENTS.md](./AGENTS.md) — shared AI engineering rules
- `wkappbot skill list` — accumulated knowhow: grap syntax, MFC quirks, CDP gotchas, and more
