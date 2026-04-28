# Pricing

Three tiers. No license keys. Your GitHub account is your license.

| Tier | Price | Best for |
|------|------:|----------|
| Free | 0 KRW/mo | Personal use, learning, basic Win32/UIA/Android automation |
| CDP  | 100,000 KRW/mo | Browser automation + multi-AI delegation + scheduling |
| Sudo | 500,000 KRW/mo | Trading terminals & admin apps (Kiwoom HTS, elevated processes) |

---

## Feature Comparison

| Feature | Free | CDP | Sudo |
|---------|:----:|:---:|:----:|
| `a11y` (UIA / Win32 / Android) | yes | yes | yes |
| `inspect`, `find`, `highlight` | yes | yes | yes |
| `run` YAML scenarios            | yes | yes | yes |
| `skill` system                  | yes | yes | yes |
| `speak` (TTS)                   | yes | yes | yes |
| `file`, `logcat`                | yes | yes | yes |
| **CDP browser automation**      |  -  | yes | yes |
| **`ask` (triad / gpt / gemini)** |  -  | yes | yes |
| **`schedule`**                   |  -  | yes | yes |
| **`--sudo` elevated process access** |  -  |  -  | yes |

---

## How It Works

wkappbot uses **GitHub collaborator permissions** as its license backend.

1. You pay via KIS bank transfer (memo = your GitHub username).
2. Owner adds you as a collaborator on `kiexpert/wkappbot-sdk` with the right permission.
3. wkappbot calls `gh api` at startup to read your permission, then unlocks features.

That's the entire system. No license server, no token files, no offline activation
dance. If GitHub trusts you, wkappbot trusts you. If you cancel, GitHub revokes —
wkappbot follows.

CDP tier = `read` permission. Sudo tier = `write` permission.

---

## FAQ

**How long does activation take?**
Up to 1 hour after the bank transfer settles. The detector polls hourly and grants
permission automatically. You'll get a GitHub invite email — accept it, then run
`wkappbot license status` to confirm.

**How do I renew?**
Transfer again with the same GitHub username in the memo. The system re-grants on
the next hourly poll, before your 30-day window expires.

**How do I cancel?**
Don't renew. Permissions auto-revoke at day 30 via `check_expirations()`. Nothing to
unsubscribe from, no recurring charge to stop.

**Can I switch tiers?**
Yes — pay the new tier amount with the same memo. The detector overwrites your
permission with the higher level immediately on the next poll.

**Refunds?**
Pro-rated within 7 days of first activation. Email kiexpert@kivilab.co.kr with your
GitHub ID and transfer date.

---

See [SUBSCRIBE.md](./SUBSCRIBE.md) for the step-by-step subscription flow.
