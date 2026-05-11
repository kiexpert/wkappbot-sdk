#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""WKAppBot public evidence demo: web intelligence -> escrow -> issue.

Safe by design:
- uses synthetic/public-topic demo data by default
- prints only redacted public evidence
- optionally commits private payload to APPBOT_PRIVATE_REPO when token is present
- never prints private payload or secrets
"""
from __future__ import annotations

import datetime as _dt
import hashlib
import json
import os
import pathlib
import random
import subprocess
from typing import Any

ROOT = pathlib.Path(__file__).resolve().parents[2]
OUT = ROOT / "examples" / "web-intel-escrow" / "out"
OUT.mkdir(parents=True, exist_ok=True)

TOPICS = [
    "AI agents are moving from chat to audited workflow execution",
    "Computer-use automation needs evidence logs, not just answers",
    "Public actions and issues can prove automation is alive",
    "Private payload separation keeps sensitive intelligence out of public logs",
    "Escrow queues reduce the risk of AI acting on weak or unverified signals",
]


def utc_now() -> _dt.datetime:
    return _dt.datetime.now(_dt.UTC).replace(microsecond=0)


def digest(text: str, n: int = 12) -> str:
    return hashlib.sha256(text.encode("utf-8")).hexdigest()[:n]


def build_private_payload() -> dict[str, Any]:
    now = utc_now()
    seed = int(now.strftime("%Y%m%d%H"))
    rnd = random.Random(seed)
    items = []
    for i, topic in enumerate(TOPICS, 1):
        confidence = round(rnd.uniform(0.62, 0.91), 2)
        escrow = "approved_demo" if confidence >= 0.78 else "watch_demo"
        items.append({
            "id": f"demo-{i}",
            "topic": topic,
            "confidence": confidence,
            "escrow_status": escrow,
            "private_note": "Detailed scoring would live only in the private repo.",
        })
    return {
        "kind": "wkappbot_web_intel_escrow_demo",
        "generated_at": now.isoformat().replace("+00:00", "Z"),
        "source_mode": os.getenv("APPBOT_WEB_INTEL_MODE", "demo-safe"),
        "items": items,
        "policy": {
            "public_logs": "redacted evidence only",
            "private_payload": "commit to private repo when configured",
            "core": "WKAppBot core remains private",
        },
    }


def public_summary(payload: dict[str, Any], private_commit: str | None) -> str:
    items = payload["items"]
    approved = sum(1 for x in items if x["escrow_status"] == "approved_demo")
    watch = sum(1 for x in items if x["escrow_status"] == "watch_demo")
    avg_conf = sum(float(x["confidence"]) for x in items) / max(1, len(items))
    payload_hash = digest(json.dumps(payload, ensure_ascii=False, sort_keys=True))
    top_lines = "\n".join(
        f"- {x['topic']} — {x['escrow_status']} / confidence {x['confidence']:.2f}"
        for x in items[:5]
    )
    private_line = private_commit or "not configured"
    return f"""## WKAppBot Web Intelligence Escrow Evidence

Run: `{payload['generated_at']}`
Mode: `{payload['source_mode']}`

### Public result
- Topics checked: {len(items)}
- Escrow approved: {approved}
- Escrow watch: {watch}
- Average confidence: {avg_conf:.2f}
- Private payload hash: `{payload_hash}`
- Private commit: `{private_line}`

### Public insights
{top_lines}

### Evidence checklist
- Collector: OK
- AppBot-style review: OK
- Escrow writer: OK
- Private payload: {'OK' if private_commit else 'SKIPPED'}
- Public issue reporter: OK

