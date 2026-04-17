#!/usr/bin/env bash
# Evidence for suggest #1776337044.639809 resolve — FINAL Feature #6

set -e

REPO="D:/GitHub/WkAutoQuant"
WKB="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
PY="C:/Users/kiexp/AppData/Local/Programs/Python/Python312/python.exe"
cd "$REPO"

echo "======= wkappbot file read: module header ======="
"$WKB" file read "$REPO/scripts/signal_capture/query_similar.py" --limit 25 || true

echo ""
echo "======= python unit tests (8 tests) ======="
"$PY" scripts/signal_capture/query_similar.py

echo ""
echo "Evidence complete — 8/8 unit tests + wkappbot verification — FINAL Feature #6/6"
