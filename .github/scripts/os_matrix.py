#!/usr/bin/env python3
"""Build os-matrix.json for the portal release board from one "Harness QA (Windows + macOS)" run.

Usage: os_matrix.py <repo owner/name> <run_id> <out.json>      (GITHUB_TOKEN in the environment)
       os_matrix.py [--self-test]                                (no arguments runs the self-test)

One row per OS (windows-*, macos-*; ubuntu kept if present). conclusion is "success" only when every job of
that OS in the run succeeded; otherwise the first non-success conclusion. last_screen is the last lines of the
OS's first failing job, else its main "harness-qa" job, with timestamps, ANSI codes, runner group markers and
post-job cleanup removed and anything token-like masked. The file is kept under 20 KB.
"""
import json
import re
import sys
import os
import datetime
import urllib.error
import urllib.request

API = "https://api.github.com"
MAX_BYTES = 20_000
SCREEN_LINES = 10
ANSI = re.compile(r"\x1b\[[0-9;?]*[ -/]*[@-~]")
TS = re.compile(r"^\ufeff?\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d(\.\d+)?Z ?")
TOKENISH = [
    re.compile(r"(ghp|gho|ghu|ghs|ghr)_[A-Za-z0-9]{10,}"),
    re.compile(r"github_pat_[A-Za-z0-9_]{10,}"),
    re.compile(r"glpat-[A-Za-z0-9_-]{10,}"),
    re.compile(r"sk-ant-[A-Za-z0-9_-]{10,}"),
    re.compile(r"sk-[A-Za-z0-9]{20,}"),
    re.compile(r"(?i)\b(bearer|token|authorization|password|passwd|secret|api[_-]?key)\b(\s*[:=]\s*|\s+)\S+"),
    re.compile(r"https?://[^/\s:@]+:[^@\s]+@"),       # credentials in a URL
    re.compile(r"\b[A-Za-z0-9+/_-]{32,}={0,2}"),      # long opaque strings (keys, tokens, hashes)
]
NOISE = re.compile(r"^(##\[(group|endgroup|debug)\]|\[command\]|Cleaning up orphan processes|"
                   r"Temporarily overriding HOME|Adding repository directory|.*DeprecationWarning|"
                   r"\(Use `node --trace-deprecation)")


class _NoRedirect(urllib.request.HTTPRedirectHandler):
    def redirect_request(self, *a, **k):
        return None


def get(url, raw=False):
    """GET with the token. A job-log download answers 302 to a signed storage URL that must be fetched
    WITHOUT the Authorization header (urllib would forward it and the storage rejects the request)."""
    req = urllib.request.Request(url, headers={"Authorization": f"Bearer {os.environ['GITHUB_TOKEN']}",
                                               "Accept": "application/vnd.github+json"})
    try:
        with urllib.request.build_opener(_NoRedirect).open(req, timeout=60) as r:
            data = r.read()
    except urllib.error.HTTPError as e:
        if e.code not in (301, 302, 303, 307, 308) or not e.headers.get("Location"):
            raise
        with urllib.request.urlopen(e.headers["Location"], timeout=60) as r:
            data = r.read()
    return data.decode("utf-8", "replace") if raw else json.loads(data)


def mask(line):
    for rx in TOKENISH:
        line = rx.sub("***", line)
    return line


def screen(log_text, n=SCREEN_LINES):
    lines = []
    for raw in log_text.splitlines():
        line = ANSI.sub("", TS.sub("", raw)).rstrip()
        if line.startswith("Post job cleanup."):
            break
        if not line.strip() or NOISE.match(line):
            continue
        lines.append(mask(line)[:200])
    return "\n".join(lines[-n:])


def os_of(job):
    m = re.search(r"\((windows|macos|ubuntu)[^,)]*", job.get("name", ""))
    if m:
        return m.group(0)[1:]
    for lab in job.get("labels") or []:
        if lab.startswith(("windows", "macos", "ubuntu")):
            return lab
    return None


