#!/usr/bin/env bash
# wkgemini.sh -- gemini CLI wrapper with token tracking + 50% daily budget guard
# Usage: wkgemini.sh "task" [-m model] [-y] [--plan]

USAGE_LOG="${USERPROFILE:-$HOME}/.gemini/wkgemini_usage.jsonl"
DAILY_REQ_LIMIT=1000   # free tier
DAILY_TARGET_PCT=50    # use max 50% to leave room for other sessions

# ── Arg parsing ────────────────────────────────────────────────────────────────
task_parts=(); opts=(); shift_next=false; yolo=false; plan=false
for a in "$@"; do
  if $shift_next; then opts+=("$a"); shift_next=false; continue; fi
  case "$a" in
    -m|--model) opts+=("$a"); shift_next=true ;;
    --model=*)  opts+=("$a") ;;
    -y|--yolo)  yolo=true ;;
    --plan)     plan=true ;;
    -*)         opts+=("$a") ;;
    *)          task_parts+=("$a") ;;
  esac
done
task="${task_parts[*]}"
[[ -z "$task" ]] && { echo "Usage: wkgemini.sh \"task\" [-m model] [-y] [--plan]" >&2; exit 1; }

# ── Daily budget guard (50% of 1000 req/day) ──────────────────────────────────
today=$(date -u +%Y-%m-%d)
used_today=0
if [[ -f "$USAGE_LOG" ]]; then
  used_today=$(grep "\"ts\":\"${today}" "$USAGE_LOG" | wc -l)
fi
target_req=$(( DAILY_REQ_LIMIT * DAILY_TARGET_PCT / 100 ))
if (( used_today >= target_req )); then
  echo "[wkgemini] BLOCKED: daily budget ${DAILY_TARGET_PCT}% reached (${used_today}/${target_req} req today)" >&2
  echo "[wkgemini] To override: set WKGEMINI_FORCE=1" >&2
  [[ -z "$WKGEMINI_FORCE" ]] && exit 1
fi

# ── Build gemini args ──────────────────────────────────────────────────────────
gemini_args=("-p" "$task" "--output-format" "json")
$yolo && gemini_args+=("--yolo")
$plan && gemini_args+=("--approval-mode" "plan")
gemini_args+=("${opts[@]}")

# ── Run (stdout=JSON, stderr=warnings suppressed) ─────────────────────────────
tmp_json=$(mktemp); tmp_err=$(mktemp)
gemini "${gemini_args[@]}" > "$tmp_json" 2>"$tmp_err"
rc=$?

# ── Parse response ─────────────────────────────────────────────────────────────
python3 - "$tmp_json" "$tmp_err" "$USAGE_LOG" "$used_today" "$target_req" << 'PYEOF'
import json, sys, datetime

tmp_json, tmp_err, usage_log, used_today, target_req = sys.argv[1:]
raw = open(tmp_json, encoding='utf-8').read().strip()

try:
    d = json.loads(raw)
    print(d.get('response', ''))

    models = d.get('stats', {}).get('models', {})
    total_in = total_out = total_cached = 0
    main_model = 'unknown'
    for name, m in models.items():
        t = m.get('tokens', {})
        total_in     += t.get('input', 0)
        total_out    += t.get('candidates', 0)
        total_cached += t.get('cached', 0)
        if 'main' in m.get('roles', {}):
            main_model = name

    log = {
        'ts': datetime.datetime.utcnow().isoformat() + 'Z',
        'session_id': d.get('session_id', ''),
        'model': main_model,
        'tokens_in': total_in,
        'tokens_out': total_out,
        'tokens_cached': total_cached,
    }
    with open(usage_log, 'a', encoding='utf-8') as f:
        f.write(json.dumps(log) + '\n')

    new_used = int(used_today) + 1
    print(f'\n[wkgemini] {main_model} | in={total_in} out={total_out} cached={total_cached} | today={new_used}/{target_req} req ({int(new_used/int(target_req)*100)}% of 50% target)', file=sys.stderr)

except Exception as e:
    # Check for quota exhaustion
    raw_all = raw + open(tmp_err).read()
    if 'exhausted' in raw_all.lower() or 'quota' in raw_all.lower() or '429' in raw_all:
        marker = {'ts': datetime.datetime.now(datetime.UTC).isoformat(), 'quota_exhausted': True, 'model': 'unknown'}
        fout = open(usage_log, 'a', encoding='utf-8')
        fout.write(json.dumps(marker) + '
')
        fout.close()
        print('[wkgemini] QUOTA EXHAUSTED -- logged marker', file=sys.stderr)
    else:
        print(raw)
        print(f'[wkgemini] parse error: {e}', file=sys.stderr)
        err = open(tmp_err).read()
        if err: print(err, file=sys.stderr)
PYEOF

py_rc=$?
rm -f "$tmp_json" "$tmp_err"
exit $(( rc != 0 ? rc : py_rc ))
