# WKAppBot - Windows App Automation Test Framework

## Overview
Claude Code 터미널에서 사용하는 범용 Windows 앱 UI 자동화 테스트 도구.
YAML 시나리오 기반으로 Windows 앱을 자동 조작하고 결과를 검증한다.

## Architecture

### 핵심 전략: 4티어 요소 탐색 (Chain of Responsibility)
1. **Accessibility API (UIA)** - FlaUI 라이브러리, AutomationId/Name/ControlType로 탐색
2. **Vision Cache** - 이전 Vision 결과 캐시 (상대좌표, 2레벨: 메모리+디스크)
3. **Vision API (Claude)** - 스크린샷 + Claude API로 시각적 탐색 → 결과 캐시 저장
4. **좌표 기반** - 절대 좌표 (x, y) 직접 지정

### 전투 비유 🎯
```
[정찰] WATCH  — UI 요소 위치 파악
[확보] FOCUS  — 윈도우 포커스 확보 (위치확보!)
[조준] LOCATE — UIA → Vision캐시 → Vision API → 좌표
[사격] INPUT  — Click / Type / Hotkey
[판정] ASSERT — 전과 확인
[방어] CACHE  — 경험치 축적 + 노이즈 필터링
```

### 프로젝트 구조
```
W:/GitHub/WKAppBot/
├── csharp/                          # C# .NET 8.0 구현 (메인)
│   ├── WKAppBot.sln
│   └── src/
│       ├── WKAppBot.CLI/              # CLI 진입점 (run/validate/inspect/focus/watch/capture)
│       │   └── Program.cs
│       ├── WKAppBot.Core/             # 핵심 로직
│       │   ├── Runner/
│       │   │   ├── ScenarioRunner.cs    # 시나리오 오케스트레이터
│       │   │   ├── ActionExecutor.cs    # 액션 실행기 (click, type, assert 등) + EnsureFocus
│       │   │   ├── BackgroundWatcher.cs # [WATCH] 패시브 백그라운드 요소 추적
│       │   │   └── RuntimeContext.cs    # 런타임 상태 (변수, ActionPoint, StepResult, FocusConfig)
│       │   └── Scenario/
│       │       ├── Models.cs            # YAML 데이터 모델 (ScenarioConfig에 focus 설정 포함)
│       │       └── ScenarioParser.cs    # YAML 파싱/검증
│       ├── WKAppBot.Win32/            # Win32 네이티브 계층
│       │   ├── Native/
│       │   │   └── NativeMethods.cs     # P/Invoke 선언 (user32, gdi32, shcore) + SmartSetForegroundWindow
│       │   ├── Window/
│       │   │   ├── WindowFinder.cs      # 윈도우 탐색/포커스/계층경로 + SmartBringToFront
│       │   │   ├── HtsInterop.cs        # HTS 전용 MDI 조작
│       │   │   └── ControlMap.cs        # 컨트롤 매핑
│       │   ├── Accessibility/
│       │   │   └── UiaLocator.cs        # FlaUI UIA 요소 탐색/조작/트리덤프
│       │   └── Input/
│       │       ├── MouseInput.cs        # SendInput 마우스 (절대좌표)
│       │       ├── KeyboardInput.cs     # SendInput 키보드 + VK 매핑
│       │       └── ScreenCapture.cs     # 윈도우 스크린샷 캡처 (focusless — PrintWindow/BitBlt)
│       ├── WKAppBot.Vision/           # Vision 계층 (Claude API + 2레벨 캐시)
│       │   ├── VisionAnalyzer.cs        # Claude Vision API 호출 (HttpClient, base64 PNG)
│       │   ├── VisionCache.cs           # 2레벨 캐시 (메모리+디스크 JSON, SHA256 키)
│       │   └── VisionCacheEntry.cs      # 캐시 엔트리 모델 (상대좌표, per-control 학습)
│       └── WKAppBot.Report/           # 리포트 생성 (스켈레톤)
├── scenarios/                       # YAML 테스트 시나리오
│   ├── calc_basic.yaml              # 계산기 기본 (15+27=42)
│   └── calc_four_ops.yaml           # 계산기 4칙연산 (32 steps, PoC)
├── wkappbot/                        # Python 구현 (초기 버전, 미사용)
└── output/
    └── screenshots/
```

