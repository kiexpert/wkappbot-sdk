#Requires -Version 5.1
# ask optimization smoke test + dispatch benchmark + auto-suggest bug reporter
#
# Architecture note: wkappbot ask dispatches to AI tab via CDP and exits immediately.
# The "dispatch latency" (time until wkappbot exits) is the user-perceived speed.
# CLI-parity goal: dispatch < 300ms cold, < 100ms warm.
#
# Usage:
#   -Quick          BASIC: help/skill wiring only (~3s, no live calls)
#   -Full           FULL: live dispatch to all providers, routing health check
#   -Benchmark      FULL + JSON timings + history JSONL + baseline diff + completion polling
#   -AutoSuggest    Auto-file wkappbot suggest for every threshold breach
#   -Baseline path  Compare against saved bench-results.json
#   -Loop N         Repeat N times (0 = infinite); default 1
#   -IntervalSec N  Seconds between iterations; default 60
#   -FinalAnswerTimeoutSec N  Wait up to N sec for AI completion in -Benchmark; default 90
#   -SkipCompletion Skip CDP completion polling (only measure dispatch)
#
# Dispatch speed thresholds:
#   GOOD  < 300ms    WARN  300-800ms    BUG > 800ms    HANG = no "dispatched" in output
#
# Completion thresholds (FinalAnswerSec):
#   GOOD  < 30s   WARN  30-60s   SLOW  > 60s   HANG  > FinalAnswerTimeoutSec

param(
    [switch]$Quick,
    [switch]$Full,
    [switch]$Benchmark,
    [switch]$AutoSuggest,
    [string]$Baseline,
    [string]$ResultsPath    = 'benchmarks/bench-results.json',
    [string]$HistoryPath    = 'benchmarks/bench-history.jsonl',
    [int]$DispatchGoodMs    = 300,
    [int]$DispatchWarnMs    = 800,
    [int]$DispatchRegressionMs = 500,
    [int]$Loop              = 1,
    [int]$IntervalSec       = 60,
    [int]$FinalAnswerTimeoutSec = 90,
    [int]$CompletionGoodSec     = 30,
    [int]$CompletionWarnSec     = 60,
    [switch]$SkipCompletion
)

$ErrorActionPreference = 'Continue'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$repoBin  = Join-Path $repoRoot 'bin'

if (!(Test-Path (Join-Path $repoBin 'wkappbot.exe')))   { throw "bin/wkappbot.exe missing" }
if (!(Test-Path (Join-Path $repoBin 'wkappbot-core.exe'))) { throw "bin/wkappbot-core.exe missing" }

$env:PATH = "$repoBin;$env:PATH"
$isCI     = $env:GITHUB_ACTIONS -eq 'true' -or $env:CI -eq 'true'
$runFull  = ($Full -or $Benchmark) -and -not $Quick

$providers = @('claude', 'gpt', 'gemini', 'triad')

