#!/usr/bin/env bash
# wkci.sh -- CI health check with skill refs on warning (wk*-safe)
# Usage: ./wkci.sh [limit]
LIMIT=${1:-10}; REPO="kiexpert/wkappbot-sdk"
echo "=== CI Health Check: $REPO ==="
gh run list --repo "$REPO" --limit "$LIMIT" --json name,conclusion,createdAt,databaseId 2>/dev/null | python3 -c "
import json,sys
runs=json.load(sys.stdin); fail=0
for r in runs:
    c=r.get('conclusion','pending')
    icon='OK' if c=='success' else ('--' if c is None else 'FAIL')
    print('  ['+icon+'] '+r.get('createdAt','')[:16]+' '+r.get('name','')+' ('+str(c)+')')
    if c not in ['success','skipped','cancelled',None]:
        fail+=1
        print('       WARN: gh run view '+str(r.get('databaseId'))+' --log-failed')
        print('       REF:  wkappbot skill read sdk-gg-main-automation')
        print('       REF:  wkappbot skill read wkcdp-mon')
print()
print('RESULT: '+str(fail)+' failure(s)' if fail else 'RESULT: All green')
sys.exit(2 if fail else 0)
