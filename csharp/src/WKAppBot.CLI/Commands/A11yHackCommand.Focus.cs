// A11yHackCommand.Focus.cs — Focus resolution, auto-narrow, UIA element helpers
// Split from A11yHackCommand.cs for maintainability (~430 lines)

using System.Drawing;
using FlaUI.Core.AutomationElements;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    static void TryAutoNarrowHackRect(
        IntPtr hwnd,
        string grapFull,
        string? uiaScope,
        bool focusDrillRequested,
        bool hasExplicitScope,
        ref RECT wr,
        int? overrideX = null,
        int? overrideY = null)
    {
        try
        {
            using var automation = new FlaUI.UIA3.UIA3Automation();

            if (hasExplicitScope || focusDrillRequested)
            {
                var resolved = GrapHelper.ResolveFullGrap(grapFull, automation);
                if (resolved.HasValue)
                {
                    var (_, root, error) = resolved.Value;
                    if (error == null && root != null)
                    {
                        var r = root.Properties.BoundingRectangle.ValueOrDefault;
                        // Skip if resolved to window-level (90%+ of hwnd rect)
                        bool isRootLevel = r.Width >= (wr.Right - wr.Left) * 0.9
                                        && r.Height >= (wr.Bottom - wr.Top) * 0.9;
                        if (r.Width > 0 && r.Height > 0 && !isRootLevel)
                        {
                            wr.Left = r.X;
                            wr.Top = r.Y;
                            wr.Right = r.X + r.Width;
                            wr.Bottom = r.Y + r.Height;
                            Console.WriteLine($"[HACK] Scoped to grap root rect=({r.X},{r.Y} {r.Width}x{r.Height})");
                            return;
                        }
                    }
                }
                // Fall through to ElementFromPoint if scope resolved to root level
            }

            if (!string.IsNullOrEmpty(uiaScope)) return;

            // -- Analysis area determination --
            // UIA available: narrow to target node's Parent
            // UIA unavailable: clamp to Win32 parent/owner window
            // Target = root window: analyze full window (wr unchanged)
            try
            {
                // 1. UIA target -> Parent narrowing
                var root = automation.FromHandle(hwnd);
                var grapPattern = grapFull.Split('#')[0];
                var grapResult = GrapHelper.FindByNameOrAid(root, grapPattern);
                if (grapResult != null)
                {
                    // Skip if grap matched the window itself (root level) -- fall through to ElementFromPoint
                    var grapRect = grapResult.Properties.BoundingRectangle.ValueOrDefault;
                    bool isRootMatch = grapRect.Width >= (wr.Right - wr.Left) * 0.9
                                    && grapRect.Height >= (wr.Bottom - wr.Top) * 0.9;
                    if (!isRootMatch)
                    {
                        var parent = grapResult.Parent;
                        var analysisEl = parent ?? grapResult;
                        var aRect = analysisEl.Properties.BoundingRectangle.ValueOrDefault;
                        if (aRect.Width > 0 && aRect.Height > 0)
                        {
                            wr.Left = aRect.X; wr.Top = aRect.Y;
                            wr.Right = aRect.X + aRect.Width; wr.Bottom = aRect.Y + aRect.Height;
                            Console.WriteLine($"[HACK] UIA parent-narrowed: rect=({aRect.X},{aRect.Y} {aRect.Width}x{aRect.Height})");
                            return;
                        }
                    }
                }
            }
            catch { }

            // 1b. ElementFromPoint -> most specific element (--at override or cursor)
            try
            {
                int ptX, ptY;
                if (overrideX.HasValue && overrideY.HasValue)
                { ptX = overrideX.Value; ptY = overrideY.Value; }
                else
                { NativeMethods.GetCursorPos(out var cp); ptX = cp.X; ptY = cp.Y; }
                Console.WriteLine($"[HACK] ElementFromPoint at ({ptX},{ptY})");
                var pointed = automation.FromPoint(new System.Drawing.Point(ptX, ptY));
                if (pointed != null)
                {
                    // Walk up to find an element with reasonable size (not 1px noise)
                    var target = pointed;
                    var tRect = target.Properties.BoundingRectangle.ValueOrDefault;

                    // Use parent for analysis context (shows target + siblings)
                    var parent = target.Parent;
                    if (parent != null)
                    {
                        var pRect = parent.Properties.BoundingRectangle.ValueOrDefault;
                        if (pRect.Width > 0 && pRect.Height > 0
                            && pRect.Width < (wr.Right - wr.Left) * 0.95) // parent smaller than full window
                        {
                            wr.Left = pRect.X; wr.Top = pRect.Y;
                            wr.Right = pRect.X + pRect.Width; wr.Bottom = pRect.Y + pRect.Height;
                            Console.WriteLine($"[HACK] ElementFromPoint parent: rect=({pRect.X},{pRect.Y} {pRect.Width}x{pRect.Height}) target=\"{tRect.Width}x{tRect.Height}\"");
                            return;
                        }
                    }

                    // No useful parent -> use target element itself if smaller than window
                    if (tRect.Width > 0 && tRect.Height > 0
                        && tRect.Width * tRect.Height < (wr.Right - wr.Left) * (wr.Bottom - wr.Top) * 0.8)
                    {
                        wr.Left = tRect.X; wr.Top = tRect.Y;
                        wr.Right = tRect.X + tRect.Width; wr.Bottom = tRect.Y + tRect.Height;
                        Console.WriteLine($"[HACK] ElementFromPoint direct: rect=({tRect.X},{tRect.Y} {tRect.Width}x{tRect.Height})");
                        return;
                    }
                }
            }
            catch { }

            // 2. UIA failed -> clamp to Win32 Owner/Parent window
            var parentHwnd = NativeMethods.GetParent(hwnd);
            if (parentHwnd == IntPtr.Zero)
                parentHwnd = NativeMethods.GetAncestor(hwnd, 2); // GA_ROOT
            if (parentHwnd != IntPtr.Zero && parentHwnd != hwnd)
            {
                NativeMethods.GetWindowRect(parentHwnd, out var parentWr);
                if (parentWr.Right - parentWr.Left > 0 && parentWr.Bottom - parentWr.Top > 0)
                {
                    wr.Left = Math.Max(wr.Left, parentWr.Left);
                    wr.Top = Math.Max(wr.Top, parentWr.Top);
                    wr.Right = Math.Min(wr.Right, parentWr.Right);
                    wr.Bottom = Math.Min(wr.Bottom, parentWr.Bottom);
                    Console.WriteLine($"[HACK] Win32 parent-clamped: rect=({wr.Left},{wr.Top} {wr.Right - wr.Left}x{wr.Bottom - wr.Top})");
                    return;
                }
            }
            // 3. Target = root window -> analyze full window
            Console.WriteLine($"[HACK] Root window -- analyzing full: rect=({wr.Left},{wr.Top} {wr.Right - wr.Left}x{wr.Bottom - wr.Top})");
        }
        catch
        {
            Console.WriteLine("[HACK] Scope/focus narrowing error - using full window");
        }
    }

    static Rectangle TryResolveAnalysisTargetRect(RECT wr)
    {
        var captureRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        if (captureRect.Width <= 1 || captureRect.Height <= 1)
            return new Rectangle(0, 0, 1, 1);

        try
        {
            if (NativeMethods.TryGetCurrentCursorRect(out var cursorRect) && captureRect.Contains(cursorRect))
            {
                return new Rectangle(
                    cursorRect.X - wr.Left,
                    cursorRect.Y - wr.Top,
                    Math.Max(1, cursorRect.Width),
                    Math.Max(1, cursorRect.Height));
            }
        }
        catch { }

        return new Rectangle(
            Math.Max(0, captureRect.Width / 2),
            Math.Max(0, captureRect.Height / 2),
            1, 1);
    }

    static List<ConnectedComponentAnalyzer.Region> OrderRegionsForTarget(
        List<ConnectedComponentAnalyzer.Region> regions,
        Rectangle targetRect,
        int captureWidth,
        int captureHeight)
    {
        if (regions.Count <= 1)
            return regions;

        var captureBounds = new Rectangle(0, 0, Math.Max(1, captureWidth), Math.Max(1, captureHeight));
        if (!captureBounds.Contains(targetRect))
            targetRect = new Rectangle(
                Math.Clamp(targetRect.X, 0, captureBounds.Width - 1),
                Math.Clamp(targetRect.Y, 0, captureBounds.Height - 1),
                Math.Max(1, targetRect.Width),
                Math.Max(1, targetRect.Height));

        int TargetPriority(ConnectedComponentAnalyzer.Region region)
        {
            var bounds = region.Bounds;
            if (ContainsRect(bounds, targetRect)) return 0;
            if (bounds.IntersectsWith(targetRect)) return 1;
            return 2;
        }

        int DistanceToTarget(ConnectedComponentAnalyzer.Region region)
        {
            var bounds = region.Bounds;
            var rx = bounds.Left + bounds.Width / 2;
            var ry = bounds.Top + bounds.Height / 2;
            var tx = targetRect.Left + targetRect.Width / 2;
            var ty = targetRect.Top + targetRect.Height / 2;
            return Math.Abs(rx - tx) + Math.Abs(ry - ty);
        }

        return regions
            .OrderBy(TargetPriority)
            .ThenByDescending(region => ContainsRect(region.Bounds, targetRect) ? region.Bounds.Width * region.Bounds.Height : 0)
            .ThenBy(DistanceToTarget)
            .ThenBy(region => region.Bounds.Width * region.Bounds.Height)
            .ThenBy(region => region.Bounds.Top)
            .ThenBy(region => region.Bounds.Left)
            .ToList();
    }

    static bool TryResolveFocusedCaptureRect(
        FlaUI.UIA3.UIA3Automation automation,
        IntPtr hwnd,
        out Rectangle rect,
        out string description)
    {
        rect = Rectangle.Empty;
        description = "";

        try
        {
            if (NativeMethods.GetForegroundWindow() != hwnd)
                return false;

            var root = automation.FromHandle(hwnd);
            if (root == null) return false;

            var focused = GrapHelper.FindFocusedLeaf(automation, root, hwnd);
            if (focused == null) return false;

            var focusedRect = TryGetScreenRect(focused);
            if (focusedRect.Width <= 1 || focusedRect.Height <= 1) return false;
            NativeMethods.GetWindowRect(hwnd, out var wr);
            var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
            var anchorRect = TryGetPreferredFocusAnchorRect(focused, focusedRect, winRect);

            AutomationElement? best = focused;
            int bestScore = ScoreHackFocusCandidate(focused, focusedRect, anchorRect, winRect);
            var cur = focused;
            for (int depth = 0; depth < 6 && cur != null; depth++)
            {
                var candidateRect = TryGetScreenRect(cur);
                if (candidateRect.Width <= 1 || candidateRect.Height <= 1) break;
                if (!IsRectInsideWindow(candidateRect, hwnd)) break;
                if (!ContainsRect(candidateRect, anchorRect))
                {
                    try { cur = cur.Parent; } catch { break; }
                    continue;
                }

                int candidateScore = ScoreHackFocusCandidate(cur, focusedRect, anchorRect, winRect);
                if (candidateScore > bestScore)
                {
                    best = cur;
                    bestScore = candidateScore;
                }

                try { cur = cur.Parent; } catch { break; }
            }

            var bestRect = InflateWithinWindow(TryGetScreenRect(best), hwnd, 12, 10);
            if (bestRect.Width <= 1 || bestRect.Height <= 1) return false;

            rect = bestRect;
            description = $"[{SafeControlType(best)}] \"{SafeName(best)}\"";
            return true;
        }
        catch { return false; }
    }

    static Rectangle TryGetPreferredFocusAnchorRect(AutomationElement focused, Rectangle focusedRect, Rectangle winRect)
    {
        try
        {
            if (focused.Patterns.Text.IsSupported)
            {
                var ranges = focused.Patterns.Text.Pattern.GetSelection();
                if (ranges != null)
                {
                    var best = Rectangle.Empty;
                    foreach (var range in ranges)
                    {
                        try
                        {
                            var rects = range.GetBoundingRectangles();
                            if (rects == null) continue;
                            foreach (var rr in rects)
                            {
                                var candidate = Rectangle.FromLTRB(
                                    (int)Math.Floor((double)rr.X),
                                    (int)Math.Floor((double)rr.Y),
                                    (int)Math.Ceiling((double)(rr.X + rr.Width)),
                                    (int)Math.Ceiling((double)(rr.Y + rr.Height)));
                                if (candidate.Width <= 0 || candidate.Height <= 0) continue;
                                candidate = Rectangle.Intersect(candidate, winRect);
                                if (candidate.Width <= 0 || candidate.Height <= 0) continue;
                                if (best == Rectangle.Empty || candidate.Width * candidate.Height < best.Width * best.Height)
                                    best = candidate;
                            }
                        }
                        catch { }
                    }

                    if (best != Rectangle.Empty)
                        return best;
                }
            }
        }
        catch { }

        return focusedRect;
    }

    static int ScoreHackFocusCandidate(
        AutomationElement candidate,
        Rectangle focusedRect,
        Rectangle anchorRect,
        Rectangle winRect)
    {
        var rect = TryGetScreenRect(candidate);
        if (rect.Width <= 1 || rect.Height <= 1) return int.MinValue;
        if (!ContainsRect(rect, anchorRect)) return int.MinValue;

        var ct = SafeControlType(candidate);
        bool isPrimary = ct is "Document" or "Edit";
        bool isSecondary = ct is "Pane";
        bool isFallback = ct is "Group";
        if (!isPrimary && !isSecondary && !isFallback) return int.MinValue;

        int minWidth = isPrimary ? Math.Max(320, anchorRect.Width * 3) : Math.Max(260, focusedRect.Width * 2);
        int minHeight = isPrimary ? Math.Max(72, anchorRect.Height * 4) : Math.Max(48, focusedRect.Height * 2);
        int maxWidth = isPrimary ? Math.Min(winRect.Width, 1800) : Math.Min(winRect.Width, 1400);
        int maxHeight = isPrimary ? Math.Min(winRect.Height, 1100) : Math.Min(winRect.Height, 520);

        if (rect.Width < minWidth || rect.Height < minHeight) return int.MinValue / 2;
        if (rect.Width > maxWidth || rect.Height > maxHeight) return int.MinValue / 2;

        int score = ct switch
        {
            "Document" => 150,
            "Edit" => 135,
            "Pane" => 90,
            "Group" => 45,
            _ => 0
        };

        score += Math.Min(90, rect.Width / 18);
        score += Math.Min(90, rect.Height / 12);

        bool nearBottom = rect.Bottom >= winRect.Bottom - Math.Max(90, winRect.Height / 7);
        bool bannerLike = rect.Height <= Math.Max(160, winRect.Height / 4) && rect.Width <= Math.Max(700, winRect.Width / 2);
        if (nearBottom && bannerLike) score -= 160;
        if (ct == "Group" && rect.Height <= Math.Max(180, winRect.Height / 3)) score -= 50;
        if (ct == "Pane" && rect.Height <= Math.Max(90, anchorRect.Height * 3)) score -= 30;

        var name = SafeName(candidate);
        if (!string.IsNullOrWhiteSpace(name) && name.Length <= 60) score += 8;

        return score;
    }

    // -- UIA element helpers --

    static bool ContainsRect(Rectangle outer, Rectangle inner) =>
        inner.Left >= outer.Left && inner.Top >= outer.Top
        && inner.Right <= outer.Right && inner.Bottom <= outer.Bottom;

    static Rectangle TryGetScreenRect(AutomationElement? element)
    {
        if (element == null) return Rectangle.Empty;
        try
        {
            var r = element.Properties.BoundingRectangle.ValueOrDefault;
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }
        catch { return Rectangle.Empty; }
    }

    static string SafeControlType(AutomationElement? element)
    {
        if (element == null) return "?";
        try { return element.Properties.ControlType.ValueOrDefault.ToString(); }
        catch { return "?"; }
    }

    static string SafeName(AutomationElement? element)
    {
        if (element == null) return "";
        try { return element.Properties.Name.ValueOrDefault ?? ""; }
        catch { return ""; }
    }

    static bool IsRectInsideWindow(Rectangle rect, IntPtr hwnd)
    {
        NativeMethods.GetWindowRect(hwnd, out var wr);
        var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        return winRect.IntersectsWith(rect) && rect.Left >= winRect.Left && rect.Top >= winRect.Top;
    }

    static Rectangle InflateWithinWindow(Rectangle rect, IntPtr hwnd, int dx, int dy)
    {
        NativeMethods.GetWindowRect(hwnd, out var wr);
        var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        return Rectangle.Intersect(Rectangle.Inflate(rect, dx, dy), winRect);
    }
}
