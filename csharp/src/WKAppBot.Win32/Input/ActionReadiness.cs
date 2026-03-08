using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using WKAppBot.Abstractions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Action-Aware Readiness (AAR) for Windows — implements IActionReadiness.
/// 4-stage pipeline: Global → Target Resolution → Occlusion → Action-Specific.
/// Wraps existing InputReadiness for blocker detection and method probing.
///
/// Return convention:
///   null        → blocked (hard fail)
///   == target   → success (proceed)
///   != target   → retarget (popup/modal found)
///
/// Tag: [AAR]
/// </summary>
public sealed class ActionReadiness : IActionReadiness
{
    private readonly InputReadiness _readiness;

    // Stability cache via Win32 SetProp/GetProp — attached to the target window handle
    // Survives across CLI invocations (each `wkappbot a11y` = new process), zero file I/O
    private const string StabilityPropName = "WKAppBot_AAR_StableTick";
    private const int StabilityCacheTtlMs = 5000; // 5초 이내 재액션 → 풀 프로브 스킵

    public ActionReadiness(InputReadiness readiness)
    {
        _readiness = readiness ?? throw new ArgumentNullException(nameof(readiness));
    }

    // ── IActionReadiness ─────────────────────────────────────────

    public IActionTarget? Ensure(string action, IActionTarget target, ReadinessContext ctx)
    {
        var sw = Stopwatch.StartNew();
        var act = action.ToLowerInvariant();

        // ── Pass-through actions (no readiness needed) ──
        if (IsPassThrough(act))
            return target;

        // ── Stage 0: Global Pre-Condition ──
        if (!CheckGlobal(act, out var globalReason))
        {
            Console.Error.WriteLine($"[AAR] BLOCKED (global): {globalReason}");
            return null;
        }

        // ── Stage 1: Target Resolution ──
        var stage1 = CheckTargetResolution(act, target, ctx);
        if (stage1 == null) return null;    // hard block
        if (stage1 != target) return stage1; // retarget

        // ── Stage 2: Occlusion / Blocker Detection ──
        var stage2 = CheckOcclusion(act, target, ctx);
        if (stage2 == null) return null;
        if (stage2 != target) return stage2;

        // ── Stage 2.5: Stability Sampling Probe (100ms 2-shot, file-cached) ──
        if (IsStabilityRequired(act))
        {
            var probeHwnd = ResolveHwnd(target, ctx);
            if (!TryStabilityCache(probeHwnd, out var ageMs))
            {
                // Full 100ms 2-shot probe
                if (!CheckStability(target))
                {
                    Console.Error.WriteLine($"[AAR] BLOCKED: target unstable (animating/transitioning) for {action}");
                    return null;
                }
                WriteStabilityCache(probeHwnd);
            }
            else
            {
                Console.Error.WriteLine($"[AAR] Stability cached ({ageMs}ms ago)");
            }
        }

        // ── Stage 3: Stale Element Detection ──
        if (target is UiaActionTarget staleCheck && !IsAlive(staleCheck))
        {
            Console.Error.WriteLine($"[AAR] BLOCKED: stale element for {action}: \"{target.DisplayName}\"");
            return null;
        }

        // ── Stage 4: Action-Specific Checks ──
        var stage4 = CheckActionSpecific(act, target, ctx);
        if (stage4 == null) return null;

        var elapsed = sw.ElapsedMilliseconds;
        Console.Error.WriteLine($"[AAR] OK: {action} on \"{target.DisplayName}\" ({elapsed}ms)");
        return stage4;
    }

    // ── Stage 0: Global Pre-Condition ────────────────────────────

    private static bool CheckGlobal(string action, out string reason)
    {
        reason = "";

        // Lock screen detection: no foreground window means locked/secure desktop
        var fg = NativeMethods.GetForegroundWindow();
        if (fg == IntPtr.Zero)
        {
            reason = "화면 잠김 (foreground window 없음)";
            return false;
        }

        return true;
    }

    // ── Stage 1: Target Resolution ──────────────────────────────

