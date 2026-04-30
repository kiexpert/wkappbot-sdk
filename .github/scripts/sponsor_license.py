#!/usr/bin/env python3
"""sponsor_license.py — Grant or revoke WKAppBot license for GitHub Sponsors.

Usage:
  python sponsor_license.py --user <login> --amount <usd> --type <one_time|monthly> --action <created|tier_changed|cancelled|pending_cancellation>
"""
import argparse, json, math, os, sys, urllib.request
from datetime import datetime, timezone, timedelta

PRIVATE_REPO   = "kiexpert/wkappbot-private"
SLACK_CHANNEL  = "C0APNELH2LR"
GH_API         = "https://api.github.com"
GH_TOKEN       = os.environ.get("GH_LICENSE_TOKEN", "")
SLACK_TOKEN    = os.environ.get("SLACK_BOT_TOKEN", "")


# ── helpers ──────────────────────────────────────────────────────────────────

def gh(method: str, path: str, body=None) -> dict | None:
    url = f"{GH_API}{path}"
    data = json.dumps(body).encode() if body else None
    req = urllib.request.Request(
        url, data=data, method=method,
        headers={
            "Authorization": f"Bearer {GH_TOKEN}",
            "Accept": "application/vnd.github+json",
            "X-GitHub-Api-Version": "2022-11-28",
            "Content-Type": "application/json",
        },
    )
    try:
        with urllib.request.urlopen(req, timeout=20) as r:
            return json.loads(r.read()) if r.status not in (204, 404) else None
    except urllib.error.HTTPError as e:
        print(f"GitHub API {method} {path} → {e.code}: {e.read().decode()}")
        return None


def slack_notify(msg: str):
    if not SLACK_TOKEN:
        print(f"[Slack skip] {msg}")
        return
    payload = json.dumps({"channel": SLACK_CHANNEL, "text": msg}).encode()
    req = urllib.request.Request(
        "https://slack.com/api/chat.postMessage",
        data=payload,
        headers={"Authorization": f"Bearer {SLACK_TOKEN}", "Content-Type": "application/json"},
        method="POST",
    )
    try:
        with urllib.request.urlopen(req, timeout=10) as r:
            result = json.loads(r.read())
            if not result.get("ok"):
                print(f"Slack error: {result.get('error')}")
    except Exception as e:
        print(f"Slack failed: {e}")


def tier_from_amount(amount: float) -> str:
    if amount >= 363:
        return "sudo"
    return "cdp"  # covers both >= 73 and small one-time


def calc_days(amount: float, license_type: str) -> int:
    if license_type == "one_time":
        return math.floor(amount * 30 / 100)
    return 30  # monthly flat


def repo_exists() -> bool:
    result = gh("GET", f"/repos/{PRIVATE_REPO}")
    if result is None:
        print(f"Warning: {PRIVATE_REPO} not accessible or does not exist — proceeding anyway")
        return False
    return True


# ── license file in private repo ─────────────────────────────────────────────

def put_license_file(user: str, payload: dict):
    path = f"/repos/{PRIVATE_REPO}/contents/licenses/{user}.json"
    content = json.dumps(payload, indent=2)
    import base64
    encoded = base64.b64encode(content.encode()).decode()

    existing = gh("GET", path)
    body = {"message": f"chore(licenses): grant {user} [skip ci]",
            "content": encoded}
    if existing and existing.get("sha"):
        body["sha"] = existing["sha"]
    gh("PUT", path, body)


def delete_license_file(user: str):
    path = f"/repos/{PRIVATE_REPO}/contents/licenses/{user}.json"
    existing = gh("GET", path)
    if existing and existing.get("sha"):
        gh("DELETE", path, {
            "message": f"chore(licenses): revoke {user} [skip ci]",
            "sha": existing["sha"],
        })


# ── grant / revoke ────────────────────────────────────────────────────────────

def grant(user: str, days: int, amount: float, license_type: str):
    tier = tier_from_amount(amount)
    now  = datetime.now(timezone.utc)
    exp  = now + timedelta(days=days)

    # Add collaborator (read access = licensed)
    result = gh("PUT", f"/repos/{PRIVATE_REPO}/collaborators/{user}",
                {"permission": "read"})
    if result is None:
        print(f"Note: collaborator PUT returned no body (may be 204 OK or repo missing)")

    # Write license metadata file
    if repo_exists():
        put_license_file(user, {
            "github_user": user,
            "tier": tier,
            "expires_at": exp.isoformat(),
            "days": days,
            "amount_usd": amount,
            "granted_at": now.isoformat(),
        })

    kind = "one-time" if license_type == "one_time" else "monthly"
    slack_notify(f"✅ @{user} granted {days} days {tier.upper()} access (${amount:.0f} {kind})")
    print(f"Granted: {user} tier={tier} days={days} expires={exp.date()}")


def revoke(user: str):
    gh("DELETE", f"/repos/{PRIVATE_REPO}/collaborators/{user}")
    if repo_exists():
        delete_license_file(user)
    slack_notify(f"❌ @{user} license revoked")
    print(f"Revoked: {user}")


# ── main ──────────────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--user",   required=True)
    parser.add_argument("--amount", type=float, default=0)
    parser.add_argument("--type",   choices=["one_time", "monthly"], default="monthly")
    parser.add_argument("--action", required=True,
                        choices=["created", "tier_changed", "cancelled", "pending_cancellation"])
    args = parser.parse_args()

    if not GH_TOKEN:
        print("Error: GH_LICENSE_TOKEN not set")
        sys.exit(1)

    if args.action in ("created", "tier_changed"):
        days = calc_days(args.amount, args.type)
        if days <= 0:
            print(f"Warning: calculated days={days} for amount={args.amount} — skipping grant")
            sys.exit(0)
        grant(args.user, days, args.amount, args.type)
    else:
        revoke(args.user)


if __name__ == "__main__":
    main()
