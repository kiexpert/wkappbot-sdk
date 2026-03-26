using System.Collections.Concurrent;

namespace WKAppBot.CLI;

/// <summary>
/// Speed-based emoji assignment for 정반합 debate.
/// R0 finish order determines each AI's emoji profile picture.
/// 🦊=1st, 🐬=2nd, 🐙=3rd — fixed for the entire game.
/// </summary>
internal partial class Program
{
    static readonly string[] _speedEmojis = ["🦊", "🐬", "🐙"]; // 1st, 2nd, 3rd
    static readonly ConcurrentDictionary<string, string> _aiEmoji = new();
    static int _emojiIndex;

    internal static void AssignEmojiOnFinish(string ai)
    {
        var idx = Interlocked.Increment(ref _emojiIndex) - 1;
        if (idx < _speedEmojis.Length)
            _aiEmoji[ai] = _speedEmojis[idx];
    }

    internal static void ResetEmojis() { _aiEmoji.Clear(); Interlocked.Exchange(ref _emojiIndex, 0); }

    internal static string AiDisplayName(string ai)
    {
        var emoji = _aiEmoji.GetValueOrDefault(ai, "🤖");
        return ai switch
        {
            "gpt" => $"{emoji} GPT(SKEPTIC)",
            "gemini" => $"{emoji} Gemini(EXPLORER)",
            "claude" => $"{emoji} Claude(AUDITOR)",
            _ => $"{emoji} {ai}",
        };
    }

    /// <summary>Returns AiDisplayName if debate emoji assigned, otherwise raw fallback name.</summary>
    static string SlackAiName(string aiKey, string fallback)
        => _aiEmoji.ContainsKey(aiKey) ? AiDisplayName(aiKey) : fallback;
}
