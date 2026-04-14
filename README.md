# WKAppBot

Shared AI guidance: see [AGENTS.md](./AGENTS.md).

## Runtime standard
- Official launcher: `bin/wkappbot.exe`
- Official core: `bin/wkappbot-core.exe`
- Runtime HQ data: `bin/wkappbot.hq/`

## CI standard
- Build official `bin/`
- Test using `wkappbot ...` via PATH injection to `repo_root/bin`
- Prefer artifact-based validation of the same binaries users run
