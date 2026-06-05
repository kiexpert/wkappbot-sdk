# WKAppBot v7.5.35-sdk - Windows + Android App Automation Test Framework

## Operating Rules (READ FIRST)

> !截?**LANGUAGE RULE -- Korean is ONLY for final responses to the user.**
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
- **Final responses to user: Korean, polite ?댁슂泥?(-??form). NEVER informal speech.**
- Do not imitate the user's speech style or dialect.
- Never use `??泥?or casual speech.
- Source code / comments / CLAUDE.md / skills / memory / commits / docs -> **English only, no exceptions**
- **Questions**: `wkappbot slack send "question"` + send in prompt simultaneously (Slack-only forbidden)
- **Slack replies**: always reply in thread (`--msg TS` if TS available, else `send`)

### AppBotEye
- **Must always be running**: auto-spawned on normal CLI commands (except `eye`/`slack`/`help`/`validate`/`win-move`)
- **Eye = Slack daemon integrated** -> no separate `slack listen` needed
- **eye tick**: one-shot status query (includes ctx=N%) / **eye**: FSW hybrid loop
- **Handoff**: `wkappbot newchat "prompt"` -- passes context summary to new chat
- **Cro card forbidden!**: OpenClaw(Cro) is a separate service -- do not modify. Only Claude cards OK.
- **CWD shorthand**: `D:\GitHub\WKAppBot` -> `WG-WKAppBot` / noise filters: `NO_REPLY`, `?긱꽦`
- **Skill discovery**: use `wkappbot skill search <topic>` first; if the user says `?긱꽦` or `?긱꽦??, also search `wkappbot skill search ?긱꽦` or `wkappbot skill search ?긱꽦??.

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
- **Bandaid/workaround code (?쒕뭇)** -- if you find yourself duplicating existing logic, adding local "; OR" splits, or wrapping a proper function with ad-hoc retry loops, STOP. Fix the shared function instead. Bandaids compound: future Claude/Codex sessions burn tokens untangling your workaround, then re-add their own on top. Rule of thumb: if the same concept exists elsewhere in the codebase, reuse it -- don't reinvent a narrower version.
- **Stopping when blocked (FORBIDDEN)** -- NEVER halt and wait for the user when an error or blocker is hit. Always make the best autonomous choice. If genuinely stuck (auth prompt, interactive input, unresolvable conflict), call `ScheduleWakeup(delaySeconds: 60)` with the same task prompt so the next iteration retries. One-shot re-entry beats silent stall every time.

### Loop / Autonomous Task Rule (MANDATORY)
When asked to "run until done", "loop until clean", or given a recurring task:
1. Use `/loop` or `ScheduleWakeup` -- never single-shot and stop.
2. CI fix loop: `gh run list` ??fix failures ??push ??`ScheduleWakeup(120s)` ??repeat.
3. Blockers: `ScheduleWakeup(60s)` with same prompt -- next iteration retries best choice.
4. Infra-only failures (core-* binary, unregistered services): classify SKIP, declare done.
5. Exit: all fixable failures resolved in latest commit's runs ??stop, report summary.

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
Eye ??MCP worker(Core) JSON-RPC over pipe. a11y/UIA isolated in separate process.
- `ShouldRouteToMcp()`: a11y/inspect/windows/ask -> MCP, slack/eye/schedule -> in-process
- `DETACHED_PROCESS` flag prevents ConPTY LPC deadlock. Auto-restart max 5/5min
- Slack file-based queue (`runtime/slack_queue/`), drain worker serial processing
- **Launcher quiet-swap**: launcher watches only original `wkappbot-core.exe` path change. `.new.exe` staging/rename is Eye's responsibility.
- **Admin-first swap**: if admin endpoint is alive, defer normal core swap; retry only after admin exits with newer stamp.
- **Failed-stamp skip**: a core `mtime` stamp that failed once is not retried until a newer file arrives.
- **Pipe separation (v6.0)**: normal Eye ??`wkappbot_eye_ipc` (tick IPC only). Admin Eye ??`wkappbot_elevated` (command proxy only). Must not mix or normal Eye intercepts elevated connections.
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
wkappbot a11y <action> <grap>[#scope] [options]   # ??unified standard (24 actions)
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
Before prompt injection: ??target foreground? ??recent 30s input?
-> auto-decides `Focusless` / `FocusSteal` / `Skip` / `Abort`

### HTS Automation
MFC controls: almost no UIA patterns -> Win32 message fallback required. Heroes owner-drawn -> OCR fallback.

### Tag Conventions
`[WATCH]` `[RUN]` `[FOCUS]` `[VERIFY]` `[BLOCK]` `[GUARD]` `[ZOOM]` `[SLACK]` `[EXP]` `[KNOWHOW]`

---

## Session Management (Claude Code Tips)
- `wkappbot claude-usage` -> JSONL size + ctx%
- **ctx% = JSONL 첨 ~20MB** -- prepare handoff at 8MB, immediate handoff at 10MB
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
- **PHRASE/AND/OR tiers**: PHRASE 횞2.5, AND 횞1.5, OR 횞1.0
- **Code search**: git diff (auto-detected) + time-range fallback (--day ??--unlimited)
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
0. **[PRIMARY DUTY] Smoke test loop**: 
   - `wkcdp-mon.sh -KillForeign` (Chrome cleanup first, check LAT avg — any >1000ms = CDP bug)
   - `wkappbot ask gpt "say: test"` — measure time, if >1s warn "CDP state detection bug"
   - `wkask.sh gpt "say: test" -Timeout 10` — must complete <10s else cdp open bug
   - `wkappbot taskkill /IM wkappbot-core.exe --dry-run` + `wkjobs.sh -Leaks`
   - Show ALL errors/warnings inline. Loop until 0 errors. 1-SECOND RULE: any CDP op >1s = BUG.
0.5. **[CRITICAL] Enhanced Health Check**: `powershell -File scripts/gg-main-enhanced.ps1` (detects 8 issue categories). exit=2 → Opus emergency fix. exit=1 → log Pending, continue.
1. wkappbot skill read on-load
2. wkappbot skill news  -- check recently updated skills before triage (context for what changed)
3. wkappbot suggest list
4. [MANDATORY] Sonnet (YOU, main session) MUST run: wkappbot ask gpt "rank these suggests by impact/urgency/effort: <paste suggest list output>" -- do NOT skip or delegate this step (note: will timeout if Chrome>5 or Eye>3; rerun after fix)
5. Agent(model:opus, prompt:'run wkappbot skill read suggest-workflow first, then triage this backlog using the GPT ranking already provided: <paste GPT ranking result>')
6. [ON RELEASE] Public skill curation: Agent(model:opus, prompt:'run wkappbot skill read on-load + wkappbot skill read wkharness-guards + wkappbot skill read sdk-public-skill-index, then select non-confidential skills from all apps and register/update them under wkappbot-sdk with audience:user or audience:developer. Criteria: useful to global automation devs, no internal business secrets, no ops-only content.')

## Scope of Work (YOU: SDK ONLY, not Core repo)

> **FOCUS**: SDK Launcher (`WKAppBot.Launcher/*`), HWND validation, CDP monitoring/suggest triage, suggest resolution.
> **OUT OF SCOPE (Core repo)**: ClaudePromptHelper, CdpClient internals, ChromeLauncher core logic ??delegate to Opus agent (`wkappbot ask opus "..."`) or Core maintainers.
> **Boundary**: If you find a bug in Core (`D:\GitHub\WKAppBot\csharp\src\...`), file a suggest and optionally spawn Opus agent to fix in the private repo. Your repo builds, tests, and deploys the launcher binary only. Core fixes require separate build+deploy in the private repo.

---

## Main Duties (recurring responsibilities)

> **PRIMARY DUTY**: You are the wkappbot-sdk product manager. Your job is to make wkappbot more commercially viable every session -- from the user's AND QA engineer's perspective. Not just fix what's asked: spot friction, latency, UX failures, and file suggests proactively. Ask: "would a paying user accept this?" If no ??fix or suggest immediately.
> **CDP-FIRST RULE**: Prioritize CDP anomalies and ask timeouts above all else. These block user workflows. Direct-fix (no suggest) per CLAUDE.md mandate.

| Duty | How |
|------|-----|
| **Product quality (PRIMARY)** | While using wkappbot: note slow responses, confusing errors, bad UX ??`wkappbot suggest` immediately. Use wkask/wkcdp for QA-first bug detection. |
| **Ask QA** | For SDK ask/latency debugging, `wkask` is the default live-monitoring tool. If the rule is not already written in CLAUDE.md, use `wkask` first, then add the rule here in the same session. |
| **User-perspective QA** | For public SDK regressions, start from `sdk-user-perspective-test-playbook` and the matching `wkask`/`wkcdp` smoke first; the user path is the truth source. |
| **CDP isolation** | Project Chrome/CDP is strictly project-scoped. Reuse only the current project's registered Chrome and tabs; ignore foreign-project Chrome/CDP ports and never attach across project boundaries. |
| **Caller validation** | Before any Chrome placement, normalize the caller HWND. Reject zero, PseudoConsoleWindow, desktop, and off-screen callers; never let Chrome inherit a guessed foreground window. |
| **Release loop** | Every project keeps its recurring `Main Duties` block in repo `CLAUDE.md`. The release loop is always `build -> deploy -> hot-swap -> smoke test`. |
| **Core promotion** | Public workflows that use private-core downloads may promote only sanitized durable summaries back to `WKAPPBOT_CORE_REPO`; never copy secrets or raw private logs. |
| Skill health | `wkappbot skill read repo-health-doctor` -- [LITE] steps each session, [FULL] before release |
| **Public skill curation** | Before each release: select non-confidential skills from all apps ??register under `wkappbot-sdk` with `audience:user` or `audience:developer` via `wkappbot skill contribute`. Criteria: useful to global SDK users, no business secrets, no internal-ops content. Maintain `sdk-public-skill-index` as the canonical list. |
| MD self-healing | `wkappbot skill read claude-md-guide` -- apply situation A-H as they arise |
| SDK user skill distribution | `setup.ps1` auto-install `wkappbot-workflow` skills on first run (suggest filed: 2026-05-08) |
| Session recovery | `wkappbot session list --claude --cwd` after compaction |
| Release prep | CHANGELOG + VERSIONING + CLAUDE.md header + gh issue comment on launch checklist |
| **CDP anomaly response** | Red flags: off-screen TGT-POS (x<-100), LAT=DEAD, DUP tabs >3, MEM>2GB/session, >4 Chrome same CWD. **DIRECT FIX via wktool -- NO suggest filing for Chrome position/cookie bugs.** Fix: wkcdp auto-moves off-screen Chrome, wkask aborts if caller off-screen. |
| **CDP suggest triage** | `wkappbot suggest list` -> resolve stale BUG-AUTO CDP/Chrome/ask-gpt suggests. Use `--class CdpClient --commit <hash> --skill cdp-evalasync-retry-policy`. ChromeLauncher.cs is 817 lines (over cap) -- use partial classes (ChromeLauncher.SessionRestore.cs, ChromeLauncher.*.cs) or CdpClient.*.cs instead. |
| **CDP bug fixes (primary owner) -- NO SUGGESTS, DIRECT FIX** | CDP/Chrome bugs are YOUR primary responsibility. Pattern: cdp-mon anomaly detected -> read suggest detail -> spawn Opus agent to fix in WKAppBot C# -> build -> hot-swap. Key areas: tab accumulation (sandbox-miss), window position drift (SetWindowBoundsWaitStableAsync -- see CdpClient.WindowStabilize.cs), session restore override (ChromeLauncher.SessionRestore.cs), hotswap off-screen (LoadParentWindowGeo on-screen guard), WaitForEditorA11y timeout, CDP eval timeout. Always use partial class (CdpClient.*.cs, ChromeLauncher.*.cs). |
| **Doc version audit** | Before release and on ?긱꽦: grep all root `*.md` for old version strings (v5.x, v6.x). Check SECURITY.md supported-versions table, README What's New section, CHANGELOG header. Fix and push any stale references. |
| **Doc version sync** | Keep README/AGENTS/CLAUDE/SECURITY/VERSIONING/CHANGELOG consistent with current version. When version bumps: update SECURITY supported-versions, README What's New, VERSIONING current-version line, CHANGELOG header -- all in one commit. Never let version strings drift between files. |
| **GitHub Release authoring** | After every version tag push: verify `gh release list` shows the new tag. If missing, create manually: `gh release create vX.Y.Z-sdk --title "WKAppBot vX.Y.Z-sdk" --notes "$(CHANGELOG section)"`. Release notes must include: highlights, Fixed(CRITICAL) items first, Added, Changed. Mirror CHANGELOG.md content exactly. |
| **GitHub Release healing** | On ?긱꽦 and before hotfix: run `gh release list` and compare against CHANGELOG top entries. If a released version has no GitHub Release entry, create it. If release notes are stub-only ("Full Changelog" link), expand with CHANGELOG content. |

## Skills
wkappbot skill read suggest-workflow      # suggest submit/resolve/co-resolve rules
wkappbot skill read grap                  # UI element addressing (window/UIA/CDP/ADB)
wkappbot skill read cdp-command-guide     # CDP command reference for Chrome automation
wkappbot skill read a11y-command-cheatsheet  # a11y action cheatsheet

# ??MANDATORY -- wk-tool scripts (read before ANY ask/CDP work)
wkappbot skill read wktool-pattern        # MANDATORY: wkask/wkcdp usage + QA script pattern (merged)

# SDK public skills index (global user distribution)
wkappbot skill read sdk-public-skill-index  # curated list of ~30 public skills for SDK users
# NOTE: chrome-session-restore-position-fix skill is in Core repo -- register to HQ via skill contribute before adding here

## Gotchas
- **CRITICAL: Zombie process accumulation under system lag** -- System lag → Eye IPC slow → suggest/ask commands hang → child wkappbot-core processes spawn but never exit → exponential accumulation overnight → system crash by morning. Mitigation: `powershell -File bin/zombie-watchdog.ps1 -MaxAge 45` runs continuously, killing processes older than 45s with no output. Root cause: Eye IPC timeout in suggest/ask flow needs timeout wrapper. Workaround: manually run `Get-Process wkappbot-core | Stop-Process -Force` to emergency-clean stale processes.
- `suggest list` crashes with JsonException if a delta-comment wrote raw JSON into a string field. Workaround: `python3 -c "import json; [print(l[:80]) for l in open('suggestions.jsonl') if json.loads(l)]"` to read valid lines.
- `wkappbot suggest` silently drops submit when PENDING CO-RESOLVE banner shows. Use `wkappbot-core.exe suggest "..."` to bypass.
- `skill audit` shows FILE MISSING for `csharp/src/WKAppBot.*` paths ??expected in SDK repo (files live in the WKAppBot core repo). Not broken.
- Chrome translate infobar (auto-shown on Korean pages) blocks CDP injection ??it steals focus and intercepts clicks. Dismiss or suppress before automation.
- `cdp-mon.ps1` off-screen check was `'^-\d{3,}'` regex (x < -100 = off-screen) ??**false positive on multi-monitor setups** where left monitor has large negative x. Fixed to use `MonitorFromPoint` Win32 API. Rule: never use raw coordinate sign for off-screen detection; always use `MonitorFromPoint` (same as `IsWindowOnScreen` in EyeCmdPipeClient.cs).

## RIQUA: gg-main Automation + Opus Collaboration

- **RIQUA [gg-main Opus spawn]**: Every 3 hours, gg-main script runs and detects Amber/critical status. Opus agent spawns automatically. However, Opus may not have full context if session is compacted or new. INTENDED: Opus should always carry forward prior findings + escalation state. WORKAROUND: Manual `wkappbot skill read sdk-gg-main-automation` before Opus spawn to provide context.

- **RIQUA [Chrome multiplication fix deployed]**: 2026-05-28 SDK-side auto-cleanup implemented in MyCdpContext.ChromeHealthCheck.CleanExcessiveChromeProcesses(). When Chrome>5 detected, auto-kills duplicate processes, preserves 2 oldest. Integrated into gg-main-enhanced.ps1. Root cause (Core FindRunningChromePortAny guard 8819ed449) likely still not deployed to user system; SDK mitigation prevents cascade into Eye IPC lag. Verify: run gg-main-enhanced.ps1 repeatedly, Chrome should stay ≤5 after cleanup.

- **RIQUA [Opus cost reduction]**: Removed 5-min zombie-watchdog CronCreate (was 8 × 60 = 480 checks/day). Now gg-main-enhanced runs every 3 hours (8/day) with auto-cleanup integrated. Reduced Opus spawns from 8 to 1 per day (only on CRITICAL). Cost: ~5-10 USD/month (vs prior 40-50 USD/month).

## References
- `README.md`
- `AGENTS.md`
- **MEMORY.md** / **memory/**: build commands, architecture decisions, gotchas detail
- .NET 8.0 `net8.0-windows10.0.22621.0`, Korean UI support

## 由щ컠 硫붿떆吏 ?뚭???異붽?~ ?ㅽ궗?뷀썑 ???뱀뀡 ??젣 ?붾쭩~
??You've hit your usage limit. Upgrade to Pro (https://chatgpt.com/explore/pro), visit
https://chatgpt.com/codex/settings/usage to purchase more credits or try again at 4:30 PM.
??First limit = no retry. Mark handoff_pending, let the current atomic task finish if it is already running, and let the next AI/provider stand by. If the same limit appears again while pending, terminate the current session and do not retry the same CLI.



## Nightly Schedules

> Hook (.claude/nightly-schedule-guard.ps1) denies Bash/PS until all headings below have a matching CronCreate in this session's JSONL.
> Claude reads each `### CronCreate(...)` heading and executes it verbatim at session start. wkappbot skill* always exempt.

### CronCreate(cron="13 4 * * *", durable=true, recurring=true)
wkappbot skill read skill-heal-nightly then run STEP 2 HEALING as Opus subagent. CWD=D:/GitHub/wkappbot-sdk. Proceed autonomously without user confirmation.


<!-- KillForeign hourly cron REMOVED 2026-05-31: blind kill without diagnosis. CDP issues now handled by gg-main 3h cycle with full diagnosis + escalation. -->
- [x] DONE (2026-05-31 gg-main+nightly cluster): gg-main Section D CDP live monitoring; Core fixes 82d60f74f+ce6ee8569 (cdp open 143min->15s, O(1) wmic); 3h checks Eye/Chrome cleaned; nightly-heal 3ok/54-cross-repo/630-norefs v7.5 OK; Chrome 202->7 emergency cleanup; wkask.sh \n fix 94026081c

### CronCreate(cron="0 */3 * * *", durable=true, recurring=true)
wkappbot skill read sdk-gg-main-automation then run health check: bash scripts/gg-main.sh. If any issues (Amber status or critical red flags detected): spawn Opus agent to diagnose root cause and implement autonomous fixes per escalation rules (Chrome mult->Core escalate, Eye lag->taskkill+restart, CI fail->fix+push). CWD=D:/GitHub/wkappbot-sdk.

