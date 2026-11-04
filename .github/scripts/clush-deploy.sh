#!/usr/bin/env bash
# Real Kubernetes deploy proof for the Clush PoC, run by the clush-poc GitLab pipeline's deploy job
# on a tagged shell-executor runner (kind needs the host docker daemon). It creates a kind cluster,
# installs Argo CD (pinned), deploys the project's nginx "notice" app with kubectl and verifies it
# serves, then runs a REAL GitOps release cycle on a public Helm chart managed by Argo CD --
# deploy v1 -> upgrade v2 -> `argocd app rollback` back to v1 -- checking health and the served
# version at every step and recording the timeline in deploy-proof/rollback-timeline.json. It also
# installs metrics-server and gathers release metrics (best-effort) into
# deploy-proof/cluster-metrics.json, then saves kubectl/argocd state under deploy-proof/.
# No credential is ever placed on the cluster (the Helm repo is read anonymously).
set -euo pipefail

ARGOCD_VERSION="v2.13.3"
CLUSTER="portal-demo"
PODINFO_REPO="https://stefanprodan.github.io/podinfo"
PODINFO_V1="6.5.0"
PODINFO_V2="6.5.4"
export KUBECONFIG="${CI_PROJECT_DIR:-$PWD}/kubeconfig"
mkdir -p deploy-proof

echo "== kind create cluster =="
kind create cluster --name "$CLUSTER" --kubeconfig "$KUBECONFIG" --wait 120s
kubectl cluster-info

echo "== install Argo CD $ARGOCD_VERSION =="
kubectl create namespace argocd
kubectl apply -n argocd -f "https://raw.githubusercontent.com/argoproj/argo-cd/${ARGOCD_VERSION}/manifests/install.yaml"
kubectl -n argocd wait --for=condition=Available deployment --all --timeout=300s

echo "== install argocd CLI $ARGOCD_VERSION (core mode: talks to the kube API, no server login) =="
curl -sSLo ./argocd "https://github.com/argoproj/argo-cd/releases/download/${ARGOCD_VERSION}/argocd-linux-amd64"
sudo install ./argocd /usr/local/bin/argocd
argocd version --client 2>/dev/null | head -1 || true
# core mode reads the Argo CD control-plane config (argocd-cm) from the namespace Argo CD is installed
# in; point both the CLI and the kube-context default at it so `argocd app rollback` finds argocd-cm.
export ARGOCD_OPTS='--core'
export ARGOCD_NAMESPACE=argocd
kubectl config set-context --current --namespace=argocd >/dev/null 2>&1 || true

echo "== deploy the clush notice app (kubectl) =="
cat > demo.json <<'JSON'
{ "apiVersion": "v1", "kind": "List", "items": [
  { "apiVersion": "v1", "kind": "Namespace", "metadata": { "name": "clush-demo" } },
  { "apiVersion": "v1", "kind": "ConfigMap", "metadata": { "name": "notice-page", "namespace": "clush-demo" },
    "data": { "index.html": "<!doctype html><meta charset=\"utf-8\"><title>Clush PoC</title><h1>S customer notice portal</h1><p>deployed to Kubernetes via GitLab CI + Argo CD</p>" } },
  { "apiVersion": "apps/v1", "kind": "Deployment", "metadata": { "name": "notice", "namespace": "clush-demo", "labels": { "app": "notice" } },
    "spec": { "replicas": 1, "selector": { "matchLabels": { "app": "notice" } },
      "template": { "metadata": { "labels": { "app": "notice" } },
        "spec": { "containers": [ { "name": "nginx", "image": "nginx:1.27-alpine", "ports": [ { "containerPort": 80 } ],
          "volumeMounts": [ { "name": "page", "mountPath": "/usr/share/nginx/html" } ],
          "resources": { "requests": { "cpu": "10m", "memory": "16Mi" } } } ],
          "volumes": [ { "name": "page", "configMap": { "name": "notice-page" } } ] } } } },
  { "apiVersion": "v1", "kind": "Service", "metadata": { "name": "notice", "namespace": "clush-demo" },
    "spec": { "selector": { "app": "notice" }, "ports": [ { "port": 80, "targetPort": 80 } ] } }
] }
JSON
kubectl apply -f demo.json
kubectl -n clush-demo rollout status deployment/notice --timeout=180s

echo "== serve check via probe pod =="
kubectl -n clush-demo run probe --image=curlimages/curl:8.11.1 --restart=Never --command -- \
  sh -c "curl -sf http://notice.clush-demo.svc.cluster.local/"
