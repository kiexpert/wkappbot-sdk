using System.Net.Http;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: wkappbot web fetch/search/read -- HTTP + search + TTS tools for loop agents
// web fetch <url> [--timeout N] [--max-chars N]
// web search <query> [--limit N] [--port N] [--speak]
// web read <url> [--port N] [--speak] [--max-chars N]
//   search/read: uses already-running WebBot Chrome (CDP) -- no API key needed.
internal partial class Program
{
    static int WebFetchCommand(string[] args)
    {
        string? url      = null;
        int     timeout  = 30;
        int     maxChars = 50_000;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--timeout"   && i + 1 < args.Length) int.TryParse(args[++i], out timeout);
            else if (args[i] == "--max-chars" && i + 1 < args.Length) int.TryParse(args[++i], out maxChars);
            else if (!args[i].StartsWith("--")) url = args[i];
        }

        if (string.IsNullOrEmpty(url))
            return Error("Usage: web fetch <url> [--timeout N] [--max-chars N]");

        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(timeout) };
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; WKAppBot/3.7)");

            var resp = http.Send(req);
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Console.Error.WriteLine($"[FETCH] {url}");
            Console.Error.WriteLine($"[FETCH] {(int)resp.StatusCode} {resp.StatusCode} | Content-Type: {resp.Content.Headers.ContentType}");

            if (body.Length > maxChars)
            {
                Console.Error.WriteLine($"[FETCH] Truncated to {maxChars:N0} chars (total: {body.Length:N0})");
                body = body[..maxChars];
            }

            Console.WriteLine(body);
            return 0;
        }
        catch (Exception ex) { return Error($"Fetch failed: {ex.Message}"); }
    }

    // Serializes concurrent CDP navigate+eval calls -- prevents triad agents from stealing each other's tab
    static readonly System.Threading.SemaphoreSlim _cdpBrowserLock = new(1, 1);

    // -- web search -- via WebBot Chrome CDP (Google, no API key) ------------
    static int WebSearchCommand(string[] args)
    {
        var parts = new List<string>();
        int  limit = 10;
        int  port  = 9222;
        bool speak = false;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--limit" && i + 1 < args.Length) int.TryParse(args[++i], out limit);
            else if (args[i] == "--port"  && i + 1 < args.Length) int.TryParse(args[++i], out port);
            else if (args[i] == "--speak") speak = true;
            else if (!args[i].StartsWith("--")) parts.Add(args[i]);
        }

        var query = string.Join(" ", parts).Trim();
        if (string.IsNullOrEmpty(query))
            return Error("Usage: web search <query> [--limit N] [--port N] [--speak]");

        _cdpBrowserLock.Wait();
        try
        {
            var searchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(query)}&num={Math.Min(limit, 20)}";
            Console.Error.WriteLine($"[SEARCH] \"{query}\" -> {searchUrl}");

            // Pass searchUrl as navigateUrl: EnsureCorrectWindowAsync will use a dedicated
            // web tab (not an AI chat tab) and navigate it to searchUrl directly.
            // If EnsureCorrectWindowAsync already navigated, NavigateAsync below is a no-op (same URL).
            var cdp = ConnectCdp(port, withBar: false, navigateUrl: searchUrl);
            // Ensure we are on the search URL (in case ConnectCdp's EnsureCorrectWindowAsync
            // returned an existing tab that's not yet at searchUrl)
            var curUrl = cdp.GetUrlAsync().GetAwaiter().GetResult() ?? "";
            if (!curUrl.StartsWith(searchUrl.Split('?')[0], StringComparison.OrdinalIgnoreCase))
                cdp.NavigateAsync(searchUrl).GetAwaiter().GetResult();

            // Poll via JS promise -- waits up to 3s for h3 result elements to render
            var js = $$"""
                new Promise(resolve => {
                    function extract() {
                        const items = [];
                        document.querySelectorAll('h3').forEach(h => {
                            const a = h.closest('a') || h.parentElement?.closest('a');
                            if (!a || !a.href.startsWith('http')) return;
                            const block = a.closest('[data-hveid]') || a.parentElement?.parentElement;
                            const snip = block?.querySelector('[data-sncf],[style*="-webkit-line-clamp"],span:not([class])')?.innerText || '';
                            items.push({
                                title: h.innerText.trim(),
                                url: a.href,
                                snippet: snip.replace(/\n/g, ' ').slice(0, 300)
                            });
                        });
                        return items;
                    }
                    let tries = 0;
                    const check = () => {
                        const items = extract();
                        if (items.length > 0 || ++tries >= 15)
                            resolve(JSON.stringify(items.slice(0, {{limit}})));
                        else
                            setTimeout(check, 200);
                    };
                    check();
                })
                """;

            var json = cdp.EvalAsync(js, awaitPromise: true).GetAwaiter().GetResult() ?? "[]";
            if (json.StartsWith('"') && json.EndsWith('"'))
                json = System.Text.Json.JsonSerializer.Deserialize<string>(json) ?? "[]";

            var results = System.Text.Json.JsonSerializer.Deserialize<List<SearchResult>>(json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (results == null || results.Count == 0)
            {
                // JS selector may have broken (Google HTML change) -- fall back to UIA a11y tree
                Console.WriteLine("[SEARCH] JS selector returned 0 -- falling back to UIA a11y tree...");
                results = ExtractSearchResultsFromUia(limit);
            }
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("[SEARCH] No results found (is WebBot Chrome open? run: wkappbot web open)");
                return 0;
            }

            Console.Error.WriteLine($"[SEARCH] {results.Count} result(s)");
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < results.Count; i++)
            {
                var r = results[i];
                Console.WriteLine($"\n[{i + 1}] {r.Title}");
                Console.WriteLine($"    {r.Url}");
                if (!string.IsNullOrEmpty(r.Snippet))
                    Console.WriteLine($"    {r.Snippet}");

                if (speak)
                {
                    sb.Append($"{i + 1}번. {r.Title}. ");
                    if (!string.IsNullOrEmpty(r.Snippet)) sb.Append($"{r.Snippet}. ");
                }
            }

            if (speak && sb.Length > 0)
                SpeakCommand([sb.ToString()]);

            return 0;
        }
        catch (Exception ex) { return Error($"Search failed: {ex.Message}\nTip: make sure WebBot Chrome is running -- wkappbot web open"); }
        finally { _cdpBrowserLock.Release(); }
    }

    // -- web read -- navigate to URL and read visible text via a11y/CDP ------
    // Reads document.body.innerText (rendered text, no HTML) -- same as what user sees.
    // --speak: TTS-read the content with karaoke overlay.
    static int WebReadCommand(string[] args)
    {
        string? url      = null;
        int     port     = 9222;
        bool    speak    = false;
        int     maxChars = 20_000;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--port"      && i + 1 < args.Length) int.TryParse(args[++i], out port);
            else if (args[i] == "--max-chars" && i + 1 < args.Length) int.TryParse(args[++i], out maxChars);
            else if (args[i] == "--speak") speak = true;
            else if (!args[i].StartsWith("--")) url = args[i];
        }

        if (string.IsNullOrEmpty(url))
            return Error("Usage: web read <url> [--speak] [--max-chars N] [--port N]");

        _cdpBrowserLock.Wait();
        try
        {
            // Pass url as navigateUrl: ensures a dedicated web tab, never steals an AI chat tab
            var cdp = ConnectCdp(port, withBar: false, navigateUrl: url);

            Console.Error.WriteLine($"[READ] Navigating: {url}");
            cdp.NavigateAsync(url).GetAwaiter().GetResult();

            // Wait for main content via promise
            var js = """
                new Promise(resolve => {
                    const getText = () => {
                        // Prefer article/main content, fall back to body
                        const main = document.querySelector('article, main, [role="main"], .post-content, .entry-content, #content');
                        return (main || document.body)?.innerText?.trim() || '';
                    };
                    let tries = 0;
                    const check = () => {
                        const t = getText();
                        if (t.length > 100 || ++tries >= 20) resolve(t);
                        else setTimeout(check, 200);
                    };
                    check();
                })
                """;

            var text = cdp.EvalAsync(js, awaitPromise: true).GetAwaiter().GetResult() ?? "";
            if (text.StartsWith('"') && text.EndsWith('"'))
                text = System.Text.Json.JsonSerializer.Deserialize<string>(text) ?? "";

            var title = cdp.GetTitleAsync().GetAwaiter().GetResult() ?? "";
            Console.Error.WriteLine($"[READ] {title}");
            Console.Error.WriteLine($"[READ] {text.Length:N0} chars");

            if (text.Length > maxChars)
            {
                Console.Error.WriteLine($"[READ] Truncated to {maxChars:N0} chars");
                text = text[..maxChars];
            }

            Console.WriteLine(text);

            if (speak && !string.IsNullOrWhiteSpace(text))
                SpeakCommand([text]);

            return 0;
        }
        catch (Exception ex) { return Error($"Read failed: {ex.Message}\nTip: make sure WebBot Chrome is running -- wkappbot web open"); }
        finally { _cdpBrowserLock.Release(); }
    }

    record SearchResult(string Title, string Url, string Snippet);

    // -- UIA fallback: read Chrome a11y tree for search results ------------
    // Called when JS h3 selector returns 0 results (Google changed their HTML).
    // Finds the WebBot Chrome window, extracts Hyperlink elements from the page.
    static List<SearchResult> ExtractSearchResultsFromUia(int limit)
    {
        var results = new List<SearchResult>();
        try
        {
            // Find Chrome window with WKWebBot tag
            var wins = WindowFinder.FindWindows("*WKWebBot*");
            if (wins.Count == 0) return results;

            using var auto = new UIA3Automation();
            var root = auto.FromHandle(wins[0].Handle);

            var cf = auto.ConditionFactory;
            // Grab all hyperlinks from the page a11y tree
            var links = root.FindAllDescendants(cf.ByControlType(FlaUI.Core.Definitions.ControlType.Hyperlink));

            foreach (var link in links)
            {
                try
                {
                    var name = link.Properties.Name.ValueOrDefault ?? "";
                    if (string.IsNullOrWhiteSpace(name)) continue;

                    // URL: try Value pattern, else use Name as fallback
                    string url = "";
                    try
                    {
                        var vp = link.Patterns.Value;
                        if (vp.IsSupported) url = vp.Pattern.Value.ValueOrDefault ?? "";
                    }
                    catch { }

                    // Skip nav/UI links: no http, very short, or duplicates
                    if (string.IsNullOrEmpty(url) || !url.StartsWith("http")) continue;
                    if (name.Length < 5) continue;
                    if (results.Any(r => r.Url == url)) continue;

                    results.Add(new SearchResult(name, url, ""));
                    if (results.Count >= limit) break;
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SEARCH] UIA fallback error: {ex.Message}");
        }
        return results;
    }
}
