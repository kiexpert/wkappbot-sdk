# WKAppBot v7.3.0 - Windows + Android App Automation Test Framework

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
- Claude-facing project guidance should stay in this `CLAUDE.md`; use skills for execution rules and Codex-specific guardrails, not for replacing the project notes that Claude must also read.

### Language / Communication
- **Final responses to user: Korean, polite 해요체 (-요 form). NEVER informal speech.**
- Do not imitate the user's speech style or dialect.
- Never use `소`체 or casual speech.
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
- **Skill discovery**: use `wkappbot skill search <topic>` first; if the user says `ㄱㄱ` or `ㄱㄱㄱ`, also search `wkappbot skill search ㄱㄱ` or `wkappbot skill search ㄱㄱㄱ`.

### Build & Deploy
```bash
"C:/Program Files/dotnet/dotnet" publish 'D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --verbosity minimal
```
- **Hot-Swap**: publish triggers auto-detect + swap by Eye. **NEVER kill Eye!**
- Auto-publish after any `.cs` edit without waiting for instructions
- If you touch the launcher or core runtime path, you must finish the full release loop immediately: build, deploy, hot-swap, and smoke test. Do not stop at source edits, do not wait for extra confirmation, and do not leave the repo in a half-updated state.

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
- **Delegate detail work to cheap tier** -- Opus 4.7 (this session) is expensive. Push mechanical / read-heavy work to a cheap-tier agent, keep Opus for judgment.
  - **Keep on Opus**: design decisions, multi-file architectural changes, root-cause debugging, judgment calls, anything that needs whole-codebase context.
  - **Push to cheap tier**: read-only surveys, pattern matching across many files, draft-then-review, log-tail analysis, repetitive grep/glob/read loops, boilerplate edits, mechanical refactors -- anything that would otherwise bloat this session's JSONL.
  - **Cheap-tier targets -- unified via `wkappbot agent "task" - <tier>`**: use `codex-mini` (default / omitted) for mechanical work, `read-only` for Codex exploration without writes, `haiku` for Claude Haiku surveys, `sonnet` for Claude Sonnet mid-tier reasoning, and `triad` for parallel GPT + Gemini + Claude on hard problems.
  - In Claude Code IDE, the closest in-process equivalent is `Agent(subagent_type: "Explore", model: "haiku", prompt: "...")`.
  - See also `feedback_delegate_mechanical_to_codex` memory for the established Codex delegation pattern.
- **Ask triad for hard problems** -- any root-cause investigation or design decision that would take ~1 minute+ of thinking/searching, use `wkappbot agent "problem" - triad` (parallel GPT + Gemini + Claude). Three outside perspectives for the price of three cheap calls beats burning tokens on a deep dive alone. Use when: unfamiliar bug pattern, ambiguous regression, architecture trade-off with no obvious winner, stuck more than a couple iterations. See `ask-triad-when-uncertain` skill.

### Forbidden
- Directly spawning Eye / options that block Claude delivery / options that skip Eye -- all forbidden
- Asking user questions in prompt only (must send to Slack simultaneously)
- **Bandaid/workaround code (땜빵)** -- if you find yourself duplicating existing logic, adding local "; OR" splits, or wrapping a proper function with ad-hoc retry loops, STOP. Fix the shared function instead. Bandaids compound: future Claude/Codex sessions burn tokens untangling your workaround, then re-add their own on top. Rule of thumb: if the same concept exists elsewhere in the codebase, reuse it -- don't reinvent a narrower version.
- **Stopping when blocked (FORBIDDEN)** -- NEVER halt and wait for the user when an error or blocker is hit. Always make the best autonomous choice. If genuinely stuck (auth prompt, interactive input, unresolvable conflict), call `ScheduleWakeup(delaySeconds: 60)` with the same task prompt so the next iteration retries. One-shot re-entry beats silent stall every time.

### Loop / Autonomous Task Rule (MANDATORY)
When asked to "run until done", "loop until clean", or given a recurring task:
1. Use `/loop` or `ScheduleWakeup` -- never single-shot and stop.
2. CI fix loop: `gh run list` → fix failures → push → `ScheduleWakeup(120s)` → repeat.
3. Blockers: `ScheduleWakeup(60s)` with same prompt -- next iteration retries best choice.
4. Infra-only failures (core-* binary, unregistered services): classify SKIP, declare done.
5. Exit: all fixable failures resolved in latest commit's runs → stop, report summary.

