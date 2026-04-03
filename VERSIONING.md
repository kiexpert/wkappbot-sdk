# WKAppBot Versioning Rules

## How patch version is calculated (auto, no tags needed)

`Directory.Build.props` sets `WKAppBotBaseVersion` (e.g. `4.6`).
At build time, the MSBuild target runs:

```
git log -1 -S "<WKAppBotBaseVersion>4.6</WKAppBotBaseVersion>" -- Directory.Build.props
```

This finds the **bump commit** — the commit that first introduced the current value.
Patch = number of commits since that bump commit.

So `4.6.3` means: bump commit + 3 more commits on top.

---

## ⚠️ Minor version bump — MUST commit these 3 files together

When bumping minor version (e.g. `4.6 → 4.7`):

| File | What to change |
|------|---------------|
| `csharp/Directory.Build.props` | `<WKAppBotBaseVersion>4.7</WKAppBotBaseVersion>` |
| `CLAUDE.md` | Header line: `# WKAppBot v4.7.0 - ...` |
| `VERSIONING.md` | Update version references if needed |

**If you skip the bump commit:** git pickaxe won't find the new version string
→ patch defaults to 0 and stays 0 forever until you commit the bump.
Features committed before the bump will show under the old minor version.

---

## Current version

`5.9` — bumped at 2026-04-04 (ask 다중질문 파이프라인: Q# per-tab 할당, per-page Slack thread 재사용, --intercept/--interrupt/--terminate, [An] 포맷 자동 재교육, agent 인터셉트 지원)

## Previous versions

| Version | Bumped | Summary |
|---------|--------|---------|
| `5.8` | 2026-04-01 | file tool compatibility aliases 확장, file-write 백업 정책 통일, LG overlay guard 일반화 |
| `5.7` | 2026-04-01 | AppBotPipe hot-swap lastError 레이스 수정, EnsureEyeWatchdogTask Process.Start 안정화, web open [WEB] TabID/Title/URL 출력 |
| `5.6` | 2026-03-31 | AppBotPipe 완전 통합 + FocusLaunchTracker, 워치독 VBS 강화, 핫스왑 워치독 비활성화, windows --cmd 필터, JSON5 멀티필드 grap 패턴 (`{hwnd,pid,title,cls,proc,domain,url}` AND 검색) |
