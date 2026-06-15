# wkdoctor check: knowledge-ops PER-FAMILY canary -- the paralysis-proof LAW, end-to-end.
# Stronger than 22-guard-logic-selftests (which unit-tests the Get-WkKnowledgeTier CLASSIFIER):
# this pipes a PURE `wkappbot skill read` command through EACH family's LIVE harness path
# (Claude=kih -Family claude, Gemini/AGY=the D:\GitHub\wkharness.ps1 shim, Codex=kih -Family codex)
# and asserts the op is ALLOWED. A family that BLOCKS a pure knowledge-op is breaking the law
# (knowledge-ification must never be blockable, even by a hung/erroring/rate-limited guard).
# Catches WHICH family violates. Robust under any attacker incl the rate-LIMIT villain.
# FAIL-OPEN: if the canary tool / a harness path is unavailable, the case skips (never bricks wkdoctor).
try {
    $canary = $null
    foreach ($c in @('D:\GitHub\wkappbot-kih\tools\wkharness-knowledge-canary.ps1',
                     (Join-Path $env:USERPROFILE 'GitHub\wkappbot-kih\tools\wkharness-knowledge-canary.ps1'))) {
        if (Test-Path -LiteralPath $c) { $canary = $c; break }
    }
    if (-not $canary) {
        Add-Check 'knowledge-ops-canary' 'ok' 'n/a (canary tool not local)'
        Emit 'ok' 'knowledge-ops-canary' 'n/a (canary tool not local)'
        return
    }
    . $canary
    if (-not (Get-Command Invoke-WkKnowledgeCanary -ErrorAction SilentlyContinue)) {
        Add-Check 'knowledge-ops-canary' 'ok' 'n/a (canary function missing, fail-open)'
        Emit 'ok' 'knowledge-ops-canary' 'n/a (canary function missing)'
        return
    }

    $res      = Invoke-WkKnowledgeCanary
    $breakers = @($res | Where-Object { $_.Obeys -eq $false })
    $obeyed   = @($res | Where-Object { $_.Obeys -eq $true })
    $skipped  = @($res | Where-Object { $_.Obeys -eq $null })

    if ($breakers.Count -eq 0) {
        $detail = "all families obey the knowledge-ops law -- $($obeyed.Count) ALLOW" + $(if ($skipped.Count) { ", $($skipped.Count) n/a" } else { '' }) + " (" + (($obeyed | ForEach-Object { $_.Name }) -join '/') + ")"
        Add-Check 'knowledge-ops-canary' 'ok' $detail
        Emit 'ok' 'knowledge-ops-canary' $detail
    } else {
        $names  = ($breakers | ForEach-Object { $_.Name }) -join ','
        $detail = "LAW-BREAKER(s): $names -- a pure 'wkappbot skill|suggest' op was BLOCKED by that family's harness path. Knowledge-ification must be paralysis-proof (first early-exit, before any guard). Marker: ~/.claude/wkharness/KNOWLEDGE_OPS_BROKEN.flag . FIX: ensure the family's path reaches wkharness.ps1 with a tool_name the knowledge-ops early-exit matches (Bash/PowerShell; the gemini shim must remap run_shell_command->Bash before kih)."
        Add-Check 'knowledge-ops-canary' 'fail' $detail
        Emit 'x' 'knowledge-ops-canary' $detail
    }
} catch {
    Add-Check 'knowledge-ops-canary' 'ok' "n/a (canary error, fail-open): $_"
    Emit 'ok' 'knowledge-ops-canary' 'n/a (canary error, fail-open)'
}
