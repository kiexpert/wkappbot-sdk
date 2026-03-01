# WKAppBot - Windows App Automation Test Framework

## 동료 클롣을 위한 운영 규칙 (필독!)

### 응답 언어
- **유저에게 말할 때는 항상 한국어** (코드/영어 문서 작성 시는 영어 OK)
- 인수인계(Handoff) 섹션 등 내부 문서는 영어로 작성 (토큰 절약)

### 질문/소통
- **유저에게 질문할 때**: 반드시 **Slack과 Claude Code 여기에 동시에** 전달할 것
  - `wkappbot slack send "질문 내용"` + 여기(프롬프트)에도 출력
- **작업 중 상황 공유**: 슬랙에도 현황을 알려줘야 유저가 모바일에서도 확인 가능

### 앱봇의 눈 (AppBotEye)
- **항상 떠있어야 함**: 모든 CLI 명령 실행 시 AppBotEye 자동 spawn
- `eye`, `slack`, `help`, `validate`, `win-move` 제외하고 자동 실행
- AppBotEye = Slack 데몬 통합 → 별도 `slack listen` 불필요
- **크로 카드 터치 금지!**: 크로(OpenClaw)는 별도 서비스 → BuildKroStatus3 등 크로 카드 로직 수정 금지
- **클롣 카드만 개선 가능**: 클롣(wkappbot CLI)의 카드 표시/포맷은 자유롭게 개선 OK
- **CWD 약식 표시**: `W:\GitHub\WKAppBot` → `WG-WKAppBot`

### EyeTick 운영 메모
- `wkappbot eye tick`은 반드시 **one-shot 진단 명령** (글로벌 루프 진입 금지)
- Eye 루프: FSW 하이브리드 (이벤트 기반 dirty-check + 1초 full-load 안전망)
- 최근 생각 source: OpenClaw sessions jsonl (assistant 우선, user fallback)
- 노이즈 필터: `NO_REPLY`, `send ㄱㄱ`, `telegram send ㄱㄱ`, `ㄱㄱ`
- 계획 출력: `[KRO_PLAN_BEGIN]` ~ `[KRO_PLAN_END]` 블록
- **EyeTick 슬랙 메시지 전달 (MUST!)**: conversations.history API → 새 메시지 → FindPrompt → TypeAndSubmit
  - `slack_last_ts.txt`로 위치 추적, 쓰레드 댓글 자동 조회
  - **슬랙 메시지를 받았으면 반드시 해당 쓰레드에 슬랙으로 응답할 것!**
  - "전달했습니다" ack → `slack reply` 시 자동 삭제
- 경험 DB 노하우 방송: `knowhow.md`(일반) + `knowhow-{action}.md`(액션별) — 콘솔 방송
- A11Y 액션 실패 시 자동: snapshot + experience 저장 (0~9 ring)

### Slack + 프롬프트 전달
- Slack 수신 메시지는 **항상** Claude 프롬프트에 전달 (옵션 없음)
- 키워드 감시("클롣", "claude", "appbot" 등)도 **항상** 활성
- `--prompt`, `--keywords`, `--no-slack` 같은 옵션은 **존재하지 않음**
- 프롬프트 입력 실패 시: 전경 윈도우 진단 + Slack에 상황 공유

### 슬랙 답장 규칙 (MUST!)
- **슬랙에서 온 메시지엔 반드시 슬랙 쓰레드로 답장!** 여기(프롬프트)에만 답하면 안 됨
- 전달된 메시지 끝에 `thread=XXXXXXXXXX` 가 있으면 그 쓰레드에 답장:
  ```
  wkappbot slack reply "답변 내용" --thread 1772339328.052469
  ```
- 쓰레드 없는 채널 메시지에 답장:
  ```
  wkappbot slack send "답변 내용"
  ```
- 답장하면 "전달했습니다" ack 메시지가 자동 삭제됨

### 빌드 & 배포
- 코드 변경 후 **반드시** `dotnet publish` + AppBotEye 재시작까지 완료할 것
- 빌드 명령: `"$DOTNET_ROOT/dotnet" publish ... --verbosity quiet` (MEMORY.md 참조)
- 기존 wkappbot 프로세스 kill → publish → `wkappbot eye &`로 재시작

### 문서화 / 에러 대응 / Slack 스타일
- 중요 발견은 CLAUDE.md + 소스 주석에 기록 (컨텍스트 유실 대비)
- 방해꾼 창, 프롬프트 유실 → **스냅샷 + Slack 공유** (조용히 실패 금지!)
- 상태 변화는 `chat.update`로 동일 메시지 수정 (채널 스팸 방지)

### 토큰 최적화
- 요약 우선, 구간 추출, 단계 분리, 중복 출력 금지
- 대형 파일은 섹션별 순차 읽기, 문서화는 최소원칙

