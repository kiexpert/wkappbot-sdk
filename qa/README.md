# All-platform QA (Windows / Linux / macOS)

One script-driven suite for **(A) the wkappbot CLI** (+ `wkhippo`) and **(B) the kih harness**.
It prints a per-check table for the platform it runs on, saves `qa/results/<platform>.json`,
and prints a per-platform pass/fail table merged from every saved result. A platform with no
saved result shows `not run here` plus the exact command to run there.

Only Python 3.8+ and git are needed to run the suite. Hook and installer checks also need what
the harness itself needs (`pwsh`, `bash`).

## Run

```bash
# Linux / macOS (Git Bash works too)
KIH_ROOT=../wkappbot-kih WKAPPBOT_ROOT=../WKAppBot \
WKAPPBOT_CLI=../WKAppBot/<publish dir>/wkappbot TRANSCRIPT=~/.claude/projects/<slug>/<session>.jsonl \
  qa/run-qa.sh
```

```bat
rem Windows (cmd)
set KIH_ROOT=..\wkappbot-kih
set WKAPPBOT_ROOT=..\WKAppBot
set TRANSCRIPT=%USERPROFILE%\.claude\projects\<slug>\<session>.jsonl
qa\run-qa.cmd
```

Exit code: `0` all PASS/SKIP, `1` any FAIL, `2` runner error. Commit the updated
`qa/results/<platform>.json` so the table shows that platform's column for everyone.

Self-test of the suite itself (fakes only, needs no other repo): `python3 qa/selftest.py`

## Parameters

Every location is a parameter. Each one resolves in the same order on every OS:
**env var -> sibling checkout next to this repo -> `D:/GitHub/<repo>`** (the historical Windows default).

| Env var / flag | Meaning | Default after env |
|---|---|---|
| `KIH_ROOT` / `--kih-root` | wkappbot-kih checkout | `../wkappbot-kih`, then `D:/GitHub/wkappbot-kih` |
| `WKAPPBOT_ROOT` / `--wkappbot-root` | WKAppBot checkout | `../WKAppBot`, then `D:/GitHub/WKAppBot` |
| `WKAPPBOT_CLI` / `--cli` | built CLI binary | build dirs under `WKAPPBOT_ROOT`, then `wkappbot` on PATH |
| `TRANSCRIPT` / `--transcript` | a Claude session `.jsonl` | none (wkhippo transcript cases SKIP) |
| `WKHIPPO` | wkhippo launcher | `WKAPPBOT_ROOT/bin/wkhippo[.cmd]`, then PATH |
| `HOOK_SETTINGS` | extra settings.json to read hooks from | also `KIH_ROOT/.claude/settings*.json`, `~/.claude/settings.json` |
| `KIH_INSTALLER` | installer script | per OS under `KIH_ROOT` (see `checks/installer.py`) |
| `QA_HOOK_BUDGET_MS` | per hook call budget | `1000` (the 1 s mandate) |
| `QA_SKILL_ID` | skill the mutating encoding cases edit | `qa-encoding-probe` |
| `QA_TIMEOUT_S` | per command timeout | `60` |
| `PYTHON_CMD` | interpreter for the launchers | `python3` / `py -3`, then `python` |
| `--areas a,b` | subset of `static,hygiene,cli,encoding,wkhippo,hooks,installer` | all |
| `--allow-mutations` | also run cases that edit a skill / keep a reflex | off |
| `--since REF` | static check: only lines added since REF | full scan against the baseline |
| `--update-baseline` | static check: freeze the current hits | - |
| `--table-only` | print the merged per-platform table from `results/`, run nothing | - |

## Areas

