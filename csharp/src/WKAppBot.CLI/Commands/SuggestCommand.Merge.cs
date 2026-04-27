// SuggestCommand.Merge.cs -- suggest merge subcommand + helpers
// Split from SuggestCommand.cs for maintainability (~330 lines)

using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// suggest merge -- combine multiple related suggestions into one self-healing merge record.
    /// Replaces individual entries with a single grouped record tracking frequency, root cause,
    /// repro cmdlines, estimated work, and auto-heal metadata.
    /// </summary>
    static int SuggestMergeCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest merge <ts1> [ts2 ...] --title \"...\" [options]");
            Console.WriteLine("  Merge multiple related suggestions into one self-healing merge record.");
            Console.WriteLine();
            Console.WriteLine("  ts               : ISO ts prefix(es) to merge (space-separated)");
            Console.WriteLine("  --title \"...\"     Merge title (required)");
            Console.WriteLine("  --root-cause \"..\" Root cause one-liner");
            Console.WriteLine("  --cmdline \"...\"   Repro/fix command (repeatable)");
            Console.WriteLine("  --work \"3h30m\"    Estimated total work time");
            Console.WriteLine("  --components \"..\" Affected source components (comma-separated)");
            Console.WriteLine("  --priority N      Priority 1-5 (5=critical, default=3)");
            Console.WriteLine("  --error-pattern \"..\" Regex matching error text (auto-detect recurrence)");
            Console.WriteLine("  --affected-cmds \"..\" wkappbot commands that trigger this (comma-sep)");
            Console.WriteLine("  --auto-heal-script path  Script to run on recurrence (self-healing)");
            Console.WriteLine("  (max 2 ts-prefixes per merge -- bulk wipe protection)");
            Console.WriteLine("  --keep-originals      Don't remove merged entries from active list");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  wkappbot suggest merge 2026-04-02T02 --all-matching \"Runtime.enable\" \\");
            Console.WriteLine("    --title \"Chrome CDP timeout on heavy tab load\" \\");
            Console.WriteLine("    --root-cause \"Runtime.enable blocks when 11+ tabs open\" \\");
            Console.WriteLine("    --cmdline \"wkappbot ask claude test\" --work \"3h\" \\");
            Console.WriteLine("    --components \"CdpClient,ChromeLauncher\" --priority 4 \\");
            Console.WriteLine("    --error-pattern \"TimeoutException.*Runtime\\\\.enable\" \\");
            Console.WriteLine("    --affected-cmds \"ask\"");
            return 0;
        }

        // -- Parse args --
        var tsPrefixes    = new List<string>();
        string? title     = null;
        string? rootCause = null;
        var cmdlines      = new List<string>();
        string? estimatedWork    = null;
        string? componentsRaw    = null;
        int priority             = 3;
        string? errorPattern     = null;
        string? affectedCmdsRaw  = null;
        string? autoHealScript   = null;
        bool keepOriginals       = false;
        string? allMatchingPattern = null;
        bool forceNoCheck        = false;
        bool dryRun              = false;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--title"          when i + 1 < args.Length: title = args[++i]; break;
                case "--root-cause"     when i + 1 < args.Length: rootCause = args[++i]; break;
                case "--cmdline"        when i + 1 < args.Length: cmdlines.Add(args[++i]); break;
                case "--work"           when i + 1 < args.Length: estimatedWork = args[++i]; break;
                case "--components"     when i + 1 < args.Length: componentsRaw = args[++i]; break;
                case "--priority"       when i + 1 < args.Length && int.TryParse(args[i + 1], out var p):
                    priority = Math.Clamp(p, 1, 5); i++; break;
                case "--error-pattern"  when i + 1 < args.Length: errorPattern = args[++i]; break;
                case "--affected-cmds"  when i + 1 < args.Length: affectedCmdsRaw = args[++i]; break;
                case "--auto-heal-script" when i + 1 < args.Length: autoHealScript = args[++i]; break;
                case "--all-matching" when i + 1 < args.Length:
                    // Block from WKAppBot maintainer channel (DG-WKAppBot CWD) to prevent bulk-fake-resolve abuse.
                    // Non-maintainer channels (WkAutoQuant, etc.) allowed.
                    var callerCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
                    var isWkAppBotMaintainer = callerCwd.Replace('\\','/').Contains("/WKAppBot", StringComparison.OrdinalIgnoreCase)
                        && !callerCwd.Replace('\\','/').Contains("/WkAutoQuant", StringComparison.OrdinalIgnoreCase);
                    if (isWkAppBotMaintainer)
                    {
                        Console.Error.WriteLine("[MERGE] --all-matching blocked from WKAppBot maintainer channel. Use explicit ts prefixes.");
                        return 1;
                    }
                    allMatchingPattern = args[++i];
                    break;
                case "--dry-run": dryRun = true; Console.Error.WriteLine("[MERGE] --dry-run: showing matches only (no write)"); break;
                case "--keep-originals": keepOriginals = true; break;
                case "--force": forceNoCheck = true; break;
                default:
                    if (!args[i].StartsWith("--"))
                        tsPrefixes.Add(args[i]);
                    break;
            }
        }

        var missing = new List<string>();
        if (title == null)        missing.Add("--title");
        if (rootCause == null)    missing.Add("--root-cause");
        if (estimatedWork == null) missing.Add("--work");
        if (componentsRaw == null) missing.Add("--components");
        if (affectedCmdsRaw == null) missing.Add("--affected-cmds");
        if (missing.Count > 0)
        {
            Console.Error.WriteLine($"[MERGE] Missing required argument(s): {string.Join(", ", missing)}");
            Console.WriteLine("  Run 'wkappbot suggest merge --help' for usage.");
            return 1;
        }

        var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(jsonlPath))
        {
            Console.WriteLine("[MERGE] suggestions.jsonl not found");
            return 1;
        }

        var lines    = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        var allNodes = lines.Select((l, idx) => (line: l, idx, node: TryParseNode(l))).ToList();

        // -- Find matching entries --
        var matchedIdxs = new HashSet<int>();

        foreach (var tsPrefix in tsPrefixes)
        {
            for (int i = 0; i < allNodes.Count; i++)
            {
                var node = allNodes[i].node;
                if (node == null) continue;
                var entryTs     = node["ts"]?.GetValue<string>()       ?? "";
                var entrySlackTs = node["slack_ts"]?.GetValue<string>() ?? "";
                if (entryTs.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase) ||
                    entrySlackTs.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase))
                    matchedIdxs.Add(i);
            }
        }

        if (allMatchingPattern != null)
        {
            try
            {
                var rx = new System.Text.RegularExpressions.Regex(allMatchingPattern,
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                    System.Text.RegularExpressions.RegexOptions.Singleline);
                for (int i = 0; i < allNodes.Count; i++)
                {
                    var node = allNodes[i].node;
                    if (node == null) continue;
                    var text = node["text"]?.GetValue<string>() ?? "";
                    if (rx.IsMatch(text)) matchedIdxs.Add(i);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[MERGE] Invalid --all-matching regex: {ex.Message}");
                return 1;
            }
        }

        if (matchedIdxs.Count == 0)
        {
            Console.WriteLine("[MERGE] No matching suggestions found");
            return 1;
        }

        // Hard cap: 2 for explicit ts-prefix merge; 20 for --all-matching (already channel-restricted)
        int hardCap = allMatchingPattern != null ? 20 : 2;
        if (matchedIdxs.Count > hardCap)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[MERGE] BLOCKED: {matchedIdxs.Count} items matched -- max {hardCap} per merge.");
            if (allMatchingPattern == null)
                Console.Error.WriteLine("  Specify exactly 2 ts-prefixes at a time, or use --all-matching with a tighter pattern.");
            Console.ResetColor();
            return 1;
        }

        // --dry-run: show what would be merged, skip write
        if (dryRun)
        {
            Console.WriteLine($"[MERGE:DRY-RUN] Would merge {matchedIdxs.Count} item(s) into \"{title}\":");
            foreach (var idx in matchedIdxs.OrderBy(i => i))
            {
                var node = allNodes[idx].node;
                var txt = node?["text"]?.GetValue<string>() ?? "";
                Console.WriteLine($"  * {node?["ts"]?.GetValue<string>()} | {(txt.Length > 60 ? txt[..60] : txt)}");
            }
            return 0;
        }

        var matched = matchedIdxs.OrderBy(i => i).Select(i => allNodes[i].node!).ToList();

        // Block re-merging: if any matched item is itself a merge record, reject
        var alreadyMerged = matched.Where(n => string.Equals(n["type"]?.GetValue<string>(), "merge", StringComparison.OrdinalIgnoreCase)).ToList();
        if (alreadyMerged.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[MERGE] BLOCKED: {alreadyMerged.Count} matched item(s) are already merged records.");
            foreach (var m in alreadyMerged)
                Console.Error.WriteLine($"     * {m["ts"]?.GetValue<string>()} \"{m["title"]?.GetValue<string>()}\"");
            Console.Error.WriteLine("  Re-merging an already-merged record is not allowed. Specify original suggest ts-prefixes only.");
            Console.ResetColor();
            return 1;
        }

        // -- Validate: same core command --
        if (!forceNoCheck)
        {
            var coreCmds = matched
                .Select(n => ExtractCoreCmd(n["text"]?.GetValue<string>() ?? ""))
                .Where(c => c != null)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (coreCmds.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"[MERGE] Matched suggestions have different core commands:");
                foreach (var c in coreCmds) Console.WriteLine($"     * {c}");
                Console.WriteLine("  Merge requires all suggestions to share the same command context.");
                Console.WriteLine("  Use --all-matching with a tighter pattern, or add --force to override.");
                Console.ResetColor();
                return 1;
            }
        }

        Console.Error.WriteLine($"[MERGE] Merging {matched.Count} suggestion(s) -> \"{title}\"");

        // -- Compute time range & frequency --
        var timestamps = matched
            .Select(n => n["ts"]?.GetValue<string>())
            .Where(ts => ts != null)
            .Select(ts => DateTime.TryParse(ts, null, System.Globalization.DateTimeStyles.RoundtripKind, out var dt) ? dt : (DateTime?)null)
            .Where(dt => dt != null).Select(dt => dt!.Value)
            .OrderBy(dt => dt).ToList();

        var firstSeen = timestamps.Count > 0 ? timestamps.First().ToString("o") : DateTime.UtcNow.ToString("o");
        var lastSeen  = timestamps.Count > 0 ? timestamps.Last().ToString("o")  : DateTime.UtcNow.ToString("o");

        double frequencyPerHour = 0;
        if (timestamps.Count >= 2)
        {
            var hours = (timestamps.Last() - timestamps.First()).TotalHours;
            if (hours > 0) frequencyPerHour = Math.Round(matched.Count / hours, 2);
        }

        var recurrenceRisk = frequencyPerHour switch
        {
            > 10 => "critical",
            > 2  => "high",
            > 0.5 => "medium",
            _    => "low"
        };

        var mergedTs     = matched.Select(n => n["ts"]?.GetValue<string>() ?? "").Where(ts => ts.Length > 0).ToList();
        var mergedSlacks = matched.Select(n => n["slack_ts"]?.GetValue<string>()).Where(s => !string.IsNullOrEmpty(s)).ToList();

        // -- Build merge record --
        var components   = SplitCsv(componentsRaw);
        var affectedCmds = SplitCsv(affectedCmdsRaw);

        var mergeRecord = new Dictionary<string, object?>
        {
            ["type"]              = "merge",
            ["ts"]                = DateTime.UtcNow.ToString("o"),
            ["from"]              = AbbreviateCwd(Environment.CurrentDirectory),
            ["title"]             = title,
            ["text"]              = title,
            ["mergedTs"]          = mergedTs.ToArray(),
            ["mergedSlackTs"]     = mergedSlacks.ToArray(),
            ["count"]             = matched.Count,
            ["firstSeen"]         = firstSeen,
            ["lastSeen"]          = lastSeen,
            ["frequencyPerHour"]  = frequencyPerHour,
            ["recurrenceRisk"]    = recurrenceRisk,
            ["rootCause"]         = rootCause,
            ["components"]        = components,
            ["affectedCommands"]  = affectedCmds,
            ["errorPattern"]      = errorPattern,
            ["cmdlines"]          = cmdlines.ToArray(),
            ["estimatedWork"]     = estimatedWork,
            ["priority"]          = priority,
            ["autoHealScript"]    = autoHealScript,
            ["autoHealResult"]    = (object?)null,
            ["autoHealAt"]        = (object?)null,
            ["status"]            = "pending",
            ["slack_ts"]          = (object?)null,
            ["files"]             = Array.Empty<string>(),
        };

        var mergeJson = JsonSerializer.Serialize(mergeRecord);

        // Safeguard 1: pre-op snapshot of suggestions.jsonl before destructive write.
        BackupSuggestFile("merge");

        // -- Remove originals --
        if (!keepOriginals)
        {
            foreach (var idx in matchedIdxs.OrderByDescending(i => i))
                lines.RemoveAt(idx);
            Console.Error.WriteLine($"[MERGE] Removed {matchedIdxs.Count} original entries from active list");
        }

        lines.Add(mergeJson);
        File.WriteAllLines(jsonlPath, lines);
        Console.Error.WriteLine($"[MERGE] Saved (priority={priority}, risk={recurrenceRisk}, freq={frequencyPerHour}/hr, est={estimatedWork ?? "??"})");

        // -- Slack notification --
        var slackCfg  = LoadSlackConfig();
        var botToken  = slackCfg?["bot_token"]?.GetValue<string>();
        var channel   = slackCfg?["channel"]?.GetValue<string>();
        if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
        {
            var senderName = GetSuggestBypassUsername();
            var prioStars  = new string('*', priority) + new string('.', 5 - priority);
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($":twisted_rightwards_arrows: *[MERGE]* `{title}`");
            sb.AppendLine($"* *Count:* {matched.Count}  |  *Priority:* {prioStars} ({priority}/5)  |  *Risk:* `{recurrenceRisk}`");
            if (rootCause != null)       sb.AppendLine($"* *Root cause:* {rootCause}");
            if (errorPattern != null)    sb.AppendLine($"* *Error pattern:* `{errorPattern}`");
            if (cmdlines.Count > 0)      sb.AppendLine($"* *Cmdlines:* {string.Join(", ", cmdlines.Select(c => $"`{c}`"))}");
            if (estimatedWork != null)   sb.AppendLine($"* *Est. work:* {estimatedWork}");
            if (components.Length > 0)   sb.AppendLine($"* *Components:* {string.Join(", ", components)}");
            if (affectedCmds.Length > 0) sb.AppendLine($"* *Affected cmds:* {string.Join(", ", affectedCmds)}");
            if (autoHealScript != null)  sb.AppendLine($"* *Auto-heal script:* `{autoHealScript}`");
            sb.Append($"* *Range:* {(timestamps.Count > 0 ? timestamps.First().ToLocalTime().ToString("MM-dd HH:mm") : "?")} -> {(timestamps.Count > 0 ? timestamps.Last().ToLocalTime().ToString("MM-dd HH:mm") : "?")}  ({frequencyPerHour}/hr)");

            var (ok, newTs) = SlackSendViaApi(botToken, channel, sb.ToString(), username: senderName).GetAwaiter().GetResult();
            if (ok && newTs != null)
            {
                var obj = JsonSerializer.Deserialize<Dictionary<string, object?>>(lines[^1])!;
                obj["slack_ts"] = (object?)newTs;
                lines[^1] = JsonSerializer.Serialize(obj);
                File.WriteAllLines(jsonlPath, lines);
                Console.Error.WriteLine($"[MERGE] Slack sent (ts={newTs})");
            }
            else Console.Error.WriteLine("[MERGE] Slack send failed");
        }

        // Auto-commit: subject = the merge pattern when --all-matching, else the title slice.
        var mergeSubject = !string.IsNullOrEmpty(allMatchingPattern)
            ? ShortTsForCommit(allMatchingPattern, 50)
            : ShortTsForCommit(title ?? string.Join(",", tsPrefixes), 50);
        try { GitCommitSuggestFiles("merge", mergeSubject); }
        catch (InvalidOperationException) { return 1; }

        return 0;
    }

    private static string[] SplitCsv(string? raw)
        => raw?.Split(',').Select(s => s.Trim()).Where(s => s.Length > 0).ToArray() ?? Array.Empty<string>();

    private static JsonNode? TryParseNode(string line)
    {
        try { return JsonSerializer.Deserialize<JsonNode>(line); } catch { return null; }
    }

    /// <summary>
    /// Extract the dominant wkappbot command from suggestion text for merge validation.
    /// </summary>
    private static string? ExtractCoreCmd(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        var tagM = System.Text.RegularExpressions.Regex.Match(text,
            @"^\s*\[([A-Z][A-Z0-9\-]{1,15})\]", System.Text.RegularExpressions.RegexOptions.Multiline);
        if (tagM.Success) return tagM.Groups[1].Value.ToUpperInvariant();

        var wkM = System.Text.RegularExpressions.Regex.Match(text[..Math.Min(200, text.Length)],
            @"wkappbot\s+([a-z][a-z0-9\-]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        if (wkM.Success) return wkM.Groups[1].Value.ToLowerInvariant();

        var ftrM = System.Text.RegularExpressions.Regex.Match(text,
            @"\[(FOCUSLESS-HOMEWORK|BUG-AUTO)\]",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        if (ftrM.Success) return ftrM.Groups[1].Value.ToUpperInvariant();

        return null;
    }
}
