using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot screenshot ["expr1" "expr2" ...] [--padding N] [--out file.png]
    ///
    /// With grap args: resolves each to a window bbox, unions all, captures that region.
    /// Without grap args: captures the full primary screen (or --region if specified).
    /// Grap expressions are any non-flag positional argument (same syntax as a11y find).
    /// </summary>
    static int ScreenshotCommand(string[] args)
    {
        if (args.Any(a => a is "--help" or "-h" or "help"))
        {
            PrintScreenshotUsage();
            return 0;
        }

        // -- Parse args: positional = grap exprs, --padding, --out --
        var graps = new List<string>();
        int paddingPct = 0;
        string? outPath = null;

        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a.Equals("--grap", StringComparison.OrdinalIgnoreCase))
            {
                // --grap is now optional/legacy; next arg (if not a flag) is a grap expr
                if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                    graps.Add(args[++i]);
                continue;
            }
            if (a.Equals("--padding", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
            {
                if (!int.TryParse(args[++i], out paddingPct) || paddingPct < 0)
                {
                    Console.Error.WriteLine("[SCREENSHOT] --padding must be a non-negative integer (percent)");
                    return 1;
                }
                continue;
            }
            if ((a.Equals("--out", StringComparison.OrdinalIgnoreCase)
                 || a.Equals("-o", StringComparison.OrdinalIgnoreCase))
                && i + 1 < args.Length)
            {
                outPath = args[++i];
                continue;
            }
            if (a.StartsWith("--", StringComparison.Ordinal))
            {
                Console.Error.WriteLine($"[SCREENSHOT] unknown flag: {a}");
                return 1;
            }
            // Positional arg = grap expression (auto-detected, no --grap flag needed)
            graps.Add(a);
        }

        if (graps.Count == 0)
        {
            PrintScreenshotUsage();
            return 1;
        }

        // -- Pass 1: resolve every grap, collect (grap, WindowInfo, rect) for each usable hit --
        // Pass 2: if ANY grap is ambiguous (>1 usable hits), print merged ranked list and exit 2.
        // Pass 3: if every grap has exactly 1 hit, build the unified targets list as before.
        var perGrap = new List<(string grap, List<(WindowInfo info, Rectangle rect)> usable)>();
        foreach (var g in graps)
        {
            List<WindowInfo> hits;
            try { hits = WindowFinder.FindWindows(g); }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SCREENSHOT] grap parse error \"{g}\": {ex.Message}");
                return 1;
            }
            var usable = new List<(WindowInfo info, Rectangle rect)>();
            foreach (var w in hits)
            {
                if (!NativeMethods.GetWindowRect(w.Handle, out var rc)) continue;
                int wW = rc.Right - rc.Left, hH = rc.Bottom - rc.Top;
                if (wW <= 0 || hH <= 0) continue;
                usable.Add((w, new Rectangle(rc.Left, rc.Top, wW, hH)));
            }
            if (usable.Count == 0)
            {
                Console.Error.WriteLine($"[SCREENSHOT] grap resolved to 0 elements: \"{g}\"");
                return 1;
            }
            perGrap.Add((g, usable));
        }

        // If any grap is ambiguous, surface a merged ranked list across ALL candidates
        // from ALL ambiguous graps -- gives the user the full disambiguation picture
        // in one shot instead of a per-grap flat dump.
        bool anyAmbiguous = perGrap.Any(p => p.usable.Count > 1);
        if (anyAmbiguous)
        {
            PrintMergedAmbiguousCandidates(perGrap);
            return 2;
        }

        // Every grap resolved to exactly 1 hit -- build the targets list.
        var targets = new List<(IntPtr hwnd, Rectangle rect, string grap)>();
        foreach (var p in perGrap)
        {
            var only = p.usable[0];
            targets.Add((only.info.Handle, only.rect, p.grap));
            Console.Error.WriteLine($"[SCREENSHOT] \"{p.grap}\" -> hwnd:0x{only.info.Handle.ToInt64():X8} rect={only.rect.Left},{only.rect.Top},{only.rect.Right},{only.rect.Bottom}");
        }
        var rects = targets.Select(t => t.rect).ToList();

        // -- Union all rects --
        int unionLeft   = rects.Min(r => r.Left);
        int unionTop    = rects.Min(r => r.Top);
        int unionRight  = rects.Max(r => r.Right);
        int unionBottom = rects.Max(r => r.Bottom);

        int unionW = unionRight - unionLeft;
        int unionH = unionBottom - unionTop;
        if (unionW <= 0 || unionH <= 0)
        {
            Console.Error.WriteLine($"[SCREENSHOT] empty union rect: {unionLeft},{unionTop},{unionRight},{unionBottom}");
            return 1;
        }

        // -- Apply padding (percent of union dimensions, expand each side) --
        int padX = unionW * paddingPct / 100;
        int padY = unionH * paddingPct / 100;
        int finalLeft   = unionLeft   - padX;
        int finalTop    = unionTop    - padY;
        int finalRight  = unionRight  + padX;
        int finalBottom = unionBottom + padY;

        // Clamp to virtual screen bounds.
        int vsX = NativeMethods.GetSystemMetrics(76); // SM_XVIRTUALSCREEN
        int vsY = NativeMethods.GetSystemMetrics(77); // SM_YVIRTUALSCREEN
        int vsW = NativeMethods.GetSystemMetrics(78); // SM_CXVIRTUALSCREEN
        int vsH = NativeMethods.GetSystemMetrics(79); // SM_CYVIRTUALSCREEN
        if (finalLeft   < vsX)         finalLeft   = vsX;
        if (finalTop    < vsY)         finalTop    = vsY;
        if (finalRight  > vsX + vsW)   finalRight  = vsX + vsW;
        if (finalBottom > vsY + vsH)   finalBottom = vsY + vsH;

        int finalW = finalRight - finalLeft;
        int finalH = finalBottom - finalTop;
        if (finalW <= 0 || finalH <= 0)
        {
            Console.Error.WriteLine($"[SCREENSHOT] padded rect collapsed after virtual-screen clamp: " +
                                    $"{finalLeft},{finalTop},{finalRight},{finalBottom}");
            return 1;
        }

        // -- Per-target smart capture --
        // Phase 1: try PrintWindow on every target (cheap, no Z-order disturbance).
        //          PrintWindow returns null for GPU-rendered windows (DirectX/Chromium).
        // Phase 2: for GPU targets only, suppress windows ABOVE them (Z-order) and
        //          screen-capture each target's rect individually.
        // Phase 3: composite all captures back-to-front into the union bitmap, then
        //          mask pixels outside any target rect.
        var captureRect = new Rectangle(finalLeft, finalTop, finalW, finalH);

        // Per-target capture results: PrintWindow bitmap (full window) OR screen-cap (target rect).
        // Tag isPrintWindow so the composite step knows the source dimensions.
        var captures = new (IntPtr hwnd, Rectangle rect, Bitmap? bmp, bool isPrintWindow)[targets.Count];
        var gpuTargetIdx = new List<int>();
        for (int i = 0; i < targets.Count; i++)
        {
            var (hwnd, rect, _) = targets[i];
            Bitmap? pw = null;
            try { pw = ScreenCapture.TryPrintWindowOnly(hwnd); }
            catch (Exception ex) { Console.Error.WriteLine($"[SCREENSHOT] PrintWindow err hwnd:0x{hwnd.ToInt64():X8}: {ex.Message}"); }
            if (pw != null)
            {
                captures[i] = (hwnd, rect, pw, true);
                Console.Error.WriteLine($"[SCREENSHOT] hwnd:0x{hwnd.ToInt64():X8} -> PrintWindow OK ({pw.Width}x{pw.Height})");
            }
            else
            {
                captures[i] = (hwnd, rect, null, false);
                gpuTargetIdx.Add(i);
                Console.Error.WriteLine($"[SCREENSHOT] hwnd:0x{hwnd.ToInt64():X8} -> PrintWindow blank, deferring to screen capture");
            }
        }

        // Phase 2: GPU targets need screen-capture with above-Z-order suppression.
        List<(IntPtr hwnd, int prevStyle, byte prevAlpha)>? suppressed = null;
        try
        {
            if (gpuTargetIdx.Count > 0)
            {
                // Build protected set: only the GPU targets (and their root ancestors).
                // Non-GPU targets are already captured via PrintWindow; we don't need
                // to keep them visible during this phase.
                var gpuHwnds = new HashSet<IntPtr>(gpuTargetIdx.Select(i => captures[i].hwnd));
                foreach (var h in gpuHwnds.ToArray())
                {
                    var root = NativeMethods.GetAncestor(h, NativeMethods.GA_ROOT);
                    if (root != IntPtr.Zero) gpuHwnds.Add(root);
                }
                suppressed = SuppressBlockingWindows(captureRect, gpuHwnds);
                if (suppressed.Count > 0)
                {
                    Console.Error.WriteLine($"[SCREENSHOT] suppressed {suppressed.Count} blocking window(s) above {gpuTargetIdx.Count} GPU target(s)");
                    System.Threading.Thread.Sleep(30); // one frame for transparency to apply
                }
                foreach (int i in gpuTargetIdx)
                {
                    var t = captures[i];
                    var r = t.rect;
                    Bitmap? sc = null;
                    try { sc = ScreenCapture.CaptureScreenRegion(r.Left, r.Top, r.Width, r.Height); }
                    catch (Exception ex) { Console.Error.WriteLine($"[SCREENSHOT] screen capture failed for hwnd:0x{t.hwnd.ToInt64():X8}: {ex.Message}"); }
                    captures[i] = (t.hwnd, r, sc, false);
                }
            }
        }
        finally
        {
            if (suppressed != null) RestoreBlockingWindows(suppressed);
        }

        // Phase 3: composite back-to-front (Z-order: bottom drawn first).
        // Walk GW_HWNDNEXT chain from each target -- a target is "behind" another if
        // it appears later in the GW_HWNDNEXT chain starting from that other target.
        // Practical: order targets by Z-depth ascending (deepest first).
        var orderedIdx = OrderTargetsBackToFront(captures);
        Bitmap bmp;
        try
        {
            bmp = new Bitmap(finalW, finalH, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                foreach (int i in orderedIdx)
                {
                    var (hwnd, rect, src, isPw) = captures[i];
                    if (src == null) continue;
                    int dx = rect.Left - finalLeft;
                    int dy = rect.Top - finalTop;
                    if (isPw)
                    {
                        // PrintWindow bitmap is the FULL window -- draw at target's
                        // top-left within the union bbox.
                        g.DrawImage(src, dx, dy, src.Width, src.Height);
                    }
                    else
                    {
                        // Screen capture is already cropped to target rect.
                        g.DrawImage(src, dx, dy, rect.Width, rect.Height);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCREENSHOT] composite failed: {ex.Message}");
            foreach (var c in captures) c.bmp?.Dispose();
            return 1;
        }
        finally
        {
            // Source bitmaps no longer needed once composited.
            foreach (var c in captures) c.bmp?.Dispose();
        }

        // -- Transparency mask: always applied when grap targets specified --
        // Pixels outside all target rects → alpha 0 (fully transparent)
        if (rects.Count > 0)
        {
            try
            {
                bmp = ApplyTransparencyMask(bmp, rects, finalLeft, finalTop);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SCREENSHOT] mask failed: {ex.Message}");
                return 1;
            }
        }

        // -- Resolve output path --
        if (string.IsNullOrWhiteSpace(outPath))
        {
            var dir = Path.Combine(Path.GetTempPath(), "wkappbot-screenshot");
            Directory.CreateDirectory(dir);
            outPath = Path.Combine(dir, $"grap_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        }
        else
        {
            var dir = Path.GetDirectoryName(outPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        try
        {
            using (bmp)
                ScreenCapture.SaveToFile(bmp, outPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCREENSHOT] save failed: {ex.Message}");
            return 1;
        }

        var abs = Path.GetFullPath(outPath);
        Console.Error.WriteLine($"[SCREENSHOT] {finalW}x{finalH} saved -> {abs}");
        Console.WriteLine($"# SCREENSHOT path={abs} rect={finalLeft},{finalTop},{finalRight},{finalBottom}");
        return 0;
    }

    /// <summary>
    /// Resolve a grap expression to a list of screen-coordinate bounding rects.
    /// Uses the same window finder as `a11y find`. If the expression matches
    /// multiple windows, all of their rects are included in the union.
    /// </summary>
    static List<Rectangle> ResolveGrapToRects(string grap)
    {
        var result = new List<Rectangle>();
        List<WindowInfo> hits;
        try
        {
            hits = WindowFinder.FindWindows(grap);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCREENSHOT] grap parse error \"{grap}\": {ex.Message}");
            return result;
        }
        foreach (var w in hits)
        {
            if (!NativeMethods.GetWindowRect(w.Handle, out var rc)) continue;
            int width  = rc.Right - rc.Left;
            int height = rc.Bottom - rc.Top;
            if (width <= 0 || height <= 0) continue;
            result.Add(new Rectangle(rc.Left, rc.Top, width, height));
        }
        return result;
    }

    /// <summary>
    /// Order targets back-to-front (deepest in Z-order first) so a Graphics.DrawImage
    /// loop composites in the correct visual stack. We approximate Z-depth by walking
    /// GW_HWNDNEXT (toward bottom) from each target hwnd and counting hops until we
    /// reach another target -- targets closer to the bottom of the chain are drawn first.
    /// Falls back to insertion order if hops are equal/unreachable.
    /// </summary>
    static int[] OrderTargetsBackToFront(
        (IntPtr hwnd, Rectangle rect, Bitmap? bmp, bool isPrintWindow)[] captures)
    {
        int n = captures.Length;
        // Compute a Z-depth score per target: how many windows sit ABOVE it system-wide.
        // GW_HWNDPREV walks toward the topmost window. Higher hop count = further back.
        var depth = new int[n];
        for (int i = 0; i < n; i++)
        {
            int hops = 0;
            var cur = NativeMethods.GetWindow(captures[i].hwnd, NativeMethods.GW_HWNDPREV);
            while (cur != IntPtr.Zero && hops < 5000)
            {
                hops++;
                cur = NativeMethods.GetWindow(cur, NativeMethods.GW_HWNDPREV);
            }
            depth[i] = hops;
        }
        // Sort indices by depth descending (deepest first = back-to-front draw order).
        var idx = Enumerable.Range(0, n).ToArray();
        Array.Sort(idx, (a, b) => depth[b].CompareTo(depth[a]));
        return idx;
    }

    /// <summary>
    /// Z-depth hops via GW_HWNDPREV chain: how many windows sit ABOVE this hwnd
    /// system-wide. Higher hop count = further back. Mirrors the depth probe in
    /// OrderTargetsBackToFront so the merged-ambiguous score uses the same metric.
    /// </summary>
    static int CountZHops(IntPtr hwnd)
    {
        int hops = 0;
        var cur = NativeMethods.GetWindow(hwnd, NativeMethods.GW_HWNDPREV);
        while (cur != IntPtr.Zero && hops < 5000)
        {
            hops++;
            cur = NativeMethods.GetWindow(cur, NativeMethods.GW_HWNDPREV);
        }
        return hops;
    }

    /// <summary>
    /// Print a merged, score-ranked list of every candidate across every grap
    /// when at least one grap is ambiguous. Each row is tagged with its source
    /// grap so the user can see which expression produced which match.
    ///
    /// Score = Coverage * 0.6 + zDepthScore * 0.4
    ///   Coverage    = WindowInfo.Coverage (pattern specificity, 0..1)
    ///   zDepthScore = 1.0 / (1.0 + zHops)   (closer to top of Z-order = higher)
    ///
    /// First entry per grap (highest score within that grap) is tagged BEST,
    /// the rest AMB. The whole list is sorted by score descending.
    /// </summary>
    static void PrintMergedAmbiguousCandidates(
        List<(string grap, List<(WindowInfo info, Rectangle rect)> usable)> perGrap)
    {
        // Compute (grap, info, score, isBest) for every usable candidate.
        var rows = new List<(string grap, WindowInfo info, double score, bool isBest)>();
        foreach (var (grap, usable) in perGrap)
        {
            // Score every candidate within this grap, then mark the highest as BEST.
            var scored = new List<(WindowInfo info, double score)>(usable.Count);
            foreach (var (info, _) in usable)
            {
                int hops = CountZHops(info.Handle);
                double zDepthScore = 1.0 / (1.0 + hops);
                double score = info.Coverage * 0.6 + zDepthScore * 0.4;
                scored.Add((info, score));
            }
            // Pick the BEST index for this grap (highest score, first wins on tie).
            int bestIdx = 0;
            for (int i = 1; i < scored.Count; i++)
                if (scored[i].score > scored[bestIdx].score) bestIdx = i;
            for (int i = 0; i < scored.Count; i++)
                rows.Add((grap, scored[i].info, scored[i].score, i == bestIdx));
        }

        // Global sort by score descending; ties keep insertion order (stable sort).
        rows.Sort((a, b) => b.score.CompareTo(a.score));

        int totalGraps = perGrap.Count;
        int ambiguousGraps = perGrap.Count(p => p.usable.Count > 1);
        Console.Error.WriteLine(
            $"[SCREENSHOT] ambiguous -- {rows.Count} candidates across {ambiguousGraps} grap(s) " +
            $"(of {totalGraps} total). Narrow each grap to 1 match:");
        Console.Error.WriteLine();

        foreach (var (grap, info, score, isBest) in rows)
        {
            string procName = "?";
            try
            {
                NativeMethods.GetWindowThreadProcessId(info.Handle, out uint pidW);
                if (pidW != 0)
                    procName = System.Diagnostics.Process.GetProcessById((int)pidW).ProcessName;
            }
            catch { }
            string tag = isBest ? "BEST" : "AMB ";
            Console.WriteLine(
                $"{tag}  score={score:F2}  hwnd:0x{info.Handle.ToInt64():X8}  " +
                $"\"{info.Title}\"  ({procName})  [grap: \"{grap}\"]");
        }
    }

    /// <summary>
    /// Dim every pixel that falls outside ALL target rects to 40% alpha; pixels
    /// inside any target rect remain fully opaque. Returns a new 32bpp ARGB
    /// bitmap (PNG-friendly transparency); the input bitmap is disposed.
    ///
    /// Uses LockBits for ~50-100x faster pixel access than SetPixel.
    /// </summary>
    static Bitmap ApplyTransparencyMask(Bitmap src, List<Rectangle> targetRects, int originX, int originY)
    {
        int w = src.Width;
        int h = src.Height;
        var dst = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        try
        {
            // Copy source into 32bpp ARGB destination.
            using (var g = Graphics.FromImage(dst))
                g.DrawImage(src, 0, 0, w, h);
        }
        finally
        {
            src.Dispose();
        }

        // Pre-translate target rects into bitmap-local coordinates.
        var localRects = new Rectangle[targetRects.Count];
        for (int i = 0; i < targetRects.Count; i++)
        {
            var r = targetRects[i];
            localRects[i] = new Rectangle(r.Left - originX, r.Top - originY, r.Width, r.Height);
        }

        var rect = new Rectangle(0, 0, w, h);
        var data = dst.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        try
        {
            int stride = data.Stride;
            unsafe
            {
                byte* basePtr = (byte*)data.Scan0.ToPointer();
                for (int py = 0; py < h; py++)
                {
                    byte* row = basePtr + py * stride;
                    for (int px = 0; px < w; px++)
                    {
                        bool inside = false;
                        for (int i = 0; i < localRects.Length; i++)
                        {
                            var lr = localRects[i];
                            if (px >= lr.Left && px < lr.Right && py >= lr.Top && py < lr.Bottom)
                            {
                                inside = true;
                                break;
                            }
                        }
                        if (!inside)
                        {
                            // Zero out entire pixel (BGRA all 0 = transparent black)
                            byte* pixel = row + px * 4;
                            pixel[0] = 0; pixel[1] = 0; pixel[2] = 0; pixel[3] = 0;
                        }
                    }
                }
            }
        }
        finally
        {
            dst.UnlockBits(data);
        }
        return dst;
    }

    /// <summary>
    /// Temporarily make every non-target top-level window that overlaps the capture
    /// rect AND sits above any target in Z-order fully transparent (alpha=0).
    /// Returns a list of (hwnd, prevExStyle, prevAlpha) tuples for RestoreBlockingWindows.
    /// `prevAlpha` is 0xFF (opaque) when the window was not previously layered.
    ///
    /// Z-order rule: only suppress windows that appear in the GW_HWNDPREV chain
    /// before any target hwnd -- i.e. drawn ABOVE that target. Windows fully
    /// behind every target cannot occlude them, so we leave them alone.
    /// </summary>
    static List<(IntPtr hwnd, int prevStyle, byte prevAlpha)> SuppressBlockingWindows(
        Rectangle captureRect, HashSet<IntPtr> targetHwnds)
    {
        var result = new List<(IntPtr, int, byte)>();
        if (targetHwnds.Count == 0) return result;

        // Build "above-target" set: every hwnd in front of (Z-order) any target.
        // GW_HWNDPREV walks toward the topmost window from a starting hwnd.
        var aboveTarget = new HashSet<IntPtr>();
        foreach (var t in targetHwnds)
        {
            var prev = NativeMethods.GetWindow(t, NativeMethods.GW_HWNDPREV);
            int hops = 0;
            while (prev != IntPtr.Zero && hops < 500)
            {
                aboveTarget.Add(prev);
                prev = NativeMethods.GetWindow(prev, NativeMethods.GW_HWNDPREV);
                hops++;
            }
        }
        if (aboveTarget.Count == 0) return result;

        IntPtr shell = NativeMethods.GetShellWindow();
        IntPtr desktop = NativeMethods.GetDesktopWindow();

        var candidates = new List<IntPtr>();
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            if (targetHwnds.Contains(hwnd)) return true;
            if (hwnd == shell || hwnd == desktop) return true;
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;
            if (NativeMethods.IsIconic(hwnd)) return true;
            if (!aboveTarget.Contains(hwnd)) return true;
            if (!NativeMethods.GetWindowRect(hwnd, out var rc)) return true;
            int w = rc.Right - rc.Left, h = rc.Bottom - rc.Top;
            if (w <= 0 || h <= 0) return true;
            var winRect = new Rectangle(rc.Left, rc.Top, w, h);
            if (!winRect.IntersectsWith(captureRect)) return true;
            candidates.Add(hwnd);
            return true;
        }, IntPtr.Zero);

        foreach (var hwnd in candidates)
        {
            try
            {
                int prevStyle = NativeMethods.GetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE);
                byte prevAlpha = 0xFF;
                bool wasLayered = (prevStyle & NativeMethods.WS_EX_LAYERED) != 0;
                if (wasLayered)
                {
                    NativeMethods.GetLayeredWindowAttributes(hwnd, out _, out prevAlpha, out _);
                }
                else
                {
                    NativeMethods.SetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE,
                        prevStyle | NativeMethods.WS_EX_LAYERED);
                }
                if (NativeMethods.SetLayeredWindowAttributes(hwnd, 0, 0, NativeMethods.LWA_ALPHA))
                    result.Add((hwnd, prevStyle, prevAlpha));
                else if (!wasLayered)
                    NativeMethods.SetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE, prevStyle);
            }
            catch { /* ignore single-window failures */ }
        }
        return result;
    }

    /// <summary>
    /// Restore windows previously made transparent by SuppressBlockingWindows.
    /// If the window was originally layered, restore its prior alpha; otherwise
    /// strip the WS_EX_LAYERED bit we added.
    /// </summary>
    static void RestoreBlockingWindows(List<(IntPtr hwnd, int prevStyle, byte prevAlpha)> suppressed)
    {
        if (suppressed == null) return;
        foreach (var (hwnd, prevStyle, prevAlpha) in suppressed)
        {
            try
            {
                bool wasLayered = (prevStyle & NativeMethods.WS_EX_LAYERED) != 0;
                if (wasLayered)
                {
                    NativeMethods.SetLayeredWindowAttributes(hwnd, 0, prevAlpha, NativeMethods.LWA_ALPHA);
                }
                else
                {
                    // Restore original ex-style (which removes WS_EX_LAYERED)
                    NativeMethods.SetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE, prevStyle);
                }
            }
            catch { /* best-effort */ }
        }
    }

    static void PrintScreenshotUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  wkappbot screenshot [\"expr1\" \"expr2\" ...] [--padding N] [--out file.png]");
        Console.WriteLine();
        Console.WriteLine("grap args required -- auto-crops to exact target regions (token-efficient).");
        Console.WriteLine();
        Console.WriteLine("  Each grap must resolve to EXACTLY 1 window/element (exit 2 if ambiguous).");
        Console.WriteLine("  Capture pipeline (per target):");
        Console.WriteLine("    1. PrintWindow(PW_RENDERFULLCONTENT) -- focusless, works even if occluded");
        Console.WriteLine("    2. If blank (GPU-rendered): suppress blocking windows (alpha=0),");
        Console.WriteLine("       CaptureScreenRegion, restore -- 30ms settle before capture");
        Console.WriteLine("  Root ancestors of child targets are never suppressed.");
        Console.WriteLine("  Compositing: painter's algorithm (Z-order back-to-front via GW_HWNDPREV)");
        Console.WriteLine("               each target drawn at its screen-position offset in union bbox");
        Console.WriteLine("  Masking: pixels outside ALL target rects → 0x00000000 (transparent)");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  \"grap\"...   positional grap expression(s) -- one exact match each");
        Console.WriteLine("  --padding N  expand union bbox by N% per side (default: 0)");
        Console.WriteLine("  --out path   output PNG (default: %TEMP%/wkappbot-screenshot/*.png)");
        Console.WriteLine();
        Console.WriteLine("Exit codes:  0=ok  1=error (no grap / no match)  2=ambiguous grap");
        Console.WriteLine("Output:      # SCREENSHOT path=<abs> rect=L,T,R,B");
    }
}
