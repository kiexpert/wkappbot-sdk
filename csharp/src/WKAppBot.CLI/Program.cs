using System.Text;
using System.Text.Json;
using WKAppBot.Core.Scenario;
using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

/// <summary>
/// CLI entry point and command dispatcher.
/// Commands are implemented in partial class files under Commands/ and Helpers/.
///
/// File layout:
///   Program.cs                          — Main, DataDir, run, validate (this file)
///   Commands/UsageCommand.cs            — PrintUsage, Error, GetArgValue
///   Commands/InspectionCommands.cs      — inspect, focus, watch, capture + watch helpers
///   Commands/ChartCommands.cs           — chart-analyze, tooltip-probe
///   Commands/HtsStressCommand.cs        — hts-stress
///   Commands/ScanCommands.cs            — scan, form-dump
///   Commands/AutomationCommands.cs      — click, do, SmartClickButton, CheckDialogGone
///   Commands/DialogCommands.cs          — dismiss, dialog-click, toolbar-ocr, TryHandleBlocker
///   Helpers/ControlHelpers.cs           — FindAllChildrenFlat, FindMfcCombos, DumpFormTree,
///                                         BuildClassPath, notice/dialog content helpers,
///                                         InteractiveButtonLearn, GenerateLearnedHandler
/// </summary>
internal partial class Program
{
    /// <summary>
    /// Runtime data directory: {exe_dir}/wkappbot.hq/
    /// All logs, profiles, output go here — keeps SDK/bin clean.
    /// HQ = HeadQuarters (본부: 경험치 축적 + 작전 기록 + 전과 보고)
    /// </summary>
    static readonly string DataDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq");

    static int Main(string[] args)
    {
        // Force UTF-8 output (Windows 949 codepage breaks Korean in non-Korean terminals)
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Enable DPI awareness
        try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }

        // Auto-log: tee all console output to file
        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var exeName = Path.GetFileName(exePath);
        var logDir = Path.Combine(DataDir, "logs");
        Directory.CreateDirectory(logDir);
        RotateOldLogs(logDir, staleHours: 24);
        var pid = Environment.ProcessId;
        // Include command name in log filename for easy identification via ls
        // e.g. "wkappbot.exe.out-20260221_211427.eye.pid=36944.txt"
        var cmdTag = args.Length > 0 ? args[0].ToLowerInvariant().Replace(" ", "-") : "noargs";
        // For multi-word commands like "slack send", include subcommand too
        if (args.Length > 1 && cmdTag is "slack" or "web" or "schedule" or "knowhow")
            cmdTag += $"-{args[1].ToLowerInvariant()}";
        var logFile = Path.Combine(logDir, $"{exeName}.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={pid}.txt");
        var tee = new TeeTextWriter(Console.Out, logFile);
        Console.SetOut(tee);

        int exitCode = 1;
        try
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            var command = args[0].ToLowerInvariant();
            var restArgs = args.Skip(1).ToArray();

            // Global Eye tick (for eye --global multi-parent monitor)
            try { EmitEyeTick(command, cmdTag, "start"); } catch { }
            try { EmitEyeTick(command, cmdTag, "step:1/3:명령 준비"); } catch { }
            try
            {
                var promptPreview = BuildPromptPreview(command, restArgs);
                if (!string.IsNullOrWhiteSpace(promptPreview))
                    EmitEyeTick(command, cmdTag, $"prompt:{promptPreview}");
            }
            catch { }

            // Auto-launch AppBotEye for all commands except eye/help/validate/win-move/logcat
            // 앱봇이 뭔가 하면 눈은 항상 떠있어야!
            // slack은 listen/stop만 제외 (send/reply/upload/screenshot 등은 눈 띄워야!)
            var noEyeCommands = new HashSet<string> {
                "eye", "help", "--help", "-h", "validate", "win-move", "logcat"
            };
            var isSlackListenOrStop = command == "slack" && restArgs.Length > 0 &&
                (restArgs[0] == "listen" || restArgs[0] == "stop" || restArgs[0] == "status");
            if (!noEyeCommands.Contains(command) && !isSlackListenOrStop)
            {
                try { LaunchAppBotEyeIfNeeded(); } catch { /* best-effort */ }
            }

            try { EmitEyeTick(command, cmdTag, "step:2/3:명령 실행"); } catch { }
            try { Console.WriteLine($"[ACT] cmd={command} args='{string.Join(" ", restArgs)}'"); } catch { }

