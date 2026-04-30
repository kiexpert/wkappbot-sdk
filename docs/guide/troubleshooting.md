# 트러블슈팅

## `wkappbot: command not found`

`bin\`이 PATH에 없어요. 터미널에서 실행 후 재시작:

```powershell
[Environment]::SetEnvironmentVariable('PATH',
  "$env:USERPROFILE\Documents\wkappbot\bin;$([Environment]::GetEnvironmentVariable('PATH','User'))",
  'User')
```

---

## 결제 후 라이선스가 Free로 표시

**GitHub 협력자 초대를 수락해야** 라이선스가 활성화됩니다.

1. <https://github.com/notifications> 또는 이메일에서 초대 확인
2. 초대 수락 후:

```bash
wkappbot license status   # CDP 또는 Sudo로 표시돼야 함
```

---

## Eye가 시작 안 됨 / `eye tick` 실패

로그 확인:

```bash
wkappbot logcat error --past 10m
wkappbot file glob "**/*.log" --path "bin\wkappbot.hq\logs"
```

주요 원인:
- 이미 다른 Eye 실행 중 → `wkappbot windows *eye*` 로 확인
- named pipe 충돌 → `wkappbot eye tick --timeout 3` 으로 타임아웃 테스트

---

## `build.cmd` 실패: `vswhere.exe not recognized`

Visual Studio Build Tools 2022 필요 (AOT 빌드):
<https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2022>

---

## `wkappbot-core.exe`가 빌드 후 없음

```bat
build.cmd --download-only
```

다운로드 실패 시 `build.cmd`가 소스 빌드로 폴백합니다 (.NET SDK 8+ 필요).

---

## CDP 명령이 막힘 (오버레이 표시)

CDP 기능은 CDP 티어 이상 필요:

```bash
wkappbot license status
```

Free 티어는 CDP 사용 1분 후 dismissible 오버레이가 표시됩니다.

---

## 터미널에서 한글이 깨짐

```bat
chcp 65001
```

또는 UTF-8 네이티브 지원하는 Windows Terminal 사용 권장.

---

## 버그 리포트용 진단 정보 수집

```bash
wkappbot --version
wkappbot eye tick --timeout 5
wkappbot logcat error --past 30m
```

[이슈 등록](https://github.com/kiexpert/wkappbot-sdk/issues/new)시 위 출력을 첨부해주세요.
