# WKAppBot MCP/ASK 운영 노하우 (후배용)

## 1) MCP 응답 프레이밍
- 원칙: `stdout`은 JSON-RPC 완성 메시지 단위만 출력.
- 긴 작업은 `notifications/message`로 중간 진행상태를 보내고, 마지막에 `id`가 있는 최종 `result`를 반환.
- 텍스트를 flush 조각으로 내보내면 클라이언트 파싱이 깨짐.

## 2) 하이브리드 실행 전략
- 빠른 액션: 인프로세스 실행 (기존 방식 유지).
- 느린 액션(`ask-*`, `wait` 등): 외부 프로세스 분리 + stdout/stderr 파이핑 + progress notification.
- 이유: 응답성/격리/장시간 안정성.

## 3) ASK-Gemini 이슈 대응
- `stop` 버튼이 보일 때는 전송 재클릭 금지(중지 버튼 오클릭 방지).
- `응답이 중지되었습니다/대답이 중지되었습니다/already stopped` 류 문구는 실패로 분기.
- 1회 재시도 후 동일 문구면 fast-fail.

## 4) 파일 첨부 중복 안내 대응
- 문구 예: `이미 이 파일을 업로드 했습니다`.
- 정책: 실패 처리하지 말고 첨부 성공으로 처리.
- 추가 동작: 닫기/확인 버튼 클릭 시도 + ESC 폴백으로 안내창 닫기.

## 5) 엑빌(a11y) 타겟팅 팁
- `*Chrome*`만 쓰면 잘못된 창(Codex 등)을 잡기 쉬움.
- 반드시 창 타이틀 힌트 + 탭 힌트 같이 사용:
  - 예) `*Google Gemini - Chrome*#gemini.google.com`
- 먼저 `windows --process chrome --all`로 실제 타이틀 확인 후 eval/inspect 수행.

## 6) build.cmd 안정화 루틴
- `--no-restore` 실패 시 restore 재시도.
- 배포 복사 실패 시:
  - 코어: kill-and-retry 1회 후 실패하면 `.new.exe` 스테이징.
  - 런처: 실패 시 `wkappbot.new.exe` 스테이징.
- 단일파일 기대치 유지 위해 잔여 dll/deps/runtimeconfig 정리.
- 마지막에 `eye tick` 트리거.

## 7) 실전 체크리스트
- 구현 후 반드시 4종 테스트:
  1. 정상(빠른 액션)
  2. 타임아웃/실패(isError)
  3. 중간 알림(notification) + 최종 result 프레이밍
  4. 회귀(기존 ask/gpt/gemini 경로)

