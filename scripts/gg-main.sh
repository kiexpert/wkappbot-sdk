#!/bin/bash
# gg-main.sh: SDK Product Manager Comprehensive Health Check
# Purpose: Detect ALL known bad conditions, report each with the related skill ID.
# Sections: E Eye  Z Chrome/Zombie  B CDP/Ask  P Suggest  C CI  D CDP-anomaly  V Version  R Release  G CLAUDE.md  H Skill-audit  A Summary
# Exit: 0 = green (no issues), 1 = amber (WARN), 2 = critical (CRITICAL)
# Run: bash scripts/gg-main.sh

# DO NOT use set -e -- we want every check to run even if earlier ones fail.

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$REPO_ROOT"

WARN=0
CRIT=0
declare -a FINDINGS

note_warn() {
  WARN=$((WARN+1))
  FINDINGS+=("WARN  $1")
  echo "  [WARN] $1"
  [ -n "$2" ] && echo "    Skill: wkappbot skill read $2"
  [ -n "$3" ] && echo "    Action: $3"
}

note_crit() {
  CRIT=$((CRIT+1))
  FINDINGS+=("CRIT  $1")
  echo "  [CRITICAL] $1"
  [ -n "$2" ] && echo "    Skill: wkappbot skill read $2"
  [ -n "$3" ] && echo "    Action: $3"
}

echo "=== gg-main: SDK Product Manager Comprehensive Health Check ==="
echo "[$(date)]  CWD=$REPO_ROOT"
echo ""

# ============================================================================
# SECTION E: Eye Process Status
# ============================================================================
echo "==[ E ] EYE (wkappbot-core) STATUS =="
CORE_COUNT=$(powershell -NoProfile -NonInteractive -Command "@(Get-Process wkappbot-core -ErrorAction SilentlyContinue).Count" 2>/dev/null | tr -d '\r' || echo "?")
echo "wkappbot-core processes: $CORE_COUNT"
if [[ "$CORE_COUNT" =~ ^[0-9]+$ ]]; then
  if [ "$CORE_COUNT" -gt 8 ]; then
    note_crit "Eye process count $CORE_COUNT > 8 -- severe zombie accumulation" \
      "wkappbot-taskkill-usage" \
      "wkappbot taskkill /IM wkappbot-core.exe --dry-run  (then --force zombies)"
  elif [ "$CORE_COUNT" -gt 3 ]; then
    note_warn "Eye process count $CORE_COUNT > 3 -- possible zombie accumulation" \
      "wkappbot-taskkill-usage" \
      "wkappbot taskkill /IM wkappbot-core.exe --dry-run"
  fi
fi
echo ""

# ============================================================================
# SECTION Z: Chrome Process Status + zombie watchdog
# ============================================================================
echo "==[ Z ] CHROME / ZOMBIE STATUS =="
CHROME_COUNT=$(powershell -NoProfile -NonInteractive -Command "@(Get-Process chrome -ErrorAction SilentlyContinue).Count" 2>/dev/null | tr -d '\r' || echo "?")
echo "Chrome processes: $CHROME_COUNT"
if [[ "$CHROME_COUNT" =~ ^[0-9]+$ ]]; then
  if [ "$CHROME_COUNT" -gt 20 ]; then
    note_crit "Chrome multiplication: $CHROME_COUNT > 20 processes" \
      "sdk-gg-main-automation" \
      "wkcdp-mon.sh -KillForeign  (then check Core FindRunningChromePortAny deploy)"
  elif [ "$CHROME_COUNT" -gt 5 ]; then
    note_warn "Chrome multiplication: $CHROME_COUNT > 5 processes" \
      "standard-chrome-window" \
      "wkcdp-mon.sh -KillForeign"
  fi
fi

if [ -f "./wkzombie.ps1" ]; then
  if ! powershell -NoProfile -NonInteractive -File "./wkzombie.ps1" -DryRun -MaxAge 999999 >/dev/null 2>&1; then
    note_warn "wkzombie -DryRun returned non-zero -- watchdog may be impaired" \
      "sdk-launcher-maintenance" \
      "powershell -File wkzombie.ps1 -DryRun -MaxAge 60  (inspect)"
  fi
fi
echo ""

# ============================================================================
# SECTION B: CDP / Ask Quality
# ============================================================================
echo "==[ B ] CDP / ASK QUALITY =="
CDP_OUT=$(timeout 15 wkcdp-mon.sh 2>&1 || echo "WKCDP-TIMEOUT")
if echo "$CDP_OUT" | grep -q "WKCDP-TIMEOUT"; then
  note_warn "wkcdp-mon.sh timed out (>15s)" "wktool-pattern" "Check Eye IPC latency"
