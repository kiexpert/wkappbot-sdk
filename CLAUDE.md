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
│       │   ├── Program.cs
│       │   └── TooltipProbe.cs        # tooltip 윈도우 진단 (EnumWindows + PrintWindow + OCR)
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
│       ├── WKAppBot.Vision/           # Vision 계층 (OCR + Claude API + 캐시 + 차트분석)
│       │   ├── ChartAnalyzer.cs         # 차트 스크린샷 → OHLC 추출 (3전략: body-first/column-scan/HTS-style)
│       │   ├── SimpleOcrAnalyzer.cs     # Windows.Media.Ocr (무료, 오프라인, 한글+영어)
│       │   ├── ChartAnalyzer.cs         # 차트 OHLC 추출 (3전략 + deltaX + PixelGrid)
│       │   ├── TooltipCalibrator.cs     # Phase B: 툴팁 호버 → Y축 재캘리브레이션
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
wkappbot do <window-title> [form-id] <button-text> [--confirm]
wkappbot dismiss <window-title> [keywords...]
wkappbot toolbar-ocr <window-title> [--click "text"] [--save]
wkappbot chart-analyze <window-title|image.png> [--form <id>] [--candles N] [-o output.json] [--debug] [--tooltip]
wkappbot tooltip-probe <process-name> [--capture]
wkappbot ocr <window-title|image.png> [--save] [-o output.txt]
```

### chart-analyze 명령 (차트 스크린샷 → OHLC 추출)
```bash
# 라이브 윈도우 캡처 + 분석
wkappbot chart-analyze "영웅문" --form 0600 --candles 600 --debug

# 이미지 파일 분석
wkappbot chart-analyze chart_screenshot.png -o result.json

# 툴팁 Y축 캘리브레이션 포함 (Phase B)
wkappbot chart-analyze "영웅문" --form 0600 --candles 600 --tooltip --debug
wkappbot chart-analyze "영웅문" --form 0600 --tooltip --tooltip-wait 500 --probes 8

# --candles N: 봉 개수 지정 (OCR에서 읽은 값). ultra-dense deltaX 배치에 사용
# --debug: 디버그 오버레이 이미지 저장 (검출 영역 시각화)
# --form <id>: HTS MDI 자식 폼 지정 (스크린샷 전 bring-to-front)
# --tooltip: 툴팁 호버로 Y축 재캘리브레이션 (마우스 점유 주의!)
# --tooltip-wait N: 호버 후 tooltip 대기 ms (default: 400)
# --probes N: 프로빙할 극단 캔들 수 (default: 6)
```

### tooltip-probe 명령 (tooltip 윈도우 진단)
```bash
# tooltip 윈도우 탐색 (클래스/크기/위치 덤프)
wkappbot tooltip-probe nkrunlite

# tooltip 캡처 + OCR (마우스를 캔들 위에 올린 상태에서 실행)
wkappbot tooltip-probe nkrunlite --capture
```

### ocr 명령 (이미지/윈도우 OCR 텍스트 추출)
```bash
# 이미지 파일 OCR
wkappbot ocr screenshot.png

# 윈도우 캡처 + OCR
wkappbot ocr "영웅문"

# 텍스트 파일로 저장
wkappbot ocr screenshot.png -o result.txt

