#!/usr/bin/env python3
"""Assemble release-status.json for the portal release board.

Usage: clush-release-status.py <pipeline_id> <pipeline_status> <jobs.json> <cluster-metrics.json>

- jobs.json: the GitLab pipeline jobs array (has name, status, duration).
- cluster-metrics.json: best-effort metrics written by the deploy job (may be missing/empty).

Prints the assembled JSON to stdout. The primary payload is a "cd" block in the schema the
portal's release board already reads (poc-site/portal/deploy-proof.json). Never raises on
missing cluster metrics.
"""
import sys
import json
import datetime


def main() -> int:
    plid = sys.argv[1] if len(sys.argv) > 1 else ""
    status = sys.argv[2] if len(sys.argv) > 2 else ""
    jobs_path = sys.argv[3] if len(sys.argv) > 3 else ""
    cm_path = sys.argv[4] if len(sys.argv) > 4 else ""

    stages = {}
    try:
        for j in json.load(open(jobs_path)):
            stages[j["name"]] = {"status": j.get("status"), "duration_s": j.get("duration")}
    except Exception:
        pass

    cm = {}
    try:
        cm = json.load(open(cm_path))
    except Exception:
        cm = {}

    now_iso = datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ")
    sync, _, health = (cm.get("argo_sync_health") or "").partition("/")

    # Primary payload: the "cd" block in the schema the portal's release board already reads
    # (poc-site/portal/deploy-proof.json). Extra fields (pipeline_status, stages) are siblings
    # the board ignores.
    onprem = {
        "version": cm.get("app_image") or None,
        "argoApp": "guestbook",
        "sync": sync or None,
        "health": health or None,
        "revision": cm.get("app_revision") or None,
        "deployedAt": cm.get("cluster_timestamp") or None,
        "podsReady": cm.get("pods_ready") or None,
        "cpu": cm.get("top_cpu") or None,
        "mem": cm.get("top_mem") or None,
        "latencyMs": {"p50": cm.get("latency_ms_p50"), "p95": cm.get("latency_ms_p95")},
    }
    out = {
        "cd": {
            "provider": "gitlab",
            "pipeline": plid,
            "measured_at": now_iso,
            "clusters": {"onprem": onprem},
        },
        "pipeline_status": status,
        "project": "kiexpert-group/clush-poc",
        "stages": stages,
        "latency_samples": cm.get("samples"),
        "generated_at": now_iso,
    }
    print(json.dumps(out, indent=2))
    return 0


if __name__ == "__main__":
    sys.exit(main())
