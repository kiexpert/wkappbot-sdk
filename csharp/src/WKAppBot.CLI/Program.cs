using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WKAppBot.Core.Scenario;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Window;

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
    internal static readonly string DataDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq");

    internal static string GetCurrentSessionHash()
    {
        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir))
                return "none";

            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .FirstOrDefault();
            if (string.IsNullOrEmpty(latestFile) || !File.Exists(latestFile))
                return "none";

            using var sha = SHA256.Create();
            var data = Encoding.UTF8.GetBytes(Path.GetFullPath(latestFile));
            var hash = sha.ComputeHash(data);
            return Convert.ToHexString(hash).ToLowerInvariant()[..16];
        }
        catch
        {
            return "none";
        }
    }

    static int Main(string[] args)
    {
        // Force UTF-8 output (Windows 949 codepage breaks Korean in non-Korean terminals)
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // MCP stdio server — must run BEFORE TeeTextWriter (stdout = JSON-RPC only)
        if (args.Length > 0 && args[0] == "mcp")
        {
            try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
            return McpCommand(args.Skip(1).ToArray());
        }

        // Enable DPI awareness
        try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }

        // Screen reader mode: once enabled for Chromium/Electron, stays ON permanently.
        // No restore on exit — next run starts instantly (no broadcast delay).

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
            // Policy broadcast only on Eye spawn (not every CLI command)
            // — prevents stdout pollution for ask/web/other commands
            
            // Busybox-style: detect command from exe name (symlink-friendly)
            // e.g. a11y.exe find "*메모장*" → wkappbot a11y find "*메모장*"
            //      inspect.exe "*메모장*"   → wkappbot inspect "*메모장*"
            var exeBaseName = Path.GetFileNameWithoutExtension(exePath).ToLowerInvariant();
            var implicitCmd = DetectCommandFromExeName(exeBaseName);
            if (implicitCmd != null && (args.Length == 0 || args[0].ToLowerInvariant() != implicitCmd))
            {
                args = new string[] { implicitCmd }.Concat(args).ToArray();
            }

            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            var command = args[0].ToLowerInvariant();
            var restArgs = args.Skip(1).ToArray();

            // [FL] Chrome focus theft → focusless warning overlay
            WKAppBot.WebBot.ChromeLauncher.OnFocusTheft ??= (chromeHwnd, prevFgHwnd) =>
            {
                FocuslessWarningOverlay.Show(chromeHwnd, "Chrome 복원 시 포커스 강탈 → 즉시 복구됨", "chrome");
            };

            // Global option: disable zoom overlay — intentionally obnoxious name to discourage use
            if (restArgs.Any(a => a == "--i-dont-want-to-see-the-zoom-magnifier-overlay"))
            {
                ActionApi.ZoomEnabled = false;
                restArgs = restArgs.Where(a => a != "--i-dont-want-to-see-the-zoom-magnifier-overlay").ToArray();
            }

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

            // Auto-launch AppBotEye for ALL commands except help and eye global mode
            // 앱봇이 뭔가 하면 눈은 항상 떠있어야! (도움말, eye 글로벌모드는 제외 — 무한 cascade 방지)
            // eye tick은 one-shot이라 Eye를 띄워도 cascade 안 됨 → 자동 실행 대상!
            // fire-and-forget on ThreadPool — 명령 실행에 0ms 지연
            var isEyeGlobal = command == "eye" && (restArgs.Length == 0 || restArgs[0] != "tick");
            var isExcluded = command is "help" or "--help" or "-h" or "prompt-test" or "tick" or "uia-test" or "newchat" || isEyeGlobal;
            if (!isExcluded)
            {
                ThreadPool.QueueUserWorkItem(_ => { try { LaunchAppBotEyeIfNeeded(); } catch { } });
            }

            try { EmitEyeTick(command, cmdTag, "step:2/3:명령 실행"); } catch { }
            try { Console.WriteLine($"[ACT] cmd={command} args='{string.Join(" ", restArgs)}'"); } catch { }

            exitCode = command switch
            {
                // Primary: a11y universal interface (busybox: a11y.exe symlink)
                "a11y" => A11yCommand(restArgs),
                // Core
                "run" => RunCommand(restArgs),
                "find" => FindCommand(restArgs),
                "inspect" => InspectCommand(restArgs),
                "windows" => WindowsCommand(restArgs),
                "ocr" => OcrCommand(restArgs),
                "capture" => CaptureCommand(restArgs),
                "scan" => ScanCommand(restArgs),
                "ask" => AskCommand(restArgs),
                "logcat" => LogcatCommand(restArgs),
                "eye" => AppBotEyeCommand(restArgs),
                "slack" => SlackCommand(restArgs),
                "web" => WebCommand(restArgs),
                // Automation
                "do" => DoCommand(restArgs),
                "input" => InputCommand(restArgs),
                "click" => ClickCommand(restArgs),
                "dismiss" => DismissCommand(restArgs),
                "win-click" => WinClickCommand(restArgs),
                "dialog-click" => DialogClickCommand(restArgs),
                "tab-select" => TabSelectCommand(restArgs),
                "cond-add" => CondAddCommand(restArgs),
                // Inspection
                "focus" => FocusCommand(restArgs),
                "watch" => WatchCommand(restArgs),
                "snapshot" => SnapshotCommand(restArgs),
                "readiness" => ReadinessCommand(restArgs),
                "uia-test" => UiaTestCommand(restArgs),
                "form-dump" => FormDumpCommand(restArgs),
                "toolbar-ocr" => ToolbarOcrCommand(restArgs),
                "titlebar" => TitlebarCommand(restArgs),
                // Analysis & Testing
                "validate" => ValidateCommand(restArgs),
                "chart-analyze" => ChartAnalyzeCommand(restArgs),
                "hts-stress" => HtsStressCommand(restArgs),
                "tooltip-probe" => TooltipProbeCommand(restArgs),
                // Utility
                "newchat" => NewChatCommand(restArgs),
                "schedule" => ScheduleCommand(restArgs),
                "knowhow" => KnowhowCommand(restArgs),
                "win-move" => WindowMoveCommand(restArgs),
                "screen" => ScreenCommand(restArgs),
                "zoom-demo" => ZoomDemoCommand(restArgs),
                "tick" => TickCommand(restArgs),
                // External
                "kiwoom" => KiwoomCommand(restArgs),
                "com" => ComCommand(restArgs),
                "telegram" => TelegramCommand(restArgs),
                "stock-scan" => StockScanCommand(restArgs),
                "tree-select" => TreeSelectCommand(restArgs),
                "prompt-test" => PromptTestCommand(restArgs),
                "msaa-probe" => MsaaProbeCommand(restArgs),
                "--help" or "-h" or "help" => PrintUsage(),
                "--version" or "version" => PrintVersion(),
                _ when command.StartsWith("kro-trial-", StringComparison.OrdinalIgnoreCase) => KroTrialSpecialCommand(command, restArgs),
                _ => Error($"Unknown command: {command}")
            };

            try
            {
                if (exitCode == 0) Console.WriteLine($"[ACT] result=ok cmd={command}");
                else Console.WriteLine($"[FALLBACK] result=fail code={exitCode} cmd={command}");
            }
            catch { }

            // Auto snapshot + profile-based experience DB recording on A11Y action failure
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
                            Console.WriteLine($"[FALLBACK] auto snapshot+experience trigger (cmd={command})");
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

                            // 1. Snapshot (UIA tree + screenshot + OCR + per-control learning)
                            if (!string.IsNullOrWhiteSpace(cidArg))
                                SnapshotCommand(new[] { winTitle, "--tag", $"a11y_fail_{command}", "--depth", "2", "--cid", cidArg });
                            else
                                SnapshotCommand(new[] { winTitle, "--tag", $"a11y_fail_{command}", "--depth", "2" });

                            // 2. Profile-based action log (screenshot ring buffer + JSONL in control/form folder)
                            var stdAction = ResolveStandardAction(command, restArgs);
                            WriteA11yFailToProfileDb(winTitle, command, stdAction, cidArg, "action-failed");

                            // 3. Legacy flat file record (backward compat, will be phased out)
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
        public string Cwd { get; set; } = "";  // working directory of the CLI process
    }

    internal static string EyeTicksPath => Path.Combine(DataDir, "runtime", "eye_ticks.jsonl");

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
                Cwd = Environment.CurrentDirectory,
            };

            File.AppendAllText(EyeTicksPath, JsonSerializer.Serialize(tick) + Environment.NewLine);
        }
        catch
        {
            // best effort
        }
    }

    /// <summary>
    /// Emit eye tick with auto-detected host from eye_ticks.jsonl.
    /// For use in PostPublish where process tree doesn't reach Claude Desktop.
    /// Scans recent ticks for most recent Claude/Code host and adopts that host PID.
    /// </summary>
    static void EmitEyeTickWithHost(string command, string tag, string status)
    {
        try
        {
            var runtimeDir = Path.Combine(DataDir, "runtime");
            Directory.CreateDirectory(runtimeDir);

            // Find most recent host from eye_ticks.jsonl (tail 8KB = ~last 30 ticks)
            int hostPid = 0;
            string hostName = "", hostTitle = "";
            try
            {
                if (File.Exists(EyeTicksPath))
                {
                    var lines = ReadTailBytes(EyeTicksPath, 8192);
                    // Scan in reverse — most recent first
                    for (int i = lines.Length - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i])) continue;
                        var t = System.Text.Json.JsonSerializer.Deserialize<EyeTick>(lines[i]);
                        if (t?.HostPid > 0 && !string.IsNullOrWhiteSpace(t.HostTitle))
                        {
                            // Found a tick with a real host (e.g. claude, Code) — adopt it
                            hostPid = t.HostPid;
                            hostName = t.HostName ?? "";
                            hostTitle = t.HostTitle ?? "";
                            break;
                        }
                    }
                }
            }
            catch { }

            // Fallback to normal host detection if no host found in ticks
            if (hostPid <= 0)
            {
                var pid = Environment.ProcessId;
                var ppid = GetParentPid(pid);
                (hostPid, hostName, hostTitle) = FindLogicalHost(pid, ppid);
            }

            var tick = new EyeTick
            {
                Pid = Environment.ProcessId,
                ParentPid = GetParentPid(Environment.ProcessId),
                ParentName = "build",
                HostPid = hostPid,
                HostName = hostName,
                HostTitle = hostTitle,
                Command = command,
                Tag = tag,
                Status = status,
                Cwd = Environment.CurrentDirectory,
            };

            File.AppendAllText(EyeTicksPath, JsonSerializer.Serialize(tick) + Environment.NewLine);
        }
        catch { }
    }

    /// <summary>Read tail bytes from a file and split into lines (shared read, no lock)</summary>
    static string[] ReadTailBytes(string path, int byteCount)
    {
        try
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            if (fs.Length > byteCount) fs.Seek(-byteCount, SeekOrigin.End);
            using var sr = new StreamReader(fs);
            var text = sr.ReadToEnd();
            return text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }
        catch { return Array.Empty<string>(); }
    }

    static (int hostPid, string hostName, string hostTitle) FindLogicalHost(int selfPid, int directParentPid)
    {
        static bool IsShell(string n)
        {
            var x = (n ?? "").ToLowerInvariant();
            // Include build tools (dotnet, msbuild) so PostPublish ticks walk past them to Claude
            return x is "powershell" or "pwsh" or "cmd" or "conhost" or "node" or "wkappbot"
                or "dotnet" or "msbuild";
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

    /// <summary>
    /// Write A11Y failure to profile-based ExperienceDb.
    /// Screenshot → ring buffer (log_0..log_8.png) + metadata → action_log.jsonl
    /// Stored under profiles/{name}_exp/form_{id}/logs/ or form_{id}/tree/.../logs/
    /// </summary>
    static void WriteA11yFailToProfileDb(string windowTitle, string command, string stdAction, string cidArg, string reason)
    {
        try
        {
            var windows = WKAppBot.Win32.Window.WindowFinder.FindByTitle(windowTitle);
            if (windows.Count == 0) return;
            var win = windows[0];

            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid);
            string procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

            // Find matching profile
            var profileStore = new WKAppBot.Win32.Window.ProfileStore();
            var profileMatch = profileStore.FindByMatch(win.ClassName, "")
                ?? (!string.IsNullOrEmpty(procName) ? profileStore.FindByMatch("", procName) : null);

            if (profileMatch == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[A11Y] 프로파일 없음 — 경험DB 기록 건너뜀 (wkappbot scan --save 필요)");
                Console.ResetColor();
                return;
            }

            var expDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
            var expDb = new WKAppBot.Win32.Window.ExperienceDb(expDir);

            // Capture window screenshot for ring buffer
            System.Drawing.Bitmap? screenshot = null;
            try
            {
                screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
                if (screenshot != null && WKAppBot.Win32.Input.ScreenCapture.IsBlankBitmap(screenshot))
                {
                    screenshot.Dispose();
                    screenshot = null;
                }
            }
            catch { }

            // Build metadata JSON
            int? cid = int.TryParse(cidArg, out var c) ? c : null;
            var metadata = JsonSerializer.Serialize(new
            {
                ts = DateTime.Now.ToString("O"),
                cmd = command,
                action = stdAction,
                reason,
                window = win.Title,
                @class = win.ClassName,
                pid = (int)pid,
                cid,
                profile = profileMatch.Value.name
            });

            // Scan for active forms to find the right form to log under
            var scanResult = WKAppBot.Win32.Window.AppScanner.Scan(win.Handle);
            bool saved = false;

            if (cid.HasValue)
            {
                // Try to find which form contains this control
                foreach (var form in scanResult.Forms.Where(f => f.FormId != null && f.IsVisible))
                {
                    // Save to form-level log under action-name subfolder
                    expDb.SaveFormActionLog(form.FormId!, screenshot, metadata, actionName: stdAction);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[A11Y] 프로파일 경험DB 기록: profile={profileMatch.Value.name}, form={form.FormId}, action={stdAction}, cid={cid}");
                    Console.ResetColor();
                    saved = true;
                    break;
                }
            }

            if (!saved)
            {
                // Save to the first visible form, or a generic "unknown" form
                var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId != null && f.IsVisible);
                var formId = targetForm?.FormId ?? "unknown";
                expDb.SaveFormActionLog(formId, screenshot, metadata, actionName: stdAction);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[A11Y] 프로파일 경험DB 기록: profile={profileMatch.Value.name}, form={formId}, action={stdAction}");
                Console.ResetColor();
            }

            screenshot?.Dispose();

            // Also run QuickTouchControls to accumulate per-control experience
            try
            {
                var (forms, controls, screenshots2) = WKAppBot.Win32.Window.AppScanner.QuickTouchControls(scanResult, expDb);
                if (controls > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[A11Y] 추가 경험 학습: {forms} forms, {controls} controls, {screenshots2} new screenshots");
                    Console.ResetColor();
                }
            }
            catch { }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[A11Y] 프로파일 경험DB 기록 실패: {ex.Message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Show past failure history and knowhow hints for a form.
    /// Called when accessing a form that has previous action log entries in ExperienceDb.
    /// Shared across do/input/click commands.
    /// </summary>
    /// <summary>
    /// Show form-level experience hints.
    /// actionName이 있으면 knowhow-{action}.md 우선 방송, 없으면 knowhow.md 방송.
    /// </summary>
    static void ShowFormExperienceHints(ExperienceDb expDb, string formId, string? actionName = null)
    {
        try
        {
            var summary = expDb.GetFormActionLogSummary(formId);
            if (summary != null && summary.TotalEntries > 0)
            {
                var breakdown = summary.ActionBreakdown.Count > 0
                    ? string.Join(", ", summary.ActionBreakdown.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [EXP] ⚠ 이전 기록 {summary.TotalEntries}건");
                if (!string.IsNullOrEmpty(breakdown))
                    Console.Write($" ({breakdown})");
                Console.WriteLine($", 스크린샷 {summary.ScreenshotCount}장");
                Console.ResetColor();
            }

            // 액션 노하우 우선, 없으면 일반 노하우 방송
            string? knowhowPath = null;
            if (!string.IsNullOrEmpty(actionName))
                knowhowPath = expDb.GetFormActionKnowhowPath(formId, actionName);
            knowhowPath ??= expDb.GetFormKnowhowPath(formId);

            if (knowhowPath != null)
                ShowKnowhowBroadcast(knowhowPath);
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Show control-level experience hints.
    /// actionName이 있으면 knowhow-{action}.md 우선 방송, 없으면 knowhow.md 방송.
    /// 노하우 없으면 "여기에 노하우를 남겨주세요" 절대경로 안내.
    /// </summary>
    static void ShowControlExperienceHints(ExperienceDb expDb, string formId, int cid,
        string? actionName = null)
    {
        try
        {
            // Control-level log history
            var summary = expDb.GetControlActionLogSummary(formId, cid);
            if (summary != null && summary.TotalEntries > 0)
            {
                var breakdown = summary.ActionBreakdown.Count > 0
                    ? string.Join(", ", summary.ActionBreakdown.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [EXP] cid={cid}: 이전 기록 {summary.TotalEntries}건");
                if (!string.IsNullOrEmpty(breakdown))
                    Console.Write($" ({breakdown})");
                Console.WriteLine($", 스크린샷 {summary.ScreenshotCount}장");
                Console.ResetColor();
            }

            // 액션별 노하우 우선 → 일반 노하우 폴백
            bool found = false;
            if (!string.IsNullOrEmpty(actionName))
            {
                var (actFormPath, actCtrlPath) = expDb.GetActionKnowhowPaths(formId, cid, actionName);
                if (actCtrlPath != null) { ShowKnowhowBroadcast(actCtrlPath); found = true; }
                else if (actFormPath != null) { ShowKnowhowBroadcast(actFormPath); found = true; }
            }
            if (!found)
            {
                var (formPath, ctrlPath) = expDb.GetKnowhowPaths(formId, cid);
                if (ctrlPath != null) { ShowKnowhowBroadcast(ctrlPath); found = true; }
                else if (formPath != null) { ShowKnowhowBroadcast(formPath); found = true; }
            }

            // 노하우 없을 때: 노하우 파일 경로 안내 (미래 클롣이 발견하면 여기에 기록)
            if (!found && summary != null && summary.TotalEntries > 0)
            {
                var ctrlDir = expDb.GetControlDir(formId, cid, create: false);
                var suggestedFile = !string.IsNullOrEmpty(actionName)
                    ? $"knowhow-{actionName.ToLowerInvariant()}.md"
                    : "knowhow.md";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  [KNOWHOW] 노하우 발견 시 기록 → {Path.Combine(ctrlDir, suggestedFile)}");
                Console.ResetColor();
            }
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Knowhow 방송: 파일의 첫 문단(개요/목차)을 출력.
    /// 첫 문단 = 파일 시작부터 첫 번째 빈 줄(\n\n) 전까지의 비어있지 않은 줄들.
    /// ## 헤더가 있으면 첫 헤더를 타이틀로, 그 아래 첫 문단을 내용으로 표시.
    /// 섹션 수도 함께 표시 → 미래 클롣이 "더 있다"는 것을 인지.
    /// </summary>
    static void ShowKnowhowBroadcast(string knowhowPath, string tag = "KNOWHOW")
    {
        try
        {
            var allLines = File.ReadAllLines(knowhowPath, System.Text.Encoding.UTF8);
            if (allLines.Length == 0) return;

            // ## 헤더 수 카운트 (섹션 수 표시용)
            int sectionCount = 0;
            for (int i = 0; i < allLines.Length; i++)
            {
                if (allLines[i].TrimStart().StartsWith("## "))
                    sectionCount++;
            }

            // 첫 헤더 찾기 (타이틀)
            string title = "";
            int contentStart = 0;
            for (int i = 0; i < allLines.Length; i++)
            {
                var trimmed = allLines[i].TrimStart();
                if (trimmed.StartsWith("## "))
                {
                    title = trimmed.TrimStart('#').Trim();
                    contentStart = i + 1;
                    break;
                }
                // 헤더 없이 바로 내용 시작하는 경우
                if (!string.IsNullOrWhiteSpace(allLines[i]))
                {
                    contentStart = i;
                    break;
                }
            }

            // 첫 문단 추출: contentStart부터 빈 줄(\n\n) 또는 다음 ## 헤더까지
            var paragraph = new List<string>();
            bool started = false;
            for (int i = contentStart; i < allLines.Length; i++)
            {
                var line = allLines[i].TrimEnd();
                // 다음 ## 섹션이면 끝
                if (line.TrimStart().StartsWith("## "))
                    break;
                // 빈 줄이면 문단 끝 (내용 시작 후에만)
                if (started && string.IsNullOrWhiteSpace(line))
                    break;
                if (!string.IsNullOrWhiteSpace(line))
                {
                    started = true;
                    paragraph.Add(line);
                }
            }

            if (paragraph.Count == 0 && string.IsNullOrEmpty(title)) return;

            // 출력: [KNOWHOW] (N sections) [title] paragraph...
            Console.ForegroundColor = ConsoleColor.Magenta;
            var countInfo = sectionCount > 1 ? $" ({sectionCount}§)" : "";
            Console.Write($"  [{tag}]{countInfo} ");
            Console.ResetColor();

            if (!string.IsNullOrEmpty(title))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{title}: ");
                Console.ResetColor();
            }

            if (paragraph.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                // 줄 합치되 너무 길면 잘라내기 (토큰 절약)
                var text = string.Join(" ", paragraph);
                if (text.Length > 200) text = text[..197] + "...";
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
            }
        }
        catch { /* best-effort */ }
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

        // [ZOOM] Wire up zoom overlay factory for per-step visual feedback
        runner.ZoomFactory = (screenRect, formHandle, action, label) =>
        {
            var helper = ClickZoomHelper.BeginFromRect(screenRect, formHandle, action, label);
            return helper != null ? new ClickZoomAdapter(helper) : null;
        };

        // [READINESS] Wire up InputReadiness for pre-action blocker/minimize check
        runner.ReadinessInstance = CreateInputReadiness();

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
