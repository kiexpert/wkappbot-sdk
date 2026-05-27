---
id: standard-chrome-window
app: wkappbot
description: "Chrome-side parallel to standard-appbot-window. How to correctly identify and move the Chrome browser window for CDP placement. Covers tab vs window, multi-tab, non-active tab, iconified Chrome, and all edge cases. Read alongside standard-appbot-window."
---

> **Refresh**: `wkappbot skill read standard-chrome-window --if-newer` — v1.4 (2026-05-15)

# Standard Chrome Window: CDP tab to root browser window, multi-tab, iconified, placement

## Steps

1. CHROME TAB IS NOT THE ROOT WINDOW: A CDP target (tab/page) is a child of the Chrome browser window. Browser.getWindowForTarget({targetId}) resolves from tab -> root OS browser window. Always pass targetId explicitly (cdp.TargetId) -- not relying on default session context which may return wrong window in multi-session setups.
2. MULTI-TAB: Chrome window has Tab A (active) and Tab B (our CDP target, e.g. ChatGPT). GetWindowForTarget({targetId: Tab B}) returns the ROOT BROWSER WINDOW containing Tab B -- correct regardless of which tab is active. Browser.setWindowBounds on that windowId moves the entire Chrome window (all tabs). No need to identify or switch to the active tab.
3. NON-ACTIVE TARGET TAB: Our CDP target may be in the background. GetWindowForTarget with the tab's targetId still returns the correct root window. SetWindowBounds moves that root window. The tab does not need to be active/focused for placement to work.
4. ICONIFIED (MINIMIZED) CHROME: Chrome ignores Browser.setWindowBounds while minimized. Fix: two-step in SetWindowBoundsAsync -- (1) setWindowBounds with windowState=normal first, (2) WaitForWindowStateAsync until normal confirmed, (3) then set actual bounds. Already implemented in CdpClient.Window2.cs SetWindowBoundsAsync. If cdp.GetWindowForTargetAsync returns 0x0 size (minimized state indicator), fall back to ChromeLauncher.GetEffectiveBounds() for expected size.
5. PLACEMENT FLOW: (1) Resolve caller window rect via FindRectProviderHwnd (see standard-appbot-window). (2) Verify caller on-screen via MonitorFromPoint. (3) GetWindowForTargetAsync(cdp.TargetId) -> windowId + current Chrome size. (4) Compute target position (caller.Right + gap, clamped to monitor rcWork). (5) SetWindowBoundsAsync(windowId, x, y, w, h) -- handles restore-from-minimized internally.
6. COORDINATE SYSTEM: SetWindowBoundsAsync calls ChromeLauncher.ClampBoundsToVisible last-line-of-defense. Never use coordinate sign for off-screen detection -- use MonitorFromPoint. Negative x is valid on left monitor. The clamp ensures Chrome lands on a valid monitor regardless of caller position.
7. BUGS FIXED: targetId now passed to GetWindowForTargetAsync (AskCommands.CallerPlacement.cs). SetWindowBoundsAsync 2-step minimize restore (CdpClient.Window2.cs). ClampBoundsToVisible last-resort clamp. WithAutoReconnectAsync on DOM calls (26dd30c8).
8. DPI AND CDP COORDINATE SPACE: Browser.setWindowBounds uses SCREEN coordinates -- Chrome accepts the same physical pixel coordinates as Win32 SetWindowPos. When PerMonitorV2 DPI-aware process calls GetWindowRect(callerHwnd) and passes result to Browser.setWindowBounds, coordinates land correctly because both use the same physical pixel coordinate space. No conversion needed UNLESS the process DPI awareness context changes between the GetWindowRect call and the CDP call. Verify: TrySetPerMonitorV2DpiAwareness is called at Launcher Main() entry (before any window rect call). Chrome reports its position via Browser.getWindowForTarget in the same physical coordinate space. CDP:MOVE log coordinates should match cdp-mon ACT-POS values. Discrepancy = DPI context mismatch.
9. DPI verified 2026-05-16: Browser.setWindowBounds accepts PHYSICAL pixels on Windows when caller process is PerMonitorV2-aware (matches Chrome's own context). wkappbot-core.exe now sets PerMonitorV2 via NativeMethods.TrySetPerMonitorV2DpiAwarenessOnce() at Run() entry. Launcher already did this. AskCommands.CallerPlacement.TryMoveChromeToCallerAsync caller-rect and SetWindowBoundsAsync target are in the same physical-pixel space.
