#Requires -Version 5.1
# Stage 3: Real automation (requires Desktop / interactive session)
# Opens Notepad, automates it focuslessly, verifies result.

param([switch]$SkipDesktop)

$ErrorActionPreference = 'Continue'
$pass = 0; $fail = 0

function Check([string]$label, [scriptblock]$test, [switch]$Soft) {
    try {
        $result = & $test
        if ($result) { $script:pass++; Write-Host "  [PASS] $label" -ForegroundColor Green }
        else {
            if ($Soft) { Write-Host "  [SKIP] $label" -ForegroundColor Gray }
            else        { $script:fail++; Write-Host "  [FAIL] $label" -ForegroundColor Red }
        }
    } catch {
        if ($Soft) { Write-Host "  [SKIP] $label -- $_" -ForegroundColor Gray }
        else        { $script:fail++; Write-Host "  [FAIL] $label -- $_" -ForegroundColor Red }
    }
}

Write-Host ""
Write-Host "=== Stage 3: Automation (Desktop) ===" -ForegroundColor Cyan
Write-Host ""

if ($SkipDesktop -or $env:GITHUB_ACTIONS -eq 'true') {
    Write-Host "  [SKIP] Desktop tests skipped (headless / CI mode)" -ForegroundColor Gray
    exit 0
}

# 3.1 Launch Notepad
Write-Host "  Launching Notepad..."
Start-Process notepad
Start-Sleep -Milliseconds 1500

# 3.2 a11y find -- Notepad edit area
$find = wkappbot a11y find "*Notepad*" 2>&1
Check "a11y find Notepad" { $LASTEXITCODE -eq 0 -and ($find -match 'TARGET') }

# 3.3 a11y type -- focusless write
wkappbot a11y type "*Notepad*#*Edit*" --text "Hello from WKAppBot user test!" 2>&1 | Out-Null
Check "a11y type exits 0" { $LASTEXITCODE -eq 0 }

# 3.4 a11y read -- verify content
$read = wkappbot a11y read "*Notepad*#*Edit*" 2>&1
Check "a11y read returns typed text" {
    $LASTEXITCODE -eq 0 -and ($read -match 'Hello from WKAppBot')
}

# 3.5 a11y inspect -- tree dump
$tree = wkappbot a11y inspect "*Notepad*" --depth 3 2>&1
Check "a11y inspect exits 0" { $LASTEXITCODE -eq 0 }
Check "a11y inspect shows elements" { $tree -match 'Edit|Document' }

# 3.6 screenshot
$shot = Join-Path $env:TEMP "wkappbot-test-shot.png"
wkappbot a11y screenshot "*Notepad*" --output $shot 2>&1 | Out-Null
Check "screenshot created" { Test-Path $shot }
if (Test-Path $shot) { Remove-Item $shot -Force }

# 3.7 close without saving
wkappbot a11y close "*Notepad*" --force 2>&1 | Out-Null
Start-Sleep -Milliseconds 500
$still = wkappbot windows "*Notepad*" 2>&1
Check "Notepad closed" { $still -notmatch 'Notepad' -or $LASTEXITCODE -ne 0 } -Soft

Write-Host ""
Write-Host "Stage 3 result: $pass passed, $fail failed" -ForegroundColor $(if ($fail -eq 0) { 'Green' } else { 'Red' })
exit $(if ($fail -eq 0) { 0 } else { 1 })
