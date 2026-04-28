# Upgrade Guide

## General upgrade process

```bat
cd %USERPROFILE%\Documents\wkappbot
git pull
build.cmd
```

Eye detects the new `wkappbot-core.new.exe` and hot-swaps automatically. No restart needed.

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
