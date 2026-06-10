# wkdoctor check: per-family intelligence pace summary
# Emits one line per AI family with usage source present:
#   Claude  -- pace.json + usage_anchor_claude.json (weekly + monthly-accumulated)
#   Codex   -- codex_pace.json + codex_balance.json (5h + weekly, with target%)
#   Gemini  -- gemini_measured_limits.json + wkgemini_usage.jsonl (DAILY cycle per model)
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
        $cMonthlyPct = $null; $cMonthlyAvailable = $false

        # Always read pace.json first for targetElapsed and monthly data
        if (Test-Path $_claudePaceFile) {
            try {
                $p = Get-Content $_claudePaceFile -Raw | ConvertFrom-Json
                $cTarget = if ($p.PSObject.Properties['targetElapsed']) { [math]::Round([double]$p.targetElapsed, 1) } else { $null }
                # Monthly accumulation from pace.json
                if ($p.PSObject.Properties['monthlyPct']) {
                    $cMonthlyPct = [math]::Round([double]$p.monthlyPct, 1)
                }
                if ($p.PSObject.Properties['monthlyAvailable']) {
                    $cMonthlyAvailable = [bool]$p.monthlyAvailable
                }
            } catch {}
        }

        # Prefer anchor (real /usage) over estimate for current usage%
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
        # Fallback to pace.json for usage% if anchor missing
        if ($null -eq $cTotal -and (Test-Path $_claudePaceFile)) {
            try {
                $p2 = Get-Content $_claudePaceFile -Raw | ConvertFrom-Json
                $cTotal  = if ($p2.PSObject.Properties['totalPct'])  { [math]::Round([double]$p2.totalPct, 1) }  else { 0 }
                $cSonnet = if ($p2.PSObject.Properties['sonnetPct']) { [math]::Round([double]$p2.sonnetPct, 1) } else { 0 }
                if (-not $cSource) { $cSource = 'pace.json (estimate)' }
            } catch {}
        }

        # Build health flag for weekly
        if ($null -ne $cTotal -and $null -ne $cTarget -and $cTotal -gt $cTarget) { $cFlag = ' [OVER]' }
        elseif ($null -ne $cTotal -and $null -ne $cTarget -and ($cTotal -gt $cTarget * 0.9)) { $cFlag = ' [WARN]' }

        $cTotalStr  = if ($null -ne $cTotal)  { "$($cTotal)%" }  else { 'n/a' }
        $cTargetStr = if ($null -ne $cTarget) { "target=$($cTarget)%" } else { 'target=n/a' }
        $cSonnetStr = if ($null -ne $cSonnet) { "sonnet=$($cSonnet)%" } else { '' }
        $parts = @($cTotalStr, $cTargetStr, $cSonnetStr) | Where-Object { $_ }
        $detail = "$($parts -join '  ')$cFlag  [$cSource]"
        $status = if ($cFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Claude weekly' $status $detail
        Emit $status 'Family pace: Claude weekly' $detail

        # Claude monthly -- no official cap, show accumulated usage as informational
        $monthlyDetail = $null
        if ($cMonthlyAvailable -and $null -ne $cMonthlyPct) {
            $monthlyDetail = "monthly: $($cMonthlyPct)%  (no official cap -- informational)  [pace.json]"
        } elseif ($null -ne $cMonthlyPct) {
            # monthlyAvailable=false means harness could not measure it -- show best-effort
            if ($cMonthlyPct -gt 0) {
                $monthlyDetail = "monthly: $($cMonthlyPct)%  (no official cap -- informational)  [pace.json est]"
            } else {
                $monthlyDetail = "monthly: tracking (no official cap -- data accumulating)  [pace.json]"
            }
        } else {
            $monthlyDetail = "monthly: tracking (no cap)  [pace.json missing monthlyPct]"
        }
        Add-Check 'Family pace: Claude monthly' 'ok' $monthlyDetail
        Emit 'ok' 'Family pace: Claude monthly' $monthlyDetail
    } catch {
        Add-Check 'Family pace: Claude weekly' 'warn' "parse error: $_"
        Emit '!' 'Family pace: Claude weekly' "parse error"
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

        # 5-hour window target: fraction of 5h elapsed since last reset
        $xFiveHourTarget = $null; $xWeeklyRem = $null; $xWeeklyReset = $null; $xFiveHourRem = $null
        if (Test-Path $_codexBalanceFile) {
            try {
                $cb = Get-Content $_codexBalanceFile -Raw | ConvertFrom-Json
                if ($cb.PSObject.Properties['weekly']) {
                    $xWeeklyRem   = if ($null -ne $cb.weekly.remainingPct) { [int]$cb.weekly.remainingPct } else { $null }
                    $xWeeklyReset = if ($cb.weekly.resetAt) { $cb.weekly.resetAt } else { '' }
                    $xSrc = 'codex_balance.json'
                }
                if ($cb.PSObject.Properties['fiveHour']) {
                    $xFiveHourRem = if ($null -ne $cb.fiveHour.remainingPct) { [int]$cb.fiveHour.remainingPct } else { $null }
                    # Compute 5h target from resetAt
                    if ($cb.fiveHour.PSObject.Properties['resetAt'] -and $cb.fiveHour.resetAt) {
                        try {
                            $fhReset = [DateTimeOffset]::Parse($cb.fiveHour.resetAt)
                            $fhWindowH = if ($cb.fiveHour.PSObject.Properties['windowHours']) { [double]$cb.fiveHour.windowHours } else { 5.0 }
                            $fhStart = $fhReset.AddHours(-$fhWindowH)
                            $elapsed = ([DateTimeOffset]::UtcNow - $fhStart).TotalHours
                            if ($elapsed -lt 0) { $elapsed = 0 }
                            if ($elapsed -gt $fhWindowH) { $elapsed = $fhWindowH }
                            $xFiveHourTarget = [math]::Round($elapsed / $fhWindowH * 100.0, 1)
                        } catch {}
                    }
                }
            } catch {}
        }

        # 5-hour line
        $xFiveHourUsed = if ($null -ne $xFiveHourRem) { 100 - $xFiveHourRem } else { $xPct }
        if ($null -ne $xFiveHourUsed) {
            $xFhFlag = ''
            if ($null -ne $xFiveHourTarget) {
                if ($xFiveHourUsed -gt $xFiveHourTarget) { $xFhFlag = ' [OVER]' }
                elseif ($xFiveHourUsed -gt $xFiveHourTarget * 0.9) { $xFhFlag = ' [WARN]' }
            } elseif ($xFiveHourUsed -gt 95) { $xFhFlag = ' [OVER]' }
            elseif ($xFiveHourUsed -gt 80)  { $xFhFlag = ' [WARN]' }
            $fhTargStr = if ($null -ne $xFiveHourTarget) { "target=$($xFiveHourTarget)%" } else { 'target=n/a' }
            $fhDetail = "$($xFiveHourUsed)% / $fhTargStr$xFhFlag  [5h window  $xSrc]"
            $fhStatus = if ($xFhFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
            Add-Check 'Family pace: Codex 5h' $fhStatus $fhDetail
            Emit $fhStatus 'Family pace: Codex 5h' $fhDetail
        }

        # Weekly line
        $xWeeklyUsed = if ($null -ne $xWeeklyRem) { 100 - $xWeeklyRem } else { $xPct }
        # Compute weekly target: fraction of 168h elapsed since reset
        $xWeeklyTarget = $null
        if ($xWeeklyReset) {
            try {
                $wkReset = [DateTimeOffset]::Parse($xWeeklyReset)
                $wkStart = $wkReset.AddHours(-168)
                $wkElapsed = ([DateTimeOffset]::UtcNow - $wkStart).TotalHours
                if ($wkElapsed -lt 0) { $wkElapsed = 0 }
                if ($wkElapsed -gt 168) { $wkElapsed = 168 }
                $xWeeklyTarget = [math]::Round($wkElapsed / 168.0 * 100.0, 1)
            } catch {}
        }
        $xWkFlag = ''
        if ($null -ne $xWeeklyUsed) {
            if ($null -ne $xWeeklyTarget) {
                if ($xWeeklyUsed -gt $xWeeklyTarget) { $xWkFlag = ' [OVER]' }
                elseif ($xWeeklyUsed -gt $xWeeklyTarget * 0.9) { $xWkFlag = ' [WARN]' }
            } elseif ($xWeeklyUsed -gt 95) { $xWkFlag = ' [OVER]' }
            elseif ($xWeeklyUsed -gt 80) { $xWkFlag = ' [WARN]' }
        }
        $xTokStr  = if ($null -ne $xTokM)   { "${xTokM}M tokens" } else { '' }
        $xLimStr  = if ($null -ne $xLimitM) { "limit=${xLimitM}M" } else { '' }
        $xRstStr  = if ($xWeeklyReset)      { "reset=$xWeeklyReset" } else { '' }
        $xWkTargStr = if ($null -ne $xWeeklyTarget) { "target=$($xWeeklyTarget)%" } else { 'target=n/a' }
        $xWkUsedStr = if ($null -ne $xWeeklyUsed) { "$($xWeeklyUsed)%" } else { 'n/a' }
        $wkParts = @($xWkUsedStr, $xWkTargStr, $xTokStr, $xLimStr, $xRstStr) | Where-Object { $_ }
        $wkDetail = "$($wkParts -join '  ')$xWkFlag  [weekly  $xSrc]"
        $wkStatus = if ($xWkFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Codex weekly' $wkStatus $wkDetail
        Emit $wkStatus 'Family pace: Codex weekly' $wkDetail
    } catch {
        Add-Check 'Family pace: Codex' 'warn' "parse error: $_"
        Emit '!' 'Family pace: Codex' "parse error"
    }
}

# ── Gemini (AGY) ── DAILY cycle per model ─────────────────────────────────────────────────────
# Gemini resets DAILY (not weekly). gemini_measured_limits.json has DAY / HOUR limits.
# Daily target = fraction of the current calendar day (KST midnight) elapsed.
$_geminiLimFile  = Join-Path $_harnessHome 'gemini_measured_limits.json'
$_geminiUsageLog = "$env:USERPROFILE\.gemini\wkgemini_usage.jsonl"
if ((Test-Path $_geminiLimFile) -or (Test-Path $_geminiUsageLog)) {
    try {
        $gDailyLimit = [double]100e6   # default: 100M tokens/day (DAY field)
        $gFlag = ''; $gSrc = ''; $gDailyTarget = $null

        # Load measured limits -- use DAY limit for daily cycle
        if (Test-Path $_geminiLimFile) {
            try {
                $gl = Get-Content $_geminiLimFile -Raw | ConvertFrom-Json
                if ($gl.PSObject.Properties['DAY'] -and [double]$gl.DAY -gt 0) {
                    $gDailyLimit = [double]$gl.DAY
                }
                $gSrc = 'gemini_measured_limits.json'
            } catch {}
        }

        # Daily reset: Korea Standard Time, midnight (00:00 KST)
        $seoulTz  = [TimeZoneInfo]::FindSystemTimeZoneById("Korea Standard Time")
        $nowUtc   = [DateTimeOffset]::UtcNow
        $nowSeoul = [TimeZoneInfo]::ConvertTime($nowUtc, $seoulTz)
        # Start of today (KST midnight)
        $todayMidnightSeoul = $nowSeoul.Date   # DateTime (no offset)
        $resetSeoul = [DateTime]::new($todayMidnightSeoul.Year, $todayMidnightSeoul.Month, $todayMidnightSeoul.Day, 0, 0, 0)
        $resetUtc   = [DateTimeOffset]([TimeZoneInfo]::ConvertTimeToUtc($resetSeoul, $seoulTz))
        # Daily target = elapsed fraction of 24h
        $gDailyTarget = [math]::Round(($nowUtc - $resetUtc).TotalHours / 24.0 * 100.0, 1)
        if ($gDailyTarget -lt 0)   { $gDailyTarget = 0 }
        if ($gDailyTarget -gt 100) { $gDailyTarget = 100 }

        # Sum daily tokens from log (since midnight KST)
        $gDailyUsed = [double]0
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
                        $gDailyUsed += ($ti + $to + ($tc * 0.1)) * $w
                    }
                } catch {}
            }
        }

        $gPct   = [math]::Round($gDailyUsed / $gDailyLimit * 100.0, 1)
        $gUsedM = [math]::Round($gDailyUsed / 1e6, 3)
        $gLimM  = [math]::Round($gDailyLimit / 1e6, 0)
        if ($gPct -gt $gDailyTarget) { $gFlag = ' [OVER]' }
        elseif ($gPct -gt $gDailyTarget * 0.9) { $gFlag = ' [WARN]' }
        $detail = "$($gPct)% / target=$($gDailyTarget)%$gFlag  ${gUsedM}M/${gLimM}M  [daily cycle  $gSrc]"
        $status = if ($gFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Gemini daily' $status $detail
        Emit $status 'Family pace: Gemini daily' $detail

        # ── Gemini request count (daily) ──────────────────────────────────────────────────────
        # wkgemini.sh blocks at DAILY_REQ_LIMIT * DAILY_TARGET_PCT / 100 requests.
        # Mirror those constants here (source: wkappbot-kih/tools/wrappers/wkgemini.sh).
        $gReqDailyLimit  = 20000  # DAILY_REQ_LIMIT in wkgemini.sh (raised 2026-06-11: gemini is plentiful, daily cap is a runaway backstop only)
        $gReqTargetPct   = 50     # DAILY_TARGET_PCT in wkgemini.sh (the blocking threshold)
        $gReqTarget      = [int]($gReqDailyLimit * $gReqTargetPct / 100)   # 10000
        $gReqUsed        = 0
        $gReqFlag        = ''
        if (Test-Path $_geminiUsageLog) {
            try {
                foreach ($line in (Get-Content $_geminiUsageLog -Encoding UTF8 -ErrorAction SilentlyContinue)) {
                    if (-not $line.Trim()) { continue }
                    try {
                        $m = $line | ConvertFrom-Json
                        if ($m.ts -and [DateTimeOffset]::Parse($m.ts) -ge $resetUtc -and -not ($m.quota_exhausted)) {
                            $gReqUsed++
                        }
                    } catch {}
                }
            } catch {}
        }
        if ($gReqUsed -ge $gReqTarget) { $gReqFlag = ' [OVER]' }
        elseif ($gReqUsed -ge [int]($gReqTarget * 0.9)) { $gReqFlag = ' [WARN]' }
        $gReqDetail = "$gReqUsed/$gReqTarget req$gReqFlag  (limit=${gReqDailyLimit}, target=${gReqTargetPct}%)  [daily  wkgemini_usage.jsonl]"
        $gReqStatus = if ($gReqFlag -eq ' [OVER]') { 'warn' } else { 'ok' }
        Add-Check 'Family pace: Gemini req' $gReqStatus $gReqDetail
        Emit $gReqStatus 'Family pace: Gemini req' $gReqDetail
    } catch {
        Add-Check 'Family pace: Gemini daily' 'warn' "parse error: $_"
        Emit '!' 'Family pace: Gemini daily' "parse error"
    }
}
