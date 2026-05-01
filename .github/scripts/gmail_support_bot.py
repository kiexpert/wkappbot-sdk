#!/usr/bin/env python3
"""
gmail_support_bot.py — Auto-read and reply to license/support emails.

Auth: OAuth2 Desktop flow. Requires env vars:
  GMAIL_CLIENT_ID, GMAIL_CLIENT_SECRET, GMAIL_REFRESH_TOKEN
  GH_LICENSE_TOKEN  (for license status lookups)
  SLACK_BOT_TOKEN   (optional Slack notifications)

Usage:
  python gmail_support_bot.py          # process unread emails once
  python gmail_support_bot.py --dry-run  # print drafts without sending
"""

import os, json, base64, re, urllib.request, urllib.parse, email.mime.text, email.mime.multipart
from datetime import datetime, timezone

# ── Config ──────────────────────────────────────────────────────────────────
CLIENT_ID     = os.environ["GMAIL_CLIENT_ID"]
CLIENT_SECRET = os.environ["GMAIL_CLIENT_SECRET"]
REFRESH_TOKEN = os.environ["GMAIL_REFRESH_TOKEN"]
GH_TOKEN      = os.environ.get("GH_LICENSE_TOKEN", "")
SLACK_TOKEN   = os.environ.get("SLACK_BOT_TOKEN", "")
SLACK_CHANNEL = "C0APNELH2LR"
SUPPORT_EMAIL = "kiexpert@kivilab.co.kr"
LICENSE_LABEL = "wkappbot-support"   # Gmail label to apply on processed emails
DRY_RUN       = "--dry-run" in __import__("sys").argv
LICENSE_REPO  = "kiexpert/wkappbot-sdk"
GH_API        = "https://api.github.com"

# ── HTTP helpers ─────────────────────────────────────────────────────────────
def _req(url, method="GET", data=None, headers=None):
    h = headers or {}
    req = urllib.request.Request(url, data=data, method=method, headers=h)
    try:
        with urllib.request.urlopen(req, timeout=15) as r:
            return json.loads(r.read())
    except urllib.error.HTTPError as e:
        return {"_error": e.code, "_body": e.read().decode(errors="replace")}

# ── OAuth2 ───────────────────────────────────────────────────────────────────
_access_token = [None]

def get_access_token():
    if _access_token[0]:
        return _access_token[0]
    resp = _req(
        "https://oauth2.googleapis.com/token",
        method="POST",
        data=urllib.parse.urlencode({
            "client_id":     CLIENT_ID,
            "client_secret": CLIENT_SECRET,
            "refresh_token": REFRESH_TOKEN,
            "grant_type":    "refresh_token",
        }).encode(),
        headers={"Content-Type": "application/x-www-form-urlencoded"},
    )
    token = resp.get("access_token")
    if not token:
        raise RuntimeError(f"Failed to get access token: {resp}")
    _access_token[0] = token
    return token

def gmail(path, method="GET", body=None):
    token = get_access_token()
    h = {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}
    data = json.dumps(body).encode() if body else None
    return _req(f"https://gmail.googleapis.com/gmail/v1/users/me/{path}", method, data, h)

# ── License lookup ───────────────────────────────────────────────────────────
def get_license_status(github_id):
    if not GH_TOKEN or not github_id:
        return None
    h = {"Authorization": f"Bearer {GH_TOKEN}",
         "Accept": "application/vnd.github+json",
         "X-GitHub-Api-Version": "2022-11-28"}
    resp = _req(f"{GH_API}/repos/{LICENSE_REPO}/collaborators/{github_id}/permission",
                headers=h)
    perm = resp.get("permission")
    if perm in ("admin", "write", "read"):
        # Get expiry from commit message
        commits = _req(
            f"{GH_API}/repos/{LICENSE_REPO}/commits?path=.github/licenses/{github_id}&per_page=1",
            headers=h)
        tier_str = expiry_cdp = expiry_sudo = None
        if isinstance(commits, list) and commits:
            msg = commits[0].get("commit", {}).get("message", "")
            m = re.search(r"tier=([\w+]+)", msg)
            if m: tier_str = m.group(1)
            for k, v in re.findall(r"(cdp|sudo)=(\S+)", msg):
                try:
                    dt = datetime.fromisoformat(v)
                    if k == "cdp":  expiry_cdp  = dt
                    if k == "sudo": expiry_sudo = dt
                except: pass
        return {"perm": perm, "tier": tier_str, "cdp_exp": expiry_cdp, "sudo_exp": expiry_sudo}
    return {"perm": "none"}

