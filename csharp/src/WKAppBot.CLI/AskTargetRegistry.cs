using System.Collections.Concurrent;

namespace WKAppBot.CLI;

internal static class AskTargetRegistry
{
    private static readonly ConcurrentDictionary<string, string> TargetIds = new(StringComparer.OrdinalIgnoreCase);

    internal static string? GetTargetId(string tag)
    {
        return TargetIds.TryGetValue(tag, out var id) ? id : null;
    }

    internal static void SetTargetId(string tag, string targetId)
    {
        TargetIds[tag] = targetId;
    }

    internal static void RemoveTargetId(string tag)
    {
        TargetIds.TryRemove(tag, out _);
    }
}
