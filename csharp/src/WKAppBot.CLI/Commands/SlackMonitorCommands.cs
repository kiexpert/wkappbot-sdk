using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: Slack WebBot monitor, Claude busy detection, prompt lost handler
internal partial class Program
{
    static bool IsRunningFromHostApp(string hostToken, string? forceEnvVar = null)
    {
        try
        {
            // Explicit override for edge cases.
            var forced = string.IsNullOrWhiteSpace(forceEnvVar) ? null : Environment.GetEnvironmentVariable(forceEnvVar);
            if (!string.IsNullOrWhiteSpace(forced))
            {
                var v = forced.Trim().ToLowerInvariant();
                if (v is "1" or "true" or "yes" or "on") return true;
                if (v is "0" or "false" or "no" or "off") return false;
            }

            // Strict condition: parent/ancestor process must be Codex app.
            // Do not treat CODEX_HOME alone as sufficient.
            using var cur = Process.GetCurrentProcess();
            int pid = cur.Id;
            for (int depth = 0; depth < 6; depth++)
            {
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                using var p = Process.GetProcessById(ppid);
                var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                if (name == hostToken || name.Contains(hostToken))
                    return true;
                pid = ppid;
            }

            // Heuristic fallback:
            // some launch paths (MCP/agent wrappers) hide original parent chain.
            // In that case, use host-specific runtime signals.
            if (hostToken == "codex")
            {
                // Codex app environment + alive process is a strong hint.
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CODEX_HOME")) &&
                    IsProcessAlive("codex"))
                    return true;

                // Hidden windows included: do not rely on foreground/focus.
                if (HasTopLevelWindowForProcess("codex", "Chrome_WidgetWin_1"))
                    return true;
            }
            else if (hostToken == "claude")
            {
                // Hidden windows included: do not rely on foreground/focus.
                if (HasTopLevelWindowForProcess("claude", "Chrome_WidgetWin_1"))
                    return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    static bool IsRunningFromCodexApp() =>
        IsRunningFromHostApp("codex", "WKAPPBOT_ASSUME_CODEX_APP");

    static bool IsRunningFromClaudeApp() =>
        IsRunningFromHostApp("claude", "WKAPPBOT_ASSUME_CLAUDE_APP");

    static string GetSlackPrefixForHostType(string? hostType) =>
        ClaudePromptHelper.IsCodexHostType(hostType) ? SlackCodexPrefix : SlackClaudePrefix;

    static List<ClaudePromptHelper.PromptInfo> GetKnownPromptInfos()
    {
        var merged = new Dictionary<IntPtr, ClaudePromptHelper.PromptInfo>();
        foreach (var p in FindAllPromptsViaMcp())
            merged[p.WindowHandle] = p;
        foreach (var p in ClaudePromptHelper.GetAllCachedPrompts())
            if (!merged.ContainsKey(p.WindowHandle))
                merged[p.WindowHandle] = p;
        if (merged.Count == 0)
        {
            using var helper = new ClaudePromptHelper();
            foreach (var p in helper.FindAllPrompts())
                merged[p.WindowHandle] = p;
        }
        return merged.Values.ToList();
    }

    static ClaudePromptHelper.PromptInfo? FindKnownPromptInfoByHwnd(IntPtr hwnd) =>
        GetKnownPromptInfos().FirstOrDefault(p => p.WindowHandle == hwnd);

    static string InferSlackPrefixForCaller(string? callerCwd = null, IntPtr? callerHwnd = null)
    {
        if (callerHwnd.HasValue && callerHwnd.Value != IntPtr.Zero)
        {
            var hwndPrompt = FindKnownPromptInfoByHwnd(callerHwnd.Value);
            if (hwndPrompt != null)
                return GetSlackPrefixForHostType(hwndPrompt.HostType);
        }

        if (!string.IsNullOrWhiteSpace(callerCwd) && !IsSystemOrInstallDirectory(callerCwd))
        {
            var cwdPrompt = GetKnownPromptInfos().FirstOrDefault(p =>
            {
                if (!ClaudePromptHelper.IsVsCodeHostType(p.HostType)) return false;
                var pCwd = ExtractCwdFromVsCodeTitle(p.WindowTitle);
                return !string.IsNullOrEmpty(pCwd) &&
                       string.Equals(pCwd.TrimEnd('\\', '/'), callerCwd.TrimEnd('\\', '/'),
                           StringComparison.OrdinalIgnoreCase);
            });
            if (cwdPrompt != null)
                return GetSlackPrefixForHostType(cwdPrompt.HostType);
        }

        if (IsRunningFromCodexAppCertain() || IsRunningFromCodexApp())
            return SlackCodexPrefix;
        if (IsRunningFromClaudeAppCertain() || IsRunningFromClaudeApp())
            return SlackClaudePrefix;
        return SlackClaudePrefix;
    }

    /// <summary>
    /// Build Slack bot username from the active prompt window cards (Eye-cached).
    /// Priority: callerHwnd exact match -> callerCwd match -> most-recent card -> null (no cards).
    /// ParentName mapping: "claude"/"code"/"Code" -> 클롣, "codex" -> 코뎃, else -> 앱봇.
    /// </summary>
    static string? GetBotUsernameFromCachedCards(string? callerCwd = null, IntPtr? callerHwnd = null)
    {
        var cards = _cachedCards;
        // If no cards yet (Eye just started, first tick not done), build inline from tick files + prompts
        if (cards == null || cards.Count == 0)
        {
            var fresh = ReadEyeCards(staleSeconds: 86400);   // Codex/Claude tick-based cards
            SupplementCardsFromPrompts(fresh);               // VS Code idle cards
            if (fresh.Count > 0) cards = fresh;
        }
        if (cards == null || cards.Count == 0) return null;

        EyeParentCard? best = null;

        // 0. Direct hwnd match -- only trusted when callerCwd is unknown/system,
        //    OR when the hwnd's CWD actually matches callerCwd.
        //    Reason: GetForegroundWindow() (from Launcher) may return a different VS Code instance
        //    than the one that actually ran the command -> wrong CWD if accepted blindly.
        if (callerHwnd.HasValue && callerHwnd.Value != IntPtr.Zero)
        {
            var hwndPrompt = FindKnownPromptInfoByHwnd(callerHwnd.Value);
            if (hwndPrompt != null)
            {
                var hwndCwd = ClaudePromptHelper.IsVsCodeHostType(hwndPrompt.HostType)
                    ? ExtractCwdFromVsCodeTitle(hwndPrompt.WindowTitle)
                    : callerCwd;
                // Accept hwnd only when callerCwd is absent/system, or hwnd CWD matches callerCwd
                bool cwdOk = string.IsNullOrWhiteSpace(callerCwd)
                          || IsSystemOrInstallDirectory(callerCwd)
                          || (!string.IsNullOrEmpty(hwndCwd) && string.Equals(
                                 hwndCwd.TrimEnd('\\', '/'), callerCwd.TrimEnd('\\', '/'),
                                 StringComparison.OrdinalIgnoreCase));
                if (cwdOk)
                {
                    var hwndPrefix = GetSlackPrefixForHostType(hwndPrompt.HostType);
                    var hwndTag = !string.IsNullOrEmpty(hwndCwd) ? AbbreviateCwd(hwndCwd) : null;
                    var r0 = !string.IsNullOrWhiteSpace(hwndTag) ? $"{hwndPrefix}[{hwndTag}]" : hwndPrefix;
                    Console.Error.WriteLine($"[NAME] step=0(hwnd) -> {r0} | cwd={callerCwd}");
                    return r0;
                }
                // callerCwd is a real project dir that doesn't match the fg hwnd -> ignore hwnd, fall through
                Console.Error.WriteLine($"[NAME] step=0 skip: hwnd-cwd={hwndCwd} ≠ caller-cwd={callerCwd}");
            }
        }

        // 1. Exact CWD match (caller CWD from pipe)
        if (!string.IsNullOrWhiteSpace(callerCwd))
        {
            best = cards.FirstOrDefault(c =>
                !string.IsNullOrEmpty(c.Cwd) &&
                string.Equals(c.Cwd.TrimEnd('\\', '/'), callerCwd.TrimEnd('\\', '/'),
                    StringComparison.OrdinalIgnoreCase));
        }

        // 1b. Prompt window match via callerCwd (title-based -- no card needed)
        // "부모창 우선 확인": if we know the callerCwd, check VS Code windows by title first.
        if (!string.IsNullOrWhiteSpace(callerCwd) && !IsSystemOrInstallDirectory(callerCwd))
        {
            var matchPrompt = GetKnownPromptInfos()
                .FirstOrDefault(p =>
                {
                    if (!ClaudePromptHelper.IsVsCodeHostType(p.HostType)) return false;
                    var pCwd = ExtractCwdFromVsCodeTitle(p.WindowTitle);
                    return !string.IsNullOrEmpty(pCwd) && string.Equals(
                        pCwd.TrimEnd('\\', '/'), callerCwd.TrimEnd('\\', '/'),
                        StringComparison.OrdinalIgnoreCase);
                });
            if (matchPrompt != null)
            {
                var matchPrefix = GetSlackPrefixForHostType(matchPrompt.HostType);
                var matchTag = AbbreviateCwd(callerCwd);
                var r1b = !string.IsNullOrWhiteSpace(matchTag) ? $"{matchPrefix}[{matchTag}]" : matchPrefix;
                Console.Error.WriteLine($"[NAME] step=1b(prompt-title) -> {r1b} | cwd={callerCwd}");
                return r1b;
            }
            // callerCwd is known but no card/window matched -- build directly from callerCwd
            var directTag = AbbreviateCwd(callerCwd);
            if (!string.IsNullOrWhiteSpace(directTag))
            {
                var directPrefix = InferSlackPrefixForCaller(callerCwd, callerHwnd);
                var rDirect = $"{directPrefix}[{directTag}]";
                Console.Error.WriteLine($"[NAME] step=1b(direct-cwd) -> {rDirect} | cwd={callerCwd}");
                return rDirect;
            }
        }

        // 2. Most-recently-updated card (only when callerCwd is unknown)
        if (best == null && string.IsNullOrWhiteSpace(callerCwd))
            best = cards.OrderByDescending(c => c.LastTsUtc).FirstOrDefault();

        if (best == null)
        {
            Console.Error.WriteLine($"[NAME] step=? -> null (no card match) | cwd={callerCwd}");
            return null;
        }

        var pname = (best.ParentName ?? "").ToLowerInvariant();
        // Skip cards from wkappbot itself (shouldn't drive username detection)
        if (pname == "wkappbot" || pname.Contains("wkappbot") || pname == "a11y" || pname.Contains("a11y"))
        {
            Console.Error.WriteLine($"[NAME] step=? -> null (wkappbot card skipped) | cwd={callerCwd}");
            return null;
        }
        string prefix;
        if (pname == "claude" || pname.StartsWith("claude") || pname == "code" || pname.StartsWith("code"))
            prefix = SlackClaudePrefix;
        else if (pname == "codex" || pname.Contains("codex"))
            prefix = SlackCodexPrefix;
        else
        {
            Console.Error.WriteLine($"[NAME] step=? -> null (unknown parent={pname}) | cwd={callerCwd}");
            return null; // unknown ParentName -- let caller fall through to other detection
        }

        // claude-desktop: global instance, no CWD tag (install dir would abbreviate to "Claude")
        // VS Code (Code.exe): CWD tag meaningful, but skip system/install dirs
        if (pname == "claude" || pname == "claude.exe")
        {
            Console.Error.WriteLine($"[NAME] step=1/2(card) -> {prefix} | cwd={callerCwd}");
            return prefix;
        }

        string? cwdTag = null;
        if (!string.IsNullOrWhiteSpace(best.Cwd) && !IsSystemOrInstallDirectory(best.Cwd))
            cwdTag = AbbreviateCwd(best.Cwd);
        if (cwdTag == null)
            cwdTag = GetSlackFolderTag();
        var rCard = $"{prefix}[{cwdTag}]";
        Console.Error.WriteLine($"[NAME] step=1/2(card) -> {rCard} | cwd={callerCwd}");
        return rCard;
    }

    /// <summary>
    /// Walk the caller's process ancestry and return the first prompt window hwnd
    /// that belongs to one of our known Claude/VS Code/Codex instances.
    /// Returns IntPtr.Zero if not found.
    /// </summary>
    static IntPtr FindCallerParentPromptHwnd()
    {
        try
        {
            var known = ClaudePromptHelper.GetAllCachedPrompts()
                .Where(p => ClaudePromptHelper.IsVsCodeHostType(p.HostType) || p.HostType is "claude-desktop" or "codex-desktop")
                .ToList();
            if (known.Count == 0) return IntPtr.Zero;

            int pid = Process.GetCurrentProcess().Id;
            for (int depth = 0; depth < 8; depth++)
            {
                foreach (var pi in known)
                {
                    NativeMethods.GetWindowThreadProcessId(pi.WindowHandle, out uint winPid);
                    if ((int)winPid == pid) return pi.WindowHandle;
                }
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                pid = ppid;
            }
        }
        catch { }
        return IntPtr.Zero;
    }

    internal static string? GetSendReplyUsername(bool printDecision = false)
    {
        string username;

        var suggestBypass = Environment.GetEnvironmentVariable("WKAPPBOT_SUGGEST_BYPASS");
        if (!string.IsNullOrWhiteSpace(suggestBypass))
        {
            var bypass = GetSuggestBypassUsername(printDecision);
            if (printDecision) Console.Error.WriteLine($"[NAME] step=suggest-bypass -> {bypass}");
            return bypass;
        }

        // Priority 0: WKAPPBOT_CALLER_NAME env var (set by EyeMcpClient for MCP workers)
        // Per-call CallerCwd overrides the process-level env var for CWD-specific naming
        var envName = Environment.GetEnvironmentVariable("WKAPPBOT_CALLER_NAME");
        if (!string.IsNullOrEmpty(envName) && EyeCmdPipeServer.CallerCwd.Value == null)
        {
            if (printDecision) Console.Error.WriteLine($"[NAME] step=env -> {envName}");
            return envName;
        }

        // Priority 1: prompt window cards (app type + CWD from actual Claude/Codex instances)
        var callerCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        // Prefer pipe-delivered hwnd (Launcher sends GetForegroundWindow) -- faster than process-tree walk.
        // Fall back to process tree walk when running Core directly (no Eye pipe, no CallerHwnd).
        var callerHwnd = EyeCmdPipeServer.CallerHwnd.Value ?? FindCallerParentPromptHwnd();
        var cardUsername = GetBotUsernameFromCachedCards(callerCwd, callerHwnd);
        if (!string.IsNullOrEmpty(cardUsername))
            username = cardUsername;
        // Priority 2: certain process-ancestry detection
        else if (IsRunningFromCodexAppCertain())
            username = BuildSlackBotUsername(SlackCodexPrefix, null, spaceBeforeBracket: false);
        else if (IsRunningFromClaudeAppCertain())
            username = BuildSlackBotUsername(SlackClaudePrefix, null, spaceBeforeBracket: false);
        else
        {
            username = GetGeneralBotUsername();
        }

        if (printDecision)
            Console.Error.WriteLine($"[SLACK] bot-name={username} cwd={callerCwd}");

        return username;
    }

    internal static string GetSuggestBypassUsername(bool printDecision = false, string? cwd = null)
    {
        var callerCwd = cwd ?? EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        string? cwdTag = null;
        if (!string.IsNullOrWhiteSpace(callerCwd) && !IsSystemOrInstallDirectory(callerCwd))
            cwdTag = AbbreviateCwd(callerCwd);

        string username;
        if (IsRunningFromCodexAppCertain() || IsRunningFromCodexApp())
            username = !string.IsNullOrWhiteSpace(cwdTag) ? $"{SlackCodexPrefix}[{cwdTag}]" : SlackCodexPrefix;
        else if (IsRunningFromClaudeAppCertain() || IsRunningFromClaudeApp())
            username = !string.IsNullOrWhiteSpace(cwdTag) ? $"{SlackClaudePrefix}[{cwdTag}]" : SlackClaudePrefix;
        else
            username = !string.IsNullOrWhiteSpace(cwdTag) ? $"{SlackClaudePrefix}[{cwdTag}]" : SlackClaudePrefix;

        if (printDecision)
            Console.Error.WriteLine($"[SUGGEST-NAME] bypass={username} cwd={callerCwd}");

        return username;
    }

    /// <summary>
    /// True if running from Claude Code (VS Code extension) or Claude Desktop.
    /// Use IsRunningFromClaudeDesktop() to distinguish desktop vs extension.
    /// </summary>
    static bool IsRunningFromClaudeAppCertain()
    {
        try
        {
            var forced = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CLAUDE_APP")?.Trim().ToLowerInvariant();
            if (forced is "1" or "true" or "yes" or "on") return true;
            if (forced is "0" or "false" or "no" or "off") return false;

            // CLAUDE_CODE_ENTRYPOINT=claude-vscode: set by Claude Code VS Code extension.
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CLAUDE_CODE_ENTRYPOINT")))
                return true;

            // Parent/ancestor chain contains "claude" process = Claude Desktop.
            return IsRunningFromClaudeDesktop();
        }
        catch { return false; }
    }

    /// <summary>True only if Claude Desktop (standalone) is in the parent process chain.</summary>
    static bool IsRunningFromClaudeDesktop()
    {
        try
        {
            using var cur = Process.GetCurrentProcess();
            int pid = cur.Id;
            for (int depth = 0; depth < 6; depth++)
            {
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                using var p = Process.GetProcessById(ppid);
                var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                if (name == "claude" || name.StartsWith("claude"))
                    return true;
                pid = ppid;
            }
            return false;
        }
        catch { return false; }
    }

    /// <summary>
    /// True if running from Codex (VS Code extension) or Codex Desktop.
    /// Use IsRunningFromCodexDesktop() to distinguish desktop vs extension.
    /// </summary>
    static bool IsRunningFromCodexAppCertain()
    {
        try
        {
            var forced = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CODEX_APP")?.Trim().ToLowerInvariant();
            if (forced is "1" or "true" or "yes" or "on") return true;
            if (forced is "0" or "false" or "no" or "off") return false;

            // CODEX_HOME: set by Codex VS Code extension.
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CODEX_HOME")))
                return true;

            // Parent/ancestor chain contains "codex" process = Codex Desktop.
            return IsRunningFromCodexDesktop();
        }
        catch { return false; }
    }

    /// <summary>True only if Codex Desktop (standalone) is in the parent process chain.</summary>
    static bool IsRunningFromCodexDesktop()
    {
        try
        {
            using var cur = Process.GetCurrentProcess();
            int pid = cur.Id;
            for (int depth = 0; depth < 6; depth++)
            {
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                using var p = Process.GetProcessById(ppid);
                var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                if (name == "codex" || name.Contains("codex"))
                    return true;
                pid = ppid;
            }
            return false;
        }
        catch { return false; }
    }

    static int GetParentPidForCodexCheck(int pid)
    {
        try
        {
            using var searcher = new System.Management.ManagementObjectSearcher(
                $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {pid}");
            foreach (var o in searcher.Get())
            {
                var mo = (System.Management.ManagementObject)o;
                return Convert.ToInt32(mo["ParentProcessId"]);
            }
        }
        catch { }
        return -1;
    }

    static bool IsProcessAlive(string processToken)
    {
        try
        {
            var token = processToken.ToLowerInvariant();
            return Process.GetProcesses()
                .Any(p =>
                {
                    try
                    {
                        var n = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                        return n == token || n.Contains(token);
                    }
                    catch { return false; }
                });
        }
        catch
        {
            return false;
        }
    }

    static bool HasTopLevelWindowForProcess(string processToken, string windowClass)
    {
        try
        {
            var token = processToken.ToLowerInvariant();
            var found = false;
            var sb = new System.Text.StringBuilder(256);

            NativeMethods.EnumWindows((hwnd, _) =>
            {
                try
                {
                    NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                    if (pid == 0) return true;
                    using var p = Process.GetProcessById((int)pid);
                    var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                    if (!(name == token || name.Contains(token))) return true;

                    sb.Clear();
                    NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
                    var cls = sb.ToString();
                    if (string.Equals(cls, windowClass, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        return false; // stop enum
                    }
                }
                catch { }
                return true;
            }, IntPtr.Zero);

            return found;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// When Claude prompt is lost (FindPrompt returns null), capture foreground window screenshot
    /// and send it to Slack so the user can see what's blocking the prompt (e.g. permission dialog).
    /// Also writes the original message to inbox as fallback.
    /// </summary>
    static void HandlePromptLost(string botToken, string channel, string threadTs,
        string user, string cleanText, string ts, string? username = null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[SLACK] >> Prompt lost! Claude 프롬프트를 찾을 수 없습니다.");
        Console.WriteLine("[SLACK]    가능한 원인: 계획승인 중 / 권한 묻는 창 / Claude 비활성");
        Console.WriteLine("[SLACK]    대응: 전경 윈도우 스냅샷 캡처 후 Slack에 전송합니다.");
        Console.ResetColor();

        // Take UIA snapshot for diagnosis
        try
        {
            Console.WriteLine("[SLACK]    Diagnosing foreground window...");
        }
        catch { }

        try
        {
            var fgWindow = WKAppBot.Win32.Native.NativeMethods.GetForegroundWindow();
            if (fgWindow != IntPtr.Zero)
            {
                // Get foreground window info
                var fgTitle = WKAppBot.Win32.Window.WindowFinder.GetWindowText(fgWindow);
                var fgClass = WKAppBot.Win32.Window.WindowFinder.GetClassName(fgWindow);
                Console.Error.WriteLine($"[SLACK]    Foreground: \"{fgTitle}\" (class={fgClass})");

                // Capture foreground window screenshot
                var outputDir = Path.Combine(DataDir, "output", "screenshots");
                Directory.CreateDirectory(outputDir);
                var screenshotPath = Path.Combine(outputDir,
                    $"prompt_blocked_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(fgWindow);
                if (bmp != null && !WKAppBot.Win32.Input.ScreenCapture.IsBlankBitmap(bmp))
                {
                    bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
                    bmp.Dispose();
                    Console.Error.WriteLine($"[SLACK]    Screenshot: {screenshotPath}");

                    // Upload to Slack thread with diagnostic info
                    var comment = $"Claude 프롬프트를 찾을 수 없어요!\n" +
                        $"전경 윈도우: \"{fgTitle}\" (class={fgClass})\n" +
                        $"원래 메시지: {cleanText}\n\n" +
                        $"승인 다이얼로그가 떠있으면 수락해주세요!";

                    SlackUploadFileAsync(botToken, channel, screenshotPath,
                        $"Blocked: {fgTitle}", threadTs, comment).GetAwaiter().GetResult();
                }
                else
                {
                    bmp?.Dispose();
                    // Screenshot failed, send text message instead
                    var msg = $"Claude 프롬프트를 찾을 수 없어요!\n" +
                        $"전경 윈도우: \"{fgTitle}\" (class={fgClass})\n" +
                        $"원래 메시지: {cleanText}\n\n" +
                        $"승인 다이얼로그가 떠있으면 수락해주세요!";
                    SlackSendViaApi(botToken, channel, msg, threadTs, username: username).GetAwaiter().GetResult();
                }
            }
            else
            {
                // No foreground window -- just notify
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! 원래 메시지: {cleanText}",
                    threadTs, username: username).GetAwaiter().GetResult();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SLACK]    Diagnosis failed: {ex.Message}");
            // Fallback: text notification
            try
            {
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! (진단 실패: {ex.Message})\n원래 메시지: {cleanText}",
                    threadTs, username: username).GetAwaiter().GetResult();
            }
            catch { }
        }

        // Always write to inbox as fallback
        WriteInbox(channel, user, cleanText, ts);
    }

    /// <summary>
    /// Claude Desktop monitor tick -- called every ~100ms from the listen loop.
    /// Detects 3 Claude states via UIA: executing, plan_approval_pending, prompt_ready.
    /// Streams status to Slack via chat.postMessage (create) + chat.update (update).
    /// Handles thread reply relocation: when someone replies to the streaming message,
    /// creates a new message for continued streaming and acknowledges in the old thread.
    /// </summary>
    static void WebBotMonitorTick(
        string botToken, string channel,
        ref CdpClient? cdp, ref string? statusTs,
        ref string? lastUrl, ref string? lastContent,
        ref bool claudeBusy, ref int reconnectBackoff,
        ref IntPtr chromeHwnd,
        ClaudePromptHelper? promptHelper,
        ConcurrentQueue<(string user, string text, string threadTs)>? threadReplies = null,
        string? instanceName = null)
    {
        // 0. Handle thread replies to streaming message -> relocate to new message
        if (threadReplies != null && statusTs != null)
        {
            bool relocated = false;
            while (threadReplies.TryDequeue(out var reply))
            {
                if (relocated) continue; // drain queue, only relocate once per tick

                try
                {
                    bool isChannelMsg = reply.text == "__channel_msg__";

                    if (!isChannelMsg)
                    {
                        // 1. Reply in old thread (acknowledge) -- only for thread replies
                        var ackMsg = "(이 쓰레드의 스트리밍은 새 메시지로 이동했어요!)";
                        Task.Run(async () => await SlackSendViaApi(botToken, channel, ackMsg, reply.threadTs, username: GetBotUsername(instanceName))).Wait(3000);
                    }

                    // 2. Delete old status message (clean channel) or update if thread reply
                    var localOldTs = statusTs;
                    if (isChannelMsg)
                    {
                        // Channel msg relocation: just delete the old status message for clean look
                        Task.Run(async () => await SlackDeleteMessageAsync(botToken, channel, localOldTs!)).Wait(3000);
                    }
                    else
                    {
                        // Thread reply: update old message with "moved" notice
                        var oldContent = lastContent ?? "Claude 상태";
                        var oldMsg = oldContent + $"\n_:speech_balloon: 댓글 달림 -- 새 메시지로 이동_";
                        Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localOldTs!, oldMsg)).Wait(3000);
                    }

                    // 3. Create new message for continued streaming
                    var curStatus = DetectClaudeDesktopStatus(FindClaudeDesktopWindow());
                    var newEmoji = GetStatusEmoji(curStatus?.Item1);
                    var newText = curStatus?.Item2 ?? "대기 중";
                    var newContent = curStatus != null
                        ? $"{newEmoji} *{GetStatusLabel(curStatus.Item1)}* -- {newText} ({DateTime.Now:HH:mm})"
                        : $":white_check_mark: *Claude 대기 중* ({DateTime.Now:HH:mm})";

                    var botUser = GetBotUsername(instanceName);
                    var sendTask = Task.Run(async () => await SlackSendViaApi(botToken, channel, newContent, username: botUser));
                    if (sendTask.Wait(5000))
                    {
                        var (ok, newTs) = sendTask.Result;
                        if (ok && newTs != null)
                        {
                            statusTs = newTs;
                            lastContent = newContent;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Error.WriteLine($"[SLACK] Streaming relocated -> new ts={newTs}");
                            Console.ResetColor();
                        }
                    }
                    relocated = true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[SLACK] Relocation error: {ex.Message}");
                }
            }
        }

        // 1. Detect Claude Desktop status via UIA (3 states)
        var claudeHwnd = FindClaudeDesktopWindow();
        var claudeStatus = DetectClaudeDesktopStatus(claudeHwnd);
        bool nowActive = claudeStatus != null;
        string? statusKey = claudeStatus?.Item1;
        string? statusText = claudeStatus?.Item2;
        string emoji = GetStatusEmoji(statusKey);
        string label = GetStatusLabel(statusKey);

        if (nowActive && !claudeBusy)
        {
            // Transition: idle -> active
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[SLACK] Claude: {label} -- {statusText}");
            Console.ResetColor();

            var displayText = statusText ?? "작업 중...";
            lastContent = $"{emoji} *{label}* -- {displayText} ({DateTime.Now:HH:mm})";

            // Create or update status message
            if (statusTs == null)
            {
                var localContent = lastContent; // ref param can't be captured in lambda
                var botUser = GetBotUsername(instanceName);
                var sendTask = Task.Run(async () => await SlackSendViaApi(botToken, channel, localContent, username: botUser));
                if (sendTask.Wait(5000))
                {
                    (bool ok, string? ts) = sendTask.Result;
                    if (ok && ts != null)
                    {
                        statusTs = ts;
                        Console.Error.WriteLine($"[SLACK] Status: {displayText} (ts={ts})");
                    }
                }
            }
            else
            {
                var localTs = statusTs;
                var localContent = lastContent; // ref param can't be captured in lambda
                Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localTs, localContent)).Wait(3000);
                Console.Error.WriteLine($"[SLACK] Status: {displayText}");
            }
        }
        else if (!nowActive && claudeBusy)
        {
            // Transition: active -> idle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SLACK] Claude: done");
            Console.ResetColor();

            if (statusTs != null)
            {
                var doneMsg = $":white_check_mark: *Claude 응답 완료* ({DateTime.Now:HH:mm})";
                lastContent = doneMsg;
                var localTs = statusTs;
                Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localTs, doneMsg)).Wait(5000);
            }
        }
        else if (nowActive && claudeBusy)
        {
            // Still active -- update only when status text changes
            var newContent = $"{emoji} *{label}* -- {statusText ?? "..."} ({DateTime.Now:HH:mm})";
            if (newContent != lastContent)
            {
                lastContent = newContent;
                if (statusTs != null)
                {
                    var localTs = statusTs;
                    Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localTs, newContent)).Wait(3000);
                }
                Console.Error.WriteLine($"[SLACK] Status: {statusText}");
            }
        }

        claudeBusy = nowActive;
    }

    /// <summary>Get Slack emoji for Claude status key.</summary>
    static string GetStatusEmoji(string? statusKey) => statusKey switch
    {
        "executing" => ":gear:",
        "plan_approval_pending" => ":clipboard:",
        "prompt_ready" => ":speech_balloon:",
        "rate_limit" => ":warning:",
        _ => ":robot_face:"
    };

    /// <summary>Get human-readable label for Claude status key.</summary>
    static string GetStatusLabel(string? statusKey) => statusKey switch
    {
        "executing" => "Claude 작업 중",
        "plan_approval_pending" => "Claude 계획승인 대기",
        "prompt_ready" => "Claude 프롬프트 대기",
        "rate_limit" => "Claude 한도 초과",
        _ => "Claude"
    };

    const string SlackCodexPrefix = "코덳";
    const string SlackClaudePrefix = "클롣";
    const string SlackGenericPrefix = "앱봇";
    const string SlackBroadcastUsername = "앱봇아이";

    const string SlackCodexAppPrefix = "코덳앱";
    const string SlackClaudeAppPrefix = "클롣앱";

    static string GetSlackDisplayPrefixForHostType(string? hostType)
    {
        var host = (hostType ?? string.Empty).ToLowerInvariant();
        return host switch
        {
            "claude-desktop" => SlackClaudeAppPrefix,
            "codex-desktop" => SlackCodexAppPrefix,
            _ when ClaudePromptHelper.IsCodexHostType(hostType) => SlackCodexPrefix,
            _ => SlackClaudePrefix,
        };
    }

    static string FormatSlackDisplayName(string? hostType, string? cwdTag)
    {
        var prefix = GetSlackDisplayPrefixForHostType(hostType);
        return string.IsNullOrWhiteSpace(cwdTag) ? prefix : $"{prefix}[{cwdTag}]";
    }

    static string GetSlackFolderTag()
    {
        var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        if (string.IsNullOrWhiteSpace(cwd))
            return Environment.MachineName;

        var abbreviated = AbbreviateCwd(cwd);
        return string.IsNullOrWhiteSpace(abbreviated) ? Environment.MachineName : abbreviated;
    }

    static string BuildSlackBotUsername(string prefix, string? instanceName = null, bool spaceBeforeBracket = false)
    {
        var tag = string.IsNullOrWhiteSpace(instanceName) ? GetSlackFolderTag() : instanceName!.Trim();
        var sep = spaceBeforeBracket ? " " : string.Empty;
        return $"{prefix}{sep}[{tag}]";
    }

    static string GetGeneralBotUsername(string? instanceName = null)
    {
        // Explicit override preckiexpe.
        var forceCodex = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CODEX_APP")?.Trim().ToLowerInvariant();
        var forceClaude = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CLAUDE_APP")?.Trim().ToLowerInvariant();
        if (forceCodex is "1" or "true" or "yes" or "on")
            return BuildSlackBotUsername(SlackCodexPrefix, instanceName, spaceBeforeBracket: false);
        if (forceClaude is "1" or "true" or "yes" or "on")
            return BuildSlackBotUsername(SlackClaudePrefix, instanceName, spaceBeforeBracket: false);

        // Parent-chain-only detection: no AI in parent -> treat as human user.
        // Prefix is determined by host type: vscode-extension -> short name, desktop app -> "앱" suffix.
        // Codex: CODEX_HOME (VS Code ext) -> 코덳, parent "codex" process (desktop) -> 코덳앱.
        // Claude: CLAUDE_CODE_ENTRYPOINT (VS Code ext) -> 클롣, parent "claude" process (desktop) -> 클롣앱.
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CODEX_HOME")))
            return BuildSlackBotUsername(SlackCodexPrefix, instanceName, spaceBeforeBracket: false);
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CLAUDE_CODE_ENTRYPOINT")))
            return BuildSlackBotUsername(SlackClaudePrefix, instanceName, spaceBeforeBracket: false);
        if (IsRunningFromCodexDesktop())   // parent chain "codex" = Codex Desktop -> 코덳앱
            return BuildSlackBotUsername(SlackCodexAppPrefix, instanceName, spaceBeforeBracket: false);
        if (IsRunningFromClaudeDesktop())  // parent chain "claude" = Claude Desktop -> 클롣앱
            return BuildSlackBotUsername(SlackClaudeAppPrefix, instanceName, spaceBeforeBracket: false);

        // No AI in parent chain -> human user
        var loginUser = Environment.UserName;
        if (!string.IsNullOrWhiteSpace(loginUser)) return loginUser;
        return BuildSlackBotUsername(SlackGenericPrefix, instanceName, spaceBeforeBracket: false);
    }

    /// <summary>Default Slack bot username from unified policy.</summary>
    static readonly string BotUsername = GetGeneralBotUsername();

    /// <summary>Build Slack username override from instance name.</summary>
    static string? GetBotUsername(string? instanceName) =>
        GetGeneralBotUsername(instanceName);

    /// <summary>
    /// Given any PID, instantly return the Slack display name.
    /// Process name -> host type -> SlackPrefix + [cwdTag].
    /// Single source of truth for all ack/status/probe display names.
    /// </summary>
    internal static string ResolveDisplayNameByPid(int pid)
    {
        var prompt = GetKnownPromptInfos()
            .Where(p =>
            {
                NativeMethods.GetWindowThreadProcessId(p.WindowHandle, out uint winPid);
                return (int)winPid == pid;
            })
            .OrderByDescending(p => NativeMethods.IsWindowVisible(p.WindowHandle))
            .FirstOrDefault();
        if (prompt != null)
            return GetPromptDisplayInfo(prompt.WindowHandle).displayName;

        string procName = "";
        try { procName = System.Diagnostics.Process.GetProcessById(pid).ProcessName; } catch { }

        var cwd = WKAppBot.Win32.Native.NativeMethods.GetProcessCurrentDirectory(pid);
        if (!string.IsNullOrEmpty(cwd) && IsSystemOrInstallDirectory(cwd))
            cwd = null;  // reject system/install dirs -- no CWD tag
        var cwdTag = string.IsNullOrEmpty(cwd) ? null : AbbreviateCwd(cwd);

        if (procName.Equals("claude", StringComparison.OrdinalIgnoreCase))
            return FormatSlackDisplayName("claude-desktop", cwdTag);

        if (procName.Equals("codex", StringComparison.OrdinalIgnoreCase))
            return FormatSlackDisplayName("codex-desktop", cwdTag);

        if (procName.Equals("Code", StringComparison.OrdinalIgnoreCase))
            return FormatSlackDisplayName("vscode-claudecode", cwdTag);

        // Fallback: treat as Claude Code with cwd if available
        return FormatSlackDisplayName(null, cwdTag);
    }

    /// <summary>Given any HWND, instantly return the Slack display name.</summary>
    internal static string ResolveDisplayNameByHwnd(IntPtr hwnd)
    {
        var prompt = FindKnownPromptInfoByHwnd(hwnd);
        if (prompt != null)
            return GetPromptDisplayInfo(prompt.WindowHandle).displayName;

        WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
        return pid > 0 ? ResolveDisplayNameByPid((int)pid) : SlackClaudePrefix;
    }

    /// <summary>Find Chrome main window handle by PID.</summary>
    static IntPtr FindChromeHwndByPid(int pid)
    {
        if (pid <= 0) return IntPtr.Zero;

        IntPtr found = IntPtr.Zero;
        var sb = new System.Text.StringBuilder(256);
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_WidgetWin_1") return true;
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;

            NativeMethods.GetWindowThreadProcessId(hwnd, out uint wpid);
            if ((int)wpid != pid) return true;

            // Check for WS_CAPTION (main window, not popup)
            var style = NativeMethods.GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            found = hwnd;
            return false;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>
    /// Check if a thread's parent message was sent by our bot.
    /// Uses conversations.replies API to get the parent message and check for bot_id.
    /// Results are cached in ownMessageTimestamps to avoid repeated API calls.
    /// </summary>
    /// <summary>Get the text of the thread starter message (for Eye-status thread detection).</summary>
    static string? GetThreadStarterText(string botToken, string channel, string threadTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=1&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true) return null;
            var messages = json["messages"]?.AsArray();
            return messages?.Count > 0 ? messages[0]?["text"]?.GetValue<string>() : null;
        }
        catch { return null; }
    }

    static bool IsOwnThread(string botToken, string channel, string threadTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // Get only the parent message (limit=1, inclusive=true)
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=1&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true)
            {
                Console.Error.WriteLine($"[SLACK] IsOwnThread API failed: {json?["error"]}");
                return false;
            }

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return false;

            var parent = messages[0];
            // Bot messages have "bot_id" field
            var botId = parent?["bot_id"]?.GetValue<string>();
            var result = !string.IsNullOrEmpty(botId);
            if (result)
                Console.Error.WriteLine($"[SLACK] IsOwnThread: YES (bot_id={botId}, ts={threadTs})");
            return result;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SLACK] IsOwnThread exception: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Get the bot's parent message text from a thread (for context when replying).
    /// Returns the first message in the thread (the bot's original message).
    /// </summary>
    /// <summary>
    /// Build thread context for Claude prompt: bot's original message + previous user message.
    /// Returns formatted context string, or null if unavailable.
    /// Layout: [클봇 원본]\n{parent}\n\n[이전 메시지]\n{prev}
    /// </summary>
    static string? GetThreadContext(string botToken, string channel, string threadTs, string currentMsgTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // Fetch entire thread (up to 50 messages -- enough for context)
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=50&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true) return null;

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return null;

            // 1. Bot's original message (thread parent = first message)
            var parentText = messages[0]?["text"]?.GetValue<string>();

            // 2. Find previous message (right before current -- could be user OR bot)
            // In a thread, conversation flows: bot->user->bot->user, so prev msg gives context
            // Skip ack messages ("전달했습니다") -- they are transient and not meaningful context
            string? prevText = null;
            bool prevIsBot = false;
            for (int i = messages.Count - 1; i >= 1; i--)
            {
                var msgTs = messages[i]?["ts"]?.GetValue<string>();
                if (msgTs == currentMsgTs) continue; // skip current message

                var candidateText = messages[i]?["text"]?.GetValue<string>();
                // Skip ack messages and bot reminders (not useful context for Claude)
                if (candidateText != null && candidateText.StartsWith("Claude에 전달했습니다!")) continue;
                if (candidateText != null && candidateText.Contains("[AI에게]")) continue;

                prevText = candidateText;
                prevIsBot = messages[i]?["bot_id"] != null;
                break;
            }

            // Build context
            var sb = new System.Text.StringBuilder();

            if (!string.IsNullOrEmpty(parentText) && !parentText.StartsWith("Claude에 전달했습니다!"))
            {
                if (parentText.Length > 300) parentText = parentText[..297] + "...";
                sb.AppendLine("[쓰레드 시작]");
                sb.AppendLine(parentText);
            }

            if (!string.IsNullOrEmpty(prevText))
            {
                if (prevText.Length > 200) prevText = prevText[..197] + "...";
                var label = prevIsBot ? "[직전 클롣 응답]" : "[직전 메시지]";
                sb.AppendLine();
                sb.AppendLine(label);
                sb.AppendLine(prevText);
            }

            var result = sb.ToString().Trim();
            return string.IsNullOrEmpty(result) ? null : result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Update an existing Slack message via chat.update API.
    /// Returns (ok, ts) -- ts is the message timestamp.
    /// </summary>
    static async Task<(bool ok, string? ts, string? error)> SlackUpdateMessageAsync(
        string botToken, string channel, string ts, string text)
    {
        // Hard cap: Slack chat.update rejects > 40,000 chars; stay well under to avoid msg_too_long
        const int MaxSlackText = 39000;
        if (text.Length > MaxSlackText)
            text = text[..MaxSlackText] + "\n...(truncated)";

        using var http = new HttpClient();
        var payload = JsonSerializer.Serialize(new { channel, ts, text });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.update");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;
        var error = ok ? null : (json?["error"]?.GetValue<string>() ?? "unknown");

        if (!ok)
            Console.Error.WriteLine($"[SLACK] chat.update error: {error}");

        return (ok, json?["ts"]?.GetValue<string>(), error);
    }

    /// <summary>
    /// Delete a Slack message via chat.delete API.
    /// guardThreadStarter=true (default): refuses to delete if the message has replies (is a thread starter).
    /// Pass false for ack/reply messages that are themselves inside threads (not thread starters).
    /// </summary>
    static async Task<bool> SlackDeleteMessageAsync(string botToken, string channel, string ts,
        bool guardThreadStarter = true, bool skipLastMsgProtection = false)
    {
        // -- Orphan detection: reply whose thread starter was deleted -> always delete --
        // Fetch message info to check if it's an orphaned reply
        try
        {
            var replies = await SlackGetThreadRepliesAsync(botToken, channel, ts);
            if (replies != null && replies.Count >= 1)
            {
                var starter = replies[0];
                var starterTs = starter?["ts"]?.GetValue<string>();
                var starterSubtype = starter?["subtype"]?.GetValue<string>();
                // If starter ts != our ts, we're a reply -- check if starter is tombstone
                if (starterTs != ts && (starterSubtype == "tombstone" || starter?["text"]?.GetValue<string>() == "This message was deleted."))
                {
                    Console.Error.WriteLine($"[SLACK] Orphan reply ts={ts} -- thread starter deleted -> force delete");
                    await SlackDeleteRawAsync(botToken, channel, ts);
                    return true;
                }
            }
        }
        catch { /* best-effort orphan check */ }

        if (guardThreadStarter)
        {
            // Loop: delete bot replies, re-check, repeat until clean or user reply found
            for (int pass = 0; pass < 10; pass++) // max 10 passes (safety)
            {
                var threadReplies = await SlackGetThreadRepliesAsync(botToken, channel, ts);
                if (threadReplies == null || threadReplies.Count <= 1) break; // no replies -> proceed to delete starter

                // Check for human replies
                bool hasUserReply = false;
                var botReplies = new List<string>();
                for (int ri = 1; ri < threadReplies.Count; ri++)
                {
                    var r = threadReplies[ri];
                    var subtype = r?["subtype"]?.GetValue<string>();
                    if (subtype != "bot_message" && r?["bot_id"] == null)
                    { hasUserReply = true; break; }
                    var rTs = r?["ts"]?.GetValue<string>();
                    if (rTs != null) botReplies.Add(rTs);
                }

                if (hasUserReply)
                {
                    Console.Error.WriteLine($"[SLACK] SKIP delete ts={ts} -- thread has user replies");
                    return false;
                }

                if (botReplies.Count == 0) break; // all clean

                // Delete first bot reply (repeat loop will pick up the rest)
                Console.Error.WriteLine($"[SLACK] Clearing bot reply {botReplies[0]} (pass {pass + 1}, {botReplies.Count} remaining)");
                await SlackDeleteRawAsync(botToken, channel, botReplies[0]);
                await Task.Delay(300); // rate limit
            }
        }

        // -- Guard: protect last 2 messages per author+profile (message_limit fallback target) --
        // skipLastMsgProtection=true bypasses this for explicit gc/cleanup callers
        if (!skipLastMsgProtection && await IsProtectedLastMessageAsync(botToken, channel, ts))
        {
            Console.Error.WriteLine($"[SLACK] SKIP delete ts={ts} -- protected (last 2 per author)");
            return false;
        }

        // -- Pre-delete: dump full message data for audit trail --
        SlackDumpMessage(botToken, channel, ts, "[SLACK:DELETE]");

        using var http = new HttpClient();
        var payload = JsonSerializer.Serialize(new { channel, ts });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.delete");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.Error.WriteLine($"[SLACK] chat.delete error: {json?["error"]}");

        return ok;
    }

    /// <summary>
    /// wkappbot slack read <ts> -- dump full message data (text, user, emoji, files, reactions, etc.)
    /// </summary>
    static int SlackReadCommand(string[] args)
    {
        if (args.Length < 2 || args[1] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot slack read <ts>");
            Console.WriteLine("  Dump full Slack message data (text, user, emoji, files, reactions).");
            return 1;
        }
        var ts = args[1];
        var config = LoadSlackConfig();
        var botToken = config?["bot_token"]?.GetValue<string>();
        var channel  = config?["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
            return Error("Slack config not found");
        SlackDumpMessage(botToken, channel, ts, "[SLACK:READ]");
        return 0;
    }

    /// <summary>Dump full message as pretty JSON with inline comments for key fields.</summary>
    static void SlackDumpMessage(string botToken, string channel, string ts, string prefix)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
            var resp = http.GetStringAsync($"https://slack.com/api/conversations.replies?channel={channel}&ts={ts}&limit=1&inclusive=true").GetAwaiter().GetResult();
            var json = JsonSerializer.Deserialize<JsonNode>(resp);
            var msg = json?["messages"]?[0];
            if (msg == null) { Console.WriteLine($"{prefix} ts={ts} -- not found"); return; }

            Console.WriteLine($"{prefix} ts={ts}");

            // Pretty-print with inline annotations
            var pretty = msg.ToJsonString(new JsonSerializerOptions { WriteIndented = true });

            // Annotate known fields
            var annotations = new Dictionary<string, string>
            {
                ["\"username\""] = "// 표시 이름 (커스텀 가능)",
                ["\"user\""] = "// Slack user/bot ID",
                ["\"bot_id\""] = "// 봇 고유 ID",
                ["\"bot_profile\""] = "// 봇 프로필 (이름+아이콘)",
                ["\"icon_emoji\""] = "// 프로필 이모지 (:emoji:)",
                ["\"image_36\""] = "// 프로필 사진 36px",
                ["\"image_48\""] = "// 프로필 사진 48px",
                ["\"image_72\""] = "// 프로필 사진 72px",
                ["\"thread_ts\""] = "// 쓰레드 시작 ts (없으면 채널 글)",
                ["\"reply_count\""] = "// 댓글 수",
                ["\"reply_users\""] = "// 댓글 작성자 ID 목록",
                ["\"reply_users_count\""] = "// 댓글 작성자 수",
                ["\"latest_reply\""] = "// 마지막 댓글 ts",
                ["\"files\""] = "// 첨부파일 배열",
                ["\"reactions\""] = "// 리액션 이모지 목록",
                ["\"edited\""] = "// 수정 이력",
                ["\"subtype\""] = "// bot_message, thread_broadcast 등",
                ["\"text\""] = "// 메시지 본문",
                ["\"blocks\""] = "// 리치 텍스트 블록 (렌더링용)",
            };

            foreach (var line in pretty.Split('\n'))
            {
                var trimmed = line.TrimStart();
                var annotation = "";
                foreach (var (key, comment) in annotations)
                {
                    if (trimmed.StartsWith(key))
                    {
                        annotation = $"  {comment}";
                        break;
                    }
                }
                Console.WriteLine($"{line}{annotation}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{prefix} error: {ex.Message}");
        }
    }

    /// <summary>
    /// Check if a message is one of the last 2 messages by its author (username).
    /// Protected messages are fallback targets for message_limit append -- deleting them breaks the chain.
    /// </summary>
    static async Task<bool> IsProtectedLastMessageAsync(string botToken, string channel, string ts)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
            var resp = await http.GetStringAsync($"https://slack.com/api/conversations.history?channel={channel}&limit=30");
            var json = JsonSerializer.Deserialize<JsonNode>(resp);
            var msgs = json?["messages"]?.AsArray();
            if (msgs == null) return false;

            // Find the target message's username
            string? targetUser = null;
            foreach (var m in msgs)
            {
                if (m?["ts"]?.GetValue<string>() == ts)
                {
                    targetUser = m?["username"]?.GetValue<string>();
                    break;
                }
            }
            if (targetUser == null) return false; // message not found or no username

            // Count how many messages this author has (newest first)
            int authorCount = 0;
            foreach (var m in msgs)
            {
                if (m?["username"]?.GetValue<string>() == targetUser)
                {
                    authorCount++;
                    if (m?["ts"]?.GetValue<string>() == ts)
                        return authorCount <= 2; // protected if it's in the last 2
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SLACK] IsProtectedLastMessage error: {ex.Message}");
        }
        return false; // not found -> allow delete
    }

    /// <summary>
    /// Get the latest message in a channel: (ts, reply_count).
    /// Used to check if our status streaming message is still at the bottom and has no replies.
    /// </summary>
    static async Task<(string? ts, int replyCount, string? username, string? botId, string? text, string? iconEmoji)> GetChannelLatestMessageInfo(string botToken, string channel)
    {
        using var http = new HttpClient();
        using var req = new HttpRequestMessage(HttpMethod.Get,
            $"https://slack.com/api/conversations.history?channel={channel}&limit=1");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;
        if (!ok) return (null, 0, null, null, null, null);

        var messages = json?["messages"]?.AsArray();
        if (messages == null || messages.Count == 0) return (null, 0, null, null, null, null);

        var msg = messages[0];
        var ts = msg?["ts"]?.GetValue<string>();
        var replyCount = msg?["reply_count"]?.GetValue<int>() ?? 0;
        var username = msg?["username"]?.GetValue<string>();
        var botId = msg?["bot_id"]?.GetValue<string>();
        var text = msg?["text"]?.GetValue<string>();
        // icon_emoji: Slack returns message-level icon override under icons.emoji
        var iconEmoji = msg?["icons"]?["emoji"]?.GetValue<string>()
                     ?? msg?["icon_emoji"]?.GetValue<string>();
        return (ts, replyCount, username, botId, text, iconEmoji);
    }

    /// Backward-compat wrapper.
    static async Task<string?> GetChannelLatestMessageTs(string botToken, string channel)
    {
        var (ts, _, _, _, _, _) = await GetChannelLatestMessageInfo(botToken, channel);
        return ts;
    }

    /// <summary>
    /// Fetch the last N channel messages and find the most recent bot status message.
    /// Returns (ownTs: found our exact ts, adoptTs: first bot status ts found, botId).
    /// Used by PostOrUpdateAiStatusAsync to decide whether to edit-in-place or post new.
    /// </summary>
    static async Task<(bool ownFound, string? adoptTs, string? adoptBotId, bool isVeryLatest)> FindRecentBotStatus(
        string botToken, string channel, string? ourTs, int limit = 6)
    {
        try
        {
            using var http = new HttpClient();
            using var req = new HttpRequestMessage(HttpMethod.Get,
                $"https://slack.com/api/conversations.history?channel={channel}&limit={limit}");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

            var resp = await http.SendAsync(req);
            var body = await resp.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonNode>(body);
            if (!(json?["ok"]?.GetValue<bool>() ?? false)) return (false, null, null, false);

            var messages = json?["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return (false, null, null, false);

            var latestTs = messages[0]?["ts"]?.GetValue<string>();
            bool ownFound = ourTs != null && messages.Any(m => m?["ts"]?.GetValue<string>() == ourTs);
            if (ownFound) return (true, ourTs, null, latestTs == ourTs);

            // Find first bot message with profile icon (icon_emoji set) among last N messages
            for (int i = 0; i < messages.Count; i++)
            {
                var msg = messages[i];
                var botId     = msg?["bot_id"]?.GetValue<string>();
                var iconEmoji = msg?["icons"]?["emoji"]?.GetValue<string>()
                             ?? msg?["icon_emoji"]?.GetValue<string>();
                var ts = msg?["ts"]?.GetValue<string>();
                if (botId != null && iconEmoji != null && ts != null)
                    return (false, ts, botId, i == 0);
            }
        }
        catch { }
        return (false, null, null, false);
    }

    /// <summary>
    /// Check if a specific Slack message has any replies.
    /// Uses conversations.replies (more reliable than history reply_count field).
    /// Returns true on API failure (conservative: don't delete if uncertain).
    /// </summary>
    /// <summary>Get all replies for a thread (including starter at index 0). Returns null on error.</summary>
    static async Task<JsonArray?> SlackGetThreadRepliesAsync(string botToken, string channel, string messageTs)
    {
        try
        {
            using var http = new HttpClient();
            using var req = new HttpRequestMessage(HttpMethod.Get,
                $"https://slack.com/api/conversations.replies?channel={Uri.EscapeDataString(channel)}&ts={Uri.EscapeDataString(messageTs)}&limit=100");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
            var resp = await http.SendAsync(req);
            var body = await resp.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonNode>(body);
            if (json?["ok"]?.GetValue<bool>() != true) return null;
            return json?["messages"]?.AsArray();
        }
        catch { return null; }
    }

    /// <summary>
    /// Clean stale status messages from Slack channel.
    /// Per author: keep latest 2 status messages, delete older ones.
    /// For each stale message: clean bot-only replies first, then delete starter.
    /// Skips threads with human replies.
    /// </summary>
    // -- Channel history cache: avoid repeated API calls within short window --
    static JsonArray? _channelHistoryCache;
    static DateTime _channelHistoryCacheAt = DateTime.MinValue;
    static readonly TimeSpan ChannelHistoryCacheTtl = TimeSpan.FromSeconds(30);

    static async Task<JsonArray?> GetChannelHistoryCachedAsync(string botToken, string channel, int limit = 50)
    {
        if (_channelHistoryCache != null && (DateTime.UtcNow - _channelHistoryCacheAt) < ChannelHistoryCacheTtl)
            return _channelHistoryCache;
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
            var resp = await http.GetStringAsync(
                $"https://slack.com/api/conversations.history?channel={Uri.EscapeDataString(channel)}&limit={limit}");
            var json = JsonSerializer.Deserialize<JsonNode>(resp);
            if (json?["ok"]?.GetValue<bool>() != true) return null;
            _channelHistoryCache = json["messages"] as JsonArray;
            _channelHistoryCacheAt = DateTime.UtcNow;
            return _channelHistoryCache;
        }
        catch { return null; }
    }

    /// Invalidate cache after mutations (deletes) so next caller sees fresh state.
    static void InvalidateChannelHistoryCache() { _channelHistoryCache = null; }

    static async Task SlackCleanStaleStatusAsync(string botToken, string channel, int keepPerAuthor = 2)
    {
        try
        {
            var msgs = await GetChannelHistoryCachedAsync(botToken, channel);
            if (msgs == null) return;

            // Collect status messages per author (newest first -- Slack returns newest first)
            var statusByAuthor = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            var orphanReplies = new List<string>();

            foreach (var m in msgs)
            {
                var mTs = m?["ts"]?.GetValue<string>();
                var mUser = m?["username"]?.GetValue<string>();
                var mText = m?["text"]?.GetValue<string>() ?? "";
                if (mTs == null) continue;

                var threadTs2 = m?["thread_ts"]?.GetValue<string>();
                if (threadTs2 != null && threadTs2 != mTs &&
                    (mText == "This message was deleted." || m?["subtype"]?.GetValue<string>() == "tombstone"))
                { orphanReplies.Add(mTs); continue; }

                if (mUser != null && IsStatusEmoji(mText))
                {
                    if (!statusByAuthor.ContainsKey(mUser))
                        statusByAuthor[mUser] = new List<string>();
                    statusByAuthor[mUser].Add(mTs);
                }
            }

            // Clean orphan replies
            foreach (var ots in orphanReplies)
            {
                await SlackDeleteRawAsync(botToken, channel, ots);
                Console.Error.WriteLine($"[SLACK:GC] Cleaned orphan reply: {ots}");
                await Task.Delay(300);
            }

            // Per-author: keep latest N, delete older -- clean bot replies first so starter is deletable
            foreach (var (author, tsList) in statusByAuthor)
            {
                if (tsList.Count <= keepPerAuthor) continue;
                for (int si = keepPerAuthor; si < tsList.Count; si++)
                {
                    var staleTs = tsList[si];

                    var threadReplies = await SlackGetThreadRepliesAsync(botToken, channel, staleTs);
                    if (threadReplies != null && threadReplies.Count > 1)
                    {
                        bool hasHumanReply = false;
                        var botReplyTs = new List<string>();
                        for (int ri = 1; ri < threadReplies.Count; ri++)
                        {
                            var r = threadReplies[ri];
                            var rTs = r?["ts"]?.GetValue<string>();
                            if (rTs == null) continue;
                            if (r?["subtype"]?.GetValue<string>() != "bot_message" && r?["bot_id"] == null)
                            { hasHumanReply = true; break; }
                            botReplyTs.Add(rTs);
                        }
                        if (hasHumanReply)
                        {
                            Console.Error.WriteLine($"[SLACK:GC] Status {staleTs} has human reply -> skip");
                            continue;
                        }
                        foreach (var rTs in botReplyTs)
                        {
                            await SlackDeleteRawAsync(botToken, channel, rTs);
                            await Task.Delay(300);
                        }
                        if (botReplyTs.Count > 0)
                            Console.Error.WriteLine($"[SLACK:GC] Cleaned {botReplyTs.Count} bot replies from {staleTs}");
                    }

                    await SlackDeleteMessageAsync(botToken, channel, staleTs, guardThreadStarter: true);
                    Console.Error.WriteLine($"[SLACK:GC] Cleaned stale status for '{author}': {staleTs}");
                    await Task.Delay(300);
                }
            }

            // Invalidate cache after deletes so next call sees fresh state
            InvalidateChannelHistoryCache();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SLACK:GC] SlackCleanStaleStatusAsync failed: {ex.Message}");
        }
    }

    /// <summary>Raw chat.delete -- no guards, no audit. For cleaning bot replies before thread starter delete.</summary>
    static async Task SlackDeleteRawAsync(string botToken, string channel, string ts)
    {
        try
        {
            using var http = new HttpClient();
            var payload = JsonSerializer.Serialize(new { channel, ts });
            using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.delete");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
            req.Content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            await http.SendAsync(req);
        }
        catch { }
    }

    static async Task<bool> SlackMessageHasRepliesAsync(string botToken, string channel, string messageTs)
    {
        try
        {
            using var http = new HttpClient();
            using var req = new HttpRequestMessage(HttpMethod.Get,
                $"https://slack.com/api/conversations.replies?channel={Uri.EscapeDataString(channel)}&ts={Uri.EscapeDataString(messageTs)}&limit=2");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

            var resp = await http.SendAsync(req);
            var body = await resp.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonNode>(body);
            if (json?["ok"]?.GetValue<bool>() != true) return true; // API error -> assume has replies (don't delete)

            // conversations.replies returns the parent message + replies.
            // If count > 1, there is at least one reply.
            var messages = json?["messages"]?.AsArray();
            return (messages?.Count ?? 0) > 1;
        }
        catch { return true; } // Network/parse error -> assume has replies (don't delete)
    }

    // -- Pending Ack file-based IPC ------------------------------
    // Shared between AppBotEye (writes ack ts) and CLI (reads + deletes ack before replying)
    // File: wkappbot.hq/runtime/pending_acks.json
    // Format: { "threadTs": { "channel": "C...", "ackTs": "1234.5678" }, ... }

    static string PendingAcksFilePath
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime", "pending_acks.json");
        }
    }

    /// <summary>Save a pending ack ts for a thread (atomic write).</summary>
    static void SavePendingAck(string threadTs, string channel, string ackTs)
    {
        try
        {
            var all = LoadPendingAcks();
            all[threadTs] = new PendingAckEntry { Channel = channel, AckTs = ackTs };
            WritePendingAcks(all);
        }
        catch { /* best-effort */ }
    }

    /// <summary>Delete a pending ack for a thread and remove the Slack message. Returns true if deleted.</summary>
    static bool DeletePendingAckFromFile(string botToken, string threadTs)
    {
        try
        {
            var all = LoadPendingAcks();
            if (!all.TryGetValue(threadTs, out var entry)) return false;

            all.Remove(threadTs);
            WritePendingAcks(all);

            // Actually delete the Slack message
            var deleted = Task.Run(async () =>
                await SlackDeleteMessageAsync(botToken, entry.Channel, entry.AckTs, guardThreadStarter: false)).GetAwaiter().GetResult();
            if (deleted)
                Console.Error.WriteLine($"[SLACK] Deleted pending ack in thread {threadTs}");
            return deleted;
        }
        catch { return false; }
    }

    /// <summary>Delete ALL pending acks for a channel (cleanup after bot replies).</summary>
    static void DeleteAllPendingAcksInThread(string botToken, string threadTs)
    {
        DeletePendingAckFromFile(botToken, threadTs);
    }

    static Dictionary<string, PendingAckEntry> LoadPendingAcks()
    {
        try
        {
            var path = PendingAcksFilePath;
            if (!File.Exists(path)) return new();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, PendingAckEntry>>(json, _pendingAckJsonOpts) ?? new();
        }
        catch { return new(); }
    }

    static void WritePendingAcks(Dictionary<string, PendingAckEntry> data)
    {
        var path = PendingAcksFilePath;
        var dir = Path.GetDirectoryName(path)!;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        var json = JsonSerializer.Serialize(data, _pendingAckJsonOpts);
        var tmp = path + ".tmp";
        File.WriteAllText(tmp, json);
        File.Move(tmp, path, overwrite: true);
    }

    static readonly JsonSerializerOptions _pendingAckJsonOpts = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    sealed class PendingAckEntry
    {
        public string Channel { get; set; } = "";
        public string AckTs { get; set; } = "";
    }
}
