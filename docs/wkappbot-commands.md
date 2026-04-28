# WKAppBot Command Reference

> Auto-generated from `wkappbot-core.exe v6.0.0`
> Regenerate: `bash scripts/gen-command-ref.sh`

Generated: 2026-04-28

## Coverage legend (smoke-help.ps1)

| Symbol | Meaning |
|--------|---------|
| ✅ | 3+ tests |
| ⚠️ | 1-2 tests |
| ❌ | No tests yet |

---

## Global flags (apply to all commands)

```
--timeout <duration>    Hard kill after N time (e.g. 30, 2m, 500ms)
--no-regression         Skip regression self-tests
--help                  Show help for a command
--version               Print version and exit
--nth N                 Target the Nth match (1-based)
--nth N~                Target the Nth match onwards (all remaining)
--all                   Target all matches
--worker / WKAPPBOT_WORKER=1  Suppress Eye auto-spawn
--sudo                  Elevate to admin (UAC prompt)
--stderr                Show buffered stderr (launcher relay)
```

---

## `wkappbot a11y`

```
a11y <action> <grap>[#uia-scope] [options]
Universal accessibility control: UIA -> Win32 -> SendInput 3-tier fallback.

Actions: close  minimize  maximize  restore  focus  move  resize
         read  find  highlight  invoke  click  toggle  expand  collapse
         select  scroll  type  set-value  set-range
         clipboard  clipboard-read  clipboard-write  wait  eval

Grap patterns:
  AI target language for windows and accessibility nodes.
  Humans browse windows; AIs point with grap.
  Interactive actions stop at the first exact match.
  Full lookup stays array-shaped for ranking and review.
  "*App*"              glob wildcard
  "regex:btn_\\d+"     regex
  "*App*;*Calc*"       OR (semicolon)
  "App#MenuBar"        # = UIA scope drill-down
  "Chrome#ChatGPT#btn" tab portal (auto tab switch)
  "adb://device#elem"  Android ADB

Key options:
  --nth N / N~ / ~N / 2~4 / 1,3~   which match (default: first)
  --all                      apply to all matches
  --depth N                  UIA tree depth
  --force                    skip safety guards
  --hotkey                   type: label/menu dispatch (not raw keys)
  --eval-js "js"             pre-hook or primary output (web)

Examples:
  a11y invoke "*MenuBar*#*File*"
  a11y type "*edit*" "hello world"
  a11y type "*app*#*MenuBar*" "File/Save" --hotkey
  a11y read "*Chrome*#chatgpt.com" --eval-js "document.title"
  a11y close "*Chrome*" --nth 2~

Skills for 'a11y': (wkappbot skill read <id>)
  - a11y close safety: ambiguity + focus-node diagnostics  [a11y-close-safety-ambiguity-focus-node-diagnostics] [unclassified]
  - a11y find - output format and process resolution  [a11y-find] [operator/project (mixed)]
  - a11y playbook writing  [a11y-playbook-writing] [project (pure)]
  - a11y Command Cheatsheet - Operator View for Claude and Codex  [a11y-command-cheatsheet] [unclassified]
```

---

## `wkappbot file`

