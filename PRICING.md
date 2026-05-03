# Pricing

Four tiers. No license keys. **Your GitHub account is your license.**

| Tier | Price | Duration | Best for |
|------|------:|----------|----------|
| Free | — | Forever | Basic Win32 / UIA / Android automation |
| Trial | ₩10,000 (~$8) | 2 days | CDP 체험 — 기능 확인 후 결정 |
| CDP  | $100 / ₩140,000 | 30 days | Browser automation, AI delegation, scheduling |
| Sudo | $363 / ₩500,000 | 108 days | Trading terminals, elevated processes (Kiwoom HTS) |

> Payments **stack** — pay again before expiry and days are added on top, not reset.
> Trial → CDP 본구매 시 남은 Trial 기간 위에 쌓입니다.

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

## How to Pay

Pick any one — all activate automatically within minutes.

### 💳 Option A — PayPal (fastest, international, **자동 활성화**)

| 목적 | 금액 | 링크 |
|------|-----:|------|
| Trial 2일 체험 | $8 | [paypal.me/kiexpert](https://paypal.me/kiexpert) |
| CDP 30일 | $100 | [paypal.me/kiexpert](https://paypal.me/kiexpert) |
| Sudo 108일 | $363 | [paypal.me/kiexpert](https://paypal.me/kiexpert) |

1. 위 링크에서 금액 입력
2. **Memo에 GitHub 아이디** 적기 (필수!)
3. 끝 — 수분 내 자동 활성화

### 🏦 Option B — KIS 이체 (국내, 1시간 내 수동 처리)
| 항목 | 내용 |
|------|------|
| 은행 | 한국투자증권 (KIS) |
| 계좌 | `43420089-01` (김기일) |
| 금액 | Trial: ₩10,000 / CDP: ₩140,000 / Sudo: ₩500,000 |
| **입금자명** | **GitHub 아이디** (필수!) |

### ⭐ Option C — GitHub Sponsors (**자동 활성화**)
1. **github.com/sponsors/kiexpert** 방문
2. 금액 선택 ($8 = Trial 2일 / $100 = CDP 30일 / $363 = Sudo 108일)
3. 끝 — 즉시 자동 활성화

---

> ⚠️ **메모/입금자명에 GitHub 아이디를 정확히 적어야 자동 발급됩니다.**
> 미입력 시 수동 처리 지연 발생.

---

## How It Works

wkappbot uses **GitHub collaborator permissions** as its license backend.

1. 결제 → 시스템이 메모에서 GitHub ID 파싱
2. `kiexpert/wkappbot-sdk` collaborator로 자동 추가
3. `wkappbot license status` 로 만료일 확인

No license server. No token files. GitHub이 신뢰하면 wkappbot이 신뢰합니다.

CDP tier = `read` permission. Sudo tier = `write` permission.

---

## FAQ

**활성화까지 얼마나 걸리나요?**
PayPal/Sponsors → 수분 내. KIS 이체 → 입금 확인 후 1시간 이내.
GitHub 초대 이메일 수락 → `wkappbot license status` 로 확인.

**추가 결제하면 어떻게 되나요?**
남은 기간 위에 쌓입니다. 15일 남은 상태에서 30일치 결제 → 45일로 연장.

**해지하려면?**
갱신 안 하면 됩니다. 만료일에 자동 회수. 별도 해지 절차 없음.

**환불은요?**
Trial(2일) 티어는 환불 없음. CDP/Sudo는 활성화(GitHub 초대 수락) 전 취소 시 전액 환불. 수락 후에는 미사용 기간 pro-rate 환불 (7일 이내 요청). kiexpert@kivilab.co.kr 로 GitHub ID + 결제일 전송.

---

See [SUBSCRIBE.md](./SUBSCRIBE.md) for the step-by-step subscription flow.