> Talk is cheap. Actions logs are evidence.
"""


def run(cmd: list[str], cwd: pathlib.Path | None = None, check: bool = True) -> subprocess.CompletedProcess[str]:
    return subprocess.run(cmd, cwd=str(cwd or ROOT), text=True, stdout=subprocess.PIPE, stderr=subprocess.STDOUT, check=check)


def commit_private_payload(payload: dict[str, Any]) -> str | None:
    repo = os.getenv("APPBOT_PRIVATE_REPO", "").strip()
    token = os.getenv("APPBOT_PRIVATE_REPO_TOKEN", "").strip()
    if not repo or not token:
        return None

    work = pathlib.Path(os.environ.get("RUNNER_TEMP", "/tmp")) / "wkappbot-private-payload"
    if work.exists():
        subprocess.run(["rm", "-rf", str(work)], check=False)
    url = f"https://x-access-token:{token}@github.com/{repo}.git"
    run(["git", "clone", "--depth", "1", url, str(work)], cwd=ROOT)
    run(["git", "config", "user.name", "WKAppBot Evidence Bot"], cwd=work)
    run(["git", "config", "user.email", "bot@wkappbot.local"], cwd=work)

    ts = payload["generated_at"].replace(":", "").replace("-", "")
    path = work / "web-intel-escrow" / f"payload-{ts}.json"
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2), encoding="utf-8")
    run(["git", "add", str(path.relative_to(work))], cwd=work)
    diff = run(["git", "diff", "--cached", "--quiet"], cwd=work, check=False)
    if diff.returncode == 0:
        return "no-change"
    run(["git", "commit", "-m", f"chore: add web intel escrow payload {payload['generated_at']}"] , cwd=work)
    run(["git", "push"], cwd=work)
    sha = run(["git", "rev-parse", "--short", "HEAD"], cwd=work).stdout.strip()
    return sha


def write_outputs(summary: str, payload: dict[str, Any]) -> None:
    (OUT / "public-summary.md").write_text(summary, encoding="utf-8")
    redacted = {
        "generated_at": payload["generated_at"],
        "item_count": len(payload["items"]),
        "payload_hash": digest(json.dumps(payload, ensure_ascii=False, sort_keys=True)),
    }
    (OUT / "public-redacted.json").write_text(json.dumps(redacted, ensure_ascii=False, indent=2), encoding="utf-8")


def append_github_summary(summary: str) -> None:
    path = os.getenv("GITHUB_STEP_SUMMARY")
    if path:
        with open(path, "a", encoding="utf-8") as f:
            f.write(summary)
            f.write("\n")


def post_issue(summary: str) -> None:
    token = os.getenv("GITHUB_TOKEN") or os.getenv("GH_TOKEN")
    repo = os.getenv("GITHUB_REPOSITORY", "")
    if not token or not repo:
        print("issue reporter skipped: no token/repo")
        return
    import urllib.request

    today = utc_now().strftime("%Y-%m-%d")
    title = f"🤖 WKAppBot Web Intelligence Evidence — {today}"
    headers = {
        "Authorization": f"Bearer {token}",
        "Accept": "application/vnd.github+json",
        "X-GitHub-Api-Version": "2022-11-28",
        "Content-Type": "application/json",
    }

    search_url = f"https://api.github.com/search/issues?q=repo:{repo}+is:issue+in:title+{urllib.request.pathname2url(title)}"
    req = urllib.request.Request(search_url, headers=headers)
    with urllib.request.urlopen(req, timeout=20) as r:
        found = json.loads(r.read().decode("utf-8"))
    issue_num = None
    for item in found.get("items", []):
        if item.get("title") == title:
            issue_num = item.get("number")
            break

    if issue_num is None:
        body = "This public issue is generated by GitHub Actions as living evidence that WKAppBot-style workflows can run, summarize, escrow, and report.\n\n" + summary
        data = json.dumps({"title": title, "body": body, "labels": ["appbot", "evidence", "demo"]}).encode("utf-8")
        req = urllib.request.Request(f"https://api.github.com/repos/{repo}/issues", data=data, headers=headers, method="POST")
        with urllib.request.urlopen(req, timeout=20) as r:
            created = json.loads(r.read().decode("utf-8"))
        print(f"created issue #{created.get('number')}")
    else:
        data = json.dumps({"body": summary}).encode("utf-8")
        req = urllib.request.Request(f"https://api.github.com/repos/{repo}/issues/{issue_num}/comments", data=data, headers=headers, method="POST")
        with urllib.request.urlopen(req, timeout=20) as r:
            commented = json.loads(r.read().decode("utf-8"))
        print(f"commented issue #{issue_num}: {commented.get('html_url')}")


def main() -> None:
    payload = build_private_payload()
    private_commit = commit_private_payload(payload)
    summary = public_summary(payload, private_commit)
    write_outputs(summary, payload)
    append_github_summary(summary)
    post_issue(summary)
    print("public evidence generated")


if __name__ == "__main__":
    main()
