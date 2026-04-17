#!/bin/bash
# AI News Briefing — searches recent AI news, filters for WKAppBot-relevant
# items, registers as suggests, announces via speak + slack.
# Scheduled: 11:00 / 18:00 daily (lunch & dinner briefing)

WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot.exe"
TIMESTAMP=$(date +"%Y-%m-%d %H:%M")
SEARCH_KEYWORDS=(
  "AI desktop automation 2026"
  "Windows accessibility API AI"
  "Claude computer use update"
  "UI automation agent framework"
  "LLM tool use MCP update"
)

echo "[$TIMESTAMP] AI News Briefing starting"

# Step 1: Search multiple keywords, collect raw results
RAW=$(mktemp)
for kw in "${SEARCH_KEYWORDS[@]}"; do
  $WKAPPBOT web search "$kw" --limit 3 2>/dev/null >> "$RAW"
  echo "---" >> "$RAW"
done

RESULT_COUNT=$(grep -c "http" "$RAW")
echo "[$TIMESTAMP] Found $RESULT_COUNT raw results across ${#SEARCH_KEYWORDS[@]} queries"

if [ "$RESULT_COUNT" -eq 0 ]; then
  $WKAPPBOT speak "AI 뉴스 검색 결과가 없습니다"
  rm -f "$RAW"
  exit 0
fi

# Step 2: AI filter — ask Gemini to pick WKAppBot-relevant items
FILTER_PROMPT="Below are recent AI news search results. Pick the top 3 most relevant to WKAppBot (a Windows UI automation framework using UIA/Win32/SendInput with MCP integration for AI agents). For each, output EXACTLY this format — nothing else:

TITLE: <headline>
URL: <url>
WHY: <one sentence why this matters for WKAppBot>
SUMMARY: <one sentence summary>

If nothing is relevant, output: NONE

Search results:
$(cat "$RAW")"

FILTERED=$(mktemp)
$WKAPPBOT ask gemini "$FILTER_PROMPT" 2>/dev/null > "$FILTERED"

# Step 3: Parse and register suggests
SUGGEST_COUNT=0
while IFS= read -r line; do
  if [[ "$line" == TITLE:* ]]; then
    TITLE="${line#TITLE: }"
  elif [[ "$line" == WHY:* ]]; then
    WHY="${line#WHY: }"
  elif [[ "$line" == SUMMARY:* ]]; then
    SUMMARY="${line#SUMMARY: }"
    if [ -n "$TITLE" ] && [ -n "$SUMMARY" ]; then
      $WKAPPBOT suggest "[AI-NEWS] $TITLE: $WHY" 2>/dev/null
      SUGGEST_COUNT=$((SUGGEST_COUNT+1))
    fi
    TITLE="" WHY="" SUMMARY=""
  fi
done < "$FILTERED"

echo "[$TIMESTAMP] Registered $SUGGEST_COUNT suggests"

# Step 4: Build briefing message
if [ "$SUGGEST_COUNT" -gt 0 ]; then
  BRIEFING=$(grep -E "^(TITLE|SUMMARY):" "$FILTERED" | head -6)
  FIRST_SUMMARY=$(grep "^SUMMARY:" "$FILTERED" | head -1 | sed 's/^SUMMARY: //')

  # Speak one-line summary (TTS)
  $WKAPPBOT speak "AI 뉴스 브리핑. ${FIRST_SUMMARY:-새로운 소식이 있습니다}" 2>/dev/null

  # Slack full briefing
  $WKAPPBOT slack send "$(cat <<EOF
:newspaper: AI News Briefing ($TIMESTAMP)
$SUGGEST_COUNT 건의 관련 뉴스 → suggest 에 등록됨

$(cat "$FILTERED" | grep -E "^(TITLE|URL|WHY|SUMMARY):" | head -12)

_wkappbot suggest list 로 확인_
EOF
)" 2>/dev/null
else
  $WKAPPBOT speak "AI 뉴스 브리핑. 오늘은 관련 소식이 없습니다" 2>/dev/null
  $WKAPPBOT slack send ":newspaper: AI News Briefing ($TIMESTAMP) — 관련 뉴스 없음" 2>/dev/null
fi

rm -f "$RAW" "$FILTERED"
echo "[$TIMESTAMP] AI News Briefing done ($SUGGEST_COUNT items)"
