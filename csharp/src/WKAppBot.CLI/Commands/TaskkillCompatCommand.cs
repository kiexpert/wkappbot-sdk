// TaskkillCompatCommand.cs -- Windows taskkill.exe compat shim.
//
// Invoked as taskkill.exe (busybox symlink) OR directly as `wkappbot taskkill`.
// Translates classic taskkill args to wkappbot's `a11y kill <pattern>` form
// so scripts + AI agents that reach for taskkill land on the unified
// accessibility-first kill path (with FocusLaunchTracker awareness, focusless
// guards, Eye/WhisperRing preservation) instead of raw OS TerminateProcess.
//
// Supported taskkill flags (subset, common usage):
//   /F              force -- ignored (wkappbot a11y kill is already force)
//   /T              tree  -- mapped to ancestor-chain semantic where possible
//   /PID <n>        by pid -- one or repeated
//   /IM  <name>     by image name -- one or repeated
//   /FI  "filter"   filter -- not supported; warn and fall through
// Bare positionals:
//   - all-digits                 -> treated as /PID
//   - anything ending in .exe    -> treated as /IM
//   - otherwise                  -> passed through as a pattern (substring)
//
// Multiple targets are joined with ';' (a11y grap OR syntax) so one call
// evicts all of them in one a11y kill sweep.

namespace WKAppBot.CLI;

internal partial class Program
{
    static int TaskkillCompatCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "/?" or "--help" or "help")
            return TaskkillUsage();

        var targets = new List<string>();
        bool tree = false;
        bool fiWarned = false;

        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (string.IsNullOrEmpty(a)) continue;

            // Flags tolerate both / and - prefix, case-insensitive.
            string? flag = (a.StartsWith('/') || a.StartsWith('-')) ? a[1..].ToUpperInvariant() : null;

            if (flag == "F") continue; // force -- a11y kill is force by default
            if (flag == "T") { tree = true; continue; }
            if (flag == "PID" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var pid) && pid > 0)
                    targets.Add($"{{pid:{pid}}}");
                continue;
            }
            if (flag == "IM" && i + 1 < args.Length)
            {
                var name = StripExe(args[++i]);
                if (!string.IsNullOrWhiteSpace(name))
                    targets.Add($"{{proc:'{EscapeSingleQuote(name)}'}}");
                continue;
            }
            if (flag == "FI")
            {
                if (!fiWarned)
                {
                    Console.Error.WriteLine("[TASKKILL] /FI filter syntax is not supported by the wkappbot compat shim.");
                    Console.Error.WriteLine("  Use grap directly: wkappbot a11y kill \"{proc:'...',title:'...'}\"");
                    fiWarned = true;
                }
                if (i + 1 < args.Length) i++; // consume filter expression
                continue;
            }
            // Unknown slash/dash flag -- pass warning and move on.
            if (flag != null)
            {
                Console.Error.WriteLine($"[TASKKILL] unknown flag '{a}' ignored (taskkill-compat shim is a subset).");
                continue;
            }

            // Bare positional: digit-only -> pid, *.exe -> image name, else pattern.
            if (a.All(char.IsDigit) && int.TryParse(a, out var bpid) && bpid > 0)
                targets.Add($"{{pid:{bpid}}}");
            else if (a.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                targets.Add($"{{proc:'{EscapeSingleQuote(StripExe(a))}'}}");
            else
                targets.Add(a); // pattern / glob as-is
        }

        if (targets.Count == 0)
        {
            Console.Error.WriteLine("[TASKKILL] no target specified.");
            return TaskkillUsage();
        }

        // /T tree-kill maps to a11y kill ancestor syntax with a '/' separator,
        // but ancestor semantics are inverted (kill A whose ancestor matches B).
        // For a tree-down approximation we just trust a11y kill's own child-
        // process sweep; leave pattern alone and note the caller asked for tree.
        if (tree)
            Console.Error.WriteLine("[TASKKILL] /T noted -- wkappbot a11y kill terminates the matched process' child tree by default.");

        var grap = string.Join(';', targets);
        Console.Error.WriteLine($"[TASKKILL] -> wkappbot a11y kill \"{grap}\"");

        // Delegate to the real a11y kill pipeline so focus/Eye/WhisperRing
        // guards apply. --force suppresses the interactive ambiguity prompt
        // which taskkill never had anyway.
        return A11yCommand(new[] { "kill", grap, "--force" });
    }

    static int TaskkillUsage()
    {
        Console.WriteLine("Usage: taskkill [/F] [/T] {/PID <n> | /IM <name>}+");
        Console.WriteLine("       taskkill <pid-or-name> [more...]");
        Console.WriteLine("Translates to: wkappbot a11y kill \"<grap>\" (grap = ; -joined targets)");
        Console.WriteLine("Supported flags: /F (force, default), /T (tree, noted), /PID, /IM. /FI filter not supported.");
        Console.WriteLine("Bare positional: digits -> pid, *.exe -> image name, else treated as grap pattern.");
        Console.WriteLine("Examples:");
        Console.WriteLine("  taskkill /F /PID 1234             # kill by pid");
        Console.WriteLine("  taskkill /IM notepad.exe          # kill all notepad instances");
        Console.WriteLine("  taskkill 1234 5678                # multiple pids");
        Console.WriteLine("  taskkill notepad.exe chrome.exe   # multiple image names");
        return 1;
    }

    static string StripExe(string s) =>
        s.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ? s[..^4] : s;

    static string EscapeSingleQuote(string s) => s.Replace("'", "\\'");
}