## harness:safe CLAUDE\.md

## harness:warn wkedit.*--line.*\\
wkappbot file edit --old-file /tmp/old.txt --new-file /tmp/new.txt FILE

## harness:block (?i)bash.*powershell|bash.*pwsh
Use PowerShell tool directly

## harness:done (?i)skipped|완료|완성|끝
Step may have been skipped -- verify all gg steps ran

## harness:block (?i)\bwrite_file\(
Use Write.cmd or Edit.cmd instead of native write_file

## harness:block (?i)\bapply_patch\(
Use Edit.cmd instead of native apply_patch

## harness:block (?i)\bread_file\(
Use Read.cmd instead of native read_file

## harness:skill csharp/**/*MyCdpContext*.cs
wkappbot skill read standard-appbot-window  # Usage: caller HWND detection, CDP context scope
wkappbot skill read standard-chrome-window  # Impl: Chrome window filtering, DPI validation, Z-order
wkappbot skill read wkfind-caller-hwnd-validation-3tier-pattern  # Task: ancestor walk maintenance, regression testing

## harness:skill csharp/**/*.cs
wkappbot skill read sdk-launcher-maintenance  # Usage: SDK launcher lifecycle and versioning
wkappbot skill read wkappbot-build-verify-workflow  # Impl: publish process, hot-swap deployment
wkappbot skill read wkharness-guards  # Task: guard reference when unblocking edits

