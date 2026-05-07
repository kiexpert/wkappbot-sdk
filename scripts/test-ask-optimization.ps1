#Requires -Version 5.1
# ask optimization smoke test + benchmark
# Usage:
#   powershell -File scripts/test-ask-optimization.ps1                  # BASIC: help/skill wiring
#   powershell -File scripts/test-ask-optimization.ps1 -Quick           # alias for BASIC
#   powershell -File scripts/test-ask-optimization.ps1 -Full            # FULL: dispatch + final answer
#   powershell -File scripts/test-ask-optimization.ps1 -Benchmark       # FULL + JSON timings
#   powershell -File scripts/test-ask-optimization.ps1 -Benchmark -Baseline bench-baseline.json
#       compare against baseline; fail if dispatch regression > -DispatchRegressionMs (default 500)
#       or completion regression > -CompletionRegressionMs (default 5000)
#
# Goal: tune `ask` to feel CLI-fast. Track dispatch latency (user-perceived) +
# completion time (provider end-to-end) per run; detect regressions early.

param(
    [switch]$Quick,
    [switch]$Full,
    [switch]$Benchmark,
    [string]$Baseline,
    [string]$ResultsPath = 'bench-results.json',
    [int]$DispatchRegressionMs = 500,
    [int]$CompletionRegressionMs = 5000,
    [int]$FinalAnswerTimeoutSec = 240
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

$pass = 0
$warn = 0
$fail = 0
$askPrompt = "Summarize today's news and market conditions in 2-3 bullets, then state whether the routing is healthy."

# Per-provider timings (ms)
$dispatchMs   = @{}
$completionMs = @{}

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
    $code = $LASTEXITCODE
    $sw.Stop()
    $elapsed = $sw.Elapsed.TotalMilliseconds
    if ($TimingKey) { $script:dispatchMs[$TimingKey] = [int]$elapsed }

    $shown = [Math]::Min($lines.Count, 6)
    if ($shown -gt 0) { $lines[0..($shown - 1)] | ForEach-Object { Write-Host "  $_" } }
    if ($lines.Count -gt 6) { Write-Host "  ... ($($lines.Count - 6) more)" }
    Write-Host "  exit=$code"
    if ($code -ne $expect) {
        if ($Soft) { $script:warn++; Write-Host "  [WARN] expected exit $expect, got $code" -ForegroundColor Yellow }
        else       { $script:fail++; Write-Host "  [FAIL] expected exit $expect, got $code" -ForegroundColor Red }
    } else {
        $script:pass++
    }
    Write-Host ("  elapsed={0:n0}ms" -f $elapsed) -ForegroundColor DarkGray
    [Console]::Out.Flush()
    return $lines
}

function Get-LogPathFromLines([string[]]$lines) {
    foreach ($line in $lines) {
        if ($line -match '^Log:\s+(?<path>.+\.log)$') {
            return $Matches.path.Trim()
        }
    }
    return $null
}

function Wait-AskFinalBlocks([hashtable]$logMap, [hashtable]$dispatchStartUtc, [int]$TimeoutSec = 240) {
    $deadline = [DateTime]::UtcNow.AddSeconds($TimeoutSec)
    $opts = [System.Text.RegularExpressions.RegexOptions]::Singleline
    $results = @{}
    $lastReport = @{}

    foreach ($key in $logMap.Keys) { $lastReport[$key] = [DateTime]::UtcNow }

    while ($logMap.Count -gt 0 -and [DateTime]::UtcNow -lt $deadline) {
        foreach ($key in @($logMap.Keys)) {
            $logPath = $logMap[$key]
            if (-not (Test-Path $logPath)) { continue }

            $raw = Get-Content -Path $logPath -Raw -ErrorAction SilentlyContinue
            if ($raw) {
                $candidates = @()
                foreach ($pattern in @('\[ASK_FULL_ANSWER_BEGIN\](?<body>.*?)\[ASK_FULL_ANSWER_END\]', '\[ASK_ANSWER_BEGIN\](?<body>.*?)\[ASK_ANSWER_END\]')) {
                    $matchList = [regex]::Matches($raw, $pattern, $opts)
                    if ($matchList.Count -gt 0) {
                        $body = $matchList[$matchList.Count - 1].Groups['body'].Value.Trim()
                        if ($body.Length -gt 0) { $candidates += $body }
                    }
                }

                foreach ($body in $candidates) {
                    if ($body -match 'TITLE:' -and $body -match 'FILE_TITLE:') {
                        $results[$key] = $body
                        if ($dispatchStartUtc.ContainsKey($key)) {
                            $totalMs = [int]([DateTime]::UtcNow - $dispatchStartUtc[$key]).TotalMilliseconds
                            $script:completionMs[$key] = $totalMs
                            Write-FlushLine ("  [$key] final answer ready; chars=$($body.Length); total={0:n0}ms" -f $totalMs) Green
                        } else {
                            Write-FlushLine ("  [$key] final answer ready; chars=$($body.Length)") Green
                        }
                        $logMap.Remove($key)
                        break
                    }
                }
            }

            $sinceReport = ([DateTime]::UtcNow - $lastReport[$key]).TotalSeconds
            if ($sinceReport -ge 5) {
                Write-FlushLine ("  [$key] waiting for final answer...") DarkYellow
                $lastReport[$key] = [DateTime]::UtcNow
            }
        }
        Start-Sleep -Milliseconds 500
    }

    foreach ($key in @($logMap.Keys)) {
        $script:fail++
        Write-FlushLine ("  [FAIL] $key final answer timed out after ${TimeoutSec}s") Red
    }

    return $results
}

