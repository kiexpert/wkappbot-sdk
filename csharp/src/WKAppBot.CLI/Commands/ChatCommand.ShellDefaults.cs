namespace WKAppBot.CLI;

internal partial class Program
{
    // Supported shell targets. AI entries run as REPL via NonBlockingLineReader;
    // OS shells (cmd/powershell/bash) are stdio-inherited subprocesses.
    static readonly HashSet<string> _shellAis =
        new(StringComparer.OrdinalIgnoreCase) { "gpt", "gemini", "triad" };
    static readonly HashSet<string> _shellOsNames =
        new(StringComparer.OrdinalIgnoreCase) { "cmd", "powershell", "pwsh", "bash" };

    /// <summary>Path of persisted default-shell pointer. One line, shell name.</summary>
    static string ChatDefaultShellFile => Path.Combine(DataDir, "chat-default.txt");

    static string GetDefaultShell()
    {
        try
        {
            if (File.Exists(ChatDefaultShellFile))
            {
                var v = File.ReadAllText(ChatDefaultShellFile).Trim().ToLowerInvariant();
                if (!string.IsNullOrEmpty(v)) return v;
            }
        }
        catch { }
        return "claude"; // bootstrap default
    }

    static int SetDefaultShell(string shell)
    {
        shell = shell.Trim().ToLowerInvariant();
        if (shell != "claude" && shell != "codex" && !_shellAis.Contains(shell) && !_shellOsNames.Contains(shell))
        {
            Console.Error.WriteLine($"[CHAT] --set-default: unknown shell '{shell}'. Valid: claude | codex | cmd | powershell | bash | gemini | gpt | triad");
            return 2;
        }
        try
        {
            Directory.CreateDirectory(DataDir);
            File.WriteAllText(ChatDefaultShellFile, shell + "\n");
            Console.WriteLine($"[CHAT] default shell set to '{shell}' -> {ChatDefaultShellFile}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] --set-default failed: {ex.Message}");
            return 1;
        }
    }
}
