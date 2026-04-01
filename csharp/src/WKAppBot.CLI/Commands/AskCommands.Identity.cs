namespace WKAppBot.CLI;

internal partial class Program
{
    static string? ExtractAskTag(string? text, string tag)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(tag))
            return null;

        var needle = $"[{tag}:";
        var start = text.IndexOf(needle, StringComparison.OrdinalIgnoreCase);
        if (start < 0)
            return null;

        start += needle.Length;
        var end = text.IndexOf(']', start);
        if (end <= start)
            return null;

        return text[start..end].Trim();
    }

    static void BindAskIdentity(AskSession session, string question, string providerTag)
    {
        session.SetIdentity(
            gameId: ExtractAskTag(question, "G"),
            questionId: ExtractAskTag(question, "Q"),
            runId: Environment.GetEnvironmentVariable("WKAPPBOT_RUN_ID"),
            providerTag: providerTag);
    }
}