```
file <subcommand> [options]
File operations with 4-stage encoding detection (UTF-8/CP949).

Subcommands:
  open <path>[:line[:col]]
      Smart open in responsible VS Code window, then goto line/column.
  read <path> [--offset N] [--limit N] [--encoding 949|utf-16]
      Read with line numbers. .pdf -> auto-routes to read-pdf.
      Aliases: --path/--file, --start/--end/--count, --no-line-numbers.
  write <path> [--encoding N] (--stdin | --text "..." | --file <src>) [--append]
      Auto backup ON. Aliases: --path, --content, --source-file, --dry-run.
  edit <old> <new> <path> [--replace-all] [--context N] [--tab-size N]
      Indent-based block context. C-style escapes (\\n \\t \\r \\\\).
      Same old+new = search-only (no write). --old-file/--new-file for Korean args.
      Option aliases also work: --old-string/--text/--path/--dry-run.
  grep <regex> [--path <dir>] [--type <ext>] [-i] [-C N] [--max N]
      Regex search. ';' OR in --path: "D:/A;B" expands.
      Aliases: --pattern/--query, --root, --file.
  glob <pattern> [--path <dir>]
      ** glob. NOTE: ALWAYS use **/ prefix: "**/*.cs" not "*.cs".
      Alias: --pattern.

Aliases: file-open, file-read, file-edit, file-grep, file-glob (hyphenated)
wkedit.exe -- busybox symlink -- file edit auto-routing

NOTE: CP949 source files -- some WKAppBot sources have Korean in CP949.
  Check encoding before editing! Corruption = unrecoverable.

Skills for 'file': (wkappbot skill read <id>)
  - file -- Read/Write/Edit/Grep/Glob Cheatsheet  [file-command-cheatsheet] [operator (pure)]
  - Public launcher DataDir: use WKAPPBOT_HOME or %USERPROFILE%/.wkappbot  [public-launcher-datadir-use-wkappbot-home-or-userprofile-wkappbot] [unclassified]
```

---

## `wkappbot skill`

```
skill <subcommand> [options]
AI skill management -- executable knowhow for Claude instances.
Storage: {callerCwd}/skills/ (project, git-tracked)
         {hq}/skills/       (installed, shared across all repos)

Subcommands:
  list [app]                        List skills grouped by app, most recent first
  read <id>                         Full detail: steps, refs, version
  search <keyword> [--app X]        Search title/desc/tags/steps
  contribute --app X --title Y --desc Z
             [--steps "s1|s2"] [--tags "t1,t2"] [--id <slug>]
             [--refs "file:line:pattern|..."]
                                   Create or update (auto version bump)
  edit <id> [--title X] [--desc X] [--add-step X]
            [--add-requirement "cmd => expected"]
                                   Partial update; auto version bump.
                                   COPY-ON-EDIT: if skill is HQ-only (not in your
                                   project skills/), it is automatically forked into
                                   your repo's skills/ so you can edit and contribute.
                                   Higher-version fork propagates back via sync.
  delete <id>                       Remove from project dir
  install [--app X] [--force]       Copy project -> HQ (runs on publish)
  sync                              Immediately sync peer repo skills into HQ
                                   (Eye runs this every 30 min automatically)
  export [--app X] [--out f.zip]    Export to zip
  import <file.zip>                 Import into project dir
  verify <id>                       Check source_refs still match code
  audit [--app X]                   Audit all skills for stale/missing refs

Cross-repo skill flow:
  Any repo (suggester) can view all skills via `skill read` (shared HQ).
  Any repo can add requirements to a suggest: `suggest add-requirement`.
  Any repo can fork+edit an HQ skill: `skill edit` auto-forks if needed.
  Eye syncs peer repo skills/ → HQ every 30 min; version field resolves conflicts.

Examples:
  wkappbot skill list
  wkappbot skill sync
  wkappbot skill read handoff-checklist
  wkappbot skill edit my-skill --add-requirement "wkappbot windows => nfrunlite shown"
  wkappbot skill contribute --app wkappbot-webbot --title "X" --desc "Y" \
    --refs "csharp/src/WKAppBot.WebBot/CdpClient.cs::pattern"

Skills for 'skill': (wkappbot skill read <id>)
  - Skill Audience Taxonomy  [skill-audience-taxonomy] [operator/developer/project/user (mixed)]
  - Skill Authoring Rules + Contribute & Edit Workflow  [skill-contribute-edit] [operator/developer/project (mixed)]
  - Codex-Friendly Skill Template  [codex-friendly-skill-template] [operator/developer (mixed)]
  - Developer Skill Template  [developer-skill-template] [developer (pure)]
```

