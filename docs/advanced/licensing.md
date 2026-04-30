# 라이선스 시스템

## 오픈코어 모델

WKAppBot은 오픈코어 모델을 따릅니다:

| 컴포넌트 | 라이선스 | 소스 |
|----------|----------|------|
| `wkappbot.exe` (launcher) | MIT | 공개 (`csharp/src/WKAppBot.Launcher/`) |
| `wkappbot-core.exe` (core) | Proprietary | 클로즈드 소스 |

Launcher는 자유롭게 사용·수정·배포할 수 있습니다. Core는 개인 사용 무료, 상업 사용은 구독 필요.

---

## GitHub 기반 인증

별도의 라이선스 파일이나 서버 없이 **GitHub 협력자 권한**으로 티어를 결정합니다:

```
사용자 결제 (KIS 이체)
  → 오너가 kiexpert/wkappbot-sdk에 협력자 초대
  → wkappbot 시작 시 `gh api user/memberships` 호출
  → 권한 레벨 확인 → 티어 언락
```

| GitHub 권한 | WKAppBot 티어 |
|------------|--------------|
| 없음 (공개 레포 접근) | Free |
| `read` | CDP |
| `write` | Sudo |

오프라인 유예 기간: 마지막 성공 확인으로부터 **7일**.

---

## 티어별 기능

| 기능 | Free | CDP | Sudo |
|------|:----:|:---:|:----:|
| `a11y` (UIA / Win32 / Android) | ✅ | ✅ | ✅ |
| `inspect`, `find`, `highlight` | ✅ | ✅ | ✅ |
| `run` YAML 시나리오 | ✅ | ✅ | ✅ |
| `skill` 시스템 | ✅ | ✅ | ✅ |
| `speak` (TTS) | ✅ | ✅ | ✅ |
| `file`, `logcat`, `gc` | ✅ | ✅ | ✅ |
| **CDP 브라우저 자동화** | — | ✅ | ✅ |
| **`ask` (triad / gpt / gemini)** | — | ✅ | ✅ |
| **`schedule`** | — | ✅ | ✅ |
| **`--sudo` 관리자 접근** | — | — | ✅ |

---

## 라이선스 확인

```bash
gh auth login              # 최초 1회 GitHub 인증
wkappbot license status
```
```
tier: CDP
github: kiexp  permission: read
expires_at: 2026-05-30
offline_grace: 7 days remaining
```

---

## 오프라인 서명 라이선스

GitHub 네트워크 없이도 동작하는 ECDSA 서명 라이선스를 발급할 수 있습니다:

```json
{
  "userId":    "6f8b3c-...",
  "userEmail": "user@example.com",
  "tier":      "Pro",
  "expiresAt": "2027-01-01T00:00:00Z",
  "features":  ["VisionApi", "AndroidAdb"],
  "signature": "<ECDSA-P256-base64url>"
}
```

`%USERPROFILE%\.wkappbot\license.json`에 배치하면 GitHub 확인 없이 오프라인으로도 인증됩니다.

발급 도구:

```bash
dotnet script tools/gen-license.csx -- \
  --user-id  "6f8b3c-..." \
  --email    "user@example.com" \
  --tier     Pro \
  --expires  2027-01-01 \
  --features VisionApi,AndroidAdb \
  --priv     <PKCS8-base64-private-key> \
  --out      user.license.json
```

---

## Third-party 컴포넌트

전체 의존성 목록:

```bash
dotnet list package --include-transitive
```

주요 구성:

| 패키지 | 라이선스 |
|--------|----------|
| .NET 8 Runtime | MIT |
| (기타 NuGet 패키지) | [THIRD-PARTY-NOTICES.md](https://github.com/kiexpert/wkappbot-sdk/blob/main/THIRD-PARTY-NOTICES.md) 참고 |
