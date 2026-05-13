using System.Buffers.Binary;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WKAppBot.Launcher;

internal sealed record MyCdpContext(
    DateTimeOffset TimestampUtc,
    string Command,
    string? Subcommand,
    string? Target,
    bool HasEvalJs,
    bool IsGraphStyleTarget,
    string WorkingDirectory,
    string ExePath,
    string? ForegroundWindow,
    string? ConsoleWindow,
    string? HostWindow,
    string Status,
    string[] Args,
    string? CallerDiagnostic = null);

partial class Program
{
    /// <summary>
    /// The most recently validated caller HWND from <see cref="BuildMyCdpContext"/>.
    /// CoreRunner reads this to forward the anchor to Core via WKAPPBOT_CALLER_HWND,
    /// so Chrome placement (ComputePlacementNearCaller) targets the right terminal
    /// even when the Eye pipe is unavailable. IntPtr.Zero when no validated caller
    /// is available (uninitialised or last validation was rejected).
    /// </summary>
    internal static IntPtr LastValidatedCallerHwnd { get; private set; } = IntPtr.Zero;

    /// <summary>
    /// Window rect of the validated caller. Used by post-launch WebBot placement
    /// to move Chrome to the caller's screen area. RECT.Empty when unavailable.
    /// </summary>
    internal static System.Drawing.Rectangle LastValidatedCallerRect { get; private set; } = System.Drawing.Rectangle.Empty;

    static bool TryTrackMyCdpAccess(string cmd, string[] forwardArgs, out string? error)
    {
        error = null;

        var hasEvalJs = forwardArgs.Any(a => a.Equals("--eval-js", StringComparison.OrdinalIgnoreCase));
        // "cdp" is the renamed surface of "web"; both spawn Chrome and need caller-HWND
        // validation so a foreign foreground window (YouTube, unrelated browser, etc.)
        // cannot become the placement anchor and bind Chrome to the wrong project.
        var isCdpFamily = cmd is "a11y" or "web" or "cdp" or "ask";
        if (!isCdpFamily && !hasEvalJs)
            return false;

        var ctx = BuildMyCdpContext(cmd, forwardArgs, hasEvalJs, out error);
        // Persist BOTH accepted and rejected contexts -- the rejection records
        // (status starts with "rejected") are the primary debug signal for
        // HWND/caller-validation regressions, so they MUST land in the same
        // jsonl as successes so operators have a single audit trail.
        TryAppendMyCdpState(ctx);
        if (error != null)
            return true;

        return true;
    }

    static MyCdpContext BuildMyCdpContext(string cmd, string[] forwardArgs, bool hasEvalJs, out string? error)
    {
        error = null;

        // Clear any stale anchor from a previous invocation -- only the
        // "ok_*_caller" success path below republishes a usable HWND/rect.
        LastValidatedCallerHwnd = IntPtr.Zero;
        LastValidatedCallerRect = System.Drawing.Rectangle.Empty;

        var subcommand = forwardArgs.Length > 1 ? forwardArgs[1] : null;
        var target = FindMyCdpTarget(cmd, forwardArgs, hasEvalJs);
        var isGraphStyle = !string.IsNullOrWhiteSpace(target)
            && (target.IndexOf('#') >= 0
                || target.IndexOf('*') >= 0
                || target.IndexOf(';') >= 0);

        if (hasEvalJs && string.IsNullOrWhiteSpace(target))
        {
            error = $"[LAUNCHER] {cmd} --eval-js requires a CDP target/grap field";
            return new MyCdpContext(
                DateTimeOffset.UtcNow,
                cmd,
                subcommand,
                target,
                hasEvalJs,
                isGraphStyle,
                Environment.CurrentDirectory,
                Environment.ProcessPath ?? "",
                null,
                null,
                null,
                "rejected",
                forwardArgs);
        }

        if (hasEvalJs && !HasCdpField(target))
        {
            error = $"[LAUNCHER] {cmd} --eval-js requires a grap with cdp:PORT";
            return new MyCdpContext(
                DateTimeOffset.UtcNow,
                cmd,
                subcommand,
                target,
                hasEvalJs,
                isGraphStyle,
                Environment.CurrentDirectory,
                Environment.ProcessPath ?? "",
                null,
                null,
                null,
                "rejected",
                forwardArgs);
        }

        if (hasEvalJs)
        {
            var expectedPort = GetExpectedCdpPort();
            var targetPort = GetCdpPort(target);
            if (!targetPort.HasValue)
            {
                error = $"[LAUNCHER] {cmd} --eval-js requires a grap with cdp:PORT";
                return new MyCdpContext(
                    DateTimeOffset.UtcNow,
                    cmd,
                    subcommand,
                    target,
                    hasEvalJs,
                    isGraphStyle,
                    Environment.CurrentDirectory,
                    Environment.ProcessPath ?? "",
                    null,
                    null,
                    null,
                    "rejected",
                    forwardArgs);
            }

            if (expectedPort.HasValue && targetPort.Value != expectedPort.Value)
            {
                error = $"[LAUNCHER] {cmd} --eval-js requires cdp:{expectedPort.Value} for this project (got cdp:{targetPort.Value})";
                return new MyCdpContext(
                    DateTimeOffset.UtcNow,
                    cmd,
                    subcommand,
                    target,
                    hasEvalJs,
                    isGraphStyle,
                    Environment.CurrentDirectory,
                    Environment.ProcessPath ?? "",
                    null,
                    null,
                    null,
                    "rejected",
                    forwardArgs);
            }
        }

        var fgHwnd = GetForegroundWindow();
        var consoleHwnd = GetConsoleWindow();
        var hostHwnd = GetHostWindowSnapshot();

        // Caller priority: (1) console window (2) host/parent window (3) foreground as fallback
        var callerHwnd = consoleHwnd != IntPtr.Zero ? consoleHwnd
                       : hostHwnd != IntPtr.Zero ? hostHwnd
                       : fgHwnd;

        var callerValidation = ValidateCallerHwnd(callerHwnd, consoleHwnd, hostHwnd);

        if (callerValidation.IsOffScreen)
        {
            var reason = callerValidation.Status switch
            {
                "no_caller_window"        => "no caller window available",
                "invalid_window_type"     => "caller is desktop or PseudoConsoleWindow",
                "caller_offscreen"        => "caller window is off-screen",
                "caller_foreign_process"  => "caller foreground belongs to an unrelated process (not a terminal/IDE/wkappbot)",
                _                         => "caller HWND is off-screen or invalid",
            };
            error = $"[LAUNCHER] {cmd}: {reason} (console: {GetWindowSnapshot(consoleHwnd)}, host: {GetWindowSnapshot(hostHwnd)}, fg: {GetWindowSnapshot(fgHwnd)})."
                  + (string.IsNullOrEmpty(callerValidation.Diagnostic) ? "" : $" caller {callerValidation.Diagnostic}.")
                  + $" Run {cmd} from a terminal/IDE owned by your project so Chrome can be placed correctly.";
            return new MyCdpContext(
                DateTimeOffset.UtcNow,
                cmd,
                subcommand,
                target,
                hasEvalJs,
                isGraphStyle,
                Environment.CurrentDirectory,
                Environment.ProcessPath ?? "",
                GetWindowSnapshot(fgHwnd),
                GetWindowSnapshot(consoleHwnd),
                GetWindowSnapshot(hostHwnd),
                "rejected_" + callerValidation.Status,
                forwardArgs,
                callerValidation.Diagnostic);
        }

        // Publish the validated caller HWND + rect so CoreRunner (via WKAPPBOT_CALLER_HWND env)
        // and any post-launch WebBot mover can anchor Chrome on the right terminal. Reset
        // these to their "no caller" values on rejected paths above so a stale anchor from
        // a previous invocation isn't reused.
        LastValidatedCallerHwnd = callerHwnd;
        LastValidatedCallerRect = TryGetWindowRectLTRB(callerHwnd, out var cRect)
            ? System.Drawing.Rectangle.FromLTRB(cRect.Left, cRect.Top, cRect.Right, cRect.Bottom)
            : System.Drawing.Rectangle.Empty;

        return new MyCdpContext(
            DateTimeOffset.UtcNow,
            cmd,
            subcommand,
            target,
            hasEvalJs,
            isGraphStyle,
            Environment.CurrentDirectory,
            Environment.ProcessPath ?? "",
            GetWindowSnapshot(fgHwnd),
            GetWindowSnapshot(consoleHwnd),
            GetWindowSnapshot(hostHwnd),
            callerValidation.Status,
            forwardArgs);
    }

