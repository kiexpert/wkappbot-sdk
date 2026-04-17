// PromptCommand.cs -- wkappbot prompt: direct prompt delivery to named Claude/Codex windows.
//
// Usage:
//   wkappbot prompt send "<name>" "<message>"                  immediate delivery
//   wkappbot prompt send "<name>" "<message>" --after 60s      deliver after delay
//   wkappbot prompt send "<name>" "<message>" --when-idle 9s   deliver when idle >= Ns
//   wkappbot prompt list                                        list all prompt windows
//
// <name> matched case-insensitively against displayName or cwdLabel
// (e.g. "WKAppBot", "클롣", "코뎃", "클롣[WG-WKAppBot]", "WG-WKAppBot")
//
// Duration format: 30 = 30s, 2m, 500ms, 1.1s, 2h  (same as --timeout)

using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int PromptCommand(string[] args)
    {
        if (args.Length == 0) return PromptUsage();

        return args[0].ToLowerInvariant() switch
        {
            "send" => PromptSend(args[1..]),
            "list" => PromptList(),
            _      => PromptUsage(),
        };
    }

    // -- list ----------------------------------------------------------------─

    static int PromptList()
    {
        using var ph = new ClaudePromptHelper();
        var all = ph.FindAllPrompts();
        if (all.Count == 0) { Console.WriteLine("(no prompt windows found)"); return 0; }

        Console.WriteLine($"{"#",-3} {"Display Name",-30} {"Host",-18} {"hwnd",-10} Title");
        Console.WriteLine(new string('-', 100));
        for (int i = 0; i < all.Count; i++)
        {
            var p = all[i];
            var (dn, _, _) = GetPromptDisplayInfo(p.WindowHandle);
            var t = p.WindowTitle.Length > 45 ? p.WindowTitle[..42] + "..." : p.WindowTitle;
            Console.WriteLine($"{i + 1,-3} {dn,-30} {p.HostType,-18} 0x{p.WindowHandle:X8} {t}");
        }
        return 0;
    }

    // -- send ----------------------------------------------------------------─

    /// <summary>
    /// wkappbot prompt send "&lt;name&gt;" "&lt;message&gt;" [--after &lt;duration&gt;] [--when-idle &lt;duration&gt;] [--timeout &lt;duration&gt;]
    /// </summary>
    static int PromptSend(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: wkappbot prompt send \"<name>\" \"<message>\" [--after 60s] [--when-idle 9s]");
            return 1;
        }

        var nameQuery = args[0];
        var message   = args[1];

        // Parse flags
        TimeSpan? after    = null;
        TimeSpan? whenIdle = null;
        TimeSpan  timeout  = TimeSpan.FromMinutes(30);

        for (int i = 2; i < args.Length - 1; i++)
        {
            if (args[i] == "--after")     after    = ParseDuration(args[++i]);
            else if (args[i] == "--when-idle") whenIdle = ParseDuration(args[++i]);
            else if (args[i] == "--timeout")   timeout  = ParseDuration(args[++i]);
        }

        // --after: wait then deliver
        if (after.HasValue)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Error.WriteLine($"[PROMPT] Waiting {after.Value.TotalSeconds:F0}s before delivering to \"{nameQuery}\"...");
            Console.ResetColor();
            System.Threading.Thread.Sleep(after.Value);
        }

        // --when-idle: poll until JSONL idle >= duration
        if (whenIdle.HasValue)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Error.WriteLine($"[PROMPT] Waiting for \"{nameQuery}\" to be idle >= {whenIdle.Value.TotalSeconds:F0}s (timeout={timeout.TotalMinutes:F0}m)...");
            Console.ResetColor();

            var deadline = DateTime.UtcNow + timeout;
            bool delivered = false;
            while (DateTime.UtcNow < deadline)
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    using var ph2 = new ClaudePromptHelper();
                    var candidates = ph2.FindAllPrompts()
                        .Select(p => (p, info: GetPromptDisplayInfo(p.WindowHandle)))
                        .Where(x =>
                            x.info.displayName.Contains(nameQuery, StringComparison.OrdinalIgnoreCase) ||
                            x.info.cwdLabel.Contains(nameQuery, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (candidates.Count == 0) continue;
                    var cand = candidates[0];

                    var (_, age) = GetContextInfoForCwd(cand.info.cwdLabel);
                    if ((age ?? TimeSpan.MaxValue) >= whenIdle.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Error.WriteLine($"[PROMPT] \"{nameQuery}\" idle {age!.Value.TotalSeconds:F0}s -- delivering");
                        Console.ResetColor();
                        delivered = true;
                        break;
                    }
                }
                catch { }
            }

            if (!delivered)
            {
                Console.Error.WriteLine($"[PROMPT] Timeout -- \"{nameQuery}\" did not become idle within {timeout.TotalMinutes:F0}m");
                return 1;
            }
        }

        // Deliver
        return PromptDeliver(nameQuery, message);
    }

    // -- deliver (core) ------------------------------------------------------─

    static int PromptDeliver(string nameQuery, string message)
    {
        // Unescape \n / \t so callers can embed escape sequences in string args
        message = message.Replace("\\n", "\n").Replace("\\t", "\t");

        using var ph = new ClaudePromptHelper();
        var all = ph.FindAllPrompts();
        if (all.Count == 0) { Console.Error.WriteLine("[PROMPT] No prompt windows found."); return 1; }

        var matched = all
            .Select(p => (p, info: GetPromptDisplayInfo(p.WindowHandle)))
            .Where(x => x.info.displayName.Contains(nameQuery, StringComparison.OrdinalIgnoreCase)
                     || x.info.cwdLabel.Contains(nameQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matched.Count == 0)
        {
            Console.Error.WriteLine($"[PROMPT] No window matched \"{nameQuery}\". Available:");
            foreach (var p in all)
            {
                var (dn, _, _) = GetPromptDisplayInfo(p.WindowHandle);
                Console.Error.WriteLine($"  {dn}  ({p.HostType})");
            }
            return 1;
        }

        int ok = 0;
        foreach (var item in matched)
        {
            var prompt   = item.p;
            var dispName = item.info.displayName;
            Console.Error.WriteLine($"[PROMPT] -> {dispName} (0x{prompt.WindowHandle:X})");
            try
            {
                ClaudePromptHelper.AllowFocusSteal = true;
                ph.TypeAndSubmit(prompt, message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Error.WriteLine($"[PROMPT] v Delivered to {dispName}");
                Console.ResetColor();
                ok++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"[PROMPT] X Failed ({dispName}): {ex.Message}");
                Console.ResetColor();
            }
        }
        return ok > 0 ? 0 : 1;
    }

    static int PromptUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  wkappbot prompt send \"<name>\" \"<msg>\"                   immediate");
        Console.WriteLine("  wkappbot prompt send \"<name>\" \"<msg>\" --after 60s        delayed");
        Console.WriteLine("  wkappbot prompt send \"<name>\" \"<msg>\" --when-idle 9s     on idle");
        Console.WriteLine("  wkappbot prompt list                                      list windows");
        Console.WriteLine();
        Console.WriteLine("  <name>: substring match on display name or CWD tag");
        Console.WriteLine("          e.g. \"WKAppBot\"  \"클롣\"  \"코뎃\"  \"클롣[WG-WKAppBot]\"");
        return 1;
    }
}
