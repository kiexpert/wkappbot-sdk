using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Text.Json;
using WKAppBot.Core.Scenario;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    internal static volatile bool RunningInEye = false;
    internal static volatile bool IsMcpMode = false;
    [ThreadStatic] internal static bool McpElevationRequired;
    internal static bool GrepModeActive = false;
    internal static bool GrapMode = false;
    internal static bool IsPipeMode = false;
    internal static bool QuietFindOutput = Environment.GetEnvironmentVariable("WKAPPBOT_QUIET_FIND") == "1";
    private static readonly HashSet<string> _autoBugDedup = new();

    internal static void AutoBugReport(string context)
    {
        if (!_autoBugDedup.Add(context)) return;
        var stack = new System.Diagnostics.StackTrace(1, true);
        var callerFrame = stack.GetFrame(0);
        var callerInfo = $"{callerFrame?.GetMethod()?.DeclaringType?.Name}.{callerFrame?.GetMethod()?.Name}:{callerFrame?.GetFileLineNumber()}";
        var lastLog = _currentLogPath != null && File.Exists(_currentLogPath)
            ? string.Join("\n", File.ReadLines(_currentLogPath).TakeLast(5)) : "(no log)";
        var bugText = $"[BUG-AUTO] {context}\\ncaller: {callerInfo}\\ncommand: {string.Join(" ", EyeCmdPipeServer.CallerArgs.Value ?? [])}\\nlog tail:\\n{lastLog}";
        Console.Error.WriteLine($"[BUG-AUTO] {context} at {callerInfo}");
        EyeCmdPipeServer.DispatchBg(["suggest", bugText]);
    }
    internal static bool ReadOnlyMode = false;
    internal static bool IsElevated()
    {
        try
        {
            using var id = System.Security.Principal.WindowsIdentity.GetCurrent();
            return new System.Security.Principal.WindowsPrincipal(id)
                .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        catch { return false; }
    }
    internal static string? RelayFilePath = null;
    static string? _exitFilePath = null;
    static string? _exitEventName = null;
    internal static TextWriter OriginalStdout = Console.Out;
    internal static readonly string DataDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq");
    internal static string GetCurrentSessionHash()
    {
        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir)) return "none";
            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl").OrderByDescending(p => File.GetLastWriteTimeUtc(p)).FirstOrDefault();
            if (string.IsNullOrEmpty(latestFile) || !File.Exists(latestFile)) return "none";
            using var sha = SHA256.Create();
            var data = Encoding.UTF8.GetBytes(Path.GetFullPath(latestFile));
            var hash = sha.ComputeHash(data);
            return Convert.ToHexString(hash).ToLowerInvariant()[..16];
        }
        catch { return "none"; }
    }
    internal static int Main(string[] args)
    {
        int _exitCode = 1;
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            try { XRayHelper.RestoreAll(); } catch { }
            try { Console.Out.Flush(); } catch { }
            try
            {
                var raw = Console.OpenStandardError();
                raw.Write(new byte[] { 0, (byte)'U', (byte)'I', (byte)'T' });
                raw.Flush();
            }
            catch { }
            TerminateProcess(GetCurrentProcess(), (uint)_exitCode);
        };
        var _mainStarted = System.Diagnostics.Stopwatch.StartNew();
        bool _profiling = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
        Action<string> prof = _profiling ? (label => { try { Console.Error.WriteLine($"[PROFILE] {_mainStarted.ElapsedMilliseconds}ms {label}"); } catch { } }) : (_ => { });
        prof("Main() entered");
        {
            if (args.Length >= 2 && args[0].ToLowerInvariant() == "grap" && args[1].ToLowerInvariant() is "save" or "list" or "show" or "remove" or "rm" or "delete" or "del" or "verify")
            {
                try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
                Console.OutputEncoding = new System.Text.UTF8Encoding(false);
                return GrapAliasCommand(args[1..]);
            }
            if (args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep" && (args.Length == 1 || args.Any(a => a is "--help" or "-h")))
            {
                try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
                PrintGrapHelp(args[0].ToLowerInvariant());
                Console.Out.Flush();
                FastExit(0);
                return 0;
            }
            var _noArgsExeBase = Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs().FirstOrDefault() ?? "").ToLowerInvariant();
            if (args.Length == 0 && DetectCommandFromExeName(_noArgsExeBase) == null)
            {
                PrintUsage();
                FastExit(1);
                return 1;
            }
        }
        try { Console.OutputEncoding = new System.Text.UTF8Encoding(false); } catch { }
        try { Console.InputEncoding = Encoding.UTF8; } catch { }
        try { WKAppBot.Win32.Native.NativeMethods.SetConsoleCP(65001); } catch { }
        try { WKAppBot.Win32.Native.NativeMethods.SetConsoleOutputCP(65001); } catch { }
        if (Console.OutputEncoding.CodePage != 65001)
        {
            try
            {
                var raw = Console.OpenStandardOutput();
                var utf8Out = new System.IO.StreamWriter(raw, new System.Text.UTF8Encoding(false)) { AutoFlush = true };
                Console.SetOut(utf8Out);
            }
            catch { }
        }
        if (Environment.GetEnvironmentVariable("WKAPPBOT_WORKER") == "1") RunningInEye = true;
        {
            var dbgCmd = args.FirstOrDefault(a => !a.StartsWith('-'));
            var dbgSub = dbgCmd != null ? args.SkipWhile(a => a != dbgCmd).Skip(1).FirstOrDefault(a => !a.StartsWith('-')) : null;
            Console.SetError(new DebugStringWriter(Console.Error, dbgCmd, dbgSub));
            if (!QuietFindOutput) Console.Error.WriteLine($"[CMD] {string.Join(" ", args)}");
        }
        {
            var argsFileIdx = Array.FindIndex(args, a => a == "--args-file");
            if (argsFileIdx >= 0 && argsFileIdx + 1 < args.Length)
            {
                var argsFilePath = args[argsFileIdx + 1];
                if (File.Exists(argsFilePath))
                {
                    var fileArgs = File.ReadAllLines(argsFilePath, Encoding.UTF8).Where(l => l.Length > 0).ToArray();
                    args = args[..argsFileIdx].Concat(fileArgs).Concat(args[(argsFileIdx + 2)..]).ToArray();
                }
            }
        }
        {
            var efIdx = Array.FindIndex(args, a => a == "--exit-file");
            if (efIdx >= 0 && efIdx + 1 < args.Length)
            {
                _exitFilePath = args[efIdx + 1];
                args = args[..efIdx].Concat(args[(efIdx + 2)..]).ToArray();
            }
        }
        {
            var eeIdx = Array.FindIndex(args, a => a == "--exit-event");
            if (eeIdx >= 0 && eeIdx + 1 < args.Length)
            {
                _exitEventName = args[eeIdx + 1];
                args = args[..eeIdx].Concat(args[(eeIdx + 2)..]).ToArray();
            }
        }
        if (args.Length > 0 && args[0] == "mcp")
        {
            try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
            return McpCommand(args.Skip(1).ToArray());
        }
        try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
        try { InputZoomHost.CloseAllGhosts(); } catch { }
        bool _isGrapFastPath = args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep" && args.Length > 1;
        if (!_isGrapFastPath)
        {
            ThreadPool.QueueUserWorkItem(_ => { try { EnsureBusyboxAliases(); } catch { } });
            prof("EnsureBusyboxAliases queued (background)");
        }
        bool isEyeCmd = args.Length > 0 && args[0].Equals("eye", StringComparison.OrdinalIgnoreCase);
        if (!RunningInEye && !isEyeCmd)
        {
            int parentPid = GetParentPid(Environment.ProcessId);
            if (parentPid > 0)
            {
                var monitor = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            using var ph = System.Diagnostics.Process.GetProcessById(parentPid);
                            if (ph.HasExited) { Environment.Exit(0); return; }
                        }
                        catch (ArgumentException) { Environment.Exit(0); return; }
                        catch { }
                    }
                }) { IsBackground = true, Name = "OrphanGuard" };
                monitor.Start();
            }
        }
        prof("OrphanGuard started");
        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var exeName = Path.GetFileName(exePath);
        var logDir = Path.Combine(DataDir, "logs");
        var pid = Environment.ProcessId;
        bool _fastExitAfterCommand = false;
        {
            var argv0 = Environment.GetCommandLineArgs().FirstOrDefault() ?? exePath;
            var exeBaseName = Path.GetFileNameWithoutExtension(argv0).ToLowerInvariant();
            var implicitCmd = DetectCommandFromExeName(exeBaseName);
            if (implicitCmd != null && (args.Length == 0 || args[0].ToLowerInvariant() != implicitCmd))
                args = new[] { implicitCmd }.Concat(args).ToArray();
        }
        if (args.Length > 0 && args[0].ToLowerInvariant() is "grep" or "grap")
        {
            var alias = args[0].ToLowerInvariant();
            if (args.Length == 1 || args.Any(a => a is "--help" or "-h"))
            {
                prof("PrintGrapHelp");
                PrintGrapHelp(alias);
                prof("return 0");
                Console.Out.Flush();
                FastExit(0);
                return 0;
            }
            args = new[] { "logcat" }.Concat(GrapArgsToLogcat(args.Skip(1).ToArray())).ToArray();
            _fastExitAfterCommand = true;
            GrapMode = true;
        }
        if (!_fastExitAfterCommand)
        {
            Directory.CreateDirectory(logDir);
            ThreadPool.QueueUserWorkItem(_ => { try { RotateOldLogs(logDir, staleHours: 24); } catch { } });
            prof("RotateOldLogs queued (background)");
        }
        var (cmdTag, oldSubDir) = ComputeCmdTagAndSubDir(args);
        var logFile = Path.Combine(logDir, $"{exeName}.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={pid}.log");
        if (!RunningInEye) _currentLogPath = logFile;
        IsPipeMode = Console.IsOutputRedirected;
        if (args.Contains("--sudo"))
        {
            PulseStep.Init("core-sudo");
            args = args.Where(a => a != "--sudo").ToArray();
            PulseStep.Line($"flag stripped: remaining args={args.Length}");
            if (!IsElevated())
            {
                bool isEyeRelaunch = args.Length > 0 && args[0].Equals("eye", StringComparison.OrdinalIgnoreCase);
                if (isEyeRelaunch)
                {
                    // eye --sudo: just ensure admin Eye is running (no command execution)
                    PulseStep.Line("path=eye --sudo force-launch");
                    if (SudoHandler.EnsureAdminForSudo("eye --sudo force-launch"))
                    {
                        PulseStep.Finish("admin Eye ready (reused or spawned)");
                        Console.WriteLine("[SUDO] Admin Eye ready");
                        return 0;
                    }
                    PulseStep.Finish("EnsureAdminForSudo FAILED (UAC cancelled or spawn timeout)");
                    Console.Error.WriteLine("[SUDO] Failed to ensure admin Eye — UAC cancelled or timeout");
                    return 1;
                }

                // Normal --sudo command: ensure admin Eye, then proxy execution
                PulseStep.Line("path=proxy-route (non-eye command)");
                var sudoReason = args.Length > 0
                    ? $"--sudo {args[0]}"
                    : "--sudo request";
                if (SudoHandler.EnsureAdminForSudo(sudoReason))
                {
                    // admin Eye is responsive — proxy the command
                    PulseStep.Line($"ExecuteViaProxy: {args[0]} (timeout=5s)");
                    var exit = ElevatedEyeClient.ExecuteViaProxy(args[0], args.Skip(1).ToArray(), 5000);
                    PulseStep.Line($"proxy attempt exit={exit}");
                    if (exit != -1)
                    {
                        PulseStep.Finish($"proxy OK exit={exit}");
                        return exit;
                    }
                    // EnsureAdmin said alive but proxy comms dropped — rare; fall through
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Error.WriteLine("[SUDO:FALLBACK] admin Eye proxy dropped request mid-execution");
                    Console.Error.WriteLine("[SUDO:FALLBACK] continuing as regular user (admin-only ops will fail)");
                    Console.ResetColor();
                    PulseStep.Line("proxy drop → fallthrough to non-admin");
                }
                else
                {
                    // UAC cancelled or spawn failed
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Error.WriteLine("[SUDO:FALLBACK] admin Eye unavailable (UAC cancelled or spawn failed)");
                    Console.Error.WriteLine("[SUDO:FALLBACK] continuing as regular user — admin-only targets will fail with Access Denied");
                    Console.ResetColor();
                    PulseStep.Line("EnsureAdmin failed → fallthrough to non-admin");
                }
                // Fall through: args already has --sudo stripped, normal dispatch will run
            }
            PulseStep.Finish("already elevated or --sudo fallthrough → normal dispatch");
        }
        if (args.Contains("--read-only"))
        {
            ReadOnlyMode = true;
            args = args.Where(a => a != "--read-only").ToArray();
        }
        GrepModeActive = Console.IsOutputRedirected && args.Length > 0 && args[0].ToLowerInvariant() == "logcat" && !args.Any(a => a is "--past" or "--follow" or "-f" or "--timeout") && !args.Any(a => a is "--help" or "-h");
        OriginalStdout = Console.Out;
        TeeTextWriter? tee = (RunningInEye || _fastExitAfterCommand) ? null : new TeeTextWriter(GrepModeActive ? Console.Error : Console.Out, logFile, oldSubDir: oldSubDir);
        if (tee != null) Console.SetOut(new ThreadRoutingWriter(tee));
        if (tee != null && !GrepModeActive && !QuietFindOutput) Console.Error.WriteLine($"[LOG] {logFile}");
        {
            var launchCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            var launchHwnd = EyeCmdPipeServer.CallerHwnd.Value;
            var launchCmd = string.Join(" ", args);
            if (!QuietFindOutput) Console.Error.WriteLine($"[LAUNCH:CORE] cwd={launchCwd} hwnd=0x{launchHwnd:X} cmd={launchCmd}");
        }
        RelayFilePath = _fastExitAfterCommand ? Environment.GetEnvironmentVariable("WKAPPBOT_RELAY_FILE") : null;
        if (RelayFilePath != null)
        {
            try
            {
                var relayStream = new System.IO.FileStream(RelayFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Read);
                var _relayWriter = new System.IO.StreamWriter(relayStream, Console.OutputEncoding, bufferSize: 4096, leaveOpen: false) { AutoFlush = false };
                Console.SetOut(_relayWriter);
                OriginalStdout = _relayWriter;
            }
            catch { RelayFilePath = null; }
        }
        if (_fastExitAfterCommand) SetupSyncStdout();
        prof("TeeWriter ready");
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            try
            {
                var ex = e.ExceptionObject as Exception;
                var crashMsg = $"\n[CRASH] Unhandled exception — process terminating\n" + $"[CRASH] {ex?.GetType().FullName}: {ex?.Message}\n" + $"[CRASH] {ex?.StackTrace}\n";
                if (ex?.InnerException != null)
                    crashMsg += $"[CRASH] Inner: {ex.InnerException.GetType().FullName}: {ex.InnerException.Message}\n" + $"[CRASH] Inner stack: {ex.InnerException.StackTrace}\n";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(crashMsg);
                Console.ResetColor();
                try
                {
                    var stack = ex?.StackTrace?.Length > 300 ? ex.StackTrace[..300] + "..." : ex?.StackTrace;
                    var logRef = tee?.LogPath != null ? $"\nlog: {Path.GetFileName(tee.LogPath)}" : "";
                    RunSuggestCommand($"[CRASH] {ex?.GetType().Name}: {ex?.Message}{logRef}\n{stack}");
                }
                catch { }
                if (tee != null) Console.SetOut(tee.OriginalConsole);
                tee?.ForceCloseWithoutMove();
                Console.Error.WriteLine($"[CRASH] Log saved (not moved to old/): {tee?.LogPath}");
            }
            catch { }
        };
        int exitCode = 1;
        bool _skipTick = true;
        System.Threading.Timer? timeoutTimer = null;
        try
        {
            if (args.Length == 0)
            {
                prof("PrintUsage (no-args)");
                PrintUsage();
                return 1;
            }
            var command = args[0].ToLowerInvariant();
            var restArgs = args.Skip(1).ToArray();
            WKAppBot.WebBot.ChromeLauncher.OnFocusTheft ??= (chromeHwnd, prevFgHwnd) =>
            {
                FocuslessWarningOverlay.Show(chromeHwnd, "Chrome 복원 시 포커스 강탈 → 즉시 복구됨", "chrome");
                AutoBugReport($"Chrome focus theft: chrome=0x{chromeHwnd:X} prevFg=0x{prevFgHwnd:X}");
            };
            ActionApi.OnFocusStealer ??= (rootHwnd, action) =>
            {
                AppendFocusStealerKnowhow(rootHwnd, action);
                FocuslessWarningOverlay.Show(rootHwnd, $"UIA {action} 포커스 강탈 → 다음 실행 시 yield 팝업 자동 표시", null);
                AutoBugReport($"UIA focus steal: action={action} hwnd=0x{rootHwnd:X}");
            };
            WKAppBot.WebBot.CdpClient.OnFallbackSuggest ??= (text) =>
            {
                EyeCmdPipeServer.DispatchBg(["suggest", "[CDP-FALLBACK] " + text]);
            };
            if (restArgs.Any(a => a == "--i-dont-want-to-see-the-zoom-magnifier-overlay"))
            {
                ActionApi.ZoomEnabled = false;
                restArgs = restArgs.Where(a => a != "--i-dont-want-to-see-the-zoom-magnifier-overlay").ToArray();
            }
            for (int ti = 0; ti < restArgs.Length; ti++)
            {
                if (restArgs[ti] == "--timeout" && ti + 1 < restArgs.Length)
                {
                    var dur = ParseDuration(restArgs[ti + 1]);
                    restArgs = restArgs.Take(ti).Concat(restArgs.Skip(ti + 2)).ToArray();
                    if (dur > TimeSpan.Zero)
                    {
                        var timeoutMs = (long)dur.TotalMilliseconds;
                        timeoutTimer = new System.Threading.Timer(_ =>
                        {
                            Console.Error.WriteLine($"[TIMEOUT] {dur} elapsed — exiting (code 124)");
                            try { Console.Out.Flush(); Console.Error.Flush(); } catch { }
                            Environment.Exit(124);
                        }, null, timeoutMs, Timeout.Infinite);
                    }
                    break;
                }
            }
            prof($"command={command}");
            bool isFileCommand = command == "file" || command.StartsWith("file-", StringComparison.Ordinal);
            _skipTick = _fastExitAfterCommand || RunningInEye || isFileCommand;
            if (!_skipTick && !QuietFindOutput)
            {
                try { EmitEyeTick(command, cmdTag, "start"); } catch { }
                if (!GrepModeActive && !IsPipeMode) Console.Error.WriteLine(string.Join(" ", Environment.GetCommandLineArgs()));
                try { EmitEyeTick(command, cmdTag, "step:1/3:명령 준비"); } catch { }
                try
                {
                    var promptPreview = BuildPromptPreview(command, restArgs);
                    if (!string.IsNullOrWhiteSpace(promptPreview)) EmitEyeTick(command, cmdTag, $"prompt:{promptPreview}");
                }
                catch { }
            }
            var isEyeCommand = command == "eye";
            var isMcpCommand = command == "mcp";
            var subCmd = restArgs.Length > 0 ? restArgs[0].ToLowerInvariant() : "";
            bool isKillOrClose = command == "a11y" && subCmd is "kill" or "close";
            bool isHackWorker = command == "a11y" && subCmd.StartsWith("hack-", StringComparison.OrdinalIgnoreCase);
            var isExcluded = isEyeCommand || isMcpCommand || isKillOrClose || isHackWorker || _fastExitAfterCommand;
            if (!isExcluded && !RunningInEye) ThreadPool.QueueUserWorkItem(_ => { try { LaunchAppBotEyeIfNeeded(); } catch { } });
            if (TryPrintCommandHelp(command, restArgs)) { exitCode = 0; _exitCode = 0; return 0; }
            if (TryRunRegression(command, restArgs)) { exitCode = 0; _exitCode = 0; return 0; }
            if (!_fastExitAfterCommand) try { EmitEyeTick(command, cmdTag, "step:2/3:명령 실행"); } catch { }
            if (!QuietFindOutput && !GrepModeActive && !GrapMode && !IsPipeMode) try { Console.Error.WriteLine($"[ACT] cmd={command} args='{string.Join(" ", restArgs)}'"); } catch { }
            prof("dispatch");
            bool isConsoleDirect = !IsPipeMode && !RunningInEye && !IsMcpMode && !Console.IsErrorRedirected;
            using var _errScope = isConsoleDirect ? ErrorScope.Begin() : null;
            exitCode = command switch
            {
                "a11y" => A11yCommand(restArgs),
                "run" => RunCommand(restArgs),
                "find" => FindCommand(restArgs),
                "inspect" => InspectCommand(restArgs),
                "windows" => WindowsCommand(restArgs),
                "ocr" => OcrCommand(restArgs),
                "capture" => CaptureCommand(restArgs),
                "scan" => ScanCommand(restArgs),
                "ask" => AskCommand(restArgs),
                "agent" => AgentCommand(restArgs),
                "model" => ModelCommand(restArgs),
                "logcat" => LogcatCommand(restArgs),
                "eye" => AppBotEyeCommand(restArgs),
                "slack" => SlackCommand(restArgs),
                "web" => WebCommand(restArgs),
                "file" => FileCommand(restArgs),
                "code" => EditorAliasCommand("code", restArgs),
                "vscode" => EditorAliasCommand("vscode", restArgs),
                "file-edit" => FileCommand(new[] { "edit" }.Concat(restArgs).ToArray()),
                "file-open" => FileCommand(new[] { "open" }.Concat(restArgs).ToArray()),
                "file-read" => FileCommand(new[] { "read" }.Concat(restArgs).ToArray()),
                "file-grep" => FileCommand(new[] { "grep" }.Concat(restArgs).ToArray()),
                "file-glob" => FileCommand(new[] { "glob" }.Concat(restArgs).ToArray()),
                "mcp" => McpCommand(restArgs),
                "do" => DoCommand(restArgs),
                "input" => InputCommand(restArgs),
                "click" => ClickCommand(restArgs),
                "dismiss" => DismissCommand(restArgs),
                "win-click" => WinClickCommand(restArgs),
                "dialog-click" => DialogClickCommand(restArgs),
                "tab-select" => TabSelectCommand(restArgs),
                "cond-add" => CondAddCommand(restArgs),
                "focus" => FocusCommand(restArgs),
                "watch" => WatchCommand(restArgs),
                "snapshot" => SnapshotCommand(restArgs),
                "readiness" => ReadinessCommand(restArgs),
                "uia-test" => UiaTestCommand(restArgs),
                "form-dump" => FormDumpCommand(restArgs),
                "toolbar-ocr" => ToolbarOcrCommand(restArgs),
                "titlebar" => TitlebarCommand(restArgs),
                "validate" => ValidateCommand(restArgs),
                "chart-analyze" => ChartAnalyzeCommand(restArgs),
                "hts-stress" => HtsStressCommand(restArgs),
                "tooltip-probe" => TooltipProbeCommand(restArgs),
                "speak" => SpeakCommand(restArgs),
                "whisper" => WhisperCommand(restArgs),
                "newchat" => NewChatCommand(restArgs),
                "analyze-hack" => AnalyzeHackCommand(restArgs),
                "screensaver" => ScreenSaverStandaloneCommand(),
                "whisper-ring" => WhisperRingStandaloneCommand(restArgs),
                "prompt" => PromptCommand(restArgs),
                "schedule" => ScheduleCommand(restArgs),
                "dashboard" => DashboardCommand(restArgs),
                "knowhow" => KnowhowCommand(restArgs),
                "skill" => SkillCommand(restArgs),
                "win-move" => WindowMoveCommand(restArgs),
                "screen" => ScreenCommand(restArgs),
                "clipboard" => ClipboardCommand(restArgs),
                "claude-usage" => A11yClaudeUsage(),
                "enc-test" => EncTestCommand(),
                "suggest" => SuggestCommand(restArgs),
                "gc" => GcCommand(restArgs),
                "hotswap" => HotSwapCommand(restArgs),
                "update" => UpdateCommand(restArgs),
                "zoom-demo" => ZoomDemoCommand(restArgs),
                "tick" => TickCommand(restArgs),
                "kiwoom" => KiwoomCommand(restArgs),
                "com" => ComCommand(restArgs),
                "telegram" => TelegramCommand(restArgs),
                "stock-scan" => StockScanCommand(restArgs),
                "tree-select" => TreeSelectCommand(restArgs),
                "prompt-test" => PromptTestCommand(restArgs),
                "prompt-probe" => PromptProbeCommand(restArgs),
                "claude-detect" => ClaudeDetectCommand(restArgs),
                "find-prompts" => FindPromptsCommand(restArgs),
                "msaa-probe" => MsaaProbeCommand(restArgs),
                "--help" or "-h" or "help" => PrintUsage(),
                "--version" or "version" => PrintVersion(),
                _ when command.StartsWith("kro-trial-", StringComparison.OrdinalIgnoreCase) => KroTrialSpecialCommand(command, restArgs),
                _ => Error($"Unknown command: {command}")
            };
            try
            {
                if (!GrapMode && !IsPipeMode)
                {
                    if (exitCode == 0) Console.Error.WriteLine($"[ACT] result=ok cmd={command}");
                    else Console.Error.WriteLine($"[FALLBACK] result=fail code={exitCode} cmd={command}");
                }
            }
            catch { }
            if (_errScope != null)
            {
                bool errorDetected = _errScope.ErrorDetected;
                _errScope.Finalize(exitCode != 0);
                if (errorDetected && exitCode == 0)
                {
                    AutoRegisterBug($"[BUG-AUTO] `{command}` exited 0 but wrote to stderr — error suppressed");
                    exitCode = -9999;
                }
            }
            _exitCode = exitCode;
            return exitCode;
        }
        catch (Exception ex)
        {
            var errorMsg = $"Error: {ex.GetType().Name}: {ex.Message}";
            try { Console.ForegroundColor = ConsoleColor.Red; } catch { }
            try { Console.Error.WriteLine(errorMsg); } catch { }
            try { Console.WriteLine(errorMsg); } catch { }
            try { Console.ResetColor(); } catch { }
            return 1;
        }
        finally
        {
            if (!_skipTick)
            try
            {
                var cmd = args.Length > 0 ? args[0].ToLowerInvariant() : "noargs";
                if (exitCode == 0) EmitEyeTick(cmd, cmdTag, "done:작업 완료");
                else EmitEyeTick(cmd, cmdTag, "step:3/3:오류 처리");
                EmitEyeTick(cmd, cmdTag, $"end:{exitCode}");
            }
            catch { }
            if (tee != null) tee.ExitCode = exitCode;
            tee?.WriteErrorRecordNow();
            if (!_fastExitAfterCommand) WriteExitFile(exitCode);
            if (tee != null) Console.SetOut(tee.OriginalConsole);
            tee?.Dispose();
            if (tee != null) Console.Error.WriteLine($"Log saved: {tee.LogPath}  [{_mainStarted.Elapsed:m\\:ss\\.fff}  {DateTime.Now:HH:mm:ss}]");
            timeoutTimer?.Dispose();
            if (_fastExitAfterCommand) FastExit(exitCode);
        }
    }
}
