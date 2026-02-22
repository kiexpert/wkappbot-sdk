using System.Text;
using System.Text.Json;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed class EyeParentCard
    {
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public string LastTag { get; set; } = "";
        public string LastStatus { get; set; } = "";
        public DateTime LastTsUtc { get; set; }
    }

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs)
    {
        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", "WK AppBot Eye Global");
        host.UpdateAccessibilityText("Starting global monitor...");

        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            try
            {
                var cards = ReadEyeCards(staleSeconds: 25);

                var sb = new StringBuilder();
                sb.AppendLine($"Global Eye  {DateTime.Now:HH:mm:ss}");
                if (cards.Count == 0)
                {
                    sb.AppendLine("(active parent cards: 0)");
                }
                else
                {
                    foreach (var c in cards.OrderByDescending(x => x.LastTsUtc).Take(8))
                    {
                        var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                        sb.AppendLine($"[{c.ParentName}:{c.ParentPid}] {c.LastTag}  {c.LastStatus}  -{age}s");
                    }
                }

                host.UpdateAccessibilityText(sb.ToString().TrimEnd());
            }
            catch { }

            Thread.Sleep(Math.Max(100, intervalMs));
        }

        return 0;
    }

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 25)
    {
        var cards = new Dictionary<int, EyeParentCard>();
        var path = EyeTicksPath;
        if (!File.Exists(path))
            return cards.Values.ToList();

        var lines = File.ReadAllLines(path);
        // Read only recent tail for efficiency.
        var start = Math.Max(0, lines.Length - 500);

        for (int i = start; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(line);
                if (t == null) continue;

                if (!DateTime.TryParse(t.Ts, out var tsLocal)) continue;
                var tsUtc = tsLocal.ToUniversalTime();

                var ppid = t.ParentPid > 0 ? t.ParentPid : t.Pid;
                var pname = string.IsNullOrWhiteSpace(t.ParentName) ? "unknown" : t.ParentName;

                if (!cards.TryGetValue(ppid, out var c) || tsUtc > c.LastTsUtc)
                {
                    cards[ppid] = new EyeParentCard
                    {
                        ParentPid = ppid,
                        ParentName = pname,
                        LastTag = t.Tag,
                        LastStatus = t.Status,
                        LastTsUtc = tsUtc
                    };
                }
            }
            catch { }
        }

        var now = DateTime.UtcNow;
        return cards.Values
            .Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds)
            .Where(c => IsProcessAlive(c.ParentPid))
            .ToList();
    }
}
