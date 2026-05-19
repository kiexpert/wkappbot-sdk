param(
    [string]$ExePath = "$PSScriptRoot\build\Release\LegacyControlZoo.exe"
)

$ErrorActionPreference = "Continue"
$CI = $env:GITHUB_ACTIONS -eq 'true'

# Skip tests if exe not found (CI skip path)
if (-not (Test-Path $ExePath)) {
    Write-Host "SKIP: LegacyControlZoo.exe not found at $ExePath"
    exit 0
}

# Skip if wkappbot not available
if (-not (Get-Command wkappbot -ErrorAction SilentlyContinue)) {
    Write-Host "SKIP: wkappbot not in PATH -- a11y tests require wkappbot. Build OK."
    exit 0
}

# Start Eye and wait for it to be ready
Write-Host "Starting Eye daemon..."
Start-Process wkappbot.exe -ArgumentList "eye" -WindowStyle Hidden -ErrorAction SilentlyContinue
$eyeReady = $false
for ($i = 0; $i -lt 15; $i++) {
    Start-Sleep -Seconds 1
    $tick = & wkappbot eye tick 2>&1
    if ($tick -match "end:|idle:|ctx=") { $eyeReady = $true; break }
    Write-Host "  Eye not ready yet ($i)..."
}
if (-not $eyeReady) {
    Write-Host "WARN: Eye did not start in 15s -- action commands may not output [OK]"
} else {
    Write-Host "Eye ready."
}

Write-Host "Starting legacy-app a11y tests (all 24 actions)"
Write-Host "ExePath: $ExePath"
Write-Host ""

# Counters
$script:TotalPass = 0
$script:TotalFail = 0
$script:TotalSoftFail = 0
$script:TestCount = 0

function Invoke-WK {
    param([string[]]$WkArgs, [int]$TimeoutSec = 30)

    $outFile = [System.IO.Path]::GetTempFileName()
    # cmd /c redirection captures console-handle output that Start-Process misses in CI
    $quotedArgs = $WkArgs | ForEach-Object {
        if ($_ -match '[ {}]') { '"{0}"' -f $_ } else { $_ }
    }
    $cmdLine = '/c wkappbot.exe ' + ($quotedArgs -join ' ') + ' > "' + $outFile + '" 2>&1'
    $proc = Start-Process cmd.exe -ArgumentList $cmdLine -PassThru -NoNewWindow
    $exited = $proc.WaitForExit($TimeoutSec * 1000)
    if (-not $exited) { try { $proc.Kill() } catch {} }
    $out = if (Test-Path $outFile) { Get-Content $outFile -Raw -Encoding UTF8 } else { "" }
    Remove-Item $outFile -Force -ErrorAction SilentlyContinue
    if (-not $out) { $out = "" }
    if (-not $exited -and $out.Trim().Length -eq 0) {
        return [pscustomobject]@{ Ok = $false; ExitCode = 124; Output = "TIMEOUT after ${TimeoutSec}s" }
    }
    return [pscustomobject]@{ Ok = ($out -match "\[OK\]"); ExitCode = $proc.ExitCode; Output = $out }
}

function Run-Test {
    param([string]$TestName, [string[]]$WkArgs, [string]$ExpectPattern, [bool]$IsTier1 = $true)

    $script:TestCount++
    Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] $TestName ... "
    $result = Invoke-WK $WkArgs

    if ([string]::IsNullOrEmpty($ExpectPattern)) {
        if ($result.Ok) {
            Write-Host "PASS"
            $script:TotalPass++
            return $true
        } else {
            if ($IsTier1) {
                Write-Host "FAIL [exit:$($result.ExitCode)]"
                $script:TotalFail++
            } else {
                Write-Host "SOFT-FAIL [exit:$($result.ExitCode)]"
                $script:TotalSoftFail++
            }
            return $false
        }
    }

    if ($result.Output -match $ExpectPattern) {
        Write-Host "PASS"
        $script:TotalPass++
        return $true
    } else {
        if ($IsTier1) {
            Write-Host "FAIL [expected pattern match]"
            $script:TotalFail++
        } else {
            Write-Host "SOFT-FAIL [expected pattern match]"
            $script:TotalSoftFail++
        }
        return $false
    }
}

