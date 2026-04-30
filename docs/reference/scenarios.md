# YAML 시나리오

YAML 파일로 멀티스텝 자동화 흐름을 기술합니다. 코드 없이 테스트 시나리오와 자동화 스크립트를 작성할 수 있습니다.

---

## 기본 구조

```yaml
scenario:
  name: "시나리오 이름"
  description: "선택적 설명"

app:
  launch: "calc.exe"
  wait_for_window:
    title_contains: "계산기"

steps:
  - name: "단계 이름"
    target:
      automation_id: "num7Button"
    action: click

  - name: "결과 확인"
    target:
      automation_id: "CalculatorResults"
    action: assert
    params:
      type: text_contains
      expected: "7"
```

---

## `app` 섹션

앱 실행 및 대기 조건을 정의합니다:

```yaml
app:
  launch: "notepad.exe"            # 실행할 exe
  args: ["file.txt"]               # 실행 인자 (선택)
  wait_for_window:
    title_contains: "메모장"       # 창 제목 조건
    timeout: 10                    # 초 (기본 30)
  skip_if_running: true            # 이미 실행 중이면 재실행 안 함
```

---

## `target` 필드

| 필드 | 의미 |
|------|------|
| `automation_id` | UIA AutomationId |
| `name` | UIA Name (와일드카드 지원: `*매수*`) |
| `class` | Win32 클래스명 |
| `grap` | 완전한 grap 식 (가장 유연) |
| `ancestor` | 부모 조건 (아래 참고) |
| `nth` | N번째 매칭 (기본: 1) |

```yaml
# ancestor로 범위 한정
target:
  class: "Edit"
  ancestor:
    title_contains: "메모장"

# grap 직접 사용
target:
  grap: "heroes#realtime-account"

# N번째 매칭
target:
  name: "*버튼*"
  nth: 2
```

---

## 지원 액션

### `click` / `double_click` / `right_click`

```yaml
- name: "확인 버튼 클릭"
  target: { automation_id: "okButton" }
  action: click
```

### `type_text`

```yaml
- name: "종목 코드 입력"
  target: { automation_id: "codeEdit" }
  action: type_text
  params:
    text: "005930"
    clear_first: true    # 입력 전 기존 내용 지우기 (기본 false)
```

### `press_key` / `hotkey`

```yaml
- name: "Ctrl+S 저장"
  target: { grap: "*메모장*" }
  action: hotkey
  params:
    keys: "ctrl+s"

- name: "Enter 키"
  target: { grap: "*대화상자*" }
  action: press_key
  params:
    key: "enter"
```

### `assert` — 검증

```yaml
- name: "결과 확인"
  target: { automation_id: "resultLabel" }
  action: assert
  params:
    type: text_contains     # text_contains | text_equals | text_matches | visible | enabled
    expected: "주문완료"
    timeout: 5              # 최대 대기 초 (기본 3)
```

| `type` | 설명 |
|--------|------|
| `text_contains` | 텍스트에 expected 포함 |
| `text_equals` | 텍스트가 expected와 일치 |
| `text_matches` | 정규식 매칭 |
| `visible` | 요소가 보임 (expected: true/false) |
| `enabled` | 요소가 활성화 상태 |

### `wait`

```yaml
- name: "로딩 완료 대기"
  target: { name: "*완료*" }
  action: wait
  params:
    timeout: 30
    condition: visible      # visible | text_contains | enabled
    value: "완료"
```

### `screenshot`

```yaml
- name: "결과 캡처"
  action: screenshot
  params:
    path: "result.png"           # 저장 경로 (선택)
    target: { grap: "*앱*" }     # 특정 창만 캡처 (선택)
```

### `scroll`

```yaml
- name: "아래로 스크롤"
  target: { grap: "*목록*" }
  action: scroll
  params:
    direction: down    # up | down | left | right
    amount: 3
```

### `select`

```yaml
- name: "드롭다운 선택"
  target: { automation_id: "comboBox" }
  action: select
  params:
    value: "항목2"
```

### `toggle`

```yaml
- name: "체크박스 토글"
  target: { name: "동의" }
  action: toggle
```

### `expand` / `collapse`

```yaml
- name: "트리 노드 펼치기"
  target: { name: "폴더" }
  action: expand
```

### `set_range`

```yaml
- name: "슬라이더 조정"
  target: { automation_id: "volumeSlider" }
  action: set_range
  params:
    value: 75
```

### `window_close` / `minimize` / `maximize`

```yaml
- name: "앱 닫기"
  target: { grap: "*메모장*" }
  action: window_close
```

---

## 실전 예시

### HTS 매수 주문 자동화

```yaml
scenario:
  name: "삼성전자 매수 테스트"

app:
  wait_for_window:
    title_contains: "영웅문"

steps:
  - name: "종목 코드 입력"
    target:
      grap: "heroes#code-edit"
    action: type_text
    params:
      text: "005930"
      clear_first: true

  - name: "Enter로 조회"
    target:
      grap: "heroes#code-edit"
    action: press_key
    params:
      key: "enter"

  - name: "현재가 확인"
    target:
      grap: "heroes#current-price"
    action: assert
    params:
      type: text_matches
      expected: "\\d{2,3},\\d{3}"   # 가격 형식

  - name: "수량 입력"
    target:
      grap: "heroes#qty-edit"
    action: type_text
    params:
      text: "1"
      clear_first: true

  - name: "매수 버튼"
    target:
      grap: "heroes#*매수*"
    action: click

  - name: "주문 완료 확인"
    target:
      name: "*주문완료*"
    action: assert
    params:
      type: visible
      expected: true
      timeout: 10
```

### 브라우저 자동 로그인

```yaml
scenario:
  name: "웹 로그인 자동화"

steps:
  - name: "이메일 입력"
    target:
      grap: "{proc:'chrome', domain:'app.example.com'}#input[type='email']"
    action: type_text
    params:
      text: "user@example.com"

  - name: "비밀번호 입력"
    target:
      grap: "{proc:'chrome', domain:'app.example.com'}#input[type='password']"
    action: type_text
    params:
      text: "password"

  - name: "로그인 버튼"
    target:
      grap: "{proc:'chrome', domain:'app.example.com'}#.login-btn"
    action: click

  - name: "로그인 성공 확인"
    target:
      grap: "{proc:'chrome', domain:'app.example.com'}#.user-menu"
    action: assert
    params:
      type: visible
      expected: true
      timeout: 15
```

---

## 시나리오 실행

```bash
# 구문 검증 (실행 없이)
wkappbot validate my-scenario.yaml

# 실행
wkappbot run my-scenario.yaml
```
```
[scenario] "삼성전자 매수 테스트" starting...
  PASS  종목 코드 입력    (85ms)
  PASS  Enter로 조회      (320ms)
  PASS  현재가 확인       (42ms)
  PASS  수량 입력         (67ms)
  PASS  매수 버튼         (124ms)
  PASS  주문 완료 확인    (1243ms)
[scenario] 6/6 passed  1881ms total
```