# 스크린샷 저장
wkappbot ocr "영웅문" --save
```
- Windows.Media.Ocr (무료, 오프라인, 한글+영어)
- 전체 텍스트 + Y좌표별 라인 그룹핑 + 단어 좌표 출력
- 작은 이미지 자동 업스케일 (MFC 비트맵 폰트 인식률 향상)

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
- **[TOOLTIP]**: 툴팁 캘리브레이션 프로빙/결과 출력

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
- **자동 업스케일**: 작은 이미지(height<200 or width<400) 자동 2~4x 업스케일
  - height < 80 → 4x, height < 200 → 3x, else 2x (HighQualityBicubic)
  - OCR 후 좌표를 원본 스케일로 역변환
  - MFC 비트맵 폰트(11px) → 44px로 확대해도 ㄱ↔ㅋ 등 일부 오인식 존재

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
- **Phase 5 완료**: LCS puppet 패턴 — 라인 정렬 + 토큰 단위 diff + 연속 와일드카드 병합
- **Phase 6 완료**: 컨트롤 디테일 캐시 — per-control latest.png + text_history.jsonl (첫 만남 자동 캡처 + --detail로 갱신)
- **Phase 6.1 완료**: 스크린샷 캡처 수정 — CopyFromScreen→PrintWindow+CropRegion (Z-order 안전), parent container 캡처 추가
- **Phase 6.2 완료**: 트리 기반 폴더 구조 — Win32 hWnd 클래스명 트리를 실제 폴더 계층으로 미러링
- **Phase 7 완료**: 클릭 전략 DB 연동 — SmartClickButton이 ExperienceDb 성공률 기반 전략 순서 최적화
- **TouchControl 완료**: 모든 경로에서 컨트롤 만나면 자동 경험 축적 (스크린샷+텍스트, best-effort)
- **배포 구조 완료**: single-file EXE + wkappbot.hq/ (handlers, profiles, logs, output 통합 본부)
- **Dialog Handlers 완료**: YAML 기반 방해꾼 자동처리 + EnumWindows 감지 + 마우스 호버 인터랙티브 학습
- **dismiss 완료**: MDI 공지/팝업 자동 닫기 + 내용 읽기 + 중요도 분류 (긴급→닫지 않음)
- **OCR 자동 업스케일 완료**: 작은 이미지 2~4x 업스케일 → MFC 비트맵 폰트 인식률 향상
- **toolbar-ocr 완료**: MFC Pane 기반 툴바 OCR 스캔 + X-overlap 단어 그룹핑 + 텍스트 클릭
- **ChartAnalyzer v11**: 차트 스크린샷 → OHLC 추출 (3전략 + deltaX 기반 ultra-dense 배치)
- **Phase B 완료**: 툴팁 기반 Y축 재캘리브레이션 — 마우스 호버 → tooltips_class32 캡처 → OCR → OHLC 파싱 → Y축 교정
- **Volume 추출 완료**: volume 패널 바 높이 추출 + tooltip 거래량 기반 절대값 캘리브레이션
- **Columnar JSON 출력**: OHLCV별 1D 배열 (`result.arrays.json`) + 콘솔 미리보기
- **ocr 커맨드 완료**: `wkappbot ocr` — 이미지/윈도우 OCR 텍스트+좌표 추출 (adb-style 범용 도구)
- **캡처 검증 완료**: IsBlankBitmap(9-point 샘플링) + .fail.png(실제 화면 크롭) + .fail-iconic.png(최소화 태스크바 아이콘) + IsIconic 사전 체크
- **윈도우 스타일 특성 완료**: GWL_STYLE/GWL_EXSTYLE 수집 → ControlExperience 저장 + info.json per tree folder
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

### Phase 5: LCS puppet 패턴 — "토큰 단위 diff + 라인 정렬" ✅
**상태**: 완료 (commit `fda9fb6`)
- BuildPuppetPattern: LCS 기반 라인 정렬 (라인 추가/삭제 대응)
- 토큰 단위 LCS diff: 토큰 수 변경에도 안정적 (DiffTokensLcs)
- 연속 와일드카드 병합: `{*} {*} {*}` → `{*}` (MergeConsecutiveWildcards)
- TextSnapshots FIFO 5개, scan_count >= 2에서 자동 패턴 생성
- 검증: 영웅문 휴일 스캔 → 0 dynamic fields (정상: 시세 미변동)

### Phase 6: 컨트롤 디테일 캐시 — "스크린샷 + 텍스트 히스토리" ✅
**상태**: 완료 (commit `0f32ded`, 이후 자동 캡처 추가)
- **첫 만남 자동 캡처**: `--detail` 없이도 처음 보는 컨트롤은 자동으로 스크린샷 + 텍스트 기록
- **`--detail` 플래그**: 기존 스크린샷도 갱신 (항상 덮어쓰기)
- **`TouchControl()` 통합 메서드**: 한 줄 호출로 스크린샷(첫 만남) + 텍스트 히스토리(항상) 처리
  - SmartClickButton, scan 등 모든 경로에서 호출 → 앱봇이 거쳐간 모든 컨트롤에 경험 축적
- latest.png: 첫 만남 시 자동 생성, --detail 시 갱신
- text_history.jsonl: append-only, dedup (텍스트 변경 시에만 추가)
- ExperienceDb: TouchControl, HasScreenshot, SaveControlScreenshot, AppendTextHistory, GetControlDir
- 검증: 투혼 22개 + 영웅문 194개 스크린샷 자동 캐싱 확인

### Phase 6.1: 스크린샷 캡처 수정 — "PrintWindow+CropRegion (Z-order 안전)" ✅
**상태**: 완료
- **문제**: CopyFromScreen은 화면 좌표의 최상위 픽셀을 캡처 → 다른 창이 가리면 엉뚱한 이미지 (77% 오류율!)
- **해결**: PrintWindow로 폼 전체 캡처 → CropRegion으로 컨트롤 영역 크롭 (Z-order 면역)
- **ScreenCapture.CropRegion()**: 기존 Bitmap에서 영역 크롭 + 경계 클램핑
- **AppScanner**: CollectAllControls() → leaf + parent 두 리스트 반환, parent는 role="container"
- **ExperienceDb.TouchControl(formBitmap, formRect, controlRect)**: PrintWindow 비트맵에서 크롭
- **MFC 한계**: 일부 owner-drawn 컨트롤은 PrintWindow에 제대로 렌더링 안 됨 (알려진 한계)

### Phase 6.2: 트리 기반 폴더 구조 — "클래스명 경로가 실제 폴더가 된다" ✅
**상태**: 완료
- **목표**: Win32 hWnd 클래스명 트리를 실제 폴더 계층으로 미러링
- **동기**: 탐색기에서 바로 계층 파악 + 하위구조 예측 → 스마트 스킵
- **CID는 정적**: GetDlgCtrlID() = RC 리소스 파일 컴파일 타임 상수, 여러 인스턴스에서도 동일
- **HTS 윈도우 계층**: MainWindow → MDIClient(59648) → Form(AfxWnd110) → #32770(dialog) → Controls
- **MFC 클래스명 Sanitize**: `Afx:00BD0000:b:...` → `Afx_00BD0000` (모듈 주소만)
- **ControlExperience.TreePath**: JSON에 저장 → 재스캔 없이 폴더 경로 복원
- **하위호환**: TreePath null이면 레거시 `controls/cid_N/` 폴백
- **Before**: `form_{id}/controls/cid_N/latest.png` (플랫, 동일 CID 충돌 가능)
- **After**: `form_{id}/tree/#32770_59648/AfxWnd140_999/Button_3785/latest.png` (트리 미러)

