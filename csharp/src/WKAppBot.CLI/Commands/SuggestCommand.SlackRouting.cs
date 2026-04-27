// SuggestCommand.SlackRouting.cs -- cross-channel Slack reply routing for suggest resolve.
//
// Goal: when resolving a suggest filed from a DIFFERENT project
// (e.g. WkAutoQuant), the reply (RESOLVED notice / co-resolve half / force
// override) must land in the SUGGESTER's Slack channel/thread, not the
// resolver's. The thread TS only makes sense in the channel that owns it.
//
// Routing key = the suggest's `cwd` field (the exact project root where the
// suggest was filed). Slug-based lookup was fragile (multiple slugs map to
// the same root after worktree/symlink moves); the CWD is authoritative.
//
// Per-project Slack config layout (first match wins):
//   1. {suggest.cwd}/.wkappbot/hq/profiles/slack_exp/webhook.json   (canonical)
//   2. {suggest.cwd}/bin/wkappbot.hq/profiles/slack_exp/webhook.json (legacy)
//   3. null -> caller falls back to LoadSlackConfig() and logs WARN:XCHAN

using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Resolve the per-project Slack webhook config for the project rooted
    /// at <paramref name="suggestCwd"/>. Returns null when no config is
    /// found at either the canonical or legacy path -- the caller is
    /// expected to fall back to <see cref="LoadSlackConfig"/> and emit a
    /// [WARN:XCHAN] log line.
    /// </summary>
    internal static JsonNode? TryLoadWebhookForCwd(string? suggestCwd, string logTag = "[RESOLVE:SLACK]")
    {
        if (string.IsNullOrWhiteSpace(suggestCwd)) return null;
        if (!Directory.Exists(suggestCwd))
        {
            Console.WriteLine($"{logTag} WARN:XCHAN -- suggest cwd '{suggestCwd}' missing; falling back to resolver channel");
            return null;
        }

        // 1) Canonical: {cwd}/.wkappbot/hq/profiles/slack_exp/webhook.json
        var canonical = Path.Combine(suggestCwd, ".wkappbot", "hq", "profiles", "slack_exp", "webhook.json");
        var node = TryReadWebhookFile(canonical);
        if (node != null) return node;

        // 2) Legacy: {cwd}/bin/wkappbot.hq/profiles/slack_exp/webhook.json
        var legacy = Path.Combine(suggestCwd, "bin", "wkappbot.hq", "profiles", "slack_exp", "webhook.json");
        node = TryReadWebhookFile(legacy);
        if (node != null) return node;

        Console.WriteLine($"{logTag} WARN:XCHAN -- no webhook.json under '{suggestCwd}'; falling back to resolver channel");
        return null;
    }

    private static JsonNode? TryReadWebhookFile(string path)
    {
        try
        {
            if (!File.Exists(path)) return null;
            var node = JsonNode.Parse(File.ReadAllText(path));
            return HasMinimumWebhookFields(node) ? node : null;
        }
        catch
        {
            return null;
        }
    }

    private static bool HasMinimumWebhookFields(JsonNode? node)
    {
        if (node == null) return false;
        var token = node["bot_token"]?.GetValue<string>();
        var channel = node["channel"]?.GetValue<string>();
        return !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(channel);
    }

    /// <summary>
    /// Resolve the Slack (botToken, channel) pair to use when replying into
    /// the SUGGESTER's thread. Falls back to <paramref name="resolverCfg"/>
    /// (the resolver's local webhook) when no per-project config exists,
    /// emitting a single [WARN:XCHAN] log line.
    ///
    /// Priority:
    ///   1. <paramref name="slackChannelId"/> non-empty: use it as reply
    ///      channel paired with resolver's bot_token (same workspace/bot,
    ///      so the token is reusable -- only the channel routing differs).
    ///   2. CWD-based webhook lookup under <paramref name="suggestCwd"/>.
    ///   3. Resolver's channel + token (with WARN:XCHAN for cross-project
    ///      cases that fell through both prior options).
    ///
    /// <paramref name="suggestCwd"/> is the suggest entry's `cwd` field
    /// (the project root where the suggest was filed). Empty/null and
    /// resolver-local cwd both short-circuit to the resolver config.
    /// </summary>
    internal static (string? botToken, string? channel) ResolveReplyTarget(
        string? suggestCwd, string? slackChannelId, JsonNode? resolverCfg, string logTag = "[RESOLVE:SLACK]")
    {
        var resolverToken   = resolverCfg?["bot_token"]?.GetValue<string>();
        var resolverChannel = resolverCfg?["channel"]?.GetValue<string>();

        // Same-project resolves: nothing to route. Compare normalized cwd
        // against the resolver's current directory (drives the resolver's
        // own webhook config too).
        var resolverCwd = Environment.CurrentDirectory;
        if (!string.IsNullOrWhiteSpace(suggestCwd)
            && !string.IsNullOrWhiteSpace(resolverCwd)
            && PathsEqual(suggestCwd, resolverCwd))
        {
            return (resolverToken, resolverChannel);
        }

        // 1) Channel ID stored on the suggest entry wins. Same Slack workspace +
        // same bot, so resolver's bot_token authenticates fine; only the channel
        // routing changes. This is the canonical path for cross-project suggests
        // where the suggester has no separate webhook.json.
        if (!string.IsNullOrWhiteSpace(slackChannelId))
        {
            Console.WriteLine($"{logTag} XCHAN routed -- using stored channel '{slackChannelId}' from suggest entry");
            return (resolverToken, slackChannelId);
        }

        // Empty/missing cwd AND no stored channel: keep using resolver's config
        // silently (legacy bug-auto / pre-cwd entries fall through here).
        if (string.IsNullOrWhiteSpace(suggestCwd))
            return (resolverToken, resolverChannel);

        // 2) Per-project webhook.json lookup (legacy path; still useful when
        // the suggester project DOES have its own webhook.json).
        var suggesterCfg = TryLoadWebhookForCwd(suggestCwd, logTag);
        if (suggesterCfg != null)
        {
            var token   = suggesterCfg["bot_token"]?.GetValue<string>() ?? resolverToken;
            var channel = suggesterCfg["channel"]?.GetValue<string>()   ?? resolverChannel;
            Console.WriteLine($"{logTag} XCHAN routed -- replying via '{suggestCwd}' webhook");
            return (token, channel);
        }

        // 3) TryLoadWebhookForCwd already logged WARN:XCHAN; just fall back.
        return (resolverToken, resolverChannel);
    }

    private static bool PathsEqual(string a, string b)
    {
        try
        {
            var na = Path.GetFullPath(a).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var nb = Path.GetFullPath(b).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return string.Equals(na, nb, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
