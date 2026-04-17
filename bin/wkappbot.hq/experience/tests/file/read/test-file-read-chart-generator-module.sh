#!/usr/bin/env bash
# Evidence for suggest #1776337025.400269 resolve
# Verifies chart_generator.py via wkappbot file read + skill show,
# then runs python self-test + generates actual sample chart.

set -e

REPO="D:/GitHub/WkAutoQuant"
WKB="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
PY="C:/Users/kiexp/AppData/Local/Programs/Python/Python312/python.exe"
cd "$REPO"

echo "======= wkappbot file read: module header ======="
"$WKB" file read "$REPO/scripts/signal_capture/chart_generator.py" --limit 25 || true

echo ""
echo "======= wkappbot skill show: spec reference ======="
"$WKB" skill show pre-signal-capture-method 2>&1 || true

echo ""
echo "======= python unit tests (5 tests) ======="
"$PY" scripts/signal_capture/chart_generator.py

echo ""
echo "======= verify sample chart via python (no ls available) ======="
"$PY" -c "
import os
p = r'D:/GitHub/WkAutoQuant/wavevault/signal_capture_sample.png'
if os.path.isfile(p):
    print(f'SAMPLE OK: {p} ({os.path.getsize(p):,} bytes)')
else:
    print(f'SAMPLE MISSING: {p}')
    raise SystemExit(1)
"

echo ""
echo "Evidence complete"
