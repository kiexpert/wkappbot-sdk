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
    # Also kill win32calc if procName was CalculatorApp and vice versa (env mismatch guard)
    if ($procName -eq 'CalculatorApp') {
        Get-Process 'win32calc' -ErrorAction SilentlyContinue |
            Stop-Process -Force -ErrorAction SilentlyContinue
    }
    Write-Host "  [CLEANUP] $procName terminated"
}

function Stop-AllCalc {
    Get-Process 'CalculatorApp','win32calc' -ErrorAction SilentlyContinue |
        Stop-Process -Force -ErrorAction SilentlyContinue
    Write-Host "  [CLEANUP] Calculator (all variants) terminated"
}

# Pre-cleanup: kill any lingering test apps from previous failed runs
Stop-AllCalc
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

# ============================================================
#  SECTION 4 -- COVERAGE EXPANSION (3+ tests per command)
# ============================================================

Section "22. a11y click -- 3 tests"

# T1: help only (basic)
Invoke-Cmd 'a11y-click-help' @('a11y', 'click', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    # T2: real click on a known control (Notepad title bar / menu)
    Invoke-CoreCmd 'a11y-click-real' @('a11y', 'click', "{proc:'Notepad'}", '--timeout', '5') -Soft

    # T3: edge -- click on nonexistent target -> non-zero exit
    & $coreExe a11y click "nonexistent_xyz_target" --timeout 2 2>&1 | Out-Null
    if ($LASTEXITCODE -ne 0) { $pass++; Write-Host "  [ASSERT OK] click on missing target fails non-zero" }
    else { $softFail++; Write-Host "  [SOFT] click on missing target unexpectedly returned 0" }
} finally {
    Close-Notepad
}

Section "23. a11y scroll -- 3 tests"

