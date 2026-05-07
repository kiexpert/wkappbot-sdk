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
#   -Benchmark      FULL + JSON timings + history JSONL + baseline diff
#   -AutoSuggest    Auto-file wkappbot suggest for every threshold breach
#   -Baseline path  Compare against saved bench-results.json
#   -Loop N         Repeat N times (0 = infinite); default 1
#   -IntervalSec N  Seconds between iterations; default 60
#
# Dispatch speed thresholds:
#   GOOD  < 300ms    WARN  300-800ms    BUG > 800ms    HANG = no "dispatched" in output

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
    [int]$IntervalSec       = 60
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

# Reset per-run state
$script:pass = 0; $script:warn = 0; $script:fail = 0
$script:dispatchMs    = @{}
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

function Invoke-AskDispatch([string]$provider) {
    $cmd = if ($provider -eq 'triad') { @('ask','triad',$askPrompt) } else { @('ask',$provider,$askPrompt) }
    $sw  = [System.Diagnostics.Stopwatch]::StartNew()
    $out = @(& wkappbot @cmd 2>&1)
    $sw.Stop()
    $ms  = [int]$sw.Elapsed.TotalMilliseconds
    $script:dispatchMs[$provider] = $ms

    # Routing check: "dispatched to background" must appear
    $dispatched = $out | Select-String -Pattern 'dispatched to background|ask-[a-z]+'
    $noTab      = $out | Select-String -Pattern 'no.*tab|tab.*not|target.*not found|No.*window'

    $c = if ($ms -gt $DispatchWarnMs) { 'Red' } elseif ($ms -gt $DispatchGoodMs) { 'Yellow' } else { 'Green' }
    Write-Host ("  [{0}] dispatch={1}ms" -f $provider, $ms) -ForegroundColor $c

    if (-not $dispatched) {
        $script:fail++
        $script:routingFailed.Add($provider)
        Write-Host ("  [FAIL] {0} routing -- no 'dispatched' signal" -f $provider) -ForegroundColor Red
        $out | Select-Object -Last 3 | ForEach-Object { Write-Host "    $_" -ForegroundColor DarkRed }
    } elseif ($noTab) {
        $script:warn++
        Write-Host ("  [WARN] {0} tab not ready" -f $provider) -ForegroundColor Yellow
        $script:slowProviders.Add("notab:$provider")
    } elseif ($ms -gt $DispatchWarnMs) {
        $script:fail++
        $script:slowProviders.Add($provider)
        Write-Host ("  [BUG] {0} dispatch slow: {1}ms > {2}ms" -f $provider,$ms,$DispatchWarnMs) -ForegroundColor Red
        $script:pass--  # undo the default pass
    } else {
        $script:pass++
    }
    [Console]::Out.Flush()
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

    # Save JSON
    $null = New-Item -ItemType Directory -Force -Path (Split-Path (Join-Path $repoRoot $ResultsPath))
    $resObj = [ordered]@{
        timestamp_utc = [DateTime]::UtcNow.ToString('o')
        commit        = $commit
        iteration     = $iter
        thresholds    = [ordered]@{ goodMs=$DispatchGoodMs; warnMs=$DispatchWarnMs }
        dispatchMs    = $script:dispatchMs
        routingFailed = @($script:routingFailed)
        slowProviders = @($script:slowProviders)
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

    # Tab readiness pre-check
    $tabCheck = @(& wkappbot ask --help 2>&1) | Select-String -Pattern 'No.*window|no.*target|CDP requires'
    if ($tabCheck) {
        $script:warn++
        Write-Host "  [WARN] CDP tab check: AI tabs may not be open" -ForegroundColor Yellow
    }

    foreach ($p in @('claude','gpt','gemini','triad')) {
        Invoke-AskDispatch $p
        Start-Sleep -Milliseconds 200  # brief gap between dispatches
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

    Write-Host ""
    Write-Host ("=" * 60) -ForegroundColor Cyan
    Write-Host ("  iter {0}: pass={1} warn={2} fail={3}{4}{5}" -f $i,$script:pass,$script:warn,$script:fail,$slowTag,$routeTag) `
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
