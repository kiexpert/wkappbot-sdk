#!/usr/bin/env python3
"""Assemble release-status.json for the portal release board.

Usage: clush-release-status.py <pipeline_id> <pipeline_status> <jobs.json> <cluster-metrics.json> [rollback-timeline.json]

- jobs.json: the GitLab pipeline jobs array (has name, status, duration).
- cluster-metrics.json: best-effort metrics written by the deploy job (may be missing/empty).
- rollback-timeline.json: the real Argo CD deploy->upgrade->rollback timeline (optional/best-effort).

Prints the assembled JSON to stdout, in the shape the portal's existing
poc-site/portal/deploy-proof.json already reads. Never raises on missing cluster metrics.
"""
import sys
import json
import datetime


def main() -> int:
    plid = sys.argv[1] if len(sys.argv) > 1 else ""
    status = sys.argv[2] if len(sys.argv) > 2 else ""
    jobs_path = sys.argv[3] if len(sys.argv) > 3 else ""
    cm_path = sys.argv[4] if len(sys.argv) > 4 else ""
    rb_path = sys.argv[5] if len(sys.argv) > 5 else ""

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

    rollback = None
    try:
        rollback = json.load(open(rb_path))
    except Exception:
        rollback = None
    # the deploy job also embeds the timeline inside cluster-metrics.json, so read it from there
    # when no separate file is passed (keeps the workflow plumbing unchanged)
    if rollback is None and isinstance(cm.get("rollback"), dict):
        rollback = cm.get("rollback")

    now_iso = datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ")
    sync, _, health = (cm.get("argo_sync_health") or "").partition("/")

    # Shape matches the portal's existing poc-site/portal/deploy-proof.json keys so the release
    # board reads it with no transform: required run + argocd + cluster, optional source labels,
    # plus the extra measured fields. The portal keeps its own "runners" block when it merges this.
    out = {
        "run": plid,
        "provider": "GitLab",
        "pipeline": plid,
        "generated_at": now_iso,
        "argocd": {
            "app": (rollback or {}).get("app") or "podinfo",
            "sync": sync or None,
            "health": health or None,
            "revision": cm.get("app_revision") or None,
        },
        "cluster": {
            "tool": "kind",
            "name": cm.get("cluster_name") or None,
            "kubectl": cm.get("kubectl_version") or None,
            "version": cm.get("app_image") or None,
        },
        "deployedAt": cm.get("cluster_timestamp") or None,
        "podsReady": cm.get("pods_ready") or None,
        "cpu": cm.get("top_cpu") or None,
        "mem": cm.get("top_mem") or None,
        "latencyMs": {"p50": cm.get("latency_ms_p50"), "p95": cm.get("latency_ms_p95")},
        "pipeline_status": status,
        "stages": stages,
        "latency_samples": cm.get("samples"),
    }
    # The real Argo CD deploy -> upgrade -> rollback timeline, for the release board's rollback card.
    if rollback:
        out["rollback"] = rollback
    print(json.dumps(out, indent=2))
    return 0


if __name__ == "__main__":
    sys.exit(main())
