#!/usr/bin/env python3
"""grant_manual.py — Manually grant a license and send collaborator invitation.

Usage:
  python grant_manual.py --user vidkidz --days 365 --tier sudo
  python grant_manual.py --user someone  --days 30  --tier cdp
"""
import argparse, json, os, sys, urllib.request
from datetime import datetime, timezone, timedelta
sys.path.insert(0, os.path.dirname(__file__))
from license_store import read, write, get_file_sha

LICENSE_REPO  = "kiexpert/wkappbot-sdk"
GH_API        = "https://api.github.com"
GH_TOKEN      = os.environ.get("GH_LICENSE_TOKEN", "")
SLACK_TOKEN   = os.environ.get("SLACK_BOT_TOKEN", "")
SLACK_CHANNEL = "C0APNELH2LR"


def gh(method, path, body=None):
    url  = f"{GH_API}{path}"
    data = json.dumps(body).encode() if body else None
    req  = urllib.request.Request(url, data=data, method=method)
    req.add_header("Authorization",        f"Bearer {GH_TOKEN}")
    req.add_header("Accept",               "application/vnd.github+json")
    req.add_header("X-GitHub-Api-Version", "2022-11-28")
    req.add_header("User-Agent",           "grant-manual/1.0")
    if data:
        req.add_header("Content-Type", "application/json")
    try:
        with urllib.request.urlopen(req, timeout=20) as r:
            raw = r.read()
            return json.loads(raw) if raw else {}
    except urllib.error.HTTPError as e:
        print(f"[warn] {method} {path} → HTTP {e.code}: {e.read().decode()[:200]}")
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


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--user",  required=True)
    parser.add_argument("--days",  type=int, required=True)
    parser.add_argument("--tier",  choices=["cdp", "sudo"], required=True)
    args = parser.parse_args()

    if not GH_TOKEN:
        print("[error] GH_LICENSE_TOKEN not set", file=sys.stderr)
        sys.exit(1)

    now  = datetime.now(timezone.utc)
    perm = "write" if args.tier == "sudo" else "read"

    # Extend from existing if still valid
    _, expiries = read(args.user)
    prev = expiries.get(args.tier)
    base = prev if prev and prev > now else now
    exp  = base + timedelta(days=args.days)

    new_cdp  = exp if args.tier == "cdp"  else expiries.get("cdp")
    new_sudo = exp if args.tier == "sudo" else expiries.get("sudo")
    existing_tiers = {t for t, v in expiries.items() if v} | {args.tier}
    tier_str = "+".join(sorted(existing_tiers))

    sha = get_file_sha(args.user)
    write(args.user, tier_str, new_cdp, new_sudo, existing_sha=sha)

    result = gh("PUT", f"/repos/{LICENSE_REPO}/collaborators/{args.user}", {"permission": perm})
    invited = result is not None

    msg = (f"✅ @{args.user} granted {args.days}d {args.tier.upper()} manually"
           f" — expires {exp.date()}"
           f"{' (invite sent)' if invited else ' (already collaborator)'}")
    slack_notify(msg)
    print(msg)


if __name__ == "__main__":
    main()
