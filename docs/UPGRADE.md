# Upgrade Guide

## General upgrade process

```bat
cd %USERPROFILE%\Documents\wkappbot
git pull
build.cmd
```

Eye detects the new `wkappbot-core.new.exe` and hot-swaps automatically. No restart needed.

## v7.0 → v7.1

- **CDP port isolation**: each project now gets a deterministic 4-port block (9300-9995) based on SHA256 of the project root path. Foreign Chrome instances are automatically rejected.
- **IME daemon auto-update**: no more triple-character input after core updates. Eye tick replaces the old IME daemon within 60s.
- **Tab reuse**: `cdp open` and `ask` commands reuse existing tabs by project CWD hash, not HWND. Same URL from same project always reuses the tab.
- `build.cmd` upgrade is sufficient -- hot-swap handles the rest.

## v6.5 → v6.5.x (patch)

Patch releases are backwards-compatible. `git pull && build.cmd` is sufficient.

## v6.0 → v6.5

### DataDir moved

DataDir is now `{project-root}/.wkappbot/hq/` instead of `{exe-dir}/wkappbot.hq/`.

If you have existing experience DB or skills data under `bin\wkappbot.hq\`, move them:

```powershell
# From your wkappbot repo root
Move-Item bin\wkappbot.hq .wkappbot\hq -Force
```

### Pipe names now include repo hash

Named pipes now include a per-repo hash suffix (`wkappbot_eye_ipc_{hash8}`).
Multiple repo clones no longer share a single Eye — each gets its own instance.

If you have scripts that reference the old pipe name `wkappbot_eye_ipc`, update them to use `wkappbot eye tick` to discover the active instance.

### License system change

v6.5 uses ECDSA file-based licenses (offline) combined with GitHub collaborator auth.
Free tier works without any login. Paid tiers require `gh auth login` once.

## v7.1 → v7.2

- No breaking changes
- New: Chrome session geometry persistence (window position saved per project)
- New: wkfind multi-keyword code+session search tool
- New: wkask real-time ask pipeline health monitor

## v7.2 → v7.3

- No breaking changes
- Fixed: Chrome window position mismatch (session restore override) — two-layer fix
- Fixed: IME Relay jamo-sync (kana/hangul input relay)
- Fixed: Eye startup FileNotFoundException false positive (single-file publish defense)
- New: Launcher HWND ancestor-walk caller resolution (P2 ConPTY support)
- New: standard-appbot-window / standard-chrome-window skills
