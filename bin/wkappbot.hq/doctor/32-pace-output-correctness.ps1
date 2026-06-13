# wkdoctor check: per-family pace OUTPUT correctness (source freshness + state validity)
# PURPOSE: Diagnose whether each AI family's pace source is STALE or the displayed pct
#          can be TRUSTED. This is AI-free physician check: deterministic source validation.
# SCOPE: checks 3 families (Claude, Codex, Gemini) with READ-ONLY diagnostic (no fixes).
# FAIL-OPEN: missing source file => SKIP that family, never crash.
# Output: one line per family with age, verdict, and one-line solve hint.
#
# Ground-truth sources:
#   Claude: $USERPROFILE/.claude/wkharness/usage_anchor_claude.json (captured_at)
#   Codex:  $USERPROFILE/.claude/wkharness/codex_balance.json (capturedAt + resetAt fields)
#   Gemini: $USERPROFILE/.gemini/wkgemini_usage.jsonl (per-line ts)
#   Cache:  $WKHARNESS_HOME/pace.json (totalPct vs anchor total_pct for disagreement)

$_harnessHome = if ($env:WKHARNESS_HOME) { $env:WKHARNESS_HOME } else { "$env:USERPROFILE\.claude\wkharness" }

# ── Claude: source freshness + cache-anchor agreement ───────────────────────────────────────
$_claudeAnchorFile = Join-Path $_harnessHome 'usage_anchor_claude.json'
if (Test-Path $_claudeAnchorFile) {
    try {
        $cOk = $true; $cDetail = ''; $cAgeStr = ''; $cVerdict = 'ok'
        $cAnchorAge = $null

        # Read anchor: extracted captured_at timestamp
        try {
            $ca = Get-Content $_claudeAnchorFile -Encoding UTF8 -Raw | ConvertFrom-Json
            if ($ca.PSObject.Properties['captured_at'] -and $ca.captured_at) {
                $capTs = [DateTimeOffset]::Parse($ca.captured_at)
                $cAnchorAge = [math]::Round(([DateTimeOffset]::UtcNow - $capTs).TotalMinutes, 0)
                $cAgeStr = "$($cAnchorAge)min ago"
                # WARN if > 90 min old
                if ($cAnchorAge -gt 90) {
                    $cVerdict = 'warn'
                    $cDetail = "anchor STALE (>90min old)"
                }
            }
        } catch {}

        # Check cache-anchor agreement: pace.json totalPct vs anchor total_pct
        $paceFile = Join-Path $_harnessHome 'pace.json'
        if (-not $cDetail -and (Test-Path $paceFile)) {
            try {
                $pa = Get-Content $paceFile -Encoding UTF8 -Raw | ConvertFrom-Json
                $anchorTotal = if ($ca.PSObject.Properties['total_pct']) { [double]$ca.total_pct } else { $null }
                $paceTotal = if ($pa.PSObject.Properties['totalPct']) { [double]$pa.totalPct } else { $null }
                if ($null -ne $anchorTotal -and $null -ne $paceTotal) {
                    $disagreement = [math]::Abs($paceTotal - $anchorTotal)
                    if ($disagreement -gt 3.0) {
                        $cVerdict = 'warn'
                        $cDetail = "cache-anchor DISAGREEMENT (pace=$($paceTotal)% vs anchor=$($anchorTotal)%, delta=$($disagreement)%)"
                    }
                }
            } catch {}
        }

        # Build output
        $solveHint = if ($cDetail) { " -- Run: claude /usage" } else { "" }
        $fullDetail = "Claude anchor  age=$($cAgeStr)  [$cDetail]$solveHint" -replace '\s+\[\s*\]', ''
        $status = if ($cVerdict -eq 'warn') { 'warn' } else { 'ok' }
        Add-Check 'Pace: Claude output' $status $fullDetail
        Emit $status 'Pace: Claude output' $fullDetail
    } catch {
        Add-Check 'Pace: Claude output' 'warn' "parse error"
        Emit '!' 'Pace: Claude output' "parse error: $_"
    }
}

