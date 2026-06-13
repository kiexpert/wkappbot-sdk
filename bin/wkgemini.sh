#!/usr/bin/env bash
# wkgemini.sh -- gemini CLI wrapper with API key auto-load + daily budget guard
# Usage: wkgemini.sh "task" [-m model] [-y] [--plan] [--stream]

if [[ -z "$GEMINI_API_KEY" && -f "/d/GitHub/.env" ]]; then
  export GEMINI_API_KEY=$(grep "^GEMINI_API_KEY=" "/d/GitHub/.env" | cut -d= -f2)
fi

USAGE_LOG="${USERPROFILE:-$HOME}/.gemini/wkgemini_usage.jsonl"
# Gemini flash daily REQUEST cap = a runaway backstop only, NOT the real throttle.
# Gemini's actual per-day quota is plentiful; the real throttle is its per-minute RPM
# (the API returns 429 on a burst), which gemini handles itself. So this cap is set high
# to avoid false daily blocks (it was 1000 -> blocked at 500, which falsely tripped a
# plentiful gemini). Keep a backstop so a runaway loop can't bill forever. (2026-06-11)
DAILY_REQ_LIMIT=20000
DAILY_TARGET_PCT=50

task_parts=()
opts=()
shift_next=false
yolo=false
plan=false
json_mode=false
stream_mode=false

for a in "$@"; do
  if $shift_next; then
    opts+=("$a")
    shift_next=false
    continue
  fi
  case "$a" in
    -m|--model)
      opts+=("$a")
      shift_next=true
      ;;
    --model=*)
      opts+=("$a")
      ;;
    -y|--yolo)
      yolo=true
      ;;
    --plan)
      plan=true
      ;;
    --json)
      json_mode=true
      ;;
    --stream|--stream-json)
      stream_mode=true
      ;;
    -*)
      opts+=("$a")
      ;;
    *)
      task_parts+=("$a")
      ;;
  esac
done

task="${task_parts[*]}"
[[ -z "$task" ]] && { echo 'Usage: wkgemini.sh "task" [-m model] [-y] [--plan] [--stream]' >&2; exit 1; }

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

if $stream_mode; then
  gemini_args=("-p" "$task" "--output-format" "stream-json")
elif $json_mode; then
  gemini_args=("-p" "$task" "--output-format" "json")
else
  gemini_args=("-p" "$task")
fi

$yolo && gemini_args+=("--yolo")
$plan && gemini_args+=("--approval-mode" "plan")
gemini_args+=("${opts[@]}")

if $stream_mode; then
  mkdir -p "$(dirname "$USAGE_LOG")"
  tmp_py=$(mktemp)
  cat > "$tmp_py" <<'PYEOF'
import datetime
import json
import sys

usage_log, used_today, target_req = sys.argv[1:4]
printed_any = False
main_model = "unknown"
total_in = total_out = total_cached = 0
final_status = "unknown"
final_seen = False

for raw in sys.stdin:
    line = raw.strip()
    if not line:
        continue
    try:
        d = json.loads(line)
    except Exception:
        print(line, file=sys.stderr)
        continue

    t = d.get("type")
    if t == "message" and d.get("role") == "assistant":
        content = d.get("content")
        if d.get("delta"):
            if isinstance(content, str) and content:
                print(content, end="", flush=True)
                printed_any = True
        else:
            if isinstance(content, str) and content:
                print(content, end="", flush=True)
                printed_any = True
    elif t == "result":
        final_seen = True
        final_status = d.get("status", final_status)
        stats = d.get("stats", {}) or {}
        models = stats.get("models", {}) or {}
        for name, m in models.items():
            toks = m.get("tokens", {}) or {}
            total_in += toks.get("input", 0)
            total_out += toks.get("candidates", 0)
            total_cached += toks.get("cached", 0)
            if "main" in (m.get("roles", {}) or {}):
                main_model = name

if printed_any:
    print()

if final_seen:
    log = {
        "ts": datetime.datetime.utcnow().isoformat() + "Z",
        "session_id": "",
        "model": main_model,
        "tokens_in": total_in,
        "tokens_out": total_out,
        "tokens_cached": total_cached,
        "status": final_status,
    }
    with open(usage_log, "a", encoding="utf-8") as f:
        f.write(json.dumps(log) + "\n")
    new_used = int(used_today) + 1
    print(
        f"[wkgemini] {main_model} | in={total_in} out={total_out} cached={total_cached} | "
        f"today={new_used}/{target_req} req ({int(new_used/int(target_req)*100)}% of 50% target)",
        file=sys.stderr,
    )
PYEOF
  gemini "${gemini_args[@]}" | python3 "$tmp_py" "$USAGE_LOG" "$used_today" "$target_req"
  rc=${PIPESTATUS[0]}
  rm -f "$tmp_py"
  exit $rc
fi

if ! $json_mode; then
  mkdir -p "$(dirname "$USAGE_LOG")"
  gemini "${gemini_args[@]}"
  rc=$?
  exit $rc
fi

tmp_json=$(mktemp)
tmp_err=$(mktemp)
gemini "${gemini_args[@]}" > "$tmp_json" 2>"$tmp_err"
rc=$?

python3 - "$tmp_json" "$tmp_err" "$USAGE_LOG" "$used_today" "$target_req" <<'PYEOF'
import datetime
import json
import sys

tmp_json, tmp_err, usage_log, used_today, target_req = sys.argv[1:6]
raw = open(tmp_json, encoding="utf-8").read().strip()

try:
    d = json.loads(raw)
    print(d.get("response", ""))

    models = d.get("stats", {}).get("models", {})
    total_in = total_out = total_cached = 0
    main_model = "unknown"
    for name, m in models.items():
        t = m.get("tokens", {})
        total_in += t.get("input", 0)
        total_out += t.get("candidates", 0)
        total_cached += t.get("cached", 0)
        if "main" in m.get("roles", {}):
            main_model = name

    log = {
        "ts": datetime.datetime.utcnow().isoformat() + "Z",
        "session_id": d.get("session_id", ""),
        "model": main_model,
        "tokens_in": total_in,
        "tokens_out": total_out,
        "tokens_cached": total_cached,
    }
    with open(usage_log, "a", encoding="utf-8") as f:
        f.write(json.dumps(log) + "\n")

    new_used = int(used_today) + 1
    print(f"\n[wkgemini] {main_model} | in={total_in} out={total_out} cached={total_cached} | today={new_used}/{target_req} req ({int(new_used/int(target_req)*100)}% of 50% target)", file=sys.stderr)

except Exception as e:
    raw_all = raw + open(tmp_err).read()
    if "exhausted" in raw_all.lower() or "quota" in raw_all.lower() or "429" in raw_all:
        marker = {"ts": datetime.datetime.now(datetime.UTC).isoformat(), "quota_exhausted": True, "model": "unknown"}
        with open(usage_log, "a", encoding="utf-8") as fout:
            fout.write(json.dumps(marker) + "\n")
        print("[wkgemini] QUOTA EXHAUSTED -- logged marker", file=sys.stderr)
    else:
        print(raw)
        print(f"[wkgemini] parse error: {e}", file=sys.stderr)
        err = open(tmp_err).read()
        if err:
            print(err, file=sys.stderr)
PYEOF

py_rc=$?
rm -f "$tmp_json" "$tmp_err"
exit $(( rc != 0 ? rc : py_rc ))