# ── Auto-reply generator ─────────────────────────────────────────────────────
def generate_reply(subject, body_text, sender):
    subject_low = subject.lower()
    body_low = body_text.lower()

    # Extract GitHub ID if mentioned
    gh_id = None
    m = re.search(r'github[:\s]+@?([\w-]+)', body_text, re.I)
    if not m:
        m = re.search(r'@([\w-]+)', body_text)
    if m:
        gh_id = m.group(1)

    license_info = get_license_status(gh_id) if gh_id else None

    # Routing by topic
    if any(w in subject_low + body_low for w in ["활성화", "activate", "license", "라이선스", "invite", "초대"]):
        if license_info and license_info.get("perm") in ("read", "write", "admin"):
            exp_cdp = license_info.get("cdp_exp")
            exp_str = f" (만료일: {exp_cdp.strftime('%Y-%m-%d')})" if exp_cdp else ""
            reply = f"""안녕하세요!

라이선스 상태를 확인했어요.

• GitHub ID: @{gh_id}
• 티어: {'Sudo' if license_info['perm']=='write' else 'CDP' if license_info['perm']=='read' else 'Dev'}
• 상태: 활성화됨{exp_str}

터미널에서 확인하시려면:
  wkappbot license status

초대 수락이 안 되셨다면 GitHub 알림을 확인해주세요:
  https://github.com/notifications

추가 문의는 언제든지 알려주세요!"""
        elif gh_id:
            reply = f"""안녕하세요!

@{gh_id} 계정을 확인했는데 아직 라이선스가 활성화되지 않은 것 같아요.

다음 사항을 확인해주세요:
1. 이체/결제 시 입금자명/메모에 GitHub ID (@{gh_id})를 정확히 입력하셨나요?
2. GitHub 초대 이메일을 수락하셨나요? → https://github.com/notifications
3. gh auth login 로그인 상태인가요?

결제 확인 후 자동 처리까지 최대 1시간이 소요됩니다.

더 도움이 필요하시면 결제 내역과 함께 다시 문의해주세요."""
        else:
            reply = """안녕하세요!

라이선스 활성화 관련 문의 감사해요.

이메일에 GitHub ID를 알려주시면 상태를 바로 확인해드릴게요.
(예: GitHub ID: your-github-id)

결제 방법 및 활성화 과정은 아래에서 확인하실 수 있어요:
https://github.com/kiexpert/wkappbot-sdk/blob/main/SUBSCRIBE.md"""

    elif any(w in subject_low + body_low for w in ["환불", "refund"]):
        reply = """안녕하세요!

환불 요청을 받았어요.

초대 수락 후 7일 이내에는 비례 환불이 가능합니다.
결제일과 GitHub ID를 알려주시면 처리해드릴게요.

처리 시간: 보통 1-2 영업일."""

    elif any(w in subject_low + body_low for w in ["갱신", "renew", "연장", "extend"]):
        reply = """안녕하세요!

갱신은 동일한 방법으로 결제하시면 자동으로 처리됩니다.
(입금자명/메모에 GitHub ID 필수)

현재 남은 기간 위에 새로운 기간이 추가됩니다 (리셋 없음).

결제 방법: https://github.com/kiexpert/wkappbot-sdk/blob/main/PRICING.md"""

    elif any(w in subject_low + body_low for w in ["기능", "feature", "사용법", "usage", "how"]):
        reply = """안녕하세요!

wkappbot 사용법 관련 문의 감사해요.

주요 리소스:
• 명령어 목록: wkappbot --help
• 스킬 목록: wkappbot skill list
• 문서: https://github.com/kiexpert/wkappbot-sdk/blob/main/README.md

구체적인 질문 사항을 알려주시면 더 자세히 도와드릴게요!"""

    else:
        reply = f"""안녕하세요!

문의 주셔서 감사해요. 확인 후 빠르게 답변드릴게요.

자주 묻는 질문은 아래에서 확인하실 수 있어요:
https://github.com/kiexpert/wkappbot-sdk/blob/main/SUBSCRIBE.md

- wkappbot 팀"""

    return reply

