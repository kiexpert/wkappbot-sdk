# Contributing to WKAppBot SDK

Thank you for your interest in contributing!

## What you can contribute to

The **launcher** (`csharp/src/WKAppBot.Launcher/`) is fully open source (MIT).
The **core binary** (`wkappbot-core.exe`) is closed source — it ships as a pre-built binary.

Contributions to the launcher, documentation, examples, handlers, and skills are welcome.

## Prerequisites

- Windows 10 22H2+ or Windows 11
- .NET SDK 8.0+
- Visual Studio 2022 or VS Build Tools (for AOT launcher build)
- PowerShell 7+ (`winget install Microsoft.PowerShell`)

## Build

```bat
git clone https://github.com/kiexpert/wkappbot-sdk %USERPROFILE%\Documents\wkappbot
cd %USERPROFILE%\Documents\wkappbot
build.cmd
```

## Before submitting a PR

1. Run `dotnet format --verify-no-changes` — fix any formatting issues
2. Run smoke tests: `pwsh -File scripts/smoke-help.ps1`
3. Check the [PR template](.github/pull_request_template.md)

## Code conventions

- English only in source code and comments
- ~400 lines per source file (split larger files)
- No hardcoded paths — use environment variables or `ProjectRoot`
- No bandaid code — fix the shared function, not the call site

## Filing issues

Use the issue templates:
- **Bug**: something broken
- **Feature**: new capability
- **License question**: activation or tier questions
