# WKAppBot - Windows App Automation Test Framework

## 동료 클롯을 위한 운영 규칙 (필독!)

### 질문/소통
- **유저에게 질문할 때**: 반드시 **Slack과 Claude Code 여기에 동시에** 전달할 것
  - `wkappbot slack send "질문 내용"` + 여기(프롬프트)에도 출력
  - 슬랙만 보내거나 여기에만 물어보면 안 됨!
- **작업 중 상황 공유**: 슬랙에도 현황을 알려줘야 유저가 모바일에서도 확인 가능

### 앱봇의 눈 (AppBotEye)
- **항상 떠있어야 함**: 모든 CLI 명령 실행 시 AppBotEye 자동 spawn
- `eye`, `slack`, `help`, `validate` 제외하고 모든 명령에서 자동 실행
- AppBotEye = Slack 데몬 통합 → 별도 `slack listen` 불필요

### Slack + 프롬프트 전달
- Slack 수신 메시지는 **항상** Claude 프롬프트에 전달 (옵션 없음)
- 키워드 감시("클롯", "claude", "appbot" 등)도 **항상** 활성
- `--prompt`, `--keywords`, `--no-slack` 같은 옵션은 **존재하지 않음**
- 프롬프트 입력 실패 시: 전경 윈도우 진단 + Slack에 상황 공유 (계획승인/권한창 가능성)

### 빌드 & 배포
- 코드 변경 후 **반드시** `dotnet publish` + AppBotEye 재시작까지 완료할 것
- 빌드 명령: `"$DOTNET_ROOT/dotnet" publish ... --verbosity quiet` (MEMORY.md 참조)
- 기존 wkappbot 프로세스 kill → publish → `wkappbot eye &`로 재시작

### 문서화 (토큰 안전)
- **중요 발견/전략은 반드시 CLAUDE.md + 소스 주석에 기록** (컨텍스트 유실 대비!)
- 다음 세션의 클롯이 처음부터 다시 삽질하지 않게 노하우를 남겨야 함
- `knowhow.md`도 적극 활용 (컨트롤별 자동화 노하우)

### 에러 대응
- 방해꾼 창, 프롬프트 유실 등 예외 상황은 항상 **스냅샷(wkappbot snapshot) + Slack 공유**
- 조용히 실패하지 말고 반드시 진단 결과를 남길 것

### Slack 메시지 스타일
- 상태 변화는 `chat.update`로 동일 메시지 수정 (채널 스팸 방지)
- 새 이벤트/에러는 새 메시지로 전송

### HTS 자동화 핵심
- MFC 컨트롤은 UIA 패턴이 거의 없음 → Win32 메시지(BM_CLICK, WM_LBUTTON 등) 폴백 필수
- SmartClickButton 3티어: BM_CLICK → WM_LBUTTON → SendInput
- 영웅문은 owner-drawn → GetWindowText 빈 문자열 → OCR 폴백

### 금지 사항
- 클롯에게 전달을 차단하는 옵션 절대 만들지 말 것
- 앱봇의 눈을 안 띄우는 옵션 만들지 말 것
- 유저에게 여기(프롬프트)에서만 질문하지 말 것 (반드시 슬랙 동시 발송)

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
│       │   │   ├── ActionState.cs       # IPC 상태 모델 — JSON 파일로 AppBotEye와 공유
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
│       │   │   └── UiaLocator.cs        # FlaUI UIA 요소 탐색/조작 + PatternMatcher + 9패턴 풀 지원
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
│       ├── WKAppBot.WebBot/           # WebBot — CDP (Chrome DevTools Protocol) 웹 자동화
│       │   ├── CdpClient.cs           # CDP WebSocket JSON-RPC 클라이언트 (zero deps)
│       │   ├── ChromeLauncher.cs      # Chrome 자동 탐색 + --remote-debugging-port 실행
│       │   └── SlackSocketClient.cs   # Slack Socket Mode WebSocket 클라이언트 (zero deps)
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
- 시스템 환경변수 `DOTNET_ROOT` 등록됨 → `$DOTNET_ROOT/dotnet` 사용

### 빌드 (bash)
```bash
"$DOTNET_ROOT/dotnet" build 'W:/GitHub/WKAppBot/csharp/WKAppBot.sln' -c Release --verbosity quiet
```

### 빌드 (PowerShell)
```powershell
& "$env:DOTNET_ROOT\dotnet.exe" build 'W:\GitHub\WKAppBot\csharp\WKAppBot.sln' -c Release --verbosity quiet
```

### 실행 (4칙연산 테스트 — bash)
```bash
wkappbot run 'W:/GitHub/WKAppBot/scenarios/calc_four_ops.yaml' -v
```

### 실행 (PowerShell)
```powershell
wkappbot run 'W:\GitHub\WKAppBot\scenarios\calc_four_ops.yaml' -v
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
wkappbot web <subcommand> [options]   # WebBot CDP 웹 자동화
wkappbot slack <subcommand>           # Slack Socket Mode 양방향 메시징
wkappbot knowhow <subcommand>        # 컨트롤별 자동화 노하우 기록/읽기
wkappbot eye [--port N] [--interval N] [--size WxH] [--pos X,Y]  # WK AppBot Eye (Slack+Prompt 항상 ON)
wkappbot eye --app "투혼" [--interval N] [--size WxH] [--pos X,Y]  # 앱봇의 눈 (일반 앱 UIA 추적 모드)
wkappbot schedule <subcommand>       # 스케줄 관리 (자동 복구, 예약 프롬프트)
wkappbot snapshot <window-title> [--tag <name>] [--depth N]  # UIA+스크린샷+OCR 원샷 캡처
```

