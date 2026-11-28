#!/usr/bin/env bash
# Stub PreToolUse hook for the grok QA leg: logs the payload, then denies the call.
# Deny contract (docs.x.ai/build/features/hooks): stdout {"decision":"deny",...} and exit 2.
set -u
payload="$(cat)"
log="${GROK_QA_HOOK_LOG:-/tmp/grok-qa-hook.log}"
printf '%s\n' "$payload" >> "$log"
printf '%s\n' '{"decision":"deny","reason":"grok-hook-qa: refused by stub hook"}'
echo "grok-hook-qa: refused by stub hook" >&2
exit 2
