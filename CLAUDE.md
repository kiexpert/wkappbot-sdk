# WKAppBot - Windows App Automation Test Framework

## Overview
Claude Code 터미널에서 사용하는 범용 Windows 앱 UI 자동화 테스트 도구.
YAML 시나리오 기반으로 Windows 앱을 자동 조작하고 결과를 검증한다.

## Architecture

### 핵심 전략: 5티어 요소 탐색 (Chain of Responsibility)
1. **Accessibility API (UIA)** - FlaUI 라이브러리, AutomationId/Name/ControlType로 탐색
2. **Vision Cache** - 이전 Vision/OCR 결과 캐시 (상대좌표, 2레벨: 메모리+디스크)
3. **Simple OCR (Windows.Media.Ocr)** - 무료, 오프라인, 한글+영어, 텍스트 매칭
4. **Vision API (Claude)** - 스크린샷 + Claude API로 시각적 탐색 (고비용 최종 폴백)
5. **좌표 기반** - 절대 좌표 (x, y) 직접 지정

### 전투 비유 🎯
```
[정찰] WATCH  — UI 요소 위치 파악
[확보] FOCUS  — 윈도우 포커스 확보 (위치확보!)
[조준] LOCATE — UIA → Vision캐시 → OCR → Claude API → 좌표
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
│       ├── WKAppBot.CLI/              # CLI 진입점 (run/validate/inspect/focus/watch/capture/hts-stress)
│       │   └── Program.cs
│       ├── WKAppBot.Core/             # 핵심 로직
│       │   ├── Runner/
│       │   │   ├── ScenarioRunner.cs    # 시나리오 오케스트레이터
│       │   │   ├── ActionExecutor.cs    # 액션 실행기 (click, type, assert 등) + EnsureFocus
│       │   │   ├── BackgroundWatcher.cs # [WATCH] 패시브 백그라운드 요소 추적
│       │   │   ├── RuntimeContext.cs    # 런타임 상태 (변수, ActionPoint, StepResult, FocusConfig)
│       │   │   └── DialogHandlerManager.cs # [BLOCK] 방해꾼 핸들러 로더/매처/학습
│       │   └── Scenario/
│       │       ├── Models.cs            # YAML 데이터 모델 (ScenarioConfig에 focus 설정 포함)
│       │       ├── ScenarioParser.cs    # YAML 파싱/검증
│       │       └── DialogHandler.cs     # 방해꾼 핸들러 YAML 모델
│       ├── WKAppBot.Win32/            # Win32 네이티브 계층
│       │   ├── Native/
│       │   │   └── NativeMethods.cs     # P/Invoke 선언 (user32, gdi32, shcore)
│       │   ├── Window/
│       │   │   ├── WindowFinder.cs      # 윈도우 탐색/포커스/계층경로
│       │   │   ├── AppScanner.cs        # 앱 풀스캔 (존 분류 + MDI + OCR 학습)
│       │   │   ├── ExperienceDb.cs      # 경험 DB (컨트롤 맵, puppet pattern, detail cache)
│       │   │   ├── AppProfile.cs        # 앱 프로파일 (프로세스/클래스 매칭)
│       │   │   ├── ZoneClassifier.cs    # Win32 자식창 의미 분류 (toolbar/mdi/form/...)
│       │   │   ├── FormTypeIdentifier.cs # 폼 타입 식별 (fingerprint/keyword/puppet)
│       │   │   ├── HtsInterop.cs        # HTS MDI 스트레스 테스트 (3패턴)
│       │   │   └── ControlMap.cs        # 컨트롤 매핑
│       │   ├── Accessibility/
│       │   │   └── UiaLocator.cs        # FlaUI UIA 요소 탐색/조작 + PatternMatcher
│       │   └── Input/
│       │       ├── MouseInput.cs        # SendInput 마우스 (절대좌표)
│       │       ├── KeyboardInput.cs     # SendInput 키보드 + VK 매핑
│       │       └── ScreenCapture.cs     # 스크린샷 (PrintWindow + SWP_NOACTIVATE 폴백)
│       ├── WKAppBot.Vision/           # Vision 계층 (OCR + Claude API + 캐시)
│       │   ├── SimpleOcrAnalyzer.cs     # Windows.Media.Ocr (무료, 오프라인, 한글+영어)
│       │   ├── VisionAnalyzer.cs        # Claude Vision API 호출 (고비용 폴백)
│       │   ├── VisionCache.cs           # 2레벨 캐시 (메모리+디스크 JSON, SHA256 키)
│       │   └── VisionCacheEntry.cs      # 캐시 엔트리 모델 (상대좌표, per-control 학습)
│       └── WKAppBot.Report/           # 리포트 생성 (스켈레톤)
├── handlers/                        # 방해꾼 다이얼로그 핸들러 YAML (빌드 시 자동 복사)
│   ├── #32770.yaml                  # nkrunlite 범용 다이얼로그 핸들러
│   └── 접속끊김.yaml                # 접속 끊김 전용 핸들러 (30s 대기)
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
DOTNET_ROOT='W:/SDK/dotnet' 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/bin/Release/net8.0-windows10.0.22621.0/wkappbot.exe' run 'W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml' -v
```

