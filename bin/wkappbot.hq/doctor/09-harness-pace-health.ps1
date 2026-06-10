# wkdoctor check: harness pace health
# Detects the four bad-states that caused the 2026-06-10 176pct false PACE-BRICK:
#   (a) pace.json says over-target while the real /usage anchor says low  -> auto-recover
#   (b) SAFETY short-circuit missing from wkharness-guards-pace.ps1       -> fail
#   (c) TIER0/1/2 ladder missing from wkharness-guards.ps1                -> fail
#   (d) stall-guard exemption block missing from wkharness-guards-infra.ps1 -> warn
# FAIL-OPEN: missing files / parse errors always produce warn (never crash wkdoctor).
# Runs within wkdoctor context. $binDir, $repoRoot are already defined by wkdoctor.ps1.

$_harnessDir = try {
    # Try well-known canonical locations in priority order (fail-open: last fallback)
    $gitHubDir = try { Split-Path $repoRoot -Parent } catch { 'D:\GitHub' }
    $candidates = @(
        "$gitHubDir\wkappbot-kih\tools",
        "$gitHubDir\personal-docs\tools"
    )
    # Also try resolving wkharness.ps1 symlink if present
    $wkhSym = Join-Path $gitHubDir 'wkharness.ps1'
    if (Test-Path $wkhSym) {
        $item = Get-Item $wkhSym -ErrorAction SilentlyContinue
        if ($item.LinkType -eq 'SymbolicLink' -and $item.Target) { $candidates = @(Split-Path $item.Target -Parent) + $candidates }
    }
    $found = $candidates | Where-Object { Test-Path (Join-Path $_ 'wkharness-guards-pace.ps1') } | Select-Object -First 1
    if ($found) { $found } else { $candidates[0] }
} catch { 'D:\GitHub\wkappbot-kih\tools' }

$_paceFile    = Join-Path $_harnessDir 'wkharness-guards-pace.ps1'
$_infraFile   = Join-Path $_harnessDir 'wkharness-guards-infra.ps1'
$_guardsFile  = Join-Path $_harnessDir 'wkharness-guards.ps1'
$_harnessHome = if ($env:WKHARNESS_HOME) { $env:WKHARNESS_HOME } else { "$env:USERPROFILE\.claude\wkharness" }
$_anchorFile  = Join-Path $_harnessHome 'usage_anchor_claude.json'
$_paceJson    = Join-Path $_harnessHome 'pace.json'

# ── (a) FALSE OVER-TARGET: pace.json says high while anchor says low ─────────────────────────
$_paceHealthDetail = 'n/a (pace.json absent)'
if (Test-Path $_paceJson) {
    try {
        $pj = Get-Content $_paceJson -Raw | ConvertFrom-Json
        $pjTotal    = if ($pj.PSObject.Properties['totalPct'])    { [double]$pj.totalPct    } else { 0 }
        $pjTarget   = if ($pj.PSObject.Properties['targetElapsed']) { [double]$pj.targetElapsed } else { 50 }
        $pjExceeded = ($pjTotal -gt $pjTarget)

        if ($pjExceeded) {
            # Check anchor
            $anchorOk = $false; $anchorTotal = -1
            if (Test-Path $_anchorFile) {
                try {
                    $a = Get-Content $_anchorFile -Raw | ConvertFrom-Json
                    if ($a.PSObject.Properties['total_pct']) {
                        $anchorTotal = [double]$a.total_pct
                        $capturedAt  = if ($a.PSObject.Properties['captured_at']) { [DateTimeOffset]::Parse($a.captured_at) } else { [DateTimeOffset]::MinValue }
                        $ageMin      = [math]::Round(([DateTimeOffset]::UtcNow - $capturedAt).TotalMinutes, 1)
                        if ($ageMin -le 120 -and $anchorTotal -lt ($pjTarget * 0.5)) {
                            $anchorOk = $true
                        }
                    }
                } catch {}
            }
            if ($anchorOk) {
                $msg = "FALSE HIGH: pace.json=$($pjTotal)% but anchor=$($anchorTotal)% -- AUTO-RECOVERING"
                Add-Check 'Pace: false-high' 'warn' $msg
                Emit '!' 'Pace: false-high' $msg
                # Recovery: run claude-usage-capture.ps1 to re-anchor
                $_captureScript = Join-Path $_harnessDir 'claude-usage-capture.ps1'
                if (Test-Path $_captureScript) {
                    try {
                        $job = Start-Job -ScriptBlock { param($s) & powershell.exe -NoProfile -ExecutionPolicy Bypass -File $s } -ArgumentList $_captureScript
                        $null = Wait-Job $job -Timeout 20
                        Remove-Job $job -Force -ErrorAction SilentlyContinue
                        Add-Check 'Pace: false-high' 'ok' "re-anchored (capture script ran)"
                        Emit 'ok' 'Pace: false-high' 're-anchored: claude-usage-capture.ps1 ran'
                    } catch {
                        Add-Check 'Pace: false-high' 'warn' "capture script failed: $_"
                        Emit '!' 'Pace: false-high' "capture failed -- run manually: claude-usage-capture.ps1"
                    }
                } else {
                    Add-Check 'Pace: false-high' 'warn' "capture script not found at $_captureScript"
                    Emit '!' 'Pace: false-high' "manual fix: copy anchor value to $($_anchorFile)"
                }
            } else {
                $anchorStr = if ($anchorTotal -ge 0) { "anchor=$($anchorTotal)%" } else { "anchor absent" }
                Add-Check 'Pace: over-target' 'warn' "pace=$($pjTotal)% > target=$($pjTarget)%  ($anchorStr)"
                Emit '!' 'Pace: over-target' "pace=$($pjTotal)% > target=$($pjTarget)%  ($anchorStr)"
            }
        } else {
            Add-Check 'Pace: over-target' 'ok' "pace=$($pjTotal)%  target=$($pjTarget)%  [OK]"
            Emit 'ok' 'Pace: over-target' "pace=$($pjTotal)%  target=$($pjTarget)%  [OK]"
        }
    } catch {
        Add-Check 'Pace: over-target' 'warn' "pace.json unreadable: $_"
        Emit '!' 'Pace: over-target' 'pace.json unreadable'
    }
} else {
    Add-Check 'Pace: over-target' 'ok' 'pace.json absent -- no pace brick possible'
    Emit 'ok' 'Pace: over-target' 'pace.json absent'
}

