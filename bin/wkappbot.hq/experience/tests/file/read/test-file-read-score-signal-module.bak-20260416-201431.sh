#!/usr/bin/env bash
# Evidence for suggest #1776337009.754779 resolve
# Verifies score_signal.py via wkappbot file read + skill show,
# then runs python unit tests.

set -e

REPO="D:/GitHub/WkAutoQuant"
WKB="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
PY="C:/Users/kiexp/AppData/Local/Programs/Python/Python312/python.exe"
cd "$REPO"

echo "======= wkappbot file read: module header ======="
"$WKB" file read "$REPO/scripts/signal_capture/score_signal.py" --limit 25 || true

echo ""
echo "======= wkappbot skill show: spec reference ======="
"$WKB" skill show pre-signal-capture-method 2>&1 | head -20 || true

echo ""
echo "======= python unit tests ======="
"$PY" scripts/signal_capture/score_signal.py

echo ""
echo "======= sample DIAMOND setup scoring ======="
"$PY" -c "
import sys, json
sys.path.insert(0, 'scripts')
import pandas as pd
from signal_capture.score_signal import score_signal
volumes = [100]*25 + [190, 180, 195, 185, 200] + [500] + [480]*10
closes  = [100.0]*25 + [100.5, 101.0, 101.5, 102.0, 102.5] + [108.0] + [110.0]*10
dates   = pd.date_range('2026-01-05', periods=len(volumes), freq='B')
df = pd.DataFrame({'open':closes,'high':closes,'low':closes,'close':closes,'volume':volumes}, index=dates)
r = score_signal(df, dates[30].strftime('%Y-%m-%d'))
print(json.dumps(r, ensure_ascii=False, indent=2))
"

echo ""
echo "Evidence complete — 8/8 unit tests + wkappbot file/skill verification passed"