---

## `wkappbot schedule`

```
schedule add|list|remove|clear [options]
Manage scheduled prompts for auto-delivery to Claude.

schedule add "<prompt>" --at "2026-03-30T15:00" [--repeat 1h]
schedule list     show pending items
schedule remove <id>
schedule clear    remove all

Schedules are executed by Eye (must be running).

Skills for 'schedule': (wkappbot skill read <id>)
  - Joodeok YouTube Market Schedule Extractor -- Auto-Subtitle Transcript Pipeline  [joodeok-market-schedule-extractor] [unclassified]
  - AI News Briefing — lunch and dinner automated digest  [ai-news-briefing] [unclassified]
```

---

## `wkappbot eye`

```
eye [options]
WK AppBot Eye -- persistent Slack daemon + status overlay.
Runs as background process, auto-spawned by most commands.

Key behaviors:
  - Receives Slack messages -> queues to slack_queue/ -> routes to Claude
  - Posts Eye status card to Slack (context%, current task, cwd)
  - Auto-deletes stale idle messages on restart (spam prevention)
  - Hot-swap: detects .new.exe -> transparent swap, no restart needed

eye tick           one-shot status check (ctx%, cards, queue stats)
eye --interval N   loop interval in ms (default: 100)

NOTE: Do NOT spawn Eye directly -- let normal commands trigger it.
NOTE: eye tick does NOT spawn Eye (by design).

Queue: wkappbot.hq/runtime/slack_queue/*.json
  Each received Slack message = one file, drain every 1s.
  [EYE_QUEUE] in tick output shows pending/processing counts.

Skills for 'eye': (wkappbot skill read <id>)
  - Eye Slack Thread -- Live Streaming Structure & CCABot Slot Design  [eye-slack-thread-streaming-structure] [project (pure)]
  - Always-On AI Automation Daemon -- AppBot Eye Internals  [always-on-ai-automation-daemon-appbot-eye-internals] [project (pure)]
  - Admin Eye 2-phase broadcast close for clean hand-over  [admin-eye-broadcast-close-handover] [project (pure)]
  - Admin Eye Pipe Server -- Self-Respawn + Hot-Swap Deferral  [elevated-eye-admin-pipe-server] [project (pure)]
```

---

## `wkappbot license`

```
No --help entry for: license
Run 'wkappbot --help' for full command list.
```

---

## `wkappbot suggest`

```
suggest [subcommand]
AI suggestion management: propose / review / resolve workflow.

Subcommands:
  (no args)         submit a suggestion (opens editor or reads stdin)
  list              show active (unresolved) suggestions
  resolve <ts>      mark resolved (REQUIRES evidence script -- see below!)
  add-requirement <ts> "cmd => expected" [--skill <id>]
                    add a behavioral requirement to a pending suggest;
                    with --skill: also updates the named skill immediately
                    (any repo/contributor can add requirements, not just maintainer)
  repost            re-send suggestions to Slack if ts missing

REQUIREMENTS (3 required for human suggests):
  --requirement "wkappbot <cmd> <args> => <expected output>"
  NOTE: do NOT include launcher flags (--sudo, --no-wait) in requirement cmd.
        Strip them: 'wkappbot --sudo a11y ...' -> 'wkappbot a11y ...'

RESOLVE GUARD -- mandatory test evidence required:
  wkappbot suggest resolve <ts> "note"
    --i-completed-<cmd>-<subcmd>-willkim-allowed-this-script <test.sh|test.ps1|test.cmd>

  Evidence filename must match: test-{cmd}-{subcmd}-*.<sh|ps1|cmd>
  Script must: reference the command, exit 0 (all tests pass)
  Failure -> resolve BLOCKED (fix the test or the code first)

  Regression: ALL previously stored scripts in experience/tests/{cmd}/{subcmd}/
  are re-run on each resolve. If any fail -> blocked.

evidence_file is saved to history so you can trace what was tested.

Examples:
  wkappbot suggest add-requirement 2026-04-26T10 "wkappbot suggest list => pending suggests shown" --skill suggest-co-resolve-2of2
  wkappbot suggest resolve 2026-03-17T05 "fixed logcat --dbg race"
    --i-completed-logcat-dbg-willkim-allowed-this-script test/test-logcat-dbg-listener.sh

Skills for 'suggest': (wkappbot skill read <id>)
  - Suggest Triage Playbook  [suggest-triage-playbook] [operator (pure)]
  - 서제스트 관리 워크플로우  [suggest-workflow] [operator (pure)]
  - SDK suggest and skill filing rules for AI agents  [sdk-suggest-skill-filing-rules] [unclassified]
  - AutoHeal - User Input Protection: Focusless-First + Focus Theft Recovery  [autoheal-userinput-protection] [project (pure)]
```

