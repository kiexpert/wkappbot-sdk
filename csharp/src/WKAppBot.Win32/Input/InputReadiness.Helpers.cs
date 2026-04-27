using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public sealed partial class InputReadiness
{
    // -- PrintReport: 콘솔 출력 ----------------------------------─

    public static void PrintReport(InputReadinessReport report)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[READINESS] ");
        Console.ResetColor();
        Console.Write($"Target: [{report.TargetClass}]");
        if (report.TargetAutomationId != null)
            Console.Write($" aid={report.TargetAutomationId}");
        if (report.TargetName != null)
            Console.Write($" \"{report.TargetName}\"");
        Console.Write($" (pid={report.TargetPid} {report.ProcessName})");
        Console.WriteLine();

        // 윈도우 상태
        if (!report.FormVisible || !report.FormEnabled || report.FormIconic)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  [STATE] ");
            if (!report.FormVisible) Console.Write("HIDDEN ");
            if (!report.FormEnabled) Console.Write("DISABLED ");
            if (report.FormIconic) Console.Write("MINIMIZED ");
            Console.ResetColor();
            Console.WriteLine();
        }

        // 권한
        if (report.ElevationMismatch)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [ELEVATION] Target elevated, wkappbot is not! Run as admin.");
            Console.ResetColor();
        }

        // 유저 입력 간섭
        if (report.UserInputRecent)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [USER] Recent input detected ({report.UserIdleMs}ms ago) -- focus steal may interfere");
            Console.ResetColor();
        }

        // 포커스 양보 결과
        if (report.UserYieldRequested)
        {
            Console.ForegroundColor = report.UserYieldConfirmed ? ConsoleColor.Green : ConsoleColor.Yellow;
            var msg = report.UserYieldConfirmed
                ? (report.UserYieldFocusAcquired ? "User confirmed focus yield" : "Auto-approved (user idle)")
                : "Yield safety timeout";
            Console.WriteLine($"  [YIELD] {msg}");
            Console.ResetColor();
        }

        // 메서드 목록
        if (report.Methods.Count > 0)
        {
            Console.Write("  [METHODS] ");
            foreach (var m in report.Methods)
            {
                if (m.Available)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"v{m.Name}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"X{m.Name}");
                }
                Console.Write(m.Focusless ? "(FL) " : "(FN) ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // 방해꾼
        if (report.ActiveBlocker != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [BLOCKER] [{report.ActiveBlocker.ClassPath}] \"{report.ActiveBlocker.Title}\"");
            if (report.ActiveBlocker.MessageText != null)
                Console.WriteLine($"            msg: {report.ActiveBlocker.MessageText}");
            Console.ResetColor();
        }

        // 종합 판정
        Console.ForegroundColor = report.Ready ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write($"  [VERDICT] {(report.Ready ? "READY" : "NOT READY")}");
        if (report.RecommendedMethod != null)
            Console.Write($" -> {report.RecommendedMethod}");
        if (report.RequiresFocus)
            Console.Write(" (focus needed)");
        Console.ResetColor();
        Console.WriteLine();
    }

    // -- Private: UIA 패턴 전수조사 ------------------------------─

    private static void ProbeUiaPatterns(AutomationElement el, string? action, List<InputMethod> methods)
    {
        // Invoke (focusless click)
        bool invokeOk = SafePatternCheck(() => el.Patterns.Invoke.IsSupported);
        methods.Add(new InputMethod("UIA.Invoke", InputMethodCategory.UiaPattern, true,
            invokeOk, invokeOk ? null : "Pattern not supported"));

        // LegacyIAccessible DoDefaultAction (Electron Hyperlink fallback)
        bool legacyInvoke = false;
        string? legacyAction = null;
        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacyAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                legacyInvoke = !string.IsNullOrEmpty(legacyAction);
            }
        }
        catch { }
        methods.Add(new InputMethod("MSAA.DoDefaultAction", InputMethodCategory.MsaaAccessible, true,
            legacyInvoke, legacyInvoke ? null : "No default action"));

        // Value (focusless type)
        bool valueOk = SafePatternCheck(() => el.Patterns.Value.IsSupported);
        bool valueRo = false;
        if (valueOk)
        {
            try { valueRo = el.Patterns.Value.Pattern.IsReadOnly.Value; } catch { }
        }
        methods.Add(new InputMethod("UIA.Value", InputMethodCategory.UiaPattern, true,
            valueOk && !valueRo, valueOk ? (valueRo ? "Read-only" : null) : "Pattern not supported"));

        // Toggle (focusless toggle)
        bool toggleOk = SafePatternCheck(() => el.Patterns.Toggle.IsSupported);
        methods.Add(new InputMethod("UIA.Toggle", InputMethodCategory.UiaPattern, true,
            toggleOk, toggleOk ? null : "Pattern not supported"));

        // SelectionItem
        bool selectOk = SafePatternCheck(() => el.Patterns.SelectionItem.IsSupported);
        methods.Add(new InputMethod("UIA.Select", InputMethodCategory.UiaPattern, true,
            selectOk, selectOk ? null : "Pattern not supported"));

        // ExpandCollapse
        bool expandOk = SafePatternCheck(() => el.Patterns.ExpandCollapse.IsSupported);
        methods.Add(new InputMethod("UIA.ExpandCollapse", InputMethodCategory.UiaPattern, true,
            expandOk, expandOk ? null : "Pattern not supported"));

        // RangeValue
        bool rangeOk = SafePatternCheck(() => el.Patterns.RangeValue.IsSupported);
        methods.Add(new InputMethod("UIA.RangeValue", InputMethodCategory.UiaPattern, true,
            rangeOk, rangeOk ? null : "Pattern not supported"));

        // NOTE: Scroll 패턴은 GetSupportedPatterns() COM 오염 위험 -> 개별 체크만
        bool scrollOk = SafePatternCheck(() => el.Patterns.Scroll.IsSupported);
        methods.Add(new InputMethod("UIA.Scroll", InputMethodCategory.UiaPattern, true,
            scrollOk, scrollOk ? null : "Pattern not supported"));
    }

    /// <summary>UIA 패턴 체크: COM 예외 방지용 개별 try-catch.</summary>
    private static bool SafePatternCheck(Func<bool> check)
    {
        try { return check(); } catch { return false; }
    }

    // -- Private: Win32 메시지 전수조사 --------------------------─

    private static void ProbeWin32Messages(string targetClass, List<InputMethod> methods)
    {
        bool isEdit = targetClass.Contains("Edit") || targetClass.Contains("EDIT");
        bool isButton = targetClass == "Button" || targetClass.Contains("Button");
        bool isMfc = targetClass.StartsWith("Afx") || targetClass.Contains("CMaskEdit");
        bool isCombo = targetClass.Contains("ComboBox") || targetClass.Contains("COMBOBOX");

        // PostMessage WM_CHAR (focusless for MFC/Edit)
        methods.Add(new InputMethod("PostMessage.WmChar", InputMethodCategory.Win32Message, true,
            isEdit || isMfc, (isEdit || isMfc) ? null : $"Class '{targetClass}' is not Edit/MFC"));

        // WM_SETTEXT (Edit controls)
        methods.Add(new InputMethod("Win32.WmSetText", InputMethodCategory.Win32Message, true,
            isEdit, isEdit ? null : "Not an Edit control"));

        // EM_REPLACESEL (Edit/RichEdit)
        methods.Add(new InputMethod("Win32.EmReplaceSel", InputMethodCategory.Win32Message, true,
            isEdit, isEdit ? null : "Not an Edit control"));

        // BM_CLICK (Button controls)
        methods.Add(new InputMethod("Win32.BmClick", InputMethodCategory.Win32Message, true,
            isButton, isButton ? null : "Not a Button control"));

        // WM_LBUTTON (any window, focusless PostMessage click)
        methods.Add(new InputMethod("PostMessage.WmLButton", InputMethodCategory.Win32Message, true,
            true, null)); // always possible to attempt
    }

    // -- Private: SendInput 전수조사 ----------------------------─

    private static void ProbeSendInput(bool targetElevated, bool weAreElevated, List<InputMethod> methods)
    {
        bool elevOk = !targetElevated || weAreElevated;
        bool focuslessBlocked = NativeHookFocusless.Enabled;

        // Mouse click via SendInput
        methods.Add(new InputMethod("SendInput.Mouse", InputMethodCategory.SendInput, false,
            elevOk && !focuslessBlocked,
            !elevOk ? "Elevation mismatch" : focuslessBlocked ? "NativeHookFocusless active" : null));

        // Keyboard via SendInput
        methods.Add(new InputMethod("SendInput.Keyboard", InputMethodCategory.SendInput, false,
            elevOk && !focuslessBlocked,
            !elevOk ? "Elevation mismatch" : focuslessBlocked ? "NativeHookFocusless active" : null));
    }

    // -- Private: 액션별 포커스 필요 판단 --------------------------

    /// <summary>
    /// 해당 액션에 대해 포커스리스 메서드가 없어서 포커스니드로 폴백해야 하는지 판단.
    /// 포커스리스 경로가 있으면 false -> 알림창 불필요.
    /// </summary>
    private static bool NeedsFocusForAction(List<InputMethod> methods, string? action)
    {
        var act = action?.ToLowerInvariant();

        // 핫키/키입력은 항상 SendInput 필요 (포커스리스 대안 없음)
        if (act is "hotkey" or "key" or "press_key")
            return true;

        // input 명령: focusless 여부와 관계없이 항상 yield 팝업 로직 진입
        // (내부에서 전경+idle이면 자동승인, 비전경/유저활동이면 팝업)
        if (act is "input")
            return true;

        // 텍스트 입력: UIA.Value / WmChar / WmSetText / EmReplaceSel / MSAA 중 하나 필요
        if (act is "type" or "type_text")
            return !methods.Any(m => m.Available && m.Focusless
                && (m.Name.Contains("Value") || m.Name.Contains("WmChar")
                    || m.Name.Contains("WmSetText") || m.Name.Contains("EmReplaceSel")
                    || m.Name.Contains("Accessible")));

        // 그 외 (click, toggle, scroll 등): 포커스리스 메서드가 있으면 OK
        return !methods.Any(m => m.Available && m.Focusless);
    }

    // -- Private: 타겟 Rect --------------------------------------

    private static System.Drawing.Rectangle GetTargetRect(IntPtr hwnd, AutomationElement? uia)
    {
        // UIA BoundingRectangle 우선
        if (uia != null)
        {
            try
            {
                var rect = uia.Properties.BoundingRectangle.ValueOrDefault;
                if (rect.Width > 0 && rect.Height > 0)
                    return new System.Drawing.Rectangle(
                        (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            }
            catch { }
        }

        // Win32 GetWindowRect 폴백
        NativeMethods.GetWindowRect(hwnd, out var wr);
        return new System.Drawing.Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);
    }

    // -- Private: ClassPath 빌드 --------------------------------─

    private static string BuildClassPath(IntPtr hWnd)
    {
        var parts = new List<string>();
        var current = hWnd;
        for (int i = 0; i < 5 && current != IntPtr.Zero; i++)
        {
            var sb = new StringBuilder(256);
            NativeMethods.GetClassNameW(current, sb, 256);
            var cls = sb.ToString();
            if (string.IsNullOrEmpty(cls)) break;
            parts.Add(cls);
            current = NativeMethods.GetParent(current);
        }
        parts.Reverse();
        return string.Join("/", parts);
    }

    private static void BringTargetForward(IntPtr targetHwnd, IntPtr mainHwnd)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  [PATH] BringForward: ");
        var parentClass = new StringBuilder(256);
        var parent = NativeMethods.GetParent(targetHwnd);
        if (parent != IntPtr.Zero)
        {
            NativeMethods.GetClassNameW(parent, parentClass, 256);
            if (parentClass.ToString() == "MDIClient")
            {
                Console.Write("WM_MDIACTIVATE ");
                NativeMethods.SendMessageW(parent, 0x0222, targetHwnd, IntPtr.Zero);
                Console.WriteLine("OK (focusless)");
                Console.ResetColor();
                return;
            }
        }
        Console.Write("SetWindowPos HWND_TOP ");
        NativeMethods.SetWindowPos(mainHwnd, NativeMethods.HWND_TOP,
            0, 0, 0, 0,
            NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
        Console.WriteLine("OK (focusless)");
        Console.ResetColor();
    }
}
