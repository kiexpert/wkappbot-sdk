# 핵심 개념

## grap — Universal Element Address

> *사람은 창을 찾아보고, AI는 grap으로 가리킨다.*

grap은 Win32, UIA, 웹, Android의 모든 UI 요소를 표현하는 단일 주소 언어입니다.

```
*Notepad*               → 와일드카드: 제목에 "Notepad" 포함
*notepad*;*calc*        → OR: 메모장 또는 계산기
heroes#realtime-account → UIA 스코프: "heroes" 창 안의 "realtime-account" 노드
Chrome#ChatGPT#model    → 탭 포털: Chrome > ChatGPT 탭 > model 요소
adb://Fold5/*balance*   → Android: Fold5 기기의 balance 요소
hwnd:0x010B084A         → 직접 Win32 핸들
{proc:'chrome', domain:'claude.ai'} → JSON5 다중 필드 AND
```

`a11y find <grap>` 으로 실제 핸들을 확인합니다:

```bash
wkappbot a11y find "*계산기*"
# TARGET "hwnd:0x001A2B3C"
[0] hwnd:0x001A2B3C  Name="계산기"  ControlType=Window  proc=Calculator.exe
```

→ 상세 문서: [grap 패턴 레퍼런스](/reference/grap)

---

## 5-Tier Element Search

요소를 찾지 못하면 자동으로 다음 티어로 넘어갑니다:

```
1. UIA (Accessibility API)          ← 가장 빠름, 포커스리스
2. Vision Cache                     ← 이전 성공 좌표 재사용
3. Simple OCR                       ← 텍스트 레이블로 위치 추정
4. Vision API (Claude/Gemini)       ← 스크린샷 기반 AI 추론
5. Coordinate-based                 ← 마지막 수단
```

결과는 Experience DB(`wkappbot.hq/experience/`)에 캐싱되어, 다음 실행부터 비싼 티어를 건너뜁니다.

---

## AppBot Eye — Always-On 데몬

Eye는 대부분의 명령 실행 시 자동으로 백그라운드에서 기동되는 상주 프로세스입니다.

**Eye가 하는 일:**
- **Slack 데몬** — Socket Mode로 Slack 메시지 수신 → 큐 → Claude 라우팅
- **MCP 브로커** — 모든 CLI 명령을 JSON-RPC 도구로 노출 (Claude Code·Codex 연동)
- **핫스왑 감시자** — `wkappbot-core.new.exe` 감지 → 진행 중 요청 드레인 → 무중단 교체
- **워치독 VBS** — Eye가 2분 이상 죽으면 고아 core를 정리하고 재시작

```bash
wkappbot eye tick     # 현재 상태 조회 (ctx%, 카드, 큐 통계)
```
```
[EYE] running  pid=12345  uptime=2h14m
[CTX] 3.2 MB / ~20 MB (16%)
[QUEUE] pending=0  processing=0
[CARD] ts=1714000000.123456
```

::: warning Eye를 직접 시작하지 마세요
`wkappbot eye`를 수동으로 실행하면 안 됩니다. 일반 명령 실행 시 자동 기동됩니다.
:::

---

## Focusless-First 원칙

WKAppBot의 모든 동작은 포커스 탈취를 최소화합니다:

| 동작 | 방식 | 포커스 탈취 |
|------|------|-----------|
| `a11y click` (UIA 지원 요소) | UIA InvokePattern | ❌ 없음 |
| `a11y type` (UIA Value 지원) | UIA ValuePattern | ❌ 없음 |
| `a11y type` (Win32 레거시) | WM_CHAR PostMessage | ❌ 없음 |
| `a11y type --hotkey` | SendInput (EnsureFocus 후) | ⚠️ 최소 |
| `a11y click` (좌표 기반) | SendInput | ⚠️ 필요 시 |

**PromptDeliveryContext** — 프롬프트 주입 전 자동 판단:
1. 대상 창이 포어그라운드인가?
2. 최근 30초 내 입력이 있었나?
→ `Focusless` / `FocusSteal` / `Skip` / `Abort` 중 자동 선택

---

## 구독 티어

| 티어 | 월 요금 | 주요 기능 |
|------|--------:|----------|
| **Free** | 무료 | `a11y`, `run`, `skill`, `file`, `logcat`, Android |
| **CDP** | ₩100,000 | + 브라우저 자동화, `ask triad`, `schedule` |
| **Sudo** | ₩500,000 | + HTS 거래 터미널, 관리자 프로세스 접근 |

라이선스 확인은 GitHub 계정으로 — 별도 라이선스 파일 불필요:

```bash
gh auth login
wkappbot license status
```
```
tier: Free  (CDP: not subscribed)
github: kiexp  permission: read-only
```

---

## 다음 단계

- [CLI 명령어 전체](/reference/commands) — 24개 a11y 액션 + 출력 샘플
- [grap 패턴](/reference/grap) — 복잡한 UI 주소 작성
- [YAML 시나리오](/reference/scenarios) — 멀티스텝 자동화 파일 작성
