// SuggestCommand.CoResolve.cs -- 2-of-2 co-resolve guard.
//
// Goal: prevent fake bulk-resolves where one party silently closes another
// party's suggest. Cross-author resolves now require the original suggest
// author to confirm with `--confirm` from the original channel.
//
// Decision matrix (computed by CheckCoResolveStatus):
//   resolver_channel == suggest.from  -> Proceed   (same author)
//   suggest.from == "bug-auto"        -> Proceed   (auto-suggests are open)
//   --force "reason" present          -> ForceProceed (override; Slack notice)
//   already half-resolved by us       -> Skip       (duplicate call)
//   already half-resolved by other    -> CompleteSecondHalf (writes 2/2)
//   first cross-author resolve        -> WroteHalf  (1/2 -- waiting)
//
// "Half-resolved" state lives in the suggest entry as three fields:
//   half_resolved_by   = resolver channel string (e.g. "DG-WKAppBot")
//   half_resolved_note = the resolver's note
//   half_resolved_at   = ISO UTC timestamp
// Entry stays in suggestions.jsonl (NOT moved to history) until the second
// party confirms.
//
// Resolver channel = AbbreviateCwd(Environment.CurrentDirectory) -- same
// derivation that suggest submit uses to populate `from`.

using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    internal enum CoResolveDecision
    {
        Proceed,            // same author, bug-auto, or completing 2/2
        ForceProceed,       // --force override
        WroteHalf,          // 1/2 written; do NOT move to history
        Skip,               // duplicate (already half-resolved by us)
        CompleteSecondHalf, // confirming 2/2; proceed to history with both notes
    }

    internal sealed class CoResolveOutcome
    {
        public CoResolveDecision Decision { get; init; }
        public string? FirstHalfBy   { get; init; }   // who wrote 1/2 (for 2/2 messages)
        public string? FirstHalfNote { get; init; }
        public string? FirstHalfAt   { get; init; }
        public string? ForceReason   { get; init; }
    }

    /// <summary>
    /// Evaluate the 2-of-2 co-resolve gate for a resolve attempt.
    ///
    /// Side effects (only when decision == WroteHalf):
    ///   - Patches suggestions.jsonl in place with half_resolved_* fields.
    ///   - Posts a Slack thread reply asking the original author to confirm.
    ///
    /// Side effects (only when decision == ForceProceed):
    ///   - Posts an override notice to the Slack thread.
    ///
    /// Returns null on hard error (caller should treat as exit 1). All other
    /// outcomes carry a CoResolveDecision the caller switches on.
    /// </summary>
    internal static CoResolveOutcome? CheckCoResolveStatus(
        string jsonlPath,
        List<string> lines,
        int matchIdx,
        JsonNode matchEntry,
        string tsPrefix,
        string note,
        string? skillId,
        string? forceReason,
        bool confirmFlag,
        string? slackTs)
    {
        var suggestFrom = matchEntry["from"]?.GetValue<string>() ?? "unknown";
        var suggestCwd  = matchEntry["cwd"]?.GetValue<string>() ?? "";
        var slackChannelId = matchEntry["slack_channel"]?.GetValue<string>() ?? "";
        var resolverChannel = AbbreviateCwd(Environment.CurrentDirectory);
        if (string.IsNullOrEmpty(resolverChannel)) resolverChannel = "unknown";

        // 1) bug-auto suggests are open: anyone can resolve.
        if (suggestFrom.Equals("bug-auto", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"[RESOLVE:CO] bug-auto suggest -- co-resolve gate skipped");
            return new CoResolveOutcome { Decision = CoResolveDecision.Proceed };
        }

        // 2) Same author -- proceed as before.
        if (suggestFrom.Equals(resolverChannel, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"[RESOLVE:CO] same author ({resolverChannel}) -- co-resolve not required");
            return new CoResolveOutcome { Decision = CoResolveDecision.Proceed };
        }

        // 3) --force "reason" override -- post Slack notice and proceed.
        if (!string.IsNullOrWhiteSpace(forceReason))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[RESOLVE:CO] FORCE override: {resolverChannel} bypassing 2/2 (reason: {forceReason})");
            Console.ResetColor();
            PostCoResolveOverride(slackTs, resolverChannel, suggestFrom, suggestCwd, slackChannelId, forceReason);
            return new CoResolveOutcome
            {
                Decision = CoResolveDecision.ForceProceed,
                ForceReason = forceReason,
            };
        }

        var existingBy   = matchEntry["half_resolved_by"]?.GetValue<string>();
        var existingNote = matchEntry["half_resolved_note"]?.GetValue<string>();
        var existingAt   = matchEntry["half_resolved_at"]?.GetValue<string>();

        // 4) Already half-resolved.
        if (!string.IsNullOrEmpty(existingBy))
        {
            // 4a) Same party calling again -- skip.
            if (existingBy.Equals(resolverChannel, StringComparison.OrdinalIgnoreCase))
            {
                var otherParty = suggestFrom; // we're waiting for the original author
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[RESOLVE:SKIP] already waiting for {otherParty}");
                Console.ResetColor();
                return new CoResolveOutcome
                {
                    Decision = CoResolveDecision.Skip,
                    FirstHalfBy = existingBy,
                    FirstHalfNote = existingNote,
                    FirstHalfAt = existingAt,
                };
            }

            // 4b) Different party (could be the original author confirming).
            // Either matches suggestFrom (the rightful confirmer) OR yet a
            // third party. We accept anything not equal to existingBy --
            // co-resolve only requires "two distinct parties", and the typical
            // case is that the second is the original author.
            Console.WriteLine($"[RESOLVE:CO] completing 2/2 -- first half by {existingBy}, now confirmed by {resolverChannel}");
            return new CoResolveOutcome
            {
                Decision = CoResolveDecision.CompleteSecondHalf,
                FirstHalfBy = existingBy,
                FirstHalfNote = existingNote,
                FirstHalfAt = existingAt,
            };
        }

        // 5) First cross-author resolve -- write 1/2 and notify Slack.
        var nowIso = DateTime.UtcNow.ToString("o");
        var entryObj = matchEntry.AsObject();
        entryObj["half_resolved_by"]   = resolverChannel;
        entryObj["half_resolved_note"] = note;
        entryObj["half_resolved_at"]   = nowIso;
        if (!string.IsNullOrEmpty(skillId))
            entryObj["half_resolved_skill"] = skillId;

        try
        {
            lines[matchIdx] = matchEntry.ToJsonString();
            // Pre-op snapshot before we patch the JSONL in place.
            BackupSuggestFile("co-resolve-half");
            File.WriteAllLines(jsonlPath, lines);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[RESOLVE:CO] failed to write half-resolve state: {ex.Message}");
            Console.ResetColor();
            return null;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[RESOLVE:HALF] 1/2 by {resolverChannel} -- waiting for {suggestFrom} to confirm");
        Console.ResetColor();

        PostCoResolveHalfNotice(slackTs, resolverChannel, suggestFrom, suggestCwd, slackChannelId, tsPrefix, skillId);

        return new CoResolveOutcome { Decision = CoResolveDecision.WroteHalf };
    }

    private static void PostCoResolveHalfNotice(
        string? slackTs, string resolver, string originalAuthor, string? suggestCwd, string? slackChannelId, string tsPrefix, string? skillId)
    {
        if (string.IsNullOrEmpty(slackTs)) return;
        try
        {
            var cfg = LoadSlackConfig();
            // Route to suggester's channel by stored channel id (preferred) or cwd -- thread TS belongs there.
            var (botToken, channel) = ResolveReplyTarget(suggestCwd, slackChannelId, cfg, "[RESOLVE:CO]");
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            var skillArg = string.IsNullOrEmpty(skillId) ? "<skill-id>" : skillId;
            var msg = $":white_check_mark: *[RESOLVE:HALF]* `{resolver}` 가 이 제안을 resolve 했습니다.\n"
                    + $"{originalAuthor}, 아래 명령으로 확인해주세요:\n"
                    + $"> `wkappbot suggest resolve {tsPrefix} --confirm 'your note' --skill {skillArg} test.sh`\n"
                    + $"\n"
                    + $":bulb: *확인 전에 요구사항을 추가하고 싶다면:*\n"
                    + $"> `wkappbot suggest add-requirement {tsPrefix} \"wkappbot <cmd> => <expected>\" --skill {skillArg}`\n"
                    + $"> 요구사항이 스킬에 즉시 반영되고, --confirm 때 함께 검증됩니다.";

            var senderName = GetSuggestBypassUsername();
            var (ok, _) = SlackSendViaApi(botToken, channel, msg,
                threadTs: slackTs, username: senderName, replyBroadcast: false).GetAwaiter().GetResult();
            if (ok) Console.WriteLine($"[RESOLVE:CO] Slack half-resolve notice posted to thread {slackTs}");
            else    Console.WriteLine($"[RESOLVE:CO] Slack half-resolve notice FAILED");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RESOLVE:CO] Slack notice exception: {ex.Message}");
        }
    }

    private static void PostCoResolveOverride(
        string? slackTs, string resolver, string originalAuthor, string? suggestCwd, string? slackChannelId, string reason)
    {
        if (string.IsNullOrEmpty(slackTs)) return;
        try
        {
            var cfg = LoadSlackConfig();
            // Route to suggester's channel by stored channel id (preferred) or cwd -- thread TS belongs there.
            var (botToken, channel) = ResolveReplyTarget(suggestCwd, slackChannelId, cfg, "[RESOLVE:CO]");
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            var msg = $":warning: *[RESOLVE:FORCE]* {resolver} bypassed 2/2 co-resolve for {originalAuthor}'s suggest. " +
                      $"Reason: _{reason}_";

            var senderName = GetSuggestBypassUsername();
            SlackSendViaApi(botToken, channel, msg,
                threadTs: slackTs, username: senderName, replyBroadcast: false).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RESOLVE:CO] Slack override notice exception: {ex.Message}");
        }
    }

    /// <summary>
    /// Build a Slack confirmation reply for a completed 2-of-2 resolve.
    /// Caller posts via existing SlackSendViaApi flow alongside the normal
    /// "RESOLVED" message.
    /// </summary>
    internal static string BuildCoResolveDoneMessage(
        CoResolveOutcome outcome, string resolverChannel, string finalNote)
    {
        var firstBy   = outcome.FirstHalfBy   ?? "?";
        var firstNote = outcome.FirstHalfNote ?? "(no note)";
        return $":white_check_mark: *[RESOLVE:DONE] 2/2 complete*\n" +
               $"> 1/2 by `{firstBy}`: {firstNote}\n" +
               $"> 2/2 by `{resolverChannel}`: {finalNote}";
    }
}
