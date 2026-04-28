# Changelog

All notable changes to WKAppBot SDK are documented here.
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [6.5.21-sdk] - 2026-04-29

### Added
- `publish-core-for-sdk.yml` CI workflow: dev core binary auto-released as standalone asset
- MIT `LICENSE` file
- `CONTRIBUTING.md`, `CODE_OF_CONDUCT.md`, `SECURITY.md`
- Issue templates (bug, feature, license question), PR template
- GitHub Discussions enabled

### Changed
- Recommended install path updated to `%USERPROFILE%\Documents\wkappbot` (privacy rationale)
- `build.yml`: `fetch-depth: 0` for correct patch version counting
- `build.yml`: core binary now downloaded from dev `core-*` release, source build as fallback
- `find-stable-release.ps1`: `gh auth token` local fallback, `-Force` flag, JSON guard

### Fixed
- Launcher `WKAppBot.Launcher.csproj`: added `ProjectRoot.cs` compile include (Hash8 build error)
- `build.yml`: volatile `runtime/` excluded from artifact upload (ENOENT race)
- `build.yml`: `GH_TOKEN` forwarded to smoke test for license auth

### Security
- Removed Slack `webhook.json`, `.mcp.json`, `.wkappbot/`, `docs/handoff/`, `.ci-test-tmp/` from full git history
- Replaced hardcoded internal paths in `AgentsPolicy.cs` and `AppBotEyePromptInfo.cs` with env vars
- `AppBotUpdateCommand`: default repo changed from private to `kiexpert/wkappbot-sdk`

## [6.5.0-sdk] - 2026-04-28

### Added
- Initial v6.5 SDK release
- Per-repo Eye isolation via `ProjectRoot.Hash8()` pipe names
- ECDSA offline license system (`LicenseManager.cs`)
- `build.cmd --download-only` for binary-only install
- GitHub collaborator-based license tier auth (in closed core)

## [6.0.0-sdk] - 2026-04-28

### Added
- Initial public SDK release
- MIT launcher source (`WKAppBot.Launcher`)
- AOT-compiled launcher binary (~1 MB)
- Automated smoke test suite (25 basic checks)
