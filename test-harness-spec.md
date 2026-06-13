# Harness Guard Verification Spec

Create and run a PowerShell test harness at `D:/GitHub/wkappbot-sdk/test-harness-verify.ps1`.

## Requirements
- Use ONLY `[System.Diagnostics.ProcessStartInfo]` to spawn child processes.
- NEVER use nested `pwsh -Command` / `powershell -Command` to invoke the harness.
- For each case: set FileName/Arguments/Environment, redirect stdout+stderr, capture ExitCode.
- A case counts as "blocked" if ExitCode != 0 OR the expected tag appears in stderr.

## Test cases

| # | FileName | Arguments | Env | Expected |
|---|----------|-----------|-----|----------|
| 1 | wkappbot-core.exe | windows | MSYS_NO_PATHCONV=1 | BLOCK (exit2), stderr has `[env-prefix-noise]` |
| 2 | git | status | (none) | NOT blocked, no noise tag |
| 3 | wkappbot-core.exe | windows | (none) | NOT blocked |
| 4 | cmd | (forward-slash c) wkappbot windows | (none) | BLOCK, stderr has `[cmd-wrap-noise]` |
| 5 | wkappbot.cmd | windows | (none) | BLOCK, stderr has `[cmd-wrapper-noise]` |

For case 4, Arguments is the string: two forward slashes, then `c wkappbot windows`.

## Self-test (case 6)
- FileName: powershell.exe
- Arguments: `-NoProfile -ExecutionPolicy Bypass -File "D:\GitHub\wkappbot-kih\tools\wkharness.ps1" -Test`
- Env: WKHARNESS_SELFTEST=1
- Report PASS/FAIL count parsed from its output.

## Output
- Print a summary table: Case | ExitCode | TagFound | PASS/FAIL.
- WorkingDirectory for spawned children = D:/GitHub/wkappbot-sdk.

## Execute
After writing the script, run it via:
`powershell.exe -NoProfile -ExecutionPolicy Bypass -File D:/GitHub/wkappbot-sdk/test-harness-verify.ps1`
Print the full output.

Exit criterion: script ran and printed the summary table with all 6 results.
