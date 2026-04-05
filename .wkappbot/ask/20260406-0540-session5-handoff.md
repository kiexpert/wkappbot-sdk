이전 세션이 컨텍스트 한계에 도달하여 자동 인수인계합니다. CLAUDE.md와 MEMORY.md를 먼저 읽고 이어서 작업해주세요.

## 세션 5 실적 (커밋 25개, v5.11.37→v5.11.73)

### hack-hover 대폭 개선 (핵심!)
- 라이브 오버레이: 루트 창 크기, FindAllDescendants 전체 UIA 노드 점선 표시
- 타겟 부모 체인: 2배 두께 네온 점선 강조 (TreeWalker.GetParent)
- blind 윈도우 즉시 핵: UIA 없음 OR 경험DB 없음 → debounce 없이 즉시 분석 (취소 불가)
- 경험DB FSW: experience/ FileSystemWatcher → 파일 변경 즉시 오버레이 갱신
- 핫스왑: TryRenameSwap 5초마다 체크 → Launcher 경로로 재spawn
- 콘솔 출력: 매 틱 출력 (Eye에서는 변경+분석만), richGrap 기준 \r 덮어쓰기
- 스크린리더: 시작 시 ScreenReaderMode.Enable() 자동 호출
- 오버레이 atomic swap: 새 콘텐츠 준비 후 일괄 교체 (깜빡임 제거)

### TypeViaIme — focusless IME 텍스트 입력 (신규!)
- IMM32: AttachThreadInput + ImmSetCompositionString + CPS_COMPLETE
- a11y type Tier 2로 통합: UIA Value → **IME** → LegacyIA → WM_CHAR → SendKeys
- 상태 백업/검증/복구: 기존 composition 저장, 실패 시 CPS_CANCEL + 원복
- Win32/MFC 네이티브 앱 대상 (Electron 제외 — TSF 경로)
- 미테스트! 다음 세션에서 메모장/HTS 실테스트 필요

### hack-input 워커 (신규)
- a11y hack-input: UIA GetFocusedElementInfo 기반 키보드 포커스 분석
- 패턴(Value/Text/Invoke/Toggle), grap, 입력 방법, 부모 체인 표시

### auto-hack 프로세스 격리
- Eye 내 A11yHackCommand 직접 호출 → AppBotPipe.Spawn으로 분리
- Vision/CCA DLL이 Eye에 로드되지 않아 메모리 누수 방지

### 기타
- CDP EvalAsync 3회 retry + backoff
- pump watchdog 5s + 2차 감소 확인
- 오버레이 반투명 필 전면 제거, Known/Cached 알파 10%
- CommandHelpMap에 speak/hack/hack-hover/hack-input 등록
- A11yHackCommand UTF-8 인코딩 강제 + Unicode × → ASCII x

### 서제스트 처리
- 13건 → 7건 (6건 resolve: IME, pump watchdog, Eye CWD, CDP timeout, speak help, hack-hover CWD + CWD merge)
- 남은 7건: reflection help, DWM Phase2, triad backlog, pixel canvas, CCA LayoutMetrics, self-healing, multi-browser CDP

## 미완료/다음 세션 TODO
1. **TypeViaIme 실테스트**: 메모장, HTS 영웅문, 계산기에서 실제 텍스트 입력 검증
2. **hack-hover 오버레이 버그**: 박스 위치 어긋남(DPI?), 라벨 겹침, 너무 많은 박스 필터링
3. **hack-hover 인코딩**: UIA 한글 이름이 깨지는 문제 (UTF-8 강제 추가했으나 재확인)
4. **오버레이에 경험DB 데이터 표시**: 현재 UIA 노드만 표시 — 경험DB 캐시된 레이아웃도 오버레이
5. **서제스트 7건**: 대형 기능들 (self-healing, multi-browser CDP 등)
6. **hack-hover 핫스왑 검증**: Launcher 경로로 spawn — 실제 동작 확인
