# gg-main.ps1: SDK Product Manager Main Duties Automation
# Purpose: Proactively monitor system health, spot friction, file suggests
# Run: powershell -File scripts/gg-main.ps1

$ErrorActionPreference = "SilentlyContinue"

Write-Host "=== gg-main: SDK Product Manager Health Check ===" -ForegroundColor Green
Write-Host "[$(Get-Date)]"
Write-Host ""

$ISSUES = 0

# ============================================================================
# SECTION E: Eye Process Status
# ============================================================================
Write-Host "==[ E ] EYE STATUS ==" -ForegroundColor Cyan
$CORE_COUNT = @(Get-Process wkappbot-core -ErrorAction SilentlyContinue).Count
Write-Host "wkappbot-core processes: $CORE_COUNT"
if ($CORE_COUNT -gt 3) {
  Write-Host "  [WARN] $CORE_COUNT > 3 processes -- possible zombie accumulation" -ForegroundColor Yellow
  $ISSUES++
}
Write-Host ""

# ============================================================================
# SECTION Z: Chrome Count (Chrome Multiplication Detection)
# ============================================================================
Write-Host "==[ Z ] ZOMBIE PROCESS MONITORING ==" -ForegroundColor Cyan
$CHROME_COUNT = @(Get-Process chrome -ErrorAction SilentlyContinue).Count
Write-Host "Chrome processes: $CHROME_COUNT"
if ($CHROME_COUNT -gt 5) {
  Write-Host "  [CRITICAL] $CHROME_COUNT > 5 -- Chrome multiplication bug detected" -ForegroundColor Red
  $ISSUES++
}
Write-Host ""

# ============================================================================
# SECTION C: CI/Build Status
# ============================================================================
Write-Host "==[ C ] CI BUILD STATUS ==" -ForegroundColor Cyan
$GH_AVAILABLE = $null -ne (Get-Command gh -ErrorAction SilentlyContinue)
if ($GH_AVAILABLE) {
  try {
    gh run list --repo kiexpert/wkappbot-sdk --limit 5 2>$null | Select-Object -First 6
  } catch {
    Write-Host "  [ERROR] gh run list failed" -ForegroundColor Red
  }
} else {
  Write-Host "  [SKIP] gh not available" -ForegroundColor Gray
}
Write-Host ""

# ============================================================================
# SECTION P: Suggest Backlog
# ============================================================================
Write-Host "==[ P ] SUGGEST BACKLOG STATUS ==" -ForegroundColor Cyan
$WK_AVAILABLE = $null -ne (Get-Command wkappbot -ErrorAction SilentlyContinue)
if ($WK_AVAILABLE) {
  try {
    $suggests = wkappbot suggest list 2>$null | Select-String "긴급|중요" | Select-Object -First 5
    if ($suggests) {
      $suggests | ForEach-Object { Write-Host $_ }
    } else {
      Write-Host "  [OK] No urgent/important suggests" -ForegroundColor Green
    }
  } catch {
    Write-Host "  [ERROR] suggest list failed" -ForegroundColor Red
  }
} else {
  Write-Host "  [SKIP] wkappbot not available" -ForegroundColor Gray
}
Write-Host ""

# ============================================================================
# SECTION D: CDP Anomaly Red Flags
# ============================================================================
Write-Host "==[ D ] CDP ANOMALY DETECTION ==" -ForegroundColor Cyan
if ($CHROME_COUNT -gt 5) {
  Write-Host "  [CRITICAL] Chrome multiplication: $CHROME_COUNT processes" -ForegroundColor Red
  Write-Host "    Root cause: Core FindRunningChromePortAny guard (needs deploy verify)"
  Write-Host "    Action: Escalate to Core team"
}
Write-Host ""

# ============================================================================
# SECTION A: Auto-Remediation Actions
# ============================================================================
Write-Host "==[ A ] REMEDIATION SUMMARY ==" -ForegroundColor Cyan
if ($CORE_COUNT -gt 3) {
  Write-Host "  ❌ Eye lag detected ($CORE_COUNT processes)" -ForegroundColor Yellow
}
if ($CHROME_COUNT -gt 5) {
  Write-Host "  ❌ Chrome multiplication ($CHROME_COUNT processes)" -ForegroundColor Red
}

if ($ISSUES -eq 0) {
  Write-Host "  ✓ System healthy (no critical anomalies)" -ForegroundColor Green
} else {
  Write-Host "  ⚠ $ISSUES issue(s) detected -- manual remediation required" -ForegroundColor Yellow
}
Write-Host ""

# ============================================================================
# SECTION R: Release Checklist
# ============================================================================
Write-Host "==[ R ] RELEASE CHECKLIST ==" -ForegroundColor Cyan
Write-Host "  [ ] Version bump (check VERSIONING.md current-version)"
Write-Host "  [ ] CHANGELOG updated (What's New section)"
Write-Host "  [ ] README.md v.X.Y references current version"
Write-Host "  [ ] SECURITY.md supported-versions table current"
Write-Host "  [ ] GitHub Release created (gh release list)"
Write-Host ""

# ============================================================================
# Exit Status
# ============================================================================
Write-Host "=== gg-main COMPLETE ===" -ForegroundColor Green
if ($ISSUES -eq 0) {
  Write-Host "Status: ✓ GREEN (ready for operations)" -ForegroundColor Green
  exit 0
} else {
  Write-Host "Status: ⚠ AMBER (issues detected, see above)" -ForegroundColor Yellow
  exit 1
}
