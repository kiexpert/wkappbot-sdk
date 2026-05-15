#!/usr/bin/env bash
set -e

node src/server.js &
echo $! > relay.pid

for i in 1 2 3 4 5; do
  if curl -fsS http://127.0.0.1:8787/health >/dev/null; then
    exit 0
  fi
  sleep 1
done

cat relay.pid | xargs -r kill
exit 1