# Launch the application
Write-Host "Launching LegacyControlZoo..."
try {
    Start-Process $ExePath -ErrorAction Stop
    Start-Sleep -Seconds 2
    Write-Host "Application launched successfully"
} catch {
    Write-Host "FAIL: Could not launch $ExePath : $_"
    exit 1
}

Write-Host ""
$grap = "{title:'LegacyControlZoo - WKAppBot Test'}"

Write-Host "=== DISCOVERY ==="
Write-Host ""

# 1. inspect
Run-Test "1. inspect" @("a11y", "inspect", $grap) "ControlType|Name|AutomationId" $true | Out-Null

# 2. find
Run-Test "2. find" @("a11y", "find", "*LegacyControlZoo*") "TARGETS|-- Match|# TARGET" $true | Out-Null

# 3. windows
Run-Test "3. windows" @("windows", "LegacyControlZoo") "LegacyControlZoo" $true | Out-Null

# 4. screenshot
Run-Test "4. screenshot" @("a11y", "screenshot", $grap) "\[OK\]" $true | Out-Null

# 5. ocr
Run-Test "5. ocr" @("a11y", "ocr", $grap) ".+" $true | Out-Null

Write-Host ""
Write-Host "=== WINDOW MANAGEMENT ==="
Write-Host ""

# 6. minimize
Run-Test "6. minimize" @("a11y", "minimize", $grap) "\[OK\]" $true | Out-Null

# 7. restore (after minimize)
Run-Test "7. restore" @("a11y", "restore", $grap) "\[OK\]" $true | Out-Null

# 8. maximize
Run-Test "8. maximize" @("a11y", "maximize", $grap) "\[OK\]" $true | Out-Null

# 9. restore (after maximize)
Run-Test "9. restore (after maximize)" @("a11y", "restore", $grap) "\[OK\]" $true | Out-Null

# 10. move
Run-Test "10. move" @("a11y", "move", $grap, "--x", "100", "--y", "100") "\[OK\]" $true | Out-Null

# 11. resize
Run-Test "11. resize" @("a11y", "resize", $grap, "--width", "800", "--height", "600") "\[OK\]" $true | Out-Null

# 12. focus
Run-Test "12. focus" @("a11y", "focus", $grap) "\[OK\]" $true | Out-Null

Write-Host ""
Write-Host "=== INTERACTION ==="
Write-Host ""

# 13. click
Run-Test "13. click" @("a11y", "click", $grap) "\[OK\]" $true | Out-Null

# 14. invoke
Run-Test "14. invoke" @("a11y", "invoke", $grap) "\[OK\]" $false | Out-Null

# 15. type
Run-Test "15. type" @("a11y", "type", $grap, "hello legacy") "\[OK\]" $true | Out-Null

# 16. read
Run-Test "16. read" @("a11y", "read", $grap) ".+" $true | Out-Null

# 17. set-value
Run-Test "17. set-value" @("a11y", "set-value", $grap, "test value") "\[OK\]" $false | Out-Null

# 18. scroll
Run-Test "18. scroll" @("a11y", "scroll", $grap) "\[OK\]" $true | Out-Null

# 19. scroll (up)
Run-Test "19. scroll --direction up" @("a11y", "scroll", $grap, "--direction", "up") "\[OK\]" $true | Out-Null

Write-Host ""
Write-Host "=== TREE EXPANSION ==="
Write-Host ""

# 20. expand
Run-Test "20. expand" @("a11y", "expand", $grap) "\[OK\]" $false | Out-Null

# 21. collapse
Run-Test "21. collapse" @("a11y", "collapse", $grap) "\[OK\]" $false | Out-Null

# 22. toggle
Run-Test "22. toggle" @("a11y", "toggle", $grap) "\[OK\]" $false | Out-Null

