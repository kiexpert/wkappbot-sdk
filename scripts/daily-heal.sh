#!/bin/bash
# wkappbot-sdk daily system heal — runs at midnight (00:00 KST)
# Output: markdown report; Claude reads and heals any ❌ items found.
set -uo pipefail

REPO="kiexpert/wkappbot-sdk"
CORE_REPO="kiexpert/WKAppBot"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SDK_DIR="$(dirname "$SCRIPT_DIR")"

echo "# 🔧 wkappbot-sdk Daily Heal — $(date '+%Y-%m-%d %H:%M KST')"
echo ""
FAIL=0

# ── E. EYE ──────────────────────────────────────────────────────────
echo "## E. Eye & Sessions"
EYE=$(wkappbot eye tick 2>/dev/null || true)
echo "$EYE" | grep -E "ctx=|세션:|sessions|ctx%" | head -5 || true
CTX=$(echo "$EYE" | grep -oE 'ctx=[0-9]+' | grep -oE '[0-9]+' | head -1 || true)
if [ "${CTX:-0}" -gt 80 ]; then
  echo "  ❌ ctx=${CTX}% — newchat 필요"
  FAIL=$((FAIL+1))
else
  echo "  ✅ ctx=${CTX:-?}%"
fi
echo ""

# ── C. CI HEALTH ─────────────────────────────────────────────────────
echo "## C. CI Status"
echo "### SDK (kiexpert/wkappbot-sdk)"
gh run list --repo "$REPO" --limit 20 --json status,name,conclusion,workflowName 2>/dev/null \
  | python3 -c "
import sys,json
runs=json.load(sys.stdin)
seen=set()
for r in runs:
    wf=r.get('workflowName',r['name'])
    # skip ghost runs: path-style names like '.github/workflows/foo.yml' have no real jobs
    if wf.startswith('.github/'): continue
    if wf in seen: continue
    seen.add(wf)
    c=r['conclusion'] or r['status']
    icon='✅' if c=='success' else ('⏳' if c in ('in_progress','queued') else '❌')
    print(f'  {icon} {wf[:55]:55s} {c}')
