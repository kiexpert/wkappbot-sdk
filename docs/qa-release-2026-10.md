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
