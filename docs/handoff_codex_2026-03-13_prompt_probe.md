# Codex Handoff - 2026-03-13 (Prompt Probe / Routing / Build Reliability)

Author: codex

## Scope
- Prompt routing diagnostics and safety hardening.
- Probe instrumentation for deterministic debugging.
- Build/deploy hot-swap reliability improvements.

## Implemented Changes

### 1) `prompt-probe` command and CLI wiring
- Added new command entry in `Program.cs`: `prompt-probe`.
- Added usage/help entry in `UsageCommand.cs`.
- Added command implementation file:
  - `csharp/src/WKAppBot.CLI/Commands/PromptProbeCommand.cs`

### 2) Author-pattern analysis (thread owner + recent authors input)
- `--names` (comma/semicolon list) and positional names are supported.
- For each author token:
  - infers host hint: `codex | claude | any`
  - extracts workspace tag from bracket form (e.g., `[WG-WKAppBot]`)
- Prints an explicit analysis block:
  - target host
  - cwd folder
  - workspace tags
  - per-author host/workspace inference

### 3) Candidate prompt diagnostics (full dump)
- Enumerates candidates with scoring and reasons.
- Per candidate, prints:
  - hwnd/pid/process/class/title/rect/visible/iconic
  - host match / cwd match
  - mapped prompt host (when known)
- `FindAllPrompts()` details are dumped first to avoid ambiguity.

### 4) Stage-based probe output with pass/fail markers
- Added deterministic stage logs for every candidate:
  - `stage.window-state`
  - `stage.prompt-snapshot`
  - `stage.input-roundtrip`
  - `stage.uia-summary`
- Each stage prints `ok=true|false`.

### 5) Runtime snapshot details per prompt candidate
- Added prompt runtime snapshot:
  - current prompt draft candidate text
  - recent AI status hint candidates
  - submit state (`turnForm`, submit found/enabled, button name)
- Added window runtime state:
  - foreground/enabled/visible/iconic/showCmd
  - relevant window props (`WKAppBot*`, etc.)

### 6) Input round-trip probe defaults and marker policy
- Round-trip probe is enabled by default in probe flow.
- Test marker changed to a CP949-safe visible symbol: `☆`.
- Probe checks:
  - write success
  - insert observation
  - restore success
  - restored-to-original equality

### 7) Safety guardrails to prevent editor mis-typing
- Added prompt certainty gate before input round-trip:
  - `claude-desktop`: requires legacy `turn-form + submit` confidence.
  - `codex-desktop`: requires marker/rect confidence.
  - `vscode-claudecode`: skipped by default for safety.
- Output now shows:
  - `certainty=true|false`
  - skip reason when not certain.

### 8) Probe write pipeline split (main -> bridge -> fallback)
- Input probe now uses explicit phases:
  - main: focusless-only text write attempt
  - bridge: standard InputReadiness probe (`AutoApproveYield`, blocker/elevation/iconic/visibility checks)
  - fallback: only allowed when bridge is ready
- Explicit logs:
  - `input.write.main-focusless ok=...`
  - `input.write.bridge ok=...`
  - `input.write.fallback ok=...`
  - same for restore phase

### 9) Thread-scoped routing instrumentation
- Added detailed per-candidate routing logs in thread scoped prompt resolution:
  - owner/recent count/host target/tags
  - per-candidate selected/hostMatch/cwdMatch/submit-state

### 10) Verification script
- Added helper script:
  - `prompt-probe-verify.ps1`
- Provides repeatable local verification for probe behavior and output format.

### 11) Build/deploy reliability updates
- `build.cmd` hot-swap watchdog strengthened:
  - wait for `.new.exe` promotion (up to 3s)
  - if stalled: kill running bot processes and retry promote
  - race-tolerant handling when `.new.exe` disappears during retry
- Added stale artifact cleanup for `.new.exe` / `.old.exe`.
- Parser-safe batch fixes for robust execution.

## Known Caveats
- `publish/wkappbot-core.exe` may intermittently fail with `GenerateBundle` access denied when a process lock remains.
- In those cases, direct testing with fresh build outputs (non-locked path) was used to validate logic.

## Files Touched (main)
- `csharp/src/WKAppBot.CLI/Program.cs`
- `csharp/src/WKAppBot.CLI/Commands/UsageCommand.cs`
- `csharp/src/WKAppBot.CLI/Commands/PromptProbeCommand.cs`
- `csharp/src/WKAppBot.CLI/Commands/AppBotEyeSlackHandlers.cs`
- `csharp/src/WKAppBot.Win32/Window/ClaudePromptHelper.cs`
- `build.cmd`
- `prompt-probe-verify.ps1`