### 실행 (PowerShell)
```powershell
$env:DOTNET_ROOT = 'W:\SDK\dotnet'
& 'W:\GitHub\WKAppBot\csharp\src\WKAppBot.CLI\bin\Release\net8.0-windows10.0.22621.0\wkappbot.exe' run 'W:\GitHub\WKAppBot\scenarios\calc_four_ops.yaml' -v
```

### CLI 명령어
```
wkappbot run <scenario.yaml> [-v] [--no-watch] [--watch-interval N]
wkappbot validate <scenario.yaml>
wkappbot inspect <window-title> [--depth N]
wkappbot focus [--title <text>] [--delay N] [--depth N] [--win32] [-b]
wkappbot watch [--duration N] [--live] [--win32] [--interval N] [--save file]
wkappbot capture <window-title> [-o output.png]
wkappbot hts-stress <form.xmf> [-n 20] [--pattern repeat|memory|ctx-only]
wkappbot scan <window-title> [--save] [--ocr] [--detail] [--depth N]
```

### scan 명령 (앱 풀스캔 + OCR 학습 + Experience DB)
```bash
# 기본 스캔 (존 분류 + MDI 폼 열거)
wkappbot scan "영웅문"

# OCR 학습 + 프로파일 저장 (puppet pattern 자동 생성, 2회 이상 실행 필요)
wkappbot scan "영웅문" --ocr --save

# 컨트롤 디테일 캐시 (per-control 스크린샷 + 텍스트 히스토리)
wkappbot scan "영웅문" --ocr --save --detail
```
- `--ocr`: OCR 학습 (LearnFormsWithOcr) — 컨트롤별 OCR 텍스트 매칭 + fingerprint + keywords
- `--save`: 프로파일 + Experience DB JSON 저장
- `--detail`: per-control latest.png + text_history.jsonl 저장 (opt-in, 시간 소요)
- `--depth N`: 트리 탐색 깊이 (기본 1)

### HTS 스트레스 테스트 옵션
```
wkappbot hts-stress <form.xmf> [options]

--pattern <name>  테스트 패턴: repeat|memory|ctx-only (default: repeat)
-n N              반복 횟수 (default: 20)
--delay N         액션 간 딜레이 ms (default: 800)
--open-ms N       repeat 패턴 open 딜레이 (default: 1000)
--close-ms N      repeat 패턴 close 딜레이 (default: 1000)
--open N          memory 패턴 사이클당 open 수 (default: 3)
--close N         memory 패턴 사이클당 close 수 (default: 1)
--batch N         ctx-only 패턴 배치 크기 (default: 1)
--process <name>  대상 프로세스 이름 (default: HTSRun)
--no-watch        [WATCH] 백그라운드 추적 비활성화
--watch-interval  와처 폴링 ms (default: 500)
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
[WATCH] [ApplicationFrameWindow/Windows.UI.Core.CoreWindow] 계산기/NavView/Group/표시는 15
[WATCH] [Text] "표시는 15" aid="CalculatorResults" (Value)
[WATCH] OCR="표시는 15" conf=0.95★  UIA↔OCR=100%★  watch/001_CalculatorResults.png
[WATCH] ← click  ( 1234,  567) 14:30:01.234
[RUN] Click plusButton (Invoke, automation_id, focusless)
[RUN] → PASS (42ms)
```