# Provider routing table: domain (for wkappbot a11y read --eval-js targeting)
# + assistant message-count JS expression. triad fans out to all three.
$script:ProviderConfig = @{
    'claude' = @{
        Domain  = 'claude.ai'
        # Count assistant message bubbles
        CountJs = "document.querySelectorAll('.font-claude-message').length"
    }
    'gpt' = @{
        Domain  = 'chatgpt.com'
        CountJs = "document.querySelectorAll('[data-message-author-role=`"assistant`"]').length"
    }
    'gemini' = @{
        Domain  = 'gemini.google.com'
        CountJs = "document.querySelectorAll('model-response,.response-content').length"
    }
}

# Reset per-run state
$script:pass = 0; $script:warn = 0; $script:fail = 0
$script:dispatchMs    = @{}
$script:completionSec = @{}
$script:routingFailed = [System.Collections.Generic.List[string]]::new()
$script:hangProviders = [System.Collections.Generic.List[string]]::new()
$script:slowProviders = [System.Collections.Generic.List[string]]::new()

$askPrompt = "Health check: confirm your routing is alive. One sentence only."

# ── Helpers ────────────────────────────────────────────────────────────────

function Section([string]$title) {
    Write-Host ""
    Write-Host ("-" * 60) -ForegroundColor DarkGray
    Write-Host "  $title" -ForegroundColor Cyan
    Write-Host ("-" * 60) -ForegroundColor DarkGray
}

function Invoke-Cmd([string]$label, [string[]]$cmd, [int]$expect = 0, [switch]$Soft, [string]$TimingKey) {
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    Write-Host "`n=== $label ==="
    Write-Host ("  wkappbot " + ($cmd -join ' '))
    $lines = @(& wkappbot @cmd 2>&1)
    $code  = $LASTEXITCODE
    $sw.Stop()
    $ms = [int]$sw.Elapsed.TotalMilliseconds

    if ($TimingKey) {
        $script:dispatchMs[$TimingKey] = $ms
        $c = if ($ms -gt $DispatchWarnMs) { 'Red' } elseif ($ms -gt $DispatchGoodMs) { 'Yellow' } else { 'Green' }
        Write-Host ("  dispatch={0}ms" -f $ms) -ForegroundColor $c
    }

    $shown = [Math]::Min($lines.Count, 6)
    if ($shown -gt 0) { $lines[0..($shown-1)] | ForEach-Object { Write-Host "  $_" } }
    if ($lines.Count -gt 6) { Write-Host ("  ... ({0} more)" -f ($lines.Count - 6)) }
    Write-Host ("  exit={0}  elapsed={1}ms" -f $code, $ms)

    if ($code -ne $expect) {
        if ($Soft) { $script:warn++; Write-Host ("  [WARN] expected {0} got {1}" -f $expect,$code) -ForegroundColor Yellow }
        else       { $script:fail++; Write-Host ("  [FAIL] expected {0} got {1}" -f $expect,$code) -ForegroundColor Red }
    } else { $script:pass++ }

    [Console]::Out.Flush()
    return $lines
}

function Assert-Match([string]$label, [string[]]$lines, [string[]]$patterns, [switch]$Soft) {
    $missing = @()
    foreach ($p in $patterns) { if (-not ($lines | Select-String -Pattern $p)) { $missing += $p } }
    if ($missing.Count -eq 0) {
        $script:pass++
        Write-Host ("  [OK] {0}" -f $label) -ForegroundColor Green
    } else {
        if ($Soft) { $script:warn++; Write-Host ("  [WARN] {0} missing: {1}" -f $label,($missing -join ', ')) -ForegroundColor Yellow }
        else       { $script:fail++; Write-Host ("  [FAIL] {0} missing: {1}" -f $label,($missing -join ', ')) -ForegroundColor Red }
    }
}

# ── CDP tab discovery (display only) + wkappbot eval-js bridge ──────────────

# Lists all 'page' type CDP tabs on a given port. Used only for the tab-count
# summary line at the top of FULL/BENCHMARK runs (no completion logic).
function Get-CdpTabs([int]$Port) {
    try {
        $resp = Invoke-RestMethod -UseBasicParsing -TimeoutSec 2 -Uri ("http://127.0.0.1:{0}/json" -f $Port)
        $tabs = @($resp | Where-Object { $_.type -eq 'page' })
        foreach ($t in $tabs) { $t | Add-Member -NotePropertyName 'cdpPort' -NotePropertyValue $Port -Force }
        return $tabs
    } catch {
        return @()
    }
}

# Scan all known Chrome CDP ports for display purposes only.
function Get-AllDisplayTabs() {
    $all = @()
    foreach ($p in @(9222, 9223, 9224, 9225, 9226, 9229)) {
        $all += Get-CdpTabs -Port $p
    }
    return $all
}

# Run a JS expression in the active tab matched by domain via wkappbot a11y read.
# wkappbot owns the only CDP DevTools session per tab (Chrome enforces 1 session
# max), so all eval traffic MUST go through it. Direct PS WebSocket connections
# will fail or conflict. Returns trimmed result string, or $null on failure.
function Invoke-AskEval([string]$domain, [string]$js) {
    try {
        $lines = @(& wkappbot a11y read "{domain:'$domain'}" --eval-js $js 2>&1)
    } catch {
        return $null
    }
    # Output: log lines (# TARGET, [INFO], JSON) + the bare JS result line.
    # Filter known prefixes; first surviving non-empty line is the value.
    $result = $lines |
        Where-Object { $_ -is [string] -and $_ -notmatch '^\s*$' } |
        Where-Object { $_ -notmatch '^#' -and $_ -notmatch '^\[' -and $_ -notmatch '^\{' } |
        Select-Object -First 1
    if ($null -eq $result) { return $null }
    return $result.ToString().Trim()
}

# Pulls the assistant-message count for a provider. Returns -1 on failure
# (so callers can distinguish "no tab / eval error" from a real 0 count).
function Get-AssistantCount([string]$provider) {
    $cfg = $script:ProviderConfig[$provider]
    if (-not $cfg) { return -1 }
    $raw = Invoke-AskEval -domain $cfg.Domain -js $cfg.CountJs
    if ([string]::IsNullOrWhiteSpace($raw)) { return -1 }
    $n = 0
    if ([int]::TryParse($raw, [ref]$n)) { return $n }
    return -1
}

# ── Dispatch + completion ───────────────────────────────────────────────────

$script:dispatchLogPaths = @{}   # provider -> log path (for completion polling)

function Invoke-AskDispatch([string]$provider) {
    $cmd = if ($provider -eq 'triad') { @('ask','triad',$askPrompt) } else { @('ask',$provider,$askPrompt) }
    $sw  = [System.Diagnostics.Stopwatch]::StartNew()
    $out = @(& wkappbot @cmd 2>&1)
    $sw.Stop()
    $ms  = [int]$sw.Elapsed.TotalMilliseconds
    $script:dispatchMs[$provider] = $ms

    # Extract log path for response polling (line like: "Log: D:\...\ask-claude.pid=NNN.log")
    $logLine = ($out | Where-Object { $_ -match '^Log:\s+' } | Select-Object -First 1)
    if ($logLine -match '^Log:\s+(.+\.log)') {
        $script:dispatchLogPaths[$provider] = $Matches[1].Trim()
    }

    # Routing check
    $dispatched = $out | Select-String -Pattern 'dispatched to background|ask-[a-z]+'
    $noTab      = $out | Select-String -Pattern 'no.*tab|tab.*not|target.*not found|No.*window'

    $c = if ($ms -gt $DispatchWarnMs) { 'Red' } elseif ($ms -gt $DispatchGoodMs) { 'Yellow' } else { 'Green' }
    Write-Host ("  [{0}] dispatch={1}ms" -f $provider, $ms) -ForegroundColor $c

    if (-not $dispatched) {
        $script:fail++
        $script:routingFailed.Add($provider)
        Write-Host ("  [FAIL] {0} routing -- no 'dispatched' signal" -f $provider) -ForegroundColor Red
        $out | Select-Object -Last 3 | ForEach-Object { Write-Host "    $_" -ForegroundColor DarkRed }
        return $false
    } elseif ($noTab) {
        $script:warn++
        Write-Host ("  [WARN] {0} tab not ready" -f $provider) -ForegroundColor Yellow
        $script:slowProviders.Add("notab:$provider")
        return $false
    } elseif ($ms -gt $DispatchWarnMs) {
        $script:fail++
        $script:slowProviders.Add($provider)
        Write-Host ("  [BUG] {0} dispatch slow: {1}ms > {2}ms" -f $provider,$ms,$DispatchWarnMs) -ForegroundColor Red
        return $true
    } else {
        $script:pass++
        return $true
    }
    [Console]::Out.Flush()
}

# Poll ask log file for [ASK_FULL_ANSWER_BEGIN]...[ASK_FULL_ANSWER_END] with non-empty body.
# Returns the answer text or $null on timeout/failure.
function Wait-LogAnswer([string]$logPath, [int]$TimeoutSec) {
    if (-not $logPath -or -not (Test-Path $logPath)) { return $null }
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    while ($sw.Elapsed.TotalSeconds -lt $TimeoutSec) {
        $content = Get-Content -Raw -Path $logPath -ErrorAction SilentlyContinue
        if ($content) {
            if ($content -match '\[ASK_FULL_ANSWER_BEGIN\]([\s\S]*?)\[ASK_FULL_ANSWER_END\]') {
                $body = $Matches[1].Trim()
                if ($body.Length -gt 5) { return $body.Substring(0, [Math]::Min(200, $body.Length)) }
            }
            if ($content -match '\[ASK_ANSWER_BEGIN\]([\s\S]*?)\[ASK_ANSWER_END\]') {
                $body = $Matches[1].Trim()
                if ($body.Length -gt 5) { return $body.Substring(0, [Math]::Min(200, $body.Length)) }
            }
        }
        # Check moved-to-old-dir path
        $dir = Split-Path $logPath
        $file = Split-Path $logPath -Leaf
        $providerName = if ($file -match 'ask-(\w+)\.') { $Matches[1] } else { '' }
        $oldPath = Join-Path (Join-Path $dir "old ask $providerName") $file
        if ($oldPath -ne $logPath -and (Test-Path $oldPath)) {
            $content2 = Get-Content -Raw -Path $oldPath -ErrorAction SilentlyContinue
            if ($content2 -and $content2 -match '\[ASK_FULL_ANSWER_BEGIN\]([\s\S]*?)\[ASK_FULL_ANSWER_END\]') {
                $body = $Matches[1].Trim()
                if ($body.Length -gt 5) { return $body.Substring(0, [Math]::Min(200, $body.Length)) }
            }
        }
        Start-Sleep -Seconds 3
    }
    return $null
}

# Snapshots assistant message counts per leaf provider.
# triad fans out to all three single providers.
function Get-LeafProviders([string]$provider) {
    if ($provider -eq 'triad') { return @('claude','gpt','gemini') }
    return @($provider)
}

function Snapshot-Counts([string[]]$leaves) {
    $snap = @{}
    foreach ($p in $leaves) { $snap[$p] = Get-AssistantCount $p }
    return $snap
}

# Polls until all leaves register at least one new assistant message OR the
# global timeout fires. Returns a hashtable: provider => completion seconds (or -1 = hang).
# Wait for completion by polling each provider's ask log file for non-empty
# [ASK_FULL_ANSWER_BEGIN]...[ASK_FULL_ANSWER_END] markers.
# This approach is reliable regardless of Chrome/CDP window resolution issues.
function Wait-Completion {
    param(
        [Parameter(Mandatory)] [string[]]$Leaves,
        [int]$TimeoutSec = 90
    )
    $sw       = [System.Diagnostics.Stopwatch]::StartNew()
    $jobs     = @{}   # provider -> PS job

    # Launch parallel log-polling jobs
    foreach ($p in $Leaves) {
        $logPath = $script:dispatchLogPaths[$p]
        if (-not $logPath) {
            Write-Host ("    [{0}] no log path -- skip" -f $p) -ForegroundColor DarkYellow
            $script:completionSec[$p] = -1
            $script:hangProviders.Add($p)
            continue
        }
        $timeout = $TimeoutSec
        # Build the "old ask X" path: logs\old ask claude\filename.log
        $logFile   = Split-Path $logPath -Leaf
        $logsDir   = Split-Path $logPath -Parent
        $provSlug  = if ($logFile -match 'ask-(\w+)\.') { $Matches[1] } else { '' }
        $oldLogPath = Join-Path (Join-Path $logsDir "old ask $provSlug") $logFile

        $jobs[$p] = Start-Job -ScriptBlock {
            param($lp, $oldLp, $to)
            function Check-Log($path) {
                if (-not (Test-Path $path)) { return $null }
                $c = Get-Content -Raw $path -ErrorAction SilentlyContinue
                if (-not $c) { return $null }
                if ($c -match '\[ASK_FULL_ANSWER_BEGIN\]([\s\S]*?)\[ASK_FULL_ANSWER_END\]') {
                    $b = $Matches[1].Trim()
                    if ($b.Length -gt 5) { return $b.Substring(0,[Math]::Min(200,$b.Length)) }
                }
                if ($c -match '\[ASK_ANSWER_BEGIN\]([\s\S]*?)\[ASK_ANSWER_END\]') {
                    $b = $Matches[1].Trim()
                    if ($b.Length -gt 5) { return $b.Substring(0,[Math]::Min(200,$b.Length)) }
                }
                return $null
            }
            $sw2 = [System.Diagnostics.Stopwatch]::StartNew()
            while ($sw2.Elapsed.TotalSeconds -lt $to) {
                $r = Check-Log $lp; if (-not $r) { $r = Check-Log $oldLp }
                if ($r) { return $r }
                Start-Sleep -Seconds 3
            }
            return $null
        } -ArgumentList $logPath, $oldLogPath, $TimeoutSec
    }

    # Poll jobs until all done or timeout
    $done = @{}
    while ($done.Count -lt $jobs.Count -and $sw.Elapsed.TotalSeconds -lt ($TimeoutSec + 5)) {
        foreach ($p in @($jobs.Keys)) {
            if ($done.ContainsKey($p)) { continue }
            $job = $jobs[$p]
            if ($job.State -in @('Completed','Failed','Stopped')) {
                $answer = Receive-Job $job -ErrorAction SilentlyContinue
                Remove-Job $job -Force
                $secs = [int]$sw.Elapsed.TotalSeconds
                if ($answer) {
                    $script:completionSec[$p] = $secs
                    $tagC = if ($secs -gt $CompletionWarnSec) { 'Red' } elseif ($secs -gt $CompletionGoodSec) { 'Yellow' } else { 'Green' }
                    Write-Host ("    [{0}] complete @ {1}s: {2}" -f $p, $secs, $answer.Substring(0,[Math]::Min(80,$answer.Length))) -ForegroundColor $tagC
                } else {
                    $script:completionSec[$p] = -1
                    $script:hangProviders.Add($p)
                    Write-Host ("    [{0}] HANG -- no response within {1}s" -f $p, $TimeoutSec) -ForegroundColor Red
                    if ($AutoSuggest -and -not $isCI) {
                        & wkappbot suggest ("ask hang: {0} no response >{1}s" -f $p,$TimeoutSec) `
                            --requirement "wkappbot ask $p health-check => dispatched" `
                            --requirement "wkappbot ask --help --no-regression => ask gpt" `
                            --requirement "wkappbot skill read ask-command-cheatsheet => SINGLE" 2>&1 | Out-Null
                    }
                }
                $done[$p] = $true
            }
        }
        if ($done.Count -lt $jobs.Count) { Start-Sleep -Seconds 2 }
    }
    # Cleanup any remaining jobs
    $jobs.Values | Where-Object { $_.State -notin @('Completed','Failed','Stopped') } | ForEach-Object { Stop-Job $_ -ErrorAction SilentlyContinue; Remove-Job $_ -Force -ErrorAction SilentlyContinue }
}

function File-Suggest([string]$title, [string]$req1, [string]$exp1) {
    if (-not $AutoSuggest -or $isCI) { return }
    Write-Host ("  [SUGGEST] {0}" -f $title) -ForegroundColor DarkYellow
    & wkappbot suggest $title `
        --requirement "$req1 => $exp1" `
        --requirement "wkappbot ask --help --no-regression => ask gpt" `
        --requirement "wkappbot skill read ask-command-cheatsheet => SINGLE" 2>&1 | Out-Null
}

function Write-BenchmarkSummary([int]$iter) {
    Section ("Benchmark (iter {0})" -f $iter)

    $commit = (& git -C $repoRoot rev-parse --short HEAD 2>$null)

    Write-Host "Dispatch latency (target < ${DispatchGoodMs}ms):" -ForegroundColor Cyan
    foreach ($k in $providers) {
        if (-not $script:dispatchMs.ContainsKey($k)) { continue }
        $ms  = $script:dispatchMs[$k]
        $tag = ' ok'; $c = 'Green'
        if ($ms -gt $DispatchWarnMs) { $tag = ' BUG'; $c = 'Red' }
        elseif ($ms -gt $DispatchGoodMs) { $tag = ' SLOW'; $c = 'Yellow' }
        Write-Host ("  {0,-8} {1,5}ms{2}" -f $k, $ms, $tag) -ForegroundColor $c
    }

    if ($script:completionSec.Count -gt 0) {
        Write-Host ""
        Write-Host ("Completion (target < {0}s, warn > {1}s):" -f $CompletionGoodSec,$CompletionWarnSec) -ForegroundColor Cyan
        foreach ($k in @('claude','gpt','gemini')) {
            if (-not $script:completionSec.ContainsKey($k)) { continue }
            $s = $script:completionSec[$k]
            if ($s -lt 0) {
                Write-Host ("  {0,-8} HANG" -f $k) -ForegroundColor Red
            } else {
                $tag = ' ok'; $c = 'Green'
                if ($s -gt $CompletionWarnSec)      { $tag = ' SLOW'; $c = 'Red' }
                elseif ($s -gt $CompletionGoodSec)  { $tag = ' WARN'; $c = 'Yellow' }
                Write-Host ("  {0,-8} {1,5}s{2}" -f $k, $s, $tag) -ForegroundColor $c
            }
        }
    }

    # Routing failures
    if ($script:routingFailed.Count -gt 0) {
        Write-Host ("`n  [ROUTING FAIL] " + ($script:routingFailed -join ', ')) -ForegroundColor Red
        foreach ($p in $script:routingFailed) {
            File-Suggest ("ask routing fail: {0}" -f $p) "wkappbot ask $p health-check" "routing"
        }
    }

    # Slow dispatch suggest
    foreach ($s in $script:slowProviders) {
        if ($s -notmatch '^notab:') {
            File-Suggest ("ask slow dispatch: {0} >{1}ms" -f $s,$DispatchWarnMs) "wkappbot ask $s health-check" "routing"
        }
    }

    # Hang suggests (completion failed within timeout)
    foreach ($p in $script:hangProviders) {
        File-Suggest ("ask completion hang: {0} >{1}s" -f $p,$FinalAnswerTimeoutSec) `
            "wkappbot ask $p health-check" "response within ${FinalAnswerTimeoutSec}s"
    }

    # Slow completion suggest (finished but > warn threshold)
    foreach ($k in @('claude','gpt','gemini')) {
        if (-not $script:completionSec.ContainsKey($k)) { continue }
        $s = $script:completionSec[$k]
        if ($s -gt $CompletionWarnSec) {
            File-Suggest ("ask slow completion: {0} {1}s" -f $k,$s) `
                "wkappbot ask $k health-check" "response within ${CompletionGoodSec}s"
        }
    }

    # Save JSON
    $null = New-Item -ItemType Directory -Force -Path (Split-Path (Join-Path $repoRoot $ResultsPath))
    $resObj = [ordered]@{
        timestamp_utc = [DateTime]::UtcNow.ToString('o')
        commit        = $commit
        iteration     = $iter
        thresholds    = [ordered]@{
            dispatchGoodMs  = $DispatchGoodMs
            dispatchWarnMs  = $DispatchWarnMs
            completionGoodSec = $CompletionGoodSec
            completionWarnSec = $CompletionWarnSec
            finalAnswerTimeoutSec = $FinalAnswerTimeoutSec
        }
        dispatchMs    = $script:dispatchMs
        completionSec = $script:completionSec
        routingFailed = @($script:routingFailed)
        slowProviders = @($script:slowProviders)
        hangProviders = @($script:hangProviders)
        pass=$script:pass; warn=$script:warn; fail=$script:fail
    }
    $json = $resObj | ConvertTo-Json -Depth 5
    $resAbs  = Join-Path $repoRoot $ResultsPath
    $histAbs = Join-Path $repoRoot $HistoryPath
    Set-Content  -Path $resAbs  -Value $json -Encoding UTF8
    Add-Content  -Path $histAbs -Value ($resObj | ConvertTo-Json -Depth 5 -Compress) -Encoding UTF8
    Write-Host ("`n  results -> {0}" -f $resAbs) -ForegroundColor DarkGray
    Write-Host ("  history -> {0}" -f $histAbs) -ForegroundColor DarkGray

    if ($Baseline) {
        $ba = if ([System.IO.Path]::IsPathRooted($Baseline)) { $Baseline } else { Join-Path $repoRoot $Baseline }
        if (Test-Path $ba) {
            $base = Get-Content -Raw $ba | ConvertFrom-Json
            $regressions = @()
            foreach ($k in $providers) {
                $cur  = $script:dispatchMs[$k]
                $prev = if ($base.dispatchMs.PSObject.Properties[$k]) { $base.dispatchMs.$k } else { $null }
                if ($cur -and $prev -and ($cur - $prev) -gt $DispatchRegressionMs) {
                    $regressions += ("{0}: +{1}ms vs baseline" -f $k,($cur-$prev))
                }
            }
            if ($regressions.Count -gt 0) {
                Write-Host "  [REGRESSION]" -ForegroundColor Red
                $regressions | ForEach-Object {
                    Write-Host ("    {0}" -f $_) -ForegroundColor Red
                    File-Suggest ("ask dispatch regression: {0}" -f $_) "wkappbot ask gpt health-check" "routing"
                }
                $script:fail += $regressions.Count
            } else {
                Write-Host "  [BASELINE OK] no regression" -ForegroundColor Green
                $script:pass++
            }
        } else {
            Write-Host ("  [INFO] baseline not found: {0}" -f $ba) -ForegroundColor DarkYellow
        }
    }
}

# ── One test run ────────────────────────────────────────────────────────────

function Invoke-OneRun([int]$iteration) {
    $script:pass=0; $script:warn=0; $script:fail=0
    $script:dispatchMs    = @{}
    $script:completionSec = @{}
    $script:routingFailed = [System.Collections.Generic.List[string]]::new()
    $script:hangProviders = [System.Collections.Generic.List[string]]::new()
    $script:slowProviders = [System.Collections.Generic.List[string]]::new()

    $modeLabel = if ($Benchmark) { 'BENCHMARK' } elseif ($runFull) { 'FULL' } else { 'BASIC' }
    Write-Host ""
    Write-Host ("=== ask optimization smoke (iter {0}) ===" -f $iteration) -ForegroundColor Cyan
    Write-Host ("Mode={0}  AutoSuggest={1}  thresholds: good<{2}ms warn>{3}ms" -f $modeLabel,$AutoSuggest,$DispatchGoodMs,$DispatchWarnMs)

    # ── BASIC ────────────────────────────────────────────────────────────────
    Section "Baseline"
    $ss = Invoke-Cmd 'skill-search' @('skill','search','ask','--app','wkappbot-workflow')
    Assert-Match 'skills registered' $ss @('ask-command-cheatsheet','ask-command-optimization-tests')

    $sf = Join-Path $repoRoot 'skills/wkappbot-workflow/ask-command-optimization-tests.skill.json'
    if (Test-Path $sf) {
        Assert-Match 'regression skill file' @(Get-Content $sf -Raw) @('ask command optimization smoke tests','scripts/test-ask-optimization.ps1')
    } else { $script:warn++; Write-Host ("  [WARN] skill file missing") -ForegroundColor Yellow }

    $help = Invoke-Cmd 'ask-help' @('ask','--help','--no-regression')
    Assert-Match 'ask help routes' $help @('ask gpt','ask triad','Ask AI via CDP')

    if (-not $runFull) {
        Write-Host "`n[INFO] BASIC mode -- skipping live dispatch." -ForegroundColor DarkYellow
        return
    }

    # ── FULL: Dispatch latency per provider ──────────────────────────────────
    Section "Dispatch Latency"
    Write-Host "  Note: dispatch = time until wkappbot exits after injecting to AI tab via CDP"
    Write-Host ("  Targets: GOOD < {0}ms | WARN {0}-{1}ms | BUG > {1}ms" -f $DispatchGoodMs,$DispatchWarnMs)
    Write-Host ""

    # CDP port pre-check (display only -- completion polling now goes via wkappbot eval-js)
    $allTabs = Get-AllDisplayTabs
    $portSummary = $allTabs | Group-Object cdpPort | ForEach-Object { "$($_.Name)=$($_.Count)" }
    Write-Host ("  CDP tabs: " + ($portSummary -join '  ')) -ForegroundColor DarkGray
    if ($allTabs.Count -eq 0) {
        $script:warn++
        Write-Host "  [WARN] no CDP tabs reachable -- AI tabs may not be open" -ForegroundColor Yellow
    }

    $allLeaves  = @('claude','gpt','gemini')
    $usePolling = $Benchmark -and -not $SkipCompletion

    # ── Dispatch all providers with brief gap ────────────────────────────────
    $script:dispatchLogPaths = @{}  # reset log path map
    foreach ($p in @('claude','gpt','gemini','triad')) {
        [void](Invoke-AskDispatch $p)
        Start-Sleep -Milliseconds 200
    }

    # ── Poll for completion via log file markers (Benchmark only) ────────────
    if ($usePolling) {
        Section ("Completion Polling (timeout={0}s)" -f $FinalAnswerTimeoutSec)
        # Poll ask log files for [ASK_FULL_ANSWER_BEGIN]...[ASK_FULL_ANSWER_END]
        # Reliable regardless of Chrome window/domain resolution issues.
        $leavesToPoll = @($allLeaves | Where-Object { $script:dispatchLogPaths.ContainsKey($_) })
        if ($leavesToPoll.Count -eq 0) {
            Write-Host "  [SKIP] no log paths captured from dispatch output" -ForegroundColor Yellow
        } else {
            Write-Host ("  Polling ask logs for: {0}" -f ($leavesToPoll -join ', '))
            Wait-Completion -Leaves $leavesToPoll -TimeoutSec $FinalAnswerTimeoutSec
        }
    }

    if ($Benchmark) { Write-BenchmarkSummary $iteration }
}

# ── Main loop ────────────────────────────────────────────────────────────────

$totalIter = if ($Loop -le 0) { [int]::MaxValue } else { $Loop }
$gPass=0; $gFail=0

for ($i = 1; $i -le $totalIter; $i++) {
    Invoke-OneRun -iteration $i
    $gPass += $script:pass; $gFail += $script:fail

    $slowTag  = if ($script:slowProviders.Count -gt 0) { " slow=$($script:slowProviders.Count)" } else { "" }
    $routeTag = if ($script:routingFailed.Count -gt 0) { " routeFail=$($script:routingFailed.Count)" } else { "" }
    $hangTag  = if ($script:hangProviders.Count -gt 0) { " hang=$($script:hangProviders.Count)" } else { "" }

    Write-Host ""
    Write-Host ("=" * 60) -ForegroundColor Cyan
    Write-Host ("  iter {0}: pass={1} warn={2} fail={3}{4}{5}{6}" -f $i,$script:pass,$script:warn,$script:fail,$slowTag,$routeTag,$hangTag) `
        -ForegroundColor $(if ($script:fail -gt 0) { 'Red' } else { 'Green' })
    Write-Host ("=" * 60) -ForegroundColor Cyan

    if ($i -lt $totalIter -and $runFull) {
        Write-Host ("`n  next in {0}s..." -f $IntervalSec) -ForegroundColor DarkGray
        Start-Sleep -Seconds $IntervalSec
    }
}

Write-Host ""
Write-Host ("=" * 60) -ForegroundColor Cyan
Write-Host ("  TOTAL: pass={0} fail={1}" -f $gPass,$gFail) `
    -ForegroundColor $(if ($gFail -gt 0) { 'Red' } else { 'Green' })
Write-Host ("=" * 60) -ForegroundColor Cyan

if ($gFail -gt 0) { exit 1 }
exit 0
