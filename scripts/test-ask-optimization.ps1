#Requires -Version 5.1
# ask optimization smoke test + benchmark + auto-suggest bug reporter
#
# Usage:
#   -Quick          BASIC: help/skill wiring only (~3s, no live calls)
#   -Full           FULL: live dispatch + final-answer wait per provider
#   -Benchmark      FULL + JSON timings + history append + baseline diff
#   -AutoSuggest    Auto-file wkappbot suggest for every threshold breach (use with -Benchmark)
#   -Baseline path  Compare against previous bench-results.json
#   -Loop N         Repeat Benchmark N times (default 1); use 0 for infinite
#   -IntervalSec N  Seconds between loop iterations (default 120)
#
# Speed targets (CLI-parity goal):
#   Dispatch:   WARN > 500ms   BUG > 1500ms
#   Completion: WARN > 20s     BUG > 60s     HANG > FinalAnswerTimeoutSec (default 90s)
#
# Exit codes: 0=pass  1=fail  2=slow-but-no-hard-fail

param(
    [switch]$Quick,
    [switch]$Full,
    [switch]$Benchmark,
    [switch]$AutoSuggest,
    [string]$Baseline,
    [string]$ResultsPath     = 'bench-results.json',
    [string]$HistoryPath     = 'bench-history.jsonl',
    [int]$DispatchWarnMs     = 500,
    [int]$DispatchBugMs      = 1500,
    [int]$CompletionWarnSec  = 20,
    [int]$CompletionBugSec   = 60,
    [int]$FinalAnswerTimeoutSec = 90,
    [int]$DispatchRegressionMs  = 500,
    [int]$CompletionRegressionMs = 5000,
    [int]$Loop               = 1,
    [int]$IntervalSec        = 120
)

$ErrorActionPreference = 'Continue'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$repoBin  = Join-Path $repoRoot 'bin'
$wkExe    = Join-Path $repoBin 'wkappbot.exe'
$coreExe  = Join-Path $repoBin 'wkappbot-core.exe'

if (!(Test-Path $wkExe))   { throw "bin/wkappbot.exe missing" }
if (!(Test-Path $coreExe)) { throw "bin/wkappbot-core.exe missing" }

$env:PATH = "$repoBin;$env:PATH"

$isCI    = $env:GITHUB_ACTIONS -eq 'true' -or $env:CI -eq 'true'
$runFull = ($Full -or $Benchmark) -and -not $Quick

# Providers to test
$providers = @('claude', 'gpt', 'gemini', 'triad')

# Per-provider timings (ms) — reset each loop iteration
$script:dispatchMs   = @{}
$script:completionMs = @{}
$script:hangProviders = [System.Collections.Generic.List[string]]::new()
$script:slowProviders = [System.Collections.Generic.List[string]]::new()

$script:pass = 0
$script:warn = 0
$script:fail = 0
$script:slowFail = $false

$askPrompt = "In 2 bullets: (1) today's key market signal, (2) confirm ask routing is healthy. English only."

# ── Helpers ────────────────────────────────────────────────────────────────

function Section([string]$title) {
    Write-Host ""
    Write-Host ("-" * 60) -ForegroundColor DarkGray
    Write-Host "  $title" -ForegroundColor Cyan
    Write-Host ("-" * 60) -ForegroundColor DarkGray
}

function Write-FlushLine([string]$text, [ConsoleColor]$color = [ConsoleColor]::Gray) {
    Write-Host $text -ForegroundColor $color
    [Console]::Out.Flush()
}

function Invoke-Cmd([string]$label, [string[]]$cmd, [int]$expect = 0, [switch]$Soft, [string]$TimingKey) {
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    Write-Host "`n=== $label ==="
    Write-Host ("  wkappbot {0}" -f ($cmd -join ' '))
    $lines = @(& wkappbot @cmd 2>&1)
    $code  = $LASTEXITCODE
    $sw.Stop()
    $elapsed = [int]$sw.Elapsed.TotalMilliseconds

    if ($TimingKey) {
        $script:dispatchMs[$TimingKey] = $elapsed
        # Classify dispatch speed
        $dispColor = if ($elapsed -gt $DispatchBugMs) { 'Red' }
                     elseif ($elapsed -gt $DispatchWarnMs) { 'Yellow' }
                     else { 'Green' }
        Write-Host ("  dispatch={0}ms" -f $elapsed) -ForegroundColor $dispColor
        if ($elapsed -gt $DispatchBugMs) {
            $script:slowProviders.Add($TimingKey)
            $script:slowFail = $true
        }
    }

    $shown = [Math]::Min($lines.Count, 6)
    if ($shown -gt 0) { $lines[0..($shown-1)] | ForEach-Object { Write-Host "  $_" } }
    if ($lines.Count -gt 6) { Write-Host "  ... ($($lines.Count - 6) more)" }
    Write-Host "  exit=$code  elapsed={0}ms" -f $elapsed

    if ($code -ne $expect) {
        if ($Soft) { $script:warn++; Write-Host "  [WARN] expected $expect got $code" -ForegroundColor Yellow }
        else       { $script:fail++; Write-Host "  [FAIL] expected $expect got $code" -ForegroundColor Red }
    } else { $script:pass++ }

    [Console]::Out.Flush()
    return $lines
}

