#!/usr/bin/env python3
"""Assemble svc-health.json for the portal service board from READ-ONLY GitHub Actions metadata.

Usage: svc-health.py <core-runs.json> <relay-runs.json> [screenshot_path]

Inputs are the raw `GET .../actions/runs` responses for the two services. This script computes ONLY
operational health (status/load/last_ok/failures_24h/chain_gap_s/run_duration_s/queue_wait_s) and NEVER
emits any order, position, balance, P&L, ticker or account field (it never reads any such data). Never
raises on malformed input; a service it cannot read is reported red/unknown.
"""
import sys
import json
import datetime

NOW = datetime.datetime.now(datetime.timezone.utc)
FAIL = {"failure", "timed_out", "startup_failure"}


def _dt(s):
    try:
        return datetime.datetime.fromisoformat(s.replace("Z", "+00:00"))
    except Exception:
        return None


def _runs(path):
    try:
        return json.load(open(path)).get("workflow_runs", []) or []
    except Exception:
        return []


def _within_24h(dt):
    return dt is not None and (NOW - dt).total_seconds() <= 86400


def core_health(path):
    runs = _runs(path)
    failures_24h = sum(1 for r in runs
                       if r.get("conclusion") in FAIL and _within_24h(_dt(r.get("created_at", ""))))
    oks = [_dt(r.get("updated_at", "")) for r in runs if r.get("conclusion") == "success"]
    oks = [d for d in oks if d]
    last_ok = max(oks).strftime("%Y-%m-%dT%H:%M:%SZ") if oks else None
    in_progress = sum(1 for r in runs if r.get("status") == "in_progress")
    latest = runs[0] if runs else {}
    if not runs or not last_ok or not _within_24h(max(oks)):
        status = "red"
    elif latest.get("conclusion") in FAIL or failures_24h > 0:
        status = "yellow"
    else:
        status = "green"
    return {
        "service": "trading-core",
        "status": status,
        "load": in_progress,
        "last_ok": last_ok,
        "failures_24h": failures_24h,
        "chain_gap_s": None,
        "screenshot": None,
    }


def relay_health(path, shot):
    runs = _runs(path)
    failures_24h = sum(1 for r in runs
                       if r.get("conclusion") in FAIL and _within_24h(_dt(r.get("created_at", ""))))
    oks = [_dt(r.get("updated_at", "")) for r in runs if r.get("conclusion") == "success"]
    oks = [d for d in oks if d]
    last_ok = max(oks).strftime("%Y-%m-%dT%H:%M:%SZ") if oks else None
    in_progress = any(r.get("status") == "in_progress" for r in runs)

    created = [_dt(r.get("created_at", "")) for r in runs]
    created = [d for d in created if d]
    chain_gap_s = int((created[0] - created[1]).total_seconds()) if len(created) >= 2 else None

    latest = runs[0] if runs else {}
    c = _dt(latest.get("created_at", "")); s = _dt(latest.get("run_started_at", "")); u = _dt(latest.get("updated_at", ""))
    queue_wait_s = int((s - c).total_seconds()) if (s and c) else None
    run_duration_s = int((u - s).total_seconds()) if (u and s and latest.get("status") == "completed") else None

    # load label from how tightly the relay is self-chaining (smaller gap = busier); no financial meaning
    if in_progress or (chain_gap_s is not None and chain_gap_s <= 120):
        load = "busy"
    elif chain_gap_s is not None and chain_gap_s <= 900:
        load = "active"
    else:
        load = "idle"

    recent = _within_24h(created[0]) if created else False
    if not runs or not recent:
        status = "red"
    elif latest.get("conclusion") in FAIL or failures_24h > 0:
        status = "yellow"
    else:
        status = "green"

    return {
        "service": "scalper-relay",
        "status": status,
        "load": load,
        "last_ok": last_ok,
        "failures_24h": failures_24h,
        "chain_gap_s": chain_gap_s,
        "run_duration_s": run_duration_s,
        "queue_wait_s": queue_wait_s,
        "in_progress": in_progress,
        "screenshot": (shot if shot else None),
    }


def main() -> int:
    core_path = sys.argv[1] if len(sys.argv) > 1 else ""
    relay_path = sys.argv[2] if len(sys.argv) > 2 else ""
    shot = sys.argv[3] if len(sys.argv) > 3 else ""
    import os
    shot = shot if (shot and os.path.exists(shot)) else None
    out = {
        "generated_at": NOW.strftime("%Y-%m-%dT%H:%M:%SZ"),
        "source": "github-actions read-only metadata",
        "services": [core_health(core_path), relay_health(relay_path, shot)],
    }
    print(json.dumps(out, indent=2))
    return 0


if __name__ == "__main__":
    sys.exit(main())