    static string? FindMyCdpTarget(string cmd, string[] forwardArgs, bool hasEvalJs)
    {
        if (forwardArgs.Length == 0) return null;

        if (cmd.Equals("a11y", StringComparison.OrdinalIgnoreCase))
            return FindA11yTarget(forwardArgs, hasEvalJs);

        if (cmd.Equals("web", StringComparison.OrdinalIgnoreCase)
            || cmd.Equals("cdp", StringComparison.OrdinalIgnoreCase))
            return FindFirstPositionalArgBeforeEvalJs(forwardArgs, startIndex: 1);

        if (cmd.Equals("ask", StringComparison.OrdinalIgnoreCase))
            return FindFirstPositionalArgBeforeEvalJs(forwardArgs, startIndex: 1);

        return FindFirstPositionalArgBeforeEvalJs(forwardArgs, startIndex: 0);
    }

    static string? FindA11yTarget(string[] args, bool hasEvalJs)
    {
        var positional = CollectPositionalArgs(args, startIndex: 1, stopAtEvalJs: false);
        if (hasEvalJs && positional.Count < 2)
            return null;
        return positional.Count >= 2 ? positional[1] : positional.FirstOrDefault();
    }

    static string? FindFirstPositionalArgBeforeEvalJs(string[] args, int startIndex)
    {
        var positional = CollectPositionalArgs(args, startIndex, stopAtEvalJs: true);
        return positional.FirstOrDefault();
    }

    static List<string> CollectPositionalArgs(string[] args, int startIndex, bool stopAtEvalJs)
    {
        var positional = new List<string>();
        for (var i = startIndex; i < args.Length; i++)
        {
            var arg = args[i];
            if (string.IsNullOrWhiteSpace(arg)) continue;
            if (stopAtEvalJs && arg.Equals("--eval-js", StringComparison.OrdinalIgnoreCase))
                break;
            if (arg.StartsWith("-", StringComparison.Ordinal)) continue;
            positional.Add(arg);
        }
        return positional;
    }

    static bool HasCdpField(string? grap)
        => !string.IsNullOrWhiteSpace(grap)
           && (grap.Contains("cdp:", StringComparison.OrdinalIgnoreCase)
               || grap.Contains("cdp=", StringComparison.OrdinalIgnoreCase));