## Build & Run

### dotnet 경로
```
W:\SDK\dotnet\dotnet.exe
```

### 빌드 (bash)
```bash
'W:/SDK/dotnet/dotnet.exe' build 'W:/GitHub/WKAppBot/csharp/WKAppBot.sln' -c Release --verbosity quiet
```

### 빌드 (PowerShell)
```powershell
& 'W:\SDK\dotnet\dotnet.exe' build 'W:\GitHub\WKAppBot\csharp\WKAppBot.sln' -c Release --verbosity quiet
```

### 실행 (4칙연산 테스트 — bash)
```bash
DOTNET_ROOT='W:/SDK/dotnet' 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/bin/Release/net8.0-windows/wkappbot.exe' run 'W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml' -v
```

### 실행 (PowerShell)
```powershell
$env:DOTNET_ROOT = 'W:\SDK\dotnet'
& 'W:\GitHub\WKAppBot\csharp\src\WKAppBot.CLI\bin\Release\net8.0-windows\wkappbot.exe' run 'W:\GitHub\WKAppBot\scenarios\calc_four_ops.yaml' -v
```

### CLI 명령어
```
wkappbot run <scenario.yaml> [-v] [--no-watch] [--watch-interval N]
wkappbot validate <scenario.yaml>
wkappbot inspect <window-title> [--depth N]
wkappbot focus [--title <text>] [--delay N] [--depth N] [--win32] [-b]
wkappbot watch [--duration N] [--live] [--win32] [--interval N] [--save file]
wkappbot capture <window-title> [-o output.png]
wkappbot hts-stress <form.xmf> [-n 100]
```

## Key Design Decisions

### 1. Focusless-First 원칙 ("사용자 방해 금지!")
**핵심**: 포커스 없이 가능한 입력은 포커스 확보 없이 진행 → 사용자 작업 방해 최소화!

| 입력 유형 | Focusless? | 방법 |
|-----------|-----------|------|
| Click (UIA Invoke) | ✅ YES | `TryInvoke()` — 포커스 불필요! |
| Click (좌표) | ❌ NO | SendInput → EnsureFocus |
| TypeText (UIA Value) | ✅ YES | `Patterns.Value.SetValue()` — 포커스 불필요! |
| TypeText (SendInput) | ❌ NO | KeyboardInput → EnsureFocus |
| PressKey/Hotkey | ❌ NO | SendInput → EnsureFocus |
| Scroll | ❌ NO | SendInput → EnsureFocus |
| Assert | ✅ YES | UIA GetText — 포커스 불필요 |
| Screenshot | ✅ YES | PrintWindow/BitBlt — 포커스 불필요 |
| Wait | ✅ YES | Thread.Sleep |

### 2. Smart Focus ("위치확보!") — EnsureFocus 4단계
SendInput이 반드시 필요한 경우에만 작동:
```
Phase 0: Already Focused? → return (0ms)
Phase 1: Alert + Wait (alertDelay=3초)
  → MessageBeep 경고음 + FlashWindow 깜빡임
  → 사용자가 자발적으로 포커스 전환할 시간 제공
Phase 2: Force Recovery (나머지 timeout)
  → AttachThreadInput trick → SetForegroundWindow
  → 2초마다 경고음 반복
Phase 3: Timeout → 스텝 Fail
```

**출력 태그**: `[FOCUS]`
```
[FOCUS] Focus lost — alerting user...
[FOCUS] Focus restored by user ← 1200ms
또는
[FOCUS] Forcing focus recovery...
[FOCUS] Focus recovered ← 3500ms
또는
[FOCUS] FOCUS RECOVERY FAILED — timeout after 5.0s
```

