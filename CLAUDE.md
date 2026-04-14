# WKAppBot v5.13.140 - Windows + Android App Automation Test Framework

## Operating Rules (READ FIRST)

> ⚠️ **LANGUAGE RULE — Korean is ONLY for final responses to the user.**
> EVERYTHING else MUST be in English: source code, comments, CLAUDE.md, AgentsPolicy,
> skills, memory files, commit messages, docs, prompts, internal notes — ALL English.
> Korean uses 2-3x more tokens. One Korean file = wasted budget on every session load.

## Shared Markdown Cross-Reference Rule
- Before making non-trivial changes, read all top-level `*.md` files in the repository root.
- At minimum, check and keep consistent:
  - `README.md`
  - `AGENTS.md`
  - `CLAUDE.md`
- Shared AI policy must not drift between these files.

### Language / Communication
- **Final responses to user: Korean, polite 해요체 (-요 form). NEVER informal speech.**
- Source code / comments / CLAUDE.md / skills / memory / commits / docs → **English only, no exceptions**
- **Questions**: `wkappbot slack send "question"` + send in prompt simultaneously (Slack-only forbidden)
- **Slack replies**: always reply in thread (`--msg TS` if TS available, else `send`)

### AppBotEye
- **Must always be running**: auto-spawned on normal CLI commands (except `eye`/`slack`/`help`/`validate`/`win-move`)
- **Eye = Slack daemon integrated** → no separate `slack listen` needed
- **eye tick**: one-shot status query (includes ctx=N%) / **eye**: FSW hybrid loop
- **Handoff**: `wkappbot newchat "prompt"` — passes context summary to new chat
- **Cro card forbidden!**: OpenClaw(Cro) is a separate service — do not modify. Only Claude cards OK.
- **CWD shorthand**: `W:\GitHub\WKAppBot` → `WG-WKAppBot` / noise filters: `NO_REPLY`, `ㄱㄱ`

### Build & Deploy
```bash
"W:/SDK/dotnet/dotnet" publish 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --verbosity minimal
```
- **Hot-Swap**: publish triggers auto-detect + swap by Eye. **NEVER kill Eye!**
- Auto-publish after any `.cs` edit without waiting for instructions

### Minor Version Bump
→ See [VERSIONING.md](../VERSIONING.md) — commit 3 files together

### Source File Size
~400 lines/file recommended. **WKAppBot code only** (do NOT refactor customer code!)

### Skill Authoring Rules
- **Always use `wkappbot skill contribute` / `wkappbot skill edit` to create/modify skills**
- **NEVER directly edit `.skill.json` files** with Edit/Write/wkedit tools
  - Reason: `|` is the step separator — direct edits split content, miss version bump, corrupt encoding
  - Do not use `|` in step content — use newline instead
- After creating a skill, always verify with `wkappbot skill show <id>`

### Token Efficiency Rules
- **Never re-read a file already in context** — use what you already loaded
- **No speculative tool calls** — only read/search files you have a concrete reason to need
- **Parallelize independent tool calls** — glob + grep + read in parallel when not dependent
- **Don't repeat what the user already explained** — use their words, move on
- **ctx% check**: `wkappbot claude-usage` — handoff at 8MB, urgent at 10MB, use `/compact` when growing
- **Use `qmd search` before reading files** — BM25+vector pre-index of all C# + skills + docs (MCP: qmd)

### Forbidden
- Directly spawning Eye / options that block Claude delivery / options that skip Eye — all forbidden
- Asking user questions in prompt only (must send to Slack simultaneously)

---

## Overview
Windows a11y-based app UI automation. UIA→Win32→SendInput 3-tier fallback, focusless control.
`a11y.exe` = busybox symlink → `wkappbot.exe`

## Architecture

### 5-Tier Element Search
UIA → Vision Cache → Simple OCR → Vision API(Claude) → Coordinate-based

### AppBotPipe / File Tools (v5.8)
All process creation goes through `Spawn()` / `StartTracked()` — ensures CreateProcessW hook
- `Spawn(showNoActivate:true)`: for WPF overlays (WhisperRing/ScreenSaver) — shows window without focus steal
- `Spawn(default)`: `SW_HIDE` — background processes
- **FocusLaunchTracker**: tracks focus-stealing processes (`runtime/focus_launch.json`)
- **Watchdog VBS**: fires when Eye dies 2min+ → kills all wkappbot-core → restarts eye tick
- **File Tools**: `file read/write/edit/grep/glob/undo` support tool-style aliases (`--path`, `--content`, `--pattern`, `--dry-run`)
- **file write**: creates `.bak/` backup by default instead of patch tracking; `a11y file-write`/MCP use same backup path
- **LG overlay guard**: generalized detection of LG Smart Assistant topmost screen-cover windows by process+size instead of fixed `LGDisplayExtension` class

