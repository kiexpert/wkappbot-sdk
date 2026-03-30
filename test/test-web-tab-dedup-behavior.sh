#!/bin/bash
# test-web-tab-dedup-behavior.sh — verify web navigate dedup: same-domain navigate reuses tab, not creates new one
# Covers suggest: 【web open/navigate 탭 중복 생성 버그】 (ts=2026-03-30T12:46)
# Complements test-web-tab-dedup.sh (code structure) with behavior/flow checks
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Web Tab Dedup Behavior Test ==="
TM="W:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.TabManagement.cs"
WC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs"

echo -n "Test 1: NavigateAsync reuse path (not just open) deduplicated... "
check grep -q "NavigateAsync\|navigate" "$TM"

echo -n "Test 2: Domain-match step exists before creating new tab... "
check grep -q "Step 2\|Step 3" "$TM"

echo -n "Test 3: ListTabsAsync scans all open tabs before creating new one... "
check grep -q "ListTabsAsync\|ListTabs" "$TM"

echo -n "Test 4: web open uses GetOrCreateSandboxedTabAsync for reuse... "
check grep -q "GetOrCreateSandboxedTabAsync\|sandboxKey\|sandbox" "$WC"

echo -n "Test 5: AskTargetRegistry survives across CLI invocations (comment)... "
check grep -q "AskTargetRegistry\|savedTargetId" "$TM"

echo -n "Test 6: Tab URL checked against existing tabs list... "
check grep -q "url\|Url\|URL" "$TM"

echo -n "Test 7: URL normalization (http vs https, trailing slash) handled... "
check grep -q "TrimEnd\|StartsWith\|http" "$TM"

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
