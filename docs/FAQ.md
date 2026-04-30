# 자주 묻는 질문

## 설치 & 설정

**Q. `wkappbot` 명령이 인식되지 않아요.**

PATH 설정 후 터미널을 재시작했는지 확인하세요:
```powershell
[Environment]::SetEnvironmentVariable('PATH',
  "$env:USERPROFILE\Documents\wkappbot\bin;$([Environment]::GetEnvironmentVariable('PATH','User'))",
  'User')
```
새 PowerShell 또는 cmd 창을 열고 `wkappbot --version`을 실행하세요.

**Q. build.cmd 실행 시 .NET SDK 오류가 납니다.**

.NET SDK 8.0 이상이 필요합니다: [https://dot.net](https://dot.net)

---

## 자동화

**Q. core binary가 클로즈드 소스인 이유가 뭔가요?**

UIA 퀴크, MFC 우회, CDP 엣지 케이스, HTS 거래 터미널 동작에 관한 수년간의 노하우가 담겨 있습니다. Launcher(글루 레이어)는 완전히 오픈소스로 누구나 감사할 수 있습니다.

**Q. 데이터를 수집하나요?**

Experience DB(`bin\wkappbot.hq\experience\`)는 UI 요소 위치를 로컬에만 캐싱합니다. 외부 서버로 전송되는 것은 없습니다. 로그도 `bin\wkappbot.hq\logs\` 로컬에만 저장됩니다.

`WKAPPBOT_NO_TELEMETRY=1`로 모든 선택적 텔레메트리를 비활성화할 수 있습니다.

**Q. UIA를 지원하지 않는 앱은 어떻게 자동화하나요?**

WKAppBot은 5-Tier 검색으로 자동 폴백합니다: UIA → Vision Cache → OCR → Vision API → 좌표 기반. MFC/HTS 거래 터미널 같은 레거시 앱은 Win32 PostMessage로 지원합니다.

**Q. Android 자동화도 되나요?**

네. `adb://device/*element*` grap 문법으로 지원합니다. PATH에 `adb`가 있어야 하고, USB 디버깅이 활성화되어 있어야 합니다:
```bash
wkappbot a11y find "adb://Fold5/*balance*"
```

**Q. Windows Server에서 동작하나요?**

Windows Server 2019+는 헤드리스 모드로 지원됩니다. GUI 자동화(`a11y`)는 데스크탑 세션(RDP 또는 콘솔)이 필요합니다. Eye 데몬은 RDP 위에서 동작합니다.

---

## 라이선스 & 구독

**Q. 구독이 만료되면 어떻게 되나요?**

다음 `wkappbot` 시작 시 Free 티어로 자동 복귀합니다. 기본 자동화는 계속 동작하고 CDP/Sudo 기능만 비활성화됩니다.

**Q. Slack 봇 없이도 브라우저 자동화가 가능한가요?**

네. Eye와 Slack은 선택사항입니다. `wkappbot a11y`와 `wkappbot ask`는 독립적으로 동작합니다. Eye는 상주 데몬 모드와 Slack 연동을 제공합니다.

---

## 기여

**Q. 어떻게 기여할 수 있나요?**

Launcher 소스는 MIT 라이선스로 PR을 환영합니다. Core는 클로즈드 소스이지만, 스킬 작성과 핸들러 YAML 파일을 통해 기여할 수 있습니다:
```bash
wkappbot skill contribute --app my-app --title "내 노하우" --desc "설명"
```

**Q. 버그 리포트는 어디에 하나요?**

[GitHub Issues](https://github.com/kiexpert/wkappbot-sdk/issues)를 이용해주세요.
