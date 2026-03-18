using System.Drawing;
using System.Runtime.InteropServices;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    /// <summary>
    /// Get caret screen rect via UIA TextPattern.GetSelection().GetBoundingRectangles().
    /// Primary caret check — cross-process, no AttachThreadInput needed.
    /// Returns the first bounding rect of the current selection/caret.
    /// Returns null if TextPattern not supported or no selection.
    /// </summary>
    private System.Drawing.Rectangle? GetCaretRectViaUia()
    {
        try
        {
            var focused = _automation.FocusedElement();
            if (focused == null) return null;
            if (!focused.Patterns.Text.IsSupported) return null;
            var ranges = focused.Patterns.Text.Pattern.GetSelection();
            if (ranges == null || ranges.Length == 0) return null;
            var rects = ranges[0].GetBoundingRectangles();
            if (rects == null || rects.Length == 0) return null;
            return rects[0]; // first rect = caret or start of selection
        }
        catch { return null; }
    }

    /// <summary>
    /// Ensure caret is in PromptRect — actively secures input position with focusless fallback chain.
    /// Returns true = caret confirmed in prompt (ready for WM_CHAR), false = abort.
    /// Fallback chain: UIA → Win32 → iconic restore → thread focus → FocusPromptFocusless
    /// </summary>
    public bool EnsureCaretInPrompt(PromptInfo prompt, IntPtr rendererHwnd, ref IntPtr kbFocus)
    {
        // 1차+2차 시도 (공통 로직)
        bool? firstCheck = TryCaretCheck(prompt.PromptRect, ref kbFocus, out var src1);
        if (firstCheck.HasValue)
        {
            Console.WriteLine($"  [PROMPT] WM_CHAR: caret({src1}) {(firstCheck.Value ? "inside" : "outside")} PromptRect — {(firstCheck.Value ? "safe" : "abort")}");
            return firstCheck.Value;
        }

        // 3차: 최소화 → 포커스리스 복원 → 재시도
        if (NativeMethods.IsIconic(prompt.WindowHandle))
        {
            Console.WriteLine("  [PROMPT] WM_CHAR: iconic — restoring focuslessly");
            NativeMethods.ShowWindow(prompt.WindowHandle, 4); // SW_SHOWNOACTIVATE
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (NativeMethods.IsIconic(prompt.WindowHandle) && sw.ElapsedMilliseconds < 1000)
                Thread.Sleep(20);
            Thread.Sleep(30);
            kbFocus = NativeMethods.GetKeyboardFocusHwnd();
            bool? retry = TryCaretCheck(prompt.PromptRect, ref kbFocus, out var src2);
            if (retry.HasValue)
            {
                Console.WriteLine($"  [PROMPT] WM_CHAR: caret({src2}/iconic-retry) {(retry.Value ? "inside" : "outside")} PromptRect — {(retry.Value ? "safe" : "abort")}");
                return retry.Value;
            }
        }

        // 4차: 백그라운드 창 per-thread focus 확인
        uint targetTid = NativeMethods.GetWindowThreadProcessId(prompt.WindowHandle, out _);
        if (NativeMethods.GetThreadFocusHwnd(targetTid) == rendererHwnd)
        {
            Console.WriteLine("  [PROMPT] WM_CHAR: no caret (background) thread focus == renderer — safe");
            return true;
        }

        // 5차: FocusPromptFocusless (WM_LBUTTON) → 재시도
        Console.WriteLine("  [PROMPT] WM_CHAR: no caret — FocusPromptFocusless → retry");
        FocusPromptFocusless(rendererHwnd, prompt.PromptRect);
        Thread.Sleep(80);
        kbFocus = NativeMethods.GetKeyboardFocusHwnd();
        bool? final = TryCaretCheck(prompt.PromptRect, ref kbFocus, out var src3);
        if (final.HasValue)
        {
            Console.WriteLine($"  [PROMPT] WM_CHAR: caret({src3}/focus-retry) {(final.Value ? "inside" : "outside")} PromptRect — {(final.Value ? "safe" : "abort")}");
            return final.Value;
        }

        Console.WriteLine("  [PROMPT] WM_CHAR: all fallbacks exhausted — abort");
        return false;
    }

    /// <summary>
    /// EnsureCaretInPrompt 간편 오버로드 — renderer를 내부 자동 탐색.
    /// InputReadiness.EnsureInputPosition 콜백에서 사용: () => promptHelper.EnsureCaretInPrompt(prompt)
    /// </summary>
    public bool EnsureCaretInPrompt(PromptInfo prompt)
    {
        if (prompt.PromptRect == System.Drawing.Rectangle.Empty) return true; // PromptRect 없으면 스킵
        var renderer = prompt.PromptRect != System.Drawing.Rectangle.Empty
            ? GetRendererHwndByRect(prompt.WindowHandle, prompt.PromptRect)
            : GetRendererHwnd(prompt.WindowHandle);
        if (renderer == IntPtr.Zero) return false;
        var kbFocus = NativeMethods.GetKeyboardFocusHwnd();
        return EnsureCaretInPrompt(prompt, renderer, ref kbFocus);
    }

    /// <summary>
    /// 1차(UIA) + 2차(Win32) 캐럿 체크. null = 캐럿 없음, true/false = inside/outside PromptRect.
    /// </summary>
    private bool? TryCaretCheck(System.Drawing.Rectangle promptRect, ref IntPtr kbFocus, out string source)
    {
        var caretRect = GetCaretRectViaUia();
        if (caretRect.HasValue)
        {
            source = "UIA";
            return promptRect.Contains(new System.Drawing.Point(caretRect.Value.X, caretRect.Value.Y));
        }
        if (kbFocus != IntPtr.Zero)
        {
            var pt = GetCaretScreenPos(kbFocus);
            if (pt.HasValue) { source = "Win32"; return promptRect.Contains(pt.Value); }
        }
        source = "";
        return null;
    }

    /// <summary>
    /// Post WM_LBUTTONDOWN+UP to the renderer at PromptRect center — focuslessly places caret.
    /// No SetForegroundWindow. Chrome renderer responds to WM_LBUTTON without being foreground.
    /// Returns true if messages were posted (not a guarantee of success).
    /// </summary>
    private static bool FocusPromptFocusless(IntPtr rendererHwnd, System.Drawing.Rectangle promptScreenRect)
    {
        if (rendererHwnd == IntPtr.Zero || promptScreenRect.IsEmpty) return false;
        try
        {
            // Screen center of PromptRect → client coords of renderer
            var pt = new POINT
            {
                X = promptScreenRect.X + promptScreenRect.Width / 2,
                Y = promptScreenRect.Y + promptScreenRect.Height / 2
            };
            NativeMethods.ScreenToClient(rendererHwnd, ref pt);
            var lParam = (IntPtr)((pt.Y << 16) | (pt.X & 0xFFFF));
            const uint MK_LBUTTON = 0x0001;
            const uint WM_LBUTTONDOWN = 0x0201;
            const uint WM_LBUTTONUP = 0x0202;
            NativeMethods.PostMessageW(rendererHwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, lParam);
            NativeMethods.PostMessageW(rendererHwnd, WM_LBUTTONUP, IntPtr.Zero, lParam);
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Get caret screen position of the thread owning kbFocusHwnd.
    /// Uses AttachThreadInput trick so GetCaretPos works cross-process.
    /// Returns null if caret not available or attach fails.
    /// </summary>
    private static System.Drawing.Point? GetCaretScreenPos(IntPtr kbFocusHwnd)
    {
        if (kbFocusHwnd == IntPtr.Zero) return null;
        uint targetTid = NativeMethods.GetWindowThreadProcessId(kbFocusHwnd, out _);
        uint ourTid = GetCurrentThreadId();
        bool attached = targetTid != ourTid && NativeMethods.AttachThreadInput(ourTid, targetTid, true);
        try
        {
            var pt = new POINT();
            if (!GetCaretPos(out pt)) return null;
            NativeMethods.ClientToScreen(kbFocusHwnd, ref pt);
            return new System.Drawing.Point(pt.X, pt.Y);
        }
        finally
        {
            if (attached) NativeMethods.AttachThreadInput(ourTid, targetTid, false);
        }
    }

    [DllImport("user32.dll")]
    private static extern bool GetCaretPos(out POINT lpPoint);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    private static IntPtr GetRendererHwndByRect(IntPtr topWindow, System.Drawing.Rectangle targetRect)
    {
        var candidates = new List<IntPtr>();
        NativeMethods.EnumChildWindows(topWindow, (hwnd, _) =>
        {
            var sb = new System.Text.StringBuilder(64);
            NativeMethods.GetClassNameW(hwnd, sb, 64);
            if (sb.ToString() == "Chrome_RenderWidgetHostHWND")
                candidates.Add(hwnd);
            return true;
        }, IntPtr.Zero);

        Console.WriteLine($"  [PROMPT] WM_CHAR: found {candidates.Count} renderer(s) in window");

        // Find renderer whose rect overlaps with the prompt rect
        foreach (var hwnd in candidates)
        {
            NativeMethods.GetWindowRect(hwnd, out var wr);
            var rendRect = System.Drawing.Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
            if (rendRect.IntersectsWith(targetRect))
            {
                Console.WriteLine($"  [PROMPT] WM_CHAR: renderer 0x{hwnd:X8} matches prompt rect ({rendRect} ∩ {targetRect})");
                return hwnd;
            }
        }

        // Fallback: first renderer
        Console.WriteLine($"  [PROMPT] WM_CHAR: no rect match — using first renderer");
        return candidates.Count > 0 ? candidates[0] : IntPtr.Zero;
    }

    private static IntPtr GetRendererHwnd(IntPtr topWindow)
    {
        var rendererHwnd = NativeMethods.FindWindowExW(
            topWindow, IntPtr.Zero,
            "Chrome_RenderWidgetHostHWND", null);

        if (rendererHwnd != IntPtr.Zero)
            return rendererHwnd;

        // Try intermediate Chromium container: Chrome_WidgetWin_1 -> Chrome_RenderWidgetHostHWND
        var widgetWin = NativeMethods.FindWindowExW(
            topWindow, IntPtr.Zero,
            "Chrome_WidgetWin_1", null);
        if (widgetWin != IntPtr.Zero)
        {
            rendererHwnd = NativeMethods.FindWindowExW(
                widgetWin, IntPtr.Zero,
                "Chrome_RenderWidgetHostHWND", null);
        }
        return rendererHwnd;
    }


}
