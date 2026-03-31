#!/usr/bin/env bash
# Test: slack send \n escape decoding — lines=N in SLACK-DBG confirms real newlines
OUT=$(wkappbot slack send "line1\nline2\nline3" 2>&1)
echo "$OUT" | grep -q "lines=3" || { echo "[FAIL] lines=3 not found -- \\n not decoded to real newlines"; exit 1; }
echo "$OUT" | grep -q "firstLine=line1" || { echo "[FAIL] firstLine should be line1 only"; exit 1; }
echo "$OUT" | grep -q "Sent: line1" || { echo "[FAIL] Sent line not found"; exit 1; }
echo "[PASS] slack send \\n escape: lines=3, firstLine=line1, Sent: line1"
