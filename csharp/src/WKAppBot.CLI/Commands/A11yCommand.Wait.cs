using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ═══ Wait Action: poll for window/element appearance ═══

    static int A11yWaitAction(string grap, int timeoutMs, int intervalMs)
    {
        // Parse extra wait options from grap args context
        // These are set by A11yCommand before calling: _waitCondition, _waitNot, _waitCdp
        var condition = _waitCondition;
        var waitNot = _waitNot;

        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grap);
        if (win32Segments.Length == 0)
            return Error("No window title in grap pattern");

        var firstSegPatterns = win32Segments[0]
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        bool needsUia = !string.IsNullOrEmpty(uiaPath);
        var modeStr = waitNot ? "disappear" : "appear";
        var condStr = condition != null ? $", condition=\"{condition}\"" : "";
        Console.Error.WriteLine($"[A11Y] wait — polling for \"{grap}\" to {modeStr} (timeout={timeoutMs}ms, interval={intervalMs}ms{condStr})");

        // CDP web wait: if grap contains CSS selector patterns
        bool isCdpWait = grap.Contains("#") && firstSegPatterns.Any(p =>
            p.Contains("*Chrome*", StringComparison.OrdinalIgnoreCase) ||
            p.Contains("*Edge*", StringComparison.OrdinalIgnoreCase));

        using var automation = needsUia && !isCdpWait ? new UIA3Automation() : null;
        if (automation != null)
        {
            automation.ConnectionTimeout = TimeSpan.FromSeconds(2);
            automation.TransactionTimeout = TimeSpan.FromSeconds(2);
        }

        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            bool found = false;
            string foundInfo = "";

            foreach (var pat in firstSegPatterns)
            {
                var wins = WindowFinder.FindByTitle(pat);
                if (wins.Count == 0) continue;

                var hwnd = wins[0].Handle;

                // Walk Win32 children
                bool childOk = true;
                for (int seg = 1; seg < win32Segments.Length; seg++)
                {
                    var child = WindowFinder.FindChildByPattern(hwnd, win32Segments[seg]);
                    if (child == null) { childOk = false; break; }
                    hwnd = child.Handle;
                }
                if (!childOk) continue;

                // Window-only wait (no UIA scope)
                if (!needsUia)
                {
                    found = true;
                    foundInfo = $"\"{wins[0].Title}\" (hwnd={hwnd:X8})";
                    break;
                }

                // UIA scope wait
                try
                {
                    var root = automation!.FromHandle(hwnd);
                    var scoped = GrapHelper.FindUiaScope(root, uiaPath!);
                    if (scoped != null)
                    {
                        var name = scoped.Properties.Name.ValueOrDefault ?? "";
                        var type = "?";
                        try { type = scoped.Properties.ControlType.ValueOrDefault.ToString(); } catch { }

                        // Condition check: text/name must contain condition string
                        if (condition != null)
                        {
                            var text = name;
                            try { text = scoped.Patterns.Value.PatternOrDefault?.Value?.ToString() ?? name; } catch { }
                            if (!text.Contains(condition, StringComparison.OrdinalIgnoreCase))
                                continue; // condition not met, keep polling
                        }

                        found = true;
                        foundInfo = $"[{type}] \"{name}\"";
                        break;
                    }
                }
                catch { /* UIA timeout on stale elements, keep polling */ }
            }

            // --not mode: wait until element DISAPPEARS
            if (waitNot)
            {
                if (!found)
                {
                    Console.Error.WriteLine($"[A11Y] wait --not — element gone after {sw.ElapsedMilliseconds}ms");
                    return 0;
                }
            }
            else
            {
                if (found)
                {
                    Console.Error.WriteLine($"[A11Y] wait — found after {sw.ElapsedMilliseconds}ms: {foundInfo}");
                    return 0;
                }
            }

            Thread.Sleep(intervalMs);
        }

        Console.Error.WriteLine($"[A11Y] wait — timeout after {timeoutMs}ms: \"{grap}\" ({modeStr})");
        return 1;
    }

    // Wait options (set by A11yCommand before calling A11yWaitAction)
    [ThreadStatic] static string? _waitCondition;
    [ThreadStatic] static bool _waitNot;

    // ═══ Eval Action: execute JavaScript via CDP ═══

    /// <summary>Deprecated wrapper — warns about eval removal, suggests --eval-js.</summary>
    static bool DeprecatedEval(IntPtr hwnd, string tag, string jsExpression, string? tabHint)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Error.WriteLine("[DEPRECATED] 'a11y eval' is scheduled for removal — target is ambiguous.");
        Console.Error.WriteLine("  Use --eval-js with any a11y action (tab auto-found when no #scope given):");
        Console.Error.WriteLine("    a11y read  \"*Chrome*#chatgpt.com\" --eval-js \"document.title\"");
        Console.Error.WriteLine("    a11y read  \"*Chrome*\" --eval-js \"document.title\"   (tab found by window title)");
        Console.Error.WriteLine("    a11y click \"*Chrome*#gemini.google.com/button.send\" --eval-js \"el.disabled=false\"");
        Console.Error.WriteLine("  Available CDP helpers (via CdpClient):");
        Console.Error.WriteLine("    GetUrlAsync, GetTitleAsync, FocusAsync, GetTextLengthAsync,");
        Console.Error.WriteLine("    IsHiddenAsync, GetTabStateAsync, QueryCountAsync, JsClickAsync");
        Console.ResetColor();
        AutoRegisterBug($"[USAGE-DEPRECATED] a11y eval used — scheduled for removal. tag={tag} hint={tabHint ?? "none"} expr={jsExpression[..Math.Min(60, jsExpression.Length)]}");
        return A11yEvalJs(hwnd, tag, jsExpression, tabHint);
    }

    static bool A11yEvalJs(IntPtr hwnd, string tag, string jsExpression, string? tabHint)
    {
        try
        {
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var port = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)pid);
            if (port <= 0)
            {
                Console.Error.WriteLine($"[A11Y] eval — no CDP port found for {tag}");
                return false;
            }

            using var cdp = new WKAppBot.WebBot.CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();

            // Tab matching: prefer #scope hint, fallback to window title
            var windowTitle = WindowFinder.GetWindowText(hwnd);
            var tabMatch = !string.IsNullOrEmpty(tabHint) ? tabHint : windowTitle;
            if (!string.IsNullOrEmpty(tabMatch))
            {
                var tabs = cdp.ListTabsAsync(port).GetAwaiter().GetResult();
                foreach (var tab in tabs)
                {
                    if (tab.Title.Contains(tabMatch, StringComparison.OrdinalIgnoreCase) ||
                        tabMatch.Contains(tab.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        if (tab.Id != cdp.TargetId)
                            cdp.SwitchToTargetAsync(tab.Id, port).GetAwaiter().GetResult();
                        break;
                    }
                }
            }

            var result = cdp.EvalAsync(jsExpression).GetAwaiter().GetResult();
            Console.Error.WriteLine($"[A11Y] eval result: {result}");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] eval — error: {ex.Message}");
            return false;
        }
    }
}
