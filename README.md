# WKAppBot

Shared AI guidance: see [AGENTS.md](./AGENTS.md) and [CLAUDE.md](./CLAUDE.md).
AI agents should read all top-level `*.md` files in the repository root before making non-trivial changes.

## Runtime standard
- Official launcher: `bin/wkappbot.exe`
- Official core: `bin/wkappbot-core.exe`
- Runtime HQ data: `bin/wkappbot.hq/`

## CI standard
- Build official `bin/`
- Test using `wkappbot ...` via PATH injection to `repo_root/bin`
- Prefer artifact-based validation of the same binaries users run

## Shared AI references
- `README.md` for repository overview
- `AGENTS.md` for shared AI engineering rules
- `CLAUDE.md` for detailed operational guidance