### Phase 6.3: 캡처 검증 — "부실한 스크린샷은 미래의 클롣을 오염시킨다" ✅
**상태**: 완료
- **핵심 철학**: 잘못된 스크린샷이 ExperienceDB에 저장되면 미래 Claude 세션에 부실한 정보 제공
- **IsBlankBitmap()**: 3x3 그리드 9-point 샘플링 (기존 center pixel 단일 체크에서 강화)
- **blank 캡처 시 .fail.png**: PrintWindow 실패 → `CaptureScreenRegion`으로 데스크탑에서 해당 좌표 크롭 저장
  - 열어보면 "어떤 창이 가리고 있었는지" 또는 "화면 상태" 즉시 진단 가능
- **최소화 윈도우 감지**: `IsIconic(hWnd)` P/Invoke 추가
  - GetWindowRect가 태스크바 아이콘 좌표(~160x40) 반환 → 정상 캡처 불가
  - `.fail-iconic.png`로 태스크바 아이콘 영역 캡처 (귀여운 증거 + 원인 진단)
- **ExperienceDb.SaveFailScreenshot()**: 실패 진단 이미지 저장 (.fail.png)
- **AppScanner**: IsIconic → .fail-iconic.png / IsBlank → .fail.png / OK → 정상 OCR 학습
- **진단 파일 3종**: `latest.png`(정상), `.fail.png`(blank), `.fail-iconic.png`(최소화)

### Phase 6.4: 윈도우 스타일 특성 + info.json — "컨트롤의 DNA를 기록한다" ✅
**상태**: 완료
- **윈도우 스타일 수집**: GetWindowLongW(GWL_STYLE, GWL_EXSTYLE) → ControlExperience에 저장
  - WS_CAPTION(타이틀바), WS_THICKFRAME(리사이즈), WS_POPUP(팝업), WS_CHILD(자식)
  - WS_DISABLED(비활성), WS_VSCROLL/HSCROLL(스크롤바)
  - WS_EX_TOPMOST(항상위), WS_EX_TOOLWINDOW(툴), WS_EX_TRANSPARENT/LAYERED(투명/오버레이)
