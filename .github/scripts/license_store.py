#!/usr/bin/env python3
"""license_store.py — Read/write license data on the `licenses` branch.

Storage model:
  Branch : licenses  (orphan, one file per user, amend + force-push)
  File   : {user}    — placeholder for listing; content unused
  Commit : chore(licenses): @{user} tier=cdp+sudo cdp=<iso> sudo=<iso> [skip ci]
           └─ ALL data (tier + expiry) lives in the commit message → 1 API call to read
  Trust  : commit author must be in TRUSTED_AUTHORS; untrusted latest = immediate expire
"""
import base64, json, os, re, urllib.request
from datetime import datetime

LICENSE_REPO    = "kiexpert/wkappbot-sdk"
LICENSE_BRANCH  = "licenses"
GH_API          = "https://api.github.com"
GH_TOKEN        = os.environ.get("GH_LICENSE_TOKEN", "")
TRUSTED_AUTHORS = {"kiexpert", "github-actions[bot]"}

_TIER_RE = re.compile(r'tier=([\w+]+)')
_EXP_RE  = re.compile(r'(cdp|sudo)=(\S+)')


def _gh(method, path, body=None):
    url  = f"{GH_API}{path}"
    data = json.dumps(body).encode() if body else None
    req  = urllib.request.Request(url, data=data, method=method)
    req.add_header("Authorization",       f"Bearer {GH_TOKEN}")
    req.add_header("Accept",              "application/vnd.github+json")
    req.add_header("X-GitHub-Api-Version","2022-11-28")
    req.add_header("User-Agent",          "license-store/1.0")
    if data:
        req.add_header("Content-Type", "application/json")
    try:
        with urllib.request.urlopen(req, timeout=20) as r:
            raw = r.read()
            return json.loads(raw) if raw else {}
    except urllib.error.HTTPError as e:
        if e.code == 404:
            return None
        print(f"[warn] {method} {path} → HTTP {e.code}: {e.read().decode()[:200]}")
        return None


def read(user: str) -> tuple[str | None, dict[str, datetime | None]]:
    """1 API call: GET latest commit → check author → parse tier + expiry from message.
    Returns (tier_str, {cdp, sudo}) or (None, {}) if untrusted/missing.
    """
    commits = _gh("GET", f"/repos/{LICENSE_REPO}/commits"
                         f"?path={user}&sha={LICENSE_BRANCH}&per_page=1") or []
    if not commits:
        return None, {}

    c     = commits[0]
    login = (c.get("author") or {}).get("login", "")
    name  = c.get("commit", {}).get("author", {}).get("name", "")
    if login not in TRUSTED_AUTHORS and name not in TRUSTED_AUTHORS:
        print(f"[TAMPER] @{user} last commit by '{login or name}' — treating as expired")
        return None, {}

    msg      = c.get("commit", {}).get("message", "")
    tier_str = (_TIER_RE.search(msg) or {1: None}).group(1) if _TIER_RE.search(msg) else None
    expiries: dict[str, datetime | None] = {"cdp": None, "sudo": None}
    for tier, iso in _EXP_RE.findall(msg):
        try:
            expiries[tier] = datetime.fromisoformat(iso)
        except Exception:
            pass

    return tier_str, expiries


def write(user: str, tier: str, cdp_exp: datetime | None, sudo_exp: datetime | None,
          existing_sha: str | None = None):
    """Write/update license. Pass existing_sha to update in place (amend-style)."""
    parts = [f"tier={tier}"]
    if cdp_exp:
        parts.append(f"cdp={cdp_exp.isoformat()}")
    if sudo_exp:
        parts.append(f"sudo={sudo_exp.isoformat()}")
    msg     = f"chore(licenses): @{user} {' '.join(parts)} [skip ci]"
    encoded = base64.b64encode(b"").decode()  # file is a placeholder
    body: dict = {"message": msg, "content": encoded, "branch": LICENSE_BRANCH}
    if existing_sha:
        body["sha"] = existing_sha
    _gh("PUT", f"/repos/{LICENSE_REPO}/contents/{user}", body)


def get_file_sha(user: str) -> str | None:
    obj = _gh("GET", f"/repos/{LICENSE_REPO}/contents/{user}?ref={LICENSE_BRANCH}")
    return obj.get("sha") if obj else None


def delete(user: str, sha: str, reason: str):
    _gh("DELETE", f"/repos/{LICENSE_REPO}/contents/{user}", {
        "message": f"chore(licenses): revoke @{user} — {reason} [skip ci]",
        "sha": sha,
        "branch": LICENSE_BRANCH,
    })


def list_users() -> list[str]:
    items = _gh("GET", f"/repos/{LICENSE_REPO}/contents/?ref={LICENSE_BRANCH}") or []
    return [i["name"] for i in items if i.get("type") == "file" and i["name"] != "README.md"]
