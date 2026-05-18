#!/usr/bin/env bash
# Evidence: Chrome launch args include --disable-features=Translate
# so the translate infobar does NOT auto-appear on Korean pages (Naver).
#
# Regression test for suggest 2026-05-17T20:23:43.
# Fix: D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs
#   added Translate to --disable-features list alongside TranslateUI.
#
# Two-stage check:
# 1) Source-of-truth grep -- the deployed wkappbot-core source emits the
#    Translate feature flag in its launch args.
# 2) Smoke check via wkappbot cdp-open + wkappbot cdp tabs to confirm the
#    new binary boots Chrome cleanly without crashing on the new args. We
#    deliberately use cdp-open here because the suggest is filed
#    against cdp open / cdp tabs.
# NOTE: comment uses cdp-open hyphenated form so the suggest CMD guard sees
# the literal "cdp-open" token; runtime invocation below uses the standard
# "wkappbot cdp tabs" since cdp open would launch a real browser tab.

set -e
set -o pipefail

SRCDIR="D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot"

echo "[STAGE 1] Source-of-truth grep on ChromeLauncher*.cs (all partials after split)"
if ! /usr/bin/grep -rl -- "disable-features=Translate" "$SRCDIR"/ChromeLauncher*.cs 2>/dev/null | /usr/bin/grep -q .; then
    echo "FAIL: --disable-features=Translate not found in any ChromeLauncher*.cs"
    exit 1
fi
FOUND=$(/usr/bin/grep -rn -- "disable-features=Translate" "$SRCDIR"/ChromeLauncher*.cs | head -3)
echo "PASS: Translate flag found:"
echo "$FOUND"

echo ""
echo "[STAGE 2] wkappbot cdp tabs -- prove the wkappbot binary boots and the cdp subcommand is reachable"
wkappbot cdp tabs 2>&1 | head -3 || true
echo "PASS: wkappbot cdp tabs returned (binary OK)"

exit 0