else
  while IFS= read -r line; do
    [[ "$line" =~ ^[[:space:]]*$ ]] && continue
    [[ "$line" =~ ^PORT ]] && continue
    [[ "$line" =~ ^--- ]] && continue
    [[ "$line" =~ ^\[ ]] && continue
    [[ "$line" =~ ^[[:space:]]+\> ]] && continue
    [[ "$line" =~ session\(s\) ]] && continue
    if [[ "$line" =~ ^[[:space:]]*([0-9]{4,5})[[:space:]] ]]; then
      port="${BASH_REMATCH[1]}"
      lat=$(echo "$line" | grep -oE '[0-9]+/[0-9]+/[0-9]+' | head -1)
      if [ -n "$lat" ]; then
        avg=$(echo "$lat" | cut -d/ -f1)
        if [[ "$avg" =~ ^[0-9]+$ ]] && [ "$avg" -gt 1000 ]; then
          note_crit "CDP port $port: LAT avg ${avg}ms > 1000ms (1-second rule violated)" \
            "wktool-pattern" \
            "wkcdp-mon.sh -KillForeign  +  investigate cdp open path"
        fi
      fi
      if echo "$line" | grep -qi "DEAD"; then
        note_warn "CDP port $port: LAT=DEAD" "cdp-command-guide" "Kill stale session and retry"
      fi
      mem=$(echo "$line" | awk '{print $4}')
      if [[ "$mem" =~ ^[0-9]+$ ]]; then
        if [ "$mem" -gt 2048 ]; then
          note_crit "CDP port $port: memory ${mem}MB > 2GB -- runaway session" \
            "sdk-gg-main-automation" "Kill session, restart with cdp open"
        elif [ "$mem" -gt 1024 ]; then
          note_warn "CDP port $port: memory ${mem}MB > 1GB" "sdk-gg-main-automation" "Monitor; consider restart"
        fi
      fi
    fi
  done <<< "$CDP_OUT"
  echo "$CDP_OUT" | tail -3
fi

WKASK_SH="$(command -v wkask.sh 2>/dev/null || echo D:/GitHub/WKAppBot/bin/wkask.sh)"
if [ -f "$WKASK_SH" ]; then
  if grep -qE 'Bypass[^\n]*\\n[^\n]*-File' "$WKASK_SH" 2>/dev/null; then
    note_crit "wkask.sh contains literal \\n between Bypass and -File (exec broken)" \
      "wktool-pattern" \
      "Edit wkask.sh: collapse to single-line exec"
  fi
fi
echo ""

# ============================================================================
# SECTION P: Suggest Backlog (ALL channels)
# ============================================================================
echo "==[ P ] SUGGEST BACKLOG (--all channels) =="
SUGGEST_OUT=$(timeout 25 wkappbot suggest list --all 2>/dev/null || echo "SUGGEST-TIMEOUT")
if echo "$SUGGEST_OUT" | grep -q "SUGGEST-TIMEOUT"; then
  note_warn "wkappbot suggest list --all timed out (>25s)" "suggest-workflow" "Restart Eye, retry"
else
  URGENT=$(echo "$SUGGEST_OUT" | grep -oE '긴급 [0-9]+' | head -1 | grep -oE '[0-9]+')
  IMPORTANT=$(echo "$SUGGEST_OUT" | grep -oE '중요 [0-9]+' | head -1 | grep -oE '[0-9]+')
  [ -z "$URGENT" ] && URGENT=0
  [ -z "$IMPORTANT" ] && IMPORTANT=0
  echo "  Urgent: $URGENT    Important: $IMPORTANT"

  if [[ "$URGENT" =~ ^[0-9]+$ ]]; then
    if [ "$URGENT" -gt 15 ]; then
      note_crit "Urgent suggests $URGENT > 15 across all channels" "suggest-workflow" "Spawn Opus to triage backlog"
    elif [ "$URGENT" -gt 5 ]; then
      note_warn "Urgent suggests $URGENT > 5 across all channels" "suggest-workflow" "Triage top 5"
    fi
  fi

  OCE_COUNT=$(echo "$SUGGEST_OUT" | grep -ci "OperationCanceledException")
  [ -z "$OCE_COUNT" ] && OCE_COUNT=0
  if [ "$OCE_COUNT" -gt 10 ]; then
    note_crit "OperationCanceledException cluster: $OCE_COUNT entries (ask timeout epidemic)" \
      "cdp-command-guide" "Investigate Chrome multiplication root cause + ask path"
  elif [ "$OCE_COUNT" -gt 3 ]; then
    note_warn "OperationCanceledException cluster: $OCE_COUNT entries" \
      "suggest-workflow" "Group + investigate ask pipeline"
  fi

  FOCUS_COUNT=$(echo "$SUGGEST_OUT" | grep -ciE "FOCUS-STEAL|FocusSteal")
  [ -z "$FOCUS_COUNT" ] && FOCUS_COUNT=0
  if [ "$FOCUS_COUNT" -gt 2 ]; then
    note_warn "FOCUS-STEAL cluster: $FOCUS_COUNT entries" \
      "suggest-workflow" "Group + escalate to Core (FocusStealSentinel)"
  fi

  CHROMECAP_COUNT=$(echo "$SUGGEST_OUT" | grep -ciE "CHROME:CAP|multiplication")
  [ -z "$CHROMECAP_COUNT" ] && CHROMECAP_COUNT=0
  if [ "$CHROMECAP_COUNT" -gt 1 ]; then
    note_warn "CHROME:CAP / multiplication suggests: $CHROMECAP_COUNT entries" \
      "suggest-workflow" "Verify Core FindRunningChromePortAny deploy"
  fi

  echo "  Top urgent:"
  echo "$SUGGEST_OUT" | grep -E '^\s*\[[ 0-9]+\]' | head -5 | sed 's/^/    /'
fi
echo ""

# ============================================================================
# SECTION C: CI / Build Status
# ============================================================================
echo "==[ C ] CI BUILD STATUS =="
if command -v gh >/dev/null 2>&1; then
  CI_OUT=$(gh run list --repo kiexpert/wkappbot-sdk --limit 10 2>/dev/null)
  if [ -n "$CI_OUT" ]; then
    echo "$CI_OUT" | head -6
    FAIL_LINE=$(echo "$CI_OUT" | grep -E "failure|cancelled|startup_failure" | head -1)
    if [ -n "$FAIL_LINE" ]; then
      RUN_ID=$(echo "$FAIL_LINE" | awk '{for(i=1;i<=NF;i++) if($i ~ /^[0-9]{10,}$/) print $i}' | head -1)
      WF_NAME=$(echo "$FAIL_LINE" | awk -F'\t' '{print $3}')
      note_warn "CI failure detected: $WF_NAME (run $RUN_ID)" \
        "wkappbot-build-verify-workflow" \
        "gh run view $RUN_ID --repo kiexpert/wkappbot-sdk --log-failed"
    fi
  else
    note_warn "gh run list returned empty" "wkappbot-build-verify-workflow" "Check gh auth"
  fi
else
  echo "  [SKIP] gh not available"
fi
echo ""

# ============================================================================
# SECTION D: CDP Anomaly Cross-Ref
# ============================================================================
echo "==[ D ] CDP ANOMALY CROSS-REF =="
if [[ "$CHROME_COUNT" =~ ^[0-9]+$ ]] && [ "$CHROME_COUNT" -gt 5 ]; then
  echo "  Chrome multiplication active ($CHROME_COUNT) -- see Z + P sections"
fi
if [ -n "$CDP_OUT" ] && ! echo "$CDP_OUT" | grep -q "WKCDP-TIMEOUT"; then
  if echo "$CDP_OUT" | grep -qE 'd[0-9]{3,}'; then
    note_warn "CDP DRIFT detected (Chrome placement off TGT-POS)" \
      "standard-chrome-window" \
      "wkcdp-mon.sh -KillIdle  +  inspect MyCdpContext placement validation"
  fi
fi
echo ""

# ============================================================================
# SECTION V: Version Consistency
# ============================================================================
echo "==[ V ] VERSION CONSISTENCY =="
VER_VERSIONING=$(grep -iE '(Current[[:space:]-]*version|^##.*v[0-9])' VERSIONING.md 2>/dev/null | grep -oE 'v?[0-9]+\.[0-9]+\.[0-9]+(-[a-z]+)?' | head -1)
VER_CHANGELOG=$(grep -oE 'v?[0-9]+\.[0-9]+\.[0-9]+(-[a-z]+)?' CHANGELOG.md 2>/dev/null | head -1)
VER_README=$(grep -oE 'v?[0-9]+\.[0-9]+\.[0-9]+(-[a-z]+)?' README.md 2>/dev/null | head -1)
VER_SECURITY=$(grep -oE 'v?[0-9]+\.[0-9]+(\.[0-9]+)?(-[a-z]+)?' SECURITY.md 2>/dev/null | head -1)
echo "  VERSIONING.md: $VER_VERSIONING"
echo "  CHANGELOG.md:  $VER_CHANGELOG"
echo "  README.md:     $VER_README"
echo "  SECURITY.md:   $VER_SECURITY"
short() { echo "$1" | grep -oE '[0-9]+\.[0-9]+' | head -1; }
S_VER=$(short "$VER_VERSIONING")
S_CHG=$(short "$VER_CHANGELOG")
S_RDM=$(short "$VER_README")
S_SEC=$(short "$VER_SECURITY")
MISMATCH=""
[ -n "$S_VER" ] && [ -n "$S_CHG" ] && [ "$S_VER" != "$S_CHG" ] && MISMATCH="$MISMATCH CHANGELOG($S_CHG)"
[ -n "$S_VER" ] && [ -n "$S_RDM" ] && [ "$S_VER" != "$S_RDM" ] && MISMATCH="$MISMATCH README($S_RDM)"
[ -n "$S_VER" ] && [ -n "$S_SEC" ] && [ "$S_VER" != "$S_SEC" ] && MISMATCH="$MISMATCH SECURITY($S_SEC)"
if [ -n "$MISMATCH" ]; then
  note_warn "Version drift vs VERSIONING.md($S_VER):$MISMATCH" \
    "sdk-launcher-maintenance" \
    "Bump mismatched files to $S_VER in one commit"
fi
echo ""

# ============================================================================
# SECTION R: GitHub Release Readiness
# ============================================================================
echo "==[ R ] RELEASE STATUS =="
if command -v gh >/dev/null 2>&1 && [ -n "$VER_VERSIONING" ]; then
  TAG="$VER_VERSIONING"
  [[ "$TAG" =~ ^v ]] || TAG="v$TAG"
  if gh release view "$TAG" --repo kiexpert/wkappbot-sdk >/dev/null 2>&1; then
    echo "  GitHub release $TAG: present"
  else
    note_warn "GitHub release $TAG missing for VERSIONING.md current version" \
      "sdk-launcher-maintenance" \
      "gh release create $TAG --title \"WKAppBot $TAG\" --notes \"(CHANGELOG top section)\""
  fi
else
  echo "  [SKIP] gh or VERSIONING.md unavailable"
fi
echo ""

# ============================================================================
# SECTION G: CLAUDE.md Health
# ============================================================================
echo "==[ G ] CLAUDE.md HEALTH =="
if [ -f CLAUDE.md ]; then
  CMD_LINES=$(wc -l < CLAUDE.md | tr -d ' ')
  echo "  CLAUDE.md lines: $CMD_LINES"
  if [ "$CMD_LINES" -gt 600 ]; then
    note_warn "CLAUDE.md $CMD_LINES > 600 lines (token-hostile)" \
      "skill-heal-nightly" "Compress Pending block / split rules into skills"
  fi
  CMD_DONE=$(grep -cE '^- \[x\]' CLAUDE.md)
  [ -z "$CMD_DONE" ] && CMD_DONE=0
  echo "  Pending [x] items: $CMD_DONE"
  if [ "$CMD_DONE" -gt 15 ]; then
    note_warn "CLAUDE.md Pending [x] count $CMD_DONE > 15 (needs compress)" \
      "skill-heal-nightly" "Compress to one-line DONE summary"
  fi
fi
echo ""

# ============================================================================
# SECTION H: Skill Audit
# ============================================================================
echo "==[ H ] SKILL AUDIT =="
SKILL_AUDIT=$(timeout 30 wkappbot skill audit 2>/dev/null | tail -20 || echo "SKILL-AUDIT-TIMEOUT")
if echo "$SKILL_AUDIT" | grep -q "SKILL-AUDIT-TIMEOUT"; then
  note_warn "wkappbot skill audit timed out" "skill-heal-nightly" "Restart Eye + retry"
else
  BROKEN=$(echo "$SKILL_AUDIT" | grep -ciE '\bbroken\b|\bmissing\b')
  [ -z "$BROKEN" ] && BROKEN=0
  if [ "$BROKEN" -gt 0 ]; then
    note_warn "Skill audit reports $BROKEN broken/missing entries" \
      "skill-heal-nightly" "Run nightly heal pass: STEP 2 HEALING"
  fi
  echo "$SKILL_AUDIT" | tail -3
fi
echo ""

# ============================================================================
# SECTION A: Summary
# ============================================================================
echo "==[ A ] SUMMARY =="
echo "  Findings: $CRIT critical, $WARN warn"
if [ "$CRIT" -eq 0 ] && [ "$WARN" -eq 0 ]; then
  echo "  GREEN -- no anomalies"
elif [ "$CRIT" -eq 0 ]; then
  echo "  AMBER -- $WARN warning(s)"
  for f in "${FINDINGS[@]}"; do echo "    - $f"; done
else
  echo "  CRITICAL -- $CRIT critical, $WARN warn"
  for f in "${FINDINGS[@]}"; do echo "    - $f"; done
fi
echo ""
echo "=== gg-main COMPLETE ==="

if [ "$CRIT" -gt 0 ]; then
  exit 2
elif [ "$WARN" -gt 0 ]; then
  exit 1
else
  exit 0
fi
