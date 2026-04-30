# CLI 명령어 레퍼런스

> WKAppBot v6.0.0 기준. 모든 명령에 `--help` 플래그로 상세 도움말을 볼 수 있습니다.

---

## 전역 플래그

모든 명령에 적용됩니다:

| 플래그 | 설명 |
|--------|------|
| `--timeout <dur>` | 강제 종료 타임아웃 (예: `30`, `2m`, `500ms`) |
| `--nth N` | N번째 매칭 대상 (1-based, `N~` = N번째부터 전체) |
| `--all` | 매칭되는 모든 대상에 적용 |
| `--sudo` | 관리자 권한으로 실행 (UAC 프롬프트) |
| `--no-regression` | 회귀 셀프테스트 건너뜀 |
| `--version` | 버전 출력 후 종료 |
| `--worker` | Eye 자동 기동 억제 |

---

## `wkappbot a11y`

24개 액션을 가진 통합 UI 자동화 명령입니다.

```bash
wkappbot a11y <action> <grap>[#uia-scope] [options]
```

### 탐색 액션

#### `find` — 요소 탐색 및 핸들 출력

```bash
wkappbot a11y find "*메모장*"
```
```
# TARGET "hwnd:0x00050ABC"
[0] hwnd:0x00050ABC  Name="제목 없음 - 메모장"  ControlType=Window  proc=notepad.exe
```

#### `inspect` — UIA 트리 덤프

```bash
wkappbot a11y inspect "*계산기*" --depth 3
```
```
Window  "계산기"  hwnd:0x001A2B3C  proc=Calculator.exe
  Pane  "계산기 앱"
    Group  "숫자 패드"
      Button  "7"  AutomationId=num7Button  [InvokePattern]
      Button  "8"  AutomationId=num8Button  [InvokePattern]
      Button  "9"  AutomationId=num9Button  [InvokePattern]
      Button  "나누기"  AutomationId=divideButton  [InvokePattern]
```

#### `highlight` — 시각적 하이라이트 오버레이

```bash
wkappbot a11y highlight "*계산기*#*7*"
# → 화면에 해당 요소 위에 빨간 테두리 오버레이 표시
```

---

### 상호작용 액션

#### `click` — 포커스리스 클릭

```bash
wkappbot a11y click "*계산기*#num7Button"
```
```
[a11y click] target: hwnd:0x001A2B3C / Button "7" → invoked (UIA InvokePattern, focusless)
```

#### `type` — 텍스트 입력

```bash
wkappbot a11y type "*메모장*#*Edit*" "Hello, World!"
```
```
[a11y type] target: hwnd:0x00050ABC / Edit → typed 13 chars (focusless UIA SetValue)
```

핫키 메뉴 탐색:
```bash
wkappbot a11y type "*메모장*" "파일/저장" --hotkey
```
```
[a11y type] target: hwnd:0x00050ABC → hotkey dispatch "파일/저장" (WM_CHAR)
```

#### `read` — 텍스트 읽기

```bash
wkappbot a11y read "*메모장*#*Edit*"
```
```
Hello, World!
```

브라우저 (CDP 우선):
```bash
wkappbot a11y read "{proc:'chrome', domain:'claude.ai'}"
```
```
[CDP] https://claude.ai/chat/abc123
Claude is an AI assistant made by Anthropic...
(전체 페이지 텍스트)
```

JS 평가 포함:
```bash
wkappbot a11y read "*Chrome*" --eval-js "document.title"
```
```
Claude - 새 대화
```

#### `scroll` — 스크롤

```bash
wkappbot a11y scroll "*목록*" down 3
# → 3칸 아래로 스크롤
```

#### `set-value` — 값 설정 (슬라이더 등)

```bash
wkappbot a11y set-value "*볼륨*" 75
```

#### `set-range` — 범위값 설정

```bash
wkappbot a11y set-range "*진행률*" 50
```

---

### 윈도우 관리 액션

```bash
wkappbot a11y close    "*메모장*"
wkappbot a11y minimize "*앱*"
wkappbot a11y maximize "*앱*"
wkappbot a11y restore  "*앱*"
wkappbot a11y focus    "*앱*"
wkappbot a11y move     "*앱*" --x 100 --y 200
wkappbot a11y resize   "*앱*" --w 800 --h 600
```

---

### 제어 패턴 액션

```bash
wkappbot a11y invoke   "*버튼*"          # InvokePattern
wkappbot a11y toggle   "*체크박스*"      # TogglePattern
wkappbot a11y expand   "*트리노드*"      # ExpandCollapsePattern
wkappbot a11y collapse "*트리노드*"
wkappbot a11y select   "*리스트*#*항목*" # SelectionItemPattern
```

---

### 대기 & 클립보드

```bash
# 요소가 나타날 때까지 대기 (최대 30초)
wkappbot a11y wait "*다운로드완료*" --timeout 30

# 특정 텍스트가 포함될 때까지 대기
wkappbot a11y wait "*상태바*" --condition text_contains --value "완료"

# 클립보드 읽기/쓰기
wkappbot a11y clipboard-write "복사할 텍스트"
wkappbot a11y clipboard-read
```

---

## `wkappbot ask`

CDP 기반 AI 위임 명령입니다. Chrome/Edge가 열려 있어야 합니다.

