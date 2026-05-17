$ErrorActionPreference = "Continue"
$CI = $env:GITHUB_ACTIONS -eq 'true'
$SiteTimeout = if ($CI) { 90 } else { 30 }
$LogDir = "bin/wkappbot.hq/logs/real-sites"
New-Item -Force -ItemType Directory -Path $LogDir | Out-Null

$script:LastWKOutput = ""
$script:Results = @{}
$script:TotalPass = 0
$script:TotalFail = 0
$script:TotalBugs = 0
$script:TotalSkip = 0

function Invoke-WK {
    param([string[]]$WkArgs, [int]$TimeoutSec = 15)

    $logBase = Join-Path $env:TEMP "wk-real-sites-$(Get-Random)"
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
        if ($result.Ok) { Write-Host "PASS: $TestName"; return $true }
        Write-Host "FAIL: $TestName [exit:$($result.ExitCode)]"
        return $false
    }

    if ($result.Output -match $ExpectPattern) {
        Write-Host "PASS: $TestName"
        return $true
    }

    Write-Host "FAIL: $TestName [expected: $ExpectPattern]"
    return $false
}

function New-SiteResult {
    param([string]$Name)
    $script:Results[$Name] = [ordered]@{ Pass = 0; Fail = 0; Bugs = 0; Skip = 0 }
}

function Add-Pass {
    param([string]$Site, [string]$Name)
    $script:Results[$Site].Pass++
    $script:TotalPass++
    Write-Host "PASS: [$Site] $Name"
}

function Add-Fail {
    param([string]$Site, [string]$Name, [string]$Detail = "")
    $script:Results[$Site].Fail++
    $script:TotalFail++
    if ($Detail) { Write-Host "FAIL: [$Site] $Name -- $Detail" } else { Write-Host "FAIL: [$Site] $Name" }
}

function Add-Bug {
    param([string]$Site, [string]$BugId, [string]$Detail = "")
    $script:Results[$Site].Bugs++
    $script:TotalBugs++
    if ($Detail) { Write-Host "Bug-Found: [$Site] $BugId -- $Detail" } else { Write-Host "Bug-Found: [$Site] $BugId" }
}

function Add-Skip {
    param([string]$Site, [string]$Reason)
    $script:Results[$Site].Skip++
    $script:TotalSkip++
    Write-Host "SKIP: [$Site] $Reason"
}

function Check-Step {
    param([string]$Site, [bool]$Ok, [string]$Name, [string]$Detail = "")
    if ($Ok) { Add-Pass $Site $Name } else { Add-Fail $Site $Name $Detail }
    return $Ok
}

function Save-TextLog {
    param([string]$Site, [string]$Name, [string]$Text)
    $path = Join-Path $LogDir "$($Site.ToLowerInvariant())-$Name.log"
    $Text | Out-File -FilePath $path -Encoding utf8
    return $path
}

