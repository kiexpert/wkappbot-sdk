# Changelog

All notable changes to WKAppBot SDK are documented here.
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

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
