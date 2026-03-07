# WKAppBot v2.1 - Windows App Automation Test Framework

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

### EyeTick 사용법 & 운영 메모
- **`wkappbot eye tick`**: one-shot 진단 명령 — 모든 활성 클롣/크로 상태를 즉시 조회
  - 출력: 액션 트리플릿 + 카드별 상태/생각 + 크로 진행/완료/예정 + 슬랙 신규메시지
  - `ctx=N%` 표시: 현재 세션 컨텍스트 사용량 (90%→경고, 95%→긴급)
- **`wkappbot eye`**: 글로벌 루프 모드 — FSW 하이브리드 (이벤트 기반 dirty-check + 1초 안전망)
  - AppBotEye = Slack 데몬 통합 → 별도 `slack listen` 불필요
  - Eye 재시작 시 이전 idle 상태 메시지 자동 삭제 (Slack 스팸 방지)
- **카드 시스템**: 크로 카드 (OpenClaw 서비스) + 클롣 카드 (Claude Code CLI, CWD별)
  - 클롣 카드: 상태(대기/실행중) + 생각(세션 JSONL tail 추출) + CWD 약식명
  - 크로 카드: 진행/완료/예정 + 생각(OpenClaw sessions JSONL)
  - **클롣 생각 추출**: CWD → `~/.claude/projects/{projDir}/*.jsonl` → 최신 파일 tail 8-32KB → assistant 텍스트
- **슬랙 연동**:
  - EyeTick 슬랙 메시지 전달 (MUST!): conversations.history API → 새 메시지 → FindPrompt → TypeAndSubmit
  - `slack_last_ts.txt`로 위치 추적, 쓰레드 댓글 자동 조회
  - **슬랙 메시지를 받았으면 반드시 해당 쓰레드에 슬랙으로 응답할 것!**
  - "전달했습니다" ack → `slack reply` 시 자동 삭제
- 경험 DB 노하우 방송: `[KNOWHOW:A11Y]` (프로필 경험) + `[KNOWHOW:OS]` (OS 경험) 분리 출력
- A11Y 액션 실패 시 자동: snapshot + experience 저장 (0~9 ring)
- 노이즈 필터: `NO_REPLY`, `send ㄱㄱ`, `telegram send ㄱㄱ`, `ㄱㄱ`
- 계획 출력: `[KRO_PLAN_BEGIN]` ~ `[KRO_PLAN_END]` 블록
- **인수인계**: `wkappbot newchat "프롬프트"` 명령으로 새채팅 열고 프롬프트 전달 (focusless)

### Slack + 프롬프트 전달
- Slack 수신 메시지는 **항상** Claude 프롬프트에 전달 (옵션 없음)
- 키워드 감시("클롣", "claude", "appbot" 등)도 **항상** 활성
- `--prompt`, `--keywords`, `--no-slack` 같은 옵션은 **존재하지 않음**
- 프롬프트 입력 실패 시: 전경 윈도우 진단 + Slack에 상황 공유

### 슬랙 답장 규칙 (MUST!)
- **슬랙에서 온 메시지엔 반드시 슬랙 쓰레드로 답장!** 여기(프롬프트)에만 답하면 안 됨
- 전달된 메시지 끝에 `--msg XXXXXXXXXX` 가 있으면 그 메시지에 답장:
  ```
  wkappbot slack reply "답변 내용" --msg 1772339328.052469
  ```
- 쓰레드 없는 채널 메시지에 답장:
  ```
  wkappbot slack send "답변 내용"
  ```
- 답장하면 "전달했습니다" ack 메시지가 자동 삭제됨

### 빌드 & 배포
- 코드 변경 후 **반드시** `dotnet publish` + AppBotEye 재시작까지 완료할 것
- 빌드 명령: `"$DOTNET_ROOT/dotnet" publish ... --verbosity quiet` (MEMORY.md 참조)
- Eye 종료 → publish → Eye 재시작:
  ```bash
  wkappbot a11y close wkappbot 2>/dev/null; "W:/SDK/dotnet/dotnet" publish ... --verbosity quiet
  ```
  a11y close는 자기 조상 프로세스를 자동 보호 (Eye만 닫힘)

