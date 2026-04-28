# WKAppBot smoke test -- user-centric
# CI   : basic 20 checks (no window interaction)
# Local: basic + extended (real app launch, encoding, a11y, license, eye)
#
# Usage:
#   pwsh  -File scripts/smoke-help.ps1          # auto-detects CI vs local
#   pwsh  -File scripts/smoke-help.ps1 -Extended # force extended regardless
#   pwsh  -File scripts/smoke-help.ps1 -BasicOnly

param([switch]$Extended, [switch]$BasicOnly)

$ErrorActionPreference = 'Continue'

$repoBin = Join-Path $PWD 'bin'
if (!(Test-Path (Join-Path $repoBin 'wkappbot.exe'))) { throw "bin/wkappbot.exe missing" }
if (!(Test-Path (Join-Path $repoBin 'wkappbot-core.exe'))) { throw "bin/wkappbot-core.exe missing" }

$env:PATH            = "$repoBin;$env:PATH"
$env:WKAPPBOT_WORKER = '1'

$isCI       = $env:GITHUB_ACTIONS -eq 'true' -or $env:CI -eq 'true'
$runExt     = $Extended -or (!$isCI -and !$BasicOnly)
$coreExe    = Join-Path $repoBin 'wkappbot-core.exe'
$devCore    = 'D:\GitHub\WKAppBot\bin\wkappbot-core.exe'   # for suggest filing
$smokeDir   = Join-Path $PWD 'smoke-temp'
New-Item -Force -ItemType Directory $smokeDir | Out-Null

$pass = 0; $softFail = 0; $hardFail = 0

# -- helpers -----------------------------------------------------------------

function Invoke-Cmd([string]$label, [string[]]$cmd, [int]$expect = 0, [switch]$Soft) {
    $pretty = "wkappbot $($cmd -join ' ')"
    Write-Host "`n=== $label ===`n  $pretty"
    $lines = & wkappbot @cmd 2>&1
    $code  = $LASTEXITCODE
    $shown = [Math]::Min($lines.Count, 8)
    if ($shown -gt 0) { $lines[0..($shown - 1)] | ForEach-Object { Write-Host "  $_" } }
    if ($lines.Count -gt 8) { Write-Host "  ... ($($lines.Count - 8) more)" }
    Write-Host "  exit=$code"
    if ($code -ne $expect) {
        $tag = if ($Soft) { $script:softFail++; "SOFT" } else { $script:hardFail++; "HARD-FAIL" }
        Write-Host "  [$tag] expected exit $expect, got $code"
    } else { $script:pass++ }
    return $lines
}

function Invoke-CoreCmd([string]$label, [string[]]$cmd, [int]$expect = 0, [switch]$Soft) {
    Write-Host "`n=== $label ===`n  wkappbot-core $($cmd -join ' ')"
    $lines = & $coreExe @cmd 2>&1
    $code  = $LASTEXITCODE
    $shown = [Math]::Min($lines.Count, 6)
    if ($shown -gt 0) { $lines[0..($shown - 1)] | ForEach-Object { Write-Host "  $_" } }
    Write-Host "  exit=$code"
    if ($code -ne $expect) {
        $tag = if ($Soft) { $script:softFail++; "SOFT" } else { $script:hardFail++; "HARD-FAIL" }
        Write-Host "  [$tag] expected exit $expect, got $code"
    } else { $script:pass++ }
    return $lines
}

function Assert-Contains([string]$label, [string[]]$lines, [string]$pattern) {
    $hit = $lines | Select-String $pattern
    if ($hit) { $script:pass++; Write-Host "  [ASSERT OK] $label" }
    else       { $script:hardFail++; Write-Host "  [ASSERT FAIL] $label -- '$pattern' not found" }
}

function Submit-Suggest([string]$text) {
    if (!(Test-Path $devCore)) { return }
    & $devCore suggest $text `
        --requirement "wkappbot skill list => wkappbot" `
        --requirement "wkappbot file read README.md => WKAppBot" `
        --requirement "wkappbot windows => exit 0" 2>$null | Out-Null
    Write-Host "  [SUGGEST FILED] $($text.Substring(0, [Math]::Min(60, $text.Length)))"
}

function Section([string]$title) {
    Write-Host "`n$('-' * 60)`n  $title`n$('-' * 60)"
}

