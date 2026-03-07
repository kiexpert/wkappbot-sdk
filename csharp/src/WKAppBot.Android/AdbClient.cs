using System.Diagnostics;
using System.Text;

namespace WKAppBot.Android;

/// <summary>
/// Low-level ADB command executor. Wraps adb.exe process invocation
/// with MSYS_NO_PATHCONV=1 to prevent Git Bash path mangling.
/// </summary>
public class AdbClient
{
    private readonly string _adbPath;
    private readonly int _timeoutMs;

    public AdbClient(string? adbPath = null, int timeoutMs = 10000)
    {
        _adbPath = adbPath ?? FindAdb();
        _timeoutMs = timeoutMs;
    }

    public string AdbPath => _adbPath;

    // ── Core execution ────────────────────────────────────

    public AdbResult Run(string args, string? serial = null, int? timeoutMs = null)
    {
        var fullArgs = serial != null ? $"-s {serial} {args}" : args;
        return Execute(fullArgs, timeoutMs ?? _timeoutMs);
    }

    public AdbResult Shell(string shellCmd, string? serial = null, int? timeoutMs = null)
    {
        // Quote the shell command to prevent host-side interpretation
        return Run($"shell \"{shellCmd}\"", serial, timeoutMs);
    }

    /// <summary>Run shell command without quoting (for piped commands, etc.)</summary>
    public AdbResult ShellRaw(string shellCmd, string? serial = null, int? timeoutMs = null)
    {
        return Run($"shell {shellCmd}", serial, timeoutMs);
    }

    // ── Device operations ─────────────────────────────────

    public List<AdbDevice> ListDevices()
    {
        var result = Execute("devices -l", 5000);
        if (result.ExitCode != 0) return [];

        var devices = new List<AdbDevice>();
        foreach (var line in result.StdOut.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.StartsWith("List of") || line.StartsWith("*")) continue;
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) continue;

            var serial = parts[0];
            var state = parts[1]; // device, offline, unauthorized, etc.

            string? model = null, product = null, device = null, transportId = null;
            foreach (var part in parts.Skip(2))
            {
                if (part.StartsWith("model:")) model = part[6..];
                else if (part.StartsWith("product:")) product = part[8..];
                else if (part.StartsWith("device:")) device = part[7..];
                else if (part.StartsWith("transport_id:")) transportId = part[13..];
            }

