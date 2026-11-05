# QA Release Checklist - 2026-10

## Products Under Test

| Product | OS | Build Command | Smoke Command | Last Result | Owner Root | Test Date | Run ID |
|---------|----|----|---|---|---|---|---|
| wkappbot-sdk | macOS | gh workflow dispatch macos-test | macos-test.yml | PASS | ROOT | 2026-10-11 | 38128243808 |
| WKAppBot | Windows | csharp/bin/Release | wkappbot ask gpt test | PASS | DG-WKAppBot | 2026-10-11 | 38115491601 |
| WKAppBot.Reflex | Windows | build.cmd reflex | wkappbot eye tick + wkdoctor | PASS | harness-team | 2026-10-11 | 38115491601 |

## Test Status

- Created: 2026-10-11T000Z
- Updated: 2026-10-12T (ROOT session start)
- macOS workflow completed: run 38128243808 (2026-10-11, PASS)
- macOS workflow triggered: run 38251290294 (2026-10-12, in progress)
- WKAppBot reflex-app-qa completed: run 38235042876 (2026-10-12T10:14:13Z, PASS)
- Prior macOS run #6 (2026-10-01) completed PASS

## Actions Failure Triage (2026-10-12)

### wkappbot-sdk Failures
- **Workflow: Skill Check** (10 runs on 2026-10-12)
  - Run 38222257253 (08:13:23Z): FAIL - Validates 821 skills, all PASS in log preview but failure in full log (run IDs: 38222257253, 38216506260, 38214404488, 38213046476, 38208498736, 38208406184, 38208247090, 38207659663, 38202410053, 38192702469)
  - Root cause: Likely JSON validation issue in skill files or a post-validation step failure
  - Status: INVESTIGATION NEEDED - full log required to identify specific failing skill

- **Workflow: User-Perspective QA** (2 runs)
  - Run 38247733099 (12:13:59Z), Run 38211861249 (06:18:10Z): FAIL
  - Status: License status test or SDK functionality check failure

- **Workflow: CDP Connectivity Smoke** (1 run)
  - Run 38248574671 (12:21:58Z): FAIL - Core binary fetch failure (secrets required)
  - Root cause: WKAPPBOT_CORE_REPO / WKAPPBOT_CORE_PAT secrets not available or invalid
  - Status: Infrastructure/secrets issue - requires secrets validation

- **Workflow: build-skill-page** (1 run)
  - Run 38192702582 (01:39:01Z): FAIL
  - Status: INVESTIGATION NEEDED

### WKAppBot Failures  
- **Workflow: Skill Check** (20 runs, all on 2026-10-12 from 03:26Z to 08:44Z)
  - Run examples: 38225423067, 38225110907 (mentioned by user)
  - Root cause: Same Skill Check validation as SDK - likely cascading from shared skill files or validation framework
  - Status: CASCADING FAILURE - fix SDK Skill Check to resolve

### wkappbot-kih Failures
- **Workflow: harness-ci** (20 runs, all on 2026-10-12 from 01:31Z to 12:56Z)
  - Run 38252268519 (12:56:43Z): FAIL - Parse error in PowerShell script
  - Root cause: **PARSE FAIL** in `tools/wkharness-common-briefskilllock/Test-WkBriefSkillKeep.ps1`
    - Multiple parse errors: Missing closing braces, unexpected tokens, unterminated strings
    - This is a CRITICAL issue - harness validation script is corrupted/incomplete
  - Status: **BLOCKING** - requires immediate fix to Test-WkBriefSkillKeep.ps1
  - Evidence: `1 of 753 harness script(s) failed to parse`

## Evidence

- macOS workflow: https://github.com/kiexpert/wkappbot-sdk/actions/runs/38128243808
- WKAppBot workflow: https://github.com/kiexpert/WKAppBot/actions/runs/38115491601
- File created: docs/qa-release-2026-10.md
- Test command reference: gh workflow run macos-test.yml --repo kiexpert/wkappbot-sdk
- Failure summary: 30+ combined failures from 3 repos, grouped by workflow and root cause
  - 10 SDK Skill Check (investigation needed)
  - 2 SDK User-Perspective QA (investigation needed)
  - 1 SDK CDP Connectivity (secrets issue)
  - 20 WKAppBot Skill Check (cascading)
  - 20 wkappbot-kih harness-ci (BLOCKING: Test-WkBriefSkillKeep.ps1 parse error)

## QA Matrix 2026-10-13

### 1. WKAppBot Reflex App QA (kiexpert/WKAppBot)