### 3. 스텝 출력 구조 (스토리텔링)
각 스텝은 사람이 테스트하듯 완전한 이야기로 출력:
```
─── [1/32] [Addition] Click Plus ──────────────────
[WATCH] [ApplicationFrameWindow/Windows.UI.Core.CoreWindow] 계산기/계산기/NavView/Group/표시는 15
[WATCH] 14:30:01.234  ( 1234,  567)  [Text] "표시는 15" aid="CalculatorResults"  ← click
[RUN] Click plusButton (Invoke, automation_id, focusless)
[WATCH] [ApplicationFrameWindow/Windows.UI.Core.CoreWindow] 계산기/계산기/NavView/Group/표시는 15 +
[WATCH] 14:30:01.456  ( 1234,  567)  [Text] "표시는 15 + " aid="CalculatorResults"  ← click
[RUN] → PASS (42ms)
```

### 4. 태그 규칙
- **[WATCH]**: 백그라운드 와처의 모든 요소 추적 출력 (통일)
- **[RUN]**: 테스트 실행 관련 출력 (액션 상세, 결과 판정)
- **[FOCUS]**: 포커스 확보 관련 출력 (경고, 복구, 실패)

### 5. BackgroundWatcher Nudge/Ack 핸드셰이크
- `Nudge()`: ScenarioRunner가 각 스텝 전/후에 호출 → 백그라운드 스레드 깨우기
- `_nudgeAck.Wait(2000)`: 와치 출력이 완료될 때까지 대기 후 다음 스텝 진행
- 이유: "테스트입력은 사람보다 빠를필요는 없으므로 와치출력이 완료된 이후에 해도 될것 같아요"
- COM threading: UIA3Automation COM 객체는 생성 스레드에서만 사용 가능 → 백그라운드 스레드에서 PollOnce

### 6. Win32 클래스 경로 (ChildWindowFromPointEx)
- `GetWindowHierarchyPathAtPoint()`: hWnd에서 시작해 화면 좌표의 자식 윈도우로 하향 탐색
- `ChildWindowFromPointEx` + `ScreenToClient`로 각 레벨 탐색
- 결과: `[ApplicationFrameWindow/Windows.UI.Core.CoreWindow]`

### 7. 오버레이 감지
- Chrome 확장 등 투명 오버레이 윈도우를 Z-order 탐색으로 건너뜀
- `WS_EX_TRANSPARENT`, `WS_EX_LAYERED` 스타일 체크
- UIA AutomationId `BTN_BKGRND` 등 알려진 오버레이 패턴 감지

### 8. ActionDetail (Focusless + Vision 태그 포함)
- `StepResult.ActionDetail`: 각 액션이 실제로 한 일의 사람 읽기 좋은 설명
- 형식:
  - `Click plusButton (Invoke, automation_id, focusless)` ← UIA Invoke (포커스 불필요!)
  - `Click plusButton (320,550) (automation_id)` ← Invoke 실패 → SendInput
  - `Click (234, 567) (vision_cache, conf=0.95)` ← Vision 캐시 히트!
  - `Click (234, 567) (vision_api, conf=0.92)` ← Vision API 호출 (새로 캐시)
  - `Click (234, 567) (coordinate)` ← 좌표 폴백
  - `Type "15" (UIA Value, focusless)` ← Value 패턴 (포커스 불필요!)
  - `Type "15"` ← SendInput 폴백
  - `Assert text_contains "42" on CalculatorResults → "표시는 42"`

### 9. Vision Cache DB ("경험치 축적")
- **2레벨 캐시**: ConcurrentDictionary(메모리) + JSON 파일(디스크)
- **캐시 키**: `SHA256(class_path | description | WxH)[:16]`
  - class_path: `ApplicationFrameWindow/Windows.UI.Core.CoreWindow`
  - description: YAML의 `target.description`
  - 윈도우 크기: 리사이즈 시 좌표 무효화
- **상대 좌표**: (0.0~1.0) — 윈도우 위치 변경에도 유효
- **Per-control 학습**: hit_count, success_count, fail_count → SuccessRate
  - 성공률 < 30% 시 캐시 자동 무효화 (stale 판정)
- **TTL 만료**: 기본 7일 (앱 업데이트로 UI 변경 대비)
- **디스크 저장 형식**: JSON (`data/vision_cache/entries/<hash>.json`)
- **Future**: Simple OCR 모델 우선 → Claude API는 고비용 최종 폴백

