# WKAppBot AI Shared Reference

This file is the shared reference for AI agents working in this repository.
Read this first before changing code, scripts, workflows, or shared documentation.

## Document role split
- Read `CLAUDE.md` first, then use this file for Codex and other non-Claude agents.
- Keep `AGENTS.md` focused on shared operating rules, workflow discipline, and runtime guardrails.
- Keep Claude-specific reminders and project-facing guidance in `CLAUDE.md`.
- If the launcher or core runtime is edited, finish the loop immediately: build, deploy, hot-swap, and smoke test before pausing. Do not stop after the code edit or ask the user to continue the release step later.

## Codex / other-agent flow
- Use this file for Codex and other non-Claude agents; keep Claude-facing notes out of it.
- Keep shared rules here short and durable: official binaries, HQ layout, skill discipline, CDP isolation, first-limit handoff, and release loop guardrails.
- If a rule must be visible to Claude too, keep the short shared reminder here and mirror the Claude-facing note in `CLAUDE.md`.
- If `bin\wkappbot.exe` is locked during publish, do not leave the repo in a half-deployed state. Finish by restoring a runnable launcher to `bin\wkappbot.exe` from a clean publish output, and keep the previous launcher only as `wkappbot.old-*.exe` backup.

## Cross-reference rule
- Read all top-level Markdown files in the repository root before making non-trivial changes.
- At minimum, check these files when they exist:
  - `README.md`
  - `AGENTS.md`
  - `CLAUDE.md`
- Treat these files as shared AI guidance and keep them mutually consistent.
- If one file changes shared policy, update the other shared-reference files as needed.

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
- `skill` and `knowhow` are important baseline commands and should always remain testable.
- `schedule` is not part of the public SDK smoke surface unless the current binary explicitly exposes it.
- For load/compact handoff, start with:
  ```bash
  wkappbot skill read claude-session-handoff
  wkappbot skill read handoff-checklist
  wkappbot skill read handoff-send-best-practice
  ```
- Smoke tests should include at least:
  - `skill --help`
  - `knowhow --help`
  - `skill list`
- For SDK ask QA or latency debugging, use `wkask` as the default live-monitoring tool before patching ask behavior or documenting the rule.
- For public SDK regressions, start from `sdk-user-perspective-test-playbook` and the matching `wkask`/`wkcdp` smoke first; the user path is the truth source.
- CDP is project-scoped only. Reuse the current project's Chrome/tab state; ignore foreign-project Chrome/CDP ports and never attach across project boundaries.

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
- When public workflows derive durable CI evidence from private-core downloads, promote a sanitized summary back to `WKAPPBOT_CORE_REPO`; never copy secrets or raw private logs.
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
- First limit = no retry. Mark handoff_pending, let the current atomic task finish if it is already running, and let the next AI/provider stand by. If the same limit appears again while pending, terminate the current session and do not retry the same CLI.
- Every project keeps its recurring `Main Duties` block in repo `CLAUDE.md`, and the release loop is always `build -> deploy -> hot-swap -> smoke test`.

## 10.1 Korean response style
- Do not imitate the user's speech style or dialect.
- Final responses must use polite Korean `해요체`.
- Never use `소`체 or casual speech.

## 11. Encoding policy
- Treat repository text files as UTF-8 by default.
- If a source arrives in CP949 or another non-UTF-8 encoding, preserve the original file and also save a UTF-8 copy for reading and editing.
- Prefer UTF-8-safe editing tools. If a tool may silently re-encode content, use `wkedit` or another verified UTF-8-safe path.
- Keep binary files unchanged. Do not text-convert `.pdf`, `.mp4`, images, or archives.
- Multibyte filenames are allowed, but repository writes should still use UTF-8 so downstream tools and web views render them reliably.

## 12. GitHub tool usage knowhow
- Large multi-file workflow edits may be blocked by the platform safety layer even when the content is valid.
- When GitHub tool updates are unstable, split the change into small pieces:
  1. create blob
  2. create tree
  3. create commit
  4. update ref
- If a step is blocked, retry only that step first.
- When isolating a blocking pattern, prefer one-file or one-topic commits.
- Confirm the current main SHA before building a tree or commit.

## 13. GitHub Actions environment knowhow
- Do not assume every Actions expression context works in every YAML location.
- If a workflow title disappears or the workflow fails before jobs start, check expression placement first.
- `runner.*` expressions are safest inside step-level keys and `with:` values, not arbitrarily hoisted.
- Keep workflow syntax conservative.