| OS | Run ID | Result | Date |
|----|--------|--------|------|
| macos-latest + windows-latest | 38285480982 | PASS | 2026-10-12T17:37:49Z |

Newest reflex-app-qa run: 38285480982 (completed success, triggered by push feat(macos-phase2-m2)).
Prior spokesman-noted run 38281811943 superseded by this run.

### 2. wkappbot-sdk macOS Test (kiexpert/wkappbot-sdk)

| OS | Run ID | Result | Date | Trigger |
|----|--------|--------|------|---------|
| macOS | 38291793651 | PASS | 2026-10-12T18:34:10Z | workflow_dispatch (this session) |
| macOS | 38251290294 | PASS | 2026-10-12T12:47:48Z | workflow_dispatch |

Triggered `gh workflow run macos-test.yml --repo kiexpert/wkappbot-sdk` → run 38291793651 completed success in 50s.

### 3. Failure Summary by Workflow (all repos, last 20 failures each, as of 2026-10-13)

#### kiexpert/WKAppBot
| Workflow | Failure Count | Latest Failure Run | Date Range |
|----------|--------------|-------------------|------------|
| Skill Check | 20 | 38285481028 | 2026-10-12 04:00Z–17:37Z |

#### kiexpert/wkappbot-sdk
| Workflow | Failure Count | Latest Failure Run | Notes |
|----------|--------------|-------------------|-------|
| Skill Check | 12 | 38222257253 | 2026-10-11T18:07Z–2026-10-12T08:13Z |
| User-Perspective QA | 4 | 38288799172 | investigation needed |
| CDP Connectivity Smoke | 1 | 38248574671 | secrets/infra issue |
| build-skill-page | 1 | 38192702582 | investigation needed |

#### kiexpert/wkappbot-kih
| Workflow | Status | Notes |
|----------|--------|-------|
| harness-ci | **GREEN** since run 38283074760 (2026-10-12T17:16Z) | 20 prior failures resolved; commit 2becfe008 fixed Test-WkBriefSkillKeep.ps1 parse error |
| Latest 3 runs | SUCCESS (38290860495, 38284755000, 38283074760) | All 2026-10-12 |

## Actions Fix Log 2026-10-13

| Workflow | Root Cause | Fix Commits | Green Run ID |
|----------|------------|-------------|--------------|
| Skill Check | 12 skills had empty steps[] or id/filename mismatch; on-load skill lost claude-md-guide chain reference | ea7cf366 (build.cmd EnableDelayedExpansion !-swallowing), 533ced71 (on-load chain ref + VERSIONING example) | 38325685030 |
| User-Perspective QA | build.cmd exit 255 (!-swallowing); VERSIONING.md stale v7.10.0-sdk example caused version mismatch; D:/GitHub/ in CLAUDE.md triggered hygiene scan; scan scope too broad (checked marketing pages for internal email) | ea7cf366 + 533ced71 + db87710e | 38328390676 |
| All-Green Status (2026-10-13 02:03Z) | No failures since baseline 38328811218; all scheduled runs passing | N/A | 38335082567, 38334514246, 38332766123, 38332715710 |
| All-Green Status (2026-10-13 02:46Z) | No failures post-baseline; SDK scheduled runs all passing; macOS test run 38291793651 green | N/A | 38338458291, 38337808969, 38337352655, 38336914732, 38291793651 (macOS) |

## QA Matrix 2026-11-04 (private portal shims)

The target portal repo is found by sha256 prefix in each workflow and is not named here.

