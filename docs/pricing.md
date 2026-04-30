# 요금

GitHub 계정이 곧 라이선스입니다. 별도 라이선스 키 없음.

## 티어 비교

| 티어 | 월 요금 | 대상 |
|------|--------:|------|
| **Free** | 무료 | 개인 사용, 학습, Win32/UIA/Android 기본 자동화 |
| **CDP** | ₩100,000 | 브라우저 자동화 + 멀티 AI 위임 + 스케줄러 |
| **Sudo** | ₩500,000 | HTS 거래 터미널 + 관리자 프로세스 접근 |

---

## 기능 상세

| 기능 | Free | CDP | Sudo |
|------|:----:|:---:|:----:|
| `a11y` (UIA / Win32 / Android) | ✅ | ✅ | ✅ |
| `inspect`, `find`, `highlight` | ✅ | ✅ | ✅ |
| `run` YAML 시나리오 | ✅ | ✅ | ✅ |
| `skill` 시스템 | ✅ | ✅ | ✅ |
| `speak` TTS | ✅ | ✅ | ✅ |
| `file`, `logcat`, `gc` | ✅ | ✅ | ✅ |
| **CDP 브라우저 자동화** | — | ✅ | ✅ |
| **`ask triad / gpt / gemini`** | — | ✅ | ✅ |
| **`schedule`** | — | ✅ | ✅ |
| **AppBot Eye (Slack 데몬)** | — | ✅ | ✅ |
| **`--sudo` 관리자 접근** | — | — | ✅ |
| **Kiwoom HTS / MFC 거래 터미널** | — | — | ✅ |

---

## 결제 방법

1. KIS 이체: 계좌 정보는 [SUBSCRIBE.md](https://github.com/kiexpert/wkappbot-sdk/blob/main/SUBSCRIBE.md) 참고
2. 이체 메모에 GitHub 사용자명 기재
3. 자동 처리 (1시간 이내)
4. GitHub 협력자 초대 수락
5. `wkappbot license status`로 확인

---

## FAQ

**활성화까지 얼마나 걸리나요?**  
은행 이체 후 최대 1시간. 시스템이 자동으로 권한을 부여하고 GitHub 초대 이메일을 발송합니다.

**갱신 방법?**  
같은 GitHub 사용자명으로 다시 이체하면 됩니다. 30일 창이 만료되기 전에 갱신하면 연속 사용 가능합니다.

**해지 방법?**  
갱신하지 않으면 됩니다. 30일 후 자동으로 Free 티어로 복귀합니다. 별도 해지 절차 없음.

**환불 정책?**  
최초 활성화 후 7일 이내 pro-rated 환불 가능. `kiexpert@kivilab.co.kr`로 GitHub ID와 이체 날짜를 보내주세요.

**CI/CD 파이프라인에서도 사용 가능한가요?**  
네. `WKAPPBOT_WORKER=1`로 인터랙티브 출력 억제. 서비스 GitHub 계정을 협력자로 추가해 사용하세요.
