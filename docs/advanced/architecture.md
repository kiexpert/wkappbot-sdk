# 아키텍처

## 전체 구조

```
┌─────────────────────────────────────────────────────────┐
│  Your terminal / Claude Code / Codex / any AI agent     │
└───────────────────────┬─────────────────────────────────┘
                        │ wkappbot <command>
                        ▼
┌─────────────────────────────────────────────────────────┐
│  wkappbot.exe  (MIT launcher, ~1 MB, AOT)               │
│  • CLI 인자 → named pipe → core 라우팅                  │
│  • 핫스왑: .new.exe 감지 → drain → atomic rename        │
│  • GitHub 협력자 API로 라이선스 확인                    │
└───────────────────────┬─────────────────────────────────┘
                        │ named pipe  wkappbot_eye_ipc_{hash}
                        ▼
┌─────────────────────────────────────────────────────────┐
│  wkappbot-core.exe  (closed, ~25 MB, single-file)       │
│  • 모든 CLI 명령: a11y, ask, skill, eye, file, …        │
│  • AppBot Eye 데몬: Slack socket + MCP 브로커           │
│  • UIA / Win32 / CDP / ADB 자동화 엔진                  │
│  • Vision / OCR 파이프라인 + Experience DB              │
└─────────────────────────────────────────────────────────┘
         │ UIA / Win32                │ CDP (DevTools)
         ▼                            ▼
   Windows apps                 Chrome / Edge
   (WPF, MFC, UWP)              (web apps, AI chat)
```

각 git 클론은 독립적인 Eye 인스턴스와 DataDir(`{root}/.wkappbot/hq/`)을 갖습니다.

---

## 모듈 구조

| 모듈 | 역할 |
|------|------|
| `WKAppBot.CLI` | CLI 진입점, 명령 라우팅, grap 해석 |
| `WKAppBot.Core` | ScenarioRunner, ActionExecutor, AAR readiness |
| `WKAppBot.Win32` | NativeMethods, WindowFinder, UiaLocator, SendInput tiers |
| `WKAppBot.Vision` | ChartAnalyzer, SimpleOcrAnalyzer, VisionCache, CCA |
| `WKAppBot.WebBot` | CdpClient, ChromeLauncher, SlackSocketClient |
| `WKAppBot.Android` | AdbClient, AndroidA11yTree |
| `WKAppBot.Launcher` | 핫스왑 스테이징, 파이프 릴레이, 관리자 권한 상승 |

---

## 5-Tier Element Search

```
Tier 1: UIA (Accessibility API)
  └─ 실패 시 ↓
Tier 2: Vision Cache (이전 성공 좌표)
  └─ 실패 시 ↓
Tier 3: Simple OCR (텍스트 레이블)
  └─ 실패 시 ↓
Tier 4: Vision API — Claude / Gemini (스크린샷 AI 추론)
  └─ 실패 시 ↓
Tier 5: Coordinate-based (마지막 수단)
```

각 티어의 성공 결과는 `experience/` DB에 캐싱됩니다. 동일 요소의 다음 접근은 캐시 히트로 Tier 1~2를 직접 사용합니다.

---

## Self-Healing DYN-A11Y

MFC owner-drawn 컨트롤(UIA 없음) 처리 흐름:

```
CCA 세그멘테이션 (화면 영역 분할)
  ↓
OCR 삼중 교차검증 (3개 OCR 엔진 결과 비교)
  ↓
Gemini Vision 추론 (불일치 시 AI 판단)
  ↓
dyn_r{row}c{col} 동적 ID 생성
  ↓
Experience DB 캐싱 + CCA 파라미터 자동 튜닝
```

---

## AppBot Eye 내부

### Pipe 분리 (v6.0)

| 파이프 | 용도 |
|--------|------|
| `wkappbot_eye_ipc` | 일반 Eye ↔ Core (tick IPC) |
| `wkappbot_elevated` | Admin Eye ↔ Core (명령 프록시) |

두 파이프를 혼용하면 일반 Eye가 관리자 연결을 가로챕니다.

### 핫스왑 흐름

```
dotnet publish
  → wkappbot-core.new.exe 생성
  → Eye가 파일 변경 감지 (FSW)
  → 진행 중 요청 드레인 (최대 5초)
  → wkappbot-core.exe로 atomic rename
  → 신규 Core 자동 기동
```

제로 다운타임. Eye를 재시작하지 않아도 됩니다.

### Watchdog VBS

Eye가 2분 이상 응답하지 않으면:
1. 고아 `wkappbot-core.exe` 프로세스 전체 종료
2. `eye tick`으로 Eye 재시작

---

## PromptDeliveryContext

AI 프롬프트 주입 전 자동 결정 로직:

```
① 대상 창이 포어그라운드인가?
② 최근 30초 내 사용자 입력이 있었나?
↓
Focusless   → CDP eval / UIA SetValue (포커스 탈취 없음)
FocusSteal  → SendInput 사용 (최소한의 포커스)
Skip        → 이미 다른 입력 중, 보류
Abort       → 위험 상황, 취소
```

---

## CDP Ask 프롬프트 펌프

브라우저 AI에 프롬프트를 주입하는 방식:

- **청크 병합**: 텍스트 청크를 수집 후 1초 idle 시점에 일괄 전송
- **페이지 키**: `scope + targetId + editorSelector` 조합으로 페이지별 싱글턴 펌프
- **첨부 트랜잭션 잠금**: 이미지 첨부 시 업로드 완료까지 텍스트 큐에 보류 후 일괄 플러시

---

## 런타임 디렉토리

```
bin/
  wkappbot.exe           ← 공식 launcher (alias: a11y.exe)
  wkappbot-core.exe      ← core 워커 (핫스왑 대상)
  wkappbot.hq/
    experience/          ← UI 요소 학습 캐시 (git tracked)
    skills/              ← 설치된 스킬 (sync 대상)
    logs/                ← 운영 로그 (git ignored)
    runtime/             ← Slack 큐 등 런타임 상태 (git ignored)
    sessions/            ← 세션 데이터 (git ignored)
```

.NET 8.0 · `net8.0-windows10.0.22621.0` · Korean UI 지원