function Assert-Match([string]$label, [string[]]$lines, [string[]]$patterns, [switch]$Soft) {
    $missing = @()
    foreach ($p in $patterns) {
        if (-not ($lines | Select-String -Pattern $p)) { $missing += $p }
    }
    if ($missing.Count -eq 0) {
        $script:pass++
        Write-Host "  [ASSERT OK] $label" -ForegroundColor Green
    } else {
        if ($Soft) { $script:warn++; Write-Host "  [WARN] $label -- missing: $($missing -join ', ')" -ForegroundColor Yellow }
        else       { $script:fail++; Write-Host "  [ASSERT FAIL] $label -- missing: $($missing -join ', ')" -ForegroundColor Red }
    }
}

function Compare-Baseline([string]$path, [int]$dispatchThresholdMs, [int]$completionThresholdMs) {
    if (-not (Test-Path $path)) {
        Write-Host "  [INFO] baseline not found: $path -- skipping comparison" -ForegroundColor DarkYellow
        return
    }
    $base = Get-Content -Raw -Path $path | ConvertFrom-Json
    $regressions = @()

    foreach ($k in @('claude', 'gpt', 'gemini', 'triad')) {
        $curD = $script:dispatchMs[$k]
        $curC = $script:completionMs[$k]
        $baseD = if ($base.dispatchMs.PSObject.Properties[$k]) { $base.dispatchMs.$k } else { $null }
        $baseC = if ($base.completionMs.PSObject.Properties[$k]) { $base.completionMs.$k } else { $null }

        if ($curD -and $baseD -and ($curD - $baseD) -gt $dispatchThresholdMs) {
            $regressions += "  [REGRESSION] dispatch.$k : $baseD -> $curD ms (+$($curD - $baseD)ms > ${dispatchThresholdMs}ms)"
        }
        if ($curC -and $baseC -and ($curC - $baseC) -gt $completionThresholdMs) {
            $regressions += "  [REGRESSION] completion.$k : $baseC -> $curC ms (+$($curC - $baseC)ms > ${completionThresholdMs}ms)"
        }
    }

    if ($regressions.Count -gt 0) {
        Write-Host ""
        Write-Host "Baseline regression check:" -ForegroundColor Red
        $regressions | ForEach-Object { Write-Host $_ -ForegroundColor Red }
        $script:fail += $regressions.Count
    } else {
        Write-Host "  [BASELINE OK] no regression vs $path" -ForegroundColor Green
        $script:pass++
    }
}

Write-Host "=== ask optimization smoke ==="
$mode = if ($Benchmark) { 'BENCHMARK' } elseif ($runFull) { 'FULL' } else { 'BASIC' }
Write-Host "Mode : $mode"
Write-Host "Path : $repoRoot"
if (-not $Quick -and -not $Full -and -not $Benchmark) {
    Write-Host "Usage: powershell -File scripts/test-ask-optimization.ps1 [-Quick] [-Full] [-Benchmark] [-Baseline path.json]"
    Write-Host "  BASIC mode (default) checks help, registration, and source wiring."
    Write-Host "  FULL mode also performs live provider routing checks."
    Write-Host "  BENCHMARK mode adds dispatch+completion timings + optional baseline diff."
}

Section "Baseline"
$skillSearch = Invoke-Cmd 'skill-search' @('skill', 'search', 'ask', '--app', 'wkappbot-workflow')
Assert-Match 'skill search registered' $skillSearch @('ask-command-cheatsheet', 'ask-command-optimization-tests')

$skillFile = Join-Path $repoRoot 'skills/wkappbot-workflow/ask-command-optimization-tests.skill.json'
if (Test-Path $skillFile) {
    $skillFileText = Get-Content $skillFile -Raw
    Assert-Match 'ask regression skill file' @($skillFileText) @('ask command optimization smoke tests', 'scripts/test-ask-optimization.ps1')
} else {
    $script:warn++
    Write-Host "  [WARN] skill file missing: $skillFile" -ForegroundColor Yellow
}