### knowhow 명령 (컨트롤별 자동화 노하우)
```bash
# Win32 컨트롤 노하우 기록
wkappbot knowhow write <form-id> "lesson" [--cid N] [--category "..."]
wkappbot knowhow read <form-id> [--cid N]

# 웹 컨트롤 노하우 기록
wkappbot knowhow web <domain> "lesson" [--selector "..."] [--category "..."]
wkappbot knowhow web-read <domain> [--selector "..."]
wkappbot knowhow web-list
```
- knowhow.md = "클롯→미래 클롯" 노트 (한번 실수한 건 다시 안 한다!)
- Win32: 각 컨트롤 폴더에 knowhow.md append (form-level + control-level)
- Web: `wkappbot.hq/web_profiles/{domain}/knowhow.md` + 요소별 하위 폴더
- SmartClickButton 전략 폴백 시 자동 기록 (왜 실패했는지)

### slack 명령 (Socket Mode 양방향 메시징)
```bash
# Slack 연결 테스트 (auth + send + socket)
wkappbot slack test

# Socket Mode 리스너 시작 (@mention에 자동 응답)
wkappbot slack listen
wkappbot slack listen --bg                        # 백그라운드 데몬 (프롬프트 전달 + 키워드 항상 ON)
wkappbot slack listen --webbot                    # + WebBot 현황 스트리밍 (Claude busy 시)

# 메시지 전송/답장
wkappbot slack send "Hello from WKAppBot!"
wkappbot slack reply "response text"            # 최근 스레드에 답장 (컨텍스트 자동)

# 파일 업로드/스크린샷 전송
wkappbot slack upload <file> [--thread TS] [--title "name"] [--comment "msg"]
wkappbot slack screenshot [window-title] [--thread TS]

# 기타
wkappbot slack status                           # 리스너 상태 확인
wkappbot slack stop                             # 백그라운드 리스너 중지
wkappbot slack catch-up [--prompt]              # 놓친 메시지 따라잡기
```
- Socket Mode: 서버 없이 WebSocket으로 Slack과 양방향 통신
- @mention 이벤트 수신 → 자동 응답 (listen 모드)
- chat.postMessage API로 메시지 전송
- files:write 스코프로 파일/스크린샷 업로드 (v2 3단계 API)
- Prompt 전달 + 키워드 감시: 항상 ON (옵션 불필요)
- 설정: `wkappbot.hq/profiles/slack_exp/webhook.json`

### web 명령 (Chrome DevTools Protocol 웹 자동화)
```bash
# Chrome 실행 + CDP 연결 + 페이지 열기
wkappbot web open https://example.com
wkappbot web open https://example.com --port 9222 --headless

# 이미 실행 중인 Chrome에 연결만 (실행하지 않음)
wkappbot web open https://example.com --no-launch

# 페이지 조작
wkappbot web navigate https://example.com
wkappbot web click "#btn-login"
wkappbot web type "#search" "hello world"
wkappbot web text "#result"
wkappbot web check "#chk-agree" true
wkappbot web select "#country" "Korea"
wkappbot web wait "#loading" --timeout 10000

# JavaScript 실행
wkappbot web eval "document.title"
wkappbot web eval "document.querySelectorAll('a').length"

# 출력
wkappbot web screenshot -o page.png
wkappbot web html -o page.html
wkappbot web url
wkappbot web title

# 상태 확인 (탭 목록)
wkappbot web status

# 배치 실행 (텍스트 파일에서 커맨드 순차 실행)
wkappbot web run steps.txt --delay 300
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

### schedule 명령 (예약 프롬프트 + 자동 복구)
```bash
# 시간 지정 1회 실행
wkappbot schedule add --at "16:00" --prompt "작업 재개"
wkappbot schedule add --at "09:30" --prompt-file ./recovery.md

# rate limit 리셋 시 자동 실행
wkappbot schedule add --on-limit-reset --prompt "CLAUDE.md 읽고 작업 재개"

# 반복 실행 (interval: Nm=분, Nh=시간)
wkappbot schedule add --every 30m --prompt "slack catch-up 확인" --max-runs 3

# 목록/삭제/전체삭제
wkappbot schedule list
wkappbot schedule remove <id>
wkappbot schedule clear [--pending]
```
- 스케줄 파일: `wkappbot.hq/runtime/schedule.json`
- AppBotEye 폴링 루프에서 ~10초마다 체크 → 시간 도래 시 Claude 프롬프트에 자동 입력
- `on_limit_reset`: rate limit 해제 감지 시 자동 실행 (AppBotEye UIA 감지 연동)
- `--notify-slack` (기본 true): 실행 시 Slack 알림
- PC 재부팅 대응: AppBotEye 시작 시 과거 pending 항목 즉시 실행

### snapshot 명령 (진단용 원샷 캡처)
```bash
# 기본 사용 (UIA 트리 + 스크린샷 + OCR + info.json)
wkappbot snapshot "Claude" --tag rate_limit --depth 8