            devices.Add(new AdbDevice
            {
                Serial = serial,
                State = state,
                Model = model,
                Product = product,
                DeviceName = device,
                TransportId = transportId
            });
        }
        return devices;
    }

    // ── Screencap ─────────────────────────────────────────

    public bool Screencap(string outputPath, string? serial = null, string? displayId = null)
    {
        var displayArg = displayId != null ? $" -d {displayId}" : "";
        var remotePath = "/data/local/tmp/wkappbot_screen.png";
        var r1 = Shell($"screencap -p{displayArg} {remotePath}", serial, 15000);
        if (r1.ExitCode != 0) return false;

        var r2 = Run($"pull {remotePath} \"{outputPath}\"", serial, 15000);
        return r2.ExitCode == 0;
    }

    // ── UI Automator dump ─────────────────────────────────

    public string? DumpUiTree(string? serial = null)
    {
        var remotePath = "/data/local/tmp/wkappbot_ui.xml";
        var r1 = Shell($"uiautomator dump {remotePath}", serial, 15000);
        if (r1.ExitCode != 0 && !r1.StdOut.Contains("dumped to"))
            return null;

        // Pull to temp file
        var localPath = Path.Combine(Path.GetTempPath(), $"adb_ui_{serial ?? "default"}.xml");
        var r2 = Run($"pull {remotePath} \"{localPath}\"", serial, 10000);
        if (r2.ExitCode != 0) return null;

        return File.ReadAllText(localPath);
    }

    // ── Input commands ────────────────────────────────────

    public AdbResult Tap(int x, int y, string? serial = null)
        => Shell($"input tap {x} {y}", serial);

    public AdbResult Swipe(int x1, int y1, int x2, int y2, int durationMs = 300, string? serial = null)
        => Shell($"input swipe {x1} {y1} {x2} {y2} {durationMs}", serial);

    public AdbResult KeyEvent(string keycode, string? serial = null)
        => Shell($"input keyevent {keycode}", serial);

    public AdbResult InputText(string text, string? serial = null)
    {
        // ASCII only — Korean requires ADB Keyboard IME or clipboard paste
        var escaped = text.Replace("\"", "\\\"").Replace(" ", "%s").Replace("&", "\\&");
        return Shell($"input text \"{escaped}\"", serial);
    }

    /// <summary>Broadcast text via ADB Keyboard IME (supports Unicode/Korean)</summary>
    public AdbResult BroadcastText(string text, string? serial = null)
    {
        var escaped = text.Replace("\"", "\\\"");
        return Shell($"am broadcast -a ADB_INPUT_TEXT --es msg \"{escaped}\"", serial);
    }

    /// <summary>Set clipboard and paste (fallback for Korean input)</summary>
    public AdbResult ClipboardPaste(string text, string? serial = null)
    {
        var escaped = text.Replace("\"", "\\\"");
        var r = Shell($"am broadcast -a clipper.set -e text \"{escaped}\"", serial);
        if (r.ExitCode != 0) return r;
        return KeyEvent("279", serial); // KEYCODE_PASTE
    }

    // ── App management ────────────────────────────────────

    public AdbResult ForceStop(string package, string? serial = null)
        => Shell($"am force-stop {package}", serial);

    public AdbResult LongPress(int x, int y, int durationMs = 1000, string? serial = null)
        => Shell($"input swipe {x} {y} {x} {y} {durationMs}", serial);

    public AdbResult Back(string? serial = null) => KeyEvent("4", serial);
    public AdbResult Home(string? serial = null) => KeyEvent("3", serial);
    public AdbResult RecentApps(string? serial = null) => KeyEvent("187", serial);

    public AdbResult StartApp(string package, string? serial = null)
        => Shell($"monkey -p {package} -c android.intent.category.LAUNCHER 1", serial);

    public string? GetCurrentActivity(string? serial = null)
    {
        var r = Shell("dumpsys window | grep mCurrentFocus", serial);
        if (r.ExitCode != 0) return null;
        // mCurrentFocus=Window{xxx u0 com.pkg/com.pkg.Activity}
        var match = System.Text.RegularExpressions.Regex.Match(r.StdOut, @"(\S+/\S+)\}");
        return match.Success ? match.Groups[1].Value : null;
    }

    public string? GetProp(string prop, string? serial = null)
    {
        var r = Shell($"getprop {prop}", serial);
        return r.ExitCode == 0 ? r.StdOut.Trim() : null;
    }

    // ── Internal ──────────────────────────────────────────

    private AdbResult Execute(string args, int timeoutMs)
    {
        var psi = new ProcessStartInfo
        {
            FileName = _adbPath,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        // Critical: prevent Git Bash from mangling /data/local/tmp paths
        psi.Environment["MSYS_NO_PATHCONV"] = "1";
        psi.Environment["MSYS2_ARG_CONV_EXCL"] = "*";

        try
        {
            using var proc = Process.Start(psi)!;
            var stdout = proc.StandardOutput.ReadToEnd();
            var stderr = proc.StandardError.ReadToEnd();

            if (!proc.WaitForExit(timeoutMs))
            {
                try { proc.Kill(); } catch { }
                return new AdbResult(-1, "", "Timeout after " + timeoutMs + "ms");
            }

            return new AdbResult(proc.ExitCode, stdout, stderr);
        }
        catch (Exception ex)
        {
            return new AdbResult(-1, "", $"Failed to run adb: {ex.Message}");
        }
    }

    private static string FindAdb()
    {
        // Check common locations
        var candidates = new[]
        {
            Path.Combine(Environment.GetEnvironmentVariable("ANDROID_HOME") ?? "", "platform-tools", "adb.exe"),
            @"W:\SDK\Android\platform-tools\adb.exe",
            @"C:\Android\platform-tools\adb.exe",
        };
        foreach (var c in candidates)
            if (File.Exists(c)) return c;

        // Try PATH
        var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? [];
        foreach (var dir in pathDirs)
        {
            var adb = Path.Combine(dir, "adb.exe");
            if (File.Exists(adb)) return adb;
        }

        return "adb"; // Hope it's in PATH
    }
}

// ── Models ────────────────────────────────────────────────

public record AdbResult(int ExitCode, string StdOut, string StdErr)
{
    public bool IsOk => ExitCode == 0;
    public override string ToString() => IsOk ? StdOut.TrimEnd() : $"ERR({ExitCode}): {StdErr.TrimEnd()}";
}

public class AdbDevice
{
    public required string Serial { get; init; }
    public required string State { get; init; }
    public string? Model { get; init; }
    public string? Product { get; init; }
    public string? DeviceName { get; init; }
    public string? TransportId { get; init; }

    /// <summary>Friendly display name: Model or Serial</summary>
    public string DisplayName => Model?.Replace("_", " ") ?? Serial;

    public bool IsOnline => State == "device";

    public override string ToString() => $"{DisplayName} ({Serial}) [{State}]";
}
