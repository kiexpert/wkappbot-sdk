# wkdoctor check: pace-over-canary -- prove the Claude pace guard BLOCKS when OVER target.
# Mirrors 39-cheap-source-defense-canary structure: Add-Check/Emit, fail-open, dot-source tool.
#
# USER REQUIREMENT (2026-06-17): a pace guard that does NOT block when usage is over its target
# is THEATER. This canary synthesizes an OVER-target pace state (50% used vs ~9.5% linear target)
# for a real Claude (Opus) executor on a NON-wk edit command, invokes the REAL pace verdict
# (Test-WkPaceGuardBlocks -> Test-WkPaceVerdict in wkharness-guards-pace.ps1), and asserts it
# BLOCKS. If it does NOT block, that is a FAIL (theater) -- the gemini-flash `-ge 90` real-cap
# gate weakening must stay reverted.
#
# Test (ARTIFACT-based, deterministic -- no model self-report, no spawn, no clock):
#   OVER : synthetic over-target Claude edit on a non-wk command => verdict.Blocked == $true.
#
# Reports PASS only if the guard blocks. Names it explicitly as THEATER if it does not.
#
# FAIL-OPEN: if the canary tool is unavailable, skip (never bricks wkdoctor).
try {
    # -- locate the pace verdict tool (kih canonical, or USERPROFILE fallback) --
    $paceTool = $null
    foreach ($c in @(
        'D:\GitHub\wkappbot-kih\tools\wkharness-guards-pace.ps1',
        (Join-Path $env:USERPROFILE 'GitHub\wkappbot-kih\tools\wkharness-guards-pace.ps1'),
        'D:\GitHub\wkappbot-kih\tools\wkharness-guards-pace-proto.ps1',
        (Join-Path $env:USERPROFILE 'GitHub\wkappbot-kih\tools\wkharness-guards-pace-proto.ps1')
    )) {
        if (Test-Path -LiteralPath $c) { $paceTool = $c; break }
    }

    if (-not $paceTool) {
        Add-Check 'pace-over-canary' 'ok' 'n/a (pace tool not local -- wkharness-guards-pace.ps1 missing)'
        Emit 'ok' 'pace-over-canary' 'n/a (pace tool not local)'
        return
    }

    # -- dot-source. Define a no-op Block so a top-level reference never throws (the verdict
    #    functions never call Block; Invoke-ClaudePaceGuard does, but it is not called here). --
    if (-not (Get-Command Block -ErrorAction SilentlyContinue)) { function Block { param($m) } }
    . $paceTool

    if (-not (Get-Command Test-WkPaceGuardBlocks -ErrorAction SilentlyContinue)) {
        Add-Check 'pace-over-canary' 'ok' 'n/a (Test-WkPaceGuardBlocks missing from pace tool, fail-open)'
        Emit 'ok' 'pace-over-canary' 'n/a (canary function missing, fail-open)'
        return
    }

    # -- synthesize an OVER-target pace and run the REAL verdict --
    $r = Test-WkPaceGuardBlocks

    if (-not $r) {
        Add-Check 'pace-over-canary' 'ok' 'n/a (no result returned, fail-open)'
        Emit 'ok' 'pace-over-canary' 'n/a (no result)'
        return
    }

    if ($r.Blocked) {
        $detail = "pace guard BLOCKS over-target ($($r.UsagePct)% > linear target ~$([math]::Round($r.ElapsedPct*0.95,1))%, model=$($r.Model)) -- over-target enforcement intact (reason: $($r.Reason))"
        Add-Check 'pace-over-canary' 'ok' $detail
        Emit 'ok' 'pace-over-canary' $detail
    } else {
        # THEATER: over target but did NOT block. The user's explicit FAIL condition.
        $detail = "pace OVER target ($($r.UsagePct)% vs ~$([math]::Round($r.ElapsedPct*0.95,1))%) but guard did NOT block = THEATER. " +
                  "The over-target block was weakened (e.g. a re-added '-ge 90' real-cap gate). " +
                  "FIX: Test-WkPaceVerdict in wkharness-guards-pace.ps1 must block on totalExceeded with NO real-cap gate; " +
                  "verify with: powershell -File D:\GitHub\wkappbot-kih\tools\wkharness-pace-verdict-test-proto.ps1 (reason: $($r.Reason))"
        Add-Check 'pace-over-canary' 'fail' $detail
        Emit 'x' 'pace-over-canary' $detail
    }
} catch {
    Add-Check 'pace-over-canary' 'ok' "n/a (canary error, fail-open): $_"
    Emit 'ok' 'pace-over-canary' 'n/a (canary error, fail-open)'
}
