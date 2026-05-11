#Requires -Version 5.1
# Triad synthesis smoke test
#
# Usage:
#   powershell -ExecutionPolicy Bypass -File .\scripts\test-triad-synthesis.ps1
#   powershell -ExecutionPolicy Bypass -File .\scripts\test-triad-synthesis.ps1 -Quick
#   powershell -ExecutionPolicy Bypass -File .\scripts\test-triad-synthesis.ps1 -Full
#
# QUICK:
#   Help/routing wiring only.
# FULL:
#   Live ask triad smoke that waits for all 3 providers plus synthesis markers.

param(
    [switch]$Quick,
    [switch]$Full,
    [int]$TimeoutSec = 180,
    [int]$PollSec = 3
)

$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$repoBin  = Join-Path $repoRoot 'bin'
$wkExe    = Join-Path $repoBin 'wkappbot.exe'

function Write-Usage {
    Write-Host "Usage: powershell -ExecutionPolicy Bypass -File .\scripts\test-triad-synthesis.ps1 [-Quick|-Full]"
    Write-Host "  -Quick : command wiring / help smoke only"
    Write-Host "  -Full  : live triad synthesis smoke (waits for final synthesis markers)"
}

function Section([string]$title) {
    Write-Host ""
    Write-Host ("-" * 72) -ForegroundColor DarkGray
    Write-Host "  $title" -ForegroundColor Cyan
    Write-Host ("-" * 72) -ForegroundColor DarkGray
}

function Invoke-Wk([string[]]$CommandArgs) {
    $stdout = Join-Path $env:TEMP ("triad-smoke-{0}.out.log" -f ([guid]::NewGuid().ToString('N')))
    $stderr = Join-Path $env:TEMP ("triad-smoke-{0}.err.log" -f ([guid]::NewGuid().ToString('N')))
    try {
        $proc = Start-Process -FilePath $wkExe `
            -ArgumentList $CommandArgs `
            -WorkingDirectory $repoRoot `
            -RedirectStandardOutput $stdout `
            -RedirectStandardError $stderr `
            -WindowStyle Hidden `
            -PassThru

        $proc.WaitForExit()
        $proc.Refresh()
        $exitCode = 0
        try {
            $exitCode = [int]$proc.ExitCode
        } catch {
            $exitCode = 0
        }

        $lines = @()
        if (Test-Path $stdout) {
            $outText = Get-Content -Raw -Path $stdout -ErrorAction SilentlyContinue
            if (-not [string]::IsNullOrWhiteSpace($outText)) {
                $lines += ($outText -split "`r?`n" | Where-Object { $_ -ne '' })
            }
        }
        if (Test-Path $stderr) {
            $errText = Get-Content -Raw -Path $stderr -ErrorAction SilentlyContinue
            if (-not [string]::IsNullOrWhiteSpace($errText)) {
                $lines += ($errText -split "`r?`n" | Where-Object { $_ -ne '' })
            }
        }

        return @{
            Lines = @($lines)
            Code  = $exitCode
        }
    }
    finally {
        Remove-Item $stdout, $stderr -ErrorAction SilentlyContinue
    }
}

function Get-TriadPrompt {
    return "Check triad synthesis. Use exactly these sections: Points of consensus, Key disagreements & resolution, Final answer. Keep them concise. End with TRIAD_OK."
}

function Get-LogPathFromOutput([string[]]$lines) {
    $logLine = $lines | Where-Object { $_ -match '^Log:\s+' } | Select-Object -First 1
    if (-not $logLine) { return $null }
    if ($logLine -match '^Log:\s+(.+\.log)') { return $Matches[1].Trim() }
    return $null
}

function Test-LogReady([string]$path) {
    if (-not (Test-Path $path)) { return $false }
    $raw = Get-Content -Raw -Path $path -ErrorAction SilentlyContinue
    if ([string]::IsNullOrWhiteSpace($raw)) { return $false }

    $needles = @(
        '[claude]',
        '[gpt]',
        '[gemini]',
        'Points of consensus:',
        'Key disagreements & resolution:',
        'Final answer:',
        'TRIAD_OK'
    )

    foreach ($needle in $needles) {
        if ($raw -notmatch [regex]::Escape($needle)) {
            return $false
        }
    }

    return $true
}

function Show-LogSummary([string]$path) {
    $raw = Get-Content -Raw -Path $path -ErrorAction SilentlyContinue
    if ([string]::IsNullOrWhiteSpace($raw)) { return }

    Write-Host "`n--- log tail ---" -ForegroundColor DarkGray
    $tail = Get-Content -Path $path -Tail 40 -ErrorAction SilentlyContinue
    if ($tail) { $tail | ForEach-Object { Write-Host $_ } }
}

if (-not $Quick -and -not $Full) {
    Write-Usage
    exit 0
}

if (!(Test-Path $wkExe)) { throw "bin/wkappbot.exe missing" }
if (!(Test-Path (Join-Path $repoBin 'wkappbot-core.exe'))) { throw "bin/wkappbot-core.exe missing" }

$env:PATH = "$repoBin;$env:PATH"

if ($Quick) {
    Section 'Quick triad wiring smoke'
    $probe = Invoke-Wk @('ask','triad','smoke: respond with TRIAD_OK')
    Write-Host ("exit={0}" -f $probe.Code)
    $probe.Lines | ForEach-Object { Write-Host $_ }
    if ($probe.Code -ne 0) { throw "wkappbot ask triad dispatch exited with $($probe.Code)" }
    if (-not ($probe.Lines | Select-String -Pattern 'dispatched to background')) {
        throw "triad dispatch smoke missing dispatched signal"
    }
    if (-not (Get-LogPathFromOutput $probe.Lines)) {
        throw "triad dispatch smoke missing log path"
    }
    Write-Host "PASS"
    exit 0
}

Section 'Live triad synthesis smoke'
$prompt = Get-TriadPrompt

$start = [System.Diagnostics.Stopwatch]::StartNew()
$run = Invoke-Wk @('ask','triad',$prompt)
$start.Stop()

Write-Host ("dispatch_exit={0} dispatch_elapsed={1}ms" -f $run.Code, [int]$start.Elapsed.TotalMilliseconds)
if ($run.Lines) {
    $run.Lines | ForEach-Object { Write-Host $_ }
}
if ($run.Code -ne 0) {
    throw "wkappbot ask triad exited with $($run.Code)"
}

$logPath = Get-LogPathFromOutput $run.Lines
if (-not $logPath) {
    throw "triad dispatch did not emit a log path"
}

Write-Host ("log={0}" -f $logPath)

$deadline = [DateTime]::UtcNow.AddSeconds($TimeoutSec)
$ready = $false
while ([DateTime]::UtcNow -lt $deadline) {
    if (Test-LogReady $logPath) {
        $ready = $true
        break
    }
    Start-Sleep -Seconds $PollSec
}

if (-not $ready) {
    Show-LogSummary $logPath
    throw "triad synthesis did not complete within ${TimeoutSec}s"
}

Show-LogSummary $logPath
Write-Host ""
Write-Host "PASS: triad synthesis completed" -ForegroundColor Green
exit 0