function Get-LogPathFromLines([string[]]$lines) {
    foreach ($line in $lines) {
        if ($line -match '^Log:\s+(?<path>.+\.log)$') { return $Matches.path.Trim() }
    }
    return $null
}

function Wait-AskFinalBlocks([hashtable]$logMap, [hashtable]$dispatchStartUtc, [int]$TimeoutSec) {
    $deadline    = [DateTime]::UtcNow.AddSeconds($TimeoutSec)
    $opts        = [System.Text.RegularExpressions.RegexOptions]::Singleline
    $results     = @{}
    $lastReport  = @{}
    foreach ($k in $logMap.Keys) { $lastReport[$k] = [DateTime]::UtcNow }

    while ($logMap.Count -gt 0 -and [DateTime]::UtcNow -lt $deadline) {
        foreach ($key in @($logMap.Keys)) {
            $logPath = $logMap[$key]
            if (-not (Test-Path $logPath)) { continue }

            $raw = Get-Content -Path $logPath -Raw -ErrorAction SilentlyContinue
            if ($raw) {
                foreach ($pat in @('\[ASK_FULL_ANSWER_BEGIN\](?<body>.*?)\[ASK_FULL_ANSWER_END\]',
                                   '\[ASK_ANSWER_BEGIN\](?<body>.*?)\[ASK_ANSWER_END\]')) {
                    $ml = [regex]::Matches($raw, $pat, $opts)
                    if ($ml.Count -gt 0) {
                        $body = $ml[$ml.Count-1].Groups['body'].Value.Trim()
                        if ($body -match 'TITLE:' -and $body -match 'FILE_TITLE:') {
                            $results[$key] = $body
                            $totalMs = [int]([DateTime]::UtcNow - $dispatchStartUtc[$key]).TotalMilliseconds
                            $script:completionMs[$key] = $totalMs
                            $totalSec = [math]::Round($totalMs / 1000, 1)
                            $cColor = if ($totalSec -gt $CompletionBugSec) { 'Red' }
                                      elseif ($totalSec -gt $CompletionWarnSec) { 'Yellow' }
                                      else { 'Green' }
                            Write-FlushLine ("  [$key] done chars=$($body.Length) total={0}s" -f $totalSec) $cColor
                            if ($totalSec -gt $CompletionBugSec) { $script:slowProviders.Add("completion:$key") }
                            $logMap.Remove($key)
                            break
                        }
                    }
                }
            }

            $sinceReport = ([DateTime]::UtcNow - $lastReport[$key]).TotalSeconds
            if ($sinceReport -ge 10) {
                $elapsed = [int]([DateTime]::UtcNow - $dispatchStartUtc[$key]).TotalSeconds
                Write-FlushLine ("  [$key] waiting... {0}s elapsed" -f $elapsed) DarkYellow
                $lastReport[$key] = [DateTime]::UtcNow
            }
        }
        Start-Sleep -Milliseconds 500
    }

    foreach ($key in @($logMap.Keys)) {
        $script:fail++
        $script:hangProviders.Add($key)
        Write-FlushLine ("  [HANG] $key no answer after ${TimeoutSec}s") Red
    }
    return $results
}

function Assert-Match([string]$label, [string[]]$lines, [string[]]$patterns, [switch]$Soft) {
    $missing = @()
    foreach ($p in $patterns) { if (-not ($lines | Select-String -Pattern $p)) { $missing += $p } }
    if ($missing.Count -eq 0) {
        $script:pass++
        Write-Host "  [OK] $label" -ForegroundColor Green
    } else {
        if ($Soft) { $script:warn++; Write-Host "  [WARN] $label missing: $($missing -join ', ')" -ForegroundColor Yellow }
        else       { $script:fail++; Write-Host "  [FAIL] $label missing: $($missing -join ', ')" -ForegroundColor Red }
    }
}