# 간단 캡처
wkappbot snapshot "영웅문" --tag hts --depth 5
```
- 저장: `wkappbot.hq/output/snapshots/{tag}_{yyyyMMdd_HHmmss}/`
- 4개 파일: `uia_tree.txt`, `screenshot.png`, `ocr.txt`, `info.json`
- 용도: rate limit 등 특수 상태에서 UIA 구조 기록 → 탐지 로직 보완

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
| Toggle (UIA Toggle) | ✅ YES | `TryToggle()` / `TrySetToggle()` — 포커스 불필요! |
| Expand/Collapse (UIA) | ✅ YES | `TryExpand()` / `TryCollapse()` — 포커스 불필요! |
| Select (UIA SelectionItem) | ✅ YES | `TrySelect()` — 포커스 불필요! |
| Scroll (UIA Scroll) | ✅ YES | `TryScroll()` — 포커스 불필요! (SendInput 폴백) |
| SetRange (UIA RangeValue) | ✅ YES | `TrySetRangeValue()` — 포커스 불필요! |
| Window Close/Min/Max | ✅ YES | `TryWindowClose()` / `TrySetWindowState()` — 포커스 불필요! |
| PressKey/Hotkey | ❌ NO | SendInput → EnsureFocus |
| Scroll (SendInput) | ❌ NO | MouseInput.Scroll → EnsureFocus |
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
| `scroll` | 스크롤 (UIA Scroll 우선, SendInput 폴백) | automation_id/name | direction, amount |
| `screenshot` | 스크린샷 저장 (focusless — PrintWindow) | - | filename |
| `toggle` | 체크박스/토글 (UIA Toggle, focusless!) | automation_id/name | checked (bool) |
| `expand` | 트리/콤보 확장 (UIA ExpandCollapse, focusless!) | automation_id/name | - |
| `collapse` | 트리/콤보 축소 (UIA ExpandCollapse, focusless!) | automation_id/name | - |
| `select` | 리스트/탭 선택 (UIA SelectionItem, focusless!) | automation_id/name | item_text |
| `set_range` | 슬라이더 값 설정 (UIA RangeValue, focusless!) | automation_id/name | value (double) |
| `window_close` | 윈도우 닫기 (UIA Window, focusless!) | automation_id/name | - |
| `window_minimize` | 윈도우 최소화 (UIA Window, focusless!) | automation_id/name | - |
| `window_maximize` | 윈도우 최대화 (UIA Window, focusless!) | automation_id/name | - |

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
- **Web UI 자동화 완료**: Chrome UIA Accessibility로 웹페이지 자동화 (Phase 1: 21/21 PASS, Phase 2: 19/19 PASS)
- **WebBot (Phase 11A) 완료**: CDP 기반 웹 자동화 — `wkappbot web` CLI (27/27 배치 테스트 PASS)
- **윈도우 스타일 특성 완료**: GWL_STYLE/GWL_EXSTYLE 수집 → ControlExperience 저장 + info.json per tree folder
- **Slack Socket Mode 완료**: 양방향 메시징 — WebSocket으로 @mention 수신 + chat.postMessage 전송 (zero deps)
- **Slack 파일 업로드 완료**: files:write 스코프 + v2 3단계 API (getUploadURL → PUT → complete)
- **Slack→Claude 프롬프트 브릿지 완료**: 슬랙 메시지를 Claude Code 프롬프트에 직접 입력 (항상 ON)
- **키워드 감시 완료**: "클롯/claude/appbot" 등 키워드 감지 → 자동 전달 (항상 ON)
- **웹봇 Slack API 자동화 완료**: CDP로 api.slack.com OAuth 페이지에서 스코프 추가 + 앱 재설치 자동 수행
- **HandlePromptLost 완료**: 프롬프트 유실 시 전경 윈도우 스크린샷 → Slack 업로드 (원격 진단)
- **knowhow.md 완료**: 컨트롤별 자동화 노하우 기록 (Win32 + Web) — CLI + SmartClickButton 자동 기록
- **AppBotEye 완료**: Claude Desktop 우상단 오버레이 — WebBot 라이브 스크린샷 + 콘텐츠 표시 (cloaking/hot-reload)
- **AppBotEye App Mode 완료**: `--app` 플래그로 일반 Windows 앱 UIA 요소 실시간 추적 (PrintWindow + 커서 기반 요소 탐지)
- **ActionState IPC 완료**: JSON 파일 기반 프로세스 간 상태 공유 — 모든 CLI 명령이 "마지막 관심사"를 기록, AppBotEye가 읽기
- **AppBotEye 통합 모드 완료**: ActionState 최우선 표시 + 커서 기반 폴백 + Claude Desktop UIA 상태 감지 (계획승인/실행중/대기)
- **ActionState CLI Write 완료**: do/dismiss/input/web/scan 모든 CLI 명령에서 ActionState.Write() 호출
- **Slack AppBotEye 통합 완료**: `--slack` 플래그로 AppBotEye 프로세스 내 Slack Socket Mode 리스너 동시 실행
- **Claude 상태 자동 표시 완료**: AppBotEye fallback 시 Claude Desktop UIA 상태를 자동 탐지하여 표시
- **Slack WebBot 스트리밍 완료**: --webbot 모드로 Claude busy 시 WebBot 현황을 Slack chat.update로 실시간 갱신
- **Claude busy 감지 완료**: UIA "중단" 버튼 탐지 + StatusBar 텍스트로 현재 작업 상태 추출
- **UIA 웹 콘텐츠 추출 완료**: --force-renderer-accessibility로 Chrome UIA 트리에서 웹 페이지 텍스트 직접 읽기 (CDP JS 대체)
- **UIA 패턴 풀 지원 완료**: 8→18 액션, 6개 새 패턴 (Toggle/ExpandCollapse/SelectionItem/Scroll/RangeValue/Window) — 전부 Focusless!
- **inspect 패턴 표시 완료**: DumpTree에 17개 UIA 패턴 감지 + 출력 (GetSupportedPatterns)
- **Rate Limit 탐지 완료**: Claude Desktop UIA 텍스트에서 "hit your limit" 감지 + 리셋 시간 파싱 + ActionState 기록 + Slack 알림
- **Schedule 시스템 완료**: JSON 기반 예약 프롬프트 (once/on_limit_reset/recurring) + CLI + AppBotEye 자동 실행기
- **Snapshot 명령 완료**: `wkappbot snapshot` — UIA tree + 스크린샷 + OCR + info.json 원샷 진단 캡처
- **MiniView→AppBotEye 리네이밍 완료**: 모든 파일/클래스/CLI명령/로그태그 통일 (MiniView→AppBotEye)
- **Slack Block Kit 버튼 완료**: 플랜 승인 시 [수락]/[거절] 인터랙티브 버튼 → Socket Mode로 block_actions 수신 → UIA Invoke로 Claude 승인 클릭
- **response_url 버튼 비활성 완료**: 버튼 클릭 후 원본 메시지를 결과 텍스트로 교체 (버튼 제거)
- **Slack+Prompt 항상 ON 완료**: `--slack`/`--no-slack`/`--prompt`/`--keywords` 옵션 전부 제거 → 항상 활성
- **WK AppBot Eye 브랜딩 완료**: 윈도우 타이틀 + 헤더 "WK AppBot Eye"로 통일
- **Slack 상태 스트리밍 완료**: Claude 상태 변화를 chat.update로 동일 메시지 수정 (스팸 방지)
- **키움 OpenAPI+ 설치 완료**: `C:\OpenAPI\KHOpenAPI.ocx` 32비트 ActiveX + COM 등록 확인
- **ComInspect 도구 완료**: `tools/ComInspect.cs` — OCX TypeLib에서 메서드/이벤트 자동 열거 (32비트 .NET Framework)
- **키움 COM TypeLib 분석 완료**: 58 methods + 9 events 자동 열거 → `C:\OpenAPI\khopenapi_typelib.md`
- **옵션 대정리 완료**: `--prompt`/`--keywords`/`--no-slack` 전부 삭제 → Slack+프롬프트+키워드 항상 ON (옵션 없음)
- **AppBotEye 자동 실행 완료**: 모든 CLI 명령 실행 시 AppBotEye 자동 spawn (eye/slack/help/validate 제외)
- **프롬프트 실패 진단 완료**: FindPrompt 실패 시 전경 윈도우 진단 + Slack 상황 공유 (계획승인/권한창 가능성 안내)
- **Rate Limit 자기감지 루프 수정 완료**: AppBotEye 오버레이 텍스트가 UIA에 노출 → 자기 텍스트를 rate limit으로 오인하는 무한 루프 → RootWebArea 스코프 제한 + 엄격한 패턴 매칭으로 해결
- **Rate Limit ParseResetTime 수정 완료**: 리셋 시간이 지나면 다음날로 롤오버하는 버그 제거 + null 상태 캐시 잔류 수정
- **Slack 봇 이름 "클롯" 완료**: BotUsername "클봇" → "클롯"으로 변경 (chat:write.customize 스코프)
- **Slack 스레드 맥락 전달 완료**: 프롬프트 전달 시 [쓰레드 시작] + [직전 메시지/클롯 응답] 맥락 포함 (GetThreadContext 연동)
- **Slack "전달했습니다" 자동 삭제 완료**: OnSelfMessage 이벤트 + pending_acks.json 파일 IPC → 봇 응답 시 ack 메시지 자동 삭제
- **키움 프록시봇 Phase A 완료**: 32비트 프록시 EXE + COM 호스팅 + Named Pipe 서버 + 이벤트 싱크 (KiwoomProxy)
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

### Phase 10: logcat — "윈도우 변화 실시간 스트리밍"
**목표**: adb logcat처럼 프로세스/시스템 윈도우 이벤트 실시간 모니터링
**상태**: 로드맵 등록, 구현 없음
- CLI: `wkappbot logcat [process-name] [--filter <keyword>] [--interval N]`
- **감지 대상**: 윈도우 생성/소멸, 텍스트 변경, 포커스 변경, MDI 자식 추가/제거, 크기/위치 변경
- **구현 전략 (2단계)**:
  - Phase A: EnumWindows 폴링 diff (500ms~1s) — 간단, watch 코드 재활용, CPU 거의 0
  - Phase B: SetWinEventHook 콜백 — OS가 알려줌, 오버헤드 0, P/Invoke 콜백 관리 필요
- **출력 형식**: `[LOGCAT] timestamp +/-/~ type target detail`
  ```
  [LOGCAT] 14:30:01.234 +WINDOW #32770 "확인" pid=1234 (nkrunlite) 320x200
  [LOGCAT] 14:30:01.500 ~TEXT   AfxWnd140_3930 "5,230" → "5,245" (form_1301)
  [LOGCAT] 14:30:02.100 FOCUS  → nkrunlite/#32770 "매매확인"
  [LOGCAT] 14:30:03.200 -WINDOW #32770 "확인" (dismissed)
  ```
- **ExperienceDb 연동**: 텍스트 변경 → text_history.jsonl 자동 기록

### Phase 11A: WebBot CDP — "싱글톤 웹봇 시스템" ✅
**상태**: 완료
- **접근법**: CefSharp 대신 CDP (Chrome DevTools Protocol) 채택 — zero dependency, 기존 Chrome 재활용
- **CdpClient.cs**: System.Net.WebSockets 기반 WebSocket JSON-RPC 클라이언트
  - ConnectAsync, NavigateAsync, EvalAsync, ClickAsync, TypeAsync, GetTextAsync
  - GetValueAsync, SetCheckedAsync, SelectAsync, ScreenshotAsync, WaitForElementAsync
  - 백그라운드 receive loop + ConcurrentDictionary pending 응답 추적
- **ChromeLauncher.cs**: Chrome 자동 탐색 (ProgramFiles/LocalAppData/PATH) + `--remote-debugging-port` 실행
  - temp user-data-dir로 격리 (기존 Chrome 세션 간섭 없음)
  - 이미 실행 중이면 재사용 (싱글톤!)
- **CLI**: `wkappbot web <subcommand>` — open/navigate/click/type/text/eval/screenshot/check/select/wait/status/run
- **배치 모드**: `wkappbot web run steps.txt` — 텍스트 파일에서 커맨드 순차 실행
- **싱글톤 특성**: Chrome 프로세스는 앱봇 종료 후에도 살아있음 → 다음 실행 시 재연결
- **검증**: 27/27 배치 테스트 PASS (버튼, 입력, 체크박스, 셀렉트, 카운터, 네비게이션, JS eval, 스크린샷)

### Phase 12: Slack Socket Mode — "서버 없는 양방향 메시징" ✅
**상태**: 완료
- **SlackSocketClient.cs**: System.Net.WebSockets 기반 Slack Socket Mode 클라이언트 (zero deps)
  - `apps.connections.open` → WebSocket URL → connect → event receive → acknowledge
  - Events: `OnMessage` (채널 메시지), `OnMention` (봇 @mention)
  - `SendAsync()`: `chat.postMessage` API로 메시지 전송
  - `SlackUploadFileAsync()`: files:write v2 3단계 API (getUploadURL → PUT → complete)
  - 자기 메시지 필터링 (`auth.test` → botUserId)
- **SlackCommands.cs**: `wkappbot slack` CLI (partial class Program 패턴)
  - `listen --bg --prompt --keywords`: 백그라운드 데몬 (프롬프트 전달 + 키워드 감시)
  - `send "msg"`: 설정된 채널에 메시지 전송
  - `reply "msg"`: 최근 스레드에 답장 (컨텍스트 자동)
  - `upload <file>`: 파일 업로드 (files:write v2 API)
  - `screenshot [title]`: 윈도우 캡처 → Slack 업로드
  - `catch-up [--prompt]`: 놓친 메시지 따라잡기
  - `test`: auth.test + send + Socket Mode 연결 검증
- **HandlePromptLost()**: FindPrompt() 실패 시 전경 윈도우 캡처 → Slack 스크린샷 업로드
  - GetForegroundWindow() → PrintWindow → IsBlankBitmap 검증 → SlackUploadFileAsync
  - 승인 다이얼로그 등이 Claude 프롬프트를 가릴 때 원격 진단 가능
  - 3곳에서 호출: OnMention, thread reply, keyword handler
- **설정**: `wkappbot.hq/profiles/slack_exp/webhook.json` (tokens, channel, scopes)
- **프로토콜**: Socket Mode (WebSocket from client TO Slack, no public server needed)
- **토큰**: App-Level Token (`xapp-`) + Bot User OAuth Token (`xoxb-`)
- **이벤트**: `app_mention`, `message.channels`
- **스코프**: incoming-webhook, chat:write, channels:read, channels:history, app_mentions:read, files:write + connections:write (app-level)
- **`[SLACK]` 태그**: 모든 Slack 관련 출력에 prefix

### WebBot Slack API 자동화 노하우 — "클봇이 직접 설정한다"
**상태**: 완료 (files:write 스코프 추가 + 앱 재설치를 CDP로 자동 수행)
- **대상**: `https://api.slack.com/apps/{APP_ID}/oauth` (OAuth & Permissions 페이지)
- **ReactVirtualized 드롭다운 대응**:
  - Slack의 스코프 선택기는 ReactVirtualized Grid 사용 → DOM에 ~15개만 존재 (총 50+)
  - `document.querySelector('.p-app-scopes-picker__option')` 등으로 바로 탐색 불가
  - **해결**: `.ReactVirtualized__Grid` 찾기 → `grid.scrollTop = N` 으로 알파벳순 위치로 스크롤 → 보이는 DOM에서 탐색
  - `files:write`는 'f' 섹션 → `scrollTop ≈ 800` 정도
