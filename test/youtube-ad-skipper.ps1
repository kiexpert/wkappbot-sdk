#Requires -Version 5.1
# youtube-ad-skipper.ps1 -- UIA-invoke based YouTube ad skipper.
# YouTube blocks JS .click() via isTrusted check. We use wkappbot a11y invoke
# (UIA InvokePattern = trusted input) on the .ytp-skip-ad-button element.
# Detection (lightweight eval-js poll) -> action (UIA invoke). No JS click ever.
#
# Usage: powershell -File test\youtube-ad-skipper.ps1 [-Url <url>] [-RunMinutes <N>]
param(
    [string] $Url = 'https://www.youtube.com/results?search_query=%EC%8A%88%ED%8D%BC%EA%B0%9C%EB%AF%B8+%EC%B5%9C%EC%8B%A0&sp=CAI%253D',
    [int]    $RunMinutes = 0,
    [int]    $PollSeconds = 1
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$ErrorActionPreference = 'Continue'

function ts  { Get-Date -Format 'HH:mm:ss' }
function ok($m)  { Write-Host "$(ts)  OK  $m" -ForegroundColor Green;  [Console]::Out.Flush() }
function inf($m) { Write-Host "$(ts)  ..  $m" -ForegroundColor Cyan;   [Console]::Out.Flush() }
function wrn($m) { Write-Host "$(ts)  !!  $m" -ForegroundColor Yellow; [Console]::Out.Flush() }
function err($m) { Write-Host "$(ts)  XX  $m" -ForegroundColor Red;    [Console]::Out.Flush() }

# Step 1: open the YouTube tab (cdp open auto-creates or reuses).
inf "Opening $Url"
$openOut = & wkappbot cdp open $Url 2>&1 | Out-String
if ($openOut -notmatch 'cdp:(\d+)') { err 'cdp open failed -- aborting'; Write-Host $openOut; exit 1 }
$port = [int]$Matches[1]
ok "CDP port $port"

# Step 2: navigate (also pulls YouTube tab to active state).
$navOut = & wkappbot cdp navigate "{proc:'chrome',cdp:$port,domain:'www.youtube.com'}" $Url 2>&1 | Out-String
if ($navOut -notmatch 'hwnd:(0x[0-9A-Fa-f]+)') {
    wrn 'navigate did not return hwnd -- continuing with domain grap'
    $grapBase = "{proc:'chrome',cdp:$port,domain:'www.youtube.com'}"
} else {
    $hwnd = $Matches[1]
    $grapBase = "{hwnd:$hwnd,proc:'chrome',cdp:$port}"
    ok "YouTube tab hwnd $hwnd"
}

# Lightweight DOM probe: returns ad-state JSON. Read-only -- no .click(), no DOM
# mutation. Just classList check + element visibility. Cheap, no isTrusted issue.
$probeJs = 'var p=document.querySelector(".html5-video-player");var s=document.querySelector(".ytp-skip-ad-button,.ytp-ad-skip-button-modern,[class*=skip-button]");var o=document.querySelector(".ytp-ad-overlay-close-button");var v=document.querySelector("video");var layer=document.querySelector(".ytp-ad-player-overlay,.ytp-ad-module");JSON.stringify({ad:!!(p&&p.classList.contains("ad-showing"))||!!layer,skip:s?s.offsetHeight>0:false,overlay:o?o.offsetHeight>0:false,muted:v?v.muted:false,ending:!!(v&&v.duration>0&&!v.ended&&(v.duration-v.currentTime)<2.0)})'

function Get-AdState {
    $r = & wkappbot a11y read "$grapBase#Doc_RootWebArea" --eval-js $probeJs 2>&1 | Out-String
    foreach ($line in $r -split "`n") {
        if ($line -match '^\{"ad":') {
            try { return $line | ConvertFrom-Json } catch { return $null }
        }
    }
    return $null
}

function Invoke-Skip {
    # UIA InvokePattern is trusted. YouTube cannot detect/block it the way it
    # blocks JS element.click(). The skip button UIA AutomationId is
    # "skip-button:<N>" -- wildcard '#*skip-button*' matches reliably.
    $r = & wkappbot a11y invoke "$grapBase#*skip-button*" --timeout 8 --force 2>&1 | Out-String
    if ($r -match '\[STATE CHANGE DETECTED\]|REMOVED.*광고|REMOVED.*skip|invoke -- UIA Invoke') {
        return $true
    }
    return $false
}

function Invoke-Overlay {
    $r = & wkappbot a11y invoke "$grapBase#*overlay-close*" --timeout 5 --force 2>&1 | Out-String
    return ($r -match 'invoke -- UIA Invoke|STATE CHANGE')
}

function Mute-Video {
    $r = & wkappbot a11y read "$grapBase#Doc_RootWebArea" --eval-js 'document.querySelector("video").muted=true;"ok"' 2>&1 | Out-String
    return ($r -match 'eval-js OK')
}

function Unmute-Video {
    $r = & wkappbot a11y read "$grapBase#Doc_RootWebArea" --eval-js 'document.querySelector("video").muted=false;"ok"' 2>&1 | Out-String
    return ($r -match 'eval-js OK')
}

function Test-RenderHealth {
    # CDP screenshot probe: if Chrome renderer is dead, screenshot fails or returns tiny file
    $tmp = [System.IO.Path]::GetTempFileName() + '.png'
    $r = & wkappbot cdp capture "$grapBase" 2>&1 | Out-String
    if ($r -match 'error|fail|DEAD|not connected' -or -not ($r -match '\.png')) { return $false }
    # Also check readyState via eval-js
    $rs = & wkappbot a11y read "$grapBase#Doc_RootWebArea" --eval-js 'document.readyState' 2>&1 | Out-String
    if ($rs -match "eval-js OK: (complete|interactive)") { return $true }
    return $false
}

function Restart-Chrome {
    wrn 'Render dead detected -- killing Chrome and reopening'
    $openOut = & wkappbot cdp open $Url 2>&1 | Out-String
    if ($openOut -match 'cdp:(\d+)') {
        $script:port = [int]$Matches[1]
        if ($openOut -match 'hwnd:(0x[0-9A-Fa-f]+)') {
            $script:grapBase = "{hwnd:$($Matches[1]),proc:'chrome',cdp:$($script:port)}"
        } else {
            $script:grapBase = "{proc:'chrome',cdp:$($script:port),domain:'www.youtube.com'}"
        }
        ok "Chrome restarted on port $($script:port)"
        return $true
    }
    err 'Chrome restart failed'
    return $false
}

function Test-RenderHealth {
    # CDP screenshot probe: if Chrome renderer is dead, screenshot fails or returns tiny file
    $tmp = [System.IO.Path]::GetTempFileName() + '.png'
    $r = & wkappbot cdp capture "$grapBase" 2>&1 | Out-String
    if ($r -match 'error|fail|DEAD|not connected' -or -not ($r -match '\.png')) { return $false }
    # Also check readyState via eval-js
    $rs = & wkappbot a11y read "$grapBase#Doc_RootWebArea" --eval-js 'document.readyState' 2>&1 | Out-String
    if ($rs -match "eval-js OK: (complete|interactive)") { return $true }
    return $false
}

function Restart-Chrome {
    wrn 'Render dead detected -- killing Chrome and reopening'
    $openOut = & wkappbot cdp open $Url 2>&1 | Out-String
    if ($openOut -match 'cdp:(\d+)') {
        $script:port = [int]$Matches[1]
        if ($openOut -match 'hwnd:(0x[0-9A-Fa-f]+)') {
            $script:grapBase = "{hwnd:$($Matches[1]),proc:'chrome',cdp:$($script:port)}"
        } else {
            $script:grapBase = "{proc:'chrome',cdp:$($script:port),domain:'www.youtube.com'}"
        }
        ok "Chrome restarted on port $($script:port)"
        return $true
    }
    err 'Chrome restart failed'
    return $false
}

inf "Polling every ${PollSeconds}s (UIA-invoke mode). Ctrl+C to stop."
$start = Get-Date
$skipCount = 0
$overlayCount = 0
$consecutiveFails = 0
$wasMuted = $false

while ($true) {
    if ($RunMinutes -gt 0 -and ((Get-Date) - $start).TotalMinutes -ge $RunMinutes) {
        ok "RunMinutes ($RunMinutes) reached. Skips=$skipCount Overlays=$overlayCount"
        break
    }

    $state = Get-AdState
    if ($null -eq $state) {
        $consecutiveFails++
        if ($consecutiveFails -ge 5) {
            wrn 'Probe failed 5 times -- checking render health'
            if (-not (Test-RenderHealth)) {
                wrn '[RENDER DEAD] Chrome renderer failed -- restarting'
                Restart-Chrome | Out-Null
            }
            $consecutiveFails = 0
        }
        Start-Sleep -Seconds $PollSeconds
        continue
    }
    $consecutiveFails = 0

    # Pre-emptive mute: video ending -> mute before next ad fires
    if ($state.ending -and -not $state.muted -and -not $wasMuted) {
        if (Mute-Video) { inf "Video ending -- pre-muted for next ad"; $wasMuted = $true }
    }

    if ($state.ad) {
        if (-not $state.muted) {
            if (Mute-Video) { wrn 'Ad detected -- MUTED'; $wasMuted = $true }
        }
        if ($state.skip) {
            inf 'Skip visible -- invoking UIA'
            if (Invoke-Skip) {
                $skipCount++
                ok "SKIPPED ad #$skipCount"
                Unmute-Video | Out-Null; $wasMuted = $false
                Start-Sleep -Seconds 1
            }
        }
    } elseif ($state.overlay) {
        if (Invoke-Overlay) { $overlayCount++; ok "Closed overlay #$overlayCount" }
    } elseif ($wasMuted) {
        # Ad ended without skip -- unmute
        Unmute-Video | Out-Null; $wasMuted = $false; ok 'Ad ended -- unmuted'
    }
    Start-Sleep -Seconds $PollSeconds
}