function File-Suggest([string]$title, [string]$detail, [string]$req1, [string]$exp1) {
    if (-not $AutoSuggest -or $isCI) { return }
    Write-FlushLine "  [SUGGEST] filing: $title" DarkYellow
    $req = "$req1 => $exp1"
    & wkappbot suggest $title `
        --requirement $req `
        --requirement "wkappbot ask --help --no-regression => ask gpt" `
        --requirement "wkappbot skill read ask-command-cheatsheet => SINGLE" 2>&1 | Out-Null
}

function Compare-Baseline([string]$path) {
    if (-not (Test-Path $path)) {
        Write-Host "  [INFO] baseline not found: $path" -ForegroundColor DarkYellow; return
    }
    $base = Get-Content -Raw $path | ConvertFrom-Json
    $regressions = @()
    foreach ($k in $providers) {
        $curD  = $script:dispatchMs[$k]
        $curC  = $script:completionMs[$k]
        $baseD = if ($base.dispatchMs.PSObject.Properties[$k])   { $base.dispatchMs.$k }   else { $null }
        $baseC = if ($base.completionMs.PSObject.Properties[$k]) { $base.completionMs.$k } else { $null }
        if ($curD -and $baseD -and ($curD - $baseD) -gt $DispatchRegressionMs)
            { $regressions += "dispatch.$k +$($curD-$baseD)ms vs baseline" }
        if ($curC -and $baseC -and ($curC - $baseC) -gt $CompletionRegressionMs)
            { $regressions += "completion.$k +$([int](($curC-$baseC)/1000))s vs baseline" }
    }
    if ($regressions.Count -gt 0) {
        Write-Host "  [REGRESSION]" -ForegroundColor Red
        $regressions | ForEach-Object {
            Write-Host "    $_" -ForegroundColor Red
            File-Suggest "ask regression: $_" "Detected vs baseline $path" "wkappbot ask gpt health-check" "routing"
        }
        $script:fail += $regressions.Count
    } else {
        Write-Host "  [BASELINE OK] no regression" -ForegroundColor Green
        $script:pass++
    }
}

function Write-BenchmarkSummary([int]$iteration) {
    Section "Benchmark Summary (iter $iteration)"

    # Dispatch
    Write-Host "Dispatch latency:" -ForegroundColor Cyan
    foreach ($k in $providers) {
        if ($script:dispatchMs.ContainsKey($k)) {
            $ms = $script:dispatchMs[$k]
            $tag = if ($ms -gt $DispatchBugMs) { ' ← BUG' } elseif ($ms -gt $DispatchWarnMs) { ' ← SLOW' } else { '' }
            $c   = if ($ms -gt $DispatchBugMs) { 'Red' } elseif ($ms -gt $DispatchWarnMs) { 'Yellow' } else { 'Green' }
            Write-Host ("  {0,-8} {1,5}ms{2}" -f $k, $ms, $tag) -ForegroundColor $c
        }
    }
    # Completion
    Write-Host "`nCompletion time:" -ForegroundColor Cyan
    foreach ($k in $providers) {
        if ($script:completionMs.ContainsKey($k)) {
            $ms  = $script:completionMs[$k]
            $sec = [math]::Round($ms / 1000, 1)
            $tag = if ($sec -gt $CompletionBugSec) { ' ← BUG' } elseif ($sec -gt $CompletionWarnSec) { ' ← SLOW' } else { '' }
            $c   = if ($sec -gt $CompletionBugSec) { 'Red' } elseif ($sec -gt $CompletionWarnSec) { 'Yellow' } else { 'Green' }
            Write-Host ("  {0,-8} {1,6}s{2}" -f $k, $sec, $tag) -ForegroundColor $c
        }
    }

    # Hangs
    if ($script:hangProviders.Count -gt 0) {
        Write-Host "`n  [HANG] $($script:hangProviders -join ', ')" -ForegroundColor Red
        foreach ($h in $script:hangProviders) {
            File-Suggest "ask hang: $h no response >${FinalAnswerTimeoutSec}s" `
                "Provider $h returned no [ASK_FULL_ANSWER_END] within ${FinalAnswerTimeoutSec}s" `
                "wkappbot ask $h health-check" "routing"
        }
    }

    # Slow providers suggest
    foreach ($s in $script:slowProviders) {
        if ($s -match '^completion:(.+)') {
            $prov = $Matches[1]
            File-Suggest "ask slow: $prov completion >${CompletionBugSec}s" `
                "Completion time $($script:completionMs[$prov])ms exceeds ${CompletionBugSec}s bug threshold" `
                "wkappbot ask $prov health-check" "routing"
        } else {
            File-Suggest "ask slow dispatch: $s >${DispatchBugMs}ms" `
                "Dispatch $($script:dispatchMs[$s])ms exceeds ${DispatchBugMs}ms bug threshold" `
                "wkappbot ask $s health-check" "routing"
        }
    }

    # Save results JSON
    $commit  = (& git -C $repoRoot rev-parse --short HEAD 2>$null)
    $resObj  = [ordered]@{
        timestamp_utc  = [DateTime]::UtcNow.ToString('o')
        commit         = $commit
        iteration      = $iteration
        thresholds     = [ordered]@{
            dispatchWarnMs    = $DispatchWarnMs
            dispatchBugMs     = $DispatchBugMs
            completionWarnSec = $CompletionWarnSec
            completionBugSec  = $CompletionBugSec
            hangSec           = $FinalAnswerTimeoutSec
        }
        dispatchMs     = $script:dispatchMs
        completionMs   = $script:completionMs
        hangs          = @($script:hangProviders)
        slow           = @($script:slowProviders)
        pass           = $script:pass
        warn           = $script:warn
        fail           = $script:fail
    }
    $resJson = $resObj | ConvertTo-Json -Depth 5

    # Write latest results
    $resAbs = if ([System.IO.Path]::IsPathRooted($ResultsPath)) { $ResultsPath } else { Join-Path $repoRoot $ResultsPath }
    Set-Content -Path $resAbs -Value $resJson -Encoding UTF8

    # Append to history JSONL
    $histAbs = if ([System.IO.Path]::IsPathRooted($HistoryPath)) { $HistoryPath } else { Join-Path $repoRoot $HistoryPath }
    Add-Content -Path $histAbs -Value ($resObj | ConvertTo-Json -Depth 5 -Compress) -Encoding UTF8

    Write-Host "`n  results  -> $resAbs" -ForegroundColor DarkGray
    Write-Host "  history  -> $histAbs" -ForegroundColor DarkGray

    if ($Baseline) {
        $baseAbs = if ([System.IO.Path]::IsPathRooted($Baseline)) { $Baseline } else { Join-Path $repoRoot $Baseline }
        Compare-Baseline $baseAbs
    }
}

