using System.IO;
using System.Text;
using System.Text.Json;

namespace WKAppBot.Shared;

/// <summary>
/// One captured tool invocation (shell command, ask dispatch, a11y action,
/// MCP-invoked tool call, ...) with a human-readable tool-scoped id.
///
/// Addressed as "&lt;tool&gt;/&lt;slug&gt;" so the id itself hints at the content:
///     con/git-status
///     con/npm-build-3
///     ask-gemini/why-build-failed
///     a11y/invoke-ok-button
/// Easier to skim in logs, easier for the AI to remember across turns, easier
/// for operators to grep the folder layout.
/// </summary>
public sealed class ToolOutputRecord
{
    /// <summary>Human-readable per-tool id (slug). Unique within the tool folder.</summary>
    public string Slug { get; init; } = "";
    /// <summary>Tool family name: "con" (any console -- cmd/pwsh/bash/CLI),
    /// "ask-gemini", "a11y", "mcp", .... Each tool gets its own subfolder and
    /// its own slug space.</summary>
    public string Tool { get; init; } = "tool";
    /// <summary>"&lt;tool&gt;/&lt;slug&gt;" convenience address.</summary>
    public string Address => $"{Tool}/{Slug}";
    /// <summary>Full invocation string as typed.</summary>
    public string Command { get; init; } = "";
    public string Cwd { get; init; } = "";
    public int ExitCode { get; init; }
    public int LineCount { get; init; }
    public DateTime TimestampUtc { get; init; }
    public string LastLine { get; init; } = "";
    /// <summary>Tool-specific metadata (session id, hwnd, target ai, ...). Optional.</summary>
    public Dictionary<string, string>? Meta { get; init; }
}

