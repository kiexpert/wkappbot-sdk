namespace WKAppBot.CLI;

internal partial class Program
{
    static string? ResolveClaudeExe()
    {
        var exts = new[] { ".cmd", ".exe", ".bat", "" };

        // 1) Normal PATH walk (covers most installs when PATH was refreshed after install)
        var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
        foreach (var dir in pathEnv.Split(Path.PathSeparator))
        {
            if (string.IsNullOrWhiteSpace(dir)) continue;
            foreach (var ext in exts)
            {
                var p = Path.Combine(dir, "claude" + ext);
                if (File.Exists(p)) return p;
            }
        }

        // 2) Well-known npm global install locations on Windows. Eye often starts
        // BEFORE the user installs @anthropic-ai/claude-code via npm, so its cached
        // PATH doesn't yet include %APPDATA%\npm. Explicit probe avoids needing an
        // Eye restart.
        var fallbackDirs = new List<string>();
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (!string.IsNullOrEmpty(appData))
            fallbackDirs.Add(Path.Combine(appData, "npm"));
        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!string.IsNullOrEmpty(localAppData))
        {
            fallbackDirs.Add(Path.Combine(localAppData, "npm"));
            fallbackDirs.Add(Path.Combine(localAppData, "Programs", "claude")); // official installer target
        }
        fallbackDirs.Add(@"C:\Program Files\nodejs");
        fallbackDirs.Add(@"C:\Program Files (x86)\nodejs");

        foreach (var dir in fallbackDirs)
        {
            foreach (var ext in exts)
            {
                var p = Path.Combine(dir, "claude" + ext);
                if (File.Exists(p))
                {
                    Console.Error.WriteLine($"[CHAT] claude resolved via fallback probe: {p}");
                    Console.Error.WriteLine($"[CHAT] (PATH did not include {dir} -- Eye started before install; restart Eye or add to system PATH)");
                    return p;
                }
            }
        }

        return null;
    }

    static string? ResolveBashExe()
    {
        foreach (var p in new[]
        {
            @"C:\Program Files\Git\bin\bash.exe",
            @"C:\Program Files\Git\usr\bin\bash.exe",
            @"C:\Windows\System32\bash.exe",
        })
            if (File.Exists(p)) return p;
        return null;
    }

    static string? ResolveCodexExe()
    {
        try
        {
            var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
            foreach (var dir in pathEnv.Split(Path.PathSeparator))
            {
                if (string.IsNullOrWhiteSpace(dir)) continue;
                var candidate = Path.Combine(dir, "codex.exe");
                if (File.Exists(candidate)) return candidate;
            }
        }
        catch { }

        try
        {
            var home = Environment.GetEnvironmentVariable("USERPROFILE");
            if (!string.IsNullOrEmpty(home))
            {
                var fallback = Path.Combine(home, ".codex", ".sandbox-bin", "codex.exe");
                if (File.Exists(fallback)) return fallback;
            }
        }
        catch { }

        return null;
    }
}