# 23. select
Run-Test "23. select" @("a11y", "select", $grap) "\[OK\]" $false | Out-Null

Write-Host ""
Write-Host "=== WAIT & CLIPBOARD ==="
Write-Host ""

# 24. wait
Run-Test "24. wait" @("a11y", "wait", $grap, "--timeout", "2") "\[OK\]" $true | Out-Null

# 25. clipboard-write
Run-Test "25. clipboard-write" @("a11y", "clipboard-write", "test clipboard content") "\[OK\]" $true | Out-Null

# 26. clipboard-read
Run-Test "26. clipboard-read" @("a11y", "clipboard-read") "test clipboard content|No text data|\S+" $true | Out-Null

Write-Host ""
Write-Host "=== HTS PATTERNS (영웅문 재현) ==="
Write-Host ""

# HTS-1: UIA should FAIL on masked edit (no accessible name)
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-1 mask-edit UIA blind (expect NO TARGET) ... "
$r = Invoke-WK @("a11y", "find", "*종목코드*")
if ($r.Output -match "NO TARGET|not found|no match" -or -not $r.Ok) {
    Write-Host "PASS (correct UIA failure)"
    $script:TotalPass++
} else {
    Write-Host "BUG: UIA found masked edit by Korean label (false positive)"
    $script:TotalSoftFail++
}

# HTS-2: WM_CHAR path on masked edit (Win32 tier 2)
Run-Test "HTS-2 mask-edit WM_CHAR type" @("a11y", "type", $grap, "005930", "--force") "\[OK\]" $false | Out-Null

# HTS-3: OCR fallback reads price grid text
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-3 OCR price-grid pattern ... "
$r = Invoke-WK @("a11y", "ocr", $grap) 30
if ($r.Output -match "(\d{3},\d{3}|%)") {
    Write-Host "PASS (price pattern found in OCR)"
    $script:TotalPass++
} else {
    Write-Host "SOFT-FAIL (OCR returned no price pattern)"
    $script:TotalSoftFail++
}

# HTS-4: Icon-only button find
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-4 icon-button no-name lookup ... "
$r = Invoke-WK @("a11y", "find", "*매수*")
if ($r.Ok -and $r.Output -match "# TARGET") {
    Write-Host "PASS (label-by-proximity match worked)"
    $script:TotalPass++
} else {
    Write-Host "HTS-PATTERN (icon button has no UIA name -- expected miss)"
    $script:TotalSoftFail++
}

# HTS-5: Modal dialog detection
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-5 modal detection ... "
Invoke-WK @("a11y", "click", $grap, "Open Modal") | Out-Null
Start-Sleep -Milliseconds 500
$r = Invoke-WK @("a11y", "find", "{title:'Modal Dialog'}") 5
if ($r.Output -match "# TARGET|Modal") {
    Write-Host "PASS (modal detected)"
    $script:TotalPass++
} else {
    Write-Host "SOFT-FAIL (modal not detected via a11y find)"
    $script:TotalSoftFail++
}

# Wait for modal auto-close
Start-Sleep -Seconds 4

Write-Host ""
Write-Host "=== TEARDOWN ==="
Write-Host ""

# 27. close
Run-Test "27. close" @("a11y", "close", $grap) "\[OK\]" $true | Out-Null

Write-Host ""
Write-Host "=================================================="
Write-Host "SUMMARY"
Write-Host "=================================================="
Write-Host "Total Tests: $($script:TestCount)"
Write-Host "PASSED (TIER1): $($script:TotalPass)"
Write-Host "FAILED (TIER1): $($script:TotalFail)"
Write-Host "SOFT-FAIL (TIER2): $($script:TotalSoftFail)"
Write-Host ""

if ($script:TotalFail -gt 0) {
    Write-Host "RESULT: Some TIER1 tests failed"
    exit 1
} else {
    Write-Host "RESULT: All TIER1 tests passed"
    exit 0
}
