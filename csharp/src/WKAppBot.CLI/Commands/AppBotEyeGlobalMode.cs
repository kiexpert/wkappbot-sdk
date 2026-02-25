using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed class EyeParentCard
    {
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public string ParentTitle { get; set; } = "";
        public string LastTag { get; set; } = "";
        public string LastStatus { get; set; } = "";
        public DateTime LastTsUtc { get; set; }
    }

    sealed class PromptDiag
    {
        public long StatMs { get; set; }
        public long ReadMs { get; set; }
        public long ScanMs { get; set; }
        public long ParseMs { get; set; }
        public long NormMs { get; set; }
        public long CacheMs { get; set; }
        public string Source { get; set; } = "none";
    }

    static DateTime _lastTickActivityUtc = DateTime.MinValue;
    static DateTime _lastAiActivityUtc = DateTime.MinValue;
    static DateTime _lastAutoGogoUtc = DateTime.MinValue;
    static DateTime _lastKeepAwakeUtc = DateTime.MinValue;
    static string _lastPromptSource = "none";

    static string _lastPromptSessionFile = "";
    static int _lastPromptLineIndex = -1;
    static string _lastPromptPreviewCache = "";
    static long _lastPromptSessionLength = -1;
    static DateTime _lastPromptSessionWriteUtc = DateTime.MinValue;

    static string _lastEyeTickFile = "";
    static int _lastEyeTickLineIndex = -1;

    static string _lastPlanSessionFile = "";
    static int _lastPlanLineIndex = -1;
    static long _lastPlanSessionLength = -1;
    static DateTime _lastPlanSessionWriteUtc = DateTime.MinValue;
    static List<string> _lastPlanItemsCache = new();

    static EyeTick? _cachedLatestTick;
    static string _cachedPromptPreview = "";
    static List<EyeParentCard> _cachedCards = new();
    static DateTime _lastForceFullLoadUtc = DateTime.MinValue;
    static string _dirtyTickFile = "";
    static long _dirtyTickLength = -1;
    static DateTime _dirtyTickWriteUtc = DateTime.MinValue;
    static string _dirtyPromptFile = "";
    static long _dirtyPromptLength = -1;
    static DateTime _dirtyPromptWriteUtc = DateTime.MinValue;

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs)
    {
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(string.Empty);

        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
        if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;

        var keepAwakeSw = Stopwatch.StartNew();
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
            WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
            WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
        _lastKeepAwakeUtc = DateTime.UtcNow;

        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            var forceFull = ShouldForceFullLoad();
            var (tickDirty, promptDirty) = CheckGlobalDirtyFlags();
            if (!TryRunOneGlobalTick(host, timeoutMs: 3000, forceFull, tickDirty, promptDirty))
            {
                Console.WriteLine("[EYE] tick timeout (>3s) - self terminate");
                return 2;
            }

            if (keepAwakeSw.ElapsedMilliseconds >= 60_000)
            {
                keepAwakeSw.Restart();
                WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
                    WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
                    WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
                    WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
                _lastKeepAwakeUtc = DateTime.UtcNow;
                Console.WriteLine("[EYE] keep-awake tick");
            }

            Thread.Sleep(Math.Max(100, intervalMs));
        }

        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

        return 0;
    }

    static bool TryRunOneGlobalTick(AppBotEyeHost host, int timeoutMs, bool forceFull, bool tickDirty, bool promptDirty)
    {
        try
        {
            var t = Task.Run(() => RunOneGlobalTick(host, forceFull, tickDirty, promptDirty));
            return t.Wait(timeoutMs);
        }
        catch { return false; }
    }

    static void RunOneGlobalTick(AppBotEyeHost host, bool forceFull, bool tickDirty, bool promptDirty)
    {
        var swTotal = Stopwatch.StartNew();

        var swTick = Stopwatch.StartNew();
        long tickRead = 0, tickParse = 0;
        var latest = _cachedLatestTick;
        if (forceFull || tickDirty || latest == null)
        {
            latest = ReadLatestTick(out tickRead, out tickParse);
            _cachedLatestTick = latest;
            _cachedCards = ReadEyeCards(staleSeconds: 180);
        }
        swTick.Stop();

        var swPrompt = Stopwatch.StartNew();
        var promptDiag = new PromptDiag();
        var promptPreview = _cachedPromptPreview;
        if (forceFull || promptDirty || string.IsNullOrWhiteSpace(promptPreview))
        {
            promptPreview = ReadLatestOpenClawPromptPreview(promptDiag);
            _cachedPromptPreview = promptPreview;
        }
        else
        {
            promptDiag.Source = "sessions-cache";
            promptDiag.CacheMs = 1;
        }
        swPrompt.Stop();

        var swSchedule = Stopwatch.StartNew();
        // placeholder for future schedule diagnostics
        swSchedule.Stop();

        if (latest != null)
        {
            _lastTickActivityUtc = DateTime.UtcNow;
            if ((latest.Status ?? "").Contains("step:", StringComparison.OrdinalIgnoreCase) ||
                (latest.Status ?? "").Contains("done", StringComparison.OrdinalIgnoreCase))
                _lastAiActivityUtc = DateTime.UtcNow;
        }
        if (!string.IsNullOrWhiteSpace(promptPreview))
            _lastAiActivityUtc = DateTime.UtcNow;

        _lastPromptSource = promptDiag.Source;

        var cards = _cachedCards;

        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(BuildEyeSummary(cards, latest, promptPreview));

        swTotal.Stop();

        var mode = forceFull ? "full-1s" : (tickDirty || promptDirty ? "dirty" : "idle");
        Console.WriteLine($"[EYE_TICK] mode={mode} tick={swTick.ElapsedMilliseconds}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                          $"prompt={swPrompt.ElapsedMilliseconds}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) " +
                          $"schedule={swSchedule.ElapsedMilliseconds}ms total={swTotal.ElapsedMilliseconds}ms");
        Console.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");

        var execIdle = (DateTime.UtcNow - _lastTickActivityUtc).TotalSeconds;
        var aiIdle = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds;
        var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (DateTime.UtcNow - _lastAutoGogoUtc).TotalSeconds;
        var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
        Console.WriteLine($"[EYE_GUARD] armed={(armed ? 1 : 0)} execIdle={execIdle:F0}s aiIdle={aiIdle:F0}s cooldown={cooldown:F0}s");

        var latestAge = -1.0;
        if (latest != null && DateTime.TryParse(latest.Ts, out var ts))
            latestAge = (DateTime.UtcNow - ts.ToUniversalTime()).TotalSeconds;
        var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (DateTime.UtcNow - _lastKeepAwakeUtc).TotalSeconds;
        Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(keepAge < 0 ? "n/a" : keepAge.ToString("F0") + "s")} promptSource={_lastPromptSource} latestTickAge={(latestAge < 0 ? "n/a" : latestAge.ToString("F0") + "s")}");
    }

    static bool ShouldForceFullLoad()
    {
        var now = DateTime.UtcNow;
        if (_lastForceFullLoadUtc == DateTime.MinValue || (now - _lastForceFullLoadUtc).TotalMilliseconds >= 1000)
        {
            _lastForceFullLoadUtc = now;
            return true;
        }
        return false;
    }

    static (bool tickDirty, bool promptDirty) CheckGlobalDirtyFlags()
    {
        bool tickDirty = false;
        bool promptDirty = false;

        try
        {
            var tickPath = EyeTicksPath;
            if (File.Exists(tickPath))
            {
                var fi = new FileInfo(tickPath);
                if (_dirtyTickFile != tickPath || _dirtyTickLength != fi.Length || _dirtyTickWriteUtc != fi.LastWriteTimeUtc)
                {
                    tickDirty = true;
                    _dirtyTickFile = tickPath;
                    _dirtyTickLength = fi.Length;
                    _dirtyTickWriteUtc = fi.LastWriteTimeUtc;
                }
            }
        }
        catch { }

        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (Directory.Exists(sessionsDir))
            {
                var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                    .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                    .FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(latestFile) && File.Exists(latestFile))
                {
                    var fi = new FileInfo(latestFile);
                    if (_dirtyPromptFile != latestFile || _dirtyPromptLength != fi.Length || _dirtyPromptWriteUtc != fi.LastWriteTimeUtc)
                    {
                        promptDirty = true;
                        _dirtyPromptFile = latestFile;
                        _dirtyPromptLength = fi.Length;
                        _dirtyPromptWriteUtc = fi.LastWriteTimeUtc;
                    }
                }
            }
        }
        catch { }

        return (tickDirty, promptDirty);
    }

    static int EyeTickCommand(string[] args)
    {
        try
        {
            if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
            if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;
            var swTotal = Stopwatch.StartNew();
            long tickRead = 0, tickParse = 0;
            var latest = ReadLatestTick(out tickRead, out tickParse);
            var promptDiag = new PromptDiag();
            var prompt = ReadLatestOpenClawPromptPreview(promptDiag);
            var cards = ReadEyeCards(staleSeconds: 180);
            _lastPromptSource = promptDiag.Source;

            Console.WriteLine("[EYE] one-shot tick");
            Console.WriteLine($"[EYE_TICK] tick={tickRead + tickParse}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                              $"prompt={promptDiag.StatMs + promptDiag.ReadMs + promptDiag.ScanMs + promptDiag.ParseMs + promptDiag.NormMs + promptDiag.CacheMs}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) schedule=0ms total={swTotal.ElapsedMilliseconds}ms");
            Console.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");
            Console.WriteLine($"[EYE_TICK] cards={cards.Count} promptSource={promptDiag.Source}");
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.WriteLine($"[EYE_TICK] recent={prompt}");

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    Console.WriteLine($"[EYE_PLAN] —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    Console.WriteLine($"[EYE_PLAN] —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            var execIdle = (DateTime.UtcNow - _lastTickActivityUtc).TotalSeconds;
            var aiIdle = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds;
            var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (DateTime.UtcNow - _lastAutoGogoUtc).TotalSeconds;
            var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
            Console.WriteLine($"[EYE_GUARD] armed={(armed ? 1 : 0)} execIdle={execIdle:F0}s aiIdle={aiIdle:F0}s cooldown={cooldown:F0}s");

            var latestAge = -1.0;
            if (latest != null && DateTime.TryParse(latest.Ts, out var ts))
                latestAge = (DateTime.UtcNow - ts.ToUniversalTime()).TotalSeconds;
            var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (DateTime.UtcNow - _lastKeepAwakeUtc).TotalSeconds;
            Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(keepAge < 0 ? "n/a" : keepAge.ToString("F0") + "s")} promptSource={_lastPromptSource} latestTickAge={(latestAge < 0 ? "n/a" : latestAge.ToString("F0") + "s")}");

            // ── Slack inbox check + forward to Claude prompt ──
            EyeTickForwardSlackInbox();

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] error={ex.Message}");
            return 1;
        }
    }

    /// <summary>
    /// EyeTick one-shot: check Slack inbox and forward messages to Claude prompt.
    /// Reads slack_inbox.jsonl, types each message into Claude prompt, then clears inbox.
    /// If prompt not found, messages stay in inbox for next tick.
    /// </summary>
    static void EyeTickForwardSlackInbox()
    {
        try
        {
            if (!File.Exists(SlackInboxFile))
            {
                Console.WriteLine("[EYE_TICK] slack_inbox=empty");
                return;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(SlackInboxFile);
            }
            catch
            {
                Console.WriteLine("[EYE_TICK] slack_inbox=read_error");
                return;
            }

            // Filter empty lines
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            if (lines.Length == 0)
            {
                try { File.Delete(SlackInboxFile); } catch { }
                Console.WriteLine("[EYE_TICK] slack_inbox=empty");
                return;
            }

            Console.WriteLine($"[EYE_TICK] slack_inbox={lines.Length} message(s) — forwarding to Claude prompt...");

            // Find Claude prompt
            using var helper = new ClaudePromptHelper();
            var promptInfo = helper.FindPrompt();
            if (promptInfo == null)
            {
                Console.WriteLine("[EYE_TICK] WARNING: Claude prompt not found — inbox preserved for next tick");
                return;  // Don't delete inbox — try again next tick
            }

            int forwarded = 0;
            foreach (var line in lines)
            {
                try
                {
                    var msg = JsonSerializer.Deserialize<JsonNode>(line);
                    var user = msg?["user"]?.GetValue<string>() ?? "unknown";
                    var text = msg?["text"]?.GetValue<string>() ?? "";
                    var channel = msg?["channel"]?.GetValue<string>() ?? "";
                    var msgTs = msg?["ts"]?.GetValue<string>() ?? "";

                    if (string.IsNullOrWhiteSpace(text)) continue;

                    // Clean @mention tags
                    var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (string.IsNullOrWhiteSpace(cleanText)) continue;

                    var replyCmd = $"wkappbot slack reply \"response\" --channel {channel} --thread {msgTs}";
                    var promptText = $"{cleanText}\n\n(Slack @{user} — reply: {replyCmd})";

                    // Re-find prompt each time (window may shift)
                    var fresh = helper.FindPrompt();
                    if (fresh == null)
                    {
                        Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping forward");
                        break;
                    }

                    var ok = helper.TypeAndSubmit(fresh, promptText);
                    if (ok)
                    {
                        forwarded++;
                        Console.WriteLine($"[EYE_TICK] >> Slack @{user}: {cleanText}");
                        if (lines.Length > 1)
                            Thread.Sleep(2000);  // Pause between messages for Claude to process
                    }
                    else
                    {
                        Console.WriteLine($"[EYE_TICK] WARNING: TypeAndSubmit failed for @{user}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EYE_TICK] inbox parse error: {ex.Message}");
                }
            }

            // Clear inbox after processing
            if (forwarded > 0)
            {
                try { File.Delete(SlackInboxFile); } catch { }
                Console.WriteLine($"[EYE_TICK] slack_inbox forwarded={forwarded}/{lines.Length} — inbox cleared");
            }
            else
            {
                Console.WriteLine("[EYE_TICK] slack_inbox forwarded=0 — inbox preserved");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] slack forward error: {ex.Message}");
        }
    }

    static string BuildEyeSummary(List<EyeParentCard> cards, EyeTick? latest, string prompt)
    {
        var sb = new StringBuilder();

        var (a11y, act, fallback) = ReadLatestActionTriplet();
        if (!string.IsNullOrWhiteSpace(a11y)) sb.AppendLine($"엑빌: {a11y}");
        if (!string.IsNullOrWhiteSpace(act)) sb.AppendLine($"액션: {act}");
        if (!string.IsNullOrWhiteSpace(fallback)) sb.AppendLine($"폴백: {fallback}");

        if (!string.IsNullOrWhiteSpace(prompt))
            sb.AppendLine($"최근 생각: {prompt}");

        if (latest != null)
        {
            var (progress, done, next, block) = BuildKroStatus3(latest);
            sb.AppendLine($"크로 진행: {progress}");
            sb.AppendLine($"크로 완료: {done}");
            sb.AppendLine($"크로 예정: {next}");

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    sb.AppendLine($"- —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    sb.AppendLine($"- —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            if (!string.IsNullOrWhiteSpace(block))
                sb.AppendLine($"크로 이슈: {block}");
            sb.AppendLine("----");
        }

        if (cards.Count == 0)
            sb.AppendLine("(active parent cards: 0)");
        else
        {
            foreach (var c in cards.OrderByDescending(x => x.LastTsUtc).Take(6))
            {
                var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                var head = string.IsNullOrWhiteSpace(c.ParentTitle) ? $"{c.ParentName}:{c.ParentPid}" : $"{c.ParentTitle} ({c.ParentName}:{c.ParentPid})";
                sb.AppendLine($"[{head}] {c.LastTag} {c.LastStatus} -{age}s");
            }
        }

        return sb.ToString().TrimEnd();
    }

    static (int x, int y) GetRightmostMonitorAnchor(int width, int height)
    {
        try
        {
            var screens = Screen.AllScreens;
            if (screens != null && screens.Length > 0)
            {
                var rightMost = screens.OrderByDescending(s => s.Bounds.Right).First();
                var x = rightMost.Bounds.Right - width - 10;
                var y = rightMost.Bounds.Top + 10;
                return (x, y);
            }
        }
        catch { }

        return (1530, 110);
    }

    static EyeTick? ReadLatestTick(out long readMs, out long parseMs)
    {
        readMs = 0; parseMs = 0;
        var path = EyeTicksPath;
        if (!File.Exists(path)) return null;

        var swRead = Stopwatch.StartNew();
        var lines = ReadTailLinesShared(path, 64 * 1024);
        swRead.Stop();
        readMs = swRead.ElapsedMilliseconds;

        var swParse = Stopwatch.StartNew();
        var start = _lastEyeTickFile == path ? Math.Max(0, _lastEyeTickLineIndex - 1) : 0;
        for (int i = lines.Length - 1; i >= start; i--)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(lines[i]);
                if (t != null)
                {
                    _lastEyeTickFile = path;
                    _lastEyeTickLineIndex = i;
                    swParse.Stop();
                    parseMs = swParse.ElapsedMilliseconds;
                    return t;
                }
            }
            catch { }
        }
        swParse.Stop();
        parseMs = swParse.ElapsedMilliseconds;
        return null;
    }

    static string ReadLatestOpenClawPromptPreview(PromptDiag diag)
    {
        var sw = Stopwatch.StartNew();
        var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
        if (!Directory.Exists(sessionsDir)) return "";

        var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
            .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
            .FirstOrDefault();
        diag.StatMs = sw.ElapsedMilliseconds;
        if (string.IsNullOrWhiteSpace(latestFile)) return "";

        var fi = new FileInfo(latestFile);
        if (latestFile == _lastPromptSessionFile && fi.Length == _lastPromptSessionLength && fi.LastWriteTimeUtc == _lastPromptSessionWriteUtc)
        {
            diag.CacheMs = 1;
            diag.Source = "sessions-cache";
            return _lastPromptPreviewCache;
        }

        string selected = "";
        int selectedLine = -1;
        var windows = new[] { 64 * 1024, 128 * 1024, 256 * 1024 };

        foreach (var tailBytes in windows)
        {
            sw.Restart();
            var lines = ReadTailLinesShared(latestFile, tailBytes);
            diag.ReadMs += sw.ElapsedMilliseconds;

            sw.Restart();
            string bestAssistant = "";
            string bestUser = "";
            int bestLine = -1;
            var start = latestFile == _lastPromptSessionFile ? Math.Max(0, _lastPromptLineIndex - 2) : 0;
            if (start >= lines.Length) start = 0;

            for (int i = lines.Length - 1; i >= start; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("\"role\"", StringComparison.OrdinalIgnoreCase)) continue;
                if (!line.Contains("assistant", StringComparison.OrdinalIgnoreCase) && !line.Contains("user", StringComparison.OrdinalIgnoreCase)) continue;

                var parseStart = Stopwatch.StartNew();
                if (TryExtractRoleAndText(line, out var role, out var text))
                {
                    parseStart.Stop();
                    diag.ParseMs += parseStart.ElapsedMilliseconds;

                    var nsw = Stopwatch.StartNew();
                    text = NormalizePrompt(text);
                    nsw.Stop();
                    diag.NormMs += nsw.ElapsedMilliseconds;
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                    {
                        bestAssistant = text;
                        bestLine = i;
                        break;
                    }
                    if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                    {
                        bestUser = text;
                        if (bestLine < 0) bestLine = i;
                    }
                }
                else
                {
                    parseStart.Stop();
                    diag.ParseMs += parseStart.ElapsedMilliseconds;
                }
            }
            diag.ScanMs += sw.ElapsedMilliseconds;

            selected = !string.IsNullOrWhiteSpace(bestAssistant) ? bestAssistant : bestUser;
            selectedLine = bestLine;
            if (!string.IsNullOrWhiteSpace(selected)) break;
        }

        sw.Restart();
        _lastPromptSessionFile = latestFile;
        _lastPromptLineIndex = selectedLine;
        _lastPromptPreviewCache = selected;
        _lastPromptSessionLength = fi.Length;
        _lastPromptSessionWriteUtc = fi.LastWriteTimeUtc;
        diag.CacheMs = sw.ElapsedMilliseconds;
        diag.Source = string.IsNullOrWhiteSpace(selected) ? "none" : "sessions";
        return selected;
    }

    static bool TryExtractRoleAndText(string jsonLine, out string role, out string text)
    {
        role = "";
        text = "";
        try
        {
            using var doc = JsonDocument.Parse(jsonLine);
            var root = doc.RootElement;
            if (!root.TryGetProperty("message", out var msg)) return false;
            if (!msg.TryGetProperty("role", out var roleEl)) return false;
            role = roleEl.GetString() ?? "";
            if (!msg.TryGetProperty("content", out var content) || content.ValueKind != JsonValueKind.Array) return false;

            foreach (var c in content.EnumerateArray())
            {
                if (c.TryGetProperty("type", out var typeEl))
                {
                    var type = (typeEl.GetString() ?? "").ToLowerInvariant();
                    if (type is "text" or "output_text" or "input_text" or "summary_text")
                    {
                        if (c.TryGetProperty("text", out var txtEl))
                        {
                            text = txtEl.GetString() ?? "";
                            if (!string.IsNullOrWhiteSpace(text)) return true;
                        }
                    }
                }

                if (c.TryGetProperty("text", out var txtAny))
                {
                    text = txtAny.GetString() ?? "";
                    if (!string.IsNullOrWhiteSpace(text)) return true;
                }

                if (c.TryGetProperty("summary", out var summary) && summary.ValueKind == JsonValueKind.Array)
                {
                    foreach (var s in summary.EnumerateArray())
                    {
                        if (s.TryGetProperty("text", out var st))
                        {
                            text = st.GetString() ?? "";
                            if (!string.IsNullOrWhiteSpace(text)) return true;
                        }
                    }
                }
            }
            return false;
        }
        catch { return false; }
    }

    static string NormalizePrompt(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        var t = s.Replace("\r", " ").Replace("\n", " ").Trim();
        if (t.Equals("NO_REPLY", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("telegram send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Equals("ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Length > 160) t = t[..160] + "...";
        return t;
    }

    static string CompressPlanTitle(string s, int maxLen = 34)
    {
        var t = s.Replace("\r", " ").Replace("\n", " ").Trim();
        if (t.Length > maxLen) t = t[..maxLen] + "...";
        return t;
    }

    static List<string> ExtractPlanOutlineItems(string text, int maxItems)
    {
        var items = new List<string>();
        if (string.IsNullOrWhiteSpace(text)) return items;

        var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

        bool inBlock = false;
        for (int i = 0; i < lines.Count; i++)
        {
            var ln = lines[i];
            if (ln.Equals("[KRO_PLAN_BEGIN]", StringComparison.OrdinalIgnoreCase)) { inBlock = true; continue; }
            if (ln.Equals("[KRO_PLAN_END]", StringComparison.OrdinalIgnoreCase)) break;

            if (ln.StartsWith("PLAN:", StringComparison.OrdinalIgnoreCase))
            {
                var head = ln[5..].Trim();
                if (!string.IsNullOrWhiteSpace(head))
                {
                    // support single-line style: PLAN: title - item1 - item2 ...
                    var parts = head.Split(" - ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (parts.Length > 0)
                    {
                        items.Add(CompressPlanTitle(parts[0]));
                        for (int p = 1; p < parts.Length && items.Count < maxItems; p++)
                            items.Add(CompressPlanTitle(parts[p]));
                    }
                    else
                    {
                        items.Add(CompressPlanTitle(head));
                    }
                }
                inBlock = true;
                continue;
            }

            if (inBlock && (ln.StartsWith("-") || ln.StartsWith("•") || ln.StartsWith("1)") || ln.StartsWith("2)") || ln.StartsWith("3)")))
            {
                var body = ln.TrimStart('-', '•', ' ').Trim();
                if (!string.IsNullOrWhiteSpace(body)) items.Add(CompressPlanTitle(body));
                if (items.Count >= maxItems) break;
            }
        }

        return items.Take(maxItems).ToList();
    }

    static List<string> ExtractRecentPlanItems(int maxItems = 3)
    {
        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir)) return new List<string>();

            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(latestFile)) return new List<string>();

            var fi = new FileInfo(latestFile);
            if (latestFile == _lastPlanSessionFile && fi.Length == _lastPlanSessionLength && fi.LastWriteTimeUtc == _lastPlanSessionWriteUtc)
                return _lastPlanItemsCache.Take(maxItems).ToList();

            var lines = ReadTailLinesShared(latestFile, 256 * 1024);
            var start = latestFile == _lastPlanSessionFile ? Math.Max(0, _lastPlanLineIndex - 2) : 0;
            if (start >= lines.Length) start = 0;

            var outItems = new List<string>();
            int foundLine = -1;
            for (int i = lines.Length - 1; i >= start; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("assistant", StringComparison.OrdinalIgnoreCase)) continue;
                if (!TryExtractRoleAndText(line, out var role, out var text)) continue;
                if (!string.Equals(role, "assistant", StringComparison.OrdinalIgnoreCase)) continue;

                // 1) preferred: explicit plan-format outline
                var outline = ExtractPlanOutlineItems(text, maxItems);
                if (outline.Count > 0)
                {
                    // newest valid plan block wins: do not mix with older plans
                    outItems.Clear();
                    foreach (var it in outline.Take(maxItems))
                        outItems.Add(it);
                    foundLine = i;
                    break;
                }

                // 2) strict mode: no heuristic promotion
                // only explicit plan-format outputs are allowed for EYE_PLAN
                continue;
            }

            _lastPlanSessionFile = latestFile;
            _lastPlanLineIndex = foundLine;
            _lastPlanSessionLength = fi.Length;
            _lastPlanSessionWriteUtc = fi.LastWriteTimeUtc;
            _lastPlanItemsCache = outItems;

            return outItems.Take(maxItems).ToList();
        }
        catch
        {
            return new List<string>();
        }
    }

    static (string a11y, string act, string fallback) ReadLatestActionTriplet()
    {
        try
        {
            var logDir = Path.Combine(DataDir, "logs");
            if (!Directory.Exists(logDir)) return ("", "", "");

            var files = Directory.GetFiles(logDir, "*.txt", SearchOption.TopDirectoryOnly)
                .Where(p => !Path.GetFileName(p).Contains(".eye."))
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .Take(3)
                .ToList();

            string a11y = "", act = "", fallback = "";
            foreach (var f in files)
            {
                var lines = ReadTailLinesShared(f, 32 * 1024);
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var ln = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(a11y) && ln.StartsWith("[A11Y]")) a11y = ln[6..].Trim();
                    if (string.IsNullOrWhiteSpace(act) && ln.StartsWith("[ACT]")) act = ln[5..].Trim();
                    if (string.IsNullOrWhiteSpace(fallback) && ln.StartsWith("[FALLBACK]")) fallback = ln[10..].Trim();
                    if (!string.IsNullOrWhiteSpace(a11y) && !string.IsNullOrWhiteSpace(act) && !string.IsNullOrWhiteSpace(fallback))
                        break;
                }
                if (!string.IsNullOrWhiteSpace(a11y) && !string.IsNullOrWhiteSpace(act) && !string.IsNullOrWhiteSpace(fallback))
                    break;
            }

            a11y = CompressPlanTitle(a11y, 46);
            act = CompressPlanTitle(act, 46);
            fallback = CompressPlanTitle(fallback, 46);
            return (a11y, act, fallback);
        }
        catch
        {
            return ("", "", "");
        }
    }

    static string[] ReadTailLinesShared(string path, int tailBytes)
    {
        try
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var len = fs.Length;
            var start = Math.Max(0, len - tailBytes);
            fs.Seek(start, SeekOrigin.Begin);
            using var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            var txt = sr.ReadToEnd();
            if (start > 0)
            {
                var idx = txt.IndexOf('\n');
                if (idx >= 0 && idx + 1 < txt.Length)
                    txt = txt[(idx + 1)..];
            }
            return txt.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
        catch { return Array.Empty<string>(); }
    }

    static (string progress, string done, string next, string block) BuildKroStatus3(EyeTick t)
    {
        var report = $"{t.Command}/{t.Tag}";
        var status = (t.Status ?? "").ToLowerInvariant();
        var aiRecent = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds <= 45;

        if (status == "start")
            return (report, "작업 시작", "응답 처리 중", "");

        if (status.StartsWith("step:"))
        {
            if (aiRecent)
                return (report, "최근 대화 수신", "응답 처리 중", "");
            return (report, "중간 단계", "상태 갱신 대기", "");
        }

        if (status.StartsWith("end:"))
        {
            var codeText = status[4..].Trim();
            if (int.TryParse(codeText, out var code) && code == 0)
            {
                if (aiRecent)
                    return ("응답 처리 중", "최근 대화 수신", "후속 입력 대기", "");
                return (report, "작업 완료", "다음 작업 대기", "");
            }
            return (report, "오류 종료", "에러 분석/폴백", $"종료코드 {codeText}");
        }

        if (aiRecent)
            return (report, "최근 대화 수신", "응답 처리 중", "");

        return (report, "상태 대기", "상태 갱신 대기", "");
    }

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 25)
    {
        var cards = new Dictionary<int, EyeParentCard>();
        var path = EyeTicksPath;
        if (!File.Exists(path)) return cards.Values.ToList();

        var lines = ReadTailLinesShared(path, 64 * 1024);
        var now = DateTime.UtcNow;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(line);
                if (t == null) continue;
                if (!DateTime.TryParse(t.Ts, out var tsLocal)) continue;
                var tsUtc = tsLocal.ToUniversalTime();
                var ppid = t.HostPid > 0 ? t.HostPid : (t.ParentPid > 0 ? t.ParentPid : t.Pid);
                var pname = !string.IsNullOrWhiteSpace(t.HostName) ? t.HostName : (string.IsNullOrWhiteSpace(t.ParentName) ? "unknown" : t.ParentName);
                var ptitle = t.HostTitle ?? "";

                if (!cards.TryGetValue(ppid, out var c) || tsUtc > c.LastTsUtc)
                {
                    cards[ppid] = new EyeParentCard
                    {
                        ParentPid = ppid,
                        ParentName = pname,
                        ParentTitle = ptitle,
                        LastTag = t.Tag,
                        LastStatus = t.Status,
                        LastTsUtc = tsUtc
                    };
                }
            }
            catch { }
        }

        return cards.Values.Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds).ToList();
    }
}
