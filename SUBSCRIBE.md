# Subscribe to wkappbot

wkappbot uses **GitHub collaborator permissions** as its license system — no separate
auth, no license keys to lose. Pay once, get added as a collaborator on
`kiexpert/wkappbot-sdk`, and the binary you already have unlocks Pro features.

---

## Quick Start (3 steps)

1. **Authenticate GitHub locally** — `gh auth login` (HTTPS, browser flow).
2. **Bank-transfer the tier price** to KIS with your **GitHub username as the memo**.
3. **Accept the collaborator invite** that arrives at <https://github.com/notifications>.

License auto-activates within 1 hour of confirmed payment.

---

## Tier Comparison

| Tier  | Price (KRW/mo) | GitHub permission | Unlocks |
|-------|---------------:|-------------------|---------|
| Free  | 0              | (none / public)   | a11y, inspect, run, scenario, skill, speak, file, logcat |
| CDP   | 100,000        | `read`            | Free + CDP browser automation, `ask` AI (triad/gpt/gemini), `schedule` |
| Sudo  | 500,000        | `write`           | CDP + `--sudo` elevated admin process access (Kiwoom HTS, admin apps) |

All Free features remain free forever — Pro tiers add capabilities, never gate the basics.

---

## Payment Instructions (KIS bank transfer)

| Field | Value |
|-------|-------|
| Bank | 한국투자증권 (KIS) |
| Account number | `43420089-01` |
| Account holder | 김기일 |
| Amount | 100,000 KRW (CDP) or 500,000 KRW (Sudo) |
| Memo (입금자명 / 표시내용) | **your GitHub username** — REQUIRED for auto-detection |

The detector polls the KIS account hourly, parses the memo field, and runs:

```bash
gh api repos/kiexpert/wkappbot-sdk/collaborators/{your_id} -X PUT -f permission=read   # CDP
gh api repos/kiexpert/wkappbot-sdk/collaborators/{your_id} -X PUT -f permission=write  # Sudo
```

You'll receive a GitHub email + notification with the invite. License activates the
moment you accept.

> If your transfer memo doesn't match a real GitHub username, the system can't grant
> access. Double-check the spelling before you transfer.

---

## GitHub Account Requirement

A GitHub account is required because the license check uses `gh` CLI under the hood.

```bash
# Windows
winget install --id GitHub.cli

# macOS
brew install gh

# Authenticate
gh auth login
# → GitHub.com → HTTPS → Login with browser
```

wkappbot reads your auth at startup and queries the collaborator API to determine
your tier. If `gh` is not installed or not authenticated, you stay on Free tier.

---

## Pending Invite UX

Until you accept the invite, wkappbot shows a non-blocking blue toast bottom-right:
`[wkappbot] Pending GitHub invite — click to accept`. Free features keep working;
CDP/Sudo stay locked. Accept at github.com/notifications → toast clears on the next
wkappbot startup.

---

## Verify Your License

```bash
wkappbot license status
```

Sample output:

```
account     : your-github-id
tier        : CDP        (permission=read)
expires     : 2026-05-29 (29 days remaining)
unlocks     : cdp, ask, schedule
pending     : (none)
```

If `tier: Free` after you've paid and accepted: re-run `gh auth login`, then retry
`wkappbot license status`. Still stuck → email below.

---

## Renew / Cancel

- **Renew**: transfer again with the same memo before the 30-day expiry. The detector
  re-grants automatically on the next hourly poll.
- **Cancel**: simply don't renew. Permissions auto-revoke at the 30-day mark via
  `check_expirations()`. No action required from you.

---

## Contact

- **License / payment / activation** → [Open GitHub Issue](../../issues/new?template=license_question.md) (auto-reply in minutes)
- **Email** → kiexpert@kivilab.co.kr
- Bug reports / feature requests: `wkappbot suggest "..."` or open a GitHub issue.