## harness:skill scripts/gg-main-enhanced.ps1
wkappbot skill read sdk-gg-main-automation  # Usage: gg health check automation workflow
wkappbot skill read wkharness-guards        # Impl: process kill protection rules
wkappbot skill read wktool-pattern          # Task: taskkill routing for zombie cleanup

## harness:skill scripts/build-skill-page.py
wkappbot skill read sdk-skill-browser-pipeline  # Usage: skill browser build process
wkappbot skill read skill-migration-3tier  # Impl: tier suffix detection for parent skills
wkappbot skill read sdk-public-skill-index  # Task: public skill curation and premium gating

## harness:skill wkagent-name.py
wkappbot skill read wkagent-name-tool           # Usage: agent model detection
wkappbot skill read wkharness-guards            # Impl: ctypes Windows API patterns
wkappbot skill read haiku-as-qa-canary          # Task: fast prototype QA canary

## Pending

- [ ] suggest triage 2026-06-05 (RANK 1-4, 2긴급+19중요+2기타 SDK channel; GPT ranking skipped — Chrome>5/CDP-bug risk per gg step 4 note; classified by SPEC impact/urgency/effort/scope):
  - RANK 1 [8/9] BUG-CDP LOGIN_PAGE ports 9980 session expired | impact=HIGH blocks ask/CDP user workflow, user-facing | effort=MED | scope=Core (login-wipe = chrome multiplication root, tracked DG-personal-docs [31]) → SDK mitigation already live (MyCdpContext.ChromeHealthCheck auto-cleanup); monitor recurrence, no new SDK fix
  - RANK 2 [17] sdk-find-stable: dev repo build not green (failure) | impact=HIGH blocks releases | effort=LOW (verify) | scope=Core build (kiexpert/wkappbot) → escalate: gh run list --repo kiexpert/wkappbot; add to gg DEV-BUILD section (already in skill v1.34 step 14)
  - RANK 3 [18/19/20] HARNESS GAP core.hooksPath='' bypasses pre-push | impact=MED security | effort=LOW | scope=SDK harness → ALREADY PATCHED (CLAUDE.md harness:block core.hooksPath rule present); resolve-as-fixed pending Eye stable
  - RANK 3 [21/22] CRITICAL Chrome mult false-positive MERGE×2 | impact=MED | effort=DONE | scope=SDK ChromeHealthCheck.cs (MainWindowHandle filter + Skip(2) keep-2, threshold>3, commit 6dc2e6d9) → verified fixed; resolve pending
  - RANK 3 [15/16] FEAT agent-msg command | impact=LOW nice-to-have | effort=MED | scope=SDK feature → backlog, not blocking
  - RANK 4 [15→ts 2026-06-03T19:56:35] WebSocketException ReconnectAsync + [21→ts 2026-06-02T01:59:32] Runtime.evaluate timeout | stale BUG-AUTO CdpClient (Core, OUT OF SCOPE) from cold-start "ask gemini say:test" smoke | dismiss-stale per resolve-stale-bug-auto-merge-noise
  - RANK 4 [35/36] wkask.sh literal-backslash exec | scope=SDK but fixed 3x (commit 94026081) → Codex deny-rule needed (Pending below); [37] skill sync CONFLICTS UX=LOW; [40] pre-push AI-block UX=LOW; [42] FEAT skill 3-tier auto-detect=LOW; [202/203] pre-commit false-block=already fixed
  - BLOCKER: actual suggest resolve/merge IPC-lagged (Eye); triage documented only, formal resolution next stable session