**WATCH 4줄 레이아웃** (stable LEFT → volatile RIGHT):
- Line 1: `[WATCH]` [ClassPath] UIA 이름 경로
- Line 2: `[WATCH]` [Type] "Name" aid="id" (patterns)
- Line 3: `[WATCH]` OCR="text" conf=★ UIA↔OCR=% screenshot (nudge 시에만)
- Line 4: `[WATCH]` ← action (x,y) timestamp (volatile, `\r` 덮어쓰기)

### 4. 태그 규칙
- **[WATCH]**: 백그라운드 와처의 모든 요소 추적 출력 (통일)
- **[RUN]**: 테스트 실행 관련 출력 (액션 상세, 결과 판정)
- **[FOCUS]**: 포커스 확보 관련 출력 (경고, 복구, 실패)
- **[VERIFY]**: SendInput 전 대상 좌표 검증 출력 (렉트/오버레이/요소 변화)
- **[STRESS]**: HTS 스트레스 테스트 메모리 테이블 행
- **[BLOCK]**: 방해꾼 다이얼로그 감지/처리/학습 출력

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

### 8. ActionDetail (Focusless + Vision + Verify 태그 포함)
- `StepResult.ActionDetail`: 각 액션이 실제로 한 일의 사람 읽기 좋은 설명
- 형식:
  - `Click plusButton (Invoke, automation_id, focusless)` ← UIA Invoke (포커스 불필요!)
  - `Click plusButton (320,550) (automation_id, verify=OK)` ← SendInput + 검증 OK
  - `Click plusButton (320,550) (automation_id, verify=WARN(overlay))` ← 방해 창 감지!
  - `Click (234, 567) (vision_cache, conf=0.95)` ← Vision 캐시 히트!
  - `Click (234, 567) (simple_ocr, conf=0.90, "확인")` ← OCR 텍스트 매칭 (무료!)
  - `Click (234, 567) (vision_api, conf=0.92)` ← Claude API 호출 (고비용 폴백)
  - `Click (234, 567) (coordinate, verify=OK)` ← 좌표 폴백 + 검증
  - `Type "15" (UIA Value, focusless)` ← Value 패턴 (포커스 불필요!)
  - `Type "15"` ← SendInput 폴백
  - `Assert text_contains "42" on CalculatorResults → "표시는 42"`

### 8-1. Pre/Post Action Verification ([VERIFY] 태그)
SendInput 클릭 전 대상 좌표의 요소를 검증하는 3가지 체크:
1. **렉트 안에 있는가?** — 클릭 좌표가 UIA BoundingRectangle 안인지
2. **방해꾼 없는가?** — `WindowFromPoint`로 오버레이 창 감지
3. **대상 요소가 맞는가?** — 포커스 전/후 요소 비교 + AutomationId 일치 확인

```
verify=OK                           ← 렉트 안 + 방해꾼 없음 + 요소 일치
verify=WARN(overlay(Chrome_Widget)) ← 다른 창이 위에 있음!
verify=WARN(outside_bounds)         ← 좌표가 렉트 밖으로 벗어남!
verify=WARN(element_changed)        ← 포커스 전후로 다른 요소가 됨!
verify=WARN(aid_mismatch)           ← AutomationId가 기대값과 불일치!
```

**[VERIFY] 출력**: 경고 시에만 출력 (실행을 차단하지 않음)
```
[VERIFY] Overlay detected at (320,550): class="Chrome_WidgetWin_1" blocking target window
[VERIFY] Click point (320,550) outside element rect: [Button]"Plus" at (300,540 60x40)
```

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