    private IActionTarget? CheckTargetResolution(string action, IActionTarget target, ReadinessContext ctx)
    {
        // 1a. Minimized window → focusless restore
        if (target.WindowState == "minimized" || IsMinimized(target, ctx))
        {
            var hwnd = ResolveHwnd(target, ctx);
            if (hwnd != IntPtr.Zero)
            {
                // SW_SHOWNOACTIVATE (8) = focusless restore
                NativeMethods.ShowWindow(hwnd, 8);
                Thread.Sleep(300);
                Console.Error.WriteLine($"[AAR] Restored minimized window: \"{target.DisplayName}\"");
            }
        }

        // 1b. Window-level target for element-level action → warn
        if (target.IsWindow && IsElementAction(action))
        {
            Console.Error.WriteLine($"[AAR] WARNING: {action} on window-level target \"{target.DisplayName}\" — intended?");
            // Warn only, don't block
        }

        // 1c. Disabled ancestor check
        if (!target.Enabled)
        {
            if (IsBlockOnDisabled(action))
            {
                Console.Error.WriteLine($"[AAR] BLOCKED: target disabled for {action}: \"{target.DisplayName}\"");
                return null;
            }
            Console.Error.WriteLine($"[AAR] WARNING: target disabled for {action}: \"{target.DisplayName}\"");
        }

        return target;
    }

    // ── Stage 2: Occlusion / Blocker ────────────────────────────

    private IActionTarget? CheckOcclusion(string action, IActionTarget target, ReadinessContext ctx)
    {
        var hwnd = ResolveHwnd(target, ctx);
        if (hwnd == IntPtr.Zero) return target; // can't check, pass through

        var mainHwnd = ctx.Hwnd != IntPtr.Zero
            ? ctx.Hwnd
            : NativeMethods.GetAncestor(hwnd, NativeMethods.GA_ROOT);

        // DetectBlocker (~5ms)
        var blocker = _readiness.DetectBlocker(mainHwnd);
        if (blocker == null) return target; // clear

        Console.Error.WriteLine($"[AAR] Blocker detected: [{blocker.ClassName}] \"{blocker.Title}\"");

        // close action: retarget to deepest child popup (maxDepth=3 + cycle guard)
        if (action is "close")
        {
            return ResolveDeepestBlocker(mainHwnd, blocker, ctx.MaxRetargetDepth);
        }

        // Try auto-dismiss
        var (handled, shouldRetry) = _readiness.TryDismissBlocker(mainHwnd, blocker);
        if (handled)
        {
            Thread.Sleep(300);
            // Re-check
            var recheck = _readiness.DetectBlocker(mainHwnd);
            if (recheck == null)
            {
                Console.Error.WriteLine("[AAR] Blocker dismissed successfully");
                return target;
            }
        }

        // Blocker persists — policy depends on action
        if (IsBlockOnOcclusion(action))
        {
            Console.Error.WriteLine($"[AAR] BLOCKED: persistent blocker for {action}");
            return null;
        }

        Console.Error.WriteLine($"[AAR] WARNING: blocker persists for {action}, proceeding anyway");
        return target;
    }

    // ── Stage 3: Action-Specific ────────────────────────────────

    private IActionTarget? CheckActionSpecific(string action, IActionTarget target, ReadinessContext ctx)
    {
        switch (action)
        {
            case "type" or "set-value":
                // Must be enabled + visible
                if (!target.Enabled)
                {
                    Console.Error.WriteLine($"[AAR] BLOCKED: target not enabled for {action}");
                    return null;
                }
                if (!target.Visible)
                {
                    Console.Error.WriteLine($"[AAR] BLOCKED: target not visible for {action}");
                    return null;
                }
                // Check if editable (UIA Value.IsReadOnly)
                if (target is UiaActionTarget uat)
                {
                    if (IsReadOnly(uat.Element))
                    {
                        Console.Error.WriteLine($"[AAR] BLOCKED: target is read-only for {action}");
                        return null;
                    }
                }
                break;

            case "scroll":
                // Check Scrollable
                if (target is UiaActionTarget scrollTarget)
                {
                    if (!IsScrollable(scrollTarget.Element))
                    {
                        Console.Error.WriteLine($"[AAR] BLOCKED: target not scrollable");
                        return null;
                    }
                }
                break;

            case "toggle" or "select":
                if (!target.Enabled)
                {
                    Console.Error.WriteLine($"[AAR] WARNING: target disabled for {action}");
                    // Warn only — UIA Toggle/Select can sometimes work on "disabled" MFC controls
                }
                break;

            case "move" or "resize":
                if (!target.IsWindow)
                {
                    Console.Error.WriteLine($"[AAR] BLOCKED: {action} requires a window-level target");
                    return null;
                }
                break;

            case "close":
                // Ancestor protect handled by caller (A11yCommand)
                break;
        }

        return target;
    }