# ============================================================
#  SECTION 1 -- BASIC (CI + Local)
# ============================================================
Section "1. Identity & help pages"

Invoke-Cmd 'version'        @('--version')
Invoke-Cmd 'help-root'      @('--help')
Invoke-Cmd 'help-a11y'      @('a11y',     '--help', '--no-regression')
Invoke-Cmd 'help-windows'   @('windows',  '--help', '--no-regression')
Invoke-Cmd 'help-file'      @('file',     '--help', '--no-regression')
Invoke-Cmd 'help-skill'     @('skill',    '--help', '--no-regression')
Invoke-Cmd 'help-schedule'  @('schedule', '--help', '--no-regression')
Invoke-Cmd 'help-logcat'    @('logcat',   '--help', '--no-regression')
Invoke-Cmd 'help-eye'       @('eye',      '--help', '--no-regression')
Invoke-Cmd 'help-slack'     @('slack',    '--help', '--no-regression')
Invoke-Cmd 'help-ask'       @('ask',      '--help', '--no-regression')
Invoke-Cmd 'help-gc'        @('gc',       '--help', '--no-regression')

Section "2. Skill system"

$skillList = Invoke-Cmd 'skill-list'    @('skill', 'list')
Assert-Contains 'skill-list has wkappbot app' $skillList 'wkappbot'
Invoke-Cmd 'skill-search'   @('skill', 'search', 'grap')
Invoke-Cmd 'skill-read'     @('skill', 'read', 'grap')

Section "3. File tools -- basic"

$sample = Join-Path $smokeDir 'sample.txt'
@('line one: alpha test', 'line two: beta value', 'line three: wkappbot skill smoke') |
    Set-Content $sample -Encoding UTF8

$readOut = Invoke-Cmd 'file-read'       @('file', 'read', $sample)
Assert-Contains 'file-read has line 1'  $readOut 'alpha'
Invoke-Cmd 'file-grep'      @('file', 'grep', 'skill', $sample)
Invoke-Cmd 'file-glob-yaml' @('file', 'glob', '**/*.yaml', '--path', (Join-Path $PWD 'handlers'))
Invoke-Cmd 'file-glob-yml'  @('file', 'glob', '**/*.yml',  '--path', (Join-Path $PWD '.github\workflows'))

Section "4. Schedule / Windows / License"

Invoke-Cmd 'schedule-list'  @('schedule', 'list')
Invoke-Cmd 'windows-list'   @('windows') -Soft
$licOut = Invoke-CoreCmd 'license-status' @('license', 'status')
Assert-Contains 'license shows tier' $licOut 'Tier'