| Workflow | Run ID | Trigger | Result | Notes |
|----------|--------|---------|--------|-------|
| private-im-live | 41811614360 | push (PR #438 branch) | PASS | intelligence-e2e ran after the wkwire daemon; proof mode=live, handed over on the relay branch |
| private-pages-relay | 41818713862 | push (#438 merge) | PASS | portal/intelligence-e2e-proof.json served with mode=live, ok=true, run=41811614360 |
| private-pages-relay | 41924419023 | schedule | PASS | hourly republish |
| private-portal-qa | 41811649554 | push (#436 merge) | FAIL 140/141 | case `run` timed out; fixed on the portal side |
| private-portal-qa | 41819696879 | push (PR #440 branch) | PASS | after the portal overlay fix |
| private-portal-qa | 41820860569 | push (#440 merge, main) | PASS | first green run on main |
| private-portal-qa | 41918774375 | schedule 21:41 UTC | FAIL 103/112 | REGRESSION: the new owner-unlock iframe `#ownerUnlock` (`../go/unlock.html`) intercepts pointer events (selftest.js:3426); portal-side fix needed |
| private-qa-video | 41809826085 | push (PR #437 branch) | FAIL | the script-selection overlay intercepted scene clicks |
| private-qa-video | 41819697040 | push (PR #440 branch) | PASS | PORTAL_REC=1 hides the overlay while recording |

Open items:
- PR #437 (qa-video `src_ref` / `expect_sha` / `scheme` dispatch inputs) is still open. The audio-pad build needs an owner dispatch, because the session integration got 403 on workflow_dispatch.
- The portal-qa live-write twin (PR #439) stays off until the portal adds its `WK_QA_LIVE_WRITE` reader.

## QA Matrix 2026-11-05 (cross-platform runners)

Source run: 41980395003 (harness-windows-qa, branch of PR #445, merged as ad9740b). The portal repo is found by sha256 prefix and is not named.

| Job | OS | Run ID | Result | Notes |
|---|---|---|---|---|
| harness-qa | windows | 41980395003 | FAIL | Step 3 (Python selftests) stops on kih main. Reproduced on Linux: `wkhippo_reflex_families_selftest.py` RESULT 29/31; FAIL `dump_raw returns original lines byte-equal` and `dump_raw honours since`. Harness-repo regression. |
| harness-qa | macos | 41980395003 | FAIL | Failed step not inspected yet; most likely the same kih main regression. |
| portal-selftest | windows | 41980395003 | FAIL 79/83 | P72/P73: `#termToggle` label clipped by 101px at 390 wide; `run`: waitForFunction timeout at selftest.js:2144. Portal-side. |
| portal-selftest | macos | 41980395003 | FAIL 141/152 | P120: rhythm clock 6.9 cps vs the 12 target; P137: viewport 484 wide vs 390. Portal-side. |

### Re-run 2026-11-05 (branch ci/daily-tools-matrix)

The docs repo is found by sha256 prefix and is not named. The daily-tools job prints only the case line, the exit code and the exception class, because tool output can hold personal data and these logs are public.

| Job | OS | Run ID | Result | Notes |
|---|---|---|---|---|
| harness-qa | windows | 41993715640 | PASS | Python, launcher and Bash self-tests all green on kih main (families selftest regression fixed upstream). |
| harness-qa | macos | 41993715640 | PASS | Same, plus the stock /bin/bash 3.2 pass. |
| portal-selftest | macos | 41993715640 | FAIL 142/154 | P73, P75, P79, P87, P92, P93, P102, P120 (6.9-7.7 cps vs 12), P135, P137, P141 and one more. Portal-side. |
| daily-tools | windows | 41993715640 | FAIL 1/4 | calendar-list FileNotFoundError (no credential file on CI); court-pdf ModuleNotFoundError; mail-send-dry-run rc=2 (argument usage); mail-sync-help PASS. |
| daily-tools | macos | 41993715640 | FAIL 1/4 | Identical to Windows: the failures are setup, not platform. |
| daily-tools | both | 41990213734 | FAIL 0/4 | First run: output fully discarded and scripts called by fixed paths, so no cause was visible. |
| daily-tools | both | 42003780494 | FAIL 1/4 | After pymupdf and the dry-run args: mail-sync-help PASS; calendar-list and mail-send-dry-run FileNotFoundError (credential files exist only on the owner PC); court-pdf rc=1 with no exception class. CI-environment limits, not platform defects. |

### Gmail inbox sync twin (private-gmail-inbox-sync.yml)

The docs repo's Gmail sync script hard-codes a Windows .env path and output path, so it had only run on the owner PC and the inbox folders stopped at 2026-10-16. The new SDK workflow loads the script as a module and points both paths at the runner. Account letters: A and B; addresses are never logged.

| Run ID | Result | Notes |
|---|---|---|
| 41999934318 | PASS (misfiled) | 74 files committed, but account A landed in B's folder: the address secret lacks the slug the script tests. |
| 42003780766 | FAIL | One-time undo step reran a revert that was already applied; step removed. |
| 42007793451 | PASS | Per-account summary: A syncs normally; B token refresh fails with HTTP Error 400. |
| 42009675181 | FAIL at push | Restore and correct sync done (A inbox through 20261104), push rejected non-fast-forward. |
| 42011155457 | PASS | Restore, correct sync and push landed: 72 files. A is current through its newest mail (20261104); no mail dated 20261105 yet. B needs a new refresh token (owner-only). |

