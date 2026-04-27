using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    static string GetTabGrowthDumpPath()
    {
        var baseDir = Path.GetDirectoryName(Environment.ProcessPath ?? AppContext.BaseDirectory) ?? AppContext.BaseDirectory;
        var logDir = Path.Combine(baseDir, "wkappbot.hq", "logs");
        Directory.CreateDirectory(logDir);
        return Path.Combine(logDir, "cdp-tab-growth.jsonl");
    }

    static JsonArray CloneTargets(JsonArray? targets)
    {
        var arr = new JsonArray();
        if (targets == null) return arr;
        foreach (var t in targets)
        {
            if (t is JsonObject obj)
                arr.Add(JsonNode.Parse(obj.ToJsonString()));
        }
        return arr;
    }

    public async Task DumpTabGrowthAsync(
        int port,
        string reason,
        JsonArray? beforeTargets = null,
        string? key = null,
        string? expectedHost = null,
        string? createdTargetId = null)
    {
        try
        {
            JsonArray afterTargets;
            try
            {
                var afterJson = await _http.GetStringAsync($"http://localhost:{port}/json");
                afterTargets = JsonSerializer.Deserialize<JsonArray>(afterJson) ?? [];
            }
            catch
            {
                afterTargets = [];
            }

            static int CountPages(JsonArray arr)
            {
                int count = 0;
                foreach (var t in arr)
                {
                    if (string.Equals(t?["type"]?.GetValue<string>(), "page", StringComparison.OrdinalIgnoreCase))
                        count++;
                }
                return count;
            }

            static JsonArray Summarize(JsonArray arr)
            {
                var result = new JsonArray();
                foreach (var t in arr)
                {
                    if (!string.Equals(t?["type"]?.GetValue<string>(), "page", StringComparison.OrdinalIgnoreCase))
                        continue;
                    result.Add(new JsonObject
                    {
                        ["id"] = t?["id"]?.GetValue<string>() ?? "",
                        ["title"] = t?["title"]?.GetValue<string>() ?? "",
                        ["url"] = t?["url"]?.GetValue<string>() ?? ""
                    });
                }
                return result;
            }

            var before = CloneTargets(beforeTargets);
            var payload = new JsonObject
            {
                ["ts"] = DateTimeOffset.Now.ToString("O"),
                ["reason"] = reason,
                ["port"] = port,
                ["key"] = key ?? "",
                ["expectedHost"] = expectedHost ?? "",
                ["createdTargetId"] = createdTargetId ?? "",
                ["currentTargetId"] = TargetId ?? "",
                ["beforeCount"] = CountPages(before),
                ["afterCount"] = CountPages(afterTargets),
                ["before"] = Summarize(before),
                ["after"] = Summarize(afterTargets),
            };

            await File.AppendAllTextAsync(GetTabGrowthDumpPath(), payload.ToJsonString() + Environment.NewLine, Encoding.UTF8);
            Console.WriteLine($"[TAB:GROWTH] {reason} before={payload["beforeCount"]} after={payload["afterCount"]} key={key ?? "-"}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[TAB:GROWTH] dump failed: {ex.Message}");
        }
    }

    public static bool KeepPollutedTabsForDebug()
    {
        static bool IsTrue(string? v) =>
            string.Equals(v, "1", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(v, "true", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(v, "yes", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(v, "on", StringComparison.OrdinalIgnoreCase);

        return IsTrue(Environment.GetEnvironmentVariable("DEBUG_KEEP_POLLUTED_TABS")) ||
               IsTrue(Environment.GetEnvironmentVariable("WKAPPBOT_KEEP_POLLUTED_TABS"));
    }

    public async Task TryCloseTabByIdAsync(int port, string? targetId, string reason)
    {
        if (string.IsNullOrWhiteSpace(targetId)) return;
        if (KeepPollutedTabsForDebug())
        {
            Console.WriteLine($"[TAB:GROWTH] keeping polluted tab for debug: {targetId} ({reason})");
            return;
        }

        try
        {
            await _http.GetAsync($"http://localhost:{port}/json/close/{targetId}");
            Console.WriteLine($"[TAB:GROWTH] closed polluted tab: {targetId} ({reason})");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[TAB:GROWTH] close failed for {targetId}: {ex.Message}");
        }
    }
    // -- Static: CDP port detection from process ----------------─

    /// <summary>
    /// Detect CDP debugging port from a process's command line arguments.
    /// Parses --remote-debugging-port=NNNN from the process command line via WMI.
    /// Returns 0 if not found or not a Chromium-based process.
    /// </summary>
    /// <summary>Known CDP ports to probe (common defaults).</summary>
    private static readonly int[] KnownCdpPorts = [9222, 9223, 9224, 9225, 9229];

    /// <summary>
    /// Returns true if the given PID belongs to a known browser process.
    /// Prevents Electron apps (VS Code, Slack...) from being treated as browsers
    /// even when they expose a CDP endpoint (DevTools).
    /// </summary>
    public static bool IsBrowserProcess(int pid)
    {
        try
        {
            using var p = System.Diagnostics.Process.GetProcessById(pid);
            return _knownBrowserProcessNames.Contains(p.ProcessName);
        }
        catch { return false; }
    }

    private static readonly System.Collections.Generic.HashSet<string> _knownBrowserProcessNames =
        new(System.StringComparer.OrdinalIgnoreCase)
        { "chrome", "chromium", "msedge", "brave", "opera", "vivaldi", "whale", "arc" };

    public static int DetectCdpPort(int processId)
    {
        // Strategy: check known ports via netstat -> match to target PID
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-ano",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using var proc = AppBotPipe.Start(psi);
            if (proc == null) return 0;
            var output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit(3000);

            // Get all PIDs in the process tree (chrome spawns many child processes)
            var targetPids = GetProcessTreePids(processId);

            foreach (var port in KnownCdpPorts)
            {
                var needle = $":{port}";
                foreach (var rawLine in output.Split('\n'))
                {
                    var line = rawLine.Trim();
                    if (!line.Contains(needle) || !line.Contains("LISTENING")) continue;
                    var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 5) continue;
                    if (!parts[1].EndsWith(needle)) continue;
                    if (int.TryParse(parts[^1], out var listenPid) && targetPids.Contains(listenPid))
                        return port;
                }
            }

            // Broader scan: any LISTENING port owned by this process tree in high range
            foreach (var rawLine in output.Split('\n'))
            {
                var line = rawLine.Trim();
                if (!line.Contains("LISTENING")) continue;
                var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 5) continue;
                if (!int.TryParse(parts[^1], out var listenPid) || !targetPids.Contains(listenPid)) continue;
                var local = parts[1]; // e.g. "127.0.0.1:9222"
                var colonIdx = local.LastIndexOf(':');
                if (colonIdx < 0) continue;
                if (int.TryParse(local[(colonIdx + 1)..], out var foundPort) && foundPort >= 9222 && foundPort <= 9999)
                {
                    // Verify it's actually a CDP endpoint
                    try
                    {
                        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
                        var json = http.GetStringAsync($"http://localhost:{foundPort}/json/version").GetAwaiter().GetResult();
                        if (json.Contains("Browser") || json.Contains("webSocketDebuggerUrl"))
                            return foundPort;
                    }
                    catch { }
                }
            }
        }
        catch { }
        return 0;
    }

    private static HashSet<int> GetProcessTreePids(int rootPid)
    {
        var pids = new HashSet<int> { rootPid };
        try
        {
            // Simple: get all processes with same name as root
            var rootProc = Process.GetProcessById(rootPid);
            var name = rootProc.ProcessName;
            rootProc.Dispose();
            foreach (var p in Process.GetProcessesByName(name))
            {
                pids.Add(p.Id);
                p.Dispose();
            }
        }
        catch { }
        return pids;
    }

    /// <summary>
    /// Detect CDP port from a process name (e.g. "chrome", "msedge", "code").
    /// Scans all processes with that name and returns the first found port.
    /// </summary>
    public static int DetectCdpPortByName(string processName)
    {
        try
        {
            foreach (var p in Process.GetProcessesByName(processName))
            {
                try
                {
                    var port = DetectCdpPort(p.Id);
                    if (port > 0) return port;
                }
                finally { p.Dispose(); }
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// Check if a window class name indicates a Chromium-based web view.
    /// </summary>
    public static bool IsWebViewClass(string className)
    {
        return className is "Chrome_WidgetWin_1" or "Chrome_WidgetWin_0"
            or "Chromium_WidgetWin_1" or "Chromium_WidgetWin_0";
    }

    /// <summary>Tab info from CDP /json endpoint.</summary>
    public record TabInfo(string Id, string Title, string Url, string? WsUrl);

    /// <summary>List all page tabs via CDP /json.</summary>
    public async Task<List<TabInfo>> ListTabsAsync(int port = 9222)
    {
        var result = new List<TabInfo>();
        try
        {
            var json = await _http.GetStringAsync($"http://localhost:{port}/json");
            var targets = JsonSerializer.Deserialize<JsonArray>(json);
            if (targets == null) return result;
            foreach (var t in targets)
            {
                if (t?["type"]?.GetValue<string>() != "page") continue;
                result.Add(new TabInfo(
                    t?["id"]?.GetValue<string>() ?? "",
                    t?["title"]?.GetValue<string>() ?? "",
                    t?["url"]?.GetValue<string>() ?? "",
                    t?["webSocketDebuggerUrl"]?.GetValue<string>()));
            }
        }
        catch { }
        return result;
    }

    /// <summary>
    /// Find a tab by URL/title pattern (wildcard * supported).
    /// Returns null if no match found -- never opens a new tab.
    /// </summary>
    public async Task<TabInfo?> FindTabByPatternAsync(int port, string pattern)
    {
        var tabs = await ListTabsAsync(port);
        // If no wildcards, auto-wrap with * for substring match
        var matchPattern = pattern.Contains('*') ? pattern : $"*{pattern}*";
        foreach (var tab in tabs)
        {
            if (GlobMatch(tab.Title, matchPattern) || GlobMatch(tab.Url, matchPattern))
                return tab;
        }
        return null;
    }

    /// <summary>
    /// Find ALL tabs matching a URL/title pattern. For disambiguation when multiple match.
    /// </summary>
    public async Task<List<TabInfo>> FindAllTabsByPatternAsync(int port, string pattern)
    {
        var tabs = await ListTabsAsync(port);
        var matchPattern = pattern.Contains('*') ? pattern : $"*{pattern}*";
        return tabs.Where(tab => GlobMatch(tab.Title, matchPattern) || GlobMatch(tab.Url, matchPattern)).ToList();
    }

    /// <summary>
    /// Connect to a specific tab by pattern match (URL or title).
    /// If multiple tabs match, lists all candidates and returns false (disambiguation needed).
    /// Returns true if found and connected, false if no match or ambiguous.
    /// </summary>
    public async Task<bool> ConnectToTabAsync(int port, string pattern)
    {
        var allMatches = await FindAllTabsByPatternAsync(port, pattern);
        if (allMatches.Count == 0) return false;
        if (allMatches.Count == 1)
        {
            var tab = allMatches[0];
            if (tab.Id == TargetId) return true;
            return await SwitchToTargetAsync(tab.Id, port);
        }
        // Multiple matches -> show disambiguation list
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WEB] \"{pattern}\" -> {allMatches.Count}개 탭 매칭. 정확한 URL을 지정하세요:");
        Console.ResetColor();
        for (int i = 0; i < allMatches.Count; i++)
        {
            var t = allMatches[i];
            var urlShort = t.Url.Length > 60 ? t.Url[..60] : t.Url;
            var titleShort = t.Title.Length > 40 ? t.Title[..40] : t.Title;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  {i + 1}. ");
            Console.ResetColor();
            Console.Write($"\"{titleShort}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ({urlShort})");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"     wkappbot web read \"{urlShort}\"");
            Console.ResetColor();
        }
        return false;
    }

    static bool GlobMatch(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return false;
        // Simple wildcard: * matches any sequence
        var parts = pattern.Split('*');
        int idx = 0;
        for (int i = 0; i < parts.Length; i++)
        {
            if (string.IsNullOrEmpty(parts[i])) continue;
            var found = text.IndexOf(parts[i], idx, StringComparison.OrdinalIgnoreCase);
            if (found < 0) return false;
            if (i == 0 && !pattern.StartsWith("*") && found != 0) return false;
            idx = found + parts[i].Length;
        }
        if (!pattern.EndsWith("*") && idx != text.Length && parts.Length > 0 && !string.IsNullOrEmpty(parts[^1]))
            return false;
        return true;
    }

}