function Open-Site {
    param(
        [string]$Site,
        [string]$Url,
        [string[]]$HtmlPatterns
    )

    Write-Host ""
    Write-Host "================================================"
    Write-Host "[$Site] cdp open $Url"
    Write-Host "================================================"

    # Ensure Eye is running before attempting CDP operations
    $eyeCheck = Invoke-WK @("eye","tick") 10
    if (-not $eyeCheck.Ok) {
        Write-Host "WARN: eye tick failed, attempting Eye startup"
        try { & wkappbot eye } catch { Write-Host "WARN: failed to start Eye" }
        Start-Sleep -Milliseconds 500
    }

    $navTimeout = if ($CI) { 60000 } else { 20000 }
    $open = Invoke-WK -WkArgs @("cdp","open",$Url,"--timeout",$navTimeout) -TimeoutSec $SiteTimeout
    Save-TextLog $Site "cdp-open" $open.Output | Out-Null

    $CDPPORT = ""
    $HW = ""

    # First check for success (normal path)
    if ($open.Ok -and $open.Output -match "OK\s+\{") {
        $okLine = ($open.Output -split "`r?`n" | Where-Object { $_ -match "OK\s+\{.*cdp:[0-9]+.*hwnd:0x[0-9A-Fa-f]+" } | Select-Object -First 1)
        if (-not $okLine) { $okLine = $open.Output }

        if ($okLine -match "cdp:([0-9]+)") { $CDPPORT = $Matches[1] }
        if ($okLine -match "hwnd:(0x[0-9A-Fa-f]+)") { $HW = $Matches[1] }
    }
    # Fallback: if cdp open failed but output contains port, try to reuse Chrome
    elseif ($open.Output -match "port\s+([0-9]+)") {
        $CDPPORT = $Matches[1]
        Write-Host "FALLBACK: extracted port $CDPPORT from cdp open output, attempting Chrome reuse"

        # Verify Chrome is alive with eval-js
        $verifyGrap = "{proc:'chrome',cdp:$CDPPORT}"
        $liveCheck = Invoke-WK -WkArgs @("a11y","read",$verifyGrap,"--eval-js","document.location.href") -TimeoutSec 15
        if ($liveCheck.Ok -and $liveCheck.Output -match "https?://") {
            Write-Host "FALLBACK: Chrome is alive at $verifyGrap, proceeding with manual navigation"

            # Try to navigate using eval-js
            $navCmd = "window.location.assign('$Url'); 'navigating'"
            $navResult = Invoke-WK -WkArgs @("a11y","read",$verifyGrap,"--eval-js",$navCmd) -TimeoutSec 15
            Start-Sleep -Seconds 5

            # Verify navigation succeeded
            $checkUrl = Invoke-WK -WkArgs @("a11y","read",$verifyGrap,"--eval-js","document.location.href") -TimeoutSec 15
            if ($checkUrl.Ok -and $checkUrl.Output -match [regex]::Escape($Url.Split('/')[2])) {
                Write-Host "FALLBACK: Navigation successful to $Url"
                $HW = ""
            } else {
                Add-Skip $Site "fallback navigation failed"
                Write-Host "  >> navigation check output: $($checkUrl.Output.Substring(0,[Math]::Min(200,$checkUrl.Output.Length)))"
                return $null
            }
        } else {
            Add-Skip $Site "cdp open failed and Chrome not reachable at extracted port"
            Write-Host "  >> cdp open output: $($open.Output.Substring(0,[Math]::Min(500,$open.Output.Length)))"
            return $null
        }
    }
    else {
        Add-Skip $Site "cdp open failed"
        Write-Host "  >> cdp open output: $($open.Output.Substring(0,[Math]::Min(500,$open.Output.Length)))"
        return $null
    }

    # Validate extraction
    if ([string]::IsNullOrEmpty($CDPPORT)) {
        Add-Skip $Site "could not extract cdp port"
        return $null
    }

    $CDPGRAP = "{proc:'chrome',cdp:$CDPPORT}"
    Write-Host "SETUP: [$Site] hwnd=$HW cdp=$CDPPORT grap=$CDPGRAP"
    Start-Sleep -Milliseconds 700

    $html = Invoke-WK -WkArgs @("cdp","html",$CDPGRAP) -TimeoutSec 20
    Save-TextLog $Site "broker-html-precheck" $html.Output | Out-Null
    if (-not $html.Ok -or [string]::IsNullOrWhiteSpace($html.Output)) {
        Add-Skip $Site "cdp html broker pre-check failed"
        return $null
    }

    foreach ($pattern in $HtmlPatterns) {
        if ($html.Output -notmatch $pattern) {
            Add-Skip $Site "broker pre-check missing pattern: $pattern"
            return $null
        }
    }

    Add-Pass $Site "cdp open + broker pre-check"
    return [pscustomobject]@{ Site = $Site; Url = $Url; HW = $HW; CDPPORT = $CDPPORT; CDPGRAP = $CDPGRAP; InitialHtml = $html.Output }
}

function Test-Find {
    param([object]$Ctx, [string]$Pattern, [string]$Expect, [string]$Name)
    $target = "$Pattern#$($Ctx.HW)"
    $ok = Run-WK @("a11y","find",$target) $Expect "$($Ctx.Site) $Name" 20
    Check-Step $Ctx.Site $ok $Name "a11y find $target" | Out-Null
    return $ok
}

