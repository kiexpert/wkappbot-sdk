using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Core.Scenario;

namespace WKAppBot.CLI;

// Program infrastructure: log rotation, command routing, exit machinery, sync stdout setup.
internal partial class Program
{
    // -- Log tag / subdir computation ------------------------------------─

    /// <summary>
    /// Compute cmdTag and oldSubDir from args for log file naming.
    ///   a11y &lt;action&gt;       -> tag=action,          dir=action
    ///   web fetch/read &lt;url&gt; -> tag=web-fetch-{host}, dir=web-{host}
    ///   ask/agent &lt;ai&gt;       -> tag=ask-gpt etc,      dir=ask-gpt
    ///   slack/file/...         -> tag=cmd-sub,           dir=cmd(-sub)
    ///   others               -> tag=cmd,               dir=cmd
    /// </summary>
    static (string cmdTag, string oldSubDir) ComputeCmdTagAndSubDir(string[] args)
    {
        var dir = ComputeOldSubDir(args);
        return (dir.Replace(" ", "-"), dir);
    }

    /// <summary>Public accessor for EyeCmdPipeServer -- computes "old {cmd} {sub}" directory name.</summary>
    internal static string ComputeOldSubDirPublic(string[] args) => ComputeOldSubDir(args);

    static string ComputeOldSubDir(string[] args)
    {
        if (args.Length == 0) return "noargs";
        var cmd = args[0].ToLowerInvariant();
        var sub = args.Length > 1 ? args[1].ToLowerInvariant() : "";

        switch (cmd)
        {
            case "a11y":
            {
                var action = sub.Length > 0 ? sub : "a11y";
                var grap   = args.Length > 2 ? args[2] : "";
                var win    = ExtractMainWindowFromGrap(grap);
                return win != null ? $"a11y {action} {win}" : $"a11y {action}";
            }

            case "web":
            {
                if (sub is "fetch" or "read")
                {
                    var url  = args.Length > 2 ? args[2] : "";
                    var host = ExtractUrlHost(url);
                    return host != null ? $"web {sub} {host}" : $"web {sub}";
                }
                return sub.Length > 0 ? $"web {sub}" : "web";
            }

            case "ask":
            case "agent":
            case "slack":
            case "file":
            case "schedule":
            case "knowhow":
            case "skill":
                return sub.Length > 0 ? $"{cmd} {sub}" : cmd;

            default:
            {
                var known = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "eye", "run", "find", "inspect", "windows", "ocr", "input", "click", "dismiss",
                    "win-click", "dialog-click", "tab-select", "cond-add", "focus", "watch", "snapshot",
                    "readiness", "uia-test", "form-dump", "toolbar-ocr", "titlebar", "validate",
                    "chart-analyze", "hts-stress", "tooltip-probe", "speak", "whisper", "newchat",
                    "analyze-hack", "screensaver", "whisper-ring", "prompt", "dashboard", "win-move",
                    "screen", "clipboard", "claude-usage", "enc-test", "suggest", "gc", "hotswap", "tick",
                    "kiwoom", "com", "telegram", "stock-scan", "logcat", "grep", "grap",
                    "help", "--help", "-h", "mcp", "claude-detect", "noise", "capture",
                    "elevate", "webbot", "code", "vscode", "version", "--version"
                };
                return known.Contains(cmd) ? cmd : "unknown";
            }
        }
    }

    /// <summary>Extract main window name from grap pattern for log folder naming.</summary>
    static string? ExtractMainWindowFromGrap(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) return null;
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase)) return null;

        string main;
        if (pattern.StartsWith("adb://", StringComparison.OrdinalIgnoreCase))
        {
            var rest = pattern["adb://".Length..];
            var parts = rest.Split('/');
            var win = parts.Length > 1 ? parts[1].Split('#')[0].Split(';')[0].Trim() : "";
            win = win.Replace("*", "").Replace("?", "").Trim();
            main = win.Length > 0 ? $"adb {win}" : "adb";
        }
        else
        {
            main = pattern.Split('#', '/', ';')[0].Trim();
            main = Regex.Replace(main, @"[*?\s\-]+", "");
        }

        main = string.Concat(main.Where(c => !Path.GetInvalidFileNameChars().Contains(c))).Trim();
        return main.Length >= 2 ? main : null;
    }

    static string? ExtractUrlHost(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;
        try
        {
            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                url = "https://" + url;
            var host = new Uri(url).Host.ToLowerInvariant();
            return string.IsNullOrEmpty(host) ? null : host;
        }
        catch { return null; }
    }

    // -- Log rotation ------------------------------------------------------

    static void RotateOldLogs(string logDir, int staleHours = 24)
    {
        try
        {
            var now = DateTime.UtcNow;
            var files = Directory
                .GetFiles(logDir, "*.out-*.log", SearchOption.TopDirectoryOnly)
                .Select(p => new FileInfo(p))
                .OrderBy(f => f.CreationTimeUtc)
                .ToList();

            foreach (var f in files)
            {
                if ((now - f.CreationTimeUtc).TotalHours < staleHours) continue;
                if (!TryGetPidFromLogName(f.Name, out var pid)) continue;
                if (IsProcessAlive(pid)) continue;

                try
                {
                    var destDir = Path.Combine(logDir, $"old {OldSubDirFromCmdTag(f.Name)}");
                    Directory.CreateDirectory(destDir);
                    var dest = Path.Combine(destDir, f.Name);
                    if (File.Exists(dest))
                    {
                        var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        dest = Path.Combine(destDir, $"{Path.GetFileNameWithoutExtension(f.Name)}.{stamp}{f.Extension}");
                    }
                    File.Move(f.FullName, dest);
                }
                catch { }
            }
        }
        catch { }
    }

    static string OldSubDirFromCmdTag(string fileName)
    {
        var pidIdx = fileName.LastIndexOf(".pid=", StringComparison.OrdinalIgnoreCase);
        var outIdx = fileName.IndexOf(".out-", StringComparison.OrdinalIgnoreCase);
        if (pidIdx < 0 || outIdx < 0) return "misc";
        var afterTs = outIdx + ".out-".Length + 16; // "YYYYMMDD_HHMMSS."
        if (afterTs >= pidIdx) return "misc";
        var tag = fileName[afterTs..pidIdx].ToLowerInvariant();
        if (tag.StartsWith("slack-"))     return "slack " + tag["slack-".Length..];
        if (tag.StartsWith("file-"))      return "file " + tag["file-".Length..];
        if (tag.StartsWith("web-fetch-")) return "web fetch " + tag["web-fetch-".Length..];
        if (tag.StartsWith("web-read-"))  return "web read " + tag["web-read-".Length..];
        if (tag.StartsWith("web-"))       return "web " + tag["web-".Length..];
        if (tag.StartsWith("ask-"))       return "ask " + tag["ask-".Length..];
        if (tag.StartsWith("agent-"))     return "agent " + tag["agent-".Length..];
        return string.IsNullOrEmpty(tag) ? "misc" : tag;
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

    // -- Command dispatch --------------------------------------------------

    static int RunCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot run <scenario.yaml> [-v|--verbose] [--no-watch] [--report <dir>]");

        string scenarioPath = args[0];
        bool verbose = args.Contains("-v") || args.Contains("--verbose");
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 200;
        string? reportDir = GetArgValue(args, "--report");

        var doc = ScenarioParser.Load(scenarioPath);
        Console.WriteLine($"Loaded: {doc.Scenario.Name} ({doc.Steps.Count} steps)");

        var runner = new ScenarioRunner(verbose, watch: !noWatch, watchIntervalMs: watchMs);

        runner.ZoomFactory = (screenRect, formHandle, action, label) =>
        {
            var helper = ClickZoomHelper.BeginFromRect(screenRect, formHandle, action, label);
            return helper != null ? new ClickZoomAdapter(helper) : null;
        };

        runner.ReadinessInstance = CreateInputReadiness();
        runner.VisionAskFn = (bmp, desc) => AskGeminiForVisionAsync(bmp, desc);

        var result = runner.Run(doc);
        return result.IsSuccess ? 0 : 1;
    }

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

    // -- P/Invoke exit machinery ------------------------------------------─

    // TerminateProcess: bypass ExitProcess / DLL detach / managed finalizers -> immediate exit.
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    private static extern IntPtr GetCurrentProcess();

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    private static extern uint GetLastError();

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr OpenEventW(uint dwDesiredAccess, bool bInheritHandle, string lpName);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetEvent(IntPtr hEvent);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool FlushFileBuffers(IntPtr hFile);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CreateFileW(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

    // -- Stdout / exit helpers --------------------------------------------─

    /// <summary>
    /// No-op: sync FileStream on overlapped pipe handles throws ArgumentException.
    /// FastExit() uses FlushFileBuffers + TerminateProcess instead.
    /// </summary>
    internal static void SetupSyncStdout() { }

    /// <summary>
    /// Write exit-file sentinel for Launcher's IOCP poll. Uses Win32 API directly.
    /// Does NOT call TerminateProcess -- caller returns normally from Main.
    /// </summary>
    static void WriteExitFile(int code)
    {
        try { Console.Out.Flush(); } catch { }
        try
        {
            var hOut = GetStdHandle(-11);
            if (hOut != IntPtr.Zero && hOut != new IntPtr(-1))
                FlushFileBuffers(hOut);
        }
        catch { }
        try
        {
            var exitFile = _exitFilePath ?? Environment.GetEnvironmentVariable("WKAPPBOT_EXIT_FILE");
            if (!string.IsNullOrEmpty(exitFile))
            {
                var hFile = CreateFileW(exitFile, 0x40000000, 0, IntPtr.Zero,
                    2, 0x80, IntPtr.Zero);
                if (hFile != IntPtr.Zero && hFile != new IntPtr(-1))
                {
                    var ecBytes = Encoding.UTF8.GetBytes(code.ToString());
                    WriteFile(hFile, ecBytes, (uint)ecBytes.Length, out _, IntPtr.Zero);
                    FlushFileBuffers(hFile);
                    CloseHandleInfra(hFile);
                }
            }
        }
        catch { }

        if (!string.IsNullOrEmpty(_exitEventName))
        {
            try
            {
                const uint EVENT_MODIFY_STATE = 0x0002;
                var hEvt = OpenEventW(EVENT_MODIFY_STATE, false, _exitEventName);
                if (hEvt != IntPtr.Zero)
                {
                    SetEvent(hEvt);
                    CloseHandleInfra(hEvt);
                }
            }
            catch { }
        }
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "CloseHandle")]
    private static extern bool CloseHandleInfra(IntPtr hObject);

    /// <summary>
    /// FastExit for grap/grep: flush stdout pipe to kernel buffer, then TerminateProcess.
    /// Avoids ExitProcess -> DLL_PROCESS_DETACH -> loader lock deadlock (~28s).
    /// </summary>
    static void FastExit(int code = 0)
    {
        try
        {
            Console.Out.Flush();

            if (RelayFilePath == null)
            {
                var hOut = GetStdHandle(-11); // STD_OUTPUT_HANDLE
                if (hOut != IntPtr.Zero && hOut != new IntPtr(-1))
                    FlushFileBuffers(hOut);

                try
                {
                    var exitDir = System.IO.Directory.Exists(@"C:\Temp") ? @"C:\Temp" : System.IO.Path.GetTempPath();
                    var exitFile = System.IO.Path.Combine(exitDir, $"wkappbot-exit-{Environment.ProcessId}");
                    var hFile = CreateFileW(exitFile, 0x40000000, 0, IntPtr.Zero, 2, 0x80, IntPtr.Zero);
                    if (hFile != IntPtr.Zero && hFile != new IntPtr(-1))
                    {
                        var ecBytes = Encoding.UTF8.GetBytes(code.ToString());
                        WriteFile(hFile, ecBytes, (uint)ecBytes.Length, out _, IntPtr.Zero);
                        FlushFileBuffers(hFile);
                        CloseHandleInfra(hFile);
                    }
                }
                catch { }

                try
                {
                    var exitDir2 = System.IO.Directory.Exists(@"C:\Temp") ? @"C:\Temp" : System.IO.Path.GetTempPath();
                    var hDir = CreateFileW(exitDir2, 0x40000000, 7, IntPtr.Zero, 3, 0x02000000, IntPtr.Zero);
                    if (hDir != IntPtr.Zero && hDir != new IntPtr(-1))
                    {
                        FlushFileBuffers(hDir);
                        CloseHandleInfra(hDir);
                    }
                }
                catch { }
            }

            if (RelayFilePath != null)
            {
                try { Console.Out.Close(); } catch { }
                Console.SetOut(System.IO.TextWriter.Null);

                var _readyPath    = RelayFilePath + ".ready";
                var _ackPath      = RelayFilePath + ".ack";
                var _exitCodePath = RelayFilePath + ".exitcode";
                try { System.IO.File.WriteAllText(_exitCodePath, code.ToString()); } catch { }
                try { System.IO.File.WriteAllText(_readyPath, "1"); } catch { }

                var _deadline = System.Diagnostics.Stopwatch.GetTimestamp()
                              + (long)(500 * System.Diagnostics.Stopwatch.Frequency / 1000.0);
                while (System.Diagnostics.Stopwatch.GetTimestamp() < _deadline)
                {
                    if (System.IO.File.Exists(_ackPath)) break;
                    System.Threading.Thread.Sleep(5);
                }
            }
        }
        catch { }

        TerminateProcess(GetCurrentProcess(), (uint)code);
        System.Threading.Thread.Sleep(500);
        Environment.Exit(code);
    }
}
