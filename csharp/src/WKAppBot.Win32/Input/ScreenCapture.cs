using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Screen and window capture.
/// Handles dual-monitor with negative coordinates correctly.
/// </summary>
public static partial class ScreenCapture
{
    /// <summary>
    /// Optional step-level logger for capture diagnostics.
    /// Wire to PulseStep.Line in CLI layer; defaults to Console.Error.WriteLine.
    /// </summary>
    public static Action<string>? StepLogger { get; set; }

    private static void Log(string msg)
        => (StepLogger ?? Console.Error.WriteLine)($"[CAPTURE] {msg}");

    /// <summary>
    /// Capture a specific window's client area.
    /// Falls back to screen-region capture if PrintWindow fails (common with HTS).
    /// If minimized, captures the tiny taskbar icon area (caller can detect via IsIconic beforehand).
    /// </summary>
    public static Bitmap CaptureWindow(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        int w = rect.Width;
        int h = rect.Height;
        if (w <= 0 || h <= 0) throw new InvalidOperationException("Window has zero size");

        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Step 1: PrintWindow PW_RENDERFULLCONTENT (focusless, zero side effects)
        Log($"step=1 PrintWindow hwnd=0x{hWnd:X} size={w}x{h}");
        var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            IntPtr hdc = g.GetHdc();
            bool ok = NativeMethods.PrintWindow(hWnd, hdc, NativeMethods.PW_RENDERFULLCONTENT);
            g.ReleaseHdc(hdc);

            if (ok && !IsBlankBitmap(bmp))
            {
                Log($"step=1 PrintWindow OK ({sw.ElapsedMilliseconds}ms)");
                return bmp;
            }
            Log($"step=1 PrintWindow BLANK ok={ok} ({sw.ElapsedMilliseconds}ms) -> step2");
        }
        bmp.Dispose();

        // Step 2: Z-order BringToFront (SWP_NOACTIVATE -- no focus steal)
        sw.Restart();
        Log("step=2 BringToFront SWP_NOACTIVATE");
        var step2 = CaptureWindowWithBringToFront(hWnd);
        if (!IsBlankBitmap(step2))
        {
            Log($"step=2 BringToFront OK ({sw.ElapsedMilliseconds}ms)");
            return step2;
        }
        step2.Dispose();
        Log($"step=2 BringToFront BLANK ({sw.ElapsedMilliseconds}ms) -> step3");

        // Step 3: Hide covering windows via alpha=0 (no focus steal, works for elevated targets)
        sw.Restart();
        Log("step=3 HideOverlays alpha=0");
        var step3 = CaptureWindowHideOverlays(hWnd);
        if (step3 != null && !IsBlankBitmap(step3))
        {
            Log($"step=3 HideOverlays OK ({sw.ElapsedMilliseconds}ms)");
            return step3;
        }
        step3?.Dispose();
        Log($"step=3 HideOverlays BLANK/SKIP ({sw.ElapsedMilliseconds}ms) -> step4");

        // Step 4: Focus borrow (last resort -- SetForegroundWindow briefly then restore)
        sw.Restart();
        Log("step=4 FocusBorrow SetForegroundWindow");
        var step4 = CaptureWindowWithFocusBorrow(hWnd);
        Log($"step=4 FocusBorrow done blank={IsBlankBitmap(step4)} ({sw.ElapsedMilliseconds}ms)");
        return step4;
    }

    /// <summary>
    /// Options-aware capture overload. Wraps <see cref="CaptureWindow(IntPtr)"/> and adds:
    ///   * blank-rejection (returns null when <see cref="CaptureOptions.RejectBlank"/> is true and result is blank)
    ///   * debug save-to-disk (<see cref="CaptureOptions.SaveDebug"/>)
    ///   * per-call step logger (temporarily swaps <see cref="StepLogger"/>).
    /// Thread-note: StepLogger is a static field; this method restores the prior value in a finally block,
    /// but concurrent callers can race. Acceptable for current single-threaded capture call sites.
    /// </summary>
    public static Bitmap? CaptureWindow(IntPtr hWnd, CaptureOptions options)
    {
        options ??= CaptureOptions.Default;
        var prevLogger = StepLogger;
        if (options.StepLogger != null) StepLogger = options.StepLogger;
        try
        {
            Bitmap result;
            try
            {
                result = CaptureWindow(hWnd);
            }
            catch (Exception ex)
            {
                Log($"options=capture failed: {ex.Message}");
                return null;
            }

            if (options.RejectBlank && IsBlankBitmap(result))
            {
                Log("options=RejectBlank -> BLANK result discarded");
                result.Dispose();
                return null;
            }

            if (!string.IsNullOrEmpty(options.SaveDebug))
            {
                try
                {
                    SaveToFile(result, options.SaveDebug);
                    Log($"options=SaveDebug wrote {options.SaveDebug}");
                }
                catch (Exception ex)
                {
                    Log($"options=SaveDebug FAILED: {ex.Message}");
                }
            }

            return result;
        }
        finally
        {
            StepLogger = prevLogger;
        }
    }
}
