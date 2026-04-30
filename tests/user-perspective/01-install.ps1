#Requires -Version 5.1
# Stage 1: Installation check
# Verifies binaries are present and reachable from PATH.

param([switch]$CI)

$ErrorActionPreference = 'Continue'
$pass = 0; $fail = 0

function Check([string]$label, [scriptblock]$test) {
    try {
        $result = & $test
        if ($result) { $script:pass++; Write-Host "  [PASS] $label" -ForegroundColor Green }
        else          { $script:fail++; Write-Host "  [FAIL] $label" -ForegroundColor Red }
    } catch {
        $script:fail++
        Write-Host "  [FAIL] $label -- $_" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "=== Stage 1: Installation ===" -ForegroundColor Cyan
Write-Host ""

# 1.1 wkappbot.exe exists in bin\
Check "bin\wkappbot.exe present" {
    Test-Path (Join-Path $PSScriptRoot "..\..\bin\wkappbot.exe")
}

# 1.2 wkappbot-core.exe exists
Check "bin\wkappbot-core.exe present" {
    Test-Path (Join-Path $PSScriptRoot "..\..\bin\wkappbot-core.exe")
}

# 1.3 wkappbot reachable from PATH (not just bin\ relative)
Check "wkappbot reachable from PATH" {
    $null = Get-Command wkappbot -ErrorAction Stop
    $true
}

# 1.4 --version returns expected format
Check "--version returns 'wkappbot v'" {
    $v = wkappbot --version 2>&1 | Select-String 'wkappbot v\d'
    $null -ne $v
}

# 1.5 version is 6.x or higher
Check "version >= 6.0" {
    $v = (wkappbot --version 2>&1 | Select-String 'wkappbot v(\d+)\.').Matches[0].Groups[1].Value
    [int]$v -ge 6
}

# 1.6 wkappbot.hq directory auto-created
Check "wkappbot.hq created" {
    $hq = Join-Path $PSScriptRoot "..\..\bin\wkappbot.hq"
    $null = wkappbot skill list 2>&1  # triggers HQ init
    Test-Path $hq
}

# 1.7 build_info.json present (CI-built artifacts have this; local builds may not)
Check "build_info.json present" {
    Test-Path (Join-Path $PSScriptRoot "..\..\bin\build_info.json")
}

Write-Host ""
Write-Host "Stage 1 result: $pass passed, $fail failed" -ForegroundColor $(if ($fail -eq 0) { 'Green' } else { 'Red' })
exit $(if ($fail -eq 0) { 0 } else { 1 })
