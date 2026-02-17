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
│       │   │   └── RuntimeContext.cs    # 런타임 상태 (변수, ActionPoint, StepResult, FocusConfig)
│       │   └── Scenario/
│       │       ├── Models.cs            # YAML 데이터 모델 (ScenarioConfig에 focus 설정 포함)
│       │       └── ScenarioParser.cs    # YAML 파싱/검증
│       ├── WKAppBot.Win32/            # Win32 네이티브 계층
│       │   ├── Native/
│       │   │   └── NativeMethods.cs     # P/Invoke 선언 (user32, gdi32, shcore) + SmartSetForegroundWindow
│       │   ├── Window/
│       │   │   ├── WindowFinder.cs      # 윈도우 탐색/포커스/계층경로 + SmartBringToFront
│       │   │   ├── HtsInterop.cs        # HTS MDI 스트레스 테스트 (3패턴: repeat/memory/ctx-only)
│       │   │   └── ControlMap.cs        # 컨트롤 매핑
│       │   ├── Accessibility/
│       │   │   └── UiaLocator.cs        # FlaUI UIA 요소 탐색/조작/트리덤프 + PatternMatcher
│       │   └── Input/
│       │       ├── MouseInput.cs        # SendInput 마우스 (절대좌표)
│       │       ├── KeyboardInput.cs     # SendInput 키보드 + VK 매핑
│       │       └── ScreenCapture.cs     # 윈도우 스크린샷 캡처 (focusless — PrintWindow/BitBlt)
│       ├── WKAppBot.Vision/           # Vision 계층 (OCR + Claude API + 캐시)
│       │   ├── SimpleOcrAnalyzer.cs     # Windows.Media.Ocr (무료, 오프라인, 한글+영어)
│       │   ├── VisionAnalyzer.cs        # Claude Vision API 호출 (고비용 폴백)
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
```

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
- **설계 완료 (모델+주석)**: Experience DB 포펫 패턴, 컨트롤별 디테일 캐시, 클릭 전략 DB
- **미구현**: 아래 로드맵 참조

## 구현 로드맵 (Implementation Roadmap)

### Phase 4: Experience DB 실전 연동 — "풀스캔 → DB 저장"
**목표**: `wkappbot scan` 명령으로 앱 풀스캔 → Experience DB에 저장
**상태**: 모델 완료, 실행 로직 미연결
**작업**:
1. `scan` CLI 커맨드 추가
   - `wkappbot scan --title '영웅문' [--ocr] [--save]`
   - AppScanner.Scan() 실행 → FormatSummary() 출력
   - `--ocr`: LearnFormsWithOcr() 실행 (OCR 학습)
   - `--save`: ProfileStore.Save() + ExperienceDb.SaveAll()
2. ExperienceDb 폴더 구조 실제 생성
   - `data/experience/{profile}/form_{formId}/experience.json`
   - 현재 ExperienceDb는 flat JSON 파일 — 폴더 구조로 마이그레이션
3. scan 시 각 컨트롤 WM_GETTEXT 수집 → ControlExperience에 저장
4. 테스트: 영웅문 풀스캔 → JSON 저장 확인

### Phase 5: 포펫 패턴 생성 — "누적 diff → 패턴 안정화"
**목표**: 여러 시점 스캔 텍스트를 diff하여 자동으로 포펫 패턴 생성
**상태**: FormExperience에 PuppetPattern/TextSnapshots 필드 추가 완료
**작업**:
1. PuppetPatternBuilder 클래스 구현 (AppScanner.cs 또는 별도 파일)
   - `BuildPattern(List<List<string>> snapshots) → string pattern`
   - 라인 매칭: Y좌표 대역 기반 (OCR 라인 위치 활용)
   - 토큰 diff: 같은 라인의 단어 단위 비교 → 변한 토큰 = `{*}`
   - 숫자/시간/종목코드 휴리스틱: 첫 스캔에서도 동적 후보로 마킹
2. scan 실행 시 TextSnapshots에 append (FIFO 최대 5개)
3. scan_count >= 2 이면 자동 PuppetPattern 생성
4. `puppet_pattern.txt` 파일로도 저장 (사람이 읽기 좋게)
5. 테스트: 캐치 시작안내 다이얼로그 2회 스캔 → 패턴 자동 생성 확인

### Phase 6: 컨트롤 디테일 캐시 — "스크린샷 + 텍스트 히스토리"
**목표**: 컨트롤별 서브폴더에 스크린샷/텍스트 이력 누적
**상태**: 폴더 구조 설계 완료, 구현 없음
**작업**:
1. ControlDetailCache 클래스 구현
   - `SaveControlSnapshot(formId, cid, Bitmap screenshot, string text)`
   - `controls/cid_{N}/info.json` 생성/업데이트
   - `controls/cid_{N}/latest.png` 덮어쓰기
   - `controls/cid_{N}/text_history.jsonl` append
2. 텍스트 변화 감지 시에만 스크린샷 저장 (snapshots/ 폴더)
3. scan --detail 플래그: 개별 컨트롤 BitBlt 캡처 + 저장
4. 테스트: 캐치 폼 스캔 → controls/ 폴더 생성 확인

### Phase 7: 클릭 전략 DB 연동 — "SmartClick이 경험에서 배운다"
**목표**: SmartClickButton이 Experience DB의 ClickStrategies를 읽어 최적 순서로 시도
**상태**: ClickStrategyStats 모델 완료, SmartClickButton 미연동
**작업**:
1. SmartClickButton에 ExperienceDb 파라미터 추가
   - DB에서 해당 컨트롤(cid)의 ClickStrategies 조회
   - 성공률 높은 전략부터 시도 (기본 순서: bm_click → wm_lbutton → send_input)
2. 각 전략 시도 후 성공/실패를 DB에 기록
   - RecordClickStrategy(formId, cid, strategyName, success: bool)
3. DB 저장 타이밍: 프로세스 종료 시 or 일정 간격 flush
4. 테스트: 캐치 확인 버튼 3회 클릭 → DB에 wm_lbutton success 누적 확인

### Phase 8: 포펫 패턴 매칭 — "이미지만으로 폼 식별"
**목표**: FormTypeIdentifier에 Level 4 (포펫 패턴 매칭) 추가
**상태**: FormTypeIdentifier에 Level 1~3 구현됨, Level 4 미구현
**작업**:
1. FormTypeIdentifier.MatchByPuppetPattern() 구현
   - OCR 텍스트 라인 vs 패턴 라인 매칭 (고정부 일치율 계산)
   - `{*}` 위치는 아무 값이나 허용
   - confidence = 0.85 × matchRatio
2. Identify()에 Level 4 추가 (Level 3 이후)
3. 매칭 시 동적부 값도 추출 → Dictionary<string, string> extractedValues
4. 테스트: 캐치 시작안내 스크린샷 OCR → 포펫 패턴 매칭 → form_id 식별 확인

### Phase 9: 원격(Image-Only) 자동화 — "로컬 경험으로 원격 클릭"
**목표**: 스크린샷만으로 폼 식별 → 캐시된 좌표/전략으로 자동 클릭
**상태**: 설계 완료 (CLAUDE.md 원격 모드 흐름도), 구현 없음
**작업**:
1. ImageOnlyExecutor 클래스 구현
   - 입력: 스크린샷 이미지 (Bitmap or file path)
   - OCR → FormTypeIdentifier → Experience DB 로드
   - 상대좌표 × 이미지 해상도 → 절대 클릭 좌표
   - 클릭 전략 DB 기반 SmartClick 실행
2. CLI: `wkappbot image-click <screenshot.png> <control-role>`
3. RDP/VNC 연동은 별도 Phase (스크린샷 자동 캡처 + 루프)

### 미래 과제 (우선순위 낮음)
- HTML 리포트 생성
- DPI 스케일링 처리
- 여러 앱 프로파일 동시 관리
- CI/CD 연동 (headless 테스트)

## HTS 영웅문 자동화 노하우 (캐치 실전매매 [4051])

### MFC 커스텀 컨트롤 특성
- **콤보박스**: 표준 ComboBox가 아닌 `AfxWnd110` + `Edit` 자식 구조
  - CB_SETCURSEL/CB_GETCOUNT 사용 불가
  - 클릭 방법: Edit 자식 중앙 클릭 → 드롭다운 펼침 → Edit.Bottom + 14px 에서 첫 항목 마우스 클릭
  - 콤보 식별: AfxWnd 안의 enabled Edit 자식이 있고, 대상 버튼 위에 위치, 계좌번호(dash패턴)/종목코드(cid=32760) 제외
- **Owner-drawn 버튼**: 텍스트 없음 (GetWindowText = ""), 버튼 순서는 Z-order가 아닌 X좌표 정렬
  - BM_CLICK: 일부 동작하나 불안정 (영웅문 owner-drawn에서 안 먹히는 경우 많음)
  - WM_LBUTTONDOWN/UP: 대부분 동작 (SendMessage 사용, 커서 이동 없음)
  - SendInput 물리 마우스: 항상 동작하나 커서 이동됨
- **다이얼로그**: `#32770` 클래스, 모달, owner-drawn 버튼 포함

