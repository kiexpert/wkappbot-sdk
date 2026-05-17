#!/bin/bash
# AI News Briefing — HN + YouTube → Gemini filter → [AI-NEWS] suggests → Slack + speak
# Fast path: if GitHub Actions pre-collected file is fresh (<13h), skip fetch+filter
# Manual run: bash ai-news-briefing.sh [SINCE_HOURS]

set -euo pipefail
export PYTHONUTF8=1 PYTHONIOENCODING=utf-8

SDK_ROOT="D:/GitHub/wkappbot-sdk"
cd "$SDK_ROOT"   # ensure suggest filings go to DG-wkappbot-sdk channel

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
# Load local secrets (.env not committed) -- fallback to parent GitHub root
HQ_ENV="${SCRIPT_DIR}/../.env"
[ -f "$HQ_ENV" ] || HQ_ENV="${SCRIPT_DIR}/../../../.env"
[ -f "$HQ_ENV" ] || HQ_ENV="${SCRIPT_DIR}/../../../../.env"
[ -f "$HQ_ENV" ] && export $(grep -v '^#' "$HQ_ENV" | xargs) 2>/dev/null || true
[ -n "${GH_PAT:-}" ] && export GH_TOKEN="$GH_PAT"

WKAPPBOT="wkappbot"
DATA_DIR="${SDK_ROOT}/data"
CHANNELS_CFG="${SDK_ROOT}/config/ai-news-channels.jsonl"
LOCAL_FILE="${DATA_DIR}/ai-news-local.jsonl"
GHA_FILE="${DATA_DIR}/ai-news-latest.jsonl"
LATEST_FILE="${LOCAL_FILE}"
[ ! -f "${LOCAL_FILE}" ] && LATEST_FILE="${GHA_FILE}"
TIMESTAMP=$(date +"%Y-%m-%d %H:%M")
SINCE_HOURS="${1:-26}"

echo "[$TIMESTAMP] AI News Briefing starting (last ${SINCE_HOURS}h)"

FILTERED=$(mktemp)
FILTERED_COUNT=0
HN_COUNT=0; YT_COUNT=0; TOTAL=0

# ── Fast path: GitHub Actions pre-collected file ──────────────────────────────
USE_PREBUILT=0
if [ -f "$LATEST_FILE" ]; then
  FILE_AGE_MIN=$(( ( $(date +%s) - $(date -r "$LATEST_FILE" +%s 2>/dev/null || echo 0) ) / 60 ))
  if [ "$FILE_AGE_MIN" -lt 780 ]; then
    echo "[$TIMESTAMP] Using pre-collected file (${FILE_AGE_MIN}min old)"
    python.exe - "$LATEST_FILE" "$FILTERED" <<'PYEOF'
import sys, json, re
lines = open(sys.argv[1], encoding='utf-8', errors='replace').readlines()
out = []
for line in lines:
    line = line.strip()
    if not line:
        continue
    try:
        d = json.loads(line)
        score = d.get("score", 0)
        title = d.get("title", "")
        direction = d.get("direction", "")
        if title and direction:
            out.append(f"SCORE: {score}\nTITLE: {title}\nURL: {d.get('url','')}\nSOURCE: {d.get('source','?')}\nDIRECTION: {direction}\n")
    except Exception:
        pass
open(sys.argv[2], 'w', encoding='utf-8').write('\n'.join(out))
PYEOF
    FILTERED_COUNT=$(grep -c '^SCORE: [0-9]' "$FILTERED" 2>/dev/null || true)
    FILTERED_COUNT="${FILTERED_COUNT:-0}"
    TOTAL=$FILTERED_COUNT
    USE_PREBUILT=1
    echo "[$TIMESTAMP] Pre-collected: $FILTERED_COUNT items"
  fi
fi

# ── Full fetch path: HN + YouTube + Gemini filter ────────────────────────────
if [ "$USE_PREBUILT" = "0" ]; then
  ITEMS=$(mktemp)
  RAW_HN=$(mktemp)

  # Section 1: Hacker News
  HN_QUERIES=(
    "AI+desktop+automation+agent"
    "Claude+computer+use+MCP+tool"
    "Windows+UI+automation+accessibility"
    "LLM+agent+framework+tool+use"
    "AI+code+agent+computer+control"
  )
  for q in "${HN_QUERIES[@]}"; do
    curl -s "https://hn.algolia.com/api/v1/search_by_date?query=${q}&tags=story&hitsPerPage=3" >> "$RAW_HN" 2>/dev/null || true
  done
  python.exe - "$RAW_HN" "$ITEMS" <<'PYEOF'
