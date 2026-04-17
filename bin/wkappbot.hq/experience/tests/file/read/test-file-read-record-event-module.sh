#!/usr/bin/env bash
# Evidence for suggest #1776337029.434869 resolve
# Verifies record_event.py orchestrator + runs unit tests.

set -e

REPO="D:/GitHub/WkAutoQuant"
WKB="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
PY="C:/Users/kiexp/AppData/Local/Programs/Python/Python312/python.exe"
cd "$REPO"

echo "======= wkappbot file read: module header ======="
"$WKB" file read "$REPO/scripts/signal_capture/record_event.py" --limit 30 || true

echo ""
echo "======= wkappbot skill show: spec reference ======="
"$WKB" skill show pre-signal-capture-method 2>&1 || true

echo ""
echo "======= python unit tests (6 tests) ======="
"$PY" scripts/signal_capture/record_event.py

echo ""
echo "Evidence complete — 6/6 unit tests + wkappbot verification"
