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

        var body = ShellOutputStore.ReadById(rec.Id) ?? "";
        var truncNote = "";
        if (body.Length > maxResultBytes)
        {
            // Keep the tail -- that's where error messages typically live.
            var cut = body.Length - maxResultBytes;
            body = body[cut..];
            truncNote = $" truncated=\"{cut} bytes dropped from head\"";
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
