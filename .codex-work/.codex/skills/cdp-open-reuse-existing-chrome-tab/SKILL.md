---
id: cdp-open-reuse-existing-chrome-tab
app: wkappbot-webbot
description: "wkappbot cdp open <url> first checks for an alive Chrome registered to this project (via Window Property GetLaunchedPort, falling back to FindRunningChromePort over the per-project user-data-dir). When found, it skips ChromeLauncher.LaunchAsync entirely and lets ConnectCdp drive Target.createTarget into a NEW TAB on the existing instance. Only when no live registration is found does it fall through to a fresh Chrome launch. Prevents the duplicate-Chrome-on-9222/9223/9224 storm reported 2026-05-05."
tags: [cdp, open, reuse, tab-creation, Target.createTarget, chrome-launcher, duplicate-window-fix]
---

> **Refresh**: `wkappbot skill read cdp-open-reuse-existing-chrome-tab --if-newer` — v1.5 (2026-05-06)

# cdp open Reuses Existing Chrome and Opens URL as New Tab

## Steps

1. WebOpenCommand entry: set ChromeLauncher.DefaultUserDataDirBase=DataDir and ChromeLauncher.ProjectHash=ProjectRoot.Hash8() so registry lookups see the per-project bucket.
2. Reuse probe (priority order): 1) ChromeLauncher.GetLaunchedPort() reads Window Property WKAPPBOT_CDP_PORT_<hash8> on parent console/desktop HWND. 2) Fallback ChromeLauncher.FindRunningChromePort(userDataDir) does WMI/CDP scan tied to this project's user-data-dir.
3. Each candidate port verified live via ChromeLauncher.IsPortActiveAsync; dead ports drop out. WMI/CDP fallback re-binds port via RegisterLaunchedPort so subsequent calls take the fast Window-Property path.
4. Reuse path: skip LaunchAsync. Print 'Reusing Chrome on port N'. Then ConnectCdp(port, navigateUrl=url) calls GetOrCreateSandboxedTabAsync, which uses Target.createTarget when no host-matching tab exists -- new TAB on existing Chrome, not new process.
5. Launch path: only when both reuse probes return 0 (no live Chrome for this project). Falls through to ChromeLauncher.LaunchAsync(port, url) with SHA256-derived port (range 9300-9995, 4-port block per project CWD).
6. --new-window opt-out: skips reuse probe, forces new Chrome process. Reserved for isolated-process scenarios.
7. Port isolation: ports are SHA256-derived from git project root CWD. Each project gets a fixed 4-port block (e.g. 9300-9303). Never hardcode port numbers -- always use cdp open output.
8. ROOT CAUSE (2026-05-26): Chrome multiplication bug in FindRunningChromePortAny. When port file expired/missing, every cdp open call creates a new Chrome because the port-file guard (registered!=port) evaluates 0!=foundPort=true and skips all Chrome processes. Fix committed to Core: change guard to (registered>0 && registered!=port) - missing port file = recovery mode, accept any Chrome under ourBase prefix.
9. HARNESS BUILD PATTERN: wkappbot exec is the correct path to run dotnet/external tools when wk-only-gate blocks direct shell commands. Use: wkappbot exec 'C:/Program Files/dotnet/dotnet' publish <proj> -c Release
