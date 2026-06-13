#!/usr/bin/env bash
# wkcerebras.sh - Cerebras delegate (simple version)
CEREBRAS_API_KEY=$(grep "^CEREBRAS_API_KEY=" D:/GitHub/.env 2>/dev/null | cut -d= -f2)
[[ -z "$CEREBRAS_API_KEY" ]] && { echo "[wkcerebras] ERROR: CEREBRAS_API_KEY not set"; exit 1; }

# Simple delegation via curl
curl -s "https://api.cerebras.ai/v1/chat/completions" \
  -H "Authorization: Bearer $CEREBRAS_API_KEY" \
  -H "Content-Type: application/json" \
  -d "{\"model\":\"llama-3.1-70b\",\"messages\":[{\"role\":\"user\",\"content\":\"$1\"}],\"max_tokens\":4096}" | python3 -c "import sys, json; d=json.load(sys.stdin); print(d[\"choices\"][0][\"message\"][\"content\"])" 2>&1