ph=""
for i in $(seq 1 30); do
  ph="$(kubectl -n clush-demo get pod probe -o jsonpath='{.status.phase}' 2>/dev/null || echo "")"
  echo "probe phase: $ph"
  case "$ph" in
    Succeeded) break ;;
    Failed) kubectl -n clush-demo logs probe || true; echo "serve check failed"; exit 1 ;;
  esac
  sleep 3
done
echo "-- probe log --"; kubectl -n clush-demo logs probe || true
kubectl -n clush-demo delete pod probe --ignore-not-found
[ "$ph" = "Succeeded" ] || { echo "serve check did not succeed (phase=$ph)"; exit 1; }

# ===================== REAL Argo CD deploy -> upgrade -> rollback cycle =====================
# podinfo is a public Helm chart (many published versions, a /version endpoint and a version-tagged
# image), read anonymously by Argo CD, so the whole cycle is real and needs no cluster credential.
echo "== Argo CD release cycle: deploy v$PODINFO_V1 -> upgrade v$PODINFO_V2 -> rollback v$PODINFO_V1 =="

cat > podinfo.json <<JSON
{ "apiVersion": "argoproj.io/v1alpha1", "kind": "Application",
  "metadata": { "name": "podinfo", "namespace": "argocd" },
  "spec": {
    "project": "default",
    "source": { "repoURL": "$PODINFO_REPO", "chart": "podinfo", "targetRevision": "$PODINFO_V1" },
    "destination": { "server": "https://kubernetes.default.svc", "namespace": "podinfo" },
    "syncPolicy": { "automated": {}, "syncOptions": [ "CreateNamespace=true" ] } } }
JSON
# Argo CD's own application-controller drives deploy + upgrade (declarative automated sync), so the
# cycle does not depend on an argocd CLI `sync` succeeding; the CLI is used only for the rollback.
kubectl apply -f podinfo.json

# served version = the running image tag (authoritative: what the cluster actually runs)
served_ver() { kubectl -n podinfo get deploy podinfo -o jsonpath='{.spec.template.spec.containers[0].image}' 2>/dev/null | sed 's/.*://'; }
app_sync_health() { kubectl -n argocd get application podinfo -o jsonpath='{.status.sync.status}/{.status.health.status}' 2>/dev/null || echo "/"; }
# wait until the cluster actually runs $1 AND Argo reports Synced/Healthy
wait_version() {
  local want="$1" sh sv
  for _ in $(seq 1 90); do
    sh="$(app_sync_health)"; sv="$(served_ver)"
    echo "   podinfo: status=$sh served=$sv (want $want)"
    [ "$sh" = "Synced/Healthy" ] && [ "$sv" = "$want" ] && return 0
    sleep 5
  done
  echo "   (timed out) last: $(app_sync_health) served=$(served_ver)"
  argocd app get podinfo 2>/dev/null | head -30 || true
  kubectl -n podinfo get pods 2>/dev/null || true
  return 1
}

: > /tmp/rollback_steps.jsonl
record_step() { # step expected_version method
  local step="$1" want="$2" method="$3" sh sv ok at
  sh="$(app_sync_health)"; sv="$(served_ver)"; at="$(date -u +%Y-%m-%dT%H:%M:%SZ)"
  ok=true; [ "$sv" = "$want" ] || ok=false; [ "$sh" = "Synced/Healthy" ] || ok=false
  python3 - "$step" "$want" "$sv" "$sh" "$method" "$ok" "$at" >> /tmp/rollback_steps.jsonl <<'PY'
import json, sys
step, want, served, sh, method, ok, at = sys.argv[1:8]
sync, _, health = sh.partition("/")
print(json.dumps({"step": step, "method": method, "targetVersion": want, "servedVersion": served,
                  "sync": sync or None, "health": health or None, "ok": ok == "true", "at": at}))
PY
  echo "   step=$step want=$want served=$sv status=$sh ok=$ok"
  [ "$ok" = "true" ] || { echo "release-cycle step '$step' FAILED (served=$sv want=$want status=$sh)"; return 1; }
}

echo "-- v1: Argo CD auto-syncs chart $PODINFO_V1 --"
wait_version "$PODINFO_V1" || { echo "podinfo v1 never reached Synced/Healthy at $PODINFO_V1"; exit 1; }
kubectl -n podinfo rollout status deployment/podinfo --timeout=180s
record_step "deploy_v1" "$PODINFO_V1" "Argo CD auto-sync" || exit 1