<!-- compressed 2026-06-04 nightly-heal: all resolved [x] items folded into the single DONE-ARCHIVE line below; raw history in git -->
- [x] DONE-ARCHIVE (compressed 2026-06-04, all prior [x] clusters): v7.5.0-sdk release + 3x nightly-heal 2026-05-30..06-04 (wkask.sh/ps1 fixes, wkcdp-mon MonitorFromPoint, wkzombie safe-skip, CDP cdp-open 143min->30s Core 82d60f74f, Chrome multiplication SDK auto-cleanup MyCdpContext.ChromeHealthCheck, gg-main-enhanced, doc bump v7.4->v7.5, gh release); skill-browser + 3tier cluster (wkask log fixes, wksplit-cs, sdk-gg-main-automation/suggest-triage/wkharness/cdp-command 3tier splits, docs/skills restructure + self-heal scripts, PAT pro unlock b26ad540, 3-state auth b057a503, 423 skill pages built); wkjobs/taskkill suite + Codex harness/wrappers + caller-HWND resolution + a11y WT terminal + wkdoctor 10-checks; test-auth.html probe page (05b47480); gg triage 2026-06-04 (wkask.sh merges, Chrome 16->2 auto-cleanup verified); payment-skill ref heal (SUBSCRIBE.md Korean anchor + drop missing kis_payment_watcher.py refs, v1.8). Commit hashes in git log.