            exitCode = command switch
            {
                "run" => RunCommand(restArgs),
                "validate" => ValidateCommand(restArgs),
                "inspect" => InspectCommand(restArgs),
                "focus" => FocusCommand(restArgs),
                "watch" => WatchCommand(restArgs),
                "capture" => CaptureCommand(restArgs),
                "hts-stress" => HtsStressCommand(restArgs),
                "scan" => ScanCommand(restArgs),
                "click" => ClickCommand(restArgs),
                "form-dump" => FormDumpCommand(restArgs),
                "do" => DoCommand(restArgs),
                "dismiss" => DismissCommand(restArgs),
                "toolbar-ocr" => ToolbarOcrCommand(restArgs),
                "chart-analyze" => ChartAnalyzeCommand(restArgs),
                "tooltip-probe" => TooltipProbeCommand(restArgs),
                "ocr" => OcrCommand(restArgs),
                "web" => WebCommand(restArgs),
                "slack" => SlackCommand(restArgs),
                "knowhow" => KnowhowCommand(restArgs),
                "stock-scan" => StockScanCommand(restArgs),
                "eye" => AppBotEyeCommand(restArgs),
                "ask" => AskCommand(restArgs),
                "input" => InputCommand(restArgs),
                "dialog-click" => DialogClickCommand(restArgs),
                "schedule" => ScheduleCommand(restArgs),
                "snapshot" => SnapshotCommand(restArgs),
                "screen" => ScreenCommand(restArgs),
                "win-move" => WindowMoveCommand(restArgs),
                "logcat" => LogcatCommand(restArgs),
                "kiwoom" => KiwoomCommand(restArgs),
                "com" => ComCommand(restArgs),
                "telegram" => TelegramCommand(restArgs),
                "zoom-demo" => ZoomDemoCommand(restArgs),
                "--help" or "-h" or "help" => PrintUsage(),
                _ when command.StartsWith("kro-trial-", StringComparison.OrdinalIgnoreCase) => KroTrialSpecialCommand(command, restArgs),
                _ => Error($"Unknown command: {command}")
            };

            try
            {
                if (exitCode == 0) Console.WriteLine($"[ACT] result=ok cmd={command}");
                else Console.WriteLine($"[FALLBACK] result=fail code={exitCode} cmd={command}");
            }
            catch { }

            // Auto snapshot+blend on A11Y action failure (experience DB accumulation)
            try
            {
                if (exitCode != 0)
                {
                    var a11yFailCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                    {
                        "input", "click", "do", "inspect", "dialog-click"
                    };
                    if (a11yFailCommands.Contains(command) && restArgs.Length > 0)
                    {
                        var winTitle = restArgs[0];
                        if (!string.IsNullOrWhiteSpace(winTitle))
                        {
                            Console.WriteLine($"[FALLBACK] auto snapshot+blend trigger (cmd={command})");
                            Console.WriteLine($"[A11Y] unavailable (cmd={command}, reason=action-failed, text=(none), role=(none), action=(none))");
                            var cidArg = "";
                            for (int i = 0; i < restArgs.Length - 1; i++)
                            {
                                if (string.Equals(restArgs[i], "--cid", StringComparison.OrdinalIgnoreCase))
                                {
                                    cidArg = restArgs[i + 1];
                                    break;
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(cidArg))
                                SnapshotCommand(new[] { winTitle, "--tag", $"a11y_fail_{command}", "--depth", "2", "--cid", cidArg });
                            else
                                SnapshotCommand(new[] { winTitle, "--tag", $"a11y_fail_{command}", "--depth", "2" });

                            var stdAction = ResolveStandardAction(command, restArgs);
                            WriteA11yFailExperienceRecord(winTitle, command, stdAction, cidArg, "action-failed", "(none)", "(none)", "(none)");
                        }
                    }
                }
            }
            catch { }

            return exitCode;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
        finally
        {
            try
            {
                var cmd = args.Length > 0 ? args[0].ToLowerInvariant() : "noargs";
                if (exitCode == 0) EmitEyeTick(cmd, cmdTag, "done:작업 완료");
                else EmitEyeTick(cmd, cmdTag, "step:3/3:오류 처리");
                EmitEyeTick(cmd, cmdTag, $"end:{exitCode}");
            }
            catch { }
            Console.SetOut(tee.OriginalConsole);
            tee.Dispose(); // normal-exit atexit-style move to logs/old
            Console.WriteLine($"Log saved: {tee.LogPath}");
        }
    }

