#!/bin/bash
# wkappbot-sdk daily system heal — runs at midnight (00:00 KST)
# Output: markdown report; Claude reads and heals any ❌ items found.
set -euo pipefail

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
SCHED_OUT=$(wkappbot schedule list 2>/dev/null || true)
REC=$(echo "$SCHED_OUT" | grep -c "recurring" 2>/dev/null || echo 0)
if [ "$REC" -ge 4 ]; then
  echo "  ✅ Recurring schedules: ${REC}개 (11:00 prefetch/briefing, 18:00 prefetch/briefing)"
else
  echo "  ❌ Recurring schedules: ${REC}개 — 4개 필요 (11:00/11:10/18:00/18:10)"
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
" 2>/dev/null
echo ""

# ── R. RELEASE ───────────────────────────────────────────────────────
echo "## R. SDK Release"
gh release list --repo "$REPO" --limit 1 2>/dev/null \
  | awk '{printf "  Latest: %s (%s)\n", $1, $3}'
echo ""

# ── SUMMARY ──────────────────────────────────────────────────────────
echo "---"
if [ "$FAIL" -eq 0 ]; then
  echo "## ✅ All checks passed — system healthy"
else
  echo "## ❌ Issues found: ${FAIL}건 — heal actions required"
fi
