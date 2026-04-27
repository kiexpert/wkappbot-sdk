// SuggestCommand.PendingBanner.cs -- Top-of-output banner for half-resolved suggests.
//
// Whenever any `wkappbot suggest <subcommand>` runs, we surface a prominent
// banner listing entries currently in `half_resolved` state (1/2 of co-resolve
// done, awaiting the original author's confirm). Without the banner, those
// pending items are easy to forget -- the original suggest thread lives in
// another channel, and `suggest list` previously showed nothing about them.
//
// The banner prints to stdout BEFORE any subcommand dispatches. It must be
// cheap (one file read) and never crash the underlying command if the file
// is missing/corrupt.

using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Print the pending co-resolve banner if any half_resolved entries exist.
    /// Called at the top of SuggestCommand BEFORE subcommand dispatch.
    /// Silent when nothing is pending.
    /// </summary>
    static void PrintPendingCoResolveBanner()
    {
        try
        {
            var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
            if (!File.Exists(jsonlPath)) return;

            var pending = new List<(string ts, string from, string cwd, string title)>();
            foreach (var line in File.ReadLines(jsonlPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                JsonNode? node;
                try { node = JsonNode.Parse(line); }
                catch { continue; }
                if (node is not JsonObject e) continue;

                var halfBy = e["half_resolved_by"]?.GetValue<string>();
                if (string.IsNullOrEmpty(halfBy)) continue;
                if (e["resolved_at"] != null) continue; // defensive: already finalized

                var ts    = e["ts"]?.GetValue<string>() ?? "";
                var from  = e["from"]?.GetValue<string>() ?? "?";
                var cwd   = e["cwd"]?.GetValue<string>() ?? "";
                var title = e["text"]?.GetValue<string>() ?? e["title"]?.GetValue<string>() ?? "";
                pending.Add((ts, from, cwd, title));
            }

            if (pending.Count == 0) return;

            const int innerWidth = 60;
            string Pad(string s) => s.Length >= innerWidth ? s[..innerWidth] : s + new string(' ', innerWidth - s.Length);

            var prevColor = Console.ForegroundColor;
            try { Console.ForegroundColor = ConsoleColor.Yellow; } catch { }

            Console.WriteLine("╔" + new string('═', innerWidth) + "╗");
            var header = $"  ⏳ PENDING CO-RESOLVE  ({pending.Count} item(s) waiting for confirm)";
            Console.WriteLine("║" + Pad(header) + "║");
            Console.WriteLine("╠" + new string('═', innerWidth) + "╣");

            foreach (var (ts, from, cwd, title) in pending)
            {
                var fromTag = !string.IsNullOrEmpty(from) ? from : AbbreviateCwd(cwd);
                if (string.IsNullOrEmpty(fromTag)) fromTag = "?";
                var tsPrefix = ts.Length >= 19 ? ts[..19] : ts;
                var line1 = $" {fromTag}: wkappbot suggest resolve {tsPrefix} --confirm";
                Console.WriteLine("║" + Pad(line1) + "║");

                var sq = SquashTitle(title);
                if (sq.Length > innerWidth - 5) sq = sq[..(innerWidth - 8)] + "...";
                var line2 = $"   \"{sq}\"";
                Console.WriteLine("║" + Pad(line2) + "║");
            }

            Console.WriteLine("╠" + new string('─', innerWidth) + "╣");
            Console.WriteLine("║" + Pad(" 💡 아직 확인 전이라면 요구사항을 추가할 수 있어요:") + "║");
            Console.WriteLine("║" + Pad("   wkappbot suggest add-requirement <ts> \"cmd => ok\"") + "║");
            Console.WriteLine("║" + Pad("   [--skill <id>]  -- 연결된 스킬도 즉시 업데이트") + "║");
            Console.WriteLine("╚" + new string('═', innerWidth) + "╝");
            try { Console.ForegroundColor = prevColor; } catch { }
            Console.WriteLine();
        }
        catch
        {
            // Banner failure must never break the underlying suggest command.
        }
    }

    static string SquashTitle(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        var sb = new System.Text.StringBuilder(s.Length);
        bool prevWs = false;
        foreach (var c in s)
        {
            if (char.IsWhiteSpace(c))
            {
                if (!prevWs) sb.Append(' ');
                prevWs = true;
            }
            else
            {
                sb.Append(c);
                prevWs = false;
            }
        }
        return sb.ToString().Trim();
    }
}