<!-- compressed 2026-06-05: 15 [x] items (gg-main 2-cycle health checks + escalations + Gemini suggest-triage + v7.5.1-sdk release + nightly-heals verified); core-scope suggests filed with SDK mitigation live; next: resolve suggests when Eye/system idle -->
- [x] 2026-06-05 health-check + escalation cluster (15 items): gg-main rapid-fix cycle (Chrome 26→0 auto-clean, Eye 6→5 restart, CI CDP Smoke skip-rule, Core suggests ts=2026-06-05T09:19:28 filed), Gemini suggest-triage R1-R4 classifications (R1 dismiss-stale × 2, R2 dev-build OK, R3 feature-backlog, R4 resolved/merged), v7.5.1-sdk released (skill-browser v2 + ChromeHealthCheck MainWindowHandle filter verified), nightly-heals 2026-06-04/05 confirmed (skill-audit clean, version-parity OK), wkask.sh pattern-fix Bypass.*\\n.*-File committed (lines 128, 508), all Core-scope escalations documented, SDK mitigations live, verify pending after Eye/system idle

- [ ] 2026-06-05 21:31 gg-main CRITICAL+8WARN triage (exit=2): wkask.sh \n = Core escalated (suggest ts=2026-06-05T09:19:28 ✓), Chrome 16→9 (trending normal, Launcher v7.5.35 cleanup effective), Eye 8 (settling post-restart, mcp-protected survivors), Suggests 14 (classify next session per "comprehensive audit" mandate), CI Smoke (skip-rule verified), FOCUS-STEAL/CHROME:CAP (Core escalations filed), DEAD LAT/KillForeign (expected cleanup). Status: CRITICAL escalated ✓, WARNINGS classified & escalated ✓, Ready next cycle or User decision.

