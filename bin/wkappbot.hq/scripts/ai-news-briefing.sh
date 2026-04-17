#!/bin/bash
# AI News Briefing — HTTP-based HN search, direct output (no browser dependency).
# Scheduled: 11:00 / 18:00 daily (lunch & dinner briefing)
# Phase 1: raw HN results → Slack + speak (no AI filter — all CDP-based)
# Phase 2 (future): add API-based GPT/Claude filter when API key available

WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot.exe"
TIMESTAMP=$(date +"%Y-%m-%d %H:%M")

echo "[$TIMESTAMP] AI News Briefing starting"

# Step 1: Fetch from Hacker News Algolia API
RAW=$(mktemp)
QUERIES=(
  "AI+automation+agent"
  "Claude+computer+use+MCP"
  "Windows+UI+automation"
  "LLM+desktop+agent"
)
for q in "${QUERIES[@]}"; do
  $WKAPPBOT web fetch "https://hn.algolia.com/api/v1/search_by_date?query=${q}&tags=story&hitsPerPage=3" --max-chars 4000 2>/dev/null >> "$RAW"
done

# Extract titles + URLs
ITEMS=$(mktemp)
grep -o '"title":"[^"]*"' "$RAW" | sed 's/"title":"//;s/"$//' > "${ITEMS}.t"
grep -o '"url":"[^"]*"' "$RAW" | sed 's/"url":"//;s/"$//' > "${ITEMS}.u"
grep -o '"objectID":"[^"]*"' "$RAW" | sed 's/"objectID":"//;s/"$//' > "${ITEMS}.id"

paste -d'|' "${ITEMS}.t" "${ITEMS}.u" "${ITEMS}.id" | while IFS='|' read t u id; do
  [ -z "$t" ] && continue
  [ -z "$u" ] && u="https://news.ycombinator.com/item?id=$id"
  echo "$t|$u"
done | sort -u | head -10 > "$ITEMS"

rm -f "${ITEMS}.t" "${ITEMS}.u" "${ITEMS}.id"
ITEM_COUNT=$(wc -l < "$ITEMS")
echo "[$TIMESTAMP] Found $ITEM_COUNT unique stories"

if [ "$ITEM_COUNT" -eq 0 ]; then
  $WKAPPBOT speak "AI news: no results" 2>/dev/null
  $WKAPPBOT slack send ":newspaper: AI News ($TIMESTAMP) — no results from HN" 2>/dev/null
  rm -f "$RAW" "$ITEMS"
  exit 0
fi

# Step 2: Register as suggests
SUGGEST_COUNT=0
while IFS='|' read title url; do
  $WKAPPBOT suggest "[AI-NEWS] $title ($url)" 2>/dev/null
  SUGGEST_COUNT=$((SUGGEST_COUNT+1))
done < "$ITEMS"

# Step 3: Build Slack message
SLACK_MSG=":newspaper: AI News Briefing ($TIMESTAMP)
${ITEM_COUNT} stories from Hacker News
"
while IFS='|' read title url; do
  SLACK_MSG+="
• $title
  $url"
done < "$ITEMS"

SLACK_MSG+="

_${SUGGEST_COUNT} items registered as [AI-NEWS] suggests_"

# Step 4: Announce
FIRST_TITLE=$(head -1 "$ITEMS" | cut -d'|' -f1)
$WKAPPBOT speak "AI news briefing. ${FIRST_TITLE}" 2>/dev/null
$WKAPPBOT slack send "$SLACK_MSG" 2>/dev/null

rm -f "$RAW" "$ITEMS"
echo "[$TIMESTAMP] AI News Briefing done ($SUGGEST_COUNT items)"