### Eye MCP Architecture (v4.9+)
Eye ↔ MCP worker(Core) JSON-RPC over pipe. a11y/UIA isolated in separate process.
- `ShouldRouteToMcp()`: a11y/inspect/windows/ask → MCP, slack/eye/schedule → in-process
- `DETACHED_PROCESS` flag prevents ConPTY LPC deadlock. Auto-restart max 5/5min
- Slack file-based queue (`runtime/slack_queue/`), drain worker serial processing
- **Launcher quiet-swap**: launcher watches only original `wkappbot-core.exe` path change. `.new.exe` staging/rename is Eye's responsibility.
- **Admin-first swap**: if admin endpoint is alive, defer normal core swap; retry only after admin exits with newer stamp.
- **Failed-stamp skip**: a core `mtime` stamp that failed once is not retried until a newer file arrives.
- **CDP ask prompt pump**: triad/cross-prompt uses per-page singleton prompt pump. Chunks appended then sent on 1s idle; page key = `scope + targetId + editorSelector`.
- **Attachment transaction lock**: if CDP attachment present, acquire page lock and upload attachment first. Chunks arriving during lock are queued; after upload completes, append queue text + immediate flush, then unlock.

### Self-Healing DYN-A11Y
MFC owner-drawn with no UIA → CCA segmentation → OCR triple cross-validation → Gemini Vision inference
→ `dyn_r{row}c{col}` dynamic ID + Experience DB cache + CCA parameter auto-tuning

### v5.13.133~140 (2026-04-10, Session 8)
- **Core worker self-log** (v5.13.133): `IsLauncherInParentChain()` WMI walk — if Launcher not in ancestor chain, Core auto-creates `WorkerStderrTeeWriter` log. Covers Eye-spawned workers (claude-proxy, whisper-ring, analyze-hack) that bypass Launcher.
- **Launcher stderr relay hardening** (v5.13.136): buffers last-20 stderr lines + captures `[LOG]` path; writes `{source:launcher}` fallback `errors.jsonl` entry on non-zero exit. Raw-byte sentinel detection (`\0UIT`) + StringBuilder line accumulation.
- **AppBotQuitFlush** (v5.13.137): unified Launcher exit function — `stderrRelay.Wait(2000)` → `AppBotExit(code)` → flush + TerminateSelf. All bare `TerminateSelf` calls replaced.
- **Core stderr flush** (v5.13.140): `Console.Error.Flush()` added to ProcessExit hook, `DoRequiredExitCleanup`, and `WriteExitFile` (`FlushFileBuffers(STD_ERROR_HANDLE)`). Critical for MCP loop which has no `_activeTee`.
- **claude-proxy** (v5.13.130): local Anthropic API proxy on port 7788. Always-on `[PROXY] ← STATUS (Nms)` logging. `ANTHROPIC_BASE_URL=http://localhost:7788` in `~/.claude/settings.json`. Context-limit → auto Gemini handoff.
- **Skills added**: `core-worker-logging-launcher-chain` (13-step logging architecture guide), `a11y-command-cheatsheet` v2.11 (ambiguous grap auto-resolution via L1 TargetAmbiguityGuard documented)

### v5.11.101+ (2026-04-08, Session 7)
- **# TARGET output redesign** (copy-paste-ready grap):
  - `# TARGET "hwnd:0xXXXX[#absTagPath]" [OK] Nms  proc=name  {compact-context}`
  - bash-safe: `hwnd:0x...` as primary paste token, JSON5 as context suffix
  - `BuildAbsoluteTagPath`: UIA type 3-char abbreviations (`Document→Doc`, `Group→Gro`, `Edit→Edi`)
  - `BuildCompactWinGrap`: strips title/url, browser→domain(no cls), legacy→cls
  - `[OK]/[MISS]` verification + timing ms
  - window title → stderr
  - hack-hover same format + `BuildCompactWinGrap` shared function
- **FormatNodeLabel unified**: AutomationElement overload → fully delegates to string overload
  - name wrapped in single quotes `<Button'OK'>`, removed 17-char truncation
  - `→ #scope` hint right of TARGET tag when UIA scope accessible
  - rect `ltwh=` attr included; VERDICT ✓ suppressed (only ⚠/? output)
- **KNOWHOW/ShowKnowhowBroadcast/ShowKnowhowHint** → all stderr
- **UiaLocator.GetAbsoluteTagPath()** added (exposes automation tree walker)

