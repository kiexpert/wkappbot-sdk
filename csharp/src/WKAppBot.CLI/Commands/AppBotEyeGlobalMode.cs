using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed class EyeParentCard
    {
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public string ParentTitle { get; set; } = "";
        public string LastTag { get; set; } = "";
        public string LastStatus { get; set; } = "";
        public DateTime LastTsUtc { get; set; }
    }

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs)
    {
        // Default anchor: right-most monitor, top-right corner
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(string.Empty);

        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            try
            {
                var cards = ReadEyeCards(staleSeconds: 180);

                host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");

                var sb = new StringBuilder();

                var latest = ReadLatestTick();
                if (latest != null)
                {
                    var (report, next, block) = BuildKroThought(latest);
                    sb.AppendLine($"크로 보고: {report}");
                    sb.AppendLine($"크로 다음: {next}");
                    if (!string.IsNullOrWhiteSpace(block))
                        sb.AppendLine($"크로 막힘: {block}");
                    sb.AppendLine("----");
                }

                if (cards.Count == 0)
                {
                    sb.AppendLine("(active parent cards: 0)");
                }
                else
                {
                    foreach (var c in cards.OrderByDescending(x => x.LastTsUtc).Take(8))
                    {
                        var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                        var head = string.IsNullOrWhiteSpace(c.ParentTitle)
                            ? $"{c.ParentName}:{c.ParentPid}"
                            : $"{c.ParentTitle} ({c.ParentName}:{c.ParentPid})";
                        sb.AppendLine($"[{head}] {c.LastTag}  {c.LastStatus}  -{age}s");
                    }
                }

                host.UpdateAccessibilityText(sb.ToString().TrimEnd());
            }
            catch { }

            Thread.Sleep(Math.Max(100, intervalMs));
        }

        return 0;
    }

    static (int x, int y) GetRightmostMonitorAnchor(int width, int height)
    {
        try
        {
            var screens = Screen.AllScreens;
            if (screens != null && screens.Length > 0)
            {
                var rightMost = screens.OrderByDescending(s => s.Bounds.Right).First();
                var x = rightMost.Bounds.Right - width - 10;
                var y = rightMost.Bounds.Top + 110;
                return (x, y);
            }
        }
        catch { }

        return (1530, 110);
    }

    static EyeTick? ReadLatestTick()
    {
        var path = EyeTicksPath;
        if (!File.Exists(path))
            return null;

        var lines = File.ReadAllLines(path);
        for (int i = lines.Length - 1; i >= 0; i--)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(line);
                if (t != null) return t;
            }
            catch { }
        }

        return null;
    }

    static (string report, string next, string block) BuildKroThought(EyeTick t)
    {
        var report = $"{t.Command}/{t.Tag}";
        var status = (t.Status ?? "").ToLowerInvariant();

        if (status == "start")
            return (report, "작업 진행 중", "");

        if (status.StartsWith("end:"))
        {
            var codeText = status.Substring(4).Trim();
            if (int.TryParse(codeText, out var code) && code == 0)
                return (report, "다음 작업 대기", "");

            return (report, "에러 분석/폴백 시도", $"종료코드 {codeText}");
        }

        return (report, "상태 갱신 대기", "");
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

                var ppid = t.HostPid > 0 ? t.HostPid : (t.ParentPid > 0 ? t.ParentPid : t.Pid);
                var pname = !string.IsNullOrWhiteSpace(t.HostName)
                    ? t.HostName
                    : (string.IsNullOrWhiteSpace(t.ParentName) ? "unknown" : t.ParentName);
                var ptitle = t.HostTitle ?? "";

                if (!cards.TryGetValue(ppid, out var c) || tsUtc > c.LastTsUtc)
                {
                    cards[ppid] = new EyeParentCard
                    {
                        ParentPid = ppid,
                        ParentName = pname,
                        ParentTitle = ptitle,
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
            .ToList();
    }
}