### 문서화 / 에러 대응 / Slack 스타일
- 중요 발견은 CLAUDE.md + 소스 주석에 기록 (컨텍스트 유실 대비)
- 방해꾼 창, 프롬프트 유실 → **스냅샷 + Slack 공유** (조용히 실패 금지!)
- 상태 변화는 `chat.update`로 동일 메시지 수정 (채널 스팸 방지)

### Source File Size Policy
- **Recommend ~400 lines per WKAppBot source file** — split by logical unit when it grows beyond
- Reason: on token overflow, smaller models (GPT mini) may edit — smaller files are safer
- **NEVER split/refactor customer code!** This policy applies to our code (WKAppBot) only

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
Windows a11y (접근성) 표준 액션 기반 앱 UI 자동화 프레임워크.
CLI 한 줄로 UIA→Win32→SendInput 3티어 자동 폴백, 포커스리스 제어, 돋보기 피드백까지.
YAML 시나리오 기반 자동 테스트도 지원.

### Busybox-Style Shortcut
- `a11y.exe` = symlink to `wkappbot.exe` → exe 이름으로 명령 자동 감지
- `a11y click "*메모장*#*저장*"` = `wkappbot a11y click "*메모장*#*저장*"`
- Known commands: a11y, inspect, ocr, logcat, capture, scan, windows, snapshot, readiness, ask

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
> **`<grap>`** = **gr**ab **a**ccessibility **p**attern — 와일드카드/정규식/경로glob/#UIA스코프 지원
```
wkappbot run <scenario.yaml> [-v] [--no-watch]
wkappbot validate <scenario.yaml>
wkappbot inspect <grap>[/<child-grap>][#<uia-scope>] [--depth N] [--win32] [--filter <pattern>]
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
wkappbot eye [--port N] [--interval N]        # AppBotEye 글로벌 루프 (Slack+Prompt 항상 ON)
wkappbot eye tick                             # one-shot: 모든 클롣/크로 카드 상태+생각 즉시 조회
wkappbot newchat "prompt" [--file f.txt]      # Claude Desktop 새채팅 열고 프롬프트 입력 (focusless)
wkappbot readiness [grap] [--point X Y] [--yield] # InputReadiness 진단 (완전 포커스리스, 돋보기 강제)
wkappbot schedule <subcommand>                # add/list/remove/clear (예약 프롬프트)
wkappbot logcat <fileFilter> <messageFilter> [--basedir <dir>] [-r[=N]] [--hq]  # 실시간 로그 추적
wkappbot ask gpt|gemini "question" [--slack] [--timeout N] [--new-tab]  # CDP 웹 AI 질문
wkappbot a11y <action> <grap>[#uia-scope] [options]  # ★ 표준 통합 명령 (MCP 유일 도구)
  # Discovery (4): inspect, windows, screenshot, ocr — 기존 명령 위임
  # Window (7): close, minimize, maximize, restore, focus, move(--x --y), resize(--w --h)
  # Element (13): find, read, highlight, invoke, click, toggle, expand, collapse, select, scroll, type(--text), set-value(--text), set-range(--value)
  # Web auto-fallback: Chrome/Electron 윈도우 → CSS selector 자동감지 → CDP 엔진
  # grap `#`scope: "*메모장*#*파일*" (UIA), "*Chrome*#button.submit" (CSS→CDP)
  # --all, --nth N (range: 2~4, ~3, 3~), --depth N, --force, --force-close-ancestors
  # 10-step auto pipeline: find → ancestor protect → blocker dismiss → restore → child walk → UIA scope → tab activate → zoom → execute → feedback
wkappbot mcp                                   # MCP stdio 서버 (도구 1개: wkappbot)
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

### 2.5. 포커스 양보 승인 (UserInputWaitOverlay)
- "input" 액션: focusless 메서드 유무와 관계없이 **항상** yield 로직 진입
- 타겟 전경 + 유저 idle(30초) → 자동승인 (팝업 없음)
- 타겟 비전경 or 유저 활동 → 승인 팝업 (30초 카운트다운)
- 승인창 위치: positionHwnd(타겟 윈도우) 내부 상단 1/3 + 가상화면 바운드 클램핑
- 소유자(GWL_HWNDPARENT): ownerHwnd(메인 윈도우) — 최소화 시 같이 숨김