### HTS 자동화 핵심
- MFC 컨트롤은 UIA 패턴이 거의 없음 → Win32 메시지 폴백 필수
- SmartClickButton 3티어: BM_CLICK → WM_LBUTTON → SendInput
- 영웅문은 owner-drawn → GetWindowText 빈 문자열 → OCR 폴백

### 금지 사항
- 클롣에게 전달을 차단하는 옵션 절대 만들지 말 것
- 앱봇의 눈을 안 띄우는 옵션 만들지 말 것
- 유저에게 여기(프롬프트)에서만 질문하지 말 것 (반드시 슬랙 동시 발송)

## Overview
Claude Code 터미널에서 사용하는 범용 Windows 앱 UI 자동화 테스트 도구.
YAML 시나리오 기반으로 Windows 앱을 자동 조작하고 결과를 검증한다.

## Architecture

### 핵심 전략: 5티어 요소 탐색
1. **UIA** - FlaUI, AutomationId/Name/ControlType
2. **Vision Cache** - 이전 결과 캐시 (상대좌표)
3. **Simple OCR** - Windows.Media.Ocr (무료, 오프라인, 한글+영어)
4. **Vision API** - Claude API (고비용 최종 폴백)
5. **좌표 기반** - 절대 좌표 직접 지정

### 프로젝트 구조 (주요 파일)
```
csharp/src/
├── WKAppBot.CLI/           # CLI 진입점 + 각종 Command 파일들
├── WKAppBot.Core/          # ScenarioRunner, ActionExecutor, DialogHandlerManager
├── WKAppBot.Win32/         # NativeMethods, WindowFinder, AppScanner, ExperienceDb, UiaLocator, Input/*
├── WKAppBot.Vision/        # ChartAnalyzer, SimpleOcrAnalyzer, VisionCache, TooltipCalibrator
├── WKAppBot.WebBot/        # CdpClient, ChromeLauncher, SlackSocketClient
└── WKAppBot.Report/        # 리포트 생성 (스켈레톤)
handlers/                   # 방해꾼 다이얼로그 핸들러 YAML
scenarios/                  # YAML 테스트 시나리오
```

## Build & Run
```bash
# Build (bash)
"$DOTNET_ROOT/dotnet" build 'W:/GitHub/WKAppBot/csharp/WKAppBot.sln' -c Release --verbosity quiet
# Publish (single-file EXE → W:/SDK/bin/)
"$DOTNET_ROOT/dotnet" publish 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --self-contained false -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -r win-x64 --verbosity quiet
# Run
wkappbot run 'W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml' -v
```

## CLI 명령어
> **`<grap>`** = **gr**ab **a**ccessibility **p**attern — 와일드카드/정규식/경로glob 지원
```
wkappbot run <scenario.yaml> [-v] [--no-watch]
wkappbot validate <scenario.yaml>
wkappbot inspect <grap>[/<child-grap>] [--depth N] [--win32] [--filter <pattern>]
wkappbot focus [--title <grap>] [--delay N] [--depth N] [--win32] [-b]
wkappbot watch [--duration N] [--live] [--win32] [--interval N]
wkappbot capture <grap> [-o output.png]
wkappbot scan <grap> [--save] [--ocr] [--detail] [--depth N]
wkappbot do <grap> <form-id> [button-text] [--confirm]
wkappbot dismiss <grap> [keywords...]
wkappbot input <grap> <form-id> <text> [--cid N] [--enter] [--method N] [--no-zoom]
wkappbot ocr <grap|image.png> [--save] [-o output.txt]
wkappbot snapshot <grap> [--tag <name>] [--depth N]
wkappbot windows [grap] [--deep] [--process <name>] [--class <name>] [--all]
wkappbot win-click <grap> <x> <y> [--uia]
wkappbot tab-select <grap> --aid <tab-aid> [--list|--select <text>|--index N]
wkappbot cond-add <grap> <keyword> [--dry-run]
wkappbot toolbar-ocr <grap> [--click "text"] [--save]
wkappbot titlebar <grap> <form-id> [button-index] [--ocr] [--save]
wkappbot chart-analyze <grap|image.png> [--form <id>] [--candles N] [--debug] [--tooltip]
wkappbot hts-stress <form.xmf> [-n 20] [--pattern repeat|memory|ctx-only]
wkappbot uia-test <grap> [--invoke <name>]
wkappbot zoom-demo <grap> [text]
wkappbot web <subcommand> [options]           # CDP 웹 자동화 (open/navigate/click/type/eval/screenshot/...)
wkappbot slack <subcommand>                   # send/reply/upload/screenshot/test/listen/catch-up
wkappbot knowhow <subcommand>                # write/read (Win32+Web 노하우)
wkappbot eye [--port N] [--interval N]        # AppBotEye (Slack+Prompt 항상 ON)
wkappbot schedule <subcommand>                # add/list/remove/clear (예약 프롬프트)
wkappbot logcat <fileFilter> <messageFilter>  # 실시간 로그 추적
```

## Key Design Decisions

