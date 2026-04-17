using System.Diagnostics;
using System.Text.Json;

namespace WKAppBot.WebBot;

/// <summary>
/// Naver Finance web scraper using CDP.
/// Extracts rising stocks, volume surge, and popular search data.
/// Records knowhow on success/failure for future sessions.
/// </summary>
public class NaverFinanceScraper
{
    private readonly CdpClient _cdp;
    private readonly string? _webProfilesDir;
    private const string Domain = "finance.naver.com";

    // Naver Finance table extraction JS -- verified against live DOM (2026-02-20)
    // table.type_2 tbody tr: rank|name(link)|price|change|changePct|volume|...
    private const string ExtractionJs = @"
(function() {
    try {
        var rows = document.querySelectorAll('table.type_2 tbody tr');
        var results = [];
        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].querySelectorAll('td');
            if (cells.length < 6) continue;
            var link = cells[1] ? cells[1].querySelector('a') : null;
            if (!link) continue;
            var href = link.getAttribute('href') || '';
            var codeMatch = href.match(/code=(\d{6})/);
            if (!codeMatch) continue;
            var priceText = cells[2].textContent.trim().replace(/,/g, '');
            var changeText = cells[3].textContent.trim();
            var changeNum = changeText.replace(/[^0-9]/g, '');
            var pctText = cells[4].textContent.trim();
            var pctMatch = pctText.match(/([+-]?\d+\.?\d*)/);
            var volText = cells[5].textContent.trim().replace(/,/g, '');
            results.push({
                rank: parseInt(cells[0].textContent.trim()) || (i+1),
                name: link.textContent.trim(),
                code: codeMatch[1],
                price: parseInt(priceText) || 0,
                change: parseInt(changeNum) || 0,
                changePct: pctMatch ? parseFloat(pctMatch[1]) : 0,
                volume: parseInt(volText) || 0
            });
        }
        return JSON.stringify(results);
    } catch (e) {
        return JSON.stringify({error: e.message});
    }
})();
";

    // Popular search page uses table.type_5 with different column layout:
    // cells[0]=rank, [1]=name(link), [2]=searchPct, [3]=price, [4]=direction, [5]=changePct, [6]=volume
    private const string PopularExtractionJs = @"
(function() {
    try {
        var rows = document.querySelectorAll('table.type_5 tbody tr');
        var results = [];
        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].querySelectorAll('td');
            if (cells.length < 7) continue;
            var link = cells[1] ? cells[1].querySelector('a') : null;
            if (!link) continue;
            var href = link.getAttribute('href') || '';
            var codeMatch = href.match(/code=(\d{6})/);
            if (!codeMatch) continue;
            var priceText = cells[3].textContent.trim().replace(/,/g, '');
            var dirText = cells[4].textContent.trim();
            var pctText = cells[5].textContent.trim();
            var pctMatch = pctText.match(/(\d+\.?\d*)/);
            var pctVal = pctMatch ? parseFloat(pctMatch[1]) : 0;
            if (dirText.indexOf('\uD558\uB77D') >= 0) pctVal = -pctVal;
            var volText = cells[6].textContent.trim().replace(/,/g, '');
            results.push({
                rank: parseInt(cells[0].textContent.trim()) || (i+1),
                name: link.textContent.trim(),
                code: codeMatch[1],
                price: parseInt(priceText) || 0,
                change: 0,
                changePct: pctVal,
                volume: parseInt(volText) || 0
            });
        }
        return JSON.stringify(results);
    } catch (e) {
        return JSON.stringify({error: e.message});
    }
})();
";

    // Naver News search extraction -- forEach+JSON.stringify pattern (NOT IIFE! IIFE returns empty in CDP EvalAsync)
    // .list_news contains Fender SPA rendered news items
    // Pattern: short-text link (press name) followed by long-text link (article title, same href = body preview)
    // De-duplicate by href, capture press name from preceding short link
    private const string NewsExtractionJs = @"
