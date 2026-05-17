import json
import datetime
import pathlib
import sys


latest = pathlib.Path("data/ai-news-latest.jsonl")
pending = pathlib.Path("/tmp/wkcore/.wkappbot/pending_suggests.jsonl")

existing = set()
if pending.exists():
    with pending.open("r", encoding="utf-8") as f:
        for line in f:
            try:
                row = json.loads(line)
                text = str(row.get("text", ""))
                existing.add(text[:60].lower())
            except Exception:
                continue

items = []
if latest.exists():
    with latest.open("r", encoding="utf-8") as f:
        for line in f:
            try:
                row = json.loads(line)
                score = row.get("score", 0)
                if score < 3:
                    continue
                title = row.get("title", "")
                detail = row.get("direction") or row.get("desc", "")
                url = row.get("url", "")
                text = f"[AI-NEWS] [HN] {title}: {detail} -- {url}"
                key = text[:60].lower()
                if key in existing:
                    continue
                existing.add(key)
                record = {
                    "ts": datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S.0000000Z"),
                    "from": "DG-wkappbot-sdk",
                    "cwd": "D:\\\\GitHub\\\\wkappbot-sdk",
                    "text": text,
                    "files": [],
                    "slack_ts": None,
                    "review_status": None,
                    "review_note": None,
                    "review_ts": None,
                    "review_by": None,
                    "evidence_file": None,
                    "co_resolve": None,
                }
                items.append(json.dumps(record, ensure_ascii=False))
            except Exception as e:
                print(f"skip: {e}", file=sys.stderr)

if items:
    pending.parent.mkdir(parents=True, exist_ok=True)
    with pending.open("a", encoding="utf-8") as f:
        f.writelines(item + "\n" for item in items)
    print(f"Appended {len(items)} records")
else:
    print("No new items")