$helpOut = Invoke-Cmd 'ask-help' @('ask', '--help', '--no-regression')
Assert-Match 'ask help routes' $helpOut @('ask gpt', 'ask triad', 'Ask AI via CDP')

if (-not $runFull) {
    Write-Host ""
    Write-Host "[INFO] BASIC mode -- skipping provider dispatch checks." -ForegroundColor DarkYellow
} else {
    Section "Provider Routing"

    $dispatches = @(
        @{ Name = 'claude'; Cmd = @('ask', 'claude', $askPrompt); Patterns = @('dispatched to background', 'ask-claude') },
        @{ Name = 'gpt';    Cmd = @('ask', 'gpt',    $askPrompt); Patterns = @('dispatched to background', 'ask-gpt')    },
        @{ Name = 'gemini'; Cmd = @('ask', 'gemini', $askPrompt); Patterns = @('dispatched to background', 'ask-gemini') }
    )

    $logMap = @{}
    $dispatchStartUtc = @{}
    foreach ($item in $dispatches) {
        $name = [string]$item.Name
        $dispatchStartUtc[$name] = [DateTime]::UtcNow
        $out = Invoke-Cmd "ask-$name" $item.Cmd -TimingKey $name
        Assert-Match "$name dispatch" $out $item.Patterns
        $logPath = Get-LogPathFromLines $out
        if ($logPath) {
            $logMap[$name] = $logPath
            Write-FlushLine ("  [$name] log=$logPath") DarkGray
        } else {
            $script:fail++
            Write-FlushLine ("  [FAIL] $name log path missing") Red
        }
    }

    Section "Triad"

    $dispatchStartUtc['triad'] = [DateTime]::UtcNow
    $triadOut = Invoke-Cmd 'ask-triad' @('ask', 'triad', $askPrompt) -TimingKey 'triad'
    Assert-Match 'triad dispatch' $triadOut @('dispatched to background', 'ask-triad')
    $triadLog = Get-LogPathFromLines $triadOut
    if ($triadLog) {
        $logMap['triad'] = $triadLog
        Write-FlushLine ("  [triad] log=$triadLog") DarkGray
    } else {
        $script:fail++
        Write-FlushLine "  [FAIL] triad log path missing" Red
    }

    Section "Final Answers"
    $answers = Wait-AskFinalBlocks $logMap $dispatchStartUtc -TimeoutSec $FinalAnswerTimeoutSec
    foreach ($key in @('claude', 'gpt', 'gemini', 'triad')) {
        if ($answers.ContainsKey($key)) {
            Assert-Match "$key final answer" @($answers[$key]) @('TITLE:', 'FILE_TITLE:')
        }
    }

    if ($Benchmark) {
        Section "Benchmark Summary"
        Write-Host "Dispatch latency (ms) -- user-perceived speed:" -ForegroundColor Cyan
        foreach ($k in @('claude', 'gpt', 'gemini', 'triad')) {
            if ($script:dispatchMs.ContainsKey($k)) {
                Write-Host ("  {0,-8} {1,6}ms" -f $k, $script:dispatchMs[$k])
            }
        }
        Write-Host ""
        Write-Host "Completion time (ms) -- end-to-end:" -ForegroundColor Cyan
        foreach ($k in @('claude', 'gpt', 'gemini', 'triad')) {
            if ($script:completionMs.ContainsKey($k)) {
                Write-Host ("  {0,-8} {1,6}ms" -f $k, $script:completionMs[$k])
            }
        }

        $resultsObj = [ordered]@{
            timestamp_utc          = [DateTime]::UtcNow.ToString('o')
            commit                 = (& git -C $repoRoot rev-parse --short HEAD 2>$null)
            mode                   = $mode
            prompt_length          = $askPrompt.Length
            final_answer_timeout_s = $FinalAnswerTimeoutSec
            dispatchMs             = $script:dispatchMs
            completionMs           = $script:completionMs
        }
        $resultsJson = $resultsObj | ConvertTo-Json -Depth 4
        $resultsAbs = if ([System.IO.Path]::IsPathRooted($ResultsPath)) { $ResultsPath } else { Join-Path $repoRoot $ResultsPath }
        Set-Content -Path $resultsAbs -Value $resultsJson -Encoding UTF8
        Write-Host ""
        Write-Host "  results -> $resultsAbs" -ForegroundColor DarkGray

        if ($Baseline) {
            $baselineAbs = if ([System.IO.Path]::IsPathRooted($Baseline)) { $Baseline } else { Join-Path $repoRoot $Baseline }
            Compare-Baseline $baselineAbs $DispatchRegressionMs $CompletionRegressionMs
        }
    }
}

Write-Host ""
Write-Host "============================================================" -ForegroundColor Cyan
Write-Host "  ask optimization smoke: $pass passed, $warn warned, $fail failed"
Write-Host "============================================================" -ForegroundColor Cyan

if ($fail -gt 0) { exit 1 }
exit 0
