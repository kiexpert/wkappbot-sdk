#!/usr/bin/env bash
# Run the kih multi-platform harness QA (tools/wk-harness-qa.sh --quick --json) against a binary.
# Usage: kih-harness-qa.sh <wkappbot binary>   (after clone-kih.sh; run from the sdk checkout root)
# Exit: the QA script's own code -- 0 pass, 1 a check failed, 2 cannot evaluate.
set -uo pipefail
core="${1:?usage: kih-harness-qa.sh <wkappbot binary>}"
qa=kih/tools/wk-harness-qa.sh
if [ ! -f "$qa" ]; then
  echo "::error::kih $qa not found (kih @ $(git -C kih rev-parse --short HEAD 2>/dev/null))"
  exit 2
fi
export WK_QA_REPO="$PWD/kih" WKAPPBOT_CORE="$(cd "$(dirname "$core")" && pwd)/$(basename "$core")"
echo "kih harness QA: $qa --quick --json, WKAPPBOT_CORE=$WKAPPBOT_CORE"
rc=0
bash "$qa" --quick --json > harness-qa.json || rc=$?
if ! command -v python3 >/dev/null 2>&1; then
  cat harness-qa.json
else
  python3 - harness-qa.json <<'PY' || true
import json, sys
try:
    rows = json.load(open(sys.argv[1], encoding="utf-8"))
except Exception as e:
    print(f"(json not parseable: {e}); raw output follows")
    print(open(sys.argv[1], encoding="utf-8", errors="replace").read()); sys.exit(0)
for r in rows if isinstance(rows, list) else []:
    st = str(r.get("status", "?")); name = r.get("name", "?"); det = r.get("detail", "")
    print(f"{st:5} {name}: {det}")
    if st.upper() == "FAIL":
        print(f"::error::harness QA FAIL {name}: {det}")
    elif st.upper() == "WARN":
        print(f"::warning::harness QA WARN {name}: {det}")
PY
fi
echo "kih harness QA exit=$rc (0 pass, 1 fail, 2 cannot evaluate)"
exit $rc
