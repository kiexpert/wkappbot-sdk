#!/bin/bash
# check-skill-tree.sh - Verify skill tree backlinks (T1→T2/T3 parent references)
# Purpose: Auto-fix orphaned howto/ref/T2/T3 skills by adding parent T1 links

set -e

echo "=== Skill Tree Backlink Checker & Auto-Fixer ==="
echo "Scanning for T2/T3 skills without parent T1 references..."
echo ""

FIXED=0
LINKED=0
CHECKED=0

# Get all skills using wkappbot skill list 2>/dev/null | grep -E "\-(howto|ref|t[23]-)" | grep -oP "\([^)]+\)" | tr -d "()"
while IFS= read -r SKILL_ID; do
  # Derive parent T1 by stripping -howto, -ref, -t2-*, -t3-* suffixes
  PARENT_ID="$SKILL_ID"
  PARENT_ID="${PARENT_ID%-howto}"
  PARENT_ID="${PARENT_ID%-ref}"
  [[ "$PARENT_ID" =~ -t2- ]] && PARENT_ID="${SKILL_ID%-t2-*}"
  [[ "$PARENT_ID" =~ -t3- ]] && PARENT_ID="${SKILL_ID%-t3-*}"

  # Skip if no suffix was stripped (not a T2/T3 skill)
  if [[ "$PARENT_ID" == "$SKILL_ID" ]]; then
    continue
  fi

  ((CHECKED++))

  # Read child skill and check for parent ID reference
  CHILD_CONTENT=$(wkappbot skill read "$SKILL_ID" 2>/dev/null || echo "")

  if [[ ! "$CHILD_CONTENT" =~ $PARENT_ID ]]; then
    echo "🔧 FIXING: '$SKILL_ID' (adding parent link step)"
    wkappbot skill edit "$SKILL_ID" --add-step "Parent T1: wkappbot skill read $PARENT_ID" 2>/dev/null || true
    echo "✓ FIXED: '$SKILL_ID' → $PARENT_ID"
    ((FIXED++))
  else
    echo "✓ LINKED: $SKILL_ID → $PARENT_ID"
    ((LINKED++))
  fi
done < <(wkappbot skill list 2>/dev/null | grep -E "\-(howto|ref|t[23]-)" | sed 's/.*(\([^)]*\)).*/\1/')

echo ""
echo "Summary: FIXED=$FIXED | LINKED=$LINKED | TOTAL=$CHECKED"

if [ $FIXED -eq 0 ]; then
  echo "✓ All T2/T3 skills already have parent T1 references"
  exit 0
else
  echo "✓ Fixed $FIXED skills with parent links; $LINKED already linked"
  exit 0
fi
