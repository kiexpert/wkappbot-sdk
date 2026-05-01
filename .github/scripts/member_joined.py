#!/usr/bin/env python3
"""member_joined.py — Extend license expiry to compensate for invite acceptance delay.

Triggered when a collaborator accepts their invitation (member:added event).
Calculates: delay = accepted_at - grant_commit_time
Extends all active tier expiries by that delay so users don't lose paid time.
"""
import os, sys
from datetime import datetime, timezone, timedelta
sys.path.insert(0, os.path.dirname(__file__))
from license_store import read, write, get_file_sha

LICENSE_REPO = "kiexpert/wkappbot-sdk"


def main():
    user         = os.environ.get("MEMBER_LOGIN", "").strip()
    accepted_iso = os.environ.get("ACCEPTED_AT", "").strip()   # github.event timestamp

    if not user or not accepted_iso:
        print("[error] MEMBER_LOGIN and ACCEPTED_AT required", file=sys.stderr)
        sys.exit(1)

    accepted_at = datetime.fromisoformat(accepted_iso.replace("Z", "+00:00"))

    tier_str, expiries = read(user)
    if not tier_str:
        print(f"@{user}: no trusted license found — nothing to extend")
        return

    cdp_exp  = expiries.get("cdp")
    sudo_exp = expiries.get("sudo")

    # Grant time = timestamp of the trusted commit that wrote the license
    grant_time = _get_grant_time(user)
    if not grant_time:
        print(f"@{user}: could not determine grant time — skipping")
        return

    delay = accepted_at - grant_time
    if delay.total_seconds() <= 0:
        print(f"@{user}: no delay detected ({delay}) — nothing to extend")
        return

    delay_str = _fmt(delay)
    print(f"@{user}: accepted {delay_str} after grant — extending all active tiers by {delay_str}")

    new_cdp  = (cdp_exp  + delay) if cdp_exp  else None
    new_sudo = (sudo_exp + delay) if sudo_exp else None

    sha = get_file_sha(user)
    write(user, tier_str, new_cdp, new_sudo, existing_sha=sha)
    print(f"@{user}: cdp={new_cdp and new_cdp.date()} sudo={new_sudo and new_sudo.date()}")


def _get_grant_time(user: str) -> datetime | None:
    import json, urllib.request
    token = os.environ.get("GH_LICENSE_TOKEN", "")
    url   = f"https://api.github.com/repos/{LICENSE_REPO}/commits?path=.github/licenses/{user}&sha=main&per_page=1"
    req   = urllib.request.Request(url)
    req.add_header("Authorization",        f"Bearer {token}")
    req.add_header("Accept",               "application/vnd.github+json")
    req.add_header("X-GitHub-Api-Version", "2022-11-28")
    req.add_header("User-Agent",           "member-joined/1.0")
    try:
        with urllib.request.urlopen(req, timeout=20) as r:
            commits = json.loads(r.read())
            if commits:
                ts = commits[0]["commit"]["committer"]["date"]
                return datetime.fromisoformat(ts.replace("Z", "+00:00"))
    except Exception as e:
        print(f"[warn] failed to fetch grant time: {e}")
    return None


def _fmt(td: timedelta) -> str:
    h = int(td.total_seconds() // 3600)
    m = int((td.total_seconds() % 3600) // 60)
    return f"{h}시간 {m}분" if h else f"{m}분"


if __name__ == "__main__":
    main()
