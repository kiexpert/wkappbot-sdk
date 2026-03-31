using System.Diagnostics;
using System.Text.RegularExpressions;
using WKAppBot.Win32;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: Claude Desktop usage probe via UIA (--force-renderer-accessibility required)
internal partial class Program
{
    const string ClaudeExePath = @"C:\Users\edenc\AppData\Local\AnthropicClaude\app-1.1.5749\claude.exe";
    const string ClaudeA11yFlag = "--force-renderer-accessibility";
    const string ClaudeGrap = "*(claude hwnd=*";

    /// <summary>
    /// Full pipeline: ensure Claude Desktop running with a11y → open settings/usage → read → close settings.
    /// Returns parsed usage or null on failure.
    /// </summary>
    static ClaudeUsageReport? ProbeClaudeUsage(bool verbose = true)
    {
        // ── Step 1: Find or launch Claude Desktop ──
        var claudeWindow = FindClaudeDesktopWindowInfo();
        if (claudeWindow == null)
        {
            if (verbose) Console.WriteLine("[USAGE] Claude Desktop not running — launching with a11y flag...");
            try
            {
                AppBotPipe.Spawn(ClaudeExePath, ClaudeA11yFlag,
                    EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory,
                    requiresFocus: true,
                    caller: "CLAUDE-USAGE");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[USAGE] Failed to launch Claude Desktop: {ex.Message}");
                return null;
            }

            // Wait for window
            for (int i = 0; i < 20; i++) // 10 seconds max
            {
                Thread.Sleep(500);
                claudeWindow = FindClaudeDesktopWindowInfo();
                if (claudeWindow != null) break;
            }

            if (claudeWindow == null)
            {
                Console.Error.WriteLine("[USAGE] Claude Desktop window not found after launch");
                return null;
            }
            if (verbose) Console.WriteLine($"[USAGE] Claude Desktop launched: hwnd=0x{claudeWindow.Handle.ToInt64():X}");
        }

        var hwnd = claudeWindow.Handle;

        // ── Step 2: Check if a11y flag is active (RootWebArea should have children) ──
        using var automation = new FlaUI.UIA3.UIA3Automation();
        var uiaWin = automation.FromHandle(hwnd);
        if (uiaWin == null)
        {
            Console.Error.WriteLine("[USAGE] Cannot attach UIA to Claude Desktop");
            return null;
        }

        // ── Step 3: Navigate to settings/usage (always refresh via tab bounce) ──
        var usageTexts = FindUsageTexts(uiaWin);
        if (usageTexts.Count == 0)
        {
            // Not on usage page yet — open settings first
            if (verbose) Console.WriteLine("[USAGE] Navigating to settings/usage page...");
            if (!NavigateToUsagePage(uiaWin, hwnd, verbose))
            {
                Console.Error.WriteLine("[USAGE] Failed to navigate to usage page");
                return null;
            }
            Thread.Sleep(500);
        }
        else
        {
            // Already on usage page — bounce to "일반" tab and back for fresh data
            if (verbose) Console.WriteLine("[USAGE] Refreshing usage data (tab bounce)...");
            RefreshUsageTab(uiaWin);
        }

        usageTexts = FindUsageTexts(uiaWin);
        if (usageTexts.Count == 0)
        {
            Console.Error.WriteLine("[USAGE] No usage data found on page");
            return null;
        }

        // ── Step 4: Parse usage data ──
        var report = ParseUsageTexts(usageTexts);
        if (verbose)
        {
            Console.WriteLine($"[USAGE] Session: {report.SessionPercent}% (resets in {report.SessionReset})");
            Console.WriteLine($"[USAGE] Weekly All Models: {report.WeeklyAllPercent}% (resets {report.WeeklyAllReset})");
            Console.WriteLine($"[USAGE] Weekly Sonnet: {report.WeeklySonnetPercent}% (resets {report.WeeklySonnetReset})");
        }

        // ── Step 5: Close settings page ──
        try
        {
            var closeBtn = FindUiaByName(uiaWin, "설정 닫기", 10);
            closeBtn?.Patterns.Invoke.PatternOrDefault?.Invoke();
            if (verbose) Console.WriteLine("[USAGE] Settings page closed");
        }
        catch { /* best-effort */ }

        return report;
    }

