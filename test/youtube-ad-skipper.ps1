#Requires -Version 5.1
# youtube-ad-skipper.ps1 -- Inject JS into YouTube Chrome page via CDP.
# Uses a11y read #Doc_RootWebArea --eval-js for direct JS injection.
# console.log output forwarded to stderr as [CDP:CONSOLE:LOG] msg.
# Usage: powershell -File test\youtube-ad-skipper.ps1 [-Url <url>]
param( [string] $Url = 'https://www.youtube.com/watch?v=dQw4w9WgXcQ' )
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$ErrorActionPreference = 'Continue'
function ts  { Get-Date -Format 'HH:mm:ss' }
function ok($m)  { Write-Host "$(ts)  OK  $m" -ForegroundColor Green;  [Console]::Out.Flush() }
function inf($m) { Write-Host "$(ts)  ..  $m" -ForegroundColor Cyan;   [Console]::Out.Flush() }
function wrn($m) { Write-Host "$(ts)  !!  $m" -ForegroundColor Yellow; [Console]::Out.Flush() }

$js = '(function(){if(window.__wkAd)clearInterval(window.__wkAd);var t=null,n=0;function tick(){var p=document.querySelector(".html5-video-player");if(!p||!p.classList.contains("ad-showing")){t=null;return}if(!t)t=Date.now();var s=document.querySelector(".ytp-skip-ad-button,.ytp-ad-skip-button-modern");if(s&&s.offsetHeight>0){s.click();n++;console.log("[wk-ad] skip#"+n);t=null;return}var o=document.querySelector(".ytp-ad-overlay-close-button");if(o&&o.offsetHeight>0){o.click();n++;console.log("[wk-ad] overlay#"+n);t=null;return}if(Date.now()-t>3000){console.log("[wk-ad] reload");t=null;var vid=document.querySelector("video");var sec=vid?Math.floor(vid.currentTime):0;var u=new URL(location.href);u.searchParams.set("t",sec+"s");location.href=u.toString()}}window.__wkAd=setInterval(tick,500);document.addEventListener("yt-navigate-finish",function(){t=null});return "wk-ad-skipper-ok"})()'

inf "Opening $Url"
$openOut = wkappbot cdp open $Url 2>&1 | Out-String
if ($openOut -notmatch 'cdp:(\d+)') { Write-Host 'cdp open failed' -ForegroundColor Red; exit 1 }
$port = [int]$Matches[1]
$hwnd  = if ($openOut -match 'hwnd:(0x[0-9A-Fa-f]+)') { $Matches[1] } else { $null }
ok "port $port hwnd $hwnd"
Start-Sleep -Seconds 2
$g = if ($hwnd) { "{hwnd:$hwnd,proc:'chrome',cdp:$port}" } else { "{proc:'chrome',domain:'www.youtube.com',cdp:$port}" }

function Inject {
    inf 'Injecting JS...'
    $r = wkappbot-core.exe a11y read "$g#Doc_RootWebArea" --eval-js $js 2>&1 | Out-String
    if ($r -match 'wk-ad-skipper-ok') { ok 'JS injected and running' }
    elseif ($r -match 'CDP:CONSOLE:LOG.*wk-ad') { ok 'Already running (console active)' }
    else { wrn "Inject output (last 100): $($r -replace '(?s).+(.{100})', '$1')" }
}

Inject
inf 'Monitoring console (re-inject every 30s, Ctrl+C to stop)...'
while ($true) {
    Start-Sleep -Seconds 30
    $alive = wkappbot a11y read "$g#Doc_RootWebArea" --eval-js 'typeof window.__wkAd' 2>&1 | Out-String
    if ($alive -match 'eval-js OK: number') { inf 'Heartbeat: JS alive' }
    else { wrn 'JS cleared -- re-injecting'; Inject }
}