---

## `wkappbot ask`

```
ask gpt|gemini|claude "<question>" [file.png] [--slack] [--new-tab]
Ask AI via CDP (Chrome DevTools Protocol, focusless).

ask triad "<question>" [--debate [N]]
  Parallel GPT + Gemini + Claude. --debate = triad-style multi-round synthesis.

ask gpt|gemini|claude (agent mode):
  Autonomous sub-agent loop with tools.

NOTE: Always ask in ENGLISH -- Korean = 3-4x token waste.
NOTE: CDP requires Chrome/Edge open with target AI tab.
NOTE: Result artifact = chat text only (CDP eval DOM read for full content).

Skills for 'ask': (wkappbot skill read <id>)
  - ask -- AI Delegation Cheatsheet  [ask-command-cheatsheet] [operator (pure)]
  - 막히면 삼두에 위임하라 (토큰 절약)  [ask-triad-when-uncertain] [operator (pure)]
  - Triad Ask: Simultaneous Q, 정반합 Debate, Tool Loop, Agent Mode, Session Recovery  [triad-ask-simultaneous-q-debate-tool-loop-agent-mode] [operator (pure)]
  - Web AI Ask Infrastructure: Pump, CDP Worker, Editor Send Paths  [web-ai-ask-infrastructure-pump-cdp-worker-editor-send-paths] [project (pure)]
```

---

## `wkappbot slack`

```
slack <subcommand> [options]
Slack messaging via Bot API (Socket Mode).

Subcommands:
  send "<msg>" [file.png]             send to main channel
  reply "<msg>" --msg <ts> [file.png] thread reply
  upload <file> [--msg <ts>]          upload file (optionally to thread)
  route --queue                       drain slack_queue/ (internal, Eye only)
  route --file <path>                 single message route (test use)
  route '<json>'                      inline JSON route (test use)

RULE: Always reply to Slack messages IN SLACK via `slack reply --msg <ts>`.
  Answering only in the Claude Code prompt is NOT acceptable.
NOTE: `slack route` is an internal Eye command -- do NOT call manually unless testing.

Skills for 'slack': (wkappbot skill read <id>)
  - slack -- Send & Reply Cheatsheet  [slack-command-cheatsheet] [operator (pure)]
  - Eye Slack Thread -- Live Streaming Structure & CCABot Slot Design  [eye-slack-thread-streaming-structure] [project (pure)]
  - AI News Briefing — lunch and dinner automated digest  [ai-news-briefing] [unclassified]
```

---

## `wkappbot logcat`

