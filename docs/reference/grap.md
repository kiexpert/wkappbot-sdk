# grap 패턴 레퍼런스

> **grap** = glob/regex/OR(`;`)/`#UIA-scope`/`{JSON5}` 다중필드 AND

grap은 WKAppBot의 통합 UI 요소 주소 언어입니다. Win32 윈도우, UIA 노드, 웹 페이지, Android 앱의 모든 요소를 하나의 문법으로 표현합니다.

---

## 기본 패턴

### 와일드카드 (Glob)

```bash
wkappbot a11y find "*메모장*"        # 제목에 "메모장" 포함
wkappbot a11y find "계산기"          # 정확히 "계산기"
wkappbot a11y find "*설정*창*"       # 복수 와일드카드
```

### OR 패턴 (세미콜론)

```bash
wkappbot a11y find "*메모장*;*계산기*"     # 메모장 또는 계산기
wkappbot a11y find "*Chrome*;*Edge*"       # Chrome 또는 Edge
```

여러 앱 중 하나가 존재하는지 확인할 때 유용합니다.

### 정규식

```bash
wkappbot a11y find "regex:btn_\\d+"       # btn_ 뒤에 숫자
wkappbot a11y find "regex:^계산기$"       # 정확히 "계산기"만
```

---

## UIA 스코프 (`#`)

`#`으로 UIA 트리를 드릴다운합니다:

```bash
wkappbot a11y find "계산기#숫자 패드"            # 계산기 안의 "숫자 패드" 그룹
wkappbot a11y click "heroes#realtime-account"    # heroes 창 안의 realtime-account 노드
wkappbot a11y read "*메모장*#*Edit*"             # 메모장 안의 편집 영역
```

스코프는 여러 단계 중첩 가능합니다:

```bash
wkappbot a11y find "*앱*#*메뉴바*#*파일*"
```

---

## 탭 포털 (Chrome / Edge)

브라우저의 특정 탭 안 요소를 지정합니다:

```bash
# Chrome > ChatGPT 탭 > model 드롭다운
wkappbot a11y find "Chrome#ChatGPT#model"

# Edge > 특정 도메인 탭 > CSS 셀렉터
wkappbot a11y click "{proc:'msedge', domain:'app.example.com'}#.submit-btn"
```

탭 포털은 탭 전환을 자동으로 처리합니다.

---

## JSON5 다중 필드 AND

여러 조건을 AND로 조합합니다:

```bash
# 프로세스 + 도메인
{proc:'chrome', domain:'claude.ai'}

# 제목 포함 + 프로세스
{proc:'notepad', title:'제목 없음'}

# 특정 Chrome 탭의 textarea
{proc:'chrome', domain:'chatgpt.com', title:'ChatGPT'}#main textarea
```

지원 필드:

| 필드 | 의미 |
|------|------|
| `proc` | 프로세스명 (확장자 없이) |
| `domain` | 브라우저 탭 도메인 |
| `title` | 창 제목 (부분 일치) |
| `class` | Win32 클래스명 |
| `pid` | 프로세스 ID |

---

## Direct Handle

이미 알고 있는 Win32 핸들:

```bash
wkappbot a11y click "hwnd:0x010B084A"
wkappbot a11y read  "hwnd:0x010B084A"
```

`a11y find` 결과의 `# TARGET "hwnd:..."` 줄을 복사해 바로 사용할 수 있습니다.

---

## Android (ADB)

```bash
# 기기/에뮬레이터#요소
wkappbot a11y find "adb://Fold5/*balance*"
wkappbot a11y click "adb://emulator-5554/*btn_buy*"
wkappbot a11y read  "adb://device/*account_info*#balance"
```

ADB가 PATH에 있어야 하고, USB 디버깅이 활성화되어 있어야 합니다.

---

## `--nth` 옵션

여러 매칭 결과 중 특정 번째 대상을 선택합니다:

```bash
wkappbot a11y click "*Chrome*" --nth 2      # 두 번째 Chrome 창
wkappbot a11y close "*Chrome*" --nth 2~     # 두 번째부터 전체 닫기
wkappbot a11y read  "*Chrome*" --nth ~2     # 마지막에서 두 번째
wkappbot a11y type  "*Edit*"   --nth 1,3~   # 첫 번째 + 세 번째부터 전체
wkappbot a11y click "*버튼*"   --all        # 매칭되는 모든 버튼
```

---

## 실전 예시

### HTS 거래 터미널

```bash
# MFC owner-drawn 컨트롤 (UIA 없음 → OCR 폴백)
wkappbot a11y find "heroes#realtime-account"
wkappbot a11y type "heroes#code-edit" "005930"       # 삼성전자
wkappbot a11y click "heroes#*매수*"
```

### 브라우저 자동화

```bash
# Claude AI에 메시지 입력
wkappbot a11y type "{proc:'chrome', domain:'claude.ai'}#main [contenteditable]" "안녕하세요"
wkappbot a11y click "{proc:'chrome', domain:'claude.ai'}#*전송*"
```

### 멀티 모니터 / 멀티 인스턴스

```bash
# 메모장 3개 중 두 번째 것에만 입력
wkappbot a11y type "*메모장*#*Edit*" "내용" --nth 2
```

---

## 디버깅 팁

`a11y inspect`으로 UIA 트리를 먼저 확인하세요:

```bash
wkappbot a11y inspect "*앱*" --depth 4
```

`a11y highlight`으로 실제로 어떤 요소를 가리키는지 시각화:

```bash
wkappbot a11y highlight "*앱*#*버튼*"
```