    static void RotateOldLogs(string logDir, int staleHours = 24)
    {
        try
        {
            var oldDir = Path.Combine(logDir, "old");
            var now = DateTime.UtcNow;

            var files = Directory
                .GetFiles(logDir, "*.out-*.txt", SearchOption.TopDirectoryOnly)
                .Select(p => new FileInfo(p))
                .OrderBy(f => f.CreationTimeUtc)
                .ToList();

            foreach (var f in files)
            {
                // Only sweep logs older than threshold.
                if ((now - f.CreationTimeUtc).TotalHours < staleHours)
                    continue;

                // PID-based safety: move only when process is no longer alive.
                if (!TryGetPidFromLogName(f.Name, out var pid))
                    continue;
                if (IsProcessAlive(pid))
                    continue;

                try
                {
                    Directory.CreateDirectory(oldDir);
                    var dest = Path.Combine(oldDir, f.Name);
                    if (File.Exists(dest))
                    {
                        var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        dest = Path.Combine(oldDir, $"{Path.GetFileNameWithoutExtension(f.Name)}.{stamp}{f.Extension}");
                    }

                    File.Move(f.FullName, dest);
                }
                catch
                {
                    // File may be in use or already moved by another process.
                }
            }
        }
        catch
        {
            // Best-effort log housekeeping only.
        }
    }