    // ── Helpers ──────────────────────────────────────────────────

    /// <summary>Actions that need no readiness check at all.</summary>
    private static bool IsPassThrough(string action)
        => action is "read" or "inspect" or "highlight" or "find" or "windows"
            or "screenshot" or "ocr" or "eval";

    /// <summary>Element-level actions (not window-level).</summary>
    private static bool IsElementAction(string action)
        => action is "click" or "type" or "set-value" or "toggle" or "select"
            or "scroll" or "invoke" or "expand" or "collapse" or "set-range" or "focus";

    /// <summary>Actions that should hard-block when target is disabled.</summary>
    private static bool IsBlockOnDisabled(string action)
        => action is "type" or "set-value" or "scroll";

    /// <summary>Actions that should hard-block when occluded.</summary>
    private static bool IsBlockOnOcclusion(string action)
        => action is "type" or "set-value";

    /// <summary>Resolve IntPtr hwnd from IActionTarget.</summary>
    private static IntPtr ResolveHwnd(IActionTarget target, ReadinessContext ctx)
    {
        if (ctx.Hwnd != IntPtr.Zero) return ctx.Hwnd;

        return target.NativeHandle switch
        {
            IntPtr h => h,
            UiaActionTarget uat => SafeGetHwnd(uat.Element),
            _ => IntPtr.Zero
        };
    }

    /// <summary>Check if target is minimized via Win32 (fallback when WindowState unavailable).</summary>
    private static bool IsMinimized(IActionTarget target, ReadinessContext ctx)
    {
        var hwnd = ResolveHwnd(target, ctx);
        return hwnd != IntPtr.Zero && NativeMethods.IsIconic(hwnd);
    }

    /// <summary>
    /// Try reading stability cache from window property (SetProp/GetProp).
    /// The tick is stored as IntPtr value — truncated to 32-bit but wraps safely within TTL.
    /// </summary>
    private static bool TryStabilityCache(IntPtr hwnd, out long ageMs)
    {
        ageMs = 0;
        if (hwnd == IntPtr.Zero) return false;
        try
        {
            // GetPropW returns IntPtr.Zero if not set
            var rootHwnd = NativeMethods.GetAncestor(hwnd, NativeMethods.GA_ROOT);
            if (rootHwnd == IntPtr.Zero) rootHwnd = hwnd;
            var stored = NativeMethods.GetPropW(rootHwnd, StabilityPropName);
            if (stored == IntPtr.Zero) return false;
            var cachedTick = stored.ToInt64() & 0xFFFFFFFF; // stored as 32-bit
            var nowTick = Environment.TickCount64 & 0xFFFFFFFF;
            ageMs = nowTick - cachedTick;
            if (ageMs < 0) ageMs += 0x100000000L; // wrap-around
            return ageMs < StabilityCacheTtlMs;
        }
        catch { return false; }
    }

    /// <summary>Write stability cache as window property (zero file I/O).</summary>
    private static void WriteStabilityCache(IntPtr hwnd)
    {
        try
        {
            var rootHwnd = NativeMethods.GetAncestor(hwnd, NativeMethods.GA_ROOT);
            if (rootHwnd == IntPtr.Zero) rootHwnd = hwnd;
            var tick = (int)(Environment.TickCount64 & 0xFFFFFFFF);
            NativeMethods.SetPropW(rootHwnd, StabilityPropName, new IntPtr(tick));
        }
        catch { /* best effort */ }
    }

