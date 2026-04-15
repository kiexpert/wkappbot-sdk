namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot hotswap [exe-path] — apply .new.exe rename-swap for any wkappbot binary.
    /// Uses shared TryRenameSwap — same logic as Eye main loop + startup gentle-swap.
    /// </summary>
    static int HotSwapCommand(string[] args)
    {
        if (args.Length > 0 && args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage: wkappbot hotswap [exe-path]");
            Console.WriteLine("  Apply .new.exe rename-swap for a wkappbot binary.");
            Console.WriteLine("  Default: wkappbot-core.exe in the SDK bin directory.");
            Console.WriteLine("  Identical .new.exe → delete. Different → rename-swap.");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot hotswap                    # swap wkappbot-core.exe");
            Console.WriteLine("  wkappbot hotswap D:/SDK/bin/a11y.exe # swap specific binary");
            return 0;
        }

        var targetExe = args.Length > 0 && !args[0].StartsWith('-')
            ? args[0]
            : Path.Combine(Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot-core.exe");

        if (!File.Exists(targetExe))
        {
            Console.Error.WriteLine($"[HOTSWAP] Target not found: {targetExe}");
            return 1;
        }

        Console.Error.WriteLine($"[HOTSWAP] Target: {targetExe}");
        var result = TryRenameSwap(targetExe, "HOTSWAP");

        switch (result)
        {
            case HotSwapResult.NoNewExe:
                Console.WriteLine("[HOTSWAP] No .new.exe found — nothing to do");
                return 0;
            case HotSwapResult.Identical:
                Console.WriteLine("[HOTSWAP] Identical — .new.exe deleted");
                return 0;
            case HotSwapResult.Swapped:
                Console.WriteLine("[HOTSWAP] Swap complete — new binary active");
                return 0;
            default:
                Console.WriteLine("[HOTSWAP] Swap failed — rolled back");
                return 1;
        }
    }
}
