#!/usr/bin/env bash
# ci-health-check.sh -- GitHub Actions CI health check with skill references on warning
# Usage: bash scripts/ci-health-check.sh [--limit N]
# Exit: 0=Green, 1=Warning, 2=Critical

LIMIT=${1:-10}
REPO="kiexpert/wkappbot-sdk"
RED="\033[0;31m"; YELLOW="\033[0;33m"; GREEN="\033[0;32m"; RESET="\033[0m"
WARN_COUNT=0; FAIL_COUNT=0

echo "=== CI Health Check: $REPO ==="
runs=$(gh run list --repo "$REPO" --limit "$LIMIT" --json name,status,conclusion,createdAt,databaseId 2>/dev/null)
if [ -z "$runs" ]; then echo "ERROR: gh run list failed"; exit 2; fi

echo "$runs" | python3 -c "
import json, sys
runs = json.load(sys.stdin)
warn=0; fail=0
for r in runs:
    status = r.get(\"conclusion\", r.get(\"status\", \"unknown\"))
    icon = \"✅\" if status == \"success\" else (\"⚠️ \" if status in [\"skipped\",\"cancelled\"] else \"❌\")
    print(f\"  {icon} [{r.get(\"createdAt\",\"\")[:16]}] {r.get(\"name\",\"\")} ({status})\")
    if status not in [\"success\", \"skipped\", \"cancelled\", None]:
        fail += 1
        print(f\"     -> gh run view {r.get(\"databaseId\",\"\")} --log-failed\")
        print(f\"     -> wkappbot skill read sdk-gg-main-automation\")
        print(f\"     -> wkappbot skill read wkcdp-mon\")
print()
if fail > 0:
    print(f\"RESULT: ❌ {fail} failure(s) -- see commands above\")
    sys.exit(2)
else:
    print(\"RESULT: ✅ All green\")
    sys.exit(0)
"
EXIT_CODE=$?
exit $EXIT_CODE