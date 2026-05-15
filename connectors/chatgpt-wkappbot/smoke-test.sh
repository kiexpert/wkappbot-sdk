#!/usr/bin/env bash
set -euo pipefail

curl -fsS http://127.0.0.1:8787/health
curl -fsS -X POST http://127.0.0.1:8787/execute \
  -H 'content-type: application/json' \
  -d '{"args":["--version"]}'

curl -fsS http://127.0.0.1:8787/metrics
curl -fsS http://127.0.0.1:8787/dashboard >/dev/null
