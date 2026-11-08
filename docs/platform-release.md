# Platform Release pipeline (run book)

`.github/workflows/platform-release.yml` builds, verifies and releases wkappbot for every
platform from this public repo, on the free GitHub-hosted runners. The private core is cloned
with the `WKAPPBOT_CORE_PAT` secret by `.github/scripts/clone-core.sh` (token never printed).

## Build farm

This repo is the build farm for the AppBot core and launcher on every platform. Nothing deploys
unless every platform's build and verify jobs are green.

1. **Trigger.** The core repo's `.github/workflows/sdk-build-dispatch.yml` sends
   `repository_dispatch` `core-updated` with `client_payload.ref` = the pushed core sha whenever
   `csharp/` changes on main (secret `SDK_DISPATCH_PAT` there). The hourly cron here is the fallback.
2. **Resolve** (`.github/scripts/resolve-core.sh`): the ref becomes one sha that every build job
   clones, so all platforms ship the same commit. On the cron it builds only when core main differs
   from the sha kih `bin/` was last published from (the `WKAppBot@<sha>` token in the kih bin commit
   message) and `csharp/` changed between the two.
3. **Build + verify + kih harness QA** per platform (jobs below).
4. **Deploy:** `publish-to-kih` on every green run of `main` (dispatch, cron, push) or a tag; a
   `v*-sdk` tag also cuts the GitHub release. One kih commit per run:
   `bin: platform binaries from WKAppBot@<sha>` with the run URL in the body.
5. **Concurrency:** one run per core ref; two core pushes queue rather than race.

## Jobs

| Job | Runner | What |
|---|---|---|
| build linux-x64 | ubuntu-latest | `dotnet publish core/csharp/src/WKAppBot.CLI.Linux -r linux-x64 --self-contained -p:PublishSingleFile=true` |
| build osx-arm64, osx-x64 | macos-latest | same project, `-r osx-arm64` / `-r osx-x64` |
| build win-x64 | windows-latest | `WKAppBot.CLI` -> `wkappbot-core.exe`; this repo's `WKAppBot.Launcher` -> `wkappbot.exe` |
| verify (x4) | native runner | Linux / macOS: `install.sh` from the artifact (checksum-verified), then `.github/scripts/portable-smoke.sh` (`--version`, `help`, `skill read on-load` against a seeded skills dir, `skill list`, Hangul file round-trip). osx-x64 runs under Rosetta 2. Windows: core `--version` and `skill read on-load` (120 s budget each), launcher file version. |
| SHA256SUMS | ubuntu-latest | `wkappbot-core.exe` over 100 MB is shipped as `wkappbot-core.exe.xz`; sums for all assets; artifact `release-assets` |
| release | ubuntu-latest | only on a `v*-sdk` tag: creates the release with the CHANGELOG section as notes and uploads the assets |
| resolve | ubuntu-latest | ref -> one core sha; cron decides whether anything changed |
| publish-to-kih | ubuntu-latest | green run on `main` (push, dispatch or cron) or a tag; skipped with a warning while `KIH_PUSH_PAT` is unset: `.github/scripts/publish-to-kih.sh` runs kih `tools/wk-bin-acquire.sh publish` per changed binary (`WK_BIN_NO_PUSH=1`), then one `git push origin HEAD:main` with a rebase-retry loop. Unchanged binaries (sha256 equal to the MANIFEST row; the Windows core compared decompressed) make no commit. |

Every verify job also clones kih (`KIH_PUSH_PAT`) and runs `tools/wk-harness-qa.sh --quick --json`
with `WKAPPBOT_CORE` pointing at the binary under test (`.github/scripts/kih-harness-qa.sh`;
exit 0 pass, 1 fail, 2 cannot evaluate).

Any red job stops the release.

## Asset names are a contract

`wkappbot-linux-x64`, `wkappbot-osx-arm64`, `wkappbot-osx-x64`, `wkappbot.exe`,
`wkappbot-core.exe` (or `.xz`), `SHA256SUMS`. `install.sh` and the kih acquirer download
`releases/latest/download/<name>`; renaming one breaks every installer.

## Secrets

- `WKAPPBOT_CORE_PAT`: read access to the private core repo.
- `KIH_PUSH_PAT`: fine-grained token, Contents read/write on `kiexpert/wkappbot-kih` only.
  `gh secret set KIH_PUSH_PAT --repo kiexpert/wkappbot-sdk` (paste the token at the prompt).

## Triggers

- `workflow_dispatch` with input `core_ref` (default `main`). GitHub only offers this once the
  workflow file is on the default branch.
- `repository_dispatch` type `core-updated`, `client_payload.ref` = core ref.
- push of a `v*-sdk` tag: full run plus release.
- push to `main` or `claude/release-*` touching the pipeline files: build + verify (main also publishes to kih).
- hourly cron (`17 * * * *`, default branch only): builds only when the core moved (see Build farm).

`repository_dispatch`, cron and `workflow_dispatch` run the workflow file from the default branch,
so none of them works until `platform-release.yml` is on `main`.

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
