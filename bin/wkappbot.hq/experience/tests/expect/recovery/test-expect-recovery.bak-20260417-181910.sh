#!/bin/bash
# Evidence: P1 Expect-Recovery works — runs focusless-demo.yaml, expects 3/3 PASS.
WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
YAML="D:/GitHub/WKAppBot/handlers/examples/expect-recovery-demo.yaml"

out=$($WKAPPBOT run "$YAML" 2>&1)
passed=$(echo "$out" | grep -c "→ PASS")
failed=$(echo "$out" | grep -c "→ FAIL")
echo "PASS=$passed FAIL=$failed"
echo "$out" | grep -E "Results|PASS|FAIL" | tail -5
[ "$passed" -ge 3 ] && [ "$failed" -eq 0 ] && exit 0 || exit 1
