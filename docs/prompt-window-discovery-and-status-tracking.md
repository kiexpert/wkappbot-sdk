# Prompt Window Discovery & Status Tracking

How WKAppBot identifies *which* Claude/Codex prompt window issued a command,
and how it tracks per-window Slack status state across Eye restarts.

---

## 1. Prompt Window Discovery

### Goal
Every `slack send/reply` command needs a **Slack username** of the form
`[claude][project]` or `[codex][project]`.
The username is derived from the prompt window that spawned the command —
its **host type** (Claude Code vs. Codex) and **CWD (project path)**.

### Input sources (resolution order)

```
CallerHwnd  ← AsyncLocal set by Eye pipe server   (most reliable: exact hwnd)
CallerCwd   ← AsyncLocal set by Eye pipe server   (reliable:   matches CWD)
Process tree walk                                  (fallback:   no Eye pipe)
```

#### CallerHwnd — pipe delivery path

```
wkappbot.exe (Launcher)
  └─ GetForegroundWindow()  → hwnd of active window at call time
  └─ prepends "__hwnd:0x{hwnd:X8}" to arg list
  └─ sends JSON args via named pipe "WKAppBotCmdPipe"

Eye (EyeCmdPipeServer.HandleClientAsync)
  └─ parses "__hwnd:" prefix → IntPtr
  └─ stores in AsyncLocal<IntPtr?> CallerHwnd

Command handler (e.g. GetSendReplyUsername)
  └─ reads EyeCmdPipeServer.CallerHwnd.Value
```

When running Core directly (no Eye), `CallerHwnd` is null →
`FindCallerParentPromptHwnd()` walks the process tree to find a known
Claude/Codex ancestor window as a fallback.

---

### Resolution chain — `GetBotUsernameFromCachedCards`

```csharp
static string? GetBotUsernameFromCachedCards(string? callerCwd, IntPtr? callerHwnd)
```

| Priority | Condition | How username is built |
|---|---|---|
| **0. Hwnd direct** | `callerHwnd` matches a cached prompt window handle | `HostType` → prefix; title or `callerCwd` → `[tag]` |
| **1a. Card CWD** | Card with matching `.Cwd` exists | Card `HostType` → prefix + `[AbbreviateCwd(cwd)]` |
| **1b. VSCode title CWD** | No card, but a VSCode window title contains `callerCwd` | `SlackClaudePrefix[AbbreviateCwd(callerCwd)]` |
| **1c. CWD direct** | `callerCwd` is known, non-system, but nothing matched above | `SlackClaudePrefix[AbbreviateCwd(callerCwd)]` |
| **2. Most recent card** | `callerCwd` is unknown | Most recently updated card's host type + CWD |
| **null** | No cards at all | Caller falls back to process ancestry |

After `GetBotUsernameFromCachedCards`, `GetSendReplyUsername` also checks
**process ancestry** (Codex or Claude parent process) as a final safety net.

---

### Cached prompt data — `ClaudePromptHelper`

`ClaudePromptHelper.GetAllCachedPrompts()` returns the live list of known
prompt windows with fields:

| Field | Description |
|---|---|
| `WindowHandle` | HWND of the prompt window |
| `HostType` | `"vscode-claudecode"` / `"claude-desktop"` / `"codex-desktop"` |
| `WindowTitle` | Raw window title (VS Code embeds CWD here) |
| `Cwd` | Resolved CWD (if available from card) |

**CWD extraction for VS Code**: `ExtractCwdFromVsCodeTitle(title)` parses the
VS Code window title format `"... — {cwd} — Visual Studio Code"`.

**CWD abbreviation**: `AbbreviateCwd(path)` converts full paths to short tags,
e.g. `D:\GitHub\lucy_securepad` → `lucy-securepad`.

---

### Username prefixes

```csharp
SlackClaudePrefix  = "[claude]"   // Claude Code (wkappbot eye or standalone)
SlackCodexPrefix   = "[codex]"    // Codex desktop
```

Final format: `[claude][lucy-securepad]` or `[codex][wkappbot]`

---

## 2. Per-Window Status Tracking

### Goal
Each Claude/Codex window has its own Slack status message (thread starter).
Status must survive Eye restarts without losing the message timestamp or
misidentifying the current status type.

