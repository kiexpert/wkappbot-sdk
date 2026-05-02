#!/usr/bin/env python3
import base64, json, math, os, re, sys, urllib.request
from datetime import datetime, timezone, timedelta

LICENSE_REPO  = "kiexpert/wkappbot-sdk"
SLACK_CHANNEL = "C0APNELH2LR"
GH_API        = "https://api.github.com"
GH_TOKEN      = os.environ.get("GH_LICENSE_TOKEN", "")
SLACK_TOKEN   = os.environ.get("SLACK_BOT_TOKEN", "")
PP_CLIENT_ID  = os.environ.get("PAYPAL_CLIENT_ID", "")
PP_SECRET     = os.environ.get("PAYPAL_CLIENT_SECRET", "")
# Default: production. Override PAYPAL_API_BASE=https://api-m.sandbox.paypal.com for sandbox testing.
PP_API_BASE   = os.environ.get("PAYPAL_API_BASE", "https://api-m.paypal.com")
STATE_PATH    = f"/repos/{LICENSE_REPO}/contents/.github/paypal_state.json"
MAX_IDS       = 1000


def gh(method, path, body=None):
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


def slack_notify(msg):
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


def tier_from_amount(amount):
    return "sudo" if amount >= 363 else "cdp"


def grant(user, days, amount):
    from license_store import read, write, get_file_sha
    tier = tier_from_amount(amount)
    perm = "write" if tier == "sudo" else "read"
    now  = datetime.now(timezone.utc)

    _, expiries = read(user)
    prev = expiries.get(tier)
    base = prev if prev and prev > now else now
    exp  = base + timedelta(days=days)

    # merge with existing expiries so the other tier is preserved
    new_cdp  = exp  if tier == "cdp"  else expiries.get("cdp")
    new_sudo = exp  if tier == "sudo" else expiries.get("sudo")
    existing_tiers = {t for t, v in expiries.items() if v} | {tier}
    tier_str = "+".join(sorted(existing_tiers))

    sha = get_file_sha(user)
    gh("PUT", f"/repos/{LICENSE_REPO}/collaborators/{user}", {"permission": perm})
    write(user, tier_str, new_cdp, new_sudo, existing_sha=sha)
    slack_notify(f"✅ @{user} granted {days} days {tier.upper()} access (${amount:.0f} PayPal) expires {exp.date()}")
    print(f"Granted: {user} tier={tier} days={days} expires={exp.date()}")


def paypal_token():
    if not PP_CLIENT_ID or not PP_SECRET:
        print("PayPal credentials not configured — skipping (no PAYPAL_CLIENT_ID/SECRET)")
        sys.exit(0)
    creds = base64.b64encode(f"{PP_CLIENT_ID}:{PP_SECRET}".encode()).decode()
    req = urllib.request.Request(
        f"{PP_API_BASE}/v1/oauth2/token",
        data=b"grant_type=client_credentials",
        headers={"Authorization": f"Basic {creds}", "Content-Type": "application/x-www-form-urlencoded"},
        method="POST",
    )
    try:
        with urllib.request.urlopen(req, timeout=20) as r:
            return json.loads(r.read())["access_token"]
    except urllib.error.HTTPError as e:
        if e.code == 401:
            print(f"PayPal credentials invalid or not Live (401) — skipping until Live credentials are configured")
            sys.exit(0)
        raise


def paypal_transactions(token, start_date, end_date):
    params = (
        f"start_date={urllib.parse.quote(start_date)}"
        f"&end_date={urllib.parse.quote(end_date)}"
        f"&fields=all&transaction_status=S"
    )
    req = urllib.request.Request(
        f"{PP_API_BASE}/v2/reporting/transactions?{params}",
        headers={"Authorization": f"Bearer {token}", "Content-Type": "application/json"},
    )
    try:
        with urllib.request.urlopen(req, timeout=30) as r:
            data = json.loads(r.read())
        return data.get("transaction_details", [])
    except urllib.error.HTTPError as e:
        body = e.read().decode()
        if e.code == 404:
            print(f"PayPal transactions: no results (404) — likely no transactions in range or Transaction Search not enabled on app")
            return []
        print(f"PayPal transactions API error {e.code}: {body}")
        return []


def load_state():
    raw = gh("GET", STATE_PATH)
    if raw and raw.get("content"):
        content = base64.b64decode(raw["content"]).decode()
        state = json.loads(content)
        state["_sha"] = raw.get("sha")
        return state
    return {"processed_ids": [], "last_checked": None, "_sha": None}


def save_state(state):
    sha = state.pop("_sha", None)
    encoded = base64.b64encode(json.dumps(state, indent=2).encode()).decode()
    body = {"message": "chore(paypal): update state [skip ci]", "content": encoded}
    if sha:
        body["sha"] = sha
    gh("PUT", STATE_PATH, body)


def parse_github_user(note):
    if not note:
        return None
    m = re.search(r"@?([a-zA-Z0-9](?:[a-zA-Z0-9-]{0,37}[a-zA-Z0-9])?)", note)
    return m.group(1) if m else None


def main():
    if not GH_TOKEN:
        print("Error: GH_LICENSE_TOKEN not set")
        sys.exit(1)

    import urllib.parse

    # Test mode: skip PayPal API, directly grant to test user
    test_user = os.environ.get("TEST_USER", "").strip()
    if test_user:
        test_amount = float(os.environ.get("TEST_AMOUNT", "8"))
        days = max(1, math.floor(test_amount * 30 / 100))
        print(f"[TEST MODE] Granting {days} days to @{test_user} (${test_amount:.0f})")
        grant(test_user, days, test_amount)
        return

    if not PP_CLIENT_ID or not PP_SECRET:
        print("Error: PAYPAL_CLIENT_ID / PAYPAL_CLIENT_SECRET not set")
        sys.exit(1)

    state = load_state()
    processed = set(state.get("processed_ids", []))
    now = datetime.now(timezone.utc)
    last_checked = state.get("last_checked")
    start_date = last_checked if last_checked else (now - timedelta(hours=24)).isoformat()
    end_date = now.isoformat()

    token = paypal_token()
    transactions = paypal_transactions(token, start_date, end_date)
    print(f"Fetched {len(transactions)} transactions since {start_date}")

    for tx in transactions:
        info = tx.get("transaction_info", {})
        tid = info.get("transaction_id", "")
        if not tid or tid in processed:
            continue

        note = info.get("transaction_note", "")
        amount_str = info.get("transaction_amount", {}).get("value", "0")
        amount = float(amount_str)
        days = max(1, math.floor(amount * 30 / 100)) if amount > 0 else 0

        if days <= 0:
            slack_notify(f"⚠️ PayPal tx {tid}: amount=${amount:.2f} yields 0 days — skipped")
            processed.add(tid)
            continue

        user = parse_github_user(note)
        if not user:
            payer = tx.get("payer_info", {})
            payer_email = payer.get("email_address", "unknown")
            slack_notify(f"⚠️ PayPal tx {tid}: no GitHub username in note '{note}' (payer: {payer_email}, ${amount:.2f}) — manual follow-up needed")
            processed.add(tid)
            continue

        grant(user, days, amount)
        processed.add(tid)

    ids_list = list(processed)[-MAX_IDS:]
    state["processed_ids"] = ids_list
    state["last_checked"] = end_date
    save_state(state)
    print("State updated.")


if __name__ == "__main__":
    main()
