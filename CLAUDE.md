# WKAppBot v5.14.12 - Windows + Android App Automation Test Framework

## Operating Rules (READ FIRST)

> !️ **LANGUAGE RULE -- Korean is ONLY for final responses to the user.**
> EVERYTHING else MUST be in English: source code, comments, CLAUDE.md, AgentsPolicy,
> skills, memory files, commit messages, docs, prompts, internal notes -- ALL English.
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
- Source code / comments / CLAUDE.md / skills / memory / commits / docs -> **English only, no exceptions**
- **Questions**: `wkappbot slack send "question"` + send in prompt simultaneously (Slack-only forbidden)
- **Slack replies**: always reply in thread (`--msg TS` if TS available, else `send`)

### AppBotEye
- **Must always be running**: auto-spawned on normal CLI commands (except `eye`/`slack`/`help`/`validate`/`win-move`)
- **Eye = Slack daemon integrated** -> no separate `slack listen` needed
- **eye tick**: one-shot status query (includes ctx=N%) / **eye**: FSW hybrid loop
- **Handoff**: `wkappbot newchat "prompt"` -- passes context summary to new chat
- **Cro card forbidden!**: OpenClaw(Cro) is a separate service -- do not modify. Only Claude cards OK.
- **CWD shorthand**: `D:\GitHub\WKAppBot` -> `WG-WKAppBot` / noise filters: `NO_REPLY`, `ㄱㄱ`

### Build & Deploy
```bash
"C:/Program Files/dotnet/dotnet" publish 'D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --verbosity minimal
```
- **Hot-Swap**: publish triggers auto-detect + swap by Eye. **NEVER kill Eye!**
- Auto-publish after any `.cs` edit without waiting for instructions

### Minor Version Bump
-> See [VERSIONING.md](../VERSIONING.md) -- commit 3 files together

### Source File Size
~400 lines/file recommended. **WKAppBot code only** (do NOT refactor customer code!)

### Skill Authoring Rules
- **Always use `wkappbot skill contribute` / `wkappbot skill edit` to create/modify skills**
- **NEVER directly edit `.skill.json` files** with Edit/Write/wkedit tools
  - Reason: `|` is the step separator -- direct edits split content, miss version bump, corrupt encoding
  - Do not use `|` in step content -- use newline instead
- After creating a skill, always verify with `wkappbot skill read <id>`

### Suggest-Driven Backlog
- **Spot a bug or improvement during another task?** Don't interrupt -- queue it: `wkappbot suggest "title: description"`
- Resolve suggests when there's spare time between tasks (`wkappbot suggest list` -> `suggest resolve`)
- This keeps the current task focused and nothing slips through the cracks

### Token Efficiency Rules
- **Never re-read a file already in context** -- use what you already loaded
- **No speculative tool calls** -- only read/search files you have a concrete reason to need
- **Parallelize independent tool calls** -- glob + grep + read in parallel when not dependent
- **Don't repeat what the user already explained** -- use their words, move on
- **ctx% check**: `wkappbot claude-usage` -- handoff at 8MB, urgent at 10MB, use `/compact` when growing
- **Use `qmd search` before reading files** -- BM25+vector pre-index of all C# + skills + docs (MCP: qmd)

### Forbidden
- Directly spawning Eye / options that block Claude delivery / options that skip Eye -- all forbidden
- Asking user questions in prompt only (must send to Slack simultaneously)
- **Bandaid/workaround code (땜빵)** -- if you find yourself duplicating existing logic, adding local "; OR" splits, or wrapping a proper function with ad-hoc retry loops, STOP. Fix the shared function instead. Bandaids compound: future Claude/Codex sessions burn tokens untangling your workaround, then re-add their own on top. Rule of thumb: if the same concept exists elsewhere in the codebase, reuse it -- don't reinvent a narrower version.

---

## Overview
Windows a11y-based app UI automation. UIA->Win32->SendInput 3-tier fallback, focusless control.
`a11y.exe` = busybox symlink -> `wkappbot.exe`

## Architecture

### 5-Tier Element Search
UIA -> Vision Cache -> Simple OCR -> Vision API(Claude) -> Coordinate-based

### AppBotPipe / File Tools (v5.8)
All process creation goes through `Spawn()` / `StartTracked()` -- ensures CreateProcessW hook
- `Spawn(showNoActivate:true)`: for WPF overlays (WhisperRing/ScreenSaver) -- shows window without focus steal
- `Spawn(default)`: `SW_HIDE` -- background processes
- **FocusLaunchTracker**: tracks focus-stealing processes (`runtime/focus_launch.json`)
- **Watchdog VBS**: fires when Eye dies 2min+ -> kills all wkappbot-core -> restarts eye tick
- **File Tools**: `file read/write/edit/grep/glob/undo` support tool-style aliases (`--path`, `--content`, `--pattern`, `--dry-run`)
- **file write**: creates `.bak/` backup by default instead of patch tracking; `a11y file-write`/MCP use same backup path
- **LG overlay guard**: generalized detection of LG Smart Assistant topmost screen-cover windows by process+size instead of fixed `LGDisplayExtension` class

