# WKAppBot v5.9.0 - Windows + Android App Automation Test Framework

## 클롣 운영 규칙 (필독!)

### 언어/소통
- **유저: 항상 한국어** / 내부 문서·AI 파일: 영어 (토큰 절약)
- **질문 시**: `wkappbot slack send "질문"` + 프롬프트 동시 발송 (슬랙 단독 발송 금지)
- **슬랙 수신**: 반드시 슬랙 쓰레드로 답장 (`--msg TS` 있으면 reply, 없으면 send)

### AppBotEye
- **항상 떠있어야 함**: 일반 CLI 명령 실행 시 자동 spawn (`eye`/`slack`/`help`/`validate`/`win-move` 제외)
- **Eye = Slack 데몬 통합** → 별도 `slack listen` 불필요
- **eye tick**: one-shot 상태 조회 (ctx=N% 포함) / **eye**: FSW 하이브리드 루프
- **인수인계**: `wkappbot newchat "프롬프트"` — 새 채팅에 컨텍스트 요약 전달
- **크로 카드 금지!**: OpenClaw(크로)는 별도 서비스 — 수정 금지. 클롣 카드만 개선 OK
- **CWD 약식**: `W:\GitHub\WKAppBot` → `WG-WKAppBot` / 노이즈 필터: `NO_REPLY`, `ㄱㄱ`

### 빌드 & 배포
```bash
"W:/SDK/dotnet/dotnet" publish 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj' -c Release --verbosity minimal
```
- **Hot-Swap**: publish만 하면 Eye 자동 감지+교체. **Eye kill 절대 금지!**
- `.cs` 수정 후 별도 지시 없이 자동 publish

### 마이너 버전 bump
→ [VERSIONING.md](../VERSIONING.md) 참조 — 3개 파일 함께 커밋

### Source File Size
~400줄/파일 권장. **WKAppBot 코드에만 적용** (고객 코드 리팩터 금지!)

### 금지사항
- Eye 직접 spawn / 클롣 전달 차단 옵션 / Eye 안 띄우는 옵션 — 전부 금지
- 유저에게 프롬프트에서만 질문 금지 (슬랙 동시 발송 필수)

---

## Overview
Windows a11y 기반 앱 UI 자동화. UIA→Win32→SendInput 3티어 폴백, 포커스리스 제어.
`a11y.exe` = busybox symlink → `wkappbot.exe`

## Architecture

### 5티어 요소 탐색
UIA → Vision Cache → Simple OCR → Vision API(Claude) → 좌표 기반

### AppBotPipe / File Tools (v5.8)
모든 프로세스 생성은 `Spawn()` / `StartTracked()` 경유 — CreateProcessW 훅 보장
- `Spawn(showNoActivate:true)`: WPF 오버레이용 (WhisperRing/ScreenSaver) — 포커스 없이 창 표시
- `Spawn(default)`: `SW_HIDE` — 백그라운드 프로세스
- **FocusLaunchTracker**: 포커스 탈취 프로세스 추적 (`runtime/focus_launch.json`)
- **워치독 VBS**: Eye 2분+ 사망 시 발동 → wkappbot-core 전부 종료 → eye tick 재시작
- **File Tools**: `file read/write/edit/grep/glob/undo`는 tool-style alias(`--path`, `--content`, `--pattern`, `--dry-run`) 지원
- **file write**: patch 추적 대신 `.bak/` 백업 기본 생성, `a11y file-write`/MCP도 같은 백업 경로 사용
- **LG overlay guard**: `LGDisplayExtension` 단일 클래스 고정 탐지 대신 LG Smart Assistant 계열 topmost screen-cover 창을 프로세스+창 크기 기준으로 일반화 탐지

### Eye MCP Architecture (v4.9+)
Eye ↔ MCP 워커(Core) JSON-RPC over pipe. a11y/UIA를 별도 프로세스로 격리.
- `ShouldRouteToMcp()`: a11y/inspect/windows/ask → MCP, slack/eye/schedule → in-process
- `DETACHED_PROCESS` 플래그로 ConPTY LPC 교착 방지. 자동 재시작 max 5/5min
- Slack 파일 기반 큐 (`runtime/slack_queue/`), drain worker 직렬 처리
- **Launcher quiet-swap**: 런처는 `wkappbot-core.exe` 원본 경로 변경만 감시. `.new.exe` staging/rename는 Eye 책임.
- **Admin-first swap**: admin endpoint가 살아 있으면 normal core swap은 defer, admin 종료 뒤 새 stamp에서만 재시도.
- **Failed-stamp skip**: 한번 실패한 core `mtime` stamp는 더 새 파일이 올 때까지 재시도하지 않음.
- **CDP ask prompt pump**: triad/cross-prompt는 페이지별 singleton prompt pump를 사용. 청크는 append 후 1s idle에서 send하며, 페이지 key는 `scope + targetId + editorSelector`.
- **Attachment transaction lock**: CDP 첨부가 있으면 페이지 lock을 잡고 첨부를 먼저 올린다. lock 중 들어오는 청크는 queue에 쌓고, 업로드 완료 후 queue text를 append + immediate flush 한 뒤 unlock한다.

