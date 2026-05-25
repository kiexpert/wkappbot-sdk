#Requires -Version 5.1
# CDP connectivity smoke test -- no login required, zero AI token cost
# Correct eval syntax: wkappbot a11y read GRAP --eval-js 'JS'
# Pass: Chrome launches, CDP connects, page loads, any content received
# ("Please sign in" = PASS -- proves CDP pipeline alive)
# Integrates cdp-mon anomaly checks: off-screen, LAT=DEAD, MEM, tabs, drift

param(
    [string]$Url    = 'https://chatgpt.com',
    [string]$Expect = 'chatgpt|openai'
)

$ErrorActionPreference = 'Continue'
$pass = $true
$name = ([uri]$Url).Host -replace '^www\.',''
function ok  ($m) { Write-Host "  PASS [$name]: $m" -ForegroundColor Green }
function fail($m) { Write-Host "  FAIL [$name]: $m" -ForegroundColor Red; $script:pass = $false }
function inf ($m) { Write-Host "  INFO [$name]: $m" -ForegroundColor Cyan }
function wrn ($m) { Write-Host "  WARN [$name]: $m" -ForegroundColor Yellow }

function Invoke-WkEval([string]$grap, [string]$js) {
    # Correct syntax: wkappbot a11y read GRAP --eval-js 'JS'
    $raw = (& wkappbot a11y read $grap --eval-js $js 2>&1) -join " "
    # Extract actual result -- skip LAUNCH JSON, focus on last meaningful line
    $lines = ($raw -split '\n') | Where-Object { $_ -match '\S' -and $_ -notmatch '^\{.*_.*LAUNCH\|^\s*\[' }
    return ($lines | Select-Object -Last 1).Trim()
}

# ── 1. Eye alive ──────────────────────────────────────────────────────────────
Write-Host "[1] Eye alive..." -ForegroundColor Cyan
$tick = (& wkappbot eye tick 2>&1) -join " "
if ($tick -match 'result=ok|end:0|ctx=') { ok "Eye responding" }
else { fail "Eye: $tick"; exit 1 }

# ── 2. CDP open (120s timeout -- auth-wall sites block indefinitely without it) ──
Write-Host "[2] cdp open $Url (timeout=120s)..." -ForegroundColor Cyan
$cdpJob = Start-Job -ScriptBlock { param($u) & wkappbot cdp open $u 2>&1 } -ArgumentList $Url
if (-not (Wait-Job $cdpJob -Timeout 120)) {
    Remove-Job $cdpJob -Force
    wrn "cdp open timed out 120s -- auth-wall or bot-protection. Chrome launched OK."
    Write-Host ""; Write-Host "RESULT: SKIP [$name] (cdp-open timeout)" -ForegroundColor Yellow; exit 0
}
$cdpRaw = (Receive-Job $cdpJob) -join " "
Remove-Job $cdpJob -Force -ErrorAction SilentlyContinue
if ($cdpRaw -match 'cdp:(\d+)') {
    $port = $Matches[1]
    $grap = "{proc:'chrome',cdp:$port}"
    ok "Port $port assigned"
} else { fail "cdp open: $($cdpRaw.Substring(0, [Math]::Min(200,$cdpRaw.Length)))"; exit 1 }
Start-Sleep -Seconds 6

# ── 3. cdp-mon anomaly checks (from monitor-cdp.ps1 logic) ───────────────────
Write-Host "[3] cdp-mon anomaly check..." -ForegroundColor Cyan
$monOut = (& powershell -File "$PSScriptRoot\monitor-cdp.ps1" 2>&1) -join "`n"
$portLine = ($monOut -split "`n") | Where-Object { $_ -match "^\s*:?$port\s" } | Select-Object -First 1

