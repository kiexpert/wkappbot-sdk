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

`4.7` — bumped at 2026-03-20 (Eye launch bug fixes, PulseStep profiler)
