# Normalized Dynamic Defender Check for wkdoctor
$binDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$wsRoot = Split-Path -Parent (Split-Path -Parent $binDir)

$desiredPaths = @($wsRoot, "$HOME\.claude", "$env:LOCALAPPDATA\Temp\claude", "$env:TEMP\wktasklist-snapshot.json")
$desiredProcs = @("powershell.exe", "cmd.exe", "claude.exe", "python.exe", "pythonw.exe", "node.exe", "codex.exe", "wkappbot-core.exe", "khmini.exe", "nkmini.exe", "heroglobal.exe")
$maxCpu = 10

function _DefNormPath([string]$p) {
    if (-not $p) { return '' }
    ($p -replace '/','\').TrimEnd('\').ToLowerInvariant()
}

$mp = Get-MpPreference -ErrorAction SilentlyContinue
if (-not $mp) {
    Add-Check 'Defender exclusions' 'warn' 'Status unknown (Get-MpPreference failed)'
    return
}

$exPathsN = @($mp.ExclusionPath) | ForEach-Object { _DefNormPath $_ }
$exProcsN = @($mp.ExclusionProcess) | ForEach-Object { if ($_) { $_.ToLowerInvariant() } }
$cpu = $mp.ScanAvgCPULoadFactor

$missingPaths = @($desiredPaths | Where-Object { (_DefNormPath $_) -notin $exPathsN })
$missingProcs = @($desiredProcs | Where-Object { $_.ToLowerInvariant() -notin $exProcsN })
$cpuOk = ($null -ne $cpu -and $cpu -le $maxCpu)

if ($missingPaths.Count -eq 0 -and $missingProcs.Count -eq 0 -and $cpuOk) {
    Add-Check 'Defender exclusions' 'ok' "Sanctuary active ($wsRoot); ScanLimit:$cpu%"
    Emit 'ok' 'Defender exclusions' "Sanctuary active ($wsRoot)"
} else {
    $missDesc = @()
    if ($missingPaths.Count) { $missDesc += "Missing paths ($($missingPaths.Count))" }
    if ($missingProcs.Count) { $missDesc += "Missing procs ($($missingProcs.Count))" }
    if (-not $cpuOk) { $missDesc += "CPU limit $cpu% > $maxCpu%" }
    Add-Check 'Defender exclusions' 'warn' ("Optimization required: " + ($missDesc -join '; '))
    Emit 'warn' 'Defender exclusions' "Action Required: run 'wkdoctor -DefenderFix'"
}
