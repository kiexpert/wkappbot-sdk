#!/usr/bin/env bash
# Real Kubernetes deploy proof for the Clush PoC, run by the clush-poc GitLab pipeline's deploy job
# on a tagged shell-executor runner (kind needs the host docker daemon). It creates a kind cluster,
# installs Argo CD (pinned), deploys the project's nginx "notice" app with kubectl and verifies it
# serves, applies an Argo CD Application (public example app) and waits for Synced/Healthy, installs
# metrics-server and gathers release metrics (best-effort) into deploy-proof/cluster-metrics.json,
# then saves kubectl/argocd state under deploy-proof/. No credential is ever placed on the cluster.
set -euo pipefail

ARGOCD_VERSION="v2.13.3"
CLUSTER="portal-demo"
export KUBECONFIG="${CI_PROJECT_DIR:-$PWD}/kubeconfig"
mkdir -p deploy-proof

echo "== kind create cluster =="
kind create cluster --name "$CLUSTER" --kubeconfig "$KUBECONFIG" --wait 120s
kubectl cluster-info

echo "== install Argo CD $ARGOCD_VERSION =="
kubectl create namespace argocd
kubectl apply -n argocd -f "https://raw.githubusercontent.com/argoproj/argo-cd/${ARGOCD_VERSION}/manifests/install.yaml"
kubectl -n argocd wait --for=condition=Available deployment --all --timeout=300s

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

echo "== Argo CD Application (public example app) =="
cat > app.json <<'JSON'
{ "apiVersion": "argoproj.io/v1alpha1", "kind": "Application",
  "metadata": { "name": "guestbook", "namespace": "argocd" },
  "spec": {
    "project": "default",
    "source": { "repoURL": "https://github.com/argoproj/argocd-example-apps.git", "targetRevision": "HEAD", "path": "guestbook" },
    "destination": { "server": "https://kubernetes.default.svc", "namespace": "guestbook" },
    "syncPolicy": { "automated": {}, "syncOptions": [ "CreateNamespace=true" ] } } }
JSON
kubectl apply -f app.json
s="/"
for i in $(seq 1 60); do
  s="$(kubectl -n argocd get application guestbook -o jsonpath='{.status.sync.status}/{.status.health.status}' 2>/dev/null || echo "/")"
  echo "argo guestbook: $s"
  [ "$s" = "Synced/Healthy" ] && break
  sleep 5
done

# ---- best-effort release metrics (never fail the deploy on these) ----
set +e
echo "== metrics-server (best-effort) =="
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml
kubectl -n kube-system patch deployment metrics-server --type=json \
  -p='[{"op":"add","path":"/spec/template/spec/containers/0/args/-","value":"--kubelet-insecure-tls"}]'
kubectl -n kube-system rollout status deployment/metrics-server --timeout=180s

echo "== gather release metrics (best-effort) =="
rev="$(kubectl -n argocd get application guestbook -o jsonpath='{.status.sync.revision}' 2>/dev/null)"
pods_ready="$(kubectl -n clush-demo get deploy notice -o jsonpath='{.status.readyReplicas}/{.status.replicas}' 2>/dev/null)"
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
export CM_S="$s" CM_REV="$rev" CM_PR="$pods_ready" CM_CPU="$top_cpu" CM_MEM="$top_mem" CM_P50="$p50" CM_P95="$p95" CM_N="$samples"
python3 - <<'PY'
import json, os, datetime
d = {
  "argo_sync_health": os.environ.get("CM_S", ""),
  "app_revision": os.environ.get("CM_REV", ""),
  "pods_ready": os.environ.get("CM_PR", ""),
  "top_cpu": os.environ.get("CM_CPU", ""),
  "top_mem": os.environ.get("CM_MEM", ""),
  "latency_ms_p50": os.environ.get("CM_P50", ""),
  "latency_ms_p95": os.environ.get("CM_P95", ""),
  "samples": int(os.environ.get("CM_N") or 0),
  "cluster_timestamp": datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
}
json.dump(d, open("deploy-proof/cluster-metrics.json", "w"), indent=2)
print("cluster-metrics.json:", json.dumps(d))
PY
set -e
# ---- end best-effort metrics ----

echo "== capture artifacts =="
echo "$s" > deploy-proof/argocd-status.txt
kubectl get all -A > deploy-proof/kubectl-get-all.txt 2>&1 || true
kubectl -n clush-demo get all -o wide > deploy-proof/clush-demo.txt 2>&1 || true
kubectl -n argocd get application guestbook -o yaml > deploy-proof/argocd-app.yaml 2>&1 || true
cp demo.json app.json deploy-proof/ 2>/dev/null || true

echo "FINAL argocd status: $s"
[ "$s" = "Synced/Healthy" ] || { echo "Argo app never reached Synced/Healthy"; exit 1; }
echo "deploy proof complete"
