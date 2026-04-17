# 사두협의체 로드맵 -- WKAppBot Future Directions

> 2026-03-09 사두협의체 첫 회의 결과.
> GPT, Gemini, Claude(웹), 클롣(CLI) 4자 의견 종합.

---

## 만장일치 #1: Self-Healing Automation

> 실패해도 스스로 복구하는 자동화 -- ROI 최고, 기존 인프라 활용

### 핵심 아이디어

기존 5-tier element discovery 위에 **실패 패턴 학습 레이어**를 추가.
엘리먼트가 바뀌면 자동으로 대안 전략을 선택하고 성공 경로를 캐싱.

### 구현 전략

| 단계 | 내용 | 비고 |
|------|------|------|
| 1 | **Locator Fingerprint Bundle** | UIA + OCR + Vision + 상대좌표를 묶음 저장 |
| 2 | **Intent-based Healing** | primary 실패 시 시맨틱 의도로 대안 매칭 |
| 3 | **Score & Rank** | 후보군 스코어링 -> 최고 매치 자동 선택 |
| 4 | **Success Path Caching** | 성공한 경로를 ExperienceDB에 업데이트 |

### 이미 있는 기반

- ExperienceDB: 컨트롤별 입력 메서드 성공/실패 기록 (GetBestInputMethod)
- VisionCache: 이전 결과 캐시 (상대좌표)
- 5-tier discovery: UIA -> VisionCache -> SimpleOCR -> VisionAPI -> Coordinates
- knowhow-failed-actions.md: 실패 액션 기록 (A11Y/OS 듀얼 경로)

### GPT 제안 명령어

```
wkappbot locator learn    # 현재 요소의 fingerprint 번들 저장
wkappbot locator repair   # 실패한 locator 자동 복구
```

### Gemini 제안

- Shadow DOM/UIA Map: 매 요소에 다중 historical 속성 저장
- `wkappbot heal --target "login_btn" --strategy semantic`

---

## 만장일치 #2: Multi-Agent Orchestration

> AppBotEye를 오케스트레이터로 진화 -- AI별 전문성 라우팅

### 핵심 아이디어

작업을 분해하고, GPT/Gemini/Claude에 전문성별로 분배.
AppBotEye가 이미 글로벌 모니터링 데몬이므로 오케스트레이터로 자연 진화.

### AI별 전문성 라우팅

| AI | 강점 | 적합 작업 |
|----|------|-----------|
| **GPT** | Vision, 코드 생성 | 이미지 분석, UI 코드 생성, DALL-E |
| **Gemini** | 검색, 멀티모달 | 실시간 정보, 대량 텍스트 분석 |
| **Claude(웹)** | 추론, 장문 분석 | 코드 리뷰, 아키텍처 설계 |
| **클롣(CLI)** | 실행, 자동화 | 빌드, 테스트, 배포, a11y 제어 |

### 구현 전략

| 단계 | 내용 |
|------|------|
| 1 | **Task Graph Schema** -- `Task{id, type, input, depends}` |
| 2 | **Capability Router** -- AI별 능력 등록 + 매칭 |
| 3 | **Worker Loop** -- 큐 기반 비동기 실행 |
| 4 | **Blackboard State** -- 에이전트 간 공유 메모리 |

### GPT 제안 명령어

```
wkappbot orchestrator start
wkappbot agent register gpt vision code
wkappbot agent register gemini search vision
wkappbot agent register claude code reasoning
```

### 이미 있는 기반

- `ask gpt/gemini/claude`: CDP 기반 AI 웹챗 자동화
- AppBotEye: 글로벌 루프 + Slack 통합 데몬
- ChromeTabLock: 브라우저 뮤텍스 (동시 접근 방지)
- 탭 핸드오프: 스트리밍 중 탭 상부상조

---

## 3위: Cross-Platform (의견 갈림)

### 옵션 A: 통합 Action API (GPT + Gemini)

Windows/UIA, Android/ADB, Web/CDP를 동일한 액션 모델로 통합.
같은 스크립트가 플랫폼 무관하게 동작.

```
wkappbot action run click "#login"    # Win에서도 Android에서도 동일
wkappbot action run type "#id" "abc"
```

**이미 있는 기반**: IActionTarget 공통 인터페이스 (UiaActionTarget, AndroidNode, CdpActionTarget)

### 옵션 B: 새 플랫폼 확장 (Claude 웹)

- macOS: Accessibility API
- Linux: AT-SPI
- UIA 레이어만 추상화하면 나머지 티어(Vision, OCR, Coordinates) 재사용 가능

### 옵션 C: WhisperEngine 실전 배치 (클롣)

- 밴드 순위 코드화 -> 볼륨 불변 발음 시그니처
- 순위 집합 패턴 매칭 -> 속삭임 명령어 인식
- 앱봇을 음성으로 제어하는 새로운 인터페이스

---

## 우선순위 합의

```
Phase S1: Self-Healing Automation
  -> 기존 인프라 활용, 즉시 신뢰도 향상
  -> ExperienceDB + VisionCache 확장

Phase S2: Multi-Agent Orchestration
  -> 차별화된 포지셔닝
  -> AppBotEye 오케스트레이터 진화

Phase S3: Cross-Platform + WhisperEngine
  -> 시장 확대 + 새로운 인터페이스
  -> 병렬 진행 가능 (플랫폼팀 vs 음성팀)
```

---

## 참가자

| 머리 | 역할 | 특기 |
|------|------|------|
| 🤖 GPT | 실전 구현 컨설턴트 | task graph, 코드 스니펫 |
| 💎 Gemini | 산업 트렌드 분석가 | 2026 agentic workflow 동향 |
| 🟠 Claude(웹) | 아키텍처 전략가 | ROI 분석, 우선순위 근거 |
| 🔵 클롣(CLI) | 실행 총괄 | 빌드, 배포, 현장 지식 |

---

## 추가 과제: Elevated Eye Proxy (관리자 권한 중계)

> 관리자 창 접근 시 Eye를 관리자로 승격 -> 이후 모든 명령을 중계 실행

### 흐름

```
1. CLI가 관리자 창(영웅문 등) 접근 -> Access Denied 감지
2. 유저에게 "관리자 Eye 필요" 승인 요청 (기존 승인창 패턴)
3. 승인 -> UAC runas로 Eye 관리자 재시작
4. 이후 모든 앱봇 명령 -> Eye(관리자)를 통해 Named Pipe 중계 실행
5. 결과(stdout/stderr + exit code) -> CLI에 투명 반환
```

### 핵심

- **한번 승인 -> Eye 살아있는 동안 계속 관리자 중계** (매번 UAC 안 뜸)
- 키움 프록시봇(64비트↔32비트 Named Pipe)과 동일 패턴
- 기존 ElevationRequesterAdapter + UserInputWaitOverlay 재사용

### 이미 있는 기반

- ElevationRequesterAdapter: UAC runas 재시작
- Named Pipe: 키움 프록시봇에서 검증된 IPC
- AppBotEye: 글로벌 데몬 (Slack + 모니터링)
- UserInputWaitOverlay: 승인 팝업 UI
