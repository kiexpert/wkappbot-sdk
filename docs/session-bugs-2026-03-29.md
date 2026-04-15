# Session Bugs & Issues — 2026-03-29

Bugs and friction points discovered during Eye Card System implementation
and Slack workspace migration session.

## CRITICAL: ScreenSaver Blocks Screen During Automation

**Symptom**: Screen goes completely black, topmost refreshed every ~1s, user cannot interact with anything. Forced reboot required.

**Root Cause**: CDP automation (ask triad, web read, etc.) generates no keyboard/mouse input → `GetUserIdleMs()` returns high values → screensaver activates → WPF window covers all monitors with HWND_TOPMOST.

**Fix Applied** (commits 09c7c4c, fb36421, 3e81105):
- Per-window guard threads (SS-Guard-N): independent Win32 hide per monitor
- Master watchdog (SS-Master): Environment.Exit + Process.Kill
- Dispatcher liveness ping: WM_NULL with 100ms SMTO_ABORTIFHUNG timeout
- Topmost refresh throttled to 5-minute intervals
- Initial opacity starts at 0 (no black flash)

**Remaining Risk**: Eye's forced 5s kill cycle is a band-aid. Proper fix: suppress screensaver while Eye has active CDP/MCP commands.

**Suggest**: Add `EyeActiveCommandCount` check in screensaver — if Eye is executing commands, skip screensaver entirely.

---

## CRITICAL: Garbled BeginInvoke Block in ScreenSaverOverlay.cs

**Symptom**: ScreenSaver code was syntactically valid but logically broken — `BeginInvoke` block was empty, UI operations ran outside WPF thread.

**Root Cause**: Previous edit session corrupted the code structure. Lines 297-308 had:
```csharp
_dispatcher.BeginInvoke(() =>
{
    foreach (var mwin in _monitors)
    {
});  // ← BeginInvoke closes here (empty!)
Console.WriteLine("[EYE] ScreenSaver fade start (user
        mwin.Window.Opacity = 0;  // ← runs OUTSIDE BeginInvoke
```

**Fix Applied**: Reconstructed the entire `Tick()` method with correct block structure.

**Suggest**: Add a test that verifies ScreenSaverOverlay.cs compiles AND the BeginInvoke block contains UI operations. Consider `[MethodImpl(NoInlining)]` markers for critical WPF sections.

---

## HIGH: .wkappbot Marker File Blocks Directory Creation

**Symptom**: `.wkappbot/ask/` folder creation fails silently — `Directory.CreateDirectory` can't create a subdirectory when a FILE with the same name exists at the parent path.

**Root Cause**: Commit 5a3be25 added `.wkappbot` as a marker FILE for workspace root detection. Later code (AskCommands.MdWriter, AskCommands.Triad) tried to create `.wkappbot/ask/` DIRECTORY.

**Fix Applied** (commit 41cbd0f): Deleted `.wkappbot` file from git, replaced with `.wkappbot/` directory containing `ask/` subfolder. `AbbreviateCwd` already checks both `Directory.Exists` and `File.Exists`.

**Suggest**: Never create marker files that share names with potential directories. Use `.wkappbot/` directory itself as the marker.

---

## HIGH: CDP Zoom JS Eval Fails on Fresh Tabs

**Symptom**: `[CDP:JS-ERR] SyntaxError: Unexpected token '}'` on all three AI tabs (GPT, Claude, Gemini) during triad ask.

**Root Cause**: `GetElementRectAsync` called immediately after tab creation — execution context not yet ready. The IIFE `(()=>{...})()` is valid JS but fails when context is in transitional state.

**Fix Applied** (commit c6c033d): `BeginCdpZoom` catches eval failure and falls back to Chrome window client area rect (`GetClientRect`).

**Suggest**: Add 500ms delay or context-ready check before first CDP eval on newly created tabs.

---

## MEDIUM: MCP CWD Detection Fails for Non-VS-Code Hosts

**Symptom**: Card shows `[WS-bin]` (SDK/bin directory) instead of project CWD. Bot name shows "system32".

**Root Cause**: MCP server's `Environment.CurrentDirectory` inherits from launcher (D:\SDK\bin) or defaults to system32. Claude Desktop and Codex don't set child process CWD to project root.

**Fix Applied** (commit 41cbd0f):
- `DetectMcpParentCwd()`: walks parent process chain for VS Code title parse + CWD heuristic (.mcp.json/.git/CLAUDE.md)
- Session registry (`runtime/sessions/{pid}.json`) stores detected CWD

