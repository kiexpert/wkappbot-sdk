using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

/// <summary>
/// Tracks processes that steal focus after launch (logging + DB only).
/// No approval popup — focus stealing is logged but never blocked.
/// </summary>
internal static class FocusLaunchTracker
{
    private static readonly string DataPath =
        Path.Combine(Program.DataDir, "runtime", "focus_launch.json");
    private static readonly object _lock = new();
    private static HashSet<string>? _stealers;

    // Self-exe name — self-spawns (whisper-ring, screensaver, mcp worker, etc.) are always trusted.
    private static readonly string _selfExe =
        Path.GetFileName(Environment.ProcessPath ?? "wkappbot.exe").ToLowerInvariant();

    /// <summary>Wire AppBotPipe callbacks. Call once at startup.</summary>
    internal static void Wire()
    {
        // OnFocusApprovalRequired intentionally NOT wired — no confirmation dialog.
        AppBotPipe.OnPostLaunchFocusMonitor = HandlePostLaunch;
        AppBotPipe.OnIsKnownFocusStealer = IsRegistered;
    }

    internal static bool IsRegistered(string exeName)
    {
        var name = Normalize(exeName);
        if (name == _selfExe) return false; // self-spawn always trusted
        lock (_lock)
        {
            _stealers ??= Load();
            return _stealers.Contains(name);
        }
    }

    internal static void Register(string exeName)
    {
        var name = Normalize(exeName);
        if (name == _selfExe) return; // never register self
        lock (_lock)
        {
            _stealers ??= Load();
            if (!_stealers.Add(name)) return;
            Save();
        }
        Console.Error.WriteLine($"[FOCUS-TRACK] Registered focus-stealer: {name}");
    }

    /// <summary>
    /// prevFg=0 → force-register immediately (requiresFocus=true path).
    /// prevFg!=0 → monitor after 600ms, register if foreground changed.
    /// </summary>
    private static void HandlePostLaunch(string exePath, nint prevFg)
    {
        if (prevFg == 0)
        {
            Register(exePath);
            return;
        }
        _ = Task.Run(async () =>
        {
            await Task.Delay(600);
            var newFg = GetForegroundWindow();
            if (newFg != (nint)prevFg && newFg != IntPtr.Zero)
            {
                Console.Error.WriteLine($"[FOCUS-TRACK] Focus stolen by {Path.GetFileName(exePath)} — registering for future approval");
                Register(exePath);
            }
        });
    }

    private static string Normalize(string exeName) =>
        Path.GetFileName(exeName).ToLowerInvariant();

    private static HashSet<string> Load()
    {
        try
        {
            if (!File.Exists(DataPath)) return new(StringComparer.OrdinalIgnoreCase);
            var arr = JsonNode.Parse(File.ReadAllText(DataPath))?["stealers"] as JsonArray;
            return arr == null
                ? new(StringComparer.OrdinalIgnoreCase)
                : new(arr.Select(n => n?.GetValue<string>() ?? ""), StringComparer.OrdinalIgnoreCase);
        }
        catch { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DataPath)!);
            var node = new JsonObject
            {
                ["stealers"] = new JsonArray(
                    _stealers!.OrderBy(s => s).Select(s => (JsonNode)JsonValue.Create(s)!).ToArray())
            };
            File.WriteAllText(DataPath, node.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }
        catch { }
    }

    [DllImport("user32.dll")]
    static extern nint GetForegroundWindow();
}