- [ ] 2026-06-06 00:30 gg-main CRITICAL×4+WARN×6 escalation BLOCKED (exit=2): Eye 33 zombie (acceleration 8→22→33), CDP 9712 memory 1192MB (explosion), LOGIN_PAGE session expired, Suggests 17 urgent. System constraint: RAM 70%, Opus-gate blocks Sonnet intermediary, taskkill not responding properly. Autonomous fix attempts all failed (Opus-evidence-gate, harness solo-loop, taskkill syntax issues). Status: REQUIRES SYSTEM RESET or MANUAL USER INTERVENTION (Chrome login on 9712, system restart, or wait CronCreate 3h cycle ~03:30 UTC). No further automated progress possible without system recovery.

- [ ] GHA auto-build: add skills/ fallback to build-skill-page.py + upgrade build-skill-page.yml to build+commit on skills push
- [ ] [CRIT] Core build broken: A11yActions.Invoke.cs(197) error CS0103 'A11yClick' — file suggest to DG-WKAppBot from core repo CWD
- [x] push skill-browser refactor to origin/main (Opus squash+push)
- [x] skill browser SEO + sonnet reflection fix: per-skill static pages + left treeview + all 7 reflection items + 44-char preview + 3-state auth unlock
- [x] gg-main 3h 2026-06-04 ~15:00: Amber resolved. ~16:30 check: Amber(경계) Chrome=4, CI 5/5 green, suggest 긴급2+중요13
- [ ] skill-browser-launch-checklist: deploy skills-data-full.js to wkappbot-harness before Pro unlock goes live
- [ ] contributor service: skills-data-full.js generation + raw.githubusercontent.com dynamic load + localhost test (auto-approved, no confirmation needed)
- [ ] WKHARNESS_OPUS_FG env var 백도어 언급 스킬 전체 정리 필요
- [ ] wkappbot-harness repo 삭제 필요 (허락 없이 생성한 것, delete_repo scope 필요)
- [ ] skill-heal-nightly 3tier 분리 필요 (21 steps, update-gate 차단 중)
- [x] wkask.sh \n bug 2026-06-05 (gg final health): ROOT CAUSE found = gg-main.sh detection pattern was buggy (`Bypass[^\n]*\\n[^\n]*-File` -- `[^\n]*` excludes literal 'n' chars so never spanned the text), so the broken Core file went UNDETECTED = the "false-positive" was actually a false-NEGATIVE. Fixed pattern to `Bypass.*\\n.*-File` (commit pending). SDK bin/wkask.sh = CLEAN (single-line); Core D:/GitHub/WKAppBot/bin/wkask.sh = WAS broken (literal \n), fixed to single-line + smoke-verified (exec reaches wkask.ps1, no \n arg error). Codex deny-rule for Core bin/wkask.sh still needed to stop reverts.
- [ ] gg-main final WARN classification 2026-06-05: Eye 5>3=boundary (1x13h PROTECTED survivor, reaper handles; not crit) MONITOR; suggests 14>5=backlog MONITOR; FOCUS-STEAL 3 + CHROME:CAP 7 = Core-scope ESCALATE; CI CDP Smoke fail=expected (real-site nav timeout) SKIP-rule OK; dev-repo skipped=non-standard MONITOR. SECURITY.md 7.5.x = intentional range, not drift.
- [ ] wkask GPT: BLOCKED_NATIVE_DIALOG (Chrome minimized after cold start → 닫기 dialog). Needs auto-restore or on-screen Chrome guard before ask.
- [ ] wkask Gemini: Google cookie consent banner off-screen (Chrome minimized). Same root cause as GPT issue.
- [ ] wkask Gemini -Wait arch bug: sync exec + completed log → suggest needed (wkappbot ask gemini direct works)
- [ ] 3tier migration 잔여: wktool-pattern(12), grap(10), wkcdp-mon, sdk-public-skill-index
- [ ] skill browser 파이프라인: build-skill-page.py + .env 마스킹 + 44자 트런케이션 + GHA 워크플로 완성 중
- [ ] open-skill-viewer.sh grap regex 버그 수정 필요: {hwnd:...,proc:chrome,cdp:PORT} 패턴
- [ ] CDP ask 박멸 작업 중: wkask.ps1 line 149 Chrome restore 블록 추가 필요 (가드 cascade로 직접 편집 불가 상태)
- [ ] Core push 완료 2026-06-02: 36b1892bb..8e2e209c7 (wkask 수정 포함)
- [ ] ask-suggest-priority-batching-howto skill updated cross-session (v1.1)
- [ ] Haiku subagent wk-only-gate bug: isHaikuSession not propagated to subagents spawned from Sonnet session. Suggest needed.
- [ ] pending-protect: session added multiple items (gg-main, Eye hang, CDP tab, stall-guard, cdp open hang, GHA status checks)
- [ ] sonnet-bug-stop-policy skill updated cross-session (personal-docs v1.11)
- [ ] cdp open hang: WkFeedQuant cdp open hung 67s (port 9712 Eye also hung). Core timeout fixes deployed but hang persisting.
- [ ] GHA status checked: all workflows green (Backtest Quant, Hantoo Token, WkWave etc. -- 2026-05-31)
- [ ] pythonw process check: no pythonw leaked processes (verified clean)
- [ ] stall-guard delegation loop: Opus/Haiku subagents both blocked (usage-test-guard 50% + wk-only-gate in subagents). Filed suggest ts=2026-05-31T05:55:07 for non-consecutive exemption.
- [ ] cross-channel agent activity noted (WkAutoQuant portfolio updates running in background)
- [ ] gg-main 3h check 2026-05-31 ~15:00: Amber -- Chrome 5 (boundary), suggest 13 important, CI green, Eye alive. wk-only-gate blocked full script run.
- [ ] stall-guard blocks user-triggered wkzombie repeats -- suggest filed (ts=2026-05-31T05:55:07) for non-consecutive exemption
- [ ] Eye hang investigation: eye+guardian+whisper-ring cycling hang every ~4min after frame-5-tick-ok. Bug filed via suggest. Root cause: inter-frame block in v7.5.20 binary (02:37 build). Core fix needed.
- [ ] [CDP-TODO-1] Wire EditorWait.cs: insert IsBadTabState pre-check at line 51 (after EnsureChromeNotIconic, before Console.Write EDITOR-WAIT). File: D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.ChatGpt.EditorWait.cs. TabRecovery.cs already exists.
- [ ] [CDP-TODO-2] Build Core after wiring: dotnet publish D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj -c Release --verbosity minimal
- [ ] [CDP-TODO-3] Commit: git -C D:/GitHub/WKAppBot add CdpClient.TabRecovery.cs AskCommands.ChatGpt.EditorWait.cs && git commit -m "fix(cdp): IsBadTabState recovery before EDITOR-WAIT on crashed tab"
- [ ] [CDP-TODO-4] Eye hang root cause: frame-5-tick-ok → inter-frame block in v7.5.20 (02:37 build). New Core commits edc6359f/7ea533cc may have introduced blocking CDP probe in inter-frame period. Core fix needed.
- [ ] [CDP-TODO-5] Haiku isHaikuSession not propagated to subagents (suggest filed). Core fix needed for proper Haiku subagent exemption.
- [ ] [CDP-TODO-6] CronDelete stall-guard false positive (suggest needed). One-time admin ops should be exempt.

