# wkdoctor check: per-family intelligence pace summary
# Emits one line per AI family with usage source present:
#   Claude  -- pace.json + usage_anchor_claude.json
#   Codex   -- codex_pace.json + codex_balance.json
#   Gemini  -- gemini_measured_limits.json + wkgemini_usage.jsonl
# Families with no source file = skip (not a fail -- just not active on this machine).
# FAIL-OPEN: any parse error produces 'n/a' for that family.
# Runs within wkdoctor context. $binDir, $repoRoot are already defined by wkdoctor.ps1.

$_harnessHome = if ($env:WKHARNESS_HOME) { $env:WKHARNESS_HOME } else { "$env:USERPROFILE\.claude\wkharness" }

# ── Claude ────────────────────────────────────────────────────────────────────────────────────
$_claudePaceFile  = Join-Path $_harnessHome 'pace.json'
$_claudeAnchorFile = Join-Path $_harnessHome 'usage_anchor_claude.json'
if ((Test-Path $_claudePaceFile) -or (Test-Path $_claudeAnchorFile)) {
    try {
        $cTotal   = $null; $cTarget  = $null; $cSonnet  = $null
        $cSource  = ''; $cAnchorAge = $null; $cFlag = ''

        # Prefer anchor (real /usage) over estimate
        if (Test-Path $_claudeAnchorFile) {
            try {
                $a = Get-Content $_claudeAnchorFile -Raw | ConvertFrom-Json
                if ($a.PSObject.Properties['total_pct']) {
                    $cTotal  = [math]::Round([double]$a.total_pct, 1)
                    $cSonnet = if ($a.PSObject.Properties['sonnet_pct']) { [math]::Round([double]$a.sonnet_pct, 1) } else { $null }
                    $capAt   = if ($a.PSObject.Properties['captured_at']) { [DateTimeOffset]::Parse($a.captured_at) } else { [DateTimeOffset]::MinValue }
                    $cAnchorAge = [math]::Round(([DateTimeOffset]::UtcNow - $capAt).TotalMinutes, 0)
                    $src = if ($a.PSObject.Properties['source']) { $a.source } else { 'claude /usage' }
                    $cSource = "anchor($src  age=$($cAnchorAge)m)"
                }
            } catch {}
        }
        if ($null -eq $cTotal -and (Test-Path $_claudePaceFile)) {
            try {
                $p = Get-Content $_claudePaceFile -Raw | ConvertFrom-Json
                $cTotal  = if ($p.PSObject.Properties['totalPct'])    { [math]::Round([double]$p.totalPct, 1) }  else { 0 }
                $cSonnet = if ($p.PSObject.Properties['sonnetPct'])   { [math]::Round([double]$p.sonnetPct, 1) } else { 0 }
                $cTarget = if ($p.PSObject.Properties['targetElapsed']) { [math]::Round([double]$p.targetElapsed, 1) } else { $null }
                $cSource = 'pace.json (estimate)'
            } catch {}
        }
        if ($null -eq $cTarget -and (Test-Path $_claudePaceFile)) {
            try {
                $pt = Get-Content $_claudePaceFile -Raw | ConvertFrom-Json
                $cTarget = if ($pt.PSObject.Properties['targetElapsed']) { [math]::Round([double]$pt.targetElapsed, 1) } else { $null }
            } catch {}
        }

        $cTotalStr  = if ($null -ne $cTotal)  { "$($cTotal)%" }  else { 'n/a' }
        $cTargetStr = if ($null -ne $cTarget) { "target=$($cTarget)%" } else { '' }
        $cSonnetStr = if ($null -ne $cSonnet) { "sonnet=$($cSonnet)%" } else { '' }
        if ($null -ne $cTotal -and $null -ne $cTarget -and $cTotal -gt $cTarget) { $cFlag = ' [OVER]' }
        elseif ($null -ne $cTotal -and $null -ne $cTarget -and ($cTotal -gt $cTarget * 0.9)) { $cFlag = ' [WARN]' }
        $parts = @($cTotalStr, $cTargetStr, $cSonnetStr) | Where-Object { $_ }
        $detail = "$($parts -join '  ')$cFlag  [$cSource]"
        $status = if ($cFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Claude' $status $detail
        Emit $status 'Family pace: Claude' $detail
    } catch {
        Add-Check 'Family pace: Claude' 'warn' "parse error: $_"
        Emit '!' 'Family pace: Claude' "parse error"
    }
}

# ── Codex ─────────────────────────────────────────────────────────────────────────────────────
$_codexPaceFile    = Join-Path $_harnessHome 'codex_pace.json'
$_codexBalanceFile = Join-Path $_harnessHome 'codex_balance.json'
if ((Test-Path $_codexPaceFile) -or (Test-Path $_codexBalanceFile)) {
    try {
        $xPct = $null; $xTokM = $null; $xLimitM = $null; $xFlag = ''; $xSrc = ''

        if (Test-Path $_codexPaceFile) {
            try {
                $cp = Get-Content $_codexPaceFile -Raw | ConvertFrom-Json
                $xPct   = if ($cp.PSObject.Properties['pct'])    { [math]::Round([double]$cp.pct, 1) }
                           elseif ($cp.PSObject.Properties['usedPct']) { [math]::Round([double]$cp.usedPct, 1) }
                           else { $null }
                $xTokM  = if ($cp.PSObject.Properties['tokM'])   { [math]::Round([double]$cp.tokM, 2) } else { $null }
                $xLimitM = if ($cp.PSObject.Properties['limitM']) { [math]::Round([double]$cp.limitM, 0) } else { $null }
                $xSrc = 'codex_pace.json'
            } catch {}
        }
        # Balance gives remaining% which is more authoritative if present
        $xWeeklyRem = $null; $xWeeklyReset = $null
        if (Test-Path $_codexBalanceFile) {
            try {
                $cb = Get-Content $_codexBalanceFile -Raw | ConvertFrom-Json
                if ($cb.PSObject.Properties['weekly']) {
                    $xWeeklyRem   = if ($null -ne $cb.weekly.remainingPct) { [int]$cb.weekly.remainingPct } else { $null }
                    $xWeeklyReset = if ($cb.weekly.resetAt) { $cb.weekly.resetAt } else { '' }
                    $xSrc = 'codex_balance.json'
                }
            } catch {}
        }

        $usedPct = if ($null -ne $xWeeklyRem) { 100 - $xWeeklyRem } else { $xPct }
        $usedStr = if ($null -ne $usedPct) { "$($usedPct)% used" } else { 'n/a' }
        $tokStr  = if ($null -ne $xTokM)   { "${xTokM}M tokens" } else { '' }
        $limStr  = if ($null -ne $xLimitM) { "limit=${xLimitM}M" } else { '' }
        $remStr  = if ($null -ne $xWeeklyRem)   { "remaining=$($xWeeklyRem)%" } else { '' }
        $rstStr  = if ($xWeeklyReset)       { "reset=$xWeeklyReset" } else { '' }
        if ($null -ne $usedPct -and $usedPct -gt 95) { $xFlag = ' [OVER]' }
        elseif ($null -ne $usedPct -and $usedPct -gt 80) { $xFlag = ' [WARN]' }
        $parts = @($usedStr, $tokStr, $limStr, $remStr, $rstStr) | Where-Object { $_ }
        $detail = "$($parts -join '  ')$xFlag  [$xSrc]"
        $status = if ($xFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Codex' $status $detail
        Emit $status 'Family pace: Codex' $detail
    } catch {
        Add-Check 'Family pace: Codex' 'warn' "parse error: $_"
        Emit '!' 'Family pace: Codex' "parse error"
    }
}

# ── Gemini (AGY) ──────────────────────────────────────────────────────────────────────────────
$_geminiLimFile  = Join-Path $_harnessHome 'gemini_measured_limits.json'
$_geminiUsageLog = "$env:USERPROFILE\.gemini\wkgemini_usage.jsonl"
if ((Test-Path $_geminiLimFile) -or (Test-Path $_geminiUsageLog)) {
    try {
        $gWeeklyUsed = [double]0; $gWeeklyLimit = [double]400e6
        $gFlag = ''; $gSrc = ''; $gTargetPct = $null

        # Load measured limits
        if (Test-Path $_geminiLimFile) {
            try {
                $gl = Get-Content $_geminiLimFile -Raw | ConvertFrom-Json
                if ($gl.PSObject.Properties['WEEK'] -and [double]$gl.WEEK -gt 0) { $gWeeklyLimit = [double]$gl.WEEK }
                $gSrc = 'gemini_measured_limits.json'
            } catch {}
        }

        # Compute weekly reset (Korea Standard Time, Thu 07:00)
        $seoulTz   = [TimeZoneInfo]::FindSystemTimeZoneById("Korea Standard Time")
        $nowUtc    = [DateTimeOffset]::UtcNow
        $nowSeoul  = [TimeZoneInfo]::ConvertTime($nowUtc, $seoulTz)
        $daysFromThur = (([int]$nowSeoul.DayOfWeek - [int][DayOfWeek]::Thursday) + 7) % 7
        if ($daysFromThur -eq 0 -and $nowSeoul.Hour -lt 7) { $daysFromThur = 7 }
        $resetSeoul = $nowSeoul.Date.AddDays(-$daysFromThur).AddHours(7)
        $resetUtc   = [DateTimeOffset]([TimeZoneInfo]::ConvertTimeToUtc([DateTime]$resetSeoul, $seoulTz))
        $gTargetPct = [math]::Round(($nowUtc - $resetUtc).TotalHours / 168.0 * 100.0, 1)

        # Sum weekly tokens from log
        if (Test-Path $_geminiUsageLog) {
            $gSrc = 'wkgemini_usage.jsonl'
            foreach ($line in (Get-Content $_geminiUsageLog -Encoding UTF8 -ErrorAction SilentlyContinue)) {
                if (-not $line.Trim()) { continue }
                try {
                    $m = $line | ConvertFrom-Json
                    if ($m.ts -and [DateTimeOffset]::Parse($m.ts) -ge $resetUtc -and -not ($m.quota_exhausted)) {
                        $ti = if ($m.tokens_in)     { [double]$m.tokens_in }     else { 0 }
                        $to = if ($m.tokens_out)    { [double]$m.tokens_out }    else { 0 }
                        $tc = if ($m.tokens_cached) { [double]$m.tokens_cached } else { 0 }
                        $w  = if ($m.model -like "*pro*") { 1.0 } else { 0.2 }
                        $gWeeklyUsed += ($ti + $to + ($tc * 0.1)) * $w
                    }
                } catch {}
            }
        }

        $gPct    = [math]::Round($gWeeklyUsed / $gWeeklyLimit * 100.0, 1)
        $gUsedM  = [math]::Round($gWeeklyUsed / 1e6, 2)
        $gLimM   = [math]::Round($gWeeklyLimit / 1e6, 2)
        if ($gPct -gt $gTargetPct) { $gFlag = ' [OVER]' }
        elseif ($gPct -gt $gTargetPct * 0.9) { $gFlag = ' [WARN]' }
        $detail = "$($gPct)%  target=$($gTargetPct)%$gFlag  ${gUsedM}M/${gLimM}M  [$gSrc]"
        $status = if ($gFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Gemini' $status $detail
        Emit $status 'Family pace: Gemini' $detail
    } catch {
        Add-Check 'Family pace: Gemini' 'warn' "parse error: $_"
        Emit '!' 'Family pace: Gemini' "parse error"
    }
}
