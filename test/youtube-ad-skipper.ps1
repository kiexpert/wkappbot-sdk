#Requires -Version 5.1
# youtube-ad-skipper.ps1 -- Auto-skip YouTube ads via CDP a11y
# Loop: try skip button every 3s, try overlay close button every 3s.
# Covers: skippable pre-roll/mid-roll, overlay banner ads.
# Usage: powershell -File test\youtube-ad-skipper.ps1 [-Url <url>] [-RunMinutes <N>] [-Verbose]
param(
    [string] $Url        = 'https://www.youtube.com/watch?v=dQw4w9WgXcQ',
    [int]    $RunMinutes = 60,
    [switch] $Verbose
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$ErrorActionPreference = 'Continue'
function ts  { Get-Date -Format 'HH:mm:ss' }
function ok($m)  { Write-Host "$(ts)  OK  $m" -ForegroundColor Green;  [Console]::Out.Flush() }
function inf($m) { Write-Host "$(ts)  ..  $m" -ForegroundColor Cyan;   [Console]::Out.Flush() }
function wrn($m) { Write-Host "$(ts)  !!  $m" -ForegroundColor Yellow; [Console]::Out.Flush() }

inf "Opening $Url"
$openOut = wkappbot cdp open $Url 2>&1 | Out-String
if ($openOut -notmatch 'cdp:(\d+)') { Write-Host "cdp open failed" -ForegroundColor Red; exit 1 }
$port = [int]$Matches[1]
$hwnd = if ($openOut -match 'hwnd:(0x[0-9A-Fa-f]+)') { $Matches[1] } else { $null }
ok "Chrome port $port hwnd $hwnd"
Start-Sleep -Seconds 3

$g       = if ($hwnd) { "{hwnd:$hwnd,cdp:$port}" } else { "{domain:'www.youtube.com',cdp:$port}" }
$adCount = 0
$deadline = (Get-Date).AddMinutes($RunMinutes)
inf "Monitoring on port $port for $RunMinutes min..."

while ((Get-Date) -lt $deadline) {
    # 1. Skip button (skippable pre-roll/mid-roll ads)
    wkappbot a11y invoke "$g#*Skip Ad*;*Skip Ads*" --timeout 1 2>$null
    if ($LASTEXITCODE -eq 0) {
        $adCount++; ok "[$adCount] Skipped!"; Start-Sleep -Seconds 2; continue
    }

    # 2. Overlay/banner close button
    wkappbot a11y invoke "$g#*.ytp-ad-overlay-close-button*" --timeout 1 2>$null
    if ($LASTEXITCODE -eq 0) {
        $adCount++; ok "[$adCount] Overlay closed!"; Start-Sleep -Seconds 1; continue
    }

    if ($Verbose) { inf "Clean" }
    Start-Sleep -Seconds 3
}

ok "Done. Ads skipped: $adCount"
