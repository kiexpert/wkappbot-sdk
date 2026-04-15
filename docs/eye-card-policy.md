# Eye Card System — Architecture & Policy

> How WKAppBot tracks active AI sessions and displays status cards.
> Written for fellow Clots (Claude agents) and future contributors.

## Overview

A **card** represents one active AI coding session. Eye (the monitoring daemon)
displays cards on Slack showing what each AI agent is working on.

Each card is the synthesis of three signals:

| Signal | Source | Reliability |
|--------|--------|-------------|
| **Prompt HWND** | Window handle of the IDE/AI prompt | High (unique per window) |
| **Session JSONL** | `~/.claude/projects/.../session.jsonl` | High (file mtime = liveness) |
| **Working Directory** | Project root (e.g. `D:\GitHub\WKAppBot`) | Medium (detection varies) |

Card key priority: `PromptHwnd > SessionJsonl > CWD`

---

## Three-Layer Detection Model

```
┌─────────────────────────────────────────────┐
│  Layer 1: Session Registry  (authoritative) │
│  runtime/sessions/{pid}.json                │
│  MCP server registers on start              │
├─────────────────────────────────────────────┤
│  Layer 2: AI Session FSW    (discovery)     │
│  ~/.claude/, ~/.cursor/, ~/.codex/ etc.     │
│  JSONL mtime → reverse-map to CWD          │
│  (planned — not yet implemented)            │
├─────────────────────────────────────────────┤
│  Layer 3: Eye Ticks         (legacy)        │
│  eye_ticks.jsonl                            │
│  Direct CLI commands append ticks           │
└─────────────────────────────────────────────┘
```

Eye reads Layer 1 first; Layer 3 fills gaps for CWDs not covered by sessions.

---

## Layer 1: Session Registry

### File: `wkappbot.hq/runtime/sessions/{pid}.json`

```json
{
  "Id": "session-7800",
  "Pid": 7800,
  "ParentPid": 12345,
  "HostType": "vscode",
  "HostTitle": "Program.cs - WKAppBot - Visual Studio Code",
  "Cwd": "D:\\GitHub\\WKAppBot",
  "PromptHwnd": "0x403EE",
  "SessionJsonl": "~/.claude/projects/w--GitHub-WKAppBot/.../session.jsonl",
  "StartedAt": "2026-03-29T21:42:00Z",
  "Heartbeat": "2026-03-29T21:48:00Z",
  "LastCommand": "a11y inspect *Button*",
  "LastStatus": "result=ok",
  "LastTag": "inspect",
  "IsWorker": false
}
```

### Registration Flow

1. `wkappbot mcp` starts → `SessionRegister()` creates the file
2. Each MCP tool call → `SessionUpdate()` refreshes heartbeat + command
3. Process exit → `SessionUnregister()` deletes the file
4. Eye reads all `*.json` in sessions dir → builds cards

### Lifecycle

- **Heartbeat**: updated on every MCP tool call (JSON rewrite)
- **Stale**: heartbeat > 5 min old → deleted by Eye
- **Dead PID**: `Process.GetProcessById()` throws → deleted by Eye
- **Hot-swap**: MCP server survives binary replacement; session file persists

---

## Layer 2: AI Session FSW (Future)

Watch session directories of all AI tools to auto-detect active sessions:

| AI Tool | Session Directory | File Pattern |
|---------|-------------------|--------------|
| Claude Code | `~/.claude/projects/` | `*.jsonl` |
| Cursor | `~/.cursor/` | TBD |
| Codex CLI | `~/.codex/` | TBD |
| GitHub Copilot | `~/.copilot/` | TBD |
| Continue.dev | `~/.continue/` | TBD |
| Codeium | `~/.codeium/` | TBD |

### Reverse-Map: Directory → CWD

Claude example: `~/.claude/projects/w--GitHub-WKAppBot/` → `D:\GitHub\WKAppBot`

```
dir name: w--GitHub-WKAppBot
decode:   w-- → D:\   (double-dash = drive separator)
          GitHub-WKAppBot → GitHub\WKAppBot  (dash = path separator)
result:   D:\GitHub\WKAppBot
```

### Design Principles (from Triad Debate, 2026-03-29)

- **FSW supplements MCP, never replaces it** — MCP hit → skip FSW
- **mtime only, never read content** — session files contain full conversations
- **Polling fallback mandatory** — FSW unreliable on SMB/NFS/OneDrive
- **Lazy activation** — watch top-level dir creation events, then drill down
- **`FileShare.ReadWrite`** — other processes may hold locks on JSONL

---

## Layer 3: Eye Ticks (Legacy)

### File: `wkappbot.hq/runtime/eye_ticks.jsonl`

Each direct CLI command (`wkappbot a11y ...`) appends a tick:

