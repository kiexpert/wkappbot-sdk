# InputProbe

입력 파이프라인 검증용 경량 프로브입니다.

## 목적
- 다중 Notepad/방해창 상황에서 타겟 창 선택 검증
- 포커스/비포커스 입력 경로 실험
- AppBot `input` 이식 전 안전한 실험 공간 제공

## 실행
```powershell
D:\SDK\dotnet\dotnet.exe run --project D:\GitHub\WKAppBot\tools\InputProbe\InputProbe.csproj -- "메모장" "HELLO WORLD"
```

옵션:
- `--cx N --cy N`: 클릭 좌표 오프셋

## 타겟 선택 정책
1. title substring 일치 Notepad 우선
2. 없으면 Z-order 상단 Notepad 선택
3. 선택 결과를 `[TARGET] hwnd=... title=...`로 로그 출력

## 입력/검증 정책
- 우선 SendKeys 경로 시도
- 텍스트 컨트롤(Edit/RichEdit/TextBox) 탐색 후 GETTEXT 확인
- 필요 시 WM_SETTEXT 폴백
- 최종 `verified=ok/fail` 출력

## 보관본
- `Program.success.20260225.cs.bak`: 성공 시점 스냅샷 코드
  - 컴파일 제외 목적의 `.bak` 확장자 유지