echo "-- v2: set targetRevision $PODINFO_V2; Argo CD auto-syncs the upgrade --"
kubectl -n argocd patch application podinfo --type merge -p "{\"spec\":{\"source\":{\"targetRevision\":\"$PODINFO_V2\"}}}"
wait_version "$PODINFO_V2" || { echo "podinfo v2 never reached Synced/Healthy at $PODINFO_V2"; exit 1; }
kubectl -n podinfo rollout status deployment/podinfo --timeout=180s
record_step "upgrade_v2" "$PODINFO_V2" "patch targetRevision + Argo CD auto-sync" || exit 1

echo "-- rollback: argocd app rollback to the v1 deployment --"
# turn OFF automated sync first so the rollback is not immediately re-synced forward to v2
kubectl -n argocd patch application podinfo --type json -p '[{"op":"remove","path":"/spec/syncPolicy/automated"}]' || true
argocd app history podinfo || true
# pick the earliest history id whose revision is the v1 chart version (fallback to id 0)
v1_id="$(argocd app history podinfo 2>/dev/null | awk -v v="$PODINFO_V1" 'NR>1 && $NF==v {print $1; exit}' || true)"
[ -n "${v1_id:-}" ] || v1_id=0
echo "   rolling back to history id $v1_id"
rb_method="argocd app rollback"
argocd app rollback podinfo "$v1_id" || { echo "argocd app rollback errored; the targetRevision realign below still completes the rollback"; rb_method="argocd app rollback (kubectl fallback)"; }
# `argocd app rollback` reverts the workload to the v1 manifests but leaves spec.source.targetRevision
# at v2, so Argo reports OutOfSync; realign the declared target to v1 and re-enable auto-sync so the
# app reconciles to Synced/Healthy at v1 (and if the CLI errored, the controller converges here too).
kubectl -n argocd patch application podinfo --type merge -p "{\"spec\":{\"source\":{\"targetRevision\":\"$PODINFO_V1\"},\"syncPolicy\":{\"automated\":{}}}}"
wait_version "$PODINFO_V1" || { echo "podinfo rollback never reached Synced/Healthy at $PODINFO_V1"; exit 1; }
kubectl -n podinfo rollout status deployment/podinfo --timeout=180s
record_step "rollback_v1" "$PODINFO_V1" "$rb_method" || exit 1

echo "-- assemble rollback-timeline.json --"
python3 - "$PODINFO_REPO" "$PODINFO_V1" "$PODINFO_V2" > deploy-proof/rollback-timeline.json <<'PY'
import json, sys
repo, v1, v2 = sys.argv[1:4]
steps = [json.loads(l) for l in open("/tmp/rollback_steps.jsonl") if l.strip()]
out = {
    "app": "podinfo",
    "tool": "Argo CD",
    "chart": "podinfo (%s)" % repo,
    "fromVersion": v1, "upgradedTo": v2, "rolledBackTo": v1,
    "steps": steps,
    "ok": all(s.get("ok") for s in steps) and len(steps) == 3,
}
print(json.dumps(out, indent=2))
PY
echo "---- rollback-timeline.json ----"; cat deploy-proof/rollback-timeline.json
python3 -c "import json,sys; sys.exit(0 if json.load(open('deploy-proof/rollback-timeline.json'))['ok'] else 1)" \
  || { echo "rollback timeline not ok"; exit 1; }

# serve check: podinfo answers on its /version after the rollback
kubectl -n podinfo port-forward svc/podinfo 19898:9898 >/dev/null 2>&1 &
pfp=$!
sleep 3
pv="$(curl -s "http://127.0.0.1:19898/version" 2>/dev/null || echo "")"
kill "$pfp" 2>/dev/null || true
echo "podinfo served /version after rollback: $pv"

# final Argo status for the cluster-metrics block below
s="$(app_sync_health)"
# ===================== end release cycle =====================

# ---- best-effort release metrics (never fail the deploy on these) ----
set +e
echo "== metrics-server (best-effort) =="
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml
kubectl -n kube-system patch deployment metrics-server --type=json \
  -p='[{"op":"add","path":"/spec/template/spec/containers/0/args/-","value":"--kubelet-insecure-tls"}]'
kubectl -n kube-system rollout status deployment/metrics-server --timeout=180s

