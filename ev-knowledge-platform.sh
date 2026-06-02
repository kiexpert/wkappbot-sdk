#!/usr/bin/env bash
# Evidence: wkappbot-the-artificial-knowledge-platform skill installed and README updated
set -e
wkappbot skill read wkappbot-the-artificial-knowledge-platform 2>&1 | grep -q FLYWHEEL || exit 1
wkappbot skill search artificial 2>&1 | grep -q wkappbot-the-artificial-knowledge-platform || exit 1
grep -q Artificial README.md 2>/dev/null || grep -qr Artificial D:/GitHub/wkappbot-sdk/README.md || exit 1
echo PASS
