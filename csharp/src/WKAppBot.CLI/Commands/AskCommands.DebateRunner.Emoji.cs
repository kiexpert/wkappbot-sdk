using System.Collections.Concurrent;

namespace WKAppBot.CLI;

/// <summary>
/// Speed-based emoji assignment for debate mode.
/// R0 finish order determines each AI's emoji profile picture.
/// Fox = 1st, dolphin = 2nd, octopus = 3rd, fixed for the entire game.
/// </summary>
internal partial class Program
{
    static readonly string[] _speedEmojis = ["\U0001F98A", "\U0001F42C", "\U0001F419"];
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
        var emoji = _aiEmoji.GetValueOrDefault(ai, "\U0001F916");
        return ai switch
        {
            "gpt" => $"{emoji} GPT(SKEPTIC)",
            "gemini" => $"{emoji} Gemini(EXPLORER)",
            "claude" => $"{emoji} Claude(AUDITOR)",
            _ => $"{emoji} {ai}",
        };
    }

    /// <summary>Returns AiDisplayName if debate emoji assigned, otherwise a construction marker.</summary>
    static string SlackAiName(string aiKey, string fallback)
        => _aiEmoji.ContainsKey(aiKey) ? AiDisplayName(aiKey) : $"\U0001F6A7 {fallback}";
}