| Area | What it proves | Where |
|---|---|---|
| `static` | Cross-platform rule: no new `D:\` / `D:/` / `C:\Users` literals, no USERPROFILE-only joins. Hits that already exist are frozen as line hashes, so the baseline never republishes a path: the sdk baseline is `baseline/xplat-wkappbot-sdk.txt`; another repo's baseline is `<repo>/.qa/xplat-baseline.txt`, committed in that repo, so a private repo's file names never land here. Comment-only lines and `.evidence/` archives are skipped. `D:/GitHub/...` is allowed as the last resolver step, after an env lookup. Inline escape: `qa:allow-path`. | `checks/static_xplat.py` |
| `hygiene` | sdk version sync (CLAUDE.md header = CHANGELOG top = VERSIONING = README = props base); no personal machine paths in the public sdk repo; kih `.githooks/*` tracked as 100755; AutoCommit porcelain parse does not `Trim()` before slicing the path; no markdown `harness:block` rule (sdk/kih CLAUDE.md, kih REFLEX.md, `~/.claude/CLAUDE.md`) blocks pwsh from Bash off Windows, since a markdown rule cannot express an OS | `checks/hygiene.py` |
| `cli` | wkappbot command matrix: help, version, unknown command, skill list/search/read, suggest list, file read/write, parent dir creation | `fixtures/cli-matrix.json` |
| `encoding` | Hangul through file write (byte-exact), file read, a Hangul file name, skill edit + read (mutating) | `fixtures/encoding-matrix.json` |
| `wkhippo` | reflex `--rows/--since/--summary/--keep`, recall, recent (+`--json` must parse), trace, studied against `TRANSCRIPT` | `fixtures/wkhippo-matrix.json` |
| `hooks` | Uses the live wiring; when none exists it reports one `live hook wiring` SKIP row naming the files it read. Fixtures piped into every matching hook from settings, as Claude Code does: plain `ls` allow, cd-chain deny, force push to main deny, empty `core.hooksPath` deny, `pwsh` from Bash on a platform with no PowerShell tool, Read allow, PostToolUse no block, Stop clean exit; per-hook latency against the budget | `fixtures/hooks-fixtures.json` |
| `installer` | In a throwaway HOME/USERPROFILE seeded with a user hook: `--dry-run` changes nothing, install wires hooks, a 2nd install leaves every settings file byte-identical, user settings survive, `--uninstall` removes every harness hook and restores the seed. The installer never runs against the real checkout (install-linux.sh writes beside itself): KIH_ROOT is copied into the sandbox and the copy's installer runs; a final row FAILs if the real `KIH_ROOT/.claude` settings changed. All four scopes are watched (`settings.json` and `settings.local.json` in HOME and in the copy's `.claude/`) | `checks/installer.py` |

Command syntax lives in the JSON fixtures, so fix arguments there, not in code. A case with
`known_bug` still FAILs; the note is appended to its cause line. Hook fixtures call the real
hooks with a `qa-<random>` session id, so harness state files may pick up that session.

## Status

`results/linux.json` holds the first real run, relayed by the kih ROOT from the kih container
(Linux, pwsh 7.4.6, real kih + WKAppBot checkouts, the Linux minimal CLI, a live transcript):
34 PASS / 12 FAIL / 5 SKIP with mutations off. Only the per-area counts and FAIL cause lines
were relayed, so the file has no PASS/SKIP rows. View it with `qa/run-qa.sh --table-only`.
Three of those FAILs were checker issues and are fixed (comment-only lines, `.evidence/`,
installer writing `settings.local.json`); the rest are product defects or environment gaps.
Windows and macOS have not been run yet.

First-time setup on a box that has the other repos: `qa/run-qa.sh --areas static --update-baseline`
writes `.qa/xplat-baseline.txt` into kih and WKAppBot; commit it there.

## Skill home plan (dry run)

`python3 qa/skill_home_plan.py [--hq DIR] [--kih-root DIR] [--wkappbot-root DIR] [--source LABEL=DIR]`
lists every skill id with all its copies (sdk, kih, core, HQ cache, extra sources), flags ids
whose copies diverge (content hash ignoring volatile keys such as `updated`), and proposes one
home per id: `SDK` (generic, passes PUBLIC SAFETY), the one repo the skill is about, or the core repo `WKAppBot` (private and ambiguous; owner ruling 2026-10-21). Any owner-mandated private marker, or a hit of the kih public-skill gate, makes it private:
HANTOO, KIWOOM, token, account, .env, password, PIN, customer, owner-private rules, a personal
machine path, or a private app folder. `token` is split into `token:credential` and
`token:weak` (wording only) so the reviewer can tell them apart; both stay PRIVATE. Nothing is
moved or deleted. Output: `results/skill-home-plan.md`. Ids found only in private sources are
written as `private#<hash>` when the output is inside this public repo.
