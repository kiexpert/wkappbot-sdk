# Subscribe to wkappbot

wkappbot uses **GitHub collaborator permissions** as its license system — no separate
auth, no license keys to lose. Pay once, get added as a collaborator on
`kiexpert/wkappbot-sdk`, and the binary you already have unlocks Pro features.

---

## Quick Start (3 steps)

1. **GitHub 로그인** — `gh auth login` (HTTPS, browser flow)
2. **결제** — PayPal · KIS이체 · GitHub Sponsors 중 하나 → **메모에 GitHub 아이디** 필수
3. **초대 수락** — <https://github.com/notifications> 에서 수락

→ 수분~1시간 내 자동 활성화. 자세한 결제 방법: [PRICING.md](./PRICING.md)

---

## Tier Comparison

| Tier  | Price (KRW/mo) | GitHub permission | Unlocks |
|-------|---------------:|-------------------|---------|
| Free  | 0              | (none / public)   | a11y, inspect, run, scenario, skill, speak, file, logcat |
| CDP   | 100,000        | `read`            | Free + CDP browser automation, `ask` AI (triad/gpt/gemini), `schedule` |
| Sudo  | 500,000        | `write`           | CDP + `--sudo` elevated admin process access (Kiwoom HTS, admin apps) |

All Free features remain free forever — Pro tiers add capabilities, never gate the basics.

---

## Payment Methods

→ See **[PRICING.md](./PRICING.md)** for full instructions (PayPal / KIS / GitHub Sponsors).

**공통 주의사항**: 메모/입금자명에 GitHub 아이디를 정확히 적어야 자동 처리됩니다.

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

Until you accept the invite, wkappbot shows a non-blocking blue toast bottom-right
with a **GitHub 알림 확인** button (links to github.com/notifications, auto-dismisses
after 15 seconds). Free features keep working; CDP/Sudo stay locked. Accept the invite
→ toast clears on the next wkappbot startup.

---

## Verify Your License

```bash
wkappbot license status
```

Sample output:

```
  User    : @your-github-id
  Tier    : CDP
  CDP     : ✓ enabled  (expires 2026-05-29, 29일 0시간 remaining)
  Sudo    : ✗ not licensed
```

If `tier: Free` after you've paid and accepted: re-run `gh auth login`, then retry
`wkappbot license status`. Still stuck → email below.

---

## Renew / Cancel

- **Renew**: transfer again with the same memo before the 30-day expiry. The detector
  re-grants automatically on the next hourly poll.
- **Cancel**: simply don't renew. Permissions auto-revoke at your expiry date
  (check with `wkappbot license status`). No action required from you.

---

## Contact

- **License / payment / activation** → [Open GitHub Issue](../../issues/new?template=license_question.md) (auto-reply in minutes)
- **Email** → kiexpert@kivilab.co.kr
- Bug reports / feature requests: `wkappbot suggest "..."` or open a GitHub issue.
