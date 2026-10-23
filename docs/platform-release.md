# Platform Release pipeline (run book)

`.github/workflows/platform-release.yml` builds, verifies and releases wkappbot for every
platform from this public repo, on the free GitHub-hosted runners. The private core is cloned
with the `WKAPPBOT_CORE_PAT` secret by `.github/scripts/clone-core.sh` (token never printed).

## Jobs

| Job | Runner | What |
|---|---|---|
| build linux-x64 | ubuntu-latest | `dotnet publish core/csharp/src/WKAppBot.CLI.Linux -r linux-x64 --self-contained -p:PublishSingleFile=true` |
| build osx-arm64, osx-x64 | macos-latest | same project, `-r osx-arm64` / `-r osx-x64` |
| build win-x64 | windows-latest | `WKAppBot.CLI` -> `wkappbot-core.exe`; this repo's `WKAppBot.Launcher` -> `wkappbot.exe` |
| verify (x4) | native runner | Linux / macOS: `install.sh` from the artifact (checksum-verified), then `.github/scripts/portable-smoke.sh` (`--version`, `help`, `skill read on-load` against a seeded skills dir, `skill list`, Hangul file round-trip). osx-x64 runs under Rosetta 2. Windows: core `--version` and `skill read on-load` (120 s budget each), launcher file version. |
| SHA256SUMS | ubuntu-latest | `wkappbot-core.exe` over 100 MB is shipped as `wkappbot-core.exe.xz`; sums for all assets; artifact `release-assets` |
| release | ubuntu-latest | only on a `v*-sdk` tag: creates the release with the CHANGELOG section as notes and uploads the assets |

Any red job stops the release.

## Asset names are a contract

`wkappbot-linux-x64`, `wkappbot-osx-arm64`, `wkappbot-osx-x64`, `wkappbot.exe`,
`wkappbot-core.exe` (or `.xz`), `SHA256SUMS`. `install.sh` and the kih acquirer download
`releases/latest/download/<name>`; renaming one breaks every installer.

## Triggers

- `workflow_dispatch` with input `core_ref` (default `main`). GitHub only offers this once the
  workflow file is on the default branch.
- `repository_dispatch` type `core-updated`, `client_payload.ref` = core ref.
- push of a `v*-sdk` tag: full run plus release.
- push to `claude/release-*` touching the pipeline files: build + verify only (no release).

## Run it

```bash
gh workflow run platform-release.yml --repo kiexpert/wkappbot-sdk -f core_ref=main
gh api repos/kiexpert/wkappbot-sdk/dispatches -f event_type=core-updated -F 'client_payload[ref]=main'
git tag -a vX.Y.Z-sdk -m "WKAppBot vX.Y.Z-sdk" && git push origin vX.Y.Z-sdk
```

Check a run: `gh api repos/kiexpert/wkappbot-sdk/actions/runs/<id>/jobs -q '.jobs[]|.name+" "+.conclusion'`.
The SHA256SUMS lines are printed in the `SHA256SUMS` job log and its step summary.

## Known

- The Linux / macOS build is the minimal CLI (`skill`, `file`, `suggest`, `version`), and its
  `--version` says `linux-minimal` on macOS too (core-side label).
- `release.yml` is the old Windows-only release, kept for manual dispatch only.
