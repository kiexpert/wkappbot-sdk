# wkfind: Comprehensive Problem-Solving Methodology

## 목적
복잡한 소프트웨어 문제를 찾고 해결하기 위한 체계적인 접근 방법론.

## 문제해결을 위해 접근 가능한 모든 수단 (Toolbox)

### 1. Code Reading & Understanding
- **Read tool**: 파일 전체 또는 부분 읽기
- **Line-by-line analysis**: 코드 흐름 추적
- **Cross-file references**: 파일 간 호출 관계 이해
- **Usage patterns**: 특정 함수/변수 사용처 파악

### 2. Version Control & History
- **git log**: 커밋 이력에서 의도 파악
- **git show**: 특정 커밋의 변경사항 조사
- **git diff**: 현재와 과거의 차이 분석
- **git blame**: 코드 라인별 작성자/시점 확인
- **Branch history**: 병렬 진행된 작업 이해

### 3. Pattern Matching & Search
- **Grep**: 정규식으로 코드베이스 검색
- **Glob**: 파일 패턴으로 파일 찾기
- **Symbol search**: 특정 함수/클래스 찾기
- **Cross-repo search**: 여러 저장소에서 동일 패턴 찾기

### 4. Web Search & External Knowledge
- **WebSearch**: 구글 검색으로 비슷한 사례 찾기
- **Documentation**: 공식 문서/스펙 확인
- **Error messages**: 에러 메시지로 원인 파악
- **StackOverflow patterns**: 알려진 해결책 패턴

### 5. Automated Code Analysis
- **Agent with Explore**: 대규모 코드베이스 자동 탐색
- **Grep with context**: 매칭된 코드의 전후 문맥 포함
- **File type filtering**: 특정 언어/파일타입만 검색
- **Parallel searches**: 여러 검색을 동시 실행

### 6. Build & Deployment Testing
- **Compile**: 코드 정상 작동 여부 확인
- **Deploy**: 변경사항 배포
- **Hot-swap**: 실시간 변경 적용
- **Smoke test**: 기본 기능 동작 확인

### 7. Environment & Runtime Analysis
- **Environment variables**: 프로세스 상태 확인
- **Process inspection**: PID, 부모 프로세스, 윈도우 핸들 등
- **Log/telemetry files**: 실행 추적 로그 분석
- **State persistence**: 상태 저장 파일 검사

### 8. Integration & Data Flow Tracing
- **Source point**: 데이터/값이 생성되는 지점
- **Forwarding path**: 데이터가 전달되는 경로
- **Consumption point**: 데이터가 사용되는 지점
- **Persistence mechanism**: 데이터가 저장/추적되는 방식
- **End-to-end verification**: 전체 흐름 검증

### 9. Hypothesis & Validation
- **Assumption testing**: 가정의 맞고 틀림을 확인
- **Counterexample search**: 가정을 깨는 경우 찾기
- **Edge case analysis**: 경계 조건 검증
- **Regression checking**: 변경이 기존 코드 깨뜨렸는지 확인

### 10. Collaboration & Planning
- **Team consultation**: 다른 팀원의 경험 활용 (트라이드, 옵스)
- **Planning agent**: 복잡한 문제 분해 및 계획 수립
- **Skill discovery**: 유사 문제의 기존 스킬 확인

---

## 4-Phase Problem-Solving Loop

### Phase 1: Problem Definition (문제 명확화)
**무엇을 찾고 있는가?**

```
1. 증상 기술: 어떤 동작이 잘못되는가?
2. 예상 vs 현제: 뭐가 되어야 하고, 지금 뭐가 되는가?
3. 범위 한정: 어느 부분의 문제인가? (launcher? CDP? Core?)
4. 오류 메시지: 정확한 에러 로그/메시지는?
```

**도구**: Read (기본 코드 검토), WebSearch (비슷한 사례)

---

### Phase 2: Multi-Angle Search (여러 각도에서 검색)

#### 검색 전략 (깊이별)
- **Shallow** (빠른 진단, ~5분): git log + grep + 1-2 파일
- **Medium** (정상적 조사, ~30분): git history + grep + Agent + 3-5 파일 + 문맥 분석
- **Deep** (근본 원인, ~60분): 전체 코드베이스 탐색 + 웹 검색 + 트라이드 사용

#### 5가지 검색 각도

1. **Version Control Angle**
   ```bash
   git log --grep="keyword" --all
   git log --oneline -p -- path/to/file
   git show <commit>
   ```
   → 과거에 이 문제를 본 적이 있나? 어떻게 고쳤나?

2. **Reference Implementation Angle**
   ```bash
   grep -r "pattern" --include="*.cs"
   grep -r "function_name" -- csharp/src/
   ```
   → 비슷한 문제를 해결한 다른 코드가 있나?

3. **Environment & Data Flow Angle**
   ```
   - 값이 어디서 생성되는가? (source)
   - 값이 어디로 전달되는가? (forwarding)
   - 값이 어디서 사용되는가? (consumption)
   - 값이 어디 저장되는가? (persistence)
   ```

4. **Process/System Angle** (Windows/OS level)
   ```
   - GetForegroundWindow() vs GetParentProcessId()
   - MainWindowHandle vs Console window vs Foreground
   - Process chain vs single parent
   ```

5. **Web/External Knowledge Angle**
   ```bash
   WebSearch "Windows caller window detection"
   WebSearch "parent process HWND C# GetProcessById"
   ```