### Eye MCP Architecture (v4.9+)
Eye ↔ MCP worker(Core) JSON-RPC over pipe. a11y/UIA isolated in separate process.
- `ShouldRouteToMcp()`: a11y/inspect/windows/ask -> MCP, slack/eye/schedule -> in-process
- `DETACHED_PROCESS` flag prevents ConPTY LPC deadlock. Auto-restart max 5/5min
- Slack file-based queue (`runtime/slack_queue/`), drain worker serial processing
- **Launcher quiet-swap**: launcher watches only original `wkappbot-core.exe` path change. `.new.exe` staging/rename is Eye's responsibility.
- **Admin-first swap**: if admin endpoint is alive, defer normal core swap; retry only after admin exits with newer stamp.
- **Failed-stamp skip**: a core `mtime` stamp that failed once is not retried until a newer file arrives.
- **CDP ask prompt pump**: triad/cross-prompt uses per-page singleton prompt pump. Chunks appended then sent on 1s idle; page key = `scope + targetId + editorSelector`.
- **Attachment transaction lock**: if CDP attachment present, acquire page lock and upload attachment first. Chunks arriving during lock are queued; after upload completes, append queue text + immediate flush, then unlock.

### Self-Healing DYN-A11Y
MFC owner-drawn with no UIA -> CCA segmentation -> OCR triple cross-validation -> Gemini Vision inference
-> `dyn_r{row}c{col}` dynamic ID + Experience DB cache + CCA parameter auto-tuning

-> **Version changelog (v5.10~v5.13)**: [docs/changelog/v5.10-v5.13.md](docs/changelog/v5.10-v5.13.md)

### Sunset Screensaver + WhisperRing
Separate processes (WPF memory isolation, parent PID watch -> auto-exit when Eye exits)

### Project Structure
```
csharp/src/
+-- WKAppBot.CLI/     # CLI entry + Commands/
+-- WKAppBot.Core/    # ScenarioRunner, ActionExecutor
+-- WKAppBot.Win32/   # NativeMethods, WindowFinder, UiaLocator, Input/*
+-- WKAppBot.Vision/  # ChartAnalyzer, SimpleOcrAnalyzer, VisionCache
+-- WKAppBot.WebBot/  # CdpClient, ChromeLauncher, SlackSocketClient
+-- WKAppBot.Android/ # AdbClient, AndroidA11yTree
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

-> Full grap syntax: `wkappbot skill read grap`
-> `a11y find <grap>` stdout first line `# TARGET "hwnd:0x..."` -- copy-paste ready
-> **Accumulated knowhow**: `wkappbot skill list` -- search skills first when stuck, then ask triad

---

## Key Design Decisions

### Focusless-First
UIA Invoke/Value/Toggle/Select = Focusless. SendInput/Hotkey requires EnsureFocus.
WPF overlay uses `Spawn(showNoActivate:true)` -> SW_SHOWNOACTIVATE(4), no focus steal.

### PromptDeliveryContext
Before prompt injection: ① target foreground? ② recent 30s input?
-> auto-decides `Focusless` / `FocusSteal` / `Skip` / `Abort`

### HTS Automation
MFC controls: almost no UIA patterns -> Win32 message fallback required. Heroes owner-drawn -> OCR fallback.

### Tag Conventions
`[WATCH]` `[RUN]` `[FOCUS]` `[VERIFY]` `[BLOCK]` `[GUARD]` `[ZOOM]` `[SLACK]` `[EXP]` `[KNOWHOW]`

---

## Session Management (Claude Code Tips)
- `wkappbot claude-usage` -> JSONL size + ctx%
- **ctx% = JSONL ÷ ~20MB** -- prepare handoff at 8MB, immediate handoff at 10MB
- **Goal**: ~10MB or less per session. Aggressive token optimization!
- **Handoff**: `wkappbot newchat "prompt"` -- passes work summary to new chat
- **MEMORY.md**: 200-line limit. Overflow -> split into `memory/` topic files

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
D:/SDK/bin/wkappbot.exe / a11y.exe / wkappbot.hq/
```

## References
- `README.md`
- `AGENTS.md`
- **MEMORY.md** / **memory/**: build commands, architecture decisions, gotchas detail
- .NET 8.0 `net8.0-windows10.0.22621.0`, Korean UI support
