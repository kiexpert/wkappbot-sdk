#!/usr/bin/env bash
# test-a11y-kill-ambiguity.sh — verify a11y kill ambiguity guard
# Fix: multi-match without --nth → find-mode list (no kill)
#      self-kill guard: cmdline-only match on wkappbot-core → skip unless firstArg matches
PASS=0; FAIL=0

check() {
    local label="$1"; local cmd="$2"
    if eval "$cmd" > /dev/null 2>&1; then
        echo "[PASS] $label"; PASS=$((PASS+1))
    else
        echo "[FAIL] $label"; FAIL=$((FAIL+1))
    fi
}

SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/A11yCommand.Kill.cs"

# Source-level: ambiguity guard present
check "ambiguity guard: candidates.Count > 1 block" \
    "grep -q 'candidates.Count > 1 && nthRaw == null' '$SRC'"

check "ambiguity guard: shows numbered list" \
    "grep -q 'ambiguous.*processes match' '$SRC'"

check "ambiguity guard: suggests --nth" \
    "grep -q 'Use --nth N to target' '$SRC'"

# Source-level: --nth range error handling
check "nth out-of-range: error message present" \
    "grep -q 'out of range' '$SRC'"

# Source-level: self-kill guard still present
check "self-kill guard: wkappbot-core cmdline check" \
    "grep -q 'self-kill guard' '$SRC'"

check "self-kill guard: isDaemonTarget firstArg check" \
    "grep -q 'isDaemonTarget' '$SRC'"

# Functional: unmatchable pattern exits 1 (no crash, no kill)
check "a11y kill nomatch exits 1" \
    "! \"D:/SDK/bin/wkappbot.exe\" a11y kill \"__no_such_proc_xyz__\" > /dev/null 2>&1"

# Functional: --dry-run with 'wkappbot' pattern shows dry output or guard (not actual kill)
check "a11y kill wkappbot --dry-run does not hard-kill" \
    "\"D:/SDK/bin/wkappbot.exe\" a11y kill \"wkappbot\" --dry-run 2>&1 | grep -qiE 'DRY|ambiguous|no matching|GUARD'"

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ]
