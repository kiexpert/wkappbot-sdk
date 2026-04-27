using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- Pre/Post Action Verification ------------------------

    /// <summary>
    /// Snapshot of element + overlay state at a screen coordinate.
    /// </summary>
    private sealed class TargetSnapshot
    {
        public string ControlType { get; init; } = "";
        public string Name { get; init; } = "";
        public string AutomationId { get; init; } = "";
        public int BoundsX { get; init; }
        public int BoundsY { get; init; }
        public int BoundsW { get; init; }
        public int BoundsH { get; init; }
        public bool PointInsideBounds { get; init; }
        public bool OverlayDetected { get; init; }
        public string? OverlayClass { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public DateTime Timestamp { get; init; }
    }

    /// <summary>
    /// Take a UIA + overlay snapshot at screen coordinates.
    ///
    /// Checks:
    ///   1. UIA element at (x,y) -- identity and properties
    ///   2. Point inside element BoundingRectangle -- 좌표가 렉트 안쪽인지
    ///   3. WindowFromPoint -- 방해하는 창이 없는지 (overlay detection)
    /// </summary>
    private TargetSnapshot? SnapshotAt(int x, int y)
    {
        try
        {
            // UIA element at point
            var info = _uia.GetElementAtPoint(x, y);
            if (info == null) return null;

            // Point inside bounding rect?
            bool inside = x >= info.BoundsX && x < info.BoundsX + info.BoundsW
                       && y >= info.BoundsY && y < info.BoundsY + info.BoundsH;

            // Overlay detection: WindowFromPoint should return our target window
            bool overlayDetected = false;
            string? overlayClass = null;

            if (_ctx.MainWindowHandle != IntPtr.Zero)
            {
                var pt = new POINT { X = x, Y = y };
                var topWnd = NativeMethods.WindowFromPoint(pt);
                if (topWnd != IntPtr.Zero)
                {
                    // Walk up to top-level to compare with our main window
                    var topLevel = topWnd;
                    IntPtr parent;
                    while ((parent = NativeMethods.GetParent(topLevel)) != IntPtr.Zero)
                        topLevel = parent;

                    if (topLevel != _ctx.MainWindowHandle)
                    {
                        overlayDetected = true;
                        overlayClass = WindowFinder.GetClassName(topWnd);
                    }
                }
            }

            return new TargetSnapshot
            {
                ControlType = info.ControlType,
                Name = info.Name,
                AutomationId = info.AutomationId,
                BoundsX = info.BoundsX, BoundsY = info.BoundsY,
                BoundsW = info.BoundsW, BoundsH = info.BoundsH,
                PointInsideBounds = inside,
                OverlayDetected = overlayDetected,
                OverlayClass = overlayClass,
                X = x, Y = y,
                Timestamp = DateTime.Now
            };
        }
        catch { return null; }
    }

    /// <summary>
    /// Pre/Post verification around EnsureFocus + SendInput.
    ///
    /// Flow:
    ///   1. Pre-snap: UIA + overlay at target coords (before focus change)
    ///   2. EnsureFocus (may cause window switch)
    ///   3. Post-snap: UIA + overlay at target coords (after focus)
    ///   4. Compare:
    ///      - Same element? (type + automationId)
    ///      - Point inside element rect? (좌표가 렉트 안)
    ///      - No overlay? (방해 창 없음)
    ///      -> All OK -> "verify=OK"
    ///      -> Warning -> "verify=WARN(...)" + [VERIFY] log
    ///
    /// Never blocks execution -- only logs warnings.
    /// </summary>
    private string VerifiedEnsureFocus(int x, int y, string? expectedName = null, string? expectedAid = null)
    {
        // Pre-snap (before focus)
        var pre = SnapshotAt(x, y);

        // Focus
        EnsureFocus();

        // Post-snap (after focus)
        var post = SnapshotAt(x, y);

        // -- Analyze results --
        var warnings = new List<string>();

        if (pre == null && post == null)
            return "verify=skip(no_element)";

        // Use post-snap (after focus = actual state at click time)
        var snap = post ?? pre;

        // Check 1: Overlay -- 방해하는 창 없나?
        if (snap!.OverlayDetected)
        {
            warnings.Add($"overlay({snap.OverlayClass})");
            LogVerify($"Overlay detected at ({x},{y}): class=\"{snap.OverlayClass}\" blocking target window");
        }

        // Check 2: Point inside bounds -- 좌표가 렉트 안인가?
        if (!snap.PointInsideBounds)
        {
            warnings.Add("outside_bounds");
            LogVerify($"Click point ({x},{y}) outside element rect: [{snap.ControlType}]\"{snap.Name}\" at ({snap.BoundsX},{snap.BoundsY} {snap.BoundsW}x{snap.BoundsH})");
        }

        // Check 3: Expected element match
        if (expectedAid != null && !string.IsNullOrEmpty(snap.AutomationId) && snap.AutomationId != expectedAid)
        {
            // Pattern matching support: check if expectedAid is a pattern
            if (PatternMatcher.IsPattern(expectedAid))
            {
                var matcher = PatternMatcher.Create(expectedAid);
                if (!matcher.IsMatch(snap.AutomationId))
                {
                    warnings.Add($"aid_mismatch");
                    LogVerify($"Element mismatch: expected aid=\"{expectedAid}\" but found aid=\"{snap.AutomationId}\" [{snap.ControlType}]");
                }
            }
            else
            {
                warnings.Add($"aid_mismatch");
                LogVerify($"Element mismatch: expected aid=\"{expectedAid}\" but found aid=\"{snap.AutomationId}\" [{snap.ControlType}]");
            }
        }

        // Check 4: Pre vs Post stability
        if (pre != null && post != null)
        {
            bool sameElement = pre.ControlType == post.ControlType
                && pre.AutomationId == post.AutomationId;

            if (!sameElement)
            {
                warnings.Add("element_changed");
                LogVerify($"Element changed after focus: [{pre.ControlType}]\"{pre.Name}\" -> [{post.ControlType}]\"{post.Name}\"");
            }
            else if (pre.Name != post.Name)
            {
                // Same element, name changed -- likely state change (not a warning, just info)
                Log($"  [VERIFY] Name changed: \"{pre.Name}\" -> \"{post.Name}\" (state change)");
            }
        }

        if (warnings.Count == 0)
            return "verify=OK";

        return $"verify=WARN({string.Join(",", warnings)})";
    }

    private void LogVerify(string msg)
    {
        var consoleLock = _ctx.ConsoleLock ?? new object();
        lock (consoleLock)
        {
            int w;
            try { w = Console.WindowWidth; } catch { w = 120; }
            Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[VERIFY] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
