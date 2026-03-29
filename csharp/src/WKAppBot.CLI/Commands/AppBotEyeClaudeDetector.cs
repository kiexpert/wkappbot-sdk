// AppBotEyeClaudeDetector.cs — Claude Desktop UIA detection methods.
// Finds Claude Desktop window and detects its current status (executing, prompt ready, etc.)
// Extracted from AppBotEyeCommands.cs for readability.

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
// FlaUI using kept for DetectClaudeDesktopStatus/ExtractPlanContent — these methods run
// in MCP subprocess only (via claude-detect command). Eye calls ViaRoute wrappers which go through MCP.
// The FlaUI assembly loads lazily (only when these methods are JIT-compiled), which only happens in MCP subprocess.
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Vision;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    private static bool _claudeWindowSearchLogged = false; // log once to avoid spam on repeated calls

    /// <summary>
    /// Find Claude Desktop main window by walking the parent process tree.
    /// wkappbot → ... → claude.exe (Electron) — find its main window.
    /// Much faster than scanning all 14+ claude.exe processes!
    /// </summary>
    private static IntPtr FindClaudeDesktopWindow()
    {
        bool verbose = !_claudeWindowSearchLogged; // log only on first call

        // Walk up: wkappbot.exe → parent → grandparent → ... find claude.exe
        var claudePids = new HashSet<int>();
        try
        {
            int pid = Process.GetCurrentProcess().Id;
            for (int depth = 0; depth < 10; depth++)
            {
                var parentPid = GetParentPid(pid);
                if (parentPid <= 0 || parentPid == pid) break;

                try
                {
                    var parent = Process.GetProcessById(parentPid);
                    if (verbose)
                        Console.WriteLine($"[EYE] Ancestor: {parent.ProcessName} (PID={parentPid})");
                    if (parent.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                    {
                        claudePids.Add(parentPid);
                        // Also add siblings (Electron uses multiple processes under same parent tree)
                        // Get the parent's parent — the root claude.exe
                        var rootPid = GetParentPid(parentPid);
                        if (rootPid > 0)
                        {
                            try
                            {
                                var root = Process.GetProcessById(rootPid);
                                if (root.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                                    claudePids.Add(rootPid);
                            }
                            catch { }
                        }
                        break;
                    }
                    pid = parentPid;
                }
                catch { break; }
            }
        }
        catch { }

        // If ancestor walk didn't find claude.exe, try all claude processes (slow fallback)
        if (claudePids.Count == 0)
        {
            if (verbose)
                Console.WriteLine("[EYE] Ancestor walk didn't find claude.exe, scanning all...");
            foreach (var proc in Process.GetProcessesByName("claude"))
                claudePids.Add(proc.Id);
        }

        _claudeWindowSearchLogged = true;

        // Find the visible main window (with title bar + sizable) from claude PIDs
        IntPtr found = IntPtr.Zero;
        EnumWindows((hwnd, _) =>
        {
            if (!IsWindowVisible(hwnd)) return true;

            GetWindowThreadProcessId(hwnd, out var pid);
            if (!claudePids.Contains(pid)) return true;

            // Check for WS_CAPTION (main window with title bar)
            var style = GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            // Verify it's a sizable window (not a small popup or notification)
            if (GetWindowRect(hwnd, out var rect))
            {
                var w = rect.Right - rect.Left;
                var h = rect.Bottom - rect.Top;
                if (w > 400 && h > 300)
                {
                    found = hwnd;
                    return false; // stop enumeration
                }
            }

            return true;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>Get parent PID using NtQueryInformationProcess (fast, no WMI dependency).</summary>
    private static int GetParentPid(int pid)
    {
        try
        {
            var handle = OpenProcess(0x0400 /* PROCESS_QUERY_INFORMATION */, false, pid);
            if (handle == IntPtr.Zero)
                handle = OpenProcess(0x1000 /* PROCESS_QUERY_LIMITED_INFORMATION */, false, pid);
            if (handle == IntPtr.Zero) return -1;

            try
            {
                var pbi = new PROCESS_BASIC_INFORMATION();
                int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out _);
                if (status == 0)
                    return (int)pbi.InheritedFromUniqueProcessId;
            }
            finally
            {
                CloseHandle(handle);
            }
        }
        catch { }
        return -1;
    }

    // ── Plan Approval Integration ──────────────────────────────────

    /// <summary>
    /// Extract plan content. Two-tier strategy:
    ///   1st: File read (~/.claude/plans/*.md) — fast, full content
    ///   2nd: UIA text collection from Claude Desktop — environment-agnostic, accessibility standard
    /// Returns (source, content) or null if not found.
    /// </summary>
    static (string source, string content)? ExtractPlanContent(IntPtr claudeHwnd)
    {
        // ── Tier 1: File read (fast, full content) ──
        try
        {
            var plansDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "plans");

            if (Directory.Exists(plansDir))
            {
                var latestFile = Directory.GetFiles(plansDir, "*.md")
                    .OrderByDescending(f => File.GetLastWriteTimeUtc(f))
                    .FirstOrDefault();

                if (latestFile != null)
                {
                    var content = File.ReadAllText(latestFile);
                    if (!string.IsNullOrWhiteSpace(content))
                        return (Path.GetFileName(latestFile), content);
                }
            }
        }
        catch { /* fall through to UIA */ }

        // ── Tier 2: UIA text collection (accessibility standard, environment-agnostic) ──
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return null;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return null;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Collect all Text elements from the plan area
            // Plan content is between the approval button area and above
            var textElements = window.FindAllDescendants(cf.ByControlType(ControlType.Text));
            if (textElements == null || textElements.Length == 0) return null;

            var planLines = new StringBuilder();
            foreach (var elem in textElements)
            {
                try
                {
                    var text = elem.Name;
                    if (string.IsNullOrEmpty(text)) continue;

                    var rect = elem.BoundingRectangle;
                    // Skip off-screen elements and tiny elements
                    if (rect.Width < 10 || rect.Height < 5) continue;
                    // Skip UI labels (very short single words that aren't plan content)
                    if (text.Length < 3 && !text.StartsWith("#")) continue;

                    planLines.AppendLine(text.Trim());
                }
                catch { continue; }
            }

            var uiaContent = planLines.ToString().Trim();
            if (uiaContent.Length > 50) // minimal threshold for valid plan content
                return ("UIA", uiaContent);
        }
        catch { }

        return null;
    }

    /// <summary>
    /// Click the plan approval button via MCP a11y invoke.
    /// Searches for a Button containing "계획 승인" in its Name.
    /// Returns true if button was found and invoked.
    /// </summary>
    static bool ClickApproveButton(IntPtr claudeHwnd)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return false;

        try
        {
            var grap = $"*hwnd={claudeHwnd.ToInt64():X}*#*계획 승인*;*Approve*";
            var (output, exitCode) = EyeMcpClient.CallAsync(["a11y", "invoke", grap]).GetAwaiter().GetResult();
            if (exitCode == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[EYE] Plan approved via MCP: {output.TrimEnd()}");
                Console.ResetColor();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] ClickApproveButton error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Type text into the plan feedback edit field and submit via MCP.
    /// Used when Slack user wants to modify the plan instead of approving.
    /// </summary>
    static bool TypePlanFeedback(IntPtr claudeHwnd, string feedback)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return false;

        try
        {
            var hwndGrap = $"*hwnd={claudeHwnd.ToInt64():X}*";

            // Set value on the edit field
            var (output1, code1) = EyeMcpClient.CallAsync(
                ["a11y", "set-value", $"{hwndGrap}#*대신 수행*", "--text", feedback]).GetAwaiter().GetResult();
            if (code1 != 0) return false;

            // Submit: click the submit button
            var (output2, code2) = EyeMcpClient.CallAsync(
                ["a11y", "invoke", $"{hwndGrap}#*피드백 제출*"]).GetAwaiter().GetResult();
            if (code2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[EYE] Plan feedback submitted via MCP: \"{feedback}\"");
                Console.ResetColor();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] TypePlanFeedback error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Check if text is a plan approval keyword.
    /// Matches: "승인", "ㅇ", "V", "ok", "approve", "ㄱㄱ", "고", "넹", etc.
    /// </summary>
    static bool IsPlanApprovalKeyword(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        var t = text.Trim().ToLowerInvariant();
        return t is "승인" or "ㅇ" or "v" or "ok" or "approve" or "yes"
            or "ㄱㄱ" or "고" or "넹" or "넵" or "ㅇㅇ" or "확인"
            or "좋아" or "진행" or "시작" or "go" or "lgtm" or "ㅎㅎ";
    }

    /// <summary>
    /// Click a permission button by matching button name via MCP a11y invoke.
    /// Used when Slack user clicks a permission button (Allow/Deny/etc).
    /// Returns true if button was found and invoked.
    /// </summary>
    static bool ClickPermissionButton(IntPtr claudeHwnd, string buttonText)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return false;

        try
        {
            var grap = $"*hwnd={claudeHwnd.ToInt64():X}*#*{buttonText}*";
            var (output, exitCode) = EyeMcpClient.CallAsync(["a11y", "invoke", grap]).GetAwaiter().GetResult();
            if (exitCode == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[EYE] Permission button clicked via MCP: \"{buttonText}\"");
                Console.ResetColor();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] ClickPermissionButton error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Extract current permission button names from Claude Desktop via MCP a11y inspect.
    /// Returns list of button names for permission-related actions.
    /// </summary>
    static List<string> GetPermissionButtons(IntPtr claudeHwnd)
    {
        var result = new List<string>();
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return result;

        try
        {
            var grap = $"*hwnd={claudeHwnd.ToInt64():X}*";
            var (output, exitCode) = EyeMcpClient.CallAsync(
                ["a11y", "inspect", grap, "--depth", "10"], timeoutMs: 15_000).GetAwaiter().GetResult();
            if (exitCode != 0 || string.IsNullOrWhiteSpace(output)) return result;

            // Parse inspect output for button names matching permission patterns
            foreach (var line in output.Split('\n'))
            {
                var trimmed = line.Trim();
                if (!trimmed.Contains("[Button]", StringComparison.OrdinalIgnoreCase)) continue;

                // Extract button name from UIA inspect output: [Button] "ButtonName"
                var quoteStart = trimmed.IndexOf('"');
                var quoteEnd = quoteStart >= 0 ? trimmed.IndexOf('"', quoteStart + 1) : -1;
                if (quoteStart < 0 || quoteEnd < 0) continue;
                var name = trimmed.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                if (string.IsNullOrEmpty(name)) continue;

                if (name.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                    name.Contains("Deny", StringComparison.OrdinalIgnoreCase) ||
                    name.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                    name.Contains("거부", StringComparison.OrdinalIgnoreCase) ||
                    name.Contains("수락", StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(name);
                }
            }
        }
        catch { }

        return result;
    }

    // ── Claude Desktop UIA Status Detection ──────────────────────

    /// <summary>
    /// Detect Claude Desktop's current status via UIA tree inspection.
    /// Returns (status_key, display_text) tuple, or null if status unknown/idle.
    /// </summary>
    static Tuple<string, string>? DetectClaudeDesktopStatus(IntPtr claudeHwnd)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return null;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return null;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // 1. Check for "중단" (Stop) button → Claude is executing
            var stopButton = window.FindFirstDescendant(
                cf.ByControlType(ControlType.Button).And(cf.ByName("중단")));
            if (stopButton != null)
            {
                // Live response text → StatusBar fallback → generic "실행 중"
                var liveText = GetLiveResponseText(window, cf);
                var statusText = liveText ?? GetLatestStatusBarText(window, cf);
                return Tuple.Create("executing", statusText ?? "실행 중");
            }

            // 2. Check for rate limit FIRST (before turn-form!)
            // ★ Rate limit screen often still has turn-form visible → would be mis-detected as "prompt_ready"
            // Text: "You've hit your limit · resets 4pm (Asia/Seoul)"
            // ★ IMPORTANT: Must filter out AppBotEye's own overlay text!
            //   AppBotEye is a child window of Claude Desktop → its Text elements appear in FindAllDescendants
            //   If AppBotEye displays "rate limit" text → false positive self-detection loop!
            //   Solution: Search only inside "RootWebArea" Document (Claude's actual web content)
            try
            {
                // Find the RootWebArea Document first — limits search to Claude's web content only
                var rootWebArea = window.FindFirstDescendant(cf.ByAutomationId("RootWebArea"));
                var searchRoot = rootWebArea ?? window; // fallback to full window if not found
                var allTexts = searchRoot.FindAllDescendants(
                    cf.ByControlType(ControlType.Text));
                if (allTexts != null)
                {
                    foreach (var textElem in allTexts)
                    {
                        try
                        {
                            var name = textElem.Name;
                            if (string.IsNullOrEmpty(name)) continue;

                            // ★ Skip AppBotEye overlay text (prevents self-detection loop!)
                            // AppBotEye text elements are multi-line with URLs, action details, etc.
                            // Real rate limit text is a single short phrase like "You've hit your limit"
                            if (name.Contains("AppBot", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("navigate →", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("Claude:", StringComparison.OrdinalIgnoreCase) ||
                                name.Length > 200) // Real rate limit text is short
                                continue;

                            // Only match the exact Claude rate limit phrases
                            // "limit" + "reset" was too broad → matched AppBotEye overlay text!
                            if (name.Contains("hit your limit", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("You've hit your usage limit", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("한도를 초과", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("사용 한도", StringComparison.OrdinalIgnoreCase))
                            {
                                // Log the trigger text for debugging
                                Console.WriteLine($"[EYE] Rate limit trigger text: \"{(name.Length > 80 ? name[..80] + "…" : name)}\"");
                                // Try to extract reset time from this element or siblings
                                var resetTime = ParseResetTime(name);
                                if (resetTime == null)
                                {
                                    foreach (var sibling in allTexts)
                                    {
                                        try
                                        {
                                            var sibName = sibling.Name;
                                            if (!string.IsNullOrEmpty(sibName))
                                            {
                                                resetTime = ParseResetTime(sibName);
                                                if (resetTime != null) break;
                                            }
                                        }
                                        catch { }
                                    }
                                }

                                var displayText = resetTime != null
                                    ? $"한도 초과 — {resetTime.Value:HH:mm}에 리셋"
                                    : "한도 초과";

                                return Tuple.Create("rate_limit", displayText);
                            }
                        }
                        catch { continue; }
                    }
                }

                // ★ 2b. OCR fallback — Claude Desktop sometimes exposes NO UIA Text elements
                // When the UIA tree is sparse (no Text children under RootWebArea),
                // fall back to OCR screenshot analysis to detect rate limit text.
                // This handles Claude Desktop updates that change accessibility structure.
                if (allTexts == null || allTexts.Length == 0)
                {
                    var ocrResult = TryDetectRateLimitViaOcr(claudeHwnd);
                    if (ocrResult != null) return ocrResult;
                }
            }
            catch { }

            // 3. Check for plan approval dialog via Button search
            var approveButton = window.FindFirstDescendant(
                cf.ByControlType(ControlType.Button).And(cf.ByName("Approve")));
            if (approveButton != null)
                return Tuple.Create("plan_approval_pending", "계획승인 대기");

            // 3.5. Check for permission/approval prompt via Button search
            try
            {
                var buttons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
                if (buttons != null && buttons.Length > 0)
                {
                    var permButtons = new List<string>();
                    foreach (var btn in buttons)
                    {
                        try
                        {
                            var name = btn.Name;
                            if (string.IsNullOrEmpty(name)) continue;

                            if (name.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("Deny", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("거부", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("수락", StringComparison.OrdinalIgnoreCase))
                            {
                                permButtons.Add(name);
                            }
                        }
                        catch { continue; }
                    }

                    if (permButtons.Count >= 2)
                    {
                        var btnList = string.Join(" / ", permButtons);
                        return Tuple.Create("permission_prompt", $"수락 요구: [{btnList}]");
                    }
                }
            }
            catch { }

            // 4. ★ turn-form Name fallback — Electron webview buttons often aren't exposed as UIA buttons
            // turn-form Name contains concatenated text of all child elements, e.g.:
            //   " 메뉴 토글 권한 요청 Opus 4.6 중단"  → executing + permission prompt
            //   " 메뉴 토글 Opus 4.6"                 → prompt ready (normal)
            //   "계획" button visible nearby           → plan approval pending
            var turnForm = window.FindFirstDescendant(
                cf.ByAutomationId("turn-form"));
            if (turnForm != null)
            {
                try
                {
                    var rect = turnForm.BoundingRectangle;
                    if (rect.Height > 30)
                    {
                        var turnFormName = turnForm.Name ?? "";

                        // 4a. "중단" in turn-form Name → executing (Button search missed it)
                        if (turnFormName.Contains("중단"))
                        {
                            // Also check for permission prompt within executing state
                            if (turnFormName.Contains("권한 요청") || turnFormName.Contains("권한요청"))
                            {
                                return Tuple.Create("permission_prompt", "권한 요청 대기 (실행 중)");
                            }
                            var liveText = GetLiveResponseText(window, cf);
                            var statusText = liveText ?? GetLatestStatusBarText(window, cf);
                            return Tuple.Create("executing", statusText ?? "실행 중");
                        }

                        // 4b. "권한 요청" in turn-form Name → permission prompt
                        if (turnFormName.Contains("권한 요청") || turnFormName.Contains("권한요청"))
                        {
                            return Tuple.Create("permission_prompt", "권한 요청 대기");
                        }

                        // 4c. Check for plan approval: look for "계획" button nearby
                        // Claude Desktop shows "계획" tab/button when in plan mode
                        try
                        {
                            var planButtons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
                            if (planButtons != null)
                            {
                                bool hasPlanButton = false;
                                bool hasApproveKorean = false;
                                foreach (var btn in planButtons)
                                {
                                    try
                                    {
                                        var name = btn.Name;
                                        if (string.IsNullOrEmpty(name)) continue;
                                        if (name.Contains("계획 승인")) hasApproveKorean = true;
                                        if (name == "계획") hasPlanButton = true;
                                    }
                                    catch { }
                                }
                                if (hasApproveKorean)
                                    return Tuple.Create("plan_approval_pending", "계획승인 대기");
                                if (hasPlanButton)
                                    return Tuple.Create("plan_mode", "계획 모드 (피드백 입력 대기)");
                            }
                        }
                        catch { }

                        // 4d. Normal prompt ready
                        return Tuple.Create("prompt_ready", "프롬프트 입력 대기");
                    }
                }
                catch { }
            }

            // 5. Idle — no special state detected
            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Get the most recent StatusBar text from Claude Desktop's UIA tree.
    /// Returns text like "파일 읽음", "명령 실행함", "도구 사용함".
    /// </summary>
    private static string? GetLatestStatusBarText(
        FlaUI.Core.AutomationElements.AutomationElement window, ConditionFactory cf)
    {
        try
        {
            var statusBars = window.FindAllDescendants(cf.ByControlType(ControlType.StatusBar));
            if (statusBars == null || statusBars.Length == 0) return null;

            string? latestText = null;
            int maxY = int.MinValue;

            foreach (var sb in statusBars)
            {
                try
                {
                    var rect = sb.BoundingRectangle;
                    if (rect.Y < 0 || rect.Height < 1) continue;

                    var textChild = sb.FindFirstChild(cf.ByControlType(ControlType.Text));
                    if (textChild == null) continue;

                    var text = textChild.Name;
                    if (string.IsNullOrEmpty(text)) continue;

                    if (rect.Y > maxY)
                    {
                        maxY = (int)rect.Y;
                        latestText = text;
                    }
                }
                catch { continue; }
            }

            return latestText;
        }
        catch { return null; }
    }

    /// <summary>
    /// Read the live response text from an AI conversation window (Claude Desktop / VS Code / Codex).
    /// Returns the last non-empty line of the bottommost text above the turn-form (= current response tail).
    /// This changes in real-time as AI generates — used for streaming to Slack.
    /// </summary>
    private static string? GetLiveResponseText(
        FlaUI.Core.AutomationElements.AutomationElement window, ConditionFactory cf)
    {
        try
        {
            var turnForm = window.FindFirstDescendant(cf.ByAutomationId("turn-form"));
            if (turnForm == null) return null;
            var tfRect = turnForm.BoundingRectangle;
            if (tfRect.Height < 10) return null;

            // Walk up to the scroll container, then search immediate children above turn-form
            var parent = turnForm.Parent;
            if (parent == null) return null;
            var children = parent.FindAllChildren();
            if (children == null || children.Length == 0) return null;

            string? lastText = null;
            float bestY = float.MinValue;
            foreach (var child in children)
            {
                try
                {
                    var r = child.BoundingRectangle;
                    if (r.Bottom > tfRect.Top + 10) continue; // skip turn-form and below
                    if (r.Y <= bestY) continue;
                    var name = child.Name;
                    if (string.IsNullOrWhiteSpace(name) || name.Length < 4) continue;
                    bestY = r.Y;
                    lastText = name;
                }
                catch { }
            }

            if (lastText == null) return null;

            // Extract the last non-empty line — "방송" view should show the most recent output tail
            var lines = lastText.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var lastLine = lines.LastOrDefault(l => l.Length >= 4) ?? lastText.Trim();
            if (string.IsNullOrWhiteSpace(lastLine)) return null;
            return lastLine.Length > 120 ? lastLine[..120] + "…" : lastLine;
        }
        catch { return null; }
    }

    // ── OCR-based rate limit detection ──────────────────────────────────────
    // Throttle: max once per 5 seconds to avoid performance overhead in the polling loop.
    private static DateTime _lastOcrRateLimitCheck = DateTime.MinValue;
    private static readonly TimeSpan OcrRateLimitInterval = TimeSpan.FromSeconds(5);

    /// <summary>
    /// OCR fallback for rate limit detection when UIA tree has no Text elements.
    /// Captures the Claude Desktop window via PrintWindow and runs OCR to find
    /// "hit your limit" / "resets Xam/pm" text.
    /// Throttled to max once per 5 seconds to avoid performance overhead.
    /// </summary>
    static Tuple<string, string>? TryDetectRateLimitViaOcr(IntPtr claudeHwnd)
    {
        // Throttle: skip if checked recently
        if (DateTime.UtcNow - _lastOcrRateLimitCheck < OcrRateLimitInterval)
            return null;
        _lastOcrRateLimitCheck = DateTime.UtcNow;

        try
        {
            // Capture window via PrintWindow (Z-order safe, focusless)
            using var bitmap = ScreenCapture.CaptureWindow(claudeHwnd);
            if (bitmap == null || ScreenCapture.IsBlankBitmap(bitmap))
                return null;

            // Run OCR — crop upper portion only (rate limit banner is at top)
            // Cropping to top 40% reduces OCR time significantly
            var cropHeight = Math.Min(bitmap.Height * 2 / 5, bitmap.Height);
            using var topCrop = bitmap.Clone(
                new Rectangle(0, 0, bitmap.Width, cropHeight),
                bitmap.PixelFormat);

            var ocr = new SimpleOcrAnalyzer();
            var ocrResult = ocr.RecognizeAll(topCrop).GetAwaiter().GetResult();
            if (ocrResult == null || string.IsNullOrEmpty(ocrResult.FullText))
                return null;

            var fullText = ocrResult.FullText;

            // Check for rate limit phrases in OCR text
            if (fullText.Contains("hit your limit", StringComparison.OrdinalIgnoreCase) ||
                fullText.Contains("hit your usage limit", StringComparison.OrdinalIgnoreCase) ||
                fullText.Contains("한도를 초과", StringComparison.OrdinalIgnoreCase) ||
                fullText.Contains("사용 한도", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[EYE] Rate limit trigger (OCR): \"{(fullText.Length > 120 ? fullText[..120] + "…" : fullText)}\"");
                // Extract reset time from OCR text
                var resetTime = ParseResetTime(fullText);
                var displayText = resetTime != null
                    ? $"한도 초과 — {resetTime.Value:HH:mm}에 리셋"
                    : "한도 초과";

                return Tuple.Create("rate_limit", displayText);
            }
        }
        catch { /* OCR is best-effort fallback */ }

        return null;
    }

    /// <summary>
    /// Parse reset time from rate limit text.
    /// Patterns: "resets 4pm", "resets 4pm (Asia/Seoul)", "resets 16:00"
    /// Returns local DateTime or null.
    /// </summary>
    static DateTime? ParseResetTime(string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        // Pattern 1: "resets 4pm" or "resets 4 pm" with optional timezone
        var match = Regex.Match(text,
            @"resets?\s+(\d{1,2})\s*(am|pm)\s*(?:\(([^)]+)\))?",
            RegexOptions.IgnoreCase);
        if (match.Success)
        {
            int hour = int.Parse(match.Groups[1].Value);
            bool isPm = match.Groups[2].Value.Equals("pm", StringComparison.OrdinalIgnoreCase);
            if (isPm && hour < 12) hour += 12;
            else if (!isPm && hour == 12) hour = 0;

            // ★ DO NOT roll over to next day! The reset time is for TODAY.
            // If it already passed, return it as-is so callers can detect it's in the past.
            var resetTime = DateTime.Today.AddHours(hour);
            return resetTime;
        }

        // Pattern 2: "resets 16:00" (24-hour format)
        match = Regex.Match(text, @"resets?\s+(\d{1,2}):(\d{2})");
        if (match.Success)
        {
            int hour = int.Parse(match.Groups[1].Value);
            int min = int.Parse(match.Groups[2].Value);
            // ★ DO NOT roll over to next day!
            var resetTime = DateTime.Today.AddHours(hour).AddMinutes(min);
            return resetTime;
        }

        return null;
    }

    /// <summary>
    /// Get the parsed reset time from a rate_limit status display text.
    /// Extracts "HH:mm" from "한도 초과 — 16:00에 리셋".
    /// Returns null if not parseable.
    /// </summary>
    static DateTime? GetResetTimeFromDisplayText(string? displayText)
    {
        if (string.IsNullOrEmpty(displayText)) return null;

        var match = Regex.Match(displayText, @"(\d{1,2}:\d{2})에 리셋");
        if (match.Success && TimeOnly.TryParse(match.Groups[1].Value, out var time))
        {
            // ★ DO NOT roll over to next day — return today's time so callers can detect it's past
            var resetDt = DateTime.Today.Add(time.ToTimeSpan());
            return resetDt;
        }
        return null;
    }

    // ── Spinner pixel detection (idle via animation absence) ──────────────────
    // ★ Uses PrintWindow on Chrome_RenderWidgetHostHWND sub-window
    //   → works even when Claude Desktop is covered by other windows or unfocused!
    //   CopyFromScreen fails when covered. PrintWindow on Electron main window captures
    //   only native chrome (title bar). But the render sub-window captures GPU web content!

    // Legacy single-instance spinner state (unused when ClaudeInstanceState is passed)
    static uint _lastSpinnerHash;
    static int _consecutiveNoChange;
    static IntPtr _cachedRenderHwnd;  // cached Chrome_RenderWidgetHostHWND

    /// <summary>
    /// Detect Claude Desktop spinner/animation above the prompt area.
    /// Uses PrintWindow on Chrome_RenderWidgetHostHWND for focusless, Z-order-safe capture.
    /// Samples a strip of 100 pixels above turn-form. If pixels are unchanged
    /// for 2+ consecutive calls (1s apart) → spinner stopped → Claude idle.
    /// Returns: true = idle detected, false = still animating or can't detect.
    /// </summary>
    static bool DetectSpinnerIdle(IntPtr claudeHwnd, ClaudeInstanceState? state = null)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd)) return false;

        // Use per-instance state if provided, otherwise fall back to static fields
        ref uint lastHash = ref (state != null ? ref state.LastSpinnerHash : ref _lastSpinnerHash);
        ref int noChange = ref (state != null ? ref state.ConsecutiveNoChange : ref _consecutiveNoChange);
        ref IntPtr renderHwnd = ref (state != null ? ref state.CachedRenderHwnd : ref _cachedRenderHwnd);

        try
        {
            // Find render sub-window (cached for performance)
            if (renderHwnd == IntPtr.Zero || !IsWindow(renderHwnd))
            {
                renderHwnd = NativeMethods.FindWindowExW(
                    claudeHwnd, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null);
                if (renderHwnd == IntPtr.Zero) return false;
            }

            // Get render widget screen position (for coordinate conversion)
            if (!NativeMethods.GetWindowRect(renderHwnd, out var renderRect))
                return false;
            int renderW = renderRect.Right - renderRect.Left;
            int renderH = renderRect.Bottom - renderRect.Top;
            if (renderW <= 0 || renderH <= 0) return false;

            // Use cached turn-form rect from ClaudePromptHelper (avoids re-scanning)
            var cachedPrompt = FindAllPromptsViaMcp()
                .FirstOrDefault(p => p.WindowHandle == claudeHwnd);
            System.Drawing.RectangleF tfRect;
            if (cachedPrompt != null)
            {
                tfRect = new System.Drawing.RectangleF(
                    cachedPrompt.PromptRect.X, cachedPrompt.PromptRect.Y,
                    cachedPrompt.PromptRect.Width, cachedPrompt.PromptRect.Height);
            }
            else
            {
                // Fallback: scan via UIA
                using var automation = new UIA3Automation();
                var window = automation.FromHandle(claudeHwnd);
                if (window == null) return false;
                var cf = new ConditionFactory(new UIA3PropertyLibrary());
                var turnForm = window.FindFirstDescendant(cf.ByAutomationId("turn-form"));
                if (turnForm == null) return false;
                tfRect = turnForm.BoundingRectangle;
            }
            if (tfRect.Height < 10) return false;

            // Convert screen coordinates to render-widget-local coordinates
            int sampleW = 100;
            int screenX = (int)(tfRect.Left + (tfRect.Width / 2)) - (sampleW / 2);
            int screenY = (int)tfRect.Top - 60;
            int localX = screenX - renderRect.Left;
            int localY = screenY - renderRect.Top;

            // Bounds check
            if (localX < 0 || localY < 0 || localX + sampleW > renderW || localY >= renderH)
                return false;

            // PrintWindow the full render widget → extract spinner strip
            uint hash;
            using (var fullBmp = new Bitmap(renderW, renderH, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(fullBmp))
                {
                    IntPtr hdc = g.GetHdc();
                    bool ok = NativeMethods.PrintWindow(
                        renderHwnd, hdc, NativeMethods.PW_RENDERFULLCONTENT);
                    g.ReleaseHdc(hdc);
                    if (!ok) return false;
                }

                // FNV-1a hash of 100 pixels at spinner line
                hash = 2166136261u;
                for (int px = 0; px < sampleW; px++)
                    hash = (hash ^ (uint)fullBmp.GetPixel(localX + px, localY).ToArgb()) * 16777619u;
            }

            if (hash == lastHash)
            {
                noChange++;
                if (noChange >= 2)
                    return true; // 2+ consecutive same → no animation → idle
            }
            else
            {
                noChange = 0;
                lastHash = hash;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Reset spinner detection state (e.g., on active recovery).</summary>
    static void ResetSpinnerDetection(ClaudeInstanceState? state = null)
    {
        if (state != null)
        {
            state.LastSpinnerHash = 0;
            state.ConsecutiveNoChange = 0;
            state.CachedRenderHwnd = IntPtr.Zero;
        }
        else
        {
            _lastSpinnerHash = 0;
            _consecutiveNoChange = 0;
            _cachedRenderHwnd = IntPtr.Zero;
        }
    }

    // ── claude-detect CLI command — runs status detection in MCP subprocess ──

    /// <summary>
    /// CLI entry: wkappbot claude-detect &lt;hwnd-hex&gt; [--plan]
    /// Returns JSON: {"status":"executing","text":"실행 중"} or {"status":null}
    /// With --plan: {"source":"file","content":"plan text..."} or {"source":null}
    /// </summary>
    static int ClaudeDetectCommand(string[] args)
    {
        if (args.Length == 0) { Console.WriteLine("{\"status\":null}"); return 0; }

        var hwndStr = args[0].TrimStart('0', 'x').TrimStart('0', 'X');
        if (!long.TryParse(hwndStr, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal))
        {
            Console.WriteLine("{\"error\":\"invalid hwnd\"}");
            return 1;
        }
        var hwnd = new IntPtr(hwndVal);

        bool planMode = args.Contains("--plan");

        if (planMode)
        {
            var plan = ExtractPlanContent(hwnd);
            if (plan != null)
                Console.WriteLine(JsonSerializer.Serialize(new { source = plan.Value.source, content = plan.Value.content }));
            else
                Console.WriteLine("{\"source\":null}");
        }
        else
        {
            var result = DetectClaudeDesktopStatus(hwnd);
            if (result != null)
                Console.WriteLine(JsonSerializer.Serialize(new { status = result.Item1, text = result.Item2 }));
            else
                Console.WriteLine("{\"status\":null}");
        }
        return 0;
    }

    // ── find-prompts CLI command — runs FindAllPrompts in MCP subprocess ──

    /// <summary>
    /// CLI entry: wkappbot find-prompts [--all]
    /// Returns JSON array: [{"hwnd":"0x791B16","title":"...","host":"vscode-claudecode","pid":1234,"cwd":"..."}]
    /// Uses static ClaudePromptHelper — cached UIA data persists across calls in MCP subprocess.
    /// </summary>
    private static ClaudePromptHelper? _mcpPromptHelper;
    private static readonly object _mcpPromptHelperLock = new();

    static int FindPromptsCommand(string[] args)
    {
        // Reuse static instance — UIA per-hwnd cache persists across MCP calls
        lock (_mcpPromptHelperLock)
        {
            _mcpPromptHelper ??= new ClaudePromptHelper();
        }
        var all = _mcpPromptHelper.FindAllPrompts();
        var result = all.Select(p => new
        {
            hwnd = $"0x{p.WindowHandle.ToInt64():X}",
            title = p.WindowTitle,
            host = p.HostType,
            process = p.ProcessName,
            rect = new { x = p.PromptRect.X, y = p.PromptRect.Y, w = p.PromptRect.Width, h = p.PromptRect.Height }
        });
        Console.WriteLine(JsonSerializer.Serialize(result));
        return 0;
    }

    /// <summary>
    /// MCP-routed FindAllPrompts: calls find-prompts via MCP subprocess.
    /// Returns lightweight PromptInfo-compatible list without loading FlaUI in Eye.
    /// Caches results for 2 seconds (Eye polling cycle).
    /// </summary>
    private static List<ClaudePromptHelper.PromptInfo>? _mcpPromptCache;
    private static DateTime _mcpPromptCacheAt = DateTime.MinValue;

    // Background find-prompts: runs on worker thread, never blocks MCP main thread
    private static volatile bool _findPromptsRunning;

    internal static List<ClaudePromptHelper.PromptInfo> FindAllPromptsViaMcp(bool forceRefresh = false)
    {
        // Cache: return immediately, refresh in background if stale
        if (!forceRefresh && _mcpPromptCache != null && (DateTime.UtcNow - _mcpPromptCacheAt).TotalSeconds < 10)
            return _mcpPromptCache;

        if (!EyeMcpClient.IsRunning)
            return _mcpPromptCache ?? new List<ClaudePromptHelper.PromptInfo>();

        // Fire-and-forget background refresh — never blocks caller
        if (!_findPromptsRunning)
        {
            _findPromptsRunning = true;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try { FindAllPromptsViaMcpCore(); }
                finally { _findPromptsRunning = false; }
            });
        }
        return _mcpPromptCache ?? new List<ClaudePromptHelper.PromptInfo>();
    }

    private static void FindAllPromptsViaMcpCore()
    {
        try
        {
            // Dedicated worker: spawn separate process (NOT through MCP pipe — that blocks user commands)
            var core = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(Environment.ProcessPath) ?? ".", "wkappbot-core.exe");
            using var spawn = AppBotPipe.Spawn(core, "find-prompts", Environment.CurrentDirectory,
                redirectStdOut: true, redirectStdErr: true, env: new() { ["WKAPPBOT_WORKER"] = "1" }, caller: "FIND-PROMPTS");
            if (spawn == null) return;
            var output = spawn.StdOut!.ReadToEnd();
            var stderr = spawn.StdErr?.ReadToEnd() ?? "";
            spawn.WaitForExit(60_000); // FlaUI cold-start takes ~25s
            var code = spawn.HasExited ? spawn.ExitCode : 1;
            if (!string.IsNullOrWhiteSpace(stderr))
                Console.Error.WriteLine($"[FIND-PROMPTS] stderr: {stderr.Trim()[..Math.Min(200, stderr.Trim().Length)]}");
            if (code != 0 || string.IsNullOrWhiteSpace(output))
            {
                Console.Error.WriteLine($"[FIND-PROMPTS] exit={code} stdout={output?.Length ?? 0}chars");
                return;
            }

            var arr = JsonNode.Parse(output) as JsonArray;
            if (arr == null)
                return;

            var result = new List<ClaudePromptHelper.PromptInfo>();
            foreach (var item in arr)
            {
                if (item == null) continue;
                var hwndStr = item["hwnd"]?.GetValue<string>() ?? "0";
                var hwndVal = long.Parse(hwndStr.TrimStart('0', 'x').TrimStart('0', 'X'),
                    System.Globalization.NumberStyles.HexNumber);
                var title = item["title"]?.GetValue<string>() ?? "";
                var host = item["host"]?.GetValue<string>() ?? "";
                var pid = item["pid"]?.GetValue<int>() ?? 0;
                var rx = item["rect"]?["x"]?.GetValue<int>() ?? 0;
                var ry = item["rect"]?["y"]?.GetValue<int>() ?? 0;
                var rw = item["rect"]?["w"]?.GetValue<int>() ?? 0;
                var rh = item["rect"]?["h"]?.GetValue<int>() ?? 0;

                result.Add(new ClaudePromptHelper.PromptInfo(
                    new IntPtr(hwndVal), title, "", // ProcessName not needed for Eye routing
                    new System.Drawing.Rectangle(rx, ry, rw, rh), host));
            }

            _mcpPromptCache = result;
            _mcpPromptCacheAt = DateTime.UtcNow;
        }
        catch { }
    }

    /// <summary>
    /// MCP-routed wrapper: calls DetectClaudeDesktopStatus via MCP subprocess when in Eye.
    /// Falls back to direct call when not in Eye (e.g., running in MCP subprocess itself).
    /// </summary>
    internal static Tuple<string, string>? DetectClaudeDesktopStatusViaRoute(IntPtr claudeHwnd)
    {
        if (!RunningInEye || !EyeMcpClient.IsRunning)
            return DetectClaudeDesktopStatus(claudeHwnd);

        try
        {
            var hwndHex = claudeHwnd.ToInt64().ToString("X");
            var (output, code) = EyeMcpClient.CallAsync(["claude-detect", hwndHex], timeoutMs: 10_000).GetAwaiter().GetResult();
            if (code != 0 || string.IsNullOrWhiteSpace(output)) return null;

            var json = JsonNode.Parse(output);
            var status = json?["status"]?.GetValue<string>();
            if (status == null) return null;
            var text = json?["text"]?.GetValue<string>() ?? "";
            return Tuple.Create(status, text);
        }
        catch { return null; }
    }

    /// <summary>
    /// MCP-routed wrapper: calls ExtractPlanContent via MCP subprocess when in Eye.
    /// </summary>
    internal static (string source, string content)? ExtractPlanContentViaRoute(IntPtr claudeHwnd)
    {
        if (!RunningInEye || !EyeMcpClient.IsRunning)
            return ExtractPlanContent(claudeHwnd);

        try
        {
            var hwndHex = claudeHwnd.ToInt64().ToString("X");
            var (output, code) = EyeMcpClient.CallAsync(["claude-detect", hwndHex, "--plan"], timeoutMs: 10_000).GetAwaiter().GetResult();
            if (code != 0 || string.IsNullOrWhiteSpace(output)) return null;

            var json = JsonNode.Parse(output);
            var source = json?["source"]?.GetValue<string>();
            if (source == null) return null;
            var content = json?["content"]?.GetValue<string>() ?? "";
            return (source, content);
        }
        catch { return null; }
    }
}