---

## Overview
Windows a11y-based app UI automation. UIA->Win32->SendInput 3-tier fallback, focusless control.
`a11y.exe` = busybox symlink -> `wkappbot.exe`

## Architecture

### 5-Tier Element Search
UIA -> Vision Cache -> Simple OCR -> Vision API(Claude) -> Coordinate-based

### AppBotPipe / File Tools (v7.2)
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
- **Pipe separation (v6.0)**: normal Eye → `wkappbot_eye_ipc` (tick IPC only). Admin Eye → `wkappbot_elevated` (command proxy only). Must not mix or normal Eye intercepts elevated connections.
- **Proxy encoding**: admin Eye subprocess stdout/stderr captured as UTF-8 (`StandardOutputEncoding=UTF8` on `ProcessStartInfo`).
- **Argv recovery**: `TryRecoverUtf8Argv()` at Main() entry -- detects CreateProcessA UTF-8 bytes via `GetCommandLineA()` strict UTF-8 check, re-parses with `CommandLineToArgvW`.
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
wkappbot chat ["prompt"] [-p] [--no-fallback]     # Claude Code REPL passthrough (alias: wkchat.exe)
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

### Chat Command Usage
```bash
wkappbot chat                                      # Interactive REPL (default model: Haiku)
wkappbot chat "your question"                      # Run prompt, show full conversation
wkappbot chat -p "quick check"                     # Print mode: result only, exit immediately
wkappbot chat --model sonnet "complex task"        # Use Sonnet instead of Haiku
wkappbot chat --model opus "architecture decision" # Use Opus for critical decisions
wkappbot chat -p --max-budget-usd 0.50 "task"     # Cost-controlled batch mode
```
Alias: `wkchat.exe` opens interactive chat session. Models: haiku (default, cheap), sonnet (mid), opus (expensive).

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
- **Handoff primer**: run this first after load/compact when continuing a session:
  ```bash
  wkappbot skill read claude-session-handoff
  wkappbot skill read handoff-checklist
  wkappbot skill read handoff-send-best-practice
  ```
- **MEMORY.md**: 200-line limit. Overflow -> split into `memory/` topic files

## Internal Tools

### wkfind - Unified Code + Session Search
**Location**: `D:\GitHub\WKAppBot\bin\wkfind.ps1` (core repo)
**Usage**: `wkfind [--day|--week|--month|--year|--unlimited] <keyword1> <keyword2> ...`

Unified multi-keyword search across code + Claude sessions:
- **GlobCoverageScore ranking**: tokenize keywords, score by token_length / field_length
- **PHRASE/AND/OR tiers**: PHRASE ×2.5, AND ×1.5, OR ×1.0
- **Code search**: git diff (auto-detected) + time-range fallback (--day → --unlimited)
- **Session search**: live Claude session titles + dates (same time-range filter as code)
- **Time-range sync**: both code and sessions filtered by --day/--week/--month/--year/--unlimited
- **Triad analysis**: GPT+Gemini+Claude synthesis runs in background (configurable timeout)

Time ranges:
- `--day` (default fallback): 24 hours
- `--week`: 7 days
- `--month`: 30 days
- `--year`: 365 days
- `--unlimited`: all time

Examples:
```
wkfind "caller window" HWND          # Find code + sessions (all time)
wkfind --day "CDP position"          # Recent 24h code + 24h sessions
wkfind --week "05-13"                # Week-old sessions + code
```

Output: Top 3 sessions + top 10 code matches per tier, ranked by score.
No options; search-only. `.gitignore` auto-exclusion via ripgrep.

### wkask - Real-time Ask (Stage 3) Pipeline Health Monitor
**Location**: `D:\GitHub\WKAppBot\bin\wkask.ps1` (core repo)
**Usage**: `powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 <provider> '<prompt>' -Timeout <seconds>`

Real-time monitoring of ask CDP pipeline health. Fires a question, streams live response color-coded by AI (GPT/Gemini/Claude), detects Stage 2/3 placement corrections, and reports timing.

Providers: `gpt` (60s), `gemini` (90s), `triad` (120s parallel)

Examples:
```
powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 gpt "say: hello" -Timeout 60
powershell -File D:/GitHub/WKAppBot/bin/wkask.ps1 triad "say: test" -Timeout 120
```

When to use:
- After hot-swap deploy to verify Chrome is on-screen and responsive
- Before triad sessions to confirm all three pipelines (GPT + Gemini + Claude) are healthy
- During Stage 23 testing to watch placement corrections and DPI checks in real-time

