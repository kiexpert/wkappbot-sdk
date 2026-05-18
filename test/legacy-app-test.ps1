# Legacy app UIA 3-tier fallback test.
# Runs against built-in Windows apps (notepad/calc/mspaint) that ship on
# GitHub Actions windows-latest runners. Pure Win32/MFC controls -- ideal
# for exercising the UIA -> Win32 -> SendInput fallback chain. Test style
# mirrors cdp-a11y-real-sites.ps1 (Invoke-WK / Run-WK / Add-Pass / Add-Bug).

$ErrorActionPreference = "Continue"
$CI = $env:GITHUB_ACTIONS -eq 'true'

# Start Eye at the top (idempotent if already running)
Start-Process wkappbot.exe -ArgumentList eye -WindowStyle Hidden -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

$LogDir = "bin/wkappbot.hq/logs/legacy-apps"
New-Item -Force -ItemType Directory -Path $LogDir | Out-Null

$script:LastWKOutput = ""
$script:Results = @{}
$script:TotalPass = 0
$script:TotalFail = 0
$script:TotalBugs = 0
$script:TotalSkip = 0

function Invoke-WK {
    param([string[]]$WkArgs, [int]$TimeoutSec = 15)

    $logBase = Join-Path $env:TEMP "wk-legacy-$(Get-Random)"
    $proc = Start-Process wkappbot -ArgumentList $WkArgs -RedirectStandardOutput "$logBase.out" -RedirectStandardError "$logBase.err" -PassThru -NoNewWindow
    $exited = $proc.WaitForExit($TimeoutSec * 1000)
    if (-not $exited) {
        try { $proc.Kill() } catch {}
        $out = "TIMEOUT after ${TimeoutSec}s: wkappbot $($WkArgs -join ' ')"
        $script:LastWKOutput = $out
        Remove-Item "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue
        return [pscustomobject]@{ Ok = $false; ExitCode = 124; Output = $out }
    }

    $out = (Get-Content "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue) -join "`n"
    $script:LastWKOutput = $out
    $exitCode = $proc.ExitCode
    Remove-Item "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue
    return [pscustomobject]@{ Ok = ($exitCode -eq 0); ExitCode = $exitCode; Output = $out }
}

function Run-WK {
    param([string[]]$WkArgs, [string]$ExpectPattern, [string]$TestName, [int]$TimeoutSec = 15)

    $result = Invoke-WK $WkArgs $TimeoutSec
    if ([string]::IsNullOrEmpty($ExpectPattern)) {
        if ($result.Ok) { return $true }
        return $false
    }
    if ($result.Output -match $ExpectPattern) { return $true }
    return $false
}

function New-AppResult {
    param([string]$Name)
    $script:Results[$Name] = [ordered]@{ Pass = 0; Fail = 0; Bugs = 0; Skip = 0 }
}

function Add-Pass {
    param([string]$App, [string]$Name)
    $script:Results[$App].Pass++
    $script:TotalPass++
    Write-Host "PASS: [$App] $Name"
}

function Add-Fail {
    param([string]$App, [string]$Name, [string]$Detail = "")
    $script:Results[$App].Fail++
    $script:TotalFail++
    if ($Detail) { Write-Host "FAIL: [$App] $Name -- $Detail" } else { Write-Host "FAIL: [$App] $Name" }
}

function Add-Bug {
    param([string]$App, [string]$BugId, [string]$Detail = "")
    $script:Results[$App].Bugs++
    $script:TotalBugs++
    if ($Detail) { Write-Host "Bug-Found: [$App] $BugId -- $Detail" } else { Write-Host "Bug-Found: [$App] $BugId" }
}

function Add-Skip {
    param([string]$App, [string]$Reason)
    $script:Results[$App].Skip++
    $script:TotalSkip++
    Write-Host "SKIP: [$App] $Reason"
}

function Save-TextLog {
    param([string]$App, [string]$Name, [string]$Text)
    $path = Join-Path $LogDir "$($App.ToLowerInvariant())-$Name.log"
    $Text | Out-File -FilePath $path -Encoding utf8
    return $path
}

# Robust launch: returns $true if the window appears, $false otherwise.
function Wait-For-Window {
    param([string]$Grap, [int]$TimeoutSec = 8)
    $deadline = (Get-Date).AddSeconds($TimeoutSec)
    while ((Get-Date) -lt $deadline) {
        $r = Invoke-WK @("a11y","find",$Grap) 4
        if ($r.Output -match "## TARGET") { return $true }
        Start-Sleep -Milliseconds 400
    }
    return $false
}

# CRITICAL (a11y-command-cheatsheet step 21): NEVER taskkill Notepad on
# Win11 -- session restore resurrects all closed tabs. Always use
# `wkappbot a11y close` (WM_CLOSE focusless) instead. Same caution applies
# loosely to other apps that may persist state, so we standardize.
function Close-App {
    param([string]$App, [string]$Grap)
    $r = Invoke-WK @("a11y","close",$Grap) 10
    if ($r.Ok) {
        Add-Pass $App "a11y close $Grap"
    } else {
        Add-Fail $App "a11y close $Grap" "exit:$($r.ExitCode)"
    }
}

function Test-Notepad {
    New-AppResult "NOTEPAD"
    Write-Host ""
    Write-Host "================================================"
    Write-Host "[NOTEPAD] launch + type + read + close"
    Write-Host "================================================"

    # Launch notepad (Win11 modern Notepad has UIA, Win10 classic has Edit control fallback).
    try {
        Start-Process notepad.exe -ErrorAction Stop | Out-Null
    } catch {
        Add-Skip "NOTEPAD" "notepad.exe not available: $_"
        return
    }
    Start-Sleep -Seconds 2

    $grap = "{proc:'notepad'}"
    if (-not (Wait-For-Window $grap 10)) {
        Add-Skip "NOTEPAD" "window did not appear within 10s"
        return
    }
    Add-Pass "NOTEPAD" "launch + window appeared"

    # Find the edit/document control inside notepad.
    $findRes = Invoke-WK @("a11y","find","*notepad*#") 8
    Save-TextLog "NOTEPAD" "find" $findRes.Output | Out-Null
    if ($findRes.Output -match "## TARGET") {
        Add-Pass "NOTEPAD" "find document scope returned TARGET"
    } else {
        Add-Fail "NOTEPAD" "find document scope" "no TARGET in output"
    }

    # Type via focused scope (trailing # = focus-tunnel). This exercises
    # tier 1 (ValuePattern) or tier 3 (SendInput) depending on the control.
    $payload = "wkappbot-legacy-test-$(Get-Random -Maximum 99999)"
    $typeRes = Invoke-WK @("a11y","type",$grap,$payload) 15
    if ($typeRes.Ok) {
        Add-Pass "NOTEPAD" "a11y type into editor"
    } else {
        Add-Fail "NOTEPAD" "a11y type" "exit:$($typeRes.ExitCode)"
    }
    Start-Sleep -Milliseconds 500

    # Read back via UIA -- Win10 classic Notepad: WM_GETTEXT fallback;
    # Win11 modern Notepad: TextPattern. Either should yield the payload.
    $readRes = Invoke-WK @("a11y","read",$grap) 10
    Save-TextLog "NOTEPAD" "read" $readRes.Output | Out-Null
    if ($readRes.Output -match [regex]::Escape($payload)) {
        Add-Pass "NOTEPAD" "read back matches typed payload"
    } else {
        Add-Bug "NOTEPAD" "notepad-read-roundtrip" "typed `"$payload`" but read returned: $($readRes.Output.Substring(0,[Math]::Min(200,$readRes.Output.Length)))"
    }

    # Hotkey via type --hotkey: Ctrl+A select-all (tier 3 SendInput requires focus).
    $hkRes = Invoke-WK @("a11y","type",$grap,"^a","--hotkey") 10
    if ($hkRes.Ok) {
        Add-Pass "NOTEPAD" "hotkey ctrl+a"
    } else {
        Add-Fail "NOTEPAD" "hotkey ctrl+a" "exit:$($hkRes.ExitCode)"
    }

    Close-App "NOTEPAD" $grap

    # Win11 hazard: if modern Notepad restores tabs, a second find should
    # see no TARGET. If it still does, log as a known persistence quirk.
    Start-Sleep -Seconds 1
    $afterClose = Invoke-WK @("a11y","find",$grap) 5
    if ($afterClose.Output -match "## TARGET") {
        Add-Bug "NOTEPAD" "notepad-win11-session-restore" "notepad still findable after a11y close (Win11 tab restore?)"
    } else {
        Add-Pass "NOTEPAD" "notepad gone after close"
    }
}

