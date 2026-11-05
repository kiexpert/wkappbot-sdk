"""Write one desktop status record. Usage: status.py <state> [--force]
state: booting|up|down. Rate limit: same-state rewrites at most every 300 s; only the 3 state changes of a boot write sooner.
The record goes ONLY to the owner's private relay (an orphan branch force-pushed with one file), never to logs,
artifacts or this public repo. Env: REPO, TOK (private relay + token, both masked), DESKTOP_OS,
DESKTOP_DEADLINE (epoch s), DESKTOP_BOOTED_AT, DESKTOP_ROLE, GITHUB_RUN_ID."""
import json, os, subprocess, sys, tempfile, time
state = sys.argv[1]
d = os.environ.get("DESKTOP_DIR") or os.path.join(os.environ.get("RUNNER_TEMP", "/tmp"), "desktop")
os.makedirs(d, exist_ok=True)
mark = os.path.join(d, "status.last")
now = int(time.time())
last = open(mark).read().split() if os.path.exists(mark) else ["0", ""]
if state == last[1] and "--force" not in sys.argv and now - int(last[0]) < 300:
    sys.exit(0)  # same state: heartbeat at most every 5 min; state changes (booting/up/down, 3 per boot) write at once
osn = os.environ["DESKTOP_OS"]
deadline = int(os.environ.get("DESKTOP_DEADLINE") or 0)
booted = int(os.environ.get("DESKTOP_BOOTED_AT") or 0)
def iso(t): return time.strftime("%Y-%m-%dT%H:%M:%SZ", time.gmtime(t)) if t else None
url_f = os.path.join(d, "url")
rec = {
    "schema": "runner-desktop/1",
    "os": osn,
    "state": state,
    "role": os.environ.get("DESKTOP_ROLE", "qa"),
    "boot_time": iso(booted),
    "boot_seconds": int(os.environ.get("DESKTOP_BOOT_S") or 0) or None,
    "expires_at": iso(deadline),
    "seconds_to_shutdown": max(0, deadline - now) if deadline and state != "down" else 0,
    "viewer_url": open(url_f).read().strip() if state == "up" and os.path.exists(url_f) else None,
    "auth": "http-basic user=owner + vnc password (owner secret)",
    "run_id": os.environ.get("GITHUB_RUN_ID"),
    "updated_at": iso(now),
}
w = tempfile.mkdtemp()
open(os.path.join(w, "status.json"), "w").write(json.dumps(rec, indent=1) + "\n")
br = f"relay/desktop-{osn}"
remote = f"https://x-access-token:{os.environ['TOK']}@github.com/{os.environ['REPO']}.git"
g = lambda *a: subprocess.run(["git", "-C", w, *a], check=True, capture_output=True)
g("init", "-q", "-b", br); g("config", "user.name", "runner-desktop")
g("config", "user.email", "runner-desktop@users.noreply.github.com")
g("add", "status.json"); g("commit", "-q", "-m", f"{osn} {state} {iso(now)}")
for i in range(3):
    if subprocess.run(["git", "-C", w, "push", "-q", "-f", remote, f"HEAD:{br}"], capture_output=True).returncode == 0:
        open(mark, "w").write(f"{now} {state}")
        print(f"status {osn}={state} written"); sys.exit(0)
    time.sleep(5 * (i + 1))
print(f"status {osn}={state} write FAILED"); sys.exit(1)
