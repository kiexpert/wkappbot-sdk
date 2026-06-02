#!/usr/bin/env bash
set -euo pipefail

VIEWER_URL="file:///D:/GitHub/wkappbot-sdk/docs/viewer.html"

if [[ "${1:-}" == "-h" || "${1:-}" == "--help" ]]; then
  echo "Usage: GH_TOKEN=<github_pat> scripts/open-skill-viewer.sh"
  echo "Opens the local skill viewer and posts GH_TOKEN to the embedded skill page."
  exit 0
fi

OPEN_OUTPUT="$(wkappbot cdp open "$VIEWER_URL")"
echo "$OPEN_OUTPUT"

GRAP="$(printf '%s\n' "$OPEN_OUTPUT" | grep -Eo "\{proc:'chrome',cdp:[0-9]+\}" | tail -n 1 || true)"
if [[ -z "$GRAP" ]]; then
  GRAP="{proc:'chrome'}"
fi

if [[ -z "${GH_TOKEN:-}" ]]; then
  echo "GH_TOKEN is not set; opened viewer without token injection."
  exit 0
fi

export GH_TOKEN
TOKEN_B64="$(printf '%s' "$GH_TOKEN" | base64 | tr -d '\r\n')"
wkappbot a11y read "$GRAP" --eval-js "
(() => {
  const token = atob('$TOKEN_B64');
  localStorage.setItem('gh_token', token);
  const frame = document.getElementById('skillFrame');
  if (frame && frame.contentWindow) {
    frame.contentWindow.postMessage({ type: 'auth', token }, '*');
  }
  return 'posted auth token';
})()
"