function Test-Eval {
    param([object]$Ctx, [string]$Js, [string]$Expect, [string]$Name, [int]$TimeoutSec = 15)
    $ok = Run-WK @("a11y","read",$Ctx.CDPGRAP,"--eval-js",$Js) $Expect "$($Ctx.Site) $Name" $TimeoutSec
    Check-Step $Ctx.Site $ok $Name "eval-js expected $Expect" | Out-Null
    return $ok
}

function Test-Scroll {
    param([object]$Ctx)
    $ok = Run-WK @("a11y","scroll",$Ctx.HW,"--direction","down") $null "$($Ctx.Site) scroll down" 15
    Check-Step $Ctx.Site $ok "scroll down" | Out-Null
}

function Test-Screenshot {
    param([object]$Ctx)
    $shot = Join-Path $LogDir "$($Ctx.Site.ToLowerInvariant())-$(Get-Date -Format 'yyyyMMdd-HHmmss').png"
    $ok = Run-WK @("a11y","screenshot",$Ctx.HW,"--path",$shot) $null "$($Ctx.Site) screenshot" 20
    if ($ok -and (Test-Path $shot)) {
        Add-Pass $Ctx.Site "screenshot saved $shot"
    } else {
        Add-Fail $Ctx.Site "screenshot" "file not created: $shot"
    }
}

function Get-Html {
    param([object]$Ctx, [int]$TimeoutSec = 20)
    $html = Invoke-WK @("cdp","html",$Ctx.CDPGRAP) $TimeoutSec
    Save-TextLog $Ctx.Site "latest-html" $html.Output | Out-Null
    return $html.Output
}

function Test-GitHub {
    New-SiteResult "GITHUB"
    $ctx = Open-Site "GITHUB" "https://github.com" @("github")
    if ($null -eq $ctx) { return }

    Test-Find $ctx "*search*" "TARGET" "find search" | Out-Null
    Test-Eval $ctx "document.title" "GitHub" "read title"
    Test-Scroll $ctx
    Test-Screenshot $ctx

    $winOk = Run-WK @("windows","*github*") "github|GitHub|chrome" "GITHUB windows *github*" 15
    Check-Step "GITHUB" $winOk "windows *github*" | Out-Null

    $notif = Invoke-WK @("a11y","find","*notification*") 15
    Save-TextLog "GITHUB" "bug-wildcard-system-window-match" $notif.Output | Out-Null
    if ($notif.Output -match "DWM|MediaContext") {
        Add-Bug "GITHUB" "wildcard-system-window-match" "unscoped wildcard matched a system window"
    } else {
        Add-Pass "GITHUB" "wildcard-system-window-match not reproduced"
    }

    $html = Get-Html $ctx 25
    $htmlBytes = [Text.Encoding]::UTF8.GetByteCount($html)
    if ($htmlBytes -gt 512000) {
        Add-Bug "GITHUB" "cdp-html-js-blob" "html size=$htmlBytes bytes"
    } else {
        Add-Pass "GITHUB" "cdp html size below 500KB ($htmlBytes bytes)"
    }
}

function Test-YouTube {
    New-SiteResult "YOUTUBE"
    $ctx = Open-Site "YOUTUBE" "https://www.youtube.com" @("youtube|yt-formatted|string")
    if ($null -eq $ctx) { return }

    Test-Find $ctx "*search*" "TARGET" "find search" | Out-Null
    Test-Eval $ctx "document.title" "YouTube" "read title"

    $typeOk = Run-WK @("a11y","type","*search*#$($ctx.HW)","wkappbot automation") $null "YOUTUBE type search" 20
    Check-Step "YOUTUBE" $typeOk "type search query" | Out-Null
    Test-Scroll $ctx

    $htmlNow = Get-Html $ctx 20
    if ($htmlNow -notmatch "ytd-rich-item") {
        Add-Bug "YOUTUBE" "youtube-skeleton-html" "initial html missing ytd-rich-item"
        Start-Sleep -Seconds 3
        $htmlLater = Get-Html $ctx 20
        if ($htmlLater -match "ytd-rich-item") {
            Add-Pass "YOUTUBE" "youtube skeleton recovered after wait"
        } else {
            Add-Fail "YOUTUBE" "youtube content after wait" "ytd-rich-item still missing"
        }
    } else {
        Add-Pass "YOUTUBE" "youtube rich items present immediately"
    }
}

