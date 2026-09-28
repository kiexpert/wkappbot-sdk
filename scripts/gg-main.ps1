# gg-main.ps1 -- SDK unified main-duty runner
# Skill refs: sdk-gg-main-automation, wktool-pattern, wkharness-guards
# Run: powershell -File scripts/gg-main.ps1
# Exit: 0=PASS 1=WARN 2=FAIL
#
# MAIN DUTY: keep this tool working. A broken section is fixed before new work.

$ErrorActionPreference = "SilentlyContinue"

Write-Host "=== gg-main: SDK Main-Duty Runner ===" -ForegroundColor Cyan
Write-Host "[$(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')]"
Write-Host ""

$WARNS = 0
$FAILS = 0

function Pass  { param($msg) Write-Host "  [PASS] $msg" -ForegroundColor Green }
function Warn  { param($msg) Write-Host "  [WARN] $msg" -ForegroundColor Yellow; $script:WARNS++ }
function Fail  { param($msg) Write-Host "  [FAIL] $msg" -ForegroundColor Red;    $script:FAILS++ }
function Info  { param($msg) Write-Host "        $msg" }
function Hdr   { param($t)   Write-Host "==[ $t ]==" -ForegroundColor Cyan }

# ============================================================================
# SECTION Q: External-User QA Smoke
# Skill: wktool-pattern, sdk-user-perspective-test-playbook
# ============================================================================
Hdr "Q  EXTERNAL-USER QA SMOKE"
$wkOk = $null -ne (Get-Command wkappbot -ErrorAction SilentlyContinue)
if (-not $wkOk) {
  Fail "wkappbot not on PATH -- install incomplete"
} else {
  # wkappbot --version
  $ver = & wkappbot --version 2>&1
  if ($ver -match '\d+\.\d+\.\d+') {
    Pass "wkappbot --version: $($ver -replace '\s+',' ')"
  } else {
    Fail "wkappbot --version returned no version string"
  }
  # wkappbot --help must list core subcommands
  $help = & wkappbot --help 2>&1
  $requiredCmds = @('a11y','skill','eye','ask','chat','file')
  $missing = $requiredCmds | Where-Object { ($help | Out-String) -notmatch [regex]::Escape($_) }
  if ($missing) {
    Warn "wkappbot --help missing subcommands: $($missing -join ', ')"
  } else {
    Pass "wkappbot --help contains all documented subcommands"
  }
  # wkappbot skill list
  $skillList = & wkappbot skill list 2>&1
  if ($LASTEXITCODE -eq 0 -or ($skillList | Out-String).Length -gt 10) {
    Pass "wkappbot skill list returned output"
  } else {
    Warn "wkappbot skill list returned empty or error"
  }
}
Write-Host ""