### SmartClickButton 3티어 전략 + 창변화 감지
```
Strategy 1: BM_CLICK (PostMessage, 커서 이동 없음)
Strategy 2: WM_LBUTTONDOWN/UP (SendMessage, 커서 이동 없음) ← 영웅문에서 가장 효과적
Strategy 3: SendInput 물리 마우스 (커서 이동됨, 최후 수단)
```
각 전략 후 `CheckDialogGone()`:
- `IsWindow()` + `IsWindowVisible()` 이중 체크 (transient 상태 재확인)
- 포그라운드 윈도우 변화 감지 (새 모달 팝업)
- 참고: `IsWindowVisible`만으로는 불안정 — 리페인트 중 false 반환 가능

### CLI 커맨드 추가
```
wkappbot do <form-title> <button-text> [--confirm] [--step-delay N]
  전체 시퀀스: MFC 콤보 선택 → 버튼 클릭 → 다이얼로그 자동확인

wkappbot form-dump <form-title>
  Win32 전체 자식 트리 덤프 (WM_GETTEXT 포함)

wkappbot dialog-click <dialog-title> [button-index]
  다이얼로그 버튼 SmartClick (owner-drawn 지원)
```

### 실행 예시 (캐치 실전매매 시작)
```bash
DOTNET_ROOT='W:/SDK/dotnet' wkappbot.exe do '캐치' '매매시작' --confirm
```