## 14. Self-test workflow policy
- Script self-test should choose the latest commit among recent history that actually changed scripts.
- The selected commit's `*.ps1`, `*.py`, and `*.sh` files should all be tested.
- Self-test scripts are AppBot-integrated by default unless explicitly documented otherwise.
- If scripts need helpers, allow those helpers to be installed inside `bin/` and persisted by cache.

## 15. Cache policy
- Prefer repo-local caches so helper tools survive across CI runs.
- `bin/` is the primary reusable cache surface for self-test helpers and official binaries.
- Restore by exact key first, then by recent prefix, so cache misses are rare.
- After self-test, save updated `bin/` cache so newly installed helpers remain reusable.
- Build cache should prioritize recent-hit behavior over strict exact-only reuse.
- Intermediate build outputs may be cached when they reduce rebuild cost and do not break correctness.

## 16. GitHub Actions weirdness response
- When CI behavior is surprising, first determine whether the issue is:
  - workflow trigger design
  - expression location
  - cache key mismatch
  - no-arg script behavior
  - commit/file selection logic
- Extract tricky inline logic into repository scripts so the logic can be self-tested directly.
- Prefer repository scripts over long one-liners inside YAML for commit/file selection.

## See also
- `README.md`
- `CLAUDE.md`

## Commit Gate (encoding enforcement)

After completing any file changes, always run `git add -A && git commit -m "..."`
The pre-commit hook (cp949_save_guard.ps1) checks all staged files for CP949-incompatible characters.
If commit fails: fix the flagged file, re-stage, retry. Repeat until green.
This is the encoding enforcement gate -- never skip it.

## Skill Reference Requirement

Before any delegation or agent task, include 3+ `wkappbot skill read <id>` references in the prompt.
Use `wkappbot skill search <keyword1> <keyword2>` to find relevant IDs first.
Agents start with zero context -- skill reads are their only briefing.

## Self-Harness Rules

Declarative rules in CLAUDE.md via ## harness: headings. Fire on every tool call.
safe: skip all warn+block if toolInput matches. warn: stderr warning. block: stderr + exit 1. done: PostToolUse warning.
Edit CLAUDE.md to add or fix rules (always safe-exempted).

## 17. Tool usage mandate (Codex wrappers required)

All file I/O and shell execution MUST route through the Claude-named wrapper .cmd files.
Wrappers pipe stdin JSON through wkharness.ps1 (pre-tool guard) before executing.

| Intent | Required wrapper | Forbidden native |
|--------|-----------------|------------------|
| Write a file | `Write.cmd` | `write_file`, `apply_patch` |
| Edit a file | `Edit.cmd` | `write_file`, `apply_patch` |
| Read a file | `Read.cmd` | `read_file` |
| Glob files | `Glob.cmd` | `list_dir`, `find_files` |
| Grep/search | `Grep.cmd` | native grep via shell |
| Shell (bash) | `Bash.cmd` | `shell`, `run_shell`, `execute` |
| Shell (cmd) | `Cmd.cmd` | `shell cmd /c ...` |
| Shell (ps) | `PowerShell.cmd` | `shell powershell ...` |
| Delegate agent | `Agent.cmd` | direct agent spawn |

Wrapper location: `D:\GitHub\WKAppBot\bin\` (symlinked from `D:\GitHub\personal-docs\tools\`).
Add this bin dir to PATH so wrappers resolve without full paths.

Why: native tools bypass wkharness guards (spec-gate, brief-guard, pace-guard, harness:block).
Violations are blocked by wkharness PreToolUse hook via harness:block patterns in CLAUDE.md.

### Agent.cmd calling conventions (mirrors Claude Code Agent() built-in)

In codex exec mode, native file tools (write_file, apply_patch, read_file) bypass hooks entirely.
Route file operations through Agent.cmd to enforce harness guards:

`
# CLI arg style (preferred from shell_command):
Agent.cmd "prompt" [--model haiku|sonnet|opus]
Agent.cmd --help

# JSON stdin style (Claude Code tool-dispatch format):
echo '{"tool_input":{"prompt":"...","model":"haiku"}}' | Agent.cmd
`

**File operations via Agent.cmd** (enforces wkharness guards on the subagent):
`
Agent.cmd "wkappbot skill read wkharness-guards
wkappbot skill read codex-tool-wrappers
wkappbot skill read claude-md-harness-rules

Write 'hello' to D:/tmp/test.txt using Write.cmd"
`

Guards enforced on every Agent.cmd call:
- brief-guard: 3x wkappbot skill read refs required in prompt
- spec-gate: --model opus calls require SPEC block
- claude-md-sync-guard: CLAUDE.md Pending must be current
