# WKAppBot

[![build-launcher](https://github.com/kiexpert/wkappbot-sdk/actions/workflows/build.yml/badge.svg)](https://github.com/kiexpert/wkappbot-sdk/actions/workflows/build.yml)
[![extended-smoke](https://github.com/kiexpert/wkappbot-sdk/actions/workflows/extended-smoke.yml/badge.svg)](https://github.com/kiexpert/wkappbot-sdk/actions/workflows/extended-smoke.yml)
[![Latest Release](https://img.shields.io/github/v/release/kiexpert/wkappbot-sdk?label=download)](https://github.com/kiexpert/wkappbot-sdk/releases/latest)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dot.net)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%2B-lightgrey)](https://github.com/kiexpert/wkappbot-sdk/blob/main/docs/INSTALL.md)

> *Give AI eyes and hands -- let it use any app, so it can truly help humans.*  
> All AI agents welcome -- Claude, GPT, Gemini, Copilot, and beyond.

**Windows & Android app automation framework -- focusless, self-healing, AI-native.**

> **Free tier covers all base automation.** CDP browser automation, multi-AI
> delegation, and `--sudo` admin access are paid Pro tiers. See
> [PRICING.md](./PRICING.md) for the full comparison.

AI models are powerful reasoners, but they live behind a text interface. They can tell you *how* to click a button -- but they can't click it. WKAppBot bridges that gap: it gives AI agents **eyes** (read any UI, extract text, recognize controls) and **hands** (click, type, scroll, invoke -- without stealing focus), so they can operate existing desktop and mobile apps on behalf of the humans who use them.

No app rewrite required. No API access needed. If a human can use it, WKAppBot can automate it.

---

## Why WKAppBot?

Most automation tools steal focus, break on owner-drawn controls, and go silent when the DOM changes. WKAppBot is built around three hard-won principles:

**Focusless-first** -- UIA Invoke/Value/Toggle/Select never steal focus. Win32 PostMessage handles legacy MFC. SendInput is the last resort, used only when nothing else works. The user keeps working while the AI operates in the background.

**Self-healing** -- When UIA fails on owner-drawn controls (MFC/HTS), CCA segmentation + OCR triple cross-validation + Gemini Vision inference recover element identity automatically and cache results in an Experience DB for next time.

**AI-native** -- The same binary that runs automation also delegates to GPT, Gemini, and Claude in parallel, streams prompts into live browser AI sessions via CDP, and manages its own context handoff when token budgets run low.

---

## OS Support

| OS | Status |
|----|--------|
| Windows 11 | ✅ Fully supported |
| Windows 10 22H2+ | ✅ Fully supported |
| Windows 10 < 22H2 | ⚠️ Untested |
| Windows Server 2019+ | ✅ Headless mode (no GUI a11y) |
| macOS / Linux | ❌ Not supported |

---

## What It Automates

| Target | Method |
|--------|--------|
| Modern Windows apps (WPF, UWP, Electron) | UIA Invoke / Value / Toggle / Select patterns |
| Legacy MFC / HTS trading terminals | Win32 PostMessage, WM_CHAR, CMaskEditEx path |
| Web apps (Chrome / Edge) | CDP -- click, type, eval JS, read DOM text |
| Browser AI (Claude, GPT, Gemini) | CDP prompt pump, cross-prompt chunking, attachment lock |
| Android apps | ADB + Accessibility tree (`adb://device/...` grap) |
| Owner-drawn controls with no UIA | CCA segmentation -> OCR -> Vision API fallback chain |

---

## Core Features

### grap -- Universal Element Address

> *Human sees windows; AI points with grap.*

Every UI element gets a single address that works across Win32, UIA, web, and Android:

```
{proc:'chrome', domain:'claude.ai'}#main textarea   # CSS inside a browser window
heroes#realtime-account                             # UIA scope inside a window grap
adb://Fold5/*heromts*#balance                       # Android element
hwnd:0x010B084A                                     # Direct Win32 handle
*notepad*;*calc*                                    # OR pattern
```

`a11y find <grap>` prints a verified `# TARGET "hwnd:0x..."` line -- copy-paste ready for the next command.

### Auto-Pipeline on Every Action
Every `a11y` action runs a smart pre-flight before executing:

```
blocker dismiss -> minimize restore -> tab activate
  -> zoom/magnifier -> execute (3-tier) -> result feedback -> fade
```

No manual "wait for window" boilerplate. Blocking dialogs, minimized windows, and wrong-tab states are handled automatically.

### 5-Tier Element Search
UIA -> Vision Cache -> Simple OCR -> Vision API (Claude) -> Coordinate-based.  
Each tier auto-logs hits to an Experience DB; repeat runs skip expensive tiers.

### AppBot Eye -- Always-On Daemon
A single background process combining:
- **Slack daemon** (Socket Mode) -- live command delivery, thread-slot dashboard
- **MCP broker** -- exposes all CLI commands as JSON-RPC tools for Claude/Codex
- **Hot-swap watchdog** -- detects `wkappbot-core.new.exe`, drains in-flight requests, renames atomically. Zero downtime on `dotnet publish`.
- **Watchdog VBS** -- if Eye itself dies for 2+ minutes, kills orphan cores and restarts.

### Multi-AI Delegation
```bash
wkappbot ask triad "is this approach correct?"      # GPT + Gemini + Claude in parallel
wkappbot ask claude "explain this chart" chart.png  # vision-capable single ask
```
Triad runs thesis-antithesis-synthesis debate. Useful for architecture decisions, bug root causes, and code review -- anything where one model's blind spot is another's strength.

### Skill System
Accumulated operator knowledge lives in versioned skills, queryable at any time:
```bash
wkappbot skill list
wkappbot skill read focusless-first-principle
wkappbot skill read grap
```
Skills capture per-project knowhow -- MFC quirks, CDP gotchas, MTS layout bugs -- so every session starts informed instead of exploring from scratch.

### Suggest-Driven Backlog
```bash
wkappbot suggest "title: description"              # queue a bug or improvement mid-task
wkappbot suggest list                              # review the backlog
wkappbot suggest resolve <ts> "note" --i-completed-... evidence.sh
```
AI agents queue findings without interrupting the current task. Evidence scripts are required to close a suggest -- no unverified resolves.

---

## Installation

**Clone = install.** That's it.

```bash
git clone https://github.com/kiexpert/wkappbot-sdk %USERPROFILE%\Documents\wkappbot
cd %USERPROFILE%\Documents\wkappbot
build.cmd
```

Pre-built binaries are also available if you prefer not to build:

| | |
|---|---|
| [Latest Release](https://github.com/kiexpert/wkappbot-sdk/releases/latest) | `wkappbot-X.Y.Z.zip` — extract anywhere |
| [CI Artifacts](https://github.com/kiexpert/wkappbot-sdk/actions/workflows/build.yml) | Every build → `wkappbot-bin-{run_id}` (90-day retention) |

**Recommended layout** — clone under `Documents\` so your personal automation data (experience DB, logs, skills) stays in your home directory and is easy to find, back up, or exclude from sharing:

> **Why Documents?** WKAppBot learns from your usage and stores UI experience data under `bin\wkappbot.hq\`. Keeping this under your personal Documents folder protects your privacy — it stays separate from shared or version-controlled paths.

```
%USERPROFILE%\Documents\wkappbot\  ← recommended clone location (easy to find in Explorer)
  bin\
    wkappbot.exe                 ← launcher / busybox entry point
    wkappbot-core.exe            ← core worker (hot-swapped on update)
    wkappbot.hq\                 ← runtime data: skills, logs, experience DB (auto-created)
  csharp\
  handlers\
  skills\
  ...
```

Add `bin\` to your `PATH` so `wkappbot` is available in any terminal:

```powershell
# PowerShell (permanent, current user)
[Environment]::SetEnvironmentVariable(
  'PATH',
  "$env:USERPROFILE\Documents\wkappbot\bin;$([Environment]::GetEnvironmentVariable('PATH','User'))",
  'User'
)
```

Build from source (requires .NET SDK 8+):

```bat
build.cmd
```

Verify the install:

```bash
wkappbot --version
wkappbot skill list
```

> **Requirements:** Windows 10 22621+ (64-bit). No separate .NET runtime needed — the binary is self-contained.

---

## Activate License

Free tier works out of the box — no signup. To unlock CDP browser automation,
multi-AI `ask`, `schedule`, or `--sudo` admin access:

```bash
gh auth login              # authenticate with GitHub
wkappbot license status    # confirms current tier (Free until you subscribe)
```

Then follow [SUBSCRIBE.md](./SUBSCRIBE.md) — KIS bank transfer with your GitHub
username as the memo, accept the GitHub collaborator invite, and the same binary
unlocks Pro features within 1 hour. See [PRICING.md](./PRICING.md) for tier details.

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
UIA isolated in a separate MCP worker process -- prevents ConPTY LPC deadlock.

---

## Runtime

```
bin/wkappbot.exe          official launcher  (alias: a11y.exe)
bin/wkappbot-core.exe     core worker        (auto hot-swapped on publish)
bin/wkappbot.hq/          runtime HQ -- experience DB, skills, sessions, logs
```

.NET 8.0 · `net8.0-windows10.0.22621.0` · Korean UI support

---

## References

- [CLAUDE.md](./CLAUDE.md) -- detailed operational guidance for AI agents
- [AGENTS.md](./AGENTS.md) -- shared AI engineering rules
- `wkappbot skill list` -- accumulated knowhow: grap syntax, MFC quirks, CDP gotchas, and more

---

## Encoding Policy

Repository text assets should be treated as UTF-8 by default.

- Prefer UTF-8 when creating or updating `.md`, `.txt`, `.html`, `.json`, `.yml`, `.xml`, and other text-based files.
- If an imported source arrives in another encoding such as CP949, keep the original file for archival purposes and also store a UTF-8 converted copy for reading and editing.
- Use file-editing tools that preserve encoding correctly. If a tool may re-encode content incorrectly, prefer `wkedit` or another UTF-8-safe path.
- Binary files such as `.pdf`, `.mp4`, images, and archives should be preserved as-is and not text-converted.
- Multibyte filenames are allowed, but repository writes should still use UTF-8 so downstream tools and web viewers can read them reliably.
