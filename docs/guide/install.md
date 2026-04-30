# 설치 가이드

## 시스템 요구사항

| 항목 | 요건 |
|------|------|
| OS | Windows 10 22H2+ 또는 Windows 11 (64-bit) |
| Git | [git-scm.com](https://git-scm.com/download/win) |
| GitHub CLI | [cli.github.com](https://cli.github.com/) — 유료 티어 필수, 무료 티어 불필요 |
| .NET SDK | 8.0+ (소스 빌드 시) — [dot.net](https://dot.net) |

---

## Option A — 클론 & 빌드 (권장)

```bat
git clone https://github.com/kiexpert/wkappbot-sdk %USERPROFILE%\Documents\wkappbot
cd %USERPROFILE%\Documents\wkappbot
build.cmd
```

`build.cmd`는 사전 빌드된 core 바이너리를 다운로드하고 launcher를 소스에서 빌드합니다.

::: tip 왜 Documents?
WKAppBot은 사용 기록을 학습해 `bin\wkappbot.hq\`에 UI Experience DB를 저장합니다.
`Documents\` 아래에 두면 개인 데이터가 버전 관리 경로와 분리됩니다.
:::

---

## Option B — 릴리즈 ZIP 다운로드

1. [최신 릴리즈](https://github.com/kiexpert/wkappbot-sdk/releases/latest)로 이동
2. `wkappbot-X.Y.Z.zip` 다운로드
3. `%USERPROFILE%\Documents\wkappbot`에 압축 해제

---

## PATH 설정

```powershell
# PowerShell (현재 사용자 영구 설정)
[Environment]::SetEnvironmentVariable(
  'PATH',
  "$env:USERPROFILE\Documents\wkappbot\bin;$([Environment]::GetEnvironmentVariable('PATH','User'))",
  'User'
)
```

터미널을 재시작한 후 확인:

```bash
wkappbot --version
# WKAppBot v6.0.0 (core: 6.0.0)

wkappbot skill list
# 설치된 스킬 목록 출력
```

---

## 디렉토리 구조

```
%USERPROFILE%\Documents\wkappbot\
  bin\
    wkappbot.exe          ← launcher / busybox 진입점 (MIT)
    wkappbot-core.exe     ← core 워커 (핫스왑으로 무중단 업데이트)
    a11y.exe              ← wkappbot.exe busybox 심링크
    wkappbot.hq\          ← 런타임 HQ: skills, logs, experience DB (자동 생성)
      experience\         ← UI 요소 학습 캐시
      skills\             ← 설치된 스킬
      logs\               ← 운영 로그
  csharp\src\             ← launcher 소스 (MIT)
  docs\                   ← 이 문서
  handlers\               ← 커스텀 핸들러 YAML
  skills\                 ← 프로젝트 스킬
```

---

## 업데이트

```bat
cd %USERPROFILE%\Documents\wkappbot
git pull
build.cmd
```

Eye가 실행 중이라면 `wkappbot-core.new.exe`를 자동 감지해 무중단 핫스왑합니다.

---

## 유료 라이선스 활성화 (CDP / Sudo)

무료 티어는 별도 설정 없이 동작합니다. CDP 브라우저 자동화·멀티 AI 위임·스케줄러를 쓰려면:

```bash
gh auth login              # GitHub 인증 (최초 1회)
wkappbot license status    # 현재 티어 확인
```

이후 [SUBSCRIBE.md](https://github.com/kiexpert/wkappbot-sdk/blob/main/SUBSCRIBE.md)의 안내에 따라 구독하면 1시간 이내 자동 활성화됩니다.

티어별 기능 비교는 [요금 페이지](/pricing)를 참고하세요.
