#!/usr/bin/env bash
set -e

curl -s http://127.0.0.1:8787/health
curl -s -X POST http://127.0.0.1:8787/execute \
  -H 'content-type: application/json' \
  -d '{"args":["--version"]}'