### 3. 태그 규칙
`[WATCH]` 요소추적 / `[RUN]` 실행 / `[FOCUS]` 포커스 / `[VERIFY]` 검증 / `[BLOCK]` 방해꾼 / `[GUARD]` 포커스간섭 / `[ZOOM]` 돋보기 / `[STRESS]` 스트레스 / `[TOOLTIP]` 캘리브레이션 / `[SLACK]` 슬랙 / `[EXP]` 경험DB / `[KNOWHOW]` 노하우

### 4. grap 패턴 매칭

**검색 대상 문자열 (search key)**: `[ClassName] Title (processName hwnd=XXXXXXXX WxH)`
- 예: `[Notepad] 제목 없음 - 메모장 (Notepad.exe hwnd=001A0F2C 800x600)`
- grap 패턴은 Title 먼저, 안 되면 전체 search key에 매칭 시도
- `hwnd=` 로 핸들 직접 매칭 가능: `"*hwnd=001A0F2C*"`

| 구문 | 예시 | 동작 |
|------|------|------|
| 리터럴 | `"plusButton"` | 정확 일치 |
| 와일드카드 | `"*Button*"` | glob 스타일 |
| 정규식 | `"regex:btn_\\d+"` | 정규식 |
| OR 패턴 | `"*메모장*;*계산기*"` | `;` 구분 다중 매칭 (a11y에서 실험중) |
| 경로 glob | `"**/#32770"` | GitHub-style (classPath 매칭) |
| Win32 자식 | `"투혼/[0600]*"` | `/` 뒤 = MDI 자식 윈도우 매칭 |
| **#UIA 스코프** | `"영웅문#실시간계좌"` | `#` 뒤 = UIA Name/AutomationId 매칭으로 루트 축소 |
| UIA 다단 경로 | `"영웅문#폼#Tab"` | `#`/`/` 모두 UIA 계층 구분자 |
| **탭 포털** | `"Chrome#ChatGPT#모델"` | TabItem 매칭 → 자동 탭 전환 → RootWebArea 점프 |
| **웹 a11y** | `"Chrome#Gemini#새 채팅"` | 탭 포털 경유 웹 요소 접근 (UIA, CDP 불필요!) |
| aid 매칭 | `"Claude#email"` | Name 매칭 실패 시 AutomationId로 폴백 |

> **`#` 스코프 = URL fragment 스타일**: `window/child#a11y-bookmark`
> - `#` 앞: Win32 윈도우 탐색 (기존 grap)
> - `#` 뒤: UIA 트리에서 Name→AutomationId 매칭 (container-first)
> - **탭 포털 (v2.1)**: `#`이 TabItem에 매칭되면 자동 탭 전환 + RootWebArea 점프
>   - Chrome/Edge 탭, Electron 앱(Claude/VS Code) 웹 콘텐츠까지 도달
>   - UIA만으로 동작 → CDP 불필요, 포커스리스!
> - 적용 명령: `a11y` 전체, `inspect`, `tab-select`, `uia-test`

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
- Context Monitor (ctx% 표시 + newchat 안내), tab-select, cond-add, FocuslessGuard
- Claude 프롬프트 Focusless 입력 (MSAA put_accValue)
- newchat 명령 (사이드바 토글 + 새 대화 invoke + 프롬프트 입력, 전부 focusless)
- A11Y/OS 듀얼경로 노하우 방송 (프로필 경험 vs OS 경험 분리)
- 키움 프록시봇 Phase A (32비트 COM + Named Pipe)
- 포커스 양보 승인창 (UserInputWaitOverlay) — 멀티모니터 타겟 윈도우 배치 + positionHwnd 분리
- InputReadiness: "input" 액션 항상 yield 로직 진입 (focusless여도 승인창 표시)
- 돋보기/승인창 가상화면 바운드 클램핑 (SM_XVIRTUALSCREEN 기반, 보조모니터 음수좌표 대응)
- ElevationRequesterAdapter (UAC runas 관리자 재시작)
- ExperienceDb InputMethods (컨트롤별 입력 메서드 성공/실패 기록)
- readiness 명령: InputReadiness 진단 (완전 포커스리스, 돋보기 강제, 전수조사/좌표기반)
- 유령 돋보기: BeginFadeOut 시 포그라운드 스레드 승격 → 프로세스 종료 후에도 페이드아웃 생존
- 돋보기 불투명도 50% → 77% (럭키세븐), eye tick/Probe [PROF] 프로파일링 태그
- 코어 a11y InputReadiness: ActionExecutor 모든 UIA 액션에 EnsureInputReady (미니마이즈 복원+방해꾼감지+돋보기)
- Chrome 포커스 강탈 방어: SW_SHOWNOACTIVATE + 즉시 복구 + FocuslessWarningOverlay 연동
- AgentsPolicy 최적화: Timer 제거 → CreationTime(INITIAL)/LastWriteTime(REMINDER) 파일 시간 기반
- ask gpt/gemini a11y-first: CDP Input.insertText, a11y selector chain, 페르소나+READY 마커
- a11y 공용 액션 폴백: 표준 11개 액션 (invoke/click/toggle/expand/collapse/select/scroll/type/set-value/set-range/focus)
  - grap `#` scope로 UIA 요소 직접 지정 → ResolveFullGrap 통합
  - 각 액션별 UIA → Win32 → SendInput 3티어 폴백