# ── Gmail label ──────────────────────────────────────────────────────────────
def get_or_create_label(name):
    labels = gmail("labels")
    for lbl in labels.get("labels", []):
        if lbl.get("name") == name:
            return lbl["id"]
    r = gmail("labels", "POST", {"name": name, "labelListVisibility": "labelShow",
                                   "messageListVisibility": "show"})
    return r.get("id")

# ── Main ──────────────────────────────────────────────────────────────────────
def process_emails():
    print(f"[gmail_bot] Starting {'(DRY RUN)' if DRY_RUN else ''}...")

    # Find unread emails in inbox (not already processed)
    q = "is:unread in:inbox -label:wkappbot-support"
    result = gmail(f"messages?q={urllib.parse.quote(q)}&maxResults=10")
    messages = result.get("messages", [])
    if not messages:
        print("[gmail_bot] No unread emails.")
        return 0

    label_id = None if DRY_RUN else get_or_create_label(LICENSE_LABEL)
    processed = 0

    for msg_ref in messages:
        msg = gmail(f"messages/{msg_ref['id']}?format=full")
        if "_error" in msg:
            continue

        headers = {h["name"]: h["value"] for h in msg.get("payload", {}).get("headers", [])}
        subject = headers.get("Subject", "(no subject)")
        sender  = headers.get("From", "")
        msg_id  = headers.get("Message-ID", "")
        thread_id = msg.get("threadId", "")

        # Extract body text
        body_text = ""
        def extract_text(part):
            nonlocal body_text
            if part.get("mimeType") == "text/plain" and part.get("body", {}).get("data"):
                body_text += base64.urlsafe_b64decode(part["body"]["data"]).decode("utf-8", errors="replace")
            for sub in part.get("parts", []):
                extract_text(sub)
        extract_text(msg.get("payload", {}))

        print(f"\n[gmail_bot] Email: {subject[:60]} | From: {sender[:40]}")

        # Skip system/notification senders — only reply to real customers
        SKIP_SENDERS = [
            "noreply@", "no-reply@", "do-not-reply@",
            "notifications@github.com", "support@github.com",
            "service@intl.paypal.com", "service@paypal.com",
            "stripe.com", "gitguardian.com",
            "mailer@github.com", "security@", "accounts@",
        ]
        if any(s in sender.lower() for s in SKIP_SENDERS):
            print(f"  [SKIP] system/notification sender")
            # Mark as read so it doesn't re-appear
            if not DRY_RUN:
                gmail(f"messages/{msg_ref['id']}/modify", "POST",
                      {"removeLabelIds": ["UNREAD"]})
            continue

        reply_body = generate_reply(subject, body_text, sender)

        if DRY_RUN:
            print(f"  [DRY RUN] Reply draft:\n{reply_body[:300]}...")
            continue

        # Build reply
        mime = email.mime.multipart.MIMEMultipart()
        mime["To"]         = sender
        mime["From"]       = SUPPORT_EMAIL
        mime["Subject"]    = f"Re: {subject}" if not subject.startswith("Re:") else subject
        mime["In-Reply-To"] = msg_id
        mime["References"]  = msg_id
        mime.attach(email.mime.text.MIMEText(reply_body, "plain", "utf-8"))
        raw = base64.urlsafe_b64encode(mime.as_bytes()).decode()

        # Send
        send_result = gmail("messages/send", "POST", {"raw": raw, "threadId": thread_id})
        if "_error" in send_result:
            print(f"  ERROR sending: {send_result}")
            continue

        # Mark as read + apply label
        gmail(f"messages/{msg_ref['id']}/modify", "POST", {
            "removeLabelIds": ["UNREAD"],
            "addLabelIds": [label_id] if label_id else [],
        })
        processed += 1
        print(f"  ✓ Replied and marked processed")

        # Slack notification
        if SLACK_TOKEN:
            slack_msg = f"📧 wkappbot 지원 이메일 답변 완료\nFrom: {sender}\nSubject: {subject}\n자동 답변 발송됨"
            _req("https://slack.com/api/chat.postMessage", "POST",
                 json.dumps({"channel": SLACK_CHANNEL, "text": slack_msg}).encode(),
                 {"Authorization": f"Bearer {SLACK_TOKEN}", "Content-Type": "application/json"})

    print(f"\n[gmail_bot] Done. Processed {processed}/{len(messages)} emails.")
    return processed

if __name__ == "__main__":
    process_emails()
