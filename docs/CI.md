# CI / CD

## Workflows

| Workflow | Trigger | Purpose |
|----------|---------|---------|
| `build-launcher` | Push to `csharp/**`, `skills/**`, `handlers/**` | Build launcher + fetch core + smoke test |
| `release` | Tag `v*` or `workflow_dispatch` | Bundle zip + publish GitHub Release |
| `extended-smoke` | Push to `csharp/**` | Full 188-test smoke suite |
| `script-selftest` | Push to `**/*.ps1` | Validate changed scripts |
| `codeql` | Weekly + PR to `csharp/**` | Static security analysis |
| `lint` | PR to `csharp/**` | `dotnet format --verify-no-changes` |

## Required secrets

| Secret | Purpose |
|--------|---------|
| `WKAPPBOT_CORE_PAT` | PAT with `read` access to private dev repo — downloads `wkappbot-core.exe` |
| `WKAPPBOT_CORE_REPO` | Private dev repo slug (e.g. `kiexpert/WKAppBot`) |
| `GITHUB_TOKEN` | Auto-provided by GitHub Actions — used for release creation and smoke license auth |

## Artifacts

Every `build-launcher` run uploads `wkappbot-bin-{run_id}` containing `bin/**` (90-day retention).
`find-stable-release.ps1` downloads these artifacts, runs smoke tests locally, and tags a release.

## Version scheme

`{WKAppBotBaseVersion}.{patch}` where patch = commits since the version bump commit.
Patch is auto-counted via git pickaxe in `Directory.Build.props`.
Bump minor version by editing `<WKAppBotBaseVersion>` — no tags needed.

## Running CI locally

```powershell
# Smoke test
pwsh -File scripts/smoke-help.ps1

# Extended smoke (full 188 tests)
pwsh -File scripts/smoke-help.ps1 -Extended

# Format check
dotnet format csharp/src/WKAppBot.Launcher/WKAppBot.Launcher.csproj --verify-no-changes
```