echo "== gather release metrics (best-effort) =="
rev="$(kubectl -n argocd get application podinfo -o jsonpath='{.status.sync.revision}' 2>/dev/null)"
pods_ready="$(kubectl -n clush-demo get deploy notice -o jsonpath='{.status.readyReplicas}/{.status.replicas}' 2>/dev/null)"
app_image="$(kubectl -n clush-demo get deploy notice -o jsonpath='{.spec.template.spec.containers[0].image}' 2>/dev/null)"
kube_ctx="$(kubectl config current-context 2>/dev/null)"
kubectl_ver="$(kubectl version --client -o json 2>/dev/null | python3 -c "import sys,json;print(json.load(sys.stdin).get('clientVersion',{}).get('gitVersion',''))" 2>/dev/null)"
top_cpu=""; top_mem=""
for i in $(seq 1 25); do
  line="$(kubectl -n clush-demo top pod -l app=notice --no-headers 2>/dev/null | head -1)"
  if [ -n "$line" ]; then
    top_cpu="$(echo "$line" | awk '{print $2}')"
    top_mem="$(echo "$line" | awk '{print $3}')"
    break
  fi
  sleep 6
done
kubectl -n clush-demo port-forward svc/notice 18080:80 >/dev/null 2>&1 &
pf=$!
sleep 3
: > /tmp/lat.txt
for i in $(seq 1 20); do
  t="$(curl -s -o /dev/null -w '%{time_total}' "http://127.0.0.1:18080/" 2>/dev/null)"
  [ -n "$t" ] && echo "$t" >> /tmp/lat.txt
done
kill "$pf" 2>/dev/null
p50="$(python3 -c "import math;v=sorted(float(x) for x in open('/tmp/lat.txt') if x.strip());print(round(v[max(0,math.ceil(0.50*len(v))-1)]*1000,1)) if v else print('')" 2>/dev/null)"
p95="$(python3 -c "import math;v=sorted(float(x) for x in open('/tmp/lat.txt') if x.strip());print(round(v[max(0,math.ceil(0.95*len(v))-1)]*1000,1)) if v else print('')" 2>/dev/null)"
samples="$(grep -c . /tmp/lat.txt 2>/dev/null)"
export CM_S="$s" CM_REV="$rev" CM_PR="$pods_ready" CM_IMG="$app_image" CM_CTX="$kube_ctx" CM_KV="$kubectl_ver" CM_CPU="$top_cpu" CM_MEM="$top_mem" CM_P50="$p50" CM_P95="$p95" CM_N="$samples"
python3 - <<'PY'
import json, os, datetime
d = {
  "argo_sync_health": os.environ.get("CM_S", ""),
  "app_revision": os.environ.get("CM_REV", ""),
  "pods_ready": os.environ.get("CM_PR", ""),
  "app_image": os.environ.get("CM_IMG", ""),
  "cluster_name": os.environ.get("CM_CTX", ""),
  "kubectl_version": os.environ.get("CM_KV", ""),
  "top_cpu": os.environ.get("CM_CPU", ""),
  "top_mem": os.environ.get("CM_MEM", ""),
  "latency_ms_p50": os.environ.get("CM_P50", ""),
  "latency_ms_p95": os.environ.get("CM_P95", ""),
  "samples": int(os.environ.get("CM_N") or 0),
  "cluster_timestamp": datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
}
# carry the real deploy->upgrade->rollback timeline inside the metrics the pipeline already collects,
# so the release-status assembler publishes it with no extra workflow plumbing
try:
    d["rollback"] = json.load(open("deploy-proof/rollback-timeline.json"))
except Exception:
    pass
json.dump(d, open("deploy-proof/cluster-metrics.json", "w"), indent=2)
print("cluster-metrics.json:", json.dumps(d))
PY
set -e
# ---- end best-effort metrics ----

echo "== capture artifacts =="
echo "$s" > deploy-proof/argocd-status.txt
kubectl get all -A > deploy-proof/kubectl-get-all.txt 2>&1 || true
kubectl -n clush-demo get all -o wide > deploy-proof/clush-demo.txt 2>&1 || true
kubectl -n podinfo get all -o wide > deploy-proof/podinfo.txt 2>&1 || true
kubectl -n argocd get application podinfo -o yaml > deploy-proof/argocd-app.yaml 2>&1 || true
cp demo.json podinfo.json deploy-proof/ 2>/dev/null || true

echo "FINAL argocd status: $s"
[ "$s" = "Synced/Healthy" ] || { echo "Argo app never reached Synced/Healthy"; exit 1; }
echo "deploy proof complete (release cycle: deploy v$PODINFO_V1 -> upgrade v$PODINFO_V2 -> rollback v$PODINFO_V1)"
