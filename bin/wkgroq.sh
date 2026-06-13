#!/usr/bin/env bash
# wkgroq.sh -- Groq API wrapper with token tracking
# Usage: wkgroq.sh "task" [-m model] [-s system_prompt]

USAGE_LOG="${USERPROFILE:-$HOME}/.groq/wkgroq_usage.jsonl"
DEFAULT_MODEL="llama-3.3-70b-versatile"
# wkappbot skill read wkharness-env-discovery
_env_file="${WKHARNESS_ENV_FILE:-$(git rev-parse --show-toplevel 2>/dev/null)/.env}"
[ -z "$_env_file" ] && _env_file="$HOME/.env"
GROQ_API_KEY="${GROQ_API_KEY:-$(grep GROQ_API_KEY "$_env_file" 2>/dev/null | cut -d= -f2 | tr -d '\r\n')}"

# ── Arg parsing ────────────────────────────────────────────────────────────────
task_parts=(); model="$DEFAULT_MODEL"; system_msg="You are a helpful coding assistant."
shift_next=""; skip_next=false
for a in "$@"; do
  if $skip_next; then
    case "$shift_next" in
      -m|--model)  model="$a" ;;
      -s|--system) system_msg="$a" ;;
    esac
    skip_next=false; continue
  fi
  case "$a" in
    -m|--model|-s|--system) shift_next="$a"; skip_next=true ;;
    -*) ;;
    *) task_parts+=("$a") ;;
  esac
done
task="${task_parts[*]}"
[[ -z "$task" ]] && { echo "Usage: wkgroq.sh \"task\" [-m model] [-s system]" >&2; exit 1; }
[[ -z "$GROQ_API_KEY" ]] && { echo "[wkgroq] ERROR: GROQ_API_KEY not set" >&2; exit 1; }

# ── Build request ──────────────────────────────────────────────────────────────
payload=$(python3 -c "
import json, sys
print(json.dumps({
    'model': sys.argv[1],
    'messages': [
        {'role': 'system', 'content': sys.argv[2]},
        {'role': 'user',   'content': sys.argv[3]}
    ],
    'max_tokens': 4096
}))
" "$model" "$system_msg" "$task" 2>/dev/null)

# ── Call API ───────────────────────────────────────────────────────────────────
tmp_resp=$(mktemp); tmp_hdr=$(mktemp)
curl -s -D "$tmp_hdr" \
  "https://api.groq.com/openai/v1/chat/completions" \
  -H "Authorization: Bearer $GROQ_API_KEY" \
  -H "Content-Type: application/json" \
  -d "$payload" > "$tmp_resp" 2>/dev/null
rc=$?

# ── Parse response ─────────────────────────────────────────────────────────────
python3 - "$tmp_resp" "$tmp_hdr" "$USAGE_LOG" "$model" << 'PYEOF'
import json, sys, datetime, re

resp_f, hdr_f, usage_log, model = sys.argv[1:]
raw = open(resp_f, encoding='utf-8').read()
hdrs = open(hdr_f, encoding='utf-8').read()

try:
    d = json.loads(raw)
    if 'error' in d:
        print(f"[wkgroq] ERROR: {d['error'].get('message','unknown')}", file=sys.stderr)
        sys.exit(1)

    content = d['choices'][0]['message']['content']
    usage   = d.get('usage', {})
    prompt_tok = usage.get('prompt_tokens', 0)
    compl_tok  = usage.get('completion_tokens', 0)
    total_tok  = usage.get('total_tokens', 0)

    # Rate limit from headers
    rl_req = re.search(r'x-ratelimit-remaining-requests:\s*(\d+)', hdrs, re.I)
    rl_tok = re.search(r'x-ratelimit-remaining-tokens:\s*(\d+)', hdrs, re.I)
    remain_req = int(rl_req.group(1)) if rl_req else -1
    remain_tok = int(rl_tok.group(1)) if rl_tok else -1

    log = {
        'ts': datetime.datetime.now(datetime.UTC).isoformat(),
        'model': model,
        'tokens_prompt': prompt_tok,
        'tokens_completion': compl_tok,
        'tokens_total': total_tok,
        'remain_req': remain_req,
        'remain_tok': remain_tok,
    }
    with open(usage_log, 'a', encoding='utf-8') as fout:
        fout.write(json.dumps(log) + '\n')

    print(content)
    print(f'\n[wkgroq] {model} | prompt={prompt_tok} out={compl_tok} total={total_tok} | remain={remain_req}req {remain_tok}tok/min', file=sys.stderr)

except Exception as e:
    print(raw)
    print(f'[wkgroq] parse error: {e}', file=sys.stderr)
PYEOF

py_rc=$?
rm -f "$tmp_resp" "$tmp_hdr"
exit $(( rc != 0 ? rc : py_rc ))