### 1. Focusless-First 원칙
포커스 없이 가능한 입력은 포커스 확보 없이 진행 → 사용자 작업 방해 최소화!
- UIA Invoke/Value/Toggle/Select/Scroll/RangeValue/Window = Focusless
- PostMessage WM_CHAR (CMaskEditEx) = Focusless
- MSAA put_accValue (Claude 프롬프트 입력) = Focusless
- SendInput/Hotkey = EnsureFocus 필요

### 2. Smart Focus ("위치확보!") — EnsureFocus 4단계
Phase 0: Already Focused? → Phase 1: Alert+Wait(3초) → Phase 2: Force Recovery → Phase 3: Timeout Fail

### 3. 태그 규칙
`[WATCH]` 요소추적 / `[RUN]` 실행 / `[FOCUS]` 포커스 / `[VERIFY]` 검증 / `[BLOCK]` 방해꾼 / `[GUARD]` 포커스간섭 / `[ZOOM]` 돋보기 / `[STRESS]` 스트레스 / `[TOOLTIP]` 캘리브레이션 / `[SLACK]` 슬랙 / `[EXP]` 경험DB / `[KNOWHOW]` 노하우

### 4. grap 패턴 매칭
| 구문 | 예시 | 동작 |
|------|------|------|
| 리터럴 | `"plusButton"` | 정확 일치 |
| 와일드카드 | `"*Button*"` | glob 스타일 |
| 정규식 | `"regex:btn_\\d+"` | 정규식 |
| 경로 glob | `"**/#32770"` | GitHub-style (classPath 매칭) |

## YAML Scenario Format (요약)
```yaml
scenario: { name: "Test" }
app: { launch: "calc.exe", wait_for_window: { title_contains: "계산기" } }
config: { continue_on_error: true, retry_count: 2, prefer_focusless: true }
steps:
  - { name: "Click Plus", target: { automation_id: "plusButton" }, action: click }
  - { name: "Verify", target: { automation_id: "CalculatorResults" }, action: assert, params: { type: text_contains, expected: "42" } }
```

### 지원 액션
click, double_click, right_click, type_text, press_key, hotkey, wait, assert, scroll, screenshot, toggle, expand, collapse, select, set_range, window_close, window_minimize, window_maximize

## 구현 로드맵

### 완료된 Phases (상세는 MEMORY.md 참조)
- Phase 1~7: Focus, Vision Cache, OCR, HTS Stress, Pattern Matching, Experience DB, Click Strategy DB
- Phase 6.1~6.4: PrintWindow 캡처, 트리 폴더, 캡처 검증, 윈도우 스타일
- Dialog Handlers, dismiss, toolbar-ocr, titlebar, ChartAnalyzer v11+B+V
- WebBot CDP (Phase 11A), Slack Socket Mode (Phase 12), AppBotEye
- Focusless PostMessage, InputFocusGuard, InputZoom, GlobalMode Socket Mode
- Context Monitor + Auto-Relay, tab-select, cond-add, FocuslessGuard
- Claude 프롬프트 Focusless 입력 (MSAA put_accValue)
- 키움 프록시봇 Phase A (32비트 COM + Named Pipe)

### Phase 8: puppet 패턴 매칭 — 미구현
- FormTypeIdentifier Level 4: OCR 텍스트 vs 패턴 매칭으로 폼 자동 식별

### Phase 9: 원격(Image-Only) 자동화 — 미구현
- 스크린샷만으로 폼 식별 → Experience DB 상대좌표로 클릭

### Phase 10: logcat — 미구현
- 윈도우 변화 실시간 스트리밍 (EnumWindows diff → SetWinEventHook)

### Phase 11B: 웹봇 고급 — 로드맵
- 탭 관리, 네트워크 모니터링, 쿠키/스토리지, PDF, 이벤트 리스닝

### Phase 13: 키움 OpenAPI+ — Phase A 완료, Phase B/C 미구현
- 아키텍처: `[앱봇 64비트] ←Named Pipe→ [프록시봇 32비트] ←COM→ [키움 OpenAPI+]`
- 키움 COM: 58 methods + 9 events (TypeLib 분석 완료 → `C:\OpenAPI\khopenapi_typelib.md`)

## 배포 구조
```
W:/SDK/bin/wkappbot.exe          # single-file EXE
W:/SDK/bin/wkappbot.hq/          # 본부 (handlers, profiles, runtime, logs, output)
```

## 참고 (상세 정보 위치)
- **MEMORY.md**: 빌드 명령, 완료된 Phase 상세, 아키텍처 결정 기록
- **memory/chart-analyzer.md**: ChartAnalyzer 기술 상세 (3전략, HTS 색상, ultra-dense)
- **memory/hts-controls.md**: HTS 영웅문 컨트롤 상세
- **memory/condition_search.md**: 조건검색 자동화 전략
- **knowhow.md** (각 컨트롤 폴더): 컨트롤별 자동화 노하우
- Windows 전용 (.NET 8.0 `net8.0-windows10.0.22621.0`), 한국어 UI 지원