# -- Codex: source age + resetAt staleness (with auto-refresh from sqlite when >1h stale) -----
$_codexBalanceFile = Join-Path $_harnessHome 'codex_balance.json'
if (Test-Path $_codexBalanceFile) {
    try {
        $xVerdict = 'ok'; $xDetail = ''; $xAgeStr = 'n/a'
        $xCapturedAge = $null; $xRefreshNote = ''

        # Read balance: check capturedAt age + source type
        $cb = $null
        try {
            $cb = Get-Content $_codexBalanceFile -Encoding UTF8 -Raw | ConvertFrom-Json
            if ($cb.PSObject.Properties['capturedAt'] -and $cb.capturedAt) {
                $capTs = [DateTimeOffset]::Parse($cb.capturedAt)
                $xCapturedAge = [math]::Round(([DateTimeOffset]::UtcNow - $capTs).TotalHours, 1)
                $xAgeStr = "$($xCapturedAge)h ago"
            }
        } catch {}

        # AUTO-REFRESH: if capturedAt > 1h old, attempt sqlite refresh.
        # Fail-open: if Invoke-WkCodexBalanceRefreshFromSqlite is not in scope, try to
        # load wkharness-codex-pace.ps1 from the known kih tools path; skip if unavailable.
        if ($null -ne $xCapturedAge -and $xCapturedAge -gt 1.0) {
            if (-not (Get-Command Invoke-WkCodexBalanceRefreshFromSqlite -ErrorAction SilentlyContinue)) {
                $_pacePaths = @(
                    'D:\GitHub\wkappbot-kih\tools\wkharness-codex-pace.ps1',
                    (Join-Path (Split-Path (Split-Path $_harnessHome -Parent) -Parent) 'GitHub\wkappbot-kih\tools\wkharness-codex-pace.ps1')
                )
                foreach ($_pp in $_pacePaths) {
                    if (Test-Path $_pp) { try { . $_pp; break } catch {} }
                }
            }
            if (Get-Command Invoke-WkCodexBalanceRefreshFromSqlite -ErrorAction SilentlyContinue) {
                try {
                    $_r = Invoke-WkCodexBalanceRefreshFromSqlite -StaleThresholdMinutes 60
                    if ($_r.refreshed) {
                        $cb = Get-Content $_codexBalanceFile -Encoding UTF8 -Raw | ConvertFrom-Json
                        $capTs2 = [DateTimeOffset]::Parse($cb.capturedAt)
                        $xCapturedAge = [math]::Round(([DateTimeOffset]::UtcNow - $capTs2).TotalHours, 1)
                        $xAgeStr = "$($xCapturedAge)h ago (refreshed)"
                        $xRefreshNote = "  [auto-refreshed: $($_r.reason)]"
                    } else {
                        $xRefreshNote = "  [refresh-skipped: $($_r.reason)]"
                    }
                } catch { $xRefreshNote = "  [refresh-exception: $($_.Exception.Message)]" }
            } else {
                $xRefreshNote = "  [refresh-unavailable: wkharness-codex-pace.ps1 not found]"
            }
        }
        # end auto-refresh

        # WARN if still > 24h old after refresh attempt
        if ($null -ne $xCapturedAge -and $xCapturedAge -gt 24) {
            $xVerdict = 'warn'
            $xDetail = "captured STALE (>24h old)"
            $src = if ($cb -and $cb.PSObject.Properties['source']) { $cb.source } else { '' }
            if ($src -eq 'manual-dashboard-paste') {
                $xDetail += ' (manual-paste, not auto-refreshed)'
            }
        }

        # Check resetAt staleness: fiveHour and weekly windows (check independently of capturedAt)
        if ($cb) {
            $fhReset = $null; $wkReset = $null
            if ($cb.PSObject.Properties['fiveHour'] -and $cb.fiveHour.PSObject.Properties['resetAt']) {
                try {
                    $fhReset = [DateTimeOffset]::Parse($cb.fiveHour.resetAt)
                } catch {}
            }
            if ($cb.PSObject.Properties['weekly'] -and $cb.weekly.PSObject.Properties['resetAt']) {
                try {
                    $wkReset = [DateTimeOffset]::Parse($cb.weekly.resetAt)
                } catch {}
            }

            # Flag if resetAt is in the PAST (window already closed)
            # Check fiveHour first since it's shorter window
            $nowUtc = [DateTimeOffset]::UtcNow
            if ($fhReset -and $fhReset -lt $nowUtc) {
                # fiveHour window expired: this is a FAIL since pace data is invalid
                $fhAgeHours = [math]::Round(([TimeSpan]($nowUtc - $fhReset)).TotalHours, 1)
                if ($xDetail) { $xDetail += " | " }
                $xDetail += "fiveHour window EXPIRED ($($fhAgeHours)h past)"
                if ($xVerdict -ne 'fail') { $xVerdict = 'fail' }
            }
            # Weekly: flag if WAY past (>7 days)
            if ($wkReset -and $wkReset -lt $nowUtc) {
                $weeklyAgeDays = [math]::Round(([TimeSpan]($nowUtc - $wkReset)).TotalDays, 1)
                if ($weeklyAgeDays -gt 7) {
                    if ($xDetail) { $xDetail += " | " }
                    $xDetail += "weekly window FAR PAST ($($weeklyAgeDays)d old)"
                    if ($xVerdict -ne 'fail') { $xVerdict = 'fail' }
                }
            }
        }

        # Build output
        $solveHint = if ($xDetail -and $xVerdict -ne 'ok') { " -- Run: wkdoctor (auto-refresh will retry sqlite)" } else { "" }
        $fullDetail = "Codex balance  age=$($xAgeStr)  [$xDetail]$solveHint$xRefreshNote" -replace '\s+\[\s*\]', ''
        $status = if ($xVerdict -eq 'fail') { 'fail' } elseif ($xVerdict -eq 'warn') { 'warn' } else { 'ok' }
        Add-Check 'Pace: Codex output' $status $fullDetail
        Emit $status 'Pace: Codex output' $fullDetail
    } catch {
        Add-Check 'Pace: Codex output' 'warn' "parse error"
        Emit '!' 'Pace: Codex output' "parse error: $_"
    }
}