- ask 포커스리스 텍스트 삽입 (innerHTML+InputEvent Tier 1), 탭 URL 검증, about:blank 자동 정리
- ask 턴 감지 다중 셀렉터 (data-message-author-role / conversation-turn / article 폴백)
- ask 탭 핸드오프 — 동시 질문 시 GPT↔Gemini 탭 상부상조 (BringToFront, 포커스리스)
  - 스트리밍 중 텍스트 증가/완료 시 자동 탭 양보, 페르소나 완료 대기 후 실제 질문 전송
  - baseResponseCount로 페르소나 응답 스킵, 새 응답만 감지
- a11y 조상 프로세스 보호 (자기 프로세스 트리 자동 제외, --force-close-ancestors로 해제)
- a11y 매칭 윈도우 search key 출력 (hwnd 타겟팅 지원)
- grap `;` OR 패턴 (a11y 실험), logcat `regex:` 파일 패턴, logcat CWD 스코핑+depth 제한
- CLAUDE.md grap search key 포맷 문서화
- **v2.0 a11y 표준 액션 프레임워크** (20 actions: 7 window + 13 element)
  - Unified pipeline: collect all → select range (--nth/--all) → dispatch
  - 10-step auto pipeline: find → ancestor protect → blocker → restore → child → UIA scope → tab activate → zoom → execute → feedback
  - Busybox exe name detection: `a11y.exe` symlink → auto command injection
  - highlight action: ClickZoomHelper overlay on target element
  - find action: Win32 children + UIA tree dump (MUD "look" command)
- **v2.1 Tab Portal + Web A11y** — 합성 풀경로로 웹 컨트롤까지 접근
  - Tab Portal: `#` 스코프에서 TabItem 매칭 → 자동 탭 전환 + RootWebArea 점프
  - Chrome/Edge 탭 + Electron 앱(Claude/VS Code) 웹 콘텐츠 = UIA만으로 접근 (CDP 불필요!)
  - AutomationId 매칭: Name 실패 시 aid 폴백 (`Claude#email` → aid="email")
  - matched 출력에 search key 전체 표시 (processName 포함)
- **v3.0 Unified MCP + CDP Auto-Fallback** — ONE command to rule them all
  - MCP 서버: 도구 26개 → 1개 (`wkappbot`) 통합, description에 grap 패턴+실전 시나리오 풍성하게
  - a11y Discovery 액션: inspect, windows, screenshot, ocr → 기존 명령 위임
  - 웹뷰 CDP 자동 폴백: Chrome_WidgetWin_1 감지 → CSS selector 자동 판별 → CDP 엔진
  - CDP 포트 자동탐지: netstat + 프로세스 트리 매칭 (wmic 대체)
  - GrapHelper.LooksLikeCssSelector: `.`/`[`/`>`/HTML 태그 → CSS, `*`/`?` → UIA
  - McpDescriptions.cs: description 텍스트 분리, 실전 시나리오 5가지 + Quick Examples 10개
  - 웹 최상위 별칭 제거 (eval/tabs/navigate 등 → `web <sub>` 또는 통합 a11y로)
  - MUD-style portal 배너: "A MUD-style portal for AI to see and touch the real world."
  - EnsureTabActive: walk up UIA parents, auto-select unselected TabItem
  - Zoom/magnifier on ALL actions (before + result feedback after)
  - Source split: A11yCommand.cs (~320 lines) + A11yElementActions.cs (~600 lines)