" 2>/dev/null
SDK_FAIL=$(gh run list --repo "$REPO" --limit 20 --json conclusion,workflowName 2>/dev/null \
  | python3 -c "
import sys,json
runs=json.load(sys.stdin)
seen=set()
fail=0
for r in runs:
    wf=r.get('workflowName','')
    if wf.startswith('.github/') or wf in seen: continue
    seen.add(wf)
    if r['conclusion'] not in ('success',None,'','skipped'): fail+=1
print(fail)
" 2>/dev/null || echo 0)
[ "${SDK_FAIL}" -gt 0 ] && FAIL=$((FAIL+SDK_FAIL))

echo ""
echo "### Core (kiexpert/WKAppBot)"
gh run list --repo "$CORE_REPO" --limit 4 --json status,workflowName,conclusion 2>/dev/null \
  | python3 -c "
import sys,json
runs=json.load(sys.stdin)
seen=set()
for r in runs:
    wf=r.get('workflowName',r.get('name','?'))
    if wf in seen: continue
    seen.add(wf)
    c=r['conclusion'] or r['status']
    icon='✅' if c=='success' else ('⏳' if c in ('in_progress','queued') else '❌')
    print(f'  {icon} {wf[:55]:55s} {c}')
" 2>/dev/null
echo ""

# ── S. SCHEDULE ───────────────────────────────────────────────────────
echo "## S. News Briefing Schedule"
SCHED_TASKS=("WKAppBot_Briefing_1100" "WKAppBot_AI_News_1110" "WKAppBot_Briefing_1800" "WKAppBot_AI_News_1810")
REC=0
for task in "${SCHED_TASKS[@]}"; do
  cmd //c "schtasks /query /tn ${task} /fo LIST" 2>&1 | grep -q "${task}" && REC=$((REC+1))
done
if [ "$REC" -ge 4 ]; then
  echo "  ✅ Recurring schedules: ${REC}개/4개 (schtasks)"
else
  echo "  ❌ Recurring schedules: ${REC}개/4개 — WKAppBot_Briefing_1100/1800 + WKAppBot_AI_News_1110/1810 필요"
  FAIL=$((FAIL+1))
fi
echo ""

# ── G. GIT STATUS ────────────────────────────────────────────────────
echo "## G. Git Status"
cd "$SDK_DIR"
DIRTY=$(git status --short 2>/dev/null | grep -v "^??" || true)
UNTRACKED=$(git status --short 2>/dev/null | grep "^??" | wc -l || echo 0)
if [ -z "$DIRTY" ]; then
  echo "  ✅ Working tree clean (untracked: ${UNTRACKED})"
else
  echo "  ❌ Uncommitted changes:"
  echo "$DIRTY" | head -5 | sed 's/^/    /'
  FAIL=$((FAIL+1))
fi
echo "  Latest: $(git log --oneline -1 2>/dev/null)"
echo ""

# ── L. LICENSE SYSTEM ────────────────────────────────────────────────
echo "## L. License System"
LIC_COUNT=$(gh api repos/kiexpert/wkappbot-sdk/contents/.github/licenses 2>/dev/null \
  | python3 -c "import sys,json; files=json.load(sys.stdin); print(len([f for f in files if f['name'].endswith('.json')]))" 2>/dev/null || echo "?")
echo "  Active licenses: ${LIC_COUNT}명"
GHA_ENFORCER=$(gh run list --repo "$REPO" --workflow license-enforcer.yml --limit 1 --json conclusion 2>/dev/null \
  | python3 -c "import sys,json; runs=json.load(sys.stdin); print(runs[0]['conclusion'] if runs else 'no runs')" 2>/dev/null || echo "?")
echo "  Enforcer last run: ${GHA_ENFORCER}"
[ "$GHA_ENFORCER" = "failure" ] && { echo "  ❌ Enforcer 실패"; FAIL=$((FAIL+1)); } || echo "  ✅ Enforcer OK"
echo ""

# ── P. SUGGEST BACKLOG ───────────────────────────────────────────────
echo "## P. Suggest Backlog"
wkappbot suggest list 2>/dev/null | python3 -c "
import sys
lines=sys.stdin.readlines()
confirm=0; coresolve=0
for l in lines:
    if '컨펌 대기' in l or 'CHECK PASSED' in l:
        import re; m=re.search(r'(\d+)건', l)
        if m: confirm=int(m.group(1))
    if 'PENDING CO' in l or 'CO-RESOLVE' in l:
        import re; m=re.search(r'(\d+)건', l)
        if m: coresolve=int(m.group(1))
icon_c='✅' if confirm==0 else '⚠️'
icon_r='✅' if coresolve==0 else '⚠️'
print(f'  {icon_c} 컨펌 대기: {confirm}건')
print(f'  {icon_r} Pending co-resolve: {coresolve}건')
" 2>/dev/null || true
echo ""

# ── R. RELEASE ───────────────────────────────────────────────────────
echo "## R. SDK Release"
gh release list --repo "$REPO" --limit 1 2>/dev/null \
  | awk '{printf "  Latest: %s (%s)\n", $1, $3}' || true
echo ""

# ── X. CROSS-REPO AUDIT ──────────────────────────────────────────────
echo "## X. Cross-Repo System Audit"

# X1. All repos CI status (wkappbot-sdk, WKAppBot, WkAutoQuant, bitwisdomk)
echo "### X1. All Repos CI (latest per workflow)"
for REPO_X in "kiexpert/wkappbot-sdk" "kiexpert/WKAppBot" "kiexpert/WkAutoQuant" "bitwisdomk/bitwisdomk"; do
  LATEST=$(gh run list --repo "$REPO_X" --limit 1 --json workflowName,conclusion 2>/dev/null \
    | python3 -c "import sys,json; r=json.load(sys.stdin); print(r[0]['conclusion'] if r else 'no runs')" 2>/dev/null || echo "?")
  ICON="✅"; [ "$LATEST" = "failure" ] && { ICON="❌"; FAIL=$((FAIL+1)); }
  printf "  %s %-40s %s\n" "$ICON" "$REPO_X" "$LATEST"
done
echo ""

# X2. GitHub Pages reachability (SDK landing page)
echo "### X2. GitHub Pages"
HTTP=$(curl -s -o /dev/null -w "%{http_code}" "https://kiexpert.github.io/wkappbot-sdk/" 2>/dev/null || echo "?")
if [ "$HTTP" = "200" ]; then
  echo "  ✅ kiexpert.github.io/wkappbot-sdk/ → ${HTTP}"
else
  echo "  ❌ kiexpert.github.io/wkappbot-sdk/ → ${HTTP}"
  FAIL=$((FAIL+1))
fi
# wkappbot-sdk Pages
HTTP2=$(curl -s -o /dev/null -w "%{http_code}" "https://kiexpert.github.io/wkappbot-sdk/" 2>/dev/null || echo "?")
if [ "$HTTP2" = "200" ]; then
  echo "  ✅ kiexpert.github.io/wkappbot-sdk/ → ${HTTP2}"
else
  echo "  ❌ kiexpert.github.io/wkappbot-sdk/ → ${HTTP2}"
  FAIL=$((FAIL+1))
fi
echo ""

# X3. README media files exist in wkappbot-sdk
echo "### X3. README Media Assets (wkappbot-sdk)"
SDK_DIR_CHECK="$SDK_DIR"
for ASSET in "docs/wkappbot-sudo-license-demo-preview.gif" "docs/wkappbot-sudo-license-demo.mp4" \
             "docs/screenshots/wkappbot-sudo-demo-pin-entry.jpg" \
             "docs/screenshots/wkappbot-sudo-demo-all-ready.jpg" \
             "docs/screenshots/wkappbot-sudo-demo-portfolio.jpg"; do
  if [ -f "$SDK_DIR_CHECK/$ASSET" ]; then
    echo "  ✅ $ASSET"
  else
    echo "  ❌ $ASSET MISSING"
    FAIL=$((FAIL+1))
  fi
done
echo ""

# X4. Releases — latest tag matches expected pattern
echo "### X4. Releases"
SDK_REL=$(gh release list --repo "kiexpert/wkappbot-sdk" --limit 1 2>/dev/null | awk '{print $1}' || echo "?")
CORE_REL=$(gh release list --repo "kiexpert/WKAppBot" --limit 1 2>/dev/null | awk '{print $1}' || echo "?")
echo "  wkappbot-sdk latest : $SDK_REL"
echo "  WKAppBot latest     : $CORE_REL"
echo ""

# X5. PayPal secrets present
echo "### X5. Secrets"
PP_ID=$(gh secret list --repo "kiexpert/wkappbot-sdk" 2>/dev/null | grep -c "PAYPAL_CLIENT_ID" || echo 0)
PP_SEC=$(gh secret list --repo "kiexpert/wkappbot-sdk" 2>/dev/null | grep -c "PAYPAL_CLIENT_SECRET" || echo 0)
[ "$PP_ID" -ge 1 ] && echo "  ✅ PAYPAL_CLIENT_ID" || { echo "  ❌ PAYPAL_CLIENT_ID missing"; FAIL=$((FAIL+1)); }
[ "$PP_SEC" -ge 1 ] && echo "  ✅ PAYPAL_CLIENT_SECRET" || { echo "  ❌ PAYPAL_CLIENT_SECRET missing"; FAIL=$((FAIL+1)); }
echo ""

# X6. All repos git status — uncommitted changes check
echo "### X6. Git Status — All Repos"
for REPO_DIR in "D:/GitHub/wkappbot-sdk" "D:/GitHub/WKAppBot" "D:/GitHub/bitwisdomk" "D:/GitHub/WkAutoQuant"; do
  RNAME=$(basename "$REPO_DIR")
  DIRTY=$(git -C "$REPO_DIR" status --short 2>/dev/null | grep -v "^??" | wc -l || echo "?")
  UNTRACK=$(git -C "$REPO_DIR" status --short 2>/dev/null | grep "^??" | wc -l || echo "?")
  BRANCH=$(git -C "$REPO_DIR" branch --show-current 2>/dev/null || echo "?")
  AHEAD=$(git -C "$REPO_DIR" rev-list @{u}..HEAD --count 2>/dev/null || echo "0")
  if [ "${DIRTY:-0}" -gt 0 ]; then
    echo "  ❌ $RNAME — ${DIRTY} modified, ${UNTRACK} untracked (branch: $BRANCH)"
    FAIL=$((FAIL+1))
  elif [ "${AHEAD:-0}" -gt 0 ]; then
    echo "  ⚠️  $RNAME — ${AHEAD} commits not pushed (branch: $BRANCH)"
  else
    echo "  ✅ $RNAME — clean (branch: $BRANCH, untracked: ${UNTRACK})"
  fi
done
echo ""

# X7. All repos CI — ALL workflows last 10 runs, flag any persistent failures
echo "### X7. CI Deep Check — All Repos"
for REPO_X in "kiexpert/wkappbot-sdk" "kiexpert/WKAppBot" "kiexpert/WkAutoQuant"; do
  echo "  #### $REPO_X"
  gh run list --repo "$REPO_X" --limit 20 --json workflowName,conclusion 2>/dev/null \
    | python3 -c "
import sys,json,collections
runs=json.load(sys.stdin)
wf_results=collections.defaultdict(list)
for r in runs:
    wf=r.get('workflowName','?')
    if wf.startswith('.github/'): continue
    wf_results[wf].append(r['conclusion'])
for wf,results in sorted(wf_results.items()):
    last=results[0]; fails=sum(1 for r in results if r=='failure')
    icon='✅' if last=='success' else ('⏳' if last in ('in_progress','queued') else '❌')
    streak=' ⚠️ RECURRING FAILURE' if fails>=2 else ''
    print(f'    {icon} {wf[:50]:50s} last={last} fail={fails}{streak}')
" 2>/dev/null || echo "    (no data)"
done
echo ""

# X8. README broken tag check — video tags, wrong URLs
echo "### X8. README Integrity Check"
for README in "D:/GitHub/wkappbot-sdk/README.md" "D:/GitHub/WKAppBot/README.md" "D:/GitHub/bitwisdomk/docs/index.html"; do
  RNAME=$(basename "$(dirname "$README")")/$(basename "$README")
  # Check for <video> tags (don't render on GitHub)
  VID=$(grep -c "<video" "$README" 2>/dev/null || echo 0)
  # Check for dead image references (local paths that don't exist)
  BAD_IMGS=$(grep -oE '!\[[^]]*\]\(([^)]+)\)' "$README" 2>/dev/null \
    | grep -oE '\(([^)]+)\)' | tr -d '()' \
    | grep -v '^http' \
    | while read -r p; do
        FULL="$(dirname "$README")/$p"
        [ ! -f "$FULL" ] && echo "$p"
      done | wc -l 2>/dev/null || echo 0)
  STATUS="✅"
  MSG=""
  [ "${VID:-0}" -gt 0 ] && { STATUS="❌"; MSG="$MSG <video> tag found (won't render on GitHub);"; FAIL=$((FAIL+1)); }
  [ "${BAD_IMGS:-0}" -gt 0 ] && { STATUS="⚠️"; MSG="$MSG ${BAD_IMGS} broken image ref(s);"; }
  echo "  $STATUS $RNAME${MSG:+ — $MSG}"
done
echo ""

# X9. GitHub Actions — sponsor-license.yml push trigger guard
echo "### X9. Workflow Config Sanity"
if grep -q "^  push:" "$SDK_DIR/.github/workflows/sponsor-license.yml" 2>/dev/null; then
  echo "  ❌ sponsor-license.yml has 'push:' trigger — causes false failures on every push"
  FAIL=$((FAIL+1))
else
  echo "  ✅ sponsor-license.yml trigger clean"
fi
# Check for workflows with if: != 'push' guard mismatches
echo ""

# X10. WkAutoQuant daemon alive
echo "### X10. WkAutoQuant Daemon"
QUANT_PID=$(cat "D:/GitHub/WkAutoQuant/.wkappbot/daemon.pid" 2>/dev/null || echo "")
if [ -n "$QUANT_PID" ] && kill -0 "$QUANT_PID" 2>/dev/null; then
  echo "  ✅ WkAutoQuant daemon alive (PID=$QUANT_PID)"
else
  echo "  ⚠️  WkAutoQuant daemon not running (expected if market closed)"
fi
echo ""

# ── SUMMARY ──────────────────────────────────────────────────────────
echo "---"
if [ "$FAIL" -eq 0 ]; then
  echo "## ✅ All checks passed — system healthy"
else
  echo "## ❌ Issues found: ${FAIL}건 — heal actions required"
fi
