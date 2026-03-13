using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int AskCommand(string[] args)
    {
        if (args.Length < 2)
            return AskUsage();

        var ai = args[0].ToLowerInvariant();
        string? modelHint = null;
        bool slackReport = false;
        bool newTab = false;
        bool newSession = false;
        bool noWait = false;
        bool loopMode = false;
        bool triadMode = false;
        int loopMaxSteps = 7;
        int loopRetry = 1;
        int loopMaxParallel = 7;
        int timeoutSec = 30;
        var remaining = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--slack")
                slackReport = true;
            else if (args[i] == "--new-tab")
                newTab = true;
            else if (args[i] == "--new-session")
                newSession = true;
            else if (args[i] == "--no-wait")
                noWait = true;
            else if (args[i] == "--loop")
            {
                loopMode = true;
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out var loopN))
                {
                    loopMaxSteps = Math.Max(1, loopN);
                    i++;
                }
            }
            else if (args[i] == "--max-steps" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var maxSteps))
                    loopMaxSteps = Math.Max(1, maxSteps);
            }
            else if (args[i] == "--retry" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var retryN))
                    loopRetry = Math.Max(0, retryN);
            }
            else if (args[i] == "--max-parallel" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var mpN))
                    loopMaxParallel = Math.Max(1, mpN);
            }
            else if (args[i] == "--model" && i + 1 < args.Length)
                modelHint = args[++i];
            else if (args[i] == "--triad")
                triadMode = true;
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else if (args[i] == "--image" && i + 1 < args.Length)
                remaining.Add(args[++i]);
            else
                remaining.Add(args[i]);
        }

        var (questionParts, attachFiles) = ParseTextAndFilesWithMarkers(remaining.ToArray());
        var question = string.Join("\n", questionParts);
        if (string.IsNullOrWhiteSpace(question))
            return AskUsage();

        foreach (var f in attachFiles)
        {
            if (!File.Exists(f))
                return Error($"File not found: {f}");
        }

        if (attachFiles.Count > 0)
            Console.WriteLine($"[ASK] Attaching {attachFiles.Count} file(s): {string.Join(", ", attachFiles.Select(Path.GetFileName))}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] Model hint: {modelHint}");
        if (noWait && loopMode)
        {
            Console.WriteLine("[ASK] --no-wait enabled: loop mode is ignored for this request.");
            loopMode = false;
        }

        return ai switch
        {
            "gemini" => AskGemini(question, slackReport, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait),
            "gpt" or "chatgpt" => AskChatGpt(question, slackReport, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait),
            "claude" => AskClaude(question, slackReport, timeoutSec, newTab, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait),
            _ => Error($"Unknown AI: {ai} (use gemini, gpt, or claude)")
        };
    }

    static string BuildNoWaitQueuedMessage(string aiName)
    {
        var logDir = Path.Combine(DataDir, "logs");
        return $"[{aiName}] Prompt queued. Returning control immediately (--no-wait). Check ongoing output/logs at: {logDir}";
    }

    static int AskUsage()
    {
        Console.WriteLine(@"
WKAppBot Ask ??one-command AI Q&A via WebBot

Usage:
  wkappbot ask gemini ""question"" [files...] [--slack] [--timeout 30] [--new-tab] [--new-session] [--loop [N]] [--max-steps N] [--retry N] [--triad]
  wkappbot ask gpt ""question"" [files...]   [--slack] [--timeout 30] [--new-tab] [--new-session] [--loop [N]] [--max-steps N] [--retry N] [--triad]
  wkappbot ask claude ""question""            [--slack] [--timeout 30] [--new-tab] [--new-session] [--loop [N]] [--max-steps N] [--retry N] [--triad]

Options:
  --slack         Report answer to Slack channel
  --timeout N     Max seconds to wait for response (default: 30)
  --new-tab       Open in a new tab (default: reuse existing tab)
  --new-session   Start fresh conversation in existing tab (navigate to new chat URL)
  --no-wait       Return immediately after prompt send (do not wait for model response)
  --loop [N]      Enable remote tool-exec loop (default max steps: 3)
  --max-steps N   Max loop steps (same as --loop N)
  --retry N       Tool execution retry count per step (default: 1)
  --model 4.1     Model/version hint for remote AI (provider remains ask <ai>)
  --triad         Add triad-planning hints to loop persona

File attachment:
  Any argument that matches an existing file path is auto-attached.
  Images (png/jpg/gif/webp/bmp) ??clipboard paste, other files ??file input.
  Multiple files supported ??attached in order before sending the question.

Examples:
  wkappbot ask gemini ""?ㅻ뒛 肄붿뒪???뱀쭠二??뚮젮以?""
  wkappbot ask gpt ""???⑦꽩 遺꾩꽍?댁쨾"" --slack
  wkappbot ask gpt ""??UI 遺꾩꽍?댁쨾"" screenshot.png
  wkappbot ask gpt ""肄붾뱶 由щ럭?댁쨾"" main.cs test.log
  wkappbot ask gemini ""???몄뀡?쇰줈 吏덈Ц"" --new-session
");
        return 1;
    }

    static bool EnsureTabViaGrap(string browserGrap, string tabPattern)
    {
        try
        {
            using var automation = new UIA3Automation();
            var resolved = GrapHelper.ResolveFullGrap(browserGrap, automation);
            if (resolved == null || resolved.Value.error != null)
            {
                Console.WriteLine($"[ASK] GrapHelper: browser not found ({browserGrap})");
                return false;
            }

            var (_, root, _) = resolved.Value;
            var tab = GrapHelper.FindByNameOrAid(root, tabPattern);
            if (tab == null)
            {
                Console.WriteLine($"[ASK] GrapHelper: tab \"{tabPattern}\" not found");
                return false;
            }

            if (!GrapHelper.IsTabItem(tab))
            {
                Console.WriteLine($"[ASK] GrapHelper: \"{tabPattern}\" matched but not a TabItem (already active?)");
                return true;
            }

            ClickZoomHelper? tabZoom = null;
            try
            {
                var tabRect = tab.BoundingRectangle;
                if (tabRect.Width > 0 && tabRect.Height > 0)
                {
                    var hwnd = resolved.Value.hwnd;
                    var screenRect = new System.Drawing.Rectangle(
                        (int)tabRect.X, (int)tabRect.Y, (int)tabRect.Width, (int)tabRect.Height);
                    tabZoom = ClickZoomHelper.BeginFromRect(screenRect, hwnd, "tab-select", tabPattern);
                }
            }
            catch { }

            var ok = GrapHelper.SwitchToTab(tab);
            if (ok)
                tabZoom?.ShowPass("tab activated");
            else
                tabZoom?.ShowFail("tab switch failed");
            tabZoom?.Dispose();
            return ok;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] GrapHelper tab routing failed: {ex.Message}");
            return false;
        }
    }

    sealed record CdpPageTarget(string Id, string Url, string Title, string WebSocketDebuggerUrl);

    static string BuildAskTargetTag(string provider)
    {
        var hash = GetSessionTag() ?? "default";
        return $"{provider}-{hash}";
    }

    static CdpClient? EnsureCdpConnection(int port = 9222, string? preferredHost = null, bool newTab = false, string? targetTag = null)
    {
        var task = Task.Run(async () =>
        {
            try
            {
                var active = await ChromeLauncher.IsPortActiveAsync(port);
                if (!active)
                {
                    var launchUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    Console.WriteLine($"[ASK] Launching Chrome ??{launchUrl ?? "about:blank"}...");
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl);
                    await Task.Delay(2500);
                }

                var cdp = new CdpClient();
                await cdp.ConnectAsync(port, timeoutMs: 5000);

                var savedTargetId = !string.IsNullOrWhiteSpace(targetTag) ? AskTargetRegistry.GetTargetId(targetTag) : null;

                await CloseBlankTabs(port);
                if (!string.IsNullOrWhiteSpace(preferredHost))
                    await CloseStaleDuplicateTabs(port, preferredHost, savedTargetId);
                if (savedTargetId != null)
                    Console.WriteLine($"[ASK] Registry hit: {targetTag} ??{savedTargetId[..Math.Min(8, savedTargetId.Length)]}");

                var navigateUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                var resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag, navigateUrl, savedTargetId: savedTargetId);

                if (!string.IsNullOrWhiteSpace(preferredHost))
                {
                    var tabUrl = await cdp.EvalAsync("location.href") ?? "";
                    if (tabUrl == "about:blank" || !tabUrl.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"[ASK] Wrong tab: {tabUrl} (expected {preferredHost})");

                        if (tabUrl == "about:blank")
                        {
                            Console.WriteLine("[ASK] Closing about:blank...");
                            try { await cdp.EvalAsync("window.close()"); } catch { }
                            await Task.Delay(500);
                        }

                        if (!string.IsNullOrWhiteSpace(targetTag))
                            AskTargetRegistry.SetTargetId(targetTag, null!);

                        Console.WriteLine($"[ASK] Reconnecting to {preferredHost}...");
                        await cdp.ConnectAsync(port, timeoutMs: 5000);
                        resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag, $"https://{preferredHost}", savedTargetId: null);
                        await Task.Delay(2000);

                        tabUrl = await cdp.EvalAsync("location.href") ?? "";
                        if (!tabUrl.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                        {
                            await cdp.NavigateAsync($"https://{preferredHost}");
                            await Task.Delay(3000);
                        }

                        Console.WriteLine($"[ASK] Now on: {await cdp.EvalAsync("location.href")}");
                    }
                }

                if (resolvedId != null && !string.IsNullOrWhiteSpace(targetTag))
                {
                    AskTargetRegistry.SetTargetId(targetTag, resolvedId);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[ASK] Target: {targetTag} ??{resolvedId[..Math.Min(8, resolvedId.Length)]}");
                    Console.ResetColor();
                }

                var chromeHwnd = cdp.GetChromeWindowHandle();
                if (chromeHwnd != IntPtr.Zero)
                {
                    var readiness = new WKAppBot.Win32.Input.InputReadiness();
                    var blocker = readiness.DetectBlocker(chromeHwnd);
                    if (blocker != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[AAR:CDP] Blocker: {blocker.Title} (hwnd={blocker.Handle:X})");
                        Console.ResetColor();
                    }
                    if (NativeMethods.IsIconic(chromeHwnd))
                    {
                        Console.WriteLine($"[AAR:CDP] Chrome minimized ??focusless restore");
                        NativeMethods.ShowWindow(chromeHwnd, 4);
                        await Task.Delay(300);
                    }
                }

                cdp.EnableFocusTheftMonitoring = true;
                cdp.OnFocusTheft = (method, prevFg, curFg) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[ASK:FOCUS] ??STOLEN @ CDP:{method}: was={prevFg:X8} now={curFg:X8} ??restoring");
                    Console.ResetColor();
                };

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
                var id = node?["id"]?.GetValue<string>() ?? string.Empty;
                result.Add(new CdpPageTarget(id, url, title, ws));
            }
        }
        catch { }

        return result;
    }
}
