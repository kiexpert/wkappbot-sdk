using FlaUI.Core.AutomationElements;

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
    // action       : "find" / "read" / "click" etc. -- used in command hint line
    // extraArgs    : appended to command hint line (e.g. --eval-js "...")
    // okMiss       : "OK" / "MISS"
    // ms           : verify round-trip time
    // matchNote    : e.g. "  <- cmd: chatgpt.com" or "" for clean match
    // leafTag      : leaf node XML tag, e.g. <Edit ltwh=... aid='prompt'>텍스트</Edit>
    static void PrintTargetBlock(string titleHeading, string paste, string action,
        string[]? extraArgs, string okMiss, long ms, string matchNote = "", string leafTag = "")
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
        if (!string.IsNullOrEmpty(leafTag))
            Console.WriteLine(Ansi.Dim(leafTag));
        Console.WriteLine(Ansi.TargetLine("```"));
        Console.WriteLine();
    }

    // Build XML-like leaf node tag with all useful properties.
    // <Edit ltwh=100,200,800,50 aid='prompt' name='입력' val='' patterns='Invoke,Value'/>
    // or <Edit ltwh=... aid='prompt'>텍스트 내용</Edit>
    static string FormatLeafTag(AutomationElement el)
    {
        try
        {
            var ct   = el.Properties.ControlType.ValueOrDefault.ToString();
            var aid  = el.Properties.AutomationId.ValueOrDefault ?? "";
            var name = el.Properties.Name.ValueOrDefault ?? "";

            var sb = new System.Text.StringBuilder();
            sb.Append($"<{ct}");

            try
            {
                var r = el.Properties.BoundingRectangle.ValueOrDefault;
                sb.Append($" ltwh={(int)r.X},{(int)r.Y},{(int)r.Width},{(int)r.Height}");
            }
            catch { }

            if (!string.IsNullOrEmpty(aid))  sb.Append($" aid='{aid}'");
            if (!string.IsNullOrEmpty(name)) sb.Append($" name='{(name.Length > 40 ? name[..37] + "..." : name)}'");

            // Value pattern
            string? val = null;
            try { val = el.Patterns.Value.PatternOrDefault?.Value.ValueOrDefault; } catch { }
            if (val != null) sb.Append($" val='{(val.Length > 40 ? val[..37] + "..." : val)}'");

            // Supported patterns
            var pats = GetSupportedPatternNames(el);
            if (pats.Count > 0) sb.Append($" patterns='{string.Join(",", pats)}'");

            // Body text: value > text pattern > name (only if no aid/name already shown)
            string? body = val ?? "";
            if (string.IsNullOrEmpty(body))
            {
                try
                {
                    body = el.Patterns.Text.PatternOrDefault?
                        .DocumentRange.GetText(100).Trim();
                }
                catch { body = ""; }
            }

            if (!string.IsNullOrEmpty(body))
            {
                var escaped = body.Replace("<", "&lt;").Replace(">", "&gt;");
                if (escaped.Length > 60) escaped = escaped[..57] + "...";
                sb.Append($">{escaped}</{ct}>");
            }
            else
            {
                sb.Append("/>");
            }

            return sb.ToString();
        }
        catch { return ""; }
    }
}