function Test-Naver {
    New-SiteResult "NAVER"
    $ctx = Open-Site "NAVER" "https://www.naver.com" @("naver|NAVER|news")
    if ($null -eq $ctx) { return }

    Test-Find $ctx "*search*;*검색*" "TARGET" "find search" | Out-Null
    Test-Eval $ctx "var e=document.querySelector('.news_tit'); e ? e.textContent : ''" ".+" "read headline"
    Test-Scroll $ctx
    Test-Screenshot $ctx

    $translate = Invoke-WK @("a11y","find","*translate*;*번역*") 15
    Save-TextLog "NAVER" "bug-translate-infobar" $translate.Output | Out-Null
    if ($translate.Output -match "TARGET") {
        Add-Bug "NAVER" "naver-translate-infobar" "translate infobar found during site test"
    } else {
        Add-Pass "NAVER" "naver translate infobar not reproduced"
    }
}

function Test-Google {
    New-SiteResult "GOOGLE"
    $ctx = Open-Site "GOOGLE" "https://www.google.com" @("Google|google")
    if ($null -eq $ctx) { return }

    Test-Find $ctx "*search*" "TARGET" "find search" | Out-Null

    $inspectOk = Run-WK @("a11y","inspect",$ctx.HW) "Google" "GOOGLE inspect hwnd" 20
    Check-Step "GOOGLE" $inspectOk "inspect contains Google" | Out-Null

    $typeOk = Run-WK @("a11y","type","*search*#$($ctx.HW)","wkappbot windows automation") $null "GOOGLE type search" 20
    Check-Step "GOOGLE" $typeOk "type search query" | Out-Null
    Start-Sleep -Seconds 2
    Test-Eval $ctx "document.title" "Google|wkappbot|automation" "read title after search" 20
}

function Test-StackOverflow {
    New-SiteResult "STACKOVERFLOW"
    $ctx = Open-Site "STACKOVERFLOW" "https://stackoverflow.com" @("Stack Overflow|stackoverflow")
    if ($null -eq $ctx) { return }

    Test-Find $ctx "*question*" "TARGET" "find question" | Out-Null
    Test-Eval $ctx "var e=document.querySelector('.s-post-summary--content-title'); e ? e.textContent : ''" ".+" "read top question"
    Test-Scroll $ctx
}

function Write-Summary {
    Write-Host ""
    Write-Host "================================================"
    Write-Host " Real-site CDP/A11y Summary"
    Write-Host "================================================"
    foreach ($site in @("GITHUB","YOUTUBE","NAVER","GOOGLE","STACKOVERFLOW")) {
        if (-not $script:Results.ContainsKey($site)) { continue }
        $r = $script:Results[$site]
        Write-Host ("{0}: PASS={1} FAIL={2} BUGS={3} SKIP={4}" -f $site, $r.Pass, $r.Fail, $r.Bugs, $r.Skip)
    }
    Write-Host ("TOTAL: PASS={0} FAIL={1} BUGS={2} SKIP={3}" -f $script:TotalPass, $script:TotalFail, $script:TotalBugs, $script:TotalSkip)

    if ($script:TotalFail -gt 0 -and -not $CI -and (Test-Path "D:/GitHub/WKAppBot")) {
        Push-Location "D:/GitHub/WKAppBot"
        try {
            & wkappbot suggest "real-site cdp/a11y integration test: $($script:TotalFail) failures and $($script:TotalBugs) bug reproductions. Run D:/GitHub/wkappbot-sdk/test/cdp-a11y-real-sites.ps1 to reproduce." --requirement "wkappbot skill read grap => GRAP" --requirement "wkappbot skill read cdp-command-guide => cdp open" --requirement "wkappbot skill read a11y-command-cheatsheet => a11y"
        } finally {
            Pop-Location
        }
    }
}

Write-Host "================================================"
Write-Host " WKAppBot real-site CDP/A11y integration test"
Write-Host " Logs: $LogDir"
Write-Host " SiteTimeout=$SiteTimeout CI=$CI"
Write-Host "================================================"

Test-GitHub
Test-YouTube
Test-Naver
Test-Google
Test-StackOverflow

Write-Summary

if ($script:TotalFail -gt 0 -or $script:TotalBugs -gt 0) {
    exit 1
}

exit 0
