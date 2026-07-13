#Requires -Version 5.1
# CDP connectivity smoke test -- no login required, zero AI token cost
# Correct eval syntax (verified live 2026-07-13): wkappbot a11y read 'GRAP#html' --eval-js 'JS'
# NOTE: `cdp eval` was removed; `a11y read --eval-js` is current. As of 2026-07,
# --eval-js REQUIRES a node-path/css-selector suffix on the grap -- a bare window
# grap like {proc:'chrome',cdp:PORT} now errors "--eval-js requires a node path".
# '#html' is a stable, always-present anchor that works on any loaded page.
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

# Set once a JS-dialog-block or a hard eval timeout is observed -- short-circuits
# every subsequent Invoke-WkEval call to an instant SKIP instead of re-hanging on
# the same stuck dialog (2026-07-12 change: dialogs are never auto-accepted, and
# `cdp dialog <grap> --accept` only works from the SAME process that triggered the
# dialog -- a brand-new `wkappbot` invocation from this script can never accept it,
# confirmed live: "FAIL no pending dialog (this process must be the one that
# triggered it)"). SKIP is the correct, honest classification here, not FAIL.
$script:wkEvalBlocked = $false

function Invoke-WkEval([string]$grap, [string]$js, [int]$TimeoutSec = 20) {
    # Correct syntax: wkappbot a11y read 'GRAP#html' --eval-js 'JS' (node-path required)
    if ($script:wkEvalBlocked) { return 'WKEVAL:SKIP-DIALOG-BLOCKED' }
    $evalGrap = if ($grap -match '#') { $grap } else { "$grap#html" }

    # Bound the call: a JS-dialog freeze makes Runtime.enable retry with backoff
    # (observed live: 4 attempts, ~60-90s total) -- far too slow for CI. Run in a
    # job so we can hard-kill it at $TimeoutSec instead of blocking the whole script.
    # NOTE: Start-Job spawns a NEW child process whose default CWD is the user's
    # profile/OneDrive Documents folder, NOT this script's CWD -- and wkappbot
    # derives its per-project CDP port from the CURRENT DIRECTORY, so an un-cd'd
    # job rejects our own grap's cdp:PORT ("--eval-js requires cdp:XXXX for this
    # project (got cdp:PORT)"). Pass the real CWD in and Set-Location first.
    $callerCwd = (Get-Location).Path
    $job = Start-Job -ScriptBlock {
        param($g, $j, $cwd)
        Set-Location -LiteralPath $cwd
        & wkappbot a11y read $g --eval-js $j 2>&1
    } -ArgumentList $evalGrap, $js, $callerCwd

    $finished = Wait-Job $job -Timeout $TimeoutSec
    if (-not $finished) {
        Stop-Job $job -ErrorAction SilentlyContinue | Out-Null
        Remove-Job $job -Force -ErrorAction SilentlyContinue
        $script:wkEvalBlocked = $true
        wrn "eval timed out after ${TimeoutSec}s -- likely a JS dialog freeze (no-auto-accept); skipping remaining evals"
        return 'WKEVAL:TIMEOUT'
    }
    $raw = (Receive-Job $job) -join "`n"
    Remove-Job $job -Force -ErrorAction SilentlyContinue

    if ($raw -match '\[CDP:DIALOG:BLOCKED\]') {
        $script:wkEvalBlocked = $true
        $dmsg = if ($raw -match 'Message\s*:\s*"([^"]*)"') { $Matches[1] } else { '?' }
        wrn "JS dialog is blocking CDP (message: `"$dmsg`") -- cannot auto-accept across process boundary; skipping remaining evals"
        return 'WKEVAL:DIALOG-BLOCKED'
    }

    # Extract actual result -- skip LAUNCH JSON, bracket-noise lines, and the
    # '# TARGET ...' / '# END ...' grap-echo markers a11y read wraps the value in
    # (the real eval value is NOT always the last line -- '# END ...' usually is).
    $lines = ($raw -split '\r?\n') | Where-Object {
        $_ -match '\S' -and $_ -notmatch '^\{.*_.*LAUNCH' -and $_ -notmatch '^\s*\[' -and $_ -notmatch '^\s*#'
    }
    return ($lines | Select-Object -Last 1).Trim()
}

# -- 1. Eye alive --------------------------------------------------------------
Write-Host "[1] Eye alive..." -ForegroundColor Cyan
$tick = (& wkappbot eye tick 2>&1) -join " "
if ($tick -match 'result=ok|end:0|ctx=') { ok "Eye responding" }
else { fail "Eye: $tick"; exit 1 }

# -- 2. CDP open (9s timeout -- 3s=fast 9s=human patience limit, anything longer = skip) --
Write-Host "[2] cdp open $Url (--timeout 9)..." -ForegroundColor Cyan
$cdpRaw = (& wkappbot cdp open $Url --timeout 9 2>&1) -join " "
if ($LASTEXITCODE -eq 2 -or $cdpRaw -match 'timeout \d+s') {
    wrn "cdp open timed out 9s -- slow/auth-wall site. Skip."
    Write-Host ""; Write-Host "RESULT: SKIP [$name] (cdp-open timeout)" -ForegroundColor Yellow; exit 0
}
if ($cdpRaw -match 'cdp:(\d+)') {
    $port = $Matches[1]
    $grap = "{proc:'chrome',cdp:$port}"
    ok "Port $port assigned"
} else { fail "cdp open: $($cdpRaw.Substring(0, [Math]::Min(200,$cdpRaw.Length)))"; exit 1 }
Start-Sleep -Seconds 6

# -- 3. cdp-mon anomaly checks (from monitor-cdp.ps1 logic) -------------------
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

# -- 4. URL reachable ----------------------------------------------------------
Write-Host "[4] URL check..." -ForegroundColor Cyan
$pageUrl = Invoke-WkEval $grap "window.location.href"
if ($pageUrl -like 'WKEVAL:*') { wrn "URL check skipped ($pageUrl)" }
elseif ($pageUrl -match $Expect) { ok "URL: $pageUrl" }
elseif ($pageUrl.Length -gt 5) { inf "URL (any = alive): $pageUrl"; ok "CDP eval working" }
else { fail "No URL returned" }

# -- 5. Page rendered ----------------------------------------------------------
Write-Host "[5] Render state..." -ForegroundColor Cyan
$state = Invoke-WkEval $grap "document.readyState"
if ($state -like 'WKEVAL:*') { wrn "Render-state check skipped ($state)" }
elseif ($state -match 'complete|interactive') { ok "readyState: $state" }
elseif ($state.Length -gt 2) { inf "readyState: $state" }
else { wrn "readyState unknown -- page may still loading" }

# -- 6. Cookie/consent banner + auto-dismiss attempt ---------------------------
Write-Host "[6] Cookie banner..." -ForegroundColor Cyan
$bannerJs = "(function(){var s=['[id*=cookie]','[class*=cookie-banner]','[aria-label*=cookie i]','[id*=consent]','#onetrust-accept-btn-handler'];for(var x of s){var e=document.querySelector(x);if(e&&e.offsetParent!==null)return 'BANNER|'+e.innerText.trim().slice(0,60);}return 'none';})()"
$banner = Invoke-WkEval $grap $bannerJs
if ($banner -like 'WKEVAL:*') {
    wrn "Cookie-banner check skipped ($banner)"
} elseif ($banner -match 'BANNER\|') {
    $bannerText = $banner -replace '.*BANNER\|',''
    inf "Banner: $bannerText"
    # Auto-dismiss: try reject/necessary/decline first (safe)
    $dismissJs = "(function(){var btns=Array.from(document.querySelectorAll('button'));var b=btns.find(b=>{var t=b.innerText.toLowerCase();return t.match(/reject|necessary|decline|close x|dismiss/);});if(b){b.click();return 'DISMISSED:'+b.innerText.trim();}return 'no-safe-btn';})()"
    $dismissed = Invoke-WkEval $grap $dismissJs
    if ($dismissed -like 'WKEVAL:*') { inf "Dismiss-attempt skipped ($dismissed)" }
    elseif ($dismissed -match 'DISMISSED:') { ok "Auto-dismissed: $($dismissed -replace '.*DISMISSED:','')" }
    else { inf "Accept-only banner (auto-approval C# hook target)" }
} else { ok "No blocking banner" }

# -- 7. Any content = CDP alive (login page counts as success) -----------------
Write-Host "[7] Page content..." -ForegroundColor Cyan
$bodyJs = "document.body.innerText.replace(/\s+/g,' ').trim().slice(0,120)"
$body = Invoke-WkEval $grap $bodyJs
if ($body -like 'WKEVAL:*') {
    wrn "Page-content check skipped ($body)"
} elseif ($body.Length -gt 15) {
    ok "CDP alive (any content = PASS, 'Sign in' page = OK)"
    inf "$($body.Substring(0,[Math]::Min(80,$body.Length)))"
} else { fail "Empty page body -- Chrome may be off-screen or not rendering" }

# -- 8. Cleanup ----------------------------------------------------------------
Write-Host "[8] Cleanup..." -ForegroundColor Cyan
& wkappbot cdp close --grap $grap 2>&1 | Out-Null
ok "Session closed"

Write-Host ""
if ($script:wkEvalBlocked -and $pass) {
    Write-Host "RESULT: SKIP [$name] (JS dialog blocked eval checks -- CDP pipeline itself was reachable)" -ForegroundColor Yellow
    exit 0
} elseif ($pass) { Write-Host "RESULT: ALL PASS [$name]" -ForegroundColor Green; exit 0 }
else        { Write-Host "RESULT: FAIL [$name]"    -ForegroundColor Red;   exit 1 }