- **Derived boolean traits**: Style raw bits에서 자동 계산 (HasCaption, IsResizable 등, JSON 미직렬화)
- **info.json per tree folder**: 스크린샷 저장 시 옆에 info.json 자동 생성
  - 탐색기에서 `latest.png` + `info.json` 나란히 → 스크린샷+메타데이터 한눈에 확인
- **WindowInfo에 Style/ExStyle 추가**: FromHwnd()에서 한 번에 수집
- **자동화 활용**: resizable 폼 → 상대좌표 필수 / popup+modal → 방해꾼 후보 / disabled → 클릭 스킵

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

### dismiss + toolbar-ocr — "공지 닫기 + MFC 툴바 OCR 클릭" ✅
**상태**: 완료
- `wkappbot dismiss <title> [keywords...]` — MDI 공지창 + #32770 팝업 키워드 매칭 → WM_CLOSE
  - 기본 키워드: 공지, 인사, 안내, 알림, POP-UP
  - 공지 내용 OCR 읽기 + 중요도 분류 (긴급/장애/거래중지 → 닫지 않고 경고)
  - DoCommand에서 자동 실행 (폼 검색 전 공지 정리)
- `wkappbot toolbar-ocr <title> [--click "text"] [--save]` — MFC 툴바 버튼 OCR 스캔
  - MFC 툴바는 UIA Pane으로 노출 (Button 요소 없음) → Pane 전체 스크린샷 + OCR
  - X-overlap 기반 단어 그룹핑: 2줄 레이아웃(상단+하단) 자동 병합 (예: "캐치"+"실전" → "캐치실전")
  - `--click "캐치실전"`: OCR 텍스트 매칭 → 해당 영역 중심 좌표 클릭
  - `--save`: 툴바 스크린샷 저장 (output/toolbar/)
- OCR 자동 업스케일: height<80→4x, <200→3x, else 2x (HighQualityBicubic)

### ChartAnalyzer Phase B: 툴팁 Y축 캘리브레이션 — "서버가 알려준 정확한 가격" ✅
**상태**: 완료
- `TooltipCalibrator.cs` — 극단 캔들 마우스 호버 → tooltips_class32 캡처 → OCR → OHLC 파싱 → Y축 재구성
- `TooltipProbe.cs` — 진단 도구: EnumWindows로 tooltip 윈도우 탐색 + PrintWindow/ScreenCapture + OCR
- `ChartAnalyzer.cs` — CandleData에 PixelHighY/LowY/OpenY/CloseY 추가, Interpolate/ParsePriceText internal화
- `NativeMethods.cs` — WM_MOUSEMOVE + MakeLParam 추가
- `Program.cs` — `tooltip-probe` 커맨드, `chart-analyze --tooltip [--tooltip-wait N] [--probes N]`
- **tooltip 활성화**: SmartSetForegroundWindow + MouseInput.MoveTo (클릭하면 차트 상태 변함!)
- **호버 위치**: PixelHighY (wick top) — body center는 ultra-dense에서 miss
- **OCR 파싱**: FullText에 줄바꿈 없음 → 종목코드 `(\d{6})` 앵커 후 첫 4개 콤마숫자 = OHLC
- **가격 캘리브레이션**: (PixelHighY→High) + (PixelLowY→Low) 쌍 수집 → 단조성 검증 → 기존 OCR axis 머지
- **거래량 캘리브레이션**: tooltip 5번째 숫자 = 거래량, barHeight × median(volume/barHeight) = 절대값
- **OHLC sanity check**: H>=L, H>=O, H>=C, L<=O, L<=C 검증 (OCR 오인식 방지)
- **MISS retry**: wick top 호버 실패 시 body center로 재시도
- **3-zone 캔들 선택**: high/mid/low 가격대에서 고르게 프로빙 (2-zone에서 개선)
- **Columnar JSON 출력**: `ToColumnarJson()` — OHLCV별 1D 배열 + count/datetime/bullish
- `[TOOLTIP]` 태그: 프로빙 진행/결과 출력

