# WKAppBot SDK v7.10.0-sdk — Aligned with core v7.10

*Public Launcher release paired with WKAppBot core v7.10 (official QA launch, 2026-09-28).*

## What this release ships

- **Launcher wrapper** built from wkappbot-sdk source (Launcher + Shared only, MIT license).
- **Core binary** downloaded from the WKAppBot core v7.10.0 GitHub release and bundled with the Launcher into `wkappbot-vX.Y.Z-sdk.zip`.

## Aligned with WKAppBot core v7.10

The core carries the substantive changelog for this release; the SDK's own delta since v7.6 is maintenance only. See the core release notes for full user-visible features:

- **[WKAppBot core v7.10 release notes](https://github.com/kiexpert/WKAppBot/releases/tag/v7.10.0)** — Indexer & Executor Consolidation.

Core themes at a glance:

- **Memory system: wkhippo indexer parity** — regex search over Claude/Codex transcripts with a persistent weekly cache; `wkhippo trace SID` for one-session event tails.
- **ReadFormatter: exact-phrase then keyword fallback** — copy-pasted quotes never return "no match" when the words are all present.
- **Executor & spawn-site: one core-path resolver** — every spawn call wraps its process path through `Program.ResolveExistingCoreExe`.
- **Hotswap: swap only, never kill** — stale-worker kill loop removed from `PerformHotSwap`.
- **Harness reflex** — branch-switch refusal in shared working trees; bash-pwsh flag-only refusal.
- **Skill 3-tier migration** — `wk-unwired` fully tiered; SkillCommand.Edit gained delete-step verbatim-quote guard.
- **Raw command line pipeline** — launcher forwards caller raw command line to Eye; MCP runner hands it to core via `WKAPPBOT_RAW_CMDLINE`.

## SDK maintenance since v7.6.0-sdk

- **Bash-pwsh block refinement**: matches only powershell/pwsh CALLED with a flag; path-prefixed powershell.exe still refused.
- **Codex SKILL.md mirrors retired** at call site (owner ruling 2026-09-25).
- **`wkdoctor` agy integration** carried forward from 7.6.0-sdk.

Version scheme jumps 7.6.0-sdk → 7.10.0-sdk by design to align with core minor. 7.7 / 7.8 / 7.9 SDK entries were skipped upstream and are not backfilled.

## Install

Download the latest `wkappbot-v7.10.X-sdk.zip` from the [Releases](https://github.com/kiexpert/wkappbot-sdk/releases) page, extract, and run `bin/wkappbot.exe`. See [README.md](https://github.com/kiexpert/wkappbot-sdk#quick-start) for detail.

## Fleet credit

Sonnet 643, Haiku 508, Opus 383, Human 227 — 3344 commits total across the paired core release.

---
