namespace WKAppBot.CLI;

// partial class: shared arg parsing — separate text vs files from mixed args
internal partial class Program
{
    /// <summary>
    /// Parse mixed args into text lines and file paths (like ask/slack send/suggest/clipboard-write).
    /// Files are auto-detected by File.Exists. Args starting with "--" are never treated as files.
    /// </summary>
    /// <param name="args">Raw arguments to parse.</param>
    /// <param name="startIndex">Index to start parsing from (skip subcommand etc).</param>
    /// <returns>(textParts, filePaths) — text lines and resolved file paths.</returns>
    static (List<string> TextParts, List<string> FilePaths) ParseTextAndFiles(string[] args, int startIndex = 0)
    {
        var textParts = new List<string>();
        var filePaths = new List<string>();
        for (int i = startIndex; i < args.Length; i++)
        {
            if (!args[i].StartsWith("--") && File.Exists(args[i]))
                filePaths.Add(Path.GetFullPath(args[i]));
            else
                textParts.Add(args[i]);
        }
        return (textParts, filePaths);
    }

    /// <summary>
    /// Like ParseTextAndFiles but also detects directories (for clipboard-write CF_HDROP).
    /// Inserts [file:name] markers in text parts at file positions.
    /// </summary>
    static (List<string> TextParts, List<string> FilePaths) ParseTextAndFilesWithMarkers(string[] args, int startIndex = 0)
    {
        var textParts = new List<string>();
        var filePaths = new List<string>();
        for (int i = startIndex; i < args.Length; i++)
        {
            if (!args[i].StartsWith("--") && (File.Exists(args[i]) || Directory.Exists(args[i])))
            {
                filePaths.Add(Path.GetFullPath(args[i]));
                textParts.Add($"[file:{Path.GetFileName(args[i])}]");
            }
            else
            {
                textParts.Add(args[i]);
            }
        }
        return (textParts, filePaths);
    }

    /// <summary>
    /// Inline small .txt/.md files as question text instead of as attachments.
    /// Called after ParseTextAndFilesWithMarkers. Mutates attachFiles in place.
    /// Example: ask gpt /tmp/question.txt → file content used as question, not attached.
    /// </summary>
    static string InlineTextFiles(List<string> questionParts, List<string> attachFiles)
    {
        var parts = new List<string>(questionParts.Count);
        foreach (var part in questionParts)
        {
            if (part.StartsWith("[file:") && part.EndsWith("]"))
            {
                var fname = part[6..^1];
                var ext = Path.GetExtension(fname).ToLowerInvariant();
                if (ext is ".txt" or ".md")
                {
                    var fpath = attachFiles.Find(f =>
                        string.Equals(Path.GetFileName(f), fname, StringComparison.OrdinalIgnoreCase));
                    if (fpath != null && new FileInfo(fpath).Length < 100_000)
                    {
                        parts.Add(File.ReadAllText(fpath, System.Text.Encoding.UTF8).Trim());
                        attachFiles.Remove(fpath); // remove from attachments — inlined instead
                        continue;
                    }
                }
            }
            parts.Add(part);
        }
        return string.Join("\n", parts);
    }
}