### 컨트롤 DB + 포펫 패턴 전략

#### 폴더 = DB 구조
```
data/experience/{profile_name}/
├── profile.json                    # AppProfile (zones, form_types)
├── form_{formId}/
│   ├── experience.json             # FormExperience (controls, fingerprint, ocr_keywords)
│   ├── puppet_pattern.txt          # 포펫 패턴 (고정텍스트 + {*} 와일드카드)
│   ├── snapshot_latest.png         # 최신 스크린샷
│   └── snapshots/                  # 시점별 스크린샷 히스토리
│       ├── 20260218_013000.png
│       └── 20260218_140000.png
└── click_strategy.json             # 클래스별 클릭 전략 효과 DB
```

#### 포펫 패턴 (Puppet Pattern) — 폼 텍스트 지문
**개념**: 폼 전체의 OCR 텍스트를 레이아웃 보존한 채 동적 부분만 `{*}`로 치환한 패턴
- 시점에 따라 변하는 값 (가격, 수량, 시간, 종목코드) → `{*}`
- 항상 동일한 레이블/제목 → 고정 텍스트로 유지
- 여러 시점의 텍스트를 누적 비교하여 자동으로 고정/동적 판별

**예시: 캐치 시작안내 다이얼로그**
```
조건식: {*}
매매유형: 자동매수/자동매도
트레일링 ({*})
종목당 투자금액 {*} 로스컷 {*}
당일청산 {*}
계좌증거금률: {*}
추정예수금: {*}원
```

**생성 알고리즘**:
1. 첫 스캔: OCR 전체 텍스트를 라인별로 저장 (baseline)
2. 이후 스캔마다: 같은 라인 위치의 텍스트 비교
3. 변한 토큰 → `{*}`로 치환, 안 변한 토큰 → 고정 텍스트 확정
4. scan_count >= 3 이면 패턴 안정화 (confidence 높음)
5. 저장: `puppet_pattern.txt` (사람이 읽기 좋은 포맷)

**활용**:
- **폼 식별**: FormTypeIdentifier Level 4로 추가 (패턴 매칭)
- **상태 감지**: 고정 텍스트 일치 + 동적 부분 값 추출 → 폼의 현재 상태 파악
- **변화 감지**: 패턴의 동적 부분 값이 바뀌면 → 상태 전환 이벤트
- **Assert 자동생성**: 포펫 패턴에서 기대값 자동 추출

#### 클릭 전략 DB
클래스명별로 어떤 전략이 효과적인지 누적 학습:
```json
{
  "Button": { "bm_click": {"success": 15, "fail": 0}, "wm_lbutton": {"success": 3, "fail": 0} },
  "owner_drawn_button": { "bm_click": {"success": 0, "fail": 5}, "wm_lbutton": {"success": 8, "fail": 0} },
  "AfxWnd110_Edit": { "send_input_mouse": {"success": 12, "fail": 1} }
}
```
SmartClickButton이 전략 순서를 DB 기반으로 자동 최적화

