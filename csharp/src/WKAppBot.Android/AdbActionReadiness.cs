using WKAppBot.Abstractions;

namespace WKAppBot.Android;

/// <summary>
/// Action-Aware Readiness (AAR) for Android ADB — implements IActionReadiness.
/// Lightweight 3-stage pipeline: Global → Target → Action-Specific.
///
/// Return convention:
///   null        → blocked (hard fail)
///   == target   → success (proceed)
///   != target   → retarget (not used for Android yet)
///
/// Tag: [AAR]
/// </summary>
public sealed class AdbActionReadiness : IActionReadiness
{
    private readonly AdbClient _adb;

    public AdbActionReadiness(AdbClient adb)
    {
        _adb = adb ?? throw new ArgumentNullException(nameof(adb));
    }

    public IActionTarget? Ensure(string action, IActionTarget target, ReadinessContext ctx)
    {
        var act = action.ToLowerInvariant();

        // ── Pass-through actions ──
        if (IsPassThrough(act))
            return target;

        // ── Stage 0: Global — device awake? ──
        if (!string.IsNullOrEmpty(ctx.Serial))
        {
            if (!CheckDeviceAwake(ctx.Serial))
            {
                Console.Error.WriteLine("[AAR:ADB] BLOCKED: device screen off (mWakefulness=Asleep)");
                return null;
            }
        }

        // ── Stage 1: Target validation ──
        if (!target.Enabled && IsBlockOnDisabled(act))
        {
            Console.Error.WriteLine($"[AAR:ADB] BLOCKED: target not enabled for {action}: \"{target.DisplayName}\"");
            return null;
        }

        if (!target.Visible)
        {
            if (act is "type" or "set-value" or "click" or "invoke")
            {
                Console.Error.WriteLine($"[AAR:ADB] BLOCKED: target not visible (0-size bounds) for {action}");
                return null;
            }
        }

        // ── Stage 2: Action-specific ──
        switch (act)
        {
            case "type" or "set-value":
                // Must be focusable + enabled
                if (target.NativeHandle is AndroidNode node && !node.Focusable)
                {
                    Console.Error.WriteLine($"[AAR:ADB] WARNING: target not focusable for {action}: \"{target.DisplayName}\"");
                    // Warn only — some apps handle text input on non-focusable views
                }
                break;

            case "scroll":
                if (target.NativeHandle is AndroidNode scrollNode && !scrollNode.Scrollable)
                {
                    Console.Error.WriteLine($"[AAR:ADB] BLOCKED: target not scrollable: \"{target.DisplayName}\"");
                    return null;
                }
                break;

            case "toggle":
                if (target.NativeHandle is AndroidNode toggleNode && !toggleNode.Checkable)
                {
                    Console.Error.WriteLine($"[AAR:ADB] WARNING: target not checkable for toggle: \"{target.DisplayName}\"");
                }
                break;
        }

        Console.Error.WriteLine($"[AAR:ADB] OK: {action} on \"{target.DisplayName}\"");
        return target;
    }

    // ── Helpers ──────────────────────────────────────────────

    private static bool IsPassThrough(string action)
        => action is "read" or "inspect" or "find" or "highlight" or "windows"
            or "screenshot" or "ocr";

    private static bool IsBlockOnDisabled(string action)
        => action is "type" or "set-value" or "scroll";

    /// <summary>Check device wakefulness via dumpsys power (best-effort).</summary>
    private bool CheckDeviceAwake(string serial)
    {
        try
        {
            var result = _adb.Shell("dumpsys power | grep mWakefulness", serial);
            if (result.IsOk && result.StdOut.Contains("Asleep", StringComparison.OrdinalIgnoreCase))
                return false;
        }
        catch { /* best effort */ }
        return true; // assume awake on failure
    }
}