# ── (b) SAFETY short-circuit in wkharness-guards-pace.ps1 ────────────────────────────────────
if (-not (Test-Path $_paceFile)) {
    Add-Check 'Pace: safety-bridge' 'warn' "pace file not found ($($_paceFile))"
    Emit '!' 'Pace: safety-bridge' "file absent -- harness not installed?"
} else {
    $paceContent = try { Get-Content $_paceFile -Raw } catch { '' }
    if ($paceContent -match 'SAFETY short-circuit' -and $paceContent -match 'Get-ClaudeUsageAnchor') {
        Add-Check 'Pace: safety-bridge' 'ok' 'SAFETY short-circuit + Get-ClaudeUsageAnchor present'
        Emit 'ok' 'Pace: safety-bridge' 'SAFETY short-circuit present'
    } else {
        $missing = @()
        if ($paceContent -notmatch 'SAFETY short-circuit') { $missing += 'SAFETY short-circuit comment' }
        if ($paceContent -notmatch 'Get-ClaudeUsageAnchor') { $missing += 'Get-ClaudeUsageAnchor call' }
        Add-Check 'Pace: safety-bridge' 'fail' "missing: $($missing -join ', ')"
        Emit 'fail' 'Pace: safety-bridge' "MISSING: $($missing -join ', ') -- re-add safety bridge to $($_paceFile)"
    }
}

# ── (c) TIER0/1/2 ladder in wkharness-guards.ps1 ─────────────────────────────────────────────
if (-not (Test-Path $_guardsFile)) {
    Add-Check 'Pace: tier-ladder' 'warn' "guards file not found ($($_guardsFile))"
    Emit '!' 'Pace: tier-ladder' "guards file absent"
} else {
    $guardsContent = try { Get-Content $_guardsFile -Raw } catch { '' }
    $hasTier0 = $guardsContent -match 'Test-WkIsKnowledgeOp'
    $hasTier1 = $guardsContent -match 'Test-WkIsHarnessSourceEdit'
    $hasTier2 = $guardsContent -match 'TIER0|TIER1|TIER2'
    if ($hasTier0 -and $hasTier1 -and $hasTier2) {
        Add-Check 'Pace: tier-ladder' 'ok' 'TIER0/1/2 (Test-WkIsKnowledgeOp + Test-WkIsHarnessSourceEdit) present'
        Emit 'ok' 'Pace: tier-ladder' 'TIER0/1/2 ladder present'
    } else {
        $missing = @()
        if (-not $hasTier0) { $missing += 'Test-WkIsKnowledgeOp' }
        if (-not $hasTier1) { $missing += 'Test-WkIsHarnessSourceEdit' }
        if (-not $hasTier2) { $missing += 'TIER0/1/2 constants' }
        Add-Check 'Pace: tier-ladder' 'fail' "missing: $($missing -join ', ')"
        Emit 'fail' 'Pace: tier-ladder' "MISSING: $($missing -join ', ') -- restore from harness-guard-exemption-ladder-spec"
    }
}

# ── (d) stall exemptions in wkharness-guards-infra.ps1 ───────────────────────────────────────
if (-not (Test-Path $_infraFile)) {
    Add-Check 'Pace: stall-exempts' 'warn' "infra file not found ($($_infraFile))"
    Emit '!' 'Pace: stall-exempts' "infra file absent"
} else {
    $infraContent = try { Get-Content $_infraFile -Raw } catch { '' }
    $hasStallExempt = ($infraContent -match '_ewStallExempt') -or ($infraContent -match 'TIER0.*stall') -or ($infraContent -match 'stall.*exempt')
    if ($hasStallExempt) {
        Add-Check 'Pace: stall-exempts' 'ok' '_ewStallExempt block present'
        Emit 'ok' 'Pace: stall-exempts' 'stall exemption block present'
    } else {
        Add-Check 'Pace: stall-exempts' 'warn' '_ewStallExempt block not found -- stall guard may over-fire'
        Emit '!' 'Pace: stall-exempts' 'stall exemption block missing -- check wkharness-guards-infra.ps1'
    }
}