### 10. Simple OCR (Windows.Media.Ocr — Phase 3)
- **무료, 오프라인, 한국어+영어 내장** (Windows 10/11)
- `Windows.Media.Ocr.OcrEngine` WinRT API 사용
- TFM: `net8.0-windows10.0.22621.0` (WinRT 접근 필요)
- 매칭 전략: 라인 매칭 → 단어 매칭 (정확/포함/시작/Dice 유사도)
- 신뢰도 산출: exact=1.0, contains=0.9, starts_with=0.8, partial=0.7, fuzzy=Dice*0.6
- `RecognizeAll()`: 전체 텍스트 + 단어별 위치 (assert 검증용)
- Vision 폴백 시 디버그 스크린샷 저장: `{vision_cache_dir}/screenshots/{timestamp}_{desc}.png`
- **위치**: UIA 실패 후 첫 번째 시도 (Claude API 전)

### 11. HTS MDI 스트레스 테스트 (HtsInterop)
VIGSOne `leak_test_*.ps1` 3가지 패턴을 C#으로 포팅:

| 패턴 | 원본 스크립트 | 동작 |
|------|-------------|------|
| **repeat** | `leak_test_repeat.ps1` | 폼 open/close N회 반복 |
| **memory** | `leak_test_memory.ps1` | 사이클당 open M / close K + 메모리 테이블 |
| **ctx-only** | `leak_test_ctx_only.ps1` | 앵커 폼 유지 + 2nd 폼 반복 (V8 Context 격리) |

- `WM_COPYDATA` (dwData=100) + CP949 경로로 HTS 폼 오픈
- `WM_MDIGETACTIVE` + `WM_CLOSE`로 MDI 자식 닫기
- `Process.WorkingSet64` / `PrivateMemorySize64`로 메모리 추적
- per-cycle KB, per-context KB 자동 계산
- `[STRESS]` 태그 + 색상 코드: green(<5MB), yellow(5~20MB), red(>20MB)
- Background `[WATCH]` 통합: 사이클 경계에서 Nudge → MDI 변화 추적

### 12. 패턴 매칭 (PatternMatcher)
UIA Name/AutomationId에 와일드카드/정규식 매칭 지원:

| 구문 | 예시 | 동작 |
|------|------|------|
| 리터럴 | `"plusButton"` | 정확 일치 (기본, 하위호환) |
| 와일드카드 | `"*Button*"`, `"결과*"`, `"?u?"` | glob 스타일 (* = 0+문자, ? = 1문자) |
| 정규식 | `"regex:btn_\\d+"` | 정규식 (대소문자 무시) |

- `PatternMatcher.IsPattern()`: 패턴 구문 감지 → 리터럴이면 빠른 UIA PropertyCondition 사용
- 패턴이면 `FindAllDescendants` BFS 트리 워크 → predicate 필터 (maxDepth=6)
- `FindElement()`에서 자동 분기: exact match 먼저 → 패턴이면 BFS 폴백
- 반환 메서드: `"automation_id_pattern"`, `"name_pattern"` (기존 `"automation_id"`, `"name"`과 구분)

### 13. VisionAnalyzer (Claude API — 고비용 최종 폴백)
- `ANTHROPIC_API_KEY` 환경변수 필요 (없으면 이 티어 스킵)
- HttpClient로 Claude Messages API 직접 호출
- 스크린샷 base64 PNG → `image/png` media type
- OCR이 실패하거나 저신뢰일 때만 호출
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
- **Phase 2 완료**: Vision Cache DB (2레벨 캐시, Claude API, per-control 학습)
- **Phase 3 완료**: Simple OCR (Windows.Media.Ocr, 한글+영어, 5티어 체인 완성)
- **HTS Stress 완료**: 3패턴 스트레스 테스트 (repeat/memory/ctx-only) + [STRESS] 메모리 테이블
- **WATCH 레이아웃 완료**: 4줄 구조 (hierarchy/element/OCR/volatile), OCR + 스크린샷 on nudge
- **패턴 매칭 완료**: 와일드카드 (`*`, `?`) + 정규식 (`regex:`) UIA Name/AutomationId 매칭
- **Pre/Post 검증 완료**: [VERIFY] — 렉트 내 좌표, 오버레이 감지, 요소 안정성 확인
- **HTS 캐치 자동화 완료**: 콤보선택 → 매매시작 → 확인 다이얼로그 자동처리 (`do` 커맨드)
- **SmartClickButton 완료**: 3티어 클릭 전략 (BM_CLICK → WM_LBUTTON → SendInput) + 창변화/모달 감지
- **Phase 4 완료**: `scan` CLI + AppScanner + ExperienceDB + AppProfile + WM_GETTEXT + 스크린샷 가림방지(SWP_NOACTIVATE)
- **Phase 5 완료**: LCS 포펫 패턴 — 라인 정렬 + 토큰 단위 diff + 연속 와일드카드 병합
- **Phase 6 완료**: 컨트롤 디테일 캐시 — per-control latest.png + text_history.jsonl (`--detail` 플래그)
- **Phase 7 완료**: 클릭 전략 DB 연동 — SmartClickButton이 ExperienceDb 성공률 기반 전략 순서 최적화
- **Dialog Handlers 완료**: YAML 기반 방해꾼 자동처리 + EnumWindows 감지 + 마우스 호버 인터랙티브 학습
- **미구현**: 아래 로드맵 참조

