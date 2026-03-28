using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Write ask result to a Markdown file in .wkappbot/ask/ folder.
    /// Works for both single AI asks and triad sessions.
    /// File: .wkappbot/ask/{ai}-{date}-{time}-{topic-slug}.md
    /// </summary>
    static string? WriteAskMd(string ai, string question, string answer)
    {
        try
        {
            var cwd = EyeCmdPipeServer.CallerCwd.Value;
            if (string.IsNullOrEmpty(cwd) || !Directory.Exists(Path.Combine(cwd, ".git")))
            {
                // Walk up from exe dir to find git root
                var probe = Path.GetDirectoryName(Environment.ProcessPath) ?? Environment.CurrentDirectory;
                for (int i = 0; i < 10 && !string.IsNullOrEmpty(probe); i++)
                {
                    if (Directory.Exists(Path.Combine(probe, ".git"))) { cwd = probe; break; }
                    probe = Path.GetDirectoryName(probe);
                }
                if (string.IsNullOrEmpty(cwd)) cwd = Environment.CurrentDirectory;
            }

            var askDir = Path.Combine(cwd, ".wkappbot", "ask");
            Directory.CreateDirectory(askDir);

            // Build topic slug from question (first 40 chars, sanitized)
            var rawQ = question;
            // Strip [G:...] prefix if present
            var gMatch = Regex.Match(rawQ, @"^\[G:[^\]]+\]\s*");
            if (gMatch.Success) rawQ = rawQ[gMatch.Length..];

            var slug = BuildSlug(rawQ, 40);
            var ts = DateTime.Now.ToString("yyyyMMdd-HHmm");
            var fileName = $"{ts}-{ai.ToLowerInvariant()}-{slug}.md";
            var mdPath = Path.Combine(askDir, fileName);

            var sb = new StringBuilder();
            sb.AppendLine($"# Ask {ai.ToUpperInvariant()}");
            sb.AppendLine();
            sb.AppendLine($"> **Question**: {rawQ}");
            sb.AppendLine($"> **Time**: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"> **AI**: {ai}");
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();

            // Parse DEBATE_JSON if present
            var djMatch = Regex.Match(answer, @"\[DEBATE_JSON\](.*?)\[/DEBATE_JSON\]", RegexOptions.Singleline);
            if (djMatch.Success)
            {
                try
                {
                    var dj = JsonNode.Parse(djMatch.Groups[1].Value);
                    var role = dj?["role"]?.GetValue<string>();
                    var position = dj?["position"]?.GetValue<string>();
                    var stance = dj?["stance"];

                    if (role != null) sb.AppendLine($"**Role**: {role}");
                    if (stance != null)
                        sb.AppendLine($"**Stance**: N={stance["N"]} R={stance["R"]} C={stance["C"]} E={stance["E"]} D={stance["D"]}");
                    if (position != null) { sb.AppendLine(); sb.AppendLine($"**Position**: {position}"); }
                    sb.AppendLine();

                    var claims = dj?["claims"]?.AsArray();
                    if (claims?.Count > 0)
                    {
                        sb.AppendLine("## Claims");
                        foreach (var c in claims)
                        {
                            var conf = c?["confidence"]?.GetValue<double>() ?? 0;
                            var text = c?["claim"]?.GetValue<string>() ?? "";
                            sb.AppendLine($"- **[{conf:F2}]** {text}");
                            var assumptions = c?["key_assumptions"]?.AsArray();
                            if (assumptions?.Count > 0)
                                foreach (var a in assumptions)
                                    sb.AppendLine($"  - {a?.GetValue<string>() ?? ""}");
                        }
                        sb.AppendLine();
                    }

                    var rebuttals = dj?["rebuttals"]?.AsArray();
                    if (rebuttals?.Count > 0)
                    {
                        sb.AppendLine("## Rebuttals");
                        foreach (var r in rebuttals) sb.AppendLine($"- {r?.GetValue<string>() ?? ""}");
                        sb.AppendLine();
                    }

                    var disputes = dj?["disputes"]?.AsArray();
                    if (disputes?.Count > 0)
                    {
                        sb.AppendLine("## Disputes");
                        foreach (var d in disputes)
                        {
                            var target = d?["target_assumption"]?.GetValue<string>() ?? "";
                            var reason = d?["reason"]?.GetValue<string>() ?? "";
                            sb.AppendLine($"- **{target}** → {reason}");
                        }
                        sb.AppendLine();
                    }
                }
                catch { /* JSON parse fail — include raw */ }
            }

            // Clean answer text (strip markers + log noise)
            var cleanAnswer = answer;
            cleanAnswer = Regex.Replace(cleanAnswer, @"\[DEBATE_JSON\].*?\[/DEBATE_JSON\]", "", RegexOptions.Singleline);
            cleanAnswer = Regex.Replace(cleanAnswer, @"^(TITLE|FILE_TITLE):.*$", "", RegexOptions.Multiline);
            cleanAnswer = Regex.Replace(cleanAnswer, @"\[(PULSE|CDP|SANDBOX|HANG-DIAG|LOG|ROTATE|APPBOT_TOOL_CALL)[^\]]*\].*?\n?", "");
            cleanAnswer = cleanAnswer.Trim();

            if (cleanAnswer.Length > 0)
            {
                sb.AppendLine("## Response");
                sb.AppendLine();
                sb.AppendLine(cleanAnswer);
                sb.AppendLine();
            }

            sb.AppendLine("---");
            sb.AppendLine($"*Generated by WKAppBot ask {ai} — {DateTime.Now:yyyy-MM-dd HH:mm:ss}*");

            File.WriteAllText(mdPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"[ASK:MD] {mdPath}");
            // Open in VS Code for immediate review
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "code", Arguments = $"\"{mdPath}\"", UseShellExecute = true, CreateNoWindow = true }); } catch { }
            return mdPath;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ASK:MD] Failed: {ex.Message}");
            return null;
        }
    }

    /// <summary>Build a URL-safe slug from text (lowercase, hyphenated, max length).</summary>
    internal static string BuildSlug(string text, int maxLen)
    {
        // Transliterate common terms, keep alphanumeric + spaces
        var slug = text.ToLowerInvariant();
        slug = Regex.Replace(slug, @"[^\w\s-]", " ");  // strip special chars
        slug = Regex.Replace(slug, @"\s+", "-");         // spaces → hyphens
        slug = Regex.Replace(slug, @"-{2,}", "-");       // collapse multiple hyphens
        slug = slug.Trim('-');
        if (slug.Length > maxLen) slug = slug[..maxLen].TrimEnd('-');
        if (string.IsNullOrEmpty(slug)) slug = "question";
        return slug;
    }
}