---

### In-memory state — `ClaudeInstanceState`

```csharp
string? SlackStatusTs         // Slack message timestamp of current status post
string? LastSlackStatusText   // Full text of last posted status (dedup check)
string? LastStatusType        // Category: "executing" / "idle" / etc.
```

---

### Persistence across Eye restarts — Window Properties (GlobalAtom)

State is serialized as strings on the **HWND** using Win32 `SetProp`/`GetProp`
with GlobalAtom names. These survive Eye process restarts because they live
on the window, not in Eye's heap.

| Prop name | Constant | Content | Max |
|---|---|---|---|
| `WKAppBot.AiOut` | `PropAiOut` | Last Slack status text | 240 chars |
| `WKAppBot.SlTs` | `PropSlackTs` | Slack message TS (`"1234567890.123456"`) | 240 chars |
| `WKAppBot.StType` | `PropStatusType` | Status type string | 240 chars |

**On first access** (`GetOrCreateInstanceState`): all three props are read from
the HWND and restored into the `ClaudeInstanceState` object.

**On every status post** (`PostOrUpdateAiStatusAsync`): all three props are
written back to the HWND.

---

### Status post/update decision logic — `PostOrUpdateAiStatusAsync`

```
sameType  = (LastStatusType == newStatusType) AND (SlackStatusTs != null)
hasReplies = SlackMessageHasRepliesAsync(channel, SlackStatusTs)   // skip if no existing TS

canEdit = sameType AND NOT hasReplies
```

| Case | Action |
|---|---|
| `canEdit` AND text unchanged | **Skip** (no-op — same type, same text, no replies) |
| `canEdit` AND text changed | **Edit** existing message in-place |
| NOT `canEdit` AND has existing TS AND no replies | **Delete** old message, then **Post** new |
| NOT `canEdit` AND has existing TS AND has replies | **Post** new message (leave threaded one alone) |
| No existing TS | **Post** new message |

**Reply detection**: `SlackMessageHasRepliesAsync` calls
`conversations.replies?channel=&ts=&limit=2` and returns `true` when
`messages.Count > 1` (parent + at least one reply).
On API or network error it returns `true` (conservative: never delete a
message we can't verify).

---

## 3. Data Flow Summary

```
Launcher (wkappbot.exe)
  GetForegroundWindow() → __hwnd:0x...
  CurrentDirectory     → __cwd:...
        │
        ▼  named pipe "WKAppBotCmdPipe"
Eye (EyeCmdPipeServer)
  HandleClientAsync: parse prefixes → CallerHwnd, CallerCwd (AsyncLocal)
  RunInEye: set AsyncLocal values, route output via TeeTextWriter
        │
        ▼  async continuations inherit AsyncLocal values
Command handler (e.g. slack send/reply)
  GetSendReplyUsername()
    CallerHwnd → GetBotUsernameFromCachedCards (Priority 0 direct hwnd)
    CallerCwd  → GetBotUsernameFromCachedCards (Priority 1 CWD match)
    process ancestry → fallback username
        │
        ▼
  PostOrUpdateAiStatusAsync(hwnd, username, text, statusType)
    GetOrCreateInstanceState(hwnd) — restores TS/text/type from HWND props
    sameType + hasReplies → decide edit / delete+post / post
    SetWindowStringProp(hwnd, ...) — persist new TS/text/type
```

---

## 4. Key Files

| File | Role |
|---|---|
| `WKAppBot.Launcher/EyeCmdPipeClient.cs` | Prepends `__cwd` + `__hwnd` to pipe payload; `GetForegroundWindow()` P/Invoke |
| `WKAppBot.CLI/EyeCmdPipeServer.cs` | Parses prefixes; `CallerCwd` + `CallerHwnd` AsyncLocals |
| `WKAppBot.CLI/Commands/SlackMonitorCommands.cs` | `GetBotUsernameFromCachedCards`, `GetSendReplyUsername`, `FindCallerParentPromptHwnd`, `SlackMessageHasRepliesAsync` |
| `WKAppBot.CLI/Commands/AppBotEyeClaudeStatusStreamer.cs` | `ClaudeInstanceState`, `GetOrCreateInstanceState`, `PostOrUpdateAiStatusAsync`, window prop helpers |