## 구현 로드맵 (Implementation Roadmap)

### Phase 4: Experience DB 실전 연동 — "풀스캔 → DB 저장" ✅
**상태**: 완료 (commit `54839cc`)
- `wkappbot scan <title> [--save] [--ocr]` CLI 커맨드
- AppScanner: 존 분류(ZoneClassifier) + MDI 폼 열거 + OCR 학습(LearnFormsWithOcr)
- ExperienceDb: form_*.json 파일 저장, 컨트롤 메타데이터, fingerprint, OCR keywords
- AppProfile + ProfileStore: 프로세스명/클래스명 기반 프로파일 자동 매칭
- WM_GETTEXT 수집 → ControlExperience.WmGetText 저장
- 스크린샷 가림방지: SWP_NOACTIVATE로 Z-order 최상위 이동 후 캡처
- 검증: 영웅문 9개 폼 타입 × 137개 컨트롤 학습 확인

### Phase 5: LCS 포펫 패턴 — "토큰 단위 diff + 라인 정렬" ✅
**상태**: 완료 (commit `fda9fb6`)
- BuildPuppetPattern: LCS 기반 라인 정렬 (라인 추가/삭제 대응)
- 토큰 단위 LCS diff: 토큰 수 변경에도 안정적 (DiffTokensLcs)
- 연속 와일드카드 병합: `{*} {*} {*}` → `{*}` (MergeConsecutiveWildcards)
- TextSnapshots FIFO 5개, scan_count >= 2에서 자동 패턴 생성
- 검증: 영웅문 휴일 스캔 → 0 dynamic fields (정상: 시세 미변동)

### Phase 6: 컨트롤 디테일 캐시 — "스크린샷 + 텍스트 히스토리" ✅
**상태**: 완료 (commit `0f32ded`)
- `--detail` 플래그: per-control 스크린샷 + 텍스트 히스토리 (opt-in)
- 폴더: `profiles/{profile}_exp/form_{formId}/controls/cid_{N}/`
- latest.png: 매 스캔마다 덮어쓰기 (CaptureScreenRegion)
- text_history.jsonl: append-only, dedup (텍스트 변경 시에만 추가)
- ExperienceDb: SaveControlScreenshot, AppendTextHistory, GetControlDir
- 검증: 194개 스크린샷 저장, 2차 스캔 dedup 정상 (194→64 text changes)

### Phase 7: 클릭 전략 DB 연동 — "SmartClick이 경험에서 배운다" ✅
**상태**: 완료
- SmartClickButton: 전략 루프화 (Dictionary + foreach), ExperienceDb optional 연동
- `GetBestClickOrder()` → 성공률 기반 전략 순서 최적화 (default: bm_click → wm_lbutton → send_input)
- 매 시도마다 `RecordClickStrategy()` → 결과 기록 + `SaveAll()` 즉시 디스크 저장
- DoCommand: 인라인 BM_CLICK+WM_LBUTTON → SmartClickButton 호출로 통합 (SendInput 폴백 자동 포함)
- DoCommand: ProfileStore 매칭 → ExperienceDb 자동 로드 (best-effort)
- `[EXP]` 태그: 경험 데이터 있을 때 전략 순서+성공률 표시

