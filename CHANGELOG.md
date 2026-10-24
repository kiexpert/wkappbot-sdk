# Changelog

All notable changes to WKAppBot SDK are documented here.
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [7.12.0-sdk] - 2026-10-21

### Aligned with WKAppBot core v7.12.0 — All-platform QA + Linux

**Highlights**
- **All-platform QA suite (`qa/`)** — one script-driven suite (`qa/run-qa.sh` / `qa\run-qa.cmd`) for the wkappbot CLI, `wkhippo` and the kih harness: static cross-platform path rules, public-repo hygiene, CLI command matrix, Hangul encoding round-trips, wkhippo matrix, live hook fixtures with a 1 s budget, and a sandboxed harness installer test (install / idempotent re-install / uninstall). Per-platform results land in `qa/results/<platform>.json`; `python3 qa/selftest.py` tests the suite itself.
- **Linux CLI + kih harness installer** — first real Linux run (kih container, pwsh 7.4.6, Linux minimal CLI): 34 PASS / 12 FAIL / 5 SKIP with mutations off. Three FAILs were checker issues and are fixed; the remaining FAILs are tracked product defects or environment gaps (see `qa/results/linux.json`). Windows and macOS columns have not been run yet.
- **Public-skill gate** — `qa/skill_home_plan.py` (dry run) proposes one home per skill id and marks a skill install-eligible only when no owner-mandated marker (credentials, account, PIN, customer, personal machine path, private app folder) and no kih public-skill gate word matches. Private-only ids are written as `private#<hash>` in this public repo.
- **Skill auto-commit / minimal auto-push** (core v7.12) — skill edits are committed automatically and pushed with the smallest possible change set.
- **wkhippo on Linux** (core v7.12) — the transcript indexer runs on Linux; covered by the `wkhippo` QA area.

**Security / hygiene**
- Removed every absolute path that named a personal machine user from public files (skills, generated skill pages, test scripts, `scripts/sync-public-skills.sh`) — replaced with `%USERPROFILE%`.
- Removed two stray debug dumps (`suggest_list_tmp.txt` and a mis-named copy) that had been committed to the repo root.

**Linux / macOS distribution**
- Release now ships the portable CLI beside `wkappbot.exe`: `wkappbot-linux-x64`, `wkappbot-osx-arm64`, `wkappbot-osx-x64` and `SHA256SUMS`; the core ships as `wkappbot-core.exe` (or `.xz` when over 100 MB).
- New `install.sh` (Linux / macOS): picks the asset for `uname -s` / `uname -m`, verifies it against `SHA256SUMS`, installs to `~/.local/bin`.
- New `Platform Release` workflow (`platform-release.yml`) owns `v*-sdk` releases: builds linux-x64 (ubuntu), osx-arm64 + osx-x64 (macos) and win-x64 core + launcher (windows) from core source, verifies each binary on its native runner (`--version`, `skill read on-load`, smoke; osx-x64 under Rosetta 2), and only then publishes. Any red stops the release. `release.yml` is now a manual, Windows-only legacy path.

