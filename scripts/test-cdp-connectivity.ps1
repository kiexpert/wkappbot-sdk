#Requires -Version 5.1
# CDP connectivity smoke test -- no login required
# Pass criteria: Chrome launches, CDP connects, page loads, any response received
# "Please sign in" or rate-limit page = PASS (proves CDP pipeline alive)

$ErrorActionPreference = 'Continue'
$pass = $true
function ok ($m)  { Write-Host "  PASS: $m" -ForegroundColor Green }
function fail($m) { Write-Host "  FAIL: $m" -ForegroundColor Red; $script:pass = $false }
function inf($m)  { Write-Host "  INFO: $m" -ForegroundColor Cyan }

# ── 1. Eye alive ──────────────────────────────────────────────────────────────
Write-Host "[1] Eye alive..." -ForegroundColor Cyan
$eyeOut = (& wkappbot eye tick 2>&1) -join " "
if ($eyeOut -match 'result=ok|end:0|ctx=') { ok "Eye responding" }
else { fail "Eye not responding: $eyeOut" }

# ── 2. CDP open chatgpt.com ───────────────────────────────────────────────────
Write-Host "[2] CDP open chatgpt.com..." -ForegroundColor Cyan
$cdpOut = (& wkappbot cdp open "https://chatgpt.com" 2>&1) -join " "
if ($cdpOut -match 'cdp:(\d+)') {
    $port = $Matches[1]
    ok "CDP assigned port $port"
} else {
    fail "CDP open failed: $cdpOut"
    exit 1
}

Start-Sleep -Seconds 5   # let page load

# ── 3. Page loaded (any URL accepted) ────────────────────────────────────────
Write-Host "[3] Page URL check..." -ForegroundColor Cyan
$url = (& wkappbot a11y eval-js "window.location.href" --grap "{proc:'chrome',cdp:$port}" 2>&1) -join " "
if ($url -match 'chatgpt\.com|openai\.com') {
    ok "Page at: $url"
} else {
    fail "Unexpected URL: $url"
}

# ── 4. Page render complete ───────────────────────────────────────────────────
Write-Host "[4] Render state..." -ForegroundColor Cyan
$state = (& wkappbot a11y eval-js "document.readyState" --grap "{proc:'chrome',cdp:$port}" 2>&1) -join " "
if ($state -match 'complete|interactive') { ok "readyState: $state" }
else { fail "Page not ready: $state" }

# ── 5. Cookie banner check + auto-approval ───────────────────────────────────
Write-Host "[5] Cookie banner scan..." -ForegroundColor Cyan
$bannerJs = "(function(){var s=['[id*=cookie]','[class*=cookie-banner]','[aria-label*=cookie i]'];for(var x of s){var e=document.querySelector(x);if(e&&e.offsetParent!==null)return e.innerText.slice(0,80);}return 'none';})()"
$banner = (& wkappbot a11y eval-js $bannerJs --grap "{proc:'chrome',cdp:$port}" 2>&1) -join " "
if ($banner -match 'none|null') { ok "No blocking cookie banner" }
else { inf "Cookie banner present (auto-approval should handle): $banner" }

# ── 6. Page has ANY visible content ──────────────────────────────────────────
Write-Host "[6] Page content check..." -ForegroundColor Cyan
$bodyText = (& wkappbot a11y eval-js "document.body.innerText.trim().slice(0,200)" --grap "{proc:'chrome',cdp:$port}" 2>&1) -join " "
if ($bodyText.Trim().Length -gt 20) {
    ok "Page has content (any response = CDP working)"
    inf "Content preview: $($bodyText.Trim().Substring(0,[Math]::Min(120,$bodyText.Trim().Length)))"
} else {
    fail "Page body empty or too short"
}

# ── 7. Cleanup ────────────────────────────────────────────────────────────────
Write-Host "[7] Cleanup..." -ForegroundColor Cyan
& wkappbot cdp close --grap "{proc:'chrome',cdp:$port}" 2>&1 | Out-Null
ok "Session closed"

Write-Host ""
if ($pass) { Write-Host "RESULT: ALL PASS -- CDP pipeline alive" -ForegroundColor Green; exit 0 }
else        { Write-Host "RESULT: FAIL"                          -ForegroundColor Red;   exit 1 }
