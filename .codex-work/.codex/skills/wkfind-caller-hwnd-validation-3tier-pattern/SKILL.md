---
id: wkfind-caller-hwnd-validation-3tier-pattern
app: wkappbot
description: "Complete pattern for Windows process->window mapping. Solves GetForegroundWindow() gotcha with 3-tier priority (console>host>foreground) and 8-point edge case validation. Covers multi-monitor, DWM cloak, PseudoConsoleWindow rejection, foreign-process guard, RECT marshalling bug fix, WT multi-tab support."
tags: [caller-window, hwnd-validation, process-chain, multi-monitor, windows-terminal, chrome-placement, wkfind, edge-cases]
---

> **Refresh**: `wkappbot skill read wkfind-caller-hwnd-validation-3tier-pattern --if-newer` — v2.24 (2026-05-15)

# wkfind: Caller HWND validation - 3-tier priority cascade pattern

## Steps

1. GetForegroundWindow() Gotcha: returns current foreground app, NOT the launcher caller. Correct model: Caller HWND = process ancestor -- who spawned us? 3-tier priority: console HWND > host HWND > foreground.
2. Environment Variable Forwarding: persist caller HWND via WKAPPBOT_CALLER_HWND env (CoreRunner CreateProcessW). ChromeLauncher.TryMoveWebBotNearCaller() uses SWP_NOZORDER|SWP_NOACTIVATE to avoid focus steal.
3. Logging: every validation logged to cdp-state.jsonl -- [VALIDATION] hwnd=0x... class=... verdict=on_screen/off_screen/invalid. Use to diagnose placement failures.
4. SEE ALSO: wkappbot skill read standard-appbot-window -- definitive reference for Standard AppBot Window detection, IPC, ConPTY, multi-tab, iconified, placement.
5. 2026-05-18: CHROME:WARN CALLER_UNDETECTABLE added in ChromeLauncher.CallerRecovery.cs (commit 5383b02e9) -- all 3 recovery tiers failed.
6. 2026-05-20 DONE: IDE CWD mismatch (suggest 2026-05-14T18:26:23) fully fixed. GetNonIdeProcessCwd() applied at all 3 sites: WebCommands.Part1.cs L38+L90, AskCommands.Entry.Cdp.cs L295. Helper: WebCommands.Diagnostics.cs:128. Suggest RESOLVED.
7. 2026-05-20 FIX: Chrome-class caller rejection. TryValidatePlacementCallerWindow (ChromeLauncher.Win32.cs:99) now rejects any hwnd whose class starts with Chrome_ -- Chrome_RenderWidgetHostHWND was passing all 6 checks and becoming CallerHwndForPlacement, causing new Chrome to open near existing Chrome instead of the caller terminal.
8. 2026-05-20 FIX: RenderWidgetHostHWND walk-up. NormalizeCallerHwndForPlacement now walks Chrome_RenderWidgetHostHWND -> GetAncestor(GA_ROOT) -> Chrome_WidgetWin_1 before validation. Required for a11y --eval-js on grap from cdp open.
9. NOTE: a11y --eval-js requires Chrome_WidgetWin_1 (top-level) not Chrome_RenderWidgetHostHWND (renderer child). cdp open returns renderer hwnd for per-tab ID; use NormalizeCallerHwndForPlacement or GetAncestor to get top-level.
10. DIAGNOSIS 2026-05-20: MyCdpContext.CallerValidation.cs:89-93 (wkappbot-sdk) allows Chrome as valid caller in KnownHostProcessNames. Launcher passes Chrome hwnd as CallerHwndForPlacement via env var before Core Chrome_ reject fix can intercept. This is the root cause of webbot following focused Chrome window.
11. FIX SPEC (2026-05-20): MyCdpContext.cs lines 220-225 remove GetForegroundWindow fallback block entirely. MyCdpContext.CallerValidation.cs line 93 remove Chrome exception from IsKnownHostProcess. EyeCmdPipeClient.cs add GetAncestorPidChain() helper. Error block: log to cdp-state.jsonl + submit suggest + exit(2).
12. READY TO FIX: wkappbot-sdk MyCdpContext.cs remove GetForegroundWindow fallback block (lines 212-225), add no_caller hard exit with ancestor chain dump + cdp-state.jsonl append + suggest submit. CallerValidation.cs remove Chrome exception line 93. EyeCmdPipeClient.cs add GetAncestorPidChain() helper.
13. GetForegroundWindow hook spec: FocusGuard class in HelpAndAliases.cs. Rename DllImport to GetForegroundWindowRaw. Add wrapper GetForegroundWindow() that checks StackTrace frames for 'cdp' in DeclaringType.FullName or Method.Name (OrdinalIgnoreCase). If found: Console.Error.WriteLine + Process.Start wkappbot suggest BUG + return hwnd.
14. CLAUDE.md GUARD added 2026-05-20 (commit 311fda436): before any callerHwnd/placement change, must read standard-appbot-window + standard-chrome-window + this skill. 5 absolute rules documented.
15. CORRECTION (2026-05-21): Step 1's 3-tier priority (console>host>foreground) is OBSOLETE. Canonical rule per standard-appbot-window: P2 ANCESTOR WALK ONLY. NO foreground fallback (P1/P3 removed in 08b0b423). If P2 finds no usable ancestor window, return IntPtr.Zero -- caller MUST skip placement (Chrome stays at last position). Do NOT fall back to GetForegroundWindow, do NOT fall back to largest terminal.
16. CORRECTION (2026-05-21): ConPTY ACCEPTANCE -- PseudoConsoleWindow class with IsWindowVisible=false MUST be accepted in P2 ancestor walk. No class restriction, no visibility filter. ConPTY is the nearest ancestor when wkappbot runs in Windows Terminal -- rejecting it breaks placement. For rect provider in placement code: use GetAncestor(hwnd, GA_ROOT) to get the visible root (WindowsTerminal main HWND); keep original ConPTY hwnd as identity for input injection.
17. CORRECTION (2026-05-21): GA_ROOT FOR HIDDEN HWND RECT -- when hwnd is hidden (IsWindowVisible=false, e.g. ConPTY), rect for placement coords MUST come from GetAncestor(hwnd, GA_ROOT). Pattern: hForRect = IsWindowVisible(hwnd) ? hwnd : GetAncestor(hwnd, GA_ROOT). Without this, Chrome lands at ConPTY's zero-area or wrong rect.
18. CORRECTION (2026-05-21): LARGEST-TERMINAL FALLBACK IS FORBIDDEN. sdk ResolveValidCallerWindow's EnumWindows largest-terminal fallback is non-deterministic (depends on user resizing windows) -- filed as suggest 2026-05-21T08:27. Correct behavior: P2 ancestor walk finds nothing -> return IntPtr.Zero -> caller skips placement. Never pick a 'best guess' window the user did not launch wkappbot from.
19. CORRECTION (2026-05-21): GetConsoleWindow() RECT-VALIDATE BEFORE USE. ChromeLauncher.LaunchPreparation.cs previously used GetConsoleWindow() result without rect validation -- could be zero rect or invalid. Required: call GetWindowRect, check non-zero area, drop if invalid. Do NOT fall back to primary monitor center -- skip placement instead.
