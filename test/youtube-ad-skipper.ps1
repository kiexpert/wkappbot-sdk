#Requires -Version 5.1
# youtube-ad-skipper.ps1 -- Auto-skip YouTube ads via CDP a11y
# Usage: powershell -File test\youtube-ad-skipper.ps1 [-Url <url>] [-RunMinutes <N>] [-Verbose]
param(
    [string] $Url        = 'https://www.youtube.com',
    [int]    $RunMinutes = 60,
    [switch] $Verbose
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$ErrorActionPreference = 'Continue'
function ts  { Get-Date -Format 'HH:mm:ss' }
function ok($m)  { Write-Host "$(ts)  OK  $m" -ForegroundColor Green;  [Console]::Out.Flush() }
function inf($m) { Write-Host "$(ts)  ..  $m" -ForegroundColor Cyan;   [Console]::Out.Flush() }
function wrn($m) { Write-Host "$(ts)  !!  $m" -ForegroundColor Yellow; [Console]::Out.Flush() }

# Open YouTube in project Chrome
inf "Opening $Url"
$openOut = wkappbot cdp open $Url 2>&1 | Out-String
if ($openOut -notmatch 'cdp:(\d+)') { Write-Host "cdp open failed" -ForegroundColor Red; exit 1 }
$port = [int]$Matches[1]
$hwnd = if ($openOut -match 'hwnd:(0x[0-9A-Fa-f]+)') { $Matches[1] } else { $null }
ok "Chrome port $port"
Start-Sleep -Seconds 3

$g = if ($hwnd) { "{hwnd:$hwnd,cdp:$port}" } else { "{domain:'www.youtube.com',cdp:$port}" }
$adCount   = 0
$reloads   = 0
$deadline  = (Get-Date).AddMinutes($RunMinutes)
$currentUrl = $Url

inf "Monitoring for ads (port $port, $RunMinutes min)..."

while ((Get-Date) -lt $deadline) {
    # -- Try skip button first (appears after ~5s of non-skippable ad) --
    wkappbot a11y invoke "$g#*광고 건너뛰기*;*Skip Ad*;*Skip Ads*;*광고 건너뜀*" --timeout 1 2>$null
    if ($LASTEXITCODE -eq 0) {
        $adCount++; ok "[$adCount] Ad skipped (skip button)"; Start-Sleep -Seconds 2; continue
    }

    # -- Overlay / banner close button --
    wkappbot a11y invoke "$g#*.ytp-ad-overlay-close-button*" --timeout 1 2>$null
    if ($LASTEXITCODE -eq 0) {
        $adCount++; ok "[$adCount] Overlay ad closed"; Start-Sleep -Seconds 1; continue
    }

    # -- Detect ad-showing via DOM class --
    $jsAd = wkappbot a11y read "$g#Doc_RootWebArea" --eval-js 'document.querySelector(".html5-video-player")?.classList.contains("ad-showing")' 2>&1 | Out-String
    if ($jsAd -match 'true') {
        wrn "Ad detected -- waiting up to 35s for skip button..."
        $skipBy = (Get-Date).AddSeconds(35)
        $skipped = $false
        while ((Get-Date) -lt $skipBy) {
            Start-Sleep -Seconds 2
            wkappbot a11y invoke "$g#*광고 건너뛰기*;*Skip Ad*;*Skip Ads*" --timeout 2 2>$null
            if ($LASTEXITCODE -eq 0) { $adCount++; ok "[$adCount] Ad skipped after wait"; $skipped = $true; break }
        }
        if (-not $skipped) {
            $reloads++
            wrn "Ad unskippable -- reloading ($reloads): $currentUrl"
            wkappbot cdp open $currentUrl 2>$null
            Start-Sleep -Seconds 5
        }
        continue
    }

    # -- Capture current URL for reload reference --
    $urlOut = wkappbot a11y read "$g#Doc_RootWebArea" --eval-js 'location.href' 2>&1 | Out-String
    if ($urlOut -match 'https://[^\s"]+') { $currentUrl = $Matches[0] }

    if ($Verbose) { inf "Clean (url=$($currentUrl -replace 'https://www.youtube.com','YT'))"; }
    Start-Sleep -Seconds 3
}

ok "Session ended. Ads skipped: $adCount | Reloads: $reloads"
