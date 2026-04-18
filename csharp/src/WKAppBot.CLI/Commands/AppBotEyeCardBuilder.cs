using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static string BuildEyeSummary(List<EyeParentCard> cards, EyeTick? latest, string prompt, DateTime kroFileWriteUtc = default)
    {
        var sb = new StringBuilder();

        // -- Action triplet (always on top -- real-time accessibility probe) --
        var (a11y, act, fallback) = ReadLatestActionTriplet();
        if (!string.IsNullOrWhiteSpace(a11y)) sb.AppendLine($"엑빌: {a11y}");
        if (!string.IsNullOrWhiteSpace(act)) sb.AppendLine($"액션: {act}");
        if (!string.IsNullOrWhiteSpace(fallback)) sb.AppendLine($"폴백: {fallback}");

        // -- Build KRO section text (rendered inline with cards by recency) --
        // KRO sort time = session file mtime (= when kro last wrote to its session JSONL)
        // CardCacheGetTimestamp had a bug: content varied due to whitespace -> always UtcNow
        // Now uses file mtime directly -- more accurate indicator of kro activity
        string kroBlock = "";
        DateTime kroTsUtc = DateTime.MinValue;
        if (latest != null)
        {
            // Use session file mtime -- reflects actual kro write activity
            kroTsUtc = kroFileWriteUtc != default ? kroFileWriteUtc
                : CardCacheGetTimestamp("kro", prompt ?? ""); // fallback if mtime not passed

            var kroSb = new StringBuilder();
            // KRO's home is ~/.openclaw/, not the CLI command's CWD
            var openClawDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw");
            var kroCwd = AbbreviateCwd(openClawDir);
            var (progress, done, next, block) = BuildKroStatus3(latest);

            // Unified card format: match the clot cards (작업/상태/생각) so downstream
            // parsers and operators see a single consistent schema. The KRO-specific
            // fields (완료/예정/이슈) fold into 작업/상태 so the card stays compact.
            kroSb.AppendLine($"크로[{kroCwd}]");
            kroSb.AppendLine($"작업: {progress}");
            kroSb.AppendLine($"상태: {done}" + (string.IsNullOrWhiteSpace(next) ? "" : $" → {next}"));
            if (!string.IsNullOrWhiteSpace(block))
                kroSb.AppendLine($"이슈: {block}");

            // Latest assistant/user text from OpenClaw session
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                var kroThought = prompt.Length > 120 ? prompt[..120] + "..." : prompt;
                kroSb.AppendLine($"생각: {kroThought}");
            }

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    kroSb.AppendLine($"- --:-- {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    kroSb.AppendLine($"- --:-- 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            kroBlock = kroSb.ToString().TrimEnd();
        }

        // -- Apply CardCache to AI cards -- sort by MAX(content change, tick activity) --
        // CardCache tracks when card text (tag|status) actually changed.
        // HealthCheck sets LastTsUtc from eye_ticks.jsonl (real JSONL activity).
        // Using max of both ensures active sessions stay on top even when card text is unchanged.
        foreach (var c in cards)
        {
            var clotContent = $"{c.LastTag}|{c.LastStatus}";
            var cwdAbbrev = AbbreviateCwd(c.Cwd);
            var clotKey = !string.IsNullOrEmpty(cwdAbbrev) ? $"clot_{cwdAbbrev}" : $"clot_pid{c.ParentPid}";
            var tickTs = c.LastTsUtc; // preserve tick activity time before cache overwrites it
            var cachedTs = CardCacheGetTimestamp(clotKey, clotContent);
            if (cachedTs == DateTime.MinValue)
            {
                cachedTs = c.LastTsUtc;
                _cardCache[clotKey] = (clotContent, cachedTs);
                CardCacheSave(clotKey, clotContent, cachedTs);
            }
            c.LastTsUtc = cachedTs > tickTs ? cachedTs : tickTs;
        }

        // -- Sort ALL cards (including KRO) by recency -- newest on top --
        // KRO = Claude Code itself (~/.openclaw/), cards = individual CLI commands (each with own CWD)
        // They share the same Claude Desktop host but are separate entities -- no dedup needed
        // Hide KRO if its tick is older than 24h (stale -- KRO not active)
        bool kroStale = kroTsUtc != DateTime.MinValue && (DateTime.UtcNow - kroTsUtc).TotalHours > 24;
        bool hasKro = !string.IsNullOrWhiteSpace(kroBlock) && !kroStale;
        var sortedCards = cards.OrderByDescending(x => x.LastTsUtc).Take(6).ToList();
        bool kroRendered = !hasKro;  // if no KRO or stale, mark as already rendered

        if (sortedCards.Count == 0 && !hasKro)
        {
            sb.AppendLine("(active cards: 0)");
        }
        else
        {
            // Interleave: render each item in time order (newest first)
            int cardIdx = 0;
            while (cardIdx < sortedCards.Count || !kroRendered)
            {
                bool renderKroNow = false;
                if (!kroRendered)
                {
                    if (cardIdx >= sortedCards.Count)
                        renderKroNow = true;
                    else if (kroTsUtc > sortedCards[cardIdx].LastTsUtc)
                        renderKroNow = true;
                }

                if (renderKroNow)
                {
                    sb.AppendLine(kroBlock);
                    sb.AppendLine("----");
                    kroRendered = true;
                }
                else if (cardIdx < sortedCards.Count)
                {
                    var c = sortedCards[cardIdx++];
                    var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                    var cwdTag = AbbreviateCwd(c.Cwd);
                    var ageText = age < 60 ? $"{age}초 전" : age < 3600 ? $"{age / 60}분 전" : $"{age / 3600}시간 전";

                    // Header: display name only (no icon -- icon is visual noise in Slack)
                    var header = string.IsNullOrWhiteSpace(cwdTag)
                        ? (string.IsNullOrWhiteSpace(c.ParentTitle) ? $"{c.ParentName}:{c.ParentPid}" : c.ParentTitle)
                        : FormatSlackDisplayName(c.HostType, cwdTag);
                    sb.AppendLine(header);
                    // Context % per card: prefer session-registry JSONL (exact file), fall back to CWD scan.
                    var ctxTag = "";
                    var (cardCtx, jsonlAge, jsonlPath, jsonlFileSize) = !string.IsNullOrEmpty(c.SessionJsonl) && File.Exists(c.SessionJsonl)
                        ? GetContextInfoForJsonl(c.SessionJsonl)
                        : GetContextInfoForCwdEx(c.Cwd, c.HostType);
                    if (cardCtx >= 0)
                    {
                        var sizeMB = jsonlFileSize / (1024.0 * 1024.0);
                        var sizeTag = sizeMB >= 1.0 ? $"{sizeMB:F0}MB" : $"{jsonlFileSize / 1024}KB";
                        ctxTag = $" ctx={cardCtx}% ({sizeTag})";
                    }
                    // Session JSONL filename -- helps operators find the exact log.
                    // Truncate to last 44 chars so long UUID-style names stay on one line.
                    if (!string.IsNullOrEmpty(jsonlPath))
                    {
                        var fname = Path.GetFileName(jsonlPath);
                        if (fname.Length > 44) fname = "..." + fname[^41..];
                        sb.AppendLine($"세션: {fname}");
                    }
                    // For prompt-discovered cards, use JSONL age instead of tick age
                    if (c.LastTag == "prompt-discovered" && jsonlAge != null)
                    {
                        var jAge = (int)jsonlAge.Value.TotalSeconds;
                        var jAgeText = jAge < 60 ? $"{jAge}초 전" : jAge < 3600 ? $"{jAge / 60}분 전" : $"{jAge / 3600}시간 전";
                        sb.AppendLine($"상태: 대기중 ({jAgeText}){ctxTag}");
                    }
                    else if (age > 30)
                    {
                        sb.AppendLine($"상태: 대기중 ({ageText}){ctxTag}");
                    }
                    else
                    {
                        sb.AppendLine($"작업: {c.LastTag}");
                        sb.AppendLine($"상태: {c.LastStatus} ({ageText}){ctxTag}");
                    }
                    // Latest user/assistant text: session-registry JSONL (exact) > CWD directory scan
                    var clotThought = !string.IsNullOrEmpty(c.SessionJsonl) && File.Exists(c.SessionJsonl)
                        ? ReadClotThoughtFromJsonl(c.SessionJsonl, c.HostType)
                        : ReadClotThoughtForCwd(c.Cwd, c.HostType);
                    if (!string.IsNullOrWhiteSpace(clotThought))
                    {
                        // May contain "💬 user\n🤖 assistant" -- split into separate lines
                        foreach (var thoughtLine in clotThought.Split('\n'))
                        {
                            if (string.IsNullOrWhiteSpace(thoughtLine)) continue;
                            var trunc = thoughtLine.Length > 120 ? thoughtLine[..120] + "..." : thoughtLine;
                            sb.AppendLine($"생각: {trunc}");
                        }
                    }
                    sb.AppendLine("----");
                }
                else
                {
                    break;
                }
            }
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>Map host type to Slack emoji icon for card header.</summary>
    static string HostTypeIcon(string? hostType) => (hostType ?? "").ToLowerInvariant() switch
    {
        "vscode" => ":vs:",
        "vscode-claudecode" => ":vs:",
        "vscode-codex" => ":openai:",
        "claude-desktop" => ":claude:",
        "codex" => ":openai:",
        "codex-desktop" => ":openai:",
        "cursor" => ":cursor:",
        "copilot" => ":github:",
        "terminal" => ":terminal:",
        _ => "",
    };

    /// <summary>
    /// Abbreviate a working directory path for compact display.
    /// Drive letter + first letter of each intermediate folder + "-" + leaf folder.
    /// e.g. "D:\GitHub\WKAppBot" -> "WG-WKAppBot"
    ///      "D:\HTS-Project\Source\Main\MainLib" -> "WHSM-MainLib"
    ///      "D:\VIGSOne" -> "W-VIGSOne"
    /// </summary>
    static string AbbreviateCwd(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return "";
        // Walk up to git root (find .git directory)
        try
        {
            var dir = cwd.Replace('/', '\\');
            while (!string.IsNullOrEmpty(dir))
            {
                if (Directory.Exists(Path.Combine(dir, ".git"))
                    || Directory.Exists(Path.Combine(dir, ".svn"))
                    || Directory.Exists(Path.Combine(dir, ".wkappbot"))
                    || File.Exists(Path.Combine(dir, ".wkappbot")))
                { cwd = dir; break; }
                var parent = Path.GetDirectoryName(dir);
                if (parent == dir || parent == null) break;
                dir = parent;
            }
        }
        catch { }
        var path = cwd.Replace('\\', '/').TrimEnd('/');
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return "";
        var drive = parts[0].TrimEnd(':').ToUpperInvariant();  // "W:" -> "W"
        if (parts.Length <= 1) return drive;
        var leaf = parts[^1];
        // Middle folders -> take first char of each as uppercase initial
        var initials = "";
        for (int i = 1; i < parts.Length - 1; i++)
            if (parts[i].Length > 0) initials += char.ToUpperInvariant(parts[i][0]);
        return $"{drive}{initials}-{leaf}";
    }

    /// <summary>
    /// File-based card cache: stores each card's content + last-changed timestamp.
    /// Content-change detection: timestamp only updates when content actually differs.
    /// Survives process restarts (one-shot eye tick, eye restart, etc.)
    /// Files: wkappbot.hq/runtime/card_cache/{cardKey}.json
    /// </summary>
    static string GetCardCacheDir()
    {
        if (string.IsNullOrEmpty(_cardCacheDir))
        {
            _cardCacheDir = Path.Combine(DataDir, "runtime", "card_cache");
            Directory.CreateDirectory(_cardCacheDir);
        }
        return _cardCacheDir;
    }

    /// <summary>Purge all card cache entries (memory + disk). Called on Eye startup for a clean slate.</summary>
    static void CardCachePurgeAll()
    {
        _cardCache.Clear();
        try
        {
            var dir = GetCardCacheDir();
            foreach (var f in Directory.GetFiles(dir, "*.json"))
                try { File.Delete(f); } catch { }
        }
        catch { }
    }

    /// <summary>
    /// Get the cached changedUtc for a card, updating if content changed.
    /// Returns DateTime.MinValue if card has never been seen.
    /// </summary>
    static DateTime CardCacheGetTimestamp(string cardKey, string currentContent)
    {
        // Memory cache first
        if (_cardCache.TryGetValue(cardKey, out var cached))
        {
            if (cached.content == currentContent)
                return cached.changedUtc;
            // Content changed -- update
            var now = DateTime.UtcNow;
            _cardCache[cardKey] = (currentContent, now);
            CardCacheSave(cardKey, currentContent, now);
            return now;
        }

        // Cold start -- load from disk
        var diskEntry = CardCacheLoad(cardKey);
        if (diskEntry.HasValue)
        {
            if (diskEntry.Value.content == currentContent)
            {
                _cardCache[cardKey] = diskEntry.Value;
                return diskEntry.Value.changedUtc;
            }
            // Disk content differs from current -- content changed
            var now = DateTime.UtcNow;
            _cardCache[cardKey] = (currentContent, now);
            CardCacheSave(cardKey, currentContent, now);
            return now;
        }

        // Never seen -- first encounter, use MinValue (card goes to bottom)
        _cardCache[cardKey] = (currentContent, DateTime.MinValue);
        CardCacheSave(cardKey, currentContent, DateTime.MinValue);
        return DateTime.MinValue;
    }

    static void CardCacheSave(string cardKey, string content, DateTime changedUtc)
    {
        try
        {
            var file = Path.Combine(GetCardCacheDir(), $"{cardKey}.json");
            var json = JsonSerializer.Serialize(new { content, changedUtc = changedUtc.ToString("O") });
            var tmp = file + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, file, overwrite: true);
        }
        catch { }
    }

    static (string content, DateTime changedUtc)? CardCacheLoad(string cardKey)
    {
        try
        {
            var file = Path.Combine(GetCardCacheDir(), $"{cardKey}.json");
            if (!File.Exists(file)) return null;
            var node = JsonNode.Parse(File.ReadAllText(file));
            var content = node?["content"]?.GetValue<string>() ?? "";
            var tsStr = node?["changedUtc"]?.GetValue<string>() ?? "";
            if (DateTime.TryParse(tsStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var ts))
                return (content, ts.ToUniversalTime());
            return (content, DateTime.MinValue);
        }
        catch { return null; }
    }

    static (int x, int y) GetRightmostMonitorAnchor(int width, int height)
    {
        try
        {
            var screens = Screen.AllScreens;
            if (screens != null && screens.Length > 0)
            {
                var rightMost = screens.OrderByDescending(s => s.Bounds.Right).First();
                // Use Bounds (full monitor edge), not WorkingArea (excludes taskbar).
                // Screen.AllScreens returns logical pixels (DPI-scaled), matching WPF Window.Left/Top.
                var bounds = rightMost.Bounds;
                var x = bounds.Right - width - 10;
                var y = bounds.Top + 10;
                return (x, y);
            }
        }
        catch { }

        return (1530, 110);
    }
}