    static int? GetExpectedCdpPort()
    {
        try
        {
            var root = WKAppBot.CLI.ProjectRoot.Find();
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(root.ToLowerInvariant()));
            var n = BinaryPrimitives.ReadUInt32BigEndian(bytes);
            return 9300 + (int)((n % 174) * 4);
        }
        catch
        {
            return null;
        }
    }

    static int? GetCdpPort(string? grap)
    {
        if (string.IsNullOrWhiteSpace(grap)) return null;
        var idx = grap.IndexOf("cdp:", StringComparison.OrdinalIgnoreCase);
        var sep = 4;
        if (idx < 0)
        {
            idx = grap.IndexOf("cdp=", StringComparison.OrdinalIgnoreCase);
            sep = 4;
        }
        if (idx < 0) return null;

        var start = idx + sep;
        var end = start;
        while (end < grap.Length && char.IsDigit(grap[end])) end++;
        if (end == start) return null;
        return int.TryParse(grap[start..end], out var port) ? port : null;
    }

    record CallerValidation(bool IsOffScreen, string Status, string? Diagnostic = null);

    static string DescribeCallerRect(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero) return "(no hwnd)";
        if (!IsWindow(hwnd)) return "(not a window)";
        var cloaked = IsWindowCloaked(hwnd);
        var visible = IsWindowVisible(hwnd);
        if (!TryGetWindowRectLTRB(hwnd, out var r))
            return $"(rect unavailable; visible={visible} cloaked={cloaked})";
        return $"rect=(L={r.Left},T={r.Top},R={r.Right},B={r.Bottom}) size=({r.Width}x{r.Height}) visible={visible} cloaked={cloaked}";
    }

    static CallerValidation ValidateCallerHwnd(IntPtr callerHwnd, IntPtr consoleHwnd, IntPtr hostHwnd)
    {
        // Caller priority: (1) console window (this process's console, if available)
        //                 (2) host/parent process window (VSCode, IDE, etc. that spawned wkappbot)
        //                 (3) foreground window (fallback, may be unrelated)
        // Valid callers: must be on-screen, not desktop/PseudoConsoleWindow,
        // and (when falling back to foreground) MUST belong to a known shell/IDE
        // process — never a media player, browser, or other foreign app that
        // happens to hold focus when the user runs `cdp open`.

        if (callerHwnd == IntPtr.Zero)
            return new(true, "no_caller_window");

        if (IsDesktopWindow(callerHwnd) || IsPseudoConsoleWindow(callerHwnd))
            return new(true, "invalid_window_type");

        if (IsWindowOffScreen(callerHwnd))
            return new(true, "caller_offscreen", DescribeCallerRect(callerHwnd));

        // Determine caller type for logging
        var callerType = callerHwnd == consoleHwnd ? "console"
                       : callerHwnd == hostHwnd ? "host_process"
                       : "foreground";

        // FOREIGN-PROCESS GUARD: when the only available anchor is the foreground
        // window (no console, no parent process window), the user may have run
        // `cdp open` from a detached context with YouTube / Chrome / VLC / etc.
        // currently in front. Latching Chrome onto an unrelated app's window
        // contaminates the project's CDP placement and may cross project
        // boundaries. Reject unless the foreground belongs to a known host
        // (terminal, shell, IDE, or wkappbot itself).
        if (callerType == "foreground" && !IsKnownHostProcess(callerHwnd))
            return new(true, "caller_foreign_process");

        return new(false, $"ok_{callerType}_caller");
    }

    static readonly string[] KnownHostProcessNames = new[]
    {
        // Terminals / shells
        "windowsterminal", "conhost", "openconsole", "cmd", "powershell", "pwsh", "wt",
        "bash", "sh", "zsh", "fish", "mintty", "alacritty", "wezterm",
        // IDEs / editors that commonly host CLI runs
        "code", "code-insiders", "cursor", "windsurf",
        "devenv", "rider", "rider64", "idea", "idea64", "pycharm", "pycharm64",
        "webstorm", "webstorm64", "clion", "clion64", "goland", "goland64",
        "sublime_text", "notepad++", "atom",
        // wkappbot self
        "wkappbot", "wkappbot-core", "wkchat", "wka11y", "a11y",
        // Claude Code / Codex CLI host shells
        "claude", "codex",
    };

    static bool IsKnownHostProcess(IntPtr hwnd)
    {
        try
        {
            GetWindowThreadProcessIdLocal(hwnd, out int pid);
            if (pid <= 0) return false;
            using var p = System.Diagnostics.Process.GetProcessById(pid);
            var name = (p.ProcessName ?? "").ToLowerInvariant();
            if (string.IsNullOrEmpty(name)) return false;

            // Chrome is often foreground when user has ask gpt running.
            // Accept Chrome as a valid caller — it's almost always the
            // project's own CDP instance. Later validation (Stage 1/2/3)
            // will confirm placement accuracy regardless of caller origin.
            if (name == "chrome") return true;

            foreach (var allowed in KnownHostProcessNames)
                if (name == allowed) return true;
            return false;
        }
        catch
        {
            // If we cannot determine the process, fail closed — better to reject
            // a legitimate caller than to bind Chrome to a foreign window.
            return false;
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
    static extern int GetWindowThreadProcessIdLocal(IntPtr hWnd, out int lpdwProcessId);

    static bool IsDesktopWindow(IntPtr hwnd)
    {
        try
        {
            var cls = new System.Text.StringBuilder(256);
            GetClassNameW(hwnd, cls, 256);
            var clsText = cls.ToString();
            return clsText == "SHELLDLL_DefView" || clsText == "Progman" || clsText == "WorkerW";
        }
        catch
        {
            return false;
        }
    }

    static bool IsPseudoConsoleWindow(IntPtr hwnd)
    {
        try
        {
            var cls = new System.Text.StringBuilder(256);
            GetClassNameW(hwnd, cls, 256);
            return cls.ToString() == "PseudoConsoleWindow";
        }
        catch
        {
            return false;
        }
    }

    // Win32 RECT (left, top, right, bottom). MUST NOT be marshalled as
    // System.Drawing.Rectangle: Rectangle's fields are (X, Y, Width, Height), so
    // the same 4 LONGs from Win32 get reinterpreted as (left, top, width=right,
    // height=bottom), and accessing .Right/.Bottom adds them again -- yielding
    // garbage like Right = left+right_coord. This was the long-standing reason
    // CASCADIA_HOSTING_WINDOW_CLASS (Windows Terminal) on a left/secondary
    // monitor (negative coords like -2414..-1285) was falsely flagged as
    // off-screen and rejected the caller.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public int Width => Right - Left;
        public int Height => Bottom - Top;
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out int pvAttribute, int cbAttribute);

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool IsWindow(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    const int DWMWA_CLOAKED = 14;
    const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

    /// <summary>
    /// Returns the window's bounding rect as (Left, Top, Right, Bottom). Prefers
    /// DWM extended frame bounds (more accurate, excludes drop-shadow padding),
    /// falls back to GetWindowRect. Returns false only if both APIs fail.
    /// </summary>
    static bool TryGetWindowRectLTRB(IntPtr hwnd, out RECT rect)
    {
        rect = default;
        if (hwnd == IntPtr.Zero) return false;
        try
        {
            // DWM extended frame bounds is the "real" visible rect (excludes
            // the 8px invisible resize border on Win10+ Aero windows).
            if (DwmGetWindowAttribute(hwnd, DWMWA_EXTENDED_FRAME_BOUNDS, out RECT dwmRect, 16) == 0
                && (dwmRect.Right > dwmRect.Left) && (dwmRect.Bottom > dwmRect.Top))
            {
                rect = dwmRect;
                return true;
            }
        }
        catch { /* fall through to GetWindowRect */ }
        return GetWindowRect(hwnd, out rect);
    }

    /// <summary>
    /// True iff window is DWM-cloaked (UWP suspended app, Win+Tab thumbnail,
    /// virtual-desktop hidden). Cloaked windows are not visible to the user even
    /// though GetWindowRect returns valid coords.
    /// </summary>
    static bool IsWindowCloaked(IntPtr hwnd)
    {
        try
        {
            if (DwmGetWindowAttribute(hwnd, DWMWA_CLOAKED, out int cloaked, 4) == 0)
                return cloaked != 0;
        }
        catch { }
        return false;
    }

    // AOT-friendly: collect monitor rects into a static field via a non-capturing
    // delegate. We cannot use closures here (NativeAOT cannot synthesize the
    // marshal stub for a capturing lambda). The collection is guarded by a
    // monitor lock so concurrent validations don't clobber the shared list.
    static readonly object s_monitorRectsLock = new();
    static List<RECT>? s_monitorRectsScratch;
    static readonly MonitorEnumProc s_collectMonitorRects = CollectMonitorRect;

    static bool CollectMonitorRect(IntPtr hMon, IntPtr hdc, ref RECT mr, IntPtr _)
    {
        try { s_monitorRectsScratch?.Add(mr); } catch { }
        return true;
    }

    /// <summary>
    /// True iff the given rect does not intersect ANY monitor's bounds. Walks
    /// the full multi-monitor virtual screen via EnumDisplayMonitors so windows
    /// living entirely on a left/secondary monitor with negative coords are
    /// correctly classified as on-screen.
    /// </summary>
    static bool IsRectOutsideAllMonitors(RECT r)
    {
        List<RECT> rects;
        lock (s_monitorRectsLock)
        {
            s_monitorRectsScratch = new List<RECT>(4);
            try
            {
                if (!EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, s_collectMonitorRects, IntPtr.Zero))
                    return false; // fail open
                rects = s_monitorRectsScratch;
            }
            catch
            {
                return false; // fail open
            }
            finally
            {
                s_monitorRectsScratch = null;
            }
        }

        foreach (var mr in rects)
        {
            if (r.Left   < mr.Right
             && r.Right  > mr.Left
             && r.Top    < mr.Bottom
             && r.Bottom > mr.Top)
            {
                return false; // intersects this monitor → on-screen
            }
        }
        return true; // no monitor intersected → off-screen
    }

    static bool IsWindowOffScreen(IntPtr hwnd)
    {
        try
        {
            if (!IsWindow(hwnd)) { LogValidation(hwnd, default, false, false, false, "not_a_window"); return true; }
            bool visible = IsWindowVisible(hwnd);
            bool cloaked = IsWindowCloaked(hwnd);
            bool gotRect = TryGetWindowRectLTRB(hwnd, out var rect);

            if (!gotRect)               { LogValidation(hwnd, default, true, visible, cloaked, "get_rect_failed"); return true; }
            if (cloaked)                { LogValidation(hwnd, rect,    true, visible, cloaked, "cloaked");        return true; }
            if (!visible)               { LogValidation(hwnd, rect,    true, visible, cloaked, "not_visible");    return true; }
            if (rect.Right <= rect.Left || rect.Bottom <= rect.Top)
                                        { LogValidation(hwnd, rect,    true, visible, cloaked, "degenerate_rect"); return true; }
            if (IsRectOutsideAllMonitors(rect))
                                        { LogValidation(hwnd, rect,    true, visible, cloaked, "outside_all_monitors"); return true; }

            LogValidation(hwnd, rect, true, visible, cloaked, "on_screen");
            return false;
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[VALIDATION] hwnd=0x{hwnd.ToInt64():X} exception={ex.GetType().Name}: {ex.Message}"); } catch { }
            return true;
        }
    }

    static void LogValidation(IntPtr hwnd, RECT r, bool isWindow, bool isVisible, bool cloaked, string verdict)
    {
        try
        {
            var cls = new System.Text.StringBuilder(64);
            GetClassNameW(hwnd, cls, cls.Capacity);
            Console.Error.WriteLine(
                $"[VALIDATION] hwnd=0x{hwnd.ToInt64():X} class={cls} "
                + $"rect=(L={r.Left},T={r.Top},R={r.Right},B={r.Bottom}) "
                + $"size=({r.Width}x{r.Height}) "
                + $"IsWindow={isWindow} IsVisible={isVisible} cloaked={cloaked} verdict={verdict}");
        }
        catch { }
    }

    static string? GetWindowSnapshot(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero) return null;
        try
        {
            var cls = new System.Text.StringBuilder(256);
            var title = new System.Text.StringBuilder(256);
            GetClassNameW(hwnd, cls, 256);
            GetWindowTextW(hwnd, title, 256);
            var clsText = cls.ToString();
            var titleText = title.ToString();
            if (titleText.Length > 80) titleText = titleText[..77] + "...";
            if (string.IsNullOrWhiteSpace(clsText) && string.IsNullOrWhiteSpace(titleText))
                return $"0x{hwnd.ToInt64():X}";
            return $"0x{hwnd.ToInt64():X} {clsText} {titleText}".Trim();
        }
        catch
        {
            return $"0x{hwnd.ToInt64():X}";
        }
    }

    static IntPtr GetHostWindowSnapshot()
    {
        try
        {
            var parentPid = GetParentProcessId(Environment.ProcessId);
            if (parentPid <= 0) return IntPtr.Zero;
            using var parent = System.Diagnostics.Process.GetProcessById(parentPid);
            return parent.MainWindowHandle;
        }
        catch
        {
            return IntPtr.Zero;
        }
    }

    static void TryAppendMyCdpState(MyCdpContext ctx)
    {
        try
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? ".";
            var runtimeDir = Path.Combine(exeDir, "wkappbot.hq", "runtime");
            Directory.CreateDirectory(runtimeDir);
            var jsonlPath = Path.Combine(runtimeDir, "cdp-state.jsonl");

            var line = WriteMyCdpJsonLine(ctx);
            WKAppBot.Shared.ToolOutputStore.AppBotAppendFile(jsonlPath, line);
        }
        catch
        {
            // best-effort telemetry only
        }
    }

    /// <summary>
    /// Post-launch Chrome placement: locate the newly launched project Chrome
    /// (windows of class "Chrome_WidgetWin_1" owned by chrome.exe) and SetWindowPos
    /// it next to the validated caller window. Uses SWP_NOZORDER | SWP_NOACTIVATE
    /// so focus is never stolen from the terminal that invoked this command.
    ///
    /// Best-effort: returns silently on any failure (no caller anchor, no Chrome
    /// window found, off-screen rect, etc.). Logged to wkappbot.hq/runtime/cdp-state.jsonl
    /// via the existing telemetry path.
    /// </summary>
    internal static void TryMoveWebBotNearCaller(string cmd)
    {
        try
        {
            var caller = LastValidatedCallerHwnd;
            var callerRect = LastValidatedCallerRect;
            Console.Error.WriteLine($"[PLACEMENT:STEP1] cmd={cmd} caller=0x{caller.ToInt64():X} rect=({callerRect.Left},{callerRect.Top},{callerRect.Right},{callerRect.Bottom})");
            if (caller == IntPtr.Zero || callerRect == System.Drawing.Rectangle.Empty)
            {
                Console.Error.WriteLine($"[PLACEMENT:STEP1] no validated caller anchor -- skip move (cmd={cmd})");
                return;
            }

            // Coordinate space: by the time this runs the launcher has called
            // TrySetPerMonitorV2DpiAwareness() in Main(), so callerRect is in
            // physical pixels — the same coordinate space Chrome (PerMonitorV2)
            // sees in SetWindowPos. No further LogicalToPhysical conversion
            // needed; mixing the two systems was the root cause of the
            // "Chrome ends up huge in the wrong place" bug.
            int callerLeft = callerRect.Left;
            int callerTop = callerRect.Top;

            // Default WebBot size: fixed 800x600 (matches Core's ChromeLauncher
            // default and what users expect from `cdp open` / `web open`).
            // Chrome_WidgetWin_1 renderer window consistently reports 18×9 pixels
            // smaller than SetWindowPos dimensions, likely due to Chrome's internal
            // client area calculation. Compensate by requesting slightly larger dimensions.
            const int DefaultChromeW = 800;
            const int DefaultChromeH = 600;
            const int CompensationW  = 18;  // pixels to add for Chrome internal sizing
            const int CompensationH  = 9;   // pixels to add for Chrome internal sizing
            int targetW = DefaultChromeW + CompensationW;
            int targetH = DefaultChromeH + CompensationH;

            // Compute target position: offset slightly down-right of the caller's
            // upper-left so the terminal stays visible behind/beside Chrome. We
            // INTENTIONALLY use a POSITIVE offset (+30, +30) — the previous
            // negative offset (-30, -30) could push Chrome into the top-left of
            // the caller's monitor or even cross monitor boundaries on multi-mon
            // setups, especially when combined with the legacy DPI bug.
            int targetX = callerLeft + 30;
            int targetY = callerTop + 30;

            // Clamp target rect to the caller's monitor work area so Chrome
            // never spills onto another display. Uses MonitorFromPoint at the
            // caller's centre + GetMonitorInfo to read MONITORINFO.rcWork
            // (excludes taskbar). Same DPI context as the caller because the
            // launcher is now PerMonitorV2-aware.
            int callerCenterX = callerLeft + Math.Max(1, (callerRect.Right - callerRect.Left)) / 2;
            int callerCenterY = callerTop + Math.Max(1, (callerRect.Bottom - callerRect.Top)) / 2;
            if (TryGetWorkArea(callerCenterX, callerCenterY, out var workArea))
            {
                // Make sure Chrome fits inside the work area.
                if (targetW > workArea.Right - workArea.Left) targetW = workArea.Right - workArea.Left;
                if (targetH > workArea.Bottom - workArea.Top) targetH = workArea.Bottom - workArea.Top;
                // Clamp X/Y so the full Chrome rect lives on this monitor.
                if (targetX < workArea.Left)                   targetX = workArea.Left;
                if (targetY < workArea.Top)                    targetY = workArea.Top;
                if (targetX + targetW > workArea.Right)        targetX = workArea.Right - targetW;
                if (targetY + targetH > workArea.Bottom)       targetY = workArea.Bottom - targetH;
            }

            // Find chrome.exe Browser window. Chrome_BrowserWindow is the main frame;
            // Chrome_WidgetWin_1 is a renderer tab window (different process).
            // Prioritize Chrome_BrowserWindow. If not found, fall back to Chrome_WidgetWin_1.
            var browserWindowCandidates = new System.Collections.Generic.List<(IntPtr hwnd, int pid, DateTime startedAt)>();
            var rendererCandidates = new System.Collections.Generic.List<(IntPtr hwnd, int pid, DateTime startedAt)>();

            EnumWindowsLocal((hwnd, _) =>
            {
                if (!IsWindowVisibleLocal(hwnd)) return true;
                var cls = new System.Text.StringBuilder(64);
                GetClassNameW(hwnd, cls, cls.Capacity);
                var clsStr = cls.ToString();

                GetWindowThreadProcessIdLocal(hwnd, out int wpid);
                if (wpid <= 0) return true;
                try
                {
                    using var p = System.Diagnostics.Process.GetProcessById(wpid);
                    if (!string.Equals(p.ProcessName, "chrome", StringComparison.OrdinalIgnoreCase))
                        return true;

                    if (clsStr == "Chrome_BrowserWindow")
                        browserWindowCandidates.Add((hwnd, wpid, p.StartTime.ToUniversalTime()));
                    else if (clsStr == "Chrome_WidgetWin_1")
                        rendererCandidates.Add((hwnd, wpid, p.StartTime.ToUniversalTime()));
                }
                catch { }
                return true;
            }, IntPtr.Zero);

            // Prefer browser window; fall back to renderer only if no browser window found
            var candidates = browserWindowCandidates.Count > 0 ? browserWindowCandidates : rendererCandidates;

            Console.Error.WriteLine($"[PLACEMENT:STEP2] found {candidates.Count} Chrome candidate(s)");
            if (candidates.Count == 0)
            {
                Console.Error.WriteLine($"[PLACEMENT:STEP2] no Chrome_WidgetWin_1 windows found -- skip move (cmd={cmd})");
                return;
            }

            // Pick the most recently started chrome.exe top-level window -- that's
            // the Chrome instance Core just launched (or attached to / reused).
            // Note: on tab-reuse, the start time will be the original Chrome's
            // start time, but among multiple chrome.exe processes the recycled
            // one is typically also the most-recently-used (Chrome reuses the
            // newest in its session-restore queue).
            candidates.Sort((a, b) => b.startedAt.CompareTo(a.startedAt));
            var target = candidates[0].hwnd;
            Console.Error.WriteLine($"[PLACEMENT:STEP3] target=0x{target.ToInt64():X} pid={candidates[0].pid} startedAt={candidates[0].startedAt:HH:mm:ss}");

            // SWP_NOZORDER (0x0004) | SWP_NOACTIVATE (0x0010) -- move without
            // disturbing focus or Z-order.
            const uint SWP_NOZORDER   = 0x0004;
            const uint SWP_NOACTIVATE = 0x0010;

            // Before SetWindowPos, restore Chrome to normal state (SW_RESTORE = 9)
            // so it releases any session-restore size lock and accepts our SetWindowPos.
            ShowWindow(target, 9); // SW_RESTORE
            System.Threading.Thread.Sleep(50);

            SetWindowPos(target, IntPtr.Zero, targetX, targetY, targetW, targetH, SWP_NOZORDER | SWP_NOACTIVATE);
            Console.Error.WriteLine($"[LAUNCHER] post-launch placed Chrome 0x{target.ToInt64():X} at ({targetX},{targetY},{targetW},{targetH}) near caller 0x{caller.ToInt64():X} (cmd={cmd})");

            // Triple-check + auto-correct: Chrome may ignore SetWindowPos when its
            // own session-restore positioner fires after our move, or when DWM is
            // mid-animation. Validate the final landing rect and re-issue
            // SetWindowPos until we're within the allowed delta.
            //
            // NOTE: We request (targetW, targetH) which includes compensation for
            // Chrome's internal sizing. But validation should check against the
            // DESIRED final size (DefaultChromeW, DefaultChromeH) not the compensation target.
            var expected = new RECT
            {
                Left   = targetX,
                Top    = targetY,
                Right  = targetX + DefaultChromeW,  // Compare against desired 800x600
                Bottom = targetY + DefaultChromeH,  // not the compensated 818x609
            };
            var (placementOk, finalRect, attempts) = TryValidateAndCorrectPlacement(target, expected, maxAttempts: 5);
            if (!placementOk)
            {
                Console.Error.WriteLine($"[LAUNCHER:WARN] Chrome placement failed validation after {attempts} attempts. "
                    + $"expected=(L={expected.Left},T={expected.Top},R={expected.Right},B={expected.Bottom}) "
                    + $"final=(L={finalRect.Left},T={finalRect.Top},R={finalRect.Right},B={finalRect.Bottom}) cmd={cmd}");
            }

            // Stage 1 done. Fork a detached child to handle Stage 2 (wait
            // for Page.loadEventFired and re-validate placement) and Stage 3
            // (DPI-aware match against Chrome's final monitor). The child
            // runs ~10s in the background while the user's prompt returns
            // immediately. See MyCdpContext.Stage23.cs for the implementation.
            //
            // Only fire if Stage 1 itself didn't bail with an invalid hwnd --
            // the helpers there assume a live Chrome window to act on.
            if (IsWindow(target))
            {
                SpawnBackgroundPlacementWatcher(target, expected, cmd);
            }
        }
        catch
        {
            // best-effort -- never crash the launcher exit path
        }
    }

    /// <summary>
    /// Verifies Chrome actually landed at <paramref name="targetRect"/>; if not,
    /// re-issues SetWindowPos and re-checks, up to <paramref name="maxAttempts"/>
    /// times. Returns (success, finalRect, attemptCount).
    ///
    /// Delta thresholds: horizontal/vertical &lt; 20px, width/height &lt; 30px.
    /// Bails early if the window becomes invalid/invisible/cloaked during
    /// correction, or if GetWindowRect takes &gt;200ms. Recomputes the target if
    /// the underlying monitor work area changes mid-loop (multi-monitor reflow).
    ///
    /// Each attempt is appended to cdp-state.jsonl as a placement_validation record
    /// so operators can audit Chrome landing accuracy after the fact.
    /// </summary>
    internal static (bool success, RECT finalRect, int attemptCount) TryValidateAndCorrectPlacement(
        IntPtr chromeHwnd, RECT targetRect, int maxAttempts = 5)
    {
        Console.Error.WriteLine($"[PLACEMENT:VALIDATE-ENTRY] chrome=0x{chromeHwnd.ToInt64():X} target=(L={targetRect.Left},T={targetRect.Top},R={targetRect.Right},B={targetRect.Bottom}) maxAttempts={maxAttempts}");
        const int DeltaPosThreshold  = 15; // x/y px — allow up to ±15px for frame/border variance
        const int DeltaSizeThreshold = 10; // w/h px — strict enough to verify nominal 800x600
        const int StabilizationMs    = 150; // longer settle time for DWM composition
        const int GetRectTimeoutMs   = 200;
        const uint SWP_NOZORDER   = 0x0004;
        const uint SWP_NOACTIVATE = 0x0010;

        RECT finalRect = default;
        int attempt = 0;

        // Snapshot the monitor work area at entry so we can detect mid-loop
        // multi-monitor reflows (taskbar repositioning, resolution change, etc.)
        // and recompute the target if the work area shifts under us.
        RECT? initialWorkArea = null;
        try
        {
            int centerX = targetRect.Left + Math.Max(1, targetRect.Width) / 2;
            int centerY = targetRect.Top + Math.Max(1, targetRect.Height) / 2;
            if (TryGetWorkArea(centerX, centerY, out var wa)) initialWorkArea = wa;
        }
        catch { /* ignore -- fall back to no reflow detection */ }

        for (attempt = 1; attempt <= maxAttempts; attempt++)
        {
            // Step 1: wait for the window to settle. SetWindowPos returns before
            // DWM has finished compositing the new bounds, and Chrome's own
            // browser_process_init may still be adjusting the rect on first launch.
            System.Threading.Thread.Sleep(StabilizationMs);

            // Step 2: bail if the window has gone away or become unfit for
            // correction. Continuing to SetWindowPos a cloaked/destroyed window
            // would silently fail and just burn attempts.
            if (!IsWindow(chromeHwnd))
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} chrome hwnd no longer valid -- abort");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "hwnd_invalid");
                return (false, finalRect, attempt);
            }
            if (!IsWindowVisible(chromeHwnd) || IsWindowCloaked(chromeHwnd))
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} chrome invisible/cloaked -- abort");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "invisible_or_cloaked");
                return (false, finalRect, attempt);
            }

            // Step 3: read actual rect with a soft timeout. GetWindowRect is a
            // synchronous send-message to the window's thread, so a hung Chrome
            // could in principle stall us. The 200ms cap keeps us responsive.
            bool gotRect = false;
            RECT actual = default;
            var rectTask = System.Threading.Tasks.Task.Run(() =>
            {
                gotRect = TryGetWindowRectLTRB(chromeHwnd, out actual);
            });
            if (!rectTask.Wait(GetRectTimeoutMs))
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} GetWindowRect timeout (>{GetRectTimeoutMs}ms) -- skip iteration");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "get_rect_timeout");
                continue;
            }
            if (!gotRect)
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} GetWindowRect failed -- skip iteration");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "get_rect_failed");
                continue;
            }

            finalRect = actual;

            // Step 4: compute deltas. Use absolute values so a window that
            // overshot vs. undershot both count.
            int dX = Math.Abs(actual.Left   - targetRect.Left);
            int dY = Math.Abs(actual.Top    - targetRect.Top);
            int dW = Math.Abs(actual.Width  - targetRect.Width);
            int dH = Math.Abs(actual.Height - targetRect.Height);
            bool withinThreshold = dX < DeltaPosThreshold
                                 && dY < DeltaPosThreshold
                                 && dW < DeltaSizeThreshold
                                 && dH < DeltaSizeThreshold;

            Console.Error.WriteLine(
                $"[PLACEMENT:VALIDATE] attempt={attempt}/{maxAttempts} "
                + $"expected=(L={targetRect.Left},T={targetRect.Top},R={targetRect.Right},B={targetRect.Bottom},W={targetRect.Width},H={targetRect.Height}) "
                + $"actual=(L={actual.Left},T={actual.Top},R={actual.Right},B={actual.Bottom},W={actual.Width},H={actual.Height}) "
                + $"delta=(dX={dX},dY={dY},dW={dW},dH={dH}) within={withinThreshold}");

            AppendPlacementValidation(attempt, targetRect, actual, success: withinThreshold, note: null);

            if (withinThreshold)
                return (true, finalRect, attempt);

            // Step 5: detect mid-loop monitor reflow. If the work area has
            // shifted (e.g. user dragged the window to another monitor between
            // attempts, or the taskbar moved), recompute the target so we
            // converge on the new work area instead of fighting it.
            if (initialWorkArea.HasValue)
            {
                int centerX = targetRect.Left + Math.Max(1, targetRect.Width) / 2;
                int centerY = targetRect.Top + Math.Max(1, targetRect.Height) / 2;
                if (TryGetWorkArea(centerX, centerY, out var currentWa))
                {
                    var prev = initialWorkArea.Value;
                    if (currentWa.Left != prev.Left || currentWa.Top != prev.Top
                        || currentWa.Right != prev.Right || currentWa.Bottom != prev.Bottom)
                    {
                        Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} monitor work area changed -- recomputing target");
                        // Clamp to the new work area so we don't push Chrome
                        // off-screen on the corrected attempt.
                        int newL = targetRect.Left;
                        int newT = targetRect.Top;
                        int newW = targetRect.Width;
                        int newH = targetRect.Height;
                        if (newW > currentWa.Right - currentWa.Left) newW = currentWa.Right - currentWa.Left;
                        if (newH > currentWa.Bottom - currentWa.Top) newH = currentWa.Bottom - currentWa.Top;
                        if (newL < currentWa.Left)              newL = currentWa.Left;
                        if (newT < currentWa.Top)               newT = currentWa.Top;
                        if (newL + newW > currentWa.Right)      newL = currentWa.Right - newW;
                        if (newT + newH > currentWa.Bottom)     newT = currentWa.Bottom - newH;
                        targetRect = new RECT { Left = newL, Top = newT, Right = newL + newW, Bottom = newT + newH };
                        initialWorkArea = currentWa;
                    }
                }
            }

            // Step 6: re-issue SetWindowPos. Don't bother on the final attempt --
            // there's no follow-up validation that would catch a late correction.
            if (attempt < maxAttempts)
            {
                SetWindowPos(chromeHwnd, IntPtr.Zero,
                    targetRect.Left, targetRect.Top, targetRect.Width, targetRect.Height,
                    SWP_NOZORDER | SWP_NOACTIVATE);
            }
        }

        return (false, finalRect, attempt - 1);
    }

    /// <summary>
    /// Appends a placement_validation record to cdp-state.jsonl. Mirrors the
    /// shape used elsewhere in this file so suggest triage / cdp-mon can pick
    /// it up alongside the existing caller-validation telemetry.
    /// </summary>
    static void AppendPlacementValidation(int attempt, RECT expected, RECT actual, bool success, string? note)
    {
        try
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? ".";
            var runtimeDir = Path.Combine(exeDir, "wkappbot.hq", "runtime");
            Directory.CreateDirectory(runtimeDir);
            var jsonlPath = Path.Combine(runtimeDir, "cdp-state.jsonl");

            using var ms = new MemoryStream();
            using (var writer = new Utf8JsonWriter(ms))
            {
                writer.WriteStartObject();
                writer.WriteString("ts", DateTimeOffset.UtcNow);
                writer.WriteString("kind", "placement_validation");
                writer.WriteNumber("attempt", attempt);
                writer.WriteBoolean("success", success);
                if (!string.IsNullOrEmpty(note)) writer.WriteString("note", note);

                writer.WritePropertyName("expected");
                writer.WriteStartArray();
                writer.WriteNumberValue(expected.Left);
                writer.WriteNumberValue(expected.Top);
                writer.WriteNumberValue(expected.Right);
                writer.WriteNumberValue(expected.Bottom);
                writer.WriteNumberValue(expected.Width);
                writer.WriteNumberValue(expected.Height);
                writer.WriteEndArray();

                writer.WritePropertyName("actual");
                writer.WriteStartArray();
                writer.WriteNumberValue(actual.Left);
                writer.WriteNumberValue(actual.Top);
                writer.WriteNumberValue(actual.Right);
                writer.WriteNumberValue(actual.Bottom);
                writer.WriteNumberValue(actual.Width);
                writer.WriteNumberValue(actual.Height);
                writer.WriteEndArray();

                writer.WritePropertyName("delta");
                writer.WriteStartArray();
                writer.WriteNumberValue(actual.Left   - expected.Left);
                writer.WriteNumberValue(actual.Top    - expected.Top);
                writer.WriteNumberValue(actual.Width  - expected.Width);
                writer.WriteNumberValue(actual.Height - expected.Height);
                writer.WriteEndArray();

                writer.WriteEndObject();
            }
            var line = Encoding.UTF8.GetString(ms.ToArray());
            WKAppBot.Shared.ToolOutputStore.AppBotAppendFile(jsonlPath, line);
        }
        catch
        {
            // best-effort telemetry only -- never throw from the placement loop
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
    static extern bool IsWindowVisibleLocal(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "EnumWindows")]
    static extern bool EnumWindowsLocal(EnumWindowsProcLocal lpEnumFunc, IntPtr lParam);

    delegate bool EnumWindowsProcLocal(IntPtr hWnd, IntPtr lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr GetParent(IntPtr hWnd);

    // Monitor enumeration for "place Chrome on caller's monitor" clamping.
    // The launcher is PerMonitorV2-aware, so the coordinates we read here match
    // the physical-pixel space Chrome uses in SetWindowPos.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct POINT
    {
        public int X;
        public int Y;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool GetMonitorInfoW(IntPtr hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Returns the work area (monitor minus taskbar) of the monitor that
    /// contains the given physical-pixel point. Returns false on failure
    /// (caller should fall back to no clamping in that case).
    /// </summary>
    static bool TryGetWorkArea(int physX, int physY, out RECT workArea)
    {
        workArea = default;
        try
        {
            var pt = new POINT { X = physX, Y = physY };
            var hMon = MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST);
            if (hMon == IntPtr.Zero) return false;
            var mi = new MONITORINFO();
            mi.cbSize = System.Runtime.InteropServices.Marshal.SizeOf<MONITORINFO>();
            if (!GetMonitorInfoW(hMon, ref mi)) return false;
            workArea = mi.rcWork;
            return workArea.Right > workArea.Left && workArea.Bottom > workArea.Top;
        }
        catch
        {
            return false;
        }
    }


    static string WriteMyCdpJsonLine(MyCdpContext ctx)
    {
        using var ms = new MemoryStream();
        using (var writer = new Utf8JsonWriter(ms))
        {
            writer.WriteStartObject();
            writer.WriteString("ts", ctx.TimestampUtc);
            writer.WriteString("command", ctx.Command);
            if (!string.IsNullOrWhiteSpace(ctx.Subcommand)) writer.WriteString("subcommand", ctx.Subcommand);
            if (!string.IsNullOrWhiteSpace(ctx.Target)) writer.WriteString("target", ctx.Target);
            writer.WriteBoolean("eval_js", ctx.HasEvalJs);
            writer.WriteBoolean("graph_style_target", ctx.IsGraphStyleTarget);
            writer.WriteString("cwd", ctx.WorkingDirectory);
            writer.WriteString("exe", ctx.ExePath);
            if (!string.IsNullOrWhiteSpace(ctx.ForegroundWindow)) writer.WriteString("foreground_window", ctx.ForegroundWindow);
            if (!string.IsNullOrWhiteSpace(ctx.ConsoleWindow)) writer.WriteString("console_window", ctx.ConsoleWindow);
            if (!string.IsNullOrWhiteSpace(ctx.HostWindow)) writer.WriteString("host_window", ctx.HostWindow);
            if (!string.IsNullOrWhiteSpace(ctx.CallerDiagnostic)) writer.WriteString("caller_diagnostic", ctx.CallerDiagnostic);
            writer.WriteString("status", ctx.Status);
            writer.WritePropertyName("args");
            writer.WriteStartArray();
            foreach (var arg in ctx.Args)
                writer.WriteStringValue(arg);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