---

### Phase 3: Hypothesis & Validation (가설 세우고 검증)

```
1. 초기 가설: "GetForegroundWindow()를 사용하면 되겠다"
   → 검증: 아니다, 현재 활성 윈도우를 반환한다.
   
2. 수정 가설: "부모 프로세스의 MainWindowHandle을 사용하자"
   → 검증: 모든 부모가 window를 가지나? No.
   
3. 정제 가설: "부모 체인을 걸어가며 첫 번째 valid MainWindowHandle을 찾자"
   → 검증: 모니터/DWM/off-screen 상태는? 추가 validation 필요.

4. 최종 해결책: Full chain walk + comprehensive validation + state tracking
```

**도구**: Read, Grep, Git, Agent (탐색), WebSearch

---

### Phase 4: Integration & Persistence (완전성 검증)

```
1. Detection: 값이 제대로 감지되는가?
   └─ Program.cs 라인 234-258 확인

2. Forwarding: 값이 올바르게 전달되는가?
   └─ EyeCmdPipeClient.cs, CoreRunner.cs 확인
   └─ WKAPPBOT_CALLER_HWND 환경변수 확인

3. Consumption: Core에서 제대로 사용하는가?
   └─ ComputePlacementNearCaller 호출 확인
   └─ TryMoveWebBotNearCaller 실행 확인

4. Persistence: 상태가 기록되는가?
   └─ cdp-state.jsonl 생성 확인
   └─ LAUNCH JSON fg/fgT 필드 확인

5. End-to-End: 전체 흐름이 작동하는가?
   └─ Build + Deploy + Smoke test
   └─ Edge cases (multi-monitor, DWM cloaked, off-screen)
```

**도구**: Build, PowerShell/Bash test, Read (persistence files), Agent 검증

---

## 실제 사례: caller window 감지 문제

### Phase 1: Problem Definition
```
증상: 앱봇이 자신의 창을 fg/fgT로 전달해야 하는데 남의 창을 전달
예상: launcher → 자신을 호출한 terminal/IDE의 hwnd
현제: launcher → 현재 활성화된 윈도우 (Chrome, VSCode, 전혀 다른 앱)
문제 위치: Program.cs getfg/fgT 생성 부분
```

### Phase 2: Multi-Angle Search

**각도 1: Git History**
```bash
git log --oneline --all | grep -i "caller\|window\|hwnd"
git show dd10359f  # "place WebBot next to validated caller window"
```
→ 이미 다른 곳에서 비슷한 문제를 해결했다는 힌트

**각도 2: Reference Implementation**
```bash
grep -r "GetParentProcessId" csharp/src/
grep -r "MainWindowHandle" csharp/src/
```
→ MyCdpContext.cs에서 포괄적 검증 발견
→ EyeCmdPipeClient.cs에서 다른 구현 발견

**각도 3: Process Chain Logic**
```
GetForegroundWindow()는 global state
→ 앱봇이 foreground를 읽는 시점에 따라 다른 윈도우 반환 가능
→ 대신: 부모 프로세스 체인을 따라가면 launcher를 호출한 프로세스 찾기
```

**각도 4: Validation Flow**
```bash
grep -r "IsWindowOffScreen\|IsWindowCloaked\|ValidateCallerHwnd"
```
→ 이미 validation 로직이 준비되어 있음

**각도 5: Web Search**
```
"Windows process parent HWND C#"
"GetParentProcessId main window"
```
→ 비슷한 구현 패턴 확인

### Phase 3: Hypothesis & Validation

```
처음: GetForegroundWindow() 사용 ❌
피드백: "포그라운드가 어떻기 니 창이냐" (no, that's whoever is active)
수정: GetParentProcessId(parentPid) 체크 ❌ (only 1 level)
피드백: "부모 하나만 찾아보고 끝?" (no, walk full chain)
수정: Process chain walk (10 iterations) ✓
최종: chain walk + validation + forwarding + persistence ✓
```

### Phase 4: Integration & Persistence

```
✓ Detection: Program.cs 234-258 process chain walk
✓ Validation: MyCdpContext.cs comprehensive checks
✓ Forwarding: CoreRunner.cs WKAPPBOT_CALLER_HWND
✓ Persistence: cdp-state.jsonl logging
✓ Placement: TryMoveWebBotNearCaller
```

---

## 스킬 사용 패턴

### 빠른 진단 (Shallow)
```bash
wkfind <symptom> --depth shallow
# → git log + grep + basic Read
# 시간: ~5분
```

### 정상적 조사 (Medium)
```bash
wkfind <symptom> --depth medium
# → git history + multi-angle grep + Agent + web search
# 시간: ~30분
```

### 근본 원인 분석 (Deep)
```bash
wkfind <symptom> --depth deep --include-web
# → 전체 코드베이스 + 트라이드 상담
# 시간: ~60분
```

---

## 핵심 원칙

1. **Problem Definition First**: 문제를 제대로 이해하기 전에 코드를 고치지 말 것
2. **Multi-Angle Search**: 한 각도에서만 보지 말고, 여러 관점에서 동시 검색
3. **Hypothesis Testing**: 가정이 맞는지 검증, 틀리면 수정
4. **End-to-End Tracing**: source에서 consumption까지 전체 흐름 확인
5. **Persistence Verification**: 문제가 정말 고쳐졌는지 로그/상태로 확인
