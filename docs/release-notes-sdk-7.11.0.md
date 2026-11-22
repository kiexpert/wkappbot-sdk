# WKAppBot SDK v7.11.0-sdk — Aligned with core v7.11

Released 2026-09-30. Aligned with WKAppBot core v7.11 — macOS Portable Phase 1 + CLI Improvements.

## Highlights

### macOS Portable Phase 1
- `WKAppBot.Abstractions`, `WKAppBot.Shared`, `WKAppBot.PluginContract`, `WKAppBot.Android` now multi-target `net8.0` alongside `net8.0-windows*`.
- `Portable.slnf` solution filter lets non-Windows hosts build the portable surface.
- `EnableWindowsTargeting=true` in `Directory.Build.props` allows macOS/Linux project evaluation without failing at load time.
- `global.json` rollForward set to `latestFeature` for macOS CI.

### Global options from any position
- `--keep`, `--budget`, `--sudo`, `--timeout` peel before the subcommand via `PeelPreCommandGlobalOpts`.
- Compliant with `wkappbot-cli-argument-order-spec` (owner spec 2026-09-30).

### file-edit diff output
- ANSI background coloring on diff hunk lines.
- Byte-change counts alongside line-change counts.
- Explicit success indicators on successful edits.

### a11y-find output cleanup
- Removed spurious JSON and ANSI escape sequences from `System` and `Mouse` sections.

### Launcher safety
- PID verification before `TerminateProcess` prevents wrong-process termination after PID reuse.

## Fixes
- `skill-search`: `--budget` flag properly consumes its argument value.
- `help`: Missing `ime-relay-daemon` entry added to `CommandHelpMap`.
- Portable CI: fixed `Portable.slnf` JSON format; removed non-portable `Shared` reference; relaxed `global.json` rollForward.

## Full details
See [WKAppBot core v7.11.0 release notes](https://github.com/kiexpert/WKAppBot/releases/tag/v7.11.0) for the complete commit-level changelog.