- **React 입력 필드**:
  - `input.value = "text"` 안 먹힘 (React controlled component)
  - **해결**: CDP `wkappbot web type` 사용 (Runtime.evaluate로 focus → Input.dispatchKeyEvent)
  - 또는 nativeInputValueSetter 패턴: `Object.getOwnPropertyDescriptor(HTMLInputElement.prototype, 'value').set.call(el, val)`
- **JS 클릭 패턴**: `.click()` 안 될 때 → `dispatchEvent(new MouseEvent('click', {bubbles: true, cancelable: true}))` 사용
- **OAuth 재설치 플로우**:
  1. 스코프 추가 후 노란 배너 "reinstall your app" 링크 클릭
  2. 채널 선택: `.c-channel_entity__name` span에 mousedown+mouseup+click
  3. "허용" 버튼 클릭
  4. **bot_token은 재설치 후에도 동일** (webhook_url만 변경될 수 있음)

### AppBotEye — "앱봇의 눈" 라이브 오버레이 ✅
**상태**: 완료 (통합 모드 + Slack 데몬 통합 포함)
- Claude Desktop 우상단에 자동 배치되는 반투명 오버레이 윈도우
- **통합 모드** (기본, 플래그 없이 `wkappbot eye`):
  - ActionState IPC를 최우선 읽어 앱봇의 마지막 관심사 표시 (UIA 정보 + 액션 이름)
  - ActionState stale(>30초) 또는 없으면 커서 기반 UIA 추적으로 폴백
  - Claude Desktop UIA 상태 감지: 실행중(중단 버튼)/계획승인 대기/프롬프트 입력 대기
  - Fallback 모드에서 "(앱봇 대기 중)" 대신 "Claude: 실행 중" / "Claude: 계획승인 대기" / "Claude: 프롬프트 입력 대기" 자동 표시
  - **모든 CLI 명령에서 AppBotEye 자동 실행** (eye/slack/help/validate 제외, 블랙리스트 방식)
