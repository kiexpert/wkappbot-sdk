using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;
using System.Drawing.Imaging;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Click: BoundingRect center -> SendInput (requires focus) --
    static bool A11yClick(AutomationElement el, IntPtr hwnd)
    {
        var rect = GetBoundingRect(el);
        if (rect == null)
        {
            Console.Error.WriteLine("[A11Y] click — no BoundingRect available");
            return false;
        }

        int cx = (rect.Value.Left + rect.Value.Right) / 2;
        int cy = (rect.Value.Top + rect.Value.Bottom) / 2;

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                {
                    legacy.Pattern.DoDefaultAction();
                    Console.Error.WriteLine($"[A11Y] click — LegacyIA DoDefaultAction at ({cx},{cy})");
                    RecordTierSuccess(hwnd, el, "click", "LegacyIA DoDefaultAction", KnowhowCategory.Focusless);
                    return true;
                }
                _autoHealTiers?.Add($"LegacyIA DoDefaultAction: no default action (supported but empty)");
            }
            else
            {
                _autoHealTiers?.Add("LegacyIA DoDefaultAction: pattern not supported");
            }
        }
        catch (Exception ex) { _autoHealTiers?.Add($"LegacyIA DoDefaultAction: exception ({ex.GetType().Name})"); }

        var elHwnd = GetElementHwnd(el);
        bool flutterNoResponse = false;
        if (elHwnd != IntPtr.Zero)
        {
            // FLUTTERVIEW child? Route directly (bypasses Win32 routing, focusless)
            var flutterView = FindFlutterViewChild(elHwnd);
            if (flutterView != IntPtr.Zero)
            {
                Console.Error.WriteLine($"[A11Y] click — FLUTTERVIEW direct at ({cx},{cy})");
                if (PostClickToFlutterView(el, flutterView))
                { RecordTierSuccess(elHwnd, el, "click", "FLUTTERVIEW WM_LBUTTON", KnowhowCategory.Focusless); return true; }
                // No UI response (e.g. ModalBottomSheet canvas button) → physical click fallback
                Console.WriteLine("[A11Y] click — FLUTTERVIEW no response → physical click fallback");
                _autoHealTiers?.Add("FLUTTERVIEW WM_LBUTTON: no UI response (element alive + pixel-diff < 2%)");
                flutterNoResponse = true;
            }
            else
            {
                if (IsUipiBlocked(elHwnd))
                {
                    _autoHealTiers?.Add("Win32 WM_LBUTTON: UIPI blocked (target IL > current IL)");
                }
                else
                {
                    var pt = new POINT { X = cx, Y = cy };
                    NativeMethods.ScreenToClient(elHwnd, ref pt);
                    var lParam = (IntPtr)(pt.X | (pt.Y << 16));
                    NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)1, lParam);
                    Thread.Sleep(50);
                    NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
                    Console.Error.WriteLine($"[A11Y] click — Win32 WM_LBUTTON at ({cx},{cy})");
                    RecordTierSuccess(elHwnd, el, "click", "Win32 WM_LBUTTON", KnowhowCategory.Focusless);
                    return true;
                }
            }
        }
        else if (IsElectronWindow(hwnd))
        {
            // Electron/VS Code: elements have no NativeWindowHandle → treat window as surface.
            // Send WM_LBUTTON to Chrome_RenderWidgetHostHWND (or root Chrome_WidgetWin_1).
            // This is focusless and more reliable than SendInput for Chromium-based windows.
            if (TryElectronSurfaceClick(hwnd, cx, cy, el))
                return true;
            _autoHealTiers?.Add("Electron surface WM_LBUTTON: no UI response");
        }

        // Physical click: last resort, or FLUTTERVIEW-no-response fallback.
        // For flutter fallback: run InputReadiness.Probe() to avoid focus-steal when user is active.
        if (flutterNoResponse)
        {
            var probe = CreateInputReadiness();
            var probeResult = probe.Probe(new InputReadinessRequest
            {
                TargetHwnd     = elHwnd,
                IntendedAction = "click",
            });
            if (probeResult.ActiveBlocker != null)
            {
                Console.Error.WriteLine($"[A11Y] click — physical blocked by: \"{probeResult.ActiveBlocker.Title}\"");
                _autoHealTiers?.Add($"Physical SendInput: Probe blocked by \"{probeResult.ActiveBlocker.Title}\"");
                return false;
            }
            NativeMethods.SmartSetForegroundWindow(elHwnd);
            Thread.Sleep(150);
        }

        // Before-screenshot for pixel-diff verify (only if rect is usable)
        System.Drawing.Bitmap? sendBefore = null;
        if (rect.Value.Width > 4 && rect.Value.Height > 4)
            try { sendBefore = ScreenCapture.CaptureScreenRegion(rect.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height); } catch { }

        NativeMethods.SetCursorPos(cx, cy);
        Thread.Sleep(30);

        // Pre-click cursor diagnostics: position verify + EditCursor hint
        {
            NativeMethods.GetCursorPos(out var curPos);
            bool onTarget = rect.Value.Contains(curPos.X, curPos.Y);
            if (!onTarget)
                Console.Error.Write($"[WARN:cursor@({curPos.X},{curPos.Y}) outside target rect] ");

            if (NativeMethods.IsCurrentCursorIBeam())
            {
                NativeMethods.SetPropW(elHwnd, "WKAppBot.EditCursor", (IntPtr)1);
                Console.Write("[HINT:EditCursor] ");
            }
        }

        var inputs = new INPUT[2];
        inputs[0].type = INPUT.INPUT_MOUSE;
        inputs[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTDOWN;
        inputs[1].type = INPUT.INPUT_MOUSE;
        inputs[1].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTUP;
        NativeMethods.SendInput(2, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
        Console.Error.WriteLine($"[A11Y] click — SendInput at ({cx},{cy})");

        // Pixel-diff verification: 280ms after cursor-move = ~310ms from SendInput
        if (sendBefore != null)
        {
            Thread.Sleep(280);
            try
            {
                using var sendAfter = ScreenCapture.CaptureScreenRegion(rect.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height);
                double diff = ComputePixelDiffRatio(sendBefore, sendAfter);
                Console.Error.WriteLine($"[A11Y] click — SendInput pixel-diff {diff:P1}");
                if (diff <= 0.02)
                {
                    // No visible UI response → report failure so FireAutoHeal can engage
                    _autoHealTiers?.Add($"SendInput physical click: no UI response (pixel-diff {diff:P1} ≤ 2%)");
                    return false;
                }
                // SendInput worked — query focusless alternative once per app+class (background)
                QueryFocuslessAlternativeOnce(hwnd, el);
            }
            catch { }
            finally { sendBefore.Dispose(); }
        }

        return true;
    }

    /// <summary>Returns true if hwnd is a Chrome_WidgetWin_1 (Electron/VS Code/Codex) window.</summary>
    static bool IsElectronWindow(IntPtr hwnd)
        => hwnd != IntPtr.Zero &&
           WindowFinder.GetClassName(hwnd).Equals("Chrome_WidgetWin_1", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Electron surface click: WM_LBUTTON to Chrome_RenderWidgetHostHWND (or root Chrome_WidgetWin_1).
    /// Chromium processes input via the renderer child — sending to root is unreliable;
    /// prefer renderer hwnd with ScreenToClient conversion.
    /// Returns true if pixel-diff confirms a UI response, false otherwise.
    /// </summary>
    static bool TryElectronSurfaceClick(IntPtr windowHwnd, int screenX, int screenY, AutomationElement? el)
    {
        // Walk up to true root Chrome_WidgetWin_1 (handles nested Electron frames)
        var root = NativeMethods.GetAncestor(windowHwnd, 2); // GA_ROOT
        if (root == IntPtr.Zero) root = windowHwnd;

        // Find Chrome_RenderWidgetHostHWND (direct child, or one level deeper)
        var rendHwnd = NativeMethods.FindWindowExW(root, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null);
        if (rendHwnd == IntPtr.Zero)
        {
            var inner = NativeMethods.FindWindowExW(root, IntPtr.Zero, "Chrome_WidgetWin_1", null);
            if (inner != IntPtr.Zero)
                rendHwnd = NativeMethods.FindWindowExW(inner, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null);
        }
        var targetHwnd = rendHwnd != IntPtr.Zero ? rendHwnd : root;

        var pt = new POINT { X = screenX, Y = screenY };
        NativeMethods.ScreenToClient(targetHwnd, ref pt);
        var lParam = (IntPtr)(pt.X | (pt.Y << 16));

        // Before-screenshot for pixel-diff verification
        System.Drawing.Bitmap? before = null;
        var rect = el != null ? GetBoundingRect(el) : null;
        if (rect != null && rect.Value.Width > 4 && rect.Value.Height > 4)
            try { before = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(rect.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height); } catch { }

        NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)1, lParam);
        Thread.Sleep(50);
        NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
        Console.Error.WriteLine($"[A11Y] click — Electron surface WM_LBUTTON screen({screenX},{screenY}) client({pt.X},{pt.Y}) hwnd=0x{targetHwnd:X}");

        if (before != null)
        {
            Thread.Sleep(250);
            try
            {
                using var after = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(rect!.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height);
                double diff = ComputePixelDiffRatio(before, after);
                Console.Error.WriteLine($"[A11Y] click — Electron surface pixel-diff {diff:P1}");
                if (diff > 0.02)
                {
                    RecordTierSuccess(windowHwnd, el!, "click", "Electron surface WM_LBUTTON", KnowhowCategory.Focusless);
                    return true;
                }
                return false; // no response → caller falls through to SendInput
            }
            catch { }
            finally { before.Dispose(); }
        }

        // No rect for pixel-diff → optimistic success
        if (el != null) RecordTierSuccess(windowHwnd, el, "click", "Electron surface WM_LBUTTON", KnowhowCategory.Focusless);
        return true;
    }

    // Send WM_LBUTTON to FLUTTER_VIEW child at element's center (screen→client conversion).
    // Returns true if click had a verified UI effect, false if no response (triggers physical fallback).
    // Verification: (1) element accessibility check → (2) pixel diff of element region.
    static bool PostClickToFlutterView(AutomationElement el, IntPtr flutterView)
    {
        var rect = GetBoundingRect(el);
        int cx, cy;
        if (rect != null)
        {
            cx = (rect.Value.Left + rect.Value.Right) / 2;
            cy = (rect.Value.Top + rect.Value.Bottom) / 2;
        }
        else
        {
            Console.Error.WriteLine("[A11Y] flutter-click — no BoundingRect, using FLUTTERVIEW center");
            NativeMethods.GetClientRect(flutterView, out var fb);
            var fp = new POINT { X = (fb.Left + fb.Right) / 2, Y = (fb.Top + fb.Bottom) / 2 };
            NativeMethods.ClientToScreen(flutterView, ref fp);
            cx = fp.X; cy = fp.Y;
        }

        // Clamp to largest inscribed circle of BoundingRect — handles rounded-corner buttons.
        if (rect != null)
        {
            const int margin = 4;
            double rcx = (rect.Value.Left + rect.Value.Right)  / 2.0;
            double rcy = (rect.Value.Top  + rect.Value.Bottom) / 2.0;
            double radius = Math.Min(rect.Value.Width, rect.Value.Height) / 2.0 - margin;
            if (radius > 0)
            {
                double dx = cx - rcx, dy = cy - rcy;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist > radius)
                {
                    int origX = cx, origY = cy;
                    cx = (int)Math.Round(rcx + dx / dist * radius);
                    cy = (int)Math.Round(rcy + dy / dist * radius);
                    Console.Error.WriteLine($"[A11Y] flutter-click — clamped to inscribed circle ({origX},{origY})→({cx},{cy}) r={radius:F0}");
                }
            }
        }

        // Before-screenshot: capture element region for pixel-diff verification (focusless GDI BitBlt)
        System.Drawing.Bitmap? before = null;
        if (rect != null && rect.Value.Width > 0 && rect.Value.Height > 0)
            try { before = ScreenCapture.CaptureScreenRegion(rect.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height); } catch { }

        // Convert screen → FLUTTERVIEW client coords and send WM_LBUTTON
        var pt = new POINT { X = cx, Y = cy };
        NativeMethods.ScreenToClient(flutterView, ref pt);
        var lParam = (IntPtr)(pt.X | (pt.Y << 16));
        NativeMethods.PostMessageW(flutterView, NativeMethods.WM_LBUTTONDOWN, (IntPtr)1, lParam);
        Thread.Sleep(50);
        NativeMethods.PostMessageW(flutterView, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
        Console.Error.WriteLine($"[A11Y] flutter-click — FLUTTER_VIEW WM_LBUTTON at screen({cx},{cy}) client({pt.X},{pt.Y})");

        // Verify click had effect (wait 250ms total = 300ms from WM_LBUTTONDOWN):
        // Pass 1 — element accessibility: if element gone → click worked (dismiss happened)
        Thread.Sleep(250);
        bool elementGone = false;
        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            elementGone = r.IsEmpty || (r.Width <= 0 && r.Height <= 0);
        }
        catch { elementGone = true; }  // ElementNotAvailableException → element disappeared

        if (elementGone)
        {
            Console.WriteLine("[A11Y] flutter-click — element gone after 300ms → verified ✓");
            before?.Dispose();
            return true;
        }

        // Pass 2 — pixel diff: capture same region after click, compare with before snapshot.
        // Catches visual changes that don't dismiss the element (state toggle, ripple, etc.).
        if (before != null && rect != null)
        {
            try
            {
                using var after = ScreenCapture.CaptureScreenRegion(rect.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height);
                double diff = ComputePixelDiffRatio(before, after);
                Console.Error.WriteLine($"[A11Y] flutter-click — pixel diff {diff:P1}");
                if (diff > 0.02)  // 2% threshold: ripple, color change, or dismiss animation
                {
                    Console.WriteLine("[A11Y] flutter-click — visual change detected → verified ✓");
                    return true;
                }
            }
            catch { }
            finally { before.Dispose(); }
        }
        else
        {
            before?.Dispose();
        }

        Console.WriteLine("[A11Y] flutter-click — no UI change after 300ms → unverified, physical fallback needed");
        return false;
    }

    // Compute pixel-difference ratio between two bitmaps (sampled every 4px for speed).
    // Returns 0.0 (identical) .. 1.0 (completely different).
    // Per-channel threshold=20 to ignore minor rendering jitter.
    static double ComputePixelDiffRatio(System.Drawing.Bitmap a, System.Drawing.Bitmap b)
    {
        int w = Math.Min(a.Width, b.Width);
        int h = Math.Min(a.Height, b.Height);
        if (w <= 0 || h <= 0) return 0;
        int total = 0, diff = 0;
        const int threshold = 20;
        for (int y = 0; y < h; y += 4)
        {
            for (int x = 0; x < w; x += 4)
            {
                var pa = a.GetPixel(x, y);
                var pb = b.GetPixel(x, y);
                total++;
                if (Math.Abs(pa.R - pb.R) > threshold ||
                    Math.Abs(pa.G - pb.G) > threshold ||
                    Math.Abs(pa.B - pb.B) > threshold)
                    diff++;
            }
        }
        return total > 0 ? (double)diff / total : 0;
    }
}
