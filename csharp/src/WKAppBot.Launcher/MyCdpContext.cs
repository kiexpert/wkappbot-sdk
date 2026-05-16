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
        // Placement is only triggered for commands that actually open/use a CDP connection
        // for AI prompt delivery: `ask` (triad/gpt/gemini/claude) and `cdp` (explicit cdp open).
        // Commands like `a11y`, `web`, `windows`, `file`, `gc`, `skill`, `suggest`, `eye`,
        // `slack` MUST NOT trigger caller validation or placement -- they don't move Chrome,
        // and running GetForegroundWindow / ancestor walk on every a11y call is unnecessary overhead.
        var isCdpFamily = cmd is "cdp" or "ask";
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

        // Prefer WKAPPBOT_CALLER_HWND if set by an outer launcher invocation (nested call).
        // This avoids GetForegroundWindow() snapshotting the wrong window when the user
        // runs a nested cdp/ask command from inside a wkappbot session.
        var envCallerHwnd = IntPtr.Zero;
        {
            var envVal = Environment.GetEnvironmentVariable("WKAPPBOT_CALLER_HWND");
            if (!string.IsNullOrEmpty(envVal))
            {
                var raw = envVal.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                    ? envVal[2..] : envVal;
                if (long.TryParse(raw, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal))
                    envCallerHwnd = new IntPtr(hwndVal);
            }
        }

        var fgHwnd = envCallerHwnd != IntPtr.Zero ? envCallerHwnd : GetForegroundWindow();
        var consoleHwnd = GetConsoleWindow();
        var hostHwnd = GetHostWindowSnapshot();

        // Caller priority: (1) env WKAPPBOT_CALLER_HWND (nested call anchor)
        //                  (2) console window (3) host/parent window (4) foreground as fallback
        var callerHwnd = envCallerHwnd != IntPtr.Zero ? envCallerHwnd
                       : consoleHwnd != IntPtr.Zero ? consoleHwnd
                       : hostHwnd != IntPtr.Zero ? hostHwnd
                       : fgHwnd;

        // Auto-resolve off-screen caller to valid alternative
        callerHwnd = ResolveValidCallerWindow(callerHwnd);

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