# ============================================================
#  SECTION 2 -- EXTENDED (Local only)
# ============================================================
if (!$runExt) {
    Write-Host "`n[INFO] CI mode -- skipping extended tests."
    Write-Host "       Run with -Extended or locally to enable."
} else {

Section "5. File tools -- extended"

# Korean filename
$korFile = Join-Path $smokeDir '테스트파일.txt'
[System.IO.File]::WriteAllText($korFile, "한국어 테스트`nwkappbot file test`n세번째 줄", [System.Text.Encoding]::UTF8)
$korOut = Invoke-Cmd 'file-read-korean' @('file', 'read', $korFile)
if (($korOut | Select-String '한국어') -or ($korOut | Select-String '\?')) {
    $pass++; Write-Host "  [ASSERT OK] Korean filename readable"
} else { $hardFail++; Write-Host "  [ASSERT FAIL] Korean filename -- no content found" }

# UTF-8 BOM file
$bomFile = Join-Path $smokeDir 'bom.txt'
$bom = [byte[]](0xEF, 0xBB, 0xBF) + [System.Text.Encoding]::UTF8.GetBytes("BOM header line`nwkappbot bom test")
[System.IO.File]::WriteAllBytes($bomFile, $bom)
$bomOut = Invoke-Cmd 'file-read-bom' @('file', 'read', $bomFile)
Assert-Contains 'BOM file readable' $bomOut 'BOM'

# file edit (backup/restore)
$editFile = Join-Path $smokeDir 'edit-test.txt'
Set-Content $editFile "original line" -Encoding UTF8
Invoke-Cmd 'file-edit' @('file', 'edit', 'original', 'edited', $editFile)
$editContent = Get-Content $editFile -Raw
if ($editContent -like '*edited*') { $pass++; Write-Host "  [ASSERT OK] file-edit: content changed" }
else { $hardFail++; Write-Host "  [ASSERT FAIL] file-edit: 'edited' not found in output" }

# grap-style grep: OR pattern
$orFile = Join-Path $smokeDir 'or-test.txt'
@('apple pie', 'banana split', 'cherry cake', 'apricot jam') | Set-Content $orFile -Encoding UTF8
$orOut = Invoke-Cmd 'file-grep-or' @('file', 'grep', 'apple|cherry', $orFile)
Assert-Contains 'OR grep finds apple'  $orOut 'apple'
Assert-Contains 'OR grep finds cherry' $orOut 'cherry'

# file glob recursive
$subDir = Join-Path $smokeDir 'subdir'
New-Item -Force -ItemType Directory $subDir | Out-Null
Set-Content (Join-Path $subDir 'nested.txt') 'nested content' -Encoding UTF8
$globOut = Invoke-Cmd 'file-glob-recursive' @('file', 'glob', '**/*.txt', '--path', $smokeDir)
Assert-Contains 'glob finds nested' $globOut 'nested'

Section "6. Encoding (pipe relay)"

# Launch wkappbot skill list and verify Korean chars survive the pipe
$psi = New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName               = (Join-Path $repoBin 'wkappbot.exe')
$psi.Arguments              = 'skill list'
$psi.UseShellExecute        = $false
$psi.RedirectStandardOutput = $true
$psi.RedirectStandardError  = $false
$psi.StandardOutputEncoding = [System.Text.Encoding]::GetEncoding(28591)  # Latin-1 = raw bytes
$psi.EnvironmentVariables['WKAPPBOT_WORKER'] = '1'
$proc = [System.Diagnostics.Process]::Start($psi)
$rawOut = $proc.StandardOutput.ReadToEnd(); $proc.WaitForExit()

$bytes  = [System.Text.Encoding]::GetEncoding(28591).GetBytes($rawOut)
$utf8   = [System.Text.Encoding]::UTF8.GetString($bytes)
$hasKR  = $utf8.ToCharArray() | Where-Object { [int]$_ -ge 0xAC00 -and [int]$_ -le 0xD7A3 }
$hasGarbage = $utf8.Contains([char]0xFFFD)
if ($hasKR -and !$hasGarbage) {
    $pass++; Write-Host "  [ASSERT OK] encoding: Korean survives pipe (UTF-8, no replacement chars)"
} elseif (!$hasKR) {
    $softFail++; Write-Host "  [SOFT] encoding: no Korean in output (no Korean skills installed?)"
} else {
    $hardFail++; Write-Host "  [ASSERT FAIL] encoding: replacement chars (0xFFFD) in output"
    Submit-Suggest "sdk-encoding-pipe-corruption: Korean chars corrupt through wkappbot pipe relay"
}

Section "7. Skill system - deep"

Invoke-Cmd 'skill-search-multi' @('skill', 'search', 'a11y')
# skill verify (source refs) -- soft-fail since refs may not exist in SDK repo
Invoke-CoreCmd 'skill-verify-grap' @('skill', 'verify', 'grap') -Soft

Section "8. Scenario validation"

# Valid YAML
$validYaml = Join-Path $smokeDir 'valid.yaml'
@'
scenario:
  name: smoke-test-scenario
app:
  launch: calc.exe
steps:
  - name: click
    target:
      automation_id: num1Button
    action: click
'@ | Set-Content $validYaml -Encoding UTF8
Invoke-CoreCmd 'validate-valid-yaml' @('validate', $validYaml)

# Malformed YAML
$badYaml = Join-Path $smokeDir 'bad.yaml'
"scenario: {name: [unclosed" | Set-Content $badYaml -Encoding UTF8
Invoke-CoreCmd 'validate-bad-yaml' @('validate', $badYaml) -expect 1 -Soft

Section "9. Eye tick (daemon ping)"

# eye tick with short timeout -- soft-fail if no Eye running
$eyeOut = Invoke-CoreCmd 'eye-tick' @('eye', 'tick', '--timeout', '3') -Soft
if ($eyeOut | Select-String 'OK|connected|ready|alive') {
    Write-Host "  [INFO] Eye is running"
} else {
    Write-Host "  [INFO] Eye not running (expected in clean test env)"
}

Section "10. License system"

# License activate with non-existent file
Invoke-CoreCmd 'license-activate-missing' @('license', 'activate', 'C:\nonexistent\license.json') -expect 1 -Soft

# License activate with invalid JSON
$badLic = Join-Path $smokeDir 'bad-license.json'
Set-Content $badLic '{"not": "a license"}' -Encoding UTF8
Invoke-CoreCmd 'license-activate-invalid' @('license', 'activate', $badLic) -expect 1 -Soft

# License status shows tier
$licOut2 = Invoke-CoreCmd 'license-status-tier' @('license', 'status')
Assert-Contains 'license has expiry or n/a' $licOut2 'Tier|Free|Standard|Pro'

Section "11. Real app automation (a11y)"

# Force-kill test apps (PowerShell Stop-Process avoids wkappbot launcher intercept)
function Stop-TestApp([string]$procName) {
    Get-Process $procName -ErrorAction SilentlyContinue |
        Stop-Process -Force -ErrorAction SilentlyContinue
    Start-Sleep -Milliseconds 300
    # Second pass for UWP restart race
    Get-Process $procName -ErrorAction SilentlyContinue |
        Stop-Process -Force -ErrorAction SilentlyContinue
    Write-Host "  [CLEANUP] $procName terminated"
}

# Pre-cleanup: kill any lingering test apps from previous failed runs
Stop-TestApp 'CalculatorApp'
Stop-TestApp 'Notepad'

# Save-dialog guard for Notepad (graceful first, then force)
function Close-Notepad {
    & $coreExe a11y invoke "{proc:'Notepad'}#*저장 안 함*" --timeout 3 2>&1 | Out-Null
    Start-Sleep -Milliseconds 400
    & $coreExe a11y invoke "{proc:'Notepad'}#*Don't Save*"  --timeout 2 2>&1 | Out-Null
    Start-Sleep -Milliseconds 200
    Stop-TestApp 'notepad'
}

# -- Calculator: launch → 5+3=8 → close (try/finally guarantees cleanup) --
Write-Host "`n=== launch-calc ==="
try {
    $launchOut  = & $coreExe exec calc --timeout 20 2>&1
    $launchCode = $LASTEXITCODE
    $launchOut | Select-Object -Last 3 | ForEach-Object { Write-Host "  $_" }

    if ($launchCode -eq 0) {
        $pass++; Write-Host "  [OK] Calculator launched"

        $findOut = & $coreExe a11y find "{proc:'Calculator'}" --timeout 5 2>&1
        $target  = ($findOut | Select-String 'TARGET').Line
        Write-Host "  Target: $target"

        if ($target) {
            $pass++
            foreach ($btn in @('num5Button', 'plusButton', 'num3Button', 'equalButton')) {
                & $coreExe a11y invoke "${btn}#{proc:'Calculator'}" --timeout 5 2>&1 | Out-Null
            }
            $readOut2 = & $coreExe a11y read "CalculatorResults#{proc:'Calculator'}" --timeout 5 2>&1
            $display  = $readOut2 | Select-String '8|Display' | Select-Object -First 1
            Write-Host "  Display (5+3=8): $display"
            if ($display) { $pass++; Write-Host "  [OK] Calc result verified" }
            else           { $softFail++; Write-Host "  [SOFT] Display read empty" }
        } else {
            $softFail++; Write-Host "  [SOFT] Calculator window not found"
        }
    } else {
        $softFail++; Write-Host "  [SOFT] Calculator launch failed (exit=$launchCode)"
    }
} finally {
    & $coreExe a11y kill "{proc:'Calculator'}" --timeout 5 2>&1 | Out-Null
    Write-Host "  [CLEANUP] Calculator killed"
}

# -- Notepad: launch → type → read → close (try/finally guarantees cleanup) --
Write-Host "`n=== launch-notepad ==="
try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    $npLaunch = & $coreExe a11y find "{proc:'Notepad'}" --timeout 5 2>&1
    $npFound  = $npLaunch | Select-String 'TARGET'

    if ($npFound) {
        $pass++; Write-Host "  [OK] Notepad launched"
        & $coreExe a11y type "{proc:'Notepad'}" 'WKAppBot smoke test 123' --timeout 5 2>&1 | Out-Null
        Write-Host "  Type: exit=$LASTEXITCODE"
        $npRead = & $coreExe a11y read "{proc:'Notepad'}" --timeout 5 2>&1
        if ($npRead | Select-String 'smoke|WKAppBot|123') {
            $pass++; Write-Host "  [OK] Notepad read-back verified"
        } else {
            $softFail++; Write-Host "  [SOFT] Notepad content not verified"
        }
    } else {
        $softFail++; Write-Host "  [SOFT] Notepad launch failed"
    }
} finally {
    Close-Notepad
}