```json
{
  "Ts": "2026-03-29T21:42:00Z",
  "Pid": 14488,
  "ParentPid": 5288,
  "HostPid": 12345,
  "HostName": "Code",
  "HostTitle": "Program.cs - WKAppBot - Visual Studio Code",
  "Command": "a11y",
  "Tag": "inspect",
  "Status": "step:2/3:executing",
  "Cwd": "D:\\GitHub\\WKAppBot",
  "PromptHwnd": "0x403EE"
}
```

Ticks are **not emitted** when:
- `RunningInEye = true` (Eye pipe commands — Eye owns the card)
- `WKAPPBOT_WORKER = 1` (worker processes — no card)
- `_fastExitAfterCommand = true` (grap/grep aliases)

---

## CWD Detection

MCP servers need to know which project they belong to. Detection strategies:

### Strategy 1: Parent Process Chain (VS Code specific)

Walk parent PIDs → find VS Code → parse window title:
```
"Program.cs - WKAppBot - Visual Studio Code"
                ↓
         ExtractCwdFromVsCodeTitle()
                ↓
         "D:\GitHub\WKAppBot"
```

### Strategy 2: CWD Heuristic (Universal)

Check `Environment.CurrentDirectory` for project markers:
- `.mcp.json` → MCP config (most reliable)
- `.git/` → git repository root
- `CLAUDE.md` → Claude project instructions

Works for **any** IDE (VS Code, Claude Desktop, Codex, Copilot, Cursor).

### Strategy 3: Host Type Detection

| Parent Process | Host Type |
|---------------|-----------|
| `code` or title contains "Visual Studio Code" | `vscode` |
| `claude` | `claude-desktop` |
| `codex` | `codex` |
| `cursor` or title contains "Cursor" | `cursor` |
| `copilot` | `copilot` |
| None of the above | `terminal` |

---

## Worker Noise Suppression

Eye spawns child processes that must **not** create cards:

| Worker | Purpose | Suppression |
|--------|---------|-------------|
| `screensaver` | WPF sunset animation | `WKAPPBOT_WORKER=1` |
| `whisper-ring` | Audio ring overlay | `WKAPPBOT_WORKER=1` |
| `analyze-hack` | CCA analysis server | `WKAPPBOT_WORKER=1` |
| `find-prompts` | Claude window discovery | `WKAPPBOT_WORKER=1` |
| `slack route` | Message delivery | `WKAPPBOT_WORKER=1` |

When `WKAPPBOT_WORKER=1` is set:
- `RunningInEye = true` → ticks suppressed
- TeeWriter skipped → no log duplication
- LaunchEye skipped → no cascade spawn

---

## Card Display

Eye renders cards in Slack. Each card shows:

```
[WG-WKAppBot]
클롣 작업: a11y inspect
클롣 상태: result=ok (3초 전) ctx=4% (2MB)
클롣 생각: 🔧mcp__wkappbot__wkappbot_cli
```

- `[WG-WKAppBot]` — abbreviated CWD (`D:\GitHub\WKAppBot`)
- `클롣 작업` — last command
- `클롣 상태` — last status + context usage %
- `클롣 생각` — latest thought from AI session JSONL

### Card Key Rules

1. **PromptHwnd wins** — each IDE window = unique card
2. **CWD fallback** — same project folder = one card (survives PID restart)
3. **Never create cards for**:
   - Empty CWD
   - `C:\Windows\system32` (unresolved CWD)
   - Worker processes (`WKAPPBOT_WORKER=1`)

---

## Key Files

| File | Purpose |
|------|---------|
| `SessionRegistry.cs` | Session register/update/heartbeat/cleanup |
| `McpCommand.cs` | `DetectMcpParentCwd()`, `DetectHostType()` |
| `McpCommand.Helpers.cs` | `RunToolCore()` — session update per tool call |
| `AppBotEyeHealthCheck.cs` | `ReadEyeCards()` — session + tick merger |
| `AppBotEyeCardBuilder.cs` | `BuildEyeSummary()`, `AbbreviateCwd()` |
| `AppBotEyePromptInfo.cs` | `ExtractCwdFromVsCodeTitle()`, `FindSessionJsonl()` |
| `Program.cs` | `EmitEyeTick()`, `WKAPPBOT_WORKER` check |

---

## Triad Consensus (2026-03-29)

Three AI architects (GPT, Gemini, Claude) agreed on:

1. **MCP Registry = Truth** — registered sessions are authoritative
2. **FSW = Discovery** — detects "shadow AI" not using MCP
3. **Eye Ticks = Fallback** — legacy compatibility for direct CLI
4. **Generic FSW + thin adapters** — `ISessionAdapter { ParseSessionId, IsActive }`
5. **Security: mtime only** — never read session content
6. **Polling fallback** — FSW unreliable on network drives
7. **mtime liveness score** — `1.0 / (1 + seconds / 30)` for continuous ranking

Recommended implementation order:
1. ✅ MCP Registry (done)
2. ⬜ Generic FSW + Claude/Cursor adapters
3. ⬜ Dual-track polling fallback
4. ⬜ Heartbeat event bus refactor
