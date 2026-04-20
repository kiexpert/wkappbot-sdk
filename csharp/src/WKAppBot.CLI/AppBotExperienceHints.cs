using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// Experience hints and knowhow broadcast helpers.
// Called by do/input/click commands to surface past failure history and guidance.
internal partial class Program
{
    /// <summary>
    /// Show form-level experience hints and knowhow broadcast.
    /// </summary>
    static void ShowFormExperienceHints(ExperienceDb expDb, string formId, string? actionName = null)
    {
        try
        {
            var summary = expDb.GetFormActionLogSummary(formId);
            if (summary != null && summary.TotalEntries > 0)
            {
                var breakdown = summary.ActionBreakdown.Count > 0
                    ? string.Join(", ", summary.ActionBreakdown.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [EXP] previous records: {summary.TotalEntries}");
                if (!string.IsNullOrEmpty(breakdown))
                    Console.Write($" ({breakdown})");
                Console.WriteLine($", screenshots: {summary.ScreenshotCount}");
                Console.ResetColor();
            }

            string? knowhowPath = null;
            if (!string.IsNullOrEmpty(actionName))
                knowhowPath = expDb.GetFormActionKnowhowPath(formId, actionName);
            knowhowPath ??= expDb.GetFormKnowhowPath(formId);

            if (knowhowPath != null)
                ShowKnowhowBroadcast(knowhowPath);
        }
        catch { }
    }

    /// <summary>
    /// Show control-level experience hints and knowhow broadcast.
    /// </summary>
    static void ShowControlExperienceHints(ExperienceDb expDb, string formId, int cid,
        string? actionName = null)
    {
        try
        {
            var summary = expDb.GetControlActionLogSummary(formId, cid);
            if (summary != null && summary.TotalEntries > 0)
            {
                var breakdown = summary.ActionBreakdown.Count > 0
                    ? string.Join(", ", summary.ActionBreakdown.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [EXP] cid={cid}: previous records: {summary.TotalEntries}");
                if (!string.IsNullOrEmpty(breakdown))
                    Console.Write($" ({breakdown})");
                Console.WriteLine($", screenshots: {summary.ScreenshotCount}");
                Console.ResetColor();
            }

            bool found = false;
            if (!string.IsNullOrEmpty(actionName))
            {
                var (actFormPath, actCtrlPath) = expDb.GetActionKnowhowPaths(formId, cid, actionName);
                if (actCtrlPath != null) { ShowKnowhowBroadcast(actCtrlPath); found = true; }
                else if (actFormPath != null) { ShowKnowhowBroadcast(actFormPath); found = true; }
            }
            if (!found)
            {
                var (formPath, ctrlPath) = expDb.GetKnowhowPaths(formId, cid);
                if (ctrlPath != null) { ShowKnowhowBroadcast(ctrlPath); found = true; }
                else if (formPath != null) { ShowKnowhowBroadcast(formPath); found = true; }
            }

            if (!found && summary != null && summary.TotalEntries > 0)
            {
                var ctrlDir = expDb.GetControlDir(formId, cid, create: false);
                var suggestedFile = !string.IsNullOrEmpty(actionName)
                    ? $"knowhow-{actionName.ToLowerInvariant()}.md"
                    : "knowhow.md";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Error.WriteLine($"  [KNOWHOW] record knowhow here -> {Path.Combine(ctrlDir, suggestedFile)}");
                Console.ResetColor();
            }
        }
        catch { }
    }

    /// <summary>
    /// Knowhow broadcast: outputs the first paragraph (summary/TOC) of the file.
    /// First paragraph = non-empty lines from file start up to first blank line.
    /// ## headers used as title if present.
    /// </summary>
    static void ShowKnowhowBroadcast(string knowhowPath, string tag = "KNOWHOW")
    {
        try
        {
            var allLines = File.ReadAllLines(knowhowPath, System.Text.Encoding.UTF8);
            if (allLines.Length == 0) return;

            int sectionCount = allLines.Count(l => l.TrimStart().StartsWith("## "));

            string title = "";
            int contentStart = 0;
            for (int i = 0; i < allLines.Length; i++)
            {
                var trimmed = allLines[i].TrimStart();
                if (trimmed.StartsWith("## "))
                {
                    title = trimmed.TrimStart('#').Trim();
                    contentStart = i + 1;
                    break;
                }
                if (!string.IsNullOrWhiteSpace(allLines[i]))
                {
                    contentStart = i;
                    break;
                }
            }

            var paragraph = new List<string>();
            bool started = false;
            for (int i = contentStart; i < allLines.Length; i++)
            {
                var line = allLines[i].TrimEnd();
                if (line.TrimStart().StartsWith("## ")) break;
                if (started && string.IsNullOrWhiteSpace(line)) break;
                if (!string.IsNullOrWhiteSpace(line))
                {
                    started = true;
                    paragraph.Add(line);
                }
            }

            if (paragraph.Count == 0 && string.IsNullOrEmpty(title)) return;

            Console.ForegroundColor = ConsoleColor.Magenta;
            var countInfo = sectionCount > 1 ? $" ({sectionCount}§)" : "";
            Console.Error.Write($"  [{tag}]{countInfo} ");
            Console.ResetColor();

            if (!string.IsNullOrEmpty(title))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Error.Write($"{title}: ");
                Console.ResetColor();
            }

            if (paragraph.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                var text = string.Join(" ", paragraph.Select(l =>
                    l.TrimStart('-', '*', ' ', '\t', '`')));
                text = System.Text.RegularExpressions.Regex.Replace(text, @"\s{2,}", " ").Trim();
                if (text.Length > 200) text = text[..197] + "...";
                Console.Error.WriteLine(text);
                Console.ResetColor();
            }
            else
            {
                Console.Error.WriteLine();
            }
        }
        catch { }
    }

    static string SanitizePathTokenForExp(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "unknown";
        var invalid = Path.GetInvalidFileNameChars();
        var chars = s.Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray();
        return new string(chars);
    }

    // Alias for InspectionCommands backward compatibility (SnapshotCommand.cs was deleted)
    static string SanitizePathToken(string s) => SanitizePathTokenForExp(s);
}