```
logcat [regex] [file1.glob ...] [options]
Stream or search wkappbot logs. grep-style argument order.

First arg = content regex (';' = OR). Remaining = file globs (default: *.log).
Path segment ';' OR: "logs/媛;<name>*.log" expands to 3 patterns.

Key options:
  --hq            include wkappbot.hq/logs (finished logs live here)
  --past <dur>    scan existing files (1h, 30m, 2d). Without -f: grep-exit.
  -f / --follow   live tail after --past scan
  --timeout <dur> auto-exit after N seconds (30s, 5m)
  --dbg [pid]     capture OutputDebugString via DBWIN_BUFFER shared memory
  --json          structural JSON key+value matching (AND logic)
  -i / -n / -C N  case-insensitive / line numbers / context lines
  -v / -l / -c    invert / filenames-only / count

Examples:
  logcat "SLACK\|ROUTE" --hq --past 1h
  logcat "error" *.log --past 30m -f
  logcat '{"role":"user"}' *.jsonl --json --hq --past 2h
  grap "keyword" *.log --hq --past 30m     (grep-compat alias)

Skills for 'logcat': (wkappbot skill read <id>)
  - logcat -- Log Viewer Cheatsheet  [logcat-command-cheatsheet] [operator (pure)]
```

---

## `wkappbot gc`

```
gc [pattern] [--days N] [--sweep]
Garbage collect old logs, temp files, stale caches.
--days N: only files older than N days (default: 7).
--sweep: aggressive cleanup.

Skills for 'gc': (wkappbot skill read <id>)
  - logcat -- Log Viewer Cheatsheet  [logcat-command-cheatsheet] [operator (pure)]
```

---

## `wkappbot validate`

```
validate <scenario.yaml>
Validate YAML scenario syntax without executing.
```

---

## `wkappbot windows`

```
windows [grap]  →  converted to a11y find (v6.1)
Alias: wkappbot windows <grap> == wkappbot a11y find <grap>
No args: finds wkappbot* windows. Use a11y find directly for full options.

Skills for 'windows': (wkappbot skill read <id>)
  - Anthropic Computer Use — cross-platform AI desktop control (API + Cowork/Dispatch)  [anthropic-computer-use-cross-platform] [user/developer/operator (mixed)]
```

---

## `wkappbot run`

```
run <scenario.yaml | exec-key | exe-path | bare-exe>
Polymorphic: .yaml/.yml/.xmf = scenario; preset key (hero4/calc/notepad/...) = exec; raw exe path OR PATH-resolvable name = ad-hoc exec launch.

Skills for 'run': (wkappbot skill read <id>)
  - wkappbot exec/run managed launcher + stdin-inject + Ctrl+C/Z control chars  [wkappbot-run-hts-launch-loading-trace] [unclassified]
  - CDP Runtime.enable with retry  [cdp-runtime-enable-with-retry] [project (pure)]
```

---

## `wkappbot speak`

```
speak "text" [--bg] [--mouse] [--target <grap>] [--voice <name|culture>] [--size N]
Windows TTS voice output + karaoke overlay.
--bg: background (return immediately).
--mouse: overlay at cursor position.
--target <grap>: overlay on specified window.
--voice <name|culture>: select an installed voice.
--size N: font size px (default 32).

Skills for 'speak': (wkappbot skill read <id>)
  - AI News Briefing — lunch and dinner automated digest  [ai-news-briefing] [unclassified]
```

---

## `wkappbot claude-usage`

```
claude-usage
Show Claude API token usage: JSONL size + ctx% estimate.

ctx% = total JSONL size / ~20MB context limit.
Warns at 8MB, urgent at 10MB.

Also probes claude.ai/settings/usage via CDP for plan usage %
(fallback: UIA scrape of Claude Desktop).
```

---

## `wkappbot mcp`

```
```

---

## `wkappbot win-move`

```
win-move <grap> --x N --y N [--w N] [--h N]
Move/resize a window by grap pattern.
```

---

## `wkappbot newchat`

```
newchat "<prompt>" [--file prompt.txt]
Open new Claude Desktop chat + submit prompt (all focusless UIA).

Use for session handoff when context reaches 90%+.
Prompt is typed via WM_CHAR (focusless) -- no focus steal.

NOTE: Large prompts: use --file to avoid shell escaping issues.
NOTE: Claude Desktop must be open and visible.
```

---

