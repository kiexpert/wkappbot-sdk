# QA Release Checklist - 2026-10

## Products Under Test

| Product | OS | Build Command | Smoke Command | Last Result | Owner Root | Test Date | Run ID |
|---------|----|----|---|---|---|---|---|
| wkappbot-sdk | macOS | gh workflow dispatch macos-test | macos-test.yml | PASS | ROOT | 2026-10-11 | 38128243808 |
| WKAppBot | Windows | csharp/bin/Release | wkappbot ask gpt test | PASS | DG-WKAppBot | 2026-10-11 | 38115491601 |
| WKAppBot.Reflex | Windows | build.cmd reflex | wkappbot eye tick + wkdoctor | PASS | harness-team | 2026-10-11 | 38115491601 |

## Test Status

- Created: 2026-10-11T000Z
- macOS workflow completed: run 38128243808 (PASS)
- WKAppBot / WKAppBot.Reflex workflow completed: run 38115491601 (PASS)
- Prior macOS run #6 (2026-10-01) completed PASS

## Evidence

- macOS workflow: https://github.com/kiexpert/wkappbot-sdk/actions/runs/38128243808
- WKAppBot workflow: https://github.com/kiexpert/WKAppBot/actions/runs/38115491601
- File created: docs/qa-release-2026-10.md
- Test command reference: gh workflow run macos-test.yml --repo kiexpert/wkappbot-sdk