function Test-Calc {
    New-AppResult "CALC"
    Write-Host ""
    Write-Host "================================================"
    Write-Host "[CALC] launch + button click + result verify"
    Write-Host "================================================"

    try {
        Start-Process calc.exe -ErrorAction Stop | Out-Null
    } catch {
        Add-Skip "CALC" "calc.exe not available: $_"
        return
    }
    # Modern Win10/11 Calculator is a UWP shim -- calc.exe spawns
    # CalculatorApp.exe / Calculator.exe and exits. Discover the real
    # process by matching on title and/or class.
    Start-Sleep -Seconds 3

    $grap = "{title:'Calculator'}"
    if (-not (Wait-For-Window $grap 12)) {
        Add-Skip "CALC" "Calculator window did not appear within 12s"
        return
    }
    Add-Pass "CALC" "launch + window appeared"

    # Switch to Standard mode keystrokes: Alt+1. UIA AutomationIds on the
    # Calculator buttons: num7Button, plusButton, num5Button, equalButton,
    # CalculatorResults (output text).
    $clearRes = Invoke-WK @("a11y","invoke","{title:'Calculator'}#clearButton") 6
    if (-not $clearRes.Ok) {
        # clearEntryButton or clearMemoryButton may differ across versions;
        # not a hard fail.
        Save-TextLog "CALC" "clear-fallback" $clearRes.Output | Out-Null
    }

    $seq = @("num7Button","plusButton","num5Button","equalButton")
    $allOk = $true
    foreach ($btn in $seq) {
        $r = Invoke-WK @("a11y","invoke","{title:'Calculator'}#$btn") 6
        if (-not $r.Ok) {
            Save-TextLog "CALC" "invoke-$btn" $r.Output | Out-Null
            $allOk = $false
        }
    }
    if ($allOk) {
        Add-Pass "CALC" "invoke 7 + 5 ="
    } else {
        Add-Bug "CALC" "calc-uia-button-invoke" "one or more button AutomationIds missing -- Win11 calc may have renamed"
    }

    # Read result. CalculatorResults Name typically reads "Display is 12".
    $readRes = Invoke-WK @("a11y","read","{title:'Calculator'}#CalculatorResults") 8
    Save-TextLog "CALC" "result-read" $readRes.Output | Out-Null
    if ($readRes.Output -match "\b12\b") {
        Add-Pass "CALC" "result contains 12"
    } else {
        Add-Bug "CALC" "calc-result-readback" "expected 12, got: $($readRes.Output.Substring(0,[Math]::Min(200,$readRes.Output.Length)))"
    }

    Close-App "CALC" $grap
}

