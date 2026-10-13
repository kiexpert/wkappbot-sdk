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