### Self-Healing DYN-A11Y
UIA 없는 MFC owner-drawn → CCA 세그멘테이션 → OCR 3중 교차검증 → Gemini Vision 추론
→ `dyn_r{row}c{col}` 동적 ID + Experience DB 캐시 + CCA 파라미터 자동 튜닝

### Sunset Screensaver + WhisperRing
별도 프로세스 (WPF 메모리 격리, 부모 PID 감시 → Eye 종료 시 자동 종료)

### 프로젝트 구조
```
csharp/src/
├── WKAppBot.CLI/     # CLI 진입점 + Commands/
├── WKAppBot.Core/    # ScenarioRunner, ActionExecutor
├── WKAppBot.Win32/   # NativeMethods, WindowFinder, UiaLocator, Input/*
├── WKAppBot.Vision/  # ChartAnalyzer, SimpleOcrAnalyzer, VisionCache
├── WKAppBot.WebBot/  # CdpClient, ChromeLauncher, SlackSocketClient
├── WKAppBot.Android/ # AdbClient, AndroidA11yTree
handlers/ scenarios/
```

---

## CLI 명령어
> **`<grap>`** = glob/regex/OR(`;`)/`#UIA스코프`/`{JSON5}` 멀티필드 AND

```
wkappbot a11y <action> <grap>[#scope] [options]   # ★ 표준 통합 (24 actions)
  inspect / find / windows / screenshot / ocr     # Discovery
  close / minimize / maximize / restore / focus / move / resize
  click / invoke / toggle / expand / collapse / select / scroll
  type [--hotkey] / set-value / set-range / read
  wait [--condition/--not] / clipboard[-read/-write]
  --eval-js "js"  --all  --nth N  --force  --timeout N  --speak
wkappbot a11y kill <pattern>[/<ancestor>]
wkappbot windows [grap] [--deep] [--process <name>] [--cmd <substr>]
wkappbot slack send "msg" [file.png]  /  reply "msg" --msg TS
wkappbot eye / eye tick
wkappbot newchat "prompt" [--file f.txt]
wkappbot ask gpt|gemini|claude|triad "question" [file.png]
wkappbot logcat [regex] [file.log ...] [--past Nh] [-f] [--timeout N] [--json]
wkappbot file read/grep/glob/edit <args>
wkappbot gc [pattern] [--days N] [--sweep]
wkappbot claude-usage                             # JSONL size + ctx%
wkappbot mcp                                     # MCP stdio server
wkappbot <cmd> --help / --regression
```

## grap 패턴
| 구문 | 예시 |
|------|------|
| 와일드카드 | `"*Button*"` |
| OR | `"*메모장*;*계산기*"` |
| #UIA스코프 | `"영웅문#실시간계좌"` |
| 탭포털 | `"Chrome#ChatGPT#모델"` |
| JSON5 | `{title:'Claude',domain:'claude.ai'}` |
| hwnd | `"*hwnd=001A0F2C*"` |
| adb | `"adb://Fold5/*heromts*#해외잔고"` |

---

## Key Design Decisions

### Focusless-First
UIA Invoke/Value/Toggle/Select = Focusless. SendInput/Hotkey = EnsureFocus 필요.
WPF 오버레이는 `Spawn(showNoActivate:true)` → SW_SHOWNOACTIVATE(4), 포커스 탈취 없음.

### PromptDeliveryContext
프롬프트 주입 전: ① 타겟 포그라운드? ② 최근 30s 입력?
→ `Focusless` / `FocusSteal` / `Skip` / `Abort` 자동 결정

### HTS 자동화
MFC 컨트롤: UIA 패턴 거의 없음 → Win32 메시지 폴백 필수. 영웅문 owner-drawn → OCR 폴백.

### 태그 규칙
`[WATCH]` `[RUN]` `[FOCUS]` `[VERIFY]` `[BLOCK]` `[GUARD]` `[ZOOM]` `[SLACK]` `[EXP]` `[KNOWHOW]`

---

## Session Management (Claude Code Tips)
- `wkappbot claude-usage` → JSONL 크기 + ctx% 표시
- **ctx% = JSONL ÷ ~40MB** — 80%에서 인수인계 준비, 90%에서 즉시 handoff
- **목표**: 세션당 ~10MB 이하. 15MB+ 부터 품질 저하 시작
- **Handoff**: `wkappbot newchat "프롬프트"` — 새 채팅에 작업 요약 전달
- **MEMORY.md**: 200줄 상한. 초과 시 세부내용 `memory/` topic 파일로 분리

---

## YAML Scenario (요약)
```yaml
scenario: { name: "Test" }
app: { launch: "calc.exe", wait_for_window: { title_contains: "계산기" } }
steps:
  - { name: "Click", target: { automation_id: "plusButton" }, action: click }
  - { name: "Verify", target: { automation_id: "CalculatorResults" }, action: assert,
      params: { type: text_contains, expected: "42" } }
```
지원 액션: click/double_click/right_click/type_text/press_key/hotkey/wait/assert/scroll/screenshot/toggle/expand/collapse/select/set_range/window_close/minimize/maximize

## 배포 구조
```
W:/SDK/bin/wkappbot.exe / a11y.exe / wkappbot.hq/
```

## 참고
- **MEMORY.md** / **memory/**: 빌드 명령, 아키텍처 결정, gotchas 상세
- .NET 8.0 `net8.0-windows10.0.22621.0`, 한국어 UI 지원