function Test-MsPaint {
    New-AppResult "MSPAINT"
    Write-Host ""
    Write-Host "================================================"
    Write-Host "[MSPAINT] launch + window enumerate + close"
    Write-Host "================================================"

    try {
        Start-Process mspaint.exe -ErrorAction Stop | Out-Null
    } catch {
        Add-Skip "MSPAINT" "mspaint.exe not available: $_"
        return
    }
    Start-Sleep -Seconds 3

    $grap = "{proc:'mspaint'}"
    if (-not (Wait-For-Window $grap 10)) {
        # Win11 ships Paint as a Store app (PaintApp.exe / mspaint.exe shim).
        $altGrap = "{title:'*Paint*'}"
        if (-not (Wait-For-Window $altGrap 5)) {
            Add-Skip "MSPAINT" "Paint window did not appear within 15s"
            return
        }
        $grap = $altGrap
    }
    Add-Pass "MSPAINT" "launch + window appeared"

    # screenshot exercises CDP-free image capture path.
    $shot = Join-Path $LogDir ("mspaint-" + (Get-Date -Format 'yyyyMMdd-HHmmss') + ".png")
    $shotRes = Invoke-WK @("a11y","screenshot",$grap,"--path",$shot) 15
    if ($shotRes.Ok -and (Test-Path $shot)) {
        Add-Pass "MSPAINT" "screenshot saved $shot"
    } else {
        Add-Fail "MSPAINT" "screenshot" "exit:$($shotRes.ExitCode) file=$shot"
    }

    Close-App "MSPAINT" $grap
}

function Test-WindowsCommand {
    New-AppResult "WINDOWS"
    Write-Host ""
    Write-Host "================================================"
    Write-Host "[WINDOWS] enumerate top-level windows"
    Write-Host "================================================"

    # Launch a known-titled process so the listing has at least one
    # deterministic match. Use notepad.exe; clean up afterwards.
    try { Start-Process notepad.exe -ErrorAction Stop | Out-Null } catch {
        Add-Skip "WINDOWS" "could not seed listing with notepad.exe"
        return
    }
    Start-Sleep -Seconds 2

    $listRes = Invoke-WK @("windows","*notepad*") 10
    Save-TextLog "WINDOWS" "windows-notepad" $listRes.Output | Out-Null
    if ($listRes.Output -match "notepad" -or $listRes.Output -match "Notepad") {
        Add-Pass "WINDOWS" "windows *notepad* listed at least one notepad window"
    } else {
        Add-Fail "WINDOWS" "windows *notepad*" "no notepad row in output"
    }

    # Clean up the seed window.
    Invoke-WK @("a11y","close","{proc:'notepad'}") 8 | Out-Null
}

function Write-Summary {
    Write-Host ""
    Write-Host "================================================"
    Write-Host " Legacy-App UIA Test Summary"
    Write-Host "================================================"
    foreach ($app in @("NOTEPAD","CALC","MSPAINT","WINDOWS")) {
        if (-not $script:Results.ContainsKey($app)) { continue }
        $r = $script:Results[$app]
        Write-Host ("{0}: PASS={1} FAIL={2} BUGS={3} SKIP={4}" -f $app, $r.Pass, $r.Fail, $r.Bugs, $r.Skip)
    }
    Write-Host ("TOTAL: PASS={0} FAIL={1} BUGS={2} SKIP={3}" -f $script:TotalPass, $script:TotalFail, $script:TotalBugs, $script:TotalSkip)
}

Write-Host "================================================"
Write-Host " WKAppBot legacy-app UIA integration test"
Write-Host " Logs: $LogDir  CI=$CI"
Write-Host "================================================"

Test-Notepad
Test-Calc
Test-MsPaint
Test-WindowsCommand

Write-Summary

if ($script:TotalFail -gt 0 -or $script:TotalBugs -gt 0) {
    exit 1
}
exit 0
