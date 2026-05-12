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
    string[] Args);

partial class Program
{
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
        if (error != null)
            return true;

        TryAppendMyCdpState(ctx);
        return true;
    }

    static MyCdpContext BuildMyCdpContext(string cmd, string[] forwardArgs, bool hasEvalJs, out string? error)
    {
        error = null;

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
            error = $"[LAUNCHER] {cmd}: {reason} (console: {GetWindowSnapshot(consoleHwnd)}, host: {GetWindowSnapshot(hostHwnd)}, fg: {GetWindowSnapshot(fgHwnd)}). "
                  + $"Run {cmd} from a terminal/IDE owned by your project so Chrome can be placed correctly.";
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
                forwardArgs);
        }

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

    record CallerValidation(bool IsOffScreen, string Status);

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
            return new(true, "caller_offscreen");

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

    static bool IsWindowOffScreen(IntPtr hwnd)
    {
        try
        {
            if (!GetWindowRect(hwnd, out var rect))
                return true;

            // Window is off-screen if both left and top are past reasonable negative bounds (-500px)
            // or if rect is invalid (right < left or bottom < top)
            const int OffScreenThreshold = -500;
            if (rect.Right < rect.Left || rect.Bottom < rect.Top)
                return true;

            if (rect.Right < OffScreenThreshold && rect.Bottom < OffScreenThreshold)
                return true;

            return false;
        }
        catch
        {
            return true;
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hWnd, out System.Drawing.Rectangle lpRect);

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
            WKAppBot.Shared.ToolOutputStore.AppendRotatingLine(jsonlPath, line);
        }
        catch
        {
            // best-effort telemetry only
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
