# Architecture

## Two-binary design

```
wkappbot.exe          (MIT, open source — this repo)
  └─ launches ──►  wkappbot-core.exe   (closed source, downloaded)
```

**Launcher** (`wkappbot.exe`, ~1 MB, AOT-compiled):
- Parses CLI args and routes to core via named pipe
- Handles hot-swap: detects `wkappbot-core.new.exe`, drains requests, renames atomically
- Checks license tier via GitHub collaborator API
- No automation logic — thin relay only

**Core** (`wkappbot-core.exe`, ~25 MB, single-file):
- All CLI commands: `a11y`, `eye`, `ask`, `skill`, `file`, `schedule`, …
- AppBot Eye daemon (Slack + MCP broker)
- UIA / Win32 / CDP / ADB automation engines
- Vision/OCR pipeline

## Data directory

```
%USERPROFILE%\Documents\wkappbot\
  bin\
    wkappbot.exe
    wkappbot-core.exe
    wkappbot.hq\           ← per-install data (auto-created)
      skills\              ← versioned operator knowledge
      handlers\            ← YAML automation handlers
      experience\          ← UI element cache (self-healing DB)
      logs\
      runtime\
```

Each git clone gets its own `wkappbot.hq\` — multiple clones don't interfere.

## Per-repo isolation

Every repo root gets a unique 8-character hash (`ProjectRoot.Hash8()`).
Pipe names: `wkappbot_eye_ipc_{hash}` and `wkappbot_elevated_{hash}`.
Two repos → two independent Eye instances, separate Slack channels, separate DataDirs.

## 5-tier element search

```
UIA (focusless)
  └── Vision Cache (fast repeated automation)
       └── Simple OCR
            └── Vision API (Claude/Gemini)
                 └── Coordinate fallback
```

Results are cached in Experience DB; repeat runs skip expensive tiers.

## Hot-swap (zero-downtime update)

```
dotnet publish  →  wkappbot-core.new.exe staged
Eye detects .new.exe  →  drains in-flight pipe requests
atomic rename: .new.exe  →  wkappbot-core.exe
Eye restarts core worker
```