### v5.11.74~100 (2026-04-08, Session 6)
- **stdout noise elimination**: 124 Commands/*.cs files bulk-replaced `Console.WriteLine($"["` → stderr (wkedit bulk)
- **ClickZoomHelper**: `[ZOOM:]` Console.Write → Console.Error.Write (all stderr)
- **a11y find output restructure**:
  - first line `# FOCUS {grap}` + `# TARGET {grap}` stdout (copy-paste ready)
  - CURSOR section: run-length encode consecutive identical unnamed nodes (`<Group> x7`)
  - TARGET section: deduplicated header grap
- **BuildTargetJson5 field order**: `hwnd,pid,proc,domain,title,...` (proc/domain moved forward)
- **ErrorScope**: error-pattern immediate passthrough, ErrorDetected flag, exit-0+error → -9999
- **Skills added**: `grap` (10-step UI address system guide), `wkedit` v1.1 (bulk transaction edit)
- **Skill rule**: no direct `.skill.json` edits — must use `skill contribute/edit` commands

### v5.11 new (2026-04-05)
- **TargetAmbiguityGuard 6-Layer**: AI target ambiguity detection + auto-guidance system
  - L1: ambiguous pattern matches multiple windows → list + `[OK]` verify + hwnd auto-heal
  - L2: modal alert blocking → auto-find inside alert (UIA + Win32 buttons)
  - L3: FOCUSED/LEAF terminal node → grap target guidance on all commands
  - L4: Win32 child window not found → sibling window listing (MFC/HTS)
  - L5: element action without #scope → root children + focus chain
  - L6: UIA scope element not found → list available elements
- **CDP fixed delay → state-based polling**: triad consensus implementation
  - `WaitForWindowStateAsync` shared helper (50ms interval windowState polling)
  - SetWindowBoundsAsync, EnsureMinimizedAsync, SwitchToTargetAsync, EnsureCorrectWindowAsync all replaced
  - WebOpenCommand: Thread.Sleep(1-2s) → document.readyState polling
- **BuildTargetJson5 improvement**: title 50-char truncate, removed ellipsis (ensures substring matching)

### v5.11.37~73 (2026-04-06, Session 5)
- **hack-hover major improvements**:
  - Live overlay: root window size, full UIA descendant dotted lines
  - Target parent chain 2x thick neon dotted highlight
  - Blind window (no UIA / no experience DB) immediately fires hack (non-cancellable)
  - Experience DB FSW: overlay refreshes immediately on file change
  - Hot-swap: TryRenameSwap detects .new.exe → re-spawns Launcher
  - Console: output every tick, Eye: change+analysis only (auto-detects RunningInEye)
  - Screen reader auto-activation (boosts Chromium UIA tree)
- **hack-input worker**: keyboard focus analysis (UIA GetFocusedElementInfo)
- **TypeViaIme**: IMM32 focusless text input Tier 2 integration
  - AttachThreadInput + ImmSetCompositionString + CPS_COMPLETE
  - State backup/verify/restore (restores previous composition on failure)
- **auto-hack process isolation**: Eye A11yHackCommand → AppBotPipe.Spawn (prevents memory leak)
- **CDP EvalAsync**: 2→3 retries + 500ms/1000ms exponential backoff
- **pump watchdog**: 3s→5s + secondary decrease check (prevents false positives)
- **Overlay improvements**: removed translucent fill entirely, Known/Cached alpha 10%, atomic swap render
- **CommandHelpMap**: speak, hack, hack-hover, hack-input help registered

### v5.10 (2026-04-04)
- **Eye pipe 100ms timeout**: `firstOutputTimeoutMs` — applied to most commands except slack/ask/newchat. Auto-fallback to Core if Eye is slow
- **TryRenameSwap shared function**: Eye main loop + startup gentle-swap + `wkappbot hotswap` CLI — same logic
- **OcrCorrectionDb**: pixel-hash → correct answer self-learning dictionary (WKAppBot.Vision). UIA verify auto-learns, OCR results auto-corrected
- **suggest show/get/view**: query full content by number/ts. Unknown subcommand guard (noise prevention)
- **suggest resolve safety**: duplicate guard backup fallback, .manifest delete detection+auto-restore, evidence preserved on failure
- **CDP Input.* minimize removed**: CDP renderer direct delivery → minimize unnecessary. Only IsFocusStealingMethod minimizes
- **Runtime.enable retry**: EnableRuntimeWithRetry (max 4x exponential backoff)
- **Log routing fix**: Eye pipe commands → per-command `old {cmd}/` folder. Typos → `old unknown/`
- **auto-hack log noise removed**: TeeWriter suppressed with WKAPPBOT_WORKER=1
- **Encoding corruption watch**: U+FFFD detection after file edit + auto Slack alert
- **a11y hack top-right node name**: UIA AutomationId/Name displayed inside segment top-right

### Sunset Screensaver + WhisperRing
Separate processes (WPF memory isolation, parent PID watch → auto-exit when Eye exits)

### Project Structure
```
csharp/src/
├── WKAppBot.CLI/     # CLI entry + Commands/
├── WKAppBot.Core/    # ScenarioRunner, ActionExecutor
├── WKAppBot.Win32/   # NativeMethods, WindowFinder, UiaLocator, Input/*
├── WKAppBot.Vision/  # ChartAnalyzer, SimpleOcrAnalyzer, VisionCache
├── WKAppBot.WebBot/  # CdpClient, ChromeLauncher, SlackSocketClient
├── WKAppBot.Android/ # AdbClient, AndroidA11yTree
handlers/ scenarios/
```

---

## CLI Commands
> **`<grap>`** = glob/regex/OR(`;`)/`#UIA-scope`/`{JSON5}` multi-field AND

```
wkappbot a11y <action> <grap>[#scope] [options]   # ★ unified standard (24 actions)
  inspect / find / windows / screenshot / ocr     # Discovery
  close / minimize / maximize / restore / focus / move / resize
  click / invoke / toggle / expand / collapse / select / scroll
  type [--hotkey] / set-value / set-range / read
  wait [--condition/--not] / clipboard[-read/-write]
  --eval-js "js"  --all  --nth N  --force  --timeout N  --speak
wkappbot a11y kill <pattern>[/<ancestor>]
wkappbot windows [grap] [--deep] [--process <name>] [--cmd <substr>]
wkappbot slack send "msg" [file.png]  /  reply "msg" --msg TS
wkappbot eye / eye tick
wkappbot newchat "prompt" [--file f.txt]
wkappbot ask gpt|gemini|claude|triad "question" [file.png]
wkappbot logcat [regex] [file.log ...] [--past Nh] [-f] [--timeout N] [--json]
wkappbot file read/grep/glob/edit <args>
wkappbot gc [pattern] [--days N] [--sweep]
wkappbot claude-usage                             # JSONL size + ctx%
wkappbot mcp                                     # MCP stdio server
wkappbot <cmd> --help / --regression
```

## grap Patterns
| Syntax | Example |
|--------|---------|
| Wildcard | `"*Button*"` |
| OR | `"*notepad*;*calc*"` |
| #UIA-scope | `"heroes#realtime-account"` |
| Tab portal | `"Chrome#ChatGPT#model"` |
| JSON5 | `{proc:'chrome',domain:'chatgpt.com',title:'Claude'}` |
| Direct hwnd | `hwnd:0x010B084A` |
| adb | `"adb://Fold5/*heromts*#balance"` |

→ Full grap syntax: `wkappbot skill show grap`
→ `a11y find <grap>` stdout first line `# TARGET "hwnd:0x..."` — copy-paste ready
→ **Accumulated knowhow**: `wkappbot skill list` — search skills first when stuck, then ask triad

---

## Key Design Decisions

### Focusless-First
UIA Invoke/Value/Toggle/Select = Focusless. SendInput/Hotkey requires EnsureFocus.
WPF overlay uses `Spawn(showNoActivate:true)` → SW_SHOWNOACTIVATE(4), no focus steal.

### PromptDeliveryContext
Before prompt injection: ① target foreground? ② recent 30s input?
→ auto-decides `Focusless` / `FocusSteal` / `Skip` / `Abort`

### HTS Automation
MFC controls: almost no UIA patterns → Win32 message fallback required. Heroes owner-drawn → OCR fallback.

### Tag Conventions
`[WATCH]` `[RUN]` `[FOCUS]` `[VERIFY]` `[BLOCK]` `[GUARD]` `[ZOOM]` `[SLACK]` `[EXP]` `[KNOWHOW]`

---

## Session Management (Claude Code Tips)
- `wkappbot claude-usage` → JSONL size + ctx%
- **ctx% = JSONL ÷ ~20MB** — prepare handoff at 8MB, immediate handoff at 10MB
- **Goal**: ~10MB or less per session. Aggressive token optimization!
- **Handoff**: `wkappbot newchat "prompt"` — passes work summary to new chat
- **MEMORY.md**: 200-line limit. Overflow → split into `memory/` topic files

---

## YAML Scenario (summary)
```yaml
scenario: { name: "Test" }
app: { launch: "calc.exe", wait_for_window: { title_contains: "Calculator" } }
steps:
  - { name: "Click", target: { automation_id: "plusButton" }, action: click }
  - { name: "Verify", target: { automation_id: "CalculatorResults" }, action: assert,
      params: { type: text_contains, expected: "42" } }
```
Supported actions: click/double_click/right_click/type_text/press_key/hotkey/wait/assert/scroll/screenshot/toggle/expand/collapse/select/set_range/window_close/minimize/maximize

## Deploy Structure
```
W:/SDK/bin/wkappbot.exe / a11y.exe / wkappbot.hq/
```

## References
- `README.md`
- `AGENTS.md`
- **MEMORY.md** / **memory/**: build commands, architecture decisions, gotchas detail
- .NET 8.0 `net8.0-windows10.0.22621.0`, Korean UI support
