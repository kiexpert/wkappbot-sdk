# WKAppBot v5.13.0 - Windows + Android App Automation Test Framework

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

### 스킬 작성 규칙
- **스킬 생성/수정은 반드시 `wkappbot skill contribute` / `wkappbot skill edit` 사용**
- `.skill.json` 파일을 Edit/Write/wkedit 도구로 직접 수정 **절대 금지**
  - 이유: `|`가 step 구분자라 내용이 쪼개짐, 버전 bump 누락, 인코딩 오염
  - step 내용에 `|` 사용 금지 — 줄바꿈(newline) 대신 사용
- 스킬 생성 후 반드시 `wkappbot skill show <id>` 로 결과 검증

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

### v5.11.101+ (2026-04-08, Session 7)
- **# TARGET 출력 완전 재설계** (copy-paste-ready grap):
  - `# TARGET "{compactGrap}#{absTagPath}" [OK] Nms  WindowTitle`
  - `BuildAbsoluteTagPath`: UIA type 3자 약칭 (`Document→Doc`, `Group→Gro`, `Edit→Edi`)
  - `BuildCompactWinGrap`: title/url 제거, 브라우저→domain(no cls), 나머지→cls 유지
  - `[OK]/[MISS]` 검증 + 타이밍 ms 표시
  - window title → meta suffix (element Name 아닌 Win32 GetWindowTextW)
  - hack-hover 동일 포맷 적용 + `BuildCompactWinGrap` 공통 함수
- **FormatNodeLabel 통일**: AutomationElement 오버로드 → string 오버로드 완전 위임
  - name에 단따옴표 래핑 `<Button'확인'>`, 17자 truncation 제거
  - UIA scope 접근 가능 시 TARGET 태그 오른쪽에 `→ #scope` 힌트
  - rect `ltwh=` attr 포함; VERDICT ✓ 억제 (⚠/? 만 출력)
- **KNOWHOW/ShowKnowhowBroadcast/ShowKnowhowHint** → 전부 stderr
- **UiaLocator.GetAbsoluteTagPath()** 추가 (automation tree walker 노출)

