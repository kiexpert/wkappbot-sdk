using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ═══ Kill-by-Pattern: close --kill ═══
    // Kills processes matching grap pattern. No window needed.
    // Search key per process: "[pid]processName.exe" — substring match by default (no * needed).
    // Supports:
    //   "wkappbot-core"            → any process named wkappbot-core (substring)
    //   "node/wkappbot-core"       → wkappbot-core whose direct parent is node
    //   "flutter/node/wkappbot-core" → chain: flutter→node→wkappbot-core
    //   "**/wkappbot-core"         → wkappbot-core with any ancestor (any depth)
    //   "wkappbot-core#regex:lucy" → '#' splits name#exePathFilter
    // If process has a window → WM_CLOSE first then Kill; else → Kill directly.
    static int A11yKillByPattern(string grap, bool allowAncestors, bool dryRun = false, string? argFilter = null)
    {
        // Global dry-run mode overrides local flag
        if (_dryRunMode.Value) dryRun = true;
        // Split '#' for optional exe-path sub-filter
        string namePattern = grap;
        string? exeFilter = null;
        var hashIdx = grap.IndexOf('#');
        if (hashIdx >= 0)
        {
            namePattern = grap[..hashIdx].Trim();
            exeFilter = grap[(hashIdx + 1)..].Trim();
            if (string.IsNullOrEmpty(exeFilter)) exeFilter = null;
        }

        // Split '/' for parent/child chain: last segment = target, rest = ancestors (outermost first)
        var segments = namePattern.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string targetPattern = segments.Length > 0 ? segments[^1] : namePattern;
        string[] ancestorPatterns = segments.Length > 1 ? segments[..^1] : Array.Empty<string>();

        var targetMatcher = PatternMatcher.Create(targetPattern);
        var exeMatcher = exeFilter != null ? PatternMatcher.Create(exeFilter) : null;

        var allProcs = System.Diagnostics.Process.GetProcesses();
        // PID→processName lookup for ancestor chain resolution
        var pidToName = allProcs.GroupBy(p => p.Id)
            .ToDictionary(g => g.Key, g => g.First().ProcessName);

        var selfPids = allowAncestors ? new HashSet<int>() : GetSelfAndAncestorPids();
        var killed = new List<string>();
        var skipped = new List<string>();

        foreach (var p in allProcs)
        {
            try
            {
                var procName = p.ProcessName;
                string exePath = "";
                try { exePath = p.MainModule?.FileName ?? ""; } catch { }

                // Search key: "[pid]processName.exe" — substring by default
                var nodeKey = $"[{p.Id}]{procName}.exe";

                if (!targetMatcher.IsMatch(nodeKey) && !targetMatcher.IsMatch(procName))
                    continue;

                if (exeMatcher != null && !exeMatcher.IsMatch(string.IsNullOrEmpty(exePath) ? nodeKey : exePath))
                    continue;

                if (ancestorPatterns.Length > 0 && !KillMatchesAncestorChain(p.Id, ancestorPatterns, pidToName))
                    continue;

                if (selfPids.Contains(p.Id))
                {
                    skipped.Add($"{nodeKey} (ancestor-protected)");
                    continue;
                }

                // Protect MCP launcher processes (long-lived stdio relay, survives hot-swap)
                if (IsMcpLauncherProcess(p.Id))
                {
                    skipped.Add($"{nodeKey} (mcp-launcher-protected)");
                    continue;
                }

                // Get command line for display + protection + --arg filter
                string cmdLine = "";
                try { cmdLine = NativeMethods.GetProcessCommandLine(p.Id) ?? ""; } catch { }
                var cmdBrief = cmdLine.Length > 80 ? cmdLine[..80] + "..." : cmdLine;

                // --arg=<substring>: filter by cmdline argument
                if (argFilter != null && !cmdLine.Contains(argFilter, StringComparison.OrdinalIgnoreCase))
                    continue;

                // Protect Eye child processes: WhisperRing, ScreenSaver, analyze-hack
                if (!string.IsNullOrEmpty(cmdLine) && (
                    cmdLine.Contains("whisper-ring", StringComparison.OrdinalIgnoreCase) ||
                    cmdLine.Contains("screensaver", StringComparison.OrdinalIgnoreCase) ||
                    cmdLine.Contains("analyze-hack", StringComparison.OrdinalIgnoreCase)))
                {
                    skipped.Add($"[{p.Id}]{procName} (eye-child)");
                    Console.WriteLine($"[KILL] [{p.Id}]{procName} — SKIP (eye-child: {cmdBrief})");
                    continue;
                }

                if (dryRun)
                {
                    Console.WriteLine($"[KILL:DRY] [{p.Id}]{procName} — {cmdBrief}");
                    killed.Add($"[{p.Id}]{procName}");
                    continue;
                }

                var mainHwnd = p.MainWindowHandle;
                bool hasWindow = mainHwnd != IntPtr.Zero && NativeMethods.IsWindow(mainHwnd);
                if (hasWindow)
                {
                    Console.WriteLine($"[KILL] [{p.Id}]{procName} — window found, sending WM_CLOSE\n       cmd: {cmdBrief}");
                    NativeMethods.SendMessageTimeoutW(mainHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 2000, out _);
                    Thread.Sleep(800);
                    try { p.Refresh(); } catch { }
                    if (p.HasExited)
                    {
                        Console.WriteLine($"[KILL] [{p.Id}]{procName} — exited gracefully");
                        killed.Add($"[{p.Id}]{procName}");
                        continue;
                    }
                    Console.WriteLine($"[KILL] [{p.Id}]{procName} — still alive, force kill");
                }
                else
                {
                    Console.WriteLine($"[KILL] [{p.Id}]{procName} — no window, force kill\n       cmd: {cmdBrief}");
                }
                p.Kill(entireProcessTree: false);
                killed.Add($"[{p.Id}]{procName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KILL] pid={p.Id} failed: {ex.Message}");
            }
        }

        if (skipped.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (var s in skipped)
                Console.WriteLine($"[GUARD] skipped {s} — use --allow-ancestors to override");
            Console.ResetColor();
        }

        if (killed.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[KILL] no matching processes for \"{grap}\"");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[KILL] killed {killed.Count}: {string.Join(", ", killed)}");
        Console.ResetColor();

        // X-ray cleanup: recover any windows left transparent by killed processes
        try { XRayHelper.RestoreAll(); } catch { }

        return 0;
    }

    // Returns true if the process is an MCP launcher (wkappbot.exe mcp --launcher).
    // These are long-lived stdio relays that must survive hot-swap.
    static bool IsMcpLauncherProcess(int pid)
    {
        try
        {
            var cmdLine = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine(pid);
            if (string.IsNullOrEmpty(cmdLine)) return false;
            return cmdLine.Contains(" mcp ", StringComparison.OrdinalIgnoreCase) ||
                   cmdLine.EndsWith(" mcp", StringComparison.OrdinalIgnoreCase) ||
                   cmdLine.Contains("--launcher", StringComparison.OrdinalIgnoreCase);
        }
        catch { return false; }
    }

    // Ancestor chain matching for --kill.
    // ancestorPatterns: outermost first, e.g. ["flutter","node"] for flutter/node/target
    // Builds actual ancestor list innermost-first and matches reversed patterns.
    // "**" in patterns = skip any number of ancestor levels (multi-level wildcard).
    static bool KillMatchesAncestorChain(int childPid, string[] ancestorPatterns, Dictionary<int, string> pidToName)
    {
        var ancestors = new List<string>();
        int pid = childPid;
        for (int i = 0; i < 20; i++)
        {
            int ppid = GetParentPid(pid);
            if (ppid <= 0 || ppid == pid) break;
            if (!pidToName.TryGetValue(ppid, out var name)) break;
            ancestors.Add(name);
            pid = ppid;
        }
        // Reverse patterns so innermost = first to match
        return KillMatchChainRec(ancestorPatterns.Reverse().ToArray(), 0, ancestors, 0);
    }

    static bool KillMatchChainRec(string[] patterns, int pi, List<string> ancestors, int ai)
    {
        if (pi >= patterns.Length) return true;
        if (ai > ancestors.Count) return false;
        var pat = patterns[pi];
        if (pat == "**")
        {
            // Try matching remaining patterns at every ancestor depth
            for (int skip = 0; skip + ai <= ancestors.Count; skip++)
                if (KillMatchChainRec(patterns, pi + 1, ancestors, ai + skip)) return true;
            return false;
        }
        if (ai >= ancestors.Count) return false;
        var m = PatternMatcher.Create(pat);
        if (!m.IsMatch(ancestors[ai]) && !m.IsMatch($"[0]{ancestors[ai]}.exe")) return false;
        return KillMatchChainRec(patterns, pi + 1, ancestors, ai + 1);
    }
}
