using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// msaa-probe: Probe MSAA accessibility tree at a specific point in a window.
// Usage: wkappbot msaa-probe <grap> <relX> <relY> [--depth N]
// Dumps MSAA role/name/value hierarchy, tests put_accValue writability.
// Used to investigate VS Code webview accessibility for Claude Code input.
internal partial class Program
{
    static int MsaaProbeCommand(string[] args)
    {
        if (args.Length < 3)
            return Error("Usage: wkappbot msaa-probe <window-title> <relX> <relY> [--depth N] [--write \"text\"]");

        if (!int.TryParse(args[1], out int relX) || !int.TryParse(args[2], out int relY))
            return Error("Invalid coordinates.");

        int depth = 6;
        var depthStr = GetArgValue(args, "--depth");
        if (depthStr != null && int.TryParse(depthStr, out int d)) depth = d;

        var writeText = GetArgValue(args, "--write");

        var (win32Segments, _) = GrapHelper.SplitGrap(args[0]);
        if (win32Segments.Length == 0) return Error("Empty grap pattern");

        var found = WindowFinder.FindWindows(win32Segments[0]);
        if (found.Count == 0)
            return Error($"Window not found: \"{win32Segments[0]}\"");

        var hWnd = found[0].Handle;
        NativeMethods.GetWindowRect(hWnd, out var rect);
        int absX = rect.Left + relX;
        int absY = rect.Top + relY;

        Console.Error.WriteLine($"[MSAA-PROBE] Window: 0x{hWnd:X} at ({rect.Left},{rect.Top})");
        Console.Error.WriteLine($"[MSAA-PROBE] Probing: rel=({relX},{relY}) abs=({absX},{absY})");

        if (writeText != null)
        {
            Console.Error.WriteLine($"[MSAA-PROBE] Write mode: \"{writeText}\" — will write to L0..L3 and wait 5s");
            ClaudePromptHelper.ProbeMsaaWrite(absX, absY, writeText);
        }
        else
        {
            Console.Error.WriteLine($"[MSAA-PROBE] Walk depth: {depth}");
            ClaudePromptHelper.ProbeMsaaAtPoint(absX, absY, depth);
        }
        return 0;
    }
}
