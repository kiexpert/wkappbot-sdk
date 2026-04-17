using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // === Kill-by-Pattern: close --kill ===
    // Kills processes matching grap pattern. No window needed.
    // Search key per process: "[pid]processName.exe" -- substring match by default (no * needed).
    // Supports:
    //   "wkappbot-core"            -> any process named wkappbot-core (substring)
    //   "node/wkappbot-core"       -> wkappbot-core whose direct parent is node
    //   "flutter/node/wkappbot-core" -> chain: flutter->node->wkappbot-core
    //   "**/wkappbot-core"         -> wkappbot-core with any ancestor (any depth)
    //   "wkappbot-core#regex:lucy" -> '#' splits name#exePathFilter
    // If process has a window -> WM_CLOSE first then Kill; else -> Kill directly.
    // KillCandidate: a process that passed all guards and is eligible to kill.
    record KillCandidate(System.Diagnostics.Process Proc, string NodeKey, string CmdLine, string CmdBrief);

    static int A11yKillByPattern(string grap, bool allowAncestors, bool dryRun = false, string? argFilter = null, string? nthRaw = null)
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
        // PID->processName lookup for ancestor chain resolution
        var pidToName = allProcs.GroupBy(p => p.Id)
            .ToDictionary(g => g.Key, g => g.First().ProcessName);

        var selfPids = allowAncestors ? new HashSet<int>() : GetSelfAndAncestorPids();
        var candidates = new List<KillCandidate>();
        var skipped = new List<string>();

        // -- Pass 1: collect all eligible kill targets (no actual killing yet) --
        foreach (var p in allProcs)
        {
            try
            {
                var procName = p.ProcessName;
                string exePath = "";
                try { exePath = p.MainModule?.FileName ?? ""; } catch { }

                // Search key: "[pid]processName.exe" -- substring by default
                var nodeKey = $"[{p.Id}]{procName}.exe";

                // cmdLine is populated lazily: only fetched on name-miss (perf),
                // then reused for protection checks, argFilter, and display.
                string cmdLine = "";

                if (!targetMatcher.IsMatch(nodeKey) && !targetMatcher.IsMatch(procName))
                {
                    // Last chance: cmdline search -- token-AND for plain multi-word patterns (order-independent)
                    // e.g. "tick eye" matches "wkappbot eye tick --timeout 15" regardless of token order
                    try { cmdLine = NativeMethods.GetProcessCommandLine(p.Id) ?? ""; } catch { }
                    if (!KillCmdLineMatch(cmdLine, targetPattern)) continue;

                    // Self-kill guard: pattern matched only via cmdLine of a wkappbot-core process.
                    // Two cases:
                    //   (A) wkappbot-core running as a daemon (e.g. "screensaver", "whisper-ring") --
                    //       first arg matches the pattern -> legitimate kill target, let it proceed.
                    //   (B) wkappbot-core running a command (e.g. a11y hack "*LG*") and the pattern
                    //       "LG" only matched its command args -- false positive, skip without bug.
                    if (procName.Equals("wkappbot-core", StringComparison.OrdinalIgnoreCase)
                        && !targetPattern.Contains("wkappbot", StringComparison.OrdinalIgnoreCase))
                    {
                        var brief = cmdLine.Length > 120 ? cmdLine[..120] + "..." : cmdLine;
                        // If the matching wkappbot-core is ourselves (or an ancestor), skip silently.
                        if (selfPids.Contains(p.Id))
                        {
                            skipped.Add($"[{p.Id}]{procName} (self-skip)");
                            continue;
                        }
                        // If the pattern matches the FIRST positional arg (daemon subcommand), allow kill.
                        // e.g. "wkappbot-core screensaver ..." -> pattern "screensaver" -> legitimate target.
                        var firstArg = ExtractFirstCmdArg(cmdLine);
                        bool isDaemonTarget = !string.IsNullOrEmpty(firstArg) && targetMatcher.IsMatch(firstArg);
                        if (!isDaemonTarget)
                        {
                            // Pattern only hit command args, not the daemon name -- false positive guard.
                            Console.Error.WriteLine($"[KILL] [{p.Id}]{procName} -- SKIP (self-kill guard: \"{targetPattern}\" only matched cmd args)\n       cmd: {brief}");
                            skipped.Add($"[{p.Id}]{procName} (self-kill-guard)");
                            continue;
                        }
                        // isDaemonTarget=true: fall through to kill the daemon process
                    }
                }

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

                // Get command line for display + protection + --arg filter (may already be populated above)
                if (string.IsNullOrEmpty(cmdLine))
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
                    Console.Error.WriteLine($"[KILL] [{p.Id}]{procName} -- SKIP (eye-child: {cmdBrief})");
                    continue;
                }

                // Protect WebBot Chrome/Edge: processes with --remote-debugging-port are active CDP sessions.
                // Skip automatically unless user explicitly targeted them with --arg=remote-debugging-port=N.
                if (argFilter == null && !string.IsNullOrEmpty(cmdLine) &&
                    (procName.Equals("chrome", StringComparison.OrdinalIgnoreCase) ||
                     procName.Equals("msedge", StringComparison.OrdinalIgnoreCase)))
                {
                    var m = System.Text.RegularExpressions.Regex.Match(cmdLine, @"--remote-debugging-port=(\d+)");
                    if (m.Success)
                    {
                        skipped.Add($"[{p.Id}]{procName} (webbot-cdp:port={m.Groups[1].Value})");
                        Console.Error.WriteLine($"[KILL] [{p.Id}]{procName} -- SKIP (WebBot CDP port={m.Groups[1].Value}). Use --arg=remote-debugging-port={m.Groups[1].Value} to force.");
                        continue;
                    }
                }

                candidates.Add(new KillCandidate(p, nodeKey, cmdLine, cmdBrief));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[KILL] pid={p.Id} scan failed: {ex.Message}");
            }
        }

        if (skipped.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (var s in skipped)
                Console.Error.WriteLine($"[GUARD] skipped {s} -- use --allow-ancestors to override");
            Console.ResetColor();
        }

        if (candidates.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[KILL] no matching processes for \"{grap}\"");
            Console.ResetColor();
            return 1;
        }

        // -- Ambiguity guard: multiple candidates without --nth -> show find-mode list --
        if (candidates.Count > 1 && nthRaw == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[KILL] ambiguous -- {candidates.Count} processes match \"{grap}\". Use --nth N to target one:");
            Console.ResetColor();
            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                Console.WriteLine($"  [{i + 1}] {c.NodeKey}");
                if (!string.IsNullOrEmpty(c.CmdBrief))
                    Console.WriteLine($"       {c.CmdBrief}");
            }
            return 1;
        }

        // -- Pass 2: resolve --nth selection, then kill --
        IEnumerable<KillCandidate> targets;
        if (nthRaw != null && candidates.Count > 1)
        {
            // Parse --nth (supports comma, range, tilde) into 1-based index list
            var picked = new List<KillCandidate>();
            var seen = new HashSet<int>();
            bool parseOk = true;
            foreach (var term in nthRaw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                var idxList = ParseNthIndexes(term, candidates.Count);
                if (idxList == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"[KILL] --nth \"{term}\" is invalid or out of range (1~{candidates.Count})");
                    Console.ResetColor();
                    parseOk = false;
                    break;
                }
                foreach (var idx in idxList)
                    if (seen.Add(idx)) picked.Add(candidates[idx - 1]);
            }
            if (!parseOk) return 1;
            targets = picked;
        }
        else
        {
            targets = candidates;
        }

        // [READINESS] Probe before destructive kill (magnifier + blocker + yield popup).
        // Use first target's main window when available; kill is not focusless but the
        // contract is "every a11y action runs the input-position-guard before firing."
        if (!dryRun)
        {
            try
            {
                var firstHwnd = targets
                    .Select(c => { try { return c.Proc.MainWindowHandle; } catch { return IntPtr.Zero; } })
                    .FirstOrDefault(h => h != IntPtr.Zero && NativeMethods.IsWindow(h));
                EnsureA11yReadiness(firstHwnd, "kill");
            }
            catch { /* best-effort */ }
        }

        var killed = new List<string>();
        foreach (var c in targets)
        {
            try
            {
                var p = c.Proc;
                var procName = p.ProcessName;

                if (dryRun)
                {
                    Console.Error.WriteLine($"[KILL:DRY] {c.NodeKey} -- {c.CmdBrief}");
                    killed.Add(c.NodeKey);
                    continue;
                }

                var mainHwnd = p.MainWindowHandle;
                bool hasWindow = mainHwnd != IntPtr.Zero && NativeMethods.IsWindow(mainHwnd);
                if (hasWindow)
                {
                    Console.Error.WriteLine($"[KILL] {c.NodeKey} -- window found, sending WM_CLOSE\n       cmd: {c.CmdBrief}");
                    NativeMethods.SendMessageTimeoutW(mainHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 2000, out _);
                    Thread.Sleep(800);
                    try { p.Refresh(); } catch { }
                    if (p.HasExited)
                    {
                        Console.Error.WriteLine($"[KILL] {c.NodeKey} -- exited gracefully");
                        killed.Add(c.NodeKey);
                        continue;
                    }
                    Console.Error.WriteLine($"[KILL] {c.NodeKey} -- still alive, force kill");
                }
                else
                {
                    Console.Error.WriteLine($"[KILL] {c.NodeKey} -- no window, force kill\n       cmd: {c.CmdBrief}");
                }
                p.Kill(entireProcessTree: false);
                killed.Add(c.NodeKey);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[KILL] {c.NodeKey} failed: {ex.Message}");
            }
        }

        if (killed.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[KILL] nothing killed for \"{grap}\"");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[KILL] killed {killed.Count}: {string.Join(", ", killed)}");
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

    // Cmdline match for kill -- delegates to shared PatternMatcher.TokenMatchAny
    static bool KillCmdLineMatch(string cmdLine, string pattern) =>
        PatternMatcher.TokenMatchAny(pattern, cmdLine);

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

    // Extracts the first positional argument from a Windows command line string.
    // e.g. `"C:\bin\wkappbot-core.exe" "screensaver" "1400"` -> "screensaver"
    static string ExtractFirstCmdArg(string cmdLine)
    {
        if (string.IsNullOrWhiteSpace(cmdLine)) return "";
        // Simple tokenizer: skip the exe (first quoted or unquoted token), return the next.
        int i = 0;
        // Skip the executable token
        if (i < cmdLine.Length && cmdLine[i] == '"')
        {
            i++; // skip opening quote
            while (i < cmdLine.Length && cmdLine[i] != '"') i++;
            if (i < cmdLine.Length) i++; // skip closing quote
        }
        else
        {
            while (i < cmdLine.Length && cmdLine[i] != ' ') i++;
        }
        // Skip whitespace
        while (i < cmdLine.Length && cmdLine[i] == ' ') i++;
        if (i >= cmdLine.Length) return "";
        // Read the next token (first arg)
        if (cmdLine[i] == '"')
        {
            i++;
            int start = i;
            while (i < cmdLine.Length && cmdLine[i] != '"') i++;
            return cmdLine[start..i];
        }
        else
        {
            int start = i;
            while (i < cmdLine.Length && cmdLine[i] != ' ') i++;
            return cmdLine[start..i];
        }
    }
}
