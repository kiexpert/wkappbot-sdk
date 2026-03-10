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
}
