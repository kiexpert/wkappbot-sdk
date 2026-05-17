$ErrorActionPreference = "Continue"
$CI = $env:GITHUB_ACTIONS -eq 'true'

function Run-WK {
    param([string[]]$WkArgs, [string]$ExpectPattern, [string]$TestName, [int]$TimeoutSec = 15)
    $logBase = Join-Path $env:TEMP "wk-test-$(Get-Random)"
    $proc = Start-Process wkappbot -ArgumentList $WkArgs -RedirectStandardOutput "$logBase.out" -RedirectStandardError "$logBase.err" -PassThru -NoNewWindow
    $exited = $proc.WaitForExit($TimeoutSec * 1000)
    if (-not $exited) { try { $proc.Kill() } catch {} ; Write-Host "FAIL: $TestName [timeout ${TimeoutSec}s]"; return $false }
    $out = (Get-Content "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue) -join "`n"
    $script:LastWKOutput = $out
    Remove-Item "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue
    if ([string]::IsNullOrEmpty($ExpectPattern) -or $out -match $ExpectPattern) { Write-Host "PASS: $TestName"; return $true }
    Write-Host "FAIL: $TestName [expected: $ExpectPattern]"; return $false
}

if ($CI) {
    $PAGE = "https://kiexpert.github.io/wkappbot-sdk/test/"
} else {
    $PAGE = "file:///D:/GitHub/wkappbot-sdk/docs/test/index.html"
}
if ($args -contains "--pages") {
    $PAGE = "https://kiexpert.github.io/wkappbot-sdk/test/"
}

$PASS = 0
$FAIL = 0
$TOTAL = 22
$HW = ""
$script:LastWKOutput = ""

function Add-Result {
    param([bool]$Ok)
    if ($Ok) { $script:PASS++ } else { $script:FAIL++ }
}

function Check-Dom {
    param([string]$ExpectString, [string]$TestName)
    return Run-WK @("cdp","html",$HW) $ExpectString $TestName
}

function Auto-Suggest-And-Exit {
    Write-Host ""
    Write-Host "================================================"
    Write-Host " Results: $PASS PASS / $FAIL FAIL [of $TOTAL]"
    Write-Host "================================================"

    if ($FAIL -gt 0) {
        if (-not $CI -and (Test-Path D:/GitHub/WKAppBot)) {
            Push-Location D:/GitHub/WKAppBot
            & wkappbot-core.exe suggest "a11y/cdp integration test: $FAIL of $TOTAL tests failed. Comprehensive action coverage test. Run test/cdp-a11y-test.ps1 to reproduce." --requirement "wkappbot eye tick => ctx=" --requirement "wkappbot windows *chrome* => Match" --requirement "wkappbot a11y inspect {cdp:9741} => btn-primary"
            Pop-Location
        }
        exit 1
    }

    exit 0
}

Write-Host "================================================"
Write-Host " WKAppBot CDP/A11y Comprehensive Integration Test"
Write-Host " Covering all standard a11y actions"
Write-Host "================================================"

Write-Host ""
Write-Host "[SETUP] cdp open..."
$cdpOpenTimeout = if ($CI) { 90 } else { 30 }
$setupOk = Run-WK @("cdp","open",$PAGE) "hwnd:0x[0-9A-Fa-f]+" "cdp open" $cdpOpenTimeout
if (-not $setupOk) {
    $FAIL = $TOTAL
    Auto-Suggest-And-Exit
}

$okLine = ($script:LastWKOutput -split "`r?`n" | Where-Object { $_ -match "OK\s+\{.*hwnd:0x[0-9A-Fa-f]+" } | Select-Object -First 1)
if ($okLine -match "hwnd:(0x[0-9A-Fa-f]+)") {
    $HW = $Matches[1]
}

if ([string]::IsNullOrEmpty($HW)) {
    Write-Host "FAIL: hwnd not extracted"
    $FAIL = $TOTAL
    Auto-Suggest-And-Exit
}

Write-Host "SETUP OK hwnd=$HW"
Start-Sleep -Seconds 2

Write-Host ""
Write-Host "--- Discovery ---"

Write-Host "[T01] a11y find..."
Add-Result (Run-WK @("a11y","find","Click Me#$HW") $null "find")

Write-Host "[T02] a11y inspect..."
Add-Result (Run-WK @("a11y","inspect",$HW) "btn-primary" "inspect")

Write-Host "[T03] windows..."
Add-Result (Run-WK @("windows","*WKAppBot CDP*") "(kiexpert|chrome)" "windows")

Write-Host "[T04] a11y screenshot..."
$shot = "$env:TEMP\wk-test-shot.png"
$shotOk = Run-WK @("a11y","screenshot",$HW,"--path",$shot) $null "screenshot command"
if ($shotOk -and (Test-Path $shot)) {
    Write-Host "PASS: screenshot"
    Remove-Item $shot -ErrorAction SilentlyContinue
    Add-Result $true
} else {
    Write-Host "FAIL: screenshot [file not created]"
    Add-Result $false
}

Write-Host ""
Write-Host "--- Window Control ---"

Write-Host "[T05] a11y minimize..."
Add-Result (Run-WK @("a11y","minimize",$HW) $null "minimize")
Start-Sleep -Seconds 1

Write-Host "[T06] a11y restore..."
Add-Result (Run-WK @("a11y","restore",$HW) $null "restore")
Start-Sleep -Seconds 1

Write-Host "[T07] a11y focus..."
Add-Result (Run-WK @("a11y","focus",$HW) $null "focus")

Write-Host ""
Write-Host "--- Web Interaction ---"

Write-Host "[T08] a11y invoke btn-primary..."
[void](Run-WK @("a11y","invoke","btn-primary#$HW") $null "invoke command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "clicked:" "invoke -> DOM updated")

Write-Host "[T09] a11y type..."
[void](Run-WK @("a11y","type","input-text#$HW","hello wkappbot") $null "type command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "hello wkappbot" "type -> input value")

Write-Host "[T10] a11y toggle checkbox-a..."
[void](Run-WK @("a11y","toggle","chk-a#$HW") $null "toggle command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "A=on" "toggle -> checkbox checked")

Write-Host "[T11] a11y select Beta..."
[void](Run-WK @("a11y","select","dropdown#$HW","Beta") $null "select command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "selected: beta" "select -> dropdown value")

Write-Host "[T12] a11y scroll..."
[void](Run-WK @("a11y","scroll","scroll-box#$HW","--direction","down") $null "scroll command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "scrollTop=" "scroll -> indicator")

Write-Host "[T13] a11y read read-target..."
Add-Result (Run-WK @("a11y","read","read-target#$HW") "fox" "read")

Write-Host "[T14] a11y set-range slider..."
[void](Run-WK @("a11y","set-range","range-input#$HW","75") $null "set-range command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "value=75" "set-range -> slider value")

Write-Host "[T15] a11y expand details..."
[void](Run-WK @("a11y","expand","details-main#$HW") $null "expand command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "expanded" "expand -> details open")

Write-Host "[T16] a11y collapse details..."
[void](Run-WK @("a11y","collapse","details-main#$HW") $null "collapse command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "collapsed" "collapse -> details closed")

Write-Host "[T17] a11y wait --condition..."
[void](Run-WK @("a11y","invoke","btn-spawn#$HW") $null "spawn command")
[void](Run-WK @("a11y","wait","btn-delayed#$HW","--condition","visible","--timeout","5000") $null "wait command" 8)
Start-Sleep -Seconds 1
Add-Result (Check-Dom "appeared" "wait --condition")

Write-Host "[T18] clipboard-write/read..."
[void](Run-WK @("a11y","clipboard-write","wkappbot-test-clip") $null "clipboard-write command")
Start-Sleep -Seconds 1
Add-Result (Run-WK @("a11y","clipboard-read") "wkappbot-test-clip" "clipboard-write/read")

Write-Host "[T19] a11y type --hotkey Tab..."
Add-Result (Run-WK @("a11y","type","input-text#$HW","{TAB}","--hotkey") $null "type --hotkey")

Write-Host "[T20] a11y set-value contenteditable..."
[void](Run-WK @("a11y","set-value","setval-target#$HW","new value via set-value") $null "set-value command")
Start-Sleep -Seconds 1
Add-Result (Check-Dom "new value via set-value" "set-value -> content updated")

Write-Host "[T21] a11y move + resize..."
$moveOk = Run-WK @("a11y","move",$HW,"100","100") $null "move command"
$resizeOk = Run-WK @("a11y","resize",$HW,"900","700") $null "resize command"
Add-Result ($moveOk -and $resizeOk)

Write-Host "[T22] a11y maximize..."
Add-Result (Run-WK @("a11y","maximize",$HW) $null "maximize")

Auto-Suggest-And-Exit
