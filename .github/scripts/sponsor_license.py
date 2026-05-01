#!/usr/bin/env python3
"""sponsor_license.py — Grant or revoke WKAppBot license for GitHub Sponsors.

Usage:
  python sponsor_license.py --user <login> --amount <usd> --type <one_time|monthly> --action <created|tier_changed|cancelled|pending_cancellation>
"""
import argparse, base64, json, math, os, sys, urllib.request
from datetime import datetime, timezone, timedelta

LICENSE_REPO   = "kiexpert/wkappbot-sdk"
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


# ── grant / revoke ────────────────────────────────────────────────────────────

def _fetch_license(user: str):
    path = f"/repos/{LICENSE_REPO}/contents/.github/licenses/{user}.json"
    existing = gh("GET", path)
    if not existing:
        return path, None, {}
    sha = existing.get("sha")
    data: dict = {}
    if existing.get("content"):
        try:
            raw = json.loads(base64.b64decode(existing["content"]))
            # migrate old flat schema {"expires": "..."} → {"cdp": "..."}
            if "expires" in raw and "cdp" not in raw and "sudo" not in raw:
                data = {"cdp": raw["expires"]}
            else:
                data = raw
        except Exception:
            pass
    return path, sha, data


def write_license_file(user: str, tier: str, expires_at: str):
    path, sha, data = _fetch_license(user)
    data[tier] = expires_at
    encoded = base64.b64encode(json.dumps(data).encode()).decode()
    body = {"message": f"chore(licenses): grant @{user} [skip ci]", "content": encoded}
    if sha:
        body["sha"] = sha
    gh("PUT", path, body)


def _read_expiry(user: str, tier: str):
    _, _, data = _fetch_license(user)
    raw = data.get(tier)
    if raw:
        try:
            return datetime.fromisoformat(raw)
        except Exception:
            pass
    return None


def grant(user: str, days: int, amount: float, license_type: str):
    tier = tier_from_amount(amount)
    perm = "write" if tier == "sudo" else "read"
    now  = datetime.now(timezone.utc)
    prev = _read_expiry(user, tier)
    base = prev if prev and prev > now else now
    exp  = base + timedelta(days=days)

    gh("PUT", f"/repos/{LICENSE_REPO}/collaborators/{user}", {"permission": perm})
    write_license_file(user, tier, exp.isoformat())

    kind = "one-time" if license_type == "one_time" else "monthly"
    slack_notify(f"✅ @{user} granted {days} days {tier.upper()} access (${amount:.0f} {kind}) expires {exp.date()}")
    print(f"Granted: {user} tier={tier} days={days} expires={exp.date()}")


def revoke(user: str):
    gh("DELETE", f"/repos/{LICENSE_REPO}/collaborators/{user}")
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