### 10. VisionAnalyzer (Claude API)
- `ANTHROPIC_API_KEY` 환경변수 필요
- HttpClient로 Claude Messages API 직접 호출
- 스크린샷 base64 PNG → `image/png` media type
- 프롬프트: "Find '{description}' → JSON {x, y, width, height, confidence, label}"
- 응답에서 JSON 파싱 (markdown code fence 자동 제거)
- `confidenceThreshold` 미달 시 null 반환

## YAML Scenario Format
```yaml
scenario:
  name: "Calculator Four Operations Test"
app:
  launch: "calc.exe"
  wait_for_window:
    title_contains: "계산기"
config:
  continue_on_error: true
  retry_count: 2
  # Smart Focus config (위치확보)
  focus_check: true         # default: true — SendInput 전 포커스 확인
  focus_timeout: 5.0        # 포커스 복구 총 타임아웃 (초)
  focus_retry_delay: 0.3    # 재시도 간격 (초)
  focus_alert_delay: 3.0    # 알림 후 대기 시간 (초) — 사용자 자발적 복귀 기회
  prefer_focusless: true    # default: true — UIA Invoke/Value 우선
  # Vision Cache config (경험치 축적)
  vision_enabled: false                  # opt-in (API 비용)
  vision_cache_dir: "data/vision_cache/entries"
  vision_cache_ttl_days: 7
  vision_confidence_threshold: 0.7
  vision_model: "claude-sonnet-4-20250514"
steps:
  - name: "[Addition] Click Plus"
    target:
      automation_id: "plusButton"
      description: "The plus/addition button"  # Vision 폴백용 (UIA 실패 시)
    action: click
  - name: "Verify result"
    target:
      automation_id: "CalculatorResults"
    action: assert
    params:
      type: text_contains
      expected: "42"
teardown:
  - action: hotkey
    params: { keys: ["alt", "f4"] }
```

### 지원 액션
| 액션 | 설명 | target | params |
|------|------|--------|--------|
| `click` | 클릭 (UIA Invoke 우선, 좌표 폴백) | automation_id/name/x,y | - |
| `double_click` | 더블클릭 | automation_id/name/x,y | - |
| `right_click` | 우클릭 | automation_id/name/x,y | - |
| `type_text` | 텍스트 입력 (UIA Value 우선 → SendInput 폴백) | - | text |
| `press_key` | 단일 키 입력 | - | key |
| `hotkey` | 복합 키 입력 | - | keys[] |
| `wait` | 대기 | - | seconds |
| `assert` | 값 검증 (focusless) | automation_id/name | type, expected |
| `scroll` | 스크롤 | - | direction, amount |
| `screenshot` | 스크린샷 저장 (focusless — PrintWindow) | - | filename |

### Assert 타입
- `text_contains`, `text_equals`, `text_starts_with`, `text_not_empty`

## Dependencies (NuGet)
- **FlaUI.UIA3** - UI Automation (요소 탐색/조작)
- **YamlDotNet** - YAML 파싱
- **System.Drawing.Common** - 스크린샷
- **System.Text.Json** - Vision 캐시 JSON 직렬화
- **Anthropic SDK** (HttpClient 직접 호출) - Claude Vision API

## Current Status
- **PoC 완료**: Windows 계산기 4칙연산 32/32 PASS
- **구현 완료**: Accessibility 탐색, SendInput 입력, Background Watch, 스텝 스토리텔링 출력
- **Phase 1 완료**: Smart Focus (Focusless-First + EnsureFocus 4단계 + [FOCUS] 태그)
- **Phase 2 완료**: Vision Cache DB (4티어 로케이터, 2레벨 캐시, Claude API, per-control 학습)
- **미구현**: Simple OCR Vision 모듈 (Claude 폴백 전 1차 시도), HTML 리포트, DPI 스케일링

## Important Notes
- Windows 전용 (.NET 8.0 `net8.0-windows`)
- 한국어 UI 지원 (UTF-8 출력, Unicode Win32 API)
- UWP 앱 (ApplicationFrameHost) 지원: 타이틀 검색 전략 포함
- 계산기 PoC 시나리오가 기본 검증 테스트
- **dotnet.exe 경로**: `W:\SDK\dotnet\dotnet.exe`
