#Requires -Version 5.1
# CDP Chrome reuse + alert auto-dismiss + CDP eval smoke test
param([string]$Url = "https://example.com")
$ErrorActionPreference = "Continue"
$pass = $true
function ok  ($m) { Write-Host "  PASS: $m" -ForegroundColor Green }
function fail ($m) { Write-Host "  FAIL: $m" -ForegroundColor Red; $script:pass = $false }
function wrn  ($m) { Write-Host "  WARN: $m" -ForegroundColor Yellow }

# 1. First cdp open
Write-Host "[1] First cdp open $Url..." -ForegroundColor Cyan
$r1 = (& wkappbot cdp open $Url 2>&1) -join " "
if ($r1 -match "cdp:(\d+)") {
    $port = $Matches[1]; $grap = "{proc:'chrome',cdp:$port}"
    ok "Port $port ($(if ($r1 -match "Reusing") {'reuse'} else {'launch'}))"
} else { fail "cdp open 1: $($r1.Substring(0,[Math]::Min(200,$r1.Length)))"; exit 1 }

# 2. Second cdp open MUST reuse
Write-Host "[2] Second cdp open (MUST reuse)..." -ForegroundColor Cyan
$r2 = (& wkappbot cdp open $Url 2>&1) -join " "
if     ($r2 -match "Reusing Chrome") { ok "Chrome reused on port $port" }
elseif ($r2 -match "Launching Chrome") { fail "Chrome NOT reused -- new process launched (accumulation bug)" }
else { wrn "No reuse confirmation: $($r2.Substring(0,[Math]::Min(120,$r2.Length)))" }

Start-Sleep -Seconds 3

# 3. CDP eval via a11y read --eval-js
Write-Host "[3] CDP eval (document.title)..." -ForegroundColor Cyan
$ev = (& wkappbot a11y read $grap --eval-js "document.title" 2>&1) -join "`n"
$lines = ($ev -split "\r?\n") | Where-Object { $_ -match "\S" -and $_ -notmatch "^\{" -and $_ -notmatch "^\s*\[" }
$res = ($lines | Select-Object -Last 1).Trim()
if ($res.Length -gt 2) { ok "CDP eval: $($res.Substring(0,[Math]::Min(60,$res.Length)))" }
else { fail "CDP eval empty or no result" }

# 4. Alert auto-dismiss
Write-Host "[4] Alert auto-dismiss..." -ForegroundColor Cyan
$alertUrl = "data:text/html,<script>setTimeout(()=>alert('CDP-smoke'),500)</script><h1>Alert Test</h1>"
$alertJob = Start-Job { param($u); & wkappbot cdp open $u 2>&1 } -ArgumentList $alertUrl
$done = Wait-Job $alertJob -Timeout 15
if ($done) {
    $out = (Receive-Job $alertJob) -join " "; Remove-Job $alertJob -Force
    if ($out -match "OK \{hwnd|cdp:\d+") { ok "Alert auto-dismissed OK" }
    else { wrn "Alert result: $($out.Substring(0,[Math]::Min(120,$out.Length)))" }
} else { Remove-Job $alertJob -Force; fail "Hung 15s waiting for alert dismiss" }

# 5. Cleanup
Write-Host "[5] Cleanup..." -ForegroundColor Cyan
& wkappbot cdp close --grap $grap 2>&1 | Out-Null
ok "Session closed"

Write-Host ""
if ($pass) { Write-Host "RESULT: ALL PASS [cdp-reuse]" -ForegroundColor Green; exit 0 }
else        { Write-Host "RESULT: FAIL [cdp-reuse]" -ForegroundColor Red; exit 1 }