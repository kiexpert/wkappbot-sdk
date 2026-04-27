using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Input;

namespace WKAppBot.CLI;

/// <summary>
/// Enumerates all top-level windows for a given process and dumps their
/// class name, visibility, rect, text -- specifically to find tooltip windows.
///
/// With --capture flag, captures visible tooltips_class32 windows as screenshots
/// and runs OCR on them for Phase B tooltip calibration.
/// </summary>
internal static class TooltipProbe
{
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

    private const int GWL_STYLE = -16;
    private const int GWL_EXSTYLE = -20;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public static void Run(string processName, bool capture = false)
    {
        var procs = Process.GetProcessesByName(processName);
        if (procs.Length == 0)
        {
            Console.WriteLine($"No process found: {processName}");
            return;
        }

        var targetPids = new HashSet<uint>();
        foreach (var p in procs)
        {
            targetPids.Add((uint)p.Id);
            Console.WriteLine($"Process: {p.ProcessName} PID={p.Id}");
        }

        Console.WriteLine();
        Console.WriteLine($"{"hWnd",-14} {"Visible",-8} {"Class",-30} {"Rect",-30} {"Style",-12} {"ExStyle",-12} {"Text"}");
        Console.WriteLine(new string('-', 140));

        int count = 0;
        var visibleTooltips = new List<(IntPtr hWnd, RECT rect)>();

        EnumWindows((hWnd, _) =>
        {
            GetWindowThreadProcessId(hWnd, out uint pid);
            if (!targetPids.Contains(pid)) return true;

            var sbCls = new StringBuilder(256);
            GetClassName(hWnd, sbCls, 256);
            var cls = sbCls.ToString();

            var sbTxt = new StringBuilder(512);
            GetWindowText(hWnd, sbTxt, 512);
            var txt = sbTxt.ToString();

            bool vis = IsWindowVisible(hWnd);
            GetWindowRect(hWnd, out RECT r);
            int style = GetWindowLong(hWnd, GWL_STYLE);
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            int w = r.Right - r.Left;
            int h = r.Bottom - r.Top;

            string rectStr = $"({r.Left},{r.Top}) {w}x{h}";

            Console.WriteLine($"0x{hWnd:X8}   {(vis ? "YES" : "no"),-8} {cls,-30} {rectStr,-30} {style:X8}     {exStyle:X8}     {txt}");
            count++;

            // Track visible tooltips for capture
            if (vis && cls.Contains("tooltips_class", StringComparison.OrdinalIgnoreCase) && w > 0 && h > 0)
            {
                visibleTooltips.Add((hWnd, r));
            }

            // Also enum child windows for tooltip-like classes
            if (cls.Contains("tooltips_class", StringComparison.OrdinalIgnoreCase) ||
                cls.Contains("Afx:", StringComparison.OrdinalIgnoreCase))
            {
                EnumChildWindows(hWnd, (childHwnd, __) =>
                {
                    var sbChildCls = new StringBuilder(256);
                    GetClassName(childHwnd, sbChildCls, 256);
                    var sbChildTxt = new StringBuilder(512);
                    GetWindowText(childHwnd, sbChildTxt, 512);
                    GetWindowRect(childHwnd, out RECT cr);
                    bool childVis = IsWindowVisible(childHwnd);
                    Console.WriteLine($"  child 0x{childHwnd:X8} {(childVis ? "YES" : "no"),-8} {sbChildCls,-30} ({cr.Left},{cr.Top}) {cr.Right - cr.Left}x{cr.Bottom - cr.Top}  {sbChildTxt}");
                    return true;
                }, IntPtr.Zero);
            }

            return true;
        }, IntPtr.Zero);

        Console.WriteLine();
        Console.WriteLine($"Total top-level windows: {count}");
        Console.WriteLine($"Visible tooltips: {visibleTooltips.Count}");

        // Capture visible tooltips as screenshots + OCR
        if (capture && visibleTooltips.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("=== Capturing visible tooltips ===");

            var outputDir = Path.Combine(
                Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq", "output", "tooltips");
            Directory.CreateDirectory(outputDir);

            foreach (var (hWnd, rect) in visibleTooltips)
            {
                int w = rect.Right - rect.Left;
                int h = rect.Bottom - rect.Top;
                Console.WriteLine($"\nCapturing tooltip 0x{hWnd:X8} ({rect.Left},{rect.Top}) {w}x{h}");

                // Method 1: CaptureWindow (4-tier focusless fallback)
                try
                {
                    using var bmp = ScreenCapture.CaptureWindow(hWnd, new CaptureOptions { RejectBlank = false, StepLogger = Console.WriteLine });
                    if (bmp != null)
                    {
                        var filename = $"tooltip_{hWnd:X8}_printwin.png";
                        var path = Path.Combine(outputDir, filename);
                        bmp.Save(path, ImageFormat.Png);
                        Console.WriteLine($"  CaptureWindow -> {path}");
                        RunOcr(bmp, "CaptureWindow");
                    }
                    else
                    {
                        Console.WriteLine("  CaptureWindow returned blank, trying screen region...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  CaptureWindow error: {ex.Message}");
                }

                // Method 2: Screen region capture
                try
                {
                    using var bmp = ScreenCapture.CaptureScreenRegion(rect.Left, rect.Top, w, h);
                    var filename = $"tooltip_{hWnd:X8}_screen.png";
                    var path = Path.Combine(outputDir, filename);
                    bmp.Save(path, ImageFormat.Png);
                    Console.WriteLine($"  CaptureScreenRegion -> {path}");
                    RunOcr(bmp, "CaptureScreenRegion");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  CaptureScreenRegion error: {ex.Message}");
                }
            }
        }
    }

    private static void RunOcr(Bitmap bmp, string method)
    {
        try
        {
            var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
            var ocrResult = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
            Console.WriteLine($"  OCR ({method}): {ocrResult.Words.Count} words, full text: \"{ocrResult.FullText}\"");

            // Group into lines by Y proximity
            var lines = new List<(double Y, List<(string text, double X, double W)> words)>();
            foreach (var word in ocrResult.Words)
            {
                var existingLine = lines.FindIndex(l => Math.Abs(l.Y - word.Y) < 15);
                if (existingLine >= 0)
                    lines[existingLine].words.Add((word.Text, word.X, word.Width));
                else
                    lines.Add((word.Y, new List<(string, double, double)> { (word.Text, word.X, word.Width) }));
            }

            lines.Sort((a, b) => a.Y.CompareTo(b.Y));
            foreach (var line in lines)
            {
                line.words.Sort((a, b) => a.X.CompareTo(b.X));
                var lineText = string.Join(" ", line.words.Select(w => w.text));
                Console.WriteLine($"    [{line.Y:F0}] {lineText}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  OCR error: {ex.Message}");
        }
    }

}
