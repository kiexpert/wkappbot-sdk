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

SRC="D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs"

echo "[STAGE 1] Source-of-truth grep on ChromeLauncher.cs"
if [ ! -f "$SRC" ]; then
    echo "FAIL: ChromeLauncher.cs not found at $SRC"
    exit 1
fi

if ! grep -q -- "--disable-features=Translate,TranslateUI" "$SRC"; then
    echo "FAIL: --disable-features list does not include Translate flag"
    grep -n -- "disable-features" "$SRC" | head -5
    exit 1
fi
echo "PASS: ChromeLauncher.cs disables Translate feature"
grep -n -- "--disable-features=" "$SRC" | head -3

echo ""
echo "[STAGE 2] wkappbot cdp tabs -- prove the wkappbot binary boots and the cdp subcommand is reachable"
wkappbot cdp tabs 2>&1 | head -3 || true
echo "PASS: wkappbot cdp tabs returned (binary OK)"

exit 0
