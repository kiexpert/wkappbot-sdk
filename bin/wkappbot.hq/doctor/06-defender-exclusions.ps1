# harness:skill
# wkappbot skill read wkdoctor-sdk-environment-health-check
# wkappbot skill read file-command-cheatsheet
# wkappbot skill read defender-dev-exclusions

# wkdoctor check: Windows Defender dev-folder exclusions + auto-repair
# Stops the MsMpEng real-time scan storm (CPU spike + keyboard freeze) caused by
# Defender scanning dev hot-folders (constant JSONL writes + agent process spawns).
# Pattern mirrors 50-codex-symlink.ps1: Add-Check/Emit from orchestrator scope,
# never throws, auto-repairs (admin direct, or self-elevating UAC when not admin).

$desiredPaths = @(
    'D:\GitHub',
    (Join-Path $env:USERPROFILE '.claude'),
    (Join-Path $env:LOCALAPPDATA 'Temp\claude')
)
$desiredProcs = @('claude.exe','python.exe','pythonw.exe','node.exe','codex.exe','wkappbot-core.exe')
$maxCpu = 20

function _DefNormPath([string]$p) {
    if (-not $p) { return '' }
    ($p -replace '/','\').TrimEnd('\').ToLowerInvariant()
}

# Query Defender (never throw).
$mp = $null
try { $mp = Get-MpPreference -ErrorAction Stop } catch {}
if (-not $mp) {
    Add-Check 'Defender exclusions' 'warn' 'Get-MpPreference unavailable (Defender absent or blocked)'
    Emit 'warn' 'Defender exclusions' 'Get-MpPreference unavailable (Defender absent or blocked)'
    return
}

$exPathsN = @($mp.ExclusionPath) | ForEach-Object { _DefNormPath $_ }
$exProcsN = @($mp.ExclusionProcess) | ForEach-Object { if ($_) { $_.ToLowerInvariant() } }
$cpu      = $mp.ScanAvgCPULoadFactor

$missingPaths = @($desiredPaths | Where-Object { (_DefNormPath $_) -notin $exPathsN })
$missingProcs = @($desiredProcs | Where-Object { $_.ToLowerInvariant() -notin $exProcsN })
$cpuOk        = ($null -ne $cpu -and $cpu -gt 0 -and $cpu -le $maxCpu)

if ($missingPaths.Count -eq 0 -and $missingProcs.Count -eq 0 -and $cpuOk) {
    Add-Check 'Defender exclusions' 'ok' "dev paths+procs excluded; ScanAvgCPULoadFactor=$cpu"
    Emit 'ok' 'Defender exclusions' "dev paths+procs excluded; ScanAvgCPULoadFactor=$cpu"
    return
}

$missDesc = @()
if ($missingPaths.Count) { $missDesc += "paths: $($missingPaths -join ', ')" }
if ($missingProcs.Count) { $missDesc += "procs: $($missingProcs -join ', ')" }
if (-not $cpuOk)         { $missDesc += "ScanAvgCPULoadFactor=$cpu (want <=$maxCpu)" }
Emit 'warn' 'Defender exclusions' ("MISSING -- " + ($missDesc -join '; '))

# Build a self-contained repair script (used for the elevated path).
$repairParts = @()
foreach ($p in $missingPaths) { $repairParts += "Add-MpPreference -ExclusionPath '$p'" }
if ($missingProcs.Count) {
    $repairParts += "Add-MpPreference -ExclusionProcess " + (($missingProcs | ForEach-Object { "'$_'" }) -join ',')
}
if (-not $cpuOk) { $repairParts += "Set-MpPreference -ScanAvgCPULoadFactor $maxCpu" }
$repairScript = $repairParts -join '; '

$isAdmin = $false
try {
    $isAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)
} catch {}

if ($isAdmin) {
    try {
        foreach ($p in $missingPaths) { Add-MpPreference -ExclusionPath $p -ErrorAction Stop }
        if ($missingProcs.Count) { Add-MpPreference -ExclusionProcess $missingProcs -ErrorAction Stop }
        if (-not $cpuOk) { Set-MpPreference -ScanAvgCPULoadFactor $maxCpu -ErrorAction Stop }
        $mp2 = Get-MpPreference
        $stillN = @($mp2.ExclusionPath) | ForEach-Object { _DefNormPath $_ }
        $still  = @($desiredPaths | Where-Object { (_DefNormPath $_) -notin $stillN })
        if ($still.Count -eq 0) {
            Add-Check 'Defender exclusions' 'ok' 'auto-repaired (admin)'
            Emit 'ok' 'Defender exclusions' 'auto-repaired (admin)'
        } else {
            Add-Check 'Defender exclusions' 'fail' 'repair ran but exclusions still absent -- Tamper Protection? add via Defender GUI Exclusions'
            Emit 'fail' 'Defender exclusions' 'repair ran but exclusions still absent -- Tamper Protection? add via Defender GUI Exclusions'
        }
    } catch {
        Add-Check 'Defender exclusions' 'fail' "repair failed: $_ (Tamper Protection? use Defender GUI)"
        Emit 'fail' 'Defender exclusions' "repair failed: $_ (Tamper Protection? use Defender GUI)"
    }
} else {
    try {
        Start-Process powershell -Verb RunAs -WindowStyle Hidden -ArgumentList '-NoProfile','-Command',$repairScript -ErrorAction Stop
        Add-Check 'Defender exclusions' 'warn' 'not admin -- launched elevated repair (approve UAC), then re-run wkdoctor to verify'
        Emit 'warn' 'Defender exclusions' 'not admin -- launched elevated repair (approve UAC), then re-run wkdoctor to verify'
    } catch {
        Add-Check 'Defender exclusions' 'fail' 'not admin and elevation declined -- run wkdoctor as admin'
        Emit 'fail' 'Defender exclusions' 'not admin and elevation declined -- run wkdoctor as admin'
    }
}