### Dialog Handlers — "방해꾼 자동처리 + 인터랙티브 학습" ✅
**상태**: 완료
- `handlers/*.yaml` 파일 기반 방해꾼 자동 감지+처리 프레임워크
- **파일명 = 키워드 트리거**: 파일명(확장자 제외)이 classPath+title에 포함되면 매칭
- **classPath 계층**: `_NKHeroMainClass/#32770` — GetParent 체인으로 구축
- **2단계 감지**: (1) GetForegroundWindow, (2) EnumWindows 프로세스 전체 스캔 (owned popup 감지)
- **YAML 조건**: title_contains, class, process, message_contains (OCR 폴백 포함)
- **지원 액션**: click_button (button_text/button_index), dismiss (ESC/Alt+F4), report
- **인터랙티브 학습**: 미지 방해꾼 → 마우스 호버 1초 → 버튼 선택 → 핸들러 YAML 자동 생성
- **DoCommand 연동**: pre-action + post-action 체크, retry 루프, --confirm 하위호환
- **csproj 자동 복사**: 소스 `handlers/` → 빌드 출력 자동 복사
- DialogHandler.cs (YAML 모델), DialogHandlerManager.cs (로더/매처/샘플생성)
- `[BLOCK]` 태그: 방해꾼 감지/처리/학습 출력

### Phase 8: 포펫 패턴 매칭 — "이미지만으로 폼 식별"
**목표**: FormTypeIdentifier Level 4로 puppet pattern 기반 폼 식별
**상태**: FormTypeIdentifier Level 1~3 구현됨, Level 4 미구현
- OCR 텍스트 vs 패턴 매칭 (고정부 일치율), {*}는 아무 값 허용
- 동적부 값 추출 → 상태 감지, 변화 감지, Assert 자동생성

### Phase 9: 원격(Image-Only) 자동화 — "로컬 경험으로 원격 클릭"
**목표**: 스크린샷만으로 폼 식별 → 캐시된 상대좌표/전략으로 자동 클릭
**상태**: 설계 완료, 구현 없음
- OCR → FormTypeIdentifier → Experience DB → 상대좌표 × 해상도 → 클릭
- CLI: `wkappbot image-click <screenshot.png> <control-role>`

### 미래 과제
- HTML 리포트, DPI 스케일링, CI/CD headless 테스트

## Experience DB 실제 폴더 구조 (Phase 4~6 결과)

```
profiles/{profile}_exp/
├── form_{formId}.json              # FormExperience (controls, fingerprint, keywords, puppet_pattern, text_snapshots)
└── form_{formId}/                  # --detail 시 생성
    └── controls/
        └── cid_{N}/
            ├── latest.png          # 최신 컨트롤 스크린샷 (매 스캔 덮어쓰기)
            └── text_history.jsonl  # {"ts":...,"ocr":...,"wm":...} (append-only, dedup)
```

## HTS 영웅문 자동화 노하우

- **MFC 콤보**: AfxWnd110 + Edit 자식. CB_SETCURSEL 불가 → 클릭으로 드롭다운 조작
- **Owner-drawn 버튼**: GetWindowText="", X좌표 정렬 (Z-order 아님)
- **SmartClick 3티어**: BM_CLICK → WM_LBUTTONDOWN/UP (영웅문 최적) → SendInput
- **CheckDialogGone**: IsWindow + IsWindowVisible 이중 체크 (리페인트 중 false 방지)
- **`do` 커맨드**: `wkappbot do '캐치' '매매시작' --confirm` (콤보선택→클릭→다이얼로그)

## Important Notes
- Windows 전용 (.NET 8.0 `net8.0-windows10.0.22621.0`)
- 한국어 UI 지원 (UTF-8 출력, Unicode Win32 API)
- **dotnet.exe 경로**: `W:\SDK\dotnet\dotnet.exe`
