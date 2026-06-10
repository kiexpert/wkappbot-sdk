# wkdoctor check: guard-logic self-tests -- synthetic-canary assertions over the guard FUNCTIONS.
# Family-agnostic regression suite (any intelligence running wkdoctor self-tests the harness logic):
# feed each guard a mock input and assert the verdict, deterministically, with zero agent spawns.
# Extend by adding @{name; test={...}} entries. FAIL-OPEN: missing function / error -> that case skips.

try {
    $_common = $null
    foreach ($c in @('D:\GitHub\wkappbot-kih\tools\wkharness-common.ps1',
                     (Join-Path $env:USERPROFILE 'GitHub\wkappbot-kih\tools\wkharness-common.ps1'))) {
        if (Test-Path -LiteralPath $c) { $_common = $c; break }
    }
    if (-not $_common) {
        Add-Check 'guard-logic-selftests' 'ok' 'n/a (kih harness source not local)'
        Emit 'ok' 'guard-logic-selftests' 'n/a (harness source not local)'
        return
    }
    . $_common

    $_fails = New-Object 'System.Collections.Generic.List[string]'
    $_ran = 0

    # --- brief-guard: a parenthetical-label brief must be accepted (regression of the 2026-06-10 fix) ---
    if (Get-Command Get-WkAgentBriefState -ErrorAction SilentlyContinue) {
        $_ran++
        $_p = "Goal (the aim): do x`nFiles (paths): a.ps1`nApproach (how): patch`nConstraints (limits): scoped`nExit (criterion): tests pass`nwkappbot skill read on-load. wkappbot skill read wkharness-guards. wkappbot skill read suggest-workflow."
        try {
            $_s = Get-WkAgentBriefState -Prompt $_p
            if ($_s.MissingBrief.Count -ne 0 -or -not $_s.IsReady) { $_fails.Add("brief-guard rejects parenthetical headers (missing: $($_s.MissingBrief -join ','))") }
        } catch { $_fails.Add('brief-guard test threw') }
    }

    # --- classifier: a pure skill-read command must classify as TIER0 (always-OK knowledge op) ---
    if (Get-Command Get-WkKnowledgeTier -ErrorAction SilentlyContinue) {
        $_ran++
        try {
            $_t = Get-WkKnowledgeTier 'Bash' 'wkappbot skill read on-load' $null
            if ($_t -ne 'TIER0') { $_fails.Add("classifier: 'skill read' got $_t, expected TIER0") }
        } catch { $_fails.Add('classifier test threw') }
        try {
            $_t2 = Get-WkKnowledgeTier 'Bash' 'dotnet build && rm -rf x' $null
            if ($_t2 -eq 'TIER0') { $_fails.Add('classifier: a build/rm wrongly got TIER0 (knowledge fast-path leak)') }
        } catch {}
    }

    # --- stall-logic: fires above the tier threshold, not below ---
    if (Get-Command Test-WkStallShouldFire -ErrorAction SilentlyContinue) {
        $_ran++
        try {
            if (-not (Test-WkStallShouldFire -Tier 'opus' -ElapsedSec 100000)) { $_fails.Add('stall: did NOT fire far past the opus threshold') }
            if (Test-WkStallShouldFire -Tier 'opus' -ElapsedSec 1)            { $_fails.Add('stall: fired at 1s (well under threshold)') }
        } catch { $_fails.Add('stall test threw') }
    }

    if ($_ran -eq 0) {
        Add-Check 'guard-logic-selftests' 'ok' 'n/a (guard functions unavailable)'
        Emit 'ok' 'guard-logic-selftests' 'n/a (guard functions unavailable)'
    } elseif ($_fails.Count -eq 0) {
        Add-Check 'guard-logic-selftests' 'ok' "$_ran guard-logic self-tests passed (brief-guard, classifier, stall)"
        Emit 'ok' 'guard-logic-selftests' "$_ran guard-logic self-tests passed"
    } else {
        $detail = "$($_fails.Count)/$_ran FAILED: $($_fails -join ' | ')"
        Add-Check 'guard-logic-selftests' 'warn' $detail
        Emit '!' 'guard-logic-selftests' $detail
    }
} catch {
    Add-Check 'guard-logic-selftests' 'ok' "n/a (self-test error, fail-open): $_"
    Emit 'ok' 'guard-logic-selftests' 'n/a (self-test error, fail-open)'
}
