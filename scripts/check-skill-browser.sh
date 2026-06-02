#!/usr/bin/env bash
# check-skill-browser.sh: QA the live wkappbot skill browser via curl
# Usage: bash scripts/check-skill-browser.sh
# Exit 0 = all pass, Exit 1 = any fail

PAGE="https://kiexpert.github.io/wkappbot-sdk/skills/"
PASS=0; FAIL=0

check() { local name="$1" result="$2"
  if [ "$result" = "ok" ]; then echo "  [PASS] $name"; PASS=$((PASS+1))
  else echo "  [FAIL] $name"; FAIL=$((FAIL+1)); fi
}

echo "=== Skill Browser QA: $PAGE ==="

# 1. HTTP check
STATUS=$(curl -s -o /tmp/skill-page.html -w "%{http_code}" "$PAGE" 2>/dev/null)
check "HTTP 200 (got: $STATUS)" "$([ "$STATUS" = "200" ] && echo ok || echo fail)"

# 2. Skill cards present
CARDS=$(grep -o "skill-card\|skillCard\|class=\"card" /tmp/skill-page.html 2>/dev/null | wc -l | tr -d ' \n\r')
CARDS=${CARDS:-0}
check "Skill cards > 10 (found: $CARDS)" "$([ "${CARDS:-0}" -gt 10 ] 2>/dev/null && echo ok || echo fail)"

# 3. Hero text (Sonnet reflection)
grep -q "Sonnet\|amnesiac\|harness" /tmp/skill-page.html 2>/dev/null
check "Hero text (Sonnet/harness)" "$([ $? -eq 0 ] && echo ok || echo fail)"

# 4. Pro lock elements
grep -q "locked\|blur\|Unlock Pro" /tmp/skill-page.html 2>/dev/null
check "Pro lock elements" "$([ $? -eq 0 ] && echo ok || echo fail)"

# 5. Local docs fresh (within 25h)
if [ -f "docs/skills/index.html" ]; then
  MOD=$(stat -c %Y docs/skills/index.html 2>/dev/null || stat -f %m docs/skills/index.html 2>/dev/null || echo 0)
  NOW=$(date +%s)
  AGE=$(( (NOW - MOD) / 3600 ))
  check "docs/skills/index.html fresh (${AGE}h)" "$([ "$AGE" -lt 25 ] && echo ok || echo fail)"
else
  check "docs/skills/index.html exists" "fail"
fi

# 6. Main landing page  
STATUS2=$(curl -s -o /dev/null -w "%{http_code}" "https://kiexpert.github.io/wkappbot-sdk/" 2>/dev/null)
check "Main landing HTTP 200 (got: $STATUS2)" "$([ "$STATUS2" = "200" ] && echo ok || echo fail)"

echo ""
echo "=== Result: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ] && exit 0 || exit 1
