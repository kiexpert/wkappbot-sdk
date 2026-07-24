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

        // DEFENSIVE BAIL-OUT (concurrent-ai-isolation-env-var-detection-beats-parent-chain
        // TODO, suggest ts=2026-07-24T10:14:25): reject explorer.exe's window (Shell_TrayWnd,
        // the single machine-wide taskbar) unconditionally, before the console/host_process/
        // foreground classification below. This closes a gap that EyeCmdPipeClient's own
        // ancestor-walk skip does NOT cover: GetHostWindowSnapshot() does a raw immediate-
        // parent MainWindowHandle lookup with no process filter at all, so a ConPTY-tab-
        // creation PPID misattribution can feed explorer.exe in here directly. Worse, when
        // that same misattributed PID is ALSO what the ancestor walk lands on, callerHwnd
        // ends up EQUAL to hostHwnd purely by coincidence, which mislabels it "host_process"
        // below and skips the IsKnownHostProcess foreign-process allow-list check entirely --
        // checking here, unconditionally, closes that hole regardless of classification.
        if (IsExplorerShellWindow(callerHwnd))
            return new(true, "caller_is_explorer_shell");

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
    /// DEFENSIVE BAIL-OUT helper (concurrent-ai-isolation-env-var-detection-beats-parent-chain
    /// TODO): true when the window's owning process is explorer.exe. Twin of
    /// EyeCmdPipeClient.IsExplorerProcess(uint pid) -- kept as a separate hwnd-keyed helper here
    /// because this file's callers only ever have the hwnd, not the ancestor PID list, and this
    /// backstop specifically must fire even when the hwnd arrived via GetHostWindowSnapshot()
    /// (immediate-parent MainWindowHandle, no ancestor-walk / no process filter at all) rather
    /// than via EyeCmdPipeClient.ResolveCallerTerminalHwnd()'s own (now-filtered) ancestor walk.
    /// </summary>
    static bool IsExplorerShellWindow(IntPtr hwnd)
    {
        try
        {
            GetWindowThreadProcessIdLocal(hwnd, out int pid);
            if (pid <= 0) return false;
            using var p = System.Diagnostics.Process.GetProcessById(pid);
            return string.Equals(p.ProcessName, "explorer", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Resolve a valid on-screen caller window. If the given caller is off-screen,
    /// try deterministic caller-derived alternatives only.
    /// Returns IntPtr.Zero if no valid on-screen window is found.
    /// </summary>
    static IntPtr ResolveValidCallerWindow(IntPtr preferredCaller)
    {
        if (TryAcceptResolvedCaller(preferredCaller, "preferred"))
            return preferredCaller;

        Console.Error.WriteLine($"[CALLER:RESOLVE] preferred off-screen/invalid, trying alternatives...");

        // Try ancestor walk -- nearest parent process owning a visible on-screen window.
        // NEVER use GetForegroundWindow() -- it returns whoever holds focus (YouTube, any app).
        IntPtr ancestor = EyeCmdPipeClient.ResolveCallerTerminalHwnd();
        if (ancestor != preferredCaller && TryAcceptResolvedCaller(ancestor, "ancestor"))
            return ancestor;

        // Try host window (parent process)
        IntPtr host = GetHostWindowSnapshot();
        if (host != preferredCaller && host != ancestor && TryAcceptResolvedCaller(host, "host"))
            return host;

        Console.Error.WriteLine($"[CALLER:RESOLVE] FAIL no valid on-screen window found");
        return IntPtr.Zero;
    }

    static bool TryAcceptResolvedCaller(IntPtr hwnd, string label)
    {
        if (hwnd == IntPtr.Zero)
        {
            Console.Error.WriteLine($"[CALLER:RESOLVE] {label} missing");
            return false;
        }

        // Use the same multi-monitor-aware validation as the final caller gate.
        // The old center-point-only MonitorFromPoint check rejected legitimate
        // windows whose rect intersected a monitor but whose center landed in a
        // monitor gap or stale DWM coordinate edge case.
        if (IsWindowOffScreen(hwnd))
        {
            Console.Error.WriteLine($"[CALLER:RESOLVE] {label} rejected: {DescribeCallerRect(hwnd)}");
            return false;
        }

        Console.Error.WriteLine($"[CALLER:RESOLVE] {label} 0x{hwnd.ToInt64():X} valid: {DescribeCallerRect(hwnd)}");
        return true;
    }
}