var _el=document.querySelector('.list_news');
var _r=[];
if(_el){
    var _links=Array.from(_el.querySelectorAll('a'));
    var _seen={};
    var _prev='';
    _links.forEach(function(a){
        var t=a.textContent.trim();
        var h=a.href||'';
        if(t.length>1&&t.length<15&&h.indexOf('naver.com')<0&&h.indexOf('keep.')<0){_prev=t;return;}
        if(t.length<15||h.indexOf('keep.')>=0||h.indexOf('search.naver')>=0||_seen[h])return;
        _seen[h]=1;
        _r.push({title:t.substring(0,120),url:h,source:_prev});
        _prev='';
    });
}
JSON.stringify(_r.slice(0,5));
";

    public NaverFinanceScraper(CdpClient cdpClient, string? webProfilesDir = null)
    {
        _cdp = cdpClient;
        _webProfilesDir = webProfilesDir;
    }

    /// <summary>Scan rising stocks (상승 종목).</summary>
    public async Task<ScanResult> ScanRisingAsync(int topN, CancellationToken ct = default)
        => await ScanPageAsync("https://finance.naver.com/sise/sise_rise.naver", "rise", topN, ct);

    /// <summary>Scan volume surge stocks (거래량 급증).</summary>
    public async Task<ScanResult> ScanVolumeSurgeAsync(int topN, CancellationToken ct = default)
        => await ScanPageAsync("https://finance.naver.com/sise/sise_quant.naver", "volume", topN, ct);

    /// <summary>Scan popular search stocks (인기 검색).</summary>
    public async Task<ScanResult> ScanPopularAsync(int topN, CancellationToken ct = default)
        => await ScanPageAsync("https://finance.naver.com/sise/lastsearch2.naver", "popular", topN, ct);

    /// <summary>Search news for a list of stocks via Naver News.</summary>
    public async Task<List<NewsResult>> ScanNewsAsync(List<StockInfo> stocks, int newsPerStock = 3, CancellationToken ct = default)
    {
        var results = new List<NewsResult>();
        foreach (var stock in stocks)
        {
            try
            {
                // URL-encode stock name for search query
                var query = Uri.EscapeDataString($"{stock.Name} 주가");
                var url = $"https://search.naver.com/search.naver?where=news&query={query}&sort=1";

                await _cdp.NavigateAsync(url);
                await Task.Delay(2000, ct); // Wait for SPA render

                var json = await _cdp.EvalAsync(NewsExtractionJs);
                if (string.IsNullOrWhiteSpace(json) || json.Contains("\"error\""))
                {
                    RecordKnowhow("news", "FAIL", $"News extraction failed for {stock.Name}: {json}");
                    results.Add(new NewsResult
                    {
                        StockName = stock.Name,
                        StockCode = stock.Code,
                        Success = false,
                        ErrorMessage = json ?? "empty",
                    });
                    continue;
                }

                var news = ParseNewsJson(json, newsPerStock);
                RecordKnowhow("news", "OK", $"{stock.Name}: {news.Count} articles");
                results.Add(new NewsResult
                {
                    StockName = stock.Name,
                    StockCode = stock.Code,
                    News = news,
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                RecordKnowhow("news", "FAIL", $"{stock.Name}: {ex.Message}");
                results.Add(new NewsResult
                {
                    StockName = stock.Name,
                    StockCode = stock.Code,
                    Success = false,
                    ErrorMessage = ex.Message,
                });
            }
        }
        return results;
    }

    private static List<NewsItem> ParseNewsJson(string json, int max)
    {
        var items = new List<NewsItem>();
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.ValueKind != JsonValueKind.Array)
                return items;

            foreach (var el in doc.RootElement.EnumerateArray())
            {
                if (items.Count >= max) break;
                items.Add(new NewsItem
                {
                    Title = el.GetProperty("title").GetString() ?? "",
                    Url = el.GetProperty("url").GetString() ?? "",
                    Source = el.GetProperty("source").GetString() ?? "",
                });
            }
        }
        catch { }
        return items;
    }

    private async Task<ScanResult> ScanPageAsync(string url, string source, int topN, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            // Check if already on this page (avoid unnecessary reload)
            var currentUrl = await _cdp.GetUrlAsync();
            var targetPath = new Uri(url).AbsolutePath;
            if (currentUrl != null && currentUrl.Contains(targetPath))
            {
                await Task.Delay(500, ct); // Already on page
            }
            else
            {
                await _cdp.NavigateAsync(url);
                await Task.Delay(3000, ct); // Naver Finance pages are heavy
            }

            var js = source == "popular" ? PopularExtractionJs : ExtractionJs;
            var json = await _cdp.EvalAsync(js);
            if (string.IsNullOrWhiteSpace(json))
            {
                RecordKnowhow(source, "FAIL", "EvalAsync returned empty");
                return Fail(source, "Empty response from page", sw.Elapsed);
            }

            // Check for JS error
            if (json.Contains("\"error\""))
            {
                RecordKnowhow(source, "FAIL", $"JS error: {json}");
                return Fail(source, $"JS extraction error: {json}", sw.Elapsed);
            }

            var stocks = ParseStockJson(json, source, topN);
            RecordKnowhow(source, "OK", $"Extracted {stocks.Count} stocks from {url}");

            return new ScanResult
            {
                Stocks = stocks,
                Source = source,
                Success = true,
                Duration = sw.Elapsed,
            };
        }
        catch (Exception ex)
        {
            RecordKnowhow(source, "FAIL", $"{url} -- {ex.Message}");
            return Fail(source, ex.Message, sw.Elapsed);
        }
    }

    private static List<StockInfo> ParseStockJson(string json, string source, int topN)
    {
        var stocks = new List<StockInfo>();
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.ValueKind != JsonValueKind.Array)
                return stocks;

            foreach (var item in doc.RootElement.EnumerateArray())
            {
                if (stocks.Count >= topN) break;

                stocks.Add(new StockInfo
                {
                    Rank = item.GetProperty("rank").GetInt32(),
                    Code = item.GetProperty("code").GetString() ?? "",
                    Name = item.GetProperty("name").GetString() ?? "",
                    CurrentPrice = item.GetProperty("price").GetInt32(),
                    ChangeAmount = item.GetProperty("change").GetInt32(),
                    ChangePercent = item.GetProperty("changePct").GetDecimal(),
                    Volume = item.GetProperty("volume").GetInt64(),
                    Source = source,
                });
            }
        }
        catch { }
        return stocks;
    }

    private static ScanResult Fail(string source, string msg, TimeSpan elapsed)
        => new() { Source = source, Success = false, ErrorMessage = msg, Duration = elapsed };

    private void RecordKnowhow(string source, string status, string detail)
    {
        if (_webProfilesDir == null) return;
        try
        {
            WebKnowhow.AppendSiteKnowhow(_webProfilesDir, Domain, "Stock Scan", $"[{status}] {source}: {detail}");
        }
        catch { }
    }
}