#### 원격(Image-Only) 모드 — Win32 API 없이 자동화
로컬에서 학습한 Experience DB를 원격 RDP/VNC 환경에서 재활용하는 흐름:
```
[원격 화면 이미지]
    ↓ OCR (Windows.Media.Ocr or Claude Vision)
    ↓
[추출된 텍스트 라인들]
    ↓ FormTypeIdentifier
    ↓ Level 3: OCR 키워드 매칭
    ↓ Level 4: 포펫 패턴 매칭 (고정 레이블 라인 순서 비교)
    ↓
[폼 식별] → "form_4051 캐치 실전매매 시작안내" (conf 0.9)
    ↓
[Experience DB 로드] → form_4051/experience.json
    ↓ 캐시된 컨트롤 맵:
    ↓   확인 버튼 = relativeX:0.52, relativeY:0.85, class:owner_drawn
    ↓   취소 버튼 = relativeX:0.72, relativeY:0.85, class:owner_drawn
    ↓
[좌표 산출] → 이미지 해상도 × 상대좌표 = 절대 클릭 좌표
    ↓
[클릭 전략 선택] → click_strategy DB: owner_drawn → WM_LBUTTON 우선
    ↓
[실행] → 클릭 + CheckDialogGone 검증
```
핵심: 로컬에서 한 번 학습하면, 같은 앱의 원격 화면에서도 OCR만으로 자동화 가능

#### 트리 탐색 최적화 — "DB 히트 시 이하 스킵"
Win32 트리를 탐색할 때, DB에 이미 학습된 컨트롤을 만나면 그 아래는 탐색하지 않음:
```
첫 방문 (DB 없음): 전체 트리 재귀 탐색 + OCR + 스냅샷 → DB 저장
재방문 (DB 있음): 클래스+cid가 DB 매칭 → 이하 서브트리 스킵 → 캐시된 정보 즉시 사용
```
- ExperienceDb.IsKnownControl(formId, cid) → true면 **구조 탐색**만 스킵
- 캐시된 상대좌표/클릭전략/OCR텍스트를 바로 활용
- 트리가 깊은 MFC 앱에서 탐색 시간 대폭 단축
- 앱 업데이트로 구조 변경 시: DB 미스 → 다시 풀스캔 → 자동 재학습

**주의: 텍스트 수집은 스킵하면 안 됨!**
- 구조 스킵 ≠ 텍스트 스킵. DB 히트된 컨트롤이라도 WM_GETTEXT는 매번 수집
- 포펫 패턴 diff를 위해 텍스트 변화 추적이 필수
- 수집 비용은 낮음 (WM_GETTEXT는 마이크로초 단위)

#### 컨트롤별 디테일 캐시 (서브폴더)
각 컨트롤의 스크린샷 + 텍스트 히스토리를 폴더에 누적 저장:
```
data/experience/{profile}/form_{formId}/
├── experience.json                 # 전체 컨트롤 맵
├── puppet_pattern.txt              # 포펫 패턴
├── controls/                       # 컨트롤별 디테일 캐시
│   ├── cid_{N}/
│   │   ├── info.json               # class, role, 상대좌표, 클릭전략 통계
│   │   ├── latest.png              # 최신 컨트롤 스크린샷 (BitBlt)
│   │   ├── text_history.jsonl      # 텍스트 변화 이력 (timestamp + value)
│   │   └── snapshots/              # 시점별 스크린샷 (변화 감지 시에만 저장)
│   │       ├── 20260218_093000.png
│   │       └── 20260218_140000.png
│   └── cid_{M}/
│       └── ...
└── snapshots/                      # 폼 전체 스크린샷 히스토리
```
- text_history.jsonl: `{"ts":"2026-02-18T09:30:00","v":"5,788,470원"}` (줄 단위 append)
- 스크린샷: 전체 폼은 매 스캔, 개별 컨트롤은 텍스트 변화 감지 시에만 캡처 (디스크 절약)
- Claude 연구용: 나중에 스크린샷 + text_history 분석으로 UI 패턴 학습 가능

## Important Notes
- Windows 전용 (.NET 8.0 `net8.0-windows10.0.22621.0`)
- 한국어 UI 지원 (UTF-8 출력, Unicode Win32 API)
- UWP 앱 (ApplicationFrameHost) 지원: 타이틀 검색 전략 포함
- 계산기 PoC 시나리오가 기본 검증 테스트
- **dotnet.exe 경로**: `W:\SDK\dotnet\dotnet.exe`
