# wkdoctor check: sandbox-integrity -- deterministic self-test of the cheap-agent harness-source sandbox.
# Born from the agy/Flash QA-canary experiment (2026-06-10): instead of spawning a real agent (heavy,
# passthrough output, unobservable), call the gate functions with MOCK contexts and assert the
# expected behavior. Catches the cross-family GAP (a primary cheap session like Flash/Gemini is NOT
# sandboxed because the cheap-check keys on the Claude-spawn marker, not the model tier) + future regressions.
# FAIL-OPEN: any error -> n/a.

try {
    # Locate + dot-source the kih harness common (gate functions live there).
    $_common = $null
    foreach ($c in @(
        'D:\GitHub\wkappbot-kih\tools\wkharness-common.ps1',
        (Join-Path $env:USERPROFILE 'GitHub\wkappbot-kih\tools\wkharness-common.ps1')
    )) { if (Test-Path -LiteralPath $c) { $_common = $c; break } }

    if (-not $_common) {
        Add-Check 'sandbox-integrity' 'ok' 'n/a (kih wkharness-common.ps1 not found)'
        Emit 'ok' 'sandbox-integrity' 'n/a (harness source not local)'
        return
    }

    . $_common
    if (-not (Get-Command Test-WkIsCheapAgentSession -ErrorAction SilentlyContinue)) {
        Add-Check 'sandbox-integrity' 'ok' 'n/a (gate functions unavailable)'
        Emit 'ok' 'sandbox-integrity' 'n/a (gate functions unavailable)'
        return
    }

    # Save the real env so the mocks never leak into the running session.
    $_savedAgent  = [System.Environment]::GetEnvironmentVariable('WKHARNESS_AGENT_MODEL')
    $_savedGemini = $env:WKHARNESS_IS_GEMINI
    $_findings = New-Object 'System.Collections.Generic.List[string]'
    $_gap = $false
    try {
        # MOCK 1 -- ROOT (no spawn marker): expect NOT-cheap (may edit canonical).
        [System.Environment]::SetEnvironmentVariable('WKHARNESS_AGENT_MODEL', $null)
        $env:WKHARNESS_IS_GEMINI = $null
        $rootCheap = Test-WkIsCheapAgentSession
        if ($rootCheap) { $_findings.Add('ROOT wrongly flagged cheap') }

        # MOCK 2 -- Claude-spawned cheap subagent: expect cheap (sandbox-restricted).
        [System.Environment]::SetEnvironmentVariable('WKHARNESS_AGENT_MODEL', 'haiku')
        $subCheap = Test-WkIsCheapAgentSession
        if (-not $subCheap) { $_findings.Add('cheap subagent NOT sandboxed') }

        # MOCK 3 -- THE GAP: a PRIMARY cheap session (Flash/Gemini), no spawn marker.
        # A cheap MODEL editing canonical harness source SHOULD be sandboxed, but the gate keys on
        # the Claude-spawn marker, so a top-level Flash is treated as ROOT and can edit canonical.
        [System.Environment]::SetEnvironmentVariable('WKHARNESS_AGENT_MODEL', $null)
        $env:WKHARNESS_IS_GEMINI = '1'
        $flashCheap = Test-WkIsCheapAgentSession
        if (-not $flashCheap) { $_gap = $true; $_findings.Add('GAP: primary cheap session (Flash/Gemini) is treated as ROOT -- NOT sandboxed for canonical harness edits') }
    } finally {
        [System.Environment]::SetEnvironmentVariable('WKHARNESS_AGENT_MODEL', $_savedAgent)
        $env:WKHARNESS_IS_GEMINI = $_savedGemini
    }

    if ($_findings.Count -eq 0) {
        $detail = 'sandbox gate correct: ROOT allowed, cheap subagent sandboxed, primary cheap sandboxed'
        Add-Check 'sandbox-integrity' 'ok' $detail
        Emit 'ok' 'sandbox-integrity' $detail
    } else {
        $detail = "$($_findings -join ' | ')  FIX: Test-WkIsCheapAgentSession should key on the MODEL TIER (haiku/flash/gemini-flash/codex-mini = sandbox; opus/sonnet/gpt = canonical), not only the spawn marker -- so a primary Flash/Gemini is also gated."
        $status = if ($_gap) { 'warn' } else { 'warn' }
        Add-Check 'sandbox-integrity' $status $detail
        Emit '!' 'sandbox-integrity' $detail
    }
} catch {
    Add-Check 'sandbox-integrity' 'ok' "n/a (self-test error, fail-open): $_"
    Emit 'ok' 'sandbox-integrity' 'n/a (self-test error, fail-open)'
}