- **Slack 기본 ON** (`--no-slack`으로 끄기): Slack Socket Mode 리스너 통합 (별도 프로세스 없이 AppBotEye 안에서 Slack 양방향 통신)
  - `SetupSlackEventHandlers()`로 OnMention/OnMessage 이벤트 핸들러 등록
  - AppBotEye + Slack 데몬이 하나의 프로세스에서 동시 실행
- **Block Kit 인터랙티브 버튼**: 플랜 승인 시 [수락]/[거절] 버튼 Slack에 전송
  - Socket Mode `type: "interactive"` → `block_actions` 페이로드 수신
  - `OnBlockAction` 이벤트: action_id, value, userId, responseUrl 파싱
  - 수락 → `ClickApproveButton()` UIA Invoke, 거절 → `TypePlanFeedback()` 입력
  - `response_url` POST로 원본 메시지 교체 (버튼 제거, 결과 텍스트만 남김)
- **Slack 상태 스트리밍**: Claude 상태 변화를 chat.update로 동일 메시지 수정 (스팸 방지)
  - `slackStatusTs` 추적: 첫 메시지 → `SlackSendViaApi`, 이후 → `SlackUpdateMessageAsync`
  - `lastSlackStatusText` 비교: 텍스트 변경 시에만 업데이트
