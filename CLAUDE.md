# WKAppBot v4.4.2 - Windows + Android App Automation Test Framework

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

### EyeTick & 슬랙 연동
- **`wkappbot eye tick`**: one-shot — 모든 활성 클롣/크로 상태 즉시 조회 (`ctx=N%` 포함)
- **`wkappbot eye`**: 글로벌 루프 — FSW 하이브리드, Slack 메시지 자동 전달+ack
- Eye 재시작 시 이전 idle 상태 메시지 자동 삭제 (Slack 스팸 방지)
- **슬랙 메시지를 받았으면 반드시 해당 쓰레드에 슬랙으로 응답할 것!**
- "전달했습니다" ack → `slack reply` 시 자동 삭제
- 노이즈 필터: `NO_REPLY`, `send ㄱㄱ`, `ㄱㄱ`
- **인수인계**: `wkappbot newchat "프롬프트"` 명령으로 새채팅 열고 프롬프트 전달

### 슬랙 답장 규칙 (MUST!)
- **슬랙에서 온 메시지엔 반드시 슬랙 쓰레드로 답장!** 여기(프롬프트)에만 답하면 안 됨
- 전달된 메시지 끝에 `--msg XXXXXXXXXX` 가 있으면 그 메시지에 답장:
  ```
  wkappbot slack reply "답변 내용" --msg 1772339328.052469
  ```
- 쓰레드 없는 채널 메시지에 답장: `wkappbot slack send "답변 내용"`

### 빌드 & 배포
- 코드 변경 후 **반드시** `dotnet publish` + AppBotEye 재시작까지 완료할 것
- Eye 종료 → publish → Eye 재시작:
  ```bash
  wkappbot a11y kill wkappbot 2>/dev/null; "W:/SDK/dotnet/dotnet" publish 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --verbosity quiet
  ```
  a11y kill은 프로세스명 기반 — 창 제목에 WKAppBot 있는 VS Code 등은 매칭 안 됨 (안전)

### Source File Size Policy
- **Recommend ~400 lines per WKAppBot source file** — split by logical unit when it grows beyond
- **NEVER split/refactor customer code!** This policy applies to our code (WKAppBot) only

### 금지 사항
- 클롣에게 전달을 차단하는 옵션 절대 만들지 말 것
- 앱봇의 눈을 안 띄우는 옵션 만들지 말 것
- 유저에게 여기(프롬프트)에서만 질문하지 말 것 (반드시 슬랙 동시 발송)

### HTS 자동화 핵심
- MFC 컨트롤은 UIA 패턴이 거의 없음 → Win32 메시지 폴백 필수
- SmartClickButton 3티어: BM_CLICK → WM_LBUTTON → SendInput
- 영웅문은 owner-drawn → GetWindowText 빈 문자열 → OCR 폴백

---

## Overview
Windows a11y 기반 앱 UI 자동화 프레임워크. UIA→Win32→SendInput 3티어 자동 폴백, 포커스리스 제어, 돋보기 피드백.

### Busybox-Style Shortcut
- `a11y.exe` = symlink to `wkappbot.exe` → exe 이름으로 명령 자동 감지
- Known commands: a11y, inspect, ocr, logcat, capture, scan, windows, snapshot, readiness, ask

## Architecture

### 핵심 전략: 5티어 요소 탐색
UIA → Vision Cache → Simple OCR → Vision API(Claude) → 좌표 기반

### 프로젝트 구조
```
csharp/src/
├── WKAppBot.CLI/           # CLI 진입점 + Commands/
├── WKAppBot.Core/          # ScenarioRunner, ActionExecutor, DialogHandlerManager
├── WKAppBot.Win32/         # NativeMethods, WindowFinder, UiaLocator, Input/*
├── WKAppBot.Vision/        # ChartAnalyzer, SimpleOcrAnalyzer, VisionCache
├── WKAppBot.WebBot/        # CdpClient, ChromeLauncher, SlackSocketClient
├── WKAppBot.Android/       # AdbClient, AndroidA11yTree, AdbGrapRouter
handlers/                   # 방해꾼 다이얼로그 핸들러 YAML
scenarios/                  # YAML 테스트 시나리오
```

## Build & Run
```bash
# Publish (single-file EXE → W:/SDK/bin/)
"W:/SDK/dotnet/dotnet" publish 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --verbosity quiet
wkappbot run 'W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml' -v
```