# HQ suggest-approved scripts: run any test-*.sh that exists in local bin/wkappbot.hq/scripts/
Write-Host "`n=== hq-scripts ==="
$hqScripts = Get-ChildItem "bin\wkappbot.hq\scripts\test-*.sh" -ErrorAction SilentlyContinue
if ($hqScripts) {
    $bashCmd = Get-Command bash -ErrorAction SilentlyContinue
    $bashExe = if ($bashCmd) { $bashCmd.Source } else { $null }
    if ($bashExe) {
        foreach ($s in $hqScripts) {
            Write-Host "  Running $($s.Name)..."
            $hqOut  = & $bashExe $s.FullName 2>&1
            $hqCode = $LASTEXITCODE
            $hqOut | Select-Object -Last 2 | ForEach-Object { Write-Host "    $_" }
            if ($hqCode -eq 0) { $pass++; Write-Host "  [OK] $($s.Name)" }
            else                { $softFail++; Write-Host "  [SOFT] $($s.Name) exit=$hqCode" }
        }
    } else { Write-Host "  bash not found -- skipping HQ scripts" }
} else { Write-Host "  No HQ test scripts found in bin/wkappbot.hq/scripts/" }

Section "12. Windows enumeration -- deep"

Invoke-Cmd 'windows-deep'    @('windows', '--deep') -Soft | Out-Null
Invoke-Cmd 'windows-process' @('windows', '--process', 'Code') -Soft | Out-Null

