#Requires -Version 5.1
# youtube-ad-skipper.ps1 -- CDP reload-based ad skipper (v2)
# Strategy: poll for ads via eval-js, reload with t=cur+0.1&end=dur-0.1 to skip
param(
    [string] $Url = 'https://www.youtube.com/results?search_query=%EC%8A%88%ED%8D%BC%EA%B0%9C%EB%AF%B8+%EC%B5%9C%EC%8B%A0&sp=CAI%253D',
    [int]    $RunMinutes = 0,
    [int]    $PollSeconds = 2
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$ErrorActionPreference = 'Continue'
function ts   { Get-Date -Format 'HH:mm:ss' }
function ok($m)  { Write-Host "$(ts)  OK  $m" -ForegroundColor Green;  [Console]::Out.Flush() }
function inf($m) { Write-Host "$(ts)  ..  $m" -ForegroundColor Cyan;   [Console]::Out.Flush() }
function wrn($m) { Write-Host "$(ts)  !!  $m" -ForegroundColor Yellow; [Console]::Out.Flush() }
function err($m) { Write-Host "$(ts)  XX  $m" -ForegroundColor Red;    [Console]::Out.Flush() }

# STEP 1: Open YouTube via CDP
inf "Opening $Url"
$openOut = & wkappbot cdp open $Url 2>&1 | Out-String
if ($openOut -notmatch 'cdp:(\d+)') { err 'cdp open failed'; Write-Host $openOut; exit 1 }
$port = [int]$Matches[1]
$grap = "{proc:'chrome',cdp:$port,domain:'www.youtube.com'}"
ok "CDP port $port"

$deadline = if ($RunMinutes -gt 0) { (Get-Date).AddMinutes($RunMinutes) } else { $null }

# eval-js helper
function Eval($js) {
    $out = & wkappbot a11y read "$grap#Doc_RootWebArea" --eval-js $js 2>&1 | Out-String
    ($out -split "`n" | Where-Object { $_ -notmatch '^\[|^{\"_\"|^#|^--' } | Select-Object -First 1).Trim()
}

# STEP 2: Setup -- set &end=dur-0.1 on current video to prevent end-roll
function SetupClip {
    $state = Eval "var p=document.querySelector('#movie_player');var u=new URL(location.href);var vid=u.searchParams.get('v');var dur=p&&p.getDuration?p.getDuration():0;var clipped=u.searchParams.get('_clipped');JSON.stringify({vid:vid,dur:dur,clipped:clipped,clip:u.searchParams.has('clip')})"
    if ($state -match '"vid":"([^"]+)".*"dur":([0-9.]+).*"clipped":"?([^",}]*)"?.*"clip":(true|false)') {
        $vid     = $Matches[1]
        $dur     = [double]$Matches[2]
        $clipped = $Matches[3]
        $isClip  = $Matches[4] -eq 'true'
        if ($isClip) { inf "Clip playback detected -- skipping setup"; return $vid }
        if ($clipped -ne '1' -and $dur -gt 2) {
            $endT = [Math]::Floor($dur - 0.1)
            inf "Setting &end=$endT for vid=$vid"
            & wkappbot cdp navigate $grap "https://www.youtube.com/watch?v=$vid&end=$endT&_clipped=1" 2>&1 | Out-Null
        }
        return $vid
    }
    return $null
}

# STEP 3: Main ad-watch loop
inf "Watching for ads (poll every ${PollSeconds}s)..."
$lastVid = $null
$reloadCount = @{}

while ($true) {
    if ($deadline -and (Get-Date) -gt $deadline) { ok "Time limit reached"; break }

    $adState = Eval "
var p=document.querySelector('#movie_player');
var u=new URL(location.href);
var vid=u.searchParams.get('v');
var dur=p&&p.getDuration?p.getDuration():0;
var cur=p&&p.getCurrentTime?p.getCurrentTime():0;
var isMid=document.documentElement.classList.contains('ad-showing');
var isEnd=!!document.querySelector('.ytp-ad-player-overlay-layout');
var hasSkip=!!document.querySelector('.ytp-skip-ad-button,.ytp-ad-skip-button-container');
var isClip=u.searchParams.has('clip');
var clipped=u.searchParams.get('_clipped');
var banner=!!document.querySelector('#dismiss-button');
JSON.stringify({vid:vid,dur:dur,cur:cur,isMid:isMid,isEnd:isEnd,hasSkip:hasSkip,isClip:isClip,clipped:clipped,banner:banner})
"
    if ($adState -notmatch '"vid":"([^"]+)"') { Start-Sleep $PollSeconds; continue }

    if ($adState -match '"vid":"([^"]+)"') { $vid = $Matches[1] }
    if ($adState -match '"dur":([0-9.]+)')  { $dur = [double]$Matches[1] }
    if ($adState -match '"cur":([0-9.]+)')  { $cur = [double]$Matches[1] }
    $isMid  = $adState -match '"isMid":true'
    $isEnd  = $adState -match '"isEnd":true'
    $hasSkip= $adState -match '"hasSkip":true'
    $isClip = $adState -match '"isClip":true'
    $clipped= $adState -match '"clipped":"1"'
    $banner = $adState -match '"banner":true'

    # New video: setup clip range
    if ($vid -ne $lastVid -and -not $isClip) {
        $lastVid = $vid
        $reloadCount[$vid] = 0
        if (-not $clipped -and $dur -gt 2) {
            $endT = [Math]::Floor($dur - 0.1)
            inf "New video $vid -- setting end=$endT"
            & wkappbot cdp navigate $grap "https://www.youtube.com/watch?v=$vid&end=$endT&_clipped=1" 2>&1 | Out-Null
            Start-Sleep 2; continue
        }
    }

    # Dismiss premium banner
    if ($banner) {
        inf "Dismissing premium banner"
        & wkappbot a11y invoke "$grap#*dismiss-button*" --force --timeout 3 2>&1 | Out-Null
    }

    # Ad handling
    if (($isMid -or $isEnd) -and -not $isClip) {
        $cnt = $reloadCount[$vid]
        if ($cnt -ge 5) { wrn "Max reloads ($cnt) for $vid -- skipping"; Start-Sleep $PollSeconds; continue }

        if ($hasSkip) {
            inf "Skip button found -- invoking"
            $r = & wkappbot a11y invoke "$grap#*skip-ad-button*" --force --timeout 5 2>&1 | Out-String
            if ($r -match '\[OK\]') { ok "Ad skipped via button"; $reloadCount[$vid]++; Start-Sleep 2; continue }
        }

        if ($isEnd) {
            # End-roll: reload to dur-1 paused
            $t   = [Math]::Max(0, [Math]::Floor($dur - 1))
            $endT = [Math]::Floor($dur - 0.1)
            wrn "End-roll detected -- reloading t=$t end=$endT"
            & wkappbot cdp navigate $grap "https://www.youtube.com/watch?v=$vid&t=$t&end=$endT&_clipped=1" 2>&1 | Out-Null
            Start-Sleep 2
            Eval "document.querySelector('#movie_player')?.pauseVideo()" | Out-Null
        } else {
            # Mid/pre-roll: jump past ad cue point
            $t   = [Math]::Max(0, $cur + 0.1)
            $endT = [Math]::Floor($dur - 0.1)
            wrn "Mid/pre-roll detected at t=$([Math]::Floor($cur)) -- reloading to t=$t"
            & wkappbot cdp navigate $grap "https://www.youtube.com/watch?v=$vid&t=$t&end=$endT&_clipped=1" 2>&1 | Out-Null
        }
        $reloadCount[$vid]++
        ok "Reload #$($reloadCount[$vid]) for $vid"
        Start-Sleep 3
    }

    Start-Sleep $PollSeconds
}