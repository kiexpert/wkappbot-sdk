using WKAppBot.Abstractions;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: InputReadiness bridge methods — expose internal helpers to adapters.
// Tag: [READINESS]
internal partial class Program
{
    // ── 팩토리: InputReadiness 생성 + 모든 어댑터 연결 ──

    /// <summary>
    /// Create a fully-wired InputReadiness instance with blocker handler, knowhow broadcaster, and zoom factory.
    /// </summary>
    internal static InputReadiness CreateInputReadiness(string? handlersDir = null)
    {
        handlersDir ??= Path.Combine(DataDir, "handlers");
        var handlerMgr = Directory.Exists(handlersDir) ? new DialogHandlerManager(handlersDir) : null;

        return new InputReadiness
        {
            BlockerHandler = new BlockerHandlerAdapter(handlerMgr),
            KnowhowBroadcaster = new KnowhowBroadcasterAdapter(),
            ZoomFactory = new ReadinessZoomAdapter(),
            UserInputWait = new UserInputWaitAdapter(),
            ElevationRequester = new ElevationRequesterAdapter(),
        };
    }

    // ── 팩토리: ActionReadiness (AAR) 생성 ──

    /// <summary>
    /// Create an ActionReadiness (AAR) instance wrapping the given InputReadiness.
    /// </summary>
    internal static ActionReadiness CreateActionReadiness(InputReadiness readiness)
        => new(readiness);

    // ── 공용 입력위치확보: iconic zoom + focusless restore + blocker dismiss ──

    /// <summary>
    /// Shared input readiness pipeline for any window: iconic zoom → focusless restore → blocker dismiss.
    /// Used by A11yCommand, AskCommands, and any command that needs a window ready for interaction.
    /// Returns true if the window was restored from iconic state.
    /// </summary>
    internal static bool EnsureWindowReady(IntPtr hwnd, string actionLabel, string title, InputReadiness? readiness = null)
    {
        bool wasIconic = false;

        // Step 1: Iconic → focusless restore with zoom
        if (NativeMethods.IsIconic(hwnd))
        {
            wasIconic = true;
            Console.WriteLine($"[A11Y] 0x{hwnd.ToInt64():X} \"{title}\" minimized — restoring (focusless)");
            using var iconicZoom = ClickZoomHelper.BeginForIconic(hwnd, actionLabel, $"\"{title}\"");
            var prevFg = NativeMethods.GetForegroundWindow();
            NativeMethods.ShowWindow(hwnd, 9); // SW_RESTORE
            if (prevFg != IntPtr.Zero && prevFg != hwnd)
                NativeMethods.SetForegroundWindow(prevFg);
            Thread.Sleep(300);
            iconicZoom?.UpdateImage();
            iconicZoom?.ShowPass("restored");
            Thread.Sleep(600);
        }

        // Step 2: Blocker detection + dismiss (~5ms)
        if (readiness != null)
        {
            var blocker = readiness.DetectBlocker(hwnd);
            if (blocker != null)
            {
                Console.WriteLine($"[A11Y] blocker: {blocker.ClassName} \"{blocker.Title}\" — dismissing");
                readiness.BlockerHandler?.TryHandle(hwnd, blocker);
                Thread.Sleep(300);
                blocker = readiness.DetectBlocker(hwnd);
                if (blocker != null)
                    Console.WriteLine($"[A11Y] blocker persists: {blocker.ClassName} \"{blocker.Title}\"");
            }
        }

        return wasIconic;
    }

    // ── Bridge: TryHandleBlocker → adapter ──

    /// <summary>
    /// Bridge for BlockerHandlerAdapter: delegates to existing TryHandleBlocker.
    /// </summary>
    internal static (bool handled, bool shouldRetry) TryHandleBlockerViaReadiness(
        IntPtr mainHwnd, DialogHandlerManager? handlerMgr)
    {
        return TryHandleBlocker(mainHwnd, handlerMgr);
    }

    // ── Bridge: Knowhow broadcast → adapter ──

    /// <summary>
    /// Bridge for KnowhowBroadcasterAdapter: delegates to BroadcastInspectKnowhow.
    /// Resolves profile matching and form ID from window handle.
    /// </summary>
    internal static void BroadcastKnowhowViaReadiness(IntPtr mainHwnd, string? formId, string? actionName)
    {
        try
        {
            var className = WindowFinder.GetClassName(mainHwnd);
            var title = WindowFinder.GetWindowText(mainHwnd);
            BroadcastInspectKnowhow(mainHwnd, className, formId, title);

        }
        catch { }
    }

    /// <summary>
    /// Create/update action-method-result knowhow file.
    /// Filename: knowhow-{action}-{method}-{success|fail}.md
    /// Called by InputCommand (and potentially other commands) after each method attempt.
    /// </summary>
    internal static void WriteActionMethodKnowhow(
        IntPtr mainHwnd, string? formId, string action, string method,
        bool success, string? notes = null)
    {
        if (string.IsNullOrEmpty(formId)) return;
        try
        {
            var className = WindowFinder.GetClassName(mainHwnd);
            NativeMethods.GetWindowThreadProcessId(mainHwnd, out uint pid);
            var procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

            var profileStore = new ProfileStore();
            var profileMatch = profileStore.FindByMatch(className, procName);
            if (profileMatch == null) return;

            var a11yDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
            var formDir = Path.Combine(a11yDir, $"form_{formId}");
            Directory.CreateDirectory(formDir);

            var result = success ? "success" : "fail";
            var khPath = Path.Combine(formDir, $"knowhow-{action}-{method}-{result}.md");

            // Append if exists, create if not
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            var entry = $"- [{timestamp}] {(notes ?? "(auto-recorded)")}";

            if (File.Exists(khPath))
            {
                File.AppendAllText(khPath, entry + "\n");
            }
            else
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"## [{formId}] {action}/{method} → {result}");
                sb.AppendLine(entry);
                File.WriteAllText(khPath, sb.ToString());
            }
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Shorten MFC Afx class names for readable knowhow filenames.
    /// "Afx:009C0000:8:00010005:00000000:00000000" → "Afx"
    /// "AfxWnd140" → "AfxWnd"
    /// "Edit" → "Edit" (unchanged)
    /// "#32770" → "Dlg32770"
    /// </summary>
    internal static string ShortenClassName(string className)
    {
        if (string.IsNullOrEmpty(className)) return "unknown";

        // Afx:HEX:HEX:... → "Afx" (MFC auto-registered class)
        if (className.StartsWith("Afx:", StringComparison.OrdinalIgnoreCase) ||
            className.StartsWith("Afx_", StringComparison.OrdinalIgnoreCase))
            return "Afx";

        // AfxWnd140, AfxWnd110s → "AfxWnd"
        if (className.StartsWith("AfxWnd", StringComparison.OrdinalIgnoreCase))
            return "AfxWnd";

        // #32770 → "Dlg32770"
        if (className.StartsWith("#"))
            return "Dlg" + className.Substring(1);

        // Remove version numbers: "ToolbarWindow32" → "ToolbarWindow"
        // But keep short names as-is: "Edit", "Button", "Static"
        var sanitized = className.Replace(":", "_").Replace(" ", "");

        // Truncate to 20 chars max
        if (sanitized.Length > 20) sanitized = sanitized.Substring(0, 20);

        return sanitized;
    }
}