- **Web Mode** (`--port N`): WebBot Chrome 스크린샷 + 콘텐츠 텍스트 (CDP + UIA)
- **App Mode** (`--app "앱이름"`): 일반 Windows 앱 PrintWindow 스크린샷 + 커서 위 UIA 요소 실시간 추적
- **ActionState IPC** (`wkappbot.hq/runtime/action_state.json`):
  - 모든 CLI 명령이 "마지막 건드린 요소" 기록 → AppBotEye가 폴링으로 읽기
  - 원자적 쓰기 (`.tmp` → `File.Move`), best-effort 읽기
  - ScenarioRunner가 매 스텝 후 자동 기록 (UIA 정보 + 액션 + 진행률)
- **UIA 콘텐츠 추출**: CDP JavaScript 대신 Chrome Accessibility 트리에서 페이지 텍스트 읽기
  - `--force-renderer-accessibility` Chrome 플래그로 웹 콘텐츠 UIA 노출
  - `ExtractWebContentViaUia()`: Document[aid="RootWebArea"] → title_area → dic_area → Text 수집
- **Cloaking**: 10초 동안 페이지 변화 없으면 자동 페이드아웃 (타이머 기반)
- **Hot-reload**: EXE 파일 변경 감지 시 자동 종료 (빌드 후 자동 재시작)
- **Click-to-restore**: 오버레이 클릭 시 대상 앱/Chrome 원본 윈도우 ShowWindow
- 파일: `AppBotEyeCommands.cs`, `AppBotEyeWindow.cs`, `AppBotEyeHost.cs`, `ActionState.cs`

### Slack WebBot 라이브 스트리밍 — "클롯이 바쁠 때 슬랙에서 현황 확인" ✅
**상태**: 완료
- `--webbot` 플래그로 Slack 데몬에 WebBot 모니터링 모드 활성화
- **Claude busy 감지**: UIA "중단" 버튼 존재 여부로 Claude 작업 상태 탐지
- **Claude 상태 텍스트**: UIA StatusBar → Text 최하단 요소에서 현재 작업 설명 추출
- **콘텐츠 추출**: UIA (Accessibility)로 Chrome 웹 페이지 텍스트 읽기 (CDP JS 불필요)
- **Slack chat.update**: 하나의 메시지를 계속 편집 (채널 스팸 방지)
- **라이프사이클**: idle→busy 전환 시 메시지 생성 → 주기적 갱신 → busy→idle 시 "완료" 업데이트
- **콘솔 로깅**: `[SLACK] WebBot [UIA]: {content}` 형태로 UIA 추출 내용 출력
- CDP는 URL 조회용으로만 사용 (콘텐츠는 UIA, backoff 재연결)

### UIA 웹 콘텐츠 추출 — "장애인의 눈으로 본 웹" ✅
**상태**: 완료
- **Chrome 플래그**: `--force-renderer-accessibility` → 웹 콘텐츠의 전체 Accessibility 트리 노출
- **추출 전략** (ExtractWebContentViaUia):
  1. `aid="title_area"` → 기사 제목 (네이버 뉴스 패턴)
  2. `aid="dic_area"` 하위 Text → 기사 본문
  3. 폴백: Document 하위 모든 Text 요소 수집 (Y > 0, 2자 이상)
