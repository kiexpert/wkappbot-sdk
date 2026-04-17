using System.Text;
using System.Text.Json;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// wkappbot stock-scan -- Naver Finance stock scanner via CDP.
/// [STOCK] tag for all output.
/// </summary>
internal partial class Program
{
    // WebProfilesDir is defined in KnowhowCommands.cs

    static int StockScanCommand(string[] args)
    {
        var mode = args.Length > 0 && !args[0].StartsWith("--") ? args[0].ToLower() : "all";
        int topN = 10;
        bool sendSlack = false;
        bool outputJson = false;
        bool searchNews = false;
        int newsCount = 3;
        string? outputPath = null;
        int port = 9222;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--top" when i + 1 < args.Length: topN = int.Parse(args[++i]); break;
                case "--slack": sendSlack = true; break;
                case "--json": outputJson = true; break;
                case "--news": searchNews = true; break;
                case "--news-count" when i + 1 < args.Length: searchNews = true; newsCount = int.Parse(args[++i]); break;
                case "-o" when i + 1 < args.Length: outputPath = args[++i]; break;
                case "--port" when i + 1 < args.Length: port = int.Parse(args[++i]); break;
            }
        }

        if (mode is "help" or "-h" or "--help")
        {
            Console.WriteLine(@"Usage: wkappbot stock-scan [mode] [options]

Modes:
  all       Scan all categories (default)
  rise      Rising stocks (상승 종목)
  volume    Volume surge (거래량 급증)
  popular   Popular search (인기 검색)

Options:
  --top N          Show top N stocks (default: 10)
  --news           Search news for top stocks
  --news-count N   News per stock (default: 3)
  --slack          Send results to Slack
  --json           Save JSON output
  -o <file>        Output file path
  --port N         CDP port (default: 9222)");
            return 0;
        }

        Console.Error.WriteLine($"[STOCK] Scanning mode={mode}, top={topN}");

        // Connect to existing WebBot Chrome
        CdpClient cdp;
        try
        {
            cdp = new CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();
            // Verify it's a WebBot window
            var title = cdp.GetTitleAsync().GetAwaiter().GetResult() ?? "";
            if (!title.Contains("WKWebBot"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[STOCK] ERROR: No WebBot window found. Run 'wkappbot web open <url>' first.");
                Console.ResetColor();
                cdp.Dispose();
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[STOCK] ERROR: Cannot connect to WebBot Chrome (port {port}): {ex.Message}");
            Console.WriteLine("[STOCK] Run 'wkappbot web open <url>' first to launch WebBot.");
            Console.ResetColor();
            return 1;
        }

        using (cdp)
        {
            var scraper = new NaverFinanceScraper(cdp, WebProfilesDir);
            var allResults = new List<ScanResult>();

            if (mode is "all" or "rise")
            {
                Console.Write("[STOCK] Scanning rising stocks... ");
                var r = scraper.ScanRisingAsync(topN).GetAwaiter().GetResult();
                allResults.Add(r);
                PrintScanBrief(r);
            }

            if (mode is "all" or "volume")
            {
                Console.Write("[STOCK] Scanning volume surge... ");
                var r = scraper.ScanVolumeSurgeAsync(topN).GetAwaiter().GetResult();
                allResults.Add(r);
                PrintScanBrief(r);
            }

            if (mode is "all" or "popular")
            {
                Console.Write("[STOCK] Scanning popular stocks... ");
                var r = scraper.ScanPopularAsync(topN).GetAwaiter().GetResult();
                allResults.Add(r);
                PrintScanBrief(r);
            }

            // Aggregate unique stocks by code, sort by change%
            var allStocks = allResults
                .Where(r => r.Success)
                .SelectMany(r => r.Stocks)
                .GroupBy(s => s.Code)
                .Select(g => g.First())
                .OrderByDescending(s => s.ChangePercent)
                .Take(topN)
                .ToList();

            Console.WriteLine($"\n[STOCK] === Results: {allStocks.Count} unique stocks ===\n");
            PrintStockTable(allStocks);

            // News search for top stocks
            List<NewsResult>? newsResults = null;
            if (searchNews && allStocks.Count > 0)
            {
                // Search news for top N stocks (limit to 5 to avoid too many page loads)
                var newsTargets = allStocks.Take(Math.Min(5, topN)).ToList();
                Console.WriteLine($"\n[STOCK] Searching news for top {newsTargets.Count} stocks...");

                newsResults = scraper.ScanNewsAsync(newsTargets, newsCount).GetAwaiter().GetResult();
                PrintNewsResults(newsResults);
            }

            // JSON output
            if (outputJson || outputPath != null)
            {
                var json = JsonSerializer.Serialize(allStocks, new JsonSerializerOptions { WriteIndented = true });
                var path = outputPath ?? Path.Combine(DataDir, "output", "stock_scan.json");
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                File.WriteAllText(path, json);
                Console.Error.WriteLine($"[STOCK] JSON saved: {path}");
            }

            // Slack notification
            if (sendSlack && allStocks.Count > 0)
            {
                SendStockSlack(allStocks, newsResults);
            }

            // Navigate to blank page to reduce CPU usage while idle
            try
            {
                cdp.NavigateAsync("about:blank").GetAwaiter().GetResult();
                Console.WriteLine("[STOCK] Navigated to blank page (CPU idle)");
            }
            catch { }
        }

        return 0;
    }

    static void PrintScanBrief(ScanResult r)
    {
        if (r.Success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"OK ({r.Stocks.Count} stocks, {r.Duration.TotalSeconds:F1}s)");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"FAIL: {r.ErrorMessage} ({r.Duration.TotalSeconds:F1}s)");
        }
        Console.ResetColor();
    }

    static void PrintStockTable(List<StockInfo> stocks)
    {
        if (stocks.Count == 0) { Console.WriteLine("  (no stocks found)"); return; }

        // Header
        Console.WriteLine($"  {"#",-3} {"Code",-8} {"Name",-16} {"Price",12} {"Change",10} {"Volume",14} {"Src",-8}");
        Console.WriteLine($"  {"---",-3} {"------",-8} {"----------------",-16} {"----------",12} {"--------",10} {"------------",14} {"------",-8}");

        foreach (var s in stocks)
        {
            Console.Write($"  {s.Rank,-3} {s.Code,-8} ");

            // Name: truncate if too long (Korean chars = 2 width)
            var name = s.Name.Length > 14 ? s.Name[..13] + ".." : s.Name;
            Console.Write($"{name,-16} ");

            Console.Write($"{s.CurrentPrice,12:N0} ");

            // Color for change
            Console.ForegroundColor = s.ChangePercent >= 0 ? ConsoleColor.Red : ConsoleColor.Blue;
            var sign = s.ChangePercent >= 0 ? "+" : "";
            Console.Write($"{sign}{s.ChangePercent,8:F2}% ");
            Console.ResetColor();

            Console.Write($"{s.Volume,14:N0} ");
            Console.WriteLine($"{s.Source,-8}");
        }
        Console.WriteLine();
    }

    static void PrintNewsResults(List<NewsResult> results)
    {
        Console.WriteLine($"\n[STOCK] === News Headlines ===\n");
        foreach (var nr in results)
        {
            if (!nr.Success || nr.News.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  [{nr.StockName}] (no news found)");
                Console.ResetColor();
                continue;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  [{nr.StockName}] ");
            Console.ResetColor();
            Console.WriteLine();

            foreach (var news in nr.News)
            {
                var title = news.Title.Length > 60 ? news.Title[..59] + ".." : news.Title;
                var src = string.IsNullOrEmpty(news.Source) ? "" : $" ({news.Source})";
                Console.WriteLine($"    - {title}{src}");
            }
        }
        Console.WriteLine();
    }

    static void SendStockSlack(List<StockInfo> stocks, List<NewsResult>? newsResults = null)
    {
        try
        {
            var configPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("[STOCK] Slack config not found, skipping");
                return;
            }

            var configJson = File.ReadAllText(configPath);
            var config = JsonDocument.Parse(configJson).RootElement;
            var botToken = config.GetProperty("bot_token").GetString();
            var channel = config.GetProperty("channel").GetString();
            if (botToken == null || channel == null) return;

            // Build Slack message
            var sb = new StringBuilder();
            sb.AppendLine($"*[STOCK] 특징주 스캔 결과* ({DateTime.Now:HH:mm:ss})");
            sb.AppendLine("```");
            sb.AppendLine($"{"#",-3} {"Code",-8} {"Name",-14} {"Price",10} {"Chg%",8} {"Volume",12}");
            foreach (var s in stocks.Take(15))
            {
                var sign = s.ChangePercent >= 0 ? "+" : "";
                var name = s.Name.Length > 12 ? s.Name[..11] + ".." : s.Name;
                sb.AppendLine($"{s.Rank,-3} {s.Code,-8} {name,-14} {s.CurrentPrice,10:N0} {sign}{s.ChangePercent,7:F2}% {s.Volume,12:N0}");
            }
            sb.AppendLine("```");

            // Append news if available
            if (newsResults != null)
            {
                sb.AppendLine();
                sb.AppendLine("*관련 뉴스*");
                foreach (var nr in newsResults.Where(r => r.Success && r.News.Count > 0))
                {
                    sb.AppendLine($"• *{nr.StockName}*");
                    foreach (var news in nr.News.Take(2))
                    {
                        var title = news.Title.Length > 50 ? news.Title[..49] + ".." : news.Title;
                        var src = string.IsNullOrEmpty(news.Source) ? "" : $" _{news.Source}_";
                        sb.AppendLine($"  + <{news.Url}|{title}>{src}");
                    }
                }
            }

            // Load reply context for thread
            var (_, replyThread) = LoadReplyContext();

            SlackSendViaApi(botToken, channel, sb.ToString(), replyThread, username: BotUsername)
                .GetAwaiter().GetResult();
            Console.WriteLine("[STOCK] Results sent to Slack");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[STOCK] Slack send failed: {ex.Message}");
        }
    }
}
