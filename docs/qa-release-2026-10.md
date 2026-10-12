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

## Evidence

- macOS workflow: https://github.com/kiexpert/wkappbot-sdk/actions/runs/38128243808
- WKAppBot workflow: https://github.com/kiexpert/WKAppBot/actions/runs/38115491601
- File created: docs/qa-release-2026-10.md
- Test command reference: gh workflow run macos-test.yml --repo kiexpert/wkappbot-sdk
