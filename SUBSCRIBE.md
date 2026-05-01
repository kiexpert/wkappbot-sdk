# wkappbot 구독하기

> **라이선스 키 없음. 별도 계정 없음.** GitHub 계정 하나로 즉시 활성화.

---

## 요금제 한눈에 보기

| | Free | CDP | Sudo |
|--|:--:|:--:|:--:|
| **가격** | 무료 | ₩140,000 / $100 | ₩500,000 / $363 |
| **기간** | 영구 | 30일 | 108일 |
| UIA / Win32 / Android 자동화 | ✅ | ✅ | ✅ |
| 시나리오 실행, skill 시스템 | ✅ | ✅ | ✅ |
| **CDP 브라우저 자동화** | ❌ | ✅ | ✅ |
| **`ask` AI 위임 (triad/GPT/Gemini)** | ❌ | ✅ | ✅ |
| **`schedule` 예약 실행** | ❌ | ✅ | ✅ |
| **`--sudo` 관리자 프로세스 접근** | ❌ | ❌ | ✅ |

> 결제는 **누적**됩니다 — 남은 기간 위에 쌓이며, 기간이 리셋되지 않아요.

---

## 결제 방법

### 💳 A. PayPal (해외 / 즉시)

1. **paypal.me/kiexpert** 에서 결제
2. **메모에 GitHub 아이디 입력** ← 필수!
3. 완료 — 수분 내 자동 활성화

### 🏦 B. 국내 이체

| 항목 | 내용 |
|------|------|
| 은행 | 한국투자증권 (KIS) |
| 계좌번호 | `43420089-01` (김기일) |
| CDP 금액 | ₩140,000 |
| Sudo 금액 | ₩500,000 |
| **입금자명** | **GitHub 아이디** ← 필수! |

> 입금자명이 달라도 처리 가능하지만 지연될 수 있어요.  
> 처리 후 1시간 이내 자동 활성화.

### ⭐ C. GitHub Sponsors

1. **[github.com/sponsors/kiexpert](https://github.com/sponsors/kiexpert)** 방문
2. 티어 선택 후 구독
3. 완료 — 즉시 자동 활성화

---

## 활성화 3단계

```
1️⃣  결제  →  2️⃣  GitHub 초대 수락  →  3️⃣  wkappbot 재실행
```

**1단계 — 결제**  
위 방법 중 하나로 결제. 메모/입금자명에 GitHub 아이디 필수.

**2단계 — 초대 수락**  
이메일 또는 GitHub 알림에서 `kiexpert/wkappbot-sdk` 초대 수락.  
→ [github.com/notifications](https://github.com/notifications) 에서 확인

**3단계 — 확인**  
```bash
wkappbot license status
```
```
  User    : @your-github-id
  Tier    : CDP
  CDP     : ✓ enabled  (expires 2026-06-01, 29일 남음)
  Sudo    : ✗ not licensed
```

> 아직 `Free`로 나오면 → `gh auth login` 재실행 후 다시 확인

---

## GitHub 계정 / CLI 설치

라이선스 확인에 GitHub CLI가 필요해요 (한 번만 설정).

```bash
# Windows
winget install --id GitHub.cli

# macOS
brew install gh
```

```bash
# 로그인 (최초 1회)
gh auth login
# → GitHub.com → HTTPS → Login with browser
```

---

## 자주 묻는 질문

**활성화까지 얼마나 걸리나요?**  
PayPal / GitHub Sponsors → 수분 내. KIS 이체 → 최대 1시간.  
초대 이메일을 수락해야 최종 활성화됩니다.

**초대를 수락하기 전에도 사용할 수 있나요?**  
네. 수락 전에도 Free 기능은 정상 작동해요.  
wkappbot이 초대 대기 중임을 알림으로 표시해줍니다.

**추가 결제하면 어떻게 되나요?**  
남은 기간 위에 일수가 쌓입니다.  
예) 15일 남은 상태에서 ₩140,000 결제 → 45일로 연장.

**해지하려면?**  
별도 절차 없음 — 그냥 갱신하지 않으면 돼요.  
만료일에 자동으로 Free 티어로 전환됩니다.

**환불은요?**  
초대 수락 후 7일 이내 비례 환불 가능.  
kiexpert@kivilab.co.kr 로 GitHub ID + 결제일 전송해주세요.

---

## 문의

- 📮 **라이선스 / 결제 문제** → [GitHub Issue](../../issues/new?template=license_question.md) (수분 내 답변)
- 📧 **이메일** → kiexpert@kivilab.co.kr
- 🐛 **버그 / 기능 요청** → `wkappbot suggest "..."` 또는 GitHub Issue
