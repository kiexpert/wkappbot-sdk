# 빠른 시작 (10분)

설치가 완료되었다면 실제 자동화까지 10분이면 충분합니다.

---

## 1. 설치 확인

```bash
wkappbot --version
```
```
WKAppBot v6.0.0 (core: 6.0.0)
```

---

## 2. 열린 윈도우 확인

```bash
wkappbot windows
```
```
hwnd:0x00050ABC  [notepad.exe]  "제목 없음 - 메모장"
hwnd:0x000A1234  [chrome.exe]   "새 탭 - Google Chrome"
hwnd:0x000B5678  [explorer.exe] "바탕 화면"
```

> `hwnd:0x...` 형식으로 Win32 핸들을 직접 복사해 쓸 수 있습니다.

---

## 3. 요소 탐색

```bash
wkappbot a11y find "*메모장*"
```
```
# TARGET "hwnd:0x00050ABC"
[0] hwnd:0x00050ABC  Name="제목 없음 - 메모장"  ControlType=Window  proc=notepad.exe
```

첫 줄 `# TARGET "hwnd:..."` — 그대로 복사해 다음 명령에 붙여넣을 수 있습니다.

---

## 4. 텍스트 타이핑 (포커스 없이)

```bash
wkappbot a11y type "*메모장*#*편집*" "Hello from WKAppBot!"
```
```
[a11y type] target: hwnd:0x00050ABC / Edit → typed 21 chars (focusless UIA SetValue)
```

메모장이 포커스를 받지 않아도 텍스트가 입력됩니다.

---

## 5. 요소 읽기

```bash
wkappbot a11y read "*메모장*#*편집*"
```
```
Hello from WKAppBot!
```

---

## 6. 스킬 확인

WKAppBot에는 축적된 운영 노하우(스킬)가 내장되어 있습니다:

```bash
wkappbot skill list
```
```
=== wkappbot (42 skills) ===
  a11y-command-cheatsheet        [v3]  a11y Command Cheatsheet - Operator View
  a11y-find                      [v2]  a11y find - output format and process resolution
  ask-command-cheatsheet         [v4]  ask -- AI Delegation Cheatsheet
  file-command-cheatsheet        [v2]  file -- Read/Write/Edit/Grep/Glob Cheatsheet
  grap                           [v5]  grap -- Universal Element Address Syntax
  ...
```

```bash
wkappbot skill read grap
```
```
# grap — Universal Element Address Syntax
grap is WKAppBot's unified address language for any UI element...
```

---

## 7. YAML 시나리오 실행

간단한 시나리오 파일을 만들어봅니다:

```yaml
# test-notepad.yaml
scenario: { name: "메모장 자동화 테스트" }
steps:
  - name: "텍스트 입력"
    target: { class: "Edit", ancestor: { title_contains: "메모장" } }
    action: type_text
    params: { text: "자동화 테스트 성공!" }
  - name: "내용 확인"
    target: { class: "Edit", ancestor: { title_contains: "메모장" } }
    action: assert
    params: { type: text_contains, expected: "자동화 테스트" }
```

```bash
wkappbot validate test-notepad.yaml   # 구문 검증 (실행 없이)
wkappbot run test-notepad.yaml        # 실행
```
```
[scenario] "메모장 자동화 테스트" starting...
  PASS  텍스트 입력     (42ms)
  PASS  내용 확인       (18ms)
[scenario] 2/2 passed  60ms total
```

---

## AI에게 맞춤 설명 받기

설치 후 막막하다면 `wkappbot help` 출력을 그대로 AI에 붙여넣고 물어보세요:

```bash
wkappbot help
```

출력 전체를 복사한 뒤 Claude(또는 ChatGPT·Gemini)에 다음처럼 보내면 됩니다:

```
wkappbot help 출력이야. 이 앱봇으로 나한테 맞게 설명해줘.
```

내 PC에 설치된 버전 기준으로, 내 상황에 맞는 설명을 바로 얻을 수 있습니다.

---

## 다음 단계

- [핵심 개념](/guide/concepts) — grap, Eye 데몬, 5-tier 검색 이해
- [CLI 명령어 전체](/reference/commands) — 모든 명령어 + 출력 샘플
- [grap 패턴](/reference/grap) — 복잡한 요소 주소 작성법
