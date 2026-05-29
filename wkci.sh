#!/usr/bin/env bash
# wkci.sh -- CI health check with skill refs on warning (wk*-safe)
# Usage: ./wkci.sh [limit] [--details]
LIMIT=${1:-10}; REPO="kiexpert/wkappbot-sdk"; DETAILS=0
[[ "$*" == *--details* ]] && DETAILS=1
echo "=== CI Health Check: $REPO ==="
RUNS=$(gh run list --repo "$REPO" --limit "$LIMIT" --json name,conclusion,createdAt,databaseId 2>/dev/null)
echo "$RUNS" | python3 -c "
import json,sys
runs=json.load(sys.stdin); fail=0
for r in runs:
    c=r.get(\"conclusion\",\"pending\")
    icon=\"OK\" if c==\"success\" else (\"--\" if c is None else \"FAIL\")
    print(\"  [\"+icon+\"] \"+r.get(\"createdAt\",\"\")[:16]+\" \"+r.get(\"name\",\"\")+\" (\"+str(c)+\")\")
    if c not in [\"success\",\"skipped\",\"cancelled\",None]:
        fail+=1
        print(\"       WARN: gh run view \"+str(r.get(\"databaseId\"))+\" --log-failed\")
        print(\"       REF:  wkappbot skill read sdk-gg-main-automation\")
        print(\"       REF:  wkappbot skill read wkcdp-mon\")
print()
print(\"RESULT: \"+str(fail)+\" failure(s)\" if fail else \"RESULT: All green\")
sys.exit(2 if fail else 0)
"
if [ "$DETAILS" = "1" ]; then
  LATEST=$(echo "$RUNS" | python3 -c "import json,sys; runs=json.load(sys.stdin); print(runs[0].get(\"databaseId\",\"\")) if runs else print(\"\")")
  echo "=== Job/Step detail: run $LATEST ==="
  gh run view "$LATEST" --json jobs 2>/dev/null | python3 -c "
import json,sys
data=json.load(sys.stdin); warn=0
for j in data.get(\"jobs\",[]):
    jc=j.get(\"conclusion\",\"pending\")
    if jc not in [\"success\",\"skipped\"]:
        print(\"  JOB WARN [\"+str(jc)+\"]: \"+j.get(\"name\",\"\"))
        warn+=1
    for s in j.get(\"steps\",[]):
        sc=s.get(\"conclusion\")
        if sc and sc not in [\"success\",\"skipped\"]:
            print(\"    STEP WARN [\"+str(sc)+\"]: \"+j.get(\"name\",\"\")+\" > \"+s.get(\"name\",\"\"))
            warn+=1
print(\"Detail: \"+str(warn)+\" warning(s)\" if warn else \"Detail: All steps green\")
sys.exit(2 if warn else 0)
"
fi