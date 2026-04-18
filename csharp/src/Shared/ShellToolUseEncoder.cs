using System.Text;

namespace WKAppBot.Shared;

/// <summary>
/// Serialize a batch of <see cref="ShellOutputRecord"/>s into MCP-style
/// tool_use / tool_result XML blocks so the fallback AI receives them in
/// the same shape its native tool-use protocol would deliver.
///
/// Size is capped. When the combined encoding exceeds
/// <paramref name="maxTotalBytes"/>, the oldest records are dropped
/// first; the most recent record is also allowed to tail-truncate its
/// tool_result body if the single record itself is too large -- a 10MB
/// build log becomes a "(truncated, original 10MB)" tail sample instead
/// of breaking the AI context window.
/// </summary>
public static class ShellToolUseEncoder
{
    public static string Encode(
        IReadOnlyList<ShellOutputRecord> records,
        int maxTotalBytes = 16 * 1024,
        int maxPerResultBytes = 4 * 1024)
    {
        if (records.Count == 0) return "";

        // Build newest-first, drop oldest as needed, then reverse for chrono order.
        var blocks = new List<string>();
        int total = 0;
        for (int i = records.Count - 1; i >= 0; i--)
        {
            var block = EncodeOne(records[i], maxPerResultBytes);
            if (total + block.Length > maxTotalBytes) break;
            total += block.Length;
            blocks.Add(block);
        }
        blocks.Reverse();
        return string.Join("\n", blocks);
    }

    private static string EncodeOne(ShellOutputRecord rec, int maxResultBytes)
    {
        var sb = new StringBuilder();
        sb.Append("<tool_use name=\"shell\" id=\"").Append(rec.Id).Append("\">\n");
        sb.Append("  <input>\n");
        sb.Append("    <cmd>").Append(Xml(rec.Command)).Append("</cmd>\n");
        if (!string.IsNullOrEmpty(rec.Cwd))
            sb.Append("    <cwd>").Append(Xml(rec.Cwd)).Append("</cwd>\n");
        sb.Append("  </input>\n");
        sb.Append("</tool_use>\n");

        // Body policy: always show the AI the same text the user saw, in the
        // same reading order (head first). When the body exceeds the cap,
        // keep the head and append a single "... 짤림 ..." marker that tells
        // the AI (and operator) which tool surfaces the full content. No
        // success/failure branching -- same rule everywhere, simpler to
        // reason about, and the head-first order preserves the narrative
        // the user experienced at their terminal.
        var body = ShellOutputStore.ReadById(rec.Id) ?? "";
        var truncNote = "";
        if (body.Length > maxResultBytes)
        {
            var kept = maxResultBytes;
            body = body[..kept];
            if (!body.EndsWith('\n')) body += "\n";
            // Page-continuation hint: tell the AI exactly how to resume reading
            // from where we cut. /out supports --after <byte-offset> and
            // --lines <N> for this, modeled after tail-style pagination. The
            // underlying "file" is our captured log -- no actual source file
            // on disk is required for the AI to request a continuation.
            var totalB = rec.LineCount > 0 ? $"총 {rec.LineCount}줄, " : "";
            body += $"... 짤림: {totalB}이어보기 = `/out {rec.Id} --after {kept}` (바이트) 또는 `/out {rec.Id} --lines 50` (줄수) ...\n";
            truncNote = $" truncated=\"tail cut at {kept}B -- resume via /out {rec.Id} --after {kept}\"";
        }

        sb.Append("<tool_result for=\"").Append(rec.Id).Append("\" exit=\"").Append(rec.ExitCode)
          .Append("\" lines=\"").Append(rec.LineCount).Append("\"").Append(truncNote).Append(">\n");
        sb.Append(Xml(body));
        if (!body.EndsWith('\n')) sb.Append('\n');
        sb.Append("</tool_result>\n");

        return sb.ToString();
    }

    private static string Xml(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
}