## CLI 명령어
> **`<grap>`** = **gr**ab **a**ccessibility **p**attern — 와일드카드/정규식/경로glob/#UIA스코프 지원
```
wkappbot inspect <grap>[#<uia-scope>] [--depth N] [--win32] [--filter <pattern>]
wkappbot windows [grap] [--deep] [--process <name>] [--all]
wkappbot scan / capture / ocr / snapshot / watch / focus / zoom-demo ...
wkappbot slack send "msg" [file.png]          # 텍스트+파일 전송
wkappbot slack reply "msg" [file.png] --msg TS # 쓰레드 답장
wkappbot slack upload <file> [--msg TS]
wkappbot eye / eye tick                       # AppBotEye 루프 / one-shot
wkappbot newchat "prompt" [--file f.txt]      # Claude Desktop 새채팅 (focusless)
wkappbot ask gpt|gemini|claude "line1" [file.png]  # CDP 웹 AI 질문 (MCP도 동일 명령)
wkappbot speak / suggest / claude-usage / schedule / readiness ...
wkappbot logcat <fileGlob[;glob2]> [regex1 regex2 ...] [--hq] [--past Ns/Nm/Nh] [-f] [--timeout N] [-r]
  # fileGlob: glob + ';' OR  (e.g. "*.file.*;*.eye.*" / "**" = all)
  # regex args: pure regex, multiple = AND  (e.g. "OCR-DEEP" "block(len|size)")
  # --past only      → grep-style: scan and exit
  # --past + -f      → scan then live follow
  # --past + --timeout N → scan then live, auto-exit after N
  # no --past        → live only (real-time FSW)
  # --hq: include wkappbot.hq/logs (finished logs live here)
  # Ctrl+C / broken pipe / --timeout → clean exit
wkappbot mcp                                  # MCP stdio 서버 (도구 1개: wkappbot)
wkappbot a11y <action> <grap>[#uia-scope] [options]  # ★ 표준 통합 명령
  # Discovery: inspect, find, windows, screenshot, ocr
  # Window (7): close, minimize, maximize, restore, focus, move, resize
  # Element (13): find, read, highlight, invoke, click, toggle, expand, collapse,
  #               select, scroll, type(--text), set-value(--text), set-range(--value)
  # Async (2): wait(--timeout --interval), eval(--text "js")
  # Utility (3): clipboard, clipboard-read, clipboard-write
  # --all, --nth N, --depth N, --force, --force-close-ancestors, --timeout N, --speak
  # Web auto-fallback: Chrome/Electron → CSS selector 자동감지 → CDP 엔진
  # adb:// scheme: Android ADB USB 디바이스 제어 (30 actions)
wkappbot a11y kill <pattern>[/<ancestor>] [--allow-ancestors]  # 프로세스 킬
```

## grap 패턴 매칭

**Search key**: `[ClassName] Title (processName hwnd=XXXXXXXX WxH)`

| 구문 | 예시 | 동작 |
|------|------|------|
| 와일드카드 | `"*Button*"` | glob 스타일 |
| 정규식 | `"regex:btn_\\d+"` | 정규식 |
| OR 패턴 | `"*메모장*;*계산기*"` | `;` 구분 |
| **#UIA 스코프** | `"영웅문#실시간계좌"` | `#` 뒤 = UIA Name/AutomationId |
| **탭 포털** | `"Chrome#ChatGPT#모델"` | TabItem → 자동 탭 전환 → RootWebArea |
| **adb://** | `"adb://Fold5/*heromts*#해외잔고"` | Android ADB 제어 |
| hwnd 직접 | `"*hwnd=001A0F2C*"` | 핸들 직접 매칭 |

## Key Design Decisions

### Focusless-First
UIA Invoke/Value/Toggle/Select/Scroll/RangeValue = Focusless
PostMessage WM_CHAR / MSAA put_accValue = Focusless
SendInput/Hotkey = EnsureFocus 필요

### 포커스 양보 승인 (UserInputWaitOverlay)
- "input" 액션은 항상 yield 로직 진입
- 타겟 전경+idle(30초) → 자동승인 / 비전경/활동 → 팝업(30초 카운트다운)

### 태그 규칙
`[WATCH]` `[RUN]` `[FOCUS]` `[VERIFY]` `[BLOCK]` `[GUARD]` `[ZOOM]` `[SLACK]` `[EXP]` `[KNOWHOW]`

## YAML Scenario Format (요약)
```yaml
scenario: { name: "Test" }
app: { launch: "calc.exe", wait_for_window: { title_contains: "계산기" } }
steps:
  - { name: "Click", target: { automation_id: "plusButton" }, action: click }
  - { name: "Verify", target: { automation_id: "CalculatorResults" }, action: assert,
      params: { type: text_contains, expected: "42" } }
```
지원 액션: click, double_click, right_click, type_text, press_key, hotkey, wait, assert, scroll, screenshot, toggle, expand, collapse, select, set_range, window_close/minimize/maximize

## 배포 구조
```
W:/SDK/bin/wkappbot.exe          # single-file EXE
W:/SDK/bin/a11y.exe / wka11y.exe # symlink → wkappbot.exe
W:/SDK/bin/wkappbot.hq/          # handlers, profiles, runtime, logs
```

## 참고
- **MEMORY.md**: 빌드 명령, 완료 Phase 상세, 아키텍처 결정
- **memory/**: chart-analyzer, hts-controls, condition_search, a11y-actions 등
- Windows 전용 (.NET 8.0 `net8.0-windows10.0.22621.0`), 한국어 UI 지원
