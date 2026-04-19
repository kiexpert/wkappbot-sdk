namespace WKAppBot.CLI;

internal sealed class EyeParentCard
{
    public int ParentPid { get; set; }
    public string ParentName { get; set; } = "";
    public string ParentTitle { get; set; } = "";
    public string HostType { get; set; } = "";     // vscode, claude-desktop, codex, cursor, terminal
    public string LastTag { get; set; } = "";
    public string LastStatus { get; set; } = "";
    public DateTime LastTsUtc { get; set; }
    public string Cwd { get; set; } = "";    // working directory for display
    public string SessionJsonl { get; set; } = ""; // explicit JSONL path from session registry (bypasses cache scan)
}

internal sealed class PromptDiag
{
    public long StatMs { get; set; }
    public long ReadMs { get; set; }
    public long ScanMs { get; set; }
    public long ParseMs { get; set; }
    public long NormMs { get; set; }
    public long CacheMs { get; set; }
    public string Source { get; set; } = "none";
    public DateTime FileWriteUtc { get; set; } = DateTime.MinValue; // session file mtime
}
