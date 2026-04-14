# WKAppBot AI Shared Reference

This file is the shared reference for AI agents working in this repository.
Read this first before changing code, scripts, or workflows.

## 1. Official runtime binary
- The official runtime entrypoint is `bin/wkappbot.exe`.
- The official core binary is `bin/wkappbot-core.exe`.
- AI smoke tests and command checks must use the official binary via PATH:
  - add `repo_root/bin` to PATH
  - run commands as `wkappbot ...`
- Avoid `dotnet run` for runtime validation when the official binary is available.

## 2. HQ runtime data location
- Runtime data must accumulate under:
  - `bin/wkappbot.hq/...`
- This includes logs, skills, handlers, profiles, and experience data.
- Keep the current working directory unchanged when possible.
- Prefer changing PATH over changing CWD.

## 3. CI / smoke standard
- CI should test the closest possible environment to real user execution.
- Preferred flow:
  1. build official `bin/`
  2. upload `bin/` as artifact
  3. download artifact in later test stages
  4. add `bin/` to PATH
  5. run `wkappbot ...`
- NuGet cache is the primary safe cache.
- Artifact reuse is preferred over rebuilding in later stages.

## 4. Script standard (.py / .ps1 / .sh)
- Scripts should be safe to run with no arguments.
- No-arg behavior should do the following:
  - print usage
  - run minimal self-test if appropriate
  - exit with code 0 on healthy state
- CI may run changed scripts once with no arguments.

## 5. Skill-first principle
- AI tools should learn from skill commands first.
- `skill`, `knowhow`, and `schedule` are important baseline commands and should always remain testable.
- Smoke tests should include at least:
  - `skill --help`
  - `knowhow --help`
  - `schedule --help`
  - `skill list`
  - `schedule list`

## 6. File tool baseline
- File tools should support basic non-interactive verification.
- Smoke tests may include:
  - `file read`
  - `file grep`
  - `file glob`

## 7. Experience promotion policy
- Runtime experience may accumulate in HQ and later be promoted into the repository.
- Promote only durable and reusable knowledge.
- Avoid committing volatile or sensitive runtime artifacts.
- Good candidates for commit:
  - validated knowhow
  - stable handlers
  - generalized profiles
  - reusable experience summaries

## 8. Workflow editing rules
- Prefer simple, stable workflows over clever ones.
- Test the same artifact that users will run.
- Keep build and smoke semantics obvious.
- If there is a mismatch between script intent and workflow behavior, fix the workflow to match the runtime standard.

## 9. Change safety
- Minimize branch/PR clutter.
- Prefer fewer, coherent commits.
- When unsure, preserve the official runtime path and HQ layout.

## 10. Practical rule of thumb
- Official binary first.
- HQ path fixed.
- PATH injection preferred.
- No-arg scripts should be healthy.
- Skill commands are core AI learning surface.