**Suggest**: For Claude Desktop, try reading `~/.claude/projects/` directory names to reverse-map CWD. For Codex, check parent's command line arguments for project path.

---

## MEDIUM: Worker Processes Create Noise Cards

**Symptom**: find-prompts, screensaver, whisper-ring, analyze-hack, slack-route appear as separate cards in Eye tick display.

**Root Cause**: Eye spawns child processes that have `RunningInEye=false` → they emit ticks → Eye creates cards for them.

**Fix Applied** (commit 41cbd0f): `WKAPPBOT_WORKER=1` environment variable suppresses ticks for all Eye child spawns (6 call sites).

---

## MEDIUM: Eye Pipe "Access to the path is denied" Loop

**Symptom**: After hot-swap, Eye pipe floods log with `[EYE:PIPE] Pipe error: Access to the path is denied.` every ~1s. Commands dispatched to Eye (ask triad, etc.) silently fail.

**Root Cause**: Hot-swap replaces wkappbot-core.exe but Eye's MCP worker holds old binary. Named pipe becomes inaccessible after binary replacement.

**Workaround**: Kill Eye (`a11y kill wkappbot`) and let it respawn with new binary.

**Suggest**: Eye should detect pipe errors and auto-restart MCP worker with new binary. Add exponential backoff on pipe errors instead of retry-every-tick.

---

## LOW: Chrome CDP Port Rejected with Default User-Data-Dir

**Symptom**: `--remote-debugging-port=9222` silently ignored when Chrome launched with default user-data-dir. `DevTools remote debugging requires a non-default data directory.`

**Root Cause**: Chrome security policy — default profile rejects remote debugging. WKAppBot's `ChromeLauncher` uses a separate user-data-dir, but manual `start chrome --remote-debugging-port=9222` doesn't.

**Suggest**: Document this in CLAUDE.md or ChromeLauncher comments. Always use `wkappbot web open` to launch CDP-capable Chrome.

---

## LOW: Slack React UI Rejects JS Click Events

**Symptom**: `document.querySelector('button').click()` and `MouseEvent` dispatch are silently ignored by Slack's React app. Only `PointerEvent` dispatch works for some buttons.

**Root Cause**: React's synthetic event system requires trusted events or specific event types (PointerEvent > MouseEvent > click). Some Slack buttons (Generate Token) only respond to PointerEvent with `pointerId` and `pointerType`.

**Workaround**: Use full PointerEvent sequence: `pointerdown → mousedown → pointerup → mouseup → click`. For form submission, `form.submit()` bypasses React entirely.

---

## SUGGEST: Print Actionable Access Info for Open Pages

**Problem**: When Chrome/webbot has a page open, the agent doesn't know how to target it efficiently. Wasted 30+ minutes trying different grap patterns (`*WKWebBot*`, `*Chrome*#api.slack.com`, `*hwnd=00140922*#api.slack.com`) before finding the right one.

**Ideal Behavior**: When `web open` or `web read` loads a page, print a ready-to-copy command:
```
[WEB] Page loaded: Slack API: Applications | 앱봇알바생 Slack
[WEB] URL: https://api.slack.com/apps/A0APH6RJ1AA
[WEB] CDP Target: a11y read "*Slack API*chrome*#api.slack.com/apps/A0APH6RJ1AA" --eval-js "..."
[WEB] HWND: 0x00140922
[WEB] Tab ID: 01252F61E773
```

This saves agents from trial-and-error grap pattern discovery. The `CDP Target` line should be a working command that the agent can copy-paste.

**Additional Suggestions**:
- `eye tick` should show which Chrome tabs are CDP-accessible (port + tab ID)
- `a11y windows` output should include CDP port info for Chrome windows
- When `--eval-js` silently fails (UIA path taken instead of CDP), warn explicitly: `[WARN] --eval-js ignored: UIA path used. Use web read or #tab-hint for CDP.`

---

## LOW: Multiple Duplicate Chrome Tabs

**Symptom**: 6+ identical tabs (`api.slack.com/apps/A0APH6RJ1AA`) opened during Slack setup automation.

**Root Cause**: Each `web read` / `web open` call creates a fresh tab via sandbox policy. Tab sandbox key doesn't account for same-URL reuse.

**Suggest**: Before creating fresh tab, check if any existing tab has matching URL and reuse it.
