# test-encoding.ps1 — Encoding test for wkappbot Launcher pipe relay
# Captures raw bytes from wkappbot output, decodes as UTF-8 and CP949, verifies Korean text.
# Usage: powershell -File scripts/test-encoding.ps1 [wkappbot-path]

param(
    [string]$WkPath = "W:\SDK\bin\wkappbot.exe"
)

$ErrorActionPreference = "Continue"
$script:pass = 0
$script:fail = 0

function Check-Korean {
    param([byte[]]$bytes, [string]$encName)
    try {
        # Prefer integer codepage lookup (GetEncoding(string) can throw for numeric names)
        $cpInt = 0
        if ([int]::TryParse($encName, [ref]$cpInt)) {
            $enc = [System.Text.Encoding]::GetEncoding($cpInt)
        } else {
            $enc = [System.Text.Encoding]::GetEncoding($encName)
        }
        $text = $enc.GetString($bytes)
        # Use char code range instead of literal Korean (avoid PS5.1 ANSI encoding issue)
        $hasKorean = $false
        foreach ($c in $text.ToCharArray()) {
            if ([int]$c -ge 0xAC00 -and [int]$c -le 0xD7A3) { $hasKorean = $true; break }
        }
        $ok = $hasKorean -and (-not ($text.Contains([char]0xFFFD)))
        return @{ ok=$ok; text=($text.Trim() -replace '\r?\n.*','') }
    } catch {
        return @{ ok=$false; text="[EX:$($_.Exception.GetType().Name) $($_.Exception.Message.Substring(0,[Math]::Min(60,$_.Exception.Message.Length)))]" }
    }
}

function Run-EncTest {
    param([string]$Label, [string]$Cmd, [string]$CmdArgs)

    $psi = New-Object System.Diagnostics.ProcessStartInfo
    $psi.FileName = $Cmd
    $psi.Arguments = $CmdArgs
    $psi.UseShellExecute = $false
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $false
    # Latin1 = 1 byte per char = raw bytes, no re-encoding
    $psi.StandardOutputEncoding = [System.Text.Encoding]::GetEncoding(28591)

    $proc = [System.Diagnostics.Process]::Start($psi)
    $out = $proc.StandardOutput.ReadToEnd()
    $proc.WaitForExit()

    $bytes = [System.Text.Encoding]::GetEncoding(28591).GetBytes($out)
    $hexSnip = ($bytes | Select-Object -First 24 | ForEach-Object { $_.ToString("X2") }) -join " "

    $utf8  = Check-Korean $bytes "utf-8"
    $cp949 = Check-Korean $bytes "949"

    $ok = $utf8.ok -or $cp949.ok
    $status = if ($ok) { "PASS" } else { "FAIL" }

    Write-Host ""
    Write-Host "[$status] $Label"
    Write-Host "  bytes=$($bytes.Length) hex=[$hexSnip ...]"
    if ($utf8.ok)  { Write-Host "  UTF-8 OK : $($utf8.text)" }
    if ($cp949.ok) { Write-Host "  CP949 OK : $($cp949.text)" }
    if (-not $ok) {
        Write-Host "  FAIL: no valid Korean in output"
        Write-Host "  UTF-8 try: $($utf8.text.Substring(0, [Math]::Min(80,$utf8.text.Length)))"
        Write-Host "  CP949 try: $($cp949.text.Substring(0, [Math]::Min(80,$cp949.text.Length)))"
    }

    if ($ok) { $script:pass++ } else { $script:fail++ }
}

Write-Host "=== wkappbot Encoding Test ==="
Write-Host "Target: $WkPath"
Write-Host ""

# Normalize path to backslashes for Windows APIs
$WkWin = $WkPath.Replace("/", "\")

# Test 1: CMD with CP949 (Korean Windows default)
Write-Host "--- Test 1: CMD with chcp 949 ---"
Run-EncTest -Label "CMD-CP949" -Cmd "cmd.exe" -CmdArgs "/c `"chcp 949 > nul 2>&1 & $WkWin enc-test`""

# Test 2: CMD with CP65001 (UTF-8)
Write-Host ""
Write-Host "--- Test 2: CMD with chcp 65001 ---"
Run-EncTest -Label "CMD-CP65001" -Cmd "cmd.exe" -CmdArgs "/c `"chcp 65001 > nul 2>&1 & $WkWin enc-test`""

# Test 3: Direct PS launch (no CMD wrapper, no TERM/MSYSTEM env var in PS)
Write-Host ""
Write-Host "--- Test 3: Direct PS launch (TERM/MSYSTEM unset) ---"
# Temporarily clear env vars that would trigger UTF-8 passthrough
$savedTerm = $env:TERM; $savedMsystem = $env:MSYSTEM; $savedTermProg = $env:TERM_PROGRAM
$env:TERM = $null; $env:MSYSTEM = $null; $env:TERM_PROGRAM = $null
Run-EncTest -Label "PS-no-utf8env" -Cmd $WkWin -CmdArgs "enc-test"
$env:TERM = $savedTerm; $env:MSYSTEM = $savedMsystem; $env:TERM_PROGRAM = $savedTermProg

# Test 4: CMD CP949 Core path (--only-core bypasses Eye, tests IOCP transcode)
Write-Host ""
Write-Host "--- Test 4: CMD CP949 Core path (--only-core) ---"
Run-EncTest -Label "CMD-CP949-core" -Cmd "cmd.exe" -CmdArgs "/c `"chcp 949 > nul 2>&1 & $WkWin enc-test --only-core`""

Write-Host ""
Write-Host "=== Results: $($script:pass) passed, $($script:fail) failed ==="
if ($script:fail -gt 0) { exit 1 } else { exit 0 }
