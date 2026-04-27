using System.IO.Compression;

namespace WKAppBot.CLI;

// partial class: wkappbot skill install / export / import -- deploy and zip I/O
internal partial class Program
{
    // -- Install ------------------------------------------------------------------

    static int SkillInstallCommand(string[] args)
    {
        string? appFilter = null;
        bool force = false;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app"   && i + 1 < args.Length) appFilter = args[++i];
            if (args[i] == "--force") force = true;
        }

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills to install.");
            return 0;
        }

        int copied = 0, skipped = 0;
        var installed = new List<SkillRecord>();
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var rel  = Path.GetRelativePath(ProjectSkillsDir, f);
            if (appFilter != null && !rel.StartsWith(appFilter + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                continue;

            var dest = Path.Combine(HqSkillsDir, rel);
            if (!force && File.Exists(dest))
            {
                var src  = SkillRecord.Load(f);
                var existing = SkillRecord.Load(dest);
                if (src != null && existing != null && CompareVersions(src.Version, existing.Version) <= 0)
                { skipped++; continue; }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            File.Copy(f, dest, overwrite: true);
            copied++;
            Console.Error.WriteLine($"[SKILL] Installed: {rel}");
            var s = SkillRecord.Load(f);
            if (s != null) installed.Add(s);
        }

        Console.Error.WriteLine($"[SKILL] Install done: {copied} installed, {skipped} skipped (already up-to-date).");
        NotifyNewSkillsMatchingHistory(installed);
        return 0;
    }

    static int CompareVersions(string? a, string? b)
    {
        static (int maj, int min) Parse(string? v)
        {
            var parts = (v ?? "1.0").Split('.');
            int.TryParse(parts.ElementAtOrDefault(0), out int maj);
            int.TryParse(parts.ElementAtOrDefault(1), out int min);
            return (maj, min);
        }
        var (am, an) = Parse(a);
        var (bm, bn) = Parse(b);
        var cmp = am.CompareTo(bm);
        return cmp != 0 ? cmp : an.CompareTo(bn);
    }

    // -- Export ------------------------------------------------------------------─

    static int SkillExportCommand(string[] args)
    {
        string? appFilter = null, outPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];
            if (args[i] == "--out" && i + 1 < args.Length) outPath   = args[++i];
        }

        outPath ??= Path.Combine(Directory.GetCurrentDirectory(),
            appFilter != null ? $"skills-{appFilter}.zip" : "skills.zip");

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills to export.");
            return 1;
        }

        if (File.Exists(outPath)) File.Delete(outPath);
        using var zip = ZipFile.Open(outPath, ZipArchiveMode.Create);

        int count = 0;
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(ProjectSkillsDir, f).Replace('\\', '/');
            if (appFilter != null && !rel.StartsWith(appFilter + "/", StringComparison.OrdinalIgnoreCase))
                continue;
            zip.CreateEntryFromFile(f, rel);
            count++;
        }

        Console.Error.WriteLine($"[SKILL] Exported {count} skill(s) -> {outPath}");
        return 0;
    }

    // -- Import ------------------------------------------------------------------─

    static int SkillImportCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill import <file.zip>");
            return 1;
        }

        var zipPath = args[0];
        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"[SKILL] File not found: {zipPath}");
            return 1;
        }

        Directory.CreateDirectory(ProjectSkillsDir);
        int count = 0;
        using var zip = ZipFile.OpenRead(zipPath);
        foreach (var entry in zip.Entries.Where(e => e.Name.EndsWith(".skill.json")))
        {
            var dest = Path.Combine(ProjectSkillsDir, entry.FullName.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            entry.ExtractToFile(dest, overwrite: true);
            count++;
        }

        Console.Error.WriteLine($"[SKILL] Imported {count} skill(s) from {zipPath}");
        return 0;
    }
}
