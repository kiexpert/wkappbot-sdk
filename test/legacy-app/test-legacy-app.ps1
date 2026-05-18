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

Write-Host "Starting legacy-app a11y tests"
Write-Host "ExePath: $ExePath"
Write-Host ""

# Counters
$script:TotalPass = 0
$script:TotalFail = 0

function Invoke-WK {
    param([string[]]$WkArgs, [int]$TimeoutSec = 10)

    $outFile = [System.IO.Path]::GetTempFileName()
    $errFile = [System.IO.Path]::GetTempFileName()
    $proc = Start-Process wkappbot -ArgumentList $WkArgs -RedirectStandardOutput $outFile -RedirectStandardError $errFile -PassThru -NoNewWindow

    $exited = $proc.WaitForExit($TimeoutSec * 1000)
    if (-not $exited) {
        try { $proc.Kill() } catch {}
        $output = "TIMEOUT after ${TimeoutSec}s: wkappbot $($WkArgs -join ' ')"
        Remove-Item $outFile, $errFile -ErrorAction SilentlyContinue
        return [pscustomobject]@{ Ok = $false; ExitCode = 124; Output = $output }
    }

    $out = ""
    if (Test-Path $outFile) { $out += Get-Content $outFile -Raw }
    if (Test-Path $errFile) { $out += Get-Content $errFile -Raw }

    Remove-Item $outFile, $errFile -ErrorAction SilentlyContinue

    $exitCode = $proc.ExitCode
    return [pscustomobject]@{ Ok = ($exitCode -eq 0); ExitCode = $exitCode; Output = $out }
}

function Run-Test {
    param([string]$TestName, [string[]]$WkArgs, [string]$ExpectPattern, [bool]$IsTier1 = $true)

    Write-Host -NoNewline "Testing: $TestName ... "
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
                Write-Host "BUG (logged)"
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
            Write-Host "FAIL [expected: $ExpectPattern]"
            $script:TotalFail++
        } else {
            Write-Host "BUG (logged)"
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
Write-Host "================================================"
Write-Host "TIER 1 - UIA Standard Controls (must pass)"
Write-Host "================================================"
Write-Host ""

# T1-1: Find the window
Run-Test "T1-1: windows discovery" @("windows", "LegacyControlZoo") "LegacyControlZoo" $true | Out-Null

# T1-2: Find via a11y
Run-Test "T1-2: a11y find" @("a11y", "find", "*LegacyControlZoo*") "# TARGET" $true | Out-Null

# T1-3: Screenshot
Run-Test "T1-3: screenshot" @("a11y", "screenshot", "{title:'LegacyControlZoo - WKAppBot Test'}") "\[OK\]" $true | Out-Null

# T1-4: Read
Run-Test "T1-4: read" @("a11y", "read", "{title:'LegacyControlZoo - WKAppBot Test'}") ".+" $true | Out-Null

# T1-5: Type (send text to control)
Run-Test "T1-5: type" @("a11y", "type", "{title:'LegacyControlZoo - WKAppBot Test'}", "hello") "\[OK\]" $true | Out-Null

# T1-6: Scroll
Run-Test "T1-6: scroll" @("a11y", "scroll", "{title:'LegacyControlZoo - WKAppBot Test'}") "\[OK\]" $true | Out-Null

Write-Host ""
Write-Host "================================================"
Write-Host "TIER 2/3 - Owner-Drawn Controls (logged, not fatal)"
Write-Host "================================================"
Write-Host ""

# T2-1: Toolbar (owner-drawn, likely to fail)
Run-Test "T2-1: toolbar find" @("a11y", "find", "*toolbar*#{title:'LegacyControlZoo - WKAppBot Test'}") "# TARGET" $false | Out-Null

# T2-2: Status bar (owner-drawn, likely to fail)
Run-Test "T2-2: statusbar find" @("a11y", "find", "*status*#{title:'LegacyControlZoo - WKAppBot Test'}") "# TARGET" $false | Out-Null

Write-Host ""
Write-Host "================================================"
Write-Host "TEARDOWN"
Write-Host "================================================"
Write-Host ""

# Close the application
Run-Test "Teardown: close" @("a11y", "close", "{title:'LegacyControlZoo - WKAppBot Test'}") "" $false | Out-Null

Write-Host ""
Write-Host "================================================"
Write-Host "SUMMARY"
Write-Host "================================================"
Write-Host "PASSED: $($script:TotalPass)"
Write-Host "FAILED: $($script:TotalFail)"
Write-Host ""

if ($script:TotalFail -gt 0) {
    Write-Host "RESULT: Some TIER1 tests failed"
    exit 1
} else {
    Write-Host "RESULT: All TIER1 tests passed"
    exit 0
}