# ── Gemini: source freshness (wkgemini_usage.jsonl latest entry age) ───────────────────────
$_geminiUsageLog = "$env:USERPROFILE\.gemini\wkgemini_usage.jsonl"
if (Test-Path $_geminiUsageLog) {
    try {
        $gVerdict = 'ok'; $gDetail = ''; $gAgeStr = 'n/a'
        $gLatestTs = $null

        # Read last line of JSONL for the most recent timestamp
        $lines = @(Get-Content $_geminiUsageLog -Encoding UTF8 -ErrorAction SilentlyContinue)
        if ($lines -and $lines.Count -gt 0) {
            # Find the last non-empty line
            for ($i = $lines.Count - 1; $i -ge 0; $i--) {
                if ($lines[$i].Trim()) {
                    try {
                        $lastEntry = $lines[$i] | ConvertFrom-Json
                        if ($lastEntry.PSObject.Properties['ts'] -and $lastEntry.ts) {
                            $gLatestTs = [DateTimeOffset]::Parse($lastEntry.ts)
                            $gAge = [math]::Round(([DateTimeOffset]::UtcNow - $gLatestTs).TotalHours, 1)
                            $gAgeStr = "$($gAge)h ago"
                            # WARN if > 24h old
                            if ($gAge -gt 24) {
                                $gVerdict = 'warn'
                                $gDetail = "latest entry STALE (>24h old)"
                            }
                        }
                        break
                    } catch {}
                }
            }
        }

        # PER-MODEL (2026-06-11): each Gemini model has its own daily allowance + reset, so the
        # correctness check also reports a per-model request breakdown since KST midnight. This
        # lets the dashboard confirm wkpace.ps1's per-model output matches the raw log. Read-only;
        # never raises a Claude block (Gemini family only).
        $gPerModel = ''
        try {
            $_seoulTz  = [TimeZoneInfo]::FindSystemTimeZoneById('Korea Standard Time')
            $_nowSeoul = [TimeZoneInfo]::ConvertTime([DateTimeOffset]::UtcNow, $_seoulTz)
            $_midSeo   = [DateTime]::new($_nowSeoul.Year, $_nowSeoul.Month, $_nowSeoul.Day, 0, 0, 0)
            $_resetUtc = [DateTimeOffset]([TimeZoneInfo]::ConvertTimeToUtc($_midSeo, $_seoulTz))
            $_byModel = @{}
            foreach ($_ln in $lines) {
                if (-not $_ln.Trim()) { continue }
                try {
                    $_e = $_ln | ConvertFrom-Json
                    if ($_e.ts -and [DateTimeOffset]::Parse([string]$_e.ts) -ge $_resetUtc -and -not $_e.quota_exhausted) {
                        $_mdl = if ($_e.model) { [string]$_e.model } else { 'unknown' }
                        if (-not $_byModel.ContainsKey($_mdl)) { $_byModel[$_mdl] = 0 }
                        $_byModel[$_mdl]++
                    }
                } catch {}
            }
            if ($_byModel.Count -gt 0) {
                $gPerModel = '  models(since-reset): ' + (($_byModel.Keys | Sort-Object | ForEach-Object { "$($_)=$($_byModel[$_])req" }) -join ', ')
            }
        } catch {}

        # Build output
        if (-not $gLatestTs) {
            $gAgeStr = 'unknown'
            $gVerdict = 'warn'
            $gDetail = 'no valid timestamp found in usage log'
        }
        $solveHint = if ($gDetail) { " -- Check: wkgemini integration active" } else { "" }
        $fullDetail = "Gemini usage  age=$($gAgeStr)  [$gDetail]$solveHint$gPerModel" -replace '\s+\[\s*\]', ''
        $status = if ($gVerdict -eq 'warn') { 'warn' } else { 'ok' }
        Add-Check 'Pace: Gemini output' $status $fullDetail
        Emit $status 'Pace: Gemini output' $fullDetail
    } catch {
        Add-Check 'Pace: Gemini output' 'warn' "parse error"
        Emit '!' 'Pace: Gemini output' "parse error: $_"
    }
}
