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
    return Run-WK @("cdp","html",$CDPGRAP) $ExpectString $TestName
}

function Test-UiaDomPair {
    param(
        [string]$Label,
        [string[]]$FindArgs,
        [string]$UiaPattern,
        [string[]]$ActionArgs,
        [string]$DomPattern,
        [string]$ActionLabel = ''
    )
    if (-not $ActionLabel) { $ActionLabel = $Label }

    # Step 1: verify UIA node (mismatch = wrong node targeted)
    $uiaOk = Run-WK ($FindArgs) $UiaPattern "$Label [UIA-find]" 15
    if (-not $uiaOk) {
        Write-Host "  >> MISMATCH: UIA node not found or wrong node (expected: $UiaPattern)"
        Write-Host "  >> UIA output: $($script:LastWKOutput | Select-Object -First 3 | Out-String)"
    }

    # Step 2: capture DOM before
    $beforeHtml = (& wkappbot cdp html $CDPGRAP 2>$null) -join ' '

    # Step 3: execute action
    [void](Run-WK $ActionArgs $null "$ActionLabel [exec]" 15)
    if ($CI) { Start-Sleep -Milliseconds 500 } else { Start-Sleep -Milliseconds 300 }

    # Step 4: capture DOM after + check
    $afterHtml = (& wkappbot cdp html $CDPGRAP 2>$null) -join ' '
    $domChanged = $afterHtml -match $DomPattern

    if ($uiaOk -and $domChanged) {
        Write-Host "PASS: $Label [UIA->DOM path OK]"
        return $true
    } elseif ($uiaOk -and -not $domChanged) {
        Write-Host "MISMATCH: $Label -- UIA action fired ($UiaPattern found) but DOM didn't update (expected: $DomPattern)"
        $excerptStart = [Math]::Max(0, $beforeHtml.IndexOf('click-result') - 20)
        $excerptLength = [Math]::Min(200, $beforeHtml.Length - $excerptStart)
        Write-Host "  >> DOM before excerpt: $($beforeHtml.Substring($excerptStart, $excerptLength))"
        return $false
    } elseif (-not $uiaOk -and $domChanged) {
        Write-Host "NOTE: $Label -- UIA node mismatch but DOM updated anyway (wrong-node hit?)"
        return $false
    } else {
        Write-Host "FAIL: $Label -- both UIA node missing AND DOM unchanged"
        return $false
    }
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
if ($okLine -match 'cdp:([0-9]+)') { $CDPPORT = $Matches[1] }
if ($okLine -match "hwnd:(0x[0-9A-Fa-f]+)") {
    $HW = $Matches[1]
}

$CDPGRAP = "{proc:'chrome',cdp:$CDPPORT}"
if ([string]::IsNullOrEmpty($HW) -or [string]::IsNullOrEmpty($CDPPORT)) {
    Write-Host "FAIL: hwnd or cdp port not extracted (HW=$HW CDPPORT=$CDPPORT)"
    $FAIL = $TOTAL
    Auto-Suggest-And-Exit
}

Write-Host "SETUP OK hwnd=$HW cdp=$CDPPORT"
Start-Sleep -Milliseconds 500

Write-Host ""
Write-Host "--- CDP Baseline (no UIA) ---"

# eval-js direct click -> DOM update
Write-Host "[CDP-B1] eval-js click btn-primary..."
$evalClick = Run-WK @("a11y","read",$CDPGRAP,"--eval-js","document.getElementById('btn-primary').click(); document.getElementById('click-result').textContent") "clicked:" "cdp-b1 eval-js click" 15
Add-Result $evalClick
if (-not $evalClick) {
    Write-Host "  >> CDP BASELINE FAIL: Chrome JS execution or DOM not interactive -- all UIA->DOM tests will be invalid"
    Write-Host "  >> This indicates headless/GPU-disabled Chrome or renderer bridge issue"
}

# eval-js direct value set -> DOM read back
Write-Host "[CDP-B2] eval-js set input value..."
$evalSet = Run-WK @("a11y","read",$CDPGRAP,"--eval-js","document.getElementById('input-text').value='cdp-direct'; document.getElementById('input-text').value") "cdp-direct" "cdp-b2 eval-js set" 15
Add-Result $evalSet

# eval-js toggle checkbox -> state read back
Write-Host "[CDP-B3] eval-js toggle checkbox..."
$evalToggle = Run-WK @("a11y","read",$CDPGRAP,"--eval-js","document.getElementById('chk-a').checked=true; document.getElementById('chk-a').checked.toString()") "true" "cdp-b3 eval-js checkbox" 15
Add-Result $evalToggle

$cdpBaselineOk = $evalClick -and $evalSet -and $evalToggle
if (-not $cdpBaselineOk) {
    Write-Host "  >> [DIAGNOSIS] CDP direct JS fails -> root cause is Chrome renderer, NOT UIA bridge"
} else {
    Write-Host "  >> [DIAGNOSIS] CDP direct JS works -> if UIA actions fail later, root cause is UIA->JS event bridge"
}
$TOTAL += 3

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
Start-Sleep -Milliseconds 300

Write-Host "[T06] a11y restore..."
Add-Result (Run-WK @("a11y","restore",$HW) $null "restore")
Start-Sleep -Milliseconds 300

Write-Host "[T07] a11y focus..."
Add-Result (Run-WK @("a11y","focus",$HW) $null "focus")

Write-Host ""
Write-Host "--- Web Interaction ---"

Write-Host "[T08] a11y invoke btn-primary..."
Add-Result (Test-UiaDomPair "invoke" @("a11y","find","btn-primary#$HW") "btn-primary" @("a11y","invoke","btn-primary#$HW") "clicked:")

Write-Host "[T09] a11y type..."
Add-Result (Test-UiaDomPair "type" @("a11y","find","input-text#$HW") "input" @("a11y","type","input-text#$HW","hello wkappbot") "hello wkappbot")

Write-Host "[T10] a11y toggle checkbox-a..."
Add-Result (Test-UiaDomPair "toggle" @("a11y","find","chk-a#$HW") "chk-a|CheckBox" @("a11y","toggle","chk-a#$HW") "A=on")

Write-Host "[T11] a11y select Beta..."
Add-Result (Test-UiaDomPair "select" @("a11y","find","dropdown#$HW") "dropdown|ComboBox|ListBox" @("a11y","select","dropdown#$HW","Beta") "selected: beta")

Write-Host "[T12] a11y scroll..."
Add-Result (Test-UiaDomPair "scroll" @("a11y","find","scroll-box#$HW") "scroll" @("a11y","scroll","scroll-box#$HW","--direction","down") "scrollTop=[^0]")

Write-Host "[T13] a11y read read-target..."
Add-Result (Run-WK @("a11y","read","read-target#$HW") "fox" "read")

Write-Host "[T14] a11y set-range slider..."
Add-Result (Test-UiaDomPair "set-range" @("a11y","find","range-input#$HW") "range|slider" @("a11y","set-range","range-input#$HW","75") "value=75")

Write-Host "[T15] a11y expand details..."
Add-Result (Test-UiaDomPair "expand" @("a11y","find","details-main#$HW") "details|group" @("a11y","expand","details-main#$HW") "expanded")

Write-Host "[T16] a11y collapse details..."
Add-Result (Test-UiaDomPair "collapse" @("a11y","find","details-main#$HW") "details|group" @("a11y","collapse","details-main#$HW") "collapsed")

Write-Host "[T17] a11y wait --condition..."
[void](Run-WK @("a11y","invoke","btn-spawn#$HW") $null "spawn command")
[void](Run-WK @("a11y","wait","btn-delayed#$HW","--condition","visible","--timeout","5000") $null "wait command" 8)
Start-Sleep -Milliseconds 300
Add-Result (Check-Dom "appeared" "wait --condition")

Write-Host "[T18] clipboard-write/read..."
[void](Run-WK @("a11y","clipboard-write","wkappbot-test-clip") $null "clipboard-write command")
Start-Sleep -Milliseconds 300
Add-Result (Run-WK @("a11y","clipboard-read") "wkappbot-test-clip" "clipboard-write/read")

Write-Host "[T19] a11y type --hotkey Tab..."
Add-Result (Run-WK @("a11y","type","input-text#$HW","{TAB}","--hotkey") $null "type --hotkey")

Write-Host "[T20] a11y set-value contenteditable..."
Add-Result (Test-UiaDomPair "set-value" @("a11y","find","setval-target#$HW") "setval|edit|content" @("a11y","set-value","setval-target#$HW","new value via set-value") "new value via set-value")

Write-Host "[T21] a11y move + resize..."
$moveOk = Run-WK @("a11y","move",$HW,"100","100") $null "move command"
$resizeOk = Run-WK @("a11y","resize",$HW,"900","700") $null "resize command"
Add-Result ($moveOk -and $resizeOk)

Write-Host "[T22] a11y maximize..."
Add-Result (Run-WK @("a11y","maximize",$HW) $null "maximize")

Auto-Suggest-And-Exit