- **장점**: DOM 셀렉터 의존 없음, 사이트 구조 변경에 강건, 포커스 불필요
- **용도**: AppBotEye 오버레이 + Slack 스트리밍 양쪽에서 공용 사용
- **Claude Desktop UIA 발견사항**:
  - `[Button] "중단"` = Claude 작업 중 (Stop 버튼)
  - `[StatusBar] → [Text]` = 도구 사용 설명 (e.g., "파일 읽음", "명령 실행함")
  - `[Group] "입력하세요" aid="turn-form"` = 프롬프트 입력 영역
  - `[RadioButton] "Chat"/"Cowork"/"Code"` = 모드 선택기

### WebBot contentEditable 입력 노하우 — "GPT/Gemini 삽질기"
**상태**: 완료 (ChatGPT + Gemini 실전 검증)
- **핵심 문제**: ChatGPT(ProseMirror), Gemini(Quill) 등 리치 에디터는 `<input>` 아님 → `el.value=` 무효
- **CDP TypeAsync 한계**: 내부가 `el.value = text` 기반이라 contentEditable에서 전부 실패
- **정답 패턴** (범용, 대부분의 contentEditable 에디터에서 동작):
  ```javascript
  var el = document.querySelector('.ql-editor'); // or #prompt-textarea
  el.focus();
  var sel = window.getSelection();
  var range = document.createRange();
  range.selectNodeContents(el);
  range.collapse(false); // cursor to end
  sel.removeAllRanges();
  sel.addRange(range);
  document.execCommand('insertText', false, text);
  ```
- **텍스트 지우기**: `document.execCommand('selectAll')` → `document.execCommand('delete')`
- **ChatGPT 전송**: `button[data-testid='send-button']` 또는 composer 내 SVG 버튼
- **Gemini 전송**: `button[aria-label='메시지 보내기']`
- **Gemini 응답 읽기**: `document.querySelectorAll('model-response')` → 마지막 `.textContent`
- **ChatGPT 응답 읽기**: `[data-message-author-role='assistant']` → 마지막 `.textContent`
- **AppBotEye 연동**: 웹봇 커맨드마다 WM_APP 시그널 → 미니뷰 자동 Uncloak (클로킹 상태에서도 깨어남)

### Rate Limit 탐지 + Schedule 자동 복구 + Snapshot ✅
**상태**: 완료
- **Rate Limit 탐지** (`AppBotEyeClaudeDetector.cs`):
  - Claude Desktop UIA 텍스트에서 "hit your limit" / "limit" + "reset" 감지
  - `ParseResetTime()`: "resets 4pm (Asia/Seoul)" → DateTime 변환 (12h/24h 양쪽 지원)
  - ActionState에 `IsRateLimited`, `RateLimitResetAt` 기록
  - Slack에 `:warning:` + "Claude 한도 초과" 알림
- **Schedule 시스템** (`ScheduleManager.cs` + `ScheduleCommands.cs`):
  - 3종 스케줄: `once` (시간 지정), `on_limit_reset` (리밋 해제 시), `recurring` (반복)
  - JSON 파일 영속화: `wkappbot.hq/runtime/schedule.json` (atomic .tmp→Move)
  - CLI: `wkappbot schedule add/list/remove/clear`
  - `--prompt "텍스트"` 또는 `--prompt-file ./path.md`
  - 안전장치: `--max-runs N`, `--expires HH:mm`
- **AppBotEye 스케줄 실행기** (`AppBotEyeAppMode.cs`):
  - ~10초마다 `GetDueItems()` 체크 → Claude 프롬프트에 자동 입력
  - `wasRateLimited → !rateLimited` 전환 감지 → `on_limit_reset` 항목 실행
  - 시작 시 과거 pending 항목 즉시 실행 (PC 재부팅 대응)
  - 실행 후 Slack 알림
- **Snapshot 명령** (`SnapshotCommand.cs`):
  - `wkappbot snapshot "Claude" --tag rate_limit --depth 8`
  - UIA tree + PrintWindow 스크린샷 + OCR + info.json 원샷 캡처
  - 다음 리밋 때 UIA 구조 기록 → 탐지 로직 보완용

### Phase 11B: 웹봇 고급 기능 — "크롬이 할 수 있는 건 다 한다"
**상태**: 로드맵
- **탭 관리**: 새 탭 열기, 탭 전환, 탭 닫기
- **네트워크 모니터링**: CDP Network domain으로 XHR/Fetch 응답 가로채기
- **쿠키/스토리지**: CDP Storage domain으로 쿠키/localStorage 읽기/쓰기
- **PDF 다운로드**: CDP Page.printToPDF
- **DOM 쿼리**: CDP DOM domain으로 복잡한 DOM 탐색
- **이벤트 리스닝**: CDP 이벤트 구독 (페이지 로드, 다이얼로그 등)
- **프록시/인증**: Chrome 실행 인자로 프록시 설정
- **활용 시나리오**: GPT 웹 활용, 포털 자동화, 웹 크롤링, API 테스트

### Phase 13: 키움 OpenAPI+ 프록시봇 — "32비트 ActiveX를 64비트에서"
**상태**: 설치 완료 + COM TypeLib 분석 완료, 프록시봇 구현 진행 중

#### 설치 정보 (실측)
- **설치 경로**: `C:\OpenAPI\KHOpenAPI.ocx` (492KB, 32비트)
- **ProgID**: `KHOPENAPI.KHOpenAPICtrl.1`
- **CLSID**: `{A1574A0D-6BFA-4BD7-9020-DED88711818D}`
- **TypeLib GUID**: `{6D8C2B4D-EF41-4750-8AD4-C299033833FB}` v1.2
- **Dispatch IID**: `{CF20FBB6-EDD4-4BE5-A473-FEF91977DEB6}` (메서드)
- **Event IID**: `{7335F12D-8973-4BD5-B7F0-12DF03D175B7}` (이벤트)
- **ThreadingModel**: Apartment (STA 필수!)
- **레지스트리**: `HKCR\WOW6432Node\CLSID\{A1574A0D-...}` (32비트 전용)
- **필수 조건**: 키움 계좌 + OpenAPI 서비스 등록 + 모의투자 신청
- **개발 도구**: KOA Studio (`C:\OpenAPI\KOAStudioSA.exe`) — TR 테스트, FID 조회