# ── One test run ────────────────────────────────────────────────────────────

function Invoke-OneRun([int]$iteration) {
    # Reset per-run state
    $script:dispatchMs    = @{}
    $script:completionMs  = @{}
    $script:hangProviders = [System.Collections.Generic.List[string]]::new()
    $script:slowProviders = [System.Collections.Generic.List[string]]::new()
    $script:pass = 0; $script:warn = 0; $script:fail = 0; $script:slowFail = $false

    Write-Host ""
    Write-Host "=== ask optimization smoke (iter $iteration) ===" -ForegroundColor Cyan
    Write-Host "Mode      : $(if ($Benchmark) { 'BENCHMARK' } elseif ($runFull) { 'FULL' } else { 'BASIC' })"
    Write-Host "AutoSuggest: $AutoSuggest"
    Write-Host "Thresholds : dispatch warn=${DispatchWarnMs}ms bug=${DispatchBugMs}ms | completion warn=${CompletionWarnSec}s bug=${CompletionBugSec}s hang=${FinalAnswerTimeoutSec}s"

    # ── BASIC checks ────────────────────────────────────────────────────────
    Section "Baseline"
    $skillSearch = Invoke-Cmd 'skill-search' @('skill', 'search', 'ask', '--app', 'wkappbot-workflow')
    Assert-Match 'skill search registered' $skillSearch @('ask-command-cheatsheet', 'ask-command-optimization-tests')

    $skillFile = Join-Path $repoRoot 'skills/wkappbot-workflow/ask-command-optimization-tests.skill.json'
    if (Test-Path $skillFile) {
        $sfText = Get-Content $skillFile -Raw
        Assert-Match 'regression skill file' @($sfText) @('ask command optimization smoke tests', 'scripts/test-ask-optimization.ps1')
    } else {
        $script:warn++
        Write-Host "  [WARN] skill file missing: $skillFile" -ForegroundColor Yellow
    }

    $helpOut = Invoke-Cmd 'ask-help' @('ask', '--help', '--no-regression')
    Assert-Match 'ask help routes' $helpOut @('ask gpt', 'ask triad', 'Ask AI via CDP')

    if (-not $runFull) {
        Write-Host "`n[INFO] BASIC mode -- skipping live provider checks." -ForegroundColor DarkYellow
        return
    }

    # ── Live provider dispatch ──────────────────────────────────────────────
    Section "Provider Dispatch"

    $dispatches = @(
        @{ Name='claude'; Cmd=@('ask','claude',$askPrompt); Patterns=@('dispatched to background','ask-claude') },
        @{ Name='gpt';    Cmd=@('ask','gpt',   $askPrompt); Patterns=@('dispatched to background','ask-gpt')    },
        @{ Name='gemini'; Cmd=@('ask','gemini',$askPrompt); Patterns=@('dispatched to background','ask-gemini') }
    )

    $logMap          = @{}
    $dispatchStartUtc = @{}

    foreach ($item in $dispatches) {
        $name = [string]$item.Name
        $dispatchStartUtc[$name] = [DateTime]::UtcNow
        $out  = Invoke-Cmd "ask-$name" $item.Cmd -TimingKey $name
        Assert-Match "$name dispatch" $out $item.Patterns
        $lp = Get-LogPathFromLines $out
        if ($lp) { $logMap[$name] = $lp; Write-FlushLine "  [$name] log=$lp" DarkGray }
        else     { $script:fail++; Write-FlushLine "  [FAIL] $name log path missing" Red }
    }

    Section "Triad"
    $dispatchStartUtc['triad'] = [DateTime]::UtcNow
    $triadOut = Invoke-Cmd 'ask-triad' @('ask','triad',$askPrompt) -TimingKey 'triad'
    Assert-Match 'triad dispatch' $triadOut @('dispatched to background','ask-triad')
    $triadLog = Get-LogPathFromLines $triadOut
    if ($triadLog) { $logMap['triad'] = $triadLog; Write-FlushLine "  [triad] log=$triadLog" DarkGray }
    else           { $script:fail++;               Write-FlushLine "  [FAIL] triad log missing" Red }

    Section "Final Answers (timeout=${FinalAnswerTimeoutSec}s)"
    $answers = Wait-AskFinalBlocks $logMap $dispatchStartUtc -TimeoutSec $FinalAnswerTimeoutSec

    foreach ($key in $providers) {
        if ($answers.ContainsKey($key)) {
            Assert-Match "$key answer content" @($answers[$key]) @('TITLE:','FILE_TITLE:')
        }
    }

    if ($Benchmark) { Write-BenchmarkSummary $iteration }
}

