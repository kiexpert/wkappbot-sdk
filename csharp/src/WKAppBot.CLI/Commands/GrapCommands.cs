namespace WKAppBot.CLI;

/// <summary>
/// wkappbot grap save|list|show|remove|verify — named GRAP alias management.
/// Bare "grap &lt;pattern&gt;" is still routed to logcat (grep alias).
/// </summary>
internal partial class Program
{
    static int GrapAliasCommand(string[] args)
    {
        // args[0] = sub-command (save/list/show/remove/verify), args[1..] = params
        if (args.Length == 0) { PrintGrapAliasHelp(); return 1; }

        return args[0].ToLowerInvariant() switch
        {
            "save"   => GrapAliasSave(args[1..]),
            "list"   => GrapAliasList(args[1..]),
            "show"   => GrapAliasShow(args[1..]),
            "remove" or "rm" or "delete" or "del" => GrapAliasRemove(args[1..]),
            "verify" => GrapAliasVerify(args[1..]),
            _ => Error($"Unknown grap sub-command '{args[0]}'. Use: save | list | show | remove | verify")
        };
    }

    // ── save ──────────────────────────────────────────────────────────────────

    static int GrapAliasSave(string[] args)
    {
        // grap save <@name> <grap> [--title "desc"]
        // grap save <@name> --find <pattern>  → resolve via a11y find then save
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: grap save @<name> <grap-pattern> [--title \"desc\"]");
            Console.Error.WriteLine("       grap save @<name> --find <grap>    (resolve hwnd first)");
            return 1;
        }

        var rawName = args[0].TrimStart('@');
        if (string.IsNullOrEmpty(rawName))
            return Error("Alias name cannot be empty.");

        // title flag
        var title = "";
        var titleIdx = Array.IndexOf(args, "--title");
        if (titleIdx >= 0 && titleIdx + 1 < args.Length)
            title = args[titleIdx + 1];

        string grap;
        if (args.Length >= 2 && args[1] == "--find")
        {
            // resolve via WindowFinder
            var pattern = args.Length >= 3 ? args[2] : "";
            if (string.IsNullOrEmpty(pattern))
                return Error("--find requires a pattern argument.");

            var hits = WKAppBot.Win32.Window.WindowFinder.FindByTitle(pattern, false);
            if (hits.Count == 0)
                return Error($"--find: no windows matched '{pattern}'");
            if (hits.Count > 1)
            {
                Console.Error.WriteLine($"[GRAP-ALIAS] Multiple matches for '{pattern}':");
                foreach (var h in hits.Take(8))
                    Console.Error.WriteLine($"  {BuildTargetGrap(h.Handle)}");
                return Error("Ambiguous match — narrow the pattern.");
            }
            grap = BuildCompactWinGrap(hits[0].Handle);
            if (string.IsNullOrEmpty(title))
                title = WKAppBot.Win32.Native.NativeMethods.GetWindowTextW(hits[0].Handle);
        }
        else
        {
            grap = args[1];
            // If quoted grap starts/ends with quotes, strip them
            if (grap.StartsWith('"') && grap.EndsWith('"') && grap.Length > 1)
                grap = grap[1..^1];
        }