```bash
# 단일 AI
wkappbot ask claude "이 차트를 설명해줘" chart.png
wkappbot ask gpt    "이 코드에서 버그 찾아줘"
wkappbot ask gemini "한국어로 요약해줘"

# 트라이어드 (GPT + Gemini + Claude 병렬)
wkappbot ask triad "이 접근법이 맞는지 검토해줘"
wkappbot ask triad "버그 원인 분석" --debate 3
```
```
[triad] sending to GPT-4o... Gemini-2.5-Pro... Claude-Opus...
[GPT]     The issue is likely a race condition in...
[Gemini]  I see a potential deadlock scenario where...
[Claude]  Looking at this from a different angle...
[synthesis] All three models agree on: ...
```

::: info CDP 필수
`ask` 명령은 Chrome/Edge 브라우저에서 해당 AI 탭이 열려 있어야 합니다. CDP (Chrome DevTools Protocol) 로 포커스 없이 프롬프트를 주입합니다.
:::

---

## `wkappbot windows`

```bash
wkappbot windows              # 전체 윈도우 목록
wkappbot windows "*chrome*"   # 패턴 필터
```
```
hwnd:0x00050ABC  [notepad.exe]   "제목 없음 - 메모장"
hwnd:0x000A1234  [chrome.exe]    "새 탭 - Google Chrome"  (3 tabs)
hwnd:0x001A2B3C  [calc.exe]      "계산기"
```

---

## `wkappbot eye`

```bash
wkappbot eye tick   # 현재 상태 조회 (Eye 기동 없음)
```
```
[EYE] running  pid=12345  uptime=3h22m
[CTX] 4.1 MB / ~20 MB (20%)
[QUEUE] pending=0  processing=0
[HOTSWAP] last-swap: none
```

---

## `wkappbot skill`

```bash
wkappbot skill list                    # 스킬 목록
wkappbot skill search "focus"          # 키워드 검색
wkappbot skill read grap               # 스킬 상세 읽기
wkappbot skill contribute \            # 새 스킬 작성
  --app wkappbot \
  --title "내 스킬" \
  --desc "설명" \
  --steps "step1|step2"
```
```
=== wkappbot (42 skills) ===
  grap                 [v5]  grap -- Universal Element Address Syntax
  a11y-find            [v2]  a11y find - output format and process resolution
  focusless-first      [v3]  Focusless-First Principle
  ...
```

---

## `wkappbot schedule`

```bash
wkappbot schedule add "매일 로그 정리" --at "09:00" --repeat 1d
wkappbot schedule list
wkappbot schedule remove <id>
```
```
[schedule] added  id=sched_001  "매일 로그 정리"  next=2026-05-01T09:00
```

---

## `wkappbot logcat`

```bash
# 최근 1시간 로그에서 SLACK 관련 항목 검색
wkappbot logcat "SLACK" --hq --past 1h

# 실시간 에러 스트림
wkappbot logcat "error" --past 10m -f

# JSON 구조 검색
wkappbot logcat '{"role":"user"}' *.jsonl --json --hq --past 2h
```
```
[2026-04-30 10:23:45] [SLACK] received: "자동화 시작해줘" from @kiexp
[2026-04-30 10:23:46] [ROUTE] → claude_code_prompt
```

---

## `wkappbot file`

```bash
wkappbot file read  "src/main.cs" --offset 100 --limit 50
wkappbot file write "output.txt" --text "결과 내용"
wkappbot file edit  "old string" "new string" "src/main.cs"
wkappbot file grep  "class.*Handler" --path "csharp/src" --type cs
wkappbot file glob  "**/*.cs" --path "csharp/src"
```

::: warning CP949 주의
일부 한국어 소스 파일은 CP949 인코딩입니다. 편집 전 `--encoding 949` 옵션으로 확인하세요.
:::

---

## `wkappbot suggest`

버그나 개선사항을 현재 작업을 중단하지 않고 큐에 등록합니다:

```bash
# 등록 (요구사항 3개 필수)
wkappbot suggest "버그: 포커스 경쟁 조건" \
  --requirement "wkappbot a11y type '*app*' 'text' => focus not stolen" \
  --requirement "wkappbot eye tick => no focus-steal events" \
  --requirement "wkappbot a11y read '*app*' => correct text"

wkappbot suggest list   # 미해결 목록
```

---

## `wkappbot run`

```bash
wkappbot run scenario.yaml     # YAML 시나리오 실행
wkappbot run notepad           # 프리셋 실행
wkappbot run "calc.exe"        # 직접 실행
```

---

## `wkappbot newchat`

```bash
# 새 Claude 채팅 열기 + 프롬프트 전달 (컨텍스트 핸드오프)
wkappbot newchat "이전 작업 계속: a11y 포커스 버그 수정 중"
wkappbot newchat --file handoff.txt   # 큰 프롬프트는 파일로
```

---

## `wkappbot claude-usage`

```bash
wkappbot claude-usage
```
```
JSONL size: 3.2 MB
ctx%: 16% (3.2 MB / ~20 MB)
Plan usage: 45% (claude.ai/settings/usage)
```

> 8MB 도달 시 핸드오프 준비, 10MB 시 즉시 핸드오프 권장.

---

## `wkappbot speak`

```bash
wkappbot speak "작업 완료!" --bg
wkappbot speak "오류 발생" --target "*앱*" --voice "Microsoft Heami"
```
