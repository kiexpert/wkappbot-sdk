#Requires -Version 5.1
# youtube-ad-skipper.ps1 -- UIA ad skipper. Kills Chrome first, opens VISIBLE YouTube.
# Mutes via keyboard shortcut M, skips via UIA InvokePattern on But_skip element.
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

Add-Type -TypeDefinition @"
using System; using System.Runtime.InteropServices;
public class WinShow {
    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr h, int n);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
}
"@ -ErrorAction SilentlyContinue

# STEP 1: Kill all existing Chrome sessions
inf 'Killing existing Chrome sessions...'
& "C:\Windows\System32\taskkill.exe" /F /IM chrome.exe 2>$null
Start-Sleep -Seconds 2

# STEP 2: Open fresh Chrome
inf "Opening $Url"
$openOut = wkappbot cdp open $Url 2>&1 | Out-String
if ($openOut -notmatch 'cdp:(\d+)') { err "cdp open failed: $openOut"; exit 1 }
$port = [int]$Matches[1]
$hwnd = if ($openOut -match 'hwnd:(0x[0-9A-Fa-f]+)') { $Matches[1] } else { $null }
ok "Port $port hwnd $hwnd"

$g = if ($hwnd) { "{hwnd:$hwnd,proc:'chrome',cdp:$port}" } else { "{proc:'chrome',cdp:$port,domain:'www.youtube.com'}" }

# STEP 3: Force-navigate to override Chrome session restore
inf "Navigating to $Url (overrides session restore)"
wkappbot cdp navigate "$g" $Url 2>&1 | Out-Null
Start-Sleep -Seconds 2

# STEP 4: Make Chrome window VISIBLE via Win32 ShowWindow (cdp open uses SW_HIDE)
if ($hwnd) {
    $h = [IntPtr][Convert]::ToInt64($hwnd, 16)
    [WinShow]::ShowWindow($h, 9)  | Out-Null  # SW_RESTORE
    Start-Sleep -Milliseconds 300
    [WinShow]::ShowWindow($h, 3)  | Out-Null  # SW_MAXIMIZE
    [WinShow]::SetForegroundWindow($h) | Out-Null
    ok "Chrome shown and maximized: hwnd=$hwnd"
} else {
    wkappbot a11y maximize "$g" 2>$null
    ok 'Chrome maximize (no hwnd fallback)'
}
Start-Sleep -Seconds 2

$muted = $false
$skipCount = 0
$start = Get-Date

inf "Monitoring for ads every ${PollSeconds}s. Ctrl+C to stop."

while ($true) {
    if ($RunMinutes -gt 0 -and ((Get-Date) - $start).TotalMinutes -ge $RunMinutes) {
        ok "Done. Skips=$skipCount"; break
    }

    # Check for skip button via UIA (But_skip automation ID)
    $found = wkappbot a11y find "$g#But_skip;*skip-button*" --timeout 1 2>&1 | Out-String

    if ($LASTEXITCODE -eq 0 -or $found -match 'TARGET.*skip') {
        if (-not $muted) {
            wkappbot a11y type "$g" --hotkey m --force 2>$null
            $muted = $true
            wrn 'Ad detected -- MUTED (keyboard M)'
        }
        $r = wkappbot a11y invoke "$g#But_skip;*skip-button*" --timeout 8 --force 2>&1 | Out-String
        if ($r -match 'STATE CHANGE|REMOVED|invoke.*UIA') {
            $skipCount++
            ok "SKIPPED ad #$skipCount"
            Start-Sleep -Seconds 1
            if ($muted) {
                wkappbot a11y type "$g" --hotkey m --force 2>$null
                $muted = $false
                ok 'Unmuted'
            }
        }
    } elseif ($muted) {
        wkappbot a11y type "$g" --hotkey m --force 2>$null
        $muted = $false
        ok 'Ad ended -- unmuted'
    }

    Start-Sleep -Seconds $PollSeconds
}