    /// <summary>Actions that need stability check (coordinate-dependent).</summary>
    private static bool IsStabilityRequired(string action)
        => action is "click" or "invoke" or "type" or "set-value"
            or "toggle" or "select" or "scroll" or "set-range";

    /// <summary>
    /// Resolve the deepest nested blocker for close action.
    /// Walks owned popup chain up to maxDepth with cycle guard.
    /// Returns IActionTarget for the deepest popup, or the first blocker if chain fails.
    /// </summary>
    private IActionTarget? ResolveDeepestBlocker(IntPtr mainHwnd, BlockerInfo topBlocker, int maxDepth)
    {
        var visited = new HashSet<IntPtr> { mainHwnd };
        var current = topBlocker;

        for (int depth = 0; depth < maxDepth; depth++)
        {
            if (!visited.Add(current.Handle))
            {
                Console.Error.WriteLine($"[AAR] close retarget: cycle detected at depth {depth}");
                break;
            }

            // Check if this blocker itself has child popups
            var childBlocker = _readiness.DetectBlocker(current.Handle);
            if (childBlocker == null || childBlocker.Handle == current.Handle)
                break; // no deeper popup → current is the deepest

            Console.Error.WriteLine($"[AAR] close retarget depth {depth + 1}: [{childBlocker.ClassName}] \"{childBlocker.Title}\"");
            current = childBlocker;
        }

        Console.Error.WriteLine($"[AAR] close retarget → [{current.ClassName}] \"{current.Title}\"");
        return CreateRetarget(current);
    }

    /// <summary>
    /// Stability Sampling Probe: take BoundingRect twice with 100ms gap.
    /// If coordinates differ, the element is animating/transitioning → unstable.
    /// </summary>
    private static bool CheckStability(IActionTarget target)
    {
        try
        {
            var r1 = target.BoundingRect;
            // Zero-size elements are inherently unstable (or invisible)
            if (r1.Right - r1.Left <= 0 || r1.Bottom - r1.Top <= 0)
                return true; // pass — visibility check is elsewhere

            Thread.Sleep(100);
            var r2 = target.BoundingRect;

            if (r1 != r2)
            {
                Console.Error.WriteLine($"[AAR] Stability probe: rect changed ({r1.Left},{r1.Top},{r1.Right},{r1.Bottom}) → ({r2.Left},{r2.Top},{r2.Right},{r2.Bottom})");
                return false;
            }
            return true;
        }
        catch
        {
            return true; // assume stable on failure
        }
    }

    /// <summary>
    /// Check if a UIA element is still alive (not stale/disconnected).
    /// Tries to access a basic property — if COM throws, element is dead.
    /// </summary>
    private static bool IsAlive(UiaActionTarget target)
    {
        try
        {
            // Access ProcessId — a lightweight COM call that fails on stale elements
            _ = target.Element.Properties.ProcessId.ValueOrDefault;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Create a UiaActionTarget for a blocker window (retarget).</summary>
    private static IActionTarget? CreateRetarget(BlockerInfo blocker)
    {
        try
        {
            var automation = new FlaUI.UIA3.UIA3Automation();
            var element = automation.FromHandle(blocker.Handle);
            if (element != null)
                return new UiaActionTarget(element);
        }
        catch { }
        return null;
    }

    /// <summary>Check UIA Value.IsReadOnly safely.</summary>
    private static bool IsReadOnly(AutomationElement element)
    {
        try
        {
            if (element.Patterns.Value.IsSupported)
                return element.Patterns.Value.Pattern.IsReadOnly.ValueOrDefault;
        }
        catch { }
        return false;
    }

    /// <summary>Check UIA Scroll pattern support safely.</summary>
    private static bool IsScrollable(AutomationElement element)
    {
        try
        {
            return element.Patterns.Scroll.IsSupported;
        }
        catch { return false; }
    }

    /// <summary>Safe hwnd extraction from AutomationElement.</summary>
    private static IntPtr SafeGetHwnd(AutomationElement element)
    {
        try { return element.Properties.NativeWindowHandle.ValueOrDefault; }
        catch { return IntPtr.Zero; }
    }
}