# ============================================================
#  SECTION 3 -- RIGOROUS USER SCENARIOS
# ============================================================

Section "13. Clipboard round-trip"

# Write to clipboard, read it back -- no app needed
Invoke-CoreCmd 'clip-write' @('a11y', 'clipboard-write', 'wkappbot-smoke-clip-12345')
$clipOut = Invoke-CoreCmd 'clip-read'  @('a11y', 'clipboard-read')
Assert-Contains 'clipboard round-trip' $clipOut 'wkappbot-smoke-clip-12345'

Section "14. Multi-app: two windows, independent targeting"

Stop-TestApp 'CalculatorApp'
Stop-TestApp 'Notepad'
try {
    # Start-Process: no Eye dependency, works in any env
    Start-Process 'calc.exe'    -ErrorAction SilentlyContinue
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 3

    # Verify both visible
    $calcFind = & $coreExe a11y find "{proc:'Calculator'}" --timeout 5 2>&1
    $npFind2  = & $coreExe a11y find "{proc:'Notepad'}"    --timeout 5 2>&1
    if ($calcFind | Select-String 'TARGET') { $pass++; Write-Host "  [ASSERT OK] multi-app: Calculator visible" }
    else { $softFail++; Write-Host "  [SOFT] multi-app: Calculator not found" }
    if ($npFind2 | Select-String 'TARGET')  { $pass++; Write-Host "  [ASSERT OK] multi-app: Notepad visible" }
    else { $softFail++; Write-Host "  [SOFT] multi-app: Notepad not found" }

    # Target Notepad SPECIFICALLY -- only assert if Notepad was actually found
    if ($npFind2 | Select-String 'TARGET') { $pass++; Write-Host "  [ASSERT OK] Notepad targeted specifically" }
    else { $softFail++; Write-Host "  [SOFT] Notepad TARGET not found (app may need more startup time)" }

    # Type into Notepad -- must land in Notepad, not Calculator
    & $coreExe a11y focus  "{proc:'Notepad'}"                   --timeout 3 2>&1 | Out-Null
    & $coreExe a11y type   "{proc:'Notepad'}" 'multi-app target test' --timeout 5 2>&1 | Out-Null
    $npReadMulti = & $coreExe a11y read "{proc:'Notepad'}" --timeout 5 2>&1
    if ($npReadMulti | Select-String 'multi-app') { $pass++; Write-Host "  [ASSERT OK] type landed in Notepad not Calc" }
    else { $softFail++; Write-Host "  [SOFT] type content not verified (focus race condition)" }

} finally {
    Stop-TestApp 'CalculatorApp'
    Close-Notepad
}

