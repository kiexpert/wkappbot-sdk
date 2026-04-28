# wkappbot-sdk setup.ps1 -- idempotent first-run installer for SDK customers.
#
# Responsibilities:
#   1. Ensure .wkappbot/config.json exists (prompt for slack_webhook_url + slack_channel
#      on first run; skip on subsequent runs).
#   2. Add the wkappbot bin\ folder to the user PATH (skip if already present).
#   3. Verify the Eye is reachable via `wkappbot eye tick`.
#
# Re-running this script after a successful first run is a no-op except for the
# Eye verification probe.

#Requires -Version 5.1
[CmdletBinding()]
param(
    [string] $BinDir,
    [switch] $NonInteractive
)

$ErrorActionPreference = 'Stop'

function Write-Ok    { param($Msg) Write-Host "[OK] $Msg" -ForegroundColor Green }
function Write-Skip  { param($Msg) Write-Host "[SKIP] $Msg" -ForegroundColor DarkGray }
function Write-Step  { param($Msg) Write-Host "[..] $Msg" -ForegroundColor Cyan }
function Write-Warn2 { param($Msg) Write-Host "[WARN] $Msg" -ForegroundColor Yellow }
function Write-Err   { param($Msg) Write-Host "[FAIL] $Msg" -ForegroundColor Red }

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path

# Resolve the bin directory containing wkappbot.exe / wkappbot-core.exe.
# Priority:
#   1. -BinDir parameter
#   2. <scriptRoot>\bin   (default SDK layout)
#   3. wkappbot already on PATH (use the resolved exe's directory)
function Resolve-BinDir {
    param([string] $Hint)

    if ($Hint) {
        if (Test-Path -LiteralPath $Hint -PathType Container) {
            return (Resolve-Path -LiteralPath $Hint).Path
        }
        Write-Warn2 "-BinDir '$Hint' does not exist; falling back to defaults"
    }

    $candidate = Join-Path $scriptRoot 'bin'
    if (Test-Path -LiteralPath $candidate -PathType Container) {
        $exeProbe = Join-Path $candidate 'wkappbot.exe'
        if (Test-Path -LiteralPath $exeProbe -PathType Leaf) {
            return (Resolve-Path -LiteralPath $candidate).Path
        }
    }

    $cmd = Get-Command wkappbot -ErrorAction SilentlyContinue
    if ($cmd) {
        return (Split-Path -Parent $cmd.Source)
    }

    return $null
}

$binDirResolved = Resolve-BinDir -Hint $BinDir
if (-not $binDirResolved) {
    Write-Err "Could not locate wkappbot bin directory. Pass -BinDir <path> or run from the SDK root."
    exit 1
}
Write-Ok "bin dir: $binDirResolved"

# ---------------------------------------------------------------- 1. config.json
$configDir  = Join-Path $scriptRoot '.wkappbot'
$configPath = Join-Path $configDir  'config.json'

if (Test-Path -LiteralPath $configPath -PathType Leaf) {
    Write-Skip "config exists: $configPath"
} else {
    Write-Step "creating $configPath"
    if (-not (Test-Path -LiteralPath $configDir -PathType Container)) {
        New-Item -ItemType Directory -Path $configDir -Force | Out-Null
    }

    $slackUrl = ''
    $slackCh  = '#general'
    if (-not $NonInteractive) {
        try {
            $slackUrl = Read-Host "Slack webhook URL (leave blank to skip)"
        } catch { $slackUrl = '' }
        try {
            $chInput = Read-Host "Slack channel [#general]"
            if ($chInput) { $slackCh = $chInput }
        } catch { }
    }

    $cfg = [ordered]@{
        slack_webhook_url = $slackUrl
        slack_channel     = $slackCh
        eye_autostart     = $true
    }

    # ConvertTo-Json indents with 2 spaces in PS 5.1; that's fine for a config.
    # Write as UTF-8 without BOM via .NET so downstream readers don't choke.
    $json  = ($cfg | ConvertTo-Json -Depth 4)
    $utf8  = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($configPath, $json, $utf8)

    Write-Ok "config written: $configPath"
}

# ---------------------------------------------------------------- 2. user PATH
Write-Step "checking user PATH for $binDirResolved"
$userPath = [Environment]::GetEnvironmentVariable('Path', 'User')
if (-not $userPath) { $userPath = '' }

# Case-insensitive segment match (Windows PATH semantics).
$segments = $userPath -split ';' | Where-Object { $_ -and $_.Trim() }
$alreadyOnPath = $false
foreach ($seg in $segments) {
    if ([string]::Equals($seg.TrimEnd('\'), $binDirResolved.TrimEnd('\'), [StringComparison]::OrdinalIgnoreCase)) {
        $alreadyOnPath = $true
        break
    }
}

if ($alreadyOnPath) {
    Write-Skip "user PATH already contains bin dir"
} else {
    $newPath = if ($userPath) { "$userPath;$binDirResolved" } else { $binDirResolved }
    [Environment]::SetEnvironmentVariable('Path', $newPath, 'User')
    # Also export to current session so the eye tick probe below sees it.
    $env:Path = "$env:Path;$binDirResolved"
    Write-Ok "added to user PATH (open a new terminal for global effect)"
}

# ---------------------------------------------------------------- 3. eye tick
Write-Step "probing Eye via 'wkappbot eye tick'"
$wkappbotExe = Join-Path $binDirResolved 'wkappbot.exe'
if (-not (Test-Path -LiteralPath $wkappbotExe -PathType Leaf)) {
    Write-Err "wkappbot.exe not found at $wkappbotExe -- bin dir layout unexpected"
    exit 1
}

try {
    $tickOutput = & $wkappbotExe eye tick 2>&1
    $tickExit = $LASTEXITCODE
    if ($tickExit -eq 0) {
        Write-Ok "eye tick OK"
    } else {
        Write-Warn2 "eye tick exited $tickExit"
        if ($tickOutput) { Write-Host ($tickOutput | Out-String) }
    }
} catch {
    Write-Err "eye tick failed: $($_.Exception.Message)"
    exit 1
}

Write-Host ''
Write-Ok "wkappbot-sdk setup complete."
exit 0