#### COM 인터페이스 (TypeLib 자동 열거)
```
KHOpenAPI (COCLASS)
├── [default] _DKHOpenAPI (58 methods) — IDispatch
└── [source]  _DKHOpenAPIEvents (9 events) — ConnectionPoint
```

**주요 메서드 (58개)**:
| 카테고리 | 메서드 |
|---------|--------|
| 연결 | `CommConnect()`, `CommTerminate()`, `GetConnectState()` |
| 로그인 | `GetLoginInfo(tag)` — ACCNO/USER_ID/SERVER_TYPE 등 |
| 주문 | `SendOrder(9 params)`, `SendOrderFO(10)`, `SendOrderCredit(11)` |
| 조회 | `SetInputValue(id,val)` → `CommRqData(name,tr,next,scr)` → `GetCommData(tr,rec,idx,item)` |
| 실시간 | `SetRealReg(scr,codes,fids,opt)`, `GetCommRealData(tr,fid)`, `SetRealRemove(scr,code)` |
| 종목 | `GetMasterCodeName(code)`, `GetCodeListByMarket(mkt)`, `GetMasterLastPrice(code)` |
| 조건검색 | `GetConditionLoad()`, `SendCondition(scr,name,idx,search)`, `GetConditionNameList()` |
| 선물옵션 | `GetFutureList()`, `GetOptionCode(...)`, `GetSFutureList(...)` 등 다수 |
| 유틸 | `KOA_Functions(name,param)`, `GetAPIModulePath()`, `CommKwRqData(codes,...)` |

**이벤트 (9개)**:
| 이벤트 | 파라미터 | 용도 |
|--------|---------|------|
| `OnEventConnect` | nErrCode | 로그인 결과 |
| `OnReceiveTrData` | scrNo, rqName, trCode, recName, prevNext, ... | TR 조회 응답 |
| `OnReceiveRealData` | realKey, realType, realData | 실시간 시세 |
| `OnReceiveMsg` | scrNo, rqName, trCode, msg | 서버 메시지 |
| `OnReceiveChejanData` | gubun, itemCnt, fidList | 체결/잔고 |
| `OnReceiveConditionVer` | ret, msg | 조건식 로드 완료 |
| `OnReceiveTrCondition` | scrNo, codeList, condName, idx, next | 조건검색 결과 |
| `OnReceiveRealCondition` | trCode, type, condName, condIdx | 실시간 조건 |
| `OnReceiveInvestRealData` | realKey | 투자자 실시간 |

#### ComInspect 도구
- `tools/ComInspect.cs` — .NET Framework 4 / C# 5 / 32비트
- 빌드: `csc /platform:x86 ComInspect.cs`
- 실행: `ComInspect.exe [ocx-path] [output-path]`
- 임의의 OCX/DLL → TypeLib 로드 → 메서드/이벤트/프로퍼티 자동 열거
- 결과: `C:\OpenAPI\khopenapi_typelib.md`

#### 아키텍처
```
[앱봇 64비트] ←─ Named Pipe ──→ [프록시봇 32비트] ←─ COM ──→ [키움 OpenAPI+]
   .NET 8 x64                      .NET 8 x86            KHOpenAPI.ocx
```
- **32비트 .NET 런타임 필요**: 현재 시스템에 64비트만 설치됨 → self-contained publish 필요
- **IPC 후보** (우선순위):
  1. Named Pipe (가장 깔끔, 양방향, .NET 기본 지원)
  2. WM_COPYDATA (이미 앱봇에 인프라 있음, 단방향성)
  3. gRPC (복잡한 양방향 스트리밍 필요 시)
- **프록시봇 역할**: COM 초기화(STA) + IDispatch.Invoke + ConnectionPoint 이벤트 수신 + Named Pipe 서버
- **앱봇 역할**: Named Pipe 클라이언트 → 프록시에 조회/주문 명령 전달 + 결과 수신 + UI 자동화 병행
- **Phase A**: 프록시봇 스켈레톤 (32비트 EXE + COM 호스팅 + Named Pipe 서버)
- **Phase B**: 앱봇 클라이언트 (Named Pipe 연결 + 명령/응답 프로토콜)
- **Phase C**: 키움 OpenAPI+ 연동 (로그인, 조건검색, 실시간 시세, 주문)
- **활용**: HTS UI 자동화(Phase 1~7)와 API 직접 호출을 병행 → 최강 조합

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
    ├── runtime/                     # 런타임 IPC 파일
    │   ├── action_state.json        # ActionState — CLI→AppBotEye 상태 공유
    │   └── schedule.json            # 예약 프롬프트 (once/on_limit_reset/recurring)
    ├── logs/                        # 실행 로그
    └── output/                      # 스크린샷, 차트 분석 결과, toolbar 캡처
```

- `dotnet publish` → csproj PostPublish가 EXE + handlers 자동 복사
- `wkappbot <command>` — 어디서든 실행 가능 (DOTNET_ROOT 시스템 환경변수 등록됨)

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
- **dotnet.exe 경로**: `$DOTNET_ROOT/dotnet` (시스템 환경변수)
