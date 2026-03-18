using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
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
        diag.FileWriteUtc = fi.LastWriteTimeUtc; // expose session file mtime for kro sort
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

    // ExtractCwdFromVsCodeTitle / GetContextInfoForCwd(Ex) / ReadClotThoughtForCwd /
    // GetLastOutputLine / GetPromptDisplayInfo → AppBotEyePromptInfo.cs

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

            string? toolSummary = null; // fallback: tool_use abbreviated
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
                    // tool_use → abbreviated summary: "Bash: wkappbot ..." / "Read: file.cs"
                    if (type == "tool_use" && toolSummary == null && c.TryGetProperty("name", out var nameEl))
                    {
                        var toolName = nameEl.GetString() ?? "";
                        var brief = "";
                        if (c.TryGetProperty("input", out var inp) && inp.ValueKind == JsonValueKind.Object)
                        {
                            // Extract first useful field: command, file_path, pattern, etc.
                            foreach (var prop in inp.EnumerateObject())
                            {
                                if (prop.Value.ValueKind == JsonValueKind.String)
                                {
                                    var v = prop.Value.GetString() ?? "";
                                    if (v.Length > 60) v = v[..60] + "...";
                                    brief = v;
                                    break;
                                }
                            }
                        }
                        toolSummary = string.IsNullOrWhiteSpace(brief) ? $"🔧{toolName}" : $"🔧{toolName}: {brief}";
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
            // No text found — use tool_use summary as fallback
            if (toolSummary != null) { text = toolSummary; return true; }
            return false;
        }
        catch { return false; }
    }

    static readonly Regex _multiSpaceRx = new(@"\s{2,}", RegexOptions.Compiled);

    static string NormalizePrompt(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        // Collapse all whitespace (newlines, tabs, consecutive spaces) → single space
        var t = _multiSpaceRx.Replace(s, " ").Trim();
        if (t.Equals("NO_REPLY", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("telegram send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Equals("ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        // Filter eye tick profiling / diagnostic noise from "생각" display
        if (t.Contains("tick=") && t.Contains("ms(")) return "";
        if (t.StartsWith("[EYE_TICK]", StringComparison.Ordinal)) return "";
        if (t.StartsWith("[ACT]", StringComparison.Ordinal)) return "";
        if (t.Length > 200) t = t[..200] + "...";
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

            var files = Directory.GetFiles(logDir, "*.log", SearchOption.TopDirectoryOnly)
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
}
