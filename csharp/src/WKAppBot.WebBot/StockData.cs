namespace WKAppBot.WebBot;

/// <summary>Stock information from Naver Finance scanning.</summary>
public record StockInfo
{
    public int Rank { get; init; }
    public string Code { get; init; } = "";
    public string Name { get; init; } = "";
    public int CurrentPrice { get; init; }
    public int ChangeAmount { get; init; }
    public decimal ChangePercent { get; init; }
    public long Volume { get; init; }
    public string Source { get; init; } = "";
    public DateTime ScannedAt { get; init; } = DateTime.Now;
}

/// <summary>Scan result for one page.</summary>
public record ScanResult
{
    public List<StockInfo> Stocks { get; init; } = new();
    public string Source { get; init; } = "";
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public TimeSpan Duration { get; init; }
}

/// <summary>A single news headline for a stock.</summary>
public record NewsItem
{
    public string Title { get; init; } = "";
    public string Url { get; init; } = "";
    public string Source { get; init; } = "";    // press name (e.g. "한국경제")
    public string Date { get; init; } = "";      // e.g. "2026.02.20"
}

/// <summary>News search result for a stock.</summary>
public record NewsResult
{
    public string StockName { get; init; } = "";
    public string StockCode { get; init; } = "";
    public List<NewsItem> News { get; init; } = new();
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
}
