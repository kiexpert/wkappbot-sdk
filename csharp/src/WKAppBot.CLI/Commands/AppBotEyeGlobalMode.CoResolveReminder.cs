// AppBotEyeGlobalMode.CoResolveReminder.cs -- Hourly reminder for half-resolved suggests.
//
// When a suggest is in `half_resolved` state (1/2 of co-resolve done, awaiting
// confirmation from the original author), Eye periodically nudges the
// original author's Slack thread until they confirm. Without this nudge,
// half-resolved entries can linger indefinitely if the confirmer is in
// another workspace and never sees the original notice.
//
// Logic (called from RunOneGlobalTick on a slow cadence):
//   1. Read DataDir/suggestions.jsonl
//   2. For each entry with half_resolved_by set (= 1/2 written, waiting):
//      a. Skip if half_resolved_at < 60 min ago (don't nag within the first hour)
//      b. Skip if last_coresolve_reminder_at < 60 min ago
//      c. Otherwise: post Slack reminder + bump last_coresolve_reminder_at
//   3. Persist entry mutations back to suggestions.jsonl (with backup)
//
// Throttle is conservative on purpose: we'd rather under-remind than spam.

using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    static DateTime _lastCoResolveReminderScanUtc = DateTime.MinValue;
    // In-memory reminder timestamps -- avoids writing back to suggestions.jsonl
    // (which would race with concurrent suggest submits and silently drop new entries).
    static readonly Dictionary<string, DateTime> _coResolveReminderSent = new();

    /// <summary>
    /// Scan suggestions.jsonl for half-resolved entries and post hourly Slack
    /// reminders to the original author. Safe to call from the Eye tick loop;
    /// the method short-circuits cheaply when nothing is pending.
    /// </summary>
    static void RunCoResolveReminderScan()
    {
        // Outer cadence guard: don't even read the file more than once / 5min.
        if ((DateTime.UtcNow - _lastCoResolveReminderScanUtc).TotalMinutes < 5) return;
        _lastCoResolveReminderScanUtc = DateTime.UtcNow;

        var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(jsonlPath)) return;

        // Read-only scan: reminder state kept in-memory (_coResolveReminderSent).
        // We do NOT write back to suggestions.jsonl -- doing so races with concurrent
        // suggest submits and silently drops newly-appended entries (the overwrite
        // resets the file to the snapshot taken before the submit arrived).
        IEnumerable<string> lines;
        try { lines = File.ReadLines(jsonlPath); }
        catch { return; }

        var nowUtc = DateTime.UtcNow;
        JsonNode? slackCfg = null;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            JsonNode? node;
            try { node = JsonNode.Parse(line); }
            catch { continue; }
            if (node is not JsonObject entry) continue;

            var halfBy = entry["half_resolved_by"]?.GetValue<string>();
            if (string.IsNullOrEmpty(halfBy)) continue;
            if (entry["resolved_at"] != null) continue;

            var halfAtStr = entry["half_resolved_at"]?.GetValue<string>();
            if (!DateTime.TryParse(halfAtStr, null,
                    System.Globalization.DateTimeStyles.RoundtripKind, out var halfAt))
                continue;
            halfAt = halfAt.ToUniversalTime();

            // Don't nag within the first hour of half-resolve.
            if ((nowUtc - halfAt).TotalMinutes < 60) continue;

            var ts = entry["ts"]?.GetValue<string>() ?? "";
            if (string.IsNullOrEmpty(ts)) continue;

            // In-memory throttle: skip if reminded within the last hour.
            if (_coResolveReminderSent.TryGetValue(ts, out var lastSent) &&
                (nowUtc - lastSent).TotalMinutes < 60) continue;

            var suggestFrom    = entry["from"]?.GetValue<string>() ?? "unknown";
            var suggestCwd     = entry["cwd"]?.GetValue<string>() ?? "";
            var slackChannelId = entry["slack_channel"]?.GetValue<string>() ?? "";
            var slackTs        = entry["slack_ts"]?.GetValue<string>() ?? "";
            var titleRaw       = entry["text"]?.GetValue<string>() ?? entry["title"]?.GetValue<string>() ?? "";
            var skillId        = entry["half_resolved_skill"]?.GetValue<string>();

            if (string.IsNullOrEmpty(slackTs)) continue;
            slackCfg ??= LoadSlackConfig();

            var titlePreview = SquashWhitespace(titleRaw);
            if (titlePreview.Length > 90) titlePreview = titlePreview[..90] + "...";

            var skillArg = string.IsNullOrEmpty(skillId) ? "<skill-id>" : skillId;
            var tsPrefix = ts.Length >= 19 ? ts[..19] : ts;
            var msg =
                $":alarm_clock: *[REMIND]* `{suggestFrom}`, 아직 확인을 기다리고 있어요:\n" +
                $"> \"{titlePreview}\"\n" +
                $"> ts=`{tsPrefix}`\n" +
                $"> *확인:* `wkappbot suggest resolve {tsPrefix} --confirm \"your note\" --skill {skillArg} test.sh`\n" +
                $"\n" +
                $":bulb: *확인 전에 요구사항 추가:*\n" +
                $"> `wkappbot suggest add-requirement {tsPrefix} \"wkappbot <cmd> => <expected>\" --skill {skillArg}`";

            bool posted = TryPostCoResolveReminder(slackCfg, suggestCwd, slackChannelId, slackTs, msg);
            if (!posted) continue;

            // Record in-memory only -- no file write to avoid race with submit.
            _coResolveReminderSent[ts] = nowUtc;
            Console.Error.WriteLine($"[EYE:COREMIND] reminded {suggestFrom} on ts={tsPrefix} (half-age={(int)(nowUtc - halfAt).TotalMinutes}min)");
        }
    }

    static bool TryPostCoResolveReminder(JsonNode? cfg, string suggestCwd, string slackChannelId, string slackTs, string msg)
    {
        try
        {
            var (botToken, channel) = ResolveReplyTarget(suggestCwd, slackChannelId, cfg, "[EYE:COREMIND]");
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return false;

            var senderName = GetSuggestBypassUsername();
            var (ok, _) = SlackSendViaApi(botToken, channel, msg,
                threadTs: slackTs, username: senderName, replyBroadcast: false).GetAwaiter().GetResult();
            return ok;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:COREMIND] post failed: {ex.Message}");
            return false;
        }
    }

    static string SquashWhitespace(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        var sb = new System.Text.StringBuilder(s.Length);
        bool prevWs = false;
        foreach (var c in s)
        {
            if (char.IsWhiteSpace(c))
            {
                if (!prevWs) sb.Append(' ');
                prevWs = true;
            }
            else
            {
                sb.Append(c);
                prevWs = false;
            }
        }
        return sb.ToString().Trim();
    }
}
