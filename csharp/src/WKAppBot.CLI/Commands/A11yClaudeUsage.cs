using System.Diagnostics;
using System.Text.RegularExpressions;
using WKAppBot.Win32;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// Claude usage probe -- WebBot CDP only (claude.ai/settings/usage)
internal partial class Program
{
    /// <summary>
    /// Read Claude usage from claude.ai/settings/usage via CDP (WebBot Chrome).
    /// Auto-launches Chrome if not running.
    /// </summary>
    static ClaudeUsageReport? ProbeClaudeUsageViaWeb()
    {
        try
        {
            _cdpBrowserLock.Wait(3000);
            try
            {
                var cdp = ConnectCdp(9222, withBar: false, navigateUrl: ClaudeUsageUrl);
                cdp.NavigateAsync(ClaudeUsageUrl).GetAwaiter().GetResult();

                // Wait for page to render usage percentages (up to 6s)
                var js = """
                    new Promise(resolve => {
                        let tries = 0;
                        const check = () => {
                            const t = document.body?.innerText?.trim() || '';
                            if (t.includes('%') || ++tries >= 30) resolve(t);
                            else setTimeout(check, 200);
                        };
                        check();
                    })
                    """;

                var text = cdp.EvalAsync(js, awaitPromise: true).GetAwaiter().GetResult() ?? "";
                if (text.StartsWith('"') && text.EndsWith('"'))
                    text = System.Text.Json.JsonSerializer.Deserialize<string>(text) ?? "";

                if (string.IsNullOrWhiteSpace(text) || !text.Contains('%'))
                    return null; // not logged in or page didn't load

                Console.Error.WriteLine($"[USAGE] Web page loaded ({text.Length} chars)");
                return ParseUsageFromWebText(text);
            }
            finally { _cdpBrowserLock.Release(); }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[USAGE] Web probe failed: {ex.Message}");
            return null;
        }
    }

    static ClaudeUsageReport ParseUsageFromWebText(string text)
    {
        var report = new ClaudeUsageReport();
        var pctRx  = new Regex(@"(\d+)\s*%");
        var resetRx = new Regex(@"재설정[^\n]{0,40}|reset[^\n]{0,40}", RegexOptions.IgnoreCase);

        var pcts   = pctRx.Matches(text).Select(m => int.Parse(m.Groups[1].Value)).ToList();
        var resets = resetRx.Matches(text).Select(m => m.Value.Trim()).ToList();

        if (pcts.Count >= 1) report.SessionPercent      = pcts[0];
        if (pcts.Count >= 2) report.WeeklyAllPercent    = pcts[1];
        if (pcts.Count >= 3) report.WeeklySonnetPercent = pcts[2];

        if (resets.Count >= 1) report.SessionReset    = resets[0];
        if (resets.Count >= 2) report.WeeklyAllReset  = resets[1];
        if (resets.Count >= 3) report.WeeklySonnetReset = resets[2];

        return report;
    }

    /// <summary>claude-usage -- one-shot usage probe with Slack alert.</summary>
    static int A11yClaudeUsage()
    {
        Console.WriteLine("[USAGE] Trying web probe (claude.ai/settings/usage)...");
        var report = ProbeClaudeUsageViaWeb() ?? new ClaudeUsageReport();

        var sessionRemain = 100 - report.SessionPercent;
        var weeklyRemain = 100 - report.WeeklyAllPercent;

        // JSONL context %
        string jsonlInfo = "";
        try
        {
            var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            var (pct, _, _, fileSize) = GetContextInfoForCwdEx(cwd);
            if (fileSize <= 0)
            {
                var sessDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".claude", "projects");
                if (Directory.Exists(sessDir))
                {
                    foreach (var jsonl in Directory.EnumerateFiles(sessDir, "*.jsonl", SearchOption.AllDirectories)
                        .OrderByDescending(f => new FileInfo(f).Length).Take(1))
                    {
                        var fi = new FileInfo(jsonl);
                        fileSize = fi.Length;
                        pct = (int)(fileSize / (20.0 * 1024 * 1024) * 100);
                    }
                }
            }
            if (fileSize > 0)
            {
                var sizeMB = fileSize / (1024.0 * 1024.0);
                jsonlInfo = sizeMB >= 1 ? $" | JSONL={sizeMB:F0}MB (ctx={pct}%)" : $" | ctx={pct}%";
            }
        }
        catch { }