### v5.11.74~100 (2026-04-08, Session 6)
- **stdout noise elimination**: 124 Commands/*.cs 파일 `Console.WriteLine($"[` → stderr 일괄 치환 (wkedit bulk)
- **ClickZoomHelper**: `[ZOOM:]` Console.Write → Console.Error.Write (전부 stderr)
- **a11y find 출력 재구성**:
  - 첫 줄 `# FOCUS {json5}` + `# TARGET {json5}` stdout (복붙용)
  - CURSOR 섹션: 연속 동일 unnamed 노드 run-length encode (`<Group> x7`)
  - TARGET 섹션: 헤더 grap 중복 제거
- **BuildTargetJson5 필드 순서**: `hwnd,pid,proc,domain,title,...` (proc/domain 앞으로)
- **ErrorScope**: error-pattern 즉시 passthrough, ErrorDetected 플래그, exit-0+error → -9999
- **스킬 추가**: `grap` (10-step UI 주소체계 가이드), `wkedit` v1.1 (bulk transaction edit)
- **스킬 규칙**: `.skill.json` 직접 수정 금지 — `skill contribute/edit` 명령 필수

### v5.11 신규 (2026-04-05)
- **TargetAmbiguityGuard 6-Layer**: AI를 위한 타겟 모호성 감지 + 자동 안내 시스템
  - L1: 모호 패턴 다중 윈도우 매칭 → 목록 + `[OK]` 검증 + hwnd 오토힐
  - L2: 모달 알러트 차단 → 알러트 내부 auto-find (UIA + Win32 버튼)
  - L3: FOCUSED/LEAF 말단 노드 → 모든 명령에서 grap 타겟 안내
  - L4: Win32 자식창 미발견 → 형제 윈도우 리스팅 (MFC/HTS)
  - L5: #scope 없는 element action → 루트 자식 + 포커스 체인
  - L6: UIA scope 요소 미발견 → 사용 가능 요소 리스팅
- **CDP fixed delay → state-based polling**: 삼두 합의 구현
  - `WaitForWindowStateAsync` 공용 헬퍼 (50ms 간격 windowState 폴링)
  - SetWindowBoundsAsync, EnsureMinimizedAsync, SwitchToTargetAsync, EnsureCorrectWindowAsync 전부 교체
  - WebOpenCommand: Thread.Sleep(1-2s) → document.readyState 폴링
- **BuildTargetJson5 개선**: title 50자 truncate, 말줄임표 제거 (substring 매칭 보장)

### v5.11.37~73 (2026-04-06, Session 5)
- **hack-hover 대폭 개선**:
  - 라이브 오버레이: 루트 창 크기, UIA 전체 descendant 점선 표시
  - 타겟 부모 체인 2배 두께 네온 점선 강조
  - blind 윈도우(UIA 없음/경험DB 없음) 즉시 핵 발동 (취소 불가)
  - 경험DB FSW: 파일 변경 즉시 오버레이 갱신
  - 핫스왑: TryRenameSwap으로 .new.exe 감지 → Launcher 재spawn
  - 콘솔: 매 틱 출력, Eye: 변경+분석만 (RunningInEye 자동 판단)
  - 스크린리더 자동 활성화 (Chromium UIA 트리 보강)
- **hack-input 워커**: 키보드 포커스 분석 (UIA GetFocusedElementInfo)
- **TypeViaIme**: IMM32 focusless 텍스트 입력 Tier 2 통합
  - AttachThreadInput + ImmSetCompositionString + CPS_COMPLETE
  - 상태 백업/검증/복구 (실패 시 이전 composition 원복)
- **auto-hack 프로세스 격리**: Eye 내 A11yHackCommand → AppBotPipe.Spawn (메모리 누수 방지)
- **CDP EvalAsync**: 2회→3회 retry + 500ms/1000ms exponential backoff
- **pump watchdog**: 3s→5s + 2차 감소 확인 (오탐 방지)
- **오버레이 개선**: 반투명 필 전면 제거, Known/Cached 알파 10%, atomic swap 렌더
- **CommandHelpMap**: speak, hack, hack-hover, hack-input help 등록

### v5.10 (2026-04-04)
- **Eye 파이프 100ms 타임아웃**: `firstOutputTimeoutMs` — slack/ask/newchat 제외 대부분 명령에 적용. Eye 느리면 Core로 자동 전환
- **TryRenameSwap 공용 함수**: Eye 메인루프 + 스타트업 gentle-swap + `wkappbot hotswap` CLI — 동일 로직 공유
- **OcrCorrectionDb**: pixel-hash → 정답 매핑 자기학습 사전 (WKAppBot.Vision). UIA 검증 자동 학습, OCR 결과 자동 보정
- **suggest show/get/view**: 번호/ts로 전체 내용 조회. 알 수 없는 서브커맨드 가드 (노이즈 방지)
- **suggest resolve 안전장치**: 중복가드 backup전환, .manifest 삭제감지+자동복구, 실패시 evidence 보존
- **CDP Input.* minimize 제거**: CDP 렌더러 직접 전달 → minimize 불필요. IsFocusStealingMethod만 minimize
- **Runtime.enable 재시도**: EnableRuntimeWithRetry (최대 4회 exponential backoff)
- **로그 라우팅 수정**: Eye 파이프 명령 → 명령별 `old {cmd}/` 폴더. 오타 → `old unknown/`
- **auto-hack 로그 노이즈 제거**: WKAPPBOT_WORKER=1로 TeeWriter 억제
- **인코딩 오염 감시**: file edit 후 U+FFFD 감지 + Slack 자동 알림
- **a11y hack 우상단 노드명**: UIA AutomationId/Name을 세그먼트 내부 top-right에 표시

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
| JSON5 | `{proc:'chrome',domain:'chatgpt.com',title:'Claude'}` |
| hwnd 직접 | `{hwnd:0x010B084A}` |
| adb | `"adb://Fold5/*heromts*#해외잔고"` |

→ 자세한 grap 문법: `wkappbot skill show grap`
→ `a11y find <grap>` stdout 첫 줄 `# TARGET {...}` 복붙하면 바로 사용 가능
→ **Accumulated knowhow**: `wkappbot skill list` — search skills first when stuck, then ask triad

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
- **ctx% = JSONL ÷ ~20MB** — 8MB에서 인수인계 준비, 10MB에서 즉시 handoff
- **목표**: 세션당 ~10MB 이하. 도전적 토큰 최적화!
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
