# User-Perspective Tests

Validates WKAppBot from a **first-time user's point of view** — no prior knowledge assumed.

Each script exits 0 on pass, 1 on failure. Run in order on a clean Windows machine.

## How to run

```powershell
# Full suite
pwsh -File tests/user-perspective/run-all.ps1

# Individual stages
pwsh -File tests/user-perspective/01-install.ps1
pwsh -File tests/user-perspective/02-basics.ps1
pwsh -File tests/user-perspective/03-automation.ps1
pwsh -File tests/user-perspective/04-license.ps1
```

## What's tested

| Script | Stage | Tests |
|--------|-------|-------|
| `01-install.ps1` | Install | PATH, binaries present, version string |
| `02-basics.ps1` | Basic CLI | help, version, skill list, file tools, schedule |
| `03-automation.ps1` | Automation | Notepad find/type/read/close (requires Desktop) |
| `04-license.ps1` | License | Free tier status, upgrade URL reachable |

## Requirements

- `wkappbot.exe` and `wkappbot-core.exe` present in `bin\`
- `bin\` added to PATH
- Windows 10 22H2+ or Windows 11
- Internet access (for license check + skill list)

## On CI

`01-install.ps1` and `02-basics.ps1` run headless.  
`03-automation.ps1` requires a desktop session — skip with `-SkipDesktop`.