# ── Main loop ───────────────────────────────────────────────────────────────

$totalIterations = if ($Loop -le 0) { [int]::MaxValue } else { $Loop }
$globalPass = 0; $globalFail = 0

for ($i = 1; $i -le $totalIterations; $i++) {
    Invoke-OneRun -iteration $i
    $globalPass += $script:pass
    $globalFail += $script:fail

    Write-Host ""
    Write-Host ("=" * 60) -ForegroundColor Cyan
    $slowLabel = if ($script:slowFail) { " | slow=$($script:slowProviders.Count)" } else { "" }
    $hangLabel = if ($script:hangProviders.Count -gt 0) { " | HANGS=$($script:hangProviders.Count)" } else { "" }
    Write-Host ("  iter {0}: pass={1} warn={2} fail={3}{4}{5}" -f $i, $script:pass, $script:warn, $script:fail, $slowLabel, $hangLabel) -ForegroundColor Cyan
    Write-Host ("=" * 60) -ForegroundColor Cyan

    if ($i -lt $totalIterations -and $runFull) {
        Write-Host "`n  next iteration in ${IntervalSec}s..." -ForegroundColor DarkGray
        Start-Sleep -Seconds $IntervalSec
    }
}

Write-Host ""
Write-Host ("=" * 60) -ForegroundColor Cyan
Write-Host ("  TOTAL: pass={0} fail={1}" -f $globalPass, $globalFail) -ForegroundColor $(if ($globalFail -gt 0) { 'Red' } else { 'Green' })
Write-Host ("=" * 60) -ForegroundColor Cyan

if ($globalFail -gt 0) { exit 1 }
if ($script:slowFail)  { exit 2 }
exit 0
