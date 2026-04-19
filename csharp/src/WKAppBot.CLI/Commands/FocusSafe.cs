// FocusSafe.cs -- centralized focus-safe gate for heavy UIA / CDP operations.
//
// Background:
//   UIA FromHandle + FindAllDescendants on Chromium/Electron targets (Claude
//   Desktop, Chrome, VSCode, WT) steals foreground whenever it runs, which
//   feels awful when the user is mid-typing. Individual call sites used to
//   roll their own ad-hoc checks (scattered GetUserIdleMs probes, sentinel
//   bailouts). This file is the single authoritative yield policy.
//
// Policy:
//   - If user idle < ActiveThresholdMs (default 1500ms) -> yield.
//   - Caller decides what "yield" means (return cached, return empty,
//     defer work to next tick). Returning here lets the caller make that
//     call explicit in its own command context.
//
// How to use:
//   if (FocusSafe.ShouldYieldToActiveUser(out var idleMs)) { <skip or cache>; return; }
//   <do UIA / CDP work>

using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal static class FocusSafe
{
    /// <summary>
    /// Time window (ms) below which the user is considered actively interacting.
    /// Matches RestoreFocusWithRetryAsync's user-active bailout threshold so both
    /// "yield before starting" and "yield during retry" use the same idle bar.
    /// </summary>
    public const int ActiveThresholdMs = 1500;

    /// <summary>
    /// Returns true if the caller should yield heavy UIA/CDP work because the
    /// user has typed/clicked within <see cref="ActiveThresholdMs"/>. Also
    /// returns the measured idle ms for caller logging. On any probe failure
    /// returns false (conservative -- we'd rather run the work than block it).
    /// </summary>
    public static bool ShouldYieldToActiveUser(out int idleMs)
    {
        idleMs = 0;
        try
        {
            var raw = NativeMethods.GetUserIdleMs();
            idleMs = raw > int.MaxValue ? int.MaxValue : (int)raw;
            return idleMs >= 0 && idleMs < ActiveThresholdMs;
        }
        catch
        {
            return false;
        }
    }
}
