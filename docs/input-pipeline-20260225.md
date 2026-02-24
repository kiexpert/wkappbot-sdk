# Input Pipeline Update (2026-02-25)

## 배경
실전 사용 중(사용자 작업 병행) 입력 안정성을 높이기 위해 `wkappbot input` 기본 시도 순서를 비포커스 우선으로 재정의했습니다.

## 변경 사항
수정 파일:
- `csharp/src/WKAppBot.CLI/Commands/InputCommand.cs`

기본 순서 (`--method` 미지정):
1. Method 3: `WM_SETTEXT`
2. Method 4: `PostMessage`
3. Method 5: `Click+PostMsg` (hybrid)
4. Method 1: `Focus+WM_CHAR`
5. Method 2: `Click+VK-Type`

`--method N` 지정 시에는 기존처럼 해당 메서드만 수행합니다.

## 검증 원칙
- 1차: WM_GETTEXT / UIA
- 2차(보조): OCR

## 운영 포인트
- 실전 기본은 비포커스 우선
- 최종 폴백에서만 포커스 점유 입력
- 방해창/가림(top-window mismatch) 탐지 로깅 권장

## 검증 로그 (요약)
- 투혼 `cid=3780`에서 Method 3 단독 성공 사례 확인
- Focus+WM_CHAR는 동일 환경에서 실패 케이스 존재
- 결론: 비포커스 우선 정책 타당