import sys, re, json
content = open(sys.argv[1], encoding='utf-8', errors='replace').read()
titles = re.findall(r'"title":"((?:[^"\\]|\\.)*)"', content)
urls   = re.findall(r'"url":"((?:[^"\\]|\\.)*)"', content)
ids    = re.findall(r'"objectID":"(\d+)"', content)
seen, results = set(), []
for i, t in enumerate(titles):
    if not t or t in seen:
        continue
    seen.add(t)
    u = urls[i] if i < len(urls) else ""
    if not u and i < len(ids):
        u = f"https://news.ycombinator.com/item?id={ids[i]}"
    results.append(json.dumps({"title": t, "url": u, "source": "HN"}, ensure_ascii=False))
open(sys.argv[2], 'w', encoding='utf-8').write('\n'.join(results[:12]))
PYEOF
  HN_COUNT=$(grep -c '{' "$ITEMS" 2>/dev/null || true); HN_COUNT="${HN_COUNT:-0}"
  echo "[$TIMESTAMP] HN: $HN_COUNT stories"
  rm -f "$RAW_HN"

  # Section 2: YouTube RSS
  YT_ITEMS=$(mktemp)
  if [ -f "$CHANNELS_CFG" ]; then
    CUTOFF_TS=$(python.exe -c "
import datetime, sys
dt = datetime.datetime.utcnow() - datetime.timedelta(hours=int(sys.argv[1]))
print(dt.strftime('%Y-%m-%dT%H:%M:%S'))
" "$SINCE_HOURS" 2>/dev/null || echo "2000-01-01T00:00:00")

    while IFS= read -r line; do
      [[ -z "$line" || "$line" == \#* ]] && continue
      CH_ID=$(python.exe -c "import sys,json; d=json.loads(sys.argv[1]); print(d.get('id',''))" "$line" 2>/dev/null)
      CH_NAME=$(python.exe -c "import sys,json; d=json.loads(sys.argv[1]); print(d.get('name',''))" "$line" 2>/dev/null)
      [[ -z "$CH_ID" ]] && continue
      RSS_FILE=$(mktemp)
      curl -s "https://www.youtube.com/feeds/videos.xml?channel_id=${CH_ID}" > "$RSS_FILE" 2>/dev/null || true
      [[ ! -s "$RSS_FILE" ]] && { rm -f "$RSS_FILE"; continue; }
      python.exe - "$RSS_FILE" "$CH_NAME" "$CUTOFF_TS" "$YT_ITEMS" <<'PYEOF'
import sys, re, json
from datetime import datetime
rss = open(sys.argv[1], encoding='utf-8', errors='replace').read()
ch_name, cutoff_str, out_file = sys.argv[2], sys.argv[3], sys.argv[4]
try:
    cutoff = datetime.strptime(cutoff_str, "%Y-%m-%dT%H:%M:%S")
except Exception:
    cutoff = datetime(2000, 1, 1)
results = []
for e in re.split(r'<entry>', rss)[1:]:
    tm = re.search(r'<title>(.*?)</title>', e, re.DOTALL)
    um = re.search(r'<link rel="alternate" href="([^"]+)"', e)
    pm = re.search(r'<published>(.*?)</published>', e)
    if not tm:
        continue
    title = re.sub(r'<[^>]+>', '', tm.group(1)).strip()
    url   = um.group(1) if um else ""
    pub_str = pm.group(1)[:19] if pm else "2000-01-01T00:00:00"
    try:
        pub = datetime.strptime(pub_str, "%Y-%m-%dT%H:%M:%S")
    except Exception:
        pub = datetime(2000, 1, 1)
    if pub < cutoff:
        continue
    results.append(json.dumps({"title": title, "url": url, "source": f"YT:{ch_name}"}, ensure_ascii=False))
if results:
    open(out_file, 'a', encoding='utf-8').write('\n'.join(results) + '\n')
PYEOF
      rm -f "$RSS_FILE"
    done < "$CHANNELS_CFG"
  fi
  YT_COUNT=$(grep -c '{' "$YT_ITEMS" 2>/dev/null || true); YT_COUNT="${YT_COUNT:-0}"
  echo "[$TIMESTAMP] YouTube: $YT_COUNT recent videos"
  cat "$YT_ITEMS" >> "$ITEMS"
  rm -f "$YT_ITEMS"
  TOTAL=$(grep -c '{' "$ITEMS" 2>/dev/null || true); TOTAL="${TOTAL:-0}"
  echo "[$TIMESTAMP] Total: $TOTAL items (HN:$HN_COUNT + YT:$YT_COUNT)"

  if [ "$TOTAL" -eq 0 ]; then
    $WKAPPBOT speak "안녕하십니까. AI 뉴스 브리핑입니다. 오늘은 수집된 기사가 없습니다. 이상 AI 뉴스 브리핑이었습니다." 2>/dev/null || true
    $WKAPPBOT slack send ":newspaper: AI News ($TIMESTAMP) — no items" 2>/dev/null || true
    rm -f "$ITEMS" "$FILTERED"
    exit 0
  fi

  # Section 3: Gemini relevance filter via CDP
  STORY_FILE=$(mktemp)
  PROMPT_FILE=$(mktemp)
  python.exe - "$ITEMS" "$STORY_FILE" <<'PYEOF'
import sys, json
items = []
for line in open(sys.argv[1], encoding='utf-8', errors='replace'):
    line = line.strip()
    if not line:
        continue
    try:
        d = json.loads(line)
        items.append(f"[{d.get('source','?')}] {d.get('title','')}  {d.get('url','')}")
    except Exception:
        pass
open(sys.argv[2], 'w', encoding='utf-8').write(
    '\n'.join(f"{i+1}. {s}" for i, s in enumerate(items[:20]))
)
PYEOF

  cat > "$PROMPT_FILE" << PROMPT_EOF
You analyze AI/automation news for WKAppBot, a Windows desktop automation tool.
WKAppBot context: Windows UIA automation, Claude/GPT/Gemini agents, MCP protocol, CDP web automation, self-healing UI detection, skill system, accessibility API, Slack integration, scheduled tasks, a11y CLI.

Score each item 1-5 for WKAppBot relevance (5=highly actionable improvement idea).
Output ONLY items scored >= 2, one block per item, EXACTLY this format:

SCORE: N
TITLE: <title>
URL: <url>
SOURCE: <source label>
DIRECTION: <one sentence: specific WKAppBot improvement this news suggests>

Items:
$(cat "$STORY_FILE")

Output nothing if no items score >= 2. No extra text outside the format blocks.
PROMPT_EOF

  FILTERED_RAW=$(mktemp)
  GEMINI_PROMPT=$(cat "$PROMPT_FILE")
  claude --model claude-sonnet-4-6 -p "$GEMINI_PROMPT" > "$FILTERED_RAW" 2>/dev/null || \
    $WKAPPBOT ask claude "$GEMINI_PROMPT" --timeout 60 > "$FILTERED_RAW" 2>/dev/null || true
  python.exe - "$FILTERED_RAW" "$FILTERED" <<'PYEOF'
import sys, re
lines = open(sys.argv[1], encoding='utf-8', errors='replace').readlines()
keep = []
in_block = False
for l in lines:
    s = l.rstrip()
    if re.match(r'^SCORE: [0-9]', s):
        in_block = True; keep.append(s)
    elif in_block and re.match(r'^(TITLE|URL|SOURCE|DIRECTION):', s):
        keep.append(s)
    elif in_block and s == '':
        keep.append(''); in_block = False
open(sys.argv[2], 'w', encoding='utf-8').write('\n'.join(keep))
PYEOF
  rm -f "$STORY_FILE" "$PROMPT_FILE" "$FILTERED_RAW" "$ITEMS"
  FILTERED_COUNT=$(grep -c '^SCORE: [0-9]' "$FILTERED" 2>/dev/null || true)
  FILTERED_COUNT="${FILTERED_COUNT:-0}"
  echo "[$TIMESTAMP] Gemini filtered: $FILTERED_COUNT relevant items"
fi

# ── Section 4: Register suggests ─────────────────────────────────────────────
SUGGEST_COUNT=0
SLACK_BODY=""

register_block() {
  local block="$1"
  local SCORE TITLE URL SRC DIR
  SCORE=$(echo "$block" | grep '^SCORE:'     | awk '{print $2}' | head -1)
  TITLE=$(echo "$block" | grep '^TITLE:'     | sed 's|^TITLE: ||'     | head -1)
  URL=$(echo   "$block" | grep '^URL:'       | awk '{print $2}'       | head -1)
  SRC=$(echo   "$block" | grep '^SOURCE:'    | sed 's|^SOURCE: ||'    | head -1)
  DIR=$(echo   "$block" | grep '^DIRECTION:' | sed 's|^DIRECTION: ||' | head -1)
  [[ -z "$TITLE" || -z "$DIR" ]] && return

  # 1. 서제스트 등록 (중복 방지: 동일 타이틀이 이미 있으면 스킵)
  local SUGGEST_TEXT="[AI-NEWS] [$SRC] $TITLE: $DIR"
  [[ -n "$URL" ]] && SUGGEST_TEXT+=" -- $URL"
  local KEY_60="${SUGGEST_TEXT:0:60}"
  local ALREADY=$(python.exe -c "
import json
try:
    items = [json.loads(l) for l in open('D:/GitHub/wkappbot-sdk/bin/wkappbot.hq/suggestions.jsonl', encoding='utf-8') if l.strip()]
    key = '''${KEY_60}'''.lower()
    print('yes' if any(key in i.get('text','').lower() for i in items) else 'no')
except: print('no')
" 2>/dev/null || echo "no")
  if [ "$ALREADY" = "no" ]; then
    # Suggest filed under DG-wkappbot-sdk (cd to SDK_ROOT at script start ensures this)
    $WKAPPBOT suggest "$SUGGEST_TEXT" 2>/dev/null || true
    SUGGEST_COUNT=$((SUGGEST_COUNT+1))
  fi

  # 2. score >= 4 → 자동 스킬화 (경쟁사/기술명 제목 노출)
  if [ "${SCORE:-0}" -ge 4 ]; then
    local SKILL_TITLE="[$SRC] $TITLE — AI computer-use pattern reference"
    local SKILL_DESC="$DIR Source: $URL (score=${SCORE}, auto-generated from ai-news-briefing ${TIMESTAMP})"
    $WKAPPBOT skill contribute \
      --title "$SKILL_TITLE" \
      --app "wkappbot-workflow" \
      --desc "$SKILL_DESC" \
      --tags "ai-news,computer-use,auto-skill,$(echo "$SRC" | tr '[:upper:]' '[:lower:]' | tr ' ' '-')" \
      --affected-commands "a11y" \
      --steps \
        "SOURCE: $TITLE -- $URL" \
        "RELEVANCE (score=$SCORE): $DIR" \
        "ACTION: Review and implement if applicable. wkappbot suggest list | grep AI-NEWS for full backlog." \
      2>/dev/null || true
    echo "[$TIMESTAMP] Auto-skill: [$SRC] $TITLE"
  fi

  SLACK_BODY+="• [$SCORE] [$SRC] $TITLE
  ↳ $DIR
  $URL

"
}

if [ "$FILTERED_COUNT" -gt 0 ]; then
  CURRENT_BLOCK=""
  while IFS= read -r line <&3; do
    if [[ "$line" =~ ^SCORE: ]]; then
      [[ -n "$CURRENT_BLOCK" ]] && register_block "$CURRENT_BLOCK"
      CURRENT_BLOCK="$line"
    else
      CURRENT_BLOCK+=$'\n'"$line"
    fi
  done 3< "$FILTERED"
  [[ -n "$CURRENT_BLOCK" ]] && register_block "$CURRENT_BLOCK"
else
  # Fallback: register top 3 raw (from latest file if available)
  SRC_FILE="${LATEST_FILE}"
  [ -f "$SRC_FILE" ] || SRC_FILE="$FILTERED"
  python.exe - "$SRC_FILE" <<'PYEOF' | while IFS='|' read -r title url; do
import sys, json
for line in open(sys.argv[1], encoding='utf-8', errors='replace'):
    line = line.strip()
    if not line:
        continue
    try:
        d = json.loads(line)
        print(f"{d.get('title','')}|{d.get('url','')}")
    except Exception:
        pass
PYEOF
    $WKAPPBOT suggest "[AI-NEWS] $title -- $url" 2>/dev/null || true
    SUGGEST_COUNT=$((SUGGEST_COUNT+1))
    SLACK_BODY+="• $title  $url

"
  done
  echo "[$TIMESTAMP] Fallback: raw items registered"
fi

# ── Section 5: Announce ───────────────────────────────────────────────────────
SRC_LABEL="HN:${HN_COUNT}+YT:${YT_COUNT}"
[ "$USE_PREBUILT" = "1" ] && SRC_LABEL="GitHub Actions pre-collected"

SLACK_MSG=":newspaper: *AI News Briefing* ($TIMESTAMP)
${FILTERED_COUNT} actionable items (${SRC_LABEL} → ${TOTAL} total)

${SLACK_BODY}_${SUGGEST_COUNT} items queued → \`wkappbot suggest list\` ([AI-NEWS] tag)_"

SPEAK_TEXT="안녕하십니까. AI 뉴스 브리핑입니다. 오늘 수집된 기사 ${TOTAL}건 중 WKAppBot 개선에 활용 가능한 항목 ${SUGGEST_COUNT}건을 서제스트에 등록하였습니다. 이상 AI 뉴스 브리핑이었습니다."
$WKAPPBOT speak "$SPEAK_TEXT" 2>/dev/null || true
$WKAPPBOT slack send "$SLACK_MSG" 2>/dev/null || true

# ── GitHub Issue: NEWS section update ────────────────────────────────────────
ISSUE_PY="${SCRIPT_DIR}/daily-issue-section.py"
NEWS_TMP=$(mktemp)
python.exe - "$NEWS_TMP" "$TIMESTAMP" "$TOTAL" "$SRC_LABEL" "$FILTERED_COUNT" "$SUGGEST_COUNT" "$SLACK_BODY" <<'PYEOF'
import sys
path, ts, total, src, filtered, suggest, body = sys.argv[1], sys.argv[2], sys.argv[3], sys.argv[4], sys.argv[5], sys.argv[6], sys.argv[7] if len(sys.argv) > 7 else ""
content = f"""### {ts} 브리핑 결과

| 항목 | 값 |
|------|-----|
| 수집 | {total}건 ({src}) |
| Gemini 필터 | {filtered}건 |
| 서제스트 등록 | {suggest}건 |

{body.strip() if body.strip() else "_Gemini 관련 항목 없음 (기준 미달 또는 수집 없음)_"}
"""
open(path, 'w', encoding='utf-8').write(content)
PYEOF
ISSUE_NUM=$(python.exe "$ISSUE_PY" update NEWS "$NEWS_TMP" 2>&1 | tail -1) || true
[[ "$ISSUE_NUM" =~ ^[0-9]+$ ]] && echo "[$TIMESTAMP] GitHub Issue #${ISSUE_NUM} NEWS section updated" || echo "[$TIMESTAMP] WARN: GitHub Issue NEWS update skipped (${ISSUE_NUM})"
rm -f "$NEWS_TMP"

rm -f "$FILTERED"
echo "[$TIMESTAMP] AI News Briefing complete ($SUGGEST_COUNT suggests registered)"

# ── GitHub Issue: delta comment (news + actions) ──────────────────────────────
if [ -n "$ISSUE_PY" ] || true; then
  ISSUE_PY="${SCRIPT_DIR}/daily-issue-section.py"
  DELTA_TMP=$(mktemp)
  python.exe - "$DELTA_TMP" "$TIMESTAMP" "$TOTAL" "$SRC_LABEL" "$SUGGEST_COUNT" "$SLACK_BODY" <<'PYEOF'
import sys, datetime
path, ts, total, src, n_suggests, body = sys.argv[1], sys.argv[2], sys.argv[3], sys.argv[4], sys.argv[5], sys.argv[6] if len(sys.argv) > 6 else ""
lines = [f"**{ts} 뉴스 델타** — {total}건 수집 ({src}), {n_suggests}건 서제스트 등록"]
if body.strip():
    lines.append("")
    lines.append("주요 항목:")
    for l in body.strip().splitlines()[:12]:
        if l.strip():
            lines.append(l)
open(path, 'w', encoding='utf-8').write('\n'.join(lines))
PYEOF
  python.exe "$ISSUE_PY" comment "$DELTA_TMP" 2>/dev/null || true
  rm -f "$DELTA_TMP"
fi

# ── Section 6: System Healing Check (Sonnet CLI) ──────────────────────────────
echo "[$TIMESTAMP] System healing check starting..."

HQ_DIR="${SDK_ROOT}/bin/wkappbot.hq"
ERROR_LOG="${HQ_DIR}/logs/errors.jsonl"
SKILL_LOG="${HQ_DIR}/runtime/skill_invocations.jsonl"
SUGGEST_FILE="${HQ_DIR}/suggestions.jsonl"

# Collect metrics
PENDING_SUGGESTS=$(python.exe -c "
import json, sys
try:
    n = sum(1 for l in open(sys.argv[1], encoding='utf-8', errors='replace') if l.strip())
    print(n)
except: print(0)
" "$SUGGEST_FILE" 2>/dev/null || echo 0)

RECENT_ERRORS=$(python.exe -c "
import json, sys, datetime
cutoff = (datetime.datetime.utcnow() - datetime.timedelta(hours=24)).isoformat()
try:
    lines = [l for l in open(sys.argv[1], encoding='utf-8', errors='replace') if l.strip()]
    recent = [json.loads(l) for l in lines[-50:] if l.strip()]
    count = sum(1 for e in recent if e.get('ts','') > cutoff)
    sample = '; '.join(e.get('cmd','?') + ':' + e.get('err','?')[:60] for e in recent[-3:] if e.get('ts','') > cutoff)
    print(f'{count}|{sample}')
except: print('0|')
" "$ERROR_LOG" 2>/dev/null || echo "0|")

ERROR_COUNT=$(echo "$RECENT_ERRORS" | cut -d'|' -f1)
ERROR_SAMPLE=$(echo "$RECENT_ERRORS" | cut -d'|' -f2-)

SKILL_TODAY=$(python.exe -c "
import json, sys, datetime
today = datetime.date.today().isoformat()
try:
    lines = [l.strip() for l in open(sys.argv[1], encoding='utf-8', errors='replace') if l.strip()]
    count = sum(1 for l in lines if today in l)
    ids = list(dict.fromkeys(json.loads(l).get('id','') for l in lines if today in l))[-5:]
    print(f'{count}|{chr(44).join(ids)}')
except: print('0|')
" "$SKILL_LOG" 2>/dev/null || echo "0|")

SKILL_COUNT=$(echo "$SKILL_TODAY" | cut -d'|' -f1)
SKILL_IDS=$(echo "$SKILL_TODAY" | cut -d'|' -f2-)

EYE_STATUS=$($WKAPPBOT eye tick 2>&1 | grep -oE 'ctx=[^ ]+|step:[^ ]+|IDLE|BUSY' | head -3 | tr '\n' ' ' || echo "unknown")

# News freshness check (local prefetch preferred; GHA file as fallback)
NEWS_LOCAL="${SDK_ROOT}/data/ai-news-local.jsonl"
NEWS_GHA="${SDK_ROOT}/data/ai-news-latest.jsonl"
if [ -f "$NEWS_LOCAL" ]; then
  NEWS_LATEST="$NEWS_LOCAL"
  NEWS_SRC="local"
elif [ -f "$NEWS_GHA" ]; then
  NEWS_LATEST="$NEWS_GHA"
  NEWS_SRC="GHA"
else
  NEWS_LATEST=""
  NEWS_SRC="none"
fi
NEWS_AGE_MIN=9999
NEWS_ITEM_COUNT=0
if [ -n "$NEWS_LATEST" ]; then
  NEWS_AGE_MIN=$(( ( $(date +%s) - $(date -r "$NEWS_LATEST" +%s 2>/dev/null || echo 0) ) / 60 ))
  NEWS_ITEM_COUNT=$(grep -c '{' "$NEWS_LATEST" 2>/dev/null || echo 0)
fi
NEWS_STATUS="${NEWS_AGE_MIN}분 전 갱신 (${NEWS_ITEM_COUNT}건, src=${NEWS_SRC})"
[ "$NEWS_AGE_MIN" -gt 1440 ] && NEWS_STATUS="❌ 24h 이상 미갱신 (${NEWS_ITEM_COUNT}건, src=${NEWS_SRC})"
[ "$NEWS_AGE_MIN" -lt 120 ] && NEWS_STATUS="✅ 최신 (${NEWS_AGE_MIN}분 전, ${NEWS_ITEM_COUNT}건, src=${NEWS_SRC})"

# Schedule health
SCHEDULE_COUNT=$($WKAPPBOT schedule list 2>/dev/null | grep -c 'pending\|running' || echo 0)

# Recent source changes (git log last 24h, WKAppBot .cs/.sh/.py files only)
GIT_ROOT="${SDK_ROOT}"
RECENT_SRC=$(git -C "$GIT_ROOT" log --since="24 hours ago" --name-only --pretty=format: -- \
  "*.cs" "*.sh" "*.py" "*.json" 2>/dev/null | grep -E '\.(cs|sh|py|json)$' | sort -u | head -10 | tr '\n' ' ' || echo "none")
RECENT_SRC_COUNT=$(echo "$RECENT_SRC" | wc -w | tr -d ' ')

# Count oversized source files (>400 lines) among recent changes
OVERSIZED_FILES=$(git -C "$GIT_ROOT" log --since="24 hours ago" --name-only --pretty=format: -- "*.cs" 2>/dev/null | \
  grep '\.cs$' | sort -u | while read -r f; do
    fp="${GIT_ROOT}/${f}"
    [ -f "$fp" ] && wc -l < "$fp" 2>/dev/null || echo 0
  done | awk '$1>400{c++} END{print c+0}' 2>/dev/null || echo 0)

# Eye memory
EYE_MEM=$($WKAPPBOT eye tick 2>&1 | grep -oE 'mem=[0-9]+MB' | head -1 || echo "unknown")

HEAL_PROMPT="You are WKAppBot system health monitor. Analyze the following metrics and produce a Korean health report.

## System Metrics ($(date '+%Y-%m-%d %H:%M') KST)

| 항목 | 값 | 기준 |
|------|-----|------|
| Eye 데몬 상태 | ${EYE_STATUS:-unknown} | step:N/N 또는 IDLE |
| Eye 메모리 | ${EYE_MEM:-unknown} | < 400MB |
| Pending suggests | ${PENDING_SUGGESTS}건 | < 5건 |
| 오류 수 (24h) | ${ERROR_COUNT}건 | < 10건 |
| 오류 패턴 (top) | ${ERROR_SAMPLE:-없음} | 반복 패턴 없어야 함 |
| Skill 조회 (오늘) | ${SKILL_COUNT}회 (${SKILL_IDS:-없음}) | > 0 |
| 뉴스 프리페치 | ${NEWS_STATUS} | < 120분 |
| 로컬 스케줄 수 | ${SCHEDULE_COUNT}개 | >= 4 (10:00/11:10/17:00/18:10) |
| 최근 소스 변경 (24h) | ${RECENT_SRC_COUNT}개 파일: ${RECENT_SRC} | 코드 품질 확인 필요 |
| 400줄 초과 CS 파일 | ${OVERSIZED_FILES}개 | 0개 (파일 분리 필요) |
| 뉴스 소스 | src=${NEWS_SRC} | local 우선, GHA 폴백 |

## 로컬 vs GHA 경계

- **로컬 수행**: Eye 상태/메모리, 에러 로그, 서제스트 등록, skill 조회, 소스 검토, 뉴스 프리페치
- **GHA 수행**: CI 빌드, 배포, GHA 스케줄 워크플로 (ai-news-latest.jsonl 전용)
- **경계 점검**: ai-news-local.jsonl (로컬) vs ai-news-latest.jsonl (GHA) → 머지 충돌 없음

## 상세 체크리스트

각 항목을 ✅ OK / ⚠️ 주의 / ❌ 이상으로 평가하세요:

- [ ] Eye 데몬 응답 정상 (eye tick 성공, step 표시)
- [ ] Eye 메모리 400MB 미만
- [ ] Pending suggests 5건 미만 (백로그 관리 중)
- [ ] 24h 오류 10건 미만 (시스템 안정)
- [ ] 반복 오류 패턴 없음 (동일 오류 3회+ 반복 금지)
- [ ] 오늘 skill 조회 1회 이상 (skill 활용 중)
- [ ] AI 뉴스 프리페치 2시간 이내 갱신 (로컬 우선, src=local 확인)
- [ ] 로컬 스케줄 4개 이상 등록 (10:00/11:10/17:00/18:10)
- [ ] 최근 소스 변경 파일 검토 (24h 내 .cs/.sh/.py 변경 사항 이상 없음)
- [ ] 400줄 초과 CS 파일 없음 (있으면 파일 분리 서제스트 등록)

## 지시사항

1. 각 체크리스트 항목을 메트릭 기반으로 채우세요.
2. ⚠️/❌ 항목마다 구체적 조치 1줄 작성하세요.
3. 150자 이내 한국어 해요체.
4. 마지막 줄: 🟢 양호 / 🟡 주의 / 🔴 이상"

HEAL_RESULT=$(claude --model claude-sonnet-4-6 -p "$HEAL_PROMPT" 2>/dev/null || \
              $WKAPPBOT ask claude "$HEAL_PROMPT" 2>/dev/null || \
              echo "⚠️ Sonnet CLI unavailable — manual check required")

echo "[$TIMESTAMP] Heal result: $HEAL_RESULT"
$WKAPPBOT slack send ":wrench: *System Heal* ($TIMESTAMP)
$HEAL_RESULT" 2>/dev/null || true

# ── 자동 힐링 작업: ❌/⚠️ 항목 서제스트 자동 등록 ────────────────────────────
python.exe - "$TIMESTAMP" "$ERROR_COUNT" "$PENDING_SUGGESTS" "$SKILL_COUNT" "$NEWS_AGE_MIN" "$SCHEDULE_COUNT" "$SCRIPT_DIR" "$OVERSIZED_FILES" "$NEWS_SRC" <<'PYEOF'
import sys, subprocess, os

ts, err, suggests, skill_reads, news_age, sched, script_dir, oversized, news_src = sys.argv[1:]
err, suggests, skill_reads, news_age, sched, oversized = int(err), int(suggests), int(skill_reads), int(news_age), int(sched), int(oversized)
# wkappbot-core.exe is two levels up from scripts/
wkb = os.path.join(script_dir, "..", "..", "wkappbot-core.exe").replace("/", "\\")
if not os.path.exists(wkb):
    wkb = "wkappbot"

def suggest(text):
    env = os.environ.copy()
    env["PYTHONUTF8"] = "1"
    subprocess.run([wkb, "suggest", text], capture_output=True, timeout=15,
                   encoding="utf-8", env=env)

if err > 10:
    suggest(f"[HEAL-AUTO] 24h 오류 {err}건 초과 -- errors.jsonl 분석 후 반복 패턴 수정 필요 ({ts})")
if suggests > 5:
    suggest(f"[HEAL-AUTO] Pending suggests {suggests}건 -- suggest list 검토 후 우선순위 낮은 항목 dismiss ({ts})")
if skill_reads == 0:
    suggest(f"[HEAL-AUTO] 오늘 skill 조회 0회 -- 작업 전 wkappbot skill list 습관화 필요 ({ts})")
if news_age > 120:
    suggest(f"[HEAL-AUTO] AI 뉴스 프리페치 {news_age}분 미갱신 -- prefetch 스케줄(10:00/17:00) 정상 여부 확인 ({ts})")
if sched < 4:
    suggest(f"[HEAL-AUTO] 로컬 스케줄 {sched}개 (기준 4개 미달) -- 10:00/11:10/17:00/18:10 재등록 필요 ({ts})")
if oversized > 0:
    suggest(f"[HEAL-AUTO] 400줄 초과 CS 파일 {oversized}개 감지 -- 파일 분리 필요 (wkappbot ask codex --agent 'split oversized files') ({ts})")
if news_src == "none":
    suggest(f"[HEAL-AUTO] 뉴스 데이터 없음 -- ai-news-prefetch.sh 수동 실행 또는 스케줄 재등록 필요 ({ts})")
elif news_src == "GHA":
    suggest(f"[HEAL-AUTO] 뉴스 소스가 GHA 파일 사용 중 -- 로컬 프리페치 스케줄(10:00/17:00) 정상 여부 확인 ({ts})")
PYEOF

# ── GitHub Issue: HEAL section update ────────────────────────────────────────
HEAL_TMP=$(mktemp)
cat > "$HEAL_TMP" << EOF
### $TIMESTAMP 힐링 체크

| 항목 | 값 |
|------|-----|
| Eye 상태 | ${EYE_STATUS:-unknown} |
| Pending suggests | ${PENDING_SUGGESTS} |
| Errors (24h) | ${ERROR_COUNT} |
| Skill reads (today) | ${SKILL_COUNT} |

$HEAL_RESULT
EOF
HEAL_ISSUE=$(python.exe "$ISSUE_PY" update HEAL "$HEAL_TMP" 2>/dev/null || echo "")
[ -n "$HEAL_ISSUE" ] && echo "[$TIMESTAMP] GitHub Issue #${HEAL_ISSUE} HEAL section updated" || echo "[$TIMESTAMP] WARN: GitHub Issue HEAL update skipped"
rm -f "$HEAL_TMP"

# Post heal result as issue comment (조치사항 포함)
HEAL_CMT=$(mktemp)
cat > "$HEAL_CMT" <<HCEOF
**${TIMESTAMP} 시스템 힐링 조치**

${HEAL_RESULT}
HCEOF
python.exe "$ISSUE_PY" comment "$HEAL_CMT" 2>/dev/null || true
rm -f "$HEAL_CMT"

echo "[$TIMESTAMP] System healing check complete"

# ── Section 7: Source Change Summary ─────────────────────────────────────────
echo "[$TIMESTAMP] Source change summary starting..."
GIT_ROOT_ABS="${SDK_ROOT}"

SOURCE_SUMMARY=$(python.exe - "$GIT_ROOT_ABS" "$TIMESTAMP" <<'PYEOF'
import sys, subprocess, json, datetime, re

git_root, ts = sys.argv[1], sys.argv[2]

def git(*args):
    r = subprocess.run(["git", "-C", git_root] + list(args), capture_output=True, text=True,
                       encoding="utf-8", errors="replace", timeout=30)
    return r.stdout.strip()

today = datetime.date.today().isoformat()

# Get files changed today (24h)
today_files_raw = git("log", "--since=24 hours ago", "--name-only", "--pretty=format:",
                      "--", "*.cs", "*.sh", "*.py", "*.json", "*.yaml", "*.md")
today_files = list(dict.fromkeys(f for f in today_files_raw.splitlines() if f.strip()))

# If < 9, keep pulling from history (14d window) until >= 9 total
if len(today_files) < 9:
    for since in ["7 days ago", "14 days ago", "30 days ago"]:
        recent_raw = git("log", f"--since={since}", "--name-only", "--pretty=format:",
                         "--", "*.cs", "*.sh", "*.py", "*.json", "*.yaml", "*.md")
        for f in recent_raw.splitlines():
            f = f.strip()
            if f and f not in today_files:
                today_files.append(f)
        if len(today_files) >= 9:
            break

target_files = today_files  # summarize ALL collected, not capped

# Get commit summaries for each file (last 2 commits per file)
rows = []
for f in target_files:
    log = git("log", "--oneline", "-2", "--follow", "--", f)
    first_line = log.splitlines()[0] if log else "(no commit)"
    sha = first_line[:7]
    msg = first_line[8:70] if len(first_line) > 8 else ""
    short_f = f.split("/")[-1] if "/" in f else f
    rows.append(f"| `{short_f}` | {sha} | {msg} |")

table = "\n".join(rows) if rows else "| (변경 없음) | - | - |"

# Collect today commit messages for Sonnet summary prompt
today_commits = git("log", "--since=24 hours ago", "--oneline")
if not today_commits:
    today_commits = git("log", "--oneline", "-10")

print(f"""### {ts} 소스 변경 총평

| 파일 | 최근 커밋 | 요약 |
|------|-----------|------|
{table}

**오늘 커밋 ({today}):**
```
{today_commits[:1200]}
```
""")
PYEOF
)

# Sonnet 총평 생성
SOURCE_PROMPT="You are a WKAppBot code reviewer. Analyze the following daily source changes and write a concise Korean review (해요체, under 200 chars).

Focus on: what was fixed/added, any quality concerns (oversized files, bandaid code), patterns across changes.
Format: 2-3 bullet points max.

${SOURCE_SUMMARY}"

SOURCE_REVIEW=$(claude --model claude-sonnet-4-6 -p "$SOURCE_PROMPT" 2>/dev/null || echo "(Sonnet unavailable)")

SOURCE_TMP=$(mktemp)
cat > "$SOURCE_TMP" <<SRCEOF
${SOURCE_SUMMARY}
---
**Sonnet 총평:**
${SOURCE_REVIEW}
SRCEOF

SOURCE_ISSUE=$(python.exe "$ISSUE_PY" update SOURCE "$SOURCE_TMP" 2>/dev/null || echo "")
[ -n "$SOURCE_ISSUE" ] && echo "[$TIMESTAMP] GitHub Issue #${SOURCE_ISSUE} SOURCE section updated" || echo "[$TIMESTAMP] WARN: GitHub Issue SOURCE update skipped"
rm -f "$SOURCE_TMP"
$WKAPPBOT slack send ":file_folder: *Source Changes* ($TIMESTAMP)
$(echo "$SOURCE_SUMMARY" | head -15)
$SOURCE_REVIEW" 2>/dev/null || true
echo "[$TIMESTAMP] Source change summary complete"