        GrapAliasStore.Set(rawName, grap, title);
        Console.WriteLine($"[GRAP-ALIAS] saved @{rawName} → {grap}");
        if (!string.IsNullOrEmpty(title)) Console.WriteLine($"  title: {title}");
        return 0;
    }

    // ── list ──────────────────────────────────────────────────────────────────

    static int GrapAliasList(string[] _)
    {
        var aliases = GrapAliasStore.LoadAll();
        if (aliases.Count == 0)
        {
            Console.WriteLine("(no aliases — use: grap save @name <grap>)");
            return 0;
        }
        Console.WriteLine($"{"@name",-20} {"grap",-50} title");
        Console.WriteLine(new string('─', 90));
        foreach (var a in aliases)
        {
            var g = a.Grap.Length > 48 ? a.Grap[..45] + "..." : a.Grap;
            Console.WriteLine($"@{a.Name,-19} {g,-50} {a.Title}");
        }
        Console.WriteLine($"\n  {aliases.Count} alias(es)  — grap show @<name> for full grap");
        return 0;
    }

    // ── show ──────────────────────────────────────────────────────────────────

    static int GrapAliasShow(string[] args)
    {
        if (args.Length == 0) return Error("Usage: grap show @<name>");
        var name = args[0].TrimStart('@');
        var a = GrapAliasStore.Get(name);
        if (a == null) return Error($"@{name} not found.");
        Console.WriteLine($"@{a.Name}");
        Console.WriteLine($"  grap:  {a.Grap}");
        if (!string.IsNullOrEmpty(a.Title)) Console.WriteLine($"  title: {a.Title}");
        Console.WriteLine($"  saved: {a.Saved}");
        return 0;
    }

    // ── remove ────────────────────────────────────────────────────────────────

    static int GrapAliasRemove(string[] args)
    {
        if (args.Length == 0) return Error("Usage: grap remove @<name>");
        var name = args[0].TrimStart('@');
        if (GrapAliasStore.Remove(name))
        {
            Console.WriteLine($"[GRAP-ALIAS] removed @{name}");
            return 0;
        }
        return Error($"@{name} not found.");
    }

    // ── verify ────────────────────────────────────────────────────────────────

    static int GrapAliasVerify(string[] args)
    {
        var aliases = args.Length > 0
            ? args.Select(n => GrapAliasStore.Get(n.TrimStart('@'))).OfType<GrapAliasStore.GrapAlias>().ToList()
            : GrapAliasStore.LoadAll();

        if (aliases.Count == 0)
        {
            Console.WriteLine("(nothing to verify)");
            return 0;
        }

        int ok = 0, miss = 0, healed = 0;
        foreach (var a in aliases)
        {
            var expanded = GrapAliasStore.Expand($"@{a!.Name}", out var note);
            if (note != null) { healed++; Console.Error.WriteLine(note); }

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var hits = WKAppBot.Win32.Window.WindowFinder.FindByTitle(expanded, true);
            sw.Stop();

            if (hits.Count > 0)
            {
                ok++;
                Console.WriteLine($"[OK]   @{a.Name} → {expanded} ({hits.Count} match, {sw.ElapsedMilliseconds}ms)");
            }
            else
            {
                miss++;
                Console.WriteLine($"[MISS] @{a.Name} → {expanded} (0 matches)");
            }
        }
        Console.WriteLine($"\n  {ok} OK · {miss} MISS · {healed} healed");
        return miss > 0 ? 1 : 0;
    }

    // ── help ──────────────────────────────────────────────────────────────────

    static void PrintGrapAliasHelp()
    {
        Console.WriteLine("grap alias management — named shortcuts for GRAP patterns");
        Console.WriteLine();
        Console.WriteLine("  grap save @<name> <grap>              Save alias");
        Console.WriteLine("  grap save @<name> --find <pattern>    Resolve window then save");
        Console.WriteLine("  grap list                             List all aliases");
        Console.WriteLine("  grap show @<name>                     Show full grap for alias");
        Console.WriteLine("  grap remove @<name>                   Delete alias");
        Console.WriteLine("  grap verify [@<name> ...]             Verify all (or named) aliases");
        Console.WriteLine();
        Console.WriteLine("  Use @name anywhere a grap is accepted:");
        Console.WriteLine("  a11y find @chatgpt                    Same as: a11y find {domain:'chatgpt.com'}");
        Console.WriteLine("  a11y click @chrome#Submit             Suffix appended after expansion");
        Console.WriteLine();
        Console.WriteLine("  Self-healing: stale hwnd is stripped automatically on expansion.");
    }
}