/// <summary>
/// File-backed registry of captured tool outputs, partitioned by tool family
/// with human-readable slug ids. Replaces the numeric-id shell store.
///
/// Layout:
///   wkappbot.hq/runtime/tool-out/
///     con/
///       index.jsonl          -- append-only index of ToolOutputRecord
///       git-status.log       -- raw output for "con/git-status"
///       npm-build.log
///       npm-build-2.log      -- collision suffix
///     ask-gemini/
///       why-build-failed.log
///     a11y/
///       invoke-ok.log
///
/// Threadsafe for single-process use. Slug uniqueness is enforced by
/// probing the filesystem (append -2, -3, ... until unique). No in-memory
/// counter needed -- files on disk are the source of truth.
/// </summary>
public static class ToolOutputStore
{
    private static readonly object _lock = new();
    private static readonly string RootDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".",
        "wkappbot.hq", "runtime", "tool-out");

    private static string ToolDir(string tool) =>
        Path.Combine(RootDir, SanitizeTool(tool));

    private static string IndexFile(string tool) =>
        Path.Combine(ToolDir(tool), "index.jsonl");

    /// <summary>Normalize tool name to a safe folder name. Preserves ASCII letters,
    /// digits, hyphen, underscore; lowercases everything.</summary>
    private static string SanitizeTool(string tool)
    {
        if (string.IsNullOrWhiteSpace(tool)) return "tool";
        var sb = new StringBuilder();
        foreach (var ch in tool.Trim().ToLowerInvariant())
            if (char.IsLetterOrDigit(ch) || ch == '-' || ch == '_') sb.Append(ch);
        var s = sb.ToString();
        return string.IsNullOrEmpty(s) ? "tool" : s;
    }

    /// <summary>
    /// Derive a filesystem-safe slug from the command text. Keeps Korean / CJK
    /// / letters / digits / hyphen; spaces and punctuation collapse to '-';
    /// length-capped at 48 chars. Returns "cmd" if nothing survives.
    /// </summary>
    private static string BaseSlug(string command)
    {
        if (string.IsNullOrWhiteSpace(command)) return "cmd";
        var sb = new StringBuilder();
        bool dashPending = false;
        foreach (var ch in command.Trim())
        {
            if (sb.Length >= 48) break;
            // Keep letters/digits from any script (Korean, CJK, Latin...) and - _ .
            if (char.IsLetterOrDigit(ch) || ch == '-' || ch == '_' || ch == '.')
            {
                if (dashPending && sb.Length > 0) { sb.Append('-'); dashPending = false; }
                sb.Append(char.ToLowerInvariant(ch));
            }
            else if (ch == ' ' || ch == '/' || ch == '\\' || ch == ':' || ch == '=')
            {
                dashPending = true;
            }
            // Everything else (quotes, brackets, punctuation) is dropped.
        }
        var s = sb.ToString().Trim('-', '_', '.');
        return string.IsNullOrEmpty(s) ? "cmd" : s;
    }

    /// <summary>Pick a unique slug inside the tool folder by appending -2, -3, ...</summary>
    private static string AllocateUniqueSlug(string toolDir, string baseSlug)
    {
        var candidate = baseSlug;
        int n = 2;
        while (File.Exists(Path.Combine(toolDir, candidate + ".log")))
        {
            candidate = $"{baseSlug}-{n++}";
            if (n > 9999) break; // runaway safeguard
        }
        return candidate;
    }

    /// <summary>
    /// Store a tool's output and return the assigned record. Writes verbatim
    /// output to tool-out/&lt;tool&gt;/&lt;slug&gt;.log and appends a trimmed record to
    /// tool-out/&lt;tool&gt;/index.jsonl. Slug collision within a tool is resolved
    /// by appending -2, -3, ... .
    /// </summary>
    public static ToolOutputRecord Store(
        string tool,
        string command,
        string cwd,
        int exitCode,
        string output,
        Dictionary<string, string>? meta = null)
    {
        var safeTool = SanitizeTool(tool);
        lock (_lock)
        {
            var dir = ToolDir(safeTool);
            try { Directory.CreateDirectory(dir); } catch { }
            var slug = AllocateUniqueSlug(dir, BaseSlug(command));

            var lines = string.IsNullOrEmpty(output) ? 0 : output.Split('\n').Length;
            var lastLine = "";
            if (!string.IsNullOrEmpty(output))
            {
                var split = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (split.Length > 0)
                {
                    lastLine = split[^1].TrimEnd('\r');
                    if (lastLine.Length > 120) lastLine = lastLine[..117] + "...";
                }
            }

            var rec = new ToolOutputRecord
            {
                Slug = slug,
                Tool = safeTool,
                Command = command,
                Cwd = cwd,
                ExitCode = exitCode,
                LineCount = lines,
                TimestampUtc = DateTime.UtcNow,
                LastLine = lastLine,
                Meta = meta,
            };

            try
            {
                File.WriteAllText(Path.Combine(dir, $"{slug}.log"), output ?? "");
                File.AppendAllText(IndexFile(safeTool), JsonSerializer.Serialize(rec) + "\n");
            }
            catch { /* best-effort */ }

            return rec;
        }
    }

    /// <summary>Read full captured output by tool + slug. Null when the log is missing.</summary>
    public static string? ReadBySlug(string tool, string slug)
    {
        try
        {
            var p = Path.Combine(ToolDir(tool), $"{slug}.log");
            return File.Exists(p) ? File.ReadAllText(p) : null;
        }
        catch { return null; }
    }

    /// <summary>
    /// Read by a combined "&lt;tool&gt;/&lt;slug&gt;" address. Convenience for callers
    /// that just have the string id from a tool_use broadcast.
    /// </summary>
    public static string? ReadByAddress(string address)
    {
        var slash = address.IndexOf('/');
        if (slash <= 0 || slash == address.Length - 1) return null;
        return ReadBySlug(address[..slash], address[(slash + 1)..]);
    }

    /// <summary>
    /// Trim old outputs for one tool -- keeps only the most recent
    /// <paramref name="keepLast"/> log files. Does not touch the index file.
    /// </summary>
    public static void Cleanup(string tool, int keepLast = 1000)
    {
        try
        {
            var dir = ToolDir(tool);
            if (!Directory.Exists(dir)) return;
            var logs = new DirectoryInfo(dir).GetFiles("*.log")
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .Skip(keepLast)
                .ToList();
            foreach (var f in logs) try { f.Delete(); } catch { }
        }
        catch { }
    }
}
