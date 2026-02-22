using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

// partial class: wkappbot ask <ai> "question" — one-command AI Q&A via WebBot
internal partial class Program
{
    static int AskCommand(string[] args)
    {
        if (args.Length < 2)
            return AskUsage();

        var ai = args[0].ToLowerInvariant();
        // Collect question (everything after AI name, excluding flags)
        var questionParts = new List<string>();
        bool slackReport = false;
        bool newTab = false;
        int timeoutSec = 30;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--slack")
                slackReport = true;
            else if (args[i] == "--new-tab")
                newTab = true;
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else
                questionParts.Add(args[i]);
        }
        var question = string.Join(" ", questionParts);
        if (string.IsNullOrWhiteSpace(question))
            return AskUsage();

        return ai switch
        {
            "gemini" => AskGemini(question, slackReport, timeoutSec, newTab),
            "gpt" or "chatgpt" => AskChatGpt(question, slackReport, timeoutSec, newTab),
            _ => Error($"Unknown AI: {ai} (use gemini or gpt)")
        };
    }

    static int AskUsage()
    {
        Console.WriteLine(@"
WKAppBot Ask — one-command AI Q&A via WebBot

Usage:
  wkappbot ask gemini ""question""  [--slack] [--timeout 30] [--new-tab]
  wkappbot ask gpt ""question""     [--slack] [--timeout 30] [--new-tab]

Options:
  --slack       Report answer to Slack channel
  --timeout N   Max seconds to wait for response (default: 30)
  --new-tab     Open in a new tab (default: reuse existing tab)

Examples:
  wkappbot ask gemini ""오늘 코스피 특징주 알려줘""
  wkappbot ask gpt ""이 패턴 분석해줘"" --slack
  wkappbot ask gpt ""새 탭으로 테스트"" --new-tab
");
        return 1;
    }

    sealed record CdpPageTarget(string Url, string Title, string WebSocketDebuggerUrl);

    /// <summary>
    /// Connect to CDP, launching Chrome if needed.
    /// Default behavior: reuse an existing matching tab (single-tab friendly).
    /// </summary>
    static CdpClient? EnsureCdpConnection(int port = 9222, string? preferredHost = null, bool newTab = false)
    {
        var task = Task.Run(async () =>
        {
            // Ensure Chrome/CDP is alive first
            var active = await ChromeLauncher.IsPortActiveAsync(port);
            if (!active)
            {
                Console.WriteLine("[ASK] Launching Chrome...");
                await ChromeLauncher.LaunchAsync(port: port);
                await Task.Delay(1500);
            }

            // Discover page targets
            var pages = await GetPageTargetsAsync(port);
            if (pages.Count == 0)
            {
                Console.WriteLine("[ASK] No page target found on CDP");
                return (CdpClient?)null;
            }

            int targetIndex = 0;

            // Reuse existing tab by default
            if (!string.IsNullOrWhiteSpace(preferredHost))
            {
                for (int i = 0; i < pages.Count; i++)
                {
                    var u = pages[i].Url ?? "";
                    if (u.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                    {
                        targetIndex = i;
                        break;
                    }
                }
            }

            // Optional: open a new tab explicitly
            if (newTab)
            {
                var newTargetUrl = "about:blank";
                try
                {
                    using var http = new HttpClient();
                    _ = await http.GetStringAsync($"http://localhost:{port}/json/new?{Uri.EscapeDataString(newTargetUrl)}");
                    pages = await GetPageTargetsAsync(port);
                    targetIndex = Math.Max(0, pages.Count - 1); // newest page target
                }
                catch
                {
                    // Fallback silently to reuse mode
                }
            }

            try
            {
                var cdp = new CdpClient();
                await cdp.ConnectAsync(port, tabIndex: targetIndex, timeoutMs: 5000);
                return (CdpClient?)cdp;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Failed to connect: {ex.Message}");
                return (CdpClient?)null;
            }
        });
        return task.GetAwaiter().GetResult();
    }

    static async Task<List<CdpPageTarget>> GetPageTargetsAsync(int port)
    {
        var result = new List<CdpPageTarget>();
        try
        {
            using var http = new HttpClient();
            var json = await http.GetStringAsync($"http://localhost:{port}/json");
            var arr = JsonSerializer.Deserialize<JsonArray>(json);
            if (arr == null) return result;

            foreach (var node in arr)
            {
                var type = node?["type"]?.GetValue<string>() ?? "";
                if (!string.Equals(type, "page", StringComparison.OrdinalIgnoreCase))
                    continue;

                var ws = node?["webSocketDebuggerUrl"]?.GetValue<string>() ?? "";
                if (string.IsNullOrWhiteSpace(ws))
                    continue;

                var url = node?["url"]?.GetValue<string>() ?? "";
                var title = node?["title"]?.GetValue<string>() ?? "";
                result.Add(new CdpPageTarget(url, title, ws));
            }
        }
        catch { }
        return result;
    }

    /// <summary>Insert text into contentEditable editor (Quill/ProseMirror universal pattern).</summary>
    static async Task<bool> InsertTextContentEditable(CdpClient cdp, string selector, string text)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");
        var js = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.focus();
                var sel = window.getSelection();
                var range = document.createRange();
                range.selectNodeContents(el);
                range.collapse(false);
                sel.removeAllRanges();
                sel.addRange(range);
                document.execCommand('insertText', false, '{{escaped}}');
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        var result = await cdp.EvalAsync(js);
        return result == "OK";
    }

    /// <summary>Clear contentEditable editor.</summary>
    static async Task ClearContentEditable(CdpClient cdp, string selector)
    {
        await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return;
                el.focus();
                document.execCommand('selectAll');
                document.execCommand('delete');
            })()
            """);
    }

    // ── Gemini ──

    static int AskGemini(string question, bool slackReport, int timeoutSec, bool newTab)
    {
        Console.WriteLine($"[ASK] Gemini: {question}");

        var cdp = EnsureCdpConnection(preferredHost: "gemini.google.com", newTab: newTab);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);

        var task = Task.Run(async () =>
        {
            try
            {
                // Navigate to Gemini if not already there
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                if (!currentUrl.Contains("gemini.google.com"))
                {
                    Console.WriteLine("[ASK] Navigating to Gemini...");
                    await cdp.NavigateAsync("https://gemini.google.com/app");
                    await Task.Delay(3000);
                }

                // Wait for editor
                for (int i = 0; i < 10; i++)
                {
                    var found = await cdp.EvalAsync("document.querySelector('.ql-editor') ? 'yes' : 'no'");
                    if (found == "yes") break;
                    await Task.Delay(500);
                }

                // Clear and insert question
                await ClearContentEditable(cdp, ".ql-editor");
                await Task.Delay(200);
                var inserted = await InsertTextContentEditable(cdp, ".ql-editor", question);
                if (!inserted)
                {
                    Console.WriteLine("[ASK] Failed to insert text");
                    return (false, (string?)null);
                }

                // Click send
                await Task.Delay(300);
                var sendResult = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label="메시지 보내기"]')
                               || document.querySelector('button.send-button');
                        if (!btn) return 'NO_BUTTON';
                        btn.click();
                        return 'SENT';
                    })()
                    """);
                if (sendResult != "SENT")
                {
                    Console.WriteLine($"[ASK] Send failed: {sendResult}");
                    return (false, (string?)null);
                }

                Console.WriteLine("[ASK] Sent! Waiting for response...");

                // Wait for response — poll until text stabilizes
                string? lastText = null;
                int stableCount = 0;
                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(2000);
                    var text = await cdp.EvalAsync("""
                        (() => {
                            var responses = document.querySelectorAll('model-response');
                            if (responses.length === 0) return '';
                            return responses[responses.length - 1].textContent;
                        })()
                        """);

                    if (string.IsNullOrEmpty(text))
                        continue;

                    // Check if response is still generating
                    if (text == lastText)
                    {
                        stableCount++;
                        if (stableCount >= 2) // stable for 4+ seconds
                        {
                            Console.WriteLine($"[ASK] Response received ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            return (true, text);
                        }
                    }
                    else
                    {
                        stableCount = 0;
                        lastText = text;
                    }
                }

                // Timeout — return whatever we have
                if (!string.IsNullOrEmpty(lastText))
                {
                    Console.WriteLine($"[ASK] Timeout — partial response ({lastText.Length} chars)");
                    return (true, lastText);
                }
                Console.WriteLine("[ASK] Timeout — no response");
                return (false, (string?)null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            // Print answer (truncate for console)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── Gemini 답변 ──");
            Console.ResetColor();
            // Remove "Gemini의 응답" prefix if present
            var cleaned = answer.StartsWith("Gemini의 응답") ? answer["Gemini의 응답".Length..] : answer;
            Console.WriteLine(cleaned.Length > 2000 ? cleaned[..2000] + "\n... (truncated)" : cleaned);

            if (slackReport)
                ReportToSlack("Gemini", question, cleaned);
        }

        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── ChatGPT ──

    static int AskChatGpt(string question, bool slackReport, int timeoutSec, bool newTab)
    {
        Console.WriteLine($"[ASK] ChatGPT: {question}");

        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);

        var task = Task.Run(async () =>
        {
            try
            {
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                if (!currentUrl.Contains("chatgpt.com"))
                {
                    Console.WriteLine("[ASK] Navigating to ChatGPT...");
                    await cdp.NavigateAsync("https://chatgpt.com");
                    await Task.Delay(3000);
                }

                // Wait for editor
                for (int i = 0; i < 10; i++)
                {
                    var found = await cdp.EvalAsync("document.querySelector('#prompt-textarea') ? 'yes' : 'no'");
                    if (found == "yes") break;
                    await Task.Delay(500);
                }

                // Insert question
                var inserted = await InsertTextContentEditable(cdp, "#prompt-textarea", question);
                if (!inserted)
                {
                    Console.WriteLine("[ASK] Failed to insert text");
                    return (false, (string?)null);
                }

                // Click send
                await Task.Delay(300);
                var sendResult = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[data-testid="send-button"]');
                        if (!btn) {
                            // Fallback: find button near composer with SVG
                            var btns = document.querySelectorAll('button');
                            for (var b of btns) {
                                if (b.querySelector('svg') && b.closest('[class*="composer"]')) {
                                    b.click();
                                    return 'SENT';
                                }
                            }
                            return 'NO_BUTTON';
                        }
                        btn.click();
                        return 'SENT';
                    })()
                    """);
                if (sendResult != "SENT")
                {
                    Console.WriteLine($"[ASK] Send failed: {sendResult}");
                    return (false, (string?)null);
                }

                Console.WriteLine("[ASK] Sent! Waiting for response...");

                // Wait for response
                string? lastText = null;
                int stableCount = 0;
                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(2000);
                    var text = await cdp.EvalAsync("""
                        (() => {
                            var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                            if (msgs.length === 0) return '';
                            return msgs[msgs.length - 1].textContent;
                        })()
                        """);

                    if (string.IsNullOrEmpty(text))
                        continue;

                    if (text == lastText)
                    {
                        stableCount++;
                        if (stableCount >= 2)
                        {
                            Console.WriteLine($"[ASK] Response received ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            return (true, text);
                        }
                    }
                    else
                    {
                        stableCount = 0;
                        lastText = text;
                    }
                }

                if (!string.IsNullOrEmpty(lastText))
                {
                    Console.WriteLine($"[ASK] Timeout — partial response ({lastText.Length} chars)");
                    return (true, lastText);
                }
                Console.WriteLine("[ASK] Timeout — no response");
                return (false, (string?)null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("── ChatGPT 답변 ──");
            Console.ResetColor();
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);

            if (slackReport)
                ReportToSlack("ChatGPT", question, answer);
        }

        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── Slack Report ──

    static void ReportToSlack(string aiName, string question, string answer)
    {
        try
        {
            var config = LoadSlackConfig();
            if (config == null) return;

            var botToken = config["bot_token"]?.GetValue<string>();
            var channel = config["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            var truncated = answer.Length > 1500 ? answer[..1500] + "\n... (truncated)" : answer;
            var msg = $"*[{aiName} 답변]*\n> Q: {question}\n\n{truncated}";
            var (ok, _) = SlackSendViaApi(botToken, channel, msg, username: BotUsername).GetAwaiter().GetResult();
            if (ok)
                Console.WriteLine($"[ASK] Reported to Slack");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] Slack report failed: {ex.Message}");
        }
    }
}