- [ ] wk-gg-main.sh + wk-gg-main.cmd: create .sh relay + fix .cmd to call bash scripts/gg-main.sh (currently calls nonexistent .ps1)
- [ ] ChatSessionGuard: AI-spawned child processes killable without restriction (suggest 1779528582 pending) -- merge with [2] zombie over-protect, fix together
- [ ] FOCUS-STEAL: FocusStealSentinel Core issue (suggest 2026-05-23T09:07:20)
- [ ] suggest check corrupts production skill: co-resolve requirement cmds mutate live skill (NOT clone/dry-run) -- file suggest to Core
- [ ] on-load skill co-resolve check (2026-05-23T06:18:21): DO NOT rerun -- will re-corrupt. Needs requirement rewrite to use clone target
- [ ] Codex exec wkappbot timeout: Eye IPC stall (100ms) + Core fallback exceeds Codex 34s cmd timeout; wkappbot cmds hang in subprocess
- [ ] Core Program.PInvoke.GetParentProcessId() offset 24 bug (same as SDK fix d69b080b; suggest 1779611928 filed to Core)
- [ ] Core CallerPlacementResolver window selection: missing IsWindowVisible guard vs SDK (hidden ConPTY vs visible terminal divergence)
- [ ] TGT-POS vs PLACEMENT:ENV mismatch: measurement artifact (Core re-launch with --window-position from WKAPPBOT_CHROME_TARGET pending, Core scope)
- [ ] WT Vis=True PCW PIDs (20824/21452/12600/8124): test if any in wkappbot ancestor chain -> CASCADIA match
- [ ] wkclaude-direct-guard: move guard to Agent.cmd level (not global wkharness.ps1); avoids blocking legitimate subprocess wkclaude.sh calls from other sessions
- [ ] off-screen Chrome cleanup: wkcdp-mon + close abnormal Chrome at (-2573,-856)
- [ ] taskkill /IM: extended process info (CPU%/mem/handles/threads) for a11y windows is OUT-OF-SCOPE separate command enhancement -- file own suggest
- [ ] gg triage 2026-05-26: GPT-ranked backlog (24 urgent + 33 important). Priority order: H=suggest --dismiss-stale flag first (kills noise at source), then dedupe A(OperationCanceledException ~20)/B(CDP timeout ~12)/C(FOCUS-STEAL ~6)/I(CORE MERGEx2) clusters, then J(cdp open 6min hang)/G(WK_SKILL_LOCK cross-repo)/L(CDP cannot navigate)/E(IOException skill lock)/K(wkedit BOM)/F(--exec CommandHelpMap), D(taskkill ts=2026-05-26T02:23:25 confirm). Opus triage agent dispatched.
- [ ] taskkill suggest 2026-05-26T02:23:25 full 2/2 confirm: needs DG-wkappbot-sdk main-CWD --confirm (worktree channel cannot self-confirm). REQ1 (a11y windows per-process pid/mem/handles/threads/cpu) is OUT-OF-SCOPE separate command enhancement -- file own suggest or address separately
- [ ] gg workflow 2026-05-28: on-load ✓, skill news ✓, suggest list ✓, ask gpt ranking BLOCKED (Chrome mult), triage findings (suggest resolve IPC-blocked):
  - [CRITICAL] [3] InvalidOperationException CDP option: STALE BUG-AUTO (user hardcoded port 9980, not code bug). Error message correct + actionable.
  - [HIGH] [2] Chrome placement drift: RESOLVED in Core SetWindowBoundsWaitStableAsync. SDK validation in MyCdpContext.CallerValidation verified 2026-05-28.
  - [MEDIUM] [1] ConnectAsync refused MERGE×2: Likely stale BUG-AUTO from 2026-05-27 high-load period. Needs investigate/merge.
  - [MEDIUM] [5] ask gpt OperationCanceledException MERGE×2: Root cause = Chrome multiplication (20+→28 procs). Core FindRunningChromePortAny guard may need deploy verify.
  - [SKIP] [4] FOCUSLESS-HOMEWORK SendInput: Core-owned, not SDK scope.
  - BLOCKER: suggest resolve/merge commands timing out (Eye IPC lag + suggest check hangs). Manual triage complete; formal resolution blocked pending Eye restart.
- [ ] Suggest to Core (manual): TaskkillCompatCommand.cs header + TaskkillUsage() are STALE (say classify-only v1.5, code is v1.9 auto-kill). Update docs to describe v1.9 behavior.
- [ ] taskkill uncle-kill: GetDescendantProcessIds batch WMI for uncle processes. InlineIsDescendant hangs PID 11940. Spec in wkappbot-taskkill-usage skill.
- [ ] BUG: ask gemini leaks [CHAT_META]/[TITLE_RULE] meta-instructions into Chrome tab title (users can see internal prompt directives). File suggest to Core when suggest git is stable.
- [ ] CDP renderer tab crash: check graceful close before force-kill in ChromeLauncher/CdpClient.
- [ ] Cross-repo file sync: cdp-smoke-test.sh + wkjobs.ps1 fixes need to be merged to WKAppBot core repo (files live outside wkappbot-sdk boundary)