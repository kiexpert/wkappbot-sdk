using System.Text.Json;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// EyeTick: lightweight progress record written to eye_ticks.jsonl.
// Consumed by Eye loop (AppBotEyeHealthCheck, AppBotEyeJsonlParser) to track active sessions.
internal partial class Program
{
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
        public string Cwd { get; set; } = "";
        public string PromptHwnd { get; set; } = ""; // caller prompt window handle (session key)
        public string Args { get; set; } = "";       // command-line args (skip argv[0] exe path)
    }

    internal static string EyeTicksPath => Path.Combine(DataDir, "runtime", "eye_ticks.jsonl");

    static void EmitEyeTick(string command, string tag, string status)
    {
        try
        {
            if (QuietFindOutput) return;
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

            // Include args only on "start" to avoid repeating in every step/end entry
            var args = status == "start"
                ? string.Join(" ", Environment.GetCommandLineArgs().Skip(1))
                : "";

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
                Cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory,
                PromptHwnd = EyeCmdPipeServer.CallerHwnd.Value is IntPtr h && h != IntPtr.Zero
                    ? $"0x{h.ToInt64():X}" : "",
                Args = args,
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
                    // Scan in reverse -- most recent first
                    for (int i = lines.Length - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i])) continue;
                        var t = JsonSerializer.Deserialize<EyeTick>(lines[i]);
                        if (t?.HostPid > 0 && !string.IsNullOrWhiteSpace(t.HostTitle))
                        {
                            // Found a tick with a real host (e.g. claude, Code) -- adopt it
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
                Cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory,
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

    /// <summary>
    /// Safe wrapper for Process.MainWindowTitle -- times out after <paramref name="timeoutMs"/> ms.
    /// Process.MainWindowTitle calls GetWindowText (Win32) which sends WM_GETTEXT via SendMessage.
    /// For hung/not-responding windows this blocks for up to 30 seconds. This wrapper avoids that.
    /// </summary>
    static string GetMainWindowTitleSafe(System.Diagnostics.Process p, int timeoutMs = 150)
    {
        string? title = null;
        var t = new Thread(() => { try { title = p.MainWindowTitle; } catch { } }) { IsBackground = true };
        t.Start();
        t.Join(timeoutMs);
        return title ?? "";
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
                var title = GetMainWindowTitleSafe(p);

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
                return (directParentPid, p.ProcessName ?? "unknown", GetMainWindowTitleSafe(p));
            }
            catch { }
        }

        return (selfPid, "wkappbot", "");
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

        var c = (command ?? "").ToLowerInvariant();
        if (c is not ("ask" or "agent" or "web" or "kiwoom" or "com" or "telegram" or "schedule" or "knowhow"))
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
}
