namespace WKAppBot.Launcher;

partial class Program
{
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
            return new(true, "caller_offscreen_reject", DescribeCallerRect(callerHwnd));

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

    /// <summary>
    /// Resolve a valid on-screen caller window. If the given caller is off-screen,
    /// try alternatives: foreground window, host window, or largest visible window.
    /// Returns IntPtr.Zero if no valid on-screen window is found.
    /// </summary>
    static IntPtr ResolveValidCallerWindow(IntPtr preferredCaller)
    {
        if (preferredCaller != IntPtr.Zero && IsWindowVisibleLocal(preferredCaller))
        {
            if (GetWindowRect(preferredCaller, out RECT rect))
            {
                var centerPt = new POINT
                {
                    X = rect.Left + (rect.Right - rect.Left) / 2,
                    Y = rect.Top + (rect.Bottom - rect.Top) / 2
                };
                IntPtr monitor = MonitorFromPoint(centerPt, MONITOR_DEFAULTTONULL);
                if (monitor != IntPtr.Zero)
                {
                    Console.Error.WriteLine($"[CALLER:RESOLVE] preferred 0x{preferredCaller.ToInt64():X} valid");
                    return preferredCaller;
                }
            }
        }

        Console.Error.WriteLine($"[CALLER:RESOLVE] preferred off-screen/invalid, trying alternatives...");

        // Try foreground window
        IntPtr foreground = GetForegroundWindow();
        if (foreground != IntPtr.Zero && foreground != preferredCaller && IsWindowVisibleLocal(foreground))
        {
            if (GetWindowRect(foreground, out RECT rect))
            {
                var centerPt = new POINT { X = rect.Left + rect.Width / 2, Y = rect.Top + rect.Height / 2 };
                if (MonitorFromPoint(centerPt, MONITOR_DEFAULTTONULL) != IntPtr.Zero)
                {
                    Console.Error.WriteLine($"[CALLER:RESOLVE] using foreground 0x{foreground.ToInt64():X}");
                    return foreground;
                }
            }
        }

        // Try host window (parent process)
        IntPtr host = GetHostWindowSnapshot();
        if (host != IntPtr.Zero && host != preferredCaller && host != foreground && IsWindowVisibleLocal(host))
        {
            if (GetWindowRect(host, out RECT rect))
            {
                var centerPt = new POINT { X = rect.Left + rect.Width / 2, Y = rect.Top + rect.Height / 2 };
                if (MonitorFromPoint(centerPt, MONITOR_DEFAULTTONULL) != IntPtr.Zero)
                {
                    Console.Error.WriteLine($"[CALLER:RESOLVE] using host 0x{host.ToInt64():X}");
                    return host;
                }
            }
        }

        // Fallback: find largest on-screen window
        var largestWindow = IntPtr.Zero;
        int largestArea = 0;
        EnumWindowsLocal((hwnd, _) =>
        {
            if (!IsWindowVisibleLocal(hwnd)) return true;
            if (!GetWindowRect(hwnd, out RECT rect)) return true;
            var centerPt = new POINT { X = rect.Left + rect.Width / 2, Y = rect.Top + rect.Height / 2 };
            if (MonitorFromPoint(centerPt, MONITOR_DEFAULTTONULL) == IntPtr.Zero) return true;
            int area = rect.Width * rect.Height;
            if (area > largestArea) { largestArea = area; largestWindow = hwnd; }
            return true;
        }, IntPtr.Zero);

        if (largestWindow != IntPtr.Zero)
        {
            Console.Error.WriteLine($"[CALLER:RESOLVE] using largest window 0x{largestWindow.ToInt64():X}");
            return largestWindow;
        }

        Console.Error.WriteLine($"[CALLER:RESOLVE] FAIL no valid on-screen window found");
        return IntPtr.Zero;
    }
}