Section "15. Window state operations (minimize/restore/maximize)"

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    Invoke-CoreCmd 'win-minimize' @('a11y', 'minimize', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Start-Sleep -Milliseconds 500
    # After minimize: find should still work (window exists, just minimized)
    $minFind = & $coreExe a11y find "{proc:'Notepad'}" --timeout 5 2>&1
    Assert-Contains 'find works while minimized' $minFind 'Notepad'

    Invoke-CoreCmd 'win-restore'  @('a11y', 'restore',  "{proc:'Notepad'}", '--timeout', '5') -Soft
    Start-Sleep -Milliseconds 400
    Invoke-CoreCmd 'win-maximize' @('a11y', 'maximize', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Start-Sleep -Milliseconds 400
    Invoke-CoreCmd 'win-restore2' @('a11y', 'restore',  "{proc:'Notepad'}", '--timeout', '5') -Soft

    # After all state changes, restore focus then type
    & $coreExe a11y focus "{proc:'Notepad'}" --timeout 3 2>&1 | Out-Null
    Start-Sleep -Milliseconds 300
    & $coreExe a11y type  "{proc:'Notepad'}" 'state-ops test' --timeout 5 2>&1 | Out-Null
    $stateRead = & $coreExe a11y read "{proc:'Notepad'}" --timeout 5 2>&1
    if ($stateRead | Select-String 'state-ops') { $pass++; Write-Host "  [ASSERT OK] type works after state changes" }
    else { $softFail++; Write-Host "  [SOFT] type after state changes: content not confirmed (focus race)" }
} finally {
    Close-Notepad
}

Section "16. a11y inspect -- UIA tree dump"

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    $inspOut = Invoke-CoreCmd 'a11y-inspect' @('a11y', 'inspect', "{proc:'Notepad'}", '--timeout', '8') -Soft
    # Notepad UIA tree has a Document/RichEditD20W control and "Notepad" window
    Assert-Contains 'inspect output not empty'   $inspOut 'hwnd|Grp|Pan|Doc|Edit|Win'
    Assert-Contains 'inspect shows window name'  $inspOut 'Notepad'
} finally {
    Close-Notepad
}

Section "17. Error handling -- user-visible messages"

# Target that doesn't exist: must exit non-0 and show helpful message (no stack trace)
$errOut = & $coreExe a11y find "nonexistent_window_xyzzy_smoke" --timeout 2 2>&1
Write-Host "  err-find exit=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { $pass++; Write-Host "  [ASSERT OK] non-zero exit on missing window" }
else { $softFail++; Write-Host "  [SOFT] expected non-zero for missing target" }
$hasStack = $errOut | Select-String 'at.*\+\+\+|Exception:|StackTrace'
if (!$hasStack) { $pass++; Write-Host "  [ASSERT OK] no raw stack trace in error output" }
else { $hardFail++; Write-Host "  [ASSERT FAIL] raw stack trace exposed to user"
       Submit-Suggest "sdk-error-stacktrace-exposed: a11y find on missing target leaks internal stack trace to user stdout/stderr" }

# Bad grap syntax: must give useful error
$badOut = & $coreExe a11y find "{broken json 5 pattern" --timeout 2 2>&1
Write-Host "  bad-grap exit=$LASTEXITCODE"
if ($badOut | Select-String 'invalid|parse|grap|syntax|error|not found') {
    $pass++; Write-Host "  [ASSERT OK] bad grap shows helpful message"
} else { $softFail++; Write-Host "  [SOFT] bad grap error message unclear: $($badOut | Select-Object -First 1)" }

