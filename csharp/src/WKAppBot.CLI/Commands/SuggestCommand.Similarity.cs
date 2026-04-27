using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

// Partial: post-submit similarity nudge. After a `wkappbot suggest "..."`
// is filed, scan open suggests for word-overlap with the new title and
// Show a ready-to-run copy-paste merge command if any overlap >=40%. Silent when no
// matches. Read-only; uses Program.DataDir/suggestions.jsonl.
internal partial class Program
{
    /// <summary>
    /// Print a SIMILAR nudge if any open suggest's title overlaps the new
    /// suggest's title by >=40% (word-set overlap, stop-words removed).
    /// Top 3 matches max; silent when none. Never throws.
    /// </summary>
    /// <param name="newText">Full text of the suggest just submitted.</param>
    /// <param name="newTs">ISO ts of the new entry (excluded from matches).</param>
    static void SuggestSimilarityNudge(string newText, string newTs)
    {
        try
        {
            var newTitle = ExtractSuggestTitle(newText);
            if (string.IsNullOrWhiteSpace(newTitle)) return;

            var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
            if (!File.Exists(jsonlPath)) return;

            var matches = new List<(string ts, string title, double pct)>();
            foreach (var line in File.ReadAllLines(jsonlPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                JsonNode? node;
                try { node = JsonSerializer.Deserialize<JsonNode>(line); }
                catch { continue; }
                if (node == null) continue;

                // Skip resolved/done and merge records.
                var reviewStatus = node["review_status"]?.GetValue<string>();
                var status = node["status"]?.GetValue<string>();
                if (reviewStatus == "done" || status == "done") continue;
                var entryType = node["type"]?.GetValue<string>();
                if (entryType == "merge") continue;

                var ts = node["ts"]?.GetValue<string>() ?? "";
                if (string.IsNullOrEmpty(ts) || ts == newTs) continue;

                var text = node["text"]?.GetValue<string>() ?? "";
                var title = ExtractSuggestTitle(text);
                if (string.IsNullOrWhiteSpace(title)) continue;

                var overlap = WordOverlap(newTitle, title);
                if (overlap >= 0.40)
                    matches.Add((ts, title, overlap));
            }

            if (matches.Count == 0) return;

            var top = matches.OrderByDescending(m => m.pct).Take(3).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[SUGGEST:SIMILAR] {top.Count} related suggest(s) found -- consider merging:");
            foreach (var m in top)
            {
                var tsShort = m.ts.Length >= 16 ? m.ts[..16] : m.ts;
                var titlePrev = m.title.Length > 70 ? m.title[..70] + "..." : m.title;
                Console.WriteLine($"  ts={tsShort} \"{titlePrev}\" ({(m.pct * 100):F0}% match)");
            }
            Console.WriteLine();
            Console.WriteLine("Ready to merge:");
            // Pick a 2-3 word pattern from the new title for --all-matching.
            var pattern = BuildMergePattern(newTitle);
            var mergedTitle = newTitle.Length > 60 ? newTitle[..60] : newTitle;
            Console.WriteLine($"  wkappbot suggest merge --all-matching \"{pattern}\" --title \"{mergedTitle}\" --work \"2h\"");
            Console.ResetColor();
        }
        catch
        {
            // Nudge is best-effort: never break submit flow on failure.
        }
    }

    /// <summary>
    /// Extract the human-readable title from a suggest text body.
    /// Mirrors SuggestResolveCommand: if first line is the literal "--title"
    /// flag marker, the title is on the next line; otherwise it's line 1.
    /// </summary>
    static string ExtractSuggestTitle(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";
        var lines = text.Split('\n');
        var first = lines[0].Trim();
        if (first.Equals("--title", StringComparison.OrdinalIgnoreCase) && lines.Length > 1)
            return lines[1].Trim();
        return first;
    }

    /// <summary>
    /// Word-set overlap ratio between two titles. Stopwords removed.
    /// Result = |A ∩ B| / min(|A|, |B|), so a short title fully contained
    /// in a long one scores 1.0 (intentional, helps catch duplicates).
    /// </summary>
    static double WordOverlap(string a, string b)
    {
        var stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { "the", "and", "for", "with", "this", "that", "from", "into",
          "wkappbot", "suggest" };
        var wa = SimilarityTokenize(a)
            .Except(stopWords, StringComparer.OrdinalIgnoreCase)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        var wb = SimilarityTokenize(b)
            .Except(stopWords, StringComparer.OrdinalIgnoreCase)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (wa.Count == 0 || wb.Count == 0) return 0;
        return (double)wa.Intersect(wb, StringComparer.OrdinalIgnoreCase).Count()
             / Math.Min(wa.Count, wb.Count);
    }

    static IEnumerable<string> SimilarityTokenize(string s)
        => Regex.Split(s ?? "", @"[^A-Za-z0-9]+").Where(w => w.Length >= 3);

    /// <summary>
    /// Build a short --all-matching pattern from the new title: pick the
    /// 2 longest non-stopword tokens joined by a space. Falls back to the
    /// first token, then to the raw title.
    /// </summary>
    static string BuildMergePattern(string title)
    {
        var stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { "the", "and", "for", "with", "this", "that", "from", "into",
          "wkappbot", "suggest" };
        var tokens = SimilarityTokenize(title)
            .Where(w => !stopWords.Contains(w))
            .OrderByDescending(w => w.Length)
            .Take(2)
            .ToArray();
        if (tokens.Length >= 2) return $"{tokens[0]} {tokens[1]}";
        if (tokens.Length == 1) return tokens[0];
        return title.Length > 40 ? title[..40] : title;
    }
}