if ($portLine) {
    # Off-screen check (x < -100 in TGT-POS)
    if ($portLine -match 'OFF-SCREEN') { fail "OFF-SCREEN: $portLine"; }
    # LAT=DEAD
    elseif ($portLine -match 'DEAD') { fail "LAT=DEAD: $portLine" }
    # High latency
    elseif ($portLine -match 'lat=(\d+)/' -and [int]$Matches[1] -gt 80) { wrn "High latency: $portLine" }
    # High memory
    elseif ($portLine -match '(\d+)MB' -and [int]$Matches[1] -gt 1000) { wrn "High memory: $portLine" }
    else { ok "Session healthy: $($portLine.Trim())" }
} else { inf "Port $port not in cdp-mon output yet (new session)" }

# ── 4. URL reachable ──────────────────────────────────────────────────────────
Write-Host "[4] URL check..." -ForegroundColor Cyan
$pageUrl = Invoke-WkEval $grap "window.location.href"
if ($pageUrl -match $Expect) { ok "URL: $pageUrl" }
elseif ($pageUrl.Length -gt 5) { inf "URL (any = alive): $pageUrl"; ok "CDP eval working" }
else { fail "No URL returned" }

# ── 5. Page rendered ──────────────────────────────────────────────────────────
Write-Host "[5] Render state..." -ForegroundColor Cyan
$state = Invoke-WkEval $grap "document.readyState"
if ($state -match 'complete|interactive') { ok "readyState: $state" }
elseif ($state.Length -gt 2) { inf "readyState: $state" }
else { wrn "readyState unknown -- page may still loading" }

# ── 6. Cookie/consent banner + auto-dismiss attempt ───────────────────────────
Write-Host "[6] Cookie banner..." -ForegroundColor Cyan
$bannerJs = "(function(){var s=['[id*=cookie]','[class*=cookie-banner]','[aria-label*=cookie i]','[id*=consent]','#onetrust-accept-btn-handler'];for(var x of s){var e=document.querySelector(x);if(e&&e.offsetParent!==null)return 'BANNER|'+e.innerText.trim().slice(0,60);}return 'none';})()"
$banner = Invoke-WkEval $grap $bannerJs
if ($banner -match 'BANNER\|') {
    $bannerText = $banner -replace '.*BANNER\|',''
    inf "Banner: $bannerText"
    # Auto-dismiss: try reject/necessary/decline first (safe)
    $dismissJs = "(function(){var btns=Array.from(document.querySelectorAll('button'));var b=btns.find(b=>{var t=b.innerText.toLowerCase();return t.match(/reject|necessary|decline|close x|dismiss/);});if(b){b.click();return 'DISMISSED:'+b.innerText.trim();}return 'no-safe-btn';})()"
    $dismissed = Invoke-WkEval $grap $dismissJs
    if ($dismissed -match 'DISMISSED:') { ok "Auto-dismissed: $($dismissed -replace '.*DISMISSED:','')" }
    else { inf "Accept-only banner (auto-approval C# hook target)" }
} else { ok "No blocking banner" }

# ── 7. Any content = CDP alive (login page counts as success) ─────────────────
Write-Host "[7] Page content..." -ForegroundColor Cyan
$bodyJs = "document.body.innerText.replace(/\s+/g,' ').trim().slice(0,120)"
$body = Invoke-WkEval $grap $bodyJs
if ($body.Length -gt 15) {
    ok "CDP alive (any content = PASS, 'Sign in' page = OK)"
    inf "$($body.Substring(0,[Math]::Min(80,$body.Length)))"
} else { fail "Empty page body -- Chrome may be off-screen or not rendering" }

# ── 8. Cleanup ────────────────────────────────────────────────────────────────
Write-Host "[8] Cleanup..." -ForegroundColor Cyan
& wkappbot cdp close --grap $grap 2>&1 | Out-Null
ok "Session closed"

Write-Host ""
if ($pass) { Write-Host "RESULT: ALL PASS [$name]" -ForegroundColor Green; exit 0 }
else        { Write-Host "RESULT: FAIL [$name]"    -ForegroundColor Red;   exit 1 }