        Console.WriteLine();
        Console.WriteLine($"  Session: {report.SessionPercent}% used ({sessionRemain}% left) -- {report.SessionReset ?? "?"}{jsonlInfo}");
        Console.WriteLine($"  Weekly:  {report.WeeklyAllPercent}% used ({weeklyRemain}% left) -- {report.WeeklyAllReset ?? "?"}");
        Console.WriteLine($"  Sonnet:  {report.WeeklySonnetPercent}% used -- {report.WeeklySonnetReset ?? "?"}");
        Console.WriteLine($"  URL:     {ClaudeUsageUrl}");

        // Slack alert
        string? slackMsg = null;
        if (report.WeeklyAllPercent >= 95)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[USAGE] CRITICAL: Weekly {report.WeeklyAllPercent}% -- {weeklyRemain}% left!");
            Console.ResetColor();
            slackMsg = $":rotating_light: *Claude 주간 사용량 {report.WeeklyAllPercent}%* ({weeklyRemain}% 남음)\n해제: {report.WeeklyAllReset}\n세션: {report.SessionPercent}% ({report.SessionReset})";
        }
        else if (report.WeeklyAllPercent >= 85)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[USAGE] WARNING: Weekly {report.WeeklyAllPercent}% -- {weeklyRemain}% left");
            Console.ResetColor();
            slackMsg = $":warning: *Claude 주간 사용량 {report.WeeklyAllPercent}%* ({weeklyRemain}% 남음)\n해제: {report.WeeklyAllReset}";
        }

        if (report.SessionPercent >= 90 && slackMsg == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[USAGE] Session at {report.SessionPercent}%!");
            Console.ResetColor();
            slackMsg = $":hourglass: *Claude 세션 사용량 {report.SessionPercent}%* ({sessionRemain}% 남음)\n해제: {report.SessionReset}";
        }

        // Pick the new alert level so we can compare to the last-posted one.
        // NONE means "no threshold tripped" -- used to detect post-reset state.
        string newLevel = slackMsg == null
            ? "NONE"
            : report.WeeklyAllPercent >= 95 ? "CRITICAL"
            : report.WeeklyAllPercent >= 85 ? "WARNING"
            : "SESSION";

        if (ShouldPostUsageAlert(newLevel, report, out var suppressReason))
        {
            if (slackMsg != null)
            {
                try
                {
                    var psi = new ProcessStartInfo("wkappbot", $"slack send \"{slackMsg}\"")
                    { UseShellExecute = false, CreateNoWindow = true };
                    AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "CLAUDE-USAGE")?.WaitForExit(5000);
                    Console.WriteLine($"[USAGE] Slack alert sent (level={newLevel})");
                    PersistUsageAlertState(newLevel, report);
                }
                catch (Exception ex) { Console.Error.WriteLine($"[USAGE] Slack send failed: {ex.Message}"); }
            }
            else
            {
                // Transition TO NONE means Claude reset: clear state so the
                // next threshold cross fires again.
                PersistUsageAlertState("NONE", report);
                Console.WriteLine("[USAGE] Below thresholds -- state cleared (next alert will fire fresh)");
            }
        }
        else
        {
            Console.WriteLine($"[USAGE] Slack alert suppressed: {suppressReason}");
        }

        return 0;
    }

    // Min time between re-posting the SAME alert level. Prevents the spam
    // seen when claude-usage is scheduled hourly -- otherwise every run at
    // 95%+ would fire CRITICAL again. When the level escalates (WARNING ->
    // CRITICAL) or resets to NONE, we post immediately regardless of cooldown.
    static readonly TimeSpan UsageAlertSameLevelCooldown = TimeSpan.FromHours(1);

    static string UsageAlertStatePath()
    {
        var dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WKAppBot");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, "claude-usage-alert-state.json");
    }

    static bool ShouldPostUsageAlert(string newLevel, ClaudeUsageReport report, out string reason)
    {
        reason = "";
        try
        {
            var path = UsageAlertStatePath();
            if (!File.Exists(path))
            {
                // No prior state -- always post (or clear-to-NONE silently).
                reason = newLevel == "NONE" ? "no prior alert, nothing to clear" : "first-time alert";
                return newLevel != "NONE"; // NONE with no prior state = nothing to do, skip silently
            }

            var json = File.ReadAllText(path);
            var node = System.Text.Json.Nodes.JsonNode.Parse(json);
            var lastLevel = node?["level"]?.GetValue<string>() ?? "NONE";
            var lastTsRaw = node?["ts"]?.GetValue<string>();
            var lastWeekly = node?["weekly"]?.GetValue<int>() ?? 0;

            // Reset detection: previous level was non-NONE and weekly % dropped
            // meaningfully. Post transition-to-NONE so state clears.
            if (newLevel == "NONE" && lastLevel != "NONE")
            {
                reason = $"reset detected (was {lastLevel}@{lastWeekly}%, now below thresholds)";
                return true; // state clears, no Slack send (slackMsg is null)
            }

            if (newLevel == "NONE")
            {
                reason = "still below thresholds, state already NONE";
                return false;
            }

            // Escalation: WARNING -> CRITICAL always posts.
            var rank = (string l) => l switch { "CRITICAL" => 3, "SESSION" => 2, "WARNING" => 1, _ => 0 };
            if (rank(newLevel) > rank(lastLevel))
            {
                reason = $"escalation {lastLevel} -> {newLevel}";
                return true;
            }

            // Same-or-lower level within cooldown: suppress.
            DateTime? lastTs = null;
            if (lastTsRaw != null && DateTime.TryParse(lastTsRaw, null,
                    System.Globalization.DateTimeStyles.RoundtripKind, out var parsed))
                lastTs = parsed.ToUniversalTime();
            if (lastTs.HasValue && DateTime.UtcNow - lastTs.Value < UsageAlertSameLevelCooldown)
            {
                var elapsed = (int)(DateTime.UtcNow - lastTs.Value).TotalMinutes;
                reason = $"same-level cooldown ({lastLevel} posted {elapsed}m ago, cooldown {(int)UsageAlertSameLevelCooldown.TotalMinutes}m)";
                return false;
            }

            reason = $"cooldown elapsed, re-posting {newLevel}";
            return true;
        }
        catch (Exception ex)
        {
            reason = $"state read failed: {ex.Message} -- posting anyway";
            return true;
        }
    }

    static void PersistUsageAlertState(string level, ClaudeUsageReport report)
    {
        try
        {
            var obj = new System.Text.Json.Nodes.JsonObject
            {
                ["level"]   = level,
                ["ts"]      = DateTime.UtcNow.ToString("O"),
                ["weekly"]  = report.WeeklyAllPercent,
                ["session"] = report.SessionPercent,
            };
            File.WriteAllText(UsageAlertStatePath(), obj.ToJsonString(new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[USAGE] State persist failed: {ex.Message}");
        }
    }
}

class ClaudeUsageReport
{
    public int SessionPercent { get; set; }
    public string? SessionReset { get; set; }
    public int WeeklyAllPercent { get; set; }
    public string? WeeklyAllReset { get; set; }
    public int WeeklySonnetPercent { get; set; }
    public string? WeeklySonnetReset { get; set; }
}
