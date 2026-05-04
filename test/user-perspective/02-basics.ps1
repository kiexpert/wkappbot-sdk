#Requires -Version 5.1
# Stage 2: Basic CLI commands
# Verifies all zero-configuration commands work correctly.

param([switch]$CI)

$ErrorActionPreference = 'Continue'
$pass = 0; $fail = 0; $warn = 0

function Check([string]$label, [scriptblock]$test, [switch]$Soft) {
    try {
        $result = & $test
        if ($result) { $script:pass++; Write-Host "  [PASS] $label" -ForegroundColor Green }
        else {
            if ($Soft) { $script:warn++; Write-Host "  [WARN] $label" -ForegroundColor Yellow }
            else        { $script:fail++; Write-Host "  [FAIL] $label" -ForegroundColor Red }
        }
    } catch {
        if ($Soft) { $script:warn++; Write-Host "  [WARN] $label -- $_" -ForegroundColor Yellow }
        else        { $script:fail++; Write-Host "  [FAIL] $label -- $_" -ForegroundColor Red }
    }
}

function Run([string[]]$cmd) {
    $out = & wkappbot @cmd 2>&1
    return @{ ExitCode = $LASTEXITCODE; Output = $out -join "`n" }
}

Write-Host ""
Write-Host "=== Stage 2: Basic CLI ===" -ForegroundColor Cyan
Write-Host ""

# 2.1 --help exits 0
Check "--help exits 0" { (Run '--help').ExitCode -eq 0 }

# 2.2 --help mentions a11y
Check "--help mentions a11y" { (Run '--help').Output -match 'a11y' }

# 2.3 skill list works
$skills = Run 'skill', 'list'
Check "skill list exits 0" { $skills.ExitCode -eq 0 }
Check "skill list shows wkappbot skills" { $skills.Output -match 'wkappbot' }

# 2.4 skill read a known skill
Check "skill read grap exits 0" { (Run 'skill', 'read', 'grap').ExitCode -eq 0 }

# 2.5 file tools -- write + read + grep
$tmp = Join-Path $env:TEMP "wkappbot-test-$([guid]::NewGuid().ToString('N')[0..7] -join '').txt"
Set-Content $tmp "hello wkappbot`ntest line 2`nalpha beta gamma" -Encoding UTF8
Check "file read exits 0" { (Run 'file', 'read', $tmp).ExitCode -eq 0 }
Check "file read returns content" { (Run 'file', 'read', $tmp).Output -match 'hello wkappbot' }
Check "file grep exits 0" { (Run 'file', 'grep', 'alpha', $tmp).ExitCode -eq 0 }
Check "file grep finds match" { (Run 'file', 'grep', 'alpha', $tmp).Output -match 'alpha' }
Remove-Item $tmp -Force -ErrorAction SilentlyContinue

# 2.6 file glob
Check "file glob finds yml files" {
    $r = Run 'file', 'glob', '**/*.yml', '--path', (Join-Path $PSScriptRoot '../../.github/workflows')
    $r.ExitCode -eq 0 -and $r.Output -match '\.yml'
}

# 2.7 schedule list
Check "schedule list exits 0" { (Run 'schedule', 'list').ExitCode -eq 0 }

# 2.8 windows list (soft -- may find nothing in headless CI)
$wins = Run 'windows'
Check "windows exits 0" { $wins.ExitCode -eq 0 } -Soft

# 2.9 eye help
Check "eye --help exits 0" { (Run 'eye', '--help', '--no-regression').ExitCode -eq 0 }

# 2.10 ask help
Check "ask --help exits 0" { (Run 'ask', '--help', '--no-regression').ExitCode -eq 0 }

# 2.11 gc help
Check "gc --help exits 0" { (Run 'gc', '--help', '--no-regression').ExitCode -eq 0 }

Write-Host ""
Write-Host "Stage 2 result: $pass passed, $warn warned, $fail failed" -ForegroundColor $(if ($fail -eq 0) { 'Green' } else { 'Red' })
exit $(if ($fail -eq 0) { 0 } else { 1 })
