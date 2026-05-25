# wkdoctor -- wkappbot-sdk health check orchestrator
# Usage: wkdoctor [-Json]
# Drop custom checks as *.ps1 into wkappbot.hq/doctor/ for plugin extension
param([switch]$Json)

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$binDir     = $scriptRoot
$repoRoot   = Split-Path -Parent $scriptRoot
$pass = 0; $fail = 0; $warn = 0
$items = [System.Collections.Generic.List[PSCustomObject]]::new()

function Add-Check {
    param([string]$Name, [string]$Status, [string]$Detail)
    $items.Add([PSCustomObject]@{ Name = $Name; Status = $Status; Detail = $Detail })
    if ($Status -eq 'ok')       { $script:pass++ }
    elseif ($Status -eq 'fail') { $script:fail++ }
    else                        { $script:warn++ }
}

function Emit {
    param([string]$Status, [string]$Name, [string]$Detail)
    if ($Json) { return }
    $sym   = if ($Status -eq 'ok') { '[+]' } elseif ($Status -eq 'fail') { '[x]' } else { '[!]' }
    $color = if ($Status -eq 'ok') { 'Green' } elseif ($Status -eq 'fail') { 'Red' } else { 'Yellow' }
    $line = "$sym $Name"
    if ($Detail) { $line += " -- $Detail" }
    Write-Host $line -ForegroundColor $color
}

# Load check modules (sorted by name, so 01- runs before 02- etc.)
$doctorDir = Join-Path $binDir 'wkappbot.hq\doctor'
if (Test-Path $doctorDir -PathType Container) {
    Get-ChildItem $doctorDir -Filter '*.ps1' | Sort-Object Name | ForEach-Object { . $_.FullName }
} else {
    Add-Check 'doctor modules' 'warn' "not found at $doctorDir -- run setup.ps1"
    Emit '!' 'doctor modules' 'missing'
}

# Summary
if (-not $Json) {
    Write-Host ''
    $color = if ($fail -gt 0) { 'Red' } elseif ($warn -gt 0) { 'Yellow' } else { 'Green' }
    Write-Host "wkdoctor: $pass ok, $warn warn, $fail fail" -ForegroundColor $color
}
if ($Json) {
    [PSCustomObject]@{ pass = $pass; warn = $warn; fail = $fail; items = $items } | ConvertTo-Json -Depth 5
}
if ($fail -gt 0) { exit 1 } else { exit 0 }