### Phase 8: puppet 패턴 매칭 — "이미지만으로 폼 식별"
**목표**: FormTypeIdentifier Level 4로 puppet pattern 기반 폼 식별
**상태**: FormTypeIdentifier Level 1~3 구현됨, Level 4 미구현
- OCR 텍스트 vs 패턴 매칭 (고정부 일치율), {*}는 아무 값 허용
- 동적부 값 추출 → 상태 감지, 변화 감지, Assert 자동생성

### Phase 9: 원격(Image-Only) 자동화 — "로컬 경험으로 원격 클릭"
**목표**: 스크린샷만으로 폼 식별 → 캐시된 상대좌표/전략으로 자동 클릭
**상태**: 설계 완료, 구현 없음
- OCR → FormTypeIdentifier → Experience DB → 상대좌표 × 해상도 → 클릭
- CLI: `wkappbot image-click <screenshot.png> <control-role>`

### 앱봇의 눈 — 장기 비전 (Vision Roadmap)

**핵심 철학**: "앱봇은 쓸수록 똑똑해진다"
- 모든 경로(scan, click, do, dismiss, chart-analyze)에서 컨트롤을 만나면 자동 경험 축적
- 경험 DB가 쌓일수록: 빠른 인식 → 최적 전략 선택 → 비관심 컨트롤 스킵 → 실행 속도 향상

**Level 1 — 맹인 (Blind)** ✅ 완료
- UIA Accessibility API만으로 요소 탐색
- AutomationId/Name 정확 매칭, 실패하면 못 찾음

**Level 2 — 근시 (Nearsighted)** ✅ 완료
- Simple OCR (Windows.Media.Ocr) — 무료, 오프라인, 한글+영어
- 화면에 보이는 텍스트로 요소 위치 추정
- MFC 비트맵 폰트도 업스케일로 인식

**Level 3 — 정상시력 (Normal Vision)** ✅ 완료
- Vision Cache DB — 한 번 찾은 요소는 상대좌표로 캐시
- Claude Vision API — OCR 실패 시 스크린샷으로 시각적 탐색 (고비용 폴백)
- 5티어 체인: UIA → VisionCache → OCR → Claude API → 좌표

**Level 4 — 경험의 눈 (Experienced Eye)** 🔜 다음
- puppet 패턴으로 폼 자동 식별 (텍스트 패턴 매칭)
- 컨트롤별 스크린샷 DB로 시각적 유사도 비교
- 경험 기반 비관심 컨트롤 스킵 (static label, 빈 영역 등)
- 폼 상태 자동 감지 (로그인 전/후, 장중/장후 등)

**Level 5 — 투시 (X-Ray Vision)** 🔮 미래
- Image-Only 자동화: 스크린샷만으로 폼+컨트롤 식별 → 경험 DB의 좌표로 클릭
- 원격 데스크톱/VNC에서도 동작 (UIA 없이 순수 이미지 기반)
- 다중 HTS 지원: 영웅문 경험으로 다른 HTS의 유사 화면 자동 매핑
- 이상 탐지: 평소와 다른 UI 변화 자동 감지 → 알림

**Level 6 — 예지 (Precognition)** 🔮 꿈
- 사용자 행동 패턴 학습 → 다음 액션 예측
- 시세 패턴 + 차트 분석 → 자동 알림/신호 생성
- 자가 시나리오 생성: 관찰한 사용자 조작을 YAML 시나리오로 자동 기록
- 자가 치유: 앱 업데이트로 UI 변경 시 → 경험 DB 기반 자동 적응

### 미래 과제 (기술)
- HTML 리포트, DPI 스케일링, CI/CD headless 테스트
- multi-monitor 지원, 고DPI 스케일 팩터 자동 감지
- YAML 시나리오 녹화 모드 (record → replay)
- 앱 프로파일 공유 (export/import)

## 배포 구조 (Single-file EXE + HQ)