def build(repo, run_id, fetch=get):
    run = fetch(f"{API}/repos/{repo}/actions/runs/{run_id}")
    jobs = fetch(f"{API}/repos/{repo}/actions/runs/{run_id}/jobs?per_page=100").get("jobs", [])
    by_os = {}
    for j in jobs:
        o = os_of(j)
        if o:
            by_os.setdefault(o, []).append(j)
    rows = []
    for o in sorted(by_os, key=lambda x: (not x.startswith("windows"), not x.startswith("macos"), x)):
        js = by_os[o]
        concl = [j.get("conclusion") or j.get("status") for j in js]
        conclusion = "success" if all(c == "success" or c == "skipped" for c in concl) and "success" in concl \
            else next((c for c in concl if c not in ("success", "skipped")), "failure")
        # the screen shows the first failing job of this OS, else its main harness-qa job
        main = next((j for j in js if (j.get("conclusion") or "") not in ("success", "skipped")), None) \
            or next((j for j in js if j.get("name", "").startswith("harness-qa")), js[-1])
        try:
            last = screen(fetch(f"{API}/repos/{repo}/actions/jobs/{main['id']}/logs", raw=True))
        except Exception as e:  # logs expire or are not ready yet
            last = f"(log unavailable: {type(e).__name__})"
        finished = max((j.get("completed_at") or "" for j in js), default="") or None
        rows.append({"os": o, "run_id": str(run_id), "conclusion": conclusion, "finished_at": finished,
                     "job": main.get("name"), "jobs": [{"name": j.get("name"), "conclusion": j.get("conclusion")} for j in js],
                     "last_screen": last})
    doc = {"source": "harness-windows-qa (public SDK)",
           "generated_at": datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
           "run_id": str(run_id), "run_url": run.get("html_url"), "rows": rows}
    n = SCREEN_LINES
    while len(json.dumps(doc, ensure_ascii=False).encode()) > MAX_BYTES and n > 0:
        n -= 2
        for r in doc["rows"]:
            r["last_screen"] = "\n".join(r["last_screen"].splitlines()[-n:]) if n else ""
    return doc


if __name__ == "__main__":
    # No arguments (the repo script-selftest runs every changed script that way) or --self-test: run the self-test.
    if len(sys.argv) == 1 or (len(sys.argv) == 2 and sys.argv[1] == "--self-test"):
        log = ("2026-11-05T01:00:00.0Z \x1b[32mRESULT ok\x1b[0m\n2026-11-05T01:00:01.0Z token: ghp_abcdefghijklmnopqrstuv\n"
               "2026-11-05T01:00:01.0Z ##[group]x\n2026-11-05T01:00:02.0Z CASE 3 pass key=" + "A" * 40 + "\n"
               "2026-11-05T01:00:03.0Z Post job cleanup.\n2026-11-05T01:00:04.0Z after cleanup\n")
        s = screen(log)
        assert s.splitlines() == ["RESULT ok", "***", "CASE 3 pass key=***"], s
        jobs = {"jobs": [
            {"id": 1, "name": "harness-qa (windows-latest)", "conclusion": "success", "completed_at": "2026-11-05T01:00:00Z"},
            {"id": 2, "name": "daily-tools (windows-latest)", "conclusion": "success", "completed_at": "2026-11-05T01:01:00Z"},
            {"id": 3, "name": "harness-qa (macos-latest)", "conclusion": "failure", "completed_at": "2026-11-05T01:02:00Z"},
            {"id": 4, "name": "portal-selftest (macos-latest)", "conclusion": "skipped", "completed_at": None}]}
        fake = lambda url, raw=False: log if raw else (jobs if "/jobs?" in url else {"html_url": "u"})
        d = build("o/r", 9, fake)
        assert [(r["os"], r["conclusion"], r["finished_at"]) for r in d["rows"]] == [
            ("windows-latest", "success", "2026-11-05T01:01:00Z"), ("macos-latest", "failure", "2026-11-05T01:02:00Z")], d
        big = build("o/r", 9, lambda url, raw=False: ("x" * 150 + " Z\n") * 500 if raw else fake(url))
        assert len(json.dumps(big).encode()) <= MAX_BYTES
        print("os_matrix self-test: ok")
        sys.exit(0)
    repo, run_id, out = sys.argv[1:4]
    doc = build(repo, run_id)
    with open(out, "w", encoding="utf-8") as f:
        json.dump(doc, f, ensure_ascii=False, indent=1)
    print(f"os-matrix.json: {len(doc['rows'])} rows, {os.path.getsize(out)} bytes, " +
          ", ".join(f"{r['os']}={r['conclusion']}" for r in doc["rows"]))
