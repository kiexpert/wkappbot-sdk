#Requires -Version 5.1
param(
    [switch]$Quick,
    [switch]$Full
)

$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$repoBin  = Join-Path $repoRoot 'bin'

if (!(Test-Path (Join-Path $repoBin 'wkappbot.exe'))) { throw "bin/wkappbot.exe missing" }
if (!(Test-Path (Join-Path $repoBin 'wkappbot-core.exe'))) { throw "bin/wkappbot-core.exe missing" }

$env:PATH = "$repoBin;$env:PATH"

function Write-Usage {
    Write-Host "Usage: powershell -ExecutionPolicy Bypass -File .\scripts\test-chat-slash-switch.ps1 [-Quick|-Full]"
    Write-Host "  -Quick : slash parse smoke"
    Write-Host "  -Full  : slash parse smoke + malformed cases"
}

function Invoke-SlashProbe([string]$line) {
    $psi = [System.Diagnostics.ProcessStartInfo]::new()
    $psi.FileName = Join-Path $repoBin 'wkappbot.exe'
    $psi.Arguments = 'chat --probe-switch'
    $psi.WorkingDirectory = $repoRoot
    $psi.UseShellExecute = $false
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $true
    $psi.CreateNoWindow = $true
    $psi.EnvironmentVariables['WKAPPBOT_CHAT_PROBE_LINE'] = $line

    $proc = [System.Diagnostics.Process]::Start($psi)
    if (-not $proc) { throw "failed to start wkappbot.exe" }
    $stdout = $proc.StandardOutput.ReadToEnd()
    $stderr = $proc.StandardError.ReadToEnd()
    $proc.WaitForExit()

    $lines = @()
    if ($stdout) { $lines += ($stdout -split "`r?`n" | Where-Object { $_ -ne '' }) }
    if ($stderr) { $lines += ($stderr -split "`r?`n" | Where-Object { $_ -ne '' }) }
    [pscustomobject]@{
        Code = $proc.ExitCode
        Lines = $lines
    }
}

function Assert-Contains([string]$label, [string[]]$lines, [string[]]$needles) {
    $missing = @()
    foreach ($needle in $needles) {
        if (-not ($lines | Select-String -SimpleMatch $needle)) {
            $missing += $needle
        }
    }
    if ($missing.Count -gt 0) {
        throw "$label missing: $($missing -join ', ')"
    }
    Write-Host "[OK] $label"
}

if (-not $Quick -and -not $Full) {
    Write-Usage
    exit 0
}

$cases = @(
    @{ Name = 'slash-codex'; Line = '/codex hand off to codex'; Needles = @('switch=1', 'next_cli=codex', 'prompt=hand off to codex') },
    @{ Name = 'slash-gemini'; Line = '/gemini investigate latest news'; Needles = @('switch=1', 'next_cli=gemini', 'prompt=investigate latest news') },
    @{ Name = 'slash-claude'; Line = '/claude'; Needles = @('switch=1', 'next_cli=claude', 'prompt=') }
)

if ($Full) {
    $cases += @(
        @{ Name = 'slash-gemini-web'; Line = '/gemini-web open browser'; Needles = @('switch=1', 'next_cli=gemini-web', 'prompt=open browser') },
        @{ Name = 'non-slash'; Line = 'plain text'; Needles = @('switch=0', 'line=plain text') }
    )
}

$total = 0
foreach ($case in $cases) {
    $total++
    Write-Host "`n=== $($case.Name) ==="
    $probe = Invoke-SlashProbe $case.Line
    Start-Sleep -Milliseconds 250
    Write-Host "exit=$($probe.Code)"
    $probe.Lines | ForEach-Object { Write-Host $_ }
    if ($probe.Code -ne 0) {
        throw "$($case.Name) exited with $($probe.Code)"
    }
    Assert-Contains $case.Name $probe.Lines $case.Needles
}

Write-Host "`nPASS: $total case(s)"
exit 0