```
W:/SDK/bin/                          # PATH에 등록된 유틸 폴더
├── wkappbot.exe                     # 55MB single-file EXE (모든 DLL 내장)
└── wkappbot.hq/                     # 본부 (HeadQuarters) — 어디서 실행해도 동일 위치
    ├── handlers/                    # 방해꾼 핸들러 YAML (빌드 시 자동 복사)
    ├── profiles/                    # Experience DB (앱 프로파일 + 컨트롤 경험)
    │   ├── {profile}.json           # AppProfile (프로세스/클래스 매칭)
    │   └── {profile}_exp/           # 경험 데이터
    │       ├── form_{id}.json       # FormExperience (controls, fingerprint, puppet_pattern)
    │       ├── form_{id}/controls/  # (레거시) 플랫 구조
    │       │   └── cid_{N}/
    │       │       ├── latest.png
    │       │       └── text_history.jsonl
    │       └── form_{id}/tree/      # (신규) 트리 구조 — Win32 hWnd 계층 미러
    │           └── #32770_59648/
    │               └── AfxWnd140_999/
    │                   ├── latest.png           # 컨테이너 스크린샷
    │                   └── Button_3785/
    │                       ├── latest.png       # 컨트롤 스크린샷
    │                       └── text_history.jsonl
    ├── logs/                        # 실행 로그
    └── output/                      # 스크린샷, 차트 분석 결과, toolbar 캡처
```

- `dotnet publish` → csproj PostPublish가 EXE + handlers 자동 복사
- `DOTNET_ROOT='W:/SDK/dotnet' wkappbot <command>` — 어디서든 실행 가능

## HTS 영웅문 자동화 노하우

- **MFC 콤보**: AfxWnd110 + Edit 자식. CB_SETCURSEL 불가 → 클릭으로 드롭다운 조작
- **Owner-drawn 버튼**: GetWindowText="", X좌표 정렬 (Z-order 아님)
- **SmartClick 3티어**: BM_CLICK → WM_LBUTTONDOWN/UP (영웅문 최적) → SendInput
- **CheckDialogGone**: IsWindow + IsWindowVisible 이중 체크 (리페인트 중 false 방지)
- **`do` 커맨드**: `wkappbot do '캐치' '매매시작' --confirm` (콤보선택→클릭→다이얼로그)
- **MFC 툴바**: UIA에서 `ControlType.Pane`으로 노출 (Button 아님!). 개별 버튼이 UIA 요소가 아닌 커스텀 드로잉
  - 전략: Pane 전체 스크린샷 → OCR → X-overlap 기반 2줄 라벨 그룹핑 → 텍스트 매칭 클릭
  - `toolbar-ocr` 커맨드: 메뉴툴바/화면툴바/쾌속주문툴바 자동 인식
- **버튼 텍스트 3티어 폴백**: GetWindowText → WM_GETTEXT → OCR (단일 버튼이면 텍스트 불일치해도 클릭)
- **공지/팝업 자동 닫기**: `dismiss` 커맨드 — MDI 자식 키워드 매칭 + 내용 OCR 읽기 + 중요도 분류

## ChartAnalyzer — 차트 스크린샷 OHLC 추출 ("눈의 진화")

### 개요
HTS 차트 스크린샷에서 캔들스틱 OHLC + Volume 데이터를 추출하는 오프라인 픽셀 분석기.
OpenAPI 없이 차트 이미지만으로 시세 데이터를 복원한다.

### 핵심 파일
- `WKAppBot.Vision/ChartAnalyzer.cs` — 메인 분석 파이프라인 + 3전략 + PixelGrid + Volume 추출
- `WKAppBot.Vision/TooltipCalibrator.cs` — 툴팁 호버 → Y축/Volume 캘리브레이션
- `WKAppBot.CLI/Program.cs` — `chart-analyze` CLI 커맨드 핸들러

### 3전략 캔들 탐지 (Strategy A/B/C)

| 전략 | 이름 | 적용 대상 | 핵심 방법 |
|------|------|----------|----------|
| A | body-first | 일반 차트 (봉 폭 ≥3px) | 수평 런 → 사각형 바디 클러스터링 → 위크 추적 |
| B | column-scan | 얇은 차트 (봉 폭 1-2px) | 컬럼별 색상 카운트 → 세그먼트 그룹핑 |
| C | hts_style | HTS 전용 (영웅문 GDI) | ClassifyHts() 4요소 인식 + 인디케이터 마스크 |

- Winner = 가장 많은 캔들을 찾은 전략 (sanity check 통과 시)
- Strategy C는 A/B가 인식 못하는 HTS 고유 색상 체계를 처리

