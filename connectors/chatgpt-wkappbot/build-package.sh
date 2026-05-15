#!/usr/bin/env bash
set -e

ROOT="connectors/chatgpt-wkappbot"
OUT="wkappbot-chatgpt-connector.tar.gz"

tar -czf "$OUT" "$ROOT"

echo "$OUT created"