On failure: check `cdp-mon.ps1` for position drift (CRIT flags), kill stale Chrome processes, retry.

## Encoding Policy
- Treat repository text files as UTF-8 by default.
- When importing a source file in CP949 or any non-UTF-8 encoding, preserve the original file and also create a UTF-8 copy for safe reading and editing.
- Prefer UTF-8-safe edit paths that preserve file encoding. If a tool may silently re-encode content, use `wkedit` or another verified UTF-8-safe tool instead.
- Keep binary artifacts unchanged. Do not text-convert `.pdf`, `.mp4`, images, or archives.
- Multibyte filenames are allowed, but all repository writes should still use UTF-8 so downstream tools render them consistently.

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

## gg Main Workflow

Execute in order when user sends `gg` or `gogo`. This is the project's recurring main-workflow trigger shorthand.
1. wkappbot skill read on-load
2. wkappbot suggest list
3. [MANDATORY] Sonnet (YOU, main session) MUST run: wkappbot ask gpt "rank these suggests by impact/urgency/effort: <paste suggest list output>" -- do NOT skip or delegate this step
4. Agent(model:opus, prompt:'run wkappbot skill read suggest-workflow first, then triage this backlog using the GPT ranking already provided: <paste GPT ranking result>')

## Scope of Work (YOU: SDK ONLY, not Core repo)

> **FOCUS**: SDK Launcher (`WKAppBot.Launcher/*`), HWND validation, CDP monitoring/suggest triage, suggest resolution.
> **OUT OF SCOPE (Core repo)**: ClaudePromptHelper, CdpClient internals, ChromeLauncher core logic — delegate to Opus agent (`wkappbot ask opus "..."`) or Core maintainers.
> **Boundary**: If you find a bug in Core (`D:\GitHub\WKAppBot\csharp\src\...`), file a suggest and optionally spawn Opus agent to fix in the private repo. Your repo builds, tests, and deploys the launcher binary only. Core fixes require separate build+deploy in the private repo.

---

## Main Duties (recurring responsibilities)

> **PRIMARY DUTY**: You are the wkappbot-sdk product manager. Your job is to make wkappbot more commercially viable every session -- from the user's AND QA engineer's perspective. Not just fix what's asked: spot friction, latency, UX failures, and file suggests proactively. Ask: "would a paying user accept this?" If no → fix or suggest immediately.
> **CDP-FIRST RULE**: Prioritize CDP anomalies and ask timeouts above all else. These block user workflows. Direct-fix (no suggest) per CLAUDE.md mandate.

