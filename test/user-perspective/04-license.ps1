#Requires -Version 5.1
# Stage 4: License system
# Verifies Free tier works out of the box, upgrade URL is reachable.

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

Write-Host ""
Write-Host "=== Stage 4: License ===" -ForegroundColor Cyan
Write-Host ""

# 4.1 license status exits 0
$lic = wkappbot license status 2>&1
$licCode = $LASTEXITCODE
Check "license status exits 0" { $licCode -eq 0 }

# 4.2 license status shows Tier
Check "license status shows Tier" { $lic -match 'Tier' }

# 4.3 license status shows Free or higher
Check "license shows tier name" { $lic -match 'Free|Standard|Pro|CDP|Sudo' }

# 4.4 upgrade URL is SUBSCRIBE.md on GitHub (reachable)
$url = 'https://github.com/kiexpert/wkappbot-sdk/blob/main/SUBSCRIBE.md'
Check "SUBSCRIBE.md URL reachable" {
    try {
        $r = Invoke-WebRequest $url -UseBasicParsing -TimeoutSec 10 -ErrorAction Stop
        $r.StatusCode -eq 200
    } catch { $false }
} -Soft

# 4.5 wkappbot skill list works on Free tier (not gated)
$sl = wkappbot skill list 2>&1
Check "skill list works on Free tier" { $LASTEXITCODE -eq 0 -and ($sl -match 'wkappbot') }

# 4.6 license activate with non-existent file returns error gracefully
wkappbot license activate C:\nonexistent-test-license-99.json 2>&1 | Out-Null
Check "invalid license activate returns non-zero" { $LASTEXITCODE -ne 0 }

# 4.7 gh auth status (optional -- needed for paid tier)
$ghAuth = gh auth status 2>&1
if ($LASTEXITCODE -eq 0) {
    $script:pass++; Write-Host "  [PASS] gh auth: authenticated (paid tier ready)" -ForegroundColor Green
} else {
    $script:warn++; Write-Host "  [WARN] gh auth: not authenticated (Free tier only -- run 'gh auth login' for paid)" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Stage 4 result: $pass passed, $warn warned, $fail failed" -ForegroundColor $(if ($fail -eq 0) { 'Green' } else { 'Red' })
exit $(if ($fail -eq 0) { 0 } else { 1 })
