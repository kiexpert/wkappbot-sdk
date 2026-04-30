---
layout: home

hero:
  name: WKAppBot
  text: Windows UI Automation for AI Agents
  tagline: Focusless. Self-healing. AI-native. The bridge between LLMs and the apps humans already use.
  image:
    src: /logo.png
    alt: WKAppBot
  actions:
    - theme: brand
      text: 10분 시작하기 →
      link: /guide/quickstart
    - theme: alt
      text: 설치 가이드
      link: /guide/install
    - theme: alt
      text: GitHub
      link: https://github.com/kiexpert/wkappbot-sdk

features:
  - icon: 🎯
    title: Focusless-First
    details: UIA Invoke/Value/Toggle/Select는 포커스를 빼앗지 않습니다. Win32 PostMessage로 레거시 MFC까지 지원. SendInput은 최후의 수단. 사용자가 작업하는 동안 AI가 백그라운드에서 조작합니다.
  - icon: 🔄
    title: Self-Healing
    details: UIA가 실패하면 CCA 세그멘테이션 → OCR 삼중 교차검증 → Gemini Vision 추론으로 자동 복구. Experience DB에 결과 캐싱으로 반복 실행 시 빠른 티어 건너뜀.
  - icon: 🤖
    title: AI-Native
    details: GPT·Gemini·Claude에 병렬 위임, CDP로 라이브 브라우저 AI 세션 주입, 토큰 예산 소진 시 자동 핸드오프. 같은 바이너리가 자동화와 AI 위임을 모두 처리합니다.
  - icon: 🌐
    title: grap — Grab Accessible Pattern
    details: Win32, UIA, 웹, Android의 모든 UI 요소를 단일 주소로. 와일드카드, OR, CSS 셀렉터, UIA 스코프, ADB 경로를 하나의 문법으로.
  - icon: 📱
    title: Android 지원
    details: ADB + Accessibility Tree 기반 Android 자동화. adb://device/앱 grap 문법으로 데스크탑 자동화와 동일한 방식으로 제어.
  - icon: 🔒
    title: 무료 티어 포함
    details: 기본 Win32/UIA/Android 자동화는 무료. CDP 브라우저 자동화·멀티 AI 위임은 CDP 티어(₩100,000/월). HTS 거래 터미널 sudo 접근은 Sudo 티어.
---

## 60초 빠른 시작

```bat
git clone https://github.com/kiexpert/wkappbot-sdk %USERPROFILE%\Documents\wkappbot
cd %USERPROFILE%\Documents\wkappbot
build.cmd
```

`bin\`을 PATH에 추가하고 새 터미널에서:

```bash
wkappbot --version              # 설치 확인
wkappbot windows                # 열린 윈도우 목록
wkappbot a11y find "*Notepad*"  # 요소 탐색
wkappbot skill list             # 내장 노하우 브라우즈
```

> **어떤 앱이든 자동화 가능.** 앱 수정 불필요, 벤더 API 불필요. 사람이 쓸 수 있으면 WKAppBot이 자동화할 수 있습니다.