### HTS 캔들 4요소 (ClassifyHts)

| 요소 | 양봉 (Bullish) | 음봉 (Bearish) | 역할 |
|------|---------------|---------------|------|
| Fill | cream (246,234,214) | pale blue (214,226,239) | 몸통 채움 (밝음) |
| Border | brown (134,104,53) | teal (57,154,156) | 몸통 테두리 (어두움) |
| Wick | red (255,0,0) | blue (17,91,203) | 꼬리 (선명함, 1px) |
| Outline | near-black (57,57,52) | near-black (57,57,52) | 공용 외곽선 |

**주의**: Fill/Border는 IsReddish()/IsBluish() 체크를 통과하지 않음 → Strategy A/B로는 탐지 불가!

### Ultra-Dense 모드 (600일 차트, ~1.04px/봉)

**핵심 원리**:
- "캔들은 중간에 사라지지 않고 각각이 공평한 거리를 유지하며 반드시 있다"
- "캔들의 폭은 소숫점일 수 있다 — 소수점 캔들폭이 안티앨리어스 처리되어 소프트랜더링"
- "전체봉수는 OCR로 알고 있으니 봉갯수가 이렇게 되도록 공평한 delta로 계산"

**DeltaX 기반 캔들 배치** (expectedCandleCount > 0):
```
deltaX = pixelSpan / expectedCandleCount   (소수점!)
candle[i] = columns[floor(i*dX) .. floor((i+1)*dX))
```
- 정확히 N개 캔들 (실제 거래일 수와 일치)
- 일부 캔들 1px, 일부 2px (HTS float→int 렌더링 반영)

**인디케이터 마스크** (MA 선 제거):
- MA 선은 위크 색상(red/blue)으로 그려짐 → 가짜 위크 발생
- 수평 런 분석: ≥8px 런 = MA 선 → 마스크 처리
- 1-7px 런 = 실제 캔들 위크 → 보존
- (분포: len 1-2 = 순수위크, len 3-7 = 동일가격 캔들 클러스터, len 8-9 = MA)

### 눈의 진화 (Eye Evolution) 버전 히스토리

| 버전 | 봉 수 | 전략 | 핵심 변경 |
|------|------|------|----------|
| v1-v3 | 35 | body_first | 기본 body-first 탐지 (form 0606) |
| v4 | — | column_scan | 컬럼 스캔 폴백 추가 |
| v5 | — | hts_style | HTS 3요소 인식 추가 |
| v6 | 524 | hts_style | Y-frequency 인디케이터 필터 + ultra-dense |
| v7 | 554 | hts_style | 수평 런 인디케이터 마스크 (Y-frequency 대체) |
| v8 | 546 | hts_style | 임계값 3→8 (동일가격 위크 클러스터 보존) |
| v9 | 626 | hts_style | 후처리 필터 스킵 + htsScanBottom 확장 (0 gap!) |
| v10 | 626 | hts_style | border-as-body O/C 추정 |
| v11 | 600 | hts_style | deltaX 기반 N봉 배치 (OCR 봉 수 활용) |
| +B | 600 | hts_style + tooltip | 툴팁 Y축 재캘리브레이션 (극단 캔들 호버 → 정확한 OHLC) |
| +V | 600 | hts_style + tooltip + volume | Volume 바 높이 추출 + tooltip 거래량 캘리브레이션 |

### 알려진 한계 / 미해결

- **Y축 캘리브레이션**: OCR만으로는 5000 아래 가격 라벨 인식 어려움 → `--tooltip` 플래그로 해결 (Phase B)
- **Phase B 완료**: 마우스 호버 → 툴팁 OCR → Y축 재캘리브레이션 (TooltipCalibrator)
- **Volume 추출 완료**: 볼륨 패널 바 높이 + tooltip 거래량으로 절대값 캘리브레이션
- **volume 패널 tooltip**: 캔들이 volume 패널 경계에 있으면 거래량 tooltip이 뜸 (maxY 클램핑으로 대부분 회피)

## Important Notes
- Windows 전용 (.NET 8.0 `net8.0-windows10.0.22621.0`)
- 한국어 UI 지원 (UTF-8 출력, Unicode Win32 API)
- **dotnet.exe 경로**: `W:\SDK\dotnet\dotnet.exe`
