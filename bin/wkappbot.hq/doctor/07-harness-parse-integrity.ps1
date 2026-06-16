# wkdoctor check: harness PARSE-integrity -- THE most basic + most critical harness check.
# A hook script that does not PARSE is a DEAD hook = 100% enforcement loss for that family.
# This parse-checks the heart + every per-family wkharness-<family>-main.ps1 / -post.ps1 +
# wkharness-common.ps1. ANY parse error = a LOUD [x] FAIL naming the file + line + message.
#
# WHY THIS EXISTS (2026-06-17): a duplicated param( block corrupted wkharness-gemini-main.ps1
# (a re-sync doubled the param block). The gemini hook died on every call ("Missing expression
# after ','"). The doctor MISSED it -- it only emitted a cryptic HookTest 'model=PARSE_ERROR'
# WARN, not a FAIL, and buried the real meaning. This check makes a dead hook scream.
#
# Runs EARLY (ordinal 07) so a dead-hook FAIL surfaces before the wiring/canary checks that
# depend on the hook being runnable. FAIL-OPEN on the check's OWN errors; a target parse
# error is a hard FAIL (never swallowed).

try {
    $kih = 'D:\GitHub\wkappbot-kih\tools'
    $targets = @(
        'wkharness.ps1',
        'wkharness-claude-main.ps1', 'wkharness-gemini-main.ps1',
        'wkharness-codex-main.ps1',  'wkharness-agy-main.ps1',
        'wkharness-claude-post.ps1', 'wkharness-gemini-post.ps1',
        'wkharness-codex-post.ps1',  'wkharness-agy-post.ps1',
        'wkharness-common.ps1'
    )
    $bad = @()
    $checked = 0
    foreach ($name in $targets) {
        $t = Join-Path $kih $name
        if (-not (Test-Path -LiteralPath $t)) { continue }   # missing-file handled by wiring checks
        $checked++
        $errs = $null
        try {
            [void][System.Management.Automation.Language.Parser]::ParseFile($t, [ref]$null, [ref]$errs)
        } catch { $errs = $null }
        if ($errs -and $errs.Count -gt 0) {
            $bad += ('{0} L{1}: {2}' -f $name, $errs[0].Extent.StartLineNumber, $errs[0].Message)
        }
    }

    if ($bad.Count -gt 0) {
        $detail = 'DEAD HOOK -- {0} harness script(s) FAIL TO PARSE (the hook cannot run = total enforcement loss for that family): {1}' -f $bad.Count, ($bad -join ' || ')
        Add-Check 'harness-parse-integrity' 'fail' $detail
        Emit 'x' 'harness-parse-integrity' $detail
    } else {
        Add-Check 'harness-parse-integrity' 'ok' "all $checked harness hook scripts parse (heart + per-family mains/posts + common)"
        Emit 'ok' 'harness-parse-integrity' "all $checked harness hook scripts parse OK"
    }
} catch {
    Add-Check 'harness-parse-integrity' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'harness-parse-integrity' 'n/a (check error, fail-open)'
}
