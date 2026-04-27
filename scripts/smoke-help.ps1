# WKAppBot launcher smoke test -- user-centric
# Validates commands a first-time user would actually run.
# Designed for GitHub Actions (windows-latest) and local manual runs alike.
$ErrorActionPreference = 'Stop'

$repoBin = Join-Path $PWD 'bin'
if (!(Test-Path (Join-Path $repoBin 'wkappbot.exe'))) { throw 'bin/wkappbot.exe missing' }
$env:PATH         = "$repoBin;$env:PATH"
$env:WKAPPBOT_WORKER = '1'   # suppress Eye auto-spawn, no Slack noise

$fail  = 0
$pass  = 0
$soft  = 0   # soft-failures: logged but do not block

function Run-Cmd([string]$label, [string[]]$cmd, [int]$expectCode = 0, [switch]$Soft) {
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
Run-Cmd 'version'       @('--version')
Run-Cmd 'help-root'     @('--help')

# ── 2. Command help pages (every major surface a user discovers) ──────────────
Run-Cmd 'help-a11y'     @('a11y',     '--help', '--no-regression')
Run-Cmd 'help-windows'  @('windows',  '--help', '--no-regression')
Run-Cmd 'help-file'     @('file',     '--help', '--no-regression')
Run-Cmd 'help-skill'    @('skill',    '--help', '--no-regression')
Run-Cmd 'help-schedule' @('schedule', '--help', '--no-regression')
Run-Cmd 'help-logcat'   @('logcat',   '--help', '--no-regression')
Run-Cmd 'help-eye'      @('eye',      '--help', '--no-regression')
Run-Cmd 'help-slack'    @('slack',    '--help', '--no-regression')
Run-Cmd 'help-ask'      @('ask',      '--help', '--no-regression')
Run-Cmd 'help-gc'       @('gc',       '--help', '--no-regression')

# ── 3. Window enumeration (read-only; empty result is OK on headless CI) ──────
Run-Cmd 'windows-list'  @('windows') -Soft

# ── 4. Skill knowledge base ───────────────────────────────────────────────────
Run-Cmd 'skill-list'    @('skill', 'list')
Run-Cmd 'skill-search'  @('skill', 'search', 'grap')

# ── 5. Schedule list ──────────────────────────────────────────────────────────
Run-Cmd 'schedule-list' @('schedule', 'list')

# ── 6. File tools -- real I/O on a temp test file ────────────────────────────
$smokeDir  = Join-Path $PWD 'smoke-temp'
New-Item -ItemType Directory -Force -Path $smokeDir | Out-Null
$sampleTxt = Join-Path $smokeDir 'sample.txt'
@('line one: alpha test', 'line two: beta value', 'line three: wkappbot skill smoke', 'line four: gamma end') |
    Set-Content -Path $sampleTxt -Encoding UTF8

Run-Cmd 'file-read'     @('file', 'read',  $sampleTxt)
Run-Cmd 'file-grep'     @('file', 'grep',  'skill', $sampleTxt)
Run-Cmd 'file-glob'     @('file', 'glob',  '**/*.yml', '--path', (Join-Path $PWD 'handlers'))

# ── Summary ───────────────────────────────────────────────────────────────────
Write-Host "`n========================================`n  Smoke results: $pass passed, $soft soft-fail, $fail hard-fail`n========================================"
if ($fail -ne 0) { throw "Smoke test: $fail hard failure(s)" }
Write-Host 'All user-centric smoke checks passed.'
