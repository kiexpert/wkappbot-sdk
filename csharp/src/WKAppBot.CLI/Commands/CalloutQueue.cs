// CalloutQueue.cs -- Cross-process queue for a11y type --callout actions.
// Each entry represents a pending type operation awaiting user confirmation.
// Append-only JSONL; Eye daemon drains at 500ms cadence and shows callout.

using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    static string CalloutQueuePath => Path.Combine(DataDir, "runtime", "callout_queue.jsonl");

    record CalloutQueueEntry(
        string Id,
        string Hwnd,
        string Text,
        int RectX, int RectY, int RectW, int RectH,
        string Created,
        string Status);

    static void CalloutQueueAppend(IntPtr hwnd, string text, System.Drawing.Rectangle rect)
    {
        var entry = new JsonObject
        {
            ["id"]      = Guid.NewGuid().ToString("N")[..12],
            ["hwnd"]    = $"0x{hwnd:X}",
            ["text"]    = text,
            ["rect_x"]  = rect.X,
            ["rect_y"]  = rect.Y,
            ["rect_w"]  = rect.Width,
            ["rect_h"]  = rect.Height,
            ["created"] = DateTime.UtcNow.ToString("O"),
            ["status"]  = "pending"
        };
        Directory.CreateDirectory(Path.GetDirectoryName(CalloutQueuePath)!);
        File.AppendAllText(CalloutQueuePath, entry.ToJsonString() + "\n");
    }

    static List<CalloutQueueEntry> CalloutQueueReadPending()
    {
        if (!File.Exists(CalloutQueuePath)) return [];
        var result = new List<CalloutQueueEntry>();
        try
        {
            foreach (var line in File.ReadLines(CalloutQueuePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var n = JsonNode.Parse(line);
                if (n is not JsonObject o) continue;
                var status = o["status"]?.GetValue<string>() ?? "";
                if (status != "pending") continue;
                result.Add(new CalloutQueueEntry(
                    o["id"]?.GetValue<string>()      ?? "",
                    o["hwnd"]?.GetValue<string>()    ?? "0x0",
                    o["text"]?.GetValue<string>()    ?? "",
                    o["rect_x"]?.GetValue<int>()     ?? 0,
                    o["rect_y"]?.GetValue<int>()     ?? 0,
                    o["rect_w"]?.GetValue<int>()     ?? 100,
                    o["rect_h"]?.GetValue<int>()     ?? 30,
                    o["created"]?.GetValue<string>() ?? "",
                    status));
            }
        }
        catch { }
        return result.OrderBy(e => e.Created).ToList();
    }

    static void CalloutQueueUpdateStatus(string id, string newStatus)
    {
        if (!File.Exists(CalloutQueuePath)) return;
        try
        {
            var lines = File.ReadAllLines(CalloutQueuePath);
            bool changed = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var n = JsonNode.Parse(lines[i]);
                if (n is not JsonObject o) continue;
                if (o["id"]?.GetValue<string>() != id) continue;
                o["status"] = newStatus;
                lines[i] = o.ToJsonString();
                changed = true;
                break;
            }
            if (!changed) return;
            var tmp = CalloutQueuePath + ".tmp";
            File.WriteAllLines(tmp, lines);
            File.Move(tmp, CalloutQueuePath, overwrite: true);
        }
        catch { }
    }

    static void CalloutQueueCleanupDone()
    {
        if (!File.Exists(CalloutQueuePath)) return;
        try
        {
            var cutoff = DateTime.UtcNow.AddHours(-2);
            var keep = new List<string>();
            foreach (var line in File.ReadLines(CalloutQueuePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var n = JsonNode.Parse(line);
                if (n is not JsonObject o) { keep.Add(line); continue; }
                var status = o["status"]?.GetValue<string>() ?? "pending";
                if (status is "done" or "cancelled")
                {
                    if (DateTime.TryParse(o["created"]?.GetValue<string>(), out var created)
                        && created < cutoff) continue;
                }
                keep.Add(line);
            }
            File.WriteAllLines(CalloutQueuePath, keep);
        }
        catch { }
    }
}