    static WindowInfo? FindClaudeDesktopWindowInfo()
    {
        foreach (var w in WindowFinder.FindByTitle("*Claude*"))
        {
            NativeMethods.GetWindowThreadProcessId(w.Handle, out uint pid);
            try
            {
                var proc = Process.GetProcessById((int)pid);
                if (proc.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase)
                    && w.Title == "Claude")
                    return w;
            }
            catch { }
        }
        return null;
    }

    /// <summary>
    /// Bounce to "일반" tab and back to "사용량" to force data refresh (all focusless UIA Invoke).
    /// </summary>
    static void RefreshUsageTab(FlaUI.Core.AutomationElements.AutomationElement root)
    {
        var generalTab = FindUiaByName(root, "일반", 15);
        if (generalTab != null)
        {
            generalTab.Patterns.Invoke.PatternOrDefault?.Invoke();
            Thread.Sleep(300);
        }
        var usageTab = FindUiaByName(root, "사용량", 15);
        usageTab?.Patterns.Invoke.PatternOrDefault?.Invoke();
        Thread.Sleep(500); // let usage page render fresh data
    }

    static bool NavigateToUsagePage(FlaUI.Core.AutomationElements.AutomationElement root, IntPtr hwnd, bool verbose)
    {
        // Try 1: Menu → 파일 → 설정
        var menuBtn = FindUiaByName(root, "Menu", 10);
        if (menuBtn != null)
        {
            menuBtn.Patterns.Invoke.PatternOrDefault?.Invoke();
            Thread.Sleep(300);

            // Re-scan for 파일 menu
            var fileMenu = FindUiaByName(root, "파일", 15);
            if (fileMenu != null)
            {
                fileMenu.Patterns.ExpandCollapse.PatternOrDefault?.Expand();
                Thread.Sleep(300);

                var settingsItem = FindUiaByNameWildcard(root, "설정*", 15);
                if (settingsItem != null)
                {
                    settingsItem.Patterns.Invoke.PatternOrDefault?.Invoke();
                    Thread.Sleep(500);

                    // Click 사용량 tab
                    var usageTab = FindUiaByName(root, "사용량", 15);
                    if (usageTab != null)
                    {
                        usageTab.Patterns.Invoke.PatternOrDefault?.Invoke();
                        Thread.Sleep(300);
                        return true;
                    }
                }
            }
        }

        if (verbose) Console.Error.WriteLine("[USAGE] Menu navigation failed");
        return false;
    }

    static List<(string Name, string Type)> FindUsageTexts(FlaUI.Core.AutomationElements.AutomationElement root)
    {
        var results = new List<(string, string)>();
        var usagePattern = new Regex(@"(\d+)%\s*사용됨");

        void Walk(FlaUI.Core.AutomationElements.AutomationElement el, int depth)
        {
            if (depth > 15) return;
            try
            {
                var name = el.Properties.Name.ValueOrDefault;
                if (!string.IsNullOrEmpty(name))
                {
                    if (usagePattern.IsMatch(name))
                        results.Add((name, "usage"));
                    else if (name.Contains("재설정"))
                        results.Add((name, "reset"));
                    else if (name is "현재 세션" or "모든 모델" or "Sonnet만")
                        results.Add((name, "label"));
                }

                foreach (var child in el.FindAllChildren())
                    Walk(child, depth + 1);
            }
            catch { }
        }

        Walk(root, 0);
        return results;
    }

    static ClaudeUsageReport ParseUsageTexts(List<(string Name, string Type)> texts)
    {
        var report = new ClaudeUsageReport();
        var percentPattern = new Regex(@"(\d+)%\s*사용됨");

        // Group by position: session, weekly-all, weekly-sonnet
        var usages = texts.Where(t => t.Type == "usage").ToList();
        var resets = texts.Where(t => t.Type == "reset").ToList();

        if (usages.Count >= 1)
        {
            var m = percentPattern.Match(usages[0].Name);
            if (m.Success) report.SessionPercent = int.Parse(m.Groups[1].Value);
        }
        if (usages.Count >= 2)
        {
            var m = percentPattern.Match(usages[1].Name);
            if (m.Success) report.WeeklyAllPercent = int.Parse(m.Groups[1].Value);
        }
        if (usages.Count >= 3)
        {
            var m = percentPattern.Match(usages[2].Name);
            if (m.Success) report.WeeklySonnetPercent = int.Parse(m.Groups[1].Value);
        }

        if (resets.Count >= 1) report.SessionReset = resets[0].Name;
        if (resets.Count >= 2) report.WeeklyAllReset = resets[1].Name;
        if (resets.Count >= 3) report.WeeklySonnetReset = resets[2].Name;

        return report;
    }

    static FlaUI.Core.AutomationElements.AutomationElement? FindUiaByName(
        FlaUI.Core.AutomationElements.AutomationElement root, string name, int maxDepth)
    {
        if (maxDepth <= 0) return null;
        try
        {
            var n = root.Properties.Name.ValueOrDefault;
            if (n == name) return root;
            foreach (var child in root.FindAllChildren())
            {
                var found = FindUiaByName(child, name, maxDepth - 1);
                if (found != null) return found;
            }
        }
        catch { }
        return null;
    }

    static FlaUI.Core.AutomationElements.AutomationElement? FindUiaByNameWildcard(
        FlaUI.Core.AutomationElements.AutomationElement root, string pattern, int maxDepth)
    {
        if (maxDepth <= 0) return null;
        try
        {
            var n = root.Properties.Name.ValueOrDefault;
            if (!string.IsNullOrEmpty(n) && WildcardMatch(n, pattern)) return root;
            foreach (var child in root.FindAllChildren())
            {
                var found = FindUiaByNameWildcard(child, pattern, maxDepth - 1);
                if (found != null) return found;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Try to read Claude usage from claude.ai/settings/usage via CDP (Chrome, no app needed).
    /// Falls back to UIA approach if CDP unavailable or not logged in.
    /// </summary>
    static ClaudeUsageReport? ProbeClaudeUsageViaWeb()
    {
        try
        {
            _cdpBrowserLock.Wait(3000);
            try
            {
                var cdp = ConnectCdp(9222, withBar: false, navigateUrl: ClaudeUsageUrl);
                cdp.NavigateAsync(ClaudeUsageUrl).GetAwaiter().GetResult();

                var js = """
                    new Promise(resolve => {
                        let tries = 0;
                        const check = () => {
                            const t = document.body?.innerText?.trim() || '';
                            // Wait until usage % appears or 3s
                            if (t.includes('%') || ++tries >= 15) resolve(t);
                            else setTimeout(check, 200);
                        };
                        check();
                    })
                    """;

                var text = cdp.EvalAsync(js, awaitPromise: true).GetAwaiter().GetResult() ?? "";
                if (text.StartsWith('"') && text.EndsWith('"'))
                    text = System.Text.Json.JsonSerializer.Deserialize<string>(text) ?? "";

                if (string.IsNullOrWhiteSpace(text) || !text.Contains('%'))
                    return null; // not logged in or page didn't load

                Console.WriteLine($"[USAGE] Web page loaded ({text.Length} chars)");
                return ParseUsageFromWebText(text);
            }
            finally { _cdpBrowserLock.Release(); }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[USAGE] Web probe failed: {ex.Message} — falling back to UIA");
            return null;
        }
    }

    static ClaudeUsageReport ParseUsageFromWebText(string text)
    {
        var report = new ClaudeUsageReport();
        // Matches "42%" or "42% 사용됨" or "42 % used"
        var pctRx  = new Regex(@"(\d+)\s*%");
        var resetRx = new Regex(@"재설정[^\n]{0,40}|reset[^\n]{0,40}", RegexOptions.IgnoreCase);

        var pcts   = pctRx.Matches(text).Select(m => int.Parse(m.Groups[1].Value)).ToList();
        var resets = resetRx.Matches(text).Select(m => m.Value.Trim()).ToList();

        if (pcts.Count >= 1) report.SessionPercent      = pcts[0];
        if (pcts.Count >= 2) report.WeeklyAllPercent    = pcts[1];
        if (pcts.Count >= 3) report.WeeklySonnetPercent = pcts[2];

        if (resets.Count >= 1) report.SessionReset    = resets[0];
        if (resets.Count >= 2) report.WeeklyAllReset  = resets[1];
        if (resets.Count >= 3) report.WeeklySonnetReset = resets[2];

        return report;
    }

    /// <summary>
    /// claude-usage — one-shot usage probe with Slack alert.
    /// Web-first (CDP, no app needed) → UIA fallback (Claude Desktop).
    /// </summary>
    static int A11yClaudeUsage()
    {
        // Try web first (fast, no app needed)
        Console.WriteLine("[USAGE] Trying web probe (claude.ai/settings/usage)...");
        var report = ProbeClaudeUsageViaWeb();

        // Fallback to UIA if web failed
        if (report == null)
        {
            Console.WriteLine("[USAGE] Web probe unavailable — probing via Claude Desktop UIA...");
            report = ProbeClaudeUsage(verbose: true);
        }

        if (report == null) return 1;

        var sessionRemain = 100 - report.SessionPercent;
        var weeklyRemain = 100 - report.WeeklyAllPercent;

        // Summary line
        Console.WriteLine();
        // Session JSONL file size — try current CWD, then scan all sessions
        string jsonlInfo = "";
        try
        {
            var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            var (pct, _, _, fileSize) = GetContextInfoForCwdEx(cwd);
            if (fileSize <= 0)
            {
                // Fallback: find largest active session JSONL
                var sessDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".claude", "projects");
                if (Directory.Exists(sessDir))
                {
                    foreach (var jsonl in Directory.EnumerateFiles(sessDir, "*.jsonl", SearchOption.AllDirectories)
                        .OrderByDescending(f => new FileInfo(f).Length).Take(1))
                    {
                        var fi = new FileInfo(jsonl);
                        fileSize = fi.Length;
                        pct = (int)(fileSize / (40.0 * 1024 * 1024) * 100);
                    }
                }
            }
            if (fileSize > 0)
            {
                var sizeMB = fileSize / (1024.0 * 1024.0);
                jsonlInfo = sizeMB >= 1 ? $" | JSONL={sizeMB:F0}MB (ctx={pct}%)" : $" | ctx={pct}%";
            }
        }
        catch { }

        Console.WriteLine($"  Session: {report.SessionPercent}% used ({sessionRemain}% left) — {report.SessionReset ?? "?"}{jsonlInfo}");
        Console.WriteLine($"  Weekly:  {report.WeeklyAllPercent}% used ({weeklyRemain}% left) — {report.WeeklyAllReset ?? "?"}");
        Console.WriteLine($"  Sonnet:  {report.WeeklySonnetPercent}% used — {report.WeeklySonnetReset ?? "?"}");
        Console.WriteLine($"  URL:     {ClaudeUsageUrl}");

        // Slack alert at thresholds
        string? slackMsg = null;
        if (report.WeeklyAllPercent >= 95)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[USAGE] CRITICAL: Weekly {report.WeeklyAllPercent}% — {weeklyRemain}% left!");
            Console.ResetColor();
            slackMsg = $":rotating_light: *Claude 주간 사용량 {report.WeeklyAllPercent}%* ({weeklyRemain}% 남음)\n" +
                       $"해제: {report.WeeklyAllReset}\n" +
                       $"세션: {report.SessionPercent}% ({report.SessionReset})";
        }
        else if (report.WeeklyAllPercent >= 85)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[USAGE] WARNING: Weekly {report.WeeklyAllPercent}% — {weeklyRemain}% left");
            Console.ResetColor();
            slackMsg = $":warning: *Claude 주간 사용량 {report.WeeklyAllPercent}%* ({weeklyRemain}% 남음)\n" +
                       $"해제: {report.WeeklyAllReset}";
        }

        if (report.SessionPercent >= 90 && slackMsg == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[USAGE] Session at {report.SessionPercent}%!");
            Console.ResetColor();
            slackMsg = $":hourglass: *Claude 세션 사용량 {report.SessionPercent}%* ({sessionRemain}% 남음)\n" +
                       $"해제: {report.SessionReset}";
        }

        // Send Slack alert
        if (slackMsg != null)
        {
            try
            {
                var psi = new ProcessStartInfo("wkappbot", $"slack send \"{slackMsg}\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "CLAUDE-USAGE")?.WaitForExit(5000);
                Console.WriteLine("[USAGE] Slack alert sent");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[USAGE] Slack send failed: {ex.Message}");
            }
        }

        return 0;
    }
}

class ClaudeUsageReport
{
    public int SessionPercent { get; set; }
    public string? SessionReset { get; set; }
    public int WeeklyAllPercent { get; set; }
    public string? WeeklyAllReset { get; set; }
    public int WeeklySonnetPercent { get; set; }
    public string? WeeklySonnetReset { get; set; }
}