### Phase 8: puppet 패턴 매칭 — 미구현
- FormTypeIdentifier Level 4: OCR 텍스트 vs 패턴 매칭으로 폼 자동 식별

### Phase 9: 원격(Image-Only) 자동화 — 미구현
- 스크린샷만으로 폼 식별 → Experience DB 상대좌표로 클릭

### Phase 10: logcat — 구현됨
- 파일 실시간 스트리밍 (FSW + tail), CWD 기본, depth 제한(-r=N), --hq 옵션
- grap 스타일 파일 패턴: 와일드카드 + `regex:` + `;` OR

### Phase 11B: 웹봇 고급 — 로드맵
- 탭 관리, 네트워크 모니터링, 쿠키/스토리지, PDF, 이벤트 리스닝

### Phase 13: 키움 OpenAPI+ — Phase A 완료, Phase B/C 미구현
- 아키텍처: `[앱봇 64비트] ←Named Pipe→ [프록시봇 32비트] ←COM→ [키움 OpenAPI+]`
- 키움 COM: 58 methods + 9 events (TypeLib 분석 완료 → `C:\OpenAPI\khopenapi_typelib.md`)

### 완료: EyeTick 크로/클롣 생각 분리 (2026-03-02)
- "최근 생각" → "크로 생각:" 크로 카드 내부로 이동
- "클롣 생각:" 추가: CWD별 Claude Code JSONL → 점진적 tail 읽기 (8KB→32KB) → 최신 assistant 텍스트
- CWD→projDir 매핑: `Replace(':','-').Replace('\\','-').Replace('_','-')`

### 실패 액션 기록 정책 (knowhow-failed-actions.md)
- **A11Y 실패** → A11Y 경로에 기록 (`profiles/{name}_exp/form_{id}/knowhow-failed-actions.md`)
- **OS 실패** → OS 경로에 기록 (`experience/{process}/{class}/knowhow-failed-actions.md`)
- 각 경로에서 inspect 시 `[KNOWHOW:A11Y]` / `[KNOWHOW:OS]` 태그로 파일명만 방송
- 폴더 경로는 이미 `[A11Y]` / `[OS]` 줄로 공유되므로 파일명만 표시하면 충분

### TODO: 방해꾼(DialogHandler) 감지 누락 이슈
- **문제**: 포커스 입력(SendInput) 시도 중 팝업("#32770 선택영역을 확인하십시요")이 떴는데 방해꾼으로 자동 감지되지 않음
- **원인 추정**: cond-add 명령의 입력 루프에서 PatrolWaitLoop/DialogHandlerManager 체크가 빠져있거나, EnsureFocus 단계에서만 방해꾼 체크가 돌아가고 PostMessage/UIA Invoke 경로에는 체크가 없음
- **해야 할 것**: 포커스 입력을 시도할 때 방해꾼 체크가 입력확보 단계에서 돌아가도록 구조 개선. 특히 UIA Invoke 실패 후 팝업 감지 → 자동 dismiss → 재시도 패턴 필요
- **우선순위**: 나중에 볼 것 (2026-03-02 기록)

## 배포 구조
```
W:/SDK/bin/wkappbot.exe          # single-file EXE (v2.0)
W:/SDK/bin/a11y.exe              # symlink → wkappbot.exe (busybox shortcut)
W:/SDK/bin/wka11y.exe            # symlink → wkappbot.exe (busybox shortcut)
W:/SDK/bin/wkappbot.hq/          # 본부 (handlers, profiles, runtime, logs, output)
```

## 참고 (상세 정보 위치)
- **MEMORY.md**: 빌드 명령, 완료된 Phase 상세, 아키텍처 결정 기록
- **memory/chart-analyzer.md**: ChartAnalyzer 기술 상세 (3전략, HTS 색상, ultra-dense)
- **memory/hts-controls.md**: HTS 영웅문 컨트롤 상세
- **memory/condition_search.md**: 조건검색 자동화 전략
- **knowhow.md** (각 컨트롤 폴더): 컨트롤별 자동화 노하우
- Windows 전용 (.NET 8.0 `net8.0-windows10.0.22621.0`), 한국어 UI 지원