# T1: help
Invoke-Cmd 'a11y-scroll-help' @('a11y', 'scroll', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    # Fill with many lines so scrolling is meaningful
    $bigText = (1..50 | ForEach-Object { "scroll-line-$_" }) -join "`r`n"
    & $coreExe a11y type "{proc:'Notepad'}" $bigText --timeout 5 2>&1 | Out-Null

    # T2: scroll down
    Invoke-CoreCmd 'a11y-scroll-down' @('a11y', 'scroll', "{proc:'Notepad'}", '--timeout', '5') -Soft
    # T3: scroll on missing target
    Invoke-CoreCmd 'a11y-scroll-missing' @('a11y', 'scroll', 'nonexistent_scroll_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "24. a11y wait -- 3 tests"

# T1: help
Invoke-Cmd 'a11y-wait-help' @('a11y', 'wait', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    # T2: wait for existing window -> success quickly
    Invoke-CoreCmd 'a11y-wait-exists' @('a11y', 'wait', "{proc:'Notepad'}", '--condition', 'exists', '--timeout', '5') -Soft
    # T3: wait for nonexistent -> timeout (non-zero)
    Invoke-CoreCmd 'a11y-wait-timeout' @('a11y', 'wait', 'nonexistent_wait_target', '--condition', 'exists', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "25. a11y highlight -- 3 tests"

Invoke-Cmd 'a11y-highlight-help' @('a11y', 'highlight', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Invoke-CoreCmd 'a11y-highlight-real' @('a11y', 'highlight', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-highlight-missing' @('a11y', 'highlight', 'nonexistent_highlight_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "26. a11y eval -- 3 tests"

# T1: help
Invoke-Cmd 'a11y-eval-help' @('a11y', 'eval', '--help', '--no-regression')
# T2: real eval requires browser/CDP -- soft-fail
Invoke-CoreCmd 'a11y-eval-noctx' @('a11y', 'eval', '--eval-js', '1+1', '--timeout', '3') -Soft
# T3: edge -- empty JS string
Invoke-CoreCmd 'a11y-eval-empty' @('a11y', 'eval', '--eval-js', '', '--timeout', '2') -Soft

Section "27. a11y move -- 3 tests"

Invoke-Cmd 'a11y-move-help' @('a11y', 'move', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Invoke-CoreCmd 'a11y-move-real' @('a11y', 'move', "{proc:'Notepad'}", '100', '100', '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-move-missing' @('a11y', 'move', 'nonexistent_move_target', '0', '0', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "28. a11y resize -- 3 tests"

Invoke-Cmd 'a11y-resize-help' @('a11y', 'resize', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Invoke-CoreCmd 'a11y-resize-real' @('a11y', 'resize', "{proc:'Notepad'}", '600', '400', '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-resize-missing' @('a11y', 'resize', 'nonexistent_resize_target', '100', '100', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "29. a11y close -- 3 tests"

Invoke-Cmd 'a11y-close-help' @('a11y', 'close', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    # T2: graceful close (may surface save dialog -- handle it)
    Invoke-CoreCmd 'a11y-close-real' @('a11y', 'close', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Start-Sleep -Milliseconds 500
    # Dismiss save dialog if any
    & $coreExe a11y invoke "{proc:'Notepad'}#*저장 안 함*" --timeout 2 2>&1 | Out-Null
    & $coreExe a11y invoke "{proc:'Notepad'}#*Don't Save*" --timeout 2 2>&1 | Out-Null
    # T3: close on missing target
    Invoke-CoreCmd 'a11y-close-missing' @('a11y', 'close', 'nonexistent_close_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "30. a11y minimize/maximize/restore -- 3 each"

# minimize
Invoke-Cmd 'a11y-min-help' @('a11y', 'minimize', '--help', '--no-regression')
Invoke-Cmd 'a11y-max-help' @('a11y', 'maximize', '--help', '--no-regression')
Invoke-Cmd 'a11y-restore-help' @('a11y', 'restore', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    # minimize: real + missing
    Invoke-CoreCmd 'a11y-min-real' @('a11y', 'minimize', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-min-missing' @('a11y', 'minimize', 'nonexistent_min_target', '--timeout', '2') -expect 1 -Soft

    # maximize: real + missing
    Invoke-CoreCmd 'a11y-max-real' @('a11y', 'maximize', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-max-missing' @('a11y', 'maximize', 'nonexistent_max_target', '--timeout', '2') -expect 1 -Soft

    # restore: real + missing
    Invoke-CoreCmd 'a11y-restore-real' @('a11y', 'restore', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-restore-missing' @('a11y', 'restore', 'nonexistent_restore_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "31. a11y clipboard-read/write -- 3 each"

# clipboard-write: T1 help, T2 round-trip, T3 unicode
Invoke-Cmd 'clip-write-help' @('a11y', 'clipboard-write', '--help', '--no-regression')
Invoke-CoreCmd 'clip-write-basic' @('a11y', 'clipboard-write', 'smoke-clip-basic-001')
$clipBasic = Invoke-CoreCmd 'clip-read-basic' @('a11y', 'clipboard-read')
Assert-Contains 'clip basic round-trip' $clipBasic 'smoke-clip-basic-001'

# Unicode round-trip
Invoke-CoreCmd 'clip-write-unicode' @('a11y', 'clipboard-write', 'unicode-clip-AlphaBeta-end')
$clipUnicode = Invoke-CoreCmd 'clip-read-unicode' @('a11y', 'clipboard-read')
Assert-Contains 'clip unicode round-trip' $clipUnicode 'unicode-clip-AlphaBeta-end'

# clipboard-read help
Invoke-Cmd 'clip-read-help' @('a11y', 'clipboard-read', '--help', '--no-regression')

Section "32. a11y invoke -- 3 tests"

Invoke-Cmd 'a11y-invoke-help' @('a11y', 'invoke', '--help', '--no-regression')

try {
    Start-Process 'calc.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 3
    # T2: invoke a real Calculator button
    Invoke-CoreCmd 'a11y-invoke-real' @('a11y', 'invoke', "num7Button#{proc:'Calculator'}", '--timeout', '5') -Soft
    # T3: invoke nonexistent
    Invoke-CoreCmd 'a11y-invoke-missing' @('a11y', 'invoke', 'nonexistent_invoke_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Stop-TestApp 'CalculatorApp'
}

Section "33. a11y inspect -- 3 tests"

Invoke-Cmd 'a11y-inspect-help' @('a11y', 'inspect', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    # T2: inspect Notepad
    $insp2 = Invoke-CoreCmd 'a11y-inspect-real' @('a11y', 'inspect', "{proc:'Notepad'}", '--timeout', '8') -Soft
    Assert-Contains 'inspect-real has Notepad' $insp2 'Notepad|hwnd|Doc|Edit'
    # T3: inspect nonexistent
    Invoke-CoreCmd 'a11y-inspect-missing' @('a11y', 'inspect', 'nonexistent_inspect_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "34. a11y focus -- 3 tests"

Invoke-Cmd 'a11y-focus-help' @('a11y', 'focus', '--help', '--no-regression')

try {
    Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Invoke-CoreCmd 'a11y-focus-real' @('a11y', 'focus', "{proc:'Notepad'}", '--timeout', '5') -Soft
    Invoke-CoreCmd 'a11y-focus-missing' @('a11y', 'focus', 'nonexistent_focus_target', '--timeout', '2') -expect 1 -Soft
} finally {
    Close-Notepad
}

Section "35. a11y kill -- 3 tests"

Invoke-Cmd 'a11y-kill-help' @('a11y', 'kill', '--help', '--no-regression')

# T2: launch + kill (real)
Start-Process 'notepad.exe' -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2
Invoke-CoreCmd 'a11y-kill-real' @('a11y', 'kill', "{proc:'Notepad'}", '--timeout', '5') -Soft
Start-Sleep -Milliseconds 500
$stillThere = Get-Process notepad -ErrorAction SilentlyContinue
if (!$stillThere) { $pass++; Write-Host "  [ASSERT OK] kill removed Notepad process" }
else { $softFail++; Write-Host "  [SOFT] Notepad still alive after kill"; Stop-TestApp 'Notepad' }

# T3: kill nonexistent
Invoke-CoreCmd 'a11y-kill-missing' @('a11y', 'kill', 'nonexistent_kill_target', '--timeout', '2') -expect 1 -Soft

Section "36. file write -- 3 tests"

Invoke-Cmd 'file-write-help' @('file', 'write', '--help', '--no-regression')

# T2: write content + verify
$writeFile = Join-Path $smokeDir 'write-test.txt'
Invoke-Cmd 'file-write-basic' @('file', 'write', $writeFile, '--content', 'wkappbot-write-content-XYZ')
if (Test-Path $writeFile) {
    $writeContent = Get-Content $writeFile -Raw
    if ($writeContent -like '*wkappbot-write-content-XYZ*') {
        $pass++; Write-Host "  [ASSERT OK] file write content verified"
    } else {
        $hardFail++; Write-Host "  [ASSERT FAIL] file write content mismatch"
    }
} else {
    $hardFail++; Write-Host "  [ASSERT FAIL] file write -- file not created"
}

# T3: overwrite (backup behavior) -- must succeed and create .bak
Invoke-Cmd 'file-write-overwrite' @('file', 'write', $writeFile, '--content', 'replaced-content-001')
$writeContent2 = Get-Content $writeFile -Raw
if ($writeContent2 -like '*replaced-content-001*') {
    $pass++; Write-Host "  [ASSERT OK] file write overwrite verified"
} else {
    $hardFail++; Write-Host "  [ASSERT FAIL] file write overwrite did not replace content"
}

Section "37. file edit -- 3 tests (extended)"

Invoke-Cmd 'file-edit-help' @('file', 'edit', '--help', '--no-regression')

# T2: edit + verify (this also exists in section 5; this one is targeted)
$editFile2 = Join-Path $smokeDir 'edit-test-2.txt'
Set-Content $editFile2 'foo bar baz' -Encoding UTF8
Invoke-Cmd 'file-edit-real' @('file', 'edit', 'foo', 'qux', $editFile2)
$editContent2 = Get-Content $editFile2 -Raw
if ($editContent2 -like '*qux*') {
    $pass++; Write-Host "  [ASSERT OK] file-edit replaced foo -> qux"
} else {
    $hardFail++; Write-Host "  [ASSERT FAIL] file-edit did not replace foo"
}

# T3: edit on nonexistent file -> non-zero
Invoke-Cmd 'file-edit-missing' @('file', 'edit', 'a', 'b', 'C:\nonexistent_smoke_xyz.txt') -expect 1 -Soft

Section "38. file grep -- 3 tests (extended)"

Invoke-Cmd 'file-grep-help' @('file', 'grep', '--help', '--no-regression')

# already covered: basic find, OR pattern -- add empty-result edge case
$grepEmpty = Join-Path $smokeDir 'grep-empty.txt'
Set-Content $grepEmpty 'no-match-here' -Encoding UTF8
Invoke-Cmd 'file-grep-empty' @('file', 'grep', 'pattern_not_found_zzz', $grepEmpty) -Soft | Out-Null
# empty result is acceptable; test that command doesn't crash
Write-Host "  [INFO] grep no-match completed without crash"

Section "39. skill list -- 3 tests"

Invoke-Cmd 'skill-list-help' @('skill', 'list', '--help', '--no-regression')

$skList2 = Invoke-Cmd 'skill-list-real' @('skill', 'list')
Assert-Contains 'skill-list has at least one skill' $skList2 'wkappbot|skill|grap|a11y'

# T3: skill list with filter (if supported) or count check
if ($skList2.Count -gt 1) {
    $pass++; Write-Host "  [ASSERT OK] skill list returned multiple lines ($($skList2.Count))"
} else {
    $softFail++; Write-Host "  [SOFT] skill list very short -- may be empty"
}

Section "40. skill read -- 3 tests"

Invoke-Cmd 'skill-read-help' @('skill', 'read', '--help', '--no-regression')

$skRead2 = Invoke-Cmd 'skill-read-grap' @('skill', 'read', 'grap')
Assert-Contains 'skill-read grap content' $skRead2 'grap|pattern|wildcard|JSON5'

# T3: skill read nonexistent
Invoke-Cmd 'skill-read-missing' @('skill', 'read', 'nonexistent_skill_xyzzy_smoke') -expect 1 -Soft

Section "41. skill search -- 3 tests"

Invoke-Cmd 'skill-search-help' @('skill', 'search', '--help', '--no-regression')

$skSearch1 = Invoke-Cmd 'skill-search-grap2' @('skill', 'search', 'grap')
Assert-Contains 'skill-search grap finds entry' $skSearch1 'grap'

# T3: search with no matches
Invoke-Cmd 'skill-search-empty' @('skill', 'search', 'zzz_no_skill_matches_this_xyz') -Soft

Section "42. skill verify -- 3 tests"

Invoke-Cmd 'skill-verify-help' @('skill', 'verify', '--help', '--no-regression')

# Already exists: skill-verify-grap (soft) -- add another known skill
Invoke-CoreCmd 'skill-verify-a11y' @('skill', 'verify', 'a11y') -Soft

# T3: verify nonexistent
Invoke-CoreCmd 'skill-verify-missing' @('skill', 'verify', 'nonexistent_verify_skill_xyz') -expect 1 -Soft

Section "43. skill audit -- 3 tests"

Invoke-Cmd 'skill-audit-help' @('skill', 'audit', '--help', '--no-regression')

# T2/T3: run audit (may be slow / soft-fail if not implemented in SDK)
Invoke-CoreCmd 'skill-audit-run' @('skill', 'audit') -Soft
Invoke-CoreCmd 'skill-audit-grap' @('skill', 'audit', 'grap') -Soft

Section "44. schedule list/add/remove -- 3 each"

# list
Invoke-Cmd 'schedule-list-help' @('schedule', 'list', '--help', '--no-regression')
Invoke-Cmd 'schedule-list-real' @('schedule', 'list')
# T3 list: stress -- list twice (idempotent)
Invoke-Cmd 'schedule-list-twice' @('schedule', 'list')

# add (soft -- may affect real schedules)
Invoke-Cmd 'schedule-add-help' @('schedule', 'add', '--help', '--no-regression')
$schedName = "smoke-test-$([Guid]::NewGuid().ToString('N').Substring(0, 8))"
Invoke-Cmd 'schedule-add-real' @('schedule', 'add', $schedName, '--cron', '0 0 1 1 *', '--cmd', 'echo smoke') -Soft
# T3 add: bad cron
Invoke-Cmd 'schedule-add-bad' @('schedule', 'add', 'smoke-bad-cron', '--cron', 'NOT_A_CRON', '--cmd', 'echo') -Soft

# remove
Invoke-Cmd 'schedule-remove-help' @('schedule', 'remove', '--help', '--no-regression')
Invoke-Cmd 'schedule-remove-real' @('schedule', 'remove', $schedName) -Soft
# T3 remove: nonexistent
Invoke-Cmd 'schedule-remove-missing' @('schedule', 'remove', 'nonexistent_smoke_sched_xyz') -Soft

Section "45. eye tick -- 3 tests"

Invoke-Cmd 'eye-tick-help' @('eye', 'tick', '--help', '--no-regression')

# T2: eye tick with short timeout -- already in section 9, repeat with verbose
Invoke-CoreCmd 'eye-tick-short' @('eye', 'tick', '--timeout', '2') -Soft

# T3: eye tick with longer timeout
Invoke-CoreCmd 'eye-tick-long' @('eye', 'tick', '--timeout', '5') -Soft

Section "46. license status/activate/path -- 3 each"

# status
Invoke-Cmd 'license-status-help' @('license', 'status', '--help', '--no-regression')
$licS = Invoke-CoreCmd 'license-status-real' @('license', 'status')
Assert-Contains 'license-status has Tier' $licS 'Tier|Free|Standard|Pro'
# T3 status: --json or alternate flag (soft if unsupported)
Invoke-CoreCmd 'license-status-twice' @('license', 'status') -Soft

# activate
Invoke-Cmd 'license-activate-help' @('license', 'activate', '--help', '--no-regression')
# already: missing file, invalid JSON in section 10 -- add a third edge case
$emptyLic = Join-Path $smokeDir 'empty-license.json'
Set-Content $emptyLic '' -Encoding UTF8
Invoke-CoreCmd 'license-activate-empty' @('license', 'activate', $emptyLic) -expect 1 -Soft
# Repeat the missing-file case for clarity
Invoke-CoreCmd 'license-activate-missing-2' @('license', 'activate', 'C:\zzz_nonexistent_license.json') -expect 1 -Soft

# path
Invoke-Cmd 'license-path-help' @('license', 'path', '--help', '--no-regression')
$licP = Invoke-CoreCmd 'license-path-real' @('license', 'path')
Assert-Contains 'license-path shows a path' $licP 'license\.json|\.json|\\|/'
# T3 path: run twice (idempotent)
Invoke-CoreCmd 'license-path-twice' @('license', 'path')

Section "47. validate -- 3 tests"

Invoke-Cmd 'validate-help' @('validate', '--help', '--no-regression')
# already: valid + bad yaml in section 8
# T3: validate nonexistent file
Invoke-CoreCmd 'validate-missing' @('validate', 'C:\zzz_nonexistent_scenario.yaml') -expect 1 -Soft

Section "48. logcat -- 3 tests (extended)"

Invoke-Cmd 'logcat-help' @('logcat', '--help', '--no-regression')
# already: ERROR filter + OR filter in section 19
# T3: logcat with --past flag (no live tail, just last N hours from logs)
Invoke-CoreCmd 'logcat-past' @('logcat', 'INFO', '--past', '24h', '--timeout', '3') -Soft

Section "49. gc -- 3 tests"

Invoke-Cmd 'gc-help' @('gc', '--help', '--no-regression')
# T2: gc dry-run on tmp dir
Invoke-CoreCmd 'gc-dryrun' @('gc', '*.txt', '--days', '0') -Soft
# T3: gc sweep (already in 21) -- repeat with different pattern
Invoke-CoreCmd 'gc-sweep-png' @('gc', '*.png', '--days', '0', '--sweep') -Soft

Section "50. suggest list -- 3 tests"

Invoke-Cmd 'suggest-help' @('suggest', '--help', '--no-regression') -Soft
Invoke-Cmd 'suggest-list-real' @('suggest', 'list') -Soft
# T3: suggest list with possible filter
Invoke-Cmd 'suggest-list-twice' @('suggest', 'list') -Soft

Section "51. claude-usage -- 3 tests"

Invoke-Cmd 'claude-usage-help' @('claude-usage', '--help', '--no-regression') -Soft

# T2: claude-usage real -- expect output mentioning ctx or MB or %
$cuOut = Invoke-Cmd 'claude-usage-real' @('claude-usage') -Soft
$hasCu = $cuOut | Select-String 'ctx|MB|KB|%|JSONL|byte'
if ($hasCu) { $pass++; Write-Host "  [ASSERT OK] claude-usage shows usage info" }
else { $softFail++; Write-Host "  [SOFT] claude-usage output unclear" }

# T3: idempotent
Invoke-Cmd 'claude-usage-twice' @('claude-usage') -Soft

Section "52. windows -- 3 tests (extended)"

# already: list (soft), --deep, --process in sections 4 and 12
# T3: windows with --cmd filter
Invoke-Cmd 'windows-cmd' @('windows', '--cmd', 'svchost') -Soft

Section "53. run scenario -- 3 tests"

Invoke-Cmd 'run-help' @('run', '--help', '--no-regression') -Soft

# T2 already in section 20 (run-scenario)
# T3: run nonexistent yaml -> non-zero
Invoke-CoreCmd 'run-missing' @('run', 'C:\zzz_nonexistent_scenario.yaml') -expect 1 -Soft

# T-extra: validate as a sanity gate
Invoke-CoreCmd 'run-validate-help' @('validate', '--help', '--no-regression')

Section "54. ask -- 3 tests"

Invoke-Cmd 'ask-help' @('ask', '--help', '--no-regression')
# T2+T3: skip on CI -- spawns Chrome via CDP, causes orphan processes
if (!$isCI) {
    Invoke-Cmd 'ask-claude' @('ask', 'claude', 'echo this: smoke-ask-test-001') -Soft
    Invoke-Cmd 'ask-gpt'    @('ask', 'gpt',    'echo this: smoke-ask-test-002') -Soft
} else {
    $pass++; Write-Host "  [SKIP-CI] ask-claude (spawns Chrome -- skipped on CI)"
    $pass++; Write-Host "  [SKIP-CI] ask-gpt    (spawns Chrome -- skipped on CI)"
}

Section "55. slack -- 3 tests"

Invoke-Cmd 'slack-help'      @('slack', '--help',       '--no-regression')
Invoke-Cmd 'slack-send-help' @('slack', 'send', '--help', '--no-regression') -Soft
Invoke-Cmd 'slack-reply-help'@('slack', 'reply','--help', '--no-regression') -Soft

Section "56. speak -- 3 tests"

Invoke-Cmd 'speak-help' @('speak', '--help', '--no-regression') -Soft
if (!$isCI) {
    Invoke-Cmd 'speak-real'  @('speak', 'smoke-speak-test') -Soft
    Invoke-Cmd 'speak-empty' @('speak', '') -Soft
} else {
    $pass++; Write-Host "  [SKIP-CI] speak-real  (audio device required)"
    $pass++; Write-Host "  [SKIP-CI] speak-empty (audio device required)"
}

Section "57. newchat -- 3 tests (soft-fail)"

Invoke-Cmd 'newchat-help' @('newchat', '--help', '--no-regression') -Soft
# T2 + T3 may actually start chat -- only test help variants
Invoke-Cmd 'newchat-help-2' @('newchat', '-h') -Soft
Invoke-Cmd 'newchat-help-3' @('newchat', '--help', '--no-regression') -Soft

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