    static bool TryGetPidFromLogName(string fileName, out int pid)
    {
        pid = 0;
        const string key = ".pid=";
        var idx = fileName.LastIndexOf(key, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return false;
        idx += key.Length;

        var end = idx;
        while (end < fileName.Length && char.IsDigit(fileName[end])) end++;
        if (end <= idx) return false;

        return int.TryParse(fileName[idx..end], out pid);
    }

    static bool IsProcessAlive(int pid)
    {
        try
        {
            var p = System.Diagnostics.Process.GetProcessById(pid);
            return !p.HasExited;
        }
        catch
        {
            return false;
        }
    }

    static string BuildPromptPreview(string command, string[] restArgs)
    {
        if (restArgs == null || restArgs.Length == 0)
            return string.Empty;

        // Only surface raw prompt-like commands to reduce noise.
        var c = (command ?? "").ToLowerInvariant();
        if (c is not ("ask" or "web" or "kiwoom" or "com" or "telegram" or "schedule" or "knowhow"))
            return string.Empty;

        var raw = string.Join(" ", restArgs).Trim();
        if (string.IsNullOrWhiteSpace(raw))
            return string.Empty;

        var first = raw.Split(new[] { '\r', '\n', '.', '!', '?', '。' }, StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault()?.Trim() ?? raw;

        if (first.Length > 80)
            first = first[..80] + "...";

        return first;
    }

    sealed class EyeTick
    {
        public string Ts { get; set; } = DateTime.UtcNow.ToString("O");
        public int Pid { get; set; }
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public int HostPid { get; set; }
        public string HostName { get; set; } = "";
        public string HostTitle { get; set; } = "";
        public string Command { get; set; } = "";
        public string Tag { get; set; } = "";
        public string Status { get; set; } = "";
    }

    static string EyeTicksPath => Path.Combine(DataDir, "runtime", "eye_ticks.jsonl");

    static void EmitEyeTick(string command, string tag, string status)
    {
        try
        {
            var runtimeDir = Path.Combine(DataDir, "runtime");
            Directory.CreateDirectory(runtimeDir);

            var pid = Environment.ProcessId;
            var ppid = GetParentPid(pid);
            var parentName = "unknown";
            if (ppid > 0)
            {
                try { parentName = System.Diagnostics.Process.GetProcessById(ppid).ProcessName; } catch { }
            }

            var (hostPid, hostName, hostTitle) = FindLogicalHost(pid, ppid);

            var tick = new EyeTick
            {
                Pid = pid,
                ParentPid = ppid,
                ParentName = parentName,
                HostPid = hostPid,
                HostName = hostName,
                HostTitle = hostTitle,
                Command = command,
                Tag = tag,
                Status = status,
            };

            File.AppendAllText(EyeTicksPath, JsonSerializer.Serialize(tick) + Environment.NewLine);
        }
        catch
        {
            // best effort
        }
    }

    static (int hostPid, string hostName, string hostTitle) FindLogicalHost(int selfPid, int directParentPid)
    {
        static bool IsShell(string n)
        {
            var x = (n ?? "").ToLowerInvariant();
            return x is "powershell" or "pwsh" or "cmd" or "conhost" or "node" or "wkappbot";
        }

        int cur = directParentPid;
        int depth = 0;
        while (cur > 0 && depth < 12)
        {
            depth++;
            try
            {
                var p = System.Diagnostics.Process.GetProcessById(cur);
                var name = p.ProcessName ?? "unknown";
                var title = p.MainWindowTitle ?? "";

                if (!IsShell(name) && !string.IsNullOrWhiteSpace(title))
                    return (cur, name, title);

                var next = GetParentPid(cur);
                if (next <= 0 || next == cur)
                    break;
                cur = next;
            }
            catch { break; }
        }

        if (directParentPid > 0)
        {
            try
            {
                var p = System.Diagnostics.Process.GetProcessById(directParentPid);
                return (directParentPid, p.ProcessName ?? "unknown", p.MainWindowTitle ?? "");
            }
            catch { }
        }

        return (selfPid, "wkappbot", "");
    }

    // ── run ────────────────────────────────────────────────────

    static string ResolveStandardAction(string command, string[] restArgs)
    {
        return (command ?? "").ToLowerInvariant() switch
        {
            "input" => "SetValue",
            "click" => "Invoke",
            "do" => "Invoke",
            "dialog-click" => "Invoke",
            "inspect" => "Inspect",
            "snapshot" => "Snapshot",
            "scan" => "Scan",
            _ => "General"
        };
    }

    static void WriteA11yFailExperienceRecord(string windowTitle, string command, string stdAction, string cidArg, string reason, string text, string role, string action)
    {
        try
        {
            var windows = WKAppBot.Win32.Window.WindowFinder.FindByTitle(windowTitle);
            if (windows.Count == 0) return;
            var win = windows[0];

            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid);
            var procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

            var expDir = Path.Combine(DataDir, "experience", SanitizePathTokenForExp(procName), SanitizePathTokenForExp(win.ClassName));
            Directory.CreateDirectory(expDir);
            var actionToken = SanitizePathTokenForExp(string.IsNullOrWhiteSpace(stdAction) ? "General" : stdAction);
            var logPath = Path.Combine(expDir, $"a11y_fail_{actionToken}.jsonl");
            var knowhowPath = Path.Combine(expDir, "knowhow.md");

            int? cid = int.TryParse(cidArg, out var c) ? c : null;
            var rec = new
            {
                ts = DateTime.Now.ToString("O"),
                cmd = command,
                window = win.Title,
                @class = win.ClassName,
                pid,
                cid,
                reason,
                text,
                role,
                action
            };

            File.AppendAllText(logPath, JsonSerializer.Serialize(rec) + Environment.NewLine, Encoding.UTF8);
            Console.WriteLine($"[A11Y] 경험 기록 저장: {logPath}");
            Console.WriteLine($"[KNOWHOW] hint: {knowhowPath}");
        }
        catch { }
    }

    static string SanitizePathTokenForExp(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "unknown";
        var invalid = Path.GetInvalidFileNameChars();
        var chars = s.Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray();
        return new string(chars);
    }

    static int RunCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot run <scenario.yaml> [-v|--verbose] [--no-watch] [--report <dir>]");

        string scenarioPath = args[0];
        bool verbose = args.Contains("-v") || args.Contains("--verbose");
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 200;
        string? reportDir = GetArgValue(args, "--report");

        // Parse scenario
        var doc = ScenarioParser.Load(scenarioPath);
        Console.WriteLine($"Loaded: {doc.Scenario.Name} ({doc.Steps.Count} steps)");

        // Run with passive background watcher (default: on)
        var runner = new ScenarioRunner(verbose, watch: !noWatch, watchIntervalMs: watchMs);
        var result = runner.Run(doc);

        // TODO: Generate HTML report if reportDir specified

        return result.IsSuccess ? 0 : 1;
    }

    // ── validate ───────────────────────────────────────────────

    static int ValidateCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot validate <scenario.yaml>");

        string path = args[0];
        if (ScenarioParser.TryValidate(path, out string? error))
        {
            var doc = ScenarioParser.Load(path);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"VALID: {doc.Scenario.Name}");
            Console.ResetColor();
            Console.WriteLine($"  Steps: {doc.Steps.Count}");
            Console.WriteLine($"  App: {doc.App.Launch}");
            if (doc.Teardown != null)
                Console.WriteLine($"  Teardown: {doc.Teardown.Count} steps");
            return 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"INVALID: {error}");
            Console.ResetColor();
            return 1;
        }
    }
}