# ============================================================================
# SECTION H: Secret & Internal-Path Hygiene Scan
# Skill: wkharness-guards (PUBLIC-REPO SECRET HYGIENE rule)
# ============================================================================
Hdr "H  SECRET / INTERNAL-PATH HYGIENE"
$INTERNAL_PATTERNS = @(
  'D:\\GitHub\\',
  'D:/GitHub/',
  'C:\\Program Files\\dotnet',
  'C:/Program Files/dotnet',
  'WkAutoQuant',
  'WkFeedQuant',
  'personal-docs',
  'kivilab\.co\.kr'
)
$publicFiles = Get-ChildItem -Recurse -Include '*.md','*.txt','*.yml','*.yaml' `
  -ErrorAction SilentlyContinue |
  Where-Object { $_.FullName -notmatch '\\\.git\\' -and
                 $_.FullName -notmatch '\\\.' -and
                 $_.FullName -notmatch '\\node_modules\\' }
$hygieneHits = 0
foreach ($pat in $INTERNAL_PATTERNS) {
  $ms = $publicFiles | Select-String -Pattern $pat -ErrorAction SilentlyContinue
  foreach ($m in $ms) {
    Fail "Internal ref in $($m.Filename):$($m.LineNumber) -- $pat"
    $hygieneHits++
  }
}
if ($hygieneHits -eq 0) {
  Pass "No internal paths found ($($publicFiles.Count) files scanned)"
}
Write-Host ""

# ============================================================================
# SECTION V: Release Consistency
# Skill: sdk-release-playbook-howto
# Checks: VERSIONING.md / README.md / CHANGELOG.md / SECURITY.md version parity
# ============================================================================
Hdr "V  RELEASE CONSISTENCY"
$versioningRaw = Get-Content VERSIONING.md -Raw -ErrorAction SilentlyContinue
$readmeRaw     = Get-Content README.md      -Raw -ErrorAction SilentlyContinue
$changelogRaw  = Get-Content CHANGELOG.md   -Raw -ErrorAction SilentlyContinue
$securityRaw   = Get-Content SECURITY.md    -Raw -ErrorAction SilentlyContinue

$verVer = if ($versioningRaw -match 'v(\d+\.\d+\.\d+(?:-sdk)?)') { $matches[1] } else { "" }
$rmeVer = if ($readmeRaw     -match 'v(\d+\.\d+\.\d+-sdk)')      { $matches[1] } else { "" }
$clVer  = if ($changelogRaw  -match '##\s+\[(\d+\.\d+\.\d+(?:-sdk)?)\]') { $matches[1] } else { "" }

function StripSdk { param($v) $v -replace '-sdk','' }

if ($verVer -and $rmeVer) {
  if ((StripSdk $verVer) -eq (StripSdk $rmeVer)) {
    Pass "VERSIONING ($verVer) matches README ($rmeVer)"
  } else {
    Fail "Version mismatch: VERSIONING=$verVer vs README=$rmeVer"
  }
} else {
  Warn "Could not parse version from VERSIONING.md or README.md"
}
if ($clVer -and $rmeVer) {
  if ((StripSdk $clVer) -eq (StripSdk $rmeVer)) {
    Pass "CHANGELOG ($clVer) matches README ($rmeVer)"
  } else {
    Fail "Version mismatch: CHANGELOG=$clVer vs README=$rmeVer"
  }
} else {
  Warn "Could not parse CHANGELOG top version"
}
# Check CHANGELOG top entry is not a stub
if ($changelogRaw -match '##\s+\[\d+\.\d+\.\d+.*?\]\s*\n\s*(to\s+be\s+filled|placeholder|TBD|stub)') {
  Fail "CHANGELOG top entry is a stub -- fill it before release"
} else {
  Pass "CHANGELOG top entry appears non-stub"
}
# Check SECURITY.md references current major version
if ($securityRaw -and $rmeVer -match '^(\d+)\.') {
  $secMajor = $matches[1]
  if ($securityRaw -match "v?$secMajor\.") {
    Pass "SECURITY.md references current major version $secMajor"
  } else {
    Warn "SECURITY.md may not list current major version ($secMajor)"
  }
}
Write-Host ""

# ============================================================================
# SECTION C: CI / GitHub Actions Status
# Skill: sdk-gg-main-automation
# ============================================================================
Hdr "C  CI STATUS"
$ghOk = $null -ne (Get-Command gh -ErrorAction SilentlyContinue)
if ($ghOk) {
  $runs = gh run list --repo kiexpert/wkappbot-sdk --limit 5 2>&1
  if ($LASTEXITCODE -eq 0) {
    $runs | Select-Object -First 6 | ForEach-Object { Info $_ }
    $failed = $runs | Select-String 'failure|cancelled' -ErrorAction SilentlyContinue
    if ($failed) {
      Warn "Recent CI failures detected -- check gh run list"
    } else {
      Pass "Recent CI runs: no failures"
    }
  } else {
    Warn "gh run list failed (check auth or network)"
  }
} else {
  Warn "gh not available -- skipping CI check"
}
Write-Host ""

# ============================================================================
# SECTION P: Suggest Backlog
# Skill: suggest-workflow
# ============================================================================
Hdr "P  SUGGEST BACKLOG"
if ($wkOk) {
  # Run suggest list bare -- no pipe (anti-knowledge guard)
  $suggestOut  = & wkappbot suggest list 2>&1
  $suggestText = $suggestOut | Out-String
  # Count urgent / important in output
  $urgentCount    = ([regex]::Matches($suggestText, 'URGENT|urgent')).Count
  $importantCount = ([regex]::Matches($suggestText, 'IMPORTANT|important')).Count
  Info "Urgent: $urgentCount  Important: $importantCount"
  if ($urgentCount -gt 0) {
    Fail "$urgentCount urgent suggests pending -- resolve before new work"
  } elseif ($importantCount -gt 5) {
    Warn "$importantCount important suggests in backlog"
  } else {
    Pass "Suggest backlog within normal range"
  }
} else {
  Warn "wkappbot not available -- skipping suggest check"
}
Write-Host ""

# ============================================================================
# SECTION E: Eye / Process Health
# Skill: wktool-pattern
# ============================================================================
Hdr "E  EYE / PROCESS HEALTH"
$coreCount   = @(Get-Process wkappbot-core -ErrorAction SilentlyContinue).Count
$chromeCount = @(Get-Process chrome         -ErrorAction SilentlyContinue).Count
Info "wkappbot-core processes: $coreCount"
Info "Chrome processes:        $chromeCount"
if ($coreCount -gt 3) {
  Warn "wkappbot-core $coreCount > 3 -- possible zombie accumulation"
}
if ($chromeCount -gt 5) {
  Fail "Chrome $chromeCount > 5 -- Chrome multiplication bug (see MyCdpContext.ChromeHealthCheck)"
}
if ($coreCount -le 3 -and $chromeCount -le 5) {
  Pass "Process counts normal"
}
Write-Host ""

# ============================================================================
# SUMMARY
# ============================================================================
Write-Host "=== gg-main SUMMARY ===" -ForegroundColor Cyan
if ($FAILS -gt 0) {
  Write-Host "STATUS: FAIL ($FAILS failures, $WARNS warnings)" -ForegroundColor Red
  exit 2
} elseif ($WARNS -gt 0) {
  Write-Host "STATUS: WARN ($WARNS warnings)" -ForegroundColor Yellow
  exit 1
} else {
  Write-Host "STATUS: PASS (all sections clean)" -ForegroundColor Green
  exit 0
}
