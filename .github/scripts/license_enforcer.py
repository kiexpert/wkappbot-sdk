#!/usr/bin/env python3
"""license_enforcer.py — Enforce GitHub collaborator permissions based on license expiry.

Runs on a schedule. For each .github/licenses/{user}.json:
  - sudo valid            → ensure "write" permission
  - sudo expired, cdp ok  → downgrade to "read", remove sudo key from file
  - both expired          → revoke collaborator access, delete file
Skips "admin" users (repo owners) to avoid accidental lockout.
All changes leave a git commit so history serves as an audit log.
"""
import base64, json, os, sys, urllib.request
from datetime import datetime, timezone

LICENSE_REPO  = "kiexpert/wkappbot-sdk"
SLACK_CHANNEL = "C0APNELH2LR"
GH_API        = "https://api.github.com"
GH_TOKEN      = os.environ.get("GH_LICENSE_TOKEN", "")
SLACK_TOKEN   = os.environ.get("SLACK_BOT_TOKEN", "")


def gh(method, path, body=None):
    url = f"{GH_API}{path}"
    data = json.dumps(body).encode() if body else None
    req = urllib.request.Request(url, data=data, method=method)
    req.add_header("Authorization", f"Bearer {GH_TOKEN}")
    req.add_header("Accept", "application/vnd.github+json")
    req.add_header("X-GitHub-Api-Version", "2022-11-28")
    req.add_header("User-Agent", "license-enforcer/1.0")
    if data:
        req.add_header("Content-Type", "application/json")
    try:
        with urllib.request.urlopen(req, timeout=20) as r:
            return json.loads(r.read()) if r.length != 0 else {}
    except urllib.error.HTTPError as e:
        err = e.read().decode()
        if e.code == 404:
            return None
        print(f"[warn] {method} {path} → HTTP {e.code}: {err[:200]}")
        return None


def slack_notify(msg):
    if not SLACK_TOKEN:
        return
    urllib.request.urlopen(urllib.request.Request(
        "https://slack.com/api/chat.postMessage",
        data=json.dumps({"channel": SLACK_CHANNEL, "text": msg}).encode(),
        headers={"Authorization": f"Bearer {SLACK_TOKEN}", "Content-Type": "application/json"},
        method="POST",
    ), timeout=10)


def list_license_files():
    items = gh("GET", f"/repos/{LICENSE_REPO}/contents/.github/licenses")
    if not items:
        return []
    return [i["name"][:-5] for i in items if i["name"].endswith(".json")]


def fetch_license(user):
    path = f"/repos/{LICENSE_REPO}/contents/.github/licenses/{user}.json"
    existing = gh("GET", path)
    if not existing:
        return path, None, {}
    sha = existing.get("sha")
    data = {}
    if existing.get("content"):
        try:
            raw = json.loads(base64.b64decode(existing["content"]))
            if "expires" in raw and "cdp" not in raw and "sudo" not in raw:
                data = {"cdp": raw["expires"]}
            else:
                data = raw
        except Exception as e:
            print(f"[warn] Failed to parse license for {user}: {e}")
    return path, sha, data


def write_license_file(path, sha, data, commit_msg):
    encoded = base64.b64encode(json.dumps(data).encode()).decode()
    body = {"message": commit_msg, "content": encoded}
    if sha:
        body["sha"] = sha
    gh("PUT", path, body)


def delete_license_file(path, sha, commit_msg):
    gh("DELETE", path, {"message": commit_msg, "sha": sha})


def get_permission(user):
    resp = gh("GET", f"/repos/{LICENSE_REPO}/collaborators/{user}/permission")
    if resp is None:
        return None
    return resp.get("permission")


def desired_permission(cdp_exp, sudo_exp):
    now = datetime.now(timezone.utc)
    if sudo_exp and sudo_exp.astimezone(timezone.utc) > now:
        return "write"
    if cdp_exp and cdp_exp.astimezone(timezone.utc) > now:
        return "read"
    return None  # both expired → revoke


def enforce():
    users = list_license_files()
    if not users:
        print("No license files found.")
        return

    print(f"Checking {len(users)} license(s)...")
    changes = []

    for user in users:
        path, sha, data = fetch_license(user)
        cdp_exp  = _parse_dt(data.get("cdp"))
        sudo_exp = _parse_dt(data.get("sudo"))
        desired  = desired_permission(cdp_exp, sudo_exp)
        current  = get_permission(user)

        cdp_str  = cdp_exp.strftime("%Y-%m-%d")  if cdp_exp  else "none"
        sudo_str = sudo_exp.strftime("%Y-%m-%d") if sudo_exp else "none"
        print(f"  @{user}: cdp={cdp_str} sudo={sudo_str} current={current} desired={desired or 'revoke'}")

        if current == "admin":
            print(f"    → skip (admin)")
            continue

        if desired is None and current == "write":
            # write → read first; full revoke happens next cycle once CDP also confirmed gone
            gh("PUT", f"/repos/{LICENSE_REPO}/collaborators/{user}", {"permission": "read"})
            data.pop("sudo", None)
            write_license_file(path, sha, data,
                f"chore(licenses): downgrade @{user} all tiers expired → cdp-only pending revoke [skip ci]")
            msg = f"⬇️ @{user} all tiers expired — stepped down to read (will revoke next cycle)"
            slack_notify(msg)
            changes.append(msg)
            print(f"    → STEP-DOWN write→read (revoke next cycle)")

        elif desired is None and current == "read":
            gh("DELETE", f"/repos/{LICENSE_REPO}/collaborators/{user}")
            if sha:
                delete_license_file(path, sha,
                    f"chore(licenses): revoke @{user} — all tiers expired [skip ci]")
            msg = f"🔒 @{user} license expired — collaborator access revoked"
            slack_notify(msg)
            changes.append(msg)
            print(f"    → REVOKED")

        elif desired == "read" and current == "write":
            gh("PUT", f"/repos/{LICENSE_REPO}/collaborators/{user}", {"permission": "read"})
            # remove sudo key from file so history is clean
            data.pop("sudo", None)
            write_license_file(path, sha, data,
                f"chore(licenses): downgrade @{user} sudo expired → cdp [skip ci]")
            msg = f"⬇️ @{user} sudo expired — downgraded to CDP (read)"
            slack_notify(msg)
            changes.append(msg)
            print(f"    → DOWNGRADED write→read")

        elif desired == "write" and current != "write":
            gh("PUT", f"/repos/{LICENSE_REPO}/collaborators/{user}", {"permission": "write"})
            msg = f"⬆️ @{user} sudo active — upgraded to write"
            slack_notify(msg)
            changes.append(msg)
            print(f"    → UPGRADED to write")

        else:
            print(f"    → ok (no change)")

    print(f"\nDone. {len(changes)} change(s).")
    for c in changes:
        print(f"  {c}")


def _parse_dt(val):
    if not val:
        return None
    try:
        return datetime.fromisoformat(val)
    except Exception:
        return None


if __name__ == "__main__":
    if not GH_TOKEN:
        print("[error] GH_LICENSE_TOKEN not set", file=sys.stderr)
        sys.exit(1)
    enforce()
