#!/bin/bash
# gg-main.sh: SDK Product Manager Main Duties Automation
# Purpose: Proactively monitor system health, spot friction, file suggests
# Pattern: WkAutoQuant gg_check.py + wkappbot-sdk-daily-system-heal-checklist
# Run: bash scripts/gg-main.sh

set -e

echo "=== gg-main: SDK Product Manager Health Check ==="
echo "[$(date)]"
echo ""

# ============================================================================
# SECTION E: Eye Process Status
# ============================================================================
echo "==[ E ] EYE STATUS =="
CORE_COUNT=$(powershell -NoProfile -NonInteractive -Command "@(Get-Process wkappbot-core -ErrorAction SilentlyContinue).Count" 2>/dev/null || echo "?")
echo "wkappbot-core processes: $CORE_COUNT"
[ "$CORE_COUNT" -gt 3 ] && echo "  [WARN] $CORE_COUNT > 3 processes -- possible zombie accumulation"
echo ""

# ============================================================================
# SECTION Z: Zombie Process Count
# ============================================================================
echo "==[ Z ] ZOMBIE PROCESS MONITORING =="
CHROME_COUNT=$(powershell -NoProfile -NonInteractive -Command "@(Get-Process chrome -ErrorAction SilentlyContinue).Count" 2>/dev/null || echo "?")
echo "Chrome processes: $CHROME_COUNT"
[ "$CHROME_COUNT" -gt 5 ] && echo "  [CRITICAL] $CHROME_COUNT > 5 -- Chrome multiplication bug detected"
echo ""

# ============================================================================
# SECTION C: CI/Build Status
# ============================================================================
echo "==[ C ] CI BUILD STATUS =="
if command -v gh &>/dev/null; then
  gh run list --repo kiexpert/wkappbot-sdk --limit 5 2>/dev/null | head -6 || echo "  [ERROR] gh run list failed"
else
  echo "  [SKIP] gh not available"
fi
echo ""

# ============================================================================
# SECTION P: Suggest Backlog
# ============================================================================
echo "==[ P ] SUGGEST BACKLOG STATUS =="
if command -v wkappbot &>/dev/null; then
  wkappbot suggest list 2>/dev/null | grep "긴급\|중요" | head -5 || echo "  [ERROR] suggest list failed"
else
  echo "  [SKIP] wkappbot not available"
fi
echo ""

# ============================================================================
# SECTION D: CDP Anomaly Red Flags
# ============================================================================
echo "==[ D ] CDP ANOMALY DETECTION =="
if [ "$CHROME_COUNT" -gt 5 ]; then
  echo "  [CRITICAL] Chrome multiplication: $CHROME_COUNT processes"
  echo "    Root cause: Core FindRunningChromePortAny guard (needs deploy verify)"
  echo "    Action: Escalate to Core team"
fi
echo ""

# ============================================================================
# SECTION A: Auto-Remediation Actions
# ============================================================================
echo "==[ A ] REMEDIATION SUMMARY =="
ISSUES=0

[ "$CORE_COUNT" -gt 3 ] && echo "  ❌ Eye lag detected ($CORE_COUNT processes)" && ((ISSUES++))
[ "$CHROME_COUNT" -gt 5 ] && echo "  ❌ Chrome multiplication ($CHROME_COUNT processes)" && ((ISSUES++))

if [ $ISSUES -eq 0 ]; then
  echo "  ✓ System healthy (no critical anomalies)"
else
  echo "  ⚠ $ISSUES issue(s) detected -- manual remediation required"
fi
echo ""

# ============================================================================
# SECTION R: Release Readiness
# ============================================================================
echo "==[ R ] RELEASE CHECKLIST =="
echo "  [ ] Version bump (check VERSIONING.md current-version)"
echo "  [ ] CHANGELOG updated (What's New section)"
echo "  [ ] README.md v.X.Y references current version"
echo "  [ ] SECURITY.md supported-versions table current"
echo "  [ ] GitHub Release created (gh release list)"
echo ""

# ============================================================================
# Exit Status
# ============================================================================
echo "=== gg-main COMPLETE ==="
if [ $ISSUES -eq 0 ]; then
  echo "Status: ✓ GREEN (ready for operations)"
  exit 0
else
  echo "Status: ⚠ AMBER (issues detected, see above)"
  exit 1
fi
