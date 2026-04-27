# WKAppBot launcher smoke test -- user-centric
# Validates commands a first-time user would actually run.
# Designed for GitHub Actions (windows-latest) and local manual runs alike.
$ErrorActionPreference = 'Continue'   # let native-exe stderr flow; we track failures ourselves

$repoBin = Join-Path $PWD 'bin'
if (!(Test-Path (Join-Path $repoBin 'wkappbot.exe'))) { throw 'bin/wkappbot.exe missing' }
$env:PATH         = "$repoBin;$env:PATH"
$env:WKAPPBOT_WORKER = '1'   # suppress Eye auto-spawn, no Slack noise

$fail  = 0
$pass  = 0
$soft  = 0   # soft-failures: logged but do not block

function Invoke-Cmd([string]$label, [string[]]$cmd, [int]$expectCode = 0, [switch]$Soft) {
    $pretty = "wkappbot $($cmd -join ' ')"
    Write-Host "`n=== $label ===`n  CMD: $pretty"
    $lines = & wkappbot @cmd 2>&1
    $code  = $LASTEXITCODE
    $shown = [Math]::Min($lines.Count, 8)
    $lines[0..($shown-1)] | ForEach-Object { Write-Host "  $_" }
    if ($lines.Count -gt 8) { Write-Host "  ... ($($lines.Count - 8) more lines)" }
    Write-Host "  EXITCODE: $code"
    if ($code -ne $expectCode) {
        if ($Soft) {
            Write-Host "  SOFT-FAIL (non-blocking): expected exit $expectCode, got $code"
            $script:soft++
        } else {
            Write-Host "  FAIL: expected exit $expectCode, got $code"
            $script:fail++
        }
    } else {
        $script:pass++
    }
}

# ── 1. Binary identity ────────────────────────────────────────────────────────
Invoke-Cmd 'version'       @('--version')
Invoke-Cmd 'help-root'     @('--help')

# ── 2. Command help pages (every major surface a user discovers) ──────────────
Invoke-Cmd 'help-a11y'     @('a11y',     '--help', '--no-regression')
Invoke-Cmd 'help-windows'  @('windows',  '--help', '--no-regression')
Invoke-Cmd 'help-file'     @('file',     '--help', '--no-regression')
Invoke-Cmd 'help-skill'    @('skill',    '--help', '--no-regression')
Invoke-Cmd 'help-schedule' @('schedule', '--help', '--no-regression')
Invoke-Cmd 'help-logcat'   @('logcat',   '--help', '--no-regression')
Invoke-Cmd 'help-eye'      @('eye',      '--help', '--no-regression')
Invoke-Cmd 'help-slack'    @('slack',    '--help', '--no-regression')
Invoke-Cmd 'help-ask'      @('ask',      '--help', '--no-regression')
Invoke-Cmd 'help-gc'       @('gc',       '--help', '--no-regression')

# ── 3. Window enumeration (read-only; empty result is OK on headless CI) ──────
Invoke-Cmd 'windows-list'  @('windows') -Soft

# ── 4. Skill knowledge base ───────────────────────────────────────────────────
Invoke-Cmd 'skill-list'    @('skill', 'list')
Invoke-Cmd 'skill-search'  @('skill', 'search', 'grap')

# ── 5. Schedule list ──────────────────────────────────────────────────────────
Invoke-Cmd 'schedule-list' @('schedule', 'list')

# ── 6. File tools -- real I/O on a temp test file ────────────────────────────
$smokeDir  = Join-Path $PWD 'smoke-temp'
New-Item -ItemType Directory -Force -Path $smokeDir | Out-Null
$sampleTxt = Join-Path $smokeDir 'sample.txt'
@('line one: alpha test', 'line two: beta value', 'line three: wkappbot skill smoke', 'line four: gamma end') |
    Set-Content -Path $sampleTxt -Encoding UTF8

Invoke-Cmd 'file-read'     @('file', 'read',  $sampleTxt)
Invoke-Cmd 'file-grep'     @('file', 'grep',  'skill', $sampleTxt)
Invoke-Cmd 'file-glob'     @('file', 'glob',  '**/*.yml', '--path', (Join-Path $PWD 'handlers'))

# ── Summary ───────────────────────────────────────────────────────────────────
Write-Host "`n========================================`n  Smoke results: $pass passed, $soft soft-fail, $fail hard-fail`n========================================"
if ($fail -ne 0) { throw "Smoke test: $fail hard failure(s)" }
Write-Host 'All user-centric smoke checks passed.'
