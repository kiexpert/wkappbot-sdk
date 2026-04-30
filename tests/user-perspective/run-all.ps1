#Requires -Version 5.1
# Runs all user-perspective test stages in order.
# Exit code = number of failed stages.

param([switch]$SkipDesktop, [switch]$CI)

$root   = $PSScriptRoot
$ps    = if (Test-Path 'C:\Program Files\PowerShell\7\pwsh.exe') {
    'C:\Program Files\PowerShell\7\pwsh.exe'
} else { 'powershell.exe' }

$stages = @(
    @{ File = '01-install.ps1';    Args = @() }
    @{ File = '02-basics.ps1';     Args = @() }
    @{ File = '03-automation.ps1'; Args = if ($SkipDesktop -or $CI) { @('-SkipDesktop') } else { @() } }
    @{ File = '04-license.ps1';    Args = @() }
)

$failed = @()
foreach ($s in $stages) {
    Write-Host ""
    Write-Host ("─" * 60) -ForegroundColor DarkGray
    $stageArgs = $s.Args
    & $ps -NoProfile -ExecutionPolicy Bypass -File (Join-Path $root $s.File) @stageArgs
    if ($LASTEXITCODE -ne 0) { $failed += $s.File }
}

Write-Host ""
Write-Host ("=" * 60) -ForegroundColor Cyan
if ($failed.Count -eq 0) {
    Write-Host "  ALL STAGES PASSED" -ForegroundColor Green
} else {
    Write-Host "  FAILED STAGES: $($failed -join ', ')" -ForegroundColor Red
}
Write-Host ("=" * 60) -ForegroundColor Cyan
exit $failed.Count