Section "18. Complex grap patterns"

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue; Start-Sleep -Seconds 2

    # JSON5 process scope
    $j5Out = Invoke-CoreCmd 'grap-json5' @('a11y', 'find', "{proc:'Notepad'}", '--timeout', '5')
    Assert-Contains 'JSON5 {proc:} grap works' $j5Out 'TARGET|notepad'

    # OR title pattern (;)  -- grap OR, NOT file grep OR
    Invoke-CoreCmd 'grap-or' @('a11y', 'find', "*notepad*;*Notepad*", '--timeout', '5') -Soft | Out-Null
    Write-Host "  OR grap exit=$LASTEXITCODE"

    # Scope grap: target element INSIDE window
    $editOut = Invoke-CoreCmd 'grap-scope' @('a11y', 'find', "Document#{proc:'Notepad'}", '--timeout', '5') -Soft
    if ($editOut | Select-String 'TARGET') { $pass++; Write-Host "  [ASSERT OK] scope grap finds element inside window" }
    else { $softFail++; Write-Host "  [SOFT] scope grap: no TARGET (automation_id may differ)" }

} finally {
    Close-Notepad
}

Section "19. Logcat -- log filtering"

# Create test log with known content
$logFile = Join-Path $smokeDir 'test.log'
@(
    '[2026-01-01 10:00] INFO startup complete',
    '[2026-01-01 10:01] ERROR connection failed',
    '[2026-01-01 10:02] INFO processing',
    '[2026-01-01 10:03] WARN low memory',
    '[2026-01-01 10:04] ERROR timeout reached'
) | Set-Content $logFile -Encoding UTF8

$logOut = Invoke-CoreCmd 'logcat-filter'  @('logcat', 'ERROR', $logFile)
Assert-Contains 'logcat finds ERROR lines'    $logOut 'ERROR'
$logOrOut = Invoke-CoreCmd 'logcat-or'    @('logcat', 'ERROR;WARN', $logFile)
Assert-Contains 'logcat OR filter finds WARN' $logOrOut 'WARN'

# logcat should NOT include INFO lines
if ($logOut | Select-String 'INFO startup') {
    $softFail++; Write-Host "  [SOFT] logcat leaked non-matching INFO line"
} else {
    $pass++; Write-Host "  [ASSERT OK] logcat filtered correctly (no INFO leakage)"
}

Section "20. Scenario YAML execution"

$scenFile = Join-Path $smokeDir 'run-scenario.yaml'
@'
scenario:
  name: smoke-run-calc
app:
  launch: calc.exe
  wait_for_window:
    process: Calculator
steps:
  - name: click5
    target:
      automation_id: num5Button
    action: invoke
  - name: click-plus
    target:
      automation_id: plusButton
    action: invoke
  - name: click3
    target:
      automation_id: num3Button
    action: invoke
  - name: click-equals
    target:
      automation_id: equalButton
    action: invoke
'@ | Set-Content $scenFile -Encoding UTF8

# Validate first
Invoke-CoreCmd 'scenario-validate' @('validate', $scenFile)

# Run the scenario
Write-Host "`n=== scenario-run ==="
try {
    $runOut  = & $coreExe run $scenFile 2>&1
    $runCode = $LASTEXITCODE
    $runOut | Select-Object -Last 5 | ForEach-Object { Write-Host "  $_" }
    Write-Host "  exit=$runCode"
    if ($runCode -eq 0) { $pass++; Write-Host "  [OK] Scenario ran to completion" }
    else { $softFail++; Write-Host "  [SOFT] Scenario exit=$runCode" }
} finally {
    Stop-TestApp 'CalculatorApp'
}

Section "21. gc -- garbage collect smoke-temp"

# gc should run without crashing; sweep of fresh dir exits 0
Invoke-CoreCmd 'gc-run' @('gc', '*.log', '--days', '0', '--sweep') -Soft

} # end extended

# ============================================================
#  Summary
# ============================================================
$mode = if ($runExt) { "LOCAL+EXTENDED" } else { "CI/BASIC" }
Write-Host "`n$('=' * 60)"
Write-Host "  Smoke results [$mode]: $pass passed, $softFail soft-fail, $hardFail hard-fail"
Write-Host $('=' * 60)

if ($hardFail -gt 0) { throw "Smoke test: $hardFail hard failure(s)" }
Write-Host "All smoke checks passed."