| Duty | How |
|------|-----|
| **Product quality (PRIMARY)** | While using wkappbot: note slow responses, confusing errors, bad UX → `wkappbot suggest` immediately. Use wkask/wkcdp for QA-first bug detection. |
| **Ask QA** | For SDK ask/latency debugging, `wkask` is the default live-monitoring tool. If the rule is not already written in CLAUDE.md, use `wkask` first, then add the rule here in the same session. |
| **User-perspective QA** | For public SDK regressions, start from `sdk-user-perspective-test-playbook` and the matching `wkask`/`wkcdp` smoke first; the user path is the truth source. |
| **CDP isolation** | Project Chrome/CDP is strictly project-scoped. Reuse only the current project's registered Chrome and tabs; ignore foreign-project Chrome/CDP ports and never attach across project boundaries. |
| **Caller validation** | Before any Chrome placement, normalize the caller HWND. Reject zero, PseudoConsoleWindow, desktop, and off-screen callers; never let Chrome inherit a guessed foreground window. |
| **Release loop** | Every project keeps its recurring `Main Duties` block in repo `CLAUDE.md`. The release loop is always `build -> deploy -> hot-swap -> smoke test`. |
| **Core promotion** | Public workflows that use private-core downloads may promote only sanitized durable summaries back to `WKAPPBOT_CORE_REPO`; never copy secrets or raw private logs. |
| Skill health | `wkappbot skill read repo-health-doctor` -- [LITE] steps each session, [FULL] before release |
| MD self-healing | `wkappbot skill read claude-md-guide` -- apply situation A-H as they arise |
| SDK user skill distribution | `setup.ps1` auto-install `wkappbot-workflow` skills on first run (suggest filed: 2026-05-08) |
| Session recovery | `wkappbot session list --claude --cwd` after compaction |
| Release prep | CHANGELOG + VERSIONING + CLAUDE.md header + gh issue comment on launch checklist |
| **CDP anomaly response** | Red flags: off-screen TGT-POS (x<-100), LAT=DEAD, DUP tabs >3, MEM>2GB/session, >4 Chrome same CWD. **DIRECT FIX via wktool -- NO suggest filing for Chrome position/cookie bugs.** Fix: wkcdp auto-moves off-screen Chrome, wkask aborts if caller off-screen. |
| **CDP suggest triage** | `wkappbot suggest list` -> resolve stale BUG-AUTO CDP/Chrome/ask-gpt suggests. Use `--class CdpClient --commit <hash> --skill cdp-evalasync-retry-policy`. ChromeLauncher.cs is 817 lines (over cap) -- use CdpClient or Win32 partial instead. |
| **CDP bug fixes (primary owner) -- NO SUGGESTS, DIRECT FIX** | CDP/Chrome bugs are YOUR primary responsibility. Pattern: cdp-mon anomaly detected -> read suggest detail -> spawn Opus agent to fix in WKAppBot C# -> build -> hot-swap. Key areas: tab accumulation (sandbox-miss), window position drift (SetWindowBoundsWaitStableAsync), hotswap off-screen (LoadParentWindowGeo on-screen guard), WaitForEditorA11y timeout, CDP eval timeout. Always use partial class (CdpClient.*.cs, ChromeLauncher.*.cs) not the 817-line monolith. |
| **Doc version audit** | Before release and on ㄱㄱ: grep all root `*.md` for old version strings (v5.x, v6.x). Check SECURITY.md supported-versions table, README What's New section, CHANGELOG header. Fix and push any stale references. |
| **Doc version sync** | Keep README/AGENTS/CLAUDE/SECURITY/VERSIONING/CHANGELOG consistent with current version. When version bumps: update SECURITY supported-versions, README What's New, VERSIONING current-version line, CHANGELOG header -- all in one commit. Never let version strings drift between files. |

## Skills
wkappbot skill read suggest-workflow      # suggest submit/resolve/co-resolve rules
wkappbot skill read grap                  # UI element addressing (window/UIA/CDP/ADB)
wkappbot skill read cdp-command-guide     # CDP command reference for Chrome automation
wkappbot skill read a11y-command-cheatsheet  # a11y action cheatsheet

# ★ MANDATORY -- wk-tool scripts (read before ANY ask/CDP work)
wkappbot skill read wktool-pattern        # MANDATORY: wkask/wkcdp usage + QA script pattern (merged)

## Gotchas
- `suggest list` crashes with JsonException if a delta-comment wrote raw JSON into a string field. Workaround: `python3 -c "import json; [print(l[:80]) for l in open('suggestions.jsonl') if json.loads(l)]"` to read valid lines.
- `wkappbot suggest` silently drops submit when PENDING CO-RESOLVE banner shows. Use `wkappbot-core.exe suggest "..."` to bypass.
- `skill audit` shows FILE MISSING for `csharp/src/WKAppBot.*` paths — expected in SDK repo (files live in the WKAppBot core repo). Not broken.
- Chrome translate infobar (auto-shown on Korean pages) blocks CDP injection — it steals focus and intercepts clicks. Dismiss or suppress before automation.
- `cdp-mon.ps1` off-screen check was `'^-\d{3,}'` regex (x < -100 = off-screen) — **false positive on multi-monitor setups** where left monitor has large negative x. Fixed to use `MonitorFromPoint` Win32 API. Rule: never use raw coordinate sign for off-screen detection; always use `MonitorFromPoint` (same as `IsWindowOnScreen` in EyeCmdPipeClient.cs).

## References
- `README.md`
- `AGENTS.md`
- **MEMORY.md** / **memory/**: build commands, architecture decisions, gotchas detail
- .NET 8.0 `net8.0-windows10.0.22621.0`, Korean UI support

## 리밋 메시지 윌김이 추가~ 스킬화후 이 섹션 삭제 요망~
■ You've hit your usage limit. Upgrade to Pro (https://chatgpt.com/explore/pro), visit
https://chatgpt.com/codex/settings/usage to purchase more credits or try again at 4:30 PM.
■ First limit = no retry. Mark handoff_pending, let the current atomic task finish if it is already running, and let the next AI/provider stand by. If the same limit appears again while pending, terminate the current session and do not retry the same CLI.
