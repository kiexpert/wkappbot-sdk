namespace WKAppBot.CLI;

// Shared output helpers for a11y find + ambiguity guard.
// Both paths emit the same FOCUS/TARGET code-fence format.
internal partial class Program
{
    // Print FOCUS code-fence block (stdout, dimmed).
    // paste  : quoted grap expression, e.g. "{hwnd:0x...,proc:'Code'}#absPath"
    // okMiss : "OK" / "MISS" / "?"
    // ms     : verify round-trip time
    static void PrintFocusBlock(string paste, string okMiss, long ms)
    {
        Console.WriteLine(Ansi.Dim("## FOCUS"));
        Console.WriteLine(Ansi.Dim("```"));
        Console.WriteLine(Ansi.Dim(paste));
        Console.WriteLine(Ansi.Dim($"{Ansi.Mark(okMiss)} {ms}ms"));
        Console.WriteLine(Ansi.Dim("```"));
        Console.WriteLine();
    }

    // Print TARGET code-fence block (stdout, cyan).
    // titleHeading : window title used as ## heading
    // paste        : quoted grap expression
    // action       : "find" / "read" / "click" etc. — used in command hint line
    // extraArgs    : appended to command hint line (e.g. --eval-js "...")
    // okMiss       : "OK" / "MISS"
    // ms           : verify round-trip time
    // matchNote    : e.g. "  ← cmd: chatgpt.com" or "" for clean match
    static void PrintTargetBlock(string titleHeading, string paste, string action,
        string[]? extraArgs, string okMiss, long ms, string matchNote = "")
    {
        var cmdLine = $"wkappbot a11y {action} {paste}";
        if (extraArgs != null && extraArgs.Length > 0)
            cmdLine += " " + string.Join(" ", extraArgs);
        var verifyLine = $"{Ansi.Mark(okMiss)} {ms}ms{Ansi.Dim(matchNote)}";

        Console.WriteLine(Ansi.TargetLine($"## {titleHeading}"));
        Console.WriteLine(Ansi.TargetLine("```"));
        Console.WriteLine(Ansi.TargetLine(paste));
        Console.WriteLine(Ansi.TargetLine(cmdLine));
        Console.WriteLine(Ansi.TargetLine(verifyLine));
        Console.WriteLine(Ansi.TargetLine("```"));
        Console.WriteLine();
    }
}