**Fixed (all-platform QA findings)**
- Portable CLI `--version` names the binary's own runtime (an osx-x64 build under Rosetta 2 said osx-arm64).
- `file write` creates missing parent folders.
- `wkhippo` on Linux / macOS: `help`; finds `~/.claude/projects` (was searching a Windows-style path); `recent --json` prints exactly one JSON array; `trace` accepts a transcript path and exits nonzero for a missing session; the wrapper runs under `pwsh` and `python3` when Windows PowerShell / `python` are absent.
- kih harness: `--uninstall` removes everything install added; declarative bash-to-pwsh blocks no longer refuse the only shell on Linux / macOS; an empty `core.hooksPath` is blocked in every repo.
- Public-repo hygiene: the skill sync redaction matches any user's home path and any naver.com address (it had been reduced to a placeholder by the earlier sweep); three personal job-search skills and a personal address removed; test scripts use `$HOME` instead of an unexpanded `%USERPROFILE%`.
- QA suite: harness hooks run under a throwaway HOME and git config (a run had rewritten the machine's global `core.hooksPath`); the installer sandbox is a git repo; an explicit `--kih-root` never falls back to a sibling checkout.

**Known gaps (honest status)**
- Harness hook latency is 1.2-7 s per call on Linux (bare `pwsh` start is 0.25 s), above the 1 s mandate; owned by the kih harness.
- The Linux / macOS build is the minimal CLI (`skill`, `file`, `suggest`, `version`); UI automation is Windows-only.
- Windows: the kih installer (`install.ps1`) wires PreToolUse/PostToolUse but no Stop hook; the harness QA reports this as a WARN.

---

## [7.11.0-sdk] - 2026-09-30

### Aligned with WKAppBot core v7.11 — macOS Portable Phase 1 + CLI Improvements

**Core highlights**
- **macOS Portable Phase 1**: net8.0 multi-targeting for Abstractions, Shared, PluginContract, Android; Portable.slnf build path; EnableWindowsTargeting for macOS CI validation
- **Global options from any position**: --keep, --budget, --sudo, --timeout peel before subcommand via PeelPreCommandGlobalOpts (wkappbot-cli-argument-order-spec compliant)
- **file-edit diff output**: ANSI background coloring in diff hunks plus byte-change counts plus explicit success indicators
- **a11y-find output cleanup**: removed JSON and ANSI escape pollution from System and Mouse sections
- **Launcher safety**: PID verification before TerminateProcess prevents wrong-process kill

**Fixes**
- skill-search: --budget flag now properly consumes its value argument
- help: add missing ime-relay-daemon entry to CommandHelpMap
- Portable CI: relax global.json rollForward to latestFeature; correct Portable.slnf JSON format

See [WKAppBot core v7.11.0 release notes](https://github.com/kiexpert/WKAppBot/releases/tag/v7.11.0) for full details.

---

## [7.10.0-sdk] - 2026-09-28

### Aligned with WKAppBot core v7.10 — Indexer & Executor Consolidation

**Core highlights**
- **Memory system: wkhippo indexer parity** — regex search over Claude/Codex transcripts with persistent weekly cache; `wkhippo trace SID` for one-session event tails
- **ReadFormatter: exact-phrase then keyword fallback** — copy-pasted quotes never return "no match" when words are present
- **Executor & spawn-site: one core-path resolver** — every spawn call through `Program.ResolveExistingCoreExe`
- **Hotswap: swap only, never kill** — stale-worker kill loop removed from `PerformHotSwap`
- **Harness reflex** — branch-switch refusal in shared working trees; bash-pwsh flag-only refusal
- **Skill 3-tier migration** — `wk-unwired` fully tiered; SkillCommand.Edit gained delete-step verbatim-quote guard
- **Raw command line pipeline** — launcher forwards caller raw command line to Eye; MCP runner hands it via `WKAPPBOT_RAW_CMDLINE`

**SDK maintenance since v7.6.0-sdk**
- Bash-pwsh block refinement: matches only powershell/pwsh CALLED with a flag; path-prefixed powershell.exe still refused
- Codex SKILL.md mirrors retired at call site
- wkdoctor agy integration carried forward from 7.6.0-sdk

See [WKAppBot core v7.10.0 release notes](https://github.com/kiexpert/WKAppBot/releases/tag/v7.10.0) for full details. Note: SDK jumps 7.6.0-sdk → 7.10.0-sdk by design to align with core minor.

---

## [7.6.0-sdk] - 2026-06-09

### Added
- **`wkdoctor` agy integration**: check 08-harness-connection for antigravity CLI and Gemini settings auto-recovery
- **Alias symlink auto-repair**: doctor auto-heals tool alias breaks, routes to PATH order or doctor-bin fallback

### Fixed
- **Claude settings validation**: aligned validation with wildcard bypass rules in 08-harness-connection
- **agy wrapper shim policy**: reverted overly strict wrapper policy; delegates alias repair to wrapper install
- **Alias routing**: prefer PATH appbot bin for aliases; fallback to doctor folder when PATH missing
- **agy alias** + **gemini cli routing**: direct agy to Gemini for compatibility; routes via direct gemini CLI when configured

### Changed
- **Doctor ghost score/depth penalty**: normalized scoring for multi-repo diagnostics
- **Harness alias resolution**: english notes and summary for alias root labels

## [7.5.35-sdk] - 2026-06-04

### Added
- **gg-main health check enhancements**: release-notes quality check, dev-repo build status verification, GitHub service status monitoring
- **Skill browser v2**: deep-linking via URL hash (SEO-safe), sidebar navigation, sonnet-reflection 7-step display, 44-char preview limits for marketing appeal
- **Watchdog emergency-kill**: reap-protect logic for AI-spawned processes (pythonw/node/codex), priority-based process ranking (CPU/mem)
- **GHA auto-build**: GitHub Actions workflow (6h cron + on-push) to regenerate skill browser pages (443+ skills)
- **CLAUDE.md session tracking**: session-based pending item logging, gg-main autonomous fix documentation

### Fixed
- **Chrome health check**: count only main browser instances via MainWindowHandle filter, exclude renderer/GPU subprocesses (resolves false-positive multiplication alerts)
- **gg-main auto-cleanup**: route Stop-Process to wkappbot taskkill for kill-protection compliance (prevent accidental active-session termination)
- **Skill browser**: fetch path corrections, nav link structure migration (app/{id}.html), iframe sidebar integration
- **CI paths**: UX Audit and skill-page checks updated for v2 directory structure

### Changed
- **Skill browser architecture**: migrated to app/{id}.html structure (from flat index), separated index (nav-only) from skill pages (content-only)
- **Pending tracking**: session-based logging in CLAUDE.md (Pending block) with automated compress checkpoints (15-item threshold)

## [7.5.0-sdk] - 2026-05-30

### Fixed
- **wkask.sh**: `exec` line had stray `\n` literal between `Bypass` and `-File`, which broke the streaming relay entirely. Restored to a single line.
- **wkask.ps1**: colour-helper prefixes (`ok`/`inf`/`wrn`/`err`/`hdr`) were stored as mojibake bytes, rendering as `??` in console output. Replaced with clean ASCII tags (`[OK]/[INFO]/[WARN]/[ERR]/==`).
- **wkcdp-mon.ps1** (`Invoke-KillIdle`): off-screen check used negative-coord regex `^-\d{3,}`, false-positive on multi-monitor setups where the left monitor has large negative X. Now uses `MonitorFromPoint` (`WkWin32::IsOnAnyMonitor`), matching the anomaly-report logic.
- **wkzombie.ps1**: verified `try/catch` around `StartTime` returns `age=-1` and skips the process (Access Denied no longer kills protected wkappbot-core).
- **wkci.sh**: file mode set to executable (was `100644`, now `100755`) so CI helpers run without explicit `bash` prefix.

### Added
- **gg-main-enhanced**: Chrome multiplication auto-cleanup (>5 → kill duplicates, keep 2 oldest) integrated into the 3-hour health check loop.
- **MyCdpContext.ChromeHealthCheck**: `DiagnoseExcessiveChromeProcesses()` and `CleanExcessiveChromeProcesses()` for SDK-side mitigation when Core `FindRunningChromePortAny` guard is not yet deployed (deploy verification pending on Core repo).

### Notes
- WKAppBot core repository commit `41f722f49` carries the `wkask`/`wkcdp-mon` source fixes. Local mitigation is active in the SDK launcher and operator scripts.

## [7.4.0-sdk] - 2026-05-25

### Added
- **`wkdoctor`**: flutter-doctor-style SDK health check tool (`bin/wkdoctor.ps1`) — 10 checks with self-healing, plugin modules in `bin/wkappbot.hq/doctor/`, `-Json` flag for CI integration
- **Bootstrap auto-build**: `setup.ps1` auto-builds launcher from source on first clone; `build.cmd` creates `wkappbot.exe` inline (no subprocess calls)
- **Nightly schedule guard**: globalized to `~/.claude/settings.json` PreToolUse hook; no longer per-project `settings.local.json`

### Fixed (CRITICAL)
- **`cdp open` 6-minute hang on auth-wall sites** (claude.ai, gemini.google.com, groq.com, perplexity.ai, character.ai): `NavigateAsync` blocked until readyState complete; `EvalAsync` retries (SPA-title loop x10 + `InjectBarAsync`) stacked to 6+ min on Cloudflare/OAuth redirect pages. Fix: cap `NavigateAsync` to 3s in `WebCommands.Part3.cs` — port is live after `ConnectCdp`, full page load is Chrome's job.

### Fixed
- **CDP smoke test**: `--timeout` 9s (3s=fast site, 9s=human patience limit); log upload changed to `if: always()` so cancelled jobs also upload artifacts
- **build-skills-registry**: UTF-8 BOM strip before JSON parse; push set to `continue-on-error`
- **cross-repo-claude-notify skill**: PROCWD CLASS step recovered after pipe separator split bug corrupted step content

## [7.3.1-sdk] - 2026-05-16

### Fixed (CRITICAL)
- **CDP popup auto-dismiss**: `CdpClient.PopupDismiss.cs` runs before prompt injection on all ask flows (GPT/Gemini/Claude). Stage 1: ChatGPT login prompt — clicks "Stay logged out"/"Use as guest" to maintain anonymous session. Stage 2: generic modal/dialog overlay — finds and clicks X/close button. Exception: AI service cookie consent banners on chatgpt.com, gemini.google.com, claude.ai, copilot.microsoft.com are intentionally skipped.
- **Chrome content size stabilization**: 800×600 viewport enforced on all CDP-managed Chrome windows (outer 804×710 accounting for browser frame). Prevents layout reflow from triggering session restore geometry.
- **SetWindowBounds drift retry**: `SetWindowBoundsAggressiveAsync` wraps SetWindowBoundsAsync with 3-attempt poll-and-correct loop (500ms delay). Corrects position drift >50px after initial placement. New Chrome launches: DRIFT=OK confirmed.

## [7.3.0-sdk] - 2026-05-16

### Added
- **`wkfind`**: unified code + Claude-session search tool with multi-keyword GlobCoverageScore ranking — PHRASE/AND/OR tiered scoring, git-diff + time-range fallback (`--day`/`--week`/`--month`/`--year`/`--unlimited`), top 3 sessions + top 10 code matches per tier
- **`wkask`**: real-time ask (Stage 3) pipeline health monitor — fires question, streams live response color-coded by AI (GPT/Gemini/Claude), detects Stage 2/3 placement corrections, reports timing
- **HWND validation persistence**: launcher writes off-screen / PseudoConsole / desktop / process-mismatch rejections to `cdp-state.jsonl` for postmortem audit
- **Daily market triad teaser workflow**: CI workflow dispatches daily market triad (GPT + Gemini + Claude) on branch push, reuses latest KST daily issue, centralizes issue resolution
- **Standard window skills**: `standard-appbot-window` + `standard-chrome-window` reference skills documenting IPC and DPI conventions

### Fixed (CRITICAL)
- **Chrome session restore position override**: two-layer fix — `ChromeLauncher.SessionRestore` clears cached geometry on restart, `CdpClient.WindowStabilize` re-asserts target rect post-restore. Eliminates Chrome jumping to last-session position on hot-swap
- **Launcher caller HWND resolution**: `ResolveCallerTerminalHwnd` now walks the process ancestor chain only — no P1/P3 foreground fallbacks. P2 finds nearest ancestor with any window (not just terminal-class); visible windows apply `GA_ROOT` filter; hidden ConPTY windows are included via `GetWindowRect` (one HWND for placement and input)
- **Off-screen caller rejection**: launcher rejects zero / `PseudoConsoleWindow` / desktop / off-screen callers fail-fast with explicit error; auto-resolves to valid alternative window or falls back to primary monitor anchor
- **On-screen Chrome placement guard**: prevents window position mismatch (RIQUA-SAFE) by anchoring Chrome to on-screen rect even when caller geometry is partly off-screen
- **Caller HWND via process chain**: replaces foreground-based detection with parent-process window lookup (`NtQueryInformationProcess`); forwards `WKAPPBOT_CALLER_HWND` through IOCP env block + Eye-pipe path
- **IME Relay jamo-sync**: Korean Hangul jamo state stays in sync across focus transitions; ConPTY toggles no longer race with `ImmSetConversionStatus` (Core v7.3)
- **Stage 2 fail-fast**: log all CDP / WebSocket / `/json/list` errors to stderr immediately; reduce selector-unavailable timeout 10s → 3s; fail fast on CDP error frames instead of timeout
- **`cdp-mon` multi-monitor off-screen false positive**: regex `'^-\d{3,}'` replaced with `MonitorFromPoint` Win32 API — matches `IsWindowOnScreen` behavior, no longer flags negative-x monitor as off-screen
- **Chat command default model**: defaults to Haiku; `claude` keyword + model-name argument supported
- **Publish self-extract defense**: `IncludeAllContentForSelfExtract=true` suppresses `System.IO.FileSystem.Watcher` shim-assembly `FileNotFoundException` false positive during hot-swap

### Changed
- **Launcher partial split**: `MyCdpContext.cs` split from 1281 lines into 6 partials (each ≤ 291 lines) — under the 400-line soft cap
- **Chrome placement**: 18×9 compensation factor + validation threshold adjustment for high-DPI multi-monitor setups
- **Caller-HWND validation**: extended to `cdp` command path with foreign-process guard

### Docs
- `wkfind` GlobCoverageScore algorithm, time-range synchronization, multi-keyword discovery flow
- `wkask` real-time monitoring usage in CLAUDE.md internal tools
- Scope of Work section in CLAUDE.md (SDK launcher ownership boundary vs Core repo)
- `cdp-mon` off-screen false-positive gotcha for multi-monitor setups
- BUG-AUTO false-positive notes for CLR shim assemblies (`System.IO.FileSystem.Watcher`, `PublicKeyToken`, `Culture=neutral`)
- Suggest evidence scripts: Chrome window position mismatch, session CommandHelpMap fix, hidden-mode MCP hang, FSW false positive
- Stale version reference cleanup (`v5.8` → `v7.2.0`)

## [7.2.0-sdk] - 2026-05-08

### Added
- **`wkappbot session list`**: unified session discovery for Claude + Codex + Gemini — `--claude`, `--codex`, `--gemini`, `--cwd`, `--days N`, keyword filter; shows slug/thread name, size, age in seconds; chains to `claude-session-handoff` for recovery
- **CDP dialog auto-handling**: `Page.javascriptDialogOpening` unconditionally handled in `CdpClient` receive loop — auto-dismiss + `[CDP:DIALOG:BLOCKED]` red log + `wkappbot speak` audible alert on every connection
- **Triad dialog recovery**: `EvalAsync` retries once after dialog auto-dismiss (5s window, 300ms delay) — triad gives 3/3 results even when one tab has a blocking alert
- **Node error inspection**: `[CDP:NODE-ERROR]` red log + background page dump on node access failures (`No node`/`Cannot find context`/`detached`) — outputs URL, title, readyState, dialog nodes, top-8 interactive targets with selectors for retargeting
- **`EnsureClaudeGuideSetup`**: auto-creates/appends `CLAUDE.md` on first run — global gets Session Start block (workflow keywords), project gets skill search line (tech-stack keywords) + gg workflow template; 30-day marker prevents repeat

### Fixed
- **Korean skill search**: `SkillRecord.SearchText` used `JavaScriptEncoder.Default` escaping CJK to `\uXXXX` — switched to `UnsafeRelaxedJsonEscaping` so `wkappbot skill search 뉴비` finds tagged skills
- **IME Han/Yeong key**: ConPTY has no IME context so `IsSystemImeKoreanMode()` always returned false, immediately overwriting manual toggles via focus-sync. Fixed with 1.2s grace period (`_lastToggleTickMs`) + `ForceSystemImeMode()` calling `ImmSetConversionStatus` directly on the foreground window

### Fixed
- **`--new-tab` was dead parameter**: now clears `AskTargetRegistry` sandbox key before `GetOrCreateSandboxedTabAsync`, forcing a fresh tab instead of cache reuse
- **Tab cap enforcement path**: `EnforceTabCapAsync` now fires fire-and-forget on every ask entry (`EnsureCdpConnection`), not just `CdpTabManager.CreateScoped`
- **Idle tab recycling**: `PurgeIdleTabsAsync` tries to recycle (ping 1s + `Page.navigate` fire-and-forget) before closing — alert dismiss (500ms) before ping, 2s total budget; renderer dead or dialog stuck → kill + new tab

### Changed
- `RecycleIdleTabAsync`: simplified from 15s (switch 3s + ping 2s + navigate 8s + url-check 2s) to 2s (switch 1s + ping 1s) — navigate fired without waiting, URL validation handled by next ask

## [7.1.4-sdk] - 2026-05-08

### Fixed
- **Args leak**: launcher-injected flags (`--exit-event`, `--exit-file`) leaked into `speak` TTS text, `ask` prompts, and `suggest` text — now stripped globally before dispatch
- **`--caller-pid` preserve**: SpeakCommand needs `--caller-pid` for overlay targeting; fix to only strip `--exit-event`/`--exit-file`, not all launcher flags
- **Chrome position mismatch auto-report**: `SetWindowBoundsAsync` now verifies actual position post-set; if drift > 50px, auto-files `[BUG-AUTO]` suggest with callstack (Chrome session restore override detection)

## [7.1.3-sdk] - 2026-05-08

### Fixed (CRITICAL)
- **Chat startup loop**: `ChatStartupKeyName()` used `GetHashCode()` (randomized per-process) causing 203 duplicate registry entries under `WKAppBot_Chat_*`. Every reboot launched 203 wkchat processes simultaneously, crashing the system. Fixed to use `DerivePort()` (SHA256-stable) -- key is now `WKAppBot_Chat_{port}` (e.g. `WKAppBot_Chat_9740`), one entry per project.
- `FindRunningChromePortAny()`: unregistered Chrome (port file missing) was accepted; now requires strict port file match
- Foreign Chrome rejection in `WebOpenCommand` reuse check via 4-port block
- `CdpTabManager.GetSessionTag()` used `GetHashCode()` vs SHA256 mismatch with `WebCommands`
- `CdpTabManager.CreateScoped()` still used HWND for sandbox key; unified to SHA256 CWD hash

### Changed
- Launcher `ResolveCoreExe()`: only uses `.new.exe` when it is **newer** than current core (timestamp check)

## [7.1.0-sdk] - 2026-05-08

### Added
- CDP port isolation: SHA256-based deterministic 4-port block per project CWD (9300-9995)
- `ev-cdp-isolation.ps1`: evidence script verifying CDP isolation fixes (9 checks)
- IME daemon self-update: exits on newer binary detection (10s check cycle)
- Eye tick auto-replaces old IME daemon every 60s -- no wkchat restart needed
- `DeployLauncherTask` in csproj: rename-old + copy-new deploys to all bin dirs on publish

### Changed
- `DerivePort()`: SHA256 instead of `GetHashCode()` (non-deterministic across processes)
- Port derivation uses parent process CWD via `NtQueryInformationProcess` (Claude changes dirs)
- `BuildSandboxKey()`: CWD hash instead of HWND -- stable across sessions
- Chrome always launched with `--window-position`; geometry file persistence removed
- `ResolveCoreExe()`: uses `.new.exe` only when newer than current core (timestamp check)

### Fixed
- `FindRunningChromePortAny()`: rejects unregistered Chrome (registered=0 now skips)
- Foreign Chrome rejection in `WebOpenCommand` reuse check via 4-port block validation
- `--port N` outside project block: `AppBotExit(1)` with clear error message
- `cdp open` sandbox key used HWND → tab duplicated on each session; fixed to CWD hash
- `CdpTabManager.CreateScoped()` still used HWND for key; unified to SHA256 CWD hash
- `CdpTabManager.GetSessionTag()` used `GetHashCode()` vs `WebCommands` SHA256 mismatch
- IME relay: `ImmGetContext` side-effect created IME context on ConPTY -- caused tripling
- IME relay: proxy always initializes Korean mode; `_koreanMode` syncs with system
- Chat command intercept disabled in `PseudoConsoleRunner` (caused I/O corruption)
- Nested `chat` sessions route as one-shot via `WKAPPBOT_CHAT_SESSION` env inheritance

## [6.5.21-sdk] - 2026-04-29

### Added
- `publish-core-for-sdk.yml` CI workflow: dev core binary auto-released as standalone asset
- MIT `LICENSE` file
- `CONTRIBUTING.md`, `CODE_OF_CONDUCT.md`, `SECURITY.md`
- Issue templates (bug, feature, license question), PR template
- GitHub Discussions enabled

### Changed
- Recommended install path updated to `%USERPROFILE%\Documents\wkappbot` (privacy rationale)
- `build.yml`: `fetch-depth: 0` for correct patch version counting
- `build.yml`: core binary now downloaded from dev `core-*` release, source build as fallback
- `find-stable-release.ps1`: `gh auth token` local fallback, `-Force` flag, JSON guard

### Fixed
- Launcher `WKAppBot.Launcher.csproj`: added `ProjectRoot.cs` compile include (Hash8 build error)
- `build.yml`: volatile `runtime/` excluded from artifact upload (ENOENT race)
- `build.yml`: `GH_TOKEN` forwarded to smoke test for license auth

### Security
- Removed Slack `webhook.json`, `.mcp.json`, `.wkappbot/`, `docs/handoff/`, `.ci-test-tmp/` from full git history
- Replaced hardcoded internal paths in `AgentsPolicy.cs` and `AppBotEyePromptInfo.cs` with env vars
- `AppBotUpdateCommand`: default repo changed from private to `kiexpert/wkappbot-sdk`

## [6.5.0-sdk] - 2026-04-28

### Added
- Initial v6.5 SDK release
- Per-repo Eye isolation via `ProjectRoot.Hash8()` pipe names
- ECDSA offline license system (`LicenseManager.cs`)
- `build.cmd --download-only` for binary-only install
- GitHub collaborator-based license tier auth (in closed core)

## [6.0.0-sdk] - 2026-04-28

### Added
- Initial public SDK release
- MIT launcher source (`WKAppBot.Launcher`)
- AOT-compiled launcher binary (~1 MB)
- Automated smoke test suite (25 basic checks)